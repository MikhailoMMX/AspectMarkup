using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QUT.Gppg;

namespace ParserGenerator
{
    /* TODO
     * Починить обработку директив препроцессора
    */

    public class Engine
    {
        SymbolTable symbolTable = new SymbolTable();

        /// <summary>
        /// Самый главный метод
        /// </summary>
        /// <param name="source"></param>
        public SymbolTable ParseSourceFile(SourceFile source)
        {
            RuleDeclaration.ParsingStage = false;
            if (Options.ExtensionList.Count > 0)
                Options.Extension = (Options.ExtensionList[0]);
            else
                Options.Extension = StringConstants.DefaultExtension;
            if (Options.Extension == "\"*\"")
                Options.Extension = "\"any\"";
            Options.BaseClassName = Options.Extension.Substring(1, Options.Extension.Length - 2).ToUpper() + StringConstants.PPGBaseClassName;

            //добавляем предопределенные сущности: error, состояния пропуска по директиве
            AddPredefinedEntities();

            //Переносим сущности из исходного файла в таблицу символов
            AddSourceEntitiesToSymbolTable(source);

            //Проверяем наличие всех используемых имен
            CheckExistenceOfUsedNames();
            if (ErrorReporter.ErrorCount != 0)
                return null;

            //преобразуем правила в лексемы, если правило состоит только из нескольких веток по 1 лексеме каждая
            SimplifyTokenOnlyRules();
           
            //чтобы не генерировать ненужные узлы для дерева, помечаем безымянные правила(без символов @) как "сгенерированные"
            MarkUnnamedRules();
            //и помечаем правила, не использующиеся нигде со знаком # как не образующие сущность
            MarkRulesWithoutNode();

            //поиск и преобразование однородных наборов (List)
            ReplaceUniformListsWithRules();

            //находим строковые литералы в ветках правил, добавляем их к лексемам.
            DeclareInlinedTokens();

            //в директивах пропуска заменяем строковые литералы на лексемы
            InlineTokensInUnnamedSkips();

            //Ищем все вхождения ANY/ANYEXCEPT и генерируем для них правила.
            ConvertANYToRules();

            //Избавляемся от квадратных скобок, создавая вместо них правила
            ConvertComplexSubruleParts();
            //Избавляемся от знаков повторения (+*?), создавая правила для помеченных ими символов
            ExpandRules();

            //Дописываем к правилам действия
            AddActionsToRules();

            //Генерируем предопределенные правила с предопределенными действиями, если надо.
            GeneratePredefinedRules();

            return symbolTable;
        }

        private List<string> GetLiteralsFromBranch(List<SubRulePart> branch)
        {
            List<string> res = new List<string>();
            foreach (SubRulePart srp in branch)
            {
                if (srp is SubRuleComplexPart)
                    foreach (List<SubRulePart> br2 in (srp as SubRuleComplexPart).Items)
                        res.AddRange(GetLiteralsFromBranch(br2));
                else if (srp.Name != null && srp.Name.StartsWith("\""))
                    res.Add(srp.Name);
            }
            return res;
        }
        private Dictionary<string, int> GetLiteralUsesCount()
        {
            Dictionary<string, int> res = new Dictionary<string,int>();
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                foreach (List<SubRulePart> branch in rd.SubRules)
                {
                    List<string> lst = GetLiteralsFromBranch(branch);
                    foreach (string str in lst)
                        if (res.ContainsKey(str))
                            res[str] += 1;
                        else
                            res.Add(str, 1);
                }
            return res;
        }
        private void SimplifyTokenOnlyRules()
        {
            List<string> RemoveRules = new List<string>();
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
            {
                bool flag = true;
                foreach (List<SubRulePart> branch in rd.SubRules)
                {
                    if (branch.Count != 1)
                        flag = false;
                    else if (branch[0].GetType() != typeof(SubRulePart))
                        flag = false;
                    else if (!branch[0].Name.StartsWith("\""))
                        flag = false;
                    else if (branch[0].Repetition != SubRuleRepetition.Once)
                        flag = false;
                }
                if (flag)
                    RemoveRules.Add(rd.Name);
            }
            Dictionary<string, int> Literals = GetLiteralUsesCount();
            foreach (string str in RemoveRules)
            {
                bool flag = true;
                RuleDeclaration rd = symbolTable._rules[str];
                foreach (List<SubRulePart> branch in rd.SubRules)
                    if (Literals[branch[0].Name] != 1)
                        flag = false;
                if (!flag)
                    continue;
                TokenDeclaration td = new TokenDeclaration();
                td.Name = rd.Name;
                td.Location = rd.Location;
                foreach (List<SubRulePart> branch in rd.SubRules)
                    td.Values.Add(branch[0].Name);
                symbolTable._rules.Remove(str);
                symbolTable._allNames.Remove(str);
                symbolTable.AddToken(td);
            }
        }

