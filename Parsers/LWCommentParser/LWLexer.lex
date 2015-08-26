%namespace LWParser
%using AspectCore;

%visibility public


newline [\r\n]+

Tk  [[:IsLetterOrDigit:]_]*|[[:IsPunctuation:][:IsSymbol:]]
NewLine  \r|\n|\r\n


%x SKIPDIRECTIVE


%%

<SKIPDIRECTIVE> {

}
"*("  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._StarRopen;
}
"*)"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._StarRclose;
}
{Tk}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.Tk;
}
{NewLine}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.NewLine;
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
