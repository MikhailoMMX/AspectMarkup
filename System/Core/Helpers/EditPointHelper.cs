using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AspectCore;
using QUT.Gppg;

namespace AspectCore.Helpers
{
    public class EditPointHelper
    {
        private static string GetCurrentWord(string text, int pos, int len)
        {
            if (pos < 0 || pos > text.Length || text == "")
                return "";
            if (len != 0)
                return text.Substring(pos-1, len);
            int L = pos - 1;
            int R = pos;
            while (L >= 0 && (char.IsLetterOrDigit(text[L]) || text[L] == '_'))
                --L;
            L += 1;
            while (R < text.Length && (char.IsLetterOrDigit(text[R]) || text[R] == '_'))
                ++R;
            return text.Substring(L, R - L);
        }
        /// <summary>
        /// Поиск точки в текущем документе в позиции курсора
        /// Документ должен быть открыт.
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="Wrapper"></param>
        /// <param name="Manager"></param>
        /// <returns></returns>
        public static List<PointOfInterest> FindPointUnderCursor(IDEInterop ide, TreeManager treeManager)
        {
            PointOfInterest root = ParseCurrentTextDocument(ide, treeManager);
            LexLocation loc = ide.GetCursorPosition();
            string Text = ide.GetLine(loc.StartLine);
            List<PointOfInterest> Points = TreeSearchEngine.FindPointByLocation(root, loc.StartLine, loc.StartColumn);
            if (Points == null || Points.Count == 0)
                return null;

            List<PointOfInterest> Result = new List<PointOfInterest>();
            foreach (PointOfInterest pt in Points)
            {
                PointOfInterest p2 = pt.ClonePointAssignItems();
                p2.ApplyInnerContext();
                p2.Items = new List<PointOfInterest>();
                Result.Add(p2);
            }

            int len = loc.StartLine == loc.EndLine ? loc.EndColumn - loc.StartColumn : Text.Length - loc.StartColumn;
            Result[0].Name = GetCurrentWord(Text, loc.StartColumn, len);
            return Result;
        }

        /// <summary>
        /// Возвращает корень дерева разобранного открытого в редакторе файла, с еще не сохраненными изменениями
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="Wrapper"></param>
        /// <returns></returns>
        public static PointOfInterest ParseCurrentTextDocument(IDEInterop ide, TreeManager treeManager)
        {
            return treeManager.GetTree(ide.GetCurrentDocumentFileName(), ide.GetCurrentTextDocument());
        }

        /// <summary>
        /// Показать диалог добавления привязки к точке
        /// </summary>
        /// <param name="point"></param>
        /// <param name="td"></param>
        /// <returns></returns>
        private static void ShowPointDialog(TreeViewAdapter Adapter, List<PointOfInterest> point, TreeNode Node, bool IsNewNode, Action Callback)
        {            
            fmAddPoint fm = new fmAddPoint(Adapter);
            fm.SyncronizeControlsWithPoint(point, Node, IsNewNode, Callback);
            fm.Show();
        }

        public static void AddPoint(IDEInterop ide, TreeManager treeManager, AspectManager Manager, TreeViewAdapter Adapter, TreeNode NewParent, Action Callback)
        {
            if (ide.GetCurrentDocumentFileName() == "")
                return;

            string Text = ide.GetCurrentLine().Trim();
            if (Text == "")
                return;

            List<PointOfInterest> points = FindPointUnderCursor(ide, treeManager);
            if (points != null)
                foreach (PointOfInterest pt in points)
                {
                    pt.FileName = Manager.GetRelativePath(pt.FileName);
                    pt.Text = Text;
                }
            ShowPointDialog(Adapter, points, NewParent, true, Callback);
        }

        public static void EditPointAnchor(IDEInterop ide, TreeManager treeManager, AspectManager Manager, TreeViewAdapter Adapter, TreeNode Node, Action Callback)
        {
            if (ide.GetCurrentDocumentFileName() == "")
                return;

            string Text = ide.GetCurrentLine().Trim();
            if (Text == "")
                return;

            List<PointOfInterest> points = FindPointUnderCursor(ide, treeManager);
            PointOfInterest OriginalPoint = Adapter.GetPointByNode(Node);
            if (points != null)
                foreach (PointOfInterest pt in points)
                {
                    pt.FileName = Manager.GetRelativePath(pt.FileName);
                    pt.Name = OriginalPoint.Name;
                    pt.Note = OriginalPoint.Note;
                    pt.Text = Text;
                }

            ShowPointDialog(Adapter, points, Node, false, Callback);
        }
    }
}
