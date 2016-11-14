using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AspectCore
{
    public interface IAspectWindow
    {
        /// <summary>
        /// Добавляет узел в аспектное дерево
        /// </summary>
        /// <param name="Point">Точка привязки, описывающая новый узел</param>
        /// <param name="Parent">Родительский узел дерева, к которому добавится новый</param>
        /// <returns>Созданный и добавленный узел дерева</returns>
        TreeNode AddNodeToTree(PointOfInterest Point, TreeNode Parent);
        /// <summary>
        /// Добавляет узел без привязки к фрагменту кода в дерево аспектов
        /// </summary>
        /// <param name="Name">Имя узла</param>
        /// <param name="Parent">Родительский узел дерева, к которому добавится новый</param>
        /// <returns>Созданный и добавленный узел дерева</returns>
        TreeNode AddFolderToTree(string Name, TreeNode Parent);
        /// <summary>
        /// Возвращает выделенный узел в дереве аспектов
        /// </summary>
        /// <returns></returns>
        TreeNode GetSelectedNode();
        /// <summary>
        /// Поиск узла в дереве разбора в заданном файле, расположенного в заданных координатах
        /// </summary>
        /// <param name="FileName">Имя файла с исходным кодом</param>
        /// <param name="Line">Строка (нумерация с 1)</param>
        /// <param name="Column">Столбец (нумерация с 0)</param>
        /// <returns>Точка привязки, описывающая сущность с данными координатами</returns>
        PointOfInterest FindPointByLocation(string FileName, int Line, int Column);
        /// <summary>
        /// Поиск узла в дереве разбора в заданном тексте, расположенного в заданных координатах
        /// </summary>
        /// <param name="Text">Исходный текст</param>
        /// <param name="Extension">Имя файла (или расширение - для правильного выбора парсера)</param>
        /// <param name="Line">Строка</param>
        /// <param name="Column">Столбец</param>
        /// <returns>Точка привязки, описывающая сущность с данными координатами</returns>
        PointOfInterest FindPointByLocation(string Text, string FileName, int Line, int Column);

        /// <summary>
        /// Загружает парсер из указанной сборки. При наличии уже загруженного парсера - заменяет ранее загруженный.
        /// </summary>
        /// <param name="FileName">Полный путь к сборке с парсером</param>
        /// <returns>True, если парсер был загружен, иначе False</returns>
        //bool LoadParserFromFile(string FileName);

        /// <summary>
        /// Возвращает имя текущего аспектного файла (.axml)
        /// </summary>
        /// <returns></returns>
        string GetCurrentAspectFileName();

        /// <summary>
        /// Возвращает true, если аспектное дерево не содержит несохраненных изменений
        /// </summary>
        /// <returns></returns>
        bool IsFileSaved();
    }
}
