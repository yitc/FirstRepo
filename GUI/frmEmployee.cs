using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using System.Resources;
using BIS.Business;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Linq;
using GUI.User_Controls;


namespace GUI
{
    public partial class frmEmployee : frmTemplate
    {


        EmployeeModel emplyoee;
        EmployeeModel emplyoeeFirst;

        EmployeePassportModel emppass;

        public int IDTel;
        EmployeeTelModel empTelModel;
        List<EmployeeTelModel> empTelList;
        List<EmployeeTelModel> empTelListFirst;
        private int employeeId;

        List<EmployeeEmailModel> empEmailList;
        List<EmployeeEmailModel> empEmailListFirst;
        EmployeeEmailModel empEmailModel;
        public int IDEmail;
        private int lengthInitials = 0;
        public int IDpass; // promenljiva za Update passposrta
        int iID = -1;
        int eID = -1;        

        //Employee address user control
       // EmployeeAddress empaddress;
        AdrThree empaddress;
        //bool showEmergencyAddressForFillPErsonData;
        


        public frmEmployee(IModel model, EmployeePassportModel pass)
        {
            emplyoee = (EmployeeModel)model;
            emplyoeeFirst = new EmployeeModel(emplyoee);

            iID = emplyoee.idEmployee;
            emppass = pass;
            InitializeComponent();
        }

        public frmEmployee()
        {
            iID = -1;
            emplyoee = new EmployeeModel();
            emplyoeeFirst = new EmployeeModel();
            InitializeComponent();
          
        }

