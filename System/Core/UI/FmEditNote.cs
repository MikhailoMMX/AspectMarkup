using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AspectCore
{
    public partial class FmEditNote : Form
    {
        public string Note
        {
            get { return tbNote.Text; }
            set { tbNote.Text = value; }
        }
        public FmEditNote()
        {
            InitializeComponent();
        }

        private void FmEditNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void tbNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)13 || e.KeyChar == (char)10)
                && ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) != Keys.None))
            {
                bOk.PerformClick();
                e.KeyChar = (char)0;
            }
        }

    }
}
