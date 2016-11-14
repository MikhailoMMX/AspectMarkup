%Extension "txt"

Token Number 	[0-9]+
Token NL		[\r\n]+
Token Other		[^0-9\r\n[:IsWhiteSpace:]]+

Rule Program: 	[#Text | #Header | NL | error]*
Rule Text: 		@TkStart @Any* NL
				| @Number [@NotDot @Any*]? NL
Rule Header: 	[@#Number @"."]+ @Text
Rule TkStart: 	@AnyExcept(NL, Number)
Rule NotDot:	@AnyExcept(".", NL)