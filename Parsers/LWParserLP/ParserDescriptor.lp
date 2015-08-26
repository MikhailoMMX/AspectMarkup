%Extension "lp"
%CaseSensitive

Skip Begin "//"

Token LetterDigit [[:IsLetterOrDigit:]_]*
Token String \"([^\"\\]|\\.)*\"|\'([^\'\\]|\\.)*\'
Token Sign [[:IsPunctuation:][:IsSymbol:]]|\\\"|\\\'

Rule Program: #ProgramNode*
Rule ProgramNode: 	#Directive
					| #dSkip
					| #dToken
					| #dRule
					| error

Rule Tk: @AnyExcept("Skip", "Rule", "Token", "%")

Rule @Directive : "%" @Tk*
Rule dSkip: @"Skip" @Tk*
Rule dToken: "Token" @LetterDigit Tk*
Rule dRule: "Rule" Sign? @LetterDigit ":" Tk* 
