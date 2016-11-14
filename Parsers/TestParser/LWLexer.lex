%namespace LWParser
%using AspectCore;

%visibility public


newline [\r\n]+

Number  	[0-9]+
NL 		[\r\n]+
Other 		[^0-9\r\n[:IsWhiteSpace:]]+


%x SKIPDIRECTIVE


%%

<SKIPDIRECTIVE> {

}
"."  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Dot;
}
{Number}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.Number;
}
{NL}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.NL;
}
{Other}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.Other;
}



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
