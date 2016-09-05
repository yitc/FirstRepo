using BIS.Business;
using BIS.Core;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmUsers : frmTemplate
      //  Telerik.WinControls.UI.RadForm
    {
        UsersAllModel User;
        private UsersAllBUS _usersAllBUS;
        private UsersBUS _usersBUS;
        List<LanguagesModel> _languagesModel;
        List<IModel> _roleModel;
        List<RoleSecurityModel> menuSecurity;
        List<RoleSecurityModel> menuUserSecurity;
        private CompanyBUS _companyBUS;
        public static List<CompanyModel> _companyModelList;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        List<TranslateUstrModel> translate;
        private UserDAO _usersDAO;
        int idEmpl = -1;
        int idComp = -1;
        GridViewTemplate secondChildtemplate;
        GridViewTemplate secondChildtemplateU;
        GridViewTemplate template;
        GridViewRelation relation;
        GridViewRelation relation2;
        GridViewTemplate templateU;
        GridViewRelation relationU;
        GridViewRelation relation2U;
        Boolean isHeader = false;
        bool isSubmenu = false;
        List<RoleSecurityModel> submenu = new List<RoleSecurityModel>();
        List<MenuRoleModel> submenuR = new List<MenuRoleModel>();
        bool isRoleSubmenu = false;

        private SqlConnection conn;
        
        public frmUsers(IModel model)
        {
            User= (UsersAllModel) model;
            
            InitializeComponent();
        }
        public frmUsers()
        {
            InitializeComponent();
        }
        public void SaveModel(string username, string password, int iduser, string emailUser, DateTime changingPass, decimal numVarning, decimal passDuration, bool logInUser, DateTime logOnTime, DateTime logOffTime, bool notActiveUser, bool isFinishCalculation, bool manager, bool firstStartApp, bool isAccountUser, bool isAccountManager)
        {
            
            User.username = username;
            User.password = password;
            User.emailUser = emailUser;


            User.dtPassChanged = changingPass;
            if (passDuration != -1)
                User.numDaysPassValid = passDuration;
            else
                User.numDaysPassValid = null;

            if (numVarning != -1)
                User.numDaysStartWarn = numVarning;
            else
                User.numDaysStartWarn = null;

            User.dtUserLogin = logOnTime;

            User.dtUserLogout = logOffTime;

            if (notActiveUser == true)
                User.isNotActive= true;
            else
                User.isNotActive = false;

            if (isFinishCalculation == true)
                User.isFinishCalculation = true;
            else
                User.isFinishCalculation = false;

            if (manager == true)
                User.isUserManager = true;
            else
                User.isUserManager = false;

            if (firstStartApp == true)
                User.isFirstTimeStarted = true;
            else
                User.isFirstTimeStarted = false;
            if (isAccountUser == true)
                User.isAccountUser = true;
            else
                User.isAccountUser = false;
            if (isAccountManager == true)
                User.isAccountManager = true;
            else
                User.isAccountManager = false;

        }
        private void UpdateUserData() 
        {
            _usersDAO = new UserDAO();
            string username = txtUserName.Text;
            string password =  _usersDAO.CryptPass(txtPass.Text);
            int iduser = Convert.ToInt16(txtID.Text);
            string emailUser = txtEmail.Text;
            string fullName = "";
            if (txtemployee.Text != "")
            {
                fullName = txtemployee.Text;
            }
            else
            {
                fullName = txtUserFullName.Text;
            }

            DateTime changingPass;
            changingPass = Convert.ToDateTime(dtChangingPass.Text);

            decimal numVarning = -1;
            numVarning = (txtWarningDays.Text == null || txtWarningDays.Text == "") ? numVarning : Convert.ToDecimal(txtWarningDays.Text);
            decimal passDuration = -1;
            passDuration = (txtPassDuration.Text == null || txtPassDuration.Text == "") ? passDuration : Convert.ToDecimal(txtPassDuration.Text);

         //   int idEmplo = idEmpl;
            int idEmplo=-1;
            if(txtidEmployee.Text!="")
             idEmplo = Convert.ToInt32(txtidEmployee.Text);

            bool logInUser = chkLogInUser.Checked;
            
            DateTime logOnTime = Convert.ToDateTime(dtLogOnTime.Text);
            DateTime logOffTime = Convert.ToDateTime(dtLogOffTime.Text);

            bool notActiveUser = chkNotActive.Checked;
            bool isFinishCalculation = chkFinishCalculation.Checked;
            bool manager = chkManager.Checked;
            bool firstStartApp = chkFirstStartApp.Checked;
            bool isAccountUser = chkAccountUser.Checked;
            bool isDontSeeMedVol = chkDontSeeMedVol.Checked;
            bool isAccountManager = chkAccountManager.Checked;

            int i = Convert.ToInt32(ddlLanguage.SelectedValue);
            string language= _languagesModel[i - 1].nameLang;
            
            if (txtemployee.Text != "")
            {
                fullName = txtemployee.Text;
            }

            User.username = username;
            User.password =_usersDAO.DecryptPass(password);
            User.nameUser = fullName;
            User.emailUser = emailUser;
         
            User.dtPassChanged = changingPass;
            if (passDuration != -1)
                User.numDaysPassValid = passDuration;
            else
                User.numDaysPassValid = null;

            if (numVarning != -1)
                User.numDaysStartWarn = numVarning;
            else
                User.numDaysStartWarn = null;

            User.dtUserLogin = logOnTime;

            if (logInUser == true)
                User.isUserLogin = true;
            else
                User.isUserLogin = false;

            if (notActiveUser == true)
                User.isNotActive = true;
            else
                User.isNotActive = false;

            if (isFinishCalculation == true)
                User.isFinishCalculation= true;
            else
                User.isFinishCalculation = false;

            if (manager == true)
                User.isUserManager = true;
            else
                User.isUserManager = false;

            if (firstStartApp == true)
                User.isFirstTimeStarted = true;
            else
                User.isFirstTimeStarted = false;

            if (isDontSeeMedVol == true)
                User.isDontSeeMedVol = true;
            else
                User.isDontSeeMedVol = false;

            if (isAccountUser == true)
                User.isAccountUser = true;
            else
                User.isAccountUser = false;

            if (isAccountManager == true)
                User.isAccountManager = true;
            else
                User.isAccountManager = false;

            if (idEmplo != -1)
                User.idEmployee = idEmplo;

            
           // int broj = Convert.ToInt32(ddlLanguage.SelectedValue);
         // User.lngUser = _languagesModel[broj-1].nameLang;
           User.lngUser= ddlLanguage.SelectedItem.ToString();
            
             _usersAllBUS = new UsersAllBUS();

             if (_usersAllBUS.UpdateUser(username, password, emailUser, idEmplo, logInUser, notActiveUser, isFinishCalculation, manager, firstStartApp, changingPass, logOnTime, logOffTime, passDuration, numVarning, iduser, fullName, language, isAccountUser, isDontSeeMedVol, isAccountManager, this.Name, Login._user.idUser) == false)
             {
                 translateRadMessageBox trs = new translateRadMessageBox();
                 trs.translateAllMessageBox("Something went wrong with updating!");
             }
             else
             {
                 translateRadMessageBox trs = new translateRadMessageBox();
                 trs.translateAllMessageBox("You updated data successfully!");
                 this.DialogResult = DialogResult.Yes;
                 this.Close();
             }
        
     
        }
        private void InsertUserData()
        {
            UserDAO _usersDAO = new UserDAO();
            string username = txtUserName.Text;
            string password = _usersDAO.CryptPass(txtPass.Text);
            string fullName = txtUserFullName.Text;
            
            string idRole = txtID.Text;
            string emailUser = txtEmail.Text;

            int idEmployee = idEmpl;
           
            DateTime changingPass;
            changingPass = Convert.ToDateTime(dtChangingPass.Text);

            decimal numVarning = -1;
            numVarning = (txtWarningDays.Text == null || txtWarningDays.Text == "-1" || txtWarningDays.Text == "") ? numVarning : Convert.ToDecimal(txtWarningDays.Text);
            decimal passDuration = -1;
            passDuration = (txtPassDuration.Text == null || txtPassDuration.Text == "") ? passDuration : Convert.ToDecimal(txtPassDuration.Text);

            bool logInUser = chkLogInUser.Checked;

            DateTime logOnTime = Convert.ToDateTime(dtLogOnTime.Text);
            DateTime logOffTime = Convert.ToDateTime(dtLogOffTime.Text);

            bool isNotActive = chkNotActive.Checked;
            bool isFinishCalculation = chkFinishCalculation.Checked;
            bool manager = chkManager.Checked;
            bool firstStartApp = chkFirstStartApp.Checked;
            bool isAccountUser = chkAccountUser.Checked;
            bool isDontSeeMedVol = chkDontSeeMedVol.Checked;
            bool isAccountManager = chkAccountManager.Checked;
           
            int i = Convert.ToInt32(ddlLanguage.SelectedValue);
            string language = _languagesModel[i - 1].nameLang;
            if (txtemployee.Text != "")
            {
                fullName = txtemployee.Text;
            }
             _companyBUS = new CompanyBUS();
          
            _companyModelList = new List<CompanyModel>();
          
            _companyModelList = _companyBUS.GetCompanyDetails();
           

            //if (_companyModelList != null)
            //{
            //    foreach (var model in _companyModelList)
            //    {

            //        int idComp = model.idCompany;
                   

            //    }
            //}
           int idComp = _companyModelList[0].idCompany;

            _usersAllBUS = new UsersAllBUS();
            if (txtID.Text != "" && txtID.Text != null)
            {
                if (fullName != "")
                {
                    if (_usersAllBUS.IsertUser(username, password, fullName, Convert.ToInt32(idRole), Convert.ToInt32(idEmpl), idComp, emailUser, logInUser, isNotActive, isFinishCalculation, manager, firstStartApp, changingPass, logOnTime, logOffTime, passDuration, numVarning, language, isAccountUser, isDontSeeMedVol, isAccountManager, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong with inserting!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted data successfully!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Full name requiered!");
                                
                }
            }
            else 
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have add role in tab menu!");
            }
         
        }
        private void FillRgvRole()
        {
            _usersBUS = new UsersBUS();

            menuUserSecurity = _usersBUS.MenuRoleSecurity(Login._user.lngUser, Convert.ToInt32(txtID.Text));
            rgvRole.DataSource = menuUserSecurity;
            rgvRole.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            rgvRole.AllowAddNewRow = false;
            rgvRole.AllowDeleteRow = false;
            //  rgvRole.AllowEditRow = false;
            rgvRole.Columns["nameMenu"].ReadOnly = true;
            rgvRole.Columns["nameSecurity"].ReadOnly = true;
            rgvRole.Columns["By"].IsVisible = false;

            template = new GridViewTemplate();
            template.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            template.AllowAddNewRow = false;
            template.AllowDeleteRow = false;
            template.AllowEditRow = false;
            _usersBUS = new UsersBUS();
            template.DataSource = _usersBUS.GetSubMenus(Login._user.lngUser, Convert.ToInt32(txtIdRole.Text));
            if (template.DataSource != null)
            {
                isSubmenu = true;
                submenuR = _usersBUS.GetSubMenus(Login._user.lngUser, Convert.ToInt32(txtIdRole.Text));
            }
            rgvRole.MasterTemplate.Templates.Add(template);

            relation = new GridViewRelation(rgvRole.MasterTemplate);
            relation.ChildTemplate = template;
            relation.RelationName = "MenusSubMenus";
            relation.ParentColumnNames.Add("idMenu");
            relation.ChildColumnNames.Add("idMenuSuperior");
            rgvRole.Relations.Add(relation);

            secondChildtemplate = new GridViewTemplate();
            secondChildtemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            secondChildtemplate.AllowAddNewRow = false;
            secondChildtemplate.AllowDeleteRow = false;
            secondChildtemplate.AllowEditRow = false;
            secondChildtemplate.DataSource = template.DataSource;
            template.Templates.Add(secondChildtemplate);

            GridViewRelation relation2 = new GridViewRelation(template);
            relation2.ChildTemplate = secondChildtemplate;
            relation2.RelationName = "SubMenusSubMenus";
            relation2.ParentColumnNames.Add("idMenu");
            relation2.ChildColumnNames.Add("idMenuSuperior");
            rgvRole.Relations.Add(relation2);

            rgvRole.Columns["idMenu"].IsVisible = false;
            rgvRole.Columns["idMenuSuperior"].IsVisible = false;
            rgvRole.Columns["idSecurity"].IsVisible = false;
            template.Columns["idMenu"].IsVisible = false;
            template.Columns["idMenuSuperior"].IsVisible = false;
            template.Columns["idSecurity"].IsVisible = false;
            secondChildtemplate.Columns["idMenu"].IsVisible = false;
            secondChildtemplate.Columns["idMenuSuperior"].IsVisible = false;
            secondChildtemplate.Columns["idSecurity"].IsVisible = false;

            if (template.DataSource != null)
            {
                template.Columns["nameMenu"].MaxWidth = (int)(this.CreateGraphics().MeasureString(template.Columns["nameMenu"].HeaderText, this.Font).Width + 70);
                template.Columns["nameSecurity"].MaxWidth = (int)(this.CreateGraphics().MeasureString(template.Columns["nameSecurity"].HeaderText, this.Font).Width + 70);
            }
            if (secondChildtemplate.DataSource != null)
            {
                secondChildtemplate.Columns["nameMenu"].MaxWidth = (int)(this.CreateGraphics().MeasureString(secondChildtemplate.Columns["nameMenu"].HeaderText, this.Font).Width + 70);
                secondChildtemplate.Columns["nameSecurity"].MaxWidth = (int)(this.CreateGraphics().MeasureString(secondChildtemplate.Columns["nameSecurity"].HeaderText, this.Font).Width + 70);
            }
            for (int i = 0; i < rgvRole.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvRole.Columns[i].HeaderText != null)
                    {
                        rgvRole.Columns[i].HeaderText = resxSet.GetString(rgvRole.Columns[i].HeaderText);
                    }
                }

            }

        }
        private void FillRgvUser()
        { 
                 UsersBUS _usersBUS = new UsersBUS();

                menuUserSecurity = _usersBUS.MenuUserSecurity(Login._user.lngUser, Convert.ToInt32(txtID.Text));
                rgvUser.DataSource = menuUserSecurity;
                rgvUser.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                // rgvUser.AllowAddNewRow = false;
                rgvUser.AllowDeleteRow = true;
               // rgvUser.AllowEditRow = false;

                templateU = new GridViewTemplate();
                templateU.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                templateU.AllowAddNewRow = false;
                templateU.AllowDeleteRow = false;
                templateU.AllowEditRow = false;
                _usersBUS = new UsersBUS();
                templateU.DataSource = _usersBUS.GetSubMenusUser(Login._user.lngUser, Convert.ToInt32(txtID.Text));
                if (templateU.DataSource != null)
                {
                    isSubmenu = true;
                    submenu = _usersBUS.GetSubMenusUser(Login._user.lngUser, Convert.ToInt32(txtID.Text));
                }
                rgvUser.MasterTemplate.Templates.Add(templateU);

                GridViewRelation relationU = new GridViewRelation(rgvUser.MasterTemplate);
                relationU.ChildTemplate = templateU;
                relationU.RelationName = "MenusSubMenus";
                relationU.ParentColumnNames.Add("idMenu");
                relationU.ChildColumnNames.Add("idMenuSuperior");
                rgvUser.Relations.Add(relationU);

                secondChildtemplateU = new GridViewTemplate();
                secondChildtemplateU.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                secondChildtemplateU.AllowAddNewRow = false;
                secondChildtemplateU.AllowDeleteRow = false;
                secondChildtemplateU.AllowEditRow = false;
                secondChildtemplateU.DataSource = templateU.DataSource;
                templateU.Templates.Add(secondChildtemplateU);

                GridViewRelation relation2U = new GridViewRelation(templateU);
                relation2U.ChildTemplate = secondChildtemplateU;
                relation2U.RelationName = "SubMenusSubMenus";
                relation2U.ParentColumnNames.Add("idMenu");
                relation2U.ChildColumnNames.Add("idMenuSuperior");
                rgvUser.Relations.Add(relation2U);

                //rgvUser.Columns["idMenu"].IsVisible = false;
               // rgvUser.Columns["idMenuSuperior"].IsVisible = false;
              //  rgvUser.Columns["idSecurity"].IsVisible = false;
                templateU.Columns["idMenu"].IsVisible = false;
                templateU.Columns["idMenuSuperior"].IsVisible = false;
                templateU.Columns["idSecurity"].IsVisible = false;
                templateU.Columns["By"].IsVisible=false;
                secondChildtemplateU.Columns["idMenu"].IsVisible = false;
                secondChildtemplateU.Columns["idMenuSuperior"].IsVisible = false;
                secondChildtemplateU.Columns["idSecurity"].IsVisible = false;
                secondChildtemplateU.Columns["By"].IsVisible=false;

                if (templateU.DataSource != null)
                {
                    templateU.Columns["nameMenu"].MaxWidth = (int)(this.CreateGraphics().MeasureString(templateU.Columns["nameMenu"].HeaderText, this.Font).Width + 70);
                    templateU.Columns["nameSecurity"].MaxWidth = (int)(this.CreateGraphics().MeasureString(templateU.Columns["nameSecurity"].HeaderText, this.Font).Width + 70);
                }
                if (secondChildtemplateU.DataSource != null)
                {
                    secondChildtemplateU.Columns["nameMenu"].MaxWidth = (int)(this.CreateGraphics().MeasureString(secondChildtemplateU.Columns["nameMenu"].HeaderText, this.Font).Width + 70);
                    secondChildtemplateU.Columns["nameSecurity"].MaxWidth = (int)(this.CreateGraphics().MeasureString(secondChildtemplateU.Columns["nameSecurity"].HeaderText, this.Font).Width + 70);
                }
        
        }
        private void FillUserData()
        {
            txtID.Text = User.idUser.ToString();
            txtIdRole.Text = User.idRole.ToString();

            txtUserFullName.Text = User.nameUser.ToString();

            if (User.nameEmployee != null && User.nameEmployee!="")
            {
                txtUserFullName.Text = User.nameEmployee;
            }
            else
                txtUserFullName.Text = User.nameUser.ToString();
            txtUserName.Text = User.username.ToString();
            txtPass.Text = User.password.ToString();
            txtEmail.Text = User.emailUser.ToString();
            if (User.dtPassChanged != null)
            dtChangingPass.Text = User.dtPassChanged.ToString();
            txtPassDuration.Text = User.numDaysPassValid.ToString();
            txtWarningDays.Text = User.numDaysStartWarn.ToString();
            if (User.isUserLogin.Value == true)
                chkLogInUser.Checked = true;
            if(User.dtUserLogin!=null)
            dtLogOnTime.Text = User.dtUserLogin.ToString();
            if (User.dtUserLogout != null)
            dtLogOffTime.Text = User.dtUserLogout.ToString();

            txtemployee.Text = User.nameEmployee;
            txtidEmployee.Text = User.idEmployee.ToString();

            //if (User.idEmployee != 0 || User.idEmployee!=null)
            //    chkUserEmloyee.Checked = true;
            //else
            //    chkUserEmloyee.Checked = false;

            if (User.isNotActive.Value == true)
                chkNotActive.Checked = true;

            if (User.isFinishCalculation.Value == true)
                chkFinishCalculation.Checked = true;

            if (User.isUserManager.Value == true)
                chkManager.Checked = true;

            if (User.isFirstTimeStarted.Value == true)
                chkFirstStartApp.Checked = true;

            if (User.isAccountUser == true)
                chkAccountUser.Checked = true;

            if (User.isDontSeeMedVol == true)
                chkDontSeeMedVol.Checked = true;

            if (User.isAccountManager == true)
                chkAccountManager.Checked = true;
            


            UsersBUS ub = new UsersBUS();
            _languagesModel = ub.GetAllLanguages();
            ddlLanguage.DataSource = _languagesModel;
            ddlLanguage.DisplayMember = "nameLang";
            ddlLanguage.ValueMember = "idLang";
            
            int i = Convert.ToInt32(ddlLanguage.SelectedValue);

            //string language = User.lngUser.ToString();
            //if(language!="")
            //ddlLanguage.SelectedText = User.lngUser;
            string language = User.lngUser.ToString();
            if (language != "")
                ddlLanguage.SelectedIndex = _languagesModel.FindIndex(s => s.nameLang.ToString().TrimEnd() == language.TrimEnd());
            
        }
        private void frmUsers_Load(object sender, EventArgs e)
        {
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;

            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;            
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTravelpapers.Visibility = ElementVisibility.Collapsed;

            radRibbonReports.Visibility = ElementVisibility.Visible;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Visible;
            
            setTranslation();

            UsersBUS ub = new UsersBUS();
            _languagesModel = ub.GetAllLanguages();
            ddlLanguage.DataSource = _languagesModel;
            ddlLanguage.DisplayMember = "nameLang";
            ddlLanguage.ValueMember = "idLang";
            if (User == null)
                 {
                ddlLanguage.SelectedIndex = 1;
                chkFirstStartApp.Checked = true;
                txtPassDuration.Text = "90";
                txtWarningDays.Text = "5";
                dtChangingPass.Value = DateTime.Now;
                 }
            this.rgvRole.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvRole_DataBindingComplete);
            this.rgvUser.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvUser_DataBindingComplete);

            if (User != null)

                FillUserData();
           

            #region CheckRole
            //load panela role na tabu Role
            RoleBUS rolebus = new RoleBUS();
            _roleModel =  rolebus.GetAllRole();
            List<string> roleString = new List<string>();
            int Y = 5;
            RadRadioButton rchk = new RadRadioButton();

            for (int i = 0; i < _roleModel.Count; i++)
            {
                RoleModel rm = (RoleModel)_roleModel[i];
                rchk = new RadRadioButton();
                rchk.Font = new Font("Verdana", 9);
                rchk.ThemeName = pageUser.ThemeName;
                rchk.Name = "chkRole" + rm.idRole.ToString();
                rchk.Text = rm.nameRole;
                rchk.Location = new Point(5, Y);
                rchk.AutoSize = true;

                rchk.Click += rchk_CheckStateChanged;
                Y = Y + 3 + rchk.Height;
                panelRole.Controls.Add(rchk);
                roleString.Add((rchk.Name).Substring(3));
            }


            if (User != null)
            {
                for (int i = 0; i < roleString.Count; i++)
                {
                    RadRadioButton chk = (RadRadioButton)panelRole.Controls.Find("chkRole" + User.idRole.ToString(), true)[0];
                    chk.CheckState = CheckState.Checked;
                }
            #endregion CheckRole


                # region loadGridaUser

                FillRgvUser();
               
                List<TypesMenuModel> tmmU1 = ub.GetAllMenu();
                GridViewComboBoxColumn ddlU1 = new GridViewComboBoxColumn();

                ddlU1.DataSource = tmmU1;
                ddlU1.DisplayMember = "nameMenu";
                ddlU1.ValueMember = "idMenu";
                ddlU1.FieldName = "idMenu";
                ddlU1.Name = "nameMenuU";
                ddlU1.HeaderText = "Name menu";

                ////ddlU1.MaxWidth = (int)(this.CreateGraphics().MeasureString(ddlU1.HeaderText, this.Font).Width + 73);
                ////ddlU1.Width = ddlU1.MaxWidth;
                ////ddlU1.MinWidth = ddlU1.MaxWidth;

                GridViewComboBoxColumn ddlU2 = new GridViewComboBoxColumn();
                List<TypesSecurityModel> tmmU2 = ub.GetAllSecurity();

                ddlU2.DataSource = tmmU2;
                ddlU2.DisplayMember = "nameSecurity";
                ddlU2.ValueMember = "idSecurity";
                ddlU2.FieldName = "idSecurity";
                ddlU2.Name = "nameSecurityU";
                ddlU2.HeaderText = "Name security";

                ////ddlU2.MaxWidth = (int)(this.CreateGraphics().MeasureString(ddlU2.HeaderText, this.Font).Width + 73);
                ////ddlU2.Width = ddlU2.MaxWidth;
                ////ddlU2.MinWidth = ddlU2.MaxWidth;

                GridViewComboBoxColumn ddlU3 = new GridViewComboBoxColumn();
                List<UserRoleModel> rumU = new List<UserRoleModel>();

                UserRoleModel urmU1 = new UserRoleModel();

                urmU1.by = "User";

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    urmU1.by = resxSet.GetString(urmU1.by);
                }

                rumU.Add(urmU1);

                ddlU3.DataSource = rumU;

                ddlU3.DisplayMember = "by";
                ddlU3.ValueMember = "by";
                ddlU3.FieldName = "by";
                ddlU3.Name = "byComboU";
                ddlU3.HeaderText = "By";

                rgvUser.Columns.Add(ddlU1);
                rgvUser.Columns.Add(ddlU2);
                rgvUser.Columns.Add(ddlU3);

                ddlU1.MaxWidth = (int)(this.CreateGraphics().MeasureString(ddlU1.HeaderText, this.Font).Width + 85);
                ddlU1.Width = ddlU1.MaxWidth;
                ddlU1.MinWidth = ddlU1.MaxWidth;

                ddlU2.MaxWidth = (int)(this.CreateGraphics().MeasureString(ddlU2.HeaderText, this.Font).Width + 76);
                ddlU2.Width = ddlU2.MaxWidth;
                ddlU2.MinWidth = ddlU2.MaxWidth;

                ddlU3.MaxWidth = (int)(this.CreateGraphics().MeasureString(ddlU3.HeaderText, this.Font).Width + 79);
                ddlU3.Width = ddlU3.MaxWidth;
                ddlU3.MinWidth = ddlU3.MaxWidth;

              

                rgvUser.Columns.Move(rgvUser.Columns["nameMenuU"].Index, 0);
                rgvUser.Columns.Move(rgvUser.Columns["byComboU"].Index, 1);
                rgvUser.Columns.Move(rgvUser.Columns["nameSecurityU"].Index, 2);

                this.rgvUser.Columns["nameMenuU"].ReadOnly = true;
                this.rgvUser.Columns["idMenu"].IsVisible = false;
                this.rgvUser.Columns["idSecurity"].IsVisible = false;
                this.rgvUser.Columns["by"].IsVisible = false;
                this.rgvUser.Columns["nameMenu"].IsVisible = false;
                this.rgvUser.Columns["nameSecurity"].IsVisible = false;
                this.rgvUser.Columns["idMenuSuperior"].IsVisible = false;
           

                    for (int i = 0; i < rgvUser.Columns.Count; i++)
                    {

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (rgvUser.Columns[i].HeaderText != null && rgvUser.Columns[i].HeaderText != "")
                            {
                                rgvUser.Columns[i].HeaderText = resxSet.GetString(rgvUser.Columns[i].HeaderText);
                            }

                        }

                    }
                # endregion

                #region loadGridaRole
                // load griga Role
                           
                FillRgvRole();
             
                GridViewComboBoxColumn ddl3 = new GridViewComboBoxColumn();
                List<UserRoleModel> rum = new List<UserRoleModel>();

                UserRoleModel urm1 = new UserRoleModel();

                urm1.by = "User";

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    urm1.by = resxSet.GetString(urm1.by);
                }

                rum.Add(urm1);
                UserRoleModel urm2 = new UserRoleModel();

                urm2.by = "Role";

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    urm2.by = resxSet.GetString(urm2.by);

                }

                rum.Add(urm2);
                ddl3.DataSource = rum;

                ddl3.DisplayMember = "by";
                ddl3.ValueMember = "by";
                ddl3.FieldName = "by";
                ddl3.Name = "byCombo";
                ddl3.HeaderText = "By";

             
                rgvRole.Columns.Add(ddl3);

                ddl3.MaxWidth = (int)(this.CreateGraphics().MeasureString(ddl3.HeaderText, this.Font).Width + 79);
                ddl3.Width = ddl3.MaxWidth;
                ddl3.MinWidth = ddl3.MaxWidth;


                rgvRole.Columns["nameMenu"].MaxWidth = (int)(this.CreateGraphics().MeasureString(rgvRole.Columns["nameMenu"].HeaderText, this.Font).Width + 85);
                rgvRole.Columns["nameMenu"].Width = rgvRole.Columns["nameMenu"].MaxWidth;
                rgvRole.Columns["nameMenu"].MinWidth = rgvRole.Columns["nameMenu"].MaxWidth;

                rgvRole.Columns["nameSecurity"].MaxWidth = (int)(this.CreateGraphics().MeasureString(rgvRole.Columns["nameSecurity"].HeaderText, this.Font).Width + 58);
                rgvRole.Columns["nameSecurity"].Width = rgvRole.Columns["nameSecurity"].MaxWidth;
                rgvRole.Columns["nameSecurity"].MinWidth = rgvRole.Columns["nameSecurity"].MaxWidth;



                rgvRole.Columns.Move(rgvRole.Columns["nameMenu"].Index, 0);
                rgvRole.Columns.Move(rgvRole.Columns["byCombo"].Index, 1);
                rgvRole.Columns.Move(rgvRole.Columns["nameSecurity"].Index, 2);



               

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (ddl3.HeaderText != null && ddl3.HeaderText != "")
                        {
                            ddl3.HeaderText = resxSet.GetString(ddl3.HeaderText);
                        }

                    }

                
                # endregion
            }

        }


        private void rchk_CheckStateChanged(object sender, EventArgs e)
        {

            RadRadioButton radchk = (RadRadioButton)sender;


            if (radchk != null)
            {
                {
                    _usersBUS = new UsersBUS();
                    if (User != null)
                    {

                        if (_usersBUS.IdRoleUpdate(Convert.ToInt32(User.idUser), Convert.ToInt32(radchk.Name.Substring(7)), this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with updating!");
                        }
                        else
                        {
                            User.idRole = Convert.ToInt32(radchk.Name.Substring(7));
                            txtIdRole.Text = User.idRole.ToString();
                            this.DialogResult = DialogResult.Yes;

                        }

                    }
                    else
                    {
                        txtID.Text = radchk.Name.Substring(7).ToString();
                    }

                }
                if (User != null)
                {
                   
                    rgvRole.DataSource = new List<RoleSecurityModel>();
                    if (secondChildtemplate != null)
                        secondChildtemplate.DataSource = new List<RoleSecurityModel>();
                    if (template != null)
                        template.DataSource = new List<RoleSecurityModel>();
                    for (int i = 0; i < rgvRole.MasterTemplate.Templates.Count; i++)
                    {
                        rgvRole.MasterTemplate.Templates.Remove(rgvRole.MasterTemplate.Templates[i]);
                    }
                    FillRgvRole();

                }
            }
        } 


        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (lblUserName.Text!=null)
                    lblUserName.Text = resxSet.GetString(lblUserName.Text);
                if (lblPass.Text != null)
                    lblPass.Text = resxSet.GetString(lblPass.Text);
                if (lblEmail.Text != null)
                lblEmail.Text = resxSet.GetString(lblEmail.Text);
                 if (label1.Text != null)
                label1.Text = resxSet.GetString(label1.Text);
                 if (lblPassDuration.Text != null)
                     lblPassDuration.Text = resxSet.GetString(lblPassDuration.Text);
                 if (lblWarningDays.Text != null)
                lblWarningDays.Text = resxSet.GetString(lblWarningDays.Text);
                 if (lblLoginUser.Text != null)
                lblLoginUser.Text = resxSet.GetString(lblLoginUser.Text);
                 if (lblLogOnTime.Text != null)
                lblLogOnTime.Text = resxSet.GetString(lblLogOnTime.Text);
                 if (lblLogOffTime.Text != null)
                lblLogOffTime.Text = resxSet.GetString(lblLogOffTime.Text);

                 if (resxSet.GetString(chkNotActive.Text) != null)
                    chkNotActive.Text = resxSet.GetString(chkNotActive.Text);

                 if (resxSet.GetString(chkFinishCalculation.Text) != null)
                    chkFinishCalculation.Text = resxSet.GetString(chkFinishCalculation.Text);

                 if (resxSet.GetString(chkFirstStartApp.Text) != null)
                    chkFirstStartApp.Text = resxSet.GetString(chkFirstStartApp.Text);

                 if (resxSet.GetString(chkAccountManager.Text) != null)
                     chkAccountManager.Text = resxSet.GetString(chkAccountManager.Text);

                 if (chkManager.Text != null)
                chkManager.Text = resxSet.GetString(chkManager.Text);
                 if (chkAccountUser.Text != null)
                chkAccountUser.Text = resxSet.GetString(chkAccountUser.Text);
                 if (btnSave.Text != null)
                btnSave.Text = resxSet.GetString(btnSave.Text);
                 if (this.Text != null)
                this.Text = resxSet.GetString(this.Text);
                 if (ribbonTab1.Text != null)
                ribbonTab1.Text = resxSet.GetString(ribbonTab1.Text);
                 if (btnReport.Text != null)
                btnReport.Text = resxSet.GetString(btnReport.Text);
                 if (lblUserFullName.Text != null)
                lblUserFullName.Text = resxSet.GetString(lblUserFullName.Text);
                 if (lblEmployee.Text != null)
                lblEmployee.Text = resxSet.GetString(lblEmployee.Text);
                 if (lblLanguage.Text != null)
                     lblLanguage.Text = resxSet.GetString(lblLanguage.Text);
                for (int i = 0; i < pageUser.Pages.Count; i++)
                {
                    if (resxSet.GetString(pageUser.Pages[i].Text) != null)
                        pageUser.Pages[i].Text = resxSet.GetString(pageUser.Pages[i].Text);
                }
               
                              

            }
            }

        private void rgvRole_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
             var grid = sender as RadGridView;
            foreach (var column in rgvRole.Columns)
            {
                if (column.HeaderText != null  )
                {
                    try
                    {
                        dictionary.Add(column.HeaderText, column.FieldName);
                    
                    }
                    catch
                    {
                        continue;
                    }

                    column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 62);
                    column.Width = column.MaxWidth;
                    column.MinWidth = column.MaxWidth;
                }
            }
           
        }
        private void rgvUser_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                if (column.HeaderText != null)
                {
                 //   dictionary.Add(column.HeaderText, column.FieldName);


                    column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 62);
                    column.Width = column.MaxWidth;
                    column.MinWidth = column.MaxWidth;
                }

                for (int i = 0; i < rgvUser.Columns.Count; i++)
                {

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvUser.Columns[i].HeaderText != null)
                        {
                            rgvUser.Columns[i].HeaderText = resxSet.GetString(rgvUser.Columns[i].HeaderText);
                        }
                    }

                }
            }
        }
        public bool IsInRoleSecurity(int idMenija)
        {
            bool isContain = false;
            UsersBUS _usersBUS = new UsersBUS();

            menuSecurity = _usersBUS.IsInRoleSecurity(Login._user.lngUser, User.idUser);
            for (int i = 0; i < menuSecurity.Count; i++)
            {
                if(menuSecurity[i].idMenu.ToString()!="") 
                {
                if (Convert.ToInt32(menuSecurity[i].idMenu.ToString()) == idMenija)
                {
                    isContain = true;
                    break;
                }
                else isContain = false;

            }

            }
            return isContain;
        }

        public bool IsMenuInList(int idMenija)
        {
            bool isContain = false;
            UsersBUS _usersBUS = new UsersBUS();

            menuSecurity = _usersBUS.GetAllRoleSecurity(Login._user.lngUser, User.idUser);
            for (int i = 0; i < rgvRole.Rows.Count; i++)
            {

                if (Convert.ToInt32(menuSecurity[i].idMenu.ToString()) == idMenija)
                {
                    isContain = true;
                    break;
                }
                else isContain = false;

            }

            return isContain;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (User != null)
            {
                UpdateUserData();

            }
            else
            {           
                InsertUserData();
            }

        }
     


        private void RgvRole_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                UsersBUS ub = new UsersBUS();
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged)
                {
                    // promena vrednosti kolone [By] u "User"
                    GridViewDataRowInfo newRow = e.NewItems[0] as GridViewDataRowInfo;
                    RoleSecurityModel rsm = new RoleSecurityModel();
                    rsm.idMenu = Convert.ToInt32(newRow.Cells["idMenu"].Value.ToString());

                    translate = ub.Translate("User", Login._user.lngUser);
                    string a = translate[0].stringValue;


                    if (newRow.Cells["byCombo"].Value.ToString() == a)
                    {
                        newRow.IsVisible = false;
                        if (ub.InsertMenuSecurity(Convert.ToInt32(newRow.Cells["idMenu"].Value), Convert.ToInt32(newRow.Cells["idSecurity"].Value), Convert.ToInt32(txtID.Text), this.Name, Login._user.idUser) == true)
                        {
                            //ako je menu prebacen iz rola ispituje da li ima podmenije i insertuje i njih

                            if (submenuR != null)
                            {
                                UsersBUS ubs = new UsersBUS();
                                for (int i = 0; i < submenuR.Count; i++)
                                {
                                    ubs.InsertMenuSecurity(Convert.ToInt32(submenuR[i].idMenu.ToString()), Convert.ToInt32(submenuR[i].idSecurity.ToString()), Convert.ToInt32(txtID.Text), this.Name, Login._user.idUser);


                                }
                            }


                            rgvUser.DataSource = new List<RoleSecurityModel>();
                            if (secondChildtemplateU != null)
                                secondChildtemplateU.DataSource = new List<RoleSecurityModel>();
                            if (templateU != null)
                                templateU.DataSource = new List<RoleSecurityModel>();
                            for (int i = 0; i < rgvUser.MasterTemplate.Templates.Count; i++)
                            {
                                rgvUser.MasterTemplate.Templates.Remove(rgvUser.MasterTemplate.Templates[i]);
                            }
                            FillRgvUser();
                        }

                        if (submenu != null)
                        {
                            for (int i = 0; i < submenu.Count; i++)
                            {
                                ub.InsertMenuSecurity(Convert.ToInt32(submenu[i].idMenu.ToString()), Convert.ToInt32(submenu[i].idSecurity.ToString()), Convert.ToInt32(txtID.Text), this.Name, Login._user.idUser);


                            }
                            rgvUser.DataSource = new List<MenuRoleModel>();
                            //   if (secondChildtemplate != null)
                            secondChildtemplateU.DataSource = new List<MenuRoleModel>();
                            // if (templateU != null)
                            templateU.DataSource = new List<MenuRoleModel>();
                            for (int i = 0; i < rgvUser.MasterTemplate.Templates.Count; i++)
                            {
                                rgvUser.MasterTemplate.Templates.Remove(rgvUser.MasterTemplate.Templates[i]);
                            }
                            FillRgvUser();
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rgvRole_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            try
            {
                UsersBUS mvb = new UsersBUS();
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                RoleSecurityModel rsm = new RoleSecurityModel();

                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                {
                    e.Cancel = true;


                }


                else if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    UsersBUS mvb1 = new UsersBUS();
                    if (mgvt.CurrentRow.Cells["nameMenu"].Value != null && mgvt.CurrentRow.Cells["nameSecurity"].Value != null)
                    {
                        e.Cancel = true;

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rgvRole_SelectionChanged(object sender, EventArgs e)
        {

            if (rgvRole.SelectedRows.Count > 0)
            {

            }
            //ako je u pitanju insert novog reda tada sve kolone moraju biti setovane da je readOnly=false
            else
            {
                //for (int i = 0; i < rgvRole.Columns.Count; i++)
                //{
                //    if (rgvRole.Columns[i].Name.ToString() == "nameMenu")
                //    {
                //        rgvRole.Columns["nameMenu"].ReadOnly = false;
                //    }
                //    if (rgvRole.Columns[i].Name.ToString() == "nameSecurity")
                //    {
                //        rgvRole.Columns["nameSecurity"].ReadOnly = false;
                //    }

                //    if (rgvRole.Columns[i].Name.ToString() == "byCombo")
                //    {
                //        rgvRole.Columns["byCombo"].ReadOnly = false;
                //    }
                //}



            }
        }

        private void RgvUser_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                UsersBUS ub = new UsersBUS();
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged)
                {
                    GridViewDataRowInfo newRow = e.NewItems[0] as GridViewDataRowInfo;

                    if (ub.MenuUserUpdate(Convert.ToInt32(newRow.Cells["idMenu"].Value.ToString()), Convert.ToInt32(txtID.Text), Convert.ToInt32(newRow.Cells["idSecurity"].Value.ToString()), this.Name, Login._user.idUser) != true)
                    {
                        rgvUser.CancelEdit();
                        rgvUser.DataSource = null;
                        UsersBUS _usersBUS = new UsersBUS();
                        menuUserSecurity = _usersBUS.MenuUserSecurity(Login._user.lngUser, User.idUser);
                        rgvUser.DataSource = menuUserSecurity;
                        this.rgvUser.Columns["idMenuSuperior"].IsVisible = false;
                        translate = _usersBUS.Translate("Role", Login._user.lngUser);
                        string a = translate[0].stringValue;


                        if (newRow.Cells["byCombo"].Value.ToString() == a)
                        {
                            // newRow.Cells["byCombo"].ReadOnly = false;
                            // rgvRole.Columns["byCombo"].ReadOnly = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void rgvUser_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                UsersBUS mvb = new UsersBUS();

                Type type = sender.GetType();
                GridViewRowInfo mgvt;


                if (type == typeof(GridViewRowInfo))
                {
                    mgvt = (GridViewRowInfo)sender;
                }
                else if (type == typeof(GridViewNewRowInfo))
                {
                    mgvt = (GridViewNewRowInfo)sender;
                }
                else if (type == typeof(MasterGridViewTemplate))
                {
                    MasterGridViewTemplate mm = (MasterGridViewTemplate)sender;
                    mgvt = mm.CurrentRow;
                }
                else if (type == typeof(GridViewHierarchyRowInfo))
                {
                    GridViewHierarchyRowInfo mm = (GridViewHierarchyRowInfo)sender;
                    mgvt = (GridViewHierarchyRowInfo)sender;
                }
                else
                {
                    mgvt = (GridViewRowInfo)sender;
                }


                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                {
                    UsersBUS uBUS = new UsersBUS();


                    if (mgvt.Cells["nameMenuU"].Value != null && mgvt.Cells["nameSecurityU"].Value != null)
                    {
                        if (IsInRoleSecurity(Convert.ToInt32(mgvt.Cells["nameMenuU"].Value)) != true)
                        {
                            if (mvb.InsertMenuSecurity(Convert.ToInt32(mgvt.Cells["nameMenuU"].Value), Convert.ToInt32(mgvt.Cells["nameSecurityU"].Value), Convert.ToInt32(txtID.Text), this.Name, Login._user.idUser) != true)
                            {
                                rgvUser.CancelEdit();
                            }
                            else
                            {

                                rgvUser.Columns["nameMenuU"].ReadOnly = true;
                                rgvUser.CancelEdit();
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Menu already exist in Roles!");
                            rgvUser.CancelEdit();
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have add name many and name security!");
                        e.Cancel = true;

                    }

                }


                else if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    UsersBUS mvb1 = new UsersBUS();
                    if (mgvt.Cells["nameMenuU"].Value != null && mgvt.Cells["nameSecurityU"].Value != null)
                    {
                        mgvt.Cells["nameMenuU"].ReadOnly = false;

                        if (mvb1.DeleteMenuUser(Convert.ToInt32(mgvt.Cells["nameMenuU"].Value), Convert.ToInt32(txtID.Text), Convert.ToInt32(mgvt.Cells["nameSecurityU"].Value), this.Name, Login._user.idUser) != true)
                        {
                            e.Cancel = true;
                            rgvUser.CancelEdit();
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with delete row!");

                        }

                        else
                        {
                            if (IsInRoleSecurity(Convert.ToInt32(mgvt.Cells["nameMenuU"].Value)) == true)
                            {
                                if (rgvRole.Rows.Count == 0)
                                {
                                    UsersBUS _uBUS = new UsersBUS();

                                    menuUserSecurity = _uBUS.MenuRoleSecurity(Login._user.lngUser, Convert.ToInt32(txtID.Text));
                                    rgvRole.DataSource = menuUserSecurity;

                                    _uBUS = new UsersBUS();

                                    rgvRole.DataSource = new List<RoleSecurityModel>();
                                    if (secondChildtemplate != null)
                                        secondChildtemplate.DataSource = new List<RoleSecurityModel>();
                                    if (template != null)
                                        template.DataSource = new List<RoleSecurityModel>();
                                    for (int i = 0; i < rgvRole.MasterTemplate.Templates.Count; i++)
                                    {
                                        rgvRole.MasterTemplate.Templates.Remove(rgvUser.MasterTemplate.Templates[i]);
                                    }
                                    FillRgvRole();
                                    //  this.rgvUser.Columns["idMenuSuperior"].IsVisible = false;

                                }
                                else
                                {
                                    for (int i = 0; i < rgvRole.Rows.Count; i++)
                                    {
                                        if ((rgvRole.Rows[i].Cells["idMenu"].Value).ToString() == (mgvt.Cells["nameMenuU"].Value).ToString())
                                        {
                                            translate = mvb1.Translate("Role", Login._user.lngUser);
                                            string a = translate[0].stringValue;
                                            rgvRole.Rows[i].IsVisible = true;
                                            rgvRole.Rows[i].Cells["byCombo"].Value = a;
                                            break;
                                        }
                                    }
                                }

                            }



                            if (submenu != null)
                            {
                                UsersBUS ub = new UsersBUS();
                                for (int i = 0; i < submenu.Count; i++)
                                {
                                    ub.DeleteMenuUser(Convert.ToInt32(submenu[i].idMenu.ToString()), Convert.ToInt32(txtID.Text), Convert.ToInt32(submenu[i].idSecurity.ToString()), this.Name, Login._user.idUser);


                                }
                            }

                            e.Cancel = true;
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You deleted row successfully!");
                            //rgvUser.DataSource = null;
                            //UsersBUS _usersBUS = new UsersBUS();
                            //menuUserSecurity = _usersBUS.MenuUserSecurity(Login._user.lngUser, User.idUser);
                            //rgvUser.DataSource = menuUserSecurity;
                            rgvUser.CancelEdit();
                            rgvUser.DataSource = new List<RoleSecurityModel>();
                            if (secondChildtemplateU != null)
                                secondChildtemplateU.DataSource = new List<RoleSecurityModel>();
                            if (templateU != null)
                                templateU.DataSource = new List<RoleSecurityModel>();
                            for (int i = 0; i < rgvUser.MasterTemplate.Templates.Count; i++)
                            {
                                rgvUser.MasterTemplate.Templates.Remove(rgvUser.MasterTemplate.Templates[i]);
                            }
                            FillRgvUser();
                            this.rgvUser.Columns["idMenuSuperior"].IsVisible = false;


                        }

                        rgvUser.Columns["nameMenuU"].ReadOnly = true;
                    }
                    this.rgvUser.Columns["idMenuSuperior"].IsVisible = false;

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void rgvUser_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (rgvUser.SelectedRows.Count > 0)
                {
                    GridViewDataRowInfo newRow = rgvUser.SelectedRows[0] as GridViewDataRowInfo;

                    int rowNo = newRow.Index;


                    for (int i = 0; i < rgvUser.Columns.Count; i++)
                    {

                        if (rgvUser.Columns[i].Name.ToString() == "nameMenuU")
                        {

                            rgvUser.Columns["nameMenuU"].ReadOnly = true;
                            break;
                        }
                    }

                }
                //ako je u pitanju insert novog reda tada sve kolone moraju biti setovane da je readOnly=false
                else
                {
                    for (int i = 0; i < rgvUser.Columns.Count; i++)
                    {
                        if (rgvUser.Columns[i].Name.ToString() == "nameMenuU")
                        {
                            rgvUser.Columns["nameMenuU"].ReadOnly = false;
                        }

                        if (rgvUser.Columns[i].Name.ToString() == "byComboU")
                        {
                            rgvUser.Columns["byComboU"].ReadOnly = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnemployee_Click(object sender, EventArgs e)
        {
            EmployeeBUS EmployeeBUS = new EmployeeBUS();
            List<IModel> gm3 = new List<IModel>();

            gm3 = EmployeeBUS.GetAllEmpl(Login._user.lngUser);


            var dlgSave = new GridLookupForm(gm3, "Employee");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                EmployeeModel genm3 = new EmployeeModel();
                genm3 = (EmployeeModel)dlgSave.selectedRow;
                txtemployee.Text = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
                if (User != null)
                {
                    User.idEmployee = genm3.idEmployee;
                    User.nameEmployee = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
                }
                idEmpl = genm3.idEmployee;
                txtUserFullName.Text = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
                txtidEmployee.Text = genm3.idEmployee.ToString();
            }
        }

        private void radButtonUserEmail_Click(object sender, EventArgs e)
        {

           // frmUserMail frm = new frmUserMail(Convert.ToInt32(txtID.Text));
           // frm.ShowDialog();


        }
       private void rgvUser_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                UsersBUS uBUS = new UsersBUS();

                if (e.RowIndex != null & e.RowIndex >= 0)
                {
                    GridViewRowInfo info = this.rgvUser.CurrentRow;

                    if (info != null && info.Index >= 0)
                    {
                        RoleSecurityModel selectedMenu = (RoleSecurityModel)info.DataBoundItem;
                        if (selectedMenu != null)
                        {
                            if (selectedMenu.idMenu != null && selectedMenu.idMenu > 0)
                            {
                                List<RoleSecurityModel> listMenus = new List<RoleSecurityModel>();
                                listMenus = uBUS.GetSubMenusForSuperior(Login._user.lngUser, Convert.ToInt32(txtID.Text), selectedMenu.idMenu);
                                List<RoleSecurityModel> listSelectedMenus = new List<RoleSecurityModel>();
                                listSelectedMenus = uBUS.GetSubMenusForSuperiorForRole(Login._user.lngUser, Convert.ToInt32(txtID.Text), selectedMenu.idMenu);
                                if (listMenus.Count > 0)
                                {
                                    GridLookupFormUserMenus frm = new GridLookupFormUserMenus(listMenus, listSelectedMenus, Convert.ToInt32(txtID.Text), "Role");
                                    frm.ShowDialog();
                                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {

                                        rgvUser.DataSource = new List<RoleSecurityModel>();
                                        if (secondChildtemplateU != null)
                                            secondChildtemplateU.DataSource = new List<RoleSecurityModel>();
                                        if (templateU != null)
                                            templateU.DataSource = new List<RoleSecurityModel>();
                                        for (int i = 0; i < rgvUser.MasterTemplate.Templates.Count; i++)
                                        {
                                            rgvUser.MasterTemplate.Templates.Remove(rgvUser.MasterTemplate.Templates[i]);
                                        }
                                        FillRgvUser();
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("There is no submenus for this menu");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       private void txtPassDuration_KeyPress(object sender, KeyPressEventArgs e)
       {
           e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
       }

       private void txtWarningDays_KeyPress(object sender, KeyPressEventArgs e)
       {
           e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
       }


        
       
      
       
    }
}
