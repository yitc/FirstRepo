namespace GUI
{
    partial class frmReportOverviewBooking
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
            this.reportViewer8 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.OverviewBookingReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.OverviewBookingReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer8
            // 
            this.reportViewer8.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "OverBookingDataSet";
            reportDataSource1.Value = this.OverviewBookingReportModelBindingSource;
            this.reportViewer8.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer8.LocalReport.ReportEmbeddedResource = "GUI.ReportOverviwBookingPreview.rdlc";
            this.reportViewer8.Location = new System.Drawing.Point(0, 0);
            this.reportViewer8.Name = "reportViewer8";
            this.reportViewer8.Size = new System.Drawing.Size(1209, 399);
            this.reportViewer8.TabIndex = 0;
            // 
            // OverviewBookingReportModelBindingSource
            // 
            this.OverviewBookingReportModelBindingSource.DataSource = typeof(BIS.Model.OverviewBookingReportModel);
            // 
            // frmReportOverviewBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 399);
            this.Controls.Add(this.reportViewer8);
            this.Name = "frmReportOverviewBooking";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportOverviewBooking";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportOverviewBooking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OverviewBookingReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer8;
        private System.Windows.Forms.BindingSource OverviewBookingReportModelBindingSource;
    }
}
