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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
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
            this.HOME = new Telerik.WinControls.UI.RadMenuItem();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundRectShape1 = new Telerik.WinControls.RoundRectShape(this.components);
            this.chamferedRectShape1 = new Telerik.WinControls.ChamferedRectShape();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonExampleMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonExampleMenu
            // 
            this.ribbonExampleMenu.ApplicationMenuStyle = Telerik.WinControls.UI.ApplicationMenuStyle.BackstageView;
            this.ribbonExampleMenu.BackstageControl = this.radRibbonBarBackstageView1;
            this.ribbonExampleMenu.CommandTabs.AddRange(new Telerik.WinControls.RadItem[] {
            this.ribbonTab1});
            this.ribbonExampleMenu.EnableTheming = false;
            // 
            // 
            // 
            this.ribbonExampleMenu.ExitButton.Text = "Exit";
            this.ribbonExampleMenu.ExitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ribbonExampleMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ribbonExampleMenu.Location = new System.Drawing.Point(0, 0);
            this.ribbonExampleMenu.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonExampleMenu.Name = "ribbonExampleMenu";
            // 
            // 
            // 
            this.ribbonExampleMenu.OptionsButton.Text = "Options";
            this.ribbonExampleMenu.OptionsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // 
            // 
            this.ribbonExampleMenu.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ribbonExampleMenu.ShowExpandButton = false;
            this.ribbonExampleMenu.Size = new System.Drawing.Size(1098, 175);
            this.ribbonExampleMenu.StartMenuItems.AddRange(new Telerik.WinControls.RadItem[] {
            this.HOME});
            this.ribbonExampleMenu.TabIndex = 0;
            this.ribbonExampleMenu.Text = "frmTtest";
            this.ribbonExampleMenu.SizeChanged += new System.EventHandler(this.ribbonExampleMenu_SizeChanged);
            // 
            // radRibbonBarBackstageView1
            // 
            this.radRibbonBarBackstageView1.EnableKeyMap = true;
            this.radRibbonBarBackstageView1.Location = new System.Drawing.Point(0, 56);
            this.radRibbonBarBackstageView1.Name = "radRibbonBarBackstageView1";
            this.radRibbonBarBackstageView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radRibbonBarBackstageView1.SelectedItem = null;
            this.radRibbonBarBackstageView1.Size = new System.Drawing.Size(1049, 531);
            this.radRibbonBarBackstageView1.TabIndex = 3;
            // 
            // ribbonTab1
            // 
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
            // 
            // radRibbonSave
            // 
            this.radRibbonSave.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonSave.AutoSize = true;
            this.radRibbonSave.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            this.radRibbonSave.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnSave});
            this.radRibbonSave.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonSave.Name = "radRibbonSave";
            this.radRibbonSave.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSave.Image = global::GUI.Properties.Resources.Un_Save;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Save\r\n";
            this.btnSave.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonReports
            // 
            this.radRibbonReports.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonReports.AutoSize = true;
            this.radRibbonReports.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnReport,
            this.btnWord,
            this.btnEmail});
            this.radRibbonReports.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonReports.Name = "radRibbonReports";
            this.radRibbonReports.Text = "";
            // 
            // btnReport
            // 
            this.btnReport.AutoSize = false;
            this.btnReport.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnReport.Image = global::GUI.Properties.Resources.report;
            this.btnReport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnReport.Name = "btnReport";
            this.btnReport.Text = "Report";
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnReport.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnReport.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWord
            // 
            this.btnWord.AutoSize = false;
            this.btnWord.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnWord.Image = global::GUI.Properties.Resources.Word_add;
            this.btnWord.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnWord.Name = "btnWord";
            this.btnWord.Text = "Word";
            this.btnWord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnWord.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnWord.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEmail
            // 
            this.btnEmail.AutoSize = false;
            this.btnEmail.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEmail.Image = global::GUI.Properties.Resources.Email;
            this.btnEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Text = "Email";
            this.btnEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmail.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmail.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonDocuments
            // 
            this.radRibbonDocuments.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonDocuments.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewDoc,
            this.btnDeleteDoc});
            this.radRibbonDocuments.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonDocuments.Name = "radRibbonDocuments";
            this.radRibbonDocuments.Text = "Document";
            // 
            // btnNewDoc
            // 
            this.btnNewDoc.AutoSize = false;
            this.btnNewDoc.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnNewDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewDoc.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnNewDoc.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewDoc.Name = "btnNewDoc";
            this.btnNewDoc.Text = "New ";
            this.btnNewDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewDoc.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewDoc.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteDoc
            // 
            this.btnDeleteDoc.AutoSize = false;
            this.btnDeleteDoc.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDeleteDoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteDoc.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnDeleteDoc.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteDoc.Name = "btnDeleteDoc";
            this.btnDeleteDoc.Text = "Delete";
            this.btnDeleteDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteDoc.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteDoc.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonMemo
            // 
            this.radRibbonMemo.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonMemo.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewMemo,
            this.btnDeleteMemo});
            this.radRibbonMemo.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonMemo.Name = "radRibbonMemo";
            this.radRibbonMemo.Text = "Memo";
            // 
            // btnNewMemo
            // 
            this.btnNewMemo.AutoSize = false;
            this.btnNewMemo.Bounds = new System.Drawing.Rectangle(0, 0, 63, 73);
            this.btnNewMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewMemo.Image = global::GUI.Properties.Resources.Note_add;
            this.btnNewMemo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewMemo.Name = "btnNewMemo";
            this.btnNewMemo.Text = "New";
            this.btnNewMemo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewMemo.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewMemo.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteMemo
            // 
            this.btnDeleteMemo.AutoSize = false;
            this.btnDeleteMemo.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDeleteMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteMemo.Image = global::GUI.Properties.Resources.Note_delete;
            this.btnDeleteMemo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteMemo.Name = "btnDeleteMemo";
            this.btnDeleteMemo.Text = "Delete";
            this.btnDeleteMemo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteMemo.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteMemo.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonContact
            // 
            this.radRibbonContact.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonContact.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewContact,
            this.btnDelContact});
            this.radRibbonContact.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonContact.Name = "radRibbonContact";
            this.radRibbonContact.Text = "Contact";
            // 
            // btnNewContact
            // 
            this.btnNewContact.AutoSize = false;
            this.btnNewContact.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnNewContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewContact.Image = ((System.Drawing.Image)(resources.GetObject("btnNewContact.Image")));
            this.btnNewContact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewContact.Name = "btnNewContact";
            this.btnNewContact.Text = "New";
            this.btnNewContact.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewContact.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewContact.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelContact
            // 
            this.btnDelContact.AutoSize = false;
            this.btnDelContact.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDelContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelContact.Image = ((System.Drawing.Image)(resources.GetObject("btnDelContact.Image")));
            this.btnDelContact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelContact.Name = "btnDelContact";
            this.btnDelContact.Text = "Delete";
            this.btnDelContact.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDelContact.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDelContact.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonTask
            // 
            this.radRibbonTask.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonTask.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewTask,
            this.btnDelTask});
            this.radRibbonTask.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonTask.Name = "radRibbonTask";
            this.radRibbonTask.Text = "Task";
            // 
            // btnNewTask
            // 
            this.btnNewTask.AutoSize = false;
            this.btnNewTask.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnNewTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewTask.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTask.Image")));
            this.btnNewTask.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Text = "New";
            this.btnNewTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewTask.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewTask.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelTask
            // 
            this.btnDelTask.AutoSize = false;
            this.btnDelTask.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDelTask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelTask.Image = ((System.Drawing.Image)(resources.GetObject("btnDelTask.Image")));
            this.btnDelTask.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelTask.Name = "btnDelTask";
            this.btnDelTask.Text = "Delete";
            this.btnDelTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDelTask.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDelTask.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonMeeting
            // 
            this.radRibbonMeeting.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonMeeting.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewMeeting});
            this.radRibbonMeeting.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonMeeting.Name = "radRibbonMeeting";
            this.radRibbonMeeting.Text = "Meeting";
            // 
            // btnNewMeeting
            // 
            this.btnNewMeeting.AutoSize = false;
            this.btnNewMeeting.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnNewMeeting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewMeeting.Image = global::GUI.Properties.Resources.report;
            this.btnNewMeeting.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewMeeting.Name = "btnNewMeeting";
            this.btnNewMeeting.Text = "New";
            this.btnNewMeeting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewMeeting.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewMeeting.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonBarGroupContracts
            // 
            this.radRibbonBarGroupContracts.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonBarGroupContracts.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnNewContract,
            this.btnCopyContract,
            this.btnDeleteContract});
            this.radRibbonBarGroupContracts.Margin = new System.Windows.Forms.Padding(0);
            this.radRibbonBarGroupContracts.Name = "radRibbonBarGroupContracts";
            this.radRibbonBarGroupContracts.Text = "Contracts";
            // 
            // btnNewContract
            // 
            this.btnNewContract.AutoSize = false;
            this.btnNewContract.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnNewContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNewContract.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnNewContract.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNewContract.Name = "btnNewContract";
            this.btnNewContract.Text = "New";
            this.btnNewContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewContract.ToolTipText = "New Contract";
            this.btnNewContract.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewContract.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNewContract.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCopyContract
            // 
            this.btnCopyContract.AutoSize = false;
            this.btnCopyContract.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnCopyContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCopyContract.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnCopyContract.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCopyContract.Name = "btnCopyContract";
            this.btnCopyContract.Text = "Copy";
            this.btnCopyContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCopyContract.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCopyContract.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteContract
            // 
            this.btnDeleteContract.AutoSize = false;
            this.btnDeleteContract.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDeleteContract.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteContract.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnDeleteContract.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteContract.Name = "btnDeleteContract";
            this.btnDeleteContract.Text = "Delete";
            this.btnDeleteContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteContract.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteContract.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonBarGroupPurchase
            // 
            this.radRibbonBarGroupPurchase.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnPurchase,
            this.btnDelPurchase});
            this.radRibbonBarGroupPurchase.Name = "radRibbonBarGroupPurchase";
            this.radRibbonBarGroupPurchase.Text = "Purchase";
            // 
            // btnPurchase
            // 
            this.btnPurchase.AutoSize = false;
            this.btnPurchase.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnPurchase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPurchase.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnPurchase.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Text = "New";
            this.btnPurchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDelPurchase
            // 
            this.btnDelPurchase.AutoSize = false;
            this.btnDelPurchase.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDelPurchase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelPurchase.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnDelPurchase.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelPurchase.Name = "btnDelPurchase";
            this.btnDelPurchase.Text = "Delete";
            this.btnDelPurchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // radRibbonBarGroupTraveler
            // 
            this.radRibbonBarGroupTraveler.Alignment = System.Drawing.ContentAlignment.TopCenter;
            this.radRibbonBarGroupTraveler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.radRibbonBarGroupTraveler.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnAddTraveler,
            this.btnAddVoluntary,
            this.btnDeleteTraveler,
            this.btnCancelTraveler});
            this.radRibbonBarGroupTraveler.Name = "radRibbonBarGroupTraveler";
            this.radRibbonBarGroupTraveler.Text = "Traveler";
            // 
            // btnAddTraveler
            // 
            this.btnAddTraveler.AutoSize = false;
            this.btnAddTraveler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.btnAddTraveler.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnAddTraveler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(6)))), ((int)(((byte)(197)))));
            this.btnAddTraveler.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnAddTraveler.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddTraveler.Name = "btnAddTraveler";
            this.btnAddTraveler.Text = "New";
            this.btnAddTraveler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddTraveler.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddTraveler.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddVoluntary
            // 
            this.btnAddVoluntary.AutoSize = false;
            this.btnAddVoluntary.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnAddVoluntary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAddVoluntary.Image = global::GUI.Properties.Resources.Doc_add;
            this.btnAddVoluntary.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddVoluntary.Name = "btnAddVoluntary";
            this.btnAddVoluntary.Text = "Voluntary";
            this.btnAddVoluntary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddVoluntary.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddVoluntary.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeleteTraveler
            // 
            this.btnDeleteTraveler.AutoSize = false;
            this.btnDeleteTraveler.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnDeleteTraveler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDeleteTraveler.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnDeleteTraveler.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteTraveler.Name = "btnDeleteTraveler";
            this.btnDeleteTraveler.Text = "Delete";
            this.btnDeleteTraveler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteTraveler.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteTraveler.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelTraveler
            // 
            this.btnCancelTraveler.AutoSize = false;
            this.btnCancelTraveler.Bounds = new System.Drawing.Rectangle(0, 0, 63, 73);
            this.btnCancelTraveler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelTraveler.Image = global::GUI.Properties.Resources.Doc_delete;
            this.btnCancelTraveler.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancelTraveler.Name = "btnCancelTraveler";
            this.btnCancelTraveler.Text = "Cancel";
            this.btnCancelTraveler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelTraveler.GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.SystemColors.WindowText;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelTraveler.GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radRibbonBarGroupTravelpapers
            // 
            this.radRibbonBarGroupTravelpapers.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnRibbonTravelpapers});
            this.radRibbonBarGroupTravelpapers.Name = "radRibbonBarGroupTravelpapers";
            this.radRibbonBarGroupTravelpapers.Text = "Travel papers";
            // 
            // btnRibbonTravelpapers
            // 
            this.btnRibbonTravelpapers.AutoSize = false;
            this.btnRibbonTravelpapers.Bounds = new System.Drawing.Rectangle(0, 0, 63, 76);
            this.btnRibbonTravelpapers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRibbonTravelpapers.Image = global::GUI.Properties.Resources.report;
            this.btnRibbonTravelpapers.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRibbonTravelpapers.Name = "btnRibbonTravelpapers";
            this.btnRibbonTravelpapers.Text = "Open";
            this.btnRibbonTravelpapers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRibbonTravelpapers.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            // 
            // HOME
            // 
            this.HOME.Name = "HOME";
            this.HOME.Text = "HOME";
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 561);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(1098, 26);
            this.radStatusStrip1.SizingGrip = false;
            this.radStatusStrip1.TabIndex = 1;
            this.radStatusStrip1.Text = "radStatusStrip1";
            ((Telerik.WinControls.UI.RadStatusBarElement)(this.radStatusStrip1.GetChildAt(0))).Text = "radStatusStrip1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 175);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 386);
            this.panel1.TabIndex = 2;
            // 
            // roundRectShape1
            // 
            this.roundRectShape1.BottomLeftRounded = false;
            this.roundRectShape1.BottomRightRounded = false;
            this.roundRectShape1.TopLeftRounded = false;
            this.roundRectShape1.TopRightRounded = false;
            // 
            // frmTemplate
            // 
            this.AllowAero = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1098, 587);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radStatusStrip1);
            this.Controls.Add(this.ribbonExampleMenu);
            this.Controls.Add(this.radRibbonBarBackstageView1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTemplate";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = false;
            this.RootElement.Shape = this.roundRectShape1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTtest";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonExampleMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radRibbonBarBackstageView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadMenuItem HOME;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonSave;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonReports;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonDocuments;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonMemo;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonContact;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonTask;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonMeeting;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupContracts;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupPurchase;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupTraveler;
        public Telerik.WinControls.UI.RadRibbonBarGroup radRibbonBarGroupTravelpapers;
        public Telerik.WinControls.UI.RadButtonElement btnPurchase;
        public Telerik.WinControls.UI.RadButtonElement btnDelPurchase;
        public Telerik.WinControls.UI.RadButtonElement btnAddTraveler;
        public Telerik.WinControls.UI.RadButtonElement btnAddVoluntary;
        public Telerik.WinControls.UI.RadButtonElement btnCancelTraveler;
        public Telerik.WinControls.UI.RadButtonElement btnRibbonTravelpapers;
        public Telerik.WinControls.UI.RibbonTab ribbonTab1;
        public Telerik.WinControls.UI.RadButtonElement btnSave;
        public Telerik.WinControls.UI.RadButtonElement btnReport;
        public Telerik.WinControls.UI.RadButtonElement btnWord;
        public Telerik.WinControls.UI.RadButtonElement btnEmail;
        public Telerik.WinControls.UI.RadButtonElement btnNewDoc;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteDoc;
        public Telerik.WinControls.UI.RadButtonElement btnNewMemo;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteMemo;
        public Telerik.WinControls.UI.RadButtonElement btnNewContact;
        public Telerik.WinControls.UI.RadButtonElement btnDelContact;
        public Telerik.WinControls.UI.RadButtonElement btnNewTask;
        public Telerik.WinControls.UI.RadButtonElement btnDelTask;
        public Telerik.WinControls.UI.RadButtonElement btnNewMeeting;
        public Telerik.WinControls.UI.RadButtonElement btnNewContract;
        public Telerik.WinControls.UI.RadButtonElement btnCopyContract;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteContract;
        public Telerik.WinControls.UI.RadRibbonBar ribbonExampleMenu;
        private Telerik.WinControls.UI.RadRibbonBarBackstageView radRibbonBarBackstageView1;
        private Telerik.WinControls.RoundRectShape roundRectShape1;
        private Telerik.WinControls.ChamferedRectShape chamferedRectShape1;
        public Telerik.WinControls.UI.RadButtonElement btnDeleteTraveler;
    }
}
