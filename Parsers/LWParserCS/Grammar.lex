%namespace LWParser
%using AspectCore;

%visibility public

LetterDigit [[:IsLetterOrDigit:]_]+
Sign [[:IsPunctuation:]]|[[:IsSymbol:]]

DotChr [^\r\n]
DIRECTIVE #{DotChr}*
LINECOMMENT \/\/{DotChr}*

NoStarSlash		[^*/]
Star  			[*]
Slash 			[/]
MULTILINECOMMENT {Slash}{Star}(({NoStarSlash}*{Star}*{NoStarSlash}+)?{Slash}*)*{Star}{Slash}

StringChr [^\r\n"]
String	\"{StringChr}*\"

%x SKIPBODY
%x SKIPDIRECTIVE

%%

{LetterDigit}  {
  yylval.sVal = yytext;
  int token = Keywords.KeywordOrIDToken(yytext);
  if (token != (int)Tokens.LetterDigit)
  {
	EnteringContainer = true;
  }
  return token;
}

{Sign} {
  yylval.sVal = yytext;
  if (yytext == ";")
	return (int)Tokens.tkSemicolon;
  
  if (yytext == "{")
  {
	if (!EnteringContainer)
	{
		OpenBraceCount = 1;
		yy_push_state(SKIPBODY);
	}
	EnteringContainer = false;
	return (int)Tokens.LBRACE; 
  }
  if (yytext == "}")
  {
	EnteringContainer = false;
	return (int)Tokens.RBRACE; 
  }
  
  return (int)Tokens.SIGN;
}

{String}  {
  yylval.sVal = yytext;
  return (int)Tokens.STRING;
}

<SKIPBODY> {String}  {
  yylval.sVal = yytext;
  return (int)Tokens.STRING;
}

<SKIPDIRECTIVE> {String}  {
  yylval.sVal = yytext;
  return (int)Tokens.STRING;
}

//�� ���������� ��������� - ��������� ������������
{LINECOMMENT} { 
	ProcessComment(yytext.Substring(2));
}

{MULTILINECOMMENT} { 
	ProcessComment(yytext.Substring(2, yytext.Length-4));
}

//�� ��������� �������� ���� ������ - �����������
<SKIPBODY> {LINECOMMENT} { 
	ProcessComment(yytext.Substring(2));
}

<SKIPBODY> {MULTILINECOMMENT} { 
	ProcessComment(yytext.Substring(2, yytext.Length-4));
}


//�� ��������� �������� �� ��������� - �����������
<SKIPDIRECTIVE> {LINECOMMENT} { 
	ProcessComment(yytext.Substring(2));
}

<SKIPDIRECTIVE> {MULTILINECOMMENT} { 
	ProcessComment(yytext.Substring(2, yytext.Length-4));
}

//��������� �������� �������������
{DIRECTIVE} {
	ProcessDirectiveInNonSkipState(yytext);
}
<SKIPBODY> {DIRECTIVE} { 
	ProcessDirectiveInNonSkipState(yytext);
}

<SKIPDIRECTIVE> {DIRECTIVE} { 
	ProcessDirectiveInSkipState(yytext);
}

//��������� �������� ����� �� ��������������� ����������� ������
<SKIPBODY> {Sign} { 
  if (yytext == "{")
  {
	OpenBraceCount += 1;
  }
  if (yytext == "}")
  {
	  OpenBraceCount -=1;
	  if (OpenBraceCount == 0)
	  {
		yy_pop_state();
		return (int)Tokens.RBRACE; 
	  }
  }  
}

%{
  yylloc = new QUT.Gppg.LexLocation(tokLin, tokCol, tokELin, tokECol);
%}

%%
