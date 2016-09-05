namespace GUI.User_Controls
{
    partial class ArrangementBookDetailView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radListControl1 = new Telerik.WinControls.UI.RadListControl();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // radListControl1
            // 
            this.radListControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.radListControl1.Location = new System.Drawing.Point(0, 0);
            this.radListControl1.Margin = new System.Windows.Forms.Padding(0);
            this.radListControl1.Name = "radListControl1";
            this.radListControl1.Size = new System.Drawing.Size(283, 474);
            this.radListControl1.TabIndex = 0;
            this.radListControl1.Text = "radListControl1";
            this.radListControl1.ThemeName = "VisualStudio2012Light";
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderWidth = 0F;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderLeftWidth = 0F;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderTopWidth = 0F;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderRightWidth = 0F;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderBottomWidth = 0F;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderLeftColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderTopColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderRightColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderBottomColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BackColor = System.Drawing.SystemColors.Control;
            // 
            // ArrangementBookDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radListControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ArrangementBookDetailView";
            this.Size = new System.Drawing.Size(283, 474);
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadListControl radListControl1;
    }
}
