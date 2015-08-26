using System;
using System.Collections.Generic;
using QUT.Gppg;

namespace ParserGenerator
{
    public class SourceFile
    {
        public List<Declaration> Declarations = new List<Declaration>();
    }

    public class Declaration
    {
        public LexLocation Location = new LexLocation();
    }

    public class SkipDeclaration : Declaration
    {
        /// <summary>
        /// Признак учета вложенности пропускаемых конструкций
        /// </summary>
        public bool Nested = false;
        /// <summary>
        /// Открывающая скобка
        /// </summary>
        public List<string> Begin = new List<string>();
        /// <summary>
        /// Закрывающая скобка. Если пусто - конец строки
        /// </summary>
        public List<string> End = new List<string>();
        /// <summary>
        /// Escape-символ. Следующий после него символ не будет считаться закрывающей скобкой
        /// </summary>
        public List<string> EscapeSymbol = new List<string>();
        /// <summary>
        /// Содержит ли данныый пропускаемый фрагмент директивы препроцессора
        /// </summary>
        public bool Preprocessor = false;

        public void Merge(SkipDeclaration skip)
        {
            if (skip == null)
                return;
            Nested = Nested | skip.Nested;
            Preprocessor = Preprocessor | skip.Preprocessor;
            Begin.AddRange(skip.Begin.ToArray());
            End.AddRange(skip.End.ToArray());
            EscapeSymbol.AddRange(skip.EscapeSymbol.ToArray());
            if (skip.Location != null)
                Location = Location.Merge(skip.Location);
        }
    }

    public class TokenDeclaration : Declaration
    {
        public string Name = "";
        /// <summary>
        /// Список строк, которые лексер будет считать одной лексемой
        /// </summary>
        public List<string> Values = new List<string>();
    }

    public class RegExTokenDeclaration : TokenDeclaration
    {
        public RegExTokenDeclaration(string name, string value)
        {
            Name = name;
            Values.Add(value);
        }
    }

    public class RuleDeclaration : Declaration
    {
        /// <summary>
        /// Статический флаг, изначально установлен в true, после окончания разбора должен быть переключен в false
        /// Его значение используется в конструкторе при присвоении флагу который означает, что правило сгенерировано и не является сущностью из входного файла
        /// </summary>
        public static bool ParsingStage = true;

        public bool isGeneratedRule;

        public bool isActionGenerated = false;
        public string Type = "";

        public RuleDeclaration()
        {
            isGeneratedRule = !ParsingStage;
        }
        /// <summary>
        /// Подправила
        /// </summary>
        public List<List<SubRulePart>> SubRules = new List<List<SubRulePart>>();
        public string Name;
        /// <summary>
        /// Использовать ли имя правила в качестве имени генерируемого узла
        /// </summary>
        public bool UseRuleName = false;
        public void Merge(RuleDeclaration rule)
        {
            if (rule == null)
                return;
            if (rule.Location != null)
                Location = Location.Merge(rule.Location);
            if (rule.SubRules != null)
                SubRules.AddRange(rule.SubRules.ToArray());
        }
        public bool HasError()
        {
            foreach (List<SubRulePart> branch in SubRules)
                foreach (SubRulePart srp in branch)
                if (srp.Name == StringConstants.ErrorRuleName)
                    return true;
            return false;
        }

    }

    public enum SubRuleRepetition {Once, OneOrMore, ZeroOrMore, ZeroOrOne}

    /// <summary>
    /// Один терминал или нетерминал, встречающийся в правой части правила
    /// </summary>
    public class SubRulePart
    {
        public LexLocation Location = new LexLocation();
        public string Name;
        public bool UseName = false;
        public bool UseValue = false;
        public SubRuleRepetition Repetition = SubRuleRepetition.Once;
        public SubRulePart(bool UseNameValue = false)
        {
            UseValue = UseNameValue;
            UseName = UseNameValue;
        }
        public SubRulePart(string name, bool UseNameValue = false)
        {
            Name = name;
            UseValue = UseNameValue;
            UseName = UseNameValue;
        }
    }
    /// <summary>
    /// Несколько терминалов/нетерминалов в правой части правила, заключенные в квадратные скобки
    /// </summary>
    public class SubRuleComplexPart : SubRulePart
    {
        public List<List<SubRulePart>> Items = new List<List<SubRulePart>>();

        public SubRuleComplexPart(List<List<SubRulePart>> items)
        {
            Items = items;
        }

