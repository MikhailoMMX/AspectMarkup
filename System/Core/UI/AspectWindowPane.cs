using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using AspectCore;
using AspectCore.Helpers;
using AspectCore.UI;

namespace AspectCore
{
    public partial class AspectWindowPane : UserControl, IAspectWindow
    {
        AspectCore.IDEInterop ide;
        AspectCore.TreeManager treeManager = new TreeManager();
        AspectCore.AspectManager Manager = new AspectCore.AspectManager();
        AspectCore.TreeViewAdapter Adapter;
        int OldCursor = -2;
        FmEditNote fmEditNote = new FmEditNote();
        FmSelectPoint fmSelectPoint;
        string CurrentAspectFileName = "";
        bool blnDoubleClick = false;

        public AspectWindowPane(IDEInterop ide)
        {
            try
            {
                this.ide = ide;
                KeyboardShortcutHelper.control = this;
                InitializeComponent();
                Adapter = new AspectCore.TreeViewAdapter(Manager, tvAspects);
                fmSelectPoint = new FmSelectPoint(ide, treeManager);

                SynchronizeUndoButtons();

                if (treeManager.GetParsersCount() == 0)
                    GlobalData.traceAction(Strings.NoParsers);

                GlobalData.traceAction("Загрузка плагина завершена");
                GlobalData.traceAction("Версия .net: " + System.Environment.Version);
                GlobalData.traceAction(System.Environment.Is64BitProcess ? "64-разрядный процесс" : "32-разрядный процесс");

                //костыль
                tvAspects.Scrollable = true;
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        #region Public Methods

        public string WindowTitle {
            get
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                return string.Format("{0} ({1}.{2}.{3})", Strings.ToolWindowTitle, fvi.FileMajorPart, fvi.FileMinorPart, fvi.FilePrivatePart); 
            }
        }

        /// <summary>
        /// Возвращает корень текущего дерева аспектов (может быть null)
        /// </summary>
        public PointOfInterest AspectTreeRoot
        {
            get
            {
                if (Manager != null)
                    return Manager.WorkingAspect;
                else
                    return null;
            }
        }

        public PointOfInterest ParseFile(string FileName)
        {
            return treeManager.GetTree(FileName);
        }

        public PointOfInterest ParseText(string Text, string FileName)
        {
            return treeManager.GetTree(FileName, Text);
        }

        public void RenameAspectFile(string FileName)
        {
            if (File.Exists(CurrentAspectFileName))
                File.Move(CurrentAspectFileName, FileName);
            CurrentAspectFileName = FileName;
            SetStatus("");
        }

        public void OpenOrCreateAspectFile(string FileName)
        {
            if (File.Exists(FileName))
                OpenAspectFile(FileName);
            else
                CreateNewAspectFile(FileName);
            CurrentAspectFileName = FileName;
        }

        public void SaveAspectFile()
        {
            if (CurrentAspectFileName != "")
                Manager.SerializeAspect(CurrentAspectFileName);
        }

        #endregion

        #region IAspectWindow
        public TreeNode AddNodeToTree(PointOfInterest Point, TreeNode Parent)
        {
            return Adapter.InsertNode(Parent, Point);
        }
        public TreeNode AddFolderToTree(string Name, TreeNode Parent)
        {
            PointOfInterest point = new PointOfInterest();
            point.Name = Name;
            return Adapter.InsertNode(Parent, point);
        }
        public PointOfInterest FindPointByLocation(string FileName, int Line, int Column)
        {
            PointOfInterest point;
            try
            {
                point = treeManager.GetTree(FileName);
            }
            catch (Exception e)
            {
                throw new Exception(Strings.CannotParseFile + " " + FileName);
            }
            List<PointOfInterest> RList = TreeSearchEngine.FindPointByLocation(point, Line, Column);
            PointOfInterest Res = null;
            if (RList != null && RList.Count != 0)
            {
                Res = RList[0].ClonePointWithoutItems();
                Res.Text = ide.GetLine(Line);
            }
            return Res;
        }
        public PointOfInterest FindPointByLocation(string Text, string FileName, int Line, int Column)
        {
            PointOfInterest point;
            try
            {
                point = treeManager.GetTree(FileName, Text);
            }
            catch (Exception e)
            {
                throw new Exception(Strings.CannotParseFile + " " + FileName);
            }
            List<PointOfInterest> RList = TreeSearchEngine.FindPointByLocation(point, Line, Column);
            PointOfInterest Res = null;
            if (RList != null && RList.Count != 0)
            {
                Res = RList[0].ClonePointWithoutItems();
                Res.Text = ide.GetLine(Line);
            }
            return Res;
        }
        public bool LoadParserFromFile(string FileName)
        {
            return treeManager.Parsers.LoadParserFromFile(FileName, true);
        }
        public TreeNode GetSelectedNode()
        {
            return tvAspects.SelectedNode;
        }
        public string GetCurrentAspectFileName()
        {
            return CurrentAspectFileName;
        }
        public bool IsFileSaved()
        {
            return Manager.isFileSaved;
        }
        #endregion

        #region Private methods

        private void SetStatus(string text)
        {
            if (text == "")
            {
                slMain.Text = Path.GetFileName(CurrentAspectFileName);
                slMain.ToolTipText = CurrentAspectFileName;
            }
            else
                slMain.Text = text;
        }

        private static Cursor GetCursor(string cursorName)
        {
            var buffer = AspectCore.Properties.Resources.ResourceManager.GetObject(cursorName) as byte[];
            return new Cursor(new MemoryStream(buffer));
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            KeyboardShortcutHelper.ProcessKeyPreview(ref m, ModifierKeys);
            return base.ProcessKeyPreview(ref m);
        }
        private bool tvAspectLabelEditing = false;

        internal void TrappedKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
                tsbSave.PerformClick();
            else if (e.Control && e.KeyCode == Keys.O)
                tsbOpen.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Z)
                tsbUndo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.R)
                tsbRedo.PerformClick();
            else if (e.KeyCode == Keys.Delete && !tvAspectLabelEditing)
                tsbRemovePoint.PerformClick();
            else if (e.KeyCode == Keys.F2)
                if (tvAspects.SelectedNode != null)
                    tvAspects.SelectedNode.BeginEdit();
        }

        private void SynchronizeUndoButtons()
        {
            bool undo = Adapter.HasUndoActions();
            tsbUndo.Enabled = undo;
            отменитьToolStripMenuItem.Enabled = undo;
            bool redo = Adapter.HasRedoActions();
            tsbRedo.Enabled = redo;
            повторитьToolStripMenuItem.Enabled = redo;
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    OpenAspectFile(openFileDialog.FileName);
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void CreateNewAspectFile(string fileName)
        {
            CurrentAspectFileName = fileName;
            SynchronizeUndoButtons();
            tsbSave.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
            Adapter.ClearAspect();
            Manager.WorkingDir = Path.GetDirectoryName(fileName);
            SetStatus("");
        }

        private void OpenAspectFile(string fileName)
        {
            Adapter.ReadAspectFile(fileName, treeManager.Parsers);
            Manager.WorkingDir = Path.GetDirectoryName(fileName);
            CurrentAspectFileName = fileName;
            SynchronizeUndoButtons();
            tsbSave.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
            SetStatus("");
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (CurrentAspectFileName != "")
                Manager.SerializeAspect(CurrentAspectFileName, true);
            else
                сохранитьКакToolStripMenuItem_Click(sender, e);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Manager.SerializeAspect(saveFileDialog.FileName, true);
                    //RegistryHelper.SaveToRegistry(Strings.RegistryKeyLastOpenAspect, saveFileDialog.FileName);
                    CurrentAspectFileName = saveFileDialog.FileName;
                    tsbSave.Enabled = true;
                    сохранитьToolStripMenuItem.Enabled = true;
                    SetStatus("");
                }
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private PointOfInterest InitName(PointOfInterest point)
        {
            if (point != null && point.Context != null && point.Context.Count != 0)
                point.Name = string.Join(" ", point.Context[0]);
            return point;
        }
        private void bDebugButton_Click(object sender, EventArgs e)
        {
            string fn = ide.GetCurrentDocumentFileName();
            int line = ide.GetCursorPosition().StartLine;
            int col = ide.GetCursorPosition().StartColumn;
            PointOfInterest pt = FindPointByLocation(ide.GetCurrentTextDocument(), fn, line, col);
            pt.Name = string.Join(" ", pt.Context[0]); ;//"Autogenerated node";
            AddNodeToTree(pt, null);
            //string str = AspectOptionsReader.ReadParsersDirectory();
            //DirectoryInfo di = new DirectoryInfo(str);
            //string str2 = di.Exists ? "Папка существует, файлов: " + di.GetFiles().Length : "Папка не существует";
            //MessageBox.Show(str + Environment.NewLine + str2);
            //Document d = dte.ActiveDocument;
            //MessageBox.Show(dte.ActiveDocument.FullName
            //    + EditorHelper.GetCurrentTextDocument(dte).Selection.TopPoint.Line.ToString() + ' ' + EditorHelper.GetCurrentTextDocument(dte).Selection.TopPoint.LineCharOffset);
            //EditorHelper.InsertLine(dte, "//!aspect 1", EditorHelper.GetCurrentTextDocument(dte).Selection.TopPoint.Line, EditorHelper.GetCurrentTextDocument(dte).Selection.TopPoint.LineCharOffset);
            //fmAddPoint fm = new fmAddPoint();
            //fm.Show();
            //AspectPropertiesHelper.UpdateAspectProperties(options, aspectWindow.GetITrackSelectionService());
        }

        private void перейтиККодуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvAspects.SelectedNode == null)
                    return;
                PointOfInterest pt = Adapter.GetPointByNode(tvAspects.SelectedNode);
                if ((pt?.Context?.Count ?? 0) == 0)
                    return;
                string fileName = Manager.GetFullFilePath(pt.FileName);
                PointOfInterest Tree = ide.IsDocumentOpen(fileName) ? treeManager.GetTree(fileName, ide.GetDocument(fileName)) : treeManager.GetTree(fileName);
                TreeSearchResult Search = TreeSearchEngine.FindPointInTree(Tree, pt, treeManager.GetText(fileName));
                if (Search.Singular)
                {
                    string path = Manager.GetFullFilePath(pt.FileName);
                    ide.NavigateToFileAndPosition(path, Search[0].Location.StartLine, Search[0].Location.StartColumn);
                    SetStatus("");
                }
                else if (Search.Count == 0)
                    SetStatus(string.Format(Strings.CannotFindPoint, pt.Name));
                else
                {
                    fmSelectPoint.Launch(Search, pt);
                    SetStatus("");
                }
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_ItemDrag(object sender, ItemDragEventArgs e)
        {
            tvAspects.DoDragDrop(e.Item, DragDropEffects.Move);
            OldCursor = -2;
            Cursor.Current = DefaultCursor;
        }

        private void tvAspects_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(Strings.TreeNodeType,false) || e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
        }

        private void tvAspects_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(Strings.TreeNodeType, false))
                {
                    TreeNode newParent;
                    int pos;
                    CursorHelper.GetPositionToInsert(tvAspects, Adapter, e.X, e.Y, out newParent, out pos);
                    TreeNode NewNode = (TreeNode)e.Data.GetData(Strings.TreeNodeType);
                    Adapter.MoveNode(NewNode, newParent, pos);
                    SynchronizeUndoButtons();
                }
                else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string fileName = ((string[])(e.Data.GetData(DataFormats.FileDrop)))[0];
                    try
                    {
                        PointOfInterest subAspect = Manager.DeserializeAspect(fileName, treeManager.Parsers);
                        if (subAspect != null)
                        {
                            Point pt = tvAspects.PointToClient(new Point(e.X, e.Y));
                            TreeNode newParent = tvAspects.GetNodeAt(pt);
                            Adapter.InsertNode(newParent, subAspect);
                        }
                    }
                    catch (Exception exc)
                    {
                        SetStatus(Strings.UnsupportedAspectFile);
                    }
                    
                    SynchronizeUndoButtons();
                }
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }

        private void tvAspects_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(Strings.TreeNodeType, false))
                {
                    TreeNode Src = (TreeNode)e.Data.GetData(Strings.TreeNodeType);
                    tvAspects.SelectedNode = Src;

                    int kind = CursorHelper.GetCursorKind(tvAspects, Adapter, Src, e.X, e.Y);

                    if (kind == -1)
                        e.Effect = DragDropEffects.None;
                    else
                        e.Effect = DragDropEffects.Move;

                    if (OldCursor != kind)
                    {
                        OldCursor = kind;
                        if (kind == -1)
                            Cursor.Current = Cursors.No;
                        else if (kind == 0)
                            Cursor.Current = GetCursor(Strings.CursorNewLevel);
                        else //if (kind == 1)
                            Cursor.Current = GetCursor(Strings.CursorInLine);
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                tvAspectLabelEditing = false;
                if (e.CancelEdit || e.Label == null || e.Label == "")
                    return;
                Adapter.RenameNode(e.Node, e.Label);

                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tsbAddPoint_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode NewParent = tvAspects.SelectedNode;
                if (tvAspects.SelectedNode != null && Adapter.GetPointByNode(tvAspects.SelectedNode).Context.Count != 0)
                    NewParent = NewParent.Parent;
                EditPointHelper.AddPoint(ide, treeManager, Manager, Adapter, NewParent, SynchronizeUndoButtons);
                //FIX callback
                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void изменитьПривязкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvAspects.SelectedNode == null)
                    return;
                EditPointHelper.EditPointAnchor(ide, treeManager, Manager, Adapter, tvAspects.SelectedNode, SynchronizeUndoButtons);
                //FIX callback
                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tsbRemovePoint_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (tvAspects.SelectedNode != null)
                    if (tvAspects.SelectedNode.Nodes.Count == 0
                        || MessageBox.Show(Strings.MBRemoveNodeText, Strings.MBRemoveNodeTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Adapter.RemoveNode(tvAspects.SelectedNode);
                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                PointOfInterest pt = Adapter.GetPointByNode(e.Node);
                UpdateButtonsAndMenus(pt);

                ide.UpdateSubAspectProperties(Adapter.GetPointByNode(e.Node), e.Node);
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_Enter(object sender, EventArgs e)
        {
            try
            {
                if (tvAspects.SelectedNode != null)
                    ide.UpdateSubAspectProperties(Adapter.GetPointByNode(tvAspects.SelectedNode), tvAspects.SelectedNode);
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void UpdateButtonsAndMenus(PointOfInterest SelectedPoint)
        {
            bool EditAnchor = SelectedPoint != null;
            bool EditComment = SelectedPoint != null;
            bool Nav = SelectedPoint != null && (SelectedPoint.Context!= null && SelectedPoint.Context.Count > 0);
            bool Remove = SelectedPoint != null;

            изменитьПривязкуToolStripMenuItem.Enabled = EditAnchor;

            КомментарийToolStripMenuItem.Enabled = EditComment;

            перейтиККодуToolStripMenuItemCM.Enabled = Nav;

            удалитьУзелToolStripMenuItem.Enabled = Remove;
        }

        private void tvAspects_MouseDown(object sender, MouseEventArgs e)
        {
            //Prevent Expand/collapse on double click
            if (e.Clicks > 1)
                blnDoubleClick = true;
            else
                blnDoubleClick = false;
            
            if (tvAspects.GetNodeAt(e.X, e.Y) != tvAspects.SelectedNode)
            {
                tvAspects.SelectedNode = null;
                UpdateButtonsAndMenus(null);
            }
            else if (tvAspects.GetNodeAt(e.X, e.Y) == null)
            {
                tvAspects.SelectedNode = null;
                UpdateButtonsAndMenus(null);
            }
        }

        private void КомментарийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvAspects.SelectedNode == null)
                    return;
                PointOfInterest pt = Adapter.GetPointByNode(tvAspects.SelectedNode);
                if (pt == null)
                    return;
                fmEditNote.Note = pt.Note;
                if (fmEditNote.ShowDialog() == DialogResult.OK)
                    Adapter.UpdateToolTip(tvAspects.SelectedNode, fmEditNote.Note);
                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            перейтиККодуToolStripMenuItemCM.PerformClick();
        }

        private void tvAspects_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                tvAspects.SelectedNode = e.Node;
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            try
            {
                Adapter.Undo();
                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tsbRedo_Click(object sender, EventArgs e)
        {
            try
            {
                Adapter.Redo();
                SynchronizeUndoButtons();
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void tvAspects_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            tvAspectLabelEditing = true;
        }

        private void tsbFindNode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ide.IsDocumentOpen())
                    return;
                PointOfInterest pt =null;//= EditPointHelper.FindPointUnderCursor(ide, treeManager);
                if (pt != null)
                {
                    pt.Text = ide.GetCurrentLine();
                    pt = TreeSearchEngine.FindPointInAspectTree(Manager.WorkingAspect, pt);
                    if (pt != null)
                    {
                        TreeNode tn = Adapter.FindNodeByValue(pt);
                        tvAspects.SelectedNode = tn;
                        tvAspects.Focus();
                        SetStatus("");
                        return;
                    }
                }
                SetStatus(Strings.CannotFindSubAspect);
            }
            catch (Exception exc)
            {
                ExceptionInfoHelper.ShowInfo(exc);
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Adapter.ClearAspect();
            CurrentAspectFileName = "";
            сохранитьToolStripMenuItem.Enabled = false;
            SetStatus("");
        }

        private void tsbAddFolder_Click(object sender, EventArgs e)
        {
            PointOfInterest pt = new PointOfInterest();
            pt.Name = Strings.DefaultTreeNodeName;
            TreeNode parent = tvAspects.SelectedNode;
            if (parent != null && Adapter.GetPointByNode(tvAspects.SelectedNode).Context.Count != 0)
                parent = parent.Parent;
            Adapter.InsertNode(parent, pt);
            tvAspects.SelectedNode.BeginEdit();
            SynchronizeUndoButtons();
        }

        //prevent Expand/Collapse on double click
        private void tvAspects_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (blnDoubleClick == true && e.Action == TreeViewAction.Expand)
                e.Cancel = true;
        }
        private void tvAspects_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (blnDoubleClick == true && e.Action == TreeViewAction.Collapse)
                e.Cancel = true;
        }
        private void tvAspects_KeyDown(object sender, KeyEventArgs e)
        {
            blnDoubleClick = false;
        }

        private void удалитьТекстToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (tvAspects.SelectedNode != null)
            //{
            //    PointOfInterest point = Adapter.GetPointByNode(tvAspects.SelectedNode);
            //    List<PointOfInterest> pts = TreeSearchEngine.FindPointsFromSavedPoint(point, ide, treeManager, Manager);
            //    if (pts == null || pts.Count == 0)
            //        SetStatus(Strings.CannotFindPoint);
            //    else if (pts.Count > 1)
            //        SetStatus(Strings.CannotFindExactlyOnePoint);
            //    else
            //    {
            //        //тут удаление на основании pts[0]
            //    }
            //}
        }

        private void изменитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvAspects.SelectedNode != null)
            {
                PointOfInterest point = Adapter.GetPointByNode(tvAspects.SelectedNode);
                OFDChangeFile.InitialDirectory = Path.GetDirectoryName(Manager.GetFullFilePath(point.FileName));
                OFDChangeFile.Filter = string.Format(Strings.OFDChangeFileMask, Path.GetExtension(point.FileName));
                if (OFDChangeFile.ShowDialog() == DialogResult.OK)
                    point.FileName = Manager.GetRelativePath(OFDChangeFile.FileName);
            }
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvAspects.SelectedNode == null)
                return;
            PointOfInterest subAspect = Adapter.GetPointByNode(tvAspects.SelectedNode);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                Manager.SerializeAspect(subAspect, saveFileDialog.FileName, true);
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PointOfInterest subAspect = Manager.DeserializeAspect(openFileDialog.FileName, treeManager.Parsers);
                    if (subAspect == null)
                        return;
                    Adapter.InsertNode(tvAspects.SelectedNode, subAspect);
                }
                catch (Exception exc)
                {
                }
            }
        }

        #endregion
    }
}
