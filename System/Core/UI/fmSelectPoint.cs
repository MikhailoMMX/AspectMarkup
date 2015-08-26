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
        private AspectManager _aspectManager;
        private PointOfInterest _point;
        private string Pattern = "{0,-15} {1,-30}";
        private string _type = "Тип узла";
        private string _name = "Имя узла";

        public FmSelectPoint(IDEInterop ide, TreeManager treeManager, AspectManager aspectManager)
        {
            _ide = ide;
            _treeManager = treeManager;
            _aspectManager = aspectManager;
            InitializeComponent();
        }

        private void BuildList()
        {
            lbCandidates.SelectedIndexChanged -= lbCandidates_SelectedIndexChanged;
            label1.Text = string.Format(Pattern, _type, _name);
            lbCandidates.Items.Clear();
            foreach (TreeSearchResultNode node in _search._result)
                lbCandidates.Items.Add(string.Format(Pattern, node.TreeNode.Context[0].Type, string.Join(" ",node.TreeNode.Context[0].Name)));
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
            _point.Context = currentPoint.Context;
            Hide();
        }
    }
}
