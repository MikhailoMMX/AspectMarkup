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
            this.lbCandidates = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lOldPointInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lbCandidates
            // 
            this.lbCandidates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCandidates.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCandidates.FormattingEnabled = true;
            this.lbCandidates.ItemHeight = 16;
            this.lbCandidates.Location = new System.Drawing.Point(0, 34);
            this.lbCandidates.Name = "lbCandidates";
            this.lbCandidates.Size = new System.Drawing.Size(707, 84);
            this.lbCandidates.TabIndex = 0;
            this.lbCandidates.SelectedIndexChanged += new System.EventHandler(this.lbCandidates_SelectedIndexChanged);
            this.lbCandidates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbCandidates_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип, имя";
            // 
            // lOldPointInfo
            // 
            this.lOldPointInfo.AutoSize = true;
            this.lOldPointInfo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOldPointInfo.Location = new System.Drawing.Point(0, 18);
            this.lOldPointInfo.Name = "lOldPointInfo";
            this.lOldPointInfo.Size = new System.Drawing.Size(72, 16);
            this.lOldPointInfo.TabIndex = 2;
            this.lOldPointInfo.Text = "OldPoint";
            this.toolTip1.SetToolTip(this.lOldPointInfo, "Имя и тип сохраненной точки");
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Имя и тип сохраненной точки";
            // 
            // FmSelectPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 118);
            this.Controls.Add(this.lOldPointInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCandidates);
            this.Name = "FmSelectPoint";
            this.Text = "Фрагмент кода потерян, выберите точку для перепривязки...";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmSelectPoint_FormClosing);
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