namespace GUI
{
    partial class frmPrognoseSelection
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
            this.dtDtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDtFrom = new Telerik.WinControls.UI.RadLabel();
            this.dtDtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblDtTo = new Telerik.WinControls.UI.RadLabel();
            this.dlLabel = new Telerik.WinControls.UI.RadDropDownList();
            this.lblLabel = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dlLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(522, 151);
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
            this.btnPrint.Location = new System.Drawing.Point(388, 151);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 24);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "Print";
            this.btnPrint.ThemeName = "Windows8";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtDtFrom
            // 
            this.dtDtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDtFrom.Culture = new System.Globalization.CultureInfo("nl-NL");
            this.dtDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDtFrom.Location = new System.Drawing.Point(120, 36);
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
            this.lblDtFrom.Location = new System.Drawing.Point(33, 38);
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
            this.dtDtTo.Location = new System.Drawing.Point(353, 34);
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
            this.lblDtTo.Location = new System.Drawing.Point(278, 36);
            this.lblDtTo.Name = "lblDtTo";
            this.lblDtTo.Size = new System.Drawing.Size(52, 18);
            this.lblDtTo.TabIndex = 29;
            this.lblDtTo.Text = "Date to";
            // 
            // dlLabel
            // 
            this.dlLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dlLabel.Location = new System.Drawing.Point(122, 77);
            this.dlLabel.Name = "dlLabel";
            this.dlLabel.Size = new System.Drawing.Size(162, 20);
            this.dlLabel.TabIndex = 344;
            this.dlLabel.ThemeName = "Windows8";
            // 
            // lblLabel
            // 
            this.lblLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLabel.Location = new System.Drawing.Point(33, 79);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(39, 18);
            this.lblLabel.TabIndex = 343;
            this.lblLabel.Text = "Label";
            // 
            // frmPrognoseSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 190);
            this.Controls.Add(this.dlLabel);
            this.Controls.Add(this.dtDtTo);
            this.Controls.Add(this.lblLabel);
            this.Controls.Add(this.lblDtTo);
            this.Controls.Add(this.dtDtFrom);
            this.Controls.Add(this.lblDtFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPrognoseSelection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrognoseSelection";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmPrognoseSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dlLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadDateTimePicker dtDtFrom;
        private Telerik.WinControls.UI.RadLabel lblDtFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtDtTo;
        private Telerik.WinControls.UI.RadLabel lblDtTo;
        private Telerik.WinControls.UI.RadDropDownList dlLabel;
        private Telerik.WinControls.UI.RadLabel lblLabel;
    }
}
