namespace AspectCore
{
    partial class AspectWindowPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AspectWindowPane));
            this.tvAspects = new System.Windows.Forms.TreeView();
            this.cmtvAspects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.перейтиККодуToolStripMenuItemCM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.добавитьГруппуToolStripMenuItemCM = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьУзелToolStripMenuItemCM = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьУзелToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.КомментарийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьПривязкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.удалитьТекстToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilTreeViewIcons = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.отменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повторитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.перейтиКПодаспектуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.импортироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddFolder = new System.Windows.Forms.ToolStripButton();
            this.tsbAddPoint = new System.Windows.Forms.ToolStripButton();
            this.tsbRemovePoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFindNode = new System.Windows.Forms.ToolStripButton();
            this.bDebugButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.slMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OFDChangeFile = new System.Windows.Forms.OpenFileDialog();
            this.cmtvAspects.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvAspects
            // 
            resources.ApplyResources(this.tvAspects, "tvAspects");
            this.tvAspects.AllowDrop = true;
            this.tvAspects.ContextMenuStrip = this.cmtvAspects;
            this.tvAspects.FullRowSelect = true;
            this.tvAspects.HideSelection = false;
            this.tvAspects.ImageList = this.ilTreeViewIcons;
            this.tvAspects.LabelEdit = true;
            this.tvAspects.Name = "tvAspects";
            this.tvAspects.ShowNodeToolTips = true;
            this.tvAspects.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvAspects_BeforeLabelEdit);
            this.tvAspects.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvAspects_AfterLabelEdit);
            this.tvAspects.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvAspects_BeforeCollapse);
            this.tvAspects.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvAspects_BeforeExpand);
            this.tvAspects.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvAspects_ItemDrag);
            this.tvAspects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvAspects_AfterSelect);
            this.tvAspects.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAspects_NodeMouseClick);
            this.tvAspects.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvAspects_NodeMouseDoubleClick);
            this.tvAspects.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvAspects_DragDrop);
            this.tvAspects.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvAspects_DragEnter);
            this.tvAspects.DragOver += new System.Windows.Forms.DragEventHandler(this.tvAspects_DragOver);
            this.tvAspects.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.tvAspects_GiveFeedback);
            this.tvAspects.Enter += new System.EventHandler(this.tvAspects_Enter);
            this.tvAspects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvAspects_KeyDown);
            this.tvAspects.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvAspects_MouseDown);
            // 
            // cmtvAspects
            // 
            resources.ApplyResources(this.cmtvAspects, "cmtvAspects");
            this.cmtvAspects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.перейтиККодуToolStripMenuItemCM,
            this.toolStripMenuItem1,
            this.добавитьГруппуToolStripMenuItemCM,
            this.добавитьУзелToolStripMenuItemCM,
            this.удалитьУзелToolStripMenuItem,
            this.toolStripMenuItem2,
            this.КомментарийToolStripMenuItem,
            this.изменитьПривязкуToolStripMenuItem,
            this.изменитьФайлToolStripMenuItem,
            this.toolStripMenuItem5,
            this.удалитьТекстToolStripMenuItem});
            this.cmtvAspects.Name = "cmtvAspects";
            // 
            // перейтиККодуToolStripMenuItemCM
            // 
            resources.ApplyResources(this.перейтиККодуToolStripMenuItemCM, "перейтиККодуToolStripMenuItemCM");
            this.перейтиККодуToolStripMenuItemCM.Name = "перейтиККодуToolStripMenuItemCM";
            this.перейтиККодуToolStripMenuItemCM.Click += new System.EventHandler(this.перейтиККодуToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // добавитьГруппуToolStripMenuItemCM
            // 
            resources.ApplyResources(this.добавитьГруппуToolStripMenuItemCM, "добавитьГруппуToolStripMenuItemCM");
            this.добавитьГруппуToolStripMenuItemCM.Name = "добавитьГруппуToolStripMenuItemCM";
            this.добавитьГруппуToolStripMenuItemCM.Click += new System.EventHandler(this.tsbAddFolder_Click);
            // 
            // добавитьУзелToolStripMenuItemCM
            // 
            resources.ApplyResources(this.добавитьУзелToolStripMenuItemCM, "добавитьУзелToolStripMenuItemCM");
            this.добавитьУзелToolStripMenuItemCM.Name = "добавитьУзелToolStripMenuItemCM";
            this.добавитьУзелToolStripMenuItemCM.Click += new System.EventHandler(this.tsbAddPoint_Click);
            // 
            // удалитьУзелToolStripMenuItem
            // 
            resources.ApplyResources(this.удалитьУзелToolStripMenuItem, "удалитьУзелToolStripMenuItem");
            this.удалитьУзелToolStripMenuItem.Name = "удалитьУзелToolStripMenuItem";
            this.удалитьУзелToolStripMenuItem.Click += new System.EventHandler(this.tsbRemovePoint_Click);
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // КомментарийToolStripMenuItem
            // 
            resources.ApplyResources(this.КомментарийToolStripMenuItem, "КомментарийToolStripMenuItem");
            this.КомментарийToolStripMenuItem.Name = "КомментарийToolStripMenuItem";
            this.КомментарийToolStripMenuItem.Click += new System.EventHandler(this.КомментарийToolStripMenuItem_Click);
            // 
            // изменитьПривязкуToolStripMenuItem
            // 
            resources.ApplyResources(this.изменитьПривязкуToolStripMenuItem, "изменитьПривязкуToolStripMenuItem");
            this.изменитьПривязкуToolStripMenuItem.Name = "изменитьПривязкуToolStripMenuItem";
            this.изменитьПривязкуToolStripMenuItem.Click += new System.EventHandler(this.изменитьПривязкуToolStripMenuItem_Click);
            // 
            // изменитьФайлToolStripMenuItem
            // 
            resources.ApplyResources(this.изменитьФайлToolStripMenuItem, "изменитьФайлToolStripMenuItem");
            this.изменитьФайлToolStripMenuItem.Name = "изменитьФайлToolStripMenuItem";
            this.изменитьФайлToolStripMenuItem.Click += new System.EventHandler(this.изменитьФайлToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            // 
            // удалитьТекстToolStripMenuItem
            // 
            resources.ApplyResources(this.удалитьТекстToolStripMenuItem, "удалитьТекстToolStripMenuItem");
            this.удалитьТекстToolStripMenuItem.Name = "удалитьТекстToolStripMenuItem";
            this.удалитьТекстToolStripMenuItem.Click += new System.EventHandler(this.удалитьТекстToolStripMenuItem_Click);
            // 
            // ilTreeViewIcons
            // 
            this.ilTreeViewIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeViewIcons.ImageStream")));
            this.ilTreeViewIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTreeViewIcons.Images.SetKeyName(0, "EmptyNode.png");
            this.ilTreeViewIcons.Images.SetKeyName(1, "document-arrow-right-icon.png");
            this.ilTreeViewIcons.Images.SetKeyName(2, "Folder-icon.png");
            this.ilTreeViewIcons.Images.SetKeyName(3, "Note.png");
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.tsbAddFolder,
            this.tsbAddPoint,
            this.tsbRemovePoint,
            this.toolStripSeparator4,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator2,
            this.tsbFindNode,
            this.bDebugButton});
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripDropDownButton1
            // 
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.toolStripMenuItem3,
            this.отменитьToolStripMenuItem,
            this.повторитьToolStripMenuItem,
            this.toolStripMenuItem4,
            this.перейтиКПодаспектуToolStripMenuItem,
            this.toolStripMenuItem6,
            this.импортироватьToolStripMenuItem,
            this.экспортироватьToolStripMenuItem});
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // новыйToolStripMenuItem
            // 
            resources.ApplyResources(this.новыйToolStripMenuItem, "новыйToolStripMenuItem");
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            resources.ApplyResources(this.открытьToolStripMenuItem, "открытьToolStripMenuItem");
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            resources.ApplyResources(this.сохранитьToolStripMenuItem, "сохранитьToolStripMenuItem");
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            resources.ApplyResources(this.сохранитьКакToolStripMenuItem, "сохранитьКакToolStripMenuItem");
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // отменитьToolStripMenuItem
            // 
            resources.ApplyResources(this.отменитьToolStripMenuItem, "отменитьToolStripMenuItem");
            this.отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            this.отменитьToolStripMenuItem.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // повторитьToolStripMenuItem
            // 
            resources.ApplyResources(this.повторитьToolStripMenuItem, "повторитьToolStripMenuItem");
            this.повторитьToolStripMenuItem.Name = "повторитьToolStripMenuItem";
            this.повторитьToolStripMenuItem.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // перейтиКПодаспектуToolStripMenuItem
            // 
            resources.ApplyResources(this.перейтиКПодаспектуToolStripMenuItem, "перейтиКПодаспектуToolStripMenuItem");
            this.перейтиКПодаспектуToolStripMenuItem.Name = "перейтиКПодаспектуToolStripMenuItem";
            this.перейтиКПодаспектуToolStripMenuItem.Click += new System.EventHandler(this.tsbFindNode_Click);
            // 
            // toolStripMenuItem6
            // 
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            // 
            // импортироватьToolStripMenuItem
            // 
            resources.ApplyResources(this.импортироватьToolStripMenuItem, "импортироватьToolStripMenuItem");
            this.импортироватьToolStripMenuItem.Name = "импортироватьToolStripMenuItem";
            this.импортироватьToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // экспортироватьToolStripMenuItem
            // 
            resources.ApplyResources(this.экспортироватьToolStripMenuItem, "экспортироватьToolStripMenuItem");
            this.экспортироватьToolStripMenuItem.Name = "экспортироватьToolStripMenuItem";
            this.экспортироватьToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // tsbAddFolder
            // 
            resources.ApplyResources(this.tsbAddFolder, "tsbAddFolder");
            this.tsbAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddFolder.Name = "tsbAddFolder";
            this.tsbAddFolder.Click += new System.EventHandler(this.tsbAddFolder_Click);
            // 
            // tsbAddPoint
            // 
            resources.ApplyResources(this.tsbAddPoint, "tsbAddPoint");
            this.tsbAddPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddPoint.Name = "tsbAddPoint";
            this.tsbAddPoint.Click += new System.EventHandler(this.tsbAddPoint_Click);
            // 
            // tsbRemovePoint
            // 
            resources.ApplyResources(this.tsbRemovePoint, "tsbRemovePoint");
            this.tsbRemovePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemovePoint.Name = "tsbRemovePoint";
            this.tsbRemovePoint.Click += new System.EventHandler(this.tsbRemovePoint_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // tsbOpen
            // 
            resources.ApplyResources(this.tsbOpen, "tsbOpen");
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            resources.ApplyResources(this.tsbSave, "tsbSave");
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // tsbUndo
            // 
            resources.ApplyResources(this.tsbUndo, "tsbUndo");
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            resources.ApplyResources(this.tsbRedo, "tsbRedo");
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // tsbFindNode
            // 
            resources.ApplyResources(this.tsbFindNode, "tsbFindNode");
            this.tsbFindNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindNode.Name = "tsbFindNode";
            this.tsbFindNode.Click += new System.EventHandler(this.tsbFindNode_Click);
            // 
            // bDebugButton
            // 
            resources.ApplyResources(this.bDebugButton, "bDebugButton");
            this.bDebugButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bDebugButton.Name = "bDebugButton";
            this.bDebugButton.Click += new System.EventHandler(this.bDebugButton_Click);
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slMain});
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            // 
            // slMain
            // 
            resources.ApplyResources(this.slMain, "slMain");
            this.slMain.Name = "slMain";
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // OFDChangeFile
            // 
            resources.ApplyResources(this.OFDChangeFile, "OFDChangeFile");
            // 
            // AspectWindowPane
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvAspects);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "AspectWindowPane";
            this.cmtvAspects.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvAspects;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel slMain;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bDebugButton;
        private System.Windows.Forms.ContextMenuStrip cmtvAspects;
        private System.Windows.Forms.ToolStripMenuItem перейтиККодуToolStripMenuItemCM;
        private System.Windows.Forms.ToolStripButton tsbAddPoint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRemovePoint;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem добавитьУзелToolStripMenuItemCM;
        private System.Windows.Forms.ToolStripMenuItem удалитьУзелToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem КомментарийToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem изменитьПривязкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem отменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повторитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripButton tsbFindNode;
        private System.Windows.Forms.ToolStripMenuItem перейтиКПодаспектуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbAddFolder;
        private System.Windows.Forms.ImageList ilTreeViewIcons;
        private System.Windows.Forms.ToolStripMenuItem добавитьГруппуToolStripMenuItemCM;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem удалитьТекстToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьФайлToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OFDChangeFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem импортироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортироватьToolStripMenuItem;
    }
}
