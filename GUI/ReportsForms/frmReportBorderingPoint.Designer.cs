namespace GUI
{
    partial class frmReportBorderingPoint
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
            this.reportViewer14 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DeviceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer14
            // 
            this.reportViewer14.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "BorderingPointDataSet";
            reportDataSource1.Value = this.DeviceReportModelBindingSource;
            this.reportViewer14.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer14.LocalReport.ReportEmbeddedResource = "GUI.ReportBorderingPointPreview.rdlc";
            this.reportViewer14.Location = new System.Drawing.Point(0, 0);
            this.reportViewer14.Name = "reportViewer14";
            this.reportViewer14.Size = new System.Drawing.Size(1241, 481);
            this.reportViewer14.TabIndex = 0;
            // 
            // DeviceReportModelBindingSource
            // 
            this.DeviceReportModelBindingSource.DataSource = typeof(BIS.Model.DeviceReportModel);
            // 
            // frmReportBorderingPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 481);
            this.Controls.Add(this.reportViewer14);
            this.Name = "frmReportBorderingPoint";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportBorderingPoint";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportBorderingPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer14;
        private System.Windows.Forms.BindingSource DeviceReportModelBindingSource;
    }
}
