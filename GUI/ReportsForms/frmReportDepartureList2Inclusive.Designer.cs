﻿namespace GUI
{
    partial class frmReportDepartureList2Inclusive
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
            this.reportViewer29 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DepartureList2InclusiveModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList2InclusiveModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer29
            // 
            this.reportViewer29.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DepartureList2InclusiveDataSet";
            reportDataSource1.Value = this.DepartureList2InclusiveModelBindingSource;
            this.reportViewer29.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer29.LocalReport.ReportEmbeddedResource = "GUI.Reports.ReportDepartureList2InclusivePreview.rdlc";
            this.reportViewer29.Location = new System.Drawing.Point(0, 0);
            this.reportViewer29.Name = "reportViewer29";
            this.reportViewer29.Size = new System.Drawing.Size(1201, 425);
            this.reportViewer29.TabIndex = 0;
            // 
            // DepartureList2InclusiveModelBindingSource
            // 
            this.DepartureList2InclusiveModelBindingSource.DataSource = typeof(BIS.Model.DepartureList2InclusiveModel);
            // 
            // frmReportDepartureList2Inclusive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 425);
            this.Controls.Add(this.reportViewer29);
            this.Name = "frmReportDepartureList2Inclusive";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList2Inclusive";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList2Inclusive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList2InclusiveModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer29;
        private System.Windows.Forms.BindingSource DepartureList2InclusiveModelBindingSource;
    }
}
