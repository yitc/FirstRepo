namespace GUI
{
    partial class frmReportNumberBookings
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer10 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PurchaseReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer10
            // 
            this.reportViewer10.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "NumberBookingsDataSet";
            reportDataSource1.Value = this.PurchaseReportModelBindingSource;
            this.reportViewer10.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer10.LocalReport.ReportEmbeddedResource = "GUI.ReportNumberBookingsPreview.rdlc";
            this.reportViewer10.Location = new System.Drawing.Point(0, 0);
            this.reportViewer10.Name = "reportViewer10";
            this.reportViewer10.Size = new System.Drawing.Size(1254, 515);
            this.reportViewer10.TabIndex = 0;
            // 
            // PurchaseReportModelBindingSource
            // 
            this.PurchaseReportModelBindingSource.DataSource = typeof(BIS.Model.PurchaseReportModel);
            // 
            // frmReportNumberBookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 515);
            this.Controls.Add(this.reportViewer10);
            this.Name = "frmReportNumberBookings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportNumberBookings";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportNumberBookings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer10;
        private System.Windows.Forms.BindingSource PurchaseReportModelBindingSource;
    }
}
