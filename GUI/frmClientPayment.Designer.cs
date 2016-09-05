namespace GUI
{
    partial class frmClientPayment
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
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem4 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem5 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem6 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition4 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.gridPay = new Telerik.WinControls.UI.RadGridView();
            this.gridContract = new Telerik.WinControls.UI.RadGridView();
            this.txtPurchase = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtCredit = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtTotal = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblCredit = new Telerik.WinControls.UI.RadLabel();
            this.lblPurchase = new Telerik.WinControls.UI.RadLabel();
            this.lblTotal = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPay.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContract.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(1259, 819);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Exit";
            this.btnCancel.ThemeName = "Windows8";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gridPay
            // 
            this.gridPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.gridPay.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridPay.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridPay.ForeColor = System.Drawing.Color.Black;
            this.gridPay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gridPay.Location = new System.Drawing.Point(16, 22);
            // 
            // 
            // 
            this.gridPay.MasterTemplate.AllowAddNewRow = false;
            this.gridPay.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridPay.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            gridViewSummaryItem4.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem4.AggregateExpression = null;
            gridViewSummaryItem4.FormatString = "{0}";
            gridViewSummaryItem4.Name = "debitLine";
            gridViewSummaryItem5.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem5.AggregateExpression = null;
            gridViewSummaryItem5.FormatString = "{0}";
            gridViewSummaryItem5.Name = "creditLine";
            this.gridPay.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem4,
                gridViewSummaryItem5}));
            this.gridPay.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.gridPay.Name = "gridPay";
            this.gridPay.ReadOnly = true;
            this.gridPay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridPay.Size = new System.Drawing.Size(695, 709);
            this.gridPay.TabIndex = 1;
            this.gridPay.Text = "radGridView1";
            this.gridPay.ThemeName = "VisualStudio2012Light";
            this.gridPay.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.MasterTemplate_ViewCellFormatting);
            this.gridPay.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.gridPay_ContextMenuOpening);
            this.gridPay.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridPay_DataBindingComplete);
            // 
            // gridContract
            // 
            this.gridContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridContract.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridContract.Location = new System.Drawing.Point(738, 22);
            // 
            // 
            // 
            this.gridContract.MasterTemplate.AllowAddNewRow = false;
            this.gridContract.MasterTemplate.AllowRowResize = false;
            this.gridContract.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridContract.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            gridViewSummaryItem6.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem6.AggregateExpression = null;
            gridViewSummaryItem6.FormatString = "{0}";
            gridViewSummaryItem6.Name = "priceTotal";
            this.gridContract.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem6}));
            this.gridContract.MasterTemplate.ViewDefinition = tableViewDefinition4;
            this.gridContract.Name = "gridContract";
            this.gridContract.ReadOnly = true;
            this.gridContract.Size = new System.Drawing.Size(680, 709);
            this.gridContract.TabIndex = 2;
            this.gridContract.Text = "radGridView1";
            this.gridContract.ThemeName = "VisualStudio2012Light";
            this.gridContract.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridContract_ViewCellFormatting);
            this.gridContract.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.gridContract_ContextMenuOpening);
            this.gridContract.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridContract_DataBindingComplete);
            // 
            // txtPurchase
            // 
            this.txtPurchase.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPurchase.Location = new System.Drawing.Point(711, 787);
            this.txtPurchase.Mask = "n2";
            this.txtPurchase.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtPurchase.Name = "txtPurchase";
            this.txtPurchase.ReadOnly = true;
            this.txtPurchase.Size = new System.Drawing.Size(142, 19);
            this.txtPurchase.TabIndex = 3;
            this.txtPurchase.TabStop = false;
            this.txtPurchase.Text = "0,00";
            this.txtPurchase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCredit
            // 
            this.txtCredit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCredit.Location = new System.Drawing.Point(711, 763);
            this.txtCredit.Mask = "N2";
            this.txtCredit.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(142, 19);
            this.txtCredit.TabIndex = 4;
            this.txtCredit.TabStop = false;
            this.txtCredit.Text = "0,00";
            this.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCredit_KeyDown);
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTotal.Location = new System.Drawing.Point(711, 819);
            this.txtTotal.Mask = "N2";
            this.txtTotal.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(142, 19);
            this.txtTotal.TabIndex = 5;
            this.txtTotal.TabStop = false;
            this.txtTotal.Text = "0,00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotal_KeyDown);
            // 
            // lblCredit
            // 
            this.lblCredit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCredit.Location = new System.Drawing.Point(549, 765);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(71, 17);
            this.lblCredit.TabIndex = 6;
            this.lblCredit.Text = "Total credit";
            // 
            // lblPurchase
            // 
            this.lblPurchase.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPurchase.Location = new System.Drawing.Point(549, 789);
            this.lblPurchase.Name = "lblPurchase";
            this.lblPurchase.Size = new System.Drawing.Size(90, 17);
            this.lblPurchase.TabIndex = 7;
            this.lblPurchase.Text = "Total purchase";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTotal.Location = new System.Drawing.Point(549, 822);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(102, 17);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Still for invoicing";
            // 
            // frmClientPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 867);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblPurchase);
            this.Controls.Add(this.lblCredit);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtCredit);
            this.Controls.Add(this.txtPurchase);
            this.Controls.Add(this.gridContract);
            this.Controls.Add(this.gridPay);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmClientPayment";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client payment";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmClientPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPay.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContract.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadGridView gridPay;
        private Telerik.WinControls.UI.RadGridView gridContract;
        private Telerik.WinControls.UI.RadMaskedEditBox txtPurchase;
        private Telerik.WinControls.UI.RadMaskedEditBox txtCredit;
        private Telerik.WinControls.UI.RadMaskedEditBox txtTotal;
        private Telerik.WinControls.UI.RadLabel lblCredit;
        private Telerik.WinControls.UI.RadLabel lblPurchase;
        private Telerik.WinControls.UI.RadLabel lblTotal;
    }
}
