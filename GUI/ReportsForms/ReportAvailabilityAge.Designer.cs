namespace GUI
{
    partial class ReportAvailabilityAge
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
            this.reportViewer5 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AvailabilitySkillsModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer5
            // 
            this.reportViewer5.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "AgeCategoryDataSet";
            reportDataSource1.Value = this.AvailabilitySkillsModelBindingSource;
            this.reportViewer5.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer5.LocalReport.ReportEmbeddedResource = "GUI.VolAvailabilityAgePreview.rdlc";
            this.reportViewer5.Location = new System.Drawing.Point(0, 0);
            this.reportViewer5.Name = "reportViewer5";
            this.reportViewer5.Size = new System.Drawing.Size(1203, 474);
            this.reportViewer5.TabIndex = 0;
            // 
            // AvailabilitySkillsModelBindingSource
            // 
            this.AvailabilitySkillsModelBindingSource.DataSource = typeof(BIS.Model.AvailabilitySkillsModel);
            // 
            // ReportAvailabilityAge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 474);
            this.Controls.Add(this.reportViewer5);
            this.Name = "ReportAvailabilityAge";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "ReportAvailabilityAge";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.ReportAvailabilityAge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer5;
        private System.Windows.Forms.BindingSource AvailabilitySkillsModelBindingSource;
    }
}
