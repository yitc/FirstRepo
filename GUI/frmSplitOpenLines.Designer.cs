namespace GUI
{
    partial class frmSplitOpenLines
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
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor2 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem1 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem2 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.lblAmount = new Telerik.WinControls.UI.RadLabel();
            this.txtAmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.gridSplit = new Telerik.WinControls.UI.RadGridView();
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplit.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAmount
            // 
            this.lblAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAmount.Location = new System.Drawing.Point(23, 34);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(51, 17);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAmount.Location = new System.Drawing.Point(302, 32);
            this.txtAmount.Mask = "n2";
            this.txtAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(125, 19);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TabStop = false;
            this.txtAmount.Text = "0,00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gridSplit
            // 
            this.gridSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.gridSplit.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridSplit.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.gridSplit.ForeColor = System.Drawing.Color.Black;
            this.gridSplit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gridSplit.Location = new System.Drawing.Point(23, 69);
            // 
            // 
            // 
            this.gridSplit.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            this.gridSplit.MasterTemplate.AllowColumnChooser = false;
            gridViewDateTimeColumn1.AllowGroup = false;
            gridViewDateTimeColumn1.AllowHide = false;
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.FieldName = "dtDate";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            gridViewDateTimeColumn1.HeaderText = "Date";
            gridViewDateTimeColumn1.Name = "date";
            gridViewDateTimeColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewDateTimeColumn1.Width = 111;
            gridViewDecimalColumn1.AllowFiltering = false;
            gridViewDecimalColumn1.AllowGroup = false;
            gridViewDecimalColumn1.AllowSort = false;
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "percentPay";
            gridViewDecimalColumn1.HeaderText = "Percent";
            gridViewDecimalColumn1.Name = "percent";
            gridViewDecimalColumn1.Width = 80;
            gridViewDecimalColumn2.AllowGroup = false;
            gridViewDecimalColumn2.AllowHide = false;
            gridViewDecimalColumn2.AllowSort = false;
            gridViewDecimalColumn2.EnableExpressionEditor = false;
            gridViewDecimalColumn2.FieldName = "amount";
            gridViewDecimalColumn2.HeaderText = "Amount";
            gridViewDecimalColumn2.Name = "amount";
            gridViewDecimalColumn2.Step = new decimal(new int[] {
            0,
            0,
            0,
            0});
            gridViewDecimalColumn2.ThousandsSeparator = true;
            gridViewDecimalColumn2.VisibleInColumnChooser = false;
            gridViewDecimalColumn2.Width = 160;
            this.gridSplit.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDateTimeColumn1,
            gridViewDecimalColumn1,
            gridViewDecimalColumn2});
            sortDescriptor1.PropertyName = "date";
            sortDescriptor2.PropertyName = "column1";
            this.gridSplit.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1,
            sortDescriptor2});
            gridViewSummaryItem1.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem1.AggregateExpression = null;
            gridViewSummaryItem1.FormatString = "{0:N2}";
            gridViewSummaryItem1.Name = "percent";
            gridViewSummaryItem2.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem2.AggregateExpression = null;
            gridViewSummaryItem2.FormatString = "{0:N2}";
            gridViewSummaryItem2.Name = "amount";
            this.gridSplit.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem1,
                gridViewSummaryItem2}));
            this.gridSplit.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridSplit.Name = "gridSplit";
            this.gridSplit.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell;
            this.gridSplit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridSplit.Size = new System.Drawing.Size(404, 216);
            this.gridSplit.TabIndex = 1;
            this.gridSplit.ThemeName = "VisualStudio2012Light";
            this.gridSplit.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridSplit_ViewCellFormatting);
            this.gridSplit.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridSplit_CellBeginEdit);
            this.gridSplit.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridSplit_CellEditorInitialized);
            this.gridSplit.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridSplit_CellEndEdit);
            this.gridSplit.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.gridSplit_ContextMenuOpening);
            this.gridSplit.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridSplit_DataBindingComplete);
            this.gridSplit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridSplit_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExit.Location = new System.Drawing.Point(317, 304);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 24);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.ThemeName = "Windows8";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSave.Location = new System.Drawing.Point(172, 304);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 24);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "Windows8";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSplitOpenLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 344);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gridSplit);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Name = "frmSplitOpenLines";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment terms";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmSplitOpenLines_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplit.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblAmount;
        private Telerik.WinControls.UI.RadMaskedEditBox txtAmount;
        private Telerik.WinControls.UI.RadGridView gridSplit;
        private Telerik.WinControls.UI.RadButton btnExit;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}
