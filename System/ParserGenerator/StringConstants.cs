using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserGenerator
{
    class StringConstants
    {
        public static string EmptyProduction = "<<Empty>>";
        public static string tkANY = "<<ANY>>";
        public static string tkSkip = "Skip";
        public static string tkRule = "Rule";
        public static string tkRegExToken = "Token";
        public static string tkBegin = "Begin";
        public static string tkEnd = "End";
        public static string tkBeginEnd = "BeginEnd";
        public static string tkEscapeSymbol = "EscapeSymbol";
        public static string tkNested = "Nested";
        public static string tkCaseSensitive = "CaseSensitive";
        public static string tkCaseInsensitive = "CaseInsensitive";
        public static string tkExtension = "Extension";
        public static string tkNamespace = "Namespace";
        public static string tkRegion = "Region";
        public static string tkDefine = "Define";
        public static string tkUndef = "Undef";
        public static string tkIfDef = "IfDef";
        public static string tkIfNDef = "IfNDef";
        public static string tkElse = "Else";
        public static string tkElIf = "ElIf";
        public static string tkPreprocessor = "Preprocessor";
        public static string tkAny = "Any";
        public static string tkAnyExcept = "AnyExcept";
        public static string tkList0 = "List0";
        public static string tkList1 = "List1";
        public static string StringQuote = "\"";
        public static string DefaultExtension = "\"txt\"";

        public static string tkPipe = "|";

        public static string tkSkipBeginSuffix = "_begin";
        public static string tkSkipEndSuffix = "_end";
        public static string tkSkipEscapeSuffix = "_escape";
        public static string tkOptionalSuffix = "_opt";
        public static string tkOneOrMoreSuffix = "_listNE";
        public static string tkZeroOrMoreSuffix = "_list";
        public static string tkUniformSetSuffix = "_set";

        public static string tkStar = "*";

        public static string RuleNameSymbol = "@";
        public static string RuleValueSymbol = "#";
        public static string RuleNameValueSymbol = "$";
        public static string RuleOptSymbol = "?";
        public static string RuleZeroOrMoreSymbol = "*";
        public static string RuleOneOrMoreSymbol = "+";
        public static string ActionStartSymbol = "{";
        public static string ProgramRuleName = "Program";
        public static string ErrorRuleName = "error";
        public static string SkipDirectiveStateName = "SKIPDIRECTIVE";
        public static string SourceEntityClassName = "SourceEntity";
        public static string SourceEntityUniformSetClassName = "SourceEntityUniformSet";
        public static string TokenClassName = "Token";

        //====================================================================================================//
        //=======================================ParserPartGenerated.cs=======================================//
        //====================================================================================================//

        /// <summary>
        /// 0 - генерируемая секция
        /// 1 - статическая секция
        /// 2 - имя пространства имен
        /// </summary>
        public static string PPGFile =
@"using System.Collections.Generic;
using AspectCore;
using QUT.Gppg;

namespace {2}
{{
{0}
{1}
}}";
        public static string PPGCommonPart =
@"
    public sealed partial class Scanner : ScanBase
    {
        internal string Text;
        private int OpenBraceCount = 0;
        private Stack<int> OpenBraceStack = new Stack<int>();

        internal int LineAdd = 0;
        private Stack<string> nextTokens = new Stack<string>();
        public void AddNextToken(string str)
        {
            nextTokens.Push(str);
        }
        public void RemoveNextToken()
        {
            if (nextTokens.Count > 0)
                nextTokens.Pop();
        }

        public string errorMsg;
        public LexLocation errorLoc;

        public override void yyerror(string format, params object[] args)
        {
            errorMsg = string.Format(format, args);
            errorLoc = yylloc;
        }

        public void GoToSkipState(int state)
        {
            OpenBraceStack.Push(OpenBraceCount);
            OpenBraceCount = 1;
            yy_push_state(state);
        }
        public void GoToSkipDirectiveState(int state)
        {
            OpenBraceStack.Push(OpenBraceCount);
            OpenBraceCount = 1;
            int tmp = yy_top_state();
            yy_pop_state();
            yy_push_state(state);
            yy_push_state(tmp);
        }
        public void ReturnToLastState()
        {
            OpenBraceCount = OpenBraceStack.Pop();
            yy_pop_state();
        }

        public void ReturnFromSkipDirective()
        {
            ReturnToLastState();
        }

        /// <summary>
        /// стек состояний на момент начала директивы #if
        /// находимся ли мы во вложенной директиве или это директива ""верхнего уровня""
        /// </summary>
        private Stack<bool> SkipStateStack = new Stack<bool>();
        /// <summary>
        /// стек флагов, был ли уже внедрен фрагмент в текущей цепочке if [elif]*
        /// </summary>
        private Stack<bool> EmbedFlagStack = new Stack<bool>();
        private HashSet<string> DefinedNames = new HashSet<string>();

        private void DivideDirective(string input, out string directive, out string param)
        {
            int space = input.IndexOf(' ');
            if (space == -1)
            {
                directive = input;
                param = """";
            }
            else
            {
                directive = input.Substring(0, space);
                param = input.Substring(space + 1);
            }
        }

        private void ProcessDirectiveInNonSkipState(string DirectiveText)
        {
            string directive;
            string param;
            DivideDirective(DirectiveText, out directive, out param);
            directive = directive.ToLower();
            if (IsDirectiveIfdef(directive))
            {
                SkipStateStack.Push(false);
                if (DefinedNames.Contains(param))
                    EmbedFlagStack.Push(true);
                else
                {
                    EmbedFlagStack.Push(false);
                    GoToSkipDirectiveState(SKIPDIRECTIVE);
                }
            }
            if (IsDirectiveIfndef(directive))
            {
                SkipStateStack.Push(false);
                if (!DefinedNames.Contains(param))
                    EmbedFlagStack.Push(true);
                else
                {
                    EmbedFlagStack.Push(false);
                    GoToSkipDirectiveState(SKIPDIRECTIVE);
                }
            }
            else if (IsDirectiveElse(directive) || IsDirectiveElif(directive))
            {
                if (EmbedFlagStack.Count == 0)
                    ;// TODO ошибка
                else
                    GoToSkipDirectiveState(SKIPDIRECTIVE);
            }
            else if (IsDirectiveEnd(directive))
            {
                if (EmbedFlagStack.Count == 0)
                    ;// TODO ошибка
                else
                {
                    EmbedFlagStack.Pop();
                    SkipStateStack.Pop();
                }
            }
            else if (IsDirectiveDefine(directive))
                DefinedNames.Add(param);
            else if (IsDirectiveUndef(directive))
                DefinedNames.Remove(param);
        }

        private void ProcessDirectiveInSkipState(string DirectiveText)
        {
            string directive;
            string param;
            DivideDirective(DirectiveText, out directive, out param);

            if (IsDirectiveIfdef(directive) || IsDirectiveIfndef(directive))
            {
                SkipStateStack.Push(true);
            }
            else if (IsDirectiveElse(directive))
            {
                if (!SkipStateStack.Peek())
                    if (EmbedFlagStack.Peek())
                        ;// TODO ошибка - уже вставляли секцию кода, а теперь надо еще раз
                    else
                        ReturnFromSkipDirective();
            }
            else if (IsDirectiveElif(directive))
            {
                if (!SkipStateStack.Peek())
                    if (DefinedNames.Contains(param))
                        if (EmbedFlagStack.Peek())
                            ;// TODO ошибка - уже вставляли секцию кода, а теперь надо еще раз
                        else
                            ReturnFromSkipDirective();
            }
            else if (IsDirectiveEnd(directive))
            {
                if (!SkipStateStack.Peek())
                {
                    ReturnFromSkipDirective();
                    EmbedFlagStack.Pop();
                }
                SkipStateStack.Pop();
            }
        }

    }
    public partial class LightweightScanner : ILightWeightScanner
    {
        Scanner _scanner;
        public object Scanner { get { return _scanner; } }
        public LightweightScanner()
        {
            _scanner = new Scanner();
        }
        public void SetSource(string source)
        {
            _scanner.Text = source;
            _scanner.LineAdd = 0;
            _scanner.ResetYYLLoc();
            _scanner.SetSource(source, 0);
        }
    }
	
	public partial class Parser : ShiftReduceParser<ValueType, LexLocation>
    {
        public SourceEntity root;
        public Parser(Scanner scanner) : base(scanner) { }
    }

    public partial class LightweightParser : LightweightParserBase
    {
        Parser _parser;
        Scanner _scanner;
        public LightweightParser(Scanner scanner)
        {
            _scanner = scanner;
            _parser = new Parser(scanner);
        }
        public override bool Parse()
        {
            _parser.root = null;
            _parser.Errors.Clear();
            bool success = _parser.Parse();
            ProcessTree();
            if (!success)
            {
                _parser.root = new SourceEntity();
                _parser.root.Items.Add(new SourceEntity(_scanner.errorMsg, _scanner.errorLoc));
            }
            return success;

        }
        public override SourceEntity Root
        { 
            get { return _parser.root; }
        }
        public override List<SourceEntity> Errors
        {
            get { return _parser.Errors; }
        }
    }
";

        public static string PPGLWParser =
@"    public partial class LightweightParser : LightweightParserBase
    {{
        public override string[] LanguageID {{ get {{ return new string[] {{ {0} }}; }} }}
    }}
";

        public static string PPGLWScanner =
@"    public partial class LightweightScanner : ILightWeightScanner
    {{
        public string[] LanguageID {{ get {{ return new string[] {{ {0} }}; }} }}
    }}
";

        public static string PPGRegionPartBody = @"return str.StartsWith({0});";
        public static string PPGRegionPartNoBody = @"return false;";
        public static string PPGRegionPart =
@"  public sealed partial class Scanner : ScanBase
	{{
        private bool IsRegionStart(string str)
        {{ {0} }}
        private bool IsRegionEnd(string str)
        {{ {1} }}
        private bool IsDirectiveDefine(string str)
        {{ {2} }}
        private bool IsDirectiveUndef(string str)
        {{ {3} }}
        private bool IsDirectiveIfdef(string str)
        {{ {4} }}
        private bool IsDirectiveIfndef(string str)
        {{ {5} }}
        private bool IsDirectiveElse(string str)
        {{ {6} }}
        private bool IsDirectiveElif(string str)
        {{ {7} }}
        private bool IsDirectiveEnd(string str)
        {{ {8} }}
    }}
";
        public static string PPGVisitorClassName = "_Visitor";
        public static string PPGBaseClassName = "_TreeNode";

        public static string PPGTreeNodeBaseClass =
@"    public class {0} : SourceEntity
    {{
        public {0}(List<string> Value, LexLocation Location)
        {{
            this.Value = Value;
            this.Location = Location;
        }}
        public {0}(string Value, LexLocation Location)
        {{
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }}
        public {0}()
        {{
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }}
        public virtual void Accept({1} v)
        {{
            v.Visit(this);
        }}
    }}
";


        public static string PPGTreeNodeClass =
@"    public class {0} : {1}
    {{
        public {0}(List<string> Value, LexLocation Location)
        {{
            this.Value = Value;
            this.Location = Location;
        }}
        public {0}(string Value, LexLocation Location)
        {{
            this.Value = new List<string>();
            this.Value.Add(Value);
            this.Location = Location;
        }}
        public {0}()
        {{
            this.Value = new List<string>();
            this.Location = new LexLocation();
        }}
        public override void Accept({2} v)
        {{
            v.Visit(this);
        }}
    }}
";
        public static string PPGVisitor =
@"    public interface {0}
    {{
{1}
    }}
";
        public static string PPGVisitorEntry = @"       void Visit({0});
";

        public static string PPGKeywordLine = @"keywords.Add({0}, (int)Tokens.{1});";
        public static string PPGKeywordToUpper = @"s = s.ToUpper();";

        //====================================================================================================//
        //============================================LWLexer.lex=============================================//
        //====================================================================================================//

        /// <summary>
        /// 0 - опции, директивы
        /// 1 - лексемы в секции объявлений
        /// 2 - состояния в секции объявлений
        /// 3 - секция правил
        /// 4 - имя пространства имен
        /// </summary>
        public static string LEXFile =
@"%namespace {4}
%using AspectCore;

%visibility public
{0}

newline [\r\n]+

{1}

{2}

%%

{3}


%{{
  yylloc = new QUT.Gppg.LexLocation(tokLin+LineAdd, tokCol, tokELin+LineAdd, tokECol);
  yylval.type_Token.Location = yylloc;
%}}

%%
int token;
internal void ResetYYLLoc()
{{
    yylloc = new QUT.Gppg.LexLocation(1,0,1,0);
}}
";

        //public static string LEXPreDefTokenIDName = "ID";
        //public static string LEXPreDefTokenIDValue = "[[:IsLetter:]_][[:IsLetterOrDigit:]_]*";
        //public static string LEXPreDefTokenSignName = "Sign";
        //public static string LEXPreDefTokenSignValue = "[[:IsPunctuation:][:IsSymbol:]]";
        //public static string LEXPreDefTokenIntNumName = "IntNum";
        //public static string LEXPreDefTokenIntNumValue = "[[:IsDigit:]]+";
        //public static string NewLineLexGroupName = "newline";
        public static string LEXCaseInsensitive = "%option CaseInsensitive";
        public static string LEXState = "%x {0}\r\n";
        public static string LEXEnteringNewState =
@"{0} {{
	GoToSkipState({1});
}}
";
        public static string LEXSkipDirectiveState =
@"<SKIPDIRECTIVE> {{
{0}
}}
";
        public static string LEXEscNoRet =
@"{0} {{ }}
";

        public static string LEXToken =
@"{0}  {{
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.{1};
}}
";

        public static string LEXStateBody =
@"<{0}> {{
	{1}
}} //end of {0}
";
        public static string LEXProcessDirective = "ProcessDirective();";
        public static string LEXNewLineToken = @"{newline}";

        public static string LEXStateBegin =
@"{0} {{
	OpenBraceCount+=1;
}}
";
        public static string LEXStateBeginRet =
@"{0} {{
	OpenBraceCount+=1;
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.anytoken;
}}
";
        public static string LEXSTateEnd =
@"{0} {{ 
    {1}
    ReturnToLastState(); }}
";

        public static string LEXSTateEndNest =
@"{0} {{
	OpenBraceCount-=1;
	if (OpenBraceCount == 0)
    {
        {1}
		ReturnToLastState();
    }
}}
";

        public static string LEXSTateEndName =
@"{0} {{ 
    {2}
    ReturnToLastState(); 
    yylval = new ValueType();
    yylval.type_Token = new Token(yytext, yylloc);
    return (int)Tokens.{1};}}
";

        public static string LEXSTateEndNameNest =
@"{0} {{
	OpenBraceCount-=1;
	if (OpenBraceCount == 0)
	{{
        {2}
		ReturnToLastState();
        yylval = new ValueType();
        yylval.type_Token = new Token(yytext, yylloc);
		return (int)Tokens.{1};
	}}
}}
";
        public static string LEXSTateEndNameNestRet =
@"{0} {{
	OpenBraceCount-=1;
    yylval = new ValueType();
    yylval.type_Token = new Token(yytext, yylloc);
	if (OpenBraceCount == 0)
	{{
        {2}
		ReturnToLastState();
		return (int)Tokens.{1};
	}}
	return (int)Tokens.anytoken;
}}
";

        //====================================================================================================//
        //============================================LWLParser.y=============================================//
        //====================================================================================================//

        public static string YActionAddNameList = "\t\t$$.AddValue(${0});\r\n";
        public static string YActionAddName = "\t\t$$.Value.Add(\"{0}\");\r\n";

        public static string YActionAddValue = "\t\t$$.AddItem(${0});\r\n";
        public static string YActionAddValueAsList = "\t\t$$.AddSubItems(${0});\r\n";

        public static string YAction = 
@"
	{{
		$$ = new {2}(new List<string>(), @1.Merge(@{0}));
        @$ = $$.Location;
{1}
	}}
";
        public static string YActionAsList =
@"
	{{
		$$ = $1;
        $$.Location = $$.Location.Merge(@{0});
{1}
	}}
";
        public static string YActionForSet1 =
@"
	{{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new {0}({1}, {3}, CurrentLocationSpan);
{2}	}}
";
        public static string YActionForSet2 =
@"
	{{
        CurrentLocationSpan = @1.Merge(@{0});
		$$ = $1;
        $$.Location = CurrentLocationSpan;
{1}        $$.AddValue(${0});
    }}
";
        public static string YActionEmpty =
@"{{ $$ = new {0}();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    {1}
}}
";
        public static string YActionPartForProgram = "\t\troot = $$;";

        /// <summary>
        /// 0 - первая секция
        /// 1 - вторая секция
        /// 2 - Union
        /// 3 - имя пространства имен
        /// </summary>
        public static string YFile =
@"%namespace {3}
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{{ 
  public SourceEntity type_SourceEntity;
{2}
}}

{0}

%%

{1}

%%
public LexLocation errBegin = new LexLocation(1,0,1,0);
public List<SourceEntity> Errors = new List<SourceEntity>();
";
        public static string YerrBeginName = "errBegin";
        public static string YUnionEntry = @"  public {0} type_{0};
";
        public static string YToken = "%token <type_Token> {0}\r\n";
        public static string YType = "%type <type_{0}> {1}\r\n";

        public static string YRule =
@"{0} :
    {1}  ;
";
        public static string YRuleTokenDelimiter = " ";
        public static string YSubruleDelimiter = "\t| ";

        public static string YPreDefProgramAction = @"{$$ = $1; root = $$;}";
        public static string YPreDefProgNodeListA1 =@"{$$ = new SourceEntity();}";
        public static string YPreDefProgNodeListA2 =
@"    {
		$$ = $1;
		$$.Items.Add($2);
	}";
        public static string YPreDefProgNode =
@"{0} :
{1};
";
        public static string YPreDefProgNodeSubRule = "{$$ = $1;}\r\n";

        public static string YErrorAction =
@"
    {{
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @{1}.StartLine, @{1}.StartColumn);
        {0} err = new {0}((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }}
";
        public static string YErrorAltBranchAction = "errBegin = @$;";
        public static string YPreDefProgNodeListName = "ProgramNodeList";
        public static string YPreDefProgNodeName = "ProgramNode";
    }
}
