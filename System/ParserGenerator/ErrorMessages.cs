using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserGenerator
{
    public class ErrorMessages
    {
        public static string RedundantBrackets = "Избыточные скобки";
        public static string IncorrectOCGroupUsage = "Неправильная структура скобок";
        public static string OCGroupNotDefined = "Группа скобок \"{0}\" не определена";
        public static string NameAlreadyDefined = "Повторное определение имени \"{0}\"";
        public static string NameNotDeclared = "Имя \"{0}\" не определено";
        public static string ANYCollision = "Определение ANY через еще один ANY недопустимо";
        public static string WrongListParameters = "Неправильное количество параметров в List";
        public static string MultipleListsNotAllowed = "Несколько вхождений List в одном правиле ({0}) недопустимо";
        public static string RepetitionAfterListNotAllowed = "Символ повторения после List недопустим (Правило {0})";
        public static string WrongFirstParamInList = "В правиле {0} первым параметром в List должно быть имя правила";
        public static string WrongSecondParamInList = "В правиле {0} вторым параметром в List должна быть строка или имя лексемы";
        //public static string TokenAlreadyDefined = "Token \"{0}\" is already defined";
        //public static string TokenNotDeclared = "Token\"{0}\" is not declared";
        //public static string RuleAlreadyDefined = "{0} Rule\"{1}\" is already defined";
        //public static string IncompleteDeclaration = "{0} Not enough arguments";
        //public static string OpenOrCloseExpected = "{0} 'Open' or 'Close' expected after BracketGroup name";
        //public static string SkipMustHaveName = "{0} Skip directive must have name";
        //public static string IncorrectRule = "{0} Incorrect rule";
        //public static string IncorrectOCGroupUsage = "{0} Incorrect OCGroup usage";
    }
}
