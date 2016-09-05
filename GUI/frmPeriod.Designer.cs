namespace GUI
{
    partial class frmPeriod
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
            this.btnCancel = new Telerik.WinControls.UI.RadToggleButton();
            this.btnSave = new Telerik.WinControls.UI.RadToggleButton();
            this.txtDescription = new Telerik.WinControls.UI.RadTextBox();
            this.txtMonthTo = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtMonthFrom = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblMonthTo = new Telerik.WinControls.UI.RadLabel();
            this.lblMonthFrom = new Telerik.WinControls.UI.RadLabel();
            this.lblDescription = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonthTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonthFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMonthTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMonthFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(341, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 24);
            this.btnCancel.TabIndex = 313;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "VisualStudio2012Light";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(237, 102);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 24);
            this.btnSave.TabIndex = 312;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "VisualStudio2012Light";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDescription.Location = new System.Drawing.Point(236, 22);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 20);
            this.txtDescription.TabIndex = 311;
            this.txtDescription.ThemeName = "Windows8";
            // 
            // txtMonthTo
            // 
            this.txtMonthTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonthTo.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMonthTo.Location = new System.Drawing.Point(236, 66);
            this.txtMonthTo.Mask = "D";
            this.txtMonthTo.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtMonthTo.Name = "txtMonthTo";
            this.txtMonthTo.Size = new System.Drawing.Size(100, 20);
            this.txtMonthTo.TabIndex = 310;
            this.txtMonthTo.TabStop = false;
            this.txtMonthTo.Text = "0";
            this.txtMonthTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMonthTo.ThemeName = "Windows8";
            this.txtMonthTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonthTo_KeyDown);
            // 
            // txtMonthFrom
            // 
            this.txtMonthFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonthFrom.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMonthFrom.Location = new System.Drawing.Point(236, 44);
            this.txtMonthFrom.Mask = "D";
            this.txtMonthFrom.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtMonthFrom.Name = "txtMonthFrom";
            this.txtMonthFrom.Size = new System.Drawing.Size(100, 20);
            this.txtMonthFrom.TabIndex = 309;
            this.txtMonthFrom.TabStop = false;
            this.txtMonthFrom.Text = "0";
            this.txtMonthFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMonthFrom.ThemeName = "Windows8";
            this.txtMonthFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonthFrom_KeyDown);
            // 
            // lblMonthTo
            // 
            this.lblMonthTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthTo.Location = new System.Drawing.Point(11, 71);
            this.lblMonthTo.Name = "lblMonthTo";
            this.lblMonthTo.Size = new System.Drawing.Size(67, 18);
            this.lblMonthTo.TabIndex = 308;
            this.lblMonthTo.Text = "Month to:";
            // 
            // lblMonthFrom
            // 
            this.lblMonthFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMonthFrom.Location = new System.Drawing.Point(11, 48);
            this.lblMonthFrom.Name = "lblMonthFrom";
            this.lblMonthFrom.Size = new System.Drawing.Size(84, 18);
            this.lblMonthFrom.TabIndex = 307;
            this.lblMonthFrom.Text = "Month from:";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(11, 23);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(125, 18);
            this.lblDescription.TabIndex = 306;
            this.lblDescription.Text = "Description period:";
            // 
            // frmPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 138);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtMonthTo);
            this.Controls.Add(this.lblMonthFrom);
            this.Controls.Add(this.txtMonthFrom);
            this.Controls.Add(this.lblMonthTo);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPeriod";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowItemToolTips = false;
            this.Text = "Period";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmPeriod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonthTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonthFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMonthTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMonthFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadToggleButton btnCancel;
        private Telerik.WinControls.UI.RadToggleButton btnSave;
        private Telerik.WinControls.UI.RadTextBox txtDescription;
        private Telerik.WinControls.UI.RadMaskedEditBox txtMonthTo;
        private Telerik.WinControls.UI.RadMaskedEditBox txtMonthFrom;
        private Telerik.WinControls.UI.RadLabel lblMonthTo;
        private Telerik.WinControls.UI.RadLabel lblMonthFrom;
        private Telerik.WinControls.UI.RadLabel lblDescription;


    }
}
