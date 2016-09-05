namespace GUI
{
    partial class frmExtraArticles
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
            this.lblFromAccount = new Telerik.WinControls.UI.RadLabel();
            this.lblToAccount = new Telerik.WinControls.UI.RadLabel();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.dtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.panelLabels = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFromAccount
            // 
            this.lblFromAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFromAccount.Location = new System.Drawing.Point(23, 31);
            this.lblFromAccount.Name = "lblFromAccount";
            this.lblFromAccount.Size = new System.Drawing.Size(43, 18);
            this.lblFromAccount.TabIndex = 0;
            this.lblFromAccount.Text = "From ";
            // 
            // lblToAccount
            // 
            this.lblToAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblToAccount.Location = new System.Drawing.Point(23, 59);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(26, 18);
            this.lblToAccount.TabIndex = 1;
            this.lblToAccount.Text = "To ";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(306, 139);
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
            this.btnCancel.Location = new System.Drawing.Point(440, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "VisualStudio2012Light";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtFrom
            // 
            this.dtFrom.AutoSize = false;
            this.dtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(106, 29);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(90, 20);
            this.dtFrom.TabIndex = 320;
            this.dtFrom.TabStop = false;
            this.dtFrom.Text = "4/28/2016";
            this.dtFrom.ThemeName = "Windows8";
            this.dtFrom.Value = new System.DateTime(2016, 4, 28, 0, 0, 0, 0);
            // 
            // dtTo
            // 
            this.dtTo.AutoSize = false;
            this.dtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(106, 59);
            this.dtTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(90, 20);
            this.dtTo.TabIndex = 321;
            this.dtTo.TabStop = false;
            this.dtTo.Text = "4/28/2016";
            this.dtTo.ThemeName = "Windows8";
            this.dtTo.UseCompatibleTextRendering = false;
            this.dtTo.Value = new System.DateTime(2016, 4, 28, 0, 0, 0, 0);
            // 
            // panelLabels
            // 
            this.panelLabels.Location = new System.Drawing.Point(216, 29);
            this.panelLabels.Name = "panelLabels";
            this.panelLabels.Size = new System.Drawing.Size(200, 100);
            this.panelLabels.TabIndex = 322;
            // 
            // frmExtraArticles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 175);
            this.Controls.Add(this.panelLabels);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblToAccount);
            this.Controls.Add(this.lblFromAccount);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmExtraArticles";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extra articles";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmExtraArticles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblFromAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblFromAccount;
        private Telerik.WinControls.UI.RadLabel lblToAccount;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadDateTimePicker dtFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtTo;
        private System.Windows.Forms.Panel panelLabels;
    }
}
