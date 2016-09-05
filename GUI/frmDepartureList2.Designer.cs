namespace GUI
{
    partial class frmDepartureList2
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.rgvResult = new Telerik.WinControls.UI.RadGridView();
            this.dtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDateFrom = new Telerik.WinControls.UI.RadLabel();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.dtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDateTo = new Telerik.WinControls.UI.RadLabel();
            this.btnInclusive = new Telerik.WinControls.UI.RadRadioButton();
            this.btnExlusive = new Telerik.WinControls.UI.RadRadioButton();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclusive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExlusive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rgvResult
            // 
            this.rgvResult.Location = new System.Drawing.Point(12, 178);
            // 
            // 
            // 
            this.rgvResult.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.rgvResult.Name = "rgvResult";
            this.rgvResult.Size = new System.Drawing.Size(454, 296);
            this.rgvResult.TabIndex = 344;
            this.rgvResult.Text = "radGridView1";
            this.rgvResult.ThemeName = "VisualStudio2012Light";
            this.rgvResult.Visible = false;
            // 
            // dtFrom
            // 
            this.dtFrom.AutoSize = false;
            this.dtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(107, 32);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(100, 20);
            this.dtFrom.TabIndex = 345;
            this.dtFrom.TabStop = false;
            this.dtFrom.ThemeName = "Windows8";
            this.dtFrom.Value = new System.DateTime(((long)(0)));
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDateFrom.Location = new System.Drawing.Point(15, 32);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDateFrom.TabIndex = 346;
            this.lblDateFrom.Text = "Date from";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOK.Location = new System.Drawing.Point(339, 130);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 24);
            this.btnOK.TabIndex = 349;
            this.btnOK.Text = "OK";
            this.btnOK.ThemeName = "Windows8";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dtTo
            // 
            this.dtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(349, 32);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(100, 20);
            this.dtTo.TabIndex = 351;
            this.dtTo.TabStop = false;
            this.dtTo.ThemeName = "Windows8";
            this.dtTo.Value = new System.DateTime(((long)(0)));
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDateTo.Location = new System.Drawing.Point(272, 32);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(52, 18);
            this.lblDateTo.TabIndex = 350;
            this.lblDateTo.Text = "Date to";
            // 
            // btnInclusive
            // 
            this.btnInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnInclusive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnInclusive.Location = new System.Drawing.Point(17, 76);
            this.btnInclusive.Name = "btnInclusive";
            this.btnInclusive.Size = new System.Drawing.Size(155, 18);
            this.btnInclusive.TabIndex = 354;
            this.btnInclusive.Text = "Inclusive status Optie";
            this.btnInclusive.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // btnExlusive
            // 
            this.btnExlusive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExlusive.Location = new System.Drawing.Point(17, 100);
            this.btnExlusive.Name = "btnExlusive";
            this.btnExlusive.Size = new System.Drawing.Size(157, 18);
            this.btnExlusive.TabIndex = 355;
            this.btnExlusive.TabStop = false;
            this.btnExlusive.Text = "Exclusive status Optie";
            // 
            // frmDepartureList2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 176);
            this.Controls.Add(this.btnInclusive);
            this.Controls.Add(this.btnExlusive);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.rgvResult);
            this.Name = "frmDepartureList2";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmDepartureList2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclusive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExlusive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView rgvResult;
        private Telerik.WinControls.UI.RadDateTimePicker dtFrom;
        private Telerik.WinControls.UI.RadLabel lblDateFrom;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadDateTimePicker dtTo;
        private Telerik.WinControls.UI.RadLabel lblDateTo;
        private Telerik.WinControls.UI.RadRadioButton btnInclusive;
        private Telerik.WinControls.UI.RadRadioButton btnExlusive;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}
