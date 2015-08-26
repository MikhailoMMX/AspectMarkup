%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public JAVA_TreeNode type_JAVA_TreeNode;
  public Token type_Token;
  public Code type_Code;
  public Imports type_Imports;
  public Class type_Class;
  public Field type_Field;
  public Method type_Method;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> ID
%token <type_Token> Sign
%token <type_Token> _Copen
%token <type_Token> _Cclose
%token <type_Token> _class
%token <type_Token> _Scolon
%type <type_Code> Code
%type <type_JAVA_TreeNode> TKList
%type <type_JAVA_TreeNode> Program
%type <type_Imports> Imports
%type <type_Class> Class
%type <type_JAVA_TreeNode> ClassEntity
%type <type_Field> Field
%type <type_Method> Method
%type <type_JAVA_TreeNode> _ANY
%type <type_JAVA_TreeNode> _ANY5
%type <type_JAVA_TreeNode> _
%type <type_JAVA_TreeNode> __ANY5_list
%type <type_JAVA_TreeNode> _Imports_list
%type <type_JAVA_TreeNode> _Class_listNE
%type <type_JAVA_TreeNode> _ClassEntity_list


%%

Code :
    _Copen _ _Cclose 
	{
		$$ = new Code(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("Code");

	}
  ;
TKList :
    __ANY5_list 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Program :
    _Imports_list _Class_listNE 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddSubItems($2);
		root = $$;
	}
  ;
Imports :
    TKList _Scolon 
	{
		$$ = new Imports(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Class :
    TKList _class ID _Copen _ClassEntity_list _Cclose 
	{
		$$ = new Class(new List<string>(), @1.Merge(@6));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
		$$.AddSubItems($5);

	}
  ;
ClassEntity :
    Field 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| Method 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
  ;
Field :
    TKList _Scolon 
	{
		$$ = new Field(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Method :
    TKList Code 
	{
		$$ = new Method(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($2);

	}
  ;
_ANY :
    ID 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY5 :
    ID 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ :
    { $$ = new JAVA_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ _ANY { $$ = new JAVA_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ Code { $$ = new JAVA_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
__ANY5_list :
    { $$ = new JAVA_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY5_list _ANY5 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Imports_list :
    { $$ = new JAVA_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Imports_list Imports 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_Class_listNE :
    Class 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Class_listNE Class 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_ClassEntity_list :
    { $$ = new JAVA_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ClassEntity_list ClassEntity 
	{
		$$ = new JAVA_TreeNode(new List<string>(), @1.Merge(@2));
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
