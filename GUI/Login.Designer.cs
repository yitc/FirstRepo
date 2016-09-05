namespace GUI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtPassword = new Telerik.WinControls.UI.RadTextBox();
            this.lblBack = new System.Windows.Forms.Label();
            this.panelControls = new System.Windows.Forms.Panel();
            this.lblDatabase = new Telerik.WinControls.UI.RadLabel();
            this.lblProduct = new Telerik.WinControls.UI.RadLabel();
            this.cboLang = new Telerik.WinControls.UI.RadDropDownList();
            this.txtUsername = new Telerik.WinControls.UI.RadTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.picPassword = new System.Windows.Forms.PictureBox();
            this.picUser = new System.Windows.Forms.PictureBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.donutShape1 = new Telerik.WinControls.Tests.DonutShape();
            this.object_3f0e694a_40d1_4d8b_8bae_44be54108c75 = new Telerik.WinControls.RootRadElement();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).BeginInit();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            // 
            // 
            // 
            this.txtPassword.RootElement.AccessibleDescription = null;
            this.txtPassword.RootElement.AccessibleName = null;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtPassword.GetChildAt(0).GetChildAt(0))).NullText = "Password";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtPassword.GetChildAt(0).GetChildAt(0))).NullTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtPassword.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtPassword.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtPassword.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtPassword.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtPassword.GetChildAt(0).GetChildAt(2))).BottomColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtPassword.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.White;
            // 
            // lblBack
            // 
            this.lblBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            resources.ApplyResources(this.lblBack, "lblBack");
            this.lblBack.Name = "lblBack";
            // 
            // panelControls
            // 
            resources.ApplyResources(this.panelControls, "panelControls");
            this.panelControls.Controls.Add(this.lblDatabase);
            this.panelControls.Controls.Add(this.lblProduct);
            this.panelControls.Controls.Add(this.cboLang);
            this.panelControls.Controls.Add(this.txtUsername);
            this.panelControls.Controls.Add(this.btnLogin);
            this.panelControls.Controls.Add(this.picPassword);
            this.panelControls.Controls.Add(this.txtPassword);
            this.panelControls.Controls.Add(this.picUser);
            this.panelControls.Controls.Add(this.lblBack);
            this.panelControls.Controls.Add(this.picLogo);
            this.panelControls.Name = "panelControls";
            // 
            // lblDatabase
            // 
            resources.ApplyResources(this.lblDatabase, "lblDatabase");
            this.lblDatabase.Name = "lblDatabase";
            // 
            // 
            // 
            this.lblDatabase.RootElement.AccessibleDescription = null;
            this.lblDatabase.RootElement.AccessibleName = null;
            // 
            // lblProduct
            // 
            resources.ApplyResources(this.lblProduct, "lblProduct");
            this.lblProduct.Name = "lblProduct";
            // 
            // cboLang
            // 
            resources.ApplyResources(this.cboLang, "cboLang");
            this.cboLang.BackColor = System.Drawing.Color.White;
            this.cboLang.Name = "cboLang";
            this.cboLang.ThemeName = "VisualStudio2012Light";
            this.cboLang.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboLang_SelectedIndexChanged);
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.cboLang.GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadDropDownTextBoxElement)(this.cboLang.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0))).Alignment = ((System.Drawing.ContentAlignment)(resources.GetObject("resource.Alignment")));
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.Name = "txtUsername";
            // 
            // 
            // 
            this.txtUsername.RootElement.AccessibleDescription = null;
            this.txtUsername.RootElement.AccessibleName = null;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtUsername.GetChildAt(0).GetChildAt(0))).NullText = "Username";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtUsername.GetChildAt(0).GetChildAt(0))).NullTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtUsername.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtUsername.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtUsername.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtUsername.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtUsername.GetChildAt(0).GetChildAt(2))).BottomColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtUsername.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.White;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            this.btnLogin.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // picPassword
            // 
            this.picPassword.BackColor = System.Drawing.Color.White;
            this.picPassword.BackgroundImage = global::GUI.Properties.Resources.password;
            resources.ApplyResources(this.picPassword, "picPassword");
            this.picPassword.Name = "picPassword";
            this.picPassword.TabStop = false;
            // 
            // picUser
            // 
            this.picUser.BackColor = System.Drawing.Color.White;
            this.picUser.BackgroundImage = global::GUI.Properties.Resources.user;
            resources.ApplyResources(this.picUser, "picUser");
            this.picUser.Name = "picUser";
            this.picUser.TabStop = false;
            // 
            // picLogo
            // 
            resources.ApplyResources(this.picLogo, "picLogo");
            this.picLogo.Name = "picLogo";
            this.picLogo.TabStop = false;
            // 
            // object_3f0e694a_40d1_4d8b_8bae_44be54108c75
            // 
            this.object_3f0e694a_40d1_4d8b_8bae_44be54108c75.Name = "object_3f0e694a_40d1_4d8b_8bae_44be54108c75";
            this.object_3f0e694a_40d1_4d8b_8bae_44be54108c75.Shape = null;
            this.object_3f0e694a_40d1_4d8b_8bae_44be54108c75.StretchHorizontally = true;
            this.object_3f0e694a_40d1_4d8b_8bae_44be54108c75.StretchVertically = true;
            // 
            // Login
            // 
            this.AcceptButton = this.btnLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(169)))), ((int)(((byte)(234)))));
            this.Controls.Add(this.panelControls);
            this.Name = "Login";
            // 
            // 
            // 
            this.RootElement.AccessibleDescription = null;
            this.RootElement.AccessibleName = null;
            this.RootElement.ApplyShapeToControl = true;
            this.ThemeName = "VisualStudio2012Light";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPassword;
        private Telerik.WinControls.UI.RadTextBox txtPassword;
        private System.Windows.Forms.PictureBox picUser;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panelControls;
        private Telerik.WinControls.Tests.DonutShape donutShape1;
        private System.Windows.Forms.Button btnLogin;
        private Telerik.WinControls.RootRadElement object_3f0e694a_40d1_4d8b_8bae_44be54108c75;
        private Telerik.WinControls.UI.RadTextBox txtUsername;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadDropDownList cboLang;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadLabel lblProduct;
        private Telerik.WinControls.UI.RadLabel lblDatabase;
    }
}