%namespace ParserGenerator
%output=ParserGenParser.cs 
%partial 
%start Program
%visibility public

%union  
{ 
  public string sVal;  
  public List<string> sList;
  public HashSet<string> sHS;
  public SourceFile sf;
  public Declaration Decl;
  public SkipDeclaration skipDs;
  public RuleDeclaration ruleDs;
  public SubRuleRepetition srRep;
  public SubRulePart srp;
  public List<SubRulePart> srpL;
  public bool flag;
}

%token <sVal> Token RegExp tkList
%token tkRule tkSkip tkBlock tkRegExToken tkNested tkBegin tkEnd tkBeginEnd tkEscapeSymbol tkOpen tkClose tkAny tkAnyExcept
%token tkColon tkPipe tkAt tkSharp tkQuest tkPlus tkStar tkPercent tkEq tkSquareOpen tkSquareClose tkRoundOpen tkRoundClose tkComma
%token tkCaseSensitive tkCaseInsensitive tkExtension tkRegion tkPreprocessor tkNamespace
%token tkDefine tkUndef tkIfdef tkIfndef tkElse tkElif

%type <sf> Program
%type <Decl> Declaration
%type <skipDs> SkipParameters
%type <skipDs> SkipParam
%type <sList> TokenList
%type <sList> TokenCommaList
%type <ruleDs> SubRules
%type <sVal> UseNameValue
%type <srRep> Repetition
%type <srp> SubRuleToken
%type <srpL> SubRule
%type <flag> UseRuleName
%type <sHS> ExceptList

%%
Program
	: {
		$$ = new SourceFile();
		root = $$;
	}
	| Program Declaration {
		$$ = $1;
		if ($2 != null)
		{
			$2.Location = @2;
			$$.Declarations.Add($2);
		}
	}
	;

Declaration
	:	error
	{
	}
	| tkSkip SkipParameters {
		$$ = new SkipDeclaration();
		($$ as SkipDeclaration).Merge($2);
	}
	| tkRule UseRuleName Token tkColon SubRules {
		RuleDeclaration rd = new RuleDeclaration();
		rd.UseRuleName = $2;
		rd.Name = $3;
		rd.Merge($5);
		$$ = rd;
	}
	| tkPercent Directive
	| tkRegExToken Token {(Scanner as Scanner).BeginRegexp();} RegExp {
		$$ = new RegExTokenDeclaration($2, $4);
	}
	;
	
SkipParameters
	: 
	{
		$$ = new SkipDeclaration();
	}
	| SkipParameters SkipParam
	{
		$$ = $1;
		$$.Merge($2);
	}
	;
	
SkipParam
	: tkNested
	{
		$$ = new SkipDeclaration();
		$$.Nested = true;
	}
	| tkBegin TokenList
	{
		$$ = new SkipDeclaration();
		$$.Begin.AddRange($2.ToArray());
	}
	| tkEnd TokenList
	{
		$$ = new SkipDeclaration();
		$$.End.AddRange($2.ToArray());
	}
	| tkBeginEnd TokenList
	{
		$$ = new SkipDeclaration();
		$$.Begin.AddRange($2.ToArray());
		$$.End.AddRange($2.ToArray());
	}
	| tkEscapeSymbol TokenList
	{
		$$ = new SkipDeclaration();
		$$.EscapeSymbol.AddRange($2.ToArray());
	}
	| tkPreprocessor
	{
		$$ = new SkipDeclaration();
		$$.Preprocessor = true;
	}
	;
	
TokenList
	:
	{
		$$ = new List<string>();
	}
	| TokenList Token
	{
		$$ = $1;
		$$.Add($2);
	}
	;
TokenCommaList
	:
	{
		$$ = new List<string>();
	}
	| Token	{
		$$ = new List<string>();
		$$.Add($1);
	}
	| TokenCommaList tkComma Token
	{
		$$ = $1;
		$$.Add($3);
	}
	;

UseNameValue
	: {$$ = "";}
	| tkAt {$$ = "@";}
	| tkSharp {$$ = "#";}
	| tkAt tkSharp {$$ = "@#";}
	| tkSharp tkAt {$$ = "@#";}
	;

