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
using Microsoft.Office.Interop.Word;
using System.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using GUI.User_Controls;


namespace GUI
{
    public partial class frmClient : frmTemplate

    {
        private int iClient;        
        ClientModel client;
        ClientModel clientFirst;
        List<ClientTypesModel> ctm;
        List<ClientTelModel> ctelm;
        List<ClientTelModel> ctelmFirst;
        AccDebCreModel debCre;
        //List<ClientTelModel> cfaxm;
        List<CountryModel> ccm;
        List<ClientEmailModel> cemail;
        List<ClientEmailModel> cemailFirst;
        List<ClientAddressModel> clientAddress;
        List<DocumentsModel> clientDoc;
        List<ClientNotesModel> clientNotes;
        List<ContactsModel>clientContacts;
        List<ToDoModel> cliToDo;
        AccountData pg;
        public bool showContractTab = false;
        BindingList<ClientPersonModel> clientAndPersonList;
        List<ClientPersonBUS> cpbus;
        PersonModel persModel;
        AccSettingsModel asmo;
        private BindingList<AccIbanModel> ibanLista;

        // Layout file names for all grids
        private string layoutDocuments;
        private string layoutMemo;
        private string layoutMeetings;
        private string layoutContacts;
        private string layoutTasks;
        private string layoutContracts;
        public bool isSaved = false;
        private int idPerson;
        private string layoutClientPersonView;

        public frmClient()
        {
            iClient = -1;
            client = new ClientModel();
            clientFirst = new ClientModel();

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            InitializeComponent();
          //  btnSave.Click += btnSave_Click;
            for (int i = 0; i < PageViewClient.Pages.Count; i++)
            {
                if (PageViewClient.Pages[i].Name != "tabRelation")
                {
                    PageViewClient.Pages[i].Enabled = false;
                }
            }
        }

        public frmClient(IModel model)
        {
            client = (ClientModel)model;
            clientFirst = new ClientModel((ClientModel) model);

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            this.Text = this.Text + " - " + client.nameClient + " [ " + client.idClient + " ] ";
            iClient = client.idClient;
            InitializeComponent();
         //   btnSave.Click += btnSave_Click;
        }
        public frmClient(IModel model, int idClient)
        {
            client = (ClientModel)model;
            clientFirst = new ClientModel((ClientModel)model);

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            this.Text = this.Text + " - " + client.nameClient;
            iClient = client.idClient;
            InitializeComponent();
          //  btnSave.Click += btnSave_Click;
        }

     
    

        private void frmClient_Load(object sender, EventArgs e)
        {

            layoutDocuments = MainForm.gridFiltersFolder + "\\layoutClientDocuments.xml";
            layoutMemo = MainForm.gridFiltersFolder + "\\layoutClientMemo.xml";
            layoutMeetings = MainForm.gridFiltersFolder + "\\layoutClientMeetings.xml";
            layoutContacts = MainForm.gridFiltersFolder + "\\layoutClientContacts.xml";
            layoutTasks = MainForm.gridFiltersFolder + "\\layoutClientTasks.xml";
            layoutContracts = MainForm.gridFiltersFolder + "\\layoutClientContracts.xml";

            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            btnNewContact.Visibility = ElementVisibility.Visible;
            

            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnNewTask.Visibility = ElementVisibility.Visible;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Visible;
            btnNewContract.Visibility = ElementVisibility.Visible;

            closebuttons();

            setTranslation();


            layoutClientPersonView = MainForm.gridFiltersFolder + "\\layoutClientPersonView.xml";
            if (File.Exists(layoutClientPersonView))
            {
                rgvClientPerson.LoadLayout(layoutClientPersonView);
            }

            if (iClient != -1)
            {
                //======================Punjenje grida za Person=====================
                ClientPersonBUS cpbus = new ClientPersonBUS();
                List<ClientPersonModel> cpmodel = new List<ClientPersonModel>();
                persModel = new PersonModel();

                clientAndPersonList = new BindingList<ClientPersonModel>();

                cpmodel = cpbus.GetAllPersonsFromClient(client.idClient);
                if (cpmodel != null)
                    clientAndPersonList = new BindingList<ClientPersonModel>(cpmodel);

                rgvClientPerson.DataSource = clientAndPersonList;
            }
            
            //==============================Punjenje grida za person================================

            // prikazi contract tab ako korisnik setuje na true. u form loadu proverava

            //========== cita accsettings za account tab
            AccSettingsBUS asbu = new AccSettingsBUS();
            asmo = new AccSettingsModel();
            string cyear = DateTime.Now.Year.ToString();
            asmo = asbu.GetSettingsByID(cyear);
            //================================================== ovde ako nije account user disabe-uje polja na formi ==============
            if (Login._user.isAccountUser == false)
            {
                txtCreAcc.Enabled = false;
                txtDebAcc.Enabled = false;
                chkDebitor.Enabled = false;
                chkCreditor.Enabled = false;
                btnCreAcc.Enabled = false;
                btnDebAcc.Enabled = false;
                
                gridIbans.AllowAddNewRow = false;
                gridIbans.AllowDeleteRow = false;

                txtPayCon.Enabled = false;
                btnPayCon.Enabled = false;

            }
            //=======================================================================================================================

            //set relation dropdownlist
            ctm = new List<ClientTypesModel>();
            ClientTypesBUS ctb = new ClientTypesBUS();
            ctm = ctb.GetAllClientsTypes(Login._user.lngUser);
            ddlRelation.DataSource = ctm;
            ddlRelation.DisplayMember = "nameTypeClient";
            ddlRelation.ValueMember = "idTypeClient";

            if (iClient == -1)
            {
                if (ddlRelation.SelectedValue != null)
                {
                    client.idTypeClient = (int)ddlRelation.SelectedValue;
                    clientFirst.idTypeClient = (int)ddlRelation.SelectedValue;
                }
            }
            // Adrese 

          //  ClientAddress coa = new ClientAddress();
            AdrTwo coa = new AdrTwo();

            coa.Dock = DockStyle.Fill;  //System.Windows.Forms.DockStyle.Fill;
            coa.showBillingAddress = true;
            coa.showEmergencyAddress = false;
            pnAddress.Controls.Add(coa);


            // Account data

            


            //if it is editing exiting client fill his data
            if (client == null)
            {
                tabAccount.Enabled = false;
                tabActivities.Enabled = false;
                tabPerson.Enabled = false;
                tabDocuments.Enabled = false;
                tabMemo.Enabled = false;
            }
            else
            {

                fillClientData();

                 pg = new AccountData(client.accountCodeClient, client.idClient);
                 pg.Dock = System.Windows.Forms.DockStyle.Fill;
               
                 rpAccount.Controls.Add(pg);

              
                //Type client
                if (client.idTypeClient >= 0)
                    ddlRelation.SelectedItem = ddlRelation.Items[ctm.FindIndex(item => item.idTypeClient == client.idTypeClient)];

                //Load Tels
                ClientTelBUS clientTelBUS = new ClientTelBUS();
                ctelm = clientTelBUS.GetAllClientTels(client.idClient);
                rgvTel.DataSource = ctelm;
                this.rgvTel.Columns["idTel"].IsVisible = false;
                this.rgvTel.Columns["idClient"].IsVisible = false;
               // this.rgvTel.Columns["idTelType"].IsVisible = false;
                this.rgvTel.Columns["nameTelType"].IsVisible = false;
                rgvTel.Show();

                ctelmFirst = new List<ClientTelModel>();
                if(ctelm != null)
                {
                    foreach(ClientTelModel t in ctelm)
                    {
                        ctelmFirst.Add(t.ReturnCopy());
                    }
                }

                //ctelFirst = new 
                // Load Emails
                ClientEmailBUS clientEmailBUS = new ClientEmailBUS();
                
                cemail = clientEmailBUS.GetClientEmails(client.idClient);
                rgvEmail.DataSource = cemail;
                this.rgvEmail.Columns["idClient"].IsVisible = false;
                this.rgvEmail.Columns["idEmail"].IsVisible = false;
                rgvEmail.Show();


                cemailFirst = new List<ClientEmailModel>();
                if(cemail != null)
                {
                    foreach(ClientEmailModel m in cemail)
                    {
                        cemailFirst.Add(m.ReturnCopy());
                    }
                }


                clientAddress = new List<ClientAddressModel>();
                ClientAddressBUS pab = new ClientAddressBUS();
                clientAddress = pab.GetClientAddresses(client.idClient);

                //Tel & Email combos
                TypesTelBUS ttb = new TypesTelBUS();
                List<TypesTelModel> ttm = ttb.GetAllTypeTel(Login._user.lngUser);
                GridViewComboBoxColumn ddl = new GridViewComboBoxColumn();
                ddl.DataSource = ttm;
                ddl.DisplayMember = "nameTelType";
                ddl.ValueMember = "idTelType";
                ddl.FieldName = "idTelType";
                ddl.Name = "Type";
                ddl.HeaderText = "Type";
                rgvTel.Columns.Add(ddl);
                rgvTel.Columns["idTel"].IsVisible = false;
                rgvTel.Columns["idTelType"].IsVisible = false;
               // rgvTel.Columns["idTelType"].IsVisible = false;
                rgvTel.Columns["nameTelType"].IsVisible = false;

                TypesEmailBUS teb = new TypesEmailBUS();
                List<TypesEmailModel> tem = teb.GetAllTypeEmail(Login._user.lngUser);
                GridViewComboBoxColumn ddlemail = new GridViewComboBoxColumn();
                ddlemail.DataSource = tem;
                ddlemail.DisplayMember = "nameEmailType";
                ddlemail.ValueMember = "idEmailType";
                ddlemail.FieldName = "idEmailType";
                ddlemail.Name = "Type";
                ddlemail.HeaderText = "Type";
                rgvEmail.Columns.Add(ddlemail);
                rgvEmail.Columns["idEmail"].IsVisible = false;
                rgvEmail.Columns["idEmailType"].IsVisible = false;

                ClientNotesBUS nbus1 = new ClientNotesBUS();
                List<ClientNotesModel> specialNotes = new List<ClientNotesModel>();
                specialNotes = nbus1.GetClientNotesBy2(client.idClient);
                if (specialNotes != null)
                {
                    if (specialNotes.Count > 0)
                    {
                        // tabMemo.BackColor = RadPageViewLabelElement.BackColorProperty.Equals(Text);
                        this.tabMemo.Item.NumberOfColors = 1;
                        this.tabMemo.Item.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        // this.tabMemo.Item.ForeColor = this.tabAccount.BackColor;
                        this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                }


                // Notes
                clientNotes = new ClientNotesBUS().GetClientNotes(client.idClient);
                rgvNote.DataSource = clientNotes;
                if (rgvNote.DataSource == null)
                {
                    List<ClientNotesModel> cliNotes = new List<ClientNotesModel>();
                    rgvNote.DataSource = cliNotes;
                }

                rgvNote.Show();



                if (clientAddress != null)
                {
                    for (int i = 0; i < clientAddress.Count; i++)
                    {
                        if (clientAddress[i].idAddressType == 1)
                        {
                            pnAddress.Controls.Find("txt_adr_street", true)[0].Text = clientAddress[i].street;
                            pnAddress.Controls.Find("txt_adr_city", true)[0].Text = clientAddress[i].city;
                            pnAddress.Controls.Find("txt_adr_houseno", true)[0].Text = clientAddress[i].housenr;
                            pnAddress.Controls.Find("txt_adr_zip", true)[0].Text = clientAddress[i].postalCode;
                            pnAddress.Controls.Find("txt_adr_ext", true)[0].Text = clientAddress[i].extension;
                            if (clientAddress[i].isInternational == true)
                            {
                                RadRadioButton rchk = (RadRadioButton)pnAddress.Controls.Find("rad_adr_inter", true)[0];
                                rchk.CheckState = CheckState.Checked;
                                RadRadioButton rchkNL = (RadRadioButton)pnAddress.Controls.Find("rad_adr_nl", true)[0];
                                rchkNL.CheckState = CheckState.Unchecked;
                                pnAddress.Controls.Find("btn_adr_get", true)[0].Visible = false;
                                pnAddress.Controls.Find("lbl_adr_country", true)[0].Visible = true;
                                pnAddress.Controls.Find("txt_adr_country", true)[0].Visible = true;
                                pnAddress.Controls.Find("txt_adr_country", true)[0].Text = clientAddress[i].country;
                            }

                        }
                        else if (clientAddress[i].idAddressType == 2)
                        {
                            pnAddress.Controls.Find("txt_badr_street", true)[0].Text = clientAddress[i].street;
                            pnAddress.Controls.Find("txt_badr_city", true)[0].Text = clientAddress[i].city;
                            pnAddress.Controls.Find("txt_badr_houseno", true)[0].Text = clientAddress[i].housenr;
                            pnAddress.Controls.Find("txt_badr_zip", true)[0].Text = clientAddress[i].postalCode;
                            pnAddress.Controls.Find("txt_badr_ext", true)[0].Text = clientAddress[i].extension;
                            if (clientAddress[i].isInternational == true)
                            {
                                RadRadioButton rchk = (RadRadioButton)pnAddress.Controls.Find("rad_badr_inter", true)[0];
                                rchk.CheckState = CheckState.Checked;
                                RadRadioButton rchkNL = (RadRadioButton)pnAddress.Controls.Find("rad_badr_nl", true)[0];
                                rchkNL.CheckState = CheckState.Unchecked;
                                pnAddress.Controls.Find("btn_badr_get", true)[0].Visible = false;
                                pnAddress.Controls.Find("lbl_badr_country", true)[0].Visible = true;
                                pnAddress.Controls.Find("txt_badr_country", true)[0].Visible = true;
                                pnAddress.Controls.Find("txt_badr_country", true)[0].Text = clientAddress[i].country;
                            }
                        }
                    }

                }
            }

            btnCopyContract.Click += btnCopyContract_Click;
            btnDeleteContract.Click += btnDeleteContract_Click;

            if (showContractTab == true)
            {
                loadContracts();
                PageViewClient.SelectedPage = tabContract;

            }
        }

        private void fillClientData()
        {
            txtCompanyName.Text = client.nameClient;
            txtContactPerson.Text = client.contactPersonName;
            txtCompanyID.Text = client.idClient.ToString();
            txtCompanyCode.Text = client.accountCodeClient;
            txtWeb.Text = client.webClient;
            txtCompanyName.Text = client.nameClient;
            txtCompanyID.Text = client.idClient.ToString();
            if (client.isActiveClient == true)
            {
                chkActive.CheckState = CheckState.Checked;
            }


            // Load IBANS for Person
            AccIbanBUS ibanBUS = new AccIbanBUS();
            List<AccIbanModel> ibanTmpList = new List<AccIbanModel>();

            ibanTmpList = ibanBUS.GetIBANForClient(client.idClient);
            if (ibanTmpList != null)
                ibanLista = new BindingList<AccIbanModel>(ibanTmpList);
            else
                ibanLista = new BindingList<AccIbanModel>();

            gridIbans.DataSource = ibanLista;
            gridIbans.Columns["Id"].IsVisible = false;
            gridIbans.Columns["accNumber"].IsVisible = false;
            gridIbans.Columns["idClient"].IsVisible = false;
            gridIbans.Columns["idContPers"].IsVisible = false;
            gridIbans.Columns["ibanNumber"].Width = 200;



            AccDebCreBUS dbbus = new AccDebCreBUS();
            if (client.idClient != 0)
            {
                debCre = dbbus.GetClientDebCre(client.idClient);
                if (debCre != null)
                {
                    if (debCre.isDebitor == true)
                        chkDebitor.Checked = true;
                    if (debCre.isCreditor == true)
                        chkCreditor.Checked = true;
                    if (debCre.debAccount != "" && debCre.debAccount != null)
                    {
                        LedgerAccountBUS dbus = new LedgerAccountBUS(Login._bookyear);
                        LedgerAccountModel md = new LedgerAccountModel();
                        md = dbus.GetAccount(debCre.debAccount, Login._bookyear);
                        if (md != null)
                            txtDebAcc.Text = md.numberLedgerAccount + "  " + md.descLedgerAccount;
                    }
                    if (debCre.creditAccount != "" && debCre.creditAccount != null)
                    {
                        LedgerAccountBUS dbus1 = new LedgerAccountBUS(Login._bookyear);
                        LedgerAccountModel md1 = new LedgerAccountModel();
                        md1 = dbus1.GetAccount(debCre.creditAccount, Login._bookyear);
                        if (md1 != null)
                            txtCreAcc.Text = md1.numberLedgerAccount + "  " + md1.descLedgerAccount;
                    }
                    
                    if (debCre.payCondition != null && debCre.payCondition != 0)
                    {
                        AccPaymentBUS apb = new AccPaymentBUS();
                        AccPaymentModel apm = new AccPaymentModel();
                        apm = apb.GetPaymentByID(debCre.payCondition);
                        if (apm != null)
                        {
                            txtPayCon.Text = apm.description;
                        }
                    }

                }
                else
                {
                    debCre = new AccDebCreModel();
                }
            }
            else
            {
                debCre = new AccDebCreModel();
            }
            

           
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblCompanyName.Text) != null)
                    lblCompanyName.Text = resxSet.GetString(lblCompanyName.Text);
                if (resxSet.GetString(lblCompanyID.Text) != null)
                    lblCompanyID.Text = resxSet.GetString(lblCompanyID.Text);
                if (resxSet.GetString(lblCompanyCode.Text) != null)
                    lblCompanyCode.Text = resxSet.GetString(lblCompanyCode.Text);
                if (resxSet.GetString(lblWeb.Text) != null)
                    lblWeb.Text = resxSet.GetString(lblWeb.Text);
                if (resxSet.GetString(lblRelation.Text) != null)
                    lblRelation.Text = resxSet.GetString(lblRelation.Text);
                if (resxSet.GetString(chkActive.Text) != null)
                    chkActive.Text = resxSet.GetString(chkActive.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(lblContactPerson.Text) != null)
                    lblContactPerson.Text = resxSet.GetString(lblContactPerson.Text);      

