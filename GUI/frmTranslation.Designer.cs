namespace GUI
{
    partial class frmTranslation
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
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtSentence = new Telerik.WinControls.UI.RadTextBox();
            this.radLabelSentance = new Telerik.WinControls.UI.RadLabel();
            this.radButtonSave = new Telerik.WinControls.UI.RadButton();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtTranslation = new Telerik.WinControls.UI.RadTextBox();
            this.radLabelTranlation = new Telerik.WinControls.UI.RadLabel();
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95 = new Telerik.WinControls.RootRadElement();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).BeginInit();
            this.splitContainerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            this.radLabel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSentence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSentance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.radLabel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTranslation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelTranlation)).BeginInit();
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
            this.splitPanel1.Controls.Add(this.radLabel2);
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
            // radLabel2
            // 
            this.radLabel2.AutoSize = false;
            this.radLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.radLabel2.Controls.Add(this.txtSentence);
            this.radLabel2.Controls.Add(this.radLabelSentance);
            this.radLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(31, 35);
            this.radLabel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(470, 49);
            this.radLabel2.TabIndex = 3;
            this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSentence
            // 
            this.txtSentence.AutoSize = false;
            this.txtSentence.BackColor = System.Drawing.Color.White;
            this.txtSentence.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSentence.Location = new System.Drawing.Point(123, 3);
            this.txtSentence.MaxLength = 100;
            this.txtSentence.Name = "txtSentence";
            this.txtSentence.Size = new System.Drawing.Size(344, 43);
            this.txtSentence.TabIndex = 2;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtSentence.GetChildAt(0).GetChildAt(0))).NullText = "";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtSentence.GetChildAt(0).GetChildAt(0))).NullTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtSentence.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtSentence.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtSentence.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtSentence.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtSentence.GetChildAt(0).GetChildAt(2))).BottomColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtSentence.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.White;
            // 
            // radLabelSentance
            // 
            this.radLabelSentance.AutoSize = false;
            this.radLabelSentance.BackColor = System.Drawing.Color.White;
            this.radLabelSentance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabelSentance.ForeColor = System.Drawing.Color.Gray;
            this.radLabelSentance.Location = new System.Drawing.Point(2, 3);
            this.radLabelSentance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabelSentance.Name = "radLabelSentance";
            this.radLabelSentance.Size = new System.Drawing.Size(119, 43);
            this.radLabelSentance.TabIndex = 0;
            this.radLabelSentance.Text = "Sentence for translation:";
            this.radLabelSentance.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radButtonSave
            // 
            this.radButtonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            this.radButtonSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButtonSave.ForeColor = System.Drawing.Color.Black;
            this.radButtonSave.Location = new System.Drawing.Point(31, 152);
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
            this.radButtonCancel.Location = new System.Drawing.Point(366, 153);
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
            this.radLabel1.Controls.Add(this.txtTranslation);
            this.radLabel1.Controls.Add(this.radLabelTranlation);
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(31, 91);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(470, 49);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTranslation
            // 
            this.txtTranslation.AutoSize = false;
            this.txtTranslation.BackColor = System.Drawing.Color.White;
            this.txtTranslation.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTranslation.Location = new System.Drawing.Point(123, 3);
            this.txtTranslation.MaxLength = 100;
            this.txtTranslation.Name = "txtTranslation";
            this.txtTranslation.Size = new System.Drawing.Size(344, 43);
            this.txtTranslation.TabIndex = 2;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtTranslation.GetChildAt(0).GetChildAt(0))).NullText = "";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtTranslation.GetChildAt(0).GetChildAt(0))).NullTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtTranslation.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtTranslation.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtTranslation.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtTranslation.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtTranslation.GetChildAt(0).GetChildAt(2))).BottomColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtTranslation.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.White;
            // 
            // radLabelTranlation
            // 
            this.radLabelTranlation.AutoSize = false;
            this.radLabelTranlation.BackColor = System.Drawing.Color.White;
            this.radLabelTranlation.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabelTranlation.ForeColor = System.Drawing.Color.Gray;
            this.radLabelTranlation.Location = new System.Drawing.Point(2, 3);
            this.radLabelTranlation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabelTranlation.Name = "radLabelTranlation";
            this.radLabelTranlation.Size = new System.Drawing.Size(119, 43);
            this.radLabelTranlation.TabIndex = 0;
            this.radLabelTranlation.Text = "Enter translation:";
            this.radLabelTranlation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95
            // 
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.MinSize = new System.Drawing.Size(0, 0);
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.Name = "object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95";
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.StretchHorizontally = true;
            this.object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95.StretchVertically = true;
            // 
            // frmTranslation
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
            this.Name = "frmTranslation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save fiter";
            this.Load += new System.EventHandler(this.frmTranslation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).EndInit();
            this.splitContainerForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            this.radLabel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSentence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelSentance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.radLabel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTranslation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelTranlation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer splitContainerForm;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadLabel radLabelTranlation;
        private Telerik.WinControls.UI.RadButton radButtonSave;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtTranslation;
        private Telerik.WinControls.RootRadElement object_8d9be69f_98e3_452c_b5ba_fc8d9d133d95;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtSentence;
        private Telerik.WinControls.UI.RadLabel radLabelSentance;
    }
}