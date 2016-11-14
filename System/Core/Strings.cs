using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspectCore
{
    public class Strings
    {
        //public static string ToolWindowTitle = "Окно аспектов";
        public static string ToolWindowTitle = "Разметка кода";

        public static string CanNotCreateWindow = "Невозможно создать окно";
        public static string NoParsers = "Парсеры не загружены";
        public static string CanNotGetDTE = "Сбой инициализации: невозможно получить ссылку на объект DTE";
        public static string CannotFindPoint = "Точка привязки {0} не найдена";
        public static string CannotFindExactlyOnePoint = "Неоднозначность";
        public static string CannotFindSubAspect = "Подаспект не найден";
        public static string CannotParseFile = "Не удалось распарсить файл";
        public static string UnsupportedAspectFile = "Формат файла не поддерживается";

        public const string RegistryRoot = "Software\\AspectNavigator";
        public static string RegistryKeyLastOpenAspect = "LastOpenAspect";

        public static string CursorInLine = "CursorInline";
        public static string CursorNewLevel = "CursorNewLevel";

        public static string TreeNodeType = "System.Windows.Forms.TreeNode";
        public static string PragmaEnd = "#endregion";

        public static string DefaultAspectExtension = ".axml";
        public static string OFDChangeFileMask = "{0}-файл|*{0}|Все файлы|*.*";

        public const string AspectPropWorkingDirDescr = "Папка проекта";
        public const string AspectPropWorkingDirCat = "Aspect Options";
        public const string AspectPropWorkingDirName = "Working Directory";

        public const string SubAspectPropNameDescr = "Имя подаспекта";
        public const string SubAspectPropNameCat = "Subaspect Options";
        public const string SubAspectPropNameName = "Title";

        public const string SubAspectPropFileNameDescr = "Путь к файлу с подаспектом";
        public const string SubAspectPropFileNameCat = "Subaspect Options";
        public const string SubAspectPropFileNameName = "File name";

        public const string SubAspectPropValueDescr = "Описание точки привязки";
        public const string SubAspectPropValueCat = "Subaspect Options";
        public const string SubAspectPropValueName = "Anchor";

        public const string SubAspectTextDescr = "Строка, к которой осуществляется привязка";
        public const string SubAspectTextCat = "Subaspect Options";
        public const string SubAspectTextName = "Text";

        public const string SubAspectPropContextDepthDescr = "Размер контекста";
        public const string SubAspectPropContextDepthCat = "Subaspect Options";
        public const string SubAspectPropContextDepthName = "Context Depth";

        public static string MBRemoveNodeText = "Узел содержит подузлы, удалить все поддерево?";
        public static string MBRemoveNodeTitle = "Подтверждение";

        public static string DefaultTreeNodeName = "Новая папка";
    }
}
