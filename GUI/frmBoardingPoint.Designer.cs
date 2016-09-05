namespace GUI
{
    partial class frmBoardingPoint
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
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.btnBoardingPoint = new Telerik.WinControls.UI.RadButton();
            this.lblName = new Telerik.WinControls.UI.RadLabel();
            this.txtName = new Telerik.WinControls.UI.RadTextBox();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.lblAddressBP = new Telerik.WinControls.UI.RadLabel();
            this.txtAddressBP = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBoardingPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddressBP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressBP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Location = new System.Drawing.Point(-1, -2);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Size = new System.Drawing.Size(601, 179);
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            this.radSplitContainer1.ThemeName = "Windows8";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.Transparent;
            this.splitPanel1.Controls.Add(this.lblAddressBP);
            this.splitPanel1.Controls.Add(this.txtAddressBP);
            this.splitPanel1.Controls.Add(this.btnBoardingPoint);
            this.splitPanel1.Controls.Add(this.lblName);
            this.splitPanel1.Controls.Add(this.txtName);
            this.splitPanel1.Controls.Add(this.btnDelete);
            this.splitPanel1.Controls.Add(this.btnCancel);
            this.splitPanel1.Controls.Add(this.btnSave);
            this.splitPanel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(601, 179);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.2237762F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 64);
            this.splitPanel1.TabIndex = 1;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.ThemeName = "Windows8";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.splitPanel1.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            // 
            // btnBoardingPoint
            // 
            this.btnBoardingPoint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBoardingPoint.Location = new System.Drawing.Point(540, 41);
            this.btnBoardingPoint.Name = "btnBoardingPoint";
            this.btnBoardingPoint.Size = new System.Drawing.Size(20, 20);
            this.btnBoardingPoint.TabIndex = 295;
            this.btnBoardingPoint.Text = "...";
            this.btnBoardingPoint.ThemeName = "VisualStudio2012Light";
            this.btnBoardingPoint.Click += new System.EventHandler(this.btnBoardingPoint_Click);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(52, 41);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(143, 18);
            this.lblName.TabIndex = 294;
            this.lblName.Text = "Name boarding point:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(214, 41);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(309, 20);
            this.txtName.TabIndex = 293;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(322, 123);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 24);
            this.btnDelete.TabIndex = 292;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(429, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 24);
            this.btnCancel.TabIndex = 291;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(215, 123);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 24);
            this.btnSave.TabIndex = 290;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblAddressBP
            // 
            this.lblAddressBP.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressBP.Location = new System.Drawing.Point(52, 67);
            this.lblAddressBP.Name = "lblAddressBP";
            this.lblAddressBP.Size = new System.Drawing.Size(61, 18);
            this.lblAddressBP.TabIndex = 297;
            this.lblAddressBP.Text = "Address:";
            // 
            // txtAddressBP
            // 
            this.txtAddressBP.Location = new System.Drawing.Point(214, 67);
            this.txtAddressBP.Name = "txtAddressBP";
            this.txtAddressBP.Size = new System.Drawing.Size(309, 20);
            this.txtAddressBP.TabIndex = 296;
            // 
            // frmBoardingPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 177);
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "frmBoardingPoint";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowItemToolTips = false;
            this.Text = "";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmBoardingPoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBoardingPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddressBP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressBP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadButton btnBoardingPoint;
        private Telerik.WinControls.UI.RadLabel lblName;
        private Telerik.WinControls.UI.RadTextBox txtName;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadLabel lblAddressBP;
        private Telerik.WinControls.UI.RadTextBox txtAddressBP;
    }
}
