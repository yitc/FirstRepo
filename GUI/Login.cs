using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BIS.Business;
using BIS.Model;
// samo zbog update-a image-a u bazi
using BIS.DAO;
using BIS.Core;
using System.Configuration;
using System.Resources;
using System.Globalization;
using System.Reflection;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net;
using System.Net.Sockets;


namespace GUI
{
    public partial class Login : Telerik.WinControls.UI.RadForm
    {
        //bussiness models
        private UsersBUS _usersBUS;
        private MenuBUS _menuBUS;
        private CompanyBUS _companyBUS;
        //all languages
        private LanguagesBUS _languagesBUS;
        //current user language for translation
        private LanguageBUS _languageBUS;
        private MedicalVoluntaryBUS _medvolBUS;
        
        //user information
        public static UsersModel _user;
        //lists that contain all user settings and data
        public static List<MenuModel> _menuModelList;
        public static List<LanguagesModel> _langModelList;
        public static List<FilterModel> _personFilters;
        public static List<FilterModel> _clientFilters;
        public static List<FilterModel> _usersFilters;
        public static List<FilterModel> _medicalFilters;
        public static List<FilterModel> _voluntaryFilters;
        public static List<FilterModel> _translationFilters;
        public static List<FilterModel> _employeeFilters;
        public static List<CompanyModel> _companyModelList;
        public static List<FilterModel> _arrangeFilters;
        public static List<FilterModel> _articalgroupsFilters;
        public static List<FilterModel> _rolesFilters;
        public static List<FilterModel> _menusFilters;
        public static List<FilterModel> _articalFilters;
        public static List<FilterModel> _departmentsFilters;
        public static List<FilterModel> _multimediaFilters;
        public static List<FilterModel> _multimediaServerFilters;
        public static List<FilterModel> _countryFilters;
        public static List<FilterModel> _accsettingsFilters;
        public static List<FilterModel> _paymentFilters;
        public static List<FilterModel> _ledgerFilters;

        //define Labels (name must be lake field in)
        public static List<LabelModel> _personLabels;
        public static List<LabelModel> _clientLabels;
        public static List<LabelModel> _usersLabels;
        public static List<LabelModel> _employeeLabels;
        public static List<LabelModel> _medicalLabels;
        public static List<LabelModel> _voluntaryLabels;
        public static List<LabelModel> _translationLabels;
        public static List<LabelModel> _arrangeLabels;
        public static List<LabelModel> _articalgroupsLabels;
        public static List<LabelModel> _rolesLabels;
        public static List<LabelModel> _menusLabels;
        public static List<LabelModel> _articalLabels;
        public static List<LabelModel> _departmentsLabels;
        public static List<LabelModel> _multimediaLabels;
        public static List<LabelModel> _arrLabels;
        public static List<LabelModel> _multimediaServerLabels;
        public static List<LabelModel> _countryLabels;
        public static List<LabelModel> _accsettingsLabels;
        public static List<LabelModel> _paymentLabels;
        public static List<LabelModel> _ledgerLabels;
        public static string _bookyear;

        //resource file that contains translation for user language
        public static string resxFile = @"LanguageResources.resx";
        public static Icon iconForm;
        //for loading combo languages
        bool pageLoaded = false;

        //outlook settings
        public static bool isOutlookInstalled;
        public static Outlook.Folder sentFolder;

        Timer timer1;
        int loginCount = 0;
        int counter = 1800;
        string btnloginText = "";

