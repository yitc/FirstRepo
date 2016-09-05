namespace GUI
{
    partial class frmDifferenceAmount
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem1 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem2 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.txtAccount = new Telerik.WinControls.UI.RadTextBox();
            this.txtAmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblAction = new Telerik.WinControls.UI.RadLabel();
            this.lblAmount = new Telerik.WinControls.UI.RadLabel();
            this.rbOpenLine = new Telerik.WinControls.UI.RadRadioButton();
            this.rbAccount = new Telerik.WinControls.UI.RadRadioButton();
            this.labelKonto = new Telerik.WinControls.UI.RadLabel();
            this.btnAccount = new Telerik.WinControls.UI.RadButton();
            this.btnOk = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.gridLookup = new Telerik.WinControls.UI.RadGridView();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.radbDiff1 = new Telerik.WinControls.UI.RadRadioButton();
            this.radbDiff2 = new Telerik.WinControls.UI.RadRadioButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbOpenLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelKonto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbDiff1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbDiff2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAccount.Location = new System.Drawing.Point(480, 36);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(73, 20);
            this.txtAccount.TabIndex = 1;
            this.txtAccount.ThemeName = "Windows8";
            this.txtAccount.Visible = false;
            this.txtAccount.TextChanged += new System.EventHandler(this.txtAccount_TextChanged);
            this.txtAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccount_KeyDown);
            this.txtAccount.Leave += new System.EventHandler(this.txtAccount_Leave);
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAmount.Location = new System.Drawing.Point(480, 12);
            this.txtAmount.Mask = "N2";
            this.txtAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(125, 20);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.TabStop = false;
            this.txtAmount.Text = "0,00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.ThemeName = "Windows8";
            // 
            // lblAction
            // 
            this.lblAction.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAction.Location = new System.Drawing.Point(12, 14);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(45, 18);
            this.lblAction.TabIndex = 0;
            this.lblAction.Text = "Action";
            this.lblAction.ThemeName = "Windows8";
            // 
            // lblAmount
            // 
            this.lblAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAmount.Location = new System.Drawing.Point(342, 14);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(120, 18);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "Unbooked amount";
            this.lblAmount.ThemeName = "Windows8";
            // 
            // rbOpenLine
            // 
            this.rbOpenLine.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbOpenLine.Location = new System.Drawing.Point(98, 14);
            this.rbOpenLine.Name = "rbOpenLine";
            this.rbOpenLine.Size = new System.Drawing.Size(121, 18);
            this.rbOpenLine.TabIndex = 3;
            this.rbOpenLine.Text = "Leave line open";
            this.rbOpenLine.ThemeName = "Windows8";
            this.rbOpenLine.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rbOpenLine_ToggleStateChanged);
            // 
            // rbAccount
            // 
            this.rbAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbAccount.Location = new System.Drawing.Point(98, 38);
            this.rbAccount.Name = "rbAccount";
            this.rbAccount.Size = new System.Drawing.Size(142, 18);
            this.rbAccount.TabIndex = 4;
            this.rbAccount.Text = "Booked on account";
            this.rbAccount.ThemeName = "Windows8";
            this.rbAccount.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rbAccount_ToggleStateChanged);
            // 
            // labelKonto
            // 
            this.labelKonto.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelKonto.Location = new System.Drawing.Point(585, 38);
            this.labelKonto.Name = "labelKonto";
            this.labelKonto.Size = new System.Drawing.Size(12, 18);
            this.labelKonto.TabIndex = 1;
            this.labelKonto.Text = "r";
            this.labelKonto.ThemeName = "Windows8";
            this.labelKonto.Visible = false;
            // 
            // btnAccount
            // 
            this.btnAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAccount.Location = new System.Drawing.Point(559, 36);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(20, 20);
            this.btnAccount.TabIndex = 5;
            this.btnAccount.Text = "...";
            this.btnAccount.ThemeName = "Windows8";
            this.btnAccount.Visible = false;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOk.Location = new System.Drawing.Point(229, 406);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.ThemeName = "Windows8";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(541, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "Windows8";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gridLookup
            // 
            this.gridLookup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.gridLookup.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridLookup.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridLookup.ForeColor = System.Drawing.Color.Black;
            this.gridLookup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gridLookup.Location = new System.Drawing.Point(12, 118);
            // 
            // 
            // 
            this.gridLookup.MasterTemplate.AllowAddNewRow = false;
            this.gridLookup.MasterTemplate.AllowDeleteRow = false;
            this.gridLookup.MasterTemplate.AllowRowResize = false;
            this.gridLookup.MasterTemplate.AutoGenerateColumns = false;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "iselected";
            gridViewCheckBoxColumn1.HeaderText = "Select";
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "selected";
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.FieldName = "dtOpenLine";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            gridViewDateTimeColumn1.HeaderText = "Date";
            gridViewDateTimeColumn1.Name = "date";
            gridViewDateTimeColumn1.ReadOnly = true;
            gridViewDateTimeColumn1.Width = 90;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "invoiceOpenLine";
            gridViewTextBoxColumn1.HeaderText = "Invoice";
            gridViewTextBoxColumn1.Name = "invoice";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 90;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "descOpenLine";
            gridViewTextBoxColumn2.HeaderText = "Description";
            gridViewTextBoxColumn2.Name = "description";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 180;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "typeOpenLine";
            gridViewTextBoxColumn3.HeaderText = "Type";
            gridViewTextBoxColumn3.Name = "type";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn3.Width = 30;
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "debitOpenLine";
            gridViewDecimalColumn1.HeaderText = "Debit";
            gridViewDecimalColumn1.Name = "debit";
            gridViewDecimalColumn1.ReadOnly = true;
            gridViewDecimalColumn1.Width = 100;
            gridViewDecimalColumn2.EnableExpressionEditor = false;
            gridViewDecimalColumn2.FieldName = "CreditOpenLine";
            gridViewDecimalColumn2.HeaderText = "Credit";
            gridViewDecimalColumn2.Name = "credit";
            gridViewDecimalColumn2.ReadOnly = true;
            gridViewDecimalColumn2.Width = 100;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "codeCost";
            gridViewTextBoxColumn4.HeaderText = "Cost";
            gridViewTextBoxColumn4.Name = "cost";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 60;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "codeArr";
            gridViewTextBoxColumn5.HeaderText = "Project";
            gridViewTextBoxColumn5.Name = "project";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 60;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "idDebCre";
            gridViewTextBoxColumn6.HeaderText = "Client";
            gridViewTextBoxColumn6.Name = "client";
            gridViewTextBoxColumn6.Width = 80;
            this.gridLookup.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewDecimalColumn1,
            gridViewDecimalColumn2,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.gridLookup.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridLookup.MasterTemplate.EnableFiltering = true;
            this.gridLookup.MasterTemplate.EnableGrouping = false;
            this.gridLookup.MasterTemplate.MultiSelect = true;
            gridViewSummaryItem1.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem1.AggregateExpression = null;
            gridViewSummaryItem1.FormatString = "{0}";
            gridViewSummaryItem1.Name = "debit";
            gridViewSummaryItem2.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem2.AggregateExpression = null;
            gridViewSummaryItem2.FormatString = "{0}";
            gridViewSummaryItem2.Name = "credit";
            this.gridLookup.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem1,
                gridViewSummaryItem2}));
            this.gridLookup.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridLookup.Name = "gridLookup";
            this.gridLookup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridLookup.Size = new System.Drawing.Size(848, 282);
            this.gridLookup.TabIndex = 8;
            this.gridLookup.Text = "Open lines";
            this.gridLookup.ThemeName = "VisualStudio2012Light";
            this.gridLookup.Visible = false;
            this.gridLookup.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridLookup_DataBindingComplete);
            // 
            // radbDiff1
            // 
            this.radbDiff1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radbDiff1.Location = new System.Drawing.Point(750, 68);
            this.radbDiff1.Name = "radbDiff1";
            this.radbDiff1.Size = new System.Drawing.Size(66, 18);
            this.radbDiff1.TabIndex = 5;
            this.radbDiff1.Text = "paydiff";
            this.radbDiff1.ThemeName = "Windows8";
            this.radbDiff1.Visible = false;
            this.radbDiff1.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radbDiff1_ToggleStateChanged);
            // 
            // radbDiff2
            // 
            this.radbDiff2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radbDiff2.Location = new System.Drawing.Point(750, 92);
            this.radbDiff2.Name = "radbDiff2";
            this.radbDiff2.Size = new System.Drawing.Size(54, 18);
            this.radbDiff2.TabIndex = 6;
            this.radbDiff2.Text = "bank";
            this.radbDiff2.ThemeName = "Windows8";
            this.radbDiff2.Visible = false;
            this.radbDiff2.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radbDiff2_ToggleStateChanged);
            // 
            // radButton1
            // 
            this.radButton1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radButton1.Location = new System.Drawing.Point(120, 62);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 9;
            this.radButton1.Visible = false;
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radButton2.Location = new System.Drawing.Point(120, 88);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(110, 24);
            this.radButton2.TabIndex = 10;
            this.radButton2.Visible = false;
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // frmDifferenceAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 448);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radbDiff2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radbDiff1);
            this.Controls.Add(this.gridLookup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnAccount);
            this.Controls.Add(this.labelKonto);
            this.Controls.Add(this.rbAccount);
            this.Controls.Add(this.rbOpenLine);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.lblAction);
            this.Name = "frmDifferenceAmount";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmDifferenceAmount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbOpenLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelKonto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbDiff1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbDiff2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtAccount;
        private Telerik.WinControls.UI.RadMaskedEditBox txtAmount;
        private Telerik.WinControls.UI.RadLabel lblAction;
        private Telerik.WinControls.UI.RadLabel lblAmount;
        private Telerik.WinControls.UI.RadRadioButton rbOpenLine;
        private Telerik.WinControls.UI.RadRadioButton rbAccount;
        private Telerik.WinControls.UI.RadLabel labelKonto;
        private Telerik.WinControls.UI.RadButton btnAccount;
        private Telerik.WinControls.UI.RadButton btnOk;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadGridView gridLookup;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadRadioButton radbDiff1;
        private Telerik.WinControls.UI.RadRadioButton radbDiff2;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
    }
}
