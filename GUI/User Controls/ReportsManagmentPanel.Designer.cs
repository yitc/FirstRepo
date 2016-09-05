namespace GUI.User_Controls
{
    partial class ReportsManagmentPanel
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
            this.radScrollablePanel1 = new Telerik.WinControls.UI.RadScrollablePanel();
            this.radPanorama1 = new Telerik.WinControls.UI.RadPanorama();
            this.tgNoName = new Telerik.WinControls.UI.TileGroupElement();
            this.rtPurchase = new Telerik.WinControls.UI.RadTileElement();
            this.rtSales = new Telerik.WinControls.UI.RadTileElement();
            this.rtVoluntary = new Telerik.WinControls.UI.RadTileElement();
            this.tgManagement = new Telerik.WinControls.UI.TileGroupElement();
            this.rtDepartureList = new Telerik.WinControls.UI.RadTileElement();
            this.rtDepartureList2 = new Telerik.WinControls.UI.RadTileElement();
            this.rtOverviewBooking = new Telerik.WinControls.UI.RadTileElement();
            this.rtDepartureList3 = new Telerik.WinControls.UI.RadTileElement();
            this.rtDepartureList4 = new Telerik.WinControls.UI.RadTileElement();
            this.tgAccounting = new Telerik.WinControls.UI.TileGroupElement();
            this.rtJournal = new Telerik.WinControls.UI.RadTileElement();
            this.rtLedger = new Telerik.WinControls.UI.RadTileElement();
            this.rtMe940 = new Telerik.WinControls.UI.RadTileElement();
            this.rtInvoiceSel = new Telerik.WinControls.UI.RadTileElement();
            this.rtClosing = new Telerik.WinControls.UI.RadTileElement();
            this.rtCreditPay = new Telerik.WinControls.UI.RadTileElement();
            this.rtOpenLInes = new Telerik.WinControls.UI.RadLiveTileElement();
            this.rtApproving = new Telerik.WinControls.UI.RadTileElement();
            this.rtBankPay = new Telerik.WinControls.UI.RadTileElement();
            this.rtLedgerCard = new Telerik.WinControls.UI.RadTileElement();
            this.rtInsuranceVolunteers = new Telerik.WinControls.UI.RadTileElement();
            this.radTileElement1 = new Telerik.WinControls.UI.RadTileElement();
            this.rtManagementReports = new Telerik.WinControls.UI.RadTileElement();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.rtDebitCreditCard = new Telerik.WinControls.UI.RadTileElement();
            ((System.ComponentModel.ISupportInitialize)(this.radScrollablePanel1)).BeginInit();
            this.radScrollablePanel1.PanelContainer.SuspendLayout();
            this.radScrollablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).BeginInit();
            this.SuspendLayout();
            // 
            // radScrollablePanel1
            // 
            this.radScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radScrollablePanel1.Location = new System.Drawing.Point(0, 0);
            this.radScrollablePanel1.Name = "radScrollablePanel1";
            // 
            // radScrollablePanel1.PanelContainer
            // 
            this.radScrollablePanel1.PanelContainer.Controls.Add(this.radPanorama1);
            this.radScrollablePanel1.PanelContainer.Size = new System.Drawing.Size(1018, 646);
            this.radScrollablePanel1.Size = new System.Drawing.Size(1020, 648);
            this.radScrollablePanel1.TabIndex = 0;
            this.radScrollablePanel1.Text = "radScrollablePanel1";
            // 
            // radPanorama1
            // 
            this.radPanorama1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanorama1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanorama1.Groups.AddRange(new Telerik.WinControls.RadItem[] {
            this.tgNoName,
            this.tgManagement,
            this.tgAccounting});
            this.radPanorama1.Location = new System.Drawing.Point(0, 0);
            this.radPanorama1.Name = "radPanorama1";
            this.radPanorama1.RowsCount = 5;
            this.radPanorama1.ShowGroups = true;
            this.radPanorama1.Size = new System.Drawing.Size(1018, 646);
            this.radPanorama1.TabIndex = 9;
            this.radPanorama1.Text = "radPanorama1";
            this.radPanorama1.ThemeName = "VisualStudio2012Light";
            ((Telerik.WinControls.UI.RadPanoramaElement)(this.radPanorama1.GetChildAt(0))).BackColor = System.Drawing.SystemColors.Control;
            // 
            // tgNoName
            // 
            this.tgNoName.AccessibleDescription = "Management reports";
            this.tgNoName.AccessibleName = "Management reports";
            this.tgNoName.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgNoName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(165)))), ((int)(((byte)(205)))));
            this.tgNoName.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rtPurchase,
            this.rtSales,
            this.rtVoluntary});
            this.tgNoName.Name = "tgNoName";
            this.tgNoName.RowsCount = 5;
            this.tgNoName.StretchVertically = true;
            this.tgNoName.Text = "Report";
            // 
            // rtPurchase
            // 
            this.rtPurchase.FlipText = false;
            this.rtPurchase.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtPurchase.Image = global::GUI.Properties.Resources.report;
            this.rtPurchase.Name = "rtPurchase";
            this.rtPurchase.Text = "Purchase";
            this.rtPurchase.Click += new System.EventHandler(this.rtPurchase_Click);
            // 
            // rtSales
            // 
            this.rtSales.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtSales.Image = global::GUI.Properties.Resources.report;
            this.rtSales.Name = "rtSales";
            this.rtSales.Row = 1;
            this.rtSales.Text = "Sales";
            this.rtSales.Click += new System.EventHandler(this.rtSales_Click);
            // 
            // rtVoluntary
            // 
            this.rtVoluntary.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtVoluntary.Image = global::GUI.Properties.Resources.report;
            this.rtVoluntary.Name = "rtVoluntary";
            this.rtVoluntary.Row = 2;
            this.rtVoluntary.Text = "Voluntary";
            this.rtVoluntary.TextWrap = false;
            this.rtVoluntary.Click += new System.EventHandler(this.rtVoluntary_Click);
            // 
            // tgManagement
            // 
            this.tgManagement.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgManagement.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rtDepartureList,
            this.rtDepartureList2,
            this.rtOverviewBooking,
            this.rtDepartureList3,
            this.rtDepartureList4});
            this.tgManagement.Name = "tgManagement";
            this.tgManagement.RowsCount = 5;
            this.tgManagement.Text = "Management reports";
            // 
            // rtDepartureList
            // 
            this.rtDepartureList.AccessibleDescription = "Departure list";
            this.rtDepartureList.AccessibleName = "Departure list";
            this.rtDepartureList.ColSpan = 2;
            this.rtDepartureList.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.rtDepartureList.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtDepartureList.Name = "rtDepartureList";
            this.rtDepartureList.Text = "Departure list1";
            this.rtDepartureList.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.rtDepartureList.Click += new System.EventHandler(this.rtDepartureList_Click);
            // 
            // rtDepartureList2
            // 
            this.rtDepartureList2.ColSpan = 2;
            this.rtDepartureList2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtDepartureList2.Name = "rtDepartureList2";
            this.rtDepartureList2.Row = 1;
            this.rtDepartureList2.Text = "Departure list2";
            this.rtDepartureList2.Click += new System.EventHandler(this.rtDepartureList2_Click);
            // 
            // rtOverviewBooking
            // 
            this.rtOverviewBooking.Column = 2;
            this.rtOverviewBooking.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtOverviewBooking.Name = "rtOverviewBooking";
            this.rtOverviewBooking.RowSpan = 2;
            this.rtOverviewBooking.Text = "Overview booking";
            this.rtOverviewBooking.TextWrap = true;
            this.rtOverviewBooking.Click += new System.EventHandler(this.rtOverviewBooking_Click);
            // 
            // rtDepartureList3
            // 
            this.rtDepartureList3.AccessibleDescription = "Departure list2";
            this.rtDepartureList3.AccessibleName = "Departure list2";
            this.rtDepartureList3.ColSpan = 2;
            this.rtDepartureList3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.rtDepartureList3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtDepartureList3.Name = "rtDepartureList3";
            this.rtDepartureList3.Row = 2;
            this.rtDepartureList3.Text = "Departure list3";
            this.rtDepartureList3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.rtDepartureList3.Click += new System.EventHandler(this.rtDepartureList3_Click);
            // 
            // rtDepartureList4
            // 
            this.rtDepartureList4.ColSpan = 2;
            this.rtDepartureList4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtDepartureList4.Name = "rtDepartureList4";
            this.rtDepartureList4.Row = 3;
            this.rtDepartureList4.Text = "Departure list4";
            this.rtDepartureList4.Click += new System.EventHandler(this.rtDepartureList4_Click);
            // 
            // tgAccounting
            // 
            this.tgAccounting.BackColor = System.Drawing.Color.White;
            this.tgAccounting.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgAccounting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(165)))), ((int)(((byte)(205)))));
            this.tgAccounting.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rtJournal,
            this.rtLedger,
            this.rtMe940,
            this.rtInvoiceSel,
            this.rtClosing,
            this.rtCreditPay,
            this.rtOpenLInes,
            this.rtApproving,
            this.rtBankPay,
            this.rtLedgerCard,
            this.rtInsuranceVolunteers,
            this.rtDebitCreditCard});
            this.tgAccounting.Name = "tgAccounting";
            this.tgAccounting.RowsCount = 5;
            this.tgAccounting.Text = "Account reports";
            this.tgAccounting.Click += new System.EventHandler(this.tgAccounting_Click);
            // 
            // rtJournal
            // 
            this.rtJournal.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtJournal.Name = "rtJournal";
            this.rtJournal.Text = "Journal";
            this.rtJournal.Click += new System.EventHandler(this.rtJournal_Click);
            // 
            // rtLedger
            // 
            this.rtLedger.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtLedger.Name = "rtLedger";
            this.rtLedger.Row = 1;
            this.rtLedger.Text = "Ledger";
            this.rtLedger.Click += new System.EventHandler(this.rtLedger_Click);
            // 
            // rtMe940
            // 
            this.rtMe940.Name = "rtMe940";
            this.rtMe940.Row = 2;
            this.rtMe940.Text = "Import ME 940";
            this.rtMe940.TextWrap = true;
            this.rtMe940.Click += new System.EventHandler(this.rtMe940_Click);
            // 
            // rtInvoiceSel
            // 
            this.rtInvoiceSel.Name = "rtInvoiceSel";
            this.rtInvoiceSel.Row = 3;
            this.rtInvoiceSel.Text = "Invoice selection";
            this.rtInvoiceSel.TextWrap = true;
            this.rtInvoiceSel.Click += new System.EventHandler(this.rtInvoiceSel_Click);
            // 
            // rtClosing
            // 
            this.rtClosing.Name = "rtClosing";
            this.rtClosing.Row = 4;
            this.rtClosing.Text = "Closing lines";
            this.rtClosing.TextWrap = true;
            this.rtClosing.Click += new System.EventHandler(this.rtClosing_Click);
            // 
            // rtCreditPay
            // 
            this.rtCreditPay.Column = 1;
            this.rtCreditPay.Name = "rtCreditPay";
            this.rtCreditPay.Text = "Credit pay";
            this.rtCreditPay.TextWrap = true;
            this.rtCreditPay.Click += new System.EventHandler(this.rtCreditPay_Click);
            // 
            // rtOpenLInes
            // 
            this.rtOpenLInes.Column = 1;
            this.rtOpenLInes.Name = "rtOpenLInes";
            this.rtOpenLInes.Row = 1;
            this.rtOpenLInes.Text = "Open Lines";
            this.rtOpenLInes.TextWrap = true;
            this.rtOpenLInes.Click += new System.EventHandler(this.rtOpenLInes_Click);
            // 
            // rtApproving
            // 
            this.rtApproving.Column = 1;
            this.rtApproving.Name = "rtApproving";
            this.rtApproving.Row = 2;
            this.rtApproving.Text = "Approve payment by task";
            this.rtApproving.TextWrap = true;
            this.rtApproving.Click += new System.EventHandler(this.rtApproving_Click);
            // 
            // rtBankPay
            // 
            this.rtBankPay.Column = 1;
            this.rtBankPay.Name = "rtBankPay";
            this.rtBankPay.Row = 3;
            this.rtBankPay.Text = "Bank Credit Pay";
            this.rtBankPay.TextWrap = true;
            this.rtBankPay.Click += new System.EventHandler(this.rtBankPay_Click);
            // 
            // rtLedgerCard
            // 
            this.rtLedgerCard.Column = 1;
            this.rtLedgerCard.Name = "rtLedgerCard";
            this.rtLedgerCard.Row = 4;
            this.rtLedgerCard.Text = "Ledger card";
            this.rtLedgerCard.TextWrap = true;
            this.rtLedgerCard.Click += new System.EventHandler(this.rtLedgerCard_Click);
            // 
            // rtInsuranceVolunteers
            // 
            this.rtInsuranceVolunteers.Column = 2;
            this.rtInsuranceVolunteers.Font = new System.Drawing.Font("Verdana", 9.5F);
            this.rtInsuranceVolunteers.Name = "rtInsuranceVolunteers";
            this.rtInsuranceVolunteers.Text = "Insurance volunteers";
            this.rtInsuranceVolunteers.TextWrap = true;
            this.rtInsuranceVolunteers.Click += new System.EventHandler(this.rtInsuranceVolunteers_Click);
            // 
            // radTileElement1
            // 
            this.radTileElement1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(137)))));
            this.radTileElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radTileElement1.Name = "radTileElement1";
            this.radTileElement1.Text = "";
            this.radTileElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // rtManagementReports
            // 
            this.rtManagementReports.Name = "rtManagementReports";
            // 
            // rtDebitCreditCard
            // 
            this.rtDebitCreditCard.Column = 2;
            this.rtDebitCreditCard.Name = "rtDebitCreditCard";
            this.rtDebitCreditCard.Row = 1;
            this.rtDebitCreditCard.Text = "Debit credit card";
            this.rtDebitCreditCard.TextWrap = true;
            this.rtDebitCreditCard.Click += new System.EventHandler(this.rtDebitCreditCard_Click);
            // 
            // ReportsManagmentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radScrollablePanel1);
            this.Name = "ReportsManagmentPanel";
            this.Size = new System.Drawing.Size(1020, 648);
            this.radScrollablePanel1.PanelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radScrollablePanel1)).EndInit();
            this.radScrollablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadScrollablePanel radScrollablePanel1;
        private Telerik.WinControls.UI.RadPanorama radPanorama1;
        private Telerik.WinControls.UI.TileGroupElement tgNoName;
        private Telerik.WinControls.UI.RadTileElement rtPurchase;
        private Telerik.WinControls.UI.RadTileElement rtSales;
        private Telerik.WinControls.UI.TileGroupElement tgAccounting;
        private Telerik.WinControls.UI.RadTileElement rtJournal;
        private Telerik.WinControls.UI.RadTileElement radTileElement1;
        private Telerik.WinControls.UI.RadTileElement rtVoluntary;
        private Telerik.WinControls.UI.TileGroupElement tgManagement;
        private Telerik.WinControls.UI.RadTileElement rtDepartureList;
        private Telerik.WinControls.UI.RadTileElement rtDepartureList2;
        private Telerik.WinControls.UI.RadTileElement rtOverviewBooking;
        private Telerik.WinControls.UI.RadTileElement rtDepartureList3;
        private Telerik.WinControls.UI.RadTileElement rtDepartureList4;
        private Telerik.WinControls.UI.RadTileElement rtManagementReports;
        private Telerik.WinControls.UI.RadTileElement rtLedger;
        private Telerik.WinControls.UI.RadTileElement rtMe940;
        private Telerik.WinControls.UI.RadTileElement rtInvoiceSel;
        private Telerik.WinControls.UI.RadTileElement rtClosing;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadTileElement rtCreditPay;
        private Telerik.WinControls.UI.RadLiveTileElement rtOpenLInes;
        private Telerik.WinControls.UI.RadTileElement rtApproving;
        private Telerik.WinControls.UI.RadTileElement rtBankPay;
        private Telerik.WinControls.UI.RadTileElement rtLedgerCard;
        private Telerik.WinControls.UI.RadTileElement rtInsuranceVolunteers;
        private Telerik.WinControls.UI.RadTileElement rtDebitCreditCard;
    }
}