        private void UpdateOriginalValuesAfterSave()
        {
            emplyoeeFirst = new EmployeeModel(emplyoee);
            empTelListFirst = new List<EmployeeTelModel>();
            if (empTelList != null)
            {
                foreach (EmployeeTelModel m in empTelList)
                {
                    empTelListFirst.Add(m.ReturnCopy());
                }
            }

            empEmailListFirst = new List<EmployeeEmailModel>();
            if (empEmailList != null)
            {
                foreach (EmployeeEmailModel m1 in empEmailList)
                {
                    empEmailListFirst.Add(m1.ReturnCopy());
                }
            }
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;


            setTranslation();
            //add gender
            GenderBUS gb = new GenderBUS();
            List<GenderModel> gm = new List<GenderModel>();
            gm = gb.GetAllGenders(Login._user.lngUser);

            ddlGender.DataSource = gm;
            ddlGender.DisplayMember = "nameGender";
            ddlGender.ValueMember = "idGender";
            
             dtIssueDate.Value = DateTime.Now;
             dtValidTo.Value = DateTime.Now;
            // department combo 
            DepartmentsBUS gb1 = new DepartmentsBUS();
            List<DepartmentsModel> gm1 = new List<DepartmentsModel>();
            gm1 = gb1.GetAllDepartments1();

           // List<DepartmentsModel> gm11 = new List<DepartmentsModel>(gm1);           

            ddlDepartment.DataSource = gm1;
            ddlDepartment.DisplayMember = "nameDepartment";
            ddlDepartment.ValueMember = "idDepartment";
            
            // Function combo
            FunctionBUS gb2 = new FunctionBUS();
            List<FunctionModel> gm2 = new List<FunctionModel>();
            gm2 = gb2.GetFunctions(Login._user.lngUser);

            ddlFunction.DataSource = gm2;
            ddlFunction.DisplayMember = "nameFunction";
            ddlFunction.ValueMember = "idFunction";
            
            // Wish Function combo
            FunctionBUS gb3 = new FunctionBUS();
            List<FunctionModel> gm3 = new List<FunctionModel>();
            gm3 = gb3.GetFunctions(Login._user.lngUser);

            ddlWFunction.DataSource = gm3;
            ddlWFunction.DisplayMember = "nameFunction";
            ddlWFunction.ValueMember = "idFunction";
            
            //add title
            TitleBUS gb4 = new TitleBUS();
            List<TitleModel> gm4 = new List<TitleModel>();
            gm4 = gb4.GetAllTitle();

            ddlTitle.DataSource = gm4;
            ddlTitle.DisplayMember = "nameTitle";
            ddlTitle.ValueMember = "idTitle";
            
            //add Status
            EmployeeStatusBUS gb5 = new EmployeeStatusBUS();
            List<EmployeeStatusModel> gm5 = new List<EmployeeStatusModel>();
            gm5 = gb5.GetAllEmployeeStatus(Login._user.lngUser);

            ddlStatus.DataSource = gm5;
            ddlStatus.DisplayMember = "descriptionEmployee";
            ddlStatus.ValueMember = "idStatusEmployee";
            
            //Email Combo
            //TypesEmailBUS teb = new TypesEmailBUS();
            //List<TypesEmailModel> tem = teb.GetAllTypeEmail(Login._user.lngUser);
            //GridViewComboBoxColumn ddlemail = new GridViewComboBoxColumn();
            //ddlemail.DataSource = tem;
            //ddlemail.DisplayMember = "nameEmailType";
            //ddlemail.ValueMember = "emailType";
            //ddlemail.FieldName = "emailType";
            //ddlemail.Name = "Type";
            //ddlemail.HeaderText = "Type";
            //rgvEmail.Columns.Add(ddlemail);
            EmployeeEmailBUS eeb1= new EmployeeEmailBUS();
            //List<EmployeeEmailModel> abc = new List<EmployeeEmailModel>();
            empEmailList = eeb1.GetEmployeeEmails(emplyoee.idEmployee);
            // rgvEmail.DataSource = null;
            rgvEmail.DataSource = empEmailList;
            //this.rgvEmail.Columns["emailType"].IsVisible = false;
            //this.rgvEmail.Columns["idEmployee"].IsVisible = false;

            //Ucitavanje kombo boxa
            if (!rgvEmail.Columns.Contains("Types"))
            {

                TypesEmailBUS teb = new TypesEmailBUS();
                List<TypesEmailModel> tem = teb.GetAllTypeEmail(Login._user.lngUser);
                GridViewComboBoxColumn ddlemail = new GridViewComboBoxColumn();
                ddlemail.DataSource = tem;
                ddlemail.DisplayMember = "nameEmailType";
                ddlemail.ValueMember = "idEmailType";
                ddlemail.FieldName = "emailType";
                ddlemail.Name = "Type";
                ddlemail.HeaderText = "Type";

                rgvEmail.Columns.Add(ddlemail);
            }
            //Velicina kolone u gridu
            rgvEmail.Columns["Type"].Width = (int)(this.CreateGraphics().MeasureString(rgvEmail.Columns["Type"].HeaderText, this.Font).Width + 108);
            rgvEmail.Columns["Type"].MinWidth = rgvEmail.Columns["Type"].Width + 108;
            


            if (iID != -1)
            {

                txtIntials.Text = emplyoee.initialsEmployee.ToString();
                //if (emplyoee.titleEmployee.ToString() != "")
                //   ddlTitle.Text = emplyoee.titleEmployee.ToString();
                txtFirstName.Text = emplyoee.firstNameEmployee.ToString();
                txtMidName.Text = emplyoee.midNameEmployee.ToString();
                txtLastName.Text = emplyoee.lastNameEmployee.ToString();
                txtMaidenName.Text = emplyoee.maidenEmployee.ToString();
                dtBirthDate.Text = emplyoee.dtBirthDateEmployee.ToString();
                //if (emplyoee.genderEmployee.ToString() != "")
                //   ddlGender.ValueMember = emplyoee.genderEmployee.ToString();
                txtIdentBSN.Text = emplyoee.isentBsnEmploee.ToString();
                txtIban.Text = emplyoee.ibanEmployee.ToString();
                txtBic.Text = emplyoee.bicEmployee.ToString();
                txtEmergancyPerson.Text = emplyoee.emergencyPersonEmployee.ToString();
                txtEmergancyTel.Text = emplyoee.emergencyTelEmployee.ToString();
                txtAddress.Text = emplyoee.addressEmployee.ToString();
                txtHouseNr.Text = emplyoee.houseNumberEmployee.ToString();
                txtExtension.Text = emplyoee.extensionEmployee.ToString();
                txtZipCode.Text = emplyoee.zipCodeEmployee.ToString();
                txtCity.Text = emplyoee.cityEmployee.ToString();

                if (emplyoee.isMariedEmployee == true)
                    chkMarried.CheckState = CheckState.Checked;

                if (emplyoee.isAplicationUser == true)
                    chkAplicationUser.CheckState = CheckState.Checked;

                dtHireDate.Text = emplyoee.dtHireDateEmployee.ToString();
                txtCnontractNr.Text = emplyoee.contractNumberEmployee.ToString();

                if (emplyoee.imageEmployee != "")
                {
                    BIS.Core.ImageDB setImage = new BIS.Core.ImageDB();
                    picEmployee.Image = setImage.setImage(emplyoee.imageEmployee);
                }

                //  ddlFunction.Text = emplyoee.Function.ToString();
                //   ddlWFunction.Text = emplyoee.WishFunction.ToString();
                if (emplyoee.isAplicationUser == true)
                    chkAplicationUser.CheckState = CheckState.Checked;
                // ddlDepartment.Text = emplyoee.Department.ToString();
                if (emplyoee.genderEmployee != 0)
                    ddlGender.SelectedItem = ddlGender.Items[gm.FindIndex(item => item.idGender == emplyoee.genderEmployee)];
                if (emplyoee.Department != null && emplyoee.Department != 0)
                    ddlDepartment.SelectedItem = ddlDepartment.Items[gm1.FindIndex(item =>item.idDepartment == emplyoee.Department)];
                if (emplyoee.Function != null && emplyoee.Function != 0)
                    ddlFunction.SelectedItem = ddlFunction.Items[gm2.FindIndex(item => item.idFunction == emplyoee.Function)];
                if (emplyoee.WishFunction != null && emplyoee.WishFunction != 0)
                    ddlWFunction.SelectedItem = ddlWFunction.Items[gm3.FindIndex(item => item.idFunction == emplyoee.WishFunction)];
                if (emplyoee.titleEmployee != null)
                    ddlTitle.SelectedItem = ddlTitle.Items[gm4.FindIndex(item => item.idTitle == emplyoee.titleEmployee)];
                if (emplyoee.statusEmployee != null && emplyoee.statusEmployee != 0)
                    ddlStatus.SelectedItem = ddlStatus.Items[gm5.FindIndex(item => item.idStatusEmployee == emplyoee.statusEmployee)];
            }
            else
            {

                dtBirthDate.Value = DateTime.Now;
                dtHireDate.Value = DateTime.Now;

                if (ddlFunction.SelectedValue != null)
                    emplyoee.Function = (int)ddlFunction.SelectedValue;
                
                if (ddlWFunction.SelectedValue != null)
                    emplyoee.WishFunction = (int)ddlWFunction.SelectedValue;

                if (ddlDepartment.SelectedValue != null)
                    emplyoee.Department = (int)ddlDepartment.SelectedValue;

                if (ddlStatus.SelectedValue != null)
                    emplyoee.statusEmployee = (int)ddlStatus.SelectedValue;

                emplyoeeFirst = new EmployeeModel(emplyoee);

            }

            CountryBUS gb6 = new CountryBUS();
            List<CountryModel> gm6 = new List<CountryModel>();
            gm6 = gb6.GetCountriesWithCountryModel();

            txtNacional.DataSource = gm6;
            txtNacional.DisplayMember = "nacionality";
            txtNacional.ValueMember = "idCountry";

            // Passport section
            if (emppass != null)
            {
                // combo nacionality
                //   CountryBUS gb6 = new CountryBUS();
                //   List<CountryModel> gm6 = new List<CountryModel>();
                //    gm6 = gb6.GetCountriesWithCountryModel();

                //txtNacional.DataSource = gm6;
                //txtNacional.DisplayMember = "nacionality";
                //txtNacional.ValueMember = "idCountry";

                // promenljiva za update passport//
                IDpass = emppass.idemppass;

                txtPassName.Text = emppass.passname.ToString();
                txtPassNumber.Text = emppass.passnumber.ToString();
                txtBirthPlace.Text = emppass.passbrplace.ToString();
                //  txtBirthPlace.Text = employeePassport.passbrplace.ToString();
                txtPassIssuePlace.Text = emppass.passisplace.ToString();
                dtIssueDate.Text = emppass.passisued.ToString();
                //  dtValidTo.Text = employeePassport.passvalid.ToString();
                dtValidTo.Text = emppass.passvalid.ToString();
                //  txtNacional.Text = employeePassport.passnational.ToString();

                if (emppass.passnational != null)
                    txtNacional.SelectedItem = txtNacional.Items[gm6.FindIndex(item => item.idCountry == emppass.passnational)];

            }

            //Telephone section
          
            // telephone & email grid
            EmployeeTelBUS etb = new EmployeeTelBUS();
            EmployeeEmailBUS eeb = new EmployeeEmailBUS();
            // punjenje Email type
            // DataTable table1 = new DataTable();
            // table1.Columns.Add("Type", typeof(string));
            //table1.Columns.Add("value", typeof(int));
            // table1.Rows.Add("Office",1);
            //    table1.Rows.Add("Private",2);
            //    this.rgvEmail.AutoGenerateColumns = false;
            //    this.rgvEmail.GridViewComboBoxColumn comboCol = new GridViewComboBoxColumn("Phone");
            //    this.rgvEmail.comboColumn.FieldName = "EmailType";

            empTelList=etb.GetEmployeeTels(emplyoee.idEmployee);
            rgvTel.DataSource = empTelList; //ptb.GetEmployeeTels(emplyoee.idEmployee, Login._user.lngUser);
            this.rgvTel.Columns["idtelemp"].IsVisible = false;
            this.rgvTel.Columns["idEmployee"].IsVisible = false;
            this.rgvTel.Columns["telephoneType"].IsVisible = false;
            rgvTel.Show();                                       
            rgvEmail.Show();

            empTelListFirst = new List<EmployeeTelModel>();
            if(empTelList != null)
            {
                foreach(EmployeeTelModel m in empTelList)
                {
                    empTelListFirst.Add(m.ReturnCopy());
                }
            }

            empEmailListFirst = new List<EmployeeEmailModel>();
            if (empEmailList != null)
            {
                foreach (EmployeeEmailModel m1 in empEmailList)
                {
                    empEmailListFirst.Add(m1.ReturnCopy());
                }
            }

            // pagePerson.SelectedPage = pgEmployee;
            // Adrese 

            //ContactPersonAddress coa = new ContactPersonAddress();
            //coa.Dock = System.Windows.Forms.DockStyle.Fill;
            //coa.showBillingAddress = false;
            //coa.showEmergencyAddress = false;
            //pnlAdres.Controls.Add(coa);


            //====================Ucitavanje adresa=============================
            //enployee kontrola
          //  EmployeeAddress empaddress = new EmployeeAddress();            
            AdrThree empaddress = new AdrThree();
            EmployeeBUS empbus = new EmployeeBUS();

            empaddress.showBillingAddress = false;
            empaddress.showEmergencyAddress = false;

            empaddress.Dock = DockStyle.Fill; //System.Windows.Forms.DockStyle.Fill;
            TypesAddressBUS typeAddBus = new TypesAddressBUS();
            TypesAddresslModel typeAddModel = typeAddBus.GetTypeAddressById(3, Login._user.lngUser);

           
            pnlAdres.Controls.Add(empaddress);
           
            pnlAdres.Controls.Find("txt_adr_street", true)[0].Text = emplyoee.addressEmployee;
            pnlAdres.Controls.Find("txt_adr_city", true)[0].Text = emplyoee.cityEmployee;
            pnlAdres.Controls.Find("txt_adr_houseno", true)[0].Text = emplyoee.houseNumberEmployee;
            pnlAdres.Controls.Find("txt_adr_zip", true)[0].Text = emplyoee.zipCodeEmployee;
            pnlAdres.Controls.Find("txt_adr_ext", true)[0].Text = emplyoee.extensionEmployee;

            //if (empaddress.isInternational == true)
            //{
            //    RadRadioButton rchk = (RadRadioButton)pnlAdres.Controls.Find("rad_adr_inter", true)[0];
            //    rchk.CheckState = CheckState.Checked;
            //    RadRadioButton rchkNL = (RadRadioButton)pnlAdres.Controls.Find("rad_adr_nl", true)[0];
            //    rchkNL.CheckState = CheckState.Unchecked;
            //    pnlAdres.Controls.Find("btn_adr_get", true)[0].Visible = false;
            //    pnlAdres.Controls.Find("lbl_adr_country", true)[0].Visible = true;
            //    pnlAdres.Controls.Find("txt_adr_country", true)[0].Visible = true;
            //    pnlAdres.Controls.Find("txt_adr_country", true)[0].Text = empaddress.country;
            //}

        }


