%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public PAS_TreeNode type_PAS_TreeNode;
  public Token type_Token;
  public Block type_Block;
  public Attribute type_Attribute;
  public Field type_Field;
  public Class type_Class;
  public Method type_Method;
  public MethodHeader type_MethodHeader;
  public ClassMethod type_ClassMethod;
  public Defs type_Defs;
  public InitFinal type_InitFinal;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> LetterDigits
%token <type_Token> Sign
%token <type_Token> tkCodeOpen
%token <type_Token> tkDefs
%token <type_Token> tkClassVisModifier
%token <type_Token> tkMethod
%token <type_Token> tkDirective
%token <type_Token> tkDirectiveHeader
%token <type_Token> tkInitFinal
%token <type_Token> tkImpl
%token <type_Token> _class
%token <type_Token> _interface
%token <type_Token> _record
%token <type_Token> _Ropen
%token <type_Token> _Rclose
%token <type_Token> _end
%token <type_Token> _Sopen
%token <type_Token> _Sclose
%token <type_Token> _Dot
%token <type_Token> _type
%token <type_Token> _Scolon
%type <type_PAS_TreeNode> _ANY7
%type <type_PAS_TreeNode> tkClassOpen
%type <type_PAS_TreeNode> _ANY6
%type <type_PAS_TreeNode> _ANY
%type <type_SourceEntityUniformSet> _Defs_set
%type <type_SourceEntityUniformSet> _ClassMethodBody_set
%type <type_SourceEntityUniformSet> _MethodBody_set
%type <type_SourceEntityUniformSet> _Class_set
%type <type_PAS_TreeNode> tkInterf
%type <type_SourceEntityUniformSet> _Program_set
%type <type_PAS_TreeNode> Params
%type <type_Block> Block
%type <type_Attribute> Attribute
%type <type_PAS_TreeNode> Tk
%type <type_PAS_TreeNode> Tk2
%type <type_PAS_TreeNode> Program
%type <type_PAS_TreeNode> ProgramNode
%type <type_PAS_TreeNode> ClassNode
%type <type_PAS_TreeNode> InterfacePart
%type <type_PAS_TreeNode> InterfNode
%type <type_Field> Field
%type <type_Class> Class
%type <type_Method> Method
%type <type_MethodHeader> MethodHeader
%type <type_PAS_TreeNode> MethodBody
%type <type_PAS_TreeNode> MethodLocals
%type <type_ClassMethod> ClassMethod
%type <type_PAS_TreeNode> ClassMethodBody
%type <type_Defs> Defs
%type <type_PAS_TreeNode> FieldOrClass
%type <type_PAS_TreeNode> Directive
%type <type_PAS_TreeNode> DirectiveHeader
%type <type_InitFinal> InitFinal
%type <type_PAS_TreeNode> CodeToken
%type <type_PAS_TreeNode> _ANY8
%type <type_PAS_TreeNode> _ANY9
%type <type_PAS_TreeNode> _ANY10
%type <type_PAS_TreeNode> _
%type <type_PAS_TreeNode> _11
%type <type_PAS_TreeNode> _12
%type <type_PAS_TreeNode> _13
%type <type_PAS_TreeNode> _14
%type <type_PAS_TreeNode> __ANY8_list
%type <type_PAS_TreeNode> __class_opt
%type <type_PAS_TreeNode> _InterfNode_list
%type <type_PAS_TreeNode> _Tk2_list
%type <type_PAS_TreeNode> _Params_opt
%type <type_PAS_TreeNode> _DirectiveHeader_list
%type <type_PAS_TreeNode> __ANY9_list
%type <type_PAS_TreeNode> _CodeToken_list


%%

_ANY7 :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkCodeOpen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassVisModifier 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirective 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkInitFinal 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Ropen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Rclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _end 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
tkClassOpen :
    _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
_ANY6 :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassVisModifier 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirective 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkInitFinal 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Ropen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Rclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkCodeOpen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassVisModifier 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirective 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkInitFinal 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _end 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_Defs_set :
    FieldOrClass 
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", false, CurrentLocationSpan);
		$$.AddSubItems($1);
		$$.Value.Add("1");

	}
	| _Defs_set FieldOrClass 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddSubItems($2);
        $$.AddValue($2);
    }
  ;
_ClassMethodBody_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _ClassMethodBody_set Defs 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddItem($2);
        $$.AddValue($2);
    }
  ;
_MethodBody_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _MethodBody_set MethodLocals 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddSubItems($2);
        $$.AddValue($2);
    }
  ;
