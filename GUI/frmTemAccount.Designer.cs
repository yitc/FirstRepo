namespace GUI
{
    partial class frmTempAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTempAccount));
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.ribbonExampleMenu = new Telerik.WinControls.UI.RadRibbonBar();
            this.radRibbonBarBackstageView1 = new Telerik.WinControls.UI.RadRibbonBarBackstageView();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonSave = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNew = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonReports = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnDelete = new Telerik.WinControls.UI.RadButtonElement();
            this.btnBooking = new Telerik.WinControls.UI.RadButtonElement();
            this.btnEmail = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonDocuments = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewItem = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDeleteItem = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonMemo = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewMemo = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDeleteMemo = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonContact = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewContact = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDelContact = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonTask = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewTask = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDelTask = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonExit = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnExit = new Telerik.WinControls.UI.RadButtonElement();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonExampleMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonExampleMenu
            // 
            this.ribbonExampleMenu.ApplicationMenuStyle = Telerik.WinControls.UI.ApplicationMenuStyle.BackstageView;
            this.ribbonExampleMenu.BackstageControl = this.radRibbonBarBackstageView1;
            this.ribbonExampleMenu.CloseButton = false;
            this.ribbonExampleMenu.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab1});
            // 
            // 
            // 
            this.ribbonExampleMenu.ExitButton.Text = "Exit";
            this.ribbonExampleMenu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonExampleMenu.ForeColor = System.Drawing.Color.Transparent;
            this.ribbonExampleMenu.Location = new System.Drawing.Point(1, 0);
            this.ribbonExampleMenu.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonExampleMenu.MaximizeButton = false;
            this.ribbonExampleMenu.MinimizeButton = false;
            this.ribbonExampleMenu.Name = "ribbonExampleMenu";
            // 
            // 
            // 
            this.ribbonExampleMenu.OptionsButton.Text = "Options";
            this.ribbonExampleMenu.Size = new System.Drawing.Size(967, 164);
            this.ribbonExampleMenu.StartButtonImage = ((System.Drawing.Image)(resources.GetObject("ribbonExampleMenu.StartButtonImage")));
            this.ribbonExampleMenu.TabIndex = 3;
            this.ribbonExampleMenu.ThemeName = "Windows8";
            ((Telerik.WinControls.UI.RadRibbonBarElement)(this.ribbonExampleMenu.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RibbonBar.RibbonBarCaptionFillPrimitive)(this.ribbonExampleMenu.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.Transparent;
            // 
            // radRibbonBarBackstageView1
            // 
            this.radRibbonBarBackstageView1.EnableKeyMap = true;
            this.radRibbonBarBackstageView1.Location = new System.Drawing.Point(0, 53);
            this.radRibbonBarBackstageView1.Name = "radRibbonBarBackstageView1";
            this.radRibbonBarBackstageView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radRibbonBarBackstageView1.SelectedItem = null;
            this.radRibbonBarBackstageView1.Size = new System.Drawing.Size(801, 509);
            this.radRibbonBarBackstageView1.TabIndex = 4;
            this.radRibbonBarBackstageView1.ThemeName = "Windows8";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.AutoEllipsis = false;
            this.ribbonTab1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.ribbonTab1.IsSelected = true;
            this.ribbonTab1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radRibbonSave,
            this.radRibbonReports,
            this.radRibbonDocuments,
            this.radRibbonMemo,
            this.radRibbonContact,
            this.radRibbonTask,
            this.radRibbonExit});
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "HOME";
            this.ribbonTab1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // radRibbonSave
            // 
            this.radRibbonSave.AccessibleDescription = "Save";
            this.radRibbonSave.AccessibleName = "Save";
            this.radRibbonSave.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNew});
            this.radRibbonSave.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonSave.MaxSize = new System.Drawing.Size(0, 0);
            this.radRibbonSave.MinSize = new System.Drawing.Size(0, 0);
            this.radRibbonSave.Name = "radRibbonSave";
            this.radRibbonSave.Text = "";
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnNew.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNew.Image = global::GUI.Properties.Resources.Un_Save;
            this.btnNew.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNew.Name = "btnNew";
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonReports
            // 
            this.radRibbonReports.AccessibleDescription = "Report";
            this.radRibbonReports.AccessibleName = "Report";
            this.radRibbonReports.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnDelete,
            this.btnBooking,
            this.btnEmail});
            this.radRibbonReports.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonReports.MaxSize = new System.Drawing.Size(0, 0);
            this.radRibbonReports.MinSize = new System.Drawing.Size(0, 0);
            this.radRibbonReports.Name = "radRibbonReports";
            this.radRibbonReports.Text = "";
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.Image = global::GUI.Properties.Resources.Un_Delete;
            this.btnDelete.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnBooking
            // 
            this.btnBooking.AutoSize = false;
            this.btnBooking.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnBooking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBooking.Image = global::GUI.Properties.Resources.Word_add;
            this.btnBooking.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Text = "Booking";
            this.btnBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEmail
            // 
            this.btnEmail.AccessibleDescription = "Email";
            this.btnEmail.AccessibleName = "Email";
            this.btnEmail.AutoSize = false;
            this.btnEmail.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEmail.Image = global::GUI.Properties.Resources.Email;
            this.btnEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Text = "View";
            this.btnEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonDocuments
            // 
            this.radRibbonDocuments.AccessibleDescription = "Documents";
            this.radRibbonDocuments.AccessibleName = "Documents";
            this.radRibbonDocuments.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radRibbonDocuments.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewItem,
            this.btnDeleteItem});
            this.radRibbonDocuments.Name = "radRibbonDocuments";
            this.radRibbonDocuments.Text = "Document";
            // 
            // btnNewItem
            // 
            this.btnNewItem.AccessibleDescription = "NewItem";
            this.btnNewItem.AccessibleName = "NewItem";
            this.btnNewItem.AutoSize = false;
            this.btnNewItem.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewItem.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.btnNewItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewItem.Image = ((System.Drawing.Image)(resources.GetObject("btnNewItem.Image")));
            this.btnNewItem.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewItem.Name = "btnNewItem";
            this.btnNewItem.Text = "New Item";
            this.btnNewItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewItem.TextWrap = true;
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.AccessibleDescription = "DeleteItem";
            this.btnDeleteItem.AccessibleName = "DeleteItem";
            this.btnDeleteItem.AutoSize = false;
            this.btnDeleteItem.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDeleteItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteItem.Image")));
            this.btnDeleteItem.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Text = "Delete item";
            this.btnDeleteItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteItem.TextWrap = true;
            // 
            // radRibbonMemo
            // 
            this.radRibbonMemo.AccessibleDescription = "Note";
            this.radRibbonMemo.AccessibleName = "Note";
            this.radRibbonMemo.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewMemo,
            this.btnDeleteMemo});
            this.radRibbonMemo.Name = "radRibbonMemo";
            this.radRibbonMemo.Text = "Memo";
            // 
            // btnNewMemo
            // 
            this.btnNewMemo.AutoSize = false;
            this.btnNewMemo.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewMemo.Image = global::GUI.Properties.Resources.Note_add;
            this.btnNewMemo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewMemo.Name = "btnNewMemo";
            this.btnNewMemo.Text = "New";
            this.btnNewMemo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDeleteMemo
            // 
            this.btnDeleteMemo.AutoSize = false;
            this.btnDeleteMemo.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDeleteMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteMemo.Image = global::GUI.Properties.Resources.Note_delete;
            this.btnDeleteMemo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteMemo.Name = "btnDeleteMemo";
            this.btnDeleteMemo.Text = "Delete";
            this.btnDeleteMemo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonContact
            // 
            this.radRibbonContact.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewContact,
            this.btnDelContact});
            this.radRibbonContact.Name = "radRibbonContact";
            this.radRibbonContact.Text = "Contact";
            this.radRibbonContact.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnNewContact
            // 
            this.btnNewContact.AutoSize = false;
            this.btnNewContact.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewContact.Image = ((System.Drawing.Image)(resources.GetObject("btnNewContact.Image")));
            this.btnNewContact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewContact.Name = "btnNewContact";
            this.btnNewContact.Text = "New";
            this.btnNewContact.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDelContact
            // 
            this.btnDelContact.AutoSize = false;
            this.btnDelContact.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDelContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelContact.Image = ((System.Drawing.Image)(resources.GetObject("btnDelContact.Image")));
            this.btnDelContact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelContact.Name = "btnDelContact";
            this.btnDelContact.Text = "Delete";
            this.btnDelContact.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonTask
            // 
            this.radRibbonTask.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewTask,
            this.btnDelTask});
            this.radRibbonTask.Name = "radRibbonTask";
            this.radRibbonTask.Text = "Task";
            this.radRibbonTask.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnNewTask
            // 
            this.btnNewTask.AutoSize = false;
            this.btnNewTask.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewTask.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTask.Image")));
            this.btnNewTask.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Text = "New";
            this.btnNewTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDelTask
            // 
            this.btnDelTask.AutoSize = false;
            this.btnDelTask.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDelTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelTask.Image = ((System.Drawing.Image)(resources.GetObject("btnDelTask.Image")));
            this.btnDelTask.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelTask.Name = "btnDelTask";
            this.btnDelTask.Text = "Delete";
            this.btnDelTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonExit
            // 
            this.radRibbonExit.AccessibleDescription = "Exit";
            this.radRibbonExit.AccessibleName = "Exit";
            this.radRibbonExit.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnExit});
            this.radRibbonExit.Name = "radRibbonExit";
            this.radRibbonExit.Text = "";
            // 
            // btnExit
            // 
            this.btnExit.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.AutoSize = false;
            this.btnExit.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnExit.Image = global::GUI.Properties.Resources.filter_clear;
            this.btnExit.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit.Name = "btnExit";
            this.btnExit.Text = "Exit";
            this.btnExit.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmTempAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(969, 559);
            this.Controls.Add(this.ribbonExampleMenu);
            this.Controls.Add(this.radRibbonBarBackstageView1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "frmTempAccount";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.ThemeName = "Windows8";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonExampleMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Telerik.WinControls.UI.RadRibbonBar ribbonExampleMenu;
        public Telerik.WinControls.UI.RibbonTab ribbonTab1;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonSave;
        public Telerik.WinControls.UI.RadButtonElement btnNew;
        private Telerik.WinControls.UI.RadRibbonBarGroup radRibbonReports;
        public Telerik.WinControls.UI.RadButtonElement btnDelete;
        private Telerik.WinControls.UI.RadRibbonBarBackstageView radRibbonBarBackstageView1;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonDocuments;
        public Telerik.WinControls.UI.RadButtonElement btnNewItem;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonMemo;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteItem;
        public Telerik.WinControls.UI.RadButtonElement btnNewMemo;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteMemo;
        public Telerik.WinControls.UI.RadButtonElement btnBooking;
        public Telerik.WinControls.UI.RadButtonElement btnEmail;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonContact;
        protected Telerik.WinControls.UI.RadButtonElement btnNewContact;
        protected Telerik.WinControls.UI.RadButtonElement btnDelContact;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonTask;
        protected Telerik.WinControls.UI.RadButtonElement btnNewTask;
        protected Telerik.WinControls.UI.RadButtonElement btnDelTask;
        protected Telerik.WinControls.UI.RadButtonElement btnExit;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonExit;
    }
}
