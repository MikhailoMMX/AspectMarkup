%Extension "*"
%CaseSensitive

Token Tk [[:IsLetterOrDigit:]_]*|[[:IsPunctuation:][:IsSymbol:]]
Token NewLine \r|\n|\r\n

Rule TextLine: @Tk+
Rule CommentStart: "*(" @TextLine
Rule CommentEnd: "*)" @TextLine

Rule Program :			#ProgramNode*
Rule ProgramNode:	 	#CommentStart
						| #CommentEnd
						| TextLine
						| NewLine
						| #error
