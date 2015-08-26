using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AspectCore;

namespace AspectCore
{
    public partial class fmAddPoint : Form
    {
        //список точек - от самой вложенной до корня
        public List<PointOfInterest> AnchorPoints;

        public TreeViewAdapter Adapter;

        /// <summary>
        /// Узел дерева
        /// Если IsAddNewNode == true, то родительский
        /// Иначе - текущий
        /// </summary>
        public TreeNode Node;

        public bool IsAddNewNode;

        Action Callback;

        public fmAddPoint(TreeViewAdapter Adapter)
        {
            InitializeComponent();
            this.Adapter = Adapter;
        }

        public void SyncronizeControlsWithPoint(List<PointOfInterest> points, TreeNode Node, bool IsAdd, Action CallBack)
        {
            AnchorPoints = points;
            this.Node = Node;
            IsAddNewNode = IsAdd;
            Callback = CallBack;
            cbAnchorPoints.Items.Clear();
            if (points == null || points.Count == 0 || points[0].Context.Count == 0)
            {
                cbAnchorPoints.Enabled = false;
                cbAnchorPoints.Items.Add("Не удалось найти точку для привязки");
                tbPointName.Text = "";
                AnchorPoints = new List<PointOfInterest>();
                return;
            }
            tbPointName.Text = points[0].Name;
            tbNote.Text = points[0].Note;
            if (AnchorPoints[0].Context == null)
                AnchorPoints[0].Context = new List<OuterContextNode>();

            //отобразить в обратном порядке
            foreach (PointOfInterest pt in AnchorPoints)
                cbAnchorPoints.Items.Insert(0, pt.Context[0].Type + ": " + string.Join(" ", pt.Context[0].Name.ToArray()));
            cbAnchorPoints.Items.Insert(0, "Без привязки");
            cbAnchorPoints.Items.Add("Узел + текст: " + AnchorPoints[0].Text);
            cbAnchorPoints.SelectedIndex = cbAnchorPoints.Items.Count - 1;
        }
        public void SyncronizePointWithControls()
        {
            int PointIndex = cbAnchorPoints.Items.Count - cbAnchorPoints.SelectedIndex - 2;
            if (cbAnchorPoints.SelectedIndex == 0)
            {
                AnchorPoints.Clear();
                AnchorPoints.Add(new PointOfInterest());
            }
            else
            {
                PointOfInterest ResultPoint = AnchorPoints[Math.Max(0,  PointIndex)];
                AnchorPoints.Clear();
                AnchorPoints.Add(ResultPoint);
            }
            AnchorPoints[0].Name = tbPointName.Text;
            AnchorPoints[0].Note = tbNote.Text;
            if (PointIndex  != -1)
                AnchorPoints[0].Text = "";
        }

        private void cbAddAnchorPoint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (tbPointName.Text == "")
                tbPointName.BackColor = Color.Pink;
            else
            {
                SyncronizePointWithControls();
                if (IsAddNewNode)
                    Adapter.InsertNode(Node, AnchorPoints[0]);
                else
                    Adapter.UpdatePointAnchor(Node, AnchorPoints[0]);
                Callback();
                this.Close();
            }
        }

        private void tbAnchorValue_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = Color.White;
        }

        private void fmAddPoint_Shown(object sender, EventArgs e)
        {
            tbPointName.Focus();
        }

        private void fmAddPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool modifiers = ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) != Keys.None);
            if ( (e.KeyChar != (char)13) && (e.KeyChar != (char)10) )
                return;
            if (!(ActiveControl is TextBox) && (modifiers))
                return;
            if ((ActiveControl is TextBox) && ((ActiveControl as TextBox).Multiline != modifiers))
                return;

            bOk.PerformClick();
            e.Handled = true;
        }
    }
}