        public Login()
        {
            //user data
            _usersBUS = new UsersBUS();
            _menuBUS = new MenuBUS();
            _medvolBUS = new MedicalVoluntaryBUS();
            _personFilters = new List<FilterModel>();
            _personLabels = new List<LabelModel>();
            _clientFilters = new List<FilterModel>();
            _clientLabels = new List<LabelModel>();
            _employeeFilters = new List<FilterModel>();
            _employeeLabels = new List<LabelModel>();
            _usersFilters = new List<FilterModel>();
            _articalgroupsFilters = new List<FilterModel>();
            _rolesFilters = new List<FilterModel>();
            _menusFilters = new List<FilterModel>();
            _articalFilters = new List<FilterModel>();
            _departmentsFilters  = new List<FilterModel>();
            _multimediaFilters = new List<FilterModel>();
            _multimediaServerFilters = new List<FilterModel>();
            _countryFilters = new List<FilterModel>();
            _accsettingsFilters = new List<FilterModel>();
            _paymentFilters = new List<FilterModel>();
            _arrangeFilters = new List<FilterModel>();
            _ledgerFilters = new List<FilterModel>();

            _usersLabels = new List<LabelModel>();
            _langModelList = new List<LanguagesModel>();
            _languageBUS = new LanguageBUS();
            _languagesBUS = new LanguagesBUS();
            _medicalLabels = new List<LabelModel>();
            _medicalFilters = new List<FilterModel>();
            _voluntaryLabels = new List<LabelModel>();
            _voluntaryFilters = new List<FilterModel>();
            _translationFilters = new List<FilterModel>();
            _translationLabels = new List<LabelModel>();
            _articalgroupsLabels = new List<LabelModel>();
            _rolesLabels = new List<LabelModel>();
            _menusLabels = new List<LabelModel>();
            _articalLabels = new List<LabelModel>();
            _departmentsLabels = new List<LabelModel>();
            _multimediaLabels = new List<LabelModel>();
            _multimediaServerLabels = new List<LabelModel>();
            _countryLabels = new List<LabelModel>();
            _accsettingsLabels = new List<LabelModel>();
            _paymentLabels = new List<LabelModel>();
            _arrangeLabels = new List<LabelModel>();
            _ledgerLabels = new List<LabelModel>();

           
            //company data
            _companyBUS = new CompanyBUS();
            _companyModelList = new List<CompanyModel>();
            //string idCompany = ConfigurationManager.AppSettings["idCompany"];
            _companyModelList = _companyBUS.GetCompanyDetails();

            InitializeComponent();
            //====prikazuje na koju bazu se kaci ...
            lblDatabase.Text = "Database : " + new UsersBUS().getDBName();
            //set logo and icon depend on company
            if (_companyModelList != null)
            {
                foreach (var model in _companyModelList)
                {
                    ImageDB image = new ImageDB();
                    picLogo.Image = image.setImage(model.logoCompany);
                    this.Text = model.nameCompany;
                    IconDB icon = new IconDB();
                    iconForm = icon.setIcon(model.iconCompany);
                    this.Icon = iconForm;
                }
            }


            //sets languages combo
            _langModelList = _languagesBUS.GetLanguagesDetails();
            cboLang.DataSource = _langModelList;
            cboLang.ValueMember = "idLang";
            cboLang.DisplayMember = "nameLang";
            //sets booking year
            _bookyear = DateTime.Now.Year.ToString();

            if (ConfigurationManager.AppSettings["idLang"] != null)
            {
                cboLang.SelectedItem = cboLang.Items[_langModelList.FindIndex(item => item.nameLang.TrimEnd() == ConfigurationManager.AppSettings["idLang"].TrimEnd())];
            }
            else
                cboLang.SelectedItem = cboLang.Items[_langModelList.FindIndex(item => item.nameLang.TrimEnd() == "EN")];

            //sets last username
            if (ConfigurationManager.AppSettings["username"].ToString() != "")
            {
                txtUsername.Text = ConfigurationManager.AppSettings["username"];
            }
            else
                //if there is no user default language is english but we need to read resource file for that
                _languageBUS.GetLanguageStrings(cboLang.SelectedItem.ToString().TrimEnd());

            translateLogin();
          
            //DON'T DELETE - THIS IS FOR INSERT IMAGES AND ICONS IN DB

            //update image-a
            //CompanyDAO company = new CompanyDAO();
            //company.updateImages();
            //update icon-a
            //company.updateIcons();

            //encrypt password
            /*UserDAO u = new UserDAO();
            DataTable dt = u.selectPasswords();
            if(dt.Rows.Count>0)
            {
                for(int i = 0; i<dt.Rows.Count;i++)
                {
                    Crypt c = new Crypt();
                    u.updatePasswords(dt.Rows[i]["password"].ToString(), c.Encrypt(dt.Rows[i]["password"].ToString()));
                }
            }*/
        }