        /// <summary>
        /// Флаг для обнаружения нескольких вхождений List в одной ветке правила
        /// </summary>
        private bool isFirstTimeListEntity = true;
        private void ReplaceUniformListsWithRules(List<SubRulePart> branch, string RuleName, LexLocation Location)
        {
            //перебираем все символы ветки, на скобках запускаем рекурсию
            for (int i = 0; i < branch.Count; ++i )
                if (branch[i] is SubRuleComplexPart)
                    foreach (List<SubRulePart> subBranch in (branch[i] as SubRuleComplexPart).Items)
                        ReplaceUniformListsWithRules(subBranch, RuleName, Location);
                else if (branch[i] is SubRuleNonTermList)
                {
                    SubRuleNonTermList NTList = branch[i] as SubRuleNonTermList;
                    // Проверяем, первое ли это вхождение List в ветке правила
                    if (!isFirstTimeListEntity)
                    {
                        ErrorReporter.WriteError(ErrorMessages.MultipleListsNotAllowed, Location, RuleName);
                        return;
                    }
                    isFirstTimeListEntity = false;
                    //проверяем отсутствие после него знаков повторения
                    if (branch[i].Repetition != SubRuleRepetition.Once)
                    {
                        ErrorReporter.WriteError(ErrorMessages.RepetitionAfterListNotAllowed, Location, RuleName);
                        return;
                    }
                    //проверяем первый параметр - должно быть имя правила
                    if (symbolTable.FindRuleDescriptor(NTList.Name) == null)
                    {
                        ErrorReporter.WriteError(ErrorMessages.WrongFirstParamInList, Location, RuleName);
                        return;
                    }
                    bool SeparatorEmpty = NTList.Separator == null || NTList.Separator == "";
                    //проверяем второй параметр - должен быть разделитель - строка или имя лексемы
                    if (!SeparatorEmpty && !NTList.Separator.StartsWith("\""))
                        if (symbolTable.FindTokenList(NTList.Separator) == null)
                        {
                            ErrorReporter.WriteError(ErrorMessages.WrongFirstParamInList, Location, RuleName);
                            return;
                        }
                        else
                            NTList.Separator = symbolTable.FindTokenList(NTList.Separator).Values[0];

                   

                    //строим новое правило
                    RuleDeclaration newRule = new RuleDeclaration();
                    newRule.Name = symbolTable.GetNewName(RuleName + StringConstants.tkUniformSetSuffix);
                    newRule.Location = Location;
                    newRule.isActionGenerated = true;
                    newRule.isGeneratedRule = true;
                    newRule.UseRuleName = false;
                    newRule.Type = StringConstants.SourceEntityUniformSetClassName;
                    List<SubRulePart> Branch1 = new List<SubRulePart>();
                    List<SubRulePart> Branch2 = new List<SubRulePart>();
                    string action;
                    string Sep1 = SeparatorEmpty ? "\"\"" : NTList.Separator;
                    string ActionAddValPart = symbolTable.FindRuleDescriptor(NTList.Name).isGeneratedRule ? StringConstants.YActionAddValueAsList : StringConstants.YActionAddValue;
                    if (NTList.CanBeEmpty)
                        action = string.Format(StringConstants.YActionForSet1, StringConstants.SourceEntityUniformSetClassName, Sep1, Environment.NewLine, "true");
                    else
                    {
                        Branch1.Add(new SubRulePart(NTList.Name, true));
                        string act = string.Format(ActionAddValPart, "1") + string.Format(StringConstants.YActionAddName, "1");
                        action = string.Format(StringConstants.YActionForSet1, StringConstants.SourceEntityUniformSetClassName, Sep1, act + Environment.NewLine, "false");
                    }
                    Branch1.Add(new SubRuleAction(action));

                    Branch2.Add(new SubRulePart(newRule.Name));
                    if (SeparatorEmpty)
                    {
                        action = string.Format(StringConstants.YActionForSet2, "2", string.Format(ActionAddValPart, "2"));
                    }
                    else
                    {
                        Branch2.Add(new SubRulePart(NTList.Separator));
                        action = string.Format(StringConstants.YActionForSet2, "3", string.Format(ActionAddValPart, "3"));
                    }
                    Branch2.Add(new SubRulePart(NTList.Name, true));
                    Branch2.Add(new SubRuleAction(action));
                    newRule.SubRules.Add(Branch1);
                    newRule.SubRules.Add(Branch2);
                    symbolTable.AddRuleDeferred(newRule);

                    branch.RemoveAt(i);
                    branch.Insert(i, new SubRulePart(newRule.Name));
                    branch[i].UseValue = true;
                    branch[i].UseName = NTList.UseName;
                }
        }

