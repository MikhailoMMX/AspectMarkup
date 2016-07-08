%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public C_TreeNode type_C_TreeNode;
  public Token type_Token;
  public ClassOrNamespace type_ClassOrNamespace;
  public Field type_Field;
  public Method type_Method;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> LetterDigits
%token <type_Token> Sign
%token <type_Token> tkClassNamespace
%token <type_Token> _Copen
%token <type_Token> _Cclose
%token <type_Token> _Scolon
%type <type_C_TreeNode> _ANY
%type <type_C_TreeNode> Block
%type <type_C_TreeNode> Tk
%type <type_C_TreeNode> Program
%type <type_C_TreeNode> ProgramNode
%type <type_ClassOrNamespace> ClassOrNamespace
%type <type_Field> Field
%type <type_Method> Method
%type <type_C_TreeNode> _ANY7
%type <type_C_TreeNode> _
%type <type_C_TreeNode> _ProgramNode_list
%type <type_C_TreeNode> _Tk_list


%%

_ANY :
    LetterDigits 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassNamespace 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
Block :
    _Copen _ _Cclose 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("Block");

	}
  ;
Tk :
    _ANY7 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Program :
    _ProgramNode_list 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
		root = $$;
	}
  ;
ProgramNode :
    ClassOrNamespace 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Field 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Method 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        C_TreeNode err = new C_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
ClassOrNamespace :
    _Tk_list tkClassNamespace _Tk_list _Copen _ProgramNode_list _Cclose 
	{
		$$ = new ClassOrNamespace(new List<string>(), @1.Merge(@6));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
		$$.AddSubItems($5);

	}
  ;
Field :
    _Tk_list _Scolon 
	{
		$$ = new Field(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Method :
    _Tk_list Block 
	{
		$$ = new Method(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
_ANY7 :
    LetterDigits 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ :
    { $$ = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ _ANY { $$ = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ Block { $$ = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_ProgramNode_list :
    { $$ = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ProgramNode_list ProgramNode 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Tk_list :
    { $$ = new C_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Tk_list Tk 
	{
		$$ = new C_TreeNode(new List<string>(), @1.Merge(@2));
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
