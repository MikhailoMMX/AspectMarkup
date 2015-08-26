using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestFilesGeneratorCS
{

    class Program
    {
        static int MODIFICATIONS = 5;

        static List<string> CopyList(List<string> list)
        {
            List<string> result = new List<string>();
            foreach (string s in list)
                result.Add((string)s.Clone());
            return result;
        }
        static List<string> LexAll(string Path)
        {
            List<string> result = new List<string>();
            StreamReader sr = File.OpenText(Path);
            string src = sr.ReadToEnd();
            sr.Close();
            Scanner scan = new Scanner();
            scan.SetSource(src, 0);
            while (scan.yylex() != (int)Tokens.EOF)
            {
                result.Add(scan.yytext);
            }
            return result;
        }
        static string ListToString(List<string> TokenList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in TokenList)
                sb.Append(str);
            return sb.ToString();
        }
        static void WriteFile(string path, string text)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(text);
            sw.Close();
        }

        static string[] NewTokens = { " class ", " namespace ", " id ", " 123 ", ";", "{", "}", "*", 
                                        "//comment\n", "\"string\"", };
        static Random rand = new Random();

        static void InsertRandomToken(List<string> list)
        {
            string str = NewTokens[rand.Next(NewTokens.Length)];
            list.Insert(rand.Next(list.Count), str);
        }
        static void RemoveRandomToken(List<string> list)
        {
            list.RemoveAt(rand.Next(list.Count));
        }

        static void ModifyList(List<string> list)
        {
            for (int i = 0; i < MODIFICATIONS; ++i)
            {
                switch (rand.Next(2))
                {
                    case 0: RemoveRandomToken(list); break;
                    case 1: InsertRandomToken(list); break;
                }
            }
        }

        static void GenerateFiles(string Path, int Count)
        {
            FileInfo fi = new FileInfo(Path);
            string newFolder = Path + "_";
            if (!Directory.Exists(newFolder))
                Directory.CreateDirectory(newFolder);
            List<string> TokenList = LexAll(Path);
            for (int i = 0; i < Count; ++i)
            {
                List<string> ModifiedTokenList = CopyList(TokenList);
                ModifyList(ModifiedTokenList);
                WriteFile(newFolder + @"\" + i.ToString() + ".cs", ListToString(ModifiedTokenList));
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Необходимы параметры: папка, количество генерируемых тестовых примеров [, количество модификаций]");
                return;
            }
            DirectoryInfo di = new DirectoryInfo(args[0]);
            if (!di.Exists)
            {
                Console.WriteLine("Указанная папка не существует");
                return;
            }
            if (args.Length > 2)
                MODIFICATIONS = int.Parse(args[2]);

            int count = int.Parse(args[1]);
            FileInfo[] files = di.GetFiles("*.cs");
            foreach (FileInfo fi in files)
            {
                GenerateFiles(fi.FullName, count);
            }
        }
    }
}
