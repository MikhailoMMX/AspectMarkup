%namespace AspectCore.CommonLexer

%visibility public

LetterDigit [[:IsLetterOrDigit:]_]+
Sign [[:IsPunctuation:]]|[[:IsSymbol:]]

StringChr [^\r\n"]
String	\"{StringChr}*\"

WhiteSpace [[:IsWhiteSpace:]]+

%%


{LetterDigit}  {
  return 1;
}

{Sign} {
  return 1;
}

{String} {
  return 1;
}

%%