%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public TXT_TreeNode type_TXT_TreeNode;
  public Token type_Token;
  public Text type_Text;
  public Header type_Header;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> Number
%token <type_Token> NL
%token <type_Token> Other
%token <type_Token> _Dot
%type <type_TXT_TreeNode> Program
%type <type_Text> Text
%type <type_Header> Header
%type <type_TXT_TreeNode> TkStart
%type <type_TXT_TreeNode> NotDot
%type <type_TXT_TreeNode> _ANY
%type <type_TXT_TreeNode> _ANY1
%type <type_TXT_TreeNode> _ANY2
%type <type_TXT_TreeNode> _ANY3
%type <type_TXT_TreeNode> _
%type <type_TXT_TreeNode> _4
%type <type_TXT_TreeNode> _5
%type <type_TXT_TreeNode> __ANY_list
%type <type_TXT_TreeNode> __ANY1_list


%%

Program :
    _ 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
		root = $$;
	}
  ;
Text :
    TkStart __ANY_list NL 
	{
		$$ = new Text(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
	| Number _4 NL 
	{
		$$ = new Text(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
Header :
    _5 Text 
	{
		$$ = new Header(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);

	}
  ;
TkStart :
    _ANY2 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
NotDot :
    _ANY3 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
_ANY :
    Number 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Other 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY1 :
    Number 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Other 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY2 :
    Other 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY3 :
    Number 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Other 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ :
    { $$ = new TXT_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| _ Text 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);
errBegin = @$;
	}
	| _ Header 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);
errBegin = @$;
	}
	| _ NL 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
errBegin = @$;
	}
	| _ error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @2.StartLine, @2.StartColumn);
        TXT_TreeNode err = new TXT_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
_4 :
    { $$ = new TXT_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| NotDot __ANY1_list 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
_5 :
    Number _Dot 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddItem($1);

	}
	| _5 Number _Dot 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
__ANY_list :
    { $$ = new TXT_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY_list _ANY 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY1_list :
    { $$ = new TXT_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY1_list _ANY1 
	{
		$$ = new TXT_TreeNode(new List<string>(), @1.Merge(@2));
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
