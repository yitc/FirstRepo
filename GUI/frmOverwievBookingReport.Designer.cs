namespace GUI
{
    partial class frmOverwievBookingReport
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
            this.ddlStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.panelTPapers = new System.Windows.Forms.Panel();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.rgvResult = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlStatus
            // 
            this.ddlStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ddlStatus.Location = new System.Drawing.Point(74, 24);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(165, 20);
            this.ddlStatus.TabIndex = 333;
            this.ddlStatus.ThemeName = "Windows8";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatus.Location = new System.Drawing.Point(12, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 18);
            this.lblStatus.TabIndex = 332;
            this.lblStatus.Text = "Status";
            // 
            // panelTPapers
            // 
            this.panelTPapers.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panelTPapers.Location = new System.Drawing.Point(283, 24);
            this.panelTPapers.Margin = new System.Windows.Forms.Padding(2);
            this.panelTPapers.Name = "panelTPapers";
            this.panelTPapers.Size = new System.Drawing.Size(180, 105);
            this.panelTPapers.TabIndex = 337;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOK.Location = new System.Drawing.Point(349, 149);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 24);
            this.btnOK.TabIndex = 338;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rgvResult
            // 
            this.rgvResult.Location = new System.Drawing.Point(12, 187);
            this.rgvResult.Name = "rgvResult";
            this.rgvResult.Size = new System.Drawing.Size(612, 211);
            this.rgvResult.TabIndex = 341;
            this.rgvResult.Text = "radGridView1";
            this.rgvResult.ThemeName = "Windows8";
            this.rgvResult.Visible = false;
            // 
            // frmOverwievBookingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 180);
            this.Controls.Add(this.rgvResult);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelTPapers);
            this.Controls.Add(this.ddlStatus);
            this.Controls.Add(this.lblStatus);
            this.Name = "frmOverwievBookingReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmOverwievBookingReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList ddlStatus;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private System.Windows.Forms.Panel panelTPapers;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadGridView rgvResult;
    }
}