_Class_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _Class_set ClassNode 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddSubItems($2);
        $$.AddValue($2);
    }
  ;
tkInterf :
    _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
_Program_set :
    
	{
        CurrentLocationSpan = Scanner.yylloc;
		$$ = new SourceEntityUniformSet("", true, CurrentLocationSpan);

	}
	| _Program_set ProgramNode 
	{
        CurrentLocationSpan = @1.Merge(@2);
		$$ = $1;
        $$.Location = CurrentLocationSpan;
		$$.AddSubItems($2);
        $$.AddValue($2);
    }
  ;
Params :
    _Ropen _ _Rclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);

	}
  ;
Block :
    tkCodeOpen _11 _end 
	{
		$$ = new Block(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("Block");

	}
  ;
Attribute :
    _Sopen _12 _Sclose 
	{
		$$ = new Attribute(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);

	}
  ;
Tk :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Params 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Tk2 :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Params 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _Sopen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _Sclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Program :
    _Program_set _13 __ANY8_list 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddSubItems($2);
		root = $$;
	}
  ;
ProgramNode :
    Defs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Field 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Method 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| InitFinal 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| InterfacePart 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
errBegin = @$;
	}
	| Attribute 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        PAS_TreeNode err = new PAS_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
ClassNode :
    tkClassVisModifier { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| __class_opt Field 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddItem($2);
errBegin = @$;
	}
	| ClassMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Attribute 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        PAS_TreeNode err = new PAS_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
InterfacePart :
    tkInterf _InterfNode_list tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddSubItems($2);

	}
  ;
InterfNode :
    Defs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Field 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| MethodHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| Attribute 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        PAS_TreeNode err = new PAS_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
Field :
    Tk _Tk2_list _Scolon 
	{
		$$ = new Field(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
Class :
    Tk _Tk2_list tkClassOpen _Params_opt _Class_set _end _Scolon 
	{
		$$ = new Class(new List<string>(), @1.Merge(@7));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
		$$.AddValue($4);
		$$.AddSubItems($5);

	}
  ;
Method :
    MethodHeader MethodBody 
	{
		$$ = new Method(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($2);

	}
  ;
MethodHeader :
    __class_opt tkMethod _Tk2_list _Scolon _DirectiveHeader_list 
	{
		$$ = new MethodHeader(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);
		$$.AddValue($5);

	}
  ;
MethodBody :
    Directive { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _MethodBody_set Block _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddSubItems($1);

	}
  ;
MethodLocals :
    Defs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| Method 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
  ;
ClassMethod :
    MethodHeader ClassMethodBody 
	{
		$$ = new ClassMethod(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($2);

	}
  ;
ClassMethodBody :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| Directive { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ClassMethodBody_set Block _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddSubItems($1);

	}
  ;
Defs :
    _14 _Defs_set 
	{
		$$ = new Defs(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($2);

	}
  ;
FieldOrClass :
    Field 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| Class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
  ;
Directive :
    tkDirective __ANY9_list _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
DirectiveHeader :
    tkDirectiveHeader _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
InitFinal :
    tkInitFinal _CodeToken_list 
	{
		$$ = new InitFinal(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
CodeToken :
    _ANY10 { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| Block { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_ANY8 :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkCodeOpen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassVisModifier 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirective 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkInitFinal 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Ropen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Rclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _end 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY9 :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkCodeOpen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassVisModifier 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirective 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkInitFinal 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Ropen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Rclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _end 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY10 :
    LetterDigits 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkClassVisModifier 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkMethod 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirective 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkDirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkImpl 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _interface 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _record 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Ropen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Rclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sopen 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Sclose 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Scolon 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ _ANY 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
	| _ Params 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
_11 :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _11 _ANY6 { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _11 Block { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_12 :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _12 _ANY7 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
	| _12 Attribute 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
_13 :
    Block _Dot 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| _end _Dot { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_14 :
    tkDefs 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| _type 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
__ANY8_list :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY8_list _ANY8 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__class_opt :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _class 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_InterfNode_list :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _InterfNode_list InterfNode 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Tk2_list :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Tk2_list Tk2 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Params_opt :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| Params 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($1);

	}
  ;
_DirectiveHeader_list :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _DirectiveHeader_list DirectiveHeader 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY9_list :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY9_list _ANY9 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_CodeToken_list :
    { $$ = new PAS_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _CodeToken_list CodeToken 
	{
		$$ = new PAS_TreeNode(new List<string>(), @1.Merge(@2));
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
