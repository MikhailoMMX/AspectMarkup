%Extension "lex"
%Namespace LWLex

Skip Begin "/*" End "*/"
Skip Begin "//"

Rule Program: A B
Rule B: "b1" "b2"
Rule A: C
Rule C: B "c"
