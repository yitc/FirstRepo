﻿namespace GUI
{
    partial class frmTravelersReports
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.TravelerPapersReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TravelerPapersNewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TravelerPapersTekstModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersNewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersTekstModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TravelerPapersTekstModelBindingSource
            // 
            this.TravelerPapersTekstModelBindingSource.DataMember = "TravelerPapersTekstModel";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "SubRepTravelerPaperDataSet";
            reportDataSource1.Value = this.TravelerPapersReportModelBindingSource;
            reportDataSource2.Name = "ClientDataSet";
            reportDataSource2.Value = this.TravelerPapersNewModelBindingSource;
            reportDataSource3.Name = "SubTekstDataSet";
            reportDataSource3.Value = this.TravelerPapersTekstModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.EnableHyperlinks = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.SubRepTravelerPapers.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(929, 720);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmTravelersReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 720);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmTravelersReports";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmTravelersReports";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmTravelersReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersNewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersTekstModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource TravelerPapersReportModelBindingSource;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private System.Windows.Forms.BindingSource TravelerPapersNewModelBindingSource;
        private System.Windows.Forms.BindingSource TravelerPapersTekstModelBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}
