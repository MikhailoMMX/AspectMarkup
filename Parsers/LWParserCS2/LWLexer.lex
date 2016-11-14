%namespace LWParser
%using AspectCore;

%visibility public


newline [\r\n]+

LetterDigits  [[:IsLetterOrDigit:]_&]*
Sign  [[:IsPunctuation:][:IsSymbol:]]
DIRECTIVE #[^\r\n]*

%x SKIPDIRECTIVE
%x Anon1
%x Anon3
%x Anon4
%x Anon5
%x Anon6


%%

"//" {
	GoToSkipState(Anon1);
}
{DIRECTIVE} {
	ProcessDirectiveInNonSkipState(yytext);
}
"/*" {
	GoToSkipState(Anon3);
}
"'" {
	GoToSkipState(Anon4);
}
"\"" {
	GoToSkipState(Anon5);
}
"@\"" {
	GoToSkipState(Anon6);
}
<SKIPDIRECTIVE> {
{DIRECTIVE} {
	ProcessDirectiveInSkipState(yytext);
}
"//" {
	GoToSkipState(Anon1);
}

"/*" {
	GoToSkipState(Anon3);
}
"'" {
	GoToSkipState(Anon4);
}
"\"" {
	GoToSkipState(Anon5);
}
"@\"" {
	GoToSkipState(Anon6);
}

}
"class"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkClassNamespace;
}
"namespace"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkClassNamespace;
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
"["  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Sopen;
}
"]"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Sclose;
}
";"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Scolon;
}
"enum"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._enum;
}
{LetterDigits}  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.LetterDigits;
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

<Anon3> {
	"*/" { 
    
    ReturnToLastState(); }

} //end of Anon3
<Anon4> {
	"\\\\" { }
"\\'" { }
"'" { 
    
    ReturnToLastState(); }
"#" {}
{newline} {ReturnToLastState(); }
} //end of Anon4
<Anon5> {
	"\\\\" { }
"\\\"" { }
"\"" { 
    
    ReturnToLastState(); }
{newline} {ReturnToLastState(); }
"#" {}
} //end of Anon5
<Anon6> {
	"\"" { 
    
    ReturnToLastState(); }
"#" {}
	"\"\"" { }
} //end of Anon6



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
