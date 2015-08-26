%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public Y_TreeNode type_Y_TreeNode;
  public Token type_Token;
  public Program type_Program;
  public Section1 type_Section1;
  public S1Node type_S1Node;
  public Section2 type_Section2;
  public yRule type_yRule;
  public SubRule type_SubRule;
  public subRulePart type_subRulePart;
  public Section3 type_Section3;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> LetterDigit
%token <type_Token> Sign
%token <type_Token> _Copen
%token <type_Token> _Cclose
%token <type_Token> _PercPerc
%token <type_Token> _PercCopen
%token <type_Token> _PercCclose
%token <type_Token> _Perc
%token <type_Token> _Colon
%token <type_Token> _Scolon
%token <type_Token> _Pipe
%type <type_Y_TreeNode> Action
%type <type_Program> Program
%type <type_Section1> Section1
%type <type_S1Node> S1Node
%type <type_Section2> Section2
%type <type_yRule> yRule
%type <type_SubRule> SubRule
%type <type_subRulePart> subRulePart
%type <type_Section3> Section3
%type <type_SourceEntityUniformSet> _Section1_set
%type <type_SourceEntityUniformSet> _Section2_set
%type <type_SourceEntityUniformSet> _yRule_set
%type <type_SourceEntityUniformSet> _SubRule_set
%type <type_Y_TreeNode> _ANY
%type <type_Y_TreeNode> _ANY5
%type <type_Y_TreeNode> _ANY6
%type <type_Y_TreeNode> _ANY7
%type <type_Y_TreeNode> _ANY8
%type <type_Y_TreeNode> _ANY9
%type <type_Y_TreeNode> _
%type <type_Y_TreeNode> __ANY5_list
%type <type_Y_TreeNode> __ANY7_list
%type <type_Y_TreeNode> __ANY9_list


%%

Action :
    _Copen _ _Cclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("Action");

	}
  ;
Program :
    Section1 _PercPerc Section2 _PercPerc Section3 
	{
		$$ = new Program(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.Value.Add("Program");
		$$.AddItem($1);
		$$.AddItem($3);
		$$.AddItem($5);
		root = $$;
	}
  ;
Section1 :
    _Section1_set 
	{
		$$ = new Section1(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.Value.Add("Section1");
		$$.AddSubItems($1);

	}
  ;
S1Node :
    _PercCopen __ANY5_list _PercCclose 
	{
		$$ = new S1Node(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
errBegin = @$;
	}
	| _Perc _ANY6 __ANY7_list 
	{
		$$ = new S1Node(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        Y_TreeNode err = new Y_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
Section2 :
    _Section2_set 
	{
		$$ = new Section2(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.Value.Add("Section2");
		$$.AddSubItems($1);

	}
  ;
yRule :
    LetterDigit _Colon _yRule_set _Scolon 
	{
		$$ = new yRule(new List<string>(), @1.Merge(@4));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($3);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        Y_TreeNode err = new Y_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
SubRule :
    _SubRule_set 
	{
		$$ = new SubRule(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($1);

	}
  ;
subRulePart :
    _ANY8 
	{
		$$ = new subRulePart(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Action 
	{
		$$ = new subRulePart(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Section3 :
    __ANY9_list 
	{
		$$ = new Section3(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.Value.Add("Section3");

	}
  ;
_Section1_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _Section1_set S1Node 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddItem($2);
        $$.AddValue($2);
    }
  ;
_Section2_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _Section2_set yRule 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddItem($2);
        $$.AddValue($2);
    }
  ;
_yRule_set :
    SubRule 
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("|", false, CurrentLocationSpan);
		$$.AddItem($1);
		$$.Value.Add("1");

	}
	| _yRule_set _Pipe SubRule 
	{
        CurrentLocationSpan = @1.Merge(@3);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddItem($3);
        $$.AddValue($3);
    }
  ;
_SubRule_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _SubRule_set subRulePart 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddItem($2);
        $$.AddValue($2);
    }
  ;
_ANY :
    LetterDigit 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercPerc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCopen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Perc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Pipe 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY5 :
    LetterDigit 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Copen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Cclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercPerc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCopen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Perc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Pipe 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY6 :
    LetterDigit 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Cclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCopen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Pipe 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY7 :
    LetterDigit 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Copen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Cclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCopen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Pipe 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY8 :
    LetterDigit 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Cclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercPerc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCopen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Perc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY9 :
    LetterDigit 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Copen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Cclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercPerc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCopen 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _PercCclose 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Perc 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Colon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Pipe 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ :
    { $$ = new Y_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ _ANY { $$ = new Y_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ Action { $$ = new Y_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
__ANY5_list :
    { $$ = new Y_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY5_list _ANY5 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY7_list :
    { $$ = new Y_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY7_list _ANY7 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY9_list :
    { $$ = new Y_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY9_list _ANY9 
	{
		$$ = new Y_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;


%%
public LexLocation errBegin = new LexLocation(1,0,1,0);
public List<SourceEntity> Errors = new List<SourceEntity>();
