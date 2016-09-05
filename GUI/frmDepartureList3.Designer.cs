namespace GUI
{
    partial class frmDepartureList3
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.dtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.rgvResult = new Telerik.WinControls.UI.RadGridView();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.lblDtTo = new Telerik.WinControls.UI.RadLabel();
            this.lblDtFrom = new Telerik.WinControls.UI.RadLabel();
            this.dtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dlArangementType = new Telerik.WinControls.UI.RadDropDownList();
            this.lblArrangementType = new Telerik.WinControls.UI.RadLabel();
            this.btnExclusive = new Telerik.WinControls.UI.RadRadioButton();
            this.btnInclusive = new Telerik.WinControls.UI.RadRadioButton();
            this.panelOption = new System.Windows.Forms.Panel();
            this.dlLabel = new Telerik.WinControls.UI.RadDropDownList();
            this.lblLabel = new Telerik.WinControls.UI.RadLabel();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dlArangementType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblArrangementType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExclusive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclusive)).BeginInit();
            this.panelOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dlLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // dtFrom
            // 
            this.dtFrom.AutoSize = false;
            this.dtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(159, 18);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(103, 20);
            this.dtFrom.TabIndex = 327;
            this.dtFrom.TabStop = false;
            this.dtFrom.Text = "17-6-2015";
            this.dtFrom.ThemeName = "Windows8";
            this.dtFrom.Value = new System.DateTime(2015, 6, 17, 11, 51, 55, 168);
            // 
            // rgvResult
            // 
            this.rgvResult.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rgvResult.Location = new System.Drawing.Point(22, 390);
            // 
            // 
            // 
            this.rgvResult.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvResult.Name = "rgvResult";
            this.rgvResult.Size = new System.Drawing.Size(617, 217);
            this.rgvResult.TabIndex = 333;
            this.rgvResult.Text = "radGridView1";
            this.rgvResult.ThemeName = "Windows8";
            this.rgvResult.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOK.Location = new System.Drawing.Point(411, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(114, 24);
            this.btnOK.TabIndex = 331;
            this.btnOK.Text = "0K";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDtTo
            // 
            this.lblDtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtTo.Location = new System.Drawing.Point(15, 44);
            this.lblDtTo.Name = "lblDtTo";
            this.lblDtTo.Size = new System.Drawing.Size(52, 18);
            this.lblDtTo.TabIndex = 329;
            this.lblDtTo.Text = "Date to";
            // 
            // lblDtFrom
            // 
            this.lblDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtFrom.Location = new System.Drawing.Point(15, 20);
            this.lblDtFrom.Name = "lblDtFrom";
            this.lblDtFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDtFrom.TabIndex = 328;
            this.lblDtFrom.Text = "Date from";
            // 
            // dtTo
            // 
            this.dtTo.AutoSize = false;
            this.dtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(159, 42);
            this.dtTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(103, 20);
            this.dtTo.TabIndex = 338;
            this.dtTo.TabStop = false;
            this.dtTo.Text = "31-12-2015";
            this.dtTo.ThemeName = "Windows8";
            this.dtTo.Value = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            // 
            // dlArangementType
            // 
            this.dlArangementType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dlArangementType.Location = new System.Drawing.Point(158, 73);
            this.dlArangementType.Name = "dlArangementType";
            this.dlArangementType.Size = new System.Drawing.Size(367, 20);
            this.dlArangementType.TabIndex = 340;
            this.dlArangementType.ThemeName = "Windows8";
            // 
            // lblArrangementType
            // 
            this.lblArrangementType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblArrangementType.Location = new System.Drawing.Point(12, 74);
            this.lblArrangementType.Name = "lblArrangementType";
            this.lblArrangementType.Size = new System.Drawing.Size(119, 18);
            this.lblArrangementType.TabIndex = 339;
            this.lblArrangementType.Text = "Arrangement type";
            // 
            // btnExclusive
            // 
            this.btnExclusive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExclusive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExclusive.Location = new System.Drawing.Point(3, 34);
            this.btnExclusive.Name = "btnExclusive";
            this.btnExclusive.Size = new System.Drawing.Size(157, 18);
            this.btnExclusive.TabIndex = 342;
            this.btnExclusive.TabStop = false;
            this.btnExclusive.Text = "Exclusive status Optie";
            // 
            // btnInclusive
            // 
            this.btnInclusive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnInclusive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnInclusive.Location = new System.Drawing.Point(3, 9);
            this.btnInclusive.Name = "btnInclusive";
            this.btnInclusive.Size = new System.Drawing.Size(155, 18);
            this.btnInclusive.TabIndex = 341;
            this.btnInclusive.Text = "Inclusive status Optie";
            this.btnInclusive.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // panelOption
            // 
            this.panelOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOption.Controls.Add(this.btnInclusive);
            this.panelOption.Controls.Add(this.btnExclusive);
            this.panelOption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panelOption.Location = new System.Drawing.Point(358, 10);
            this.panelOption.Margin = new System.Windows.Forms.Padding(2);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(165, 56);
            this.panelOption.TabIndex = 337;
            // 
            // dlLabel
            // 
            this.dlLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dlLabel.Location = new System.Drawing.Point(158, 96);
            this.dlLabel.Name = "dlLabel";
            this.dlLabel.Size = new System.Drawing.Size(104, 20);
            this.dlLabel.TabIndex = 342;
            this.dlLabel.ThemeName = "Windows8";
            // 
            // lblLabel
            // 
            this.lblLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLabel.Location = new System.Drawing.Point(12, 98);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(39, 18);
            this.lblLabel.TabIndex = 341;
            this.lblLabel.Text = "Label";
            // 
            // frmDepartureList3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 212);
            this.Controls.Add(this.dlLabel);
            this.Controls.Add(this.panelOption);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.dlArangementType);
            this.Controls.Add(this.lblArrangementType);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.rgvResult);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblDtTo);
            this.Controls.Add(this.lblDtFrom);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "frmDepartureList3";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menagment Report";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmPurchaseReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dlArangementType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblArrangementType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExclusive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclusive)).EndInit();
            this.panelOption.ResumeLayout(false);
            this.panelOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dlLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDateTimePicker dtFrom;
        private Telerik.WinControls.UI.RadGridView rgvResult;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadLabel lblDtTo;
        private Telerik.WinControls.UI.RadLabel lblDtFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtTo;
        private Telerik.WinControls.UI.RadDropDownList dlArangementType;
        private Telerik.WinControls.UI.RadLabel lblArrangementType;
        private Telerik.WinControls.UI.RadRadioButton btnExclusive;
        private Telerik.WinControls.UI.RadRadioButton btnInclusive;
        private System.Windows.Forms.Panel panelOption;
        private Telerik.WinControls.UI.RadDropDownList dlLabel;
        private Telerik.WinControls.UI.RadLabel lblLabel;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}
