%namespace TestFilesGeneratorCS

%visibility public

LetterDigit [[:IsLetterOrDigit:]_]+
Sign [[:IsPunctuation:]]|[[:IsSymbol:]]

DotChr [^\r\n]
DIRECTIVE #{DotChr}*
LINECOMMENT \/\/{DotChr}*

NoStarSlash		[^*/]
Star  			[*]
Slash 			[/]
MULTILINECOMMENT {Slash}{Star}(({NoStarSlash}*{Star}*{NoStarSlash}+)?{Slash}*)*{Star}{Slash}

StringChr [^\r\n"]
String	\"{StringChr}*\"

WhiteSpace [[:IsWhiteSpace:]]+

%%

{WhiteSpace}  {
  return 1;
}

{LetterDigit}  {
  return 1;
}

{Sign} {
  return 1;
}

{String} {
  return 1;
}

{LINECOMMENT} { 
	return 1;
}

{MULTILINECOMMENT} { 
	return 1;
}

{DIRECTIVE} {
	return 1;
}

%%