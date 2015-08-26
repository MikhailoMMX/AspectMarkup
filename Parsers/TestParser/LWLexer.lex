%namespace LWLex
%using AspectCore;

%visibility public


newline [\r\n]+



%x SKIPDIRECTIVE
%x Anon1
%x Anon2


%%

"/*" {
	GoToSkipState(Anon1);
}
"//" {
	GoToSkipState(Anon2);
}
<SKIPDIRECTIVE> {
"/*" {
	GoToSkipState(Anon1);
}
"//" {
	GoToSkipState(Anon2);
}

}
"b1"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._b1;
}
"b2"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._b2;
}
"c"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._c;
}
<Anon1> {
	"*/" { 
    
    ReturnToLastState(); }

} //end of Anon1
<Anon2> {
	{newline} { 
    
    ReturnToLastState(); }

} //end of Anon2



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
