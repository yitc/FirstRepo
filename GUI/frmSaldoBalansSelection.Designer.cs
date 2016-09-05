namespace GUI
{
    partial class frmSaldoBalansSelection
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
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.dtDtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDtFrom = new Telerik.WinControls.UI.RadLabel();
            this.dtDtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDtTo = new Telerik.WinControls.UI.RadLabel();
            this.rbBalans = new Telerik.WinControls.UI.RadRadioButton();
            this.rbWinst = new Telerik.WinControls.UI.RadRadioButton();
            this.rbTotal = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbBalans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbWinst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(341, 144);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 24);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.ThemeName = "VisualStudio2012Light";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(475, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "VisualStudio2012Light";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtDtFrom
            // 
            this.dtDtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDtFrom.Culture = new System.Globalization.CultureInfo("nl-NL");
            this.dtDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDtFrom.Location = new System.Drawing.Point(136, 21);
            this.dtDtFrom.Name = "dtDtFrom";
            this.dtDtFrom.Size = new System.Drawing.Size(100, 20);
            this.dtDtFrom.TabIndex = 28;
            this.dtDtFrom.TabStop = false;
            this.dtDtFrom.Text = "21-4-2016";
            this.dtDtFrom.Value = new System.DateTime(2016, 4, 21, 22, 26, 54, 501);
            // 
            // lblDtFrom
            // 
            this.lblDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtFrom.Location = new System.Drawing.Point(29, 23);
            this.lblDtFrom.Name = "lblDtFrom";
            this.lblDtFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDtFrom.TabIndex = 27;
            this.lblDtFrom.Text = "Date from";
            // 
            // dtDtTo
            // 
            this.dtDtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDtTo.Culture = new System.Globalization.CultureInfo("nl-NL");
            this.dtDtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDtTo.Location = new System.Drawing.Point(431, 21);
            this.dtDtTo.Name = "dtDtTo";
            this.dtDtTo.Size = new System.Drawing.Size(100, 20);
            this.dtDtTo.TabIndex = 30;
            this.dtDtTo.TabStop = false;
            this.dtDtTo.Text = "21-4-2016";
            this.dtDtTo.Value = new System.DateTime(2016, 4, 21, 22, 26, 54, 501);
            // 
            // lblDtTo
            // 
            this.lblDtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtTo.Location = new System.Drawing.Point(341, 23);
            this.lblDtTo.Name = "lblDtTo";
            this.lblDtTo.Size = new System.Drawing.Size(52, 18);
            this.lblDtTo.TabIndex = 29;
            this.lblDtTo.Text = "Date to";
            // 
            // rbBalans
            // 
            this.rbBalans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbBalans.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBalans.Location = new System.Drawing.Point(29, 68);
            this.rbBalans.Name = "rbBalans";
            this.rbBalans.Size = new System.Drawing.Size(91, 18);
            this.rbBalans.TabIndex = 31;
            this.rbBalans.Text = "Saldibalans";
            this.rbBalans.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // rbWinst
            // 
            this.rbWinst.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbWinst.Location = new System.Drawing.Point(29, 92);
            this.rbWinst.Name = "rbWinst";
            this.rbWinst.Size = new System.Drawing.Size(100, 18);
            this.rbWinst.TabIndex = 32;
            this.rbWinst.TabStop = false;
            this.rbWinst.Text = "Winst verlies";
            // 
            // rbTotal
            // 
            this.rbTotal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTotal.Location = new System.Drawing.Point(29, 116);
            this.rbTotal.Name = "rbTotal";
            this.rbTotal.Size = new System.Drawing.Size(51, 18);
            this.rbTotal.TabIndex = 33;
            this.rbTotal.TabStop = false;
            this.rbTotal.Text = "Total";
            // 
            // frmSaldoBalansSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 186);
            this.Controls.Add(this.rbTotal);
            this.Controls.Add(this.rbBalans);
            this.Controls.Add(this.rbWinst);
            this.Controls.Add(this.dtDtTo);
            this.Controls.Add(this.lblDtTo);
            this.Controls.Add(this.dtDtFrom);
            this.Controls.Add(this.lblDtFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSaldoBalansSelection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balans saldo selection";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmSaldoBalansSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbBalans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbWinst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadDateTimePicker dtDtFrom;
        private Telerik.WinControls.UI.RadLabel lblDtFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtDtTo;
        private Telerik.WinControls.UI.RadLabel lblDtTo;
        private Telerik.WinControls.UI.RadRadioButton rbBalans;
        private Telerik.WinControls.UI.RadRadioButton rbWinst;
        private Telerik.WinControls.UI.RadRadioButton rbTotal;
    }
}
