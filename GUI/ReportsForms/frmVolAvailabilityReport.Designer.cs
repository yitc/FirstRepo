namespace GUI
{
    partial class frmVolAvailabilityReport
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
            this.VolAvailabilityModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AvailabilitySkillsReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.VolAvailabilityModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // VolAvailabilityModelBindingSource
            // 
            this.VolAvailabilityModelBindingSource.DataSource = typeof(BIS.Model.VolAvailabilityModel);
            // 
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "VolAvaNewModelDataSet";
            reportDataSource1.Value = this.AvailabilitySkillsReportModelBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "GUI.VolAvailabilityNewPreview.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(1222, 587);
            this.reportViewer2.TabIndex = 0;
            // 
            // AvailabilitySkillsReportModelBindingSource
            // 
            this.AvailabilitySkillsReportModelBindingSource.DataSource = typeof(BIS.Model.AvailabilitySkillsModel);
            // 
            // frmVolAvailabilityReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 587);
            this.Controls.Add(this.reportViewer2);
            this.Name = "frmVolAvailabilityReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmVolAvailabilityReport";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmVolAvailabilityReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VolAvailabilityModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource VolAvailabilityModelBindingSource;
        private System.Windows.Forms.BindingSource AvailabilitySkillsReportModelBindingSource;
    }
}
