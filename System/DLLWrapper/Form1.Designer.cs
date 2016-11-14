namespace DLLWrapper
{
    partial class Form1
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
            this.bOpenSourceFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tbEditor = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bReparseText = new System.Windows.Forms.Button();
            this.tvParsedPoints = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOpenSourceFile
            // 
            this.bOpenSourceFile.Location = new System.Drawing.Point(12, 12);
            this.bOpenSourceFile.Name = "bOpenSourceFile";
            this.bOpenSourceFile.Size = new System.Drawing.Size(166, 32);
            this.bOpenSourceFile.TabIndex = 2;
            this.bOpenSourceFile.TabStop = false;
            this.bOpenSourceFile.Text = "Открыть файл...";
            this.bOpenSourceFile.UseVisualStyleBackColor = true;
            this.bOpenSourceFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "CS files|*.cs|PAS-files|*.pas|All files|*.*";
            // 
            // tbEditor
            // 
            this.tbEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEditor.Location = new System.Drawing.Point(0, 0);
            this.tbEditor.Multiline = true;
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEditor.Size = new System.Drawing.Size(568, 416);
            this.tbEditor.TabIndex = 1;
            this.tbEditor.WordWrap = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 469);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(881, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // bReparseText
            // 
            this.bReparseText.Enabled = false;
            this.bReparseText.Location = new System.Drawing.Point(184, 12);
            this.bReparseText.Name = "bReparseText";
            this.bReparseText.Size = new System.Drawing.Size(166, 32);
            this.bReparseText.TabIndex = 7;
            this.bReparseText.TabStop = false;
            this.bReparseText.Text = "Перепарсить";
            this.bReparseText.UseVisualStyleBackColor = true;
            this.bReparseText.Click += new System.EventHandler(this.bReparseText_Click);
            // 
            // tvParsedPoints
            // 
            this.tvParsedPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvParsedPoints.HideSelection = false;
            this.tvParsedPoints.Location = new System.Drawing.Point(0, 0);
            this.tvParsedPoints.Name = "tvParsedPoints";
            this.tvParsedPoints.ShowNodeToolTips = true;
            this.tvParsedPoints.Size = new System.Drawing.Size(285, 416);
            this.tvParsedPoints.TabIndex = 0;
            this.tvParsedPoints.TabStop = false;
            this.tvParsedPoints.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvParsedPoints_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvParsedPoints);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbEditor);
            this.splitContainer1.Size = new System.Drawing.Size(857, 416);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "Сохранить как разметку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(593, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 491);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bReparseText);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.bOpenSourceFile);
            this.Name = "Form1";
            this.Text = "Визуализатор дерева";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOpenSourceFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbEditor;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button bReparseText;
        private System.Windows.Forms.TreeView tvParsedPoints;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button2;
    }
}

