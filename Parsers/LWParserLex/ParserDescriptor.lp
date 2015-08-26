%Extension "lex"
%Namespace LWLex

Skip Begin "/*" End "*/"
Skip Begin "//"

//0 - тоже будем считать идентификатором
Token ID           		[a-zA-Z0-9_]+
Token NL				[\r\n]+
Token Sign [[:IsPunctuation:][:IsSymbol:]]

//Сущности, нужные для подбора регулярки
//Token RepMrk       \{{Number}(,{Number}?)?\}
//Token ClsRef       \{{ID}\}
//Token PredefChrCls	\[:{ID}:\]
//Token ChrCls       \[([^\[\]\r\n]|{PredefChrCls})+\]
//Token EscChr       \\[^\r\n]
//Token LitStr		\"([^\"]|\\\")*\"
//Token SetOp			"{-}"|"{+}"|"{*}"
//То же самое, но в одну строчку
Token RegexItem	\{[0-9]+(,[0-9]*)?\}|\{{ID}\}|\[:{ID}:\]|\[([^\[\]\r\n]|\[:{ID}:\])+\]|\\[^\r\n]|\"([^\"]|\\\")*\"|"{-}"|"{+}"|"{*}"


//объявим все ключевые символы здесь и будем использовать их по именам, чтобы был полный список в одном месте, а не разбросанный по коду
Rule tkLT: @"<"
Rule tkGT: @">"
Rule tkPC: @"%"
Rule tkPC2: @"%%"
Rule tkBOpen: @"{"
Rule tkBClose: @"}"
Rule tkPCX: @"%x"
Rule tkPCS: @"%s"

//Lex-файл
Rule @Program: #Section1 tkPC2 #Section2 tkPC2 Any*

//Вспомогательные сущности
Rule @DirectCode: tkPC tkBOpen Any* tkPC tkBClose								//%{ ... %}											
Rule @ContextCode: tkBOpen [Any | ContextCode]* tkBClose						//{ ... }
//регулярки во второй секции не могут начинаться с "%%" и "<", иначе будут конфликты
//Поэтому сделаем регулярки с разным составом для разных секций
//регулярки во второй секции начинаются с элемента регулярки, символа, % и >
//в "хвост" могут входить вышеперечисленное и еще %% < %x %s. Фигурные скобки не могут - во избежание конфликтов с конструкциями типа {id} и кодом после регулярки
Rule RegexHead: @RegexItem|@Sign  |  @tkPC|@tkGT
Rule RegexTail: @RegexHead|@tkPC2|@tkLT|@tkPCX|@tkPCS|@ID
Rule Regex1 : @RegexTail*
Rule Regex2 : @RegexHead @RegexTail*
Rule IdList: #@ID [Sign? #@ID]*

//Секция определений
//Второе правило пришлось ввести, чтобы при обработке ошибок не терялся накопленный список. Исправлю.
Rule @Section1: #Section1Item*
Rule Section1Item: #StateDecl | #RegexDecl | DirectCode | PercentDecl | NL | error NL
Rule RegexDecl: @ID #Regex1 NL
Rule StateDecl: [@tkPCX | @tkPCS] #IdList NL		//разделитель - любой знак или ничего. Минус ключевой символ, плюс возможность не падать на ошибках
Rule PercentDecl: tkPC @AnyExcept(tkBOpen)*	NL			//все кроме {, иначе конфликт с DirectCode, он тоже начинается с %{. Конец строки тоже сюда не входит

//Секция правил
Rule @Section2: #Section2Item*
Rule Section2Item: #RuleOrGroup | DirectCode | NL | error NL
Rule States: tkLT [@IdList | @Sign] tkGT NL*
Rule RuleOrGroup: @States? NL* [#Group | #LexRule]
Rule LexRule: [@#Regex2 NL*]+ [ContextCode | AnyExcept(tkBOpen)* NL] //Иначе не подбирается строка кода без { }
Rule @Group: tkBOpen [#Group | #LexRule| NL | error NL]* tkBClose
