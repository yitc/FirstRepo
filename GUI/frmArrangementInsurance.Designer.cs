namespace GUI
{
    partial class frmArrangementInsurance
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
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.dropdownLabel = new Telerik.WinControls.UI.RadDropDownList();
            this.dropdownCode = new Telerik.WinControls.UI.RadDropDownList();
            this.maskedAmmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.dtValidFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.lblLabelInsurance = new Telerik.WinControls.UI.RadLabel();
            this.lblCodeInsurance = new Telerik.WinControls.UI.RadLabel();
            this.lblAmmount = new Telerik.WinControls.UI.RadLabel();
            this.lblDateFrom = new Telerik.WinControls.UI.RadLabel();
            this.lblDateTo = new Telerik.WinControls.UI.RadLabel();
            this.dtValidTo = new Telerik.WinControls.UI.RadDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskedAmmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabelInsurance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCodeInsurance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // dropdownLabel
            // 
            this.dropdownLabel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.dropdownLabel.Font = new System.Drawing.Font("Verdana", 9F);
            this.dropdownLabel.Location = new System.Drawing.Point(152, 12);
            this.dropdownLabel.Name = "dropdownLabel";
            this.dropdownLabel.NullText = "Empty";
            this.dropdownLabel.Size = new System.Drawing.Size(149, 20);
            this.dropdownLabel.TabIndex = 0;
            this.dropdownLabel.ThemeName = "Windows8";
            // 
            // dropdownCode
            // 
            this.dropdownCode.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.dropdownCode.Font = new System.Drawing.Font("Verdana", 9F);
            this.dropdownCode.Location = new System.Drawing.Point(152, 38);
            this.dropdownCode.Name = "dropdownCode";
            this.dropdownCode.NullText = "Empty";
            this.dropdownCode.Size = new System.Drawing.Size(149, 20);
            this.dropdownCode.TabIndex = 1;
            this.dropdownCode.ThemeName = "Windows8";
            // 
            // maskedAmmount
            // 
            this.maskedAmmount.Font = new System.Drawing.Font("Verdana", 9F);
            this.maskedAmmount.Location = new System.Drawing.Point(152, 64);
            this.maskedAmmount.Mask = "n2";
            this.maskedAmmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.maskedAmmount.Name = "maskedAmmount";
            this.maskedAmmount.Size = new System.Drawing.Size(149, 20);
            this.maskedAmmount.TabIndex = 2;
            this.maskedAmmount.TabStop = false;
            this.maskedAmmount.Text = "0,00";
            this.maskedAmmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maskedAmmount.ThemeName = "Windows8";
            this.maskedAmmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.maskedAmmount_KeyDown);
            // 
            // dtValidFrom
            // 
            this.dtValidFrom.Font = new System.Drawing.Font("Verdana", 9F);
            this.dtValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValidFrom.Location = new System.Drawing.Point(152, 90);
            this.dtValidFrom.Name = "dtValidFrom";
            this.dtValidFrom.Size = new System.Drawing.Size(149, 20);
            this.dtValidFrom.TabIndex = 3;
            this.dtValidFrom.TabStop = false;
            this.dtValidFrom.Text = "22-10-2015";
            this.dtValidFrom.ThemeName = "Windows8";
            this.dtValidFrom.Value = new System.DateTime(2015, 10, 22, 23, 10, 59, 546);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(52, 173);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(249, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "Windows8";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLabelInsurance
            // 
            this.lblLabelInsurance.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblLabelInsurance.Location = new System.Drawing.Point(48, 14);
            this.lblLabelInsurance.Name = "lblLabelInsurance";
            this.lblLabelInsurance.Size = new System.Drawing.Size(39, 18);
            this.lblLabelInsurance.TabIndex = 5;
            this.lblLabelInsurance.Text = "Label";
            // 
            // lblCodeInsurance
            // 
            this.lblCodeInsurance.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblCodeInsurance.Location = new System.Drawing.Point(48, 40);
            this.lblCodeInsurance.Name = "lblCodeInsurance";
            this.lblCodeInsurance.Size = new System.Drawing.Size(38, 18);
            this.lblCodeInsurance.TabIndex = 6;
            this.lblCodeInsurance.Text = "Code";
            // 
            // lblAmmount
            // 
            this.lblAmmount.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblAmmount.Location = new System.Drawing.Point(48, 68);
            this.lblAmmount.Name = "lblAmmount";
            this.lblAmmount.Size = new System.Drawing.Size(67, 18);
            this.lblAmmount.TabIndex = 7;
            this.lblAmmount.Text = "Ammount";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDateFrom.Location = new System.Drawing.Point(48, 92);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDateFrom.TabIndex = 8;
            this.lblDateFrom.Text = "Date from";
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDateTo.Location = new System.Drawing.Point(48, 119);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(52, 18);
            this.lblDateTo.TabIndex = 10;
            this.lblDateTo.Text = "Date to";
            // 
            // dtValidTo
            // 
            this.dtValidTo.Font = new System.Drawing.Font("Verdana", 9F);
            this.dtValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtValidTo.Location = new System.Drawing.Point(152, 117);
            this.dtValidTo.Name = "dtValidTo";
            this.dtValidTo.Size = new System.Drawing.Size(149, 20);
            this.dtValidTo.TabIndex = 9;
            this.dtValidTo.TabStop = false;
            this.dtValidTo.Text = "22-10-2015";
            this.dtValidTo.ThemeName = "Windows8";
            this.dtValidTo.Value = new System.DateTime(2015, 10, 22, 23, 10, 59, 546);
            // 
            // frmArrangementInsurance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 311);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtValidTo);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.lblAmmount);
            this.Controls.Add(this.lblCodeInsurance);
            this.Controls.Add(this.lblLabelInsurance);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtValidFrom);
            this.Controls.Add(this.maskedAmmount);
            this.Controls.Add(this.dropdownCode);
            this.Controls.Add(this.dropdownLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArrangementInsurance";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arrangement Insurance";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmArrangementInsurance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dropdownLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropdownCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskedAmmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabelInsurance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCodeInsurance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAmmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadDropDownList dropdownLabel;
        private Telerik.WinControls.UI.RadDropDownList dropdownCode;
        private Telerik.WinControls.UI.RadMaskedEditBox maskedAmmount;
        private Telerik.WinControls.UI.RadDateTimePicker dtValidFrom;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadLabel lblLabelInsurance;
        private Telerik.WinControls.UI.RadLabel lblDateFrom;
        private Telerik.WinControls.UI.RadLabel lblCodeInsurance;
        private Telerik.WinControls.UI.RadLabel lblAmmount;
        private Telerik.WinControls.UI.RadLabel lblDateTo;
        private Telerik.WinControls.UI.RadDateTimePicker dtValidTo;
    }
}