                // Buttons
                //if (resxSet.GetString(btnSave.Text) != null)
                //    btnSave.Text = resxSet.GetString( btnSave.Text);
                //if (resxSet.GetString(btnNewDoc.Text) != null)
                //    btnNewDoc.Text = resxSet.GetString(btnNewDoc.Text);
                //if (resxSet.GetString(btnDeleteDoc.Text) != null)
                //    btnDeleteDoc.Text = resxSet.GetString(btnDeleteDoc.Text);
                //if (resxSet.GetString(btnNewMemo.Text) != null)
                //    btnNewMemo.Text = resxSet.GetString(btnNewMemo.Text);
                //if (resxSet.GetString(btnDeleteMemo.Text) != null)
                //    btnDeleteMemo.Text = resxSet.GetString(btnDeleteMemo.Text);
                //if (resxSet.GetString(btnEmail.Text) != null)
                //    btnEmail.Text = resxSet.GetString(btnEmail.Text);
                //if (resxSet.GetString(btnReport.Text) != null)
                //    btnReport.Text = resxSet.GetString(btnReport.Text);
                //if (resxSet.GetString(radRibbonMemo.Text) != null)
                //    radRibbonMemo.Text = resxSet.GetString(radRibbonMemo.Text);
                //if (resxSet.GetString(radRibbonDocuments.Text) != null)
                //    radRibbonDocuments.Text = resxSet.GetString(radRibbonDocuments.Text);
                //if (resxSet.GetString(ribbonTab1.Text) != null)
                //    ribbonTab1.Text = resxSet.GetString(ribbonTab1.Text);
                //if (resxSet.GetString(radRibbonTask.Text) != null)
                //    radRibbonTask.Text = resxSet.GetString(radRibbonTask.Text);
                //if (resxSet.GetString(btnDelContact.Text) != null)
                //    btnDelContact.Text = resxSet.GetString(btnDelContact.Text);
                //if (resxSet.GetString(btnDelTask.Text) != null)
                //    btnDelTask.Text = resxSet.GetString(btnDelTask.Text);
                //if (resxSet.GetString(btnNewContact.Text) != null)
                //    btnNewContact.Text = resxSet.GetString(btnNewContact.Text);
                //if (resxSet.GetString(btnCopyContract.Text) != null)
                //    btnCopyContract.Text = resxSet.GetString(btnCopyContract.Text);
                //if (resxSet.GetString(btnDeleteContract.Text) != null)
                //    btnDeleteContract.Text = resxSet.GetString(btnDeleteContract.Text);
                //if (resxSet.GetString(btnNewTask.Text) != null)
                //    btnNewTask.Text = resxSet.GetString(btnNewTask.Text);

                for (int i = 0; i < ribbonExampleMenu.CommandTabs.Count; i++)
                {
                    if (resxSet.GetString(ribbonExampleMenu.CommandTabs[i].Text) != null)
                        ribbonExampleMenu.CommandTabs[i].Text = resxSet.GetString(ribbonExampleMenu.CommandTabs[i].Text);
                    RibbonTab ri = (RibbonTab)ribbonExampleMenu.CommandTabs[i];
                    for (int j = 0; j < ri.Items.Count; j++)
                    {
                        if (ri.Items[j].Visibility == ElementVisibility.Visible)
                        {
                            if (resxSet.GetString(ri.Items[j].Text) != null)
                                ri.Items[j].Text = resxSet.GetString(ri.Items[j].Text);
                            RadRibbonBarGroup rgb = (RadRibbonBarGroup)ri.Items[j];
                            for (int n = 0; n < rgb.Items.Count; n++)
                            {
                                if (resxSet.GetString(rgb.Items[n].Text) != null)
                                    rgb.Items[n].Text = resxSet.GetString(rgb.Items[n].Text);
                            }
                        }
                    }
                }

                for (int i = 0; i < PageViewClient.Pages.Count;i++ )
                {
                    if (resxSet.GetString(PageViewClient.Pages[i].Text) != null)
                        PageViewClient.Pages[i].Text = resxSet.GetString(PageViewClient.Pages[i].Text);
                }

