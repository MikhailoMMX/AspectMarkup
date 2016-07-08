Token Text [^[:IsWhiteSpace:]=]+|=
Rule Program: Text* #Chapter2*
Rule Chapter2: "==" @Text* "==" Text* #Chapter3*
Rule Chapter3: "===" @Text* "===" Text*