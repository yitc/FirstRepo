namespace GUI
{
    partial class frmBH_Reispapieren_Begeleiders_Grote_Bus
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.TravelerPapersReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TravelerPapersNewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TravelerPapersTekstModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ArrangementRemainingModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersNewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersTekstModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArrangementRemainingModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TravelerPapersTekstModelBindingSource
            // 
            this.TravelerPapersTekstModelBindingSource.DataMember = "TravelerPapersTekstModel";
            // 
            // ArrangementRemainingModelBindingSource
            // 
            this.ArrangementRemainingModelBindingSource.DataSource = typeof(BIS.Model.ArrangementRemainingModel);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "SubRepTravelerPaperDataSet";
            reportDataSource5.Value = this.TravelerPapersReportModelBindingSource;
            reportDataSource6.Name = "ClientDataSet";
            reportDataSource6.Value = this.TravelerPapersNewModelBindingSource;
            reportDataSource7.Name = "SubTekstDataSet";
            reportDataSource7.Value = this.TravelerPapersTekstModelBindingSource;
            reportDataSource8.Name = "ArrangementRemaining";
            reportDataSource8.Value = this.ArrangementRemainingModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource8);
            this.reportViewer1.LocalReport.EnableHyperlinks = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.TravelPapersVolunteers.SubBH_Reispapieren_Begeleiders_Grote_Bus.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(996, 516);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmBH_Reispapieren_Begeleiders_Grote_Bus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 516);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmBH_Reispapieren_Begeleiders_Grote_Bus";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BH_Reispapieren_Begeleiders_Grote_Bus";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmBH_Reispapieren_Begeleiders_Grote_Bus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersNewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersTekstModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArrangementRemainingModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource TravelerPapersReportModelBindingSource;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private System.Windows.Forms.BindingSource TravelerPapersNewModelBindingSource;
        private System.Windows.Forms.BindingSource TravelerPapersTekstModelBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ArrangementRemainingModelBindingSource;
    }
}
