%namespace LWParser
%using AspectCore;
%output=LWParser.cs 
%partial 
%start Program
%visibility public

%union  
{ 
  public string sVal;  
  public List<string> sList;
  public SourceEntity se;
}

%token <sVal> LetterDigit
%token <sVal> SIGN
%token <sVal> STRING
%token <sVal> tkNamespaceOrClass
%token tkSemicolon
%token LBRACE RBRACE

%type <sList> TokenList
%type <sVal> Token
%type <se> ProgramNode
%type <se> ProgramNodeList


%%
Program
	: ProgramNodeList {
		root = $1;
	}
	;

ProgramNodeList
	:
	{ 
		$$ = new SourceEntity();
	}
	| ProgramNodeList ProgramNode
	{
		$$ = $1;
		if ($2 == null)
			return;
		$1.Items.Add($2);
		if ($$.Location == null)
			$$.Location = @2;
		else
			$$.Location = $$.Location.Merge($2.Location);
	}
	;
	
ProgramNode
	: 
	error
	{
		// ошибка - тоже сущность!
		if (NextToken == (int)Tokens.EOF && NestingLevel > 0)
            NextToken = (int)Tokens.RBRACE;
		//"Scanner as Scanner": {1} - Property, {2} - class
		(Scanner as Scanner).ResetEnteringContainer();
	}
	| TokenList tkSemicolon
	{ 
		//поле и все такое
		$$ = new SourceEntity($1, @1.Merge(@2));
	}
	| TokenList LBRACE TokenList RBRACE 
	{
		// Метод
		$$ = new SourceEntity($1, @1.Merge(@4));
	}
	| TokenList tkNamespaceOrClass TokenList LBRACE {NestingLevel +=1;} ProgramNodeList RBRACE 
	{
		//Пространство имен или класс
		$3.Insert(0, $2);
		$$ = new SourceEntity($3, @1.Merge(@7));
		$$.Items = $6.Items;
		NestingLevel -= 1;
	}
	;
	
TokenList :
	// empty
	{
		$$ = new List<string>();
	}
	| TokenList Token {
			if ($2 != null)
				$1.Add($2);
			$$ = $1;
		}
	;
	
Token
	: SIGN {$$ = $1;}
	| LetterDigit {$$ = $1;}
	| STRING {$$ = $1;}
	;	
%%

private int NestingLevel = 0;