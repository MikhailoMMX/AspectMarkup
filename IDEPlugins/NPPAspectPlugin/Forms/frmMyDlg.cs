using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using AspectCore;
using System.Windows.Forms;

namespace NPPAspectPlugin
{
    public partial class frmMyDlg : Form
    {
        private AspectWindowPane awp;
        public frmMyDlg()
        {
            InitializeComponent();
            awp = new AspectWindowPane(new NPPInterop());
            Text = awp.WindowTitle;
            awp.Parent = this;
            awp.Dock = DockStyle.Fill;
            awp.Visible = true;
        }
    }
}
