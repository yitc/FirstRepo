namespace GUI
{
    partial class ReportListWithExpired
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
            this.AvailabilitySkillsModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer6 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // AvailabilitySkillsModelBindingSource
            // 
            this.AvailabilitySkillsModelBindingSource.DataSource = typeof(BIS.Model.AvailabilitySkillsModel);
            // 
            // reportViewer6
            // 
            this.reportViewer6.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ListWithExpiredDataSet";
            reportDataSource1.Value = this.AvailabilitySkillsModelBindingSource;
            this.reportViewer6.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer6.LocalReport.ReportEmbeddedResource = "GUI.ListWithExpiredPreview.rdlc";
            this.reportViewer6.Location = new System.Drawing.Point(0, 0);
            this.reportViewer6.Name = "reportViewer6";
            this.reportViewer6.Size = new System.Drawing.Size(1209, 493);
            this.reportViewer6.TabIndex = 0;
            // 
            // ReportListWithExpired
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 493);
            this.Controls.Add(this.reportViewer6);
            this.Name = "ReportListWithExpired";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "ReportListWithExpired";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.ReportListWithExpired_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer6;
        private System.Windows.Forms.BindingSource AvailabilitySkillsModelBindingSource;
    }
}