        private void translateLogin()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(resxFile))
            {
                btnLogin.Text = resxSet.GetString("LogInButton");
                txtUsername.NullText = resxSet.GetString("Username");
                txtPassword.NullText = resxSet.GetString("Password");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            _user = new UsersModel();
            _menuModelList = new List<MenuModel>();

            //check if user exists
            _user = _usersBUS.Login(txtUsername.Text, _usersBUS.encryptPassword(txtPassword.Text));

            if (_user == null)
            {
                
                using (ResXResourceSet resxSet = new ResXResourceSet(resxFile))
                {
                    if (resxSet.GetString("User not found") != null)
                        RadMessageBox.Show(resxSet.GetString("User not found"));
                    else
                        RadMessageBox.Show("User not found");
                }
                loginCount++;
                if (loginCount >= 3)
                {
                    lockUser(0);
                    _usersBUS.insertUsersIp(LocalIPAddress().ToString());
                }
            }
            else if(_user.isNotActive == true)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(resxFile))
                {
                    if (resxSet.GetString("User not active") != null)
                        RadMessageBox.Show(resxSet.GetString("User not found"));
                    else
                        RadMessageBox.Show("User not active");
                } 
            }
            else
            {
                _usersBUS.loginLogOut(DateTime.Now, _user.idUser, true);
                //change user language if combo is changed
                if (_user.lngUser.TrimEnd() != cboLang.SelectedItem.ToString().TrimEnd())
                {
                    if (_languagesBUS.updateUserLanguage(cboLang.SelectedItem.ToString().TrimEnd(), _user.idUser, this.Name) == true)
                    {
                        _user = _usersBUS.updatelng(_user, cboLang.SelectedItem.ToString().TrimEnd());
                      
                    }
                    else
                    {
                        RadMessageBox.Show("Something went wrong with updating your language.");
                    }
                }
                

                //set username from last user logged in
                //open App.Config of executable
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                //remove an Application Setting
                if (ConfigurationManager.AppSettings["username"] != null)
                    config.AppSettings.Settings.Remove("username");

                //force a reload of a changed section.
                if (_user.username != null && _user.username != "")
                    config.AppSettings.Settings.Add("username", _user.username);
                else
                    config.AppSettings.Settings.Add("username", "");
                //save the changes in App.config file.
                config.Save(ConfigurationSaveMode.Modified);

                //sets user language and other data
                string language = "EN";
                if (_user.lngUser != null && _user.lngUser != "")
                    language = _user.lngUser;
                _languageBUS.GetLanguageStrings(_user.lngUser);
                _menuModelList = _menuBUS.GetUserSecurityDetails(_user.idUser, language);
                _personFilters = _usersBUS.GetPersonFilters(language);
                _clientFilters = _usersBUS.GetClientFilters(language);
                _usersFilters = _usersBUS.GetUsersFilters(language);
                _employeeFilters = _usersBUS.GetEmployeeFilters(language);

                //sets labels for defined lists
                foreach (MenuModel m in _menuModelList)
                {
                    if (m.idMenu!=null)
                    {
                        if (new MenuBUS().GetMenuIsGrid(m.idMenu) == true)
                        {
                            string controlName = "";
                            controlName = "_" + (m.onClickMenu).ToLower() + "Labels";
                            List<LabelModel> l = new List<LabelModel>();
                            Type type1 = typeof(Login);
                            FieldInfo info = type1.GetField(controlName);
                            if (info != null)
                            {
                                info.SetValue(l, new LabelsBUS().GetLabels(language, m.idMenu));
                            }

                         
                        }
                        //set labels for sub sub menus
                        List<MenuModel> tmSubMenus = new MenuBUS().GetMenus(m.idMenu, _user.idUser, _user.lngUser);
                        foreach (MenuModel mSubSub in tmSubMenus)
                        {
                            List<MenuModel> tmSubSubMenus = new MenuBUS().GetMenus(mSubSub.idMenu, _user.idUser, _user.lngUser);
                            setLabelsForSubmenus(tmSubSubMenus);
                        }

                    }
                }
                _arrLabels = _personLabels;
               // _voluntaryLabels = _personLabels;
                Boolean isSuccesufullyLogged = false;

                if (_menuModelList != null)
                {
                    //number of days that password is valid
                    int _countDays = Convert.ToInt32((DateTime.Now - _user.dtPassChanged).TotalDays);

                    //app starts first time, change pass
                    if (_user.isFirstTimeStarted == true)
                    {
                        ChangePassword newPass = new ChangePassword(_user.idUser.ToString(), _user.password);
                        if (newPass.ShowDialog(this) == DialogResult.Yes)
                        {
                            isSuccesufullyLogged = true;
                        }
                    }
                    else
                    {
                        //5 days warning before changing
                        if (_countDays < _user.numDaysPassValid && _user.numDaysPassValid - _countDays <= 5)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translatePartAndNonTranslatedPartInMiddleText("You have", (_user.numDaysPassValid - _countDays).ToString(), "days untill your password expires!");
                            isSuccesufullyLogged = true;
                        }
                        //change pass
                        else if (_user.numDaysPassValid - _countDays == 0)
                        {
                            ChangePassword newPass = new ChangePassword(_user.idUser.ToString(), _user.password);
                            if (newPass.ShowDialog(this) == DialogResult.Yes)
                            {
                                isSuccesufullyLogged = true;
                            }
                        }
                        else if (_user.numDaysPassValid - _countDays < 0)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You can not log in because your password is expired!");
                            isSuccesufullyLogged = false;
                        }
                        else
                        {
                            isSuccesufullyLogged = true;
                        }
                    }
                    if (isSuccesufullyLogged == true)
                    {
                        MainForm mFrm = new MainForm();
                        mFrm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    RadMessageBox.Show("No security roles");
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void setLabelsForSubmenus(List<MenuModel> _menuModelList)
        {
            foreach (MenuModel m in _menuModelList)
            {
                if (m.idMenu != null)
                {
                    if (new MenuBUS().GetMenuIsGrid(m.idMenu) == true)
                    {
                        string controlName = "";
                        controlName = "_" + (m.onClickMenu).ToLower() + "Labels";
                        List<LabelModel> l = new List<LabelModel>();
                        Type type1 = typeof(Login);
                        FieldInfo info = type1.GetField(controlName);
                        if (info != null)
                        {
                            info.SetValue(l, new LabelsBUS().GetLabels(_user.lngUser, m.idMenu));
                        }
                    }
                }
            }
        }

        private void lockUser(int a) 
        {
            counter = (30-a) * 60;
            using (ResXResourceSet resxSet = new ResXResourceSet(resxFile))
            {
                if (resxSet.GetString("You have to wait " + (30 - a) + " minutes and then you can try again!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have to wait " + (30 - a) + " minutes and then you can try again!"));
                else
                    RadMessageBox.Show("You have to wait " + (30 - a) + " minutes and then you can try again!");
            }
            btnLogin.Enabled = false;
            btnloginText = btnLogin.Text;
            startTimer();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            pageLoaded = true;
            IPAddress ip = LocalIPAddress();
            int time = _usersBUS.checkUser(ip.ToString());
            if (time>=0) 
            {
                
                lockUser(time);
                
            }
            //================== ucitava setovanja za layout aplikacije
           ThemeResolutionService.LoadPackageFile(@"BWindows8.tssp");
           ThemeResolutionService.AllowAnimations = false;
            //ThemeResolutionService.LoadPackageFile(@"D:\Buiten_templste\Nieuwe map\ControlDefault.tssp");
            ThemeResolutionService.ApplicationThemeName = "BWindows8";
            RadMessageBox.SetThemeName("BWindows8");
            //==================
           //  RadMessageBox.SetThemeName("Windows8");

            Type officeType = Type.GetTypeFromProgID("Outlook.Application");
            if (officeType == null)
            {
                //MessageBox.Show("OfficeDontExist");
                isOutlookInstalled = false;
            }
            else
            {
                //if outlook is installed...
                isOutlookInstalled = true;
            //    AddOutlookBISFolder();
            }

        }

        private void cboLang_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (pageLoaded == true)
            {
                //set username and password from last user logged in
                //open App.Config of executable
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                //remoe and add an Application Setting.
                config.AppSettings.Settings.Remove("idLang");

                config.AppSettings.Settings.Add("idLang", cboLang.SelectedItem.ToString().TrimEnd());

                //save the changes in App.config file.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("AppSettings");

                //reload language transation
                _languageBUS.GetLanguageStrings(cboLang.SelectedItem.ToString().TrimEnd());
                translateLogin();
            }
        }

        private void AddOutlookBISFolder()
        {
            Outlook.Application outlookApp = new Outlook.Application();

            Outlook.Folder folder =
                outlookApp.Session.GetDefaultFolder(
                Outlook.OlDefaultFolders.olFolderSentMail)
                as Outlook.Folder;


            //Outlook.Rules rules = outlookApp.Session.DefaultStore.GetRules();
            //Outlook.Rule rule = rules["BIS"];
            //if(rule == null)
            //{
            //    rule = rules.Create("BIS", Outlook.OlRuleType.olRuleReceive);                

            //}            

            Outlook.Folders folders = folder.Folders;
            try
            {
                bool found = false;
                foreach(Outlook.Folder f in folders)
                {
                    if(f.Name == "BIS")
                    {
                        found = true;
                        sentFolder = f;
                        break;
                    }
                }

                if (found == false)
                {
                    Outlook.Folder newFolder = folders.Add(
                        "BIS", Type.Missing)
                        as Outlook.Folder;

                    newFolder.Display();

                    sentFolder = newFolder;
                }
            }
            catch
            {
                MessageBox.Show(
                    "Could not add 'BIS Folder'",
                    "Add Folder",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            
        }
        public static Outlook.Folder GetOutlookBisFolder()
        {
            Outlook.Folder retValue = null;
            Outlook.Application outlookApp = new Outlook.Application();

            Outlook.Folder folder =
                outlookApp.Session.GetDefaultFolder(
                Outlook.OlDefaultFolders.olFolderSentMail)
                as Outlook.Folder;

            retValue = folder;

            Outlook.Folders folders = folder.Folders;
            try
            {

                foreach (Outlook.Folder f in folders)
                {
                    if (f.Name == "BIS")
                    {
                        retValue = f;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return retValue;
        }
        private void startTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;            
            btnLogin.Text = "You need to wait for " + Math.Ceiling((double)counter/60) + " more minute/s";

            if (counter <= 0)
            {
                timer1.Stop();
                btnLogin.Enabled = true;
                btnLogin.Text = btnloginText;
            }
        }
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
