%namespace LWParser
%using AspectCore;

%visibility public


newline [\r\n]+

ID  [[:IsLetterOrDigit:]_]+
Sign  [[:IsPunctuation:][:IsSymbol:]]


%x SKIPDIRECTIVE
%x Anon1
%x Anon2
%x Anon3
%x Anon4


%%

"//" {
	GoToSkipState(Anon1);
}
"/*" {
	GoToSkipState(Anon2);
}
"'" {
	GoToSkipState(Anon3);
}
"\"" {
	GoToSkipState(Anon4);
}
<SKIPDIRECTIVE> {
"//" {
	GoToSkipState(Anon1);
}
"/*" {
	GoToSkipState(Anon2);
}
"'" {
	GoToSkipState(Anon3);
}
"\"" {
	GoToSkipState(Anon4);
}

}
"{"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Copen;
}
"}"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Cclose;
}
"class"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._class;
}
";"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Scolon;
}
{ID}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.ID;
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
<Anon2> {
	"*/" { 
    
    ReturnToLastState(); }

} //end of Anon2
<Anon3> {
	"''" { }
"''" { }
"'" { 
    
    ReturnToLastState(); }

} //end of Anon3
<Anon4> {
	"\\\\" { }
"\\\"" { }
"\"" { 
    
    ReturnToLastState(); }

} //end of Anon4



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
