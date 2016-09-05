namespace GUI
{
    partial class frmDailyBankEntry
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridDailyBank = new Telerik.WinControls.UI.RadGridView();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.radSplitContainerForm = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanelUp = new Telerik.WinControls.UI.SplitPanel();
            this.labelIban = new Telerik.WinControls.UI.RadLabel();
            this.labelDailyType = new Telerik.WinControls.UI.RadLabel();
            this.labelAccount = new Telerik.WinControls.UI.RadLabel();
            this.labelDescription = new Telerik.WinControls.UI.RadLabel();
            this.labelDaily = new Telerik.WinControls.UI.RadLabel();
            this.lblDailyType = new Telerik.WinControls.UI.RadLabel();
            this.lblIban = new Telerik.WinControls.UI.RadLabel();
            this.lblAccount = new Telerik.WinControls.UI.RadLabel();
            this.lblDescription = new Telerik.WinControls.UI.RadLabel();
            this.lblDaily = new Telerik.WinControls.UI.RadLabel();
            this.splitPanelDown = new Telerik.WinControls.UI.SplitPanel();
            this.gridDailyKas = new Telerik.WinControls.UI.RadGridView();
            this.gridDailyMemo = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyBank.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainerForm)).BeginInit();
            this.radSplitContainerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelUp)).BeginInit();
            this.splitPanelUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelIban)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDailyType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDailyType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIban)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelDown)).BeginInit();
            this.splitPanelDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyKas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyKas.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyMemo.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.AccessibleDescription = "HOME";
            this.ribbonTab1.AccessibleName = "HOME";
            this.ribbonTab1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ribbonTab1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ribbonTab1.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid;
            this.ribbonTab1.Bounds = new System.Drawing.Rectangle(0, 0, 52, 28);
            this.ribbonTab1.ClipDrawing = true;
            this.ribbonTab1.ClipText = true;
            this.ribbonTab1.DrawBorder = true;
            this.ribbonTab1.DrawFill = true;
            this.ribbonTab1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonTab1.ForeColor = System.Drawing.Color.Black;
            this.ribbonTab1.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            this.ribbonTab1.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonTab1.ImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ribbonTab1.MinSize = new System.Drawing.Size(8, 8);
            this.ribbonTab1.NumberOfColors = 1;
            this.ribbonTab1.Padding = new System.Windows.Forms.Padding(4);
            this.ribbonTab1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonTab1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnNew
            // 
            this.btnNew.AccessibleDescription = "New";
            this.btnNew.AccessibleName = "New";
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.CanFocus = true;
            this.btnNew.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = "Delete";
            this.btnDelete.AccessibleName = "Delete";
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.CanFocus = true;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // radRibbonDocuments
            // 
            this.radRibbonDocuments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonDocuments.Bounds = new System.Drawing.Rectangle(0, 0, 136, 100);
            this.radRibbonDocuments.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonDocuments.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonDocuments.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonDocuments.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonDocuments.MinSize = new System.Drawing.Size(20, 86);
            // 
            // btnNewItem
            // 
            this.btnNewItem.BackColor = System.Drawing.Color.Transparent;
            this.btnNewItem.CanFocus = true;
            this.btnNewItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonMemo
            // 
            this.radRibbonMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonMemo.Bounds = new System.Drawing.Rectangle(0, 0, 136, 100);
            this.radRibbonMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonMemo.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonMemo.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonMemo.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonMemo.MinSize = new System.Drawing.Size(20, 86);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteItem.CanFocus = true;
            this.btnDeleteItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnNewMemo
            // 
            this.btnNewMemo.AccessibleDescription = "New";
            this.btnNewMemo.AccessibleName = "New";
            this.btnNewMemo.BackColor = System.Drawing.Color.Transparent;
            this.btnNewMemo.CanFocus = true;
            this.btnNewMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDeleteMemo
            // 
            this.btnDeleteMemo.AccessibleDescription = "Delete";
            this.btnDeleteMemo.AccessibleName = "Delete";
            this.btnDeleteMemo.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteMemo.CanFocus = true;
            this.btnDeleteMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnBooking
            // 
            this.btnBooking.AccessibleDescription = "Booking";
            this.btnBooking.AccessibleName = "Booking";
            this.btnBooking.BackColor = System.Drawing.Color.Transparent;
            this.btnBooking.CanFocus = true;
            this.btnBooking.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmail
            // 
            this.btnEmail.BackColor = System.Drawing.Color.Transparent;
            this.btnEmail.CanFocus = true;
            this.btnEmail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonContact
            // 
            this.radRibbonContact.AccessibleDescription = "Contact";
            this.radRibbonContact.AccessibleName = "Contact";
            this.radRibbonContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonContact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonContact.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonContact.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonContact.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonContact.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonContact.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // btnNewContact
            // 
            this.btnNewContact.AccessibleDescription = "New";
            this.btnNewContact.AccessibleName = "New";
            this.btnNewContact.BackColor = System.Drawing.Color.Transparent;
            this.btnNewContact.CanFocus = true;
            this.btnNewContact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDelContact
            // 
            this.btnDelContact.AccessibleDescription = "Delete";
            this.btnDelContact.AccessibleName = "Delete";
            this.btnDelContact.BackColor = System.Drawing.Color.Transparent;
            this.btnDelContact.CanFocus = true;
            this.btnDelContact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonTask
            // 
            this.radRibbonTask.AccessibleDescription = "Task";
            this.radRibbonTask.AccessibleName = "Task";
            this.radRibbonTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonTask.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonTask.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonTask.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonTask.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonTask.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonTask.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // btnNewTask
            // 
            this.btnNewTask.AccessibleDescription = "New";
            this.btnNewTask.AccessibleName = "New";
            this.btnNewTask.BackColor = System.Drawing.Color.Transparent;
            this.btnNewTask.CanFocus = true;
            this.btnNewTask.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDelTask
            // 
            this.btnDelTask.AccessibleDescription = "Delete";
            this.btnDelTask.AccessibleName = "Delete";
            this.btnDelTask.BackColor = System.Drawing.Color.Transparent;
            this.btnDelTask.CanFocus = true;
            this.btnDelTask.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExit
            // 
            this.btnExit.AccessibleDescription = "Exit";
            this.btnExit.AccessibleName = "Exit";
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.CanFocus = true;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonExit
            // 
            this.radRibbonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonExit.Bounds = new System.Drawing.Rectangle(0, 0, 73, 100);
            this.radRibbonExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonExit.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonExit.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonExit.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonExit.MinSize = new System.Drawing.Size(20, 86);
            // 
            // gridDailyBank
            // 
            this.gridDailyBank.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDailyBank.AutoGenerateHierarchy = true;
            this.gridDailyBank.EnableKineticScrolling = true;
            this.gridDailyBank.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridDailyBank.Location = new System.Drawing.Point(326, 17);
            // 
            // 
            // 
            this.gridDailyBank.MasterTemplate.AllowAddNewRow = false;
            this.gridDailyBank.MasterTemplate.AllowDeleteRow = false;
            this.gridDailyBank.MasterTemplate.AllowEditRow = false;
            this.gridDailyBank.MasterTemplate.AllowSearchRow = true;
            this.gridDailyBank.MasterTemplate.AutoExpandGroups = true;
            this.gridDailyBank.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridDailyBank.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridDailyBank.MasterTemplate.EnableFiltering = true;
            this.gridDailyBank.MasterTemplate.PageSize = 15;
            this.gridDailyBank.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridDailyBank.Name = "gridDailyBank";
            this.gridDailyBank.Size = new System.Drawing.Size(187, 89);
            this.gridDailyBank.TabIndex = 0;
            this.gridDailyBank.ThemeName = "VisualStudio2012Light";
            this.gridDailyBank.Visible = false;
            this.gridDailyBank.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridDailyBank_CurrentRowChanged);
            this.gridDailyBank.UserAddedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.gridDailyBank_UserAddedRow);
            this.gridDailyBank.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.gridDailyBank_UserDeletingRow);
            this.gridDailyBank.UserDeletedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.gridDailyBank_UserDeletedRow);
            this.gridDailyBank.RowsChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.gridDailyBank_RowsChanged);
            this.gridDailyBank.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDailyBank_CellDoubleClick);
            this.gridDailyBank.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridDailyBank_DataBindingComplete);
            this.gridDailyBank.Click += new System.EventHandler(this.gridDailyBank_Click);
            // 
            // radMenu1
            // 
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(365, 20);
            this.radMenu1.TabIndex = 2;
            this.radMenu1.Text = "radMenu1";
            // 
            // radSplitContainerForm
            // 
            this.radSplitContainerForm.Controls.Add(this.splitPanelUp);
            this.radSplitContainerForm.Controls.Add(this.splitPanelDown);
            this.radSplitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainerForm.Location = new System.Drawing.Point(1, 167);
            this.radSplitContainerForm.Name = "radSplitContainerForm";
            this.radSplitContainerForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.radSplitContainerForm.Size = new System.Drawing.Size(890, 579);
            this.radSplitContainerForm.SplitterWidth = 1;
            this.radSplitContainerForm.TabIndex = 5;
            this.radSplitContainerForm.TabStop = false;
            this.radSplitContainerForm.Text = "radSplitContainer1";
            this.radSplitContainerForm.ThemeName = "Windows8";
            // 
            // splitPanelUp
            // 
            this.splitPanelUp.Controls.Add(this.labelIban);
            this.splitPanelUp.Controls.Add(this.labelDailyType);
            this.splitPanelUp.Controls.Add(this.labelAccount);
            this.splitPanelUp.Controls.Add(this.labelDescription);
            this.splitPanelUp.Controls.Add(this.labelDaily);
            this.splitPanelUp.Controls.Add(this.lblDailyType);
            this.splitPanelUp.Controls.Add(this.lblIban);
            this.splitPanelUp.Controls.Add(this.lblAccount);
            this.splitPanelUp.Controls.Add(this.lblDescription);
            this.splitPanelUp.Controls.Add(this.lblDaily);
            this.splitPanelUp.Location = new System.Drawing.Point(0, 0);
            this.splitPanelUp.Name = "splitPanelUp";
            this.splitPanelUp.Size = new System.Drawing.Size(890, 45);
            this.splitPanelUp.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.4219381F);
            this.splitPanelUp.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -190);
            this.splitPanelUp.TabIndex = 0;
            this.splitPanelUp.TabStop = false;
            this.splitPanelUp.ThemeName = "Windows8";
            // 
            // labelIban
            // 
            this.labelIban.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelIban.BackColor = System.Drawing.Color.Transparent;
            this.labelIban.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelIban.ForeColor = System.Drawing.Color.Teal;
            this.labelIban.Location = new System.Drawing.Point(606, 24);
            this.labelIban.Name = "labelIban";
            this.labelIban.Size = new System.Drawing.Size(62, 17);
            this.labelIban.TabIndex = 8;
            this.labelIban.Text = "radLabel6";
            // 
            // labelDailyType
            // 
            this.labelDailyType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDailyType.BackColor = System.Drawing.Color.Transparent;
            this.labelDailyType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDailyType.ForeColor = System.Drawing.Color.Teal;
            this.labelDailyType.Location = new System.Drawing.Point(788, 24);
            this.labelDailyType.Name = "labelDailyType";
            this.labelDailyType.Size = new System.Drawing.Size(62, 17);
            this.labelDailyType.TabIndex = 8;
            this.labelDailyType.Text = "radLabel6";
            // 
            // labelAccount
            // 
            this.labelAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAccount.BackColor = System.Drawing.Color.Transparent;
            this.labelAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelAccount.ForeColor = System.Drawing.Color.Teal;
            this.labelAccount.Location = new System.Drawing.Point(375, 24);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(62, 17);
            this.labelAccount.TabIndex = 7;
            this.labelAccount.Text = "radLabel6";
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDescription.ForeColor = System.Drawing.Color.Teal;
            this.labelDescription.Location = new System.Drawing.Point(131, 24);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(62, 17);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "radLabel6";
            // 
            // labelDaily
            // 
            this.labelDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDaily.BackColor = System.Drawing.Color.Transparent;
            this.labelDaily.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDaily.ForeColor = System.Drawing.Color.Teal;
            this.labelDaily.Location = new System.Drawing.Point(13, 24);
            this.labelDaily.Name = "labelDaily";
            this.labelDaily.Size = new System.Drawing.Size(62, 17);
            this.labelDaily.TabIndex = 5;
            this.labelDaily.Text = "radLabel6";
            // 
            // lblDailyType
            // 
            this.lblDailyType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDailyType.BackColor = System.Drawing.Color.Transparent;
            this.lblDailyType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDailyType.Location = new System.Drawing.Point(780, 0);
            this.lblDailyType.Name = "lblDailyType";
            this.lblDailyType.Size = new System.Drawing.Size(66, 17);
            this.lblDailyType.TabIndex = 4;
            this.lblDailyType.Text = "Daily Type";
            // 
            // lblIban
            // 
            this.lblIban.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIban.BackColor = System.Drawing.Color.Transparent;
            this.lblIban.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblIban.Location = new System.Drawing.Point(595, 0);
            this.lblIban.Name = "lblIban";
            this.lblIban.Size = new System.Drawing.Size(35, 17);
            this.lblIban.TabIndex = 3;
            this.lblIban.Text = "IBAN";
            // 
            // lblAccount
            // 
            this.lblAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccount.BackColor = System.Drawing.Color.Transparent;
            this.lblAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAccount.Location = new System.Drawing.Point(375, 0);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(78, 17);
            this.lblAccount.TabIndex = 2;
            this.lblAccount.Text = "Dbk.Account";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDescription.Location = new System.Drawing.Point(131, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 17);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblDaily
            // 
            this.lblDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDaily.BackColor = System.Drawing.Color.Transparent;
            this.lblDaily.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDaily.Location = new System.Drawing.Point(13, 0);
            this.lblDaily.Name = "lblDaily";
            this.lblDaily.Size = new System.Drawing.Size(61, 17);
            this.lblDaily.TabIndex = 0;
            this.lblDaily.Text = "Dbk.Code";
            // 
            // splitPanelDown
            // 
            this.splitPanelDown.Controls.Add(this.gridDailyKas);
            this.splitPanelDown.Controls.Add(this.gridDailyMemo);
            this.splitPanelDown.Controls.Add(this.gridDailyBank);
            this.splitPanelDown.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.splitPanelDown.Location = new System.Drawing.Point(0, 46);
            this.splitPanelDown.Name = "splitPanelDown";
            this.splitPanelDown.Size = new System.Drawing.Size(890, 533);
            this.splitPanelDown.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.4219381F);
            this.splitPanelDown.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 190);
            this.splitPanelDown.TabIndex = 1;
            this.splitPanelDown.TabStop = false;
            this.splitPanelDown.ThemeName = "Windows8";
            // 
            // gridDailyKas
            // 
            this.gridDailyKas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDailyKas.AutoGenerateHierarchy = true;
            this.gridDailyKas.EnableKineticScrolling = true;
            this.gridDailyKas.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridDailyKas.Location = new System.Drawing.Point(661, 17);
            // 
            // 
            // 
            this.gridDailyKas.MasterTemplate.AllowAddNewRow = false;
            this.gridDailyKas.MasterTemplate.AllowDeleteRow = false;
            this.gridDailyKas.MasterTemplate.AllowEditRow = false;
            this.gridDailyKas.MasterTemplate.AllowSearchRow = true;
            this.gridDailyKas.MasterTemplate.AutoExpandGroups = true;
            this.gridDailyKas.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridDailyKas.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridDailyKas.MasterTemplate.EnableFiltering = true;
            this.gridDailyKas.MasterTemplate.PageSize = 15;
            this.gridDailyKas.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.gridDailyKas.Name = "gridDailyKas";
            this.gridDailyKas.Size = new System.Drawing.Size(158, 89);
            this.gridDailyKas.TabIndex = 1;
            this.gridDailyKas.ThemeName = "VisualStudio2012Light";
            this.gridDailyKas.Visible = false;
            this.gridDailyKas.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridDailyKas_ViewCellFormatting);
            this.gridDailyKas.ChildViewExpanding += new Telerik.WinControls.UI.ChildViewExpandingEventHandler(this.gridDailyKas_ChildViewExpanding);
            this.gridDailyKas.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDailyKas_CellDoubleClick);
            this.gridDailyKas.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridDailyKas_DataBindingComplete);
            // 
            // gridDailyMemo
            // 
            this.gridDailyMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDailyMemo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridDailyMemo.Location = new System.Drawing.Point(13, 17);
            // 
            // 
            // 
            this.gridDailyMemo.MasterTemplate.AllowAddNewRow = false;
            this.gridDailyMemo.MasterTemplate.AllowDeleteRow = false;
            this.gridDailyMemo.MasterTemplate.AllowEditRow = false;
            this.gridDailyMemo.MasterTemplate.AllowSearchRow = true;
            this.gridDailyMemo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridDailyMemo.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridDailyMemo.MasterTemplate.EnableFiltering = true;
            this.gridDailyMemo.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.gridDailyMemo.Name = "gridDailyMemo";
            this.gridDailyMemo.Size = new System.Drawing.Size(183, 89);
            this.gridDailyMemo.TabIndex = 1;
            this.gridDailyMemo.ThemeName = "VisualStudio2012Light";
            this.gridDailyMemo.Visible = false;
            this.gridDailyMemo.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridDailyMemo_ViewCellFormatting);
            this.gridDailyMemo.ChildViewExpanding += new Telerik.WinControls.UI.ChildViewExpandingEventHandler(this.gridDailyMemo_ChildViewExpanding);
            this.gridDailyMemo.UserAddedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.gridDailyMemo_UserAddedRow);
            this.gridDailyMemo.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.gridDailyMemo_UserDeletingRow);
            this.gridDailyMemo.UserDeletedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.gridDailyMemo_UserDeletedRow);
            this.gridDailyMemo.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDailyMemo_CellDoubleClick);
            this.gridDailyMemo.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridDailyMemo_DataBindingComplete);
            // 
            // frmDailyBankEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 747);
            this.Controls.Add(this.radSplitContainerForm);
            this.Name = "frmDailyBankEntry";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmDailyBankEntry";
            this.Load += new System.EventHandler(this.frmDailyBankEntry_Load);
            this.Controls.SetChildIndex(this.radSplitContainerForm, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyBank.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainerForm)).EndInit();
            this.radSplitContainerForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelUp)).EndInit();
            this.splitPanelUp.ResumeLayout(false);
            this.splitPanelUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelIban)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDailyType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDailyType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblIban)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelDown)).EndInit();
            this.splitPanelDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyKas.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyKas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyMemo.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDailyMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridDailyBank;                
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainerForm;
        private Telerik.WinControls.UI.SplitPanel splitPanelUp;
        private Telerik.WinControls.UI.SplitPanel splitPanelDown;
        private Telerik.WinControls.UI.RadLabel lblDaily;
        private Telerik.WinControls.UI.RadLabel lblDescription;
        private Telerik.WinControls.UI.RadLabel lblIban;
        private Telerik.WinControls.UI.RadLabel lblAccount;
        private Telerik.WinControls.UI.RadLabel labelDaily;
        private Telerik.WinControls.UI.RadLabel lblDailyType;
        private Telerik.WinControls.UI.RadLabel labelDailyType;
        private Telerik.WinControls.UI.RadLabel labelAccount;
        private Telerik.WinControls.UI.RadLabel labelDescription;
        private Telerik.WinControls.UI.RadGridView gridDailyMemo;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadGridView gridDailyKas;
        private Telerik.WinControls.UI.RadLabel labelIban;

    }
}