%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public LP_TreeNode type_LP_TreeNode;
  public Token type_Token;
  public Directive type_Directive;
  public dSkip type_dSkip;
  public dToken type_dToken;
  public dRule type_dRule;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> LetterDigit
%token <type_Token> String
%token <type_Token> Sign
%token <type_Token> _Skip
%token <type_Token> _Rule
%token <type_Token> _Token
%token <type_Token> _Perc
%token <type_Token> _Colon
%type <type_LP_TreeNode> Program
%type <type_LP_TreeNode> ProgramNode
%type <type_LP_TreeNode> Tk
%type <type_Directive> Directive
%type <type_dSkip> dSkip
%type <type_dToken> dToken
%type <type_dRule> dRule
%type <type_LP_TreeNode> _ANY
%type <type_LP_TreeNode> _ProgramNode_list
%type <type_LP_TreeNode> _Tk_list
%type <type_LP_TreeNode> _Sign_opt


%%

Program :
    _ProgramNode_list 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
		root = $$;
	}
  ;
ProgramNode :
    Directive 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| dSkip 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| dToken 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| dRule 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        LP_TreeNode err = new LP_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
Tk :
    _ANY 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Directive :
    _Perc _Tk_list 
	{
		$$ = new Directive(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.Value.Add("Directive");
		$$.AddValue($2);

	}
  ;
dSkip :
    _Skip _Tk_list 
	{
		$$ = new dSkip(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
dToken :
    _Token LetterDigit _Tk_list 
	{
		$$ = new dToken(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($2);

	}
  ;
dRule :
    _Rule _Sign_opt LetterDigit _Colon _Tk_list 
	{
		$$ = new dRule(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.AddValue($3);

	}
  ;
_ANY :
    LetterDigit 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| String 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ProgramNode_list :
    { $$ = new LP_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ProgramNode_list ProgramNode 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Tk_list :
    { $$ = new LP_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Tk_list Tk 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Sign_opt :
    { $$ = new LP_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| Sign 
	{
		$$ = new LP_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;


%%
public LexLocation errBegin = new LexLocation(1,0,1,0);
public List<SourceEntity> Errors = new List<SourceEntity>();
