namespace GUI
{
    partial class frmReportTeamList
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
            this.reportViewer13 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DeviceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer13
            // 
            this.reportViewer13.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "TeamListPreview";
            reportDataSource1.Value = this.DeviceReportModelBindingSource;
            this.reportViewer13.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer13.LocalReport.ReportEmbeddedResource = "GUI.ReportTeamListPreview.rdlc";
            this.reportViewer13.Location = new System.Drawing.Point(0, 0);
            this.reportViewer13.Name = "reportViewer13";
            this.reportViewer13.Size = new System.Drawing.Size(1255, 438);
            this.reportViewer13.TabIndex = 0;
            // 
            // DeviceReportModelBindingSource
            // 
            this.DeviceReportModelBindingSource.DataSource = typeof(BIS.Model.DeviceReportModel);
            // 
            // frmReportTeamList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 438);
            this.Controls.Add(this.reportViewer13);
            this.Name = "frmReportTeamList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportTeamList";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportTeamList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer13;
        private System.Windows.Forms.BindingSource DeviceReportModelBindingSource;
    }
}
