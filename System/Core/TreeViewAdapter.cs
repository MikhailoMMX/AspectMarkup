using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AspectCore
{
    public class TreeViewAdapter
    {
        Dictionary<TreeNode, PointOfInterest> NodesToPoints = new Dictionary<TreeNode, PointOfInterest>();
        private TreeView treeView;
        private AspectManager manager;
        private UndoEngine undoEngine = new UndoEngine();
        public TreeViewAdapter(AspectManager manager, TreeView treeView)
        {
            this.manager = manager;
            this.treeView = treeView;
        }

        public void RebuildTree()
        {
            NodesToPoints.Clear();
            treeView.Nodes.Clear();
            if (manager.WorkingAspect == null)
            {
                manager.WorkingAspect = new PointOfInterest();
                return;
            }
            if(manager.WorkingAspect.Items == null || manager.WorkingAspect.Items.Count == 0)
                return;

            foreach (PointOfInterest point in manager.WorkingAspect.Items)
                treeView.Nodes.Add(ConvertPointToTree(point));
        }

        /// <summary>
        /// Возвращает точку, соответствующую данному узлу дерева
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        public PointOfInterest GetPointByNode(TreeNode tn)
        {
            if (NodesToPoints.ContainsKey(tn))
                return NodesToPoints[tn];
            else
                return null;
        }

        /// <summary>
        /// Очистка текущего аспекта
        /// Или создание нового, пустого, с незаданным путем к проекту.
        /// Не отменяется, удаляет информацию для отмены предыдущих действий.
        /// </summary>
        public void ClearAspect()
        {
            NodesToPoints.Clear();
            treeView.Nodes.Clear();
            if (manager.WorkingAspect == null)
                manager.WorkingAspect = new PointOfInterest();
            manager.WorkingAspect.Items.Clear();
            manager.WorkingDir = "";
            undoEngine.Clear();
            manager.isFileSaved = false;
        }

        public void ReadAspectFile(string path)
        {
            ClearAspect();
            if (File.Exists(path))
            {
                manager.WorkingAspect = manager.DeserializeAspect(path);
                manager.WorkingDir = Path.GetDirectoryName(path) + "\\";
                manager.isFileSaved = true;
                RebuildTree();
            }
        }

        /// <summary>
        /// Преобразование аспектного дерева в дерево для отображения в компоненте TreeView
        /// </summary>
        /// <param name="p">Аспектное дерево</param>
        /// <returns>Список узлов TreeNode для компонента TreeView</returns>
        private TreeNode ConvertPointToTree(PointOfInterest pt)
        {
            if (pt == null)
                return null;
            TreeNode result = new TreeNode(pt.Name);
            result.ToolTipText = pt.Note;

            //0 - Empty, 1 - Aspect, 2 - Folder, 3 - Note
            int imageindex = 0;
            if (pt.Context != null && pt.Context.Count > 0)
                imageindex = 1;
            else if (pt.Items != null && pt.Items.Count > 0)
                imageindex = 2;
            else if (!string.IsNullOrWhiteSpace(pt.Note))
                imageindex = 3;
            result.ImageIndex = imageindex;
            result.SelectedImageIndex = imageindex;

            NodesToPoints.Add(result, pt);
            if (pt.Items != null)
                foreach (PointOfInterest p in pt.Items)
                    result.Nodes.Add(ConvertPointToTree(p));
            return result;
        }

        /// <summary>
        /// Добавление нового подузла к дереву
        /// </summary>
        /// <param name="rootNode">Узел, к которому нужно добавить новый подузел, если null - то в корень дерева</param>
        /// <param name="pt">Точка, описывающая новый узел</param>
        /// <param name="pos">Позиция нового подузла (-1: в конец)</param>
        public TreeNode InsertNode(TreeNode rootNode, PointOfInterest pt, int pos = -1)
        {
            //!Aspect UndoWhenAddNode
            PointOfInterest parentPoint = rootNode == null ? manager.WorkingAspect : NodesToPoints[rootNode];
            undoEngine.SavePointState(pt, parentPoint, ActionKind.Add);

            if (pos < 0)
                if (rootNode == null)
                    pos = treeView.Nodes.Count;
                else
                    pos = rootNode.Nodes.Count;
            TreeNode newNode = ConvertPointToTree(pt);
            if (rootNode != null)
            {
                rootNode.Nodes.Insert(pos, newNode);
                NodesToPoints[rootNode].Items.Insert(pos, pt);
            }
            else
            {
                treeView.Nodes.Insert(pos, newNode);
                manager.WorkingAspect.Items.Insert(pos, pt);
            }
            treeView.SelectedNode = newNode;
            manager.isFileSaved = false;
            return newNode;
        }

        /// <summary>
        /// Перемещает узел
        /// </summary>
        /// <param name="Node">Узел, который нужно переместить</param>
        /// <param name="newParent">Новый родительский узел для перемещаемого узла</param>
        /// <param name="newPos">Позиция нового узла среди подузлов родительского узла</param>
        public void MoveNode(TreeNode Node, TreeNode newParent, int newPos)
        {
            //!Aspect UndoWhenMoveNode
            PointOfInterest point = NodesToPoints[Node];
            PointOfInterest oldParentPoint = Node.Parent == null ? manager.WorkingAspect : NodesToPoints[Node.Parent];
            PointOfInterest newParentPoint = newParent == null ? manager.WorkingAspect : NodesToPoints[newParent];
            undoEngine.SavePointState(point, oldParentPoint, ActionKind.Move, newParentPoint);

            //TreeNode newNode = (TreeNode)Node.Clone();
            //PointOfInterest pt = NodesToPoints[Node];
            //NodesToPoints.Remove(Node);
            //NodesToPoints.Add(newNode, pt);

            //!Aspect MoveNodeWithinCollectionBugFix
            if (Node.Parent == newParent && newPos >= Node.Index)
                newPos -= 1;

            if (Node.Parent == null)
                manager.WorkingAspect.Items.Remove(point);
            else
                NodesToPoints[Node.Parent].Items.Remove(point);
            Node.Remove();

            if (newParent == null)
            {
                treeView.Nodes.Insert(newPos, Node);
                manager.WorkingAspect.Items.Insert(newPos, point);
            }
            else
            {
                newParent.Nodes.Insert(newPos, Node);
                NodesToPoints[newParent].Items.Insert(newPos, point);
            }
            manager.isFileSaved = false;
        }

        /// <summary>
        /// Переименовывает точку, соответствующую заданному узлу
        /// </summary>
        /// <param name="Node">Узел дерева</param>
        /// <param name="newName">Новое имя узла</param>
        public void RenameNode(TreeNode Node, string newName)
        {
            //!Aspect UndoWhenRenameNode
            undoEngine.SavePointState(NodesToPoints[Node], null, ActionKind.Edit);

            NodesToPoints[Node].Name = newName;
            Node.Text = newName;
            manager.isFileSaved = false;
        }

        public void UpdatePointAnchor(TreeNode Node, PointOfInterest newAnchor)
        {
            PointOfInterest point = NodesToPoints[Node];

            //!Aspect UndoWhenUpdateAnchor
            undoEngine.SavePointState(point, null, ActionKind.Edit);

            point.Name = newAnchor.Name;
            point.Note = newAnchor.Note;
            point.Text = newAnchor.Text;
            point.FileName = newAnchor.FileName;
            point.Context = newAnchor.Context;
            point.InnerContext = newAnchor.InnerContext;
            Node.Text = point.Name;
            Node.ToolTipText = point.Note;
            Node.ImageIndex = point.Context.Count > 0 ? 1 : 0;
            Node.SelectedImageIndex = Node.ImageIndex;
            manager.isFileSaved = false;
        }

        /// <summary>
        /// Удаление узла и соответствующей точки. Подузлы остаются для возможной отмены удаления
        /// </summary>
        /// <param name="Node"></param>
        public void RemoveNode(TreeNode Node)
        {
            //!Aspect UndoWhenRemoveNode
            PointOfInterest parent;
            if (Node.Parent != null)
                parent = NodesToPoints[Node.Parent];
            else
                parent = manager.WorkingAspect;
            undoEngine.SavePointState(NodesToPoints[Node], parent, ActionKind.Remove);

            if (Node.Parent == null)
                manager.WorkingAspect.Items.RemoveAt(treeView.Nodes.IndexOf(Node));
            else
                NodesToPoints[Node.Parent].Items.RemoveAt(Node.Parent.Nodes.IndexOf(Node));
            Node.Remove();
            removeNodeFromDictionary(Node);
            manager.isFileSaved = false;
        }

        /// <summary>
        /// Рекурсивное удаление узла из словаря соответствия узлов и точек
        /// </summary>
        /// <param name="Node"></param>
        private void removeNodeFromDictionary(TreeNode Node)
        {
            NodesToPoints.Remove(Node);
            for (int i = Node.Nodes.Count - 1; i >= 0; --i)
                removeNodeFromDictionary(Node.Nodes[i]);
        }

        public void UpdateToolTip(TreeNode node, string text)
        {
            //!Aspect UndoWhenEditNote
            undoEngine.SavePointState(NodesToPoints[node], null, ActionKind.Edit);

            node.ToolTipText = text;
            NodesToPoints[node].Note = text;
            manager.isFileSaved = false;
        }

        public TreeNode FindNodeByValue(PointOfInterest point)
        {
            foreach (KeyValuePair<TreeNode, PointOfInterest> p in NodesToPoints)
                if (p.Value == point)
                    return p.Key;
            return null;
        }

        private void ApplyUndoUnit(UndoUnit action)
        {
            if (action.Kind == ActionKind.Add)
            {
                //Добавляли, нужно удалить
                TreeNode node = FindNodeByValue(action.OriginalPointRef);
                action.OriginalParent.Items.Remove(action.OriginalPointRef);
                removeNodeFromDictionary(node);
                node.Remove();
            }
            else if (action.Kind == ActionKind.Remove)
            {
                //Удаляли, нужно добавить
                TreeNode parentNode = FindNodeByValue(action.OriginalParent);
                action.OriginalParent.Items.Insert(action.OriginalPosition, action.OriginalPointRef);
                TreeNode newNode = ConvertPointToTree(action.OriginalPointRef);
                if (parentNode == null)
                    treeView.Nodes.Insert(action.OriginalPosition, newNode);
                else
                    parentNode.Nodes.Insert(action.OriginalPosition, newNode);
            }
            else if (action.Kind == ActionKind.Move)
            {
                //Перемещали
                TreeNode node = FindNodeByValue(action.OriginalPointRef);
                TreeNodeCollection oldCol = node.Parent == null ? treeView.Nodes : node.Parent.Nodes;
                TreeNode newParent = FindNodeByValue(action.OriginalParent);
                TreeNodeCollection newCol = newParent == null ? treeView.Nodes : newParent.Nodes;
                oldCol.Remove(node);
                newCol.Insert(action.OriginalPosition, node);
                action.OriginalPointContent.Items.Remove(action.OriginalPointRef);
                action.OriginalParent.Items.Insert(action.OriginalPosition, action.OriginalPointRef);
            }
            else if (action.Kind == ActionKind.Edit)
            {
                //Редактировали
                TreeNode node = FindNodeByValue(action.OriginalPointRef);
                action.OriginalPointRef.ApplyChanges(action.OriginalPointContent);
                node.Text = action.OriginalPointRef.Name;
                node.ToolTipText = action.OriginalPointRef.Note;
            }
            manager.isFileSaved = false;
        }

        public void Undo()
        {
            UndoUnit undo = undoEngine.Undo();
            if (undo == null)
                return;

            ApplyUndoUnit(undo);
        }

        public void Redo()
        {
            UndoUnit redo = undoEngine.Redo();
            if (redo == null)
                return;

            ApplyUndoUnit(redo);
        }

        #region !Aspect UndoButtonsEnablingAdapter
        public bool HasUndoActions()
        {
            return undoEngine.HasUndoActions();
        }

        public bool HasRedoActions()
        {
            return undoEngine.HasRedoActions();
        }
        #endregion
    }
}
