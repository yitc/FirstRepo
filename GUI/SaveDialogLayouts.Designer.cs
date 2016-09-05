namespace GUI
{
    partial class SaveDialogLayouts
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
            this.splitContainerForm = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.radButtonSave = new Telerik.WinControls.UI.RadButton();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtFilename = new Telerik.WinControls.UI.RadTextBox();
            this.radLabelFilename = new Telerik.WinControls.UI.RadLabel();
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95 = new Telerik.WinControls.RootRadElement();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).BeginInit();
            this.splitContainerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.radLabel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilename)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelFilename)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerForm
            // 
            this.splitContainerForm.Controls.Add(this.splitPanel1);
            this.splitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerForm.Location = new System.Drawing.Point(0, 0);
            this.splitContainerForm.Name = "splitContainerForm";
            this.splitContainerForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.splitContainerForm.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitContainerForm.Size = new System.Drawing.Size(536, 230);
            this.splitContainerForm.SplitterWidth = 0;
            this.splitContainerForm.TabIndex = 0;
            this.splitContainerForm.TabStop = false;
            this.splitContainerForm.Text = "radSplitContainer1";
            this.splitContainerForm.ThemeName = "VisualStudio2012Light";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.Transparent;
            this.splitPanel1.Controls.Add(this.radButtonSave);
            this.splitPanel1.Controls.Add(this.radButtonCancel);
            this.splitPanel1.Controls.Add(this.radLabel1);
            this.splitPanel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(536, 230);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.2237762F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 64);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.ThemeName = "VisualStudio2012Light";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.splitPanel1.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            // 
            // radButtonSave
            // 
            this.radButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            this.radButtonSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonSave.ForeColor = System.Drawing.Color.Black;
            this.radButtonSave.Location = new System.Drawing.Point(31, 146);
            this.radButtonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radButtonSave.Name = "radButtonSave";
            this.radButtonSave.Size = new System.Drawing.Size(135, 43);
            this.radButtonSave.TabIndex = 0;
            this.radButtonSave.Text = "Save";
            this.radButtonSave.ThemeName = "VisualStudio2012Light";
            this.radButtonSave.Click += new System.EventHandler(this.radButtonSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButtonSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButtonSave.GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButtonSave.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButtonSave.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.radButtonCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonCancel.Location = new System.Drawing.Point(366, 147);
            this.radButtonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(135, 43);
            this.radButtonCancel.TabIndex = 0;
            this.radButtonCancel.Text = "Cancel";
            this.radButtonCancel.ThemeName = "VisualStudio2012Light";
            this.radButtonCancel.Click += new System.EventHandler(this.radButtonCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButtonCancel.GetChildAt(0))).Text = "Cancel";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButtonCancel.GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButtonCancel.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButtonCancel.GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButtonCancel.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.radLabel1.Controls.Add(this.txtFilename);
            this.radLabel1.Controls.Add(this.radLabelFilename);
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(31, 57);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(470, 49);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFilename
            // 
            this.txtFilename.AutoSize = false;
            this.txtFilename.BackColor = System.Drawing.Color.White;
            this.txtFilename.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(123, 3);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(389, 43);
            this.txtFilename.TabIndex = 2;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtFilename.GetChildAt(0).GetChildAt(0))).NullText = "";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtFilename.GetChildAt(0).GetChildAt(0))).NullTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtFilename.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtFilename.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtFilename.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtFilename.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFilename.GetChildAt(0).GetChildAt(2))).BottomColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFilename.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.White;
            // 
            // radLabelFilename
            // 
            this.radLabelFilename.AutoSize = false;
            this.radLabelFilename.BackColor = System.Drawing.Color.White;
            this.radLabelFilename.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabelFilename.ForeColor = System.Drawing.Color.Gray;
            this.radLabelFilename.Location = new System.Drawing.Point(2, 3);
            this.radLabelFilename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabelFilename.Name = "radLabelFilename";
            this.radLabelFilename.Size = new System.Drawing.Size(119, 43);
            this.radLabelFilename.TabIndex = 0;
            this.radLabelFilename.Text = "Enter filename:";
            this.radLabelFilename.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95
            // 
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.MinSize = new System.Drawing.Size(0, 0);
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.Name = "object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95";
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.StretchHorizontally = true;
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.StretchVertically = true;
            // 
            // SaveDialogLayouts
            // 
            this.AcceptButton = this.radButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(169)))), ((int)(((byte)(234)))));
            this.CancelButton = this.radButtonCancel;
            this.ClientSize = new System.Drawing.Size(536, 230);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerForm);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveDialogLayouts";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save fiter";
            this.Load += new System.EventHandler(this.SaveDialogLayouts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).EndInit();
            this.splitContainerForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.radLabel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFilename)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelFilename)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer splitContainerForm;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadLabel radLabelFilename;
        private Telerik.WinControls.UI.RadButton radButtonSave;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtFilename;
        private Telerik.WinControls.RootRadElement object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95;
    }
}