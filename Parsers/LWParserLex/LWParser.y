%namespace LWLex
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public LEX_TreeNode type_LEX_TreeNode;
  public Token type_Token;
  public Program type_Program;
  public Regex1 type_Regex1;
  public Regex2 type_Regex2;
  public IdList type_IdList;
  public Section1 type_Section1;
  public RegexDecl type_RegexDecl;
  public StateDecl type_StateDecl;
  public Section2 type_Section2;
  public RuleOrGroup type_RuleOrGroup;
  public LexRule type_LexRule;
  public Group type_Group;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> ID
%token <type_Token> NL
%token <type_Token> Sign
%token <type_Token> RegexItem
%token <type_Token> tkLT
%token <type_Token> tkGT
%token <type_Token> tkPC
%token <type_Token> tkPC2
%token <type_Token> tkBOpen
%token <type_Token> tkBClose
%token <type_Token> tkPCX
%token <type_Token> tkPCS
%type <type_LEX_TreeNode> _8
%type <type_LEX_TreeNode> _7
%type <type_LEX_TreeNode> _
%type <type_LEX_TreeNode> _ANY6
%type <type_LEX_TreeNode> _ANY5
%type <type_LEX_TreeNode> _ANY4
%type <type_LEX_TreeNode> _ANY3
%type <type_LEX_TreeNode> _ANY
%type <type_Program> Program
%type <type_LEX_TreeNode> DirectCode
%type <type_LEX_TreeNode> ContextCode
%type <type_LEX_TreeNode> RegexHead
%type <type_LEX_TreeNode> RegexTail
%type <type_Regex1> Regex1
%type <type_Regex2> Regex2
%type <type_IdList> IdList
%type <type_Section1> Section1
%type <type_LEX_TreeNode> Section1Item
%type <type_RegexDecl> RegexDecl
%type <type_StateDecl> StateDecl
%type <type_LEX_TreeNode> PercentDecl
%type <type_Section2> Section2
%type <type_LEX_TreeNode> Section2Item
%type <type_LEX_TreeNode> States
%type <type_RuleOrGroup> RuleOrGroup
%type <type_LexRule> LexRule
%type <type_Group> Group
%type <type_LEX_TreeNode> _9
%type <type_LEX_TreeNode> _10
%type <type_LEX_TreeNode> _11
%type <type_LEX_TreeNode> _12
%type <type_LEX_TreeNode> _13
%type <type_LEX_TreeNode> _Sign_opt
%type <type_LEX_TreeNode> __ANY_list
%type <type_LEX_TreeNode> __ANY3_list
%type <type_LEX_TreeNode> _RegexTail_list
%type <type_LEX_TreeNode> _Section1Item_list
%type <type_LEX_TreeNode> __ANY5_list
%type <type_LEX_TreeNode> _Section2Item_list
%type <type_LEX_TreeNode> _NL_list
%type <type_LEX_TreeNode> _States_opt
%type <type_LEX_TreeNode> __ANY6_list


%%

_8 :
    tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
_7 :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _7 _Sign_opt ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($3);
		$$.AddSubItems($1);
		$$.AddItem($3);

	}
  ;
_ :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ _ANY4 { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ ContextCode { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_ANY6 :
    ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| RegexItem 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkLT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkGT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC2 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkBClose 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY5 :
    ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| RegexItem 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkLT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkGT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC2 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkBClose 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY4 :
    ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| NL 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| RegexItem 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkLT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkGT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC2 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY3 :
    ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| NL 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| RegexItem 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkLT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkGT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC2 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkBOpen 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkBClose 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
_ANY :
    ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| NL 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| RegexItem 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkLT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkGT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPC2 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkBOpen 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkBClose 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
Program :
    Section1 tkPC2 Section2 tkPC2 __ANY_list 
	{
		$$ = new Program(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.Value.Add("Program");
		$$.AddItem($1);
		$$.AddItem($3);
		root = $$;
	}
  ;
DirectCode :
    tkPC tkBOpen __ANY3_list tkPC tkBClose 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@5));
        @$ = $$.Location;
		$$.Value.Add("DirectCode");

	}
  ;
ContextCode :
    tkBOpen _ tkBClose 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("ContextCode");

	}
  ;
RegexHead :
    RegexItem 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkPC 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkGT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
RegexTail :
    RegexHead 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkPC2 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkLT 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkPCX 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| tkPCS 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| ID 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Regex1 :
    _RegexTail_list 
	{
		$$ = new Regex1(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
Regex2 :
    RegexHead _RegexTail_list 
	{
		$$ = new Regex2(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);

	}
  ;
IdList :
    ID _7 
	{
		$$ = new IdList(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddItem($1);
		$$.AddSubItems($2);

	}
  ;
Section1 :
    _Section1Item_list 
	{
		$$ = new Section1(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.Value.Add("Section1");
		$$.AddSubItems($1);

	}
  ;
Section1Item :
    StateDecl 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| RegexDecl 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| DirectCode { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| PercentDecl { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| NL { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| error NL 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @2.StartLine, @2.StartColumn);
        LEX_TreeNode err = new LEX_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
RegexDecl :
    ID Regex1 NL 
	{
		$$ = new RegexDecl(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($2);

	}
  ;
StateDecl :
    _8 IdList NL 
	{
		$$ = new StateDecl(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($2);

	}
  ;
PercentDecl :
    tkPC __ANY5_list NL 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($2);

	}
  ;
Section2 :
    _Section2Item_list 
	{
		$$ = new Section2(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.Value.Add("Section2");
		$$.AddSubItems($1);

	}
  ;
Section2Item :
    RuleOrGroup 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| DirectCode { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| NL { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| error NL 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @2.StartLine, @2.StartColumn);
        LEX_TreeNode err = new LEX_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
States :
    tkLT _9 tkGT _NL_list 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@4));
        @$ = $$.Location;
		$$.AddValue($2);

	}
  ;
RuleOrGroup :
    _States_opt _NL_list _10 
	{
		$$ = new RuleOrGroup(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($3);

	}
  ;
LexRule :
    _11 _12 
	{
		$$ = new LexRule(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($1);

	}
  ;
Group :
    tkBOpen _13 tkBClose 
	{
		$$ = new Group(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.Value.Add("Group");
		$$.AddSubItems($2);

	}
  ;
_9 :
    IdList 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
_10 :
    Group 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
	| LexRule 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);

	}
  ;
_11 :
    Regex2 _NL_list 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _11 Regex2 _NL_list 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@3));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_12 :
    ContextCode { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY6_list NL { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
_13 :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| _13 Group 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);
errBegin = @$;
	}
	| _13 LexRule 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
		$$.AddItem($2);
errBegin = @$;
	}
	| _13 NL 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddSubItems($1);
errBegin = @$;
	}
	| _13 error NL 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @3.StartLine, @3.StartColumn);
        LEX_TreeNode err = new LEX_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
_Sign_opt :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| Sign 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
  ;
__ANY_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY_list _ANY 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY3_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY3_list _ANY3 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_RegexTail_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _RegexTail_list RegexTail 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Section1Item_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Section1Item_list Section1Item 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
__ANY5_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY5_list _ANY5 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_Section2Item_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _Section2Item_list Section2Item 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddSubItems($2);

	}
  ;
_NL_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _NL_list NL 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_States_opt :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| States 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddSubItems($1);

	}
  ;
__ANY6_list :
    { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| __ANY6_list _ANY6 
	{
		$$ = new LEX_TreeNode(new List<string>(), @1.Merge(@2));
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
