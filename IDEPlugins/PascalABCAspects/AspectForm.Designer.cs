namespace PascalABCAspects
{
    partial class AspectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AspectForm));
            this.PluginImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PluginImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PluginImage
            // 
            this.PluginImage.Enabled = false;
            this.PluginImage.Image = ((System.Drawing.Image)(resources.GetObject("PluginImage.Image")));
            this.PluginImage.Location = new System.Drawing.Point(12, 12);
            this.PluginImage.Name = "PluginImage";
            this.PluginImage.Size = new System.Drawing.Size(32, 32);
            this.PluginImage.TabIndex = 0;
            this.PluginImage.TabStop = false;
            this.PluginImage.Visible = false;
            // 
            // AspectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 495);
            this.Controls.Add(this.PluginImage);
            this.Name = "AspectForm";
            this.TabText = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AspectForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PluginImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox PluginImage;

    }
}