                //-- account
                if (resxSet.GetString(chkDebitor.Text) != null)
                    chkDebitor.Text = resxSet.GetString(chkDebitor.Text);
                if (resxSet.GetString(chkCreditor.Text) != null)
                    chkCreditor.Text = resxSet.GetString(chkCreditor.Text);
                if (resxSet.GetString(lblDebitor.Text) != null)
                    lblDebitor.Text = resxSet.GetString(lblDebitor.Text);
                if (resxSet.GetString(lblCreditor.Text) != null)
                    lblCreditor.Text = resxSet.GetString(lblCreditor.Text);
                if (resxSet.GetString(lblPayCond.Text) != null)
                    lblPayCond.Text = resxSet.GetString(lblPayCond.Text);                
                //
                if (resxSet.GetString(radMenuItemSaveMemoLayout.Text) != null)
                    radMenuItemSaveMemoLayout.Text = resxSet.GetString(radMenuItemSaveMemoLayout.Text);
                if (resxSet.GetString(radMenuItemSaveMeetingsLayout.Text) != null)
                    radMenuItemSaveMeetingsLayout.Text = resxSet.GetString(radMenuItemSaveMeetingsLayout.Text);
                if (resxSet.GetString(radMenuItemSaveTasksLayout.Text) != null)
                    radMenuItemSaveTasksLayout.Text = resxSet.GetString(radMenuItemSaveTasksLayout.Text);
                if (resxSet.GetString(radMenuItemSaveContactsLayout.Text) != null)
                    radMenuItemSaveContactsLayout.Text = resxSet.GetString(radMenuItemSaveContactsLayout.Text);
                if (resxSet.GetString(radMenuItemSaveDocumentsLayout.Text) != null)
                    radMenuItemSaveDocumentsLayout.Text = resxSet.GetString(radMenuItemSaveDocumentsLayout.Text);
                if (resxSet.GetString(radMenuItemSaveContract.Text)!=null)
                    radMenuItemSaveContract.Text = resxSet.GetString(radMenuItemSaveContract.Text);
                               
                
            }
        }
        private void saveAddress()
        {
            clientAddress = new List<ClientAddressModel>();
            ClientAddressModel pm = new ClientAddressModel();
            pm.idClient = client.idClient;
            pm.idAddressType = 1;
            pm.street = pnAddress.Controls.Find("txt_adr_street", true)[0].Text;
            pm.city = pnAddress.Controls.Find("txt_adr_city", true)[0].Text;
            pm.housenr = pnAddress.Controls.Find("txt_adr_houseno", true)[0].Text;
            pm.postalCode = pnAddress.Controls.Find("txt_adr_zip", true)[0].Text;
            pm.extension = pnAddress.Controls.Find("txt_adr_ext", true)[0].Text;
            RadRadioButton rchkNL = (RadRadioButton)pnAddress.Controls.Find("rad_adr_nl", true)[0];
            if (rchkNL.IsChecked == true)
                pm.isInternational = false;
            else
                pm.isInternational = true;
            pm.country = pnAddress.Controls.Find("txt_adr_country", true)[0].Text;
            
            clientAddress.Add(pm);

            pm = new ClientAddressModel();
            pm.idClient = client.idClient;
            pm.idAddressType = 2;
            pm.street = pnAddress.Controls.Find("txt_badr_street", true)[0].Text;
            pm.city = pnAddress.Controls.Find("txt_badr_city", true)[0].Text;
            pm.housenr = pnAddress.Controls.Find("txt_badr_houseno", true)[0].Text;
            pm.postalCode = pnAddress.Controls.Find("txt_badr_zip", true)[0].Text;
            pm.extension = pnAddress.Controls.Find("txt_badr_ext", true)[0].Text;
            RadRadioButton rchkNL2 = (RadRadioButton)pnAddress.Controls.Find("rad_badr_nl", true)[0];
            if (rchkNL2.IsChecked == true)
                pm.isInternational = false;
            else
                pm.isInternational = true;
            pm.country = pnAddress.Controls.Find("txt_badr_country", true)[0].Text;
            
            clientAddress.Add(pm);
        }
        private void saveEmail()
        {
            cemail = new List<ClientEmailModel>();
            for (int i = 0; i < rgvEmail.Rows.Count; i++)
            {
                ClientEmailModel pem = new ClientEmailModel();
                pem.idEmail = Convert.ToInt32(rgvEmail.Rows[i].Cells["idEmail"].Value.ToString());
                if (rgvEmail.Rows[i].Cells["email"].Value != null)
                {
                    pem.email = rgvEmail.Rows[i].Cells["email"].Value.ToString();
                }
                if (rgvEmail.Rows[i].Cells["idClient"].Value != null)
                {
                    pem.idClient = client.idClient;
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("You have to insert ID Client") != null)
                            RadMessageBox.Show(resxSet.GetString("You have to insert ID Client"));
                        else
                            RadMessageBox.Show("You have to insert ID Client");
                    }
                    //RadMessageBox.Show("You have to insert ID Client");
                }
                   
                if (rgvEmail.Rows[i].Cells["idEmailType"].Value != null)
                {
                    pem.idEmailType = Convert.ToInt32(rgvEmail.Rows[i].Cells["idEmailType"].Value.ToString());
                }
                pem.isCommunication = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isCommunication"].Value.ToString());
                pem.isInvoicing = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isInvoicing"].Value.ToString());
                pem.isNewsletters = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isNewsletters"].Value.ToString());
                cemail.Add(pem);

            }
        }
        private void saveTel()
        {
            ctelm = new List<ClientTelModel>();
            for (int i = 0; i < rgvTel.Rows.Count; i++)
            {
                ClientTelModel ptm = new ClientTelModel();
                ptm.idTel = Convert.ToInt32(rgvTel.Rows[i].Cells["idTel"].Value.ToString());
                if (rgvTel.Rows[i].Cells["numberTel"].Value != null)
                {
                    ptm.numberTel = rgvTel.Rows[i].Cells["numberTel"].Value.ToString();
                }
                if (rgvTel.Rows[i].Cells["idClient"].Value != null)
                {
                    ptm.idClient = client.idClient;
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("You have to insert ID Client") != null)
                            RadMessageBox.Show(resxSet.GetString("You have to insert ID Client"));
                        else
                            RadMessageBox.Show("You have to insert ID Client");
                    }

                   // RadMessageBox.Show("You have to insert ID Client");
                }
                    
                if (rgvTel.Rows[i].Cells["idTelType"].Value != null)
                {
                    ptm.idTelType = Convert.ToInt32(rgvTel.Rows[i].Cells["idTelType"].Value.ToString());
                }
                if (rgvTel.Rows[i].Cells["descriptionTel"].Value != null)
                    ptm.descriptionTel = rgvTel.Rows[i].Cells["descriptionTel"].Value.ToString();
                ptm.idDefaultTel = Convert.ToBoolean(rgvTel.Rows[i].Cells["idDefaultTel"].Value.ToString());
                ctelm.Add(ptm);
            }
        }

        private void rgvTel_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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

        private void rgvEmail_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvEmail.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvEmail.Columns[i].HeaderText != null && resxSet.GetString(rgvEmail.Columns[i].HeaderText) != null)
                        rgvEmail.Columns[i].HeaderText = resxSet.GetString(rgvEmail.Columns[i].HeaderText);
                }
            }
        }

        private void rgvEmail_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                ClientEmailBUS peb = new ClientEmailBUS();
                if (peb.Delete(Convert.ToInt32(mgvt.CurrentRow.Cells["idEmail"].Value.ToString()), this.Name, Login._user.idUser) == false)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Something went wrong with deleting this email") != null)
                            RadMessageBox.Show(resxSet.GetString("Something went wrong with deleting this email"));
                        else
                            RadMessageBox.Show("Something went wrong with deleting this email");
                    }
                   // RadMessageBox.Show("Something went wrong with deleting this email");
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
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                ClientTelBUS ptb = new ClientTelBUS();
                if (ptb.Delete(Convert.ToInt32(mgvt.CurrentRow.Cells["idTel"].Value.ToString()), this.Name, Login._user.idUser) == false)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Something went wrong with deleting this telephone") != null)
                            RadMessageBox.Show(resxSet.GetString("Something went wrong with deleting this telephone"));
                        else
                            RadMessageBox.Show("Something went wrong with deleting this telephone");
                    }
                    //RadMessageBox.Show("Something went wrong with deleting this telephone");
                    e.Cancel = true;
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                if (mgvt.CurrentRow.Cells["numberTel"].Value != null)
                {
                    if (mgvt.CurrentRow.Cells["numberTel"].Value.ToString().Trim() == "")
                        e.Cancel = true;
                }
                else
                    e.Cancel = true;
            }
        }

        private void closebuttons()
        {
            
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Visible;
            btnNewContact.Visibility = ElementVisibility.Visible;
            
            radRibbonTask.Visibility = ElementVisibility.Visible;
            btnNewTask.Visibility = ElementVisibility.Visible;
            btnDelContact.Visibility = ElementVisibility.Collapsed;
            btnDelTask.Visibility = ElementVisibility.Collapsed;

        }

        private void PageViewClient_SelectedPageChanged(object sender, EventArgs e)
        {
            
             RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;

            


           // if (sName != "tabVoluntary")
            switch (sName)
            {
                case "tabRelation":
                    closebuttons();
                    break;
                case "tabAccount":
                    closebuttons();
                    break;
                case "tabPerson":
                    closebuttons();
                    break;
                case "tabActivities":
                    closebuttons();
                    LoadMeetings();
                    pgvActivities_Click(sender, e);
                    pgvActivities_SelectedPageChanged(sender, e);
                    break;
                case "tabDocuments":
                    closebuttons();
                    btnDeleteDoc.Visibility = ElementVisibility.Visible;

                    rgvDocuments.DataSource = new DocumentsBUS().GetClientDoc(client.idClient, Login._user.lngUser);
                    // saki

                    if (rgvDocuments.DataSource == null)
                    {
                        List<DocumentsModel> docmdl = new List<DocumentsModel>();
                        rgvDocuments.DataSource = docmdl;
                    }
                    // rgvDocuments.Columns["dinoutdoc"].IsVisible = false;
                    rgvDocuments.Columns["idProject"].IsVisible = false;
                    rgvDocuments.Columns["idClient"].IsVisible = false;
                    rgvDocuments.Columns["idContPers"].IsVisible = false;
                    rgvDocuments.Columns["idEmployee"].IsVisible = false;
                    rgvDocuments.Columns["idResponsableEmployee"].IsVisible = false;
                    rgvDocuments.Columns["idDocumentStatus"].IsVisible = false;
                    rgvDocuments.Columns["dtModified"].IsVisible = false;
                    rgvDocuments.Columns["userModified"].IsVisible = false;
                    rgvDocuments.Columns["userCreated"].IsVisible = false;
                    rgvDocuments.Columns["idLayout"].IsVisible = false;
                    rgvDocuments.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                    rgvDocuments.Show();

                    break;

                case "tabMemo":
                    closebuttons();
                    btnDeleteMemo.Visibility = ElementVisibility.Visible;
                  
                     rgvNote.DataSource = new ClientNotesBUS().GetClientNotes(client.idClient);
                    // saki

                     if (rgvNote.DataSource == null)
                    {
                        List<ClientNotesModel> clientNotes = new List<ClientNotesModel>();
                        rgvNote.DataSource = clientNotes;
                    }
                     rgvNote.Show();
                   
                    break;
                case "tabContract":

                    closebuttons();
                    btnCopyContract.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    btnCopyContract.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    btnDeleteContract.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    loadContracts();


                    break;

            }

        
        }

        private void rgvDocuments_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (rgvDocuments.Rows.Count > 0)
            {
                int index = rgvDocuments.SelectedRows[0].Index;
                int iID = Int32.Parse(rgvDocuments.SelectedRows[0].Cells["idDocument"].Value.ToString());
                int idClient = client.idClient;


                using (frmDocuments frm = new frmDocuments(iID, idClient))
                {
                    frm.ShowDialog();
                    if (frm.modelChanged == true)
                    {
                        DocumentsBUS nbus = new DocumentsBUS();

                        clientDoc = nbus.GetClientDoc(client.idClient, Login._user.lngUser);
                        rgvDocuments.DataSource = null;
                        rgvDocuments.DataSource = clientDoc;
                        if (rgvDocuments.Rows.Count > index)
                        {
                            rgvDocuments.Rows[index].IsCurrent = true;
                            rgvDocuments.Rows[index].IsSelected = true;
                        }
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


            }
        }

        private void rgvDocuments_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvDocuments.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvDocuments.Columns[i].HeaderText != null && resxSet.GetString(rgvDocuments.Columns[i].HeaderText) != null)
                        rgvDocuments.Columns[i].HeaderText = resxSet.GetString(rgvDocuments.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutDocuments))
            {
                rgvDocuments.LoadLayout(layoutDocuments);
            }
            else
            {
                rgvDocuments.Columns["idProject"].IsVisible = false;
                rgvDocuments.Columns["idClient"].IsVisible = false;
                rgvDocuments.Columns["idContPers"].IsVisible = false;
                rgvDocuments.Columns["idEmployee"].IsVisible = false;
                rgvDocuments.Columns["idResponsableEmployee"].IsVisible = false;
                rgvDocuments.Columns["idDocumentStatus"].IsVisible = false;
                rgvDocuments.Columns["dtModified"].IsVisible = false;
                rgvDocuments.Columns["userModified"].IsVisible = false;
                rgvDocuments.Columns["userCreated"].IsVisible = false;
                rgvDocuments.Columns["idLayout"].IsVisible = false;
            }
        }

        private void LoadMeetings()
        {
            if (iClient != -1)
            {
                rgvMeetings.DataSource = new AppointmentsBUS().GetAppointmentsByClient(client.idClient, Login._user.lngUser);
                // saki

                if (rgvMeetings.DataSource == null)
                {
                    List<BISAppointment> clientMeetings = new List<BISAppointment>();
                    rgvMeetings.DataSource = clientMeetings;
                }

                //rgvMeetings.Columns["id"].IsVisible = false;
                ////   rgvMeetings.Columns["dtEnd"].IsVisible = false;
                //rgvMeetings.Columns["category"].IsVisible = false;
                //rgvMeetings.Columns["priority"].IsVisible = false;
                //rgvMeetings.Columns["status"].IsVisible = false;
                //rgvMeetings.Columns["client"].IsVisible = false;
                //rgvMeetings.Columns["clientName"].IsVisible = false;
                //rgvMeetings.Columns["contact"].IsVisible = false;
                //rgvMeetings.Columns["contactName"].IsVisible = false;
                //rgvMeetings.Columns["project"].IsVisible = false;
                //rgvMeetings.Columns["projectName"].IsVisible = false;
                //rgvMeetings.Columns["owner"].IsVisible = false;
                //rgvMeetings.Columns["ownerName"].IsVisible = false;
                //rgvMeetings.Columns["responsible"].IsVisible = false;
                //rgvMeetings.Columns["responsibleName"].IsVisible = false;
                //rgvMeetings.Columns["isAllDay"].IsVisible = false;
                //rgvMeetings.Columns["background"].IsVisible = false;
                //rgvMeetings.Columns["showtime"].IsVisible = false;
                //rgvMeetings.Columns["Reminder"].IsVisible = false;
                //rgvMeetings.Columns["ReminderSnoozed"].IsVisible = false;
                //rgvMeetings.Columns["ReminderDismissed"].IsVisible = false;
                rgvMeetings.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                rgvMeetings.Show();

            }
        }


        private void loadContracts()
        {
            if (iClient != -1)
            {
                PriceListBUS pbus = new PriceListBUS();
                List<PriceListModel> pmodel = new List<PriceListModel>();
                pmodel = pbus.GetAllPriceLists(client.idClient);
                                
                // saki

                if (pmodel == null)
                {
                    List<PriceListModel> clientPriceList = new List<PriceListModel>();
                    rgvContract.DataSource = clientPriceList;
                }
                else
                {
                    rgvContract.DataSource = pmodel;
                }

                rgvContract.Show();
            }
        }

        private void pgvActivities_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv1 = (RadPageView)sender;
            string sName1 = ((RadPageView)sender).SelectedPage.Name;

            switch (sName1)
            {
                case "tabMeetings":

                    closebuttons();
                    LoadMeetings();


                    break;
                case "tabContacts":
                    closebuttons();
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    //btnNewTask.Visibility = ElementVisibility.Visible;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    //radRibbonContact.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Visible;
                    btnNewTask.Visibility = ElementVisibility.Visible;

                    rgvContacts.DataSource = new ContactsBUS().GetContactsByClient(client.idClient);
                   

                    if (rgvContacts.DataSource == null)
                    {
                        List<ContactsModel> clientContacts = new List<ContactsModel>();
                        rgvContacts.DataSource = clientContacts;
                    }

                    rgvContacts.Columns["idContact"].IsVisible = false;
                    rgvContacts.Columns["idClient"].IsVisible = false;
                    rgvContacts.Columns["idContPers"].IsVisible = false;
                    rgvContacts.Columns["idProject"].IsVisible = false;
                    rgvContacts.Columns["idContactReason"].IsVisible = false;
                    rgvContacts.Columns["idContactType"].IsVisible = false;
                    rgvContacts.Columns["idCreator"].IsVisible = false;


                    rgvContacts.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                    rgvContacts.Show();
                   
                    break;
                case "tabTasks":
                    closebuttons();
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    //radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelTask.Visibility = ElementVisibility.Visible;
                    btnNewContact.Visibility = ElementVisibility.Visible;
                   
                    //btnDelContact.Visibility = ElementVisibility.Collapsed;

                    rgvToDo.DataSource = new ToDoBUS().GetToDoClient(client.idClient);
                    // saki

                    if (rgvToDo.DataSource == null)
                    {
                        List<ToDoModel> clientToDo = new List<ToDoModel>();
                        rgvToDo.DataSource = clientToDo;
                    }

                    rgvToDo.Columns["idToDo"].IsVisible = false;
                    rgvToDo.Columns["idClient"].IsVisible = false;
                    rgvToDo.Columns["idContPers"].IsVisible = false;
                    rgvToDo.Columns["idProject"].IsVisible = false;
                    rgvToDo.Columns["idOwner"].IsVisible = false;
                    rgvToDo.Columns["idEmployee"].IsVisible = false;
                    rgvToDo.Columns["idPriorityToDo"].IsVisible = false;
                    rgvToDo.Columns["idStatusToDo"].IsVisible = false;
                    rgvToDo.Columns["idToDoType"].IsVisible = false;
                    //rgvToDo.Columns["isRemider"].IsVisible = false;

                    rgvToDo.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                    rgvToDo.Show();
                    break;
               
                
            }


        }

        private void rgvNote_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvNote.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvNote.Columns[i].HeaderText != null && resxSet.GetString(rgvNote.Columns[i].HeaderText) != null)
                        rgvNote.Columns[i].HeaderText = resxSet.GetString(rgvNote.Columns[i].HeaderText);
                }
                if (rgvNote.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                {
                    if (rgvNote.Columns[i].Name.ToLower() != "dtModified".ToLower() && rgvNote.Columns[i].Name.ToLower() != "dtCreated".ToLower())
                    {
                        rgvNote.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                    }
                }
            }


            if (File.Exists(layoutMemo))
            {
                rgvNote.LoadLayout(layoutMemo);
            }
            else
            {

                rgvNote.Columns["idClient"].IsVisible = false;
                rgvNote.Columns["idEmployee"].IsVisible = false;
                rgvNote.Columns["dtModified"].IsVisible = false;
                rgvNote.Columns["idUserModified"].IsVisible = false;
                rgvNote.Columns["idUserCreated"].IsVisible = false;
                rgvNote.Columns["idTypeNote"].IsVisible = false;

            }

            rgvNote.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvNote.Show();
        }

        private void rgvNote_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (rgvNote.Rows.Count > 0)
            {
                int iID = Int32.Parse(rgvNote.SelectedRows[0].Cells["idNote"].Value.ToString());
                int idClient = client.idClient;


                using (frmClientNotes frm = new frmClientNotes(iID, idClient))
                {
                    frm.ShowDialog();
                    ClientNotesBUS nbus1 = new ClientNotesBUS();
                    List<ClientNotesModel> specialNotes = new List<ClientNotesModel>();
                    specialNotes = nbus1.GetClientNotesBy2(client.idClient);
                    if (specialNotes != null)
                    {
                        if (specialNotes.Count > 0)
                        {
                            // tabMemo.BackColor = RadPageViewLabelElement.BackColorProperty.Equals(Text);
                            this.tabMemo.Item.NumberOfColors = 1;
                            this.tabMemo.Item.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            // this.tabMemo.Item.ForeColor = this.tabAccount.BackColor;
                            this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                    else
                    {
                        this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                    }

                    if (frm.modelChanged == true)
                    {
                        ClientNotesBUS nbus = new ClientNotesBUS();

                        clientNotes = nbus.GetClientNotes(client.idClient);
                        rgvNote.DataSource = null;
                        rgvNote.DataSource = clientNotes;
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

            }
        }

        private void btnNewMemo_Click(object sender, EventArgs e)
        {
            using (frmClientNotes frm = new frmClientNotes(client.idClient))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    ClientNotesBUS nbus = new ClientNotesBUS();
                    clientNotes = nbus.GetClientNotes(client.idClient);
                    rgvNote.DataSource = null;
                    rgvNote.DataSource = clientNotes;
                }
            }
        }

        private void btnDeleteMemo_Click(object sender, EventArgs e)
        {
            if (rgvNote.SelectedRows.Count > 0)
            {
                int id;
                string text = "";

                if (rgvNote.SelectedRows[0].Cells["idNote"].Value != null)
                {
                    id = Int32.Parse(rgvNote.SelectedRows[0].Cells["idNote"].Value.ToString().Trim());
                    text = rgvNote.SelectedRows[0].Cells["noteText"].Value.ToString();
                    DialogResult dr = RadMessageBox.Show("Are you sure that you want to delete note ? \n " + text, "", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        ClientNotesBUS nb = new ClientNotesBUS();
                        nb.Delete(id, this.Name, Login._user.idUser);
                        clientNotes = nb.GetClientNotes(client.idClient);
                        rgvNote.DataSource = null;
                        rgvNote.DataSource = clientNotes;
                    }
                }
            }
            else
            {
                if (rgvNote.Rows.Count > 0)
                    RadMessageBox.Show("First you have to select note that you want to delete");
                else
                    RadMessageBox.Show("You don't have notes to delete");
            }
        }

        private void rgvMeetings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = rgvMeetings.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                BISAppointment appID = (BISAppointment)info.DataBoundItem;
                IEvent ev = MainForm.meetingScheduler.radScheduler1.Appointments.GetById(appID.Id);
                using (MeetingEditAppointment editAppForm = new MeetingEditAppointment())
                {
                    editAppForm.ThemeName = "Windows8";
                    editAppForm.EditAppointment(ev, MainForm.meetingScheduler.radScheduler1);
                    editAppForm.ShowDialog();
                    //dodala Neta jer nije radio refresh
                    rgvMeetings.DataSource = null;
                    rgvMeetings.DataSource = new AppointmentsBUS().GetAppointmentsByClient(client.idClient, Login._user.lngUser);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }


        private void rgvToDo_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvToDo.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvToDo.Rows.Count > 0)
                {
                    int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    int idClient = client.idClient;

                    string what = "open";
                    using (frmTasks frm = new frmTasks(iID, what, idClient))
                    {
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            ToDoBUS nbus = new ToDoBUS();

                            cliToDo = nbus.GetToDoClient(client.idClient);
                            rgvToDo.DataSource = null;
                            rgvToDo.DataSource = cliToDo;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
            }

        }
        private void pgvActivities_Click(object sender, EventArgs e)

        {
            if (pgvActivities.SelectedPage.Name == "tabTasks")
            {
                radRibbonContact.Visibility = ElementVisibility.Collapsed;
                radRibbonTask.Visibility = ElementVisibility.Visible;
            }
            if (pgvActivities.SelectedPage.Name == "tabContacts")
            {
                radRibbonContact.Visibility = ElementVisibility.Visible;
                radRibbonTask.Visibility = ElementVisibility.Collapsed;
            }

        
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            int iID = -1;               //Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
            int idClient = client.idClient;

            string what = "new";
            string stype = "client";
            int Person = 0;
            using (frmContacts frm = new frmContacts(iID, what, stype, idClient, Person))
            {
                //frmContacts frm = new frmContacts();
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    ContactsBUS nbus3 = new ContactsBUS();

                    clientContacts = nbus3.GetContactsByClient(client.idClient);
                    rgvContacts.DataSource = null;
                    rgvContacts.DataSource = clientContacts;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnDeleteContract_Click(object sender, EventArgs e)
        {
            if (rgvContract != null)
            {
                if (rgvContract.SelectedRows != null)
                {
                    if (rgvContract.SelectedRows.Count > 0)
                    {
                        PriceListBUS pb = new PriceListBUS();
                        if (pb.checkForDelete(Convert.ToInt32(rgvContract.SelectedRows[0].Cells["idPriceList"].Value.ToString()))<=0)
                        {
                            if (pb.Delete(Convert.ToInt32(rgvContract.SelectedRows[0].Cells["idPriceList"].Value.ToString()), this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }
                            else
                            {
                                rgvContract.DataSource = pb.GetAllPriceLists(client.idClient);
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Probably you already have booked some articles from contract please check!");
                        }

                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have to have at least one selected contract so you can delete them!");
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to have at least one selected contract so you can delete them!");
                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to have at least one contract so you can delete them!");
            }
        }

        private void btnCopyContract_Click(object sender, EventArgs e)
        {
            if (rgvContract.SelectedRows != null)
            {
                if (rgvContract.SelectedRows.Count > 0)
                {
                    PriceListModel priceListModel = (PriceListModel) rgvContract.SelectedRows[0].DataBoundItem;
                    using (frmPriceList frm = new frmPriceList(priceListModel, client.idClient))
                    {
                        frm.isCopy = true;
                        frm.ShowDialog();
                        loadContracts();
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }

        private void rgvContacts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = rgvContacts.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                if (rgvContacts.Rows.Count > 0)
                {

                    int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                    int iPerson = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContPers"].Value.ToString());
                    

                    string what = "open";
                    string stype = "client";
                    int idClient = client.idClient;
                    using (frmContacts frm = new frmContacts(iID, what, stype, idClient, iPerson))
                    {
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            ContactsBUS nbus1 = new ContactsBUS();
                            clientContacts = nbus1.GetContactsByClient(client.idClient);
                            rgvContacts.DataSource = null;
                            rgvContacts.DataSource = clientContacts;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }

        private void btnDelContact_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Contact?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                    ContactsBUS del1 = new ContactsBUS();
                    del1.Delete(iID, this.Name, Login._user.idUser);
                    clientContacts = del1.GetContactsByClient(client.idClient);
                    rgvContacts.DataSource = null;
                    rgvContacts.DataSource = clientContacts;
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting Contact. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            string what = "new";
            string stype = "client";
            int iID = -1;
            int ixPerson = 0;
            using (frmTasks frm = new frmTasks(iID, ixPerson, what, stype, client.idClient))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    ToDoBUS nbus = new ToDoBUS();

                    cliToDo = nbus.GetToDoClient(client.idClient);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = cliToDo;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnDelTask_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Task?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    ToDoBUS del = new ToDoBUS();
                    del.Delete(iID, this.Name, Login._user.idUser);
                    cliToDo = del.GetToDoClient(client.idClient);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = cliToDo;
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting document. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void rgvMeetings_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvMeetings.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvMeetings.Columns[i].HeaderText != null && resxSet.GetString(rgvMeetings.Columns[i].HeaderText) != null)
                        rgvMeetings.Columns[i].HeaderText = resxSet.GetString(rgvMeetings.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutMeetings))
            {
                rgvMeetings.LoadLayout(layoutMeetings);
            }
            else
            {
                rgvMeetings.Columns["id"].IsVisible = false;
                //   rgvMeetings.Columns["dtEnd"].IsVisible = false;
                rgvMeetings.Columns["category"].IsVisible = false;
                rgvMeetings.Columns["priority"].IsVisible = false;
                rgvMeetings.Columns["status"].IsVisible = false;
                rgvMeetings.Columns["client"].IsVisible = false;
                rgvMeetings.Columns["clientName"].IsVisible = false;
                rgvMeetings.Columns["contact"].IsVisible = false;
                rgvMeetings.Columns["contactName"].IsVisible = false;
                rgvMeetings.Columns["project"].IsVisible = false;
                rgvMeetings.Columns["projectName"].IsVisible = false;
                rgvMeetings.Columns["owner"].IsVisible = false;
                rgvMeetings.Columns["ownerName"].IsVisible = false;
                rgvMeetings.Columns["responsible"].IsVisible = false;
                rgvMeetings.Columns["responsibleName"].IsVisible = false;
                rgvMeetings.Columns["isAllDay"].IsVisible = false;
                rgvMeetings.Columns["background"].IsVisible = false;
                rgvMeetings.Columns["showtime"].IsVisible = false;
                rgvMeetings.Columns["Reminder"].IsVisible = false;
                rgvMeetings.Columns["ReminderSnoozed"].IsVisible = false;
                //rgvMeetings.Columns["ReminderDismissed"].IsVisible = false;
            }

            rgvMeetings.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvMeetings.Show();
        }

        private void rgvContacts_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            radRibbonContact.Visibility = ElementVisibility.Visible;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;

            for (int i = 0; i < rgvContacts.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvContacts.Columns[i].HeaderText != null && resxSet.GetString(rgvContacts.Columns[i].HeaderText) != null)
                        rgvContacts.Columns[i].HeaderText = resxSet.GetString(rgvContacts.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutContacts))
            {
                rgvContacts.LoadLayout(layoutContacts);
            }
            else
            {
                rgvContacts.Columns["idContact"].IsVisible = false;
                rgvContacts.Columns["idClient"].IsVisible = false;
                rgvContacts.Columns["idContPers"].IsVisible = false;
                rgvContacts.Columns["idProject"].IsVisible = false;
                rgvContacts.Columns["idContactReason"].IsVisible = false;
                rgvContacts.Columns["idContactType"].IsVisible = false;
                rgvContacts.Columns["idCreator"].IsVisible = false;
            }

            rgvContacts.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvContacts.Show();
        }

        private void rgvToDo_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Visible;
            for (int i = 0; i < rgvToDo.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvToDo.Columns[i].HeaderText != null && resxSet.GetString(rgvToDo.Columns[i].HeaderText) != null)
                        rgvToDo.Columns[i].HeaderText = resxSet.GetString(rgvToDo.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutTasks))
            {
                rgvToDo.LoadLayout(layoutTasks);
            }
            else
            {
                rgvToDo.Columns["idToDo"].IsVisible = false;
                rgvToDo.Columns["idClient"].IsVisible = false;
                rgvToDo.Columns["idContPers"].IsVisible = false;
                rgvToDo.Columns["idProject"].IsVisible = false;
                rgvToDo.Columns["idOwner"].IsVisible = false;
                rgvToDo.Columns["idEmployee"].IsVisible = false;
                rgvToDo.Columns["idPriorityToDo"].IsVisible = false;
                rgvToDo.Columns["idStatusToDo"].IsVisible = false;
                rgvToDo.Columns["idToDoType"].IsVisible = false;
                rgvToDo.Columns["idContact"].IsVisible = false;
            }

            rgvToDo.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvToDo.Show();
        }

        private void rgvToDo_CellDoubleClick_1(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvToDo.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvToDo.Rows.Count > 0)
                {
                    int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    int iPerson = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idContPers"].Value.ToString());
                    int idClient = client.idClient;

                    string what = "open";
                    string stype = "client";
                    using (frmTasks frm = new frmTasks(iID, iPerson, what, stype, idClient))
                    {
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            ToDoBUS nbus = new ToDoBUS();

                            cliToDo = nbus.GetToDoClient(client.idClient);
                            rgvToDo.DataSource = null;
                            rgvToDo.DataSource = cliToDo;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
            }
        }

        private void btnNewDoc_Click(object sender, EventArgs e)
        {
            string what = "client";
            int iDocument = -1;
            using (frmDocuments frm = new frmDocuments(iDocument, client.idClient, what))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    DocumentsBUS nbus = new DocumentsBUS();

                    clientDoc = nbus.GetClientDoc(client.idClient, Login._user.lngUser);
                    rgvDocuments.DataSource = null;
                    rgvDocuments.DataSource = clientDoc;
                }
            }
        }

        private void btnDeleteDoc_Click(object sender, EventArgs e)
        {
            if (rgvDocuments.SelectedRows.Count > 0)
            {
                int id;
                string text = "";

                if (rgvDocuments.SelectedRows[0].Cells["idDocument"].Value != null)
                {
                    id = Int32.Parse(rgvDocuments.SelectedRows[0].Cells["idDocument"].Value.ToString().Trim());
                    text = rgvDocuments.SelectedRows[0].Cells["fileDocument"].Value.ToString();
                    DialogResult dr = RadMessageBox.Show("Are you sure that you want to delete document ? \n " + text, "", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        DocumentsBUS db = new DocumentsBUS();
                        db.RemoveClientFromDocument(id, this.Name, Login._user.idUser);
                        clientDoc = db.GetClientDoc(client.idClient, Login._user.lngUser);
                        rgvDocuments.DataSource = null;
                        rgvDocuments.DataSource = clientDoc;
                    }
                }
            }
            else
            {
                if (rgvDocuments.Rows.Count > 0)
                    RadMessageBox.Show("First you have to select document that you want to delete");
                else
                    RadMessageBox.Show("You don't have documents to delete");
            }
        }

        //private void btnEmail_Click(object sender, EventArgs e)
        //{
        //    if (client.idClient != null && client.idClient != 0)
        //    {
        //        BIS.Core.dbConnection dbcon = new BIS.Core.dbConnection();
        //        BIS.DAO.ClientEmailDAO clientmaildao = new BIS.DAO.ClientEmailDAO();
        //        System.Data.DataTable dt = clientmaildao.GetClientEmails(client.idClient);

        //        string emailTo = "";
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow dr = dt.Rows[0];
        //                emailTo = dr["email"].ToString();
        //            }
        //            else
        //            {
        //                RadMessageBox.Show("No Email address");
        //                return;
        //            }
        //        }

        //        int idPers = 0;

        //        Email.NewEmailForm fEmail = new Email.NewEmailForm("", dbcon.Conn, (Int32)Login._user.idUser, client.idClient, idPers, (Int32)25, "se057;bizzv&01", "smtp.xs4all.nl", Login._user.emailUser, emailTo, "", "");
        //        fEmail.Show();
        //    }
        //    else
        //    {
        //        RadMessageBox.Show("You need to add client first.");
        //    }
        //}
      private void btnEmail_Click(object sender, EventArgs e)
        {
            if (Login.isOutlookInstalled == true)
            {
                if (client.idClient != 0)
                {
                    ClientEmailBUS ebus = new ClientEmailBUS();
                    System.Data.DataTable dt = ebus.GetClientEmailsTable(client.idClient);

                    string emailTo = "";
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            emailTo = dr["email"].ToString();
                        }
                        else
                        {
                            RadMessageBox.Show("No Email address");
                            return;
                        }
                    }

                    try
                    {
                        List<string> lstAllRecipients = new List<string>();
                        //Below is hardcoded - can be replaced with db data
                        if (emailTo.Trim() != "")
                            lstAllRecipients.Add(emailTo);
                        //lstAllRecipients.Add("chandan.kumarpanda@testmail.com");

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
                          //  oMailItem.Subject = "Geachte heer/mevrouw, \r\n";
                            oMailItem.Subject = "";
                            oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText;
                            oMailItem.Body = "Geachte heer/mevrouw, \r\n";

                            Outlook.Folder outlookfolder = Login.GetOutlookBisFolder();
                            if (outlookfolder != null)
                                oMailItem.SaveSentMessageFolder = outlookfolder;

                           // oMailItem.SaveSentMessageFolder = Login.sentFolder;

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
                    RadMessageBox.Show("You need to add client first.");
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

                if (client.idClient != 0)
                {
                    DocumentsModel model = new DocumentsModel();
                    model.idContPers = 0;
                    model.idClient = client.idClient;
                    model.descriptionDocument = "Email";
                    model.fileDocument = item.EntryID + ".msg";
                    model.typeDocument = "EML";
                    model.idDocumentStatus = 2;
                    model.idEmployee = 0;
                    model.idResponsableEmployee = 0;
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
        ////void outlookApp_ItemSend(object Item, ref bool Cancel)
        //{
        //    if (Item is Microsoft.Office.Interop.Outlook.MailItem)
        //    {
        //        Microsoft.Office.Interop.Outlook.MailItem item = (Microsoft.Office.Interop.Outlook.MailItem)Item;
        //        item.Save();

        //        //MailSentModel model = new MailSentModel();
        //        //model.entryId = item.EntryID;
        //        //model.idUser = Login._user.idUser;
        //        //model.Subject = item.Subject;
        //        //model.idPersonTo = Person.idContPers;
        //        //model.idClientTo = 0;
        //        //model.locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";
        //        //model.dtSent = DateTime.Now;

        //        //item.SaveAs(model.locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);
        //        //MailSentBUS bus = new MailSentBUS();
        //        //bus.Save(model);

        //        DocumentsBUS sbus = new DocumentsBUS();
        //        PersonEmailBUS emailbus = new PersonEmailBUS();


        //        string locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";

        //        if (!File.Exists(locationOnDisk))
        //            item.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

        //        if (client.idClient != 0)
        //        {
        //            DocumentsModel model = new DocumentsModel();
        //            model.idContPers = 0;
        //            model.idClient = client.idClient;
        //            model.descriptionDocument = "Email";
        //            model.fileDocument = item.EntryID + ".msg";
        //            model.typeDocument = "EML";
        //            model.idDocumentStatus = 2;
        //            model.idEmployee = 0;
        //            model.idResponsableEmployee = 0;
        //            model.inOutDocument = 0;
        //            model.noteDocument = "Sent Email";
        //            model.idArrangement = 0;
        //            //model.id

        //            model.dtCreated = DateTime.Now;
        //            model.dtModified = DateTime.Now;
        //            model.userCreated = Login._user.idUser;
        //            model.userModified = Login._user.idUser;

        //            sbus.Save(model);
        //        }

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
      
        //void outlookApp_ItemSend(object Item, ref bool Cancel)
        //{
        //    if (Item is Outlook.MailItem)
        //    {
        //        Outlook.MailItem item = (Outlook.MailItem)Item;
        //        item.Save();

        //        MailSentModel model = new MailSentModel();
        //        model.entryId = item.EntryID;
        //        model.idUser = Login._user.idUser;
        //        model.Subject = item.Subject;
        //        model.idPersonTo = 0;
        //        model.idClientTo = client.idClient;
        //        model.locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";
        //        model.dtSent = DateTime.Now;

        //        item.SaveAs(model.locationOnDisk, Outlook.OlSaveAsType.olMSG);
        //        MailSentBUS bus = new MailSentBUS();
        //        bus.Save(model);

        //        //Cancel = true;
        //        //item.Close(Outlook.OlInspectorClose.olDiscard);
        //    }
        //}
        
        private Microsoft.Office.Interop.Word.Application wordApp;
  
        //private void btnWord_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<IModel> lookupModel = new List<IModel>();
        //        LayoutsBUS bBUS = new LayoutsBUS();
        //        lookupModel = bBUS.GetBookmarksDistinctCLIENT();

        //        var lookfrm = new GridLookupForm(lookupModel, "Bookmarks");
        //        if (lookfrm.ShowDialog(this) == DialogResult.Yes)
        //        {
        //            LayoutsModel selmodel = new LayoutsModel();
        //            selmodel = (LayoutsModel)lookfrm.selectedRow;

        //            //MessageBox.Show(selmodel.nameLayout);

        //            List<BookmarksModel> bookmarkList = new List<BookmarksModel>();
        //            BookmarksBUS bookmarkBUS = new BookmarksBUS();
        //            bookmarkList = bookmarkBUS.GetBookmarksByBookmarkId(selmodel.bookmark);

        //            List<BookmarkSpecModel> specModelList = new List<BookmarkSpecModel>();
        //            foreach (BookmarksModel m in bookmarkList)
        //            {

        //                if (m.tableName == "Client")
        //                {

        //                    BookmarkSpecModel s = new BookmarkSpecModel();
        //                    s.field = m.fieldBookmark;
        //                    s.table = m.tableName;
        //                    s.value = bookmarkBUS.CustomClientQuery(m.tableName, m.fieldName, client.idClient, "idClient").ToString();
        //                    specModelList.Add(s);

        //                }
        //                if (m.tableName == "TypesTel")
        //                {
        //                    ClientTelBUS telbus = new ClientTelBUS();
        //                    List<ClientTelModel> telmodelList = new List<ClientTelModel>();
        //                    telmodelList = telbus.GetAllClientTelsByType(Int32.Parse(m.fieldValue), client.idClient);

        //                    foreach (ClientTelModel tm in telmodelList)
        //                    {
        //                        BookmarkSpecModel s = new BookmarkSpecModel();
        //                        //s.field = "Tel";
        //                        s.field = m.fieldBookmark;
        //                        s.table = m.tableName;
        //                        //s.value = m.fieldName + ": " + tm.numberTel;
        //                        s.value = tm.numberTel;
        //                        specModelList.Add(s);
        //                    }
        //                }

        //                if (m.tableName == "TypesAddress")
        //                {
        //                    ClientAddressBUS adrbus = new ClientAddressBUS();
        //                    List<ClientAddressModel> adrmodelList = new List<ClientAddressModel>();
        //                    adrmodelList = adrbus.GetClientAddressesByType(Int32.Parse(m.fieldValue), client.idClient);

        //                    foreach (ClientAddressModel tm in adrmodelList)
        //                    {
        //                        //BookmarkSpecModel s = new BookmarkSpecModel();                             
        //                        //s.field = m.fieldBookmark;
        //                        //s.table = m.tableName;
        //                        ////s.value = m.fieldName + ": " + tm.street + " " + tm.housenr + " " + tm.postalCode + " " + tm.city + " " + tm.country;
        //                        //s.value = tm.street + " " + tm.housenr + " " + tm.postalCode + " " + tm.city + " " + tm.country;
        //                        //specModelList.Add(s);

        //                        //dodaj bookmark za svaku kolonu posebno
        //                        BookmarkSpecModel s = new BookmarkSpecModel();
        //                        s.field = m.fieldBookmark + "_street";
        //                        s.table = m.tableName;
        //                        s.value = tm.street;
        //                        specModelList.Add(s);

        //                        s = new BookmarkSpecModel();
        //                        s.field = m.fieldBookmark + "_housenr";
        //                        s.table = m.tableName;
        //                        s.value = tm.housenr;
        //                        specModelList.Add(s);

        //                        s = new BookmarkSpecModel();
        //                        s.field = m.fieldBookmark + "_extension";
        //                        s.table = m.tableName;
        //                        s.value = tm.extension;
        //                        specModelList.Add(s);

        //                        s = new BookmarkSpecModel();
        //                        s.field = m.fieldBookmark + "_postalcode";
        //                        s.table = m.tableName;
        //                        s.value = tm.postalCode;
        //                        specModelList.Add(s);

        //                        s = new BookmarkSpecModel();
        //                        s.field = m.fieldBookmark + "_city";
        //                        s.table = m.tableName;
        //                        s.value = tm.city;
        //                        specModelList.Add(s);

        //                        s = new BookmarkSpecModel();
        //                        s.field = m.fieldBookmark + "_country";
        //                        s.table = m.tableName;
        //                        s.value = tm.country;
        //                        specModelList.Add(s);

        //                    }
        //                }

        //                if (m.tableName == "TypesEmail")
        //                {
        //                    ClientEmailBUS ebus = new ClientEmailBUS();
        //                    List<ClientEmailModel> eList = new List<ClientEmailModel>();
        //                    eList = ebus.GetClientEmailsByType(Int32.Parse(m.fieldValue), client.idClient);

        //                    foreach (ClientEmailModel tm in eList)
        //                    {
        //                        BookmarkSpecModel s = new BookmarkSpecModel();
        //                        //s.field = "Email";
        //                        s.field = m.fieldBookmark;
        //                        s.table = m.tableName;
        //                        //s.value = m.fieldName + ": " + tm.email;
        //                        s.value = tm.email;
        //                        specModelList.Add(s);
        //                    }
        //                }

        //            }

        //            string templateFolder = AppDomain.CurrentDomain.BaseDirectory + "Templates\\";
        //            string documentFolder = AppDomain.CurrentDomain.BaseDirectory + "Documents\\";

        //            object fileName = templateFolder + selmodel.nameLayout + ".docx";
        //            object confirmConversions = Type.Missing;
        //            object readOnly = Type.Missing;
        //            object addToRecentFiles = Type.Missing;
        //            object passwordDoc = Type.Missing;
        //            object passwordTemplate = Type.Missing;
        //            object revert = Type.Missing;
        //            object writepwdoc = Type.Missing;
        //            object writepwTemplate = Type.Missing;
        //            object format = Type.Missing;
        //            object encoding = Type.Missing;
        //            object visible = Type.Missing;
        //            object openRepair = Type.Missing;
        //            object docDirection = Type.Missing;
        //            object notEncoding = Type.Missing;
        //            object xmlTransform = Type.Missing;

        //            wordApp = new Microsoft.Office.Interop.Word.Application();
        //            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref fileName, ref confirmConversions, ref readOnly, ref addToRecentFiles, ref passwordDoc, ref passwordTemplate, ref revert, ref writepwdoc, ref writepwTemplate, ref format, ref encoding, ref visible, ref openRepair, ref docDirection, ref notEncoding, ref xmlTransform);

        //            foreach (BookmarkSpecModel oBooks in specModelList)
        //                ReplaceBookmarkText(doc, oBooks);

        //            DeleteEmptyBookmarks(doc, specModelList);

        //            string sFile = CreateDocName(client.idClient) + ".docx";
        //            fileName = documentFolder + sFile;
        //            //================================================
        //            // Create a new Microsoft Word application object
        //            //Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

        //            //// C# doesn't have optional arguments so we'll need a dummy value
        //            //object oMissing = System.Reflection.Missing.Value;

        //            //// Get list of Word files in specified directory
        //            //DirectoryInfo dirInfo = new DirectoryInfo("D:\\");
        //            //FileInfo[] wordFiles = dirInfo.GetFiles("*.doc");

        //            //word.Visible = false;
        //            //word.ScreenUpdating = false;

        //            //foreach (FileInfo wordFile in wordFiles)
        //            //{
        //            //    // Cast as Object for word Open method
        //            //    Object filename = (Object)wordFile.FullName;

        //            //    // Use the dummy value as a placeholder for optional arguments
        //            //    Document doci = word.Documents.Open(ref filename, ref oMissing,
        //            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        //            //    doci.Activate();

        //            //    object outputFileName = wordFile.FullName.Replace(".doc", ".pdf");
        //            //    object fileFormat = WdSaveFormat.wdFormatPDF;

        //            //    // Save document into PDF Format
        //            //    doci.SaveAs(ref outputFileName,
        //            //        ref fileFormat, ref oMissing, ref oMissing,
        //            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //            //        ref oMissing, ref oMissing, ref oMissing, ref oMissing);

        //            //    // Close the Word document, but leave the Word application open.
        //            //    // doc has to be cast to type _Document so that it will find the
        //            //    // correct Close method.                
        //            //    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
        //            //    ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
        //            //    doc = null;
        //            //}

        //            //// word has to be cast to type _Application so that it will find
        //            //// the correct Quit method.
        //            //((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
        //            //word = null;

        //            //=========================================
        //            doc.SaveAs2(fileName);

        //            //Otvaranje template-a za pregled
        //            doc.Application.Visible = true;
        //            doc.Activate();

        //            LayoutsModel tmpLayout = (LayoutsModel)lookupModel[0];
        //            DocumentsModel docmodel = new DocumentsModel();
        //            docmodel.idContPers = 0;
        //            docmodel.idClient = client.idClient;
        //            docmodel.inOutDocument = 1;
        //            docmodel.typeDocument = tmpLayout.typeDocument;
        //            docmodel.fileDocument = sFile;
        //            docmodel.dtCreated = DateTime.Now;
        //            docmodel.dtModified = DateTime.Now;
        //            docmodel.userCreated = Login._user.idUser;
        //            docmodel.idLayout = tmpLayout.idLayout;
        //            docmodel.descriptionDocument = "_";

        //            docmodel.idEmployee = Login._user.idEmployee;
        //            docmodel.idResponsableEmployee = 2; // ?
        //            docmodel.idDocumentStatus = 1; // ?
        //            docmodel.noteDocument = "_";

        //            docmodel.userModified = Login._user.idUser; // ?

        //            DocumentsBUS docbus = new DocumentsBUS();
        //            docbus.Save(docmodel);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

           
        //}

        private void radButton1_Click(object sender, EventArgs e)
        {
            string sFile = "";
            sFile = txtWeb.Text;
            if (sFile != "")
            {
                try
                {
                    System.Diagnostics.Process.Start(sFile);
                }
                catch
                        (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        RadMessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    RadMessageBox.Show(other.Message);
                }

                //System.Diagnostics.Process.Start(sFile);
            }
        }

        private void NoteMenuSaveClick(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutMemo))
            {
                File.Delete(layoutMemo);
            }
            rgvNote.SaveLayout(layoutMemo);
        }

        private void DocumentsMenuSaveClick(object sender, EventArgs e)
        {
            
            if (File.Exists(layoutDocuments))
            {
                File.Delete(layoutDocuments);
            }
            rgvDocuments.SaveLayout(layoutDocuments);
        }

        private void MeetingsMenuSaveClick(object sender, EventArgs e)
        {
            
            if (File.Exists(layoutMeetings))
            {
                File.Delete(layoutMeetings);
            }
            rgvMeetings.SaveLayout(layoutMeetings);
        }

        private void ContactsMenuSaveClick(object sender, EventArgs e)
        {
            
            if (File.Exists(layoutContacts))
            {
                File.Delete(layoutContacts);
            }
            rgvContacts.SaveLayout(layoutContacts);
        }

        private void TasksMenuSaveClick(object sender, EventArgs e)
        {
            
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            rgvToDo.SaveLayout(layoutTasks);
        }

        private void radMenuItemSaveDocumentsLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutDocuments))
            {
                File.Delete(layoutDocuments);
            }
            rgvDocuments.SaveLayout(layoutDocuments);

            RadMessageBox.Show("Layout saved");
        }

        private void radMenuItemSaveMemoLayout_Click(object sender, EventArgs e)
        {

            if (File.Exists(layoutMemo))
            {
                File.Delete(layoutMemo);
            }
            rgvNote.SaveLayout(layoutMemo);

            RadMessageBox.Show("Layout saved");
        }

        private void radMenuItemSaveMeetingsLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutMeetings))
            {
                File.Delete(layoutMeetings);
            }
            rgvMeetings.SaveLayout(layoutMeetings);

            RadMessageBox.Show("Layout saved");
        }

        private void radMenuItemSaveContactsLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutContacts))
            {
                File.Delete(layoutContacts);
            }
            rgvContacts.SaveLayout(layoutContacts);

            RadMessageBox.Show("Layout saved");
        }

        private void radMenuItemSaveTasksLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            rgvToDo.SaveLayout(layoutTasks);

            RadMessageBox.Show("Layout saved");
        }

        private void radMenuItemSaveContract_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutContracts))
            {
                File.Delete(layoutContracts);
            }
            rgvContract.SaveLayout(layoutContracts);

            RadMessageBox.Show("Layout saved");
        }


        private void btnDebAcc_Click(object sender, EventArgs e)
        {
            if (chkDebitor.Checked == true)
            {
                LedgerAccountBUS accBUS = new LedgerAccountBUS(Login._bookyear);
                List<IModel> am = new List<IModel>();

                am = accBUS.GetAllAccounts();


                var dlgClient = new GridLookupForm(am, "Ledger");

                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    LedgerAccountModel okm = new LedgerAccountModel();
                    okm = (LedgerAccountModel)dlgClient.selectedRow;
                    txtDebAcc.Text = okm.numberLedgerAccount + " " + okm.descLedgerAccount;
                    debCre.debAccount = okm.numberLedgerAccount;

                }
            }
            else
            {
                RadMessageBox.Show("Check Debitor first !");
                chkDebitor.Focus();
            }
        }

        private void btnCreAcc_Click(object sender, EventArgs e)
        {
            if (chkCreditor.Checked == true)
            {
                LedgerAccountBUS accBUS1 = new LedgerAccountBUS(Login._bookyear);
                List<IModel> am1 = new List<IModel>();

                am1 = accBUS1.GetAllAccounts();


                var dlgClient = new GridLookupForm(am1, "Ledger");

                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    LedgerAccountModel okm1 = new LedgerAccountModel();
                    okm1 = (LedgerAccountModel)dlgClient.selectedRow;
                    txtCreAcc.Text = okm1.numberLedgerAccount + " " + okm1.descLedgerAccount;
                    debCre.creditAccount = okm1.numberLedgerAccount;

                }
            }
            else
            {
                RadMessageBox.Show("Check Creditor first !");
                chkCreditor.Focus();
            }
        }

        private void chkDebitor_CheckStateChanged(object sender, EventArgs e)
        {
            // ako decekira brise se sadrzaj lookapa i anulira u modelu
            if (chkDebitor.Checked == false)
            {
                txtDebAcc.Text = "";
                debCre.debAccount = "";
            }
            else
            {
                if (asmo != null)
                    if (asmo.defDebitorAccount != null && asmo.defDebitorAccount != "")
                    {
                        AccAcountUpdate acuu = new AccAcountUpdate();
                        string nameacc = "";
                        nameacc = acuu.AccountName(asmo.defDebitorAccount.ToString());
                        txtDebAcc.Text = asmo.defDebitorAccount.ToString() + " " + nameacc;
                        debCre.debAccount = asmo.defDebitorAccount;
                    }
            }
        }

        private void chkCreditor_CheckStateChanged(object sender, EventArgs e)
        {
            // ako decekira brise se sadrzaj lookapa i anulira u modelu
            if (chkCreditor.Checked == false)
            {
                txtCreAcc.Text = "";
                debCre.creditAccount = "";
            }
            else
            {
                if (asmo != null)
                    if (asmo.defCreditorAccount != null && asmo.defCreditorAccount != "")
                    {
                        AccAcountUpdate acuu = new AccAcountUpdate();
                        string nameacc = "";
                        nameacc = acuu.AccountName(asmo.defCreditorAccount.ToString());
                        txtCreAcc.Text = asmo.defCreditorAccount.ToString() + " " + nameacc;
                        debCre.creditAccount = asmo.defCreditorAccount;
                    }
            }
        }
        private bool ValidateClient()
        {
            if (txtCompanyName.Text == "")
            {
                RadMessageBox.Show("Can't SAVE witout Client name !");
                txtCompanyName.Focus();
                return false;
            }
            //Mitar i Aleksa
           /* if (txtCompanyCode.Text == "")
            {
                RadMessageBox.Show("Can't SAVE witout Company code !");
                txtCompanyCode.Focus();
                return false;
            }*/
            //Mitar i Aleksa
            return true;
        }
        private void SaveClientOnUpdate()
        {

            client.idClient = iClient;
            client.nameClient = txtCompanyName.Text;
            client.contactPersonName = txtContactPerson.Text;
            client.webClient = txtWeb.Text;
            client.accountCodeClient = Convert.ToString(txtCompanyCode.Text);
            client.idTypeClient = Convert.ToInt32(ddlRelation.SelectedValue);
            client.dtModified = DateTime.Now;
            client.userModified = Login._user.idUser;

            if (chkActive.Checked == true)
            {
                client.isActiveClient = true;
            }
            else
            {
                client.isActiveClient = false;
            }            
        }

        private void SaveClientOnNew()
        {
            client = new ClientModel();
            client.nameClient = txtCompanyName.Text;
            client.contactPersonName = txtContactPerson.Text;
            client.accountCodeClient = txtCompanyCode.Text;
            client.webClient = txtWeb.Text;

            if (chkActive.Checked == true)
                client.isActiveClient = true;
            else
                client.isActiveClient = false;

            //client.nameClient = txtCompanyName.Text;
            //client.webClient = txtWeb.Text;
            //client.accountCodeClient = txtCompanyCode.Text;
            client.idTypeClient = Convert.ToInt32(ddlRelation.SelectedValue);
            client.dtModified = DateTime.Now;
            client.userModified = Login._user.idUser;
            client.userCreated = Login._user.idUser;
            client.dtCreated = DateTime.Now;
        }

        private void UpdateClient()
        {
            // client = new ClientModel();            
            ClientBUS bus = new ClientBUS();
            ClientAddressBUS cab = new ClientAddressBUS();
            ClientEmailBUS ceb = new ClientEmailBUS();
            ClientTelBUS ctb = new ClientTelBUS();

            Boolean isSuccessfully = false;
            if (txtPayCon.Text == "")
                debCre.payCondition = 0;
                       
            if (chkDebitor.Checked == true)
            {
                debCre.isDebitor = true;
            }
            else
            {
                debCre.isDebitor = false;
            }
            if (chkCreditor.Checked == true)
            {
                debCre.isCreditor = true;
            }
            else
            {
                debCre.isCreditor = false;
            }
            // bus.Update(client, iClient);

            if (bus.Update(client, iClient, this.Name, Login._user.idUser) == true)
            {
                isSuccessfully = true;
                saveAddress();
                for (int n = 0; n < clientAddress.Count; n++)
                {
                    if (clientAddress[n].street != "" || clientAddress[n].housenr.Trim() != "" || clientAddress[n].postalCode.Trim() != ""
                        || clientAddress[n].city.Trim() != "")
                    {
                        if (cab.Update(clientAddress[n], this.Name, Login._user.idUser) == true)
                        {
                            isSuccessfully = true;
                        }
                        else
                        {
                            RadMessageBox.Show("Error saving Address data " + (n + 1).ToString());
                        }
                    }
                    else
                    {
                        //delete address if street, housenr, postal or city is empty
                        cab.Delete(client.idClient, (int)clientAddress[n].idAddressType, this.Name, Login._user.idUser);
                    }
                }
            }
            if (cemail != null)
            {
                saveEmail();
                for (int i = 0; i < cemail.Count; i++)
                {
                    if (ceb.Update(cemail[i], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("Error saving data for email " + (i + 1).ToString());
                    }
                }
            }
            if (ctelm != null)
            {
                saveTel();
                for (int j = 0; j < ctelm.Count; j++)
                {
                    if (ctb.Update(ctelm[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("Error saving data for telephone " + (j + 1).ToString());
                    }
                }
            }
            // -------- save debitor creditor record
            if (debCre != null)
            {
                    AccDebCreBUS dbbus = new AccDebCreBUS();
                    AccDebCreModel aa = new AccDebCreModel();
                    isSaved = false;
                    // provera da  postoji slog
                    aa = dbbus.GetClientDebCre(client.idClient);
                    if (aa != null)
                    {
                        debCre.accNumber = client.accountCodeClient;
                        debCre.idClient = client.idClient;
                        debCre.idAccDebCre = aa.idAccDebCre;

                        isSaved = dbbus.Update(debCre, this.Name, Login._user.idUser);
                        if (isSaved == false)
                        {
                            RadMessageBox.Show("Error updating Account data ");
                        }
                    }
                    else
                    {
                        debCre.idClient = client.idClient;
                        debCre.accNumber = client.accountCodeClient;
                        if (txtPayCon.Text != "")
                            debCre.payCondition = Convert.ToInt32(txtPayCon.Text);
                        isSaved = dbbus.Save(debCre, this.Name, Login._user.idUser);
                        if (isSaved == false)
                        {
                            RadMessageBox.Show("Error saving Account data ");
                        }
                    }
                

            }
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Saved") != null)
                    RadMessageBox.Show(resxSet.GetString("Saved"));
                else
                    RadMessageBox.Show("Saved");

                UpdateOriginalValuesAfterSave();
            }
        }

        private void InsertClient()
        {
            ClientBUS bus = new ClientBUS();
            ClientAddressBUS cab = new ClientAddressBUS();
            ClientEmailBUS ceb = new ClientEmailBUS();
            ClientTelBUS ctb = new ClientTelBUS();


            Boolean isSuccessfully = false;
            //bus.Save(client);
            int lastId = bus.Save(client, this.Name, Login._user.idUser);
            if (lastId >= 0)
            //if (bus.Save(client) == true)
            {
                client.idClient = lastId;

                iClient = lastId;
                if (txtCompanyCode.Text == "")
                {
                    debCre = new AccDebCreModel();
                    AccDebCreBUS dbbus = new AccDebCreBUS();
                    //=============================== ubacuje za novog DEBITOR / CREDITOR BROJ
                    txtCompanyCode.Text = "B" + lastId.ToString().PadLeft(5, '0');
                    debCre.accNumber = txtCompanyCode.Text;
                    debCre.idClient = lastId;
                    isSaved = dbbus.Save(debCre, this.Name, Login._user.idUser);
                    client.accountCodeClient = txtCompanyCode.Text;
                    bus.Update(client, iClient, this.Name, Login._user.idUser);
                }

                //=============================================================


                isSuccessfully = true;
                saveAddress();
                for (int n = 0; n < clientAddress.Count; n++)
                {
                    if (clientAddress[n].street != "" || clientAddress[n].housenr.Trim() != "" || clientAddress[n].postalCode.Trim() != ""
                        || clientAddress[n].city.Trim() != "")
                    {
                        if (cab.Save(clientAddress[n], this.Name, Login._user.idUser) == true)
                        {
                            isSuccessfully = true;
                        }
                        else
                        {
                            RadMessageBox.Show("Error saving Address data " + (n + 1).ToString());
                        }
                    }
                }
            }
            if (cemail != null)
            {
                saveEmail();
                for (int i = 0; i < cemail.Count; i++)
                {
                    if (ceb.Save(cemail[i], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("Error saving data for email " + (i + 1).ToString());
                    }
                }
            }
            if (ctelm != null)
            {
                saveTel();
                for (int j = 0; j < ctelm.Count; j++)
                {
                    if (ctb.Save(ctelm[j], this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;
                    }
                    else
                    {
                        RadMessageBox.Show("Error saving data for telephone " + (j + 1).ToString());
                    }
                }
            }
            //  ---- save Debitor-Creditor recors

            RadMessageBox.Show("Save");
            UpdateOriginalValuesAfterSave();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // client = new ClientModel();            
            ClientBUS bus = new ClientBUS();
            ClientAddressBUS cab = new ClientAddressBUS();
            ClientEmailBUS ceb = new ClientEmailBUS();
            ClientTelBUS ctb = new ClientTelBUS();
            Boolean isSuccessfully = false;

            if (iClient != -1)
            {
                bool validate = ValidateClient();
                if (validate == true)
                {
                    SaveClientOnUpdate();
                    UpdateClient();
                }
            }
            else
            {
                bool validate = ValidateClient();
                if (validate == true)
                {
                    SaveClientOnNew();
                    InsertClient();
                }
            }
            // this.Close();
            for (int i = 0; i < PageViewClient.Pages.Count; i++)
            {
                if (PageViewClient.Pages[i].Name != "tabRelation")
                {
                    PageViewClient.Pages[i].Enabled = true;
                }
            }
        }
        private void btnWord_Click(object sender, EventArgs e)
        {
           // ReadTemplateFile(wordApp, "Client", "idClient", client.idClient);

            List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
            BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
            lookupModel = bBUS.GetAllLayoutsbyTemplateTable("Client");

            using (var lookfrm = new GridLookupForm(lookupModel, "Templates"))
            {
                if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                {
                    BookmarkFunctions.ReadTemplateFile(wordApp, "Client", "idClient", client.idClient, null, (BIS.Model.LayoutsModel)lookfrm.selectedRow, this.Name, Login._user.idUser);
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void PageViewClient_Enter(object sender, EventArgs e)
        {
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Visible;
        }

        private void PageViewClient_Leave(object sender, EventArgs e)
        {
           // radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
        }

        private void btnNewContract_Click(object sender, EventArgs e)
        {
            
        }

        private void rgvContract_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if(e.Row.DataBoundItem != null)
            {
                PriceListModel priceListModel = (PriceListModel)e.Row.DataBoundItem;

                using (frmPriceList frm = new frmPriceList(priceListModel, client.idClient))
                {
                    frm.ShowDialog();
                    loadContracts();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private void btnNewContract_Click_1(object sender, EventArgs e)
        {
            using (frmPriceList frm = new frmPriceList(client.idClient))
            {
                frm.ShowDialog();
                loadContracts();
            }
        }

        private void rgvContract_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Visible;
            for (int i = 0; i < rgvContract.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvContract.Columns[i].HeaderText != null && resxSet.GetString(rgvContract.Columns[i].HeaderText) != null)
                        rgvContract.Columns[i].HeaderText = resxSet.GetString(rgvContract.Columns[i].HeaderText);
                }
                if (rgvContract.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                {
                    if (rgvContract.Columns[i].Name.ToLower() != "dtUserModified".ToLower() && rgvContract.Columns[i].Name.ToLower() != "dtUserCreated".ToLower())
                    {
                        rgvContract.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                    }
                }
            }

            if (File.Exists(layoutContracts))
            {
                rgvContract.LoadLayout(layoutContracts);
            }
            else
            {
                rgvContract.Columns["idPriceList"].IsVisible = false;
                rgvContract.Columns["idArrangement"].IsVisible = false;
                rgvContract.Columns["idClient"].IsVisible = false;
                rgvContract.Columns["idUserCreated"].IsVisible = false;
                rgvContract.Columns["nameUserCreated"].IsVisible = false;
                rgvContract.Columns["dtUserCreated"].IsVisible = false;
                rgvContract.Columns["idUserModified"].IsVisible = false;
                rgvContract.Columns["nameUserModified"].IsVisible = false;
                rgvContract.Columns["dtUserModified"].IsVisible = false;
                rgvContract.Columns["idHotelService"].IsVisible = false;

            }

            rgvContract.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvContract.Show();
        }

        private void frmClient_SizeChanged(object sender, EventArgs e)
        {
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Visible;
            btnNewContract.Visibility = ElementVisibility.Visible;
            btnDeleteContract.Visibility = ElementVisibility.Visible;
        }

        private void btnPayCon_Click(object sender, EventArgs e)
        {
              AccPaymentBUS ccentar = new AccPaymentBUS();
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllAccPayment();
                using (var dlgSave = new GridLookupForm(gmX, "Pay Conditions"))
                {

                    if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                    {
                        AccPaymentModel genmX = new AccPaymentModel();
                        genmX = (AccPaymentModel)dlgSave.selectedRow;

                        if (genmX != null)
                        {
                            //set textbox
                            if (genmX.idPayment != null)
                            {
                                txtPayCon.Text = genmX.description;
                                debCre.payCondition = genmX.idPayment;
                            }
                        }
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
        }

        private void rgvClientPerson_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvClientPerson.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvClientPerson.Columns[i].HeaderText != null && resxSet.GetString(rgvClientPerson.Columns[i].HeaderText) != null)
                        rgvClientPerson.Columns[i].HeaderText = resxSet.GetString(rgvClientPerson.Columns[i].HeaderText);
                }
            }
            
            if (File.Exists(layoutClientPersonView))
            {
                rgvClientPerson.LoadLayout(layoutClientPersonView);
            }
        }

        private void rgvClientPerson_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            PersonBUS pbus = new PersonBUS();
            PersonModel pmodel = new PersonModel();

            if (rgvClientPerson.CurrentRow.Cells["idContPerson"].Value != null)
                idPerson = Convert.ToInt32(rgvClientPerson.CurrentRow.Cells["idContPerson"].Value);
            pmodel = pbus.GetPerson(idPerson);
            if (pmodel != null)
            {
                using (frmPerson frm = new frmPerson(pmodel))
                {
                    frm.ShowDialog();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

        }

        private void rgvClientPerson_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
               string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayout;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }
        private void SaveLayout(object sender,EventArgs e)
        {
            if(File.Exists(layoutClientPersonView))
            {
                File.Delete(layoutClientPersonView);
            }
            rgvClientPerson.SaveLayout(layoutClientPersonView);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        
        }

        private void rgvClientPerson_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            rgvClientPerson.AllowEditRow = true;

            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialog("Delete entry ?", "Delete");

            if (dr != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                if (rgvClientPerson.CurrentRow.DataBoundItem != null)
                {
                    ClientPersonBUS bus = new ClientPersonBUS();
                    ClientPersonModel model = (ClientPersonModel)rgvClientPerson.CurrentRow.DataBoundItem;
                    bus.Delete(model.idCliPer, this.Name, Login._user.idUser);
                }
            }
            rgvClientPerson.AllowEditRow = false;
        }

        private void rgvEmail_ValueChanged(object sender, EventArgs e)
        {
            //if (this.rgvEmail.ActiveEditor is RadCheckBoxEditor)
            //{
            //    rgvEmail.EndEdit();
            //}
        }

        private void rgvTel_ValueChanged(object sender, EventArgs e)
        {
            //if (this.rgvTel.ActiveEditor is RadCheckBoxEditor)
            //{
            //    rgvTel.EndEdit();
            //}
        }


        private void UpdateOriginalValuesAfterSave()
        {
            clientFirst = new ClientModel(client);
            ctelmFirst = new List<ClientTelModel>();
            cemailFirst = new List<ClientEmailModel>();

            if(ctelm != null)
            {
                foreach(ClientTelModel t in ctelm)
                {
                    ctelmFirst.Add(t.ReturnCopy());
                }
            }

            if(cemail != null)
            {
                foreach(ClientEmailModel m in cemail)
                {
                    cemailFirst.Add(m.ReturnCopy());
                }
            }

            //persEmailFirst = new List<PersonEmailModel>();
            //persTelFirst = new List<PersonTelModel>();
            //if (persEmail != null)
            //{
            //    foreach (PersonEmailModel m in persEmail)
            //    {
            //        persEmailFirst.Add(m.ReturnCopy());
            //    }
            //}
            //if (persTel != null)
            //{
            //    foreach (PersonTelModel m in persTel)
            //    {
            //        persTelFirst.Add(m.ReturnCopy());
            //    }
            //}
            
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {     
            
            if (iClient != -1)
                 SaveClientOnUpdate();
            else
                 SaveClientOnNew();
            
            bool changes = client.CompareWith(clientFirst);
            ClientTelModelComparer clientTelComparer = new ClientTelModelComparer();
            IEnumerable<ClientTelModel> differenceTel = ctelm.Except(ctelmFirst, clientTelComparer);
            ClientEmailModelComparer clientEmailComparer = new ClientEmailModelComparer();
            IEnumerable<ClientEmailModel> differenceEmail = cemail.Except(cemailFirst, clientEmailComparer);
            bool resultTel = Utils.IsAny(differenceTel);
            bool resultEmail = Utils.IsAny(differenceEmail);


            if (changes == true || resultTel == true || resultEmail == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Save before close ?", "Save");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool validate = ValidateClient();
                    if (validate == true)
                    {
                        if (iClient != -1)
                             UpdateClient();                            
                        else
                            InsertClient();
                           
                    }
                }
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    // NO option
                    client.CopyValues(clientFirst);
                } 
            }


        }

        private void gridIbans_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
            GridViewRowInfo rowToBeAdded = e.Rows.FirstOrDefault();
            if (rowToBeAdded != null && rowToBeAdded.Cells["ibanNumber"].Value != null)
            {
                string strIban = rowToBeAdded.Cells["ibanNumber"].Value.ToString();
                if (strIban.Trim() != "")
                {
                    MakeInvoice cc = new MakeInvoice();
                    bool b = cc.ValidateIban(strIban.Trim());
                    if (b == true)
                    {
                        AccIbanBUS ibanbus = new AccIbanBUS();
                        AccIbanModel model = new AccIbanModel();

                        List<AccIbanModel> checkIBAN = ibanbus.CheckIbanForClient(strIban.Trim(), client.idClient);
                        if (checkIBAN == null || checkIBAN.Count <= 0)
                        {
                            // za save u bazu
                            model.accNumber = client.accountCodeClient;
                            model.idContPers = 0;
                            model.ibanNumber = strIban;
                            model.idClient = client.idClient;
                            model.Id = ibanbus.Save(model, this.Name, Login._user.idUser);

                            //za save u grid
                            rowToBeAdded.Cells["accNumber"].Value = model.accNumber;
                            rowToBeAdded.Cells["idClient"].Value = model.idClient;
                            rowToBeAdded.Cells["idContPers"].Value = model.idContPers;
                            rowToBeAdded.Cells["Id"].Value = model.Id;

                            if (model.Id == 0)
                            {
                                translateRadMessageBox trr = new translateRadMessageBox();
                                trr.translateAllMessageBox("Error while saving.");
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            translateRadMessageBox trr = new translateRadMessageBox();
                            trr.translateAllMessageBox("IBAN  already exist for that person.");
                            e.Cancel = true;
                        }

                    }
                    else
                    {
                        translateRadMessageBox trr = new translateRadMessageBox();
                        trr.translateAllMessageBox("IBAN  NOT CORRECT.");
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;
        }

        private void gridIbans_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            GridViewRowInfo rowToBeDeleted = e.Rows.FirstOrDefault();
            AccIbanModel model = (AccIbanModel)rowToBeDeleted.DataBoundItem;

            if (model != null)
            {
                translateRadMessageBox trr = new translateRadMessageBox();
                DialogResult dr = trr.translateAllMessageBoxDialogYesNo("Delete IBAN number: " + model.ibanNumber + " ?", "Delete");

                if (dr == DialogResult.Yes)
                {
                    AccIbanBUS ibanbus = new AccIbanBUS();
                    ibanbus.Delete(model.Id, this.Name, Login._user.idUser);

                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;            
        }
    }
}
