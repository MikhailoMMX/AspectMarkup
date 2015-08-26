using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace AspectCore
{
    /// <summary>
    /// Класс содержит вспомогательные функции, необходимые для реализации Drag-and-Drop для компонента TreeView
    /// и изменения курсора в процессе перетаскивания
    /// </summary>
    public class CursorHelper
    {
        /// <summary>
        /// Вычисляет место для вставки узла при перетаскивании
        /// </summary>
        /// <param name="tv">Дерево</param>
        /// <param name="x">Экранные координаты курсора</param>
        /// <param name="y">Экранные координаты курсора</param>
        /// <param name="NewParent">Узел, являющийся новым родителем</param>
        /// <param name="NewPos">Позиция среди подузлов этого родителя</param>
        public static void GetPositionToInsert(TreeView tv, TreeViewAdapter tva, int x, int y, out TreeNode NewParent, out int NewPos)
        {
            Point pt = tv.PointToClient(new Point(x, y));
            TreeNode DestinationNode = tv.GetNodeAt(pt);
            if (DestinationNode == null)
            {
                NewParent = null;
                NewPos = tv.Nodes.Count;
                return;
            }
            int kind;
            PointOfInterest pti = tva.GetPointByNode(DestinationNode);
            bool NestingAllowed = pti != null;
            int border = DestinationNode.Bounds.Height / 4;

            if (pt.Y <= (DestinationNode.Bounds.Top + border))
                kind = -1;  //верхняя четверть - перед узлом
            else if (pt.Y >= (DestinationNode.Bounds.Bottom - border))
            {
                if (!DestinationNode.IsExpanded)
                    kind = 1;   //нижняя четверть неразвернутого узла - после узла
                else
                    kind = 0;   //нижняя четверть развернутого узла - внутрь
            }
            else
                kind = 0;       //Иначе - внутрь

            if (kind == 0 && ! NestingAllowed)
                if (pt.Y <= (DestinationNode.Bounds.Top + 2*border))
                    kind = -1;  //если внутрь нельзя и верхняя половина - перед узлом
                else
                    kind = 1;   //Внутрь нельзя и нижняя половина - после узла

            if (kind == -1)
            {
                //Перед узлом
                NewParent = DestinationNode.Parent;
                if (NewParent == null)
                    NewPos = tv.Nodes.IndexOf(DestinationNode);
                else
                    NewPos = DestinationNode.Parent.Nodes.IndexOf(DestinationNode);
            }
            else if (kind == 1)
            {
                //После узла
                NewParent = DestinationNode.Parent;
                if (NewParent == null)
                    NewPos = tv.Nodes.IndexOf(DestinationNode) + 1;
                else
                    NewPos = DestinationNode.Parent.Nodes.IndexOf(DestinationNode) + 1;
            }
            else
            {
                //Внутрь узла
                NewParent = DestinationNode;
                NewPos = DestinationNode.Nodes.Count;
            }

        }
        /// <summary>
        /// Проверяет, можно ли добавить узел Node подузлом к Root. Если Root вложен в Node, то нельзя
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="Root"></param>
        /// <returns></returns>
        public static bool NestingAllowed(TreeNode Node, TreeNode Root)
        {
            while (Root != null)
            {
                if (Root == Node)
                    return false;
                Root = Root.Parent;
            }
            return true;
        }

        /// <summary>
        /// Возвращает номер курсора, который нужно показать в данной точке
        /// </summary>
        /// <param name="tv">TreeView, надо которым перемещается узел</param>
        /// <param name="node">Перемещаемый узел</param>
        /// <param name="x">Координаты курсора</param>
        /// <param name="y">Координаты курсора</param>
        /// <returns>-1 - обычный курсор, перемещать в эту точку нельзя
        /// 0 - курсор, указывающий на добавление нового уровня
        /// 1 - курсор, указывающий на добавление узла на том же уровне</returns>
        public static int GetCursorKind(TreeView tv, TreeViewAdapter tva, TreeNode node, int x, int y)
        {
            TreeNode Dst;
            int pos;
            CursorHelper.GetPositionToInsert(tv, tva, x, y, out Dst, out pos);
            if (!CursorHelper.NestingAllowed(node, Dst)// нельзя перемещать узел в своего же потомка
                || (Dst == node.Parent && (node.Index == pos || node.Index == pos - 1))// не надо двигать, если положение и так совпадет
                )
                return -1;
            if (Dst != null && Dst == tv.GetNodeAt(tv.PointToClient(new Point(x, y))))
                return 0;
            else
                return 1;
        }

    }
}
