namespace GUI
{
    partial class frmReportCencelledTrips
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
            this.reportViewer19 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PurchaseReportModel2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseReportModel2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer19
            // 
            this.reportViewer19.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "CancelledTripsDataSet";
            reportDataSource1.Value = this.PurchaseReportModel2BindingSource;
            this.reportViewer19.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer19.LocalReport.ReportEmbeddedResource = "GUI.Reports.CancelledTripsPreview.rdlc";
            this.reportViewer19.Location = new System.Drawing.Point(0, 0);
            this.reportViewer19.Name = "reportViewer19";
            this.reportViewer19.Size = new System.Drawing.Size(1214, 479);
            this.reportViewer19.TabIndex = 0;
            // 
            // PurchaseReportModel2BindingSource
            // 
            this.PurchaseReportModel2BindingSource.DataSource = typeof(BIS.Model.PurchaseReportModel2);
            // 
            // frmReportCencelledTrips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 479);
            this.Controls.Add(this.reportViewer19);
            this.Name = "frmReportCencelledTrips";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportCencelledTrips";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportCencelledTrips_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PurchaseReportModel2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer19;
        private System.Windows.Forms.BindingSource PurchaseReportModel2BindingSource;
    }
}
