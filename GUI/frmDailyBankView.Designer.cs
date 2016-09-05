namespace GUI
{
    partial class frmDailyBankView
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
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem1 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem2 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem3 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.lblStatement = new Telerik.WinControls.UI.RadLabel();
            this.txtStatement = new Telerik.WinControls.UI.RadTextBox();
            this.lblDateStatement = new Telerik.WinControls.UI.RadLabel();
            this.lblBegin = new Telerik.WinControls.UI.RadLabel();
            this.txtBeginSaldo = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblEnd = new Telerik.WinControls.UI.RadLabel();
            this.txtEndSaldo = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.gridBank = new Telerik.WinControls.UI.RadGridView();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.lblUnlisted = new Telerik.WinControls.UI.RadLabel();
            this.txtDiff = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtDateStatement = new Telerik.WinControls.UI.RadTextBox();
            this.labelKonto = new Telerik.WinControls.UI.RadLabel();
            this.btnPdf = new Telerik.WinControls.UI.RadButton();
            this.btnErasePdf = new Telerik.WinControls.UI.RadButton();
            this.txtBook = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblBooked = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSaldo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSaldo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBank.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnlisted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelKonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnErasePdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBooked)).BeginInit();
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
            this.btnNew.Bounds = new System.Drawing.Rectangle(0, 0, 63, 75);
            this.btnNew.CanFocus = true;
            this.btnNew.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Click += new System.EventHandler(this.btnSave_Click);
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
            this.radRibbonDocuments.AutoSize = false;
            this.radRibbonDocuments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonDocuments.Bounds = new System.Drawing.Rectangle(0, -5, 136, 100);
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
            this.radRibbonMemo.Bounds = new System.Drawing.Rectangle(0, 0, 136, 99);
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
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // radRibbonExit
            // 
            this.radRibbonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonExit.Bounds = new System.Drawing.Rectangle(0, 0, 73, 99);
            this.radRibbonExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonExit.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonExit.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonExit.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonExit.MinSize = new System.Drawing.Size(20, 86);
            // 
            // lblStatement
            // 
            this.lblStatement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatement.Location = new System.Drawing.Point(14, 193);
            this.lblStatement.Name = "lblStatement";
            this.lblStatement.Size = new System.Drawing.Size(72, 18);
            this.lblStatement.TabIndex = 0;
            this.lblStatement.Text = "Statement";
            this.lblStatement.ThemeName = "Windows8";
            // 
            // txtStatement
            // 
            this.txtStatement.Enabled = false;
            this.txtStatement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtStatement.Location = new System.Drawing.Point(134, 191);
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.ReadOnly = true;
            this.txtStatement.Size = new System.Drawing.Size(100, 20);
            this.txtStatement.TabIndex = 1;
            this.txtStatement.TabStop = false;
            this.txtStatement.ThemeName = "Windows8";
            // 
            // lblDateStatement
            // 
            this.lblDateStatement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDateStatement.Location = new System.Drawing.Point(14, 171);
            this.lblDateStatement.Name = "lblDateStatement";
            this.lblDateStatement.Size = new System.Drawing.Size(36, 18);
            this.lblDateStatement.TabIndex = 1;
            this.lblDateStatement.Text = "Date";
            this.lblDateStatement.ThemeName = "Windows8";
            // 
            // lblBegin
            // 
            this.lblBegin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBegin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBegin.Location = new System.Drawing.Point(635, 171);
            this.lblBegin.Name = "lblBegin";
            this.lblBegin.Size = new System.Drawing.Size(80, 18);
            this.lblBegin.TabIndex = 2;
            this.lblBegin.Text = "Begin Saldo";
            this.lblBegin.ThemeName = "Windows8";
            // 
            // txtBeginSaldo
            // 
            this.txtBeginSaldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeginSaldo.Enabled = false;
            this.txtBeginSaldo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBeginSaldo.Location = new System.Drawing.Point(753, 169);
            this.txtBeginSaldo.Mask = "N2";
            this.txtBeginSaldo.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtBeginSaldo.Name = "txtBeginSaldo";
            this.txtBeginSaldo.ReadOnly = true;
            this.txtBeginSaldo.Size = new System.Drawing.Size(125, 20);
            this.txtBeginSaldo.TabIndex = 3;
            this.txtBeginSaldo.TabStop = false;
            this.txtBeginSaldo.Text = "0,00";
            this.txtBeginSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBeginSaldo.ThemeName = "Windows8";
            // 
            // lblEnd
            // 
            this.lblEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblEnd.Location = new System.Drawing.Point(635, 215);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(69, 18);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "End Saldo";
            this.lblEnd.ThemeName = "Windows8";
            // 
            // txtEndSaldo
            // 
            this.txtEndSaldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndSaldo.Enabled = false;
            this.txtEndSaldo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtEndSaldo.Location = new System.Drawing.Point(753, 213);
            this.txtEndSaldo.Mask = "N2";
            this.txtEndSaldo.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtEndSaldo.Name = "txtEndSaldo";
            this.txtEndSaldo.ReadOnly = true;
            this.txtEndSaldo.Size = new System.Drawing.Size(125, 20);
            this.txtEndSaldo.TabIndex = 4;
            this.txtEndSaldo.TabStop = false;
            this.txtEndSaldo.Text = "0,00";
            this.txtEndSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEndSaldo.ThemeName = "Windows8";
            // 
            // gridBank
            // 
            this.gridBank.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.gridBank.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridBank.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridBank.ForeColor = System.Drawing.Color.Black;
            this.gridBank.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gridBank.Location = new System.Drawing.Point(4, 263);
            // 
            // 
            // 
            this.gridBank.MasterTemplate.AllowAddNewRow = false;
            this.gridBank.MasterTemplate.AllowDeleteRow = false;
            this.gridBank.MasterTemplate.AllowEditRow = false;
            this.gridBank.MasterTemplate.AllowSearchRow = true;
            this.gridBank.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridBank.MasterTemplate.EnableFiltering = true;
            gridViewSummaryItem1.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem1.AggregateExpression = null;
            gridViewSummaryItem1.FormatString = "{0}";
            gridViewSummaryItem1.Name = "debitLine";
            gridViewSummaryItem2.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem2.AggregateExpression = null;
            gridViewSummaryItem2.FormatString = "{0}";
            gridViewSummaryItem2.Name = "creditLine";
            gridViewSummaryItem3.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem3.AggregateExpression = null;
            gridViewSummaryItem3.FormatString = "{0}";
            gridViewSummaryItem3.Name = "versil";
            this.gridBank.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem1,
                gridViewSummaryItem2,
                gridViewSummaryItem3}));
            this.gridBank.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridBank.Name = "gridBank";
            this.gridBank.ReadOnly = true;
            this.gridBank.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridBank.Size = new System.Drawing.Size(874, 433);
            this.gridBank.TabIndex = 5;
            this.gridBank.ThemeName = "VisualStudio2012Light";
            this.gridBank.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.MasterTemplate_ViewCellFormatting);
            this.gridBank.RowsChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.gridBank_RowsChanged);
            this.gridBank.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridBank_CellDoubleClick);
            this.gridBank.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.gridBank_ContextMenuOpening);
            this.gridBank.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridBank_DataBindingComplete);
            // 
            // radMenu1
            // 
            this.radMenu1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radMenu1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem1});
            this.radMenu1.Location = new System.Drawing.Point(0, 580);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(886, 20);
            this.radMenu1.TabIndex = 1;
            this.radMenu1.Text = "radMenu1";
            this.radMenu1.ThemeName = "Windows8";
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "Save layout";
            this.radMenuItem1.Click += new System.EventHandler(this.radMenuItem1_Click);
            // 
            // lblUnlisted
            // 
            this.lblUnlisted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnlisted.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblUnlisted.Location = new System.Drawing.Point(635, 238);
            this.lblUnlisted.Name = "lblUnlisted";
            this.lblUnlisted.Size = new System.Drawing.Size(57, 18);
            this.lblUnlisted.TabIndex = 4;
            this.lblUnlisted.Text = "Unlisted";
            this.lblUnlisted.ThemeName = "Windows8";
            // 
            // txtDiff
            // 
            this.txtDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiff.Enabled = false;
            this.txtDiff.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDiff.Location = new System.Drawing.Point(753, 235);
            this.txtDiff.Mask = "N2";
            this.txtDiff.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtDiff.Name = "txtDiff";
            this.txtDiff.Size = new System.Drawing.Size(125, 20);
            this.txtDiff.TabIndex = 8;
            this.txtDiff.TabStop = false;
            this.txtDiff.Text = "0,00";
            this.txtDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiff.ThemeName = "Windows8";
            // 
            // txtDateStatement
            // 
            this.txtDateStatement.Enabled = false;
            this.txtDateStatement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDateStatement.Location = new System.Drawing.Point(134, 169);
            this.txtDateStatement.MaxLength = 10;
            this.txtDateStatement.Name = "txtDateStatement";
            this.txtDateStatement.ReadOnly = true;
            this.txtDateStatement.Size = new System.Drawing.Size(100, 20);
            this.txtDateStatement.TabIndex = 2;
            this.txtDateStatement.TabStop = false;
            this.txtDateStatement.ThemeName = "Windows8";
            // 
            // labelKonto
            // 
            this.labelKonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelKonto.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelKonto.Location = new System.Drawing.Point(249, 171);
            this.labelKonto.Name = "labelKonto";
            this.labelKonto.Size = new System.Drawing.Size(14, 18);
            this.labelKonto.TabIndex = 3;
            this.labelKonto.Text = "x";
            this.labelKonto.ThemeName = "Windows8";
            // 
            // btnPdf
            // 
            this.btnPdf.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPdf.Location = new System.Drawing.Point(527, 168);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(66, 24);
            this.btnPdf.TabIndex = 9;
            this.btnPdf.Text = "Pdf";
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // btnErasePdf
            // 
            this.btnErasePdf.Location = new System.Drawing.Point(599, 168);
            this.btnErasePdf.Name = "btnErasePdf";
            this.btnErasePdf.Size = new System.Drawing.Size(23, 24);
            this.btnErasePdf.TabIndex = 10;
            this.btnErasePdf.Text = "X";
            this.btnErasePdf.Click += new System.EventHandler(this.btnErasePdf_Click);
            // 
            // txtBook
            // 
            this.txtBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBook.Enabled = false;
            this.txtBook.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtBook.Location = new System.Drawing.Point(753, 191);
            this.txtBook.Mask = "N2";
            this.txtBook.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtBook.Name = "txtBook";
            this.txtBook.Size = new System.Drawing.Size(125, 20);
            this.txtBook.TabIndex = 12;
            this.txtBook.TabStop = false;
            this.txtBook.Text = "0,00";
            this.txtBook.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBook.ThemeName = "Windows8";
            // 
            // lblBooked
            // 
            this.lblBooked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBooked.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBooked.Location = new System.Drawing.Point(635, 194);
            this.lblBooked.Name = "lblBooked";
            this.lblBooked.Size = new System.Drawing.Size(52, 18);
            this.lblBooked.TabIndex = 11;
            this.lblBooked.Text = "Booked";
            this.lblBooked.ThemeName = "Windows8";
            // 
            // frmDailyBankView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 747);
            this.Controls.Add(this.txtBook);
            this.Controls.Add(this.lblBooked);
            this.Controls.Add(this.btnErasePdf);
            this.Controls.Add(this.btnPdf);
            this.Controls.Add(this.labelKonto);
            this.Controls.Add(this.txtDateStatement);
            this.Controls.Add(this.txtDiff);
            this.Controls.Add(this.lblUnlisted);
            this.Controls.Add(this.gridBank);
            this.Controls.Add(this.txtEndSaldo);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.txtBeginSaldo);
            this.Controls.Add(this.lblBegin);
            this.Controls.Add(this.lblDateStatement);
            this.Controls.Add(this.txtStatement);
            this.Controls.Add(this.lblStatement);
            this.Name = "frmDailyBankView";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "BANK Statement preview";
            this.Load += new System.EventHandler(this.frmDailyBankView_Load);
            this.Controls.SetChildIndex(this.lblStatement, 0);
            this.Controls.SetChildIndex(this.txtStatement, 0);
            this.Controls.SetChildIndex(this.lblDateStatement, 0);
            this.Controls.SetChildIndex(this.lblBegin, 0);
            this.Controls.SetChildIndex(this.txtBeginSaldo, 0);
            this.Controls.SetChildIndex(this.lblEnd, 0);
            this.Controls.SetChildIndex(this.txtEndSaldo, 0);
            this.Controls.SetChildIndex(this.gridBank, 0);
            this.Controls.SetChildIndex(this.lblUnlisted, 0);
            this.Controls.SetChildIndex(this.txtDiff, 0);
            this.Controls.SetChildIndex(this.txtDateStatement, 0);
            this.Controls.SetChildIndex(this.labelKonto, 0);
            this.Controls.SetChildIndex(this.btnPdf, 0);
            this.Controls.SetChildIndex(this.btnErasePdf, 0);
            this.Controls.SetChildIndex(this.lblBooked, 0);
            this.Controls.SetChildIndex(this.txtBook, 0);
            ((System.ComponentModel.ISupportInitialize)(this.lblStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBeginSaldo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndSaldo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBank.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnlisted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelKonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnErasePdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBooked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblStatement;
        private Telerik.WinControls.UI.RadTextBox txtStatement;
        private Telerik.WinControls.UI.RadLabel lblDateStatement;
        private Telerik.WinControls.UI.RadLabel lblBegin;
        private Telerik.WinControls.UI.RadMaskedEditBox txtBeginSaldo;
        private Telerik.WinControls.UI.RadLabel lblEnd;
        private Telerik.WinControls.UI.RadMaskedEditBox txtEndSaldo;
        private Telerik.WinControls.UI.RadGridView gridBank;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadLabel lblUnlisted;
        private Telerik.WinControls.UI.RadMaskedEditBox txtDiff;
        private Telerik.WinControls.UI.RadTextBox txtDateStatement;
        private Telerik.WinControls.UI.RadLabel labelKonto;
        private Telerik.WinControls.UI.RadButton btnPdf;
        private Telerik.WinControls.UI.RadButton btnErasePdf;
        private Telerik.WinControls.UI.RadMaskedEditBox txtBook;
        private Telerik.WinControls.UI.RadLabel lblBooked;
    }
}
