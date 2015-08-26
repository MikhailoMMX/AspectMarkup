using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QUT.Gppg;
using System.IO;

namespace AspectCore
{
    public class IDEInterop
    {
        /// <summary>
        /// Открыт ли хоть один документ
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDocumentOpen()
        {
            return false;
        }

        /// <summary>
        /// Открыт ли указанный документ
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public virtual bool IsDocumentOpen(string FileName)
        {
            return false;
        }

        /// <summary>
        /// Возвращает всё содержимое текущего текстового документа
        /// </summary>
        /// <returns></returns>
        public virtual string GetCurrentTextDocument()
        {
            return "";
        }

        /// <summary>
        /// Возвращает полное имя текущего текстового документа
        /// </summary>
        /// <returns></returns>
        public virtual string GetCurrentDocumentFileName()
        {
            return "";
        }

        /// <summary>
        /// Если документ с указанным именем открыт в среде - возвращает его содержимое
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public virtual string GetDocument(string FileName)
        {
            return File.ReadAllText(FileName, Encoding.Default);
        }

        /// <summary>
        /// Возвращает строку, на которой находится курсор
        /// </summary>
        /// <returns></returns>
        public virtual string GetCurrentLine()
        {
            return "";
        }

        /// <summary>
        /// Возвращает строку с указанным номером (нумерация с нуля)
        /// </summary>
        /// <param name="lineIndex"></param>
        /// <returns></returns>
        public virtual string GetLine(int lineIndex)
        {
            return "";
        }

        /// <summary>
        /// Возвращает координаты выделенного текста (позицию курсора)
        /// </summary>
        /// <returns></returns>
        public virtual LexLocation GetCursorPosition()
        {
            return new LexLocation();
        }

        /// <summary>
        /// Устанавливает курсор в указанном документе в указанные координаты
        /// </summary>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <param name="col"></param>
        /// <param name="lineEnd"></param>
        /// <param name="columnEnd"></param>
        public virtual void NavigateToFileAndPosition(string file, int line, int col, int lineEnd = 0, int columnEnd = 0)
        {  }

        /// <summary>
        /// Отображает свойства выделенного подаспекта в панели Properties (необязательно)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="node"></param>
        public virtual void UpdateSubAspectProperties(PointOfInterest point, System.Windows.Forms.TreeNode node)
        { }
    }
}
