namespace GUI
{
    partial class frmLedgerCard
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
            this.dtPeriodTo = new System.Windows.Forms.DateTimePicker();
            this.lblPeriodTo = new Telerik.WinControls.UI.RadLabel();
            this.lblPeriodFrom = new Telerik.WinControls.UI.RadLabel();
            this.dtPeriodFrom = new System.Windows.Forms.DateTimePicker();
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
            ((System.ComponentModel.ISupportInitialize)(this.lblPeriodTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPeriodFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFromAccount
            // 
            this.lblFromAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFromAccount.Location = new System.Drawing.Point(23, 31);
            this.lblFromAccount.Name = "lblFromAccount";
            this.lblFromAccount.Size = new System.Drawing.Size(91, 18);
            this.lblFromAccount.TabIndex = 0;
            this.lblFromAccount.Text = "From account";
            // 
            // txtFromAccount
            // 
            this.txtFromAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtFromAccount.Location = new System.Drawing.Point(130, 29);
            this.txtFromAccount.Name = "txtFromAccount";
            this.txtFromAccount.Size = new System.Drawing.Size(82, 20);
            this.txtFromAccount.TabIndex = 1;
            this.txtFromAccount.ThemeName = "Windows8";
            // 
            // btnFromAccount
            // 
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
            this.labelFromAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFromAccount.Location = new System.Drawing.Point(255, 30);
            this.labelFromAccount.Name = "labelFromAccount";
            this.labelFromAccount.Size = new System.Drawing.Size(14, 18);
            this.labelFromAccount.TabIndex = 1;
            this.labelFromAccount.Text = "x";
            // 
            // lblToAccount
            // 
            this.lblToAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblToAccount.Location = new System.Drawing.Point(23, 59);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(74, 18);
            this.lblToAccount.TabIndex = 1;
            this.lblToAccount.Text = "To account";
            // 
            // txtToAccount
            // 
            this.txtToAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtToAccount.Location = new System.Drawing.Point(130, 55);
            this.txtToAccount.Name = "txtToAccount";
            this.txtToAccount.Size = new System.Drawing.Size(82, 20);
            this.txtToAccount.TabIndex = 2;
            this.txtToAccount.ThemeName = "Windows8";
            // 
            // btnToAccount
            // 
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
            this.labelToAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelToAccount.Location = new System.Drawing.Point(255, 57);
            this.labelToAccount.Name = "labelToAccount";
            this.labelToAccount.Size = new System.Drawing.Size(14, 18);
            this.labelToAccount.TabIndex = 2;
            this.labelToAccount.Text = "x";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrint.Location = new System.Drawing.Point(447, 139);
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
            this.btnCancel.Location = new System.Drawing.Point(581, 139);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "VisualStudio2012Light";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbAmounts
            // 
            this.rbAmounts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbAmounts.Font = new System.Drawing.Font("Verdana", 9F);
            this.rbAmounts.Location = new System.Drawing.Point(23, 93);
            this.rbAmounts.Name = "rbAmounts";
            this.rbAmounts.Size = new System.Drawing.Size(75, 18);
            this.rbAmounts.TabIndex = 6;
            this.rbAmounts.Text = "Amounts";
            this.rbAmounts.ThemeName = "ControlDefault";
            this.rbAmounts.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rbAmounts.Visible = false;
            // 
            // rbLedger
            // 
            this.rbLedger.Font = new System.Drawing.Font("Verdana", 9F);
            this.rbLedger.Location = new System.Drawing.Point(23, 118);
            this.rbLedger.Name = "rbLedger";
            this.rbLedger.Size = new System.Drawing.Size(63, 18);
            this.rbLedger.TabIndex = 7;
            this.rbLedger.TabStop = false;
            this.rbLedger.Text = "Ledger";
            this.rbLedger.ThemeName = "ControlDefault";
            this.rbLedger.Visible = false;
            // 
            // dtPeriodTo
            // 
            this.dtPeriodTo.CustomFormat = "MM-yyyy";
            this.dtPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPeriodTo.Location = new System.Drawing.Point(554, 52);
            this.dtPeriodTo.Name = "dtPeriodTo";
            this.dtPeriodTo.Size = new System.Drawing.Size(82, 20);
            this.dtPeriodTo.TabIndex = 22;
            // 
            // lblPeriodTo
            // 
            this.lblPeriodTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPeriodTo.Location = new System.Drawing.Point(447, 55);
            this.lblPeriodTo.Name = "lblPeriodTo";
            this.lblPeriodTo.Size = new System.Drawing.Size(65, 18);
            this.lblPeriodTo.TabIndex = 21;
            this.lblPeriodTo.Text = "To period";
            // 
            // lblPeriodFrom
            // 
            this.lblPeriodFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPeriodFrom.Location = new System.Drawing.Point(447, 29);
            this.lblPeriodFrom.Name = "lblPeriodFrom";
            this.lblPeriodFrom.Size = new System.Drawing.Size(82, 18);
            this.lblPeriodFrom.TabIndex = 20;
            this.lblPeriodFrom.Text = "From period";
            // 
            // dtPeriodFrom
            // 
            this.dtPeriodFrom.CustomFormat = "MM-yyyy";
            this.dtPeriodFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPeriodFrom.Location = new System.Drawing.Point(554, 27);
            this.dtPeriodFrom.Name = "dtPeriodFrom";
            this.dtPeriodFrom.Size = new System.Drawing.Size(82, 20);
            this.dtPeriodFrom.TabIndex = 19;
            // 
            // frmLedgerCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 175);
            this.Controls.Add(this.dtPeriodTo);
            this.Controls.Add(this.lblPeriodTo);
            this.Controls.Add(this.lblPeriodFrom);
            this.Controls.Add(this.dtPeriodFrom);
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
            this.Name = "frmLedgerCard";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ledger card";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmLedgerCard_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.lblPeriodTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPeriodFrom)).EndInit();
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
        private System.Windows.Forms.DateTimePicker dtPeriodTo;
        private Telerik.WinControls.UI.RadLabel lblPeriodTo;
        private Telerik.WinControls.UI.RadLabel lblPeriodFrom;
        private System.Windows.Forms.DateTimePicker dtPeriodFrom;
    }
}
