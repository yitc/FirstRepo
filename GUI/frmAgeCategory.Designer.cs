namespace GUI
{
    partial class frmAgeCategory
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
            this.txtDesciption = new Telerik.WinControls.UI.RadTextBox();
            this.txtMax = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtMin = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.lblMaxAgeCategory = new Telerik.WinControls.UI.RadLabel();
            this.lblMinAgeCategory = new Telerik.WinControls.UI.RadLabel();
            this.lblDescription = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesciption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMaxAgeCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMinAgeCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(344, 97);
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
            this.btnSave.Location = new System.Drawing.Point(240, 97);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 24);
            this.btnSave.TabIndex = 312;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "VisualStudio2012Light";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDesciption
            // 
            this.txtDesciption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesciption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDesciption.Location = new System.Drawing.Point(239, 17);
            this.txtDesciption.MaxLength = 40;
            this.txtDesciption.Name = "txtDesciption";
            this.txtDesciption.Size = new System.Drawing.Size(200, 20);
            this.txtDesciption.TabIndex = 311;
            this.txtDesciption.ThemeName = "Windows8";
            // 
            // txtMax
            // 
            this.txtMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMax.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMax.Location = new System.Drawing.Point(239, 61);
            this.txtMax.Mask = "D";
            this.txtMax.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(100, 20);
            this.txtMax.TabIndex = 310;
            this.txtMax.TabStop = false;
            this.txtMax.Text = "0";
            this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMax.ThemeName = "Windows8";
            this.txtMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMax_KeyDown);
            // 
            // txtMin
            // 
            this.txtMin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMin.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMin.Location = new System.Drawing.Point(239, 39);
            this.txtMin.Mask = "D";
            this.txtMin.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(100, 20);
            this.txtMin.TabIndex = 309;
            this.txtMin.TabStop = false;
            this.txtMin.Text = "0";
            this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMin.ThemeName = "Windows8";
            this.txtMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMin_KeyDown);
            // 
            // lblMaxAgeCategory
            // 
            this.lblMaxAgeCategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxAgeCategory.Location = new System.Drawing.Point(14, 66);
            this.lblMaxAgeCategory.Name = "lblMaxAgeCategory";
            this.lblMaxAgeCategory.Size = new System.Drawing.Size(158, 18);
            this.lblMaxAgeCategory.TabIndex = 308;
            this.lblMaxAgeCategory.Text = "Maximum age category:";
            // 
            // lblMinAgeCategory
            // 
            this.lblMinAgeCategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMinAgeCategory.Location = new System.Drawing.Point(14, 43);
            this.lblMinAgeCategory.Name = "lblMinAgeCategory";
            this.lblMinAgeCategory.Size = new System.Drawing.Size(154, 18);
            this.lblMinAgeCategory.TabIndex = 307;
            this.lblMinAgeCategory.Text = "Minimum age category:";
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(14, 18);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(167, 18);
            this.lblDescription.TabIndex = 306;
            this.lblDescription.Text = "Description age category:";
            // 
            // frmAgeCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 138);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDesciption);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.lblMaxAgeCategory);
            this.Controls.Add(this.lblMinAgeCategory);
            this.Controls.Add(this.lblDescription);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmAgeCategory";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowItemToolTips = false;
            this.Text = "Age Category";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmAgeCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesciption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMaxAgeCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMinAgeCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadToggleButton btnCancel;
        private Telerik.WinControls.UI.RadToggleButton btnSave;
        private Telerik.WinControls.UI.RadTextBox txtDesciption;
        private Telerik.WinControls.UI.RadMaskedEditBox txtMax;
        private Telerik.WinControls.UI.RadMaskedEditBox txtMin;
        private Telerik.WinControls.UI.RadLabel lblMaxAgeCategory;
        private Telerik.WinControls.UI.RadLabel lblMinAgeCategory;
        private Telerik.WinControls.UI.RadLabel lblDescription;

    }
}
