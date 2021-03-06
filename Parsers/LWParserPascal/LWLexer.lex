%namespace LWParser
%using AspectCore;

%visibility public
%option CaseInsensitive

newline [\r\n]+

LetterDigits  [[:IsLetterOrDigit:]_&]*
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
"{" {
	GoToSkipState(Anon2);
}
"/*" {
	GoToSkipState(Anon3);
}
"'" {
	GoToSkipState(Anon4);
}
<SKIPDIRECTIVE> {
"//" {
	GoToSkipState(Anon1);
}
"{" {
	GoToSkipState(Anon2);
}
"/*" {
	GoToSkipState(Anon3);
}
"'" {
	GoToSkipState(Anon4);
}

}
"begin"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkCodeOpen;
}
"case"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkCodeOpen;
}
"try"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkCodeOpen;
}
"internal"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkClassVisModifier;
}
"private"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkClassVisModifier;
}
"protected"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkClassVisModifier;
}
"public"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkClassVisModifier;
}
"procedure"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkMethod;
}
"function"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkMethod;
}
"constructor"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkMethod;
}
"destructor"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkMethod;
}
"forward"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkDirective;
}
"external"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkDirective;
}
"initialization"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkInitFinal;
}
"finalization"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkInitFinal;
}
"implementation"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens.tkImpl;
}
"class"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._class;
}
"interface"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._interface;
}
"record"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._record;
}
"var"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._var;
}
"const"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._const;
}
"label"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._label;
}
"virtual"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._virtual;
}
"override"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._override;
}
"reintroduce"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._reintroduce;
}
"extensionmethod"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._extensionmethod;
}
"where"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._where;
}
"("  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Ropen;
}
")"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Rclose;
}
";"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Scolon;
}
"end"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._end;
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
"."  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._Dot;
}
"type"  {
    yylval = new ValueType();
	yylval.type_Token = new Token(yytext, yylloc);
	return (int)Tokens._type;
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
<Anon2> {
	"}" { 
    
    ReturnToLastState(); }

} //end of Anon2
<Anon3> {
	"*/" { 
    
    ReturnToLastState(); }

} //end of Anon3
<Anon4> {
	"''" { }
"''" { }
"'" { 
    
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
