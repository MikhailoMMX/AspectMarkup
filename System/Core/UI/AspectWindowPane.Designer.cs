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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.slMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OFDChangeFile = new System.Windows.Forms.OpenFileDialog();
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
            this.tsbAddFolder = new System.Windows.Forms.ToolStripButton();
            this.tsbAddPoint = new System.Windows.Forms.ToolStripButton();
            this.tsbRemovePoint = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.tsbFindNode = new System.Windows.Forms.ToolStripButton();
            this.bDebugButton = new System.Windows.Forms.ToolStripButton();
            this.cmtvAspects.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvAspects
            // 
            this.tvAspects.AllowDrop = true;
            this.tvAspects.ContextMenuStrip = this.cmtvAspects;
            this.tvAspects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAspects.FullRowSelect = true;
            this.tvAspects.HideSelection = false;
            this.tvAspects.ImageIndex = 0;
            this.tvAspects.ImageList = this.ilTreeViewIcons;
            this.tvAspects.Indent = 16;
            this.tvAspects.LabelEdit = true;
            this.tvAspects.Location = new System.Drawing.Point(0, 25);
            this.tvAspects.Name = "tvAspects";
            this.tvAspects.SelectedImageIndex = 0;
            this.tvAspects.ShowNodeToolTips = true;
            this.tvAspects.Size = new System.Drawing.Size(307, 346);
            this.tvAspects.TabIndex = 0;
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
            this.cmtvAspects.Size = new System.Drawing.Size(191, 198);
            // 
            // перейтиККодуToolStripMenuItemCM
            // 
            this.перейтиККодуToolStripMenuItemCM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.перейтиККодуToolStripMenuItemCM.Name = "перейтиККодуToolStripMenuItemCM";
            this.перейтиККодуToolStripMenuItemCM.Size = new System.Drawing.Size(190, 22);
            this.перейтиККодуToolStripMenuItemCM.Text = "Перейти к коду";
            this.перейтиККодуToolStripMenuItemCM.Click += new System.EventHandler(this.перейтиККодуToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 6);
            // 
            // добавитьГруппуToolStripMenuItemCM
            // 
            this.добавитьГруппуToolStripMenuItemCM.Name = "добавитьГруппуToolStripMenuItemCM";
            this.добавитьГруппуToolStripMenuItemCM.Size = new System.Drawing.Size(190, 22);
            this.добавитьГруппуToolStripMenuItemCM.Text = "Добавить группу";
            this.добавитьГруппуToolStripMenuItemCM.Click += new System.EventHandler(this.tsbAddFolder_Click);
            // 
            // добавитьУзелToolStripMenuItemCM
            // 
            this.добавитьУзелToolStripMenuItemCM.Name = "добавитьУзелToolStripMenuItemCM";
            this.добавитьУзелToolStripMenuItemCM.Size = new System.Drawing.Size(190, 22);
            this.добавитьУзелToolStripMenuItemCM.Text = "Добавить узел...";
            this.добавитьУзелToolStripMenuItemCM.Click += new System.EventHandler(this.tsbAddPoint_Click);
            // 
            // удалитьУзелToolStripMenuItem
            // 
            this.удалитьУзелToolStripMenuItem.Name = "удалитьУзелToolStripMenuItem";
            this.удалитьУзелToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.удалитьУзелToolStripMenuItem.Text = "Удалить узел";
            this.удалитьУзелToolStripMenuItem.Click += new System.EventHandler(this.tsbRemovePoint_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(187, 6);
            // 
            // КомментарийToolStripMenuItem
            // 
            this.КомментарийToolStripMenuItem.Name = "КомментарийToolStripMenuItem";
            this.КомментарийToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.КомментарийToolStripMenuItem.Text = "Комментарий...";
            this.КомментарийToolStripMenuItem.Click += new System.EventHandler(this.КомментарийToolStripMenuItem_Click);
            // 
            // изменитьПривязкуToolStripMenuItem
            // 
            this.изменитьПривязкуToolStripMenuItem.Name = "изменитьПривязкуToolStripMenuItem";
            this.изменитьПривязкуToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.изменитьПривязкуToolStripMenuItem.Text = "Изменить привязку...";
            this.изменитьПривязкуToolStripMenuItem.Click += new System.EventHandler(this.изменитьПривязкуToolStripMenuItem_Click);
            // 
            // изменитьФайлToolStripMenuItem
            // 
            this.изменитьФайлToolStripMenuItem.Name = "изменитьФайлToolStripMenuItem";
            this.изменитьФайлToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.изменитьФайлToolStripMenuItem.Text = "Изменить файл...";
            this.изменитьФайлToolStripMenuItem.Click += new System.EventHandler(this.изменитьФайлToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(187, 6);
            // 
            // удалитьТекстToolStripMenuItem
            // 
            this.удалитьТекстToolStripMenuItem.Name = "удалитьТекстToolStripMenuItem";
            this.удалитьТекстToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.удалитьТекстToolStripMenuItem.Text = "Удалить текст";
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
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(307, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slMain});
            this.statusStrip.Location = new System.Drawing.Point(0, 371);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(307, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // slMain
            // 
            this.slMain.Name = "slMain";
            this.slMain.Size = new System.Drawing.Size(10, 17);
            this.slMain.Text = " ";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "AXML|*.axml|Все файлы|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "AXML|*.axml|Все файлы|*.*";
            // 
            // toolStripDropDownButton1
            // 
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
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(54, 22);
            this.toolStripDropDownButton1.Text = "Меню";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.открытьToolStripMenuItem.Text = "Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Enabled = false;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(192, 6);
            // 
            // отменитьToolStripMenuItem
            // 
            this.отменитьToolStripMenuItem.Name = "отменитьToolStripMenuItem";
            this.отменитьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.отменитьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.отменитьToolStripMenuItem.Text = "Отменить";
            this.отменитьToolStripMenuItem.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // повторитьToolStripMenuItem
            // 
            this.повторитьToolStripMenuItem.Name = "повторитьToolStripMenuItem";
            this.повторитьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.повторитьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.повторитьToolStripMenuItem.Text = "Повторить";
            this.повторитьToolStripMenuItem.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(192, 6);
            // 
            // перейтиКПодаспектуToolStripMenuItem
            // 
            this.перейтиКПодаспектуToolStripMenuItem.Name = "перейтиКПодаспектуToolStripMenuItem";
            this.перейтиКПодаспектуToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.перейтиКПодаспектуToolStripMenuItem.Text = "Перейти к подаспекту";
            this.перейтиКПодаспектуToolStripMenuItem.Click += new System.EventHandler(this.tsbFindNode_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(192, 6);
            // 
            // импортироватьToolStripMenuItem
            // 
            this.импортироватьToolStripMenuItem.Name = "импортироватьToolStripMenuItem";
            this.импортироватьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.импортироватьToolStripMenuItem.Text = "Импортировать...";
            this.импортироватьToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // экспортироватьToolStripMenuItem
            // 
            this.экспортироватьToolStripMenuItem.Name = "экспортироватьToolStripMenuItem";
            this.экспортироватьToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.экспортироватьToolStripMenuItem.Text = "Экспортировать...";
            this.экспортироватьToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // tsbAddFolder
            // 
            this.tsbAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddFolder.Image")));
            this.tsbAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddFolder.Name = "tsbAddFolder";
            this.tsbAddFolder.Size = new System.Drawing.Size(23, 22);
            this.tsbAddFolder.Text = "Добавить группу";
            this.tsbAddFolder.Click += new System.EventHandler(this.tsbAddFolder_Click);
            // 
            // tsbAddPoint
            // 
            this.tsbAddPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddPoint.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddPoint.Image")));
            this.tsbAddPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddPoint.Name = "tsbAddPoint";
            this.tsbAddPoint.Size = new System.Drawing.Size(23, 22);
            this.tsbAddPoint.Text = "Добавить узел";
            this.tsbAddPoint.Click += new System.EventHandler(this.tsbAddPoint_Click);
            // 
            // tsbRemovePoint
            // 
            this.tsbRemovePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemovePoint.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemovePoint.Image")));
            this.tsbRemovePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemovePoint.Name = "tsbRemovePoint";
            this.tsbRemovePoint.Size = new System.Drawing.Size(23, 22);
            this.tsbRemovePoint.Text = "Удалить узел";
            this.tsbRemovePoint.Click += new System.EventHandler(this.tsbRemovePoint_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Открыть...";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndo.Image")));
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbUndo.Text = "Отменить";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbRedo.Image")));
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbRedo.Text = "Повторить";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // tsbFindNode
            // 
            this.tsbFindNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFindNode.Image = ((System.Drawing.Image)(resources.GetObject("tsbFindNode.Image")));
            this.tsbFindNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFindNode.Name = "tsbFindNode";
            this.tsbFindNode.Size = new System.Drawing.Size(23, 22);
            this.tsbFindNode.Text = "Найти подаспект";
            this.tsbFindNode.Click += new System.EventHandler(this.tsbFindNode_Click);
            // 
            // bDebugButton
            // 
            this.bDebugButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bDebugButton.Image = ((System.Drawing.Image)(resources.GetObject("bDebugButton.Image")));
            this.bDebugButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bDebugButton.Name = "bDebugButton";
            this.bDebugButton.Size = new System.Drawing.Size(23, 22);
            this.bDebugButton.Text = "Кнопка для отладки";
            this.bDebugButton.Visible = false;
            this.bDebugButton.Click += new System.EventHandler(this.bDebugButton_Click);
            // 
            // AspectWindowPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvAspects);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "AspectWindowPane";
            this.Size = new System.Drawing.Size(307, 393);
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
