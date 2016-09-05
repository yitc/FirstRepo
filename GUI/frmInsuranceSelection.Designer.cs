namespace GUI
{
    partial class frmInsuranceSelection
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
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.rbCancelInsurance = new Telerik.WinControls.UI.RadRadioButton();
            this.rbInsurance = new Telerik.WinControls.UI.RadRadioButton();
            this.panelOption = new System.Windows.Forms.Panel();
            this.btnArrangementLookup = new Telerik.WinControls.UI.RadButton();
            this.rbDate = new Telerik.WinControls.UI.RadRadioButton();
            this.txtArrangementName = new Telerik.WinControls.UI.RadTextBox();
            this.dtDtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDtFrom = new Telerik.WinControls.UI.RadLabel();
            this.rbArrangement = new Telerik.WinControls.UI.RadRadioButton();
            this.dtDtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDtTo = new Telerik.WinControls.UI.RadLabel();
            this.rbCancelInsuranceWithoutArrangement = new Telerik.WinControls.UI.RadRadioButton();
            this.rbInsuranceWithoutArrangemnt = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCancelInsurance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbInsurance)).BeginInit();
            this.panelOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnArrangementLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrangementName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCancelInsuranceWithoutArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbInsuranceWithoutArrangemnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(522, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "Windows8";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(388, 185);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 24);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "Print";
            this.btnPrint.ThemeName = "Windows8";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rbCancelInsurance
            // 
            this.rbCancelInsurance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbCancelInsurance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCancelInsurance.Location = new System.Drawing.Point(32, 99);
            this.rbCancelInsurance.Name = "rbCancelInsurance";
            this.rbCancelInsurance.Size = new System.Drawing.Size(126, 18);
            this.rbCancelInsurance.TabIndex = 22;
            this.rbCancelInsurance.Text = "Cancel insurance";
            this.rbCancelInsurance.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // rbInsurance
            // 
            this.rbInsurance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInsurance.Location = new System.Drawing.Point(32, 123);
            this.rbInsurance.Name = "rbInsurance";
            this.rbInsurance.Size = new System.Drawing.Size(82, 18);
            this.rbInsurance.TabIndex = 23;
            this.rbInsurance.TabStop = false;
            this.rbInsurance.Text = "Insurance";
            // 
            // panelOption
            // 
            this.panelOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOption.Controls.Add(this.btnArrangementLookup);
            this.panelOption.Controls.Add(this.rbDate);
            this.panelOption.Controls.Add(this.txtArrangementName);
            this.panelOption.Controls.Add(this.dtDtFrom);
            this.panelOption.Controls.Add(this.lblDtFrom);
            this.panelOption.Controls.Add(this.rbArrangement);
            this.panelOption.Controls.Add(this.dtDtTo);
            this.panelOption.Controls.Add(this.lblDtTo);
            this.panelOption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panelOption.Location = new System.Drawing.Point(13, 11);
            this.panelOption.Margin = new System.Windows.Forms.Padding(2);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(592, 66);
            this.panelOption.TabIndex = 338;
            // 
            // btnArrangementLookup
            // 
            this.btnArrangementLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArrangementLookup.Location = new System.Drawing.Point(404, 31);
            this.btnArrangementLookup.Name = "btnArrangementLookup";
            this.btnArrangementLookup.Size = new System.Drawing.Size(20, 20);
            this.btnArrangementLookup.TabIndex = 340;
            this.btnArrangementLookup.Text = "...";
            this.btnArrangementLookup.ThemeName = "Windows8";
            this.btnArrangementLookup.Click += new System.EventHandler(this.btnArrangementLookup_Click);
            // 
            // rbDate
            // 
            this.rbDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbDate.Font = new System.Drawing.Font("Verdana", 9F);
            this.rbDate.Location = new System.Drawing.Point(20, 5);
            this.rbDate.Name = "rbDate";
            this.rbDate.Size = new System.Drawing.Size(50, 18);
            this.rbDate.TabIndex = 24;
            this.rbDate.Text = "Date";
            this.rbDate.ThemeName = "ControlDefault";
            this.rbDate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rbDate.CheckStateChanged += new System.EventHandler(this.rbDate_CheckStateChanged);
            // 
            // txtArrangementName
            // 
            this.txtArrangementName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArrangementName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtArrangementName.Location = new System.Drawing.Point(142, 31);
            this.txtArrangementName.Name = "txtArrangementName";
            this.txtArrangementName.ReadOnly = true;
            this.txtArrangementName.Size = new System.Drawing.Size(256, 20);
            this.txtArrangementName.TabIndex = 339;
            this.txtArrangementName.ThemeName = "Windows8";
            // 
            // dtDtFrom
            // 
            this.dtDtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDtFrom.Location = new System.Drawing.Point(325, 5);
            this.dtDtFrom.Name = "dtDtFrom";
            this.dtDtFrom.Size = new System.Drawing.Size(100, 20);
            this.dtDtFrom.TabIndex = 26;
            this.dtDtFrom.TabStop = false;
            this.dtDtFrom.Text = "21-4-2016";
            this.dtDtFrom.Value = new System.DateTime(2016, 4, 21, 22, 26, 54, 501);
            // 
            // lblDtFrom
            // 
            this.lblDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtFrom.Location = new System.Drawing.Point(202, 5);
            this.lblDtFrom.Name = "lblDtFrom";
            this.lblDtFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDtFrom.TabIndex = 22;
            this.lblDtFrom.Text = "Date from";
            // 
            // rbArrangement
            // 
            this.rbArrangement.Font = new System.Drawing.Font("Verdana", 9F);
            this.rbArrangement.Location = new System.Drawing.Point(20, 33);
            this.rbArrangement.Name = "rbArrangement";
            this.rbArrangement.Size = new System.Drawing.Size(102, 18);
            this.rbArrangement.TabIndex = 25;
            this.rbArrangement.TabStop = false;
            this.rbArrangement.Text = "Arrangement";
            this.rbArrangement.ThemeName = "ControlDefault";
            this.rbArrangement.CheckStateChanged += new System.EventHandler(this.rbArrangement_CheckStateChanged);
            // 
            // dtDtTo
            // 
            this.dtDtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDtTo.Location = new System.Drawing.Point(324, 31);
            this.dtDtTo.Name = "dtDtTo";
            this.dtDtTo.Size = new System.Drawing.Size(100, 20);
            this.dtDtTo.TabIndex = 27;
            this.dtDtTo.TabStop = false;
            this.dtDtTo.Text = "21-4-2016";
            this.dtDtTo.Value = new System.DateTime(2016, 4, 21, 22, 27, 2, 823);
            // 
            // lblDtTo
            // 
            this.lblDtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtTo.Location = new System.Drawing.Point(202, 31);
            this.lblDtTo.Name = "lblDtTo";
            this.lblDtTo.Size = new System.Drawing.Size(52, 18);
            this.lblDtTo.TabIndex = 23;
            this.lblDtTo.Text = "Date to";
            // 
            // rbCancelInsuranceWithoutArrangement
            // 
            this.rbCancelInsuranceWithoutArrangement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCancelInsuranceWithoutArrangement.Location = new System.Drawing.Point(215, 99);
            this.rbCancelInsuranceWithoutArrangement.Name = "rbCancelInsuranceWithoutArrangement";
            this.rbCancelInsuranceWithoutArrangement.Size = new System.Drawing.Size(302, 18);
            this.rbCancelInsuranceWithoutArrangement.TabIndex = 24;
            this.rbCancelInsuranceWithoutArrangement.TabStop = false;
            this.rbCancelInsuranceWithoutArrangement.Text = "Cancel insurance without arrangement group";
            // 
            // rbInsuranceWithoutArrangemnt
            // 
            this.rbInsuranceWithoutArrangemnt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInsuranceWithoutArrangemnt.Location = new System.Drawing.Point(215, 123);
            this.rbInsuranceWithoutArrangemnt.Name = "rbInsuranceWithoutArrangemnt";
            this.rbInsuranceWithoutArrangemnt.Size = new System.Drawing.Size(258, 18);
            this.rbInsuranceWithoutArrangemnt.TabIndex = 25;
            this.rbInsuranceWithoutArrangemnt.TabStop = false;
            this.rbInsuranceWithoutArrangemnt.Text = "Insurance without arrangement group";
            // 
            // frmInsuranceSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 224);
            this.Controls.Add(this.rbCancelInsuranceWithoutArrangement);
            this.Controls.Add(this.rbInsuranceWithoutArrangemnt);
            this.Controls.Add(this.panelOption);
            this.Controls.Add(this.rbCancelInsurance);
            this.Controls.Add(this.rbInsurance);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmInsuranceSelection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInsuranceSelection";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmInsuranceSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCancelInsurance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbInsurance)).EndInit();
            this.panelOption.ResumeLayout(false);
            this.panelOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnArrangementLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrangementName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbArrangement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbCancelInsuranceWithoutArrangement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbInsuranceWithoutArrangemnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadRadioButton rbCancelInsurance;
        private Telerik.WinControls.UI.RadRadioButton rbInsurance;
        private Telerik.WinControls.UI.RadRadioButton rbDate;
        private Telerik.WinControls.UI.RadRadioButton rbArrangement;
        private System.Windows.Forms.Panel panelOption;
        private Telerik.WinControls.UI.RadDateTimePicker dtDtFrom;
        private Telerik.WinControls.UI.RadLabel lblDtFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtDtTo;
        private Telerik.WinControls.UI.RadLabel lblDtTo;
        private Telerik.WinControls.UI.RadButton btnArrangementLookup;
        private Telerik.WinControls.UI.RadTextBox txtArrangementName;
        private Telerik.WinControls.UI.RadRadioButton rbCancelInsuranceWithoutArrangement;
        private Telerik.WinControls.UI.RadRadioButton rbInsuranceWithoutArrangemnt;
    }
}
