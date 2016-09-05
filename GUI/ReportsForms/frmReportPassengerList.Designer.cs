namespace GUI
{
    partial class frmReportPassengerList
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
            this.reportViewer7 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DeviceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer7
            // 
            this.reportViewer7.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "PassengerListDataSet";
            reportDataSource1.Value = this.DeviceReportModelBindingSource;
            this.reportViewer7.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer7.LocalReport.ReportEmbeddedResource = "GUI.ReportPassengerListPreview.rdlc";
            this.reportViewer7.Location = new System.Drawing.Point(0, 0);
            this.reportViewer7.Name = "reportViewer7";
            this.reportViewer7.Size = new System.Drawing.Size(1261, 514);
            this.reportViewer7.TabIndex = 0;
            // 
            // DeviceReportModelBindingSource
            // 
            this.DeviceReportModelBindingSource.DataSource = typeof(BIS.Model.DeviceReportModel);
            // 
            // frmReportPassengerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 514);
            this.Controls.Add(this.reportViewer7);
            this.Name = "frmReportPassengerList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportPassengerList";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportPassengerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer7;
        private System.Windows.Forms.BindingSource DeviceReportModelBindingSource;
    }
}
