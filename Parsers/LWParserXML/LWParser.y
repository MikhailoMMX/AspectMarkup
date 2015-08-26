%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public XML_TreeNode type_XML_TreeNode;
  public Token type_Token;
  public XMLDecl type_XMLDecl;
  public DoctypeDcecl type_DoctypeDcecl;
  public Tag type_Tag;
  public Attribute type_Attribute;
  public Text type_Text;
  public String type_String;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> ID
%token <type_Token> Sign
%token <type_Token> TagStart
%token <type_Token> TagEnd
%token <type_Token> _Quest
%token <type_Token> _xml
%token <type_Token> _Excl
%token <type_Token> _DOCTYPE
%token <type_Token> _Slash
%token <type_Token> _
%token <type_Token> _BslashDquote
%type <type_XML_TreeNode> _ANY2
%type <type_XML_TreeNode> _ANY
%type <type_XML_TreeNode> Program
%type <type_XMLDecl> XMLDecl
%type <type_DoctypeDcecl> DoctypeDcecl
%type <type_Tag> Tag
%type <type_XML_TreeNode> CloseTag
%type <type_Attribute> Attribute
%type <type_Text> Text
%type <type_String> String
%type <type_XML_TreeNode> _ANY3
%type <type_XML_TreeNode> _4
%type <type_XML_TreeNode> _5
%type <type_XML_TreeNode> _Attribute_list
%type <type_XML_TreeNode> __ANY_listNE
%type <type_XML_TreeNode> __ANY2_listNE
%type <type_XML_TreeNode> __ANY3_list


%%

_ANY2 :
    ID 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Quest 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _xml 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Excl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _DOCTYPE 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Slash 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _ 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _BslashDquote 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY :
    ID 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| TagStart 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Quest 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _xml 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Excl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _DOCTYPE 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Slash 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _BslashDquote 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
Program :
    _4 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
		root = $$;
	}
  ;
XMLDecl :
    TagStart _Quest _xml _Attribute_list _Quest TagEnd 
	{
		$$ = new XMLDecl(new List<string>(), @1.Merge(@6));
        @$ = $$.Location;
		$$.AddValue($3);
		$$.AddSubItems($4);

	}
  ;
DoctypeDcecl :
    TagStart _Excl _DOCTYPE _Attribute_list TagEnd 
	{
		$$ = new DoctypeDcecl(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.AddValue($3);
		$$.AddSubItems($4);

	}
  ;
Tag :
    TagStart ID _Attribute_list TagEnd _5 CloseTag 
	{
		$$ = new Tag(new List<string>(), @1.Merge(@6));
        @$ = $$.Location;
		$$.AddValue($2);
		$$.AddSubItems($3);
		$$.AddSubItems($5);

	}
	| TagStart ID _Attribute_list _Slash TagEnd 
	{
		$$ = new Tag(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.AddValue($2);
		$$.AddSubItems($3);

	}
  ;
CloseTag :
    TagStart _Slash ID TagEnd 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@4));
        @$ = $$.Location;
		$$.AddValue($3);

	}
  ;
Attribute :
    __ANY_listNE _ String 
	{
		$$ = new Attribute(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($3);

	}
  ;
Text :
    __ANY2_listNE 
	{
		$$ = new Text(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
String :
    _BslashDquote __ANY3_list _BslashDquote 
	{
		$$ = new String(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddValue($3);

	}
  ;
_ANY3 :
    ID 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| TagStart 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| TagEnd 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Quest 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _xml 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Excl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _DOCTYPE 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Slash 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _ 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_4 :
    XMLDecl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| DoctypeDcecl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| Tag 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| _4 XMLDecl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
	| _4 DoctypeDcecl 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
	| _4 Tag 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_5 :
    { $$ = new XML_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _5 Tag 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
	| _5 Text 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_Attribute_list :
    { $$ = new XML_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Attribute_list Attribute 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
__ANY_listNE :
    _ANY 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($1);

	}
	| __ANY_listNE _ANY 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY2_listNE :
    _ANY2 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($1);

	}
	| __ANY2_listNE _ANY2 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY3_list :
    { $$ = new XML_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY3_list _ANY3 
	{
		$$ = new XML_TreeNode(new List<string>(), @1.Merge(@2));
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