        /// <summary>
        /// Инициализация переменных UseName и UseValue по значениям элементов
        /// </summary>
        public void InitUses()
        {
            foreach (List<SubRulePart> L in Items)
                foreach (SubRulePart sr in L)
                {
                    UseName = UseName | sr.UseName;
                    UseValue = UseValue | sr.UseValue;
                }
        }
    }
    public class SubRuleAny : SubRulePart
    {
        public HashSet<string> Except = new HashSet<string>();
    }
    public class SubRuleAction : SubRulePart
    {
        public SubRuleAction(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Унаследованное поле Name - имя нетерминала
    /// </summary>
    public class SubRuleNonTermList : SubRulePart
    {
        public string Separator;
        public bool CanBeEmpty;
        public SubRuleNonTermList(string NonTerm, string Separator)
        {
            Name = NonTerm;
            this.Separator = Separator;
            UseValue = true;
        }
    }
    
    public class SymbolTable
    {
        public HashSet<string> _allNames = new HashSet<string>();
        private List<RuleDeclaration> _deferredRules = new List<RuleDeclaration>();
        public Dictionary<string, TokenDeclaration> _tokens = new Dictionary<string, TokenDeclaration>();
        public List<SkipDeclaration> _skips = new List<SkipDeclaration>();
        public Dictionary<string, RuleDeclaration> _rules = new Dictionary<string, RuleDeclaration>();
        public Dictionary<string, string> _ruleNames = new Dictionary<string, string>();
        public Dictionary<SkipDeclaration, string> _skipNames = new Dictionary<SkipDeclaration, string>();
        public Dictionary<string, int> _states = new Dictionary<string, int>();

        private Dictionary<string, HashSet<string>> FIRST;
        private Dictionary<SubRuleAny, HashSet<string>> FIRSTForANY = new Dictionary<SubRuleAny, HashSet<string>>();

        private int stateNo = 1;
        int NameIndex;

        string GetNewName()
        {
            NameIndex += 1;
            return "Anon" + NameIndex.ToString();
        }
        private string GetNameForChar(char c)
        {
            switch (c)
            {
                case '!': return "Excl";
                case '@': return "At";
                case '#': return "Sharp";
                case '%': return "Perc";
                case '^': return "Cflex";
                case '&': return "Amp";
                case '*': return "Star";
                case '+': return "Plus";
                case '-': return "Minus";
                case '(': return "Ropen";
                case ')': return "Rclose";
                case '[': return "Sopen";
                case ']': return "Sclose";
                case '{': return "Copen";
                case '}': return "Cclose";
                case ';': return "Scolon";
                case ':': return "Colon";
                case '/': return "Slash";
                case '\'': return "Quote";
                case '\"': return "Dquote";
                case '\\': return "Bslash";
                case '|': return "Pipe";
                case '.': return "Dot";
                case ',': return "Comma";
                case '?': return "Quest";
                case '~': return "Tilde";
                default: return "";
            }
        }

        public string GetNewName(string str)
        {
            if (str.StartsWith("\""))
                str = str.Substring(1, str.Length - 2);
            string result = "_";
            for (int i = 0; i < str.Length; ++i)
                if (char.IsLetterOrDigit(str[i]) || str[i] == '_')
                    result += str[i];
                else
                    result += GetNameForChar(str[i]);
            if (!_allNames.Contains(result))
                return result;
            string res1 = "";
            do
            {
                NameIndex += 1;
                res1 = result + NameIndex.ToString();
            } while (_allNames.Contains(res1));
            return res1;
        }

        public void AddToken(TokenDeclaration token)
        {
            if (_allNames.Contains(token.Name))
                return;

            foreach (string s in token.Values)
                if (FindToken(s) != "")
                {
                    ErrorReporter.WriteError(ErrorMessages.NameAlreadyDefined, token.Location, token.Name);
                    return;
                }

            _tokens.Add(token.Name, token);
            _allNames.Add(token.Name);
        }

        public void RemoveToken(TokenDeclaration token)
        {
            if (!_allNames.Contains(token.Name))
                return;

            _tokens.Remove(token.Name);
            _allNames.Remove(token.Name);
        }
        public void AddSkip(SkipDeclaration skip)
        {
            string skipName = GetNewName();
            _skips.Add(skip);
            _skipNames.Add(skip, skipName);
            _states.Add(skipName, stateNo);
            stateNo += 1;
        }

        public void AddState(string Name)
        {
            _states.Add(Name, stateNo);
            stateNo += 1;
        }
        public void AddRule(RuleDeclaration ruleDescriptor)
        {
            if (_allNames.Contains(ruleDescriptor.Name))
            {
                ErrorReporter.WriteError(ErrorMessages.NameAlreadyDefined, ruleDescriptor.Location, ruleDescriptor.Name);
                return;
            }
            else
            {
                _rules.Add(ruleDescriptor.Name, ruleDescriptor);
                _allNames.Add(ruleDescriptor.Name);
            }
        }

        public void AddRuleDeferred(RuleDeclaration rule)
        {
            if (_allNames.Contains(rule.Name))
            {
                ErrorReporter.WriteError(ErrorMessages.NameAlreadyDefined, rule.Location, rule.Name);
                return;
            }
            else
            {
                _deferredRules.Add(rule);
                _allNames.Add(rule.Name);
            }
        }
        public void CommitDeferredRules()
        {
            for (int i = 0; i < _deferredRules.Count; ++i)
                _rules.Add(_deferredRules[i].Name, _deferredRules[i]);
            _deferredRules.Clear();
        }

       /// <summary>
       /// Поиск описания лексемы по имени
       /// При отсутствии - возвращает null
       /// </summary>
       /// <param name="Name"></param>
       /// <returns></returns>
        public TokenDeclaration FindTokenList(string Name)
        {
            if (_tokens.ContainsKey(Name))
                return _tokens[Name];
            else
                return null;
        }
       /// <summary>
       /// Поиск имени описания группы лексем по одному из значений
       /// При отсутствии возвращает пустую строку
       /// </summary>
       /// <param name="Name"></param>
       /// <returns></returns>
        public string FindToken(string Name)
        {
            foreach (KeyValuePair<string, TokenDeclaration> tl in _tokens)
                if (tl.Value.Values.Contains(Name))
                    return tl.Key;
            return "";
        }
        public string FindOrCreateToken(string Name)
        {
            string result = FindToken(Name);
            if (result != "")
                return result;

            TokenDeclaration td = new TokenDeclaration();
            td.Name = GetNewName(Name);
            td.Values.Add(Name);
            AddToken(td);

            return td.Name;
        }
        public string FindGeneratedRuleName(string name)
        {
            if (_ruleNames.ContainsKey(name))
                return _ruleNames[name];
            if (_rules.ContainsKey(name))
                return name;
            return "";
        }
        public string FindSkipName(SkipDeclaration skip)
        {
            if (_skipNames.ContainsKey(skip))
                return _skipNames[skip];
            return "";
        }

        public RuleDeclaration FindRuleDescriptor(string Name)
        {
            if (_rules.ContainsKey(Name))
                return _rules[Name];
            else 
                return null;
        }

        public HashSet<string> GetFIRST(string Name)
        {
            if (FIRST == null)
                BuildFIRST();
            if (_tokens.ContainsKey(Name) || FindToken(Name) != "" || Name.StartsWith(StringConstants.tkANY) || Name == StringConstants.ErrorRuleName)
            {
                HashSet<string> res = new HashSet<string>();
                res.Add(Name);
                return res;
            }
            if (_ruleNames.ContainsKey(Name))
                Name = _ruleNames[Name];
            if (FIRST.ContainsKey(Name))
                return FIRST[Name];
            else
                throw new Exception("Unknown Name in GetFIRST");
        }

        private HashSet<string> GetFIRSTForBranch(List<SubRulePart> branch)
        {
            HashSet<string> res = new HashSet<string>();
            if (branch.Count == 0)
                    res.Add(StringConstants.EmptyProduction);
            foreach (SubRulePart srp in branch)
            {
                if (srp is SubRuleAction)
                    continue;
                HashSet<string> HS;
                if (srp is SubRuleComplexPart)
                    HS = GetFIRSTforComplexSubRulePart(srp as SubRuleComplexPart);
                else
                    HS = GetFIRST(srp.Name);
                res.UnionWith(HS);
                if (!HS.Contains(StringConstants.EmptyProduction))
                    break;
            }
            return res;
        }
        public HashSet<string> GetFIRSTforComplexSubRulePart(SubRuleComplexPart SC)
        {
            if (FIRST == null)
                BuildFIRST();
            HashSet<string> res = new HashSet<string>();
            foreach (List<SubRulePart> branch in SC.Items)
                res.UnionWith(GetFIRSTForBranch(branch));
            return res;
        }
        private void BuildFIRST()
        {
            FIRST = new Dictionary<string,HashSet<string>>();
            foreach (RuleDeclaration rd in _rules.Values)
                FIRST.Add(rd.Name, new HashSet<string>());

            bool stop = false;
            while (!stop)
            {
                stop = true;
                foreach (RuleDeclaration rd in _rules.Values)
                {
                    HashSet<string> ruleFIRST = new HashSet<string>();
                    foreach (List<SubRulePart> branch in rd.SubRules)
                        ruleFIRST.UnionWith(GetFIRSTForBranch(branch));

                    if (ruleFIRST.Count != FIRST[rd.Name].Count)
                        stop = false;
                    FIRST[rd.Name] = ruleFIRST;
                }

            }
        }
    }

}