%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public CS_TreeNode type_CS_TreeNode;
  public Token type_Token;
  public ClassOrNamespace type_ClassOrNamespace;
  public Field type_Field;
  public Method type_Method;
  public Enum type_Enum;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> LetterDigits
%token <type_Token> Sign
%token <type_Token> tkClassNamespace
%token <type_Token> _Copen
%token <type_Token> _Cclose
%token <type_Token> _Sopen
%token <type_Token> _Sclose
%token <type_Token> _Scolon
%token <type_Token> _enum
%type <type_CS_TreeNode> _ANY
%type <type_CS_TreeNode> Block
%type <type_CS_TreeNode> Attrib
%type <type_CS_TreeNode> Tk
%type <type_CS_TreeNode> Program
%type <type_CS_TreeNode> ProgramNode
%type <type_ClassOrNamespace> ClassOrNamespace
%type <type_Field> Field
%type <type_Method> Method
%type <type_Enum> Enum
%type <type_CS_TreeNode> _ANY7
%type <type_CS_TreeNode> _ANY8
%type <type_CS_TreeNode> _ANY9
%type <type_CS_TreeNode> _
%type <type_CS_TreeNode> _10
%type <type_CS_TreeNode> _ProgramNode_list
%type <type_CS_TreeNode> _Tk_list
%type <type_CS_TreeNode> __Scolon_opt
%type <type_CS_TreeNode> __ANY9_list


%%

_ANY :
    LetterDigits 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassNamespace 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _enum 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
Block :
    _Copen _ _Cclose 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("Block");

	}
  ;
Attrib :
    _Sopen _10 _Sclose { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
Tk :
    _ANY8 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Program :
    _ProgramNode_list 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
		root = $$;
	}
  ;
ProgramNode :
    ClassOrNamespace 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Field 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Method 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Enum 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Attrib { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        CS_TreeNode err = new CS_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
ClassOrNamespace :
    _Tk_list tkClassNamespace _Tk_list _Copen _ProgramNode_list _Cclose __Scolon_opt 
	{
		$$ = new ClassOrNamespace(new List<string>(), @1.Merge(@7));
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
    _Tk_list Block __Scolon_opt 
	{
		$$ = new Method(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Enum :
    _Tk_list _enum _Tk_list _Copen __ANY9_list _Cclose __Scolon_opt 
	{
		$$ = new Enum(new List<string>(), @1.Merge(@7));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);

	}
  ;
_ANY7 :
    LetterDigits 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassNamespace 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Copen 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Cclose 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _enum 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY8 :
    LetterDigits 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY9 :
    LetterDigits 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassNamespace 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Copen 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _enum 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ :
    { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ _ANY { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ Block { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_10 :
    { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _10 _ANY7 { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _10 Attrib { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_ProgramNode_list :
    { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ProgramNode_list ProgramNode 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Tk_list :
    { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Tk_list Tk 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__Scolon_opt :
    { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Scolon 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
__ANY9_list :
    { $$ = new CS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY9_list _ANY9 
	{
		$$ = new CS_TreeNode(new List<string>(), @1.Merge(@2));
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
