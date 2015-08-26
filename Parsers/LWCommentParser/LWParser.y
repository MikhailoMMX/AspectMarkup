%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial
%start Program
%visibility public

%union  
{ 
  public SourceEntity type_SourceEntity;
  public ANY_TreeNode type_ANY_TreeNode;
  public Token type_Token;
  public CommentStart type_CommentStart;
  public CommentEnd type_CommentEnd;
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> Tk
%token <type_Token> NewLine
%token <type_Token> _StarRopen
%token <type_Token> _StarRclose
%type <type_ANY_TreeNode> TextLine
%type <type_CommentStart> CommentStart
%type <type_CommentEnd> CommentEnd
%type <type_ANY_TreeNode> Program
%type <type_ANY_TreeNode> ProgramNode
%type <type_ANY_TreeNode> _Tk_listNE
%type <type_ANY_TreeNode> _ProgramNode_list


%%

TextLine :
    _Tk_listNE 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);

	}
  ;
CommentStart :
    _StarRopen TextLine 
	{
		$$ = new CommentStart(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($2);

	}
  ;
CommentEnd :
    _StarRclose TextLine 
	{
		$$ = new CommentEnd(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($2);

	}
  ;
Program :
    _ProgramNode_list 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddSubItems($1);
		root = $$;
	}
  ;
ProgramNode :
    CommentStart 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| CommentEnd 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddItem($1);
errBegin = @$;
	}
	| TextLine { $$ = new ANY_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| NewLine { $$ = new ANY_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    errBegin = @$;
}
	| error 
    {
        @$ = new LexLocation(errBegin.EndLine, errBegin.EndColumn, @1.StartLine, @1.StartColumn);
        ANY_TreeNode err = new ANY_TreeNode((Scanner as Scanner).errorMsg, @$);
        Errors.Add(err);
        errBegin = @$;
    }
  ;
_Tk_listNE :
    Tk 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@1));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddItem($1);

	}
	| _Tk_listNE Tk 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@2));
        @$ = $$.Location;
		$$.AddValue($1);
		$$.AddValue($2);
		$$.AddSubItems($1);
		$$.AddItem($2);

	}
  ;
_ProgramNode_list :
    { $$ = new ANY_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
	| _ProgramNode_list ProgramNode 
	{
		$$ = new ANY_TreeNode(new List<string>(), @1.Merge(@2));
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
