using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParserGenerator
{
    class Generator
    {
        private SymbolTable symbolTable;
        public Generator(SymbolTable symbolTable)
        {
            this.symbolTable = symbolTable;
        }

        public void GenerateSourceFiles()
        {
            //инициализация
            Init();

            //генерируем файл ParserPartGenerated.cs
            GeneratePPGFile();

            //Генерируем файл лексера
            GenerateLEXFile();

            //Генерируем файл парсера
            GenerateYACCFile();
        }

        private void Init()
        {
            if (Options.Namespace.StartsWith("\""))
                Options.Namespace = Options.Namespace.Substring(1);
            if (Options.Namespace.EndsWith("\""))
                Options.Namespace = Options.Namespace.Substring(0, Options.Namespace.Length-1);
            //if (Options.)
        }

        /// <summary>
        /// TODO Extension
        /// TODO Case-sensitivity
        /// Генерация файла ParserPartGenerated.cs
        /// </summary>
        private void GeneratePPGFile()
        {
            string DirFuncs = GenerateDirectiveCheckFunctions();
            string ExtList = string.Join(", ", Options.ExtensionList.ToArray());
            string scan = string.Format(StringConstants.PPGLWScanner, ExtList);
            string pars = string.Format(StringConstants.PPGLWParser, ExtList);

            string EXT = Options.Extension.Substring(1, Options.Extension.Length - 2).ToUpper();
            string VisitorName = EXT + StringConstants.PPGVisitorClassName;

            StringBuilder sbClasses = new StringBuilder();
            sbClasses.AppendFormat(StringConstants.PPGTreeNodeBaseClass, Options.BaseClassName, VisitorName);
            StringBuilder sbInterface = new StringBuilder();
            sbInterface.AppendFormat(StringConstants.PPGVisitorEntry, Options.BaseClassName + " _" + Options.BaseClassName);
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
            {
                if (rd.isGeneratedRule)
                    continue;
                sbClasses.AppendFormat(StringConstants.PPGTreeNodeClass, rd.Name, Options.BaseClassName, VisitorName);
                string Params = rd.Name + " _" + rd.Name;
                sbInterface.AppendFormat(StringConstants.PPGVisitorEntry, Params);
            }
            //predefined class Token
            sbClasses.AppendFormat(StringConstants.PPGTreeNodeClass, StringConstants.TokenClassName, Options.BaseClassName, VisitorName);
            sbInterface.AppendFormat(StringConstants.PPGVisitorEntry, StringConstants.TokenClassName + " _" + StringConstants.TokenClassName);
            string Interf = string.Format(StringConstants.PPGVisitor, VisitorName, sbInterface.ToString());

            StreamWriter sw = File.CreateText(Options.PPGFileName);
            sw.Write(string.Format(StringConstants.PPGFile, sbClasses + Interf + DirFuncs + scan + pars, StringConstants.PPGCommonPart, Options.Namespace));
            sw.Close();
        }

        /// <summary>
        /// Генерация lex-файла
        /// </summary>
        private void GenerateLEXFile()
        {
            string CS = "";
            if (Options.CaseInsensitive)
                CS = StringConstants.LEXCaseInsensitive;

            string LexCat = GenerateLexCategories();

            StringBuilder States = new StringBuilder();
            foreach (string str in symbolTable._states.Keys)
                States.AppendFormat(StringConstants.LEXState, str);

            StringBuilder Rules = new StringBuilder();
            Rules.Append(GenerateEnteringIntoSkipStates());
            Rules.Append(GenerateSkipDirectiveState());

            Rules.Append(GenerateLexRulesForTokens());

            Rules.Append(GenerateLexStates());

            StreamWriter sw = File.CreateText(Options.LEXFileName);
            sw.Write(string.Format(StringConstants.LEXFile, CS, LexCat, States.ToString(), Rules.ToString(), Options.Namespace));
            sw.Close();
        }

        private void GenerateYACCFile()
        {
            string Section1 = GenerateYTokensAndTypes();
            string Section2 = GenerateYRules();

            StringBuilder sbUnion = new StringBuilder();
            sbUnion.AppendFormat(StringConstants.YUnionEntry, Options.BaseClassName);
            sbUnion.AppendFormat(StringConstants.YUnionEntry, StringConstants.TokenClassName);
            HashSet<string> HS = new HashSet<string>();
            foreach (RuleDeclaration rd in symbolTable._rules.Values)
                if (!rd.isGeneratedRule)
                    sbUnion.AppendFormat(StringConstants.YUnionEntry, rd.Name);
            sbUnion.AppendFormat(StringConstants.YUnionEntry, StringConstants.SourceEntityUniformSetClassName);

            StreamWriter sw = File.CreateText(Options.YFileName);
            sw.Write(string.Format(StringConstants.YFile, Section1, Section2, sbUnion, Options.Namespace));
            sw.Close();
        }        

        /// <summary>
        /// Вспомогательная фукция, возвращает тело метода проверки строки на соответствие имени директивы
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GenerateDirectiveCheckFunction(string name)
        {
            if (name == "")
                return StringConstants.PPGRegionPartNoBody;
            else
                return string.Format(StringConstants.PPGRegionPartBody, name);
        }

        /// <summary>
        /// Генерирует функции проверки строк на соответствие именам директив в данном языке
        /// </summary>
        /// <returns></returns>
        private string GenerateDirectiveCheckFunctions()
        {
            string RegionCheckBody1;
            string RegionCheckBody2;
            if (Options.RegionBegin != "" && Options.RegionEnd != "")
            {
                RegionCheckBody1 = string.Format(StringConstants.PPGRegionPartBody, Options.RegionBegin);
                RegionCheckBody2 = string.Format(StringConstants.PPGRegionPartBody, Options.RegionEnd);
            }
            else
            {
                RegionCheckBody1 = StringConstants.PPGRegionPartNoBody;
                RegionCheckBody2 = StringConstants.PPGRegionPartNoBody;
            }
            //Todo Check for consistency
            string DirDefine = GenerateDirectiveCheckFunction(Options.DirectiveDefine);
            string DirUndef = GenerateDirectiveCheckFunction(Options.DirectiveUndef);
            string DirIfdef = GenerateDirectiveCheckFunction(Options.DirectiveIfdef);
            string DirIfndef = GenerateDirectiveCheckFunction(Options.DirectiveIfndef);
            string DirElse = GenerateDirectiveCheckFunction(Options.DirectiveElse);
            string DirElif = GenerateDirectiveCheckFunction(Options.DirectiveElif);
            string DirEnd = GenerateDirectiveCheckFunction(Options.DirectiveEnd);
            string DirectiveFuncs = string.Format(StringConstants.PPGRegionPart,
                RegionCheckBody1, RegionCheckBody2, DirDefine, DirUndef, DirIfdef, DirIfndef, DirElse, DirElif, DirEnd);
            return DirectiveFuncs;
        }

        /// <summary>
        /// Генерирует состояние пропуска директивы с входом в состояния пропуска комментариев
        /// </summary>
        /// <returns></returns>
        private string GenerateSkipDirectiveState()
        {
            return string.Format(StringConstants.LEXSkipDirectiveState, GenerateEnteringIntoSkipStates());
        }

        /// <summary>
        /// Генерируются правила, связанные с переходами в другие состояния
        /// </summary>
        /// <returns></returns>
        private string GenerateEnteringIntoSkipStates()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SkipDeclaration sd in symbolTable._skips)
            {
                foreach (string str in sd.Begin)
                    sb.AppendFormat(StringConstants.LEXEnteringNewState, str, symbolTable.FindSkipName(sd));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Генерация обработки Escape-последовательностей для директив пропуска
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        private string GenerateEscapeSequences(List<string> esc, List<string> end)
        {
            StringBuilder sb = new StringBuilder();
            List<string> pairs = new List<string>();
            foreach (string str1 in esc)
            {
                sb.AppendFormat(StringConstants.LEXEscNoRet, "\"" + str1.Substring(1, str1.Length - 2) + str1.Substring(1, str1.Length - 2) + "\"");
                foreach (string str2 in end)
                {
                    string str = "\"" + str1.Substring(1, str1.Length - 2) + str2.Substring(1, str2.Length - 2) + "\"";
                    sb.AppendFormat(StringConstants.LEXEscNoRet, str);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Поиск строковых лексем для начала и конца именованных директив пропуска
        /// </summary>
        /// <param name="tl"></param>
        /// <returns></returns>
        private List<string> FindTokensForNamedSkipBeginEnd(List<string> tl)
        {
            List<string> result = new List<string>();
            foreach (string str in tl)
                if (str.StartsWith("\""))
                    result.Add(str);
                else
                {
                    if (symbolTable._tokens.ContainsKey(str))
                        result.AddRange(symbolTable._tokens[str].Values);
                    else if (symbolTable._rules.ContainsKey(str))
                    {
                        RuleDeclaration rd = symbolTable._rules[str];
                        foreach (List<SubRulePart> tl2 in rd.SubRules)
                            result.Add(symbolTable._tokens[tl2[0].Name].Values[0]);
                    }
                }
            return result;
        }

        /// <summary>
        /// генерация обработки символов начала пропускаемого фрагмента
        /// Только если Nested == true
        /// </summary>
        /// <param name="tl"></param>
        /// <returns></returns>
        private string GenerateBeginInState(List<string> tl, bool ReturnTokens = false)
        {
            StringBuilder sb = new StringBuilder();
            List<string> begins = FindTokensForNamedSkipBeginEnd(tl);
            foreach (string str in begins)
                sb.AppendFormat(ReturnTokens ? StringConstants.LEXStateBeginRet : StringConstants.LEXStateBegin, str);
            return sb.ToString();
        }

        /// <summary>
        /// генерация обработки символов конца пропускаемого фрагмента
        /// </summary>
        /// <param name="tl"></param>
        /// <returns></returns>
        private string GenerateEndInState(List<string> tl, bool Anonymous = true, bool Nested = false, bool ReturnTokens = false, bool ProcessDirective = false)
        {
            StringBuilder sb = new StringBuilder();
            List<string> ends = FindTokensForNamedSkipBeginEnd(tl);
            string procDir = "";//ProcessDirective ? StringConstants.LEXProcessDirective : "";
            foreach (string str in ends)
            {
                if (Anonymous)
                    if (Nested)
                        sb.AppendFormat(StringConstants.LEXSTateEndNest, str, procDir);
                    else
                        sb.AppendFormat(StringConstants.LEXSTateEnd, str, procDir);
                else
                    if (Nested)
                        if (ReturnTokens)
                            sb.AppendFormat(StringConstants.LEXSTateEndNameNestRet, str, symbolTable.FindToken(str), procDir);
                        else
                            sb.AppendFormat(StringConstants.LEXSTateEndNameNest, str, symbolTable.FindToken(str), procDir);
                    else
                        sb.AppendFormat(StringConstants.LEXSTateEndName, str, symbolTable.FindToken(str), procDir);
            }
            if (ends.Count == 0)
                sb.AppendFormat(StringConstants.LEXSTateEnd, StringConstants.LEXNewLineToken, procDir);
            return sb.ToString();
        }

        /// <summary>
        /// генерация состояний в секции правил
        /// </summary>
        /// <returns></returns>
        private string GenerateLexStates()
        {
            StringBuilder sb = new StringBuilder();

            foreach (SkipDeclaration sd in symbolTable._skips)
            {
                StringBuilder state = new StringBuilder();
                state.Append(GenerateEscapeSequences(sd.EscapeSymbol, sd.End));
                //state.Append(GenerateEnteringIntoSkipStates());
                if (sd.Nested)
                    state.Append(GenerateBeginInState(sd.Begin));
                state.Append(GenerateEndInState(sd.End, true, false, false, sd.Preprocessor));
                //state.Append(StringConstants.LEXTokenInComment);
                sb.AppendFormat(StringConstants.LEXStateBody, symbolTable.FindSkipName(sd), state.ToString());
            }

            return sb.ToString();
        }

        private string GenerateLexRulesForTokens()
        {
            StringBuilder result = new StringBuilder();

            foreach (TokenDeclaration td in symbolTable._tokens.Values)
                if (! (td is RegExTokenDeclaration))
                foreach (string str in td.Values)
                    result.AppendFormat(StringConstants.LEXToken, str, td.Name);

            foreach (TokenDeclaration td in symbolTable._tokens.Values)
                if (td is RegExTokenDeclaration)
                    result.AppendFormat(StringConstants.LEXToken, "{" + td.Name + "}", td.Name);

            return result.ToString();
        }

        private string GenerateLexCategories()
        {
            StringBuilder LexCategories = new StringBuilder();

            foreach (TokenDeclaration td in symbolTable._tokens.Values)
                if (td is RegExTokenDeclaration)
                    LexCategories.AppendLine(td.Name + " " + td.Values[0]);

            return LexCategories.ToString();
        }

        /// <summary>
        /// Генерация лексем и типов в первой секции yacc-файла
        /// </summary>
        /// <returns></returns>
        private string GenerateYTokensAndTypes()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, TokenDeclaration> tl in symbolTable._tokens)
                sb.AppendFormat(StringConstants.YToken, tl.Key);
            foreach (KeyValuePair<string, RuleDeclaration> rd in symbolTable._rules)
            {
                if (rd.Value.Type != "")
                    sb.AppendFormat(StringConstants.YType, rd.Value.Type, rd.Key);
                else
                    if (rd.Value.isGeneratedRule)
                        sb.AppendFormat(StringConstants.YType,Options.BaseClassName , rd.Key);
                    else
                        sb.AppendFormat(StringConstants.YType, rd.Key, rd.Key);
            }

            return sb.ToString();
        }

        private string GenerateYRules()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, RuleDeclaration> rd in symbolTable._rules)
            {
                StringBuilder sbSubRules = new StringBuilder();
                for (int sr = 0; sr < rd.Value.SubRules.Count; ++sr)
                {
                    List<SubRulePart> tl = rd.Value.SubRules[sr];
                    StringBuilder sbSR = new StringBuilder();
                    for (int i = 0; i < tl.Count; ++i)
                    {
                        if (i != 0)
                            sbSR.Append(StringConstants.YRuleTokenDelimiter);
                        sbSR.Append(tl[i].Name);
                    }
                    if (sr != 0)
                        sbSubRules.Append(StringConstants.YSubruleDelimiter);
                    sbSubRules.Append(sbSR.ToString());
                }
                sb.AppendFormat(StringConstants.YRule, rd.Key, sbSubRules.ToString());
            }

            return sb.ToString();
        }


    }
}
