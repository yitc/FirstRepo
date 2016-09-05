namespace GUI
{
    partial class ReportAvailabilityNotBookedFunction
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
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AvailabilitySkillsModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer3
            // 
            this.reportViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "AvailabilityNotBookedFunctionDataSet";
            reportDataSource1.Value = this.AvailabilitySkillsModelBindingSource;
            this.reportViewer3.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "GUI.VolAvailabilityNotBookedPreview.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(0, 0);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.Size = new System.Drawing.Size(1186, 532);
            this.reportViewer3.TabIndex = 0;
            // 
            // AvailabilitySkillsModelBindingSource
            // 
            this.AvailabilitySkillsModelBindingSource.DataSource = typeof(BIS.Model.AvailabilitySkillsModel);
            // 
            // ReportAvailabilityNotBookedFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 532);
            this.Controls.Add(this.reportViewer3);
            this.Name = "ReportAvailabilityNotBookedFunction";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "ReportAvailabilityNotBookedFunction";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.ReportAvailabilityNotBookedFunction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private System.Windows.Forms.BindingSource AvailabilitySkillsModelBindingSource;
    }
}
