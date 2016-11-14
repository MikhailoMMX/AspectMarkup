namespace AspectCore.UI
{
    partial class FmSelectPoint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmSelectPoint));
            this.lbCandidates = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lOldPointInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lbCandidates
            // 
            resources.ApplyResources(this.lbCandidates, "lbCandidates");
            this.lbCandidates.FormattingEnabled = true;
            this.lbCandidates.Name = "lbCandidates";
            this.lbCandidates.SelectedIndexChanged += new System.EventHandler(this.lbCandidates_SelectedIndexChanged);
            this.lbCandidates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbCandidates_MouseDoubleClick);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lOldPointInfo
            // 
            resources.ApplyResources(this.lOldPointInfo, "lOldPointInfo");
            this.lOldPointInfo.Name = "lOldPointInfo";
            this.toolTip1.SetToolTip(this.lOldPointInfo, resources.GetString("lOldPointInfo.ToolTip"));
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Имя и тип сохраненной точки";
            // 
            // FmSelectPoint
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lOldPointInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCandidates);
            this.KeyPreview = true;
            this.Name = "FmSelectPoint";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmSelectPoint_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FmSelectPoint_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FmSelectPoint_PreviewKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbCandidates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lOldPointInfo;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}