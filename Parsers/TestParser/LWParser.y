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
  public SourceEntityUniformSet type_SourceEntityUniformSet;

}

%token <type_Token> _b1
%token <type_Token> _b2
%token <type_Token> _c
%type <type_LEX_TreeNode> Program
%type <type_LEX_TreeNode> B
%type <type_LEX_TreeNode> A
%type <type_LEX_TreeNode> C


%%

Program :
    A B { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
B :
    _b1 _b2 { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
A :
    C { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;
C :
    B _c { $$ = new LEX_TreeNode();
    if (CurrentLocationSpan == null)
        CurrentLocationSpan = new LexLocation(1,0,1,0); 
    
}
  ;


%%
public LexLocation errBegin = new LexLocation(1,0,1,0);
public List<SourceEntity> Errors = new List<SourceEntity>();
