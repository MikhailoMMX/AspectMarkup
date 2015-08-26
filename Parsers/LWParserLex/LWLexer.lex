%namespace LWLex
%using AspectCore;

%visibility public


newline [\r\n]+

ID            		[a-zA-Z0-9_]+
NL 				[\r\n]+
Sign  [[:IsPunctuation:][:IsSymbol:]]
RegexItem 	\{[0-9]+(,[0-9]*)?\}|\{{ID}\}|\[:{ID}:\]|\[([^\[\]\r\n]|\[:{ID}:\])+\]|\\[^\r\n]|\"([^\"]|\\\")*\"|"{-}"|"{+}"|"{*}"


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
"<"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkLT;
}
">"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkGT;
}
"%"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkPC;
}
"%%"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkPC2;
}
"{"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkBOpen;
}
"}"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkBClose;
}
"%x"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkPCX;
}
"%s"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkPCS;
}
{ID}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.ID;
}
{NL}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.NL;
}
{Sign}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.Sign;
}
{RegexItem}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.RegexItem;
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
