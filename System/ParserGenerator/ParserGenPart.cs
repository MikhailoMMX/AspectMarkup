using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QUT.Gppg;
using AspectCore;

namespace ParserGenerator
{
    public partial class Parser : ShiftReduceParser<ValueType, LexLocation>
    {
        public SourceFile root;
        public Parser(Scanner scanner) : base(scanner) { }
    }

    public sealed partial class Scanner : ScanBase
    {
        public override void yyerror(string format, params object[] args)
        {
            ErrorReporter.WriteError(string.Format(format, args), yylloc);
        }

        public static class Keywords
        {
            private static Dictionary<string, int> keywords = new Dictionary<string, int>();

            static Keywords()
            {
                keywords.Add(StringConstants.tkSkip, (int)Tokens.tkSkip);
                keywords.Add(StringConstants.tkRule, (int)Tokens.tkRule);
                keywords.Add(StringConstants.tkRegExToken, (int)Tokens.tkRegExToken);
                keywords.Add(StringConstants.tkBegin, (int)Tokens.tkBegin);
                keywords.Add(StringConstants.tkEnd, (int)Tokens.tkEnd);
                keywords.Add(StringConstants.tkBeginEnd, (int)Tokens.tkBeginEnd);
                keywords.Add(StringConstants.tkEscapeSymbol, (int)Tokens.tkEscapeSymbol);
                keywords.Add(StringConstants.tkNested, (int)Tokens.tkNested);
                keywords.Add(StringConstants.tkCaseSensitive, (int)Tokens.tkCaseSensitive);
                keywords.Add(StringConstants.tkCaseInsensitive, (int)Tokens.tkCaseInsensitive);
                keywords.Add(StringConstants.tkExtension, (int)Tokens.tkExtension);
                keywords.Add(StringConstants.tkNamespace, (int)Tokens.tkNamespace);
                keywords.Add(StringConstants.tkRegion, (int)Tokens.tkRegion);
                keywords.Add(StringConstants.tkDefine, (int)Tokens.tkDefine);
                keywords.Add(StringConstants.tkUndef, (int)Tokens.tkUndef);
                keywords.Add(StringConstants.tkIfDef, (int)Tokens.tkIfdef);
                keywords.Add(StringConstants.tkIfNDef, (int)Tokens.tkIfndef);
                keywords.Add(StringConstants.tkElse, (int)Tokens.tkElse);
                keywords.Add(StringConstants.tkElIf, (int)Tokens.tkElif);
                keywords.Add(StringConstants.tkPreprocessor, (int)Tokens.tkPreprocessor);
                keywords.Add(StringConstants.tkAny, (int)Tokens.tkAny);
                keywords.Add(StringConstants.tkAnyExcept, (int)Tokens.tkAnyExcept);
                keywords.Add(StringConstants.tkList0, (int)Tokens.tkList);
                keywords.Add(StringConstants.tkList1, (int)Tokens.tkList);
                keywords.Add(":", (int)Tokens.tkColon);
                keywords.Add(",", (int)Tokens.tkComma);
                keywords.Add("|", (int)Tokens.tkPipe);
                keywords.Add("@", (int)Tokens.tkAt);
                keywords.Add("#", (int)Tokens.tkSharp);
                keywords.Add("?", (int)Tokens.tkQuest);
                keywords.Add("+", (int)Tokens.tkPlus);
                keywords.Add("*", (int)Tokens.tkStar);
                keywords.Add("%", (int)Tokens.tkPercent);
                keywords.Add("=", (int)Tokens.tkEq);
                keywords.Add("[", (int)Tokens.tkSquareOpen);
                keywords.Add("]", (int)Tokens.tkSquareClose);
                keywords.Add("(", (int)Tokens.tkRoundOpen);
                keywords.Add(")", (int)Tokens.tkRoundClose);
            }

            public static int KeywordOrIDToken(string s)
            {
                if (keywords.ContainsKey(s))
                    return keywords[s];
                else
                    return (int)Tokens.Token;
            }
        }
    }
}
