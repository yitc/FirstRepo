namespace GUI
{
    partial class frmArrangementInsurancePremie
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
            this.lblAmmount = new Telerik.WinControls.UI.RadLabel();
            this.lblCodeInsurance = new Telerik.WinControls.UI.RadLabel();
            this.lblPremie = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.maskedAmmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.dropdownCode = new Telerik.WinControls.UI.RadDropDownList();
            this.dropdownPremie = new Telerik.WinControls.UI.RadDropDownList();
            this.chkSport = new Telerik.WinControls.UI.RadCheckBox();
            this.chkMedicalDevices = new Telerik.WinControls.UI.RadCheckBox();
            this.dtValidTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDateTo = new Telerik.WinControls.UI.RadLabel();
            this.lblDateFrom = new Telerik.WinControls.UI.RadLabel();
            this.dtValidFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCodeInsurance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPremie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskedAmmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownPremie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMedicalDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAmmount
            // 
            this.lblAmmount.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblAmmount.Location = new System.Drawing.Point(46, 64);
            this.lblAmmount.Name = "lblAmmount";
            this.lblAmmount.Size = new System.Drawing.Size(67, 18);
            this.lblAmmount.TabIndex = 16;
            this.lblAmmount.Text = "Ammount";
            // 
            // lblCodeInsurance
            // 
            this.lblCodeInsurance.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblCodeInsurance.Location = new System.Drawing.Point(46, 38);
            this.lblCodeInsurance.Name = "lblCodeInsurance";
            this.lblCodeInsurance.Size = new System.Drawing.Size(38, 18);
            this.lblCodeInsurance.TabIndex = 15;
            this.lblCodeInsurance.Text = "Code";
            // 
            // lblPremie
            // 
            this.lblPremie.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblPremie.Location = new System.Drawing.Point(46, 12);
            this.lblPremie.Name = "lblPremie";
            this.lblPremie.Size = new System.Drawing.Size(49, 18);
            this.lblPremie.TabIndex = 14;
            this.lblPremie.Text = "Premie";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(46, 217);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(243, 24);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "Windows8";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // maskedAmmount
            // 
            this.maskedAmmount.Font = new System.Drawing.Font("Verdana", 9F);
            this.maskedAmmount.Location = new System.Drawing.Point(140, 62);
            this.maskedAmmount.Mask = "n2";
            this.maskedAmmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.maskedAmmount.Name = "maskedAmmount";
            this.maskedAmmount.Size = new System.Drawing.Size(149, 20);
            this.maskedAmmount.TabIndex = 11;
            this.maskedAmmount.TabStop = false;
            this.maskedAmmount.Text = "0,00";
            this.maskedAmmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedAmmount.ThemeName = "Windows8";
            this.maskedAmmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedAmmount_KeyDown);
            // 
            // dropdownCode
            // 
            this.dropdownCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.dropdownCode.Font = new System.Drawing.Font("Verdana", 9F);
            this.dropdownCode.Location = new System.Drawing.Point(140, 36);
            this.dropdownCode.Name = "dropdownCode";
            this.dropdownCode.NullText = "Empty";
            this.dropdownCode.Size = new System.Drawing.Size(149, 20);
            this.dropdownCode.TabIndex = 10;
            this.dropdownCode.ThemeName = "Windows8";
            // 
            // dropdownPremie
            // 
            this.dropdownPremie.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.dropdownPremie.Font = new System.Drawing.Font("Verdana", 9F);
            this.dropdownPremie.Location = new System.Drawing.Point(140, 10);
            this.dropdownPremie.Name = "dropdownPremie";
            this.dropdownPremie.NullText = "Empty";
            this.dropdownPremie.Size = new System.Drawing.Size(149, 20);
            this.dropdownPremie.TabIndex = 9;
            this.dropdownPremie.ThemeName = "Windows8";
            // 
            // chkSport
            // 
            this.chkSport.Font = new System.Drawing.Font("Verdana", 9F);
            this.chkSport.Location = new System.Drawing.Point(140, 149);
            this.chkSport.Name = "chkSport";
            this.chkSport.Size = new System.Drawing.Size(54, 18);
            this.chkSport.TabIndex = 19;
            this.chkSport.Text = "Sport";
            this.chkSport.Visible = false;
            // 
            // chkMedicalDevices
            // 
            this.chkMedicalDevices.Font = new System.Drawing.Font("Verdana", 9F);
            this.chkMedicalDevices.Location = new System.Drawing.Point(140, 173);
            this.chkMedicalDevices.Name = "chkMedicalDevices";
            this.chkMedicalDevices.Size = new System.Drawing.Size(119, 18);
            this.chkMedicalDevices.TabIndex = 20;
            this.chkMedicalDevices.Text = "Medical Devices";
            this.chkMedicalDevices.Visible = false;
            // 
            // dtValidTo
            // 
            this.dtValidTo.Font = new System.Drawing.Font("Verdana", 9F);
            this.dtValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValidTo.Location = new System.Drawing.Point(140, 114);
            this.dtValidTo.Name = "dtValidTo";
            this.dtValidTo.Size = new System.Drawing.Size(149, 20);
            this.dtValidTo.TabIndex = 21;
            this.dtValidTo.TabStop = false;
            this.dtValidTo.Text = "22-10-2015";
            this.dtValidTo.ThemeName = "Windows8";
            this.dtValidTo.Value = new System.DateTime(2015, 10, 22, 23, 10, 59, 546);
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDateTo.Location = new System.Drawing.Point(46, 116);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(52, 18);
            this.lblDateTo.TabIndex = 22;
            this.lblDateTo.Text = "Date to";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDateFrom.Location = new System.Drawing.Point(46, 90);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDateFrom.TabIndex = 24;
            this.lblDateFrom.Text = "Date from";
            // 
            // dtValidFrom
            // 
            this.dtValidFrom.Font = new System.Drawing.Font("Verdana", 9F);
            this.dtValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValidFrom.Location = new System.Drawing.Point(140, 88);
            this.dtValidFrom.Name = "dtValidFrom";
            this.dtValidFrom.Size = new System.Drawing.Size(149, 20);
            this.dtValidFrom.TabIndex = 23;
            this.dtValidFrom.TabStop = false;
            this.dtValidFrom.Text = "22-10-2015";
            this.dtValidFrom.ThemeName = "Windows8";
            this.dtValidFrom.Value = new System.DateTime(2015, 10, 22, 23, 10, 59, 546);
            // 
            // frmArrangementInsurancePremie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 366);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.dtValidFrom);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtValidTo);
            this.Controls.Add(this.chkMedicalDevices);
            this.Controls.Add(this.chkSport);
            this.Controls.Add(this.lblAmmount);
            this.Controls.Add(this.lblCodeInsurance);
            this.Controls.Add(this.lblPremie);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.maskedAmmount);
            this.Controls.Add(this.dropdownCode);
            this.Controls.Add(this.dropdownPremie);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArrangementInsurancePremie";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arrangement Insurance Premie";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmArrangementInsurancePremie_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblAmmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCodeInsurance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPremie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskedAmmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownPremie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMedicalDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblAmmount;
        private Telerik.WinControls.UI.RadLabel lblCodeInsurance;
        private Telerik.WinControls.UI.RadLabel lblPremie;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadMaskedEditBox maskedAmmount;
        private Telerik.WinControls.UI.RadDropDownList dropdownCode;
        private Telerik.WinControls.UI.RadDropDownList dropdownPremie;
        private Telerik.WinControls.UI.RadCheckBox chkSport;
        private Telerik.WinControls.UI.RadCheckBox chkMedicalDevices;
        private Telerik.WinControls.UI.RadDateTimePicker dtValidTo;
        private Telerik.WinControls.UI.RadLabel lblDateTo;
        private Telerik.WinControls.UI.RadLabel lblDateFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtValidFrom;
    }
}