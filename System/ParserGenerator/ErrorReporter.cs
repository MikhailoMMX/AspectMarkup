using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QUT.Gppg;

namespace ParserGenerator
{
    public class ErrorReporter
    {
        public static int ErrorCount = 0;
        public static void WriteError(string Message, LexLocation Location, string Param = "")
        {
            string res;
            if (Param == "")
                res = Message;
            else
                res = string.Format(Message, Param);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("("+ Location.StartLine+","+Location.StartColumn+") " + res);
            Console.ResetColor();
            ErrorCount += 1;
        }
    }
}