        private void radPageViewPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radPageViewPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUploadPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = dialog.FileName;
                //save image
                picEmployee.Image = Image.FromFile(dialog.FileName);

            }
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                lblinitials.Text = resxSet.GetString("Initials") + " / " + resxSet.GetString("Title");
                lblFrstname.Text = resxSet.GetString("First name");
                lblmidname.Text = resxSet.GetString("Middle name");
                lbllastname.Text = resxSet.GetString("Last name");
                lblmaiden.Text = resxSet.GetString("Maiden name");
                lblbirth.Text = resxSet.GetString("Birth date");
                lblBsn.Text = resxSet.GetString("Ident BSN");
                lbliban.Text = resxSet.GetString("IBAN");
                lblbic.Text = resxSet.GetString("BIC");
                chkMarried.Text = resxSet.GetString("Married");
                //chkAplicationUser.Text = resxSet.GetString("Application user");
                lblgender.Text = resxSet.GetString("Gender");
                lblEmePerson.Text = resxSet.GetString("Emerg.person");
                lblEmerTel.Text = resxSet.GetString("Emerg.phone");
                // passport
                lblPassname.Text = resxSet.GetString("Passport") + " " + resxSet.GetString("Name").ToLower();
                lblPassnumber.Text = resxSet.GetString("Passport") + " " + resxSet.GetString("Number").ToLower();
                lblBirthplace.Text = resxSet.GetString("Birth place");
                lblIssueplace.Text = resxSet.GetString("Issue place");
                lblIssuedate.Text = resxSet.GetString("Issue date");
                lblValid.Text = resxSet.GetString("Valid to");
                lblNacionality.Text = resxSet.GetString("Nacionality");
                // seconf page
                lblHiredate.Text = resxSet.GetString("Hire date");
                lblContract.Text = resxSet.GetString("Contract nr");
                lblDepart.Text = resxSet.GetString("Department");
                lblFunct.Text = resxSet.GetString("Function");
                lblWishFunc.Text = resxSet.GetString("Wish function");
                //lblStatus.Text = resxSet.GetString("Company ID");
            }
        }
        private void btnDelPic_Click(object sender, EventArgs e)
        {
            picEmployee.Image = null; // GUI.Properties.Resources.DefaultPerson;
            emplyoee.imageEmployee = "";
        }

        private void rgvTel_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTel.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTel.Columns[i].HeaderText != null && resxSet.GetString(rgvTel.Columns[i].HeaderText) != null)
                        rgvTel.Columns[i].HeaderText = resxSet.GetString(rgvTel.Columns[i].HeaderText);
                }
                
            }
          
        }

        private void rgvEmail_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (rgvEmail.Columns.Count > 0) //provera redova u gridu da ne bi pucao
            {
                for (int i = 0; i < rgvEmail.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvEmail.Columns[i].HeaderText != null && resxSet.GetString(rgvEmail.Columns[i].HeaderText) != null)
                            rgvEmail.Columns[i].HeaderText = resxSet.GetString(rgvEmail.Columns[i].HeaderText);

                    }
                    //za velicinu kolina u gridu
                    rgvEmail.Columns[i].Width = (int)(this.CreateGraphics().MeasureString(rgvEmail.Columns[i].HeaderText, this.Font).Width + 108);
                    rgvEmail.Columns[i].MinWidth = rgvEmail.Columns[i].Width+108;

                }
                //za poziciju kolone u gridu
                rgvEmail.Columns.Move(0, rgvEmail.Columns.Count - 1);
                this.rgvEmail.Columns["emailType"].IsVisible = false;
                this.rgvEmail.Columns["idEmployee"].IsVisible = false;
                this.rgvEmail.Columns["idEmpEmail"].IsVisible = false;

            }
           
        }

       
        private bool ValidateEmploye()
        {            
            if (txtLastName.Text == "")
            {
                RadMessageBox.Show("Can't SAVE witout Employee name !");
                txtLastName.Focus();
                return false;
            }

            return true;
        }

        private void SaveEmployee()
        {
            emplyoee.idEmployee = iID;
            emplyoee.firstNameEmployee = txtFirstName.Text;
            emplyoee.initialsEmployee = txtIntials.Text;
            emplyoee.titleEmployee = Convert.ToInt32(ddlTitle.SelectedValue);
            emplyoee.genderEmployee = Convert.ToInt32(ddlGender.SelectedValue);
            emplyoee.Department = Convert.ToInt32(ddlDepartment.SelectedValue);
            emplyoee.Function = Convert.ToInt32(ddlFunction.SelectedValue);
            emplyoee.WishFunction = Convert.ToInt32(ddlWFunction.SelectedValue);
            emplyoee.statusEmployee = Convert.ToInt32(ddlStatus.SelectedValue);
            emplyoee.midNameEmployee = txtMidName.Text;
            emplyoee.lastNameEmployee = txtLastName.Text;
            emplyoee.maidenEmployee = txtMaidenName.Text;
            emplyoee.addressEmployee = txtAddress.Text;
            emplyoee.houseNumberEmployee = txtHouseNr.Text;
            emplyoee.extensionEmployee = txtExtension.Text;
            emplyoee.zipCodeEmployee = txtZipCode.Text;
            emplyoee.cityEmployee = txtCity.Text;
            emplyoee.dtBirthDateEmployee = Convert.ToDateTime(dtBirthDate.Text);
            if (chkMarried.Checked == true)
            {
                emplyoee.isMariedEmployee = true;
            }
            else
            {
                emplyoee.isMariedEmployee = false;
            }

            if (chkAplicationUser.Checked == true)
            {
                emplyoee.isAplicationUser = true;
            }
            else
            {
                emplyoee.isAplicationUser = false;
            }

            emplyoee.isentBsnEmploee = txtIdentBSN.Text;
            emplyoee.bicEmployee = txtBic.Text;
            emplyoee.ibanEmployee = txtIban.Text;
            emplyoee.emergencyPersonEmployee = txtEmergancyPerson.Text;
            emplyoee.emergencyTelEmployee = txtEmergancyTel.Text;
            emplyoee.dtHireDateEmployee = Convert.ToDateTime(dtHireDate.Text);
            emplyoee.contractNumberEmployee = txtCnontractNr.Text;

            //Dodavanje slike
            if (picEmployee.Image != GUI.Properties.Resources.DefaultPerson && picEmployee.Image != null)
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                emplyoee.imageEmployee = Convert.ToBase64String(im.ImageToBytes(picEmployee.Image));

            }

            emplyoee.addressEmployee = pnlAdres.Controls.Find("txt_adr_street", true)[0].Text;
            emplyoee.cityEmployee = pnlAdres.Controls.Find("txt_adr_city", true)[0].Text;
            emplyoee.houseNumberEmployee = pnlAdres.Controls.Find("txt_adr_houseno", true)[0].Text;
            emplyoee.zipCodeEmployee = pnlAdres.Controls.Find("txt_adr_zip", true)[0].Text;
            emplyoee.extensionEmployee = pnlAdres.Controls.Find("txt_adr_ext", true)[0].Text;

            ////Update Pasosa
            //if (iID != -1)
            //{
            //    emppass = new EmployeePassportModel();
            //    // emppass.idemppass = iID;
            //    emppass.idEmployee = iID;
            //    emppass.passname = txtPassName.Text;
            //    emppass.passnumber = txtPassNumber.Text;
            //    emppass.idemppass = IDpass; //za passport
            //    emppass.passbrplace = txtBirthPlace.Text;
            //    emppass.passisplace = txtPassIssuePlace.Text;
            //    emppass.passisued = dtIssueDate.Value;
            //    emppass.passvalid = dtValidTo.Value;
            //    emppass.passnational = Convert.ToInt32(txtNacional.SelectedValue);
            //}


            ///////////////////////////////
            
            //Dodavanje pasosa
            emppass = new EmployeePassportModel();

            emppass.passname = txtPassName.Text;
            emppass.passnumber = txtPassNumber.Text;
            emppass.passbrplace = txtBirthPlace.Text;
            emppass.passisplace = txtPassIssuePlace.Text;
            emppass.passisued = dtIssueDate.Value;
            emppass.passvalid = dtValidTo.Value;
            emppass.passnational = Convert.ToInt32(txtNacional.SelectedValue);
        }

        private void UpdateEmployee()
        {
            EmployeeBUS bus = new EmployeeBUS();
            EmployeePassportBUS pasbus = new EmployeePassportBUS();
            EmployeeTelBUS empTelBus = new EmployeeTelBUS();
            EmployeeEmailBUS eeb = new EmployeeEmailBUS();

            bus.Update(emplyoee, this.Name, Login._user.idUser);
            //==============Za telefon================                          
            Boolean isSuccessfully = false;
            EmployeeTelBUS etb = new EmployeeTelBUS();

            if (empTelList != null)
            {
                saveTel();

                for (int j = 0; j < empTelList.Count; j++)
                {
                    if (etb.Update(empTelList[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for tel " + (j + 1).ToString());
                    }

                }
            }
            //==============Za telefon================


            //===========Za Email===================
            saveEmail();
            eeb = new EmployeeEmailBUS();
            if (empEmailList != null)
            {
                //saveEmail();

                for (int j = 0; j < empEmailList.Count; j++)
                {
                    if (eeb.Update(empEmailList[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for email" + (j + 1).ToString());
                    }
                }
            }
            //==============Za Email==================


            //===========Za Pasos===================
            if (pasbus.Update(emppass, this.Name, Login._user.idUser) == true)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Saved") != null)
                        RadMessageBox.Show(resxSet.GetString("Saved"));
                    else
                        RadMessageBox.Show("Saved");
                }
                // RadMessageBox.Show("Update");                    
            }
            //=================Za Pasos=====================

        }

        private void InsertEmployee()
        {
            EmployeeBUS bus = new EmployeeBUS();
            EmployeePassportBUS pasbus = new EmployeePassportBUS();
            EmployeeTelBUS empTelBus = new EmployeeTelBUS();
            EmployeeEmailBUS eeb = new EmployeeEmailBUS();

            EmployeeBUS eb = new EmployeeBUS();
            //Dodavanje Telefona
            empTelModel = new EmployeeTelModel();
            empEmailModel = new EmployeeEmailModel();

            //bus.Save(client);
            // int employeeId = -1; //  promenljiva zbog insertovanja pasosa


            if (bus.Save(emplyoee, this.Name, Login._user.idUser) == true)

                //========Za pasos============
                employeeId = eb.GeLastEmployeeID();
            if (employeeId != -1)
            {
                emppass.idEmployee = employeeId;
            }
            //=========Za Pasos============

            //    //=====================telefon=====================
            employeeId = eb.GeLastEmployeeID();

            EmployeeTelModel etm = new EmployeeTelModel();
            Boolean isSuccessfully = false;
            EmployeeTelBUS etb = new EmployeeTelBUS();
            etm.idEmployee = employeeId;
            if (empTelList != null)
            {
                saveTel();

                for (int j = 0; j < empTelList.Count; j++)
                {
                    if (etb.Save(empTelList[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for tel " + (j + 1).ToString());
                    }

                }
            }
            saveTel();
            ////====================telefon=========================


            //==================Email==============
            saveEmail();
            employeeId = eb.GeLastEmployeeID();
            EmployeeEmailModel eem = new EmployeeEmailModel();
            eeb = new EmployeeEmailBUS();
            eem.idEmployee = employeeId;
            if (empEmailList != null)
            {
                // saveEmail();

                for (int j = 0; j < empEmailList.Count; j++)
                {
                    if (eeb.Save(empEmailList[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for email " + (j + 1).ToString());
                    }
                }
            }

            //=======================Email=================

            if (pasbus.Save(emppass, this.Name, Login._user.idUser) == true)
            {
                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //    if (resxSet.GetString("Insert") != null)
                //        RadMessageBox.Show(resxSet.GetString("Insert"));
                //    else
                //        RadMessageBox.Show("Insert");
                UpdateOriginalValuesAfterSave();
                this.Close();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeBUS bus = new EmployeeBUS();
            EmployeePassportBUS pasbus = new EmployeePassportBUS();
            EmployeeTelBUS empTelBus = new EmployeeTelBUS();
            EmployeeEmailBUS eeb = new EmployeeEmailBUS();

            if (iID != -1)
            {
                SaveEmployee();
                UpdateEmployee();   
            }

            else
            {                
                bool b = ValidateEmploye();
                if(b == false)
                {
                    return;
                }
                SaveEmployee();
                InsertEmployee();                
            }
            // this.Close();

            UpdateOriginalValuesAfterSave();
        }

        //Dodavanje Telefona  
        private void saveTel()
        {
            empTelList = new List<EmployeeTelModel>();
            for (int i = 0; i < rgvTel.Rows.Count; i++)
            {
                EmployeeTelModel etm = new EmployeeTelModel();
                etm.idEmployee = employeeId;

                etm.idtelemp = Convert.ToInt32(rgvTel.Rows[i].Cells["idtelemp"].Value.ToString());
                if (rgvTel.Rows[i].Cells["telephone"].Value != null)
                {
                    etm.telephone = rgvTel.Rows[i].Cells["telephone"].Value.ToString();
                }

                if (rgvTel.Rows[i].Cells["idEmployee"].Value != null)
                {
                   // etm.idEmployee = emplyoee.idEmployee;
                    if (iID != -1 && iID != 0)
                    {
                        etm.idEmployee = iID;
                    }
                    else
                    {
                        etm.idEmployee = employeeId;
                    }

                }
                else
                {
                    RadMessageBox.Show("You have to insert ID person");
                }

                if (rgvTel.Rows[i].Cells["telephoneType"].Value != null)
                {
                    etm.telephoneType = Convert.ToInt32(rgvTel.Rows[i].Cells["telephoneType"].Value.ToString());
                }

                if (rgvTel.Rows[i].Cells["description"].Value != null)
                    etm.description = rgvTel.Rows[i].Cells["description"].Value.ToString();
                etm.isDefault = Convert.ToBoolean(rgvTel.Rows[i].Cells["isDefault"].Value.ToString());
                empTelList.Add(etm);
            }
        }

        private void updateTel()
        {
            Boolean isSuccessfully = false;
            EmployeeTelBUS etb= new EmployeeTelBUS();
            
                if(empTelList!=null)
                {
                    saveTel();

                    for(int j=0;j<empTelList.Count; j++)
                    {
                        if (etb.Update(empTelList[j], this.Name, Login._user.idUser) == true)
                        {
                            isSuccessfully = true;
                        }
                        else
                        {
                            RadMessageBox.Show("You have not successfully save data for tel " + (j + 1).ToString());
                        }

                    }
                }
        }
        //Update Email
        private void updateEmail()
        {
            Boolean isSuccessfully = false;
            EmployeeEmailBUS eeb = new EmployeeEmailBUS();

            if(empEmailList!=null)
            {
                saveEmail();

                for (int j = 0; j < empEmailList.Count;j++)
                {
                    if (eeb.Update(empEmailList[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for email " + (j + 1).ToString());
                    }
                }
            }
        }
        //Dodavanje Emaila
        private void saveEmail()
        {
            empEmailList = new List<EmployeeEmailModel>();
            for (int i = 0; i < rgvEmail.Rows.Count; i++)
            {
                EmployeeEmailModel eem = new EmployeeEmailModel();
                eem.idEmpEmail = Convert.ToInt32(rgvEmail.Rows[i].Cells["idEmpEmail"].Value.ToString());
                if (rgvEmail.Rows[i].Cells["email"].Value != null)
                {
                    eem.email = rgvEmail.Rows[i].Cells["email"].Value.ToString();
                }
                if (rgvEmail.Rows[i].Cells["idEmployee"].Value != null)
                {
                   // eem.idEmployee = emplyoee.idEmployee;
                    if (iID != -1 && iID != 0)
                    {
                        eem.idEmployee = iID;
                    }
                    else
                    {
                        eem.idEmployee = employeeId;
                    }
                }
                else
                    RadMessageBox.Show("You have to insert ID employee");
                if (rgvEmail.Rows[i].Cells["emailType"].Value != null)
                {
                    eem.emailType = Convert.ToInt32(rgvEmail.Rows[i].Cells["emailType"].Value.ToString());
                }
                empEmailList.Add(eem);
            }
        }

        private void rgvEmail_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                EmployeeEmailBUS eeb = new EmployeeEmailBUS();
                if (eeb.Delete(Convert.ToInt32(mgvt.CurrentRow.Cells["idEmpEmail"].Value.ToString()), this.Name, Login._user.idUser) == false)
                {
                    RadMessageBox.Show("Something went wrong with deleting this email");
                    e.Cancel = true;
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                if (mgvt.CurrentRow.Cells["email"].Value != null)
                {
                    if (mgvt.CurrentRow.Cells["email"].Value.ToString().Trim() == "")
                        e.Cancel = true;
                }
                else
                    e.Cancel = true;
            }
        }

        private void rgvTel_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                if (mgvt.CurrentRow.Cells["telephone"].Value != null)
                {
                    if (mgvt.CurrentRow.Cells["telephone"].Value.ToString().Trim() == "")
                        e.Cancel = true;
                }
                else
                    e.Cancel = true;
            }
        }

      private void btnEmail_Click(object sender, EventArgs e)
        {
            if (Login.isOutlookInstalled == true)
            {
                if (emplyoee.idEmployee != null)
                {
                    EmployeeEmailBUS ebus = new EmployeeEmailBUS();
                    DataTable dt = ebus.GetEmployeeEmailByTypeTable(2, emplyoee.idEmployee);

                    string emailTo = "";
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            emailTo = dr["email"].ToString();
                        }
                    }

                    try
                    {
                        List<string> lstAllRecipients = new List<string>();
                        if (emailTo.Trim() != "")
                            lstAllRecipients.Add(emailTo);

                        if (lstAllRecipients.Count > 0)
                        {
                            Outlook.Application outlookApp = new Outlook.Application();
                            outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);

                            Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                            Outlook.Inspector oInspector = oMailItem.GetInspector;
                            oMailItem.DeleteAfterSubmit = false;

                            // Recipient
                            Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                            foreach (String recipient in lstAllRecipients)
                            {
                                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(recipient);
                                oRecip.Resolve();
                            }
                            //Add CC
                            // Outlook.Recipient oCCRecip = oRecips.Add("THIYAGARAJAN.DURAIRAJAN@testmail.com");
                            //oCCRecip.Type = (int)Outlook.OlMailRecipientType.olCC;
                            //oCCRecip.Resolve();

                            //Add Subject
                            string personame = "";
                            if (emplyoee != null)
                            {
                                if (emplyoee.firstNameEmployee.Trim() != "")
                                    personame += emplyoee.firstNameEmployee.Trim();

                                personame += " ";

                                if (emplyoee.midNameEmployee.Trim() != "")
                                    personame += emplyoee.midNameEmployee.Trim();

                                personame += " ";

                                if (emplyoee.lastNameEmployee.Trim() != "")
                                    personame += emplyoee.lastNameEmployee.Trim();                               
                            }

                            oMailItem.Subject = "";
                            oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText;
                            oMailItem.Body = "Beste " + personame + ", \r\n";
                            Outlook.Folder outlookfolder = Login.GetOutlookBisFolder();
                            if (outlookfolder != null)
                                oMailItem.SaveSentMessageFolder = outlookfolder;


                          //  oMailItem.SaveSentMessageFolder = Login.sentFolder;

                            //Display the mailbox
                            oMailItem.Display(true);
                        }
                        else
                        {
                            translateRadMessageBox msgbox = new translateRadMessageBox();
                            msgbox.translateAllMessageBox("Invalid mail address.");
                        }

                    }
                    catch (Exception objEx)
                    {
                        RadMessageBox.Show(objEx.ToString());
                    }
                }
                else
                {
                    RadMessageBox.Show("You need to add person first.");
                }
            }
            else
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
            }
        }

        void outlookApp_ItemSend(object Item, ref bool Cancel)
        {
            if (Item is Microsoft.Office.Interop.Outlook.MailItem)
            {
                Microsoft.Office.Interop.Outlook.MailItem item = (Microsoft.Office.Interop.Outlook.MailItem)Item;
                item.Save();

                DocumentsBUS sbus = new DocumentsBUS();
                PersonEmailBUS emailbus = new PersonEmailBUS();


                string locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";

                if (!File.Exists(locationOnDisk))
                    item.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                if (emplyoee.idEmployee != 0)
                {
                    DocumentsModel model = new DocumentsModel();
                    model.idContPers = 0;
                    model.idClient = 0;
                    model.descriptionDocument = "Email";
                    model.fileDocument = item.EntryID + ".msg";
                    model.typeDocument = "EML";
                    model.idDocumentStatus = 2;
                    model.idEmployee = emplyoee.idEmployee;
                    model.idResponsableEmployee = emplyoee.idEmployee;
                    model.inOutDocument = 0;
                    model.noteDocument = "Sent Email";
                    model.idArrangement = 0;
                    //model.id

                    model.dtCreated = DateTime.Now;
                    model.dtModified = DateTime.Now;
                    model.userCreated = Login._user.idUser;
                    model.userModified = Login._user.idUser;

                    sbus.Save(model, this.Name, Login._user.idUser);
                }
                
                 Cancel = false;               
            }
        }

        //        //item.Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olSave);
        //        // Cancel = true;
        //        // (item as Microsoft.Office.Interop.Outlook._MailItem).Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olDiscard);
        //        ((Microsoft.Office.Interop.Outlook.ItemEvents_10_Event)item).Close += new Microsoft.Office.Interop.Outlook.ItemEvents_10_CloseEventHandler(ThisAddIn_Close);

        //    }
        //}
        void ThisAddIn_Close(ref bool Cancel)
        {
            //MessageBox.Show("MailItem is closed");
        }

        private void frmEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveEmployee();
            saveTel();
            saveEmail();

            bool changes = emplyoee.CompareWith(emplyoeeFirst);

            EmployeeTelModelComparer empTelComparer = new EmployeeTelModelComparer();
            IEnumerable<EmployeeTelModel> differenceTel = empTelList.Except(empTelListFirst, empTelComparer);
            EmployeeEmailModelComparer empEmailComparer = new EmployeeEmailModelComparer();
            IEnumerable<EmployeeEmailModel> differenceEmail = empEmailList.Except(empEmailListFirst, empEmailComparer);

            bool resultTel = Utils.IsAny(differenceTel);
            bool resultEmail = Utils.IsAny(differenceEmail);

            if (changes == true || resultTel == true || resultEmail == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Save before close ?", "Save");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool validate = ValidateEmploye();
                    if (validate == true)
                    {
                        if (iID != -1)
                            UpdateEmployee();
                        else
                            InsertEmployee();
                    }
                }
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    // NO option
                    emplyoee.CopyValues(emplyoeeFirst);
                } 
            }
        }

        private void txtIntials_TextChanged(object sender, EventArgs e)
        {
            if (!txtIntials.Text.EndsWith(".") && txtIntials.Text.Length > lengthInitials)
            {
                txtIntials.Text += ".";

                txtIntials.SelectionStart = txtIntials.Text.Length;
                lengthInitials = txtIntials.Text.Length;
            }
        }

        private void txtIntials_TextChanging(object sender, Telerik.WinControls.TextChangingEventArgs e)
        {
            lengthInitials = e.OldValue.Length;
        }              
    }
}


      
     

       