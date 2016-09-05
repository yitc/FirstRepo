namespace GUI
{
    partial class frmTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.radRibbonBarButtonGroup1 = new Telerik.WinControls.UI.RadRibbonBarButtonGroup();
            this.ribbonExampleMenu = new Telerik.WinControls.UI.RadRibbonBar();
            this.radRibbonBarBackstageView1 = new Telerik.WinControls.UI.RadRibbonBarBackstageView();
            this.ribbonTab1 = new Telerik.WinControls.UI.RibbonTab();
            this.radRibbonSave = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnSave = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonReports = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnReport = new Telerik.WinControls.UI.RadButtonElement();
            this.btnWord = new Telerik.WinControls.UI.RadButtonElement();
            this.btnEmail = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonDocuments = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewDoc = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDeleteDoc = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonMemo = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewMemo = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDeleteMemo = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonContact = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewContact = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDelContact = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonTask = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewTask = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDelTask = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonMeeting = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewMeeting = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroupContracts = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnNewContract = new Telerik.WinControls.UI.RadButtonElement();
            this.btnCopyContract = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDeleteContract = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroupPurchase = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnPurchase = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDelPurchase = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroupTraveler = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnAddTraveler = new Telerik.WinControls.UI.RadButtonElement();
            this.btnAddVoluntary = new Telerik.WinControls.UI.RadButtonElement();
            this.btnDeleteTraveler = new Telerik.WinControls.UI.RadButtonElement();
            this.btnCancelTraveler = new Telerik.WinControls.UI.RadButtonElement();
            this.radRibbonBarGroupTravelpapers = new Telerik.WinControls.UI.RadRibbonBarGroup();
            this.btnRibbonTravelpapers = new Telerik.WinControls.UI.RadButtonElement();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonExampleMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radRibbonBarButtonGroup1
            // 
            this.radRibbonBarButtonGroup1.Name = "radRibbonBarButtonGroup1";
            this.radRibbonBarButtonGroup1.Text = "radRibbonBarButtonGroup1";
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
            this.ribbonExampleMenu.Size = new System.Drawing.Size(1048, 164);
            this.ribbonExampleMenu.StartButtonImage = ((System.Drawing.Image)(resources.GetObject("ribbonExampleMenu.StartButtonImage")));
            this.ribbonExampleMenu.TabIndex = 3;
            this.ribbonExampleMenu.ThemeName = "Windows8";
            this.ribbonExampleMenu.SizeChanged += new System.EventHandler(this.ribbonExampleMenu_SizeChanged);
            ((Telerik.WinControls.UI.RadRibbonBarElement)(this.ribbonExampleMenu.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RibbonBar.RibbonBarCaptionFillPrimitive)(this.ribbonExampleMenu.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.Transparent;
            // 
            // radRibbonBarBackstageView1
            // 
            this.radRibbonBarBackstageView1.EnableKeyMap = true;
            this.radRibbonBarBackstageView1.Location = new System.Drawing.Point(1, 50);
            this.radRibbonBarBackstageView1.Name = "radRibbonBarBackstageView1";
            this.radRibbonBarBackstageView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radRibbonBarBackstageView1.SelectedItem = null;
            this.radRibbonBarBackstageView1.Size = new System.Drawing.Size(1050, 509);
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
            this.radRibbonMeeting,
            this.radRibbonBarGroupContracts,
            this.radRibbonBarGroupPurchase,
            this.radRibbonBarGroupTraveler,
            this.radRibbonBarGroupTravelpapers});
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "HOME";
            this.ribbonTab1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // radRibbonSave
            // 
            this.radRibbonSave.AccessibleDescription = "Save";
            this.radRibbonSave.AccessibleName = "Save";
            this.radRibbonSave.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnSave});
            this.radRibbonSave.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonSave.MaxSize = new System.Drawing.Size(0, 0);
            this.radRibbonSave.MinSize = new System.Drawing.Size(0, 0);
            this.radRibbonSave.Name = "radRibbonSave";
            this.radRibbonSave.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnSave.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSave.Image = global::GUI.Properties.Resources.Un_Save;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonReports
            // 
            this.radRibbonReports.AccessibleDescription = "Report";
            this.radRibbonReports.AccessibleName = "Report";
            this.radRibbonReports.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnReport,
            this.btnWord,
            this.btnEmail});
            this.radRibbonReports.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonReports.MaxSize = new System.Drawing.Size(0, 0);
            this.radRibbonReports.MinSize = new System.Drawing.Size(0, 0);
            this.radRibbonReports.Name = "radRibbonReports";
            this.radRibbonReports.Text = "";
            // 
            // btnReport
            // 
            this.btnReport.AutoSize = false;
            this.btnReport.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnReport.Image = global::GUI.Properties.Resources.report;
            this.btnReport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnReport.Name = "btnReport";
            this.btnReport.Text = "Report";
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnWord
            // 
            this.btnWord.AutoSize = false;
            this.btnWord.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnWord.Image = global::GUI.Properties.Resources.Word_add;
            this.btnWord.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnWord.Name = "btnWord";
            this.btnWord.Text = "Word";
            this.btnWord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnEmail
            // 
            this.btnEmail.AutoSize = false;
            this.btnEmail.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEmail.Image = global::GUI.Properties.Resources.Email;
            this.btnEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Text = "Email";
            this.btnEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonDocuments
            // 
            this.radRibbonDocuments.AccessibleDescription = "Documents";
            this.radRibbonDocuments.AccessibleName = "Documents";
            this.radRibbonDocuments.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radRibbonDocuments.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewDoc,
            this.btnDeleteDoc});
            this.radRibbonDocuments.Name = "radRibbonDocuments";
            this.radRibbonDocuments.Text = "Document";
            // 
            // btnNewDoc
            // 
            this.btnNewDoc.AutoSize = false;
            this.btnNewDoc.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewDoc.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.btnNewDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDoc.Image")));
            this.btnNewDoc.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewDoc.Name = "btnNewDoc";
            this.btnNewDoc.Text = "New";
            this.btnNewDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDeleteDoc
            // 
            this.btnDeleteDoc.AutoSize = false;
            this.btnDeleteDoc.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDeleteDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteDoc.Image")));
            this.btnDeleteDoc.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteDoc.Name = "btnDeleteDoc";
            this.btnDeleteDoc.Text = "Delete";
            this.btnDeleteDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // radRibbonMeeting
            // 
            this.radRibbonMeeting.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewMeeting});
            this.radRibbonMeeting.Name = "radRibbonMeeting";
            this.radRibbonMeeting.Text = "Meeting";
            this.radRibbonMeeting.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnNewMeeting
            // 
            this.btnNewMeeting.AutoSize = false;
            this.btnNewMeeting.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewMeeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewMeeting.Image = global::GUI.Properties.Resources.report;
            this.btnNewMeeting.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewMeeting.Name = "btnNewMeeting";
            this.btnNewMeeting.Text = "New";
            this.btnNewMeeting.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewMeeting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewMeeting.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // radRibbonBarGroupContracts
            // 
            this.radRibbonBarGroupContracts.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewContract,
            this.btnCopyContract,
            this.btnDeleteContract});
            this.radRibbonBarGroupContracts.Name = "radRibbonBarGroupContracts";
            this.radRibbonBarGroupContracts.Text = "Contracts";
            this.radRibbonBarGroupContracts.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnNewContract
            // 
            this.btnNewContract.AutoSize = false;
            this.btnNewContract.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNewContract.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNewContract.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnNewContract.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewContract.Name = "btnNewContract";
            this.btnNewContract.Text = "New";
            this.btnNewContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewContract.ToolTipText = "New Contract";
            this.btnNewContract.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnCopyContract
            // 
            this.btnCopyContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCopyContract.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnCopyContract.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCopyContract.Name = "btnCopyContract";
            this.btnCopyContract.Text = "Copy";
            this.btnCopyContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDeleteContract
            // 
            this.btnDeleteContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteContract.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteContract.Image")));
            this.btnDeleteContract.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteContract.Name = "btnDeleteContract";
            this.btnDeleteContract.Text = "Delete";
            this.btnDeleteContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonBarGroupPurchase
            // 
            this.radRibbonBarGroupPurchase.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnPurchase,
            this.btnDelPurchase});
            this.radRibbonBarGroupPurchase.Name = "radRibbonBarGroupPurchase";
            this.radRibbonBarGroupPurchase.Text = "Purchase";
            this.radRibbonBarGroupPurchase.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnPurchase
            // 
            this.btnPurchase.AutoSize = false;
            this.btnPurchase.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnPurchase.FlipText = false;
            this.btnPurchase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPurchase.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnPurchase.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Text = "New";
            this.btnPurchase.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPurchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPurchase.ToolTipText = "New Purchase";
            this.btnPurchase.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnDelPurchase
            // 
            this.btnDelPurchase.AutoSize = false;
            this.btnDelPurchase.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnDelPurchase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelPurchase.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnDelPurchase.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelPurchase.Name = "btnDelPurchase";
            this.btnDelPurchase.Text = "Delete";
            this.btnDelPurchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelPurchase.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // radRibbonBarGroupTraveler
            // 
            this.radRibbonBarGroupTraveler.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnAddTraveler,
            this.btnAddVoluntary,
            this.btnDeleteTraveler,
            this.btnCancelTraveler});
            this.radRibbonBarGroupTraveler.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonBarGroupTraveler.MaxSize = new System.Drawing.Size(0, 0);
            this.radRibbonBarGroupTraveler.MinSize = new System.Drawing.Size(0, 0);
            this.radRibbonBarGroupTraveler.Name = "radRibbonBarGroupTraveler";
            this.radRibbonBarGroupTraveler.Text = "Traveler";
            // 
            // btnAddTraveler
            // 
            this.btnAddTraveler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddTraveler.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnAddTraveler.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddTraveler.Name = "btnAddTraveler";
            this.btnAddTraveler.Text = "New";
            this.btnAddTraveler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnAddVoluntary
            // 
            this.btnAddVoluntary.ForeColor = System.Drawing.Color.Black;
            this.btnAddVoluntary.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVoluntary.Image")));
            this.btnAddVoluntary.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddVoluntary.Name = "btnAddVoluntary";
            this.btnAddVoluntary.Text = "Voluntary";
            this.btnAddVoluntary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDeleteTraveler
            // 
            this.btnDeleteTraveler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteTraveler.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnDeleteTraveler.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteTraveler.Name = "btnDeleteTraveler";
            this.btnDeleteTraveler.Text = "Delete";
            this.btnDeleteTraveler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnCancelTraveler
            // 
            this.btnCancelTraveler.ForeColor = System.Drawing.Color.Black;
            this.btnCancelTraveler.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnCancelTraveler.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelTraveler.Name = "btnCancelTraveler";
            this.btnCancelTraveler.Text = "Cancel";
            this.btnCancelTraveler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonBarGroupTravelpapers
            // 
            this.radRibbonBarGroupTravelpapers.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radRibbonBarGroupTravelpapers.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnRibbonTravelpapers});
            this.radRibbonBarGroupTravelpapers.Name = "radRibbonBarGroupTravelpapers";
            this.radRibbonBarGroupTravelpapers.Text = "Travelpapers";
            this.radRibbonBarGroupTravelpapers.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // btnRibbonTravelpapers
            // 
            this.btnRibbonTravelpapers.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRibbonTravelpapers.AutoSize = false;
            this.btnRibbonTravelpapers.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.btnRibbonTravelpapers.Bounds = new System.Drawing.Rectangle(0, 0, 70, 75);
            this.btnRibbonTravelpapers.DisplayStyle = Telerik.WinControls.DisplayStyle.ImageAndText;
            this.btnRibbonTravelpapers.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            this.btnRibbonTravelpapers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRibbonTravelpapers.Image = global::GUI.Properties.Resources.report;
            this.btnRibbonTravelpapers.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRibbonTravelpapers.Name = "btnRibbonTravelpapers";
            this.btnRibbonTravelpapers.Text = "Open";
            this.btnRibbonTravelpapers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRibbonTravelpapers.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnRibbonTravelpapers.GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.TopLeft;
            // 
            // frmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1050, 559);
            this.Controls.Add(this.ribbonExampleMenu);
            this.Controls.Add(this.radRibbonBarBackstageView1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "frmTemplate";
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
        public Telerik.WinControls.UI.RadButtonElement btnSave;
        public  Telerik.WinControls.UI.RadRibbonBarGroup radRibbonReports;
        public Telerik.WinControls.UI.RadButtonElement btnReport;
        private Telerik.WinControls.UI.RadRibbonBarBackstageView radRibbonBarBackstageView1;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonDocuments;
        public Telerik.WinControls.UI.RadButtonElement btnNewDoc;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonMemo;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteDoc;
        public Telerik.WinControls.UI.RadButtonElement btnNewMemo;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteMemo;
        public Telerik.WinControls.UI.RadButtonElement btnWord;
        public Telerik.WinControls.UI.RadButtonElement btnEmail;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonContact;
        protected Telerik.WinControls.UI.RadButtonElement btnNewContact;
        protected Telerik.WinControls.UI.RadButtonElement btnDelContact;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonTask;
        protected Telerik.WinControls.UI.RadButtonElement btnNewTask;
        protected Telerik.WinControls.UI.RadButtonElement btnDelTask;
        protected Telerik.WinControls.UI.RadButtonElement btnNewMeeting;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonMeeting;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupContracts;
        public Telerik.WinControls.UI.RadButtonElement btnNewContract;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupPurchase;
        public Telerik.WinControls.UI.RadButtonElement btnPurchase;
        public Telerik.WinControls.UI.RadButtonElement btnDelPurchase;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupTraveler;
        public Telerik.WinControls.UI.RadButtonElement btnAddTraveler;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteTraveler;
        public Telerik.WinControls.UI.RadButtonElement btnCopyContract;
        private Telerik.WinControls.UI.RadRibbonBarButtonGroup radRibbonBarButtonGroup1;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteContract;
        public Telerik.WinControls.UI.RadButtonElement btnAddVoluntary;
        public Telerik.WinControls.UI.RadButtonElement btnCancelTraveler;
        protected Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupTravelpapers;
        protected Telerik.WinControls.UI.RadButtonElement btnRibbonTravelpapers;
    }
}
