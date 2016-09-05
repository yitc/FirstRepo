namespace GUI
{
    partial class PersonDetailView
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
            this.imageProfile = new System.Windows.Forms.PictureBox();
            this.labelFullname = new Telerik.WinControls.UI.RadLabel();
            this.radPanelTop = new Telerik.WinControls.UI.RadPanel();
            this.radPanelMiddle = new Telerik.WinControls.UI.RadPanel();
            this.radListControl1 = new Telerik.WinControls.UI.RadListControl();
            this.object_5802c056_22e9_4971_9f6c_416719d87ee7 = new Telerik.WinControls.RootRadElement();
            this.radPanelBottom = new Telerik.WinControls.UI.RadPanel();
            this.pictureLetter = new System.Windows.Forms.PictureBox();
            this.pictureEmail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelFullname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelTop)).BeginInit();
            this.radPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelMiddle)).BeginInit();
            this.radPanelMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelBottom)).BeginInit();
            this.radPanelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // imageProfile
            // 
            this.imageProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imageProfile.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageProfile.Image = global::GUI.Properties.Resources.user;
            this.imageProfile.InitialImage = null;
            this.imageProfile.Location = new System.Drawing.Point(3, 3);
            this.imageProfile.MaximumSize = new System.Drawing.Size(128, 115);
            this.imageProfile.MinimumSize = new System.Drawing.Size(50, 50);
            this.imageProfile.Name = "imageProfile";
            this.imageProfile.Size = new System.Drawing.Size(128, 115);
            this.imageProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageProfile.TabIndex = 0;
            this.imageProfile.TabStop = false;
            // 
            // labelFullname
            // 
            this.labelFullname.AutoSize = false;
            this.labelFullname.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.labelFullname.Location = new System.Drawing.Point(3, 122);
            this.labelFullname.Name = "labelFullname";
            this.labelFullname.Padding = new System.Windows.Forms.Padding(3);
            this.labelFullname.Size = new System.Drawing.Size(255, 28);
            this.labelFullname.TabIndex = 1;
            this.labelFullname.Text = "radLabel1";
            this.labelFullname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelFullname.TextWrap = false;
            this.labelFullname.ThemeName = "Windows8";
            this.labelFullname.Click += new System.EventHandler(this.labelFullname_Click);
            // 
            // radPanelTop
            // 
            this.radPanelTop.Controls.Add(this.radPanelBottom);
            this.radPanelTop.Controls.Add(this.imageProfile);
            this.radPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanelTop.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanelTop.Location = new System.Drawing.Point(0, 0);
            this.radPanelTop.Name = "radPanelTop";
            this.radPanelTop.Padding = new System.Windows.Forms.Padding(3);
            this.radPanelTop.Size = new System.Drawing.Size(283, 121);
            this.radPanelTop.TabIndex = 4;
            this.radPanelTop.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.radPanelTop.ThemeName = "Windows8";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanelTop.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(3);
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanelTop.GetChildAt(0).GetChildAt(1))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanelTop.GetChildAt(0).GetChildAt(1))).AutoSize = false;
            // 
            // radPanelMiddle
            // 
            this.radPanelMiddle.Controls.Add(this.radListControl1);
            this.radPanelMiddle.Controls.Add(this.labelFullname);
            this.radPanelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanelMiddle.Location = new System.Drawing.Point(0, 0);
            this.radPanelMiddle.Name = "radPanelMiddle";
            this.radPanelMiddle.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.radPanelMiddle.Size = new System.Drawing.Size(283, 500);
            this.radPanelMiddle.TabIndex = 5;
            this.radPanelMiddle.ThemeName = "Windows8";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanelMiddle.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanelMiddle.GetChildAt(0).GetChildAt(1))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanelMiddle.GetChildAt(0).GetChildAt(1))).AutoSize = false;
            // 
            // radListControl1
            // 
            this.radListControl1.BackColor = System.Drawing.Color.Transparent;
            this.radListControl1.Location = new System.Drawing.Point(3, 156);
            this.radListControl1.Name = "radListControl1";
            this.radListControl1.Size = new System.Drawing.Size(277, 432);
            this.radListControl1.TabIndex = 2;
            this.radListControl1.Text = "radListControl1";
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderLeftColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderTopColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderRightColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderBottomColor = System.Drawing.SystemColors.Control;
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.UI.RadScrollBarElement)(this.radListControl1.GetChildAt(0).GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radListControl1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(223)))), ((int)(((byte)(244)))));
            // 
            // object_5802c056_22e9_4971_9f6c_416719d87ee7
            // 
            this.object_5802c056_22e9_4971_9f6c_416719d87ee7.Name = "object_5802c056_22e9_4971_9f6c_416719d87ee7";
            this.object_5802c056_22e9_4971_9f6c_416719d87ee7.StretchHorizontally = true;
            this.object_5802c056_22e9_4971_9f6c_416719d87ee7.StretchVertically = true;
            // 
            // radPanelBottom
            // 
            this.radPanelBottom.Controls.Add(this.pictureLetter);
            this.radPanelBottom.Controls.Add(this.pictureEmail);
            this.radPanelBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanelBottom.Location = new System.Drawing.Point(131, 3);
            this.radPanelBottom.Name = "radPanelBottom";
            this.radPanelBottom.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.radPanelBottom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // 
            // 
            this.radPanelBottom.RootElement.AutoSize = false;
            this.radPanelBottom.Size = new System.Drawing.Size(149, 118);
            this.radPanelBottom.TabIndex = 1;
            this.radPanelBottom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanelBottom.GetChildAt(0))).TextOrientation = System.Windows.Forms.Orientation.Vertical;
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanelBottom.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanelBottom.GetChildAt(0))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentPadding;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanelBottom.GetChildAt(0).GetChildAt(1))).AutoSize = false;
            // 
            // pictureLetter
            // 
            this.pictureLetter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureLetter.Image = global::GUI.Properties.Resources.Word_add;
            this.pictureLetter.Location = new System.Drawing.Point(37, 0);
            this.pictureLetter.Name = "pictureLetter";
            this.pictureLetter.Size = new System.Drawing.Size(32, 118);
            this.pictureLetter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureLetter.TabIndex = 5;
            this.pictureLetter.TabStop = false;
            // 
            // pictureEmail
            // 
            this.pictureEmail.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEmail.Image = global::GUI.Properties.Resources.Email;
            this.pictureEmail.Location = new System.Drawing.Point(5, 0);
            this.pictureEmail.Name = "pictureEmail";
            this.pictureEmail.Size = new System.Drawing.Size(32, 118);
            this.pictureEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureEmail.TabIndex = 4;
            this.pictureEmail.TabStop = false;
            // 
            // PersonDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.radPanelTop);
            this.Controls.Add(this.radPanelMiddle);
            this.Name = "PersonDetailView";
            this.Size = new System.Drawing.Size(283, 500);
            ((System.ComponentModel.ISupportInitialize)(this.imageProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelFullname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelTop)).EndInit();
            this.radPanelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanelMiddle)).EndInit();
            this.radPanelMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelBottom)).EndInit();
            this.radPanelBottom.ResumeLayout(false);
            this.radPanelBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imageProfile;
        private Telerik.WinControls.UI.RadLabel labelFullname;
        private Telerik.WinControls.UI.RadPanel radPanelTop;
        private Telerik.WinControls.UI.RadPanel radPanelMiddle;
        private Telerik.WinControls.UI.RadListControl radListControl1;
        private Telerik.WinControls.RootRadElement object_5802c056_22e9_4971_9f6c_416719d87ee7;
        private Telerik.WinControls.UI.RadPanel radPanelBottom;
        private System.Windows.Forms.PictureBox pictureLetter;
        private System.Windows.Forms.PictureBox pictureEmail;
    }
}