        private void ReplaceUniformListsWithRules()
        {
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                foreach (List<SubRulePart> branch in rd.SubRules)
                {
                    isFirstTimeListEntity = true;
                    ReplaceUniformListsWithRules(branch, rd.Name, rd.Location);
                }
            symbolTable.CommitDeferredRules();
        }

        /// <summary>
        /// Добавление предопределенных сущностей error, состояния пропуска по директиве
        /// </summary>
        private void AddPredefinedEntities()
        {
            symbolTable._allNames.Add(StringConstants.ErrorRuleName);

            //TokenDeclaration td = new TokenDeclaration();
            //td.Name = StringConstants.NewLineLexGroupName;
            //td.Values = new List<string>();
            //td.Values.Add(StringConstants.NewLineLexGroupName);
            //symbolTable.AddToken(td);
            symbolTable.AddState(StringConstants.SkipDirectiveStateName);
            symbolTable._allNames.Add(StringConstants.ErrorRuleName);
        }

        /// <summary>
        /// Добавление сущностей из исходного файла в таблицу символов
        /// </summary>
        /// <param name="source"></param>
        private void AddSourceEntitiesToSymbolTable(SourceFile source)
        {
            foreach (Declaration d in source.Declarations)
            {
                if (d is TokenDeclaration)
                    symbolTable.AddToken(d as TokenDeclaration);
                if (d is SkipDeclaration)
                    symbolTable.AddSkip(d as SkipDeclaration);
                if (d is RuleDeclaration)
                    symbolTable.AddRule(d as RuleDeclaration);
            }
        }

        private void CheckOrAddPredefinedName(string name, LexLocation location)
        {
            if (name.StartsWith("\"") && name.EndsWith("\""))
                return;
            if (symbolTable._allNames.Contains(name))
                return;

            if (name == StringConstants.ProgramRuleName)
                return; //ничего не делать, добавим позже
            //if (name == StringConstants.LEXPreDefTokenIDName)
            //{
            //    LexGroupDeclaration lg = new LexGroupDeclaration(StringConstants.LEXPreDefTokenIDName, StringConstants.LEXPreDefTokenIDValue);
            //    symbolTable.AddToken(lg);
            //}
            //else if (name == StringConstants.LEXPreDefTokenIntNumName)
            //{
            //    LexGroupDeclaration lg = new LexGroupDeclaration(StringConstants.LEXPreDefTokenIntNumName, StringConstants.LEXPreDefTokenIntNumValue);
            //    symbolTable.AddToken(lg);
            //}
            //else if (name == StringConstants.LEXPreDefTokenSignName)
            //{
            //    LexGroupDeclaration lg = new LexGroupDeclaration(StringConstants.LEXPreDefTokenSignName, StringConstants.LEXPreDefTokenSignValue);
            //    symbolTable.AddToken(lg);
            //    return;
            //}
            //else
                ErrorReporter.WriteError(ErrorMessages.NameNotDeclared, location, name);
        }

        /// <summary>
        /// Для каждого элемента списка проверяет наличие объявления сущности с данным именем во входном файле
        /// Если нет - выдача сообщения об ошибке с указанием места
        /// </summary>
        /// <param name="list"></param>
        /// <param name="location"></param>
        private void CheckList(List<string> list, LexLocation location)
        {
            foreach (string str in list)
                CheckOrAddPredefinedName(str, location);
        }

