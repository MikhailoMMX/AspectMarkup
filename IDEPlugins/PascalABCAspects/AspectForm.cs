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
using VisualPascalABCPlugins;
using WeifenLuo.WinFormsUI.Docking;
using VisualPascalABC;

namespace PascalABCAspects
{
    public partial class AspectForm : DockContent
    {
        private IWorkbench workbench;
        private AspectWindowPane awp;
        public AspectForm(IWorkbench workbench)
        {
            this.workbench = workbench;
            InitializeComponent();
            
            awp = new AspectWindowPane(new PABCInterop(workbench));
            awp.Parent = this;
            awp.Dock = DockStyle.Fill;
            awp.Visible = true;
            (workbench as Form1).FormClosed += AspectForm_FormClosed; //костыль, на собственное событие не реагирует

            this.Text = awp.WindowTitle;
            this.TabText = awp.WindowTitle;
        }

        private void AspectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        public void LoadFile(string FileName)
        {
            FileName = System.IO.Path.GetDirectoryName(FileName) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(FileName) + AspectCore.Strings.DefaultAspectExtension;
            awp.SaveAspectFile();
            awp.OpenOrCreateAspectFile(FileName);
        }
        public void SaveCurrentFile()
        {
            awp.SaveAspectFile();
        }

        private void AspectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            awp.SaveAspectFile();
        }
    }
}
