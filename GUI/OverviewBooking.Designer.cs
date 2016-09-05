namespace GUI
{
    partial class OverviewBooking
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            this.dtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDateTo = new Telerik.WinControls.UI.RadLabel();
            this.dtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDateFrom = new Telerik.WinControls.UI.RadLabel();
            this.rgvOverviewBooking = new Telerik.WinControls.UI.RadGridView();
            this.ddlStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.lblOverviewBooking = new Telerik.WinControls.UI.RadLabel();
            this.txtArrangement = new System.Windows.Forms.TextBox();
            this.btDo = new Telerik.WinControls.UI.RadButton();
            this.lblStatusOB = new Telerik.WinControls.UI.RadLabel();
            this.radMenuSaveTasks = new Telerik.WinControls.UI.RadMenu();
            this.rmiOverviewBooking = new Telerik.WinControls.UI.RadMenuItem();
            this.panelLabels = new System.Windows.Forms.Panel();
            this.panelTravelPapers = new System.Windows.Forms.Panel();
            this.rbCountry = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnExcell = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOverviewBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOverviewBooking.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOverviewBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusOB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuSaveTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // dtTo
            // 
            this.dtTo.AutoSize = false;
            this.dtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(363, 11);
            this.dtTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(100, 20);
            this.dtTo.TabIndex = 65;
            this.dtTo.TabStop = false;
            this.dtTo.Text = "10-10-2016";
            this.dtTo.ThemeName = "Windows8";
            this.dtTo.Value = new System.DateTime(2016, 10, 10, 0, 0, 0, 0);
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDateTo.Location = new System.Drawing.Point(269, 10);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(52, 18);
            this.lblDateTo.TabIndex = 66;
            this.lblDateTo.Text = "Date to";
            // 
            // dtFrom
            // 
            this.dtFrom.AutoSize = false;
            this.dtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(110, 11);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(100, 20);
            this.dtFrom.TabIndex = 64;
            this.dtFrom.TabStop = false;
            this.dtFrom.Text = "1-1-2016";
            this.dtFrom.ThemeName = "Windows8";
            this.dtFrom.Value = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDateFrom.Location = new System.Drawing.Point(22, 10);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDateFrom.TabIndex = 63;
            this.lblDateFrom.Text = "Date from";
            this.lblDateFrom.ThemeName = "Windows8";
            // 
            // rgvOverviewBooking
            // 
            this.rgvOverviewBooking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rgvOverviewBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.rgvOverviewBooking.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvOverviewBooking.Font = new System.Drawing.Font("Verdana", 9F);
            this.rgvOverviewBooking.ForeColor = System.Drawing.Color.Black;
            this.rgvOverviewBooking.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvOverviewBooking.Location = new System.Drawing.Point(12, 186);
            // 
            // 
            // 
            this.rgvOverviewBooking.MasterTemplate.AllowAddNewRow = false;
            this.rgvOverviewBooking.MasterTemplate.AllowDeleteRow = false;
            this.rgvOverviewBooking.MasterTemplate.AllowRowResize = false;
            this.rgvOverviewBooking.MasterTemplate.AllowSearchRow = true;
            this.rgvOverviewBooking.MasterTemplate.EnableFiltering = true;
            this.rgvOverviewBooking.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.rgvOverviewBooking.Name = "rgvOverviewBooking";
            this.rgvOverviewBooking.ReadOnly = true;
            this.rgvOverviewBooking.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvOverviewBooking.Size = new System.Drawing.Size(1091, 276);
            this.rgvOverviewBooking.TabIndex = 119;
            this.rgvOverviewBooking.ThemeName = "VisualStudio2012Light";
            this.rgvOverviewBooking.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvOverviewBooking_DataBindingComplete);
            // 
            // ddlStatus
            // 
            this.ddlStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStatus.Location = new System.Drawing.Point(110, 159);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(125, 20);
            this.ddlStatus.TabIndex = 120;
            this.ddlStatus.ThemeName = "Windows8";
            // 
            // lblOverviewBooking
            // 
            this.lblOverviewBooking.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblOverviewBooking.Location = new System.Drawing.Point(269, 161);
            this.lblOverviewBooking.Name = "lblOverviewBooking";
            this.lblOverviewBooking.Size = new System.Drawing.Size(88, 18);
            this.lblOverviewBooking.TabIndex = 121;
            this.lblOverviewBooking.Text = "Arrangement";
            // 
            // txtArrangement
            // 
            this.txtArrangement.Location = new System.Drawing.Point(363, 159);
            this.txtArrangement.Name = "txtArrangement";
            this.txtArrangement.Size = new System.Drawing.Size(100, 20);
            this.txtArrangement.TabIndex = 122;
            // 
            // btDo
            // 
            this.btDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDo.Font = new System.Drawing.Font("Verdana", 9F);
            this.btDo.Location = new System.Drawing.Point(993, 155);
            this.btDo.Name = "btDo";
            this.btDo.Size = new System.Drawing.Size(110, 24);
            this.btDo.TabIndex = 123;
            this.btDo.Text = "Do";
            this.btDo.ThemeName = "Windows8";
            this.btDo.Click += new System.EventHandler(this.btDo_Click);
            // 
            // lblStatusOB
            // 
            this.lblStatusOB.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblStatusOB.Location = new System.Drawing.Point(22, 163);
            this.lblStatusOB.Name = "lblStatusOB";
            this.lblStatusOB.Size = new System.Drawing.Size(46, 18);
            this.lblStatusOB.TabIndex = 124;
            this.lblStatusOB.Text = "Status";
            // 
            // radMenuSaveTasks
            // 
            this.radMenuSaveTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radMenuSaveTasks.Dock = System.Windows.Forms.DockStyle.None;
            this.radMenuSaveTasks.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rmiOverviewBooking});
            this.radMenuSaveTasks.Location = new System.Drawing.Point(10, 465);
            this.radMenuSaveTasks.Name = "radMenuSaveTasks";
            this.radMenuSaveTasks.Size = new System.Drawing.Size(1093, 20);
            this.radMenuSaveTasks.TabIndex = 125;
            this.radMenuSaveTasks.Text = "radMenu2";
            this.radMenuSaveTasks.ThemeName = "Windows8";
            // 
            // rmiOverviewBooking
            // 
            this.rmiOverviewBooking.Font = new System.Drawing.Font("Verdana", 9F);
            this.rmiOverviewBooking.Name = "rmiOverviewBooking";
            this.rmiOverviewBooking.Text = "Save Layout";
            this.rmiOverviewBooking.Click += new System.EventHandler(this.rmiOverviewBooking_Click);
            // 
            // panelLabels
            // 
            this.panelLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLabels.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLabels.Location = new System.Drawing.Point(647, 10);
            this.panelLabels.Margin = new System.Windows.Forms.Padding(2);
            this.panelLabels.Name = "panelLabels";
            this.panelLabels.Size = new System.Drawing.Size(152, 171);
            this.panelLabels.TabIndex = 126;
            // 
            // panelTravelPapers
            // 
            this.panelTravelPapers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTravelPapers.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTravelPapers.Location = new System.Drawing.Point(823, 10);
            this.panelTravelPapers.Margin = new System.Windows.Forms.Padding(2);
            this.panelTravelPapers.Name = "panelTravelPapers";
            this.panelTravelPapers.Size = new System.Drawing.Size(152, 171);
            this.panelTravelPapers.TabIndex = 127;
            // 
            // rbCountry
            // 
            this.rbCountry.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCountry.Location = new System.Drawing.Point(469, 159);
            this.rbCountry.Name = "rbCountry";
            this.rbCountry.Size = new System.Drawing.Size(20, 20);
            this.rbCountry.TabIndex = 128;
            this.rbCountry.Text = "...";
            this.rbCountry.ThemeName = "Windows8";
            this.rbCountry.Click += new System.EventHandler(this.rbCountry_Click);
            // 
            // radButton1
            // 
            this.radButton1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.radButton1.ForeColor = System.Drawing.Color.Red;
            this.radButton1.Location = new System.Drawing.Point(495, 159);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(20, 20);
            this.radButton1.TabIndex = 129;
            this.radButton1.Text = "X";
            this.radButton1.ThemeName = "Windows8";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnPrint.Location = new System.Drawing.Point(993, 125);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 24);
            this.btnPrint.TabIndex = 124;
            this.btnPrint.Text = "Print";
            this.btnPrint.ThemeName = "Windows8";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExcell
            // 
            this.btnExcell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcell.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcell.Location = new System.Drawing.Point(993, 95);
            this.btnExcell.Name = "btnExcell";
            this.btnExcell.Size = new System.Drawing.Size(110, 24);
            this.btnExcell.TabIndex = 130;
            this.btnExcell.Text = "Export to excell";
            this.btnExcell.ThemeName = "Windows8";
            this.btnExcell.Visible = false;
            this.btnExcell.Click += new System.EventHandler(this.btnExcell_Click);
            // 
            // OverviewBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 500);
            this.Controls.Add(this.btnExcell);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.rbCountry);
            this.Controls.Add(this.panelTravelPapers);
            this.Controls.Add(this.panelLabels);
            this.Controls.Add(this.radMenuSaveTasks);
            this.Controls.Add(this.lblStatusOB);
            this.Controls.Add(this.btDo);
            this.Controls.Add(this.txtArrangement);
            this.Controls.Add(this.lblOverviewBooking);
            this.Controls.Add(this.ddlStatus);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.rgvOverviewBooking);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OverviewBooking";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "OverviewBooking";
            this.ThemeName = "Windows8";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OverviewBooking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOverviewBooking.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOverviewBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOverviewBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusOB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuSaveTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDateTimePicker dtTo;
        private Telerik.WinControls.UI.RadLabel lblDateTo;
        private Telerik.WinControls.UI.RadDateTimePicker dtFrom;
        private Telerik.WinControls.UI.RadLabel lblDateFrom;
        private Telerik.WinControls.UI.RadGridView rgvOverviewBooking;
        private Telerik.WinControls.UI.RadDropDownList ddlStatus;
        private Telerik.WinControls.UI.RadLabel lblOverviewBooking;
        private System.Windows.Forms.TextBox txtArrangement;
        private Telerik.WinControls.UI.RadButton btDo;
        private Telerik.WinControls.UI.RadLabel lblStatusOB;
        private Telerik.WinControls.UI.RadMenu radMenuSaveTasks;
        private Telerik.WinControls.UI.RadMenuItem rmiOverviewBooking;
        private System.Windows.Forms.Panel panelLabels;
        private System.Windows.Forms.Panel panelTravelPapers;
        private Telerik.WinControls.UI.RadButton rbCountry;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadButton btnExcell;
    }
}
