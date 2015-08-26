using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserGenerator
{
    class Program
    {
        static string FileName = "";

        private static bool isHelp(string str)
        {
            if (str.StartsWith("/") || str.StartsWith("-"))
                str = str.Substring(1);
            if (str == "?" || str == "h" || str == "help")
                return true;
            return false;
        }

        private static string FindArg(string[] args, string arg)
        {
            for (int i = 0; i < args.Length-1; ++i)
                if (args[i].ToLower() == arg.ToLower())
                    return args[i + 1];
            return "";
        }
        private static void ParseArgs(string[] args)
        {
            //входной файл
            FileName = args[0];
            string dir = Path.GetDirectoryName(FileName);

            //lex-файл
            string lex = FindArg(args, "/l");
            if (lex == "")
                Options.LEXFileName = dir + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".lex";
            else
                Options.LEXFileName = lex;

            //yacc-файл
            string yacc = FindArg(args, "/y");
            if (yacc == "")
                Options.YFileName = dir + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".y";
            else
                Options.YFileName = yacc;

            //cs-файл
            string cs = FindArg(args, "/cs");
            if (cs == "")
                Options.PPGFileName = dir + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".cs";
            else
                Options.PPGFileName = cs;
        }

        private static void ShowHelp()
        {
            Console.WriteLine(
@"Command line:
ParserGenerator <input file> [ output options ]
Output options :
/l <lex file name>
/y <yacc file name>
/cs <cs file name>
"
);
        }

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Не указан входной файл");
                return 0;
            }

            if (isHelp(args[0]))
            {
                ShowHelp();
                return 0;
            }
            
            ParseArgs(args);
            SourceFile sf;
            //Парсим входной файл
            try
            {
                Console.SetError(TextWriter.Null);
                string Source = File.ReadAllText(FileName);

                Scanner s = new Scanner();
                s.SetSource(Source, 0);
                Parser p = new Parser(s);
                if (!p.Parse())
                    throw new Exception();
                sf = p.root;
                Console.WriteLine("Source file " + FileName + " parsed");
                Console.WriteLine("Declarations found: " + sf.Declarations.Count.ToString());

            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error while parsing source file");
                Console.ResetColor();
                return 1;
            }

            //Преобразуем результат парсинга к готовому к генерации виду и генерируем выходные файлы
            SymbolTable symbolTable;
            try
            {
                Engine e = new Engine();
                symbolTable = e.ParseSourceFile(sf);

                if (ErrorReporter.ErrorCount != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Files were not generated due to errors in input file");
                    Console.ResetColor();
                    return 1;
                }

                //Генерируем выходные файлы
                Generator gen = new Generator(symbolTable);
                gen.GenerateSourceFiles();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Files were not generated due to internal error");
                Console.WriteLine(e.ToString());
                Console.ResetColor();
                return 1;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Files were successfully generated");
            Console.ResetColor();
            return 0;
        }
    }
}