        private void CheckList(List<SubRulePart> list, LexLocation location)
        {
            foreach (SubRulePart srp in list)
                if (srp is SubRuleComplexPart)
                    foreach (List<SubRulePart> srl in (srp as SubRuleComplexPart).Items)
                        CheckList(srl, location);
                else
                    if (!(srp is SubRuleAny))
                        CheckOrAddPredefinedName(srp.Name, srp.Location);
        }
        private void CheckExistenceOfUsedNames()
        {
            foreach (SkipDeclaration sd in symbolTable._skips)
            {
                CheckList(sd.Begin, sd.Location);
                CheckList(sd.EscapeSymbol, sd.Location);
                CheckList(sd.End, sd.Location);
            }
            foreach (TokenDeclaration td in symbolTable._tokens.Values)
                if (!(td is RegExTokenDeclaration))
                    CheckList(td.Values, td.Location);
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                foreach (List<SubRulePart> sr in rd.SubRules)
                    CheckList(sr, rd.Location);
        }

        private List<string> FindRulesWithSharp(List<SubRulePart> branch)
        {
            List<string> res = new List<string>();
            foreach (SubRulePart srp in branch)
                if (srp is SubRuleComplexPart)
                    foreach (List<SubRulePart> br in (srp as SubRuleComplexPart).Items)
                        res.AddRange(FindRulesWithSharp(br));
                else
                    if (srp.UseValue)
                        res.Add(srp.Name);
            return res;
        }
        private void MarkRulesWithoutNode()
        {
            HashSet<string> RuleNames = new HashSet<string>();
            List<string> UsedNamesList = new List<string>();
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
            {
                RuleNames.Add(rd.Name);
                foreach (List<SubRulePart> branch in rd.SubRules)
                    UsedNamesList.AddRange(FindRulesWithSharp(branch));
            }
            if (symbolTable._rules.ContainsKey(StringConstants.ProgramRuleName))
                UsedNamesList.Add(StringConstants.ProgramRuleName);
            foreach (string str in UsedNamesList)
                if (RuleNames.Contains(str))
                    RuleNames.Remove(str);
            foreach (string str in RuleNames)
                symbolTable._rules[str].isGeneratedRule = true;
        }
        private void MarkUnnamedRules()
        {
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                if (!rd.UseRuleName)
                {
                    bool named = false;
                    foreach (List<SubRulePart> branch in rd.SubRules)
                        if (IsUseNameSymbolInBranch(branch))
                        {
                            named = true;
                            break;
                        }
                    if (!named)
                        rd.isGeneratedRule = true;
                }
        }

        private bool IsUseNameSymbolInBranch(List<SubRulePart> branch)
        {
            foreach (SubRulePart srp in branch)
            {
                if (srp.UseName)
                    return true;
                if (srp is SubRuleComplexPart)
                {
                    SubRuleComplexPart sc = srp as SubRuleComplexPart;
                    foreach (List<SubRulePart> br2 in sc.Items)
                        if (IsUseNameSymbolInBranch(br2))
                            return true;
                }
            }
                return false;
        }

        private class Pair
        {
            public List<SubRulePart> List;
            public int Index;
            public Pair(List<SubRulePart> List, int Index)
            {
                this.List = List;
                this.Index = Index;
            }
        }

        /// <summary>
        /// Генерирует множество FIRST для элемента подправила (имя терминала/нетерминала/скобка[...])
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="flag">True, если элемент подправила допускает пустую продукцию</param>
        /// <returns></returns>
        private HashSet<string> GetFIRST(SubRulePart sr, ref bool flag)
        {
            HashSet<string> res;
            if (sr is SubRuleComplexPart)
                res = symbolTable.GetFIRSTforComplexSubRulePart(sr as SubRuleComplexPart);
            else
                res = symbolTable.GetFIRST(sr.Name);
            flag = res.Contains(StringConstants.EmptyProduction);
            return res;
        }

        /// <summary>
        /// Генерирует множество FIRST
        /// </summary>
        /// <returns></returns>
        private HashSet<string> GetFIRST(List<Pair> trace, int level)
        {
            if (level < 0)
                return new HashSet<string>();

            HashSet<string> FIRST = new HashSet<string>();
            //флаг для остановки цикла. Становится False если очередное вхождение не может быть пустым
            bool flag = true;
            int i = trace[level].Index + 1;
            List<SubRulePart> list = trace[level].List;
            while (flag && i < list.Count)
            {
                if (!(list[i] is SubRuleAction))
                {
                    FIRST.UnionWith(GetFIRST(list[i], ref flag));
                    if (list[i].Repetition == SubRuleRepetition.ZeroOrMore || list[i].Repetition == SubRuleRepetition.ZeroOrOne)
                        flag = true;
                }
                i += 1;
            }
            if (flag)
                FIRST.UnionWith(GetFIRST(trace, level-1));
            if (level > 0)
            {
                SubRulePart s = trace[level - 1].List[trace[level - 1].Index];
                if (s.Repetition == SubRuleRepetition.OneOrMore || s.Repetition == SubRuleRepetition.ZeroOrMore)
                {
                    bool f=false;
                    FIRST.UnionWith(GetFIRST(s, ref f));
                }
            }
            return FIRST;
        }

