namespace GUI
{
    partial class frmLedgerSelection
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
            this.txtFromAccount = new Telerik.WinControls.UI.RadTextBox();
            this.btnFromAccount = new Telerik.WinControls.UI.RadButton();
            this.labelFromAccount = new Telerik.WinControls.UI.RadLabel();
            this.lblToAccount = new Telerik.WinControls.UI.RadLabel();
            this.txtToAccount = new Telerik.WinControls.UI.RadTextBox();
            this.btnToAccount = new Telerik.WinControls.UI.RadButton();
            this.labelToAccount = new Telerik.WinControls.UI.RadLabel();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.rbAmounts = new Telerik.WinControls.UI.RadRadioButton();
            this.rbLedger = new Telerik.WinControls.UI.RadRadioButton();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.chkZero = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFromAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelFromAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnToAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelToAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbAmounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkZero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFromAccount
            // 
            this.lblFromAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFromAccount.Location = new System.Drawing.Point(23, 31);
            this.lblFromAccount.Name = "lblFromAccount";
            this.lblFromAccount.Size = new System.Drawing.Size(84, 17);
            this.lblFromAccount.TabIndex = 0;
            this.lblFromAccount.Text = "From account";
            // 
            // txtFromAccount
            // 
            this.txtFromAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtFromAccount.Location = new System.Drawing.Point(130, 29);
            this.txtFromAccount.Name = "txtFromAccount";
            this.txtFromAccount.Size = new System.Drawing.Size(82, 19);
            this.txtFromAccount.TabIndex = 1;
            this.txtFromAccount.ThemeName = "Windows8";
            // 
            // btnFromAccount
            // 
            this.btnFromAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnFromAccount.Image = global::GUI.Properties.Resources.lookup_x20;
            this.btnFromAccount.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFromAccount.Location = new System.Drawing.Point(218, 29);
            this.btnFromAccount.Name = "btnFromAccount";
            this.btnFromAccount.Size = new System.Drawing.Size(20, 20);
            this.btnFromAccount.TabIndex = 2;
            this.btnFromAccount.Text = "...";
            this.btnFromAccount.ThemeName = "Windows8";
            this.btnFromAccount.Click += new System.EventHandler(this.btnFromAccount_Click);
            // 
            // labelFromAccount
            // 
            this.labelFromAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFromAccount.Location = new System.Drawing.Point(255, 30);
            this.labelFromAccount.Name = "labelFromAccount";
            this.labelFromAccount.Size = new System.Drawing.Size(13, 17);
            this.labelFromAccount.TabIndex = 1;
            this.labelFromAccount.Text = "x";
            // 
            // lblToAccount
            // 
            this.lblToAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblToAccount.Location = new System.Drawing.Point(23, 59);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(68, 17);
            this.lblToAccount.TabIndex = 1;
            this.lblToAccount.Text = "To account";
            // 
            // txtToAccount
            // 
            this.txtToAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtToAccount.Location = new System.Drawing.Point(130, 55);
            this.txtToAccount.Name = "txtToAccount";
            this.txtToAccount.Size = new System.Drawing.Size(82, 19);
            this.txtToAccount.TabIndex = 2;
            this.txtToAccount.ThemeName = "Windows8";
            // 
            // btnToAccount
            // 
            this.btnToAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnToAccount.Image = global::GUI.Properties.Resources.lookup_x20;
            this.btnToAccount.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnToAccount.Location = new System.Drawing.Point(218, 55);
            this.btnToAccount.Name = "btnToAccount";
            this.btnToAccount.Size = new System.Drawing.Size(20, 20);
            this.btnToAccount.TabIndex = 3;
            this.btnToAccount.Text = "...";
            this.btnToAccount.ThemeName = "Windows8";
            this.btnToAccount.Click += new System.EventHandler(this.btnToAccount_Click);
            // 
            // labelToAccount
            // 
            this.labelToAccount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelToAccount.Location = new System.Drawing.Point(255, 57);
            this.labelToAccount.Name = "labelToAccount";
            this.labelToAccount.Size = new System.Drawing.Size(13, 17);
            this.labelToAccount.TabIndex = 2;
            this.labelToAccount.Text = "x";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(341, 134);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 24);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.ThemeName = "Windows8";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(475, 134);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "Windows8";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbAmounts
            // 
            this.rbAmounts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbAmounts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbAmounts.Location = new System.Drawing.Point(23, 93);
            this.rbAmounts.Name = "rbAmounts";
            this.rbAmounts.Size = new System.Drawing.Size(71, 17);
            this.rbAmounts.TabIndex = 6;
            this.rbAmounts.Text = "Amounts";
            this.rbAmounts.ThemeName = "ControlDefault";
            this.rbAmounts.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rbAmounts.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rbAmounts_ToggleStateChanged);
            // 
            // rbLedger
            // 
            this.rbLedger.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbLedger.Location = new System.Drawing.Point(23, 118);
            this.rbLedger.Name = "rbLedger";
            this.rbLedger.Size = new System.Drawing.Size(59, 17);
            this.rbLedger.TabIndex = 7;
            this.rbLedger.TabStop = false;
            this.rbLedger.Text = "Ledger";
            this.rbLedger.ThemeName = "ControlDefault";
            // 
            // chkZero
            // 
            this.chkZero.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkZero.Location = new System.Drawing.Point(255, 93);
            this.chkZero.Name = "chkZero";
            this.chkZero.Size = new System.Drawing.Size(129, 18);
            this.chkZero.TabIndex = 8;
            this.chkZero.Text = "with zero amounts";
            this.chkZero.ThemeName = "Windows8";
            // 
            // frmLedgerSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 175);
            this.Controls.Add(this.chkZero);
            this.Controls.Add(this.rbLedger);
            this.Controls.Add(this.rbAmounts);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.labelToAccount);
            this.Controls.Add(this.btnToAccount);
            this.Controls.Add(this.txtToAccount);
            this.Controls.Add(this.lblToAccount);
            this.Controls.Add(this.labelFromAccount);
            this.Controls.Add(this.btnFromAccount);
            this.Controls.Add(this.txtFromAccount);
            this.Controls.Add(this.lblFromAccount);
            this.Name = "frmLedgerSelection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ledger Selection";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmLedgerSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblFromAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFromAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelFromAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnToAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelToAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbAmounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkZero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblFromAccount;
        private Telerik.WinControls.UI.RadTextBox txtFromAccount;
        private Telerik.WinControls.UI.RadButton btnFromAccount;
        private Telerik.WinControls.UI.RadLabel labelFromAccount;
        private Telerik.WinControls.UI.RadLabel lblToAccount;
        private Telerik.WinControls.UI.RadTextBox txtToAccount;
        private Telerik.WinControls.UI.RadButton btnToAccount;
        private Telerik.WinControls.UI.RadLabel labelToAccount;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadRadioButton rbAmounts;
        private Telerik.WinControls.UI.RadRadioButton rbLedger;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadCheckBox chkZero;
    }
}
