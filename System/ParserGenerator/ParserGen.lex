%namespace ParserGenerator
%visibility public

LetterDigit [[:IsLetterOrDigit:]_.]+
Sign [[:IsPunctuation:]]|[[:IsSymbol:]]

DotChr [^\r\n]
LINECOMMENT \/\/{DotChr}*

StringChr1 [^\r\n']
StringChr2 [^\r\n"]
String	'{StringChr1}*'|\"{StringChr2}*\"

%x REGEXP

%%

{LetterDigit}  {
  yylval = new ValueType();
  yylval.sVal = yytext;
  return Keywords.KeywordOrIDToken(yytext);
}

{Sign} {
  yylval = new ValueType();
  yylval.sVal = yytext;
  return Keywords.KeywordOrIDToken(yytext);
}

{String}  {
  yylval = new ValueType();
  yylval.sVal = yytext;
  if (yylval.sVal.StartsWith("'"))
  yylval.sVal = "\"" + yylval.sVal.Substring(1, yylval.sVal.Length-2) + "\"";
  return Keywords.KeywordOrIDToken(yytext);
}

{LINECOMMENT} { 
}

<REGEXP> {DotChr}* {
  yylval = new ValueType();
  yylval.sVal = yytext;
  BEGIN(INITIAL);
  return (int)Tokens.RegExp;
}

%{
  yylloc = new QUT.Gppg.LexLocation(tokLin, tokCol, tokELin, tokECol);
%}

%%
public void BeginRegexp()
{
	BEGIN(REGEXP);
}