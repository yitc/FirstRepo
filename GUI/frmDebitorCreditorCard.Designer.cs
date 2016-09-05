namespace GUI
{
    partial class frmDebitorCreditorCard
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
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem4 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem5 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.rbDebitor = new Telerik.WinControls.UI.RadRadioButton();
            this.rbCreditor = new Telerik.WinControls.UI.RadRadioButton();
            this.rlCustomerFrom = new Telerik.WinControls.UI.RadLabel();
            this.rlCustomerTo = new Telerik.WinControls.UI.RadLabel();
            this.txtCustomerFrom = new Telerik.WinControls.UI.RadTextBox();
            this.txtCustomerTo = new Telerik.WinControls.UI.RadTextBox();
            this.btnCustomerFrom = new Telerik.WinControls.UI.RadButton();
            this.btnCustomerTo = new Telerik.WinControls.UI.RadButton();
            this.lblNameCustomerFrom = new Telerik.WinControls.UI.RadLabel();
            this.lblNameCustomerTo = new Telerik.WinControls.UI.RadLabel();
            this.chkWithBeginBalans = new Telerik.WinControls.UI.RadCheckBox();
            this.lblFromDate = new Telerik.WinControls.UI.RadLabel();
            this.lblToDate = new Telerik.WinControls.UI.RadLabel();
            this.pickerFromDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.pickerToDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.btnDo = new Telerik.WinControls.UI.RadButton();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.rbnSum = new Telerik.WinControls.UI.RadRadioButton();
            this.rbnDetail = new Telerik.WinControls.UI.RadRadioButton();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            ((System.ComponentModel.ISupportInitialize)(this.rbDebitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCreditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlCustomerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlCustomerTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomerTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNameCustomerFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNameCustomerTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWithBeginBalans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pickerFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pickerToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbnSum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbnDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rbDebitor
            // 
            this.rbDebitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbDebitor.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbDebitor.Location = new System.Drawing.Point(13, 13);
            this.rbDebitor.Name = "rbDebitor";
            this.rbDebitor.Size = new System.Drawing.Size(66, 18);
            this.rbDebitor.TabIndex = 0;
            this.rbDebitor.Text = "Debitor";
            this.rbDebitor.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rbDebitor.CheckStateChanged += new System.EventHandler(this.rbDebitor_CheckStateChanged);
            // 
            // rbCreditor
            // 
            this.rbCreditor.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbCreditor.Location = new System.Drawing.Point(133, 12);
            this.rbCreditor.Name = "rbCreditor";
            this.rbCreditor.Size = new System.Drawing.Size(71, 18);
            this.rbCreditor.TabIndex = 1;
            this.rbCreditor.TabStop = false;
            this.rbCreditor.Text = "Creditor";
            // 
            // rlCustomerFrom
            // 
            this.rlCustomerFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rlCustomerFrom.Location = new System.Drawing.Point(13, 35);
            this.rlCustomerFrom.Name = "rlCustomerFrom";
            this.rlCustomerFrom.Size = new System.Drawing.Size(100, 18);
            this.rlCustomerFrom.TabIndex = 2;
            this.rlCustomerFrom.Text = "Customer from";
            // 
            // rlCustomerTo
            // 
            this.rlCustomerTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rlCustomerTo.Location = new System.Drawing.Point(13, 59);
            this.rlCustomerTo.Name = "rlCustomerTo";
            this.rlCustomerTo.Size = new System.Drawing.Size(83, 18);
            this.rlCustomerTo.TabIndex = 3;
            this.rlCustomerTo.Text = "Customer to";
            // 
            // txtCustomerFrom
            // 
            this.txtCustomerFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCustomerFrom.Location = new System.Drawing.Point(133, 34);
            this.txtCustomerFrom.Name = "txtCustomerFrom";
            this.txtCustomerFrom.Size = new System.Drawing.Size(87, 20);
            this.txtCustomerFrom.TabIndex = 4;
            this.txtCustomerFrom.ThemeName = "Windows8";
            this.txtCustomerFrom.Leave += new System.EventHandler(this.txtCustomerFrom_Leave);
            // 
            // txtCustomerTo
            // 
            this.txtCustomerTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCustomerTo.Location = new System.Drawing.Point(133, 61);
            this.txtCustomerTo.Name = "txtCustomerTo";
            this.txtCustomerTo.Size = new System.Drawing.Size(87, 20);
            this.txtCustomerTo.TabIndex = 5;
            this.txtCustomerTo.ThemeName = "Windows8";
            this.txtCustomerTo.Leave += new System.EventHandler(this.txtCustomerTo_Leave);
            // 
            // btnCustomerFrom
            // 
            this.btnCustomerFrom.Location = new System.Drawing.Point(226, 34);
            this.btnCustomerFrom.Name = "btnCustomerFrom";
            this.btnCustomerFrom.Size = new System.Drawing.Size(20, 20);
            this.btnCustomerFrom.TabIndex = 3;
            this.btnCustomerFrom.Text = "...";
            this.btnCustomerFrom.ThemeName = "Windows8";
            this.btnCustomerFrom.Click += new System.EventHandler(this.btnCustomerFrom_Click);
            // 
            // btnCustomerTo
            // 
            this.btnCustomerTo.Location = new System.Drawing.Point(226, 61);
            this.btnCustomerTo.Name = "btnCustomerTo";
            this.btnCustomerTo.Size = new System.Drawing.Size(20, 20);
            this.btnCustomerTo.TabIndex = 10;
            this.btnCustomerTo.Text = "...";
            this.btnCustomerTo.ThemeName = "Windows8";
            this.btnCustomerTo.Click += new System.EventHandler(this.btnCustomerTo_Click);
            // 
            // lblNameCustomerFrom
            // 
            this.lblNameCustomerFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNameCustomerFrom.Location = new System.Drawing.Point(252, 35);
            this.lblNameCustomerFrom.Name = "lblNameCustomerFrom";
            this.lblNameCustomerFrom.Size = new System.Drawing.Size(11, 18);
            this.lblNameCustomerFrom.TabIndex = 11;
            this.lblNameCustomerFrom.Text = " ";
            // 
            // lblNameCustomerTo
            // 
            this.lblNameCustomerTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNameCustomerTo.Location = new System.Drawing.Point(252, 63);
            this.lblNameCustomerTo.Name = "lblNameCustomerTo";
            this.lblNameCustomerTo.Size = new System.Drawing.Size(11, 18);
            this.lblNameCustomerTo.TabIndex = 12;
            this.lblNameCustomerTo.Text = " ";
            // 
            // chkWithBeginBalans
            // 
            this.chkWithBeginBalans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWithBeginBalans.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkWithBeginBalans.Location = new System.Drawing.Point(681, 12);
            this.chkWithBeginBalans.Name = "chkWithBeginBalans";
            this.chkWithBeginBalans.Size = new System.Drawing.Size(133, 18);
            this.chkWithBeginBalans.TabIndex = 13;
            this.chkWithBeginBalans.Text = "With Begin Balans";
            // 
            // lblFromDate
            // 
            this.lblFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFromDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFromDate.Location = new System.Drawing.Point(900, 12);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(70, 18);
            this.lblFromDate.TabIndex = 16;
            this.lblFromDate.Text = "From date";
            // 
            // lblToDate
            // 
            this.lblToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblToDate.Location = new System.Drawing.Point(900, 36);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(53, 18);
            this.lblToDate.TabIndex = 17;
            this.lblToDate.Text = "To date";
            // 
            // pickerFromDate
            // 
            this.pickerFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pickerFromDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pickerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.pickerFromDate.Location = new System.Drawing.Point(1001, 10);
            this.pickerFromDate.Name = "pickerFromDate";
            this.pickerFromDate.Size = new System.Drawing.Size(110, 20);
            this.pickerFromDate.TabIndex = 18;
            this.pickerFromDate.TabStop = false;
            this.pickerFromDate.Text = "1-1-2016";
            this.pickerFromDate.ThemeName = "Windows8";
            this.pickerFromDate.Value = new System.DateTime(2016, 1, 1, 14, 38, 0, 0);
            // 
            // pickerToDate
            // 
            this.pickerToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pickerToDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pickerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.pickerToDate.Location = new System.Drawing.Point(1001, 33);
            this.pickerToDate.Name = "pickerToDate";
            this.pickerToDate.Size = new System.Drawing.Size(110, 20);
            this.pickerToDate.TabIndex = 19;
            this.pickerToDate.TabStop = false;
            this.pickerToDate.Text = "25-4-2016";
            this.pickerToDate.ThemeName = "Windows8";
            this.pickerToDate.Value = new System.DateTime(2016, 4, 25, 14, 38, 33, 630);
            // 
            // radGridView1
            // 
            this.radGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radGridView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radGridView1.Location = new System.Drawing.Point(13, 98);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.radGridView1.MasterTemplate.EnableFiltering = true;
            gridViewSummaryItem1.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem1.AggregateExpression = null;
            gridViewSummaryItem1.FormatString = "{0}";
            gridViewSummaryItem1.Name = "Credit";
            gridViewSummaryItem2.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem2.AggregateExpression = null;
            gridViewSummaryItem2.FormatString = "{0}";
            gridViewSummaryItem2.Name = "Debit";
            gridViewSummaryItem3.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem3.AggregateExpression = null;
            gridViewSummaryItem3.FormatString = "{0}";
            gridViewSummaryItem3.Name = "Saldo";
            gridViewSummaryItem4.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem4.AggregateExpression = null;
            gridViewSummaryItem4.FormatString = "{0}";
            gridViewSummaryItem4.Name = "DebitBalans";
            gridViewSummaryItem5.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem5.AggregateExpression = null;
            gridViewSummaryItem5.FormatString = "{0}";
            gridViewSummaryItem5.Name = "CreditBalans";
            this.radGridView1.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem1,
                gridViewSummaryItem2,
                gridViewSummaryItem3,
                gridViewSummaryItem4,
                gridViewSummaryItem5}));
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.Size = new System.Drawing.Size(1099, 410);
            this.radGridView1.TabIndex = 20;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.ThemeName = "VisualStudio2012Light";
            this.radGridView1.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.radGridView1_ViewCellFormatting);
            this.radGridView1.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.radGridView1_DataBindingComplete);
            // 
            // btnDo
            // 
            this.btnDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDo.Location = new System.Drawing.Point(704, 526);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(125, 24);
            this.btnDo.TabIndex = 21;
            this.btnDo.Text = "Do";
            this.btnDo.ThemeName = "Windows8";
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(845, 526);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(125, 24);
            this.btnPrint.TabIndex = 22;
            this.btnPrint.Text = "Print";
            this.btnPrint.ThemeName = "Windows8";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(986, 526);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 24);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "Windows8";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.radGroupBox1.Controls.Add(this.rbnSum);
            this.radGroupBox1.Controls.Add(this.rbnDetail);
            this.radGroupBox1.HeaderText = "";
            this.radGroupBox1.Location = new System.Drawing.Point(675, 35);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.Size = new System.Drawing.Size(202, 56);
            this.radGroupBox1.TabIndex = 26;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(1).GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(1).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(2).GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radGroupBox1.GetChildAt(0).GetChildAt(2).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            // 
            // rbnSum
            // 
            this.rbnSum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbnSum.Location = new System.Drawing.Point(8, 29);
            this.rbnSum.Name = "rbnSum";
            this.rbnSum.Size = new System.Drawing.Size(49, 18);
            this.rbnSum.TabIndex = 3;
            this.rbnSum.TabStop = false;
            this.rbnSum.Text = "Sum";
            // 
            // rbnDetail
            // 
            this.rbnDetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbnDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbnDetail.Location = new System.Drawing.Point(8, 5);
            this.rbnDetail.Name = "rbnDetail";
            this.rbnDetail.Size = new System.Drawing.Size(56, 18);
            this.rbnDetail.TabIndex = 2;
            this.rbnDetail.Text = "Detail";
            this.rbnDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // frmDebitorCreditorCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 571);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.pickerToDate);
            this.Controls.Add(this.pickerFromDate);
            this.Controls.Add(this.lblToDate);
            this.Controls.Add(this.lblFromDate);
            this.Controls.Add(this.chkWithBeginBalans);
            this.Controls.Add(this.lblNameCustomerTo);
            this.Controls.Add(this.lblNameCustomerFrom);
            this.Controls.Add(this.btnCustomerTo);
            this.Controls.Add(this.btnCustomerFrom);
            this.Controls.Add(this.txtCustomerTo);
            this.Controls.Add(this.txtCustomerFrom);
            this.Controls.Add(this.rlCustomerTo);
            this.Controls.Add(this.rlCustomerFrom);
            this.Controls.Add(this.rbCreditor);
            this.Controls.Add(this.rbDebitor);
            this.Name = "frmDebitorCreditorCard";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debitor Creditor Card";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmDebitorCreditorCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rbDebitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCreditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlCustomerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlCustomerTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomerTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNameCustomerFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNameCustomerTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWithBeginBalans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pickerFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pickerToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbnSum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbnDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadRadioButton rbDebitor;
        private Telerik.WinControls.UI.RadRadioButton rbCreditor;
        private Telerik.WinControls.UI.RadLabel rlCustomerFrom;
        private Telerik.WinControls.UI.RadLabel rlCustomerTo;
        private Telerik.WinControls.UI.RadTextBox txtCustomerFrom;
        private Telerik.WinControls.UI.RadTextBox txtCustomerTo;
        private Telerik.WinControls.UI.RadButton btnCustomerFrom;
        private Telerik.WinControls.UI.RadButton btnCustomerTo;
        private Telerik.WinControls.UI.RadLabel lblNameCustomerFrom;
        private Telerik.WinControls.UI.RadLabel lblNameCustomerTo;
        private Telerik.WinControls.UI.RadCheckBox chkWithBeginBalans;
        private Telerik.WinControls.UI.RadLabel lblFromDate;
        private Telerik.WinControls.UI.RadLabel lblToDate;
        private Telerik.WinControls.UI.RadDateTimePicker pickerFromDate;
        private Telerik.WinControls.UI.RadDateTimePicker pickerToDate;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.RadButton btnDo;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadRadioButton rbnSum;
        private Telerik.WinControls.UI.RadRadioButton rbnDetail;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
    }
}
