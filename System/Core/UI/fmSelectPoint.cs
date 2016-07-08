using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AspectCore.UI
{
    public partial class FmSelectPoint : Form
    {
        private IDEInterop _ide;
        private TreeSearchResult _search;
        private TreeManager _treeManager;
        private PointOfInterest _point;
        private string Pattern = "{0,-4} {1,-15} {2,-30}";
        private string _type = "Тип узла";
        private string _typeEn = "Type";
        private string _name = "Имя узла";
        private string _nameEn = "Name";
        private string _dist = "%";

        public FmSelectPoint(IDEInterop ide, TreeManager treeManager)
        {
            _ide = ide;
            _treeManager = treeManager;
            InitializeComponent();
        }

        private void BuildList()
        {
            lbCandidates.SelectedIndexChanged -= lbCandidates_SelectedIndexChanged;
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en" || true) //hardcoded
                label1.Text = string.Format(Pattern, _dist, _typeEn, _nameEn);
            else
                label1.Text = string.Format(Pattern, _dist, _type, _name);
            lOldPointInfo.Text = string.Format(Pattern, 1.ToString("F2"), _point.Context[0].Type, string.Join(" ", _point.Context[0].Name));

            lbCandidates.Items.Clear();
            foreach (TreeSearchResultNode node in _search._result)
                lbCandidates.Items.Add(string.Format(Pattern, ((float)node.TotalMatch/TreeSearchOptions.Equility).ToString("F2"), node.TreeNode.Context[0].Type, string.Join(" ",node.TreeNode.Context[0].Name)));
            lbCandidates.SelectedIndexChanged += lbCandidates_SelectedIndexChanged;
        }

        public void Launch(TreeSearchResult Search, PointOfInterest point)
        {
            _search = Search;
            _point = point;
            BuildList();
            Show();
        }

        private void fmSelectPoint_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void lbCandidates_SelectedIndexChanged(object sender, EventArgs e)
        {
            PointOfInterest currentPoint = _search[lbCandidates.SelectedIndex];

            currentPoint.Text = _point.Text;
            int line = currentPoint.Location.StartLine;
            int col = currentPoint.Location.StartColumn;

            if (_point.Text != "")
            {
                QUT.Gppg.LexLocation Loc = TreeSearchEngine.FindTextPosition(currentPoint, _treeManager.GetText(currentPoint.FileName));
                line = Loc.StartLine;
                col = Loc.StartColumn;
            }

            _ide.NavigateToFileAndPosition(currentPoint.FileName, line, 0);
        }

        private void lbCandidates_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbCandidates.SelectedIndex < 0)
                return;
            PointOfInterest currentPoint = _search[lbCandidates.SelectedIndex];
            PointOfInterest TreeRoot = _treeManager.GetTree(currentPoint.FileName, true);
            TreeSearchEngine.SetNearLG(TreeRoot, currentPoint, _treeManager.GetText(currentPoint.FileName), out _point.NearL, out _point.NearG);
            _point.Context = currentPoint.Context;
            _point.InnerContext = currentPoint.InnerContext;
            _point.Text = _ide.GetCurrentLine().Trim();
            Hide();
        }
    }
}
