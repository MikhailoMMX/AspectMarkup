using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserGenerator
{
    public class Options
    {
        //Устанавливается при чтении файла, используется при генерации
        public static bool CaseInsensitive = false;
        public static List<string> ExtensionList = new List<string>();
        public static string Extension = "\"cs\"";
        public static string Namespace = "LWParser";
        public static string RegionBegin = "";
        public static string RegionEnd = "";
        public static string DirectiveDefine = "";
        public static string DirectiveUndef = "";
        public static string DirectiveIfdef = "";
        public static string DirectiveIfndef = "";
        public static string DirectiveElse = "";
        public static string DirectiveElif = "";
        public static string DirectiveEnd = "";
        public static string BaseClassName = "";

        public static string PPGFileName = @"..\Parsers\GeneratedParser\ParserPartGenerated.cs";
        public static string LEXFileName = @"..\Parsers\GeneratedParser\LWLexer.lex";
        public static string YFileName = @"..\Parsers\GeneratedParser\LWParser.y";
    }
}