UseRuleName
	: {$$ = false; }
	| tkAt {$$ = true; }
	;
	
Repetition
	:	{$$ = SubRuleRepetition.Once;}
	| tkQuest {$$ = SubRuleRepetition.ZeroOrOne;}
	| tkPlus {$$ = SubRuleRepetition.OneOrMore;}
	| tkStar {$$ = SubRuleRepetition.ZeroOrMore;}
	;
	
SubRuleToken
	: UseNameValue Token Repetition
	{
		SubRulePart srp = new SubRulePart($2);
		srp.UseName = $1 == "@" || $1 == "@#";
		srp.UseValue = $1 == "#" || $1 == "@#";
		srp.Repetition = $3;
		$$ = srp;
	}
	| 	UseNameValue tkAny Repetition
	{
		SubRuleAny sr = new SubRuleAny();
		sr.UseName = $1 == "@" || $1 == "@#";
		sr.UseValue = $1 == "#" || $1 == "@#";
		sr.Repetition = $3;
		$$ = sr;
	}
	| 	UseNameValue tkAnyExcept tkRoundOpen ExceptList tkRoundClose Repetition
	{
		SubRuleAny sr = new SubRuleAny();
		sr.UseName = $1 == "@" || $1 == "@#";
		sr.UseValue = $1 == "#" || $1 == "@#";
		sr.Repetition = $6;
		sr.Except = $4;
		$$ = sr;
	}
	| 	UseNameValue tkList tkRoundOpen TokenCommaList tkRoundClose Repetition
	{
		if ($4.Count == 0 || $4.Count>2)
			ErrorReporter.WriteError(ErrorMessages.WrongListParameters, @2);
		else
		{
			string NT = $4[0];
			string Sep = "";
			if ($4.Count > 1)
				Sep = $4[1];
			SubRuleNonTermList sr = new SubRuleNonTermList(NT, Sep);
			sr.UseName = $1 == "@" || $1 == "@#";
			sr.CanBeEmpty = $2 == StringConstants.tkList0;
			sr.Repetition = $6;
			$$ = sr;
		}
	}
	| tkSquareOpen SubRules tkSquareClose Repetition
	{
		$$ = new SubRuleComplexPart($2.SubRules);
		$$.Repetition = $4;
	}
	;
	
SubRule
	:	{$$ = new List<SubRulePart>();}
	| SubRule SubRuleToken
	{
		$$ = $1;
		$$.Add($2);
		$2.Location = @2;
	}
	;
	
SubRules
	: SubRule
	{
		$$ = new RuleDeclaration();
		$$.SubRules.Add($1);
	}
	| SubRules tkPipe SubRule
	{
		$$ = $1;
		$$.SubRules.Add($3);
		$$.Location = @1.Merge(@3);
	}
	;
	
ExceptList
	: Token {$$ = new HashSet<string>(); $$.Add($1); }
	| ExceptList tkComma Token {$$ = $1; $$.Add($3); }
	;
	
Directive
	: tkCaseSensitive  {Options.CaseInsensitive = false;}
	|	tkCaseInsensitive  {Options.CaseInsensitive = true;}
	|	tkExtension TokenList {Options.ExtensionList = $2;}
	|	tkRegion tkBegin tkEq Token tkEnd tkEq Token
	{
		Options.RegionBegin = $4;
		Options.RegionEnd = $7;
	}
	| tkPreprocessor PreProcDirective {}
	| tkNamespace Token { Options.Namespace = $2; }
	;
	
PreProcDirective
	: PreProcDirectivePart {}
	| PreProcDirective PreProcDirectivePart {}
	;

PreProcDirectivePart
	: tkDefine tkEq Token {Options.DirectiveDefine = $3;}
	| tkUndef tkEq Token {Options.DirectiveUndef = $3;}
	| tkIfdef tkEq Token {Options.DirectiveIfdef = $3;}
	| tkIfndef tkEq Token {Options.DirectiveIfndef = $3;}
	| tkElse tkEq Token {Options.DirectiveElse = $3;}
	| tkElif tkEq Token {Options.DirectiveElif = $3;}
	| tkEnd tkEq Token {Options.DirectiveEnd = $3;}
	;
%%
