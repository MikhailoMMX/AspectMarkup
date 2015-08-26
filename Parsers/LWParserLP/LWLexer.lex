%namespace LWParser
%using AspectCore;

%visibility public


newline [\r\n]+

LetterDigit  [[:IsLetterOrDigit:]_]*
String  \"([^\"\\]|\\.)*\"|\'([^\'\\]|\\.)*\'
Sign  [[:IsPunctuation:][:IsSymbol:]]|\\\"|\\\'


%x SKIPDIRECTIVE
%x Anon1


%%

"//" {
	GoToSkipState(Anon1);
}
<SKIPDIRECTIVE> {
"//" {
	GoToSkipState(Anon1);
}

}
"Skip"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Skip;
}
"Rule"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Rule;
}
"Token"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Token;
}
"%"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Perc;
}
":"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Colon;
}
{LetterDigit}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.LetterDigit;
}
{String}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.String;
}
{Sign}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.Sign;
}
<Anon1> {
	{newline} { 
    
    ReturnToLastState(); }

} //end of Anon1



%{
  yylloc = new QUT.Gppg.LexLocation(tokLin+LineAdd, tokCol, tokELin+LineAdd, tokECol);
  yylval.type_Token.Location = yylloc;
%}

%%
int token;
internal void ResetYYLLoc()
{
    yylloc = new QUT.Gppg.LexLocation(1,0,1,0);
}