        private void CreateRuleForANY(List<Pair> trace, SubRuleAny ANY, RuleDeclaration rdec)
        {
            foreach (string str in ANY.Except)
                if (str.StartsWith(StringConstants.tkANY) && str != ANY.Name)
                    ErrorReporter.WriteError(ErrorMessages.ANYCollision, rdec.Location);


            HashSet<string> Except = GetFIRST(trace, trace.Count - 1);
            foreach (string str in ANY.Except)
                Except.UnionWith(symbolTable.GetFIRST(str));
            ANY.Except = Except;

            RuleDeclaration rd = new RuleDeclaration();
            rd.Name = symbolTable.GetNewName("ANY");
            ANY.Name = rd.Name;
            foreach (string str in symbolTable._tokens.Keys)
                if (!ANY.Except.Contains(str))
                {
                    List<SubRulePart> branch = new List<SubRulePart>();
                    branch.Add(new SubRulePart(str, true));
                    rd.SubRules.Add(branch);
                }
            symbolTable.AddRuleDeferred(rd);
        }
        /// <summary>
        /// Поиск нетерминала ANY и генерация правила для него.
        /// </summary>
        /// <param name="trace"></param>
        private void findNonTermANY(List<Pair> trace, RuleDeclaration rd)
        {
            List<SubRulePart> list = trace[trace.Count-1].List;
            for (int i = 0; i < list.Count; ++i)
            {
                trace[trace.Count - 1].Index = i;

                //главная проверка if list[i] is subruleany
                if (list[i] is SubRuleAny)
                    CreateRuleForANY(trace, list[i] as SubRuleAny, rd);

                if (list[i] is SubRuleComplexPart)
                {
                    
                    SubRuleComplexPart sc = list[i] as SubRuleComplexPart;
                    for (int j = 0; j< sc.Items.Count; ++j)
                    {
                        trace.Add(new Pair(sc.Items[j], 0));
                        findNonTermANY(trace, rd);
                        trace.RemoveAt(trace.Count - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Преобразуем все вхождения ANY/AnyExcept в сгенерированные правила
        /// </summary>
        private void ConvertANYToRules()
        {
            List<Pair> trace = new List<Pair>();
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                foreach (List<SubRulePart> branch in rd.SubRules)
                {
                    trace.Add(new Pair(branch, 0));
                    findNonTermANY(trace, rd);
                    trace.Clear();
                }
            symbolTable.CommitDeferredRules();
        }

        /// <summary>
        /// Преобразуем квадратные скобки в новое правило
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        private SubRulePart ConvertComplexSubrulePart(SubRuleComplexPart part)
        {
            for (int i = 0; i < part.Items.Count; ++i)
                for (int j = 0; j < part.Items[i].Count; ++j)
                    if (part.Items[i][j] is SubRuleComplexPart)
                    {
                        part.Items[i][j] = ConvertComplexSubrulePart(part.Items[i][j] as SubRuleComplexPart);
                        if (part.Items[i][j] == null)
                            part.Items[i].RemoveAt(j);
                        --j;
                    }

            part.InitUses();
            //тут можно сгенерировать имя так, чтобы одинаковые конструкции не дублировались
            SubRulePart result = new SubRulePart();
            result.UseName = part.UseName;
            result.UseValue = part.UseValue;
            result.Name = symbolTable.GetNewName("");
            result.Repetition = SubRuleRepetition.Once;

            RuleDeclaration rd = new RuleDeclaration();
            rd.Name = result.Name;
            rd.Location = part.Location;

            SubRulePart thisRule = new SubRulePart(result.Name);
            thisRule.UseName = result.UseName;
            thisRule.UseValue = result.UseValue;

            if (part.Repetition == SubRuleRepetition.ZeroOrOne)
            {
                rd.SubRules.Add(new List<SubRulePart>());
                rd.SubRules.AddRange(part.Items);
            }
            else if (part.Repetition == SubRuleRepetition.ZeroOrMore)
            {
                rd.SubRules.Add(new List<SubRulePart>());
                rd.SubRules.AddRange(part.Items);
                for (int i = 1; i < rd.SubRules.Count; ++i)
                    rd.SubRules[i].Insert(0, thisRule);
            }
            else if (part.Repetition == SubRuleRepetition.OneOrMore)
            {
                rd.SubRules.AddRange(part.Items);
                foreach (List<SubRulePart> L in part.Items)
                {
                    List<SubRulePart> L2 = new List<SubRulePart>();
                    L2.Add(thisRule);
                    L2.AddRange(L);
                    rd.SubRules.Add(L2);
                }
            }
            else if (part.Repetition == SubRuleRepetition.Once)
            {
                if (part.Items.Count == 0)
                    return null;
                if (part.Items.Count == 1 && part.Items[0].Count == 1)
                    return part.Items[0][0];
                rd.SubRules.AddRange(part.Items);
            }

            symbolTable.AddRuleDeferred(rd);
            
            return result;
        }

        /// <summary>
        /// Избавляемся от квадратных скобок, создавая вместо них новые правила
        /// </summary>
        private void ConvertComplexSubruleParts()
        {
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                foreach (List<SubRulePart> SL in rd.SubRules)
                    for (int i = 0; i < SL.Count; ++i)
                        if (SL[i] is SubRuleComplexPart)
                        {
                            SL[i] = ConvertComplexSubrulePart(SL[i] as SubRuleComplexPart);
                            if (SL[i] == null)
                                SL.RemoveAt(i);
                            --i;
                        }
            symbolTable.CommitDeferredRules();
        }

        private int AnyCounter = 0;
        /// <summary>
        /// В ветке заменяем строковые литералы на лексемы, существующие (при наличии) или новые.
        /// </summary>
        /// <param name="branch"></param>
        private void DeclareTokensInComplexSRP(List<SubRulePart> branch)
        {
            foreach (SubRulePart srp in branch)
            {
                if (srp is SubRuleAny)
                {
                    SubRuleAny Any = srp as SubRuleAny;
                    srp.Name = StringConstants.tkANY + AnyCounter;
                    AnyCounter += 1;

                    HashSet<string> newExcept = new HashSet<string>();
                    foreach (string str in Any.Except)
                        if (str.StartsWith(StringConstants.StringQuote))
                            newExcept.Add(symbolTable.FindOrCreateToken(str));
                        else
                            newExcept.Add(str);
                    Any.Except = newExcept;

                    continue;
                }
                SubRuleComplexPart sc = srp as SubRuleComplexPart;
                if (sc != null)
                    foreach (List<SubRulePart> br2 in sc.Items)
                        DeclareTokensInComplexSRP(br2);
                else
                    if (srp.Name.StartsWith(StringConstants.StringQuote))
                        srp.Name = symbolTable.FindOrCreateToken(srp.Name);
            }
        }

        /// <summary>
        /// В правилах и блоках заменяем строки на лексемы
        /// </summary>
        /// <param name="source"></param>
        private void DeclareInlinedTokens()
        {
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                foreach (List<SubRulePart> l in rd.SubRules)
                    DeclareTokensInComplexSRP(l);
        }
        
        /// <summary>
        /// Заменяет именованные лексемы на списки строковых литералов
        /// </summary>
        /// <param name="l"></param>
        private void ReplaceTokensWithStrings(List<string> l, LexLocation location)
        {
            for (int i = 0; i < l.Count; ++i)
                if (!l[i].StartsWith(StringConstants.StringQuote))
                {
                    TokenDeclaration td = symbolTable.FindTokenList(l[i]);
                    if (td == null)
                        ErrorReporter.WriteError(ErrorMessages.NameNotDeclared, location, l[i]);
                    l.AddRange(td.Values);
                    l.RemoveAt(i);
                    --i;
                }
        }
        /// <summary>
        /// В Skip'ах заменяем лексемы на строки
        /// </summary>
        private void InlineTokensInUnnamedSkips()
        {
            foreach (SkipDeclaration sd in symbolTable._skips)
            {
                ReplaceTokensWithStrings(sd.Begin, sd.Location);
                ReplaceTokensWithStrings(sd.End, sd.Location);
                ReplaceTokensWithStrings(sd.EscapeSymbol, sd.Location);
            }
        }

        private string GetRepetitionSymbol(SubRulePart srp)
        {
            if (srp.Repetition == SubRuleRepetition.OneOrMore)
                return StringConstants.RuleOneOrMoreSymbol;
            else if (srp.Repetition == SubRuleRepetition.ZeroOrMore)
                return StringConstants.RuleZeroOrMoreSymbol;
            else if (srp.Repetition == SubRuleRepetition.ZeroOrOne)
                return StringConstants.RuleOptSymbol;
            else return "";
        }

        /// <summary>
        /// Избавляемся в ветке правила от *+?, генерируя по необходимости новые правила
        /// </summary>
        /// <param name="tl"></param>
        private void ExpandSubrule(List<SubRulePart> subRule)
        {
            if (subRule.Count == 0)
                return;
            for (int i = 0; i < subRule.Count; )
            {
                if (subRule[i].Repetition != SubRuleRepetition.Once)
                {
                    string ruleName = symbolTable.FindGeneratedRuleName(subRule[i].Name + GetRepetitionSymbol(subRule[i]));
                    if (ruleName == "")
                    {
                        RuleDeclaration newRule = new RuleDeclaration();
                        if (subRule[i].Repetition == SubRuleRepetition.ZeroOrOne)
                        {
                            ruleName = symbolTable.GetNewName(subRule[i].Name + StringConstants.tkOptionalSuffix);
                            newRule.SubRules.Add(new List<SubRulePart>());
                            List<SubRulePart> sr2 = new List<SubRulePart>();
                            sr2.Add(new SubRulePart(subRule[i].Name, true));
                            newRule.SubRules.Add(sr2);
                        }
                        else if (subRule[i].Repetition == SubRuleRepetition.ZeroOrMore)
                        {
                            ruleName = symbolTable.GetNewName(subRule[i].Name + StringConstants.tkZeroOrMoreSuffix);
                            newRule.SubRules.Add(new List<SubRulePart>());
                            List<SubRulePart> sr2 = new List<SubRulePart>();
                            sr2.Add(new SubRulePart(ruleName, true));
                            sr2.Add(new SubRulePart(subRule[i].Name, true));
                            newRule.SubRules.Add(sr2);
                        }
                        else if (subRule[i].Repetition == SubRuleRepetition.OneOrMore)
                        {
                            ruleName = symbolTable.GetNewName(subRule[i].Name + StringConstants.tkOneOrMoreSuffix);
                            List<SubRulePart> sr1 = new List<SubRulePart>();
                            sr1.Add(new SubRulePart(subRule[i].Name, true));
                            newRule.SubRules.Add(sr1);
                            List<SubRulePart> sr2 = new List<SubRulePart>();
                            sr2.Add(new SubRulePart(ruleName, true));
                            sr2.Add(new SubRulePart(subRule[i].Name, true));
                            newRule.SubRules.Add(sr2);
                        }

                        newRule.Name = ruleName;

                        symbolTable.AddRule(newRule);
                        symbolTable._ruleNames.Add(subRule[i].Name + GetRepetitionSymbol(subRule[i]), ruleName);
                    }
                    subRule[i].Name = ruleName;
                    subRule[i].Repetition = SubRuleRepetition.Once;
                }
                else
                    ++i;
            }
        }

        /// <summary>
        /// Избавляемся в правилах от *+?, генерируя по необходимости новые правила
        /// </summary>
        private void ExpandRules()
        {
            List<List<SubRulePart>> AllSubRules = new List<List<SubRulePart>>();
            foreach (KeyValuePair<string, RuleDeclaration> rule in symbolTable._rules)
                foreach (List<SubRulePart> tl in rule.Value.SubRules)
                    AllSubRules.Add(tl);
            foreach (List<SubRulePart> tl in AllSubRules)
                ExpandSubrule(tl);
        }

        /// <summary>
        /// Дописываем к правилам действия
        /// </summary>
        private void AddActionsToRules()
        {
            foreach (KeyValuePair<string, RuleDeclaration> rd in symbolTable._rules)
            {
                if (rd.Value.isActionGenerated)
                    continue;
                foreach (List<SubRulePart> tl in rd.Value.SubRules)
                {
                    string str;
                    bool isError = false;
                    foreach (SubRulePart srp in tl)
                        if (srp.Name == StringConstants.ErrorRuleName)
                            isError = true;
                    if (isError)
                        str = string.Format(StringConstants.YErrorAction, Options.BaseClassName, tl.Count);
                    else
                    {
                        List<int> Names = new List<int>();
                        List<int> Values = new List<int>();
                        for (int i = 0; i < tl.Count; ++i)
                        {
                            if (tl[i].UseName)
                                Names.Add(i + 1);
                            if (tl[i].UseValue)
                                Values.Add(i + 1);
                        }
                        StringBuilder sb = new StringBuilder();
                        if (rd.Value.UseRuleName)
                            sb.AppendFormat(StringConstants.YActionAddName, rd.Value.Name);
                        foreach (int i in Names)
                            sb.AppendFormat(StringConstants.YActionAddNameList, i);
                        foreach (int i in Values)
                        {
                            RuleDeclaration frd = symbolTable.FindRuleDescriptor(tl[i - 1].Name);
                            if ((frd != null && frd.isGeneratedRule) || tl[i - 1].Name == StringConstants.ProgramRuleName)
                                sb.AppendFormat(StringConstants.YActionAddValueAsList, i);
                            else
                                sb.AppendFormat(StringConstants.YActionAddValue, i);
                        }
                        if (rd.Key == StringConstants.ProgramRuleName)
                            sb.Append(StringConstants.YActionPartForProgram);

                        string strErr = "";
                        if (rd.Value.HasError())
                        {
                            strErr = StringConstants.YErrorAltBranchAction;
                            sb.Append(strErr);
                        }

                        string type = rd.Value.isGeneratedRule ? Options.BaseClassName : rd.Value.Name;
                        if (Names.Count == 0 && Values.Count == 0 && !rd.Value.UseRuleName)
                        {
                            str = string.Format(StringConstants.YActionEmpty, type, strErr);
                        }
                        else
                        {
                            int last = tl.Count;
                            while (last > 1 && tl[last - 1].Name.TrimStart().StartsWith(StringConstants.ActionStartSymbol))
                                last -= 1;

                            str = string.Format(StringConstants.YAction, last, sb.ToString(), type);
                        }
                    }
                    tl.Add(new SubRuleAction(str));
                }
                rd.Value.isActionGenerated = true;
            }
        }

        private List<string> GetMainRules()
        {
            HashSet<string> MainRules = new HashSet<string>();
            foreach (string str in symbolTable._rules.Keys)
                MainRules.Add(str);

            foreach (KeyValuePair<string, RuleDeclaration> rd in symbolTable._rules)
                foreach (List<SubRulePart> tl in rd.Value.SubRules)
                    for (int i = 0; i < tl.Count; )
                        if (MainRules.Contains(tl[i].Name))
                            MainRules.Remove(tl[i].Name);
                        else
                            ++i;
            return MainRules.ToList<string>();
        }

        /// <summary>
        /// генерируем предопределенные правила progrm, programnodelist, programnode
        /// </summary>
        private void GeneratePredefinedRules()
        {
            if (symbolTable._rules.ContainsKey(StringConstants.ProgramRuleName))
                return;
            StringBuilder sb = new StringBuilder();

            List<string> MainRules = GetMainRules();
            string PNListName = symbolTable.GetNewName(StringConstants.YPreDefProgNodeListName);
            string PNName = symbolTable.GetNewName(StringConstants.YPreDefProgNodeName);

            RuleDeclaration rd = new RuleDeclaration();
            rd.Name = PNListName;
            List<SubRulePart> SR = new List<SubRulePart>();
            SR.Add(new SubRulePart(StringConstants.YPreDefProgNodeListA1));
            rd.SubRules.Add(SR);
            SR = new List<SubRulePart>();
            SR.Add(new SubRulePart(PNListName));
            SR.Add(new SubRulePart(PNName));
            SR.Add(new SubRulePart(StringConstants.YPreDefProgNodeListA2));
            rd.SubRules.Add(SR);
            symbolTable.AddRule(rd);

            rd = new RuleDeclaration();
            rd.Name = PNName;
            SubRulePart Action = new SubRulePart(StringConstants.YPreDefProgNodeSubRule);
            foreach (string str in MainRules)
            {
                SR = new List<SubRulePart>();
                SR.Add(new SubRulePart(str));
                SR.Add(Action);
                rd.SubRules.Add(SR);
            }
            symbolTable.AddRule(rd);

            rd = new RuleDeclaration();
            rd.Name = StringConstants.ProgramRuleName;
            SR = new List<SubRulePart>();
            SR.Add(new SubRulePart(PNListName));
            SR.Add(new SubRulePart(StringConstants.YPreDefProgramAction));
            rd.SubRules.Add(SR);
            rd.isGeneratedRule = false;
            symbolTable.AddRule(rd);
        }
    }
}
