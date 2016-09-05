using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using BIS.DAO;
using BIS.Business;
using System.Resources;
using Microsoft;
using Telerik.WinControls.UI;
using System.IO;

namespace GUI
{
    //public partial class frmDocuments : Telerik.WinControls.UI.RadForm
         public partial class frmDocuments : frmTemplate
         {
             private int iDocument = -1;
             private int idConstPers;
             private int idClient;
             
             private string swhat;
             private int idArr = -1;
             private string nameArr = "";
             public DocumentsModel model;
             public bool modelChanged = false;
             List<LayoutsModel> layouts;
             List<DocumentStatusModel> status; 
             List<DocumentTypeModel> dtm;
             string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
             public string Namef;
             public string pathdoc;

             private string uniqueNameForThisDocument = String.Empty;
             
          //DocumentsModel PersDoc;
        public frmDocuments()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Document");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();
        }
        public frmDocuments(int idPers, string what)
        {
                       
         //   PersDoc = (DocumentsModel) document;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Document");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
          
            InitializeComponent();
            swhat = what;
            if (swhat == "client")
            {
                idConstPers = 0;
                idClient = idPers;
            }
            else if (swhat == "person")
            {
                idConstPers = idPers;
                idClient = 0;
            }
            else if (swhat == "arr")
            {
                idConstPers = 0;
                idClient = 0;
                idArr = idPers;
            }
           // idConstPers = idPers;
        }
        public frmDocuments(int uDocument, int idPers)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Document");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            iDocument = uDocument;
            idConstPers = idPers;
        }

        public frmDocuments(int uDocument, int idPers, string what)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Document");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            iDocument = uDocument;
            swhat = what;
            if (swhat == "client")
            {
                idConstPers = 0;
                idClient = idPers;
            }
            else if (swhat == "person")
            {
                idConstPers = idPers;
                idClient = 0;
            }
            else if (swhat == "arr")
            {
                idConstPers = 0;
                idClient = 0;
                idArr = idPers;
            }
        }

        public frmDocuments(int uDocument, int idArrangement, int idPers, int idClient, string what)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Document");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            iDocument = uDocument;
            idArr = idArrangement;
            idConstPers = idPers;
            this.idClient = idClient;

            swhat = what;
        }

        private void frmDocuments_Load(object sender, EventArgs e)
        {
            try
            {
                radRibbonMemo.Visibility = ElementVisibility.Collapsed;
                btnReport.Visibility = ElementVisibility.Collapsed;
                btnEmail.Visibility = ElementVisibility.Collapsed;
                btnWord.Visibility = ElementVisibility.Collapsed;
                btnNewMemo.Visibility = ElementVisibility.Collapsed;
                btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                btnDeleteDoc.Visibility = ElementVisibility.Visible;
                btnNewDoc.Visibility = ElementVisibility.Collapsed;
                radRibbonContact.Visibility = ElementVisibility.Collapsed;
                radRibbonTask.Visibility = ElementVisibility.Collapsed;


                setTranslation();

                //add layout
                LayoutsBUS tb = new LayoutsBUS();
                layouts = tb.GetAllLayouts();

                ddlLayout.DataSource = layouts;
                ddlLayout.DisplayMember = "nameLayout";
                ddlLayout.ValueMember = "idLayout";

                ////add document status
                DocumentStatusBUS tb1 = new DocumentStatusBUS();
                status = tb1.GetStatuses(Login._user.lngUser);

                ddlStatus.DataSource = status;
                ddlStatus.DisplayMember = "descriptionStatus";
                ddlStatus.ValueMember = "idDocumentStatus";

                DocumentTypeBUS DocumentTypeBUS = new DocumentTypeBUS();
                dtm = new List<DocumentTypeModel>();
                dtm = DocumentTypeBUS.GetALLDocumentTypes();

                model = new DocumentsBUS().GetDocument(iDocument);
                if (iDocument != -1)
                {
                    if (model != null)
                    {
                        txtidDocument.Text = model.idDocument.ToString();
                        txtext.Text = model.typeDocument.ToString();
                        lkpdocType.Text = dtm.Find(item => item.typeDocument.TrimEnd() == model.typeDocument.TrimEnd()).nameDocumentType;
                        txtdescription.Text = model.descriptionDocument.ToString();
                        txtdocName.Text = model.fileDocument.ToString();
                        txtDocNote.Text = model.noteDocument.ToString();
                        txtperson.Text = model.namePerson;
                        txtclient.Text = model.nameClient;
                        txtproject.Text = model.idProject.ToString();
                        txtemployee.Text = model.nameEmployee;
                        txtresponsible.Text = model.nameEmployeeResponsible;
                        txtCus.Text = model.userCreated.ToString();
                        txtMus.Text = model.userModified.ToString();
                        dtCreated.Text = model.dtCreated.ToString();
                        dtModified.Text = model.dtModified.ToString();
                        if (model.nameArrangement != null)
                            txtArrangement.Text = model.nameArrangement.ToString();
                        if (model.inOutDocument == 1)
                            rbInOut.CheckState = CheckState.Checked;
                        else
                            if (model.inOutDocument == 2)
                                rbIncome.CheckState = CheckState.Checked;
                        if (model.idDocumentStatus != 0 && model.idDocumentStatus != null)
                            ddlStatus.SelectedItem = ddlStatus.Items[status.FindIndex(item => item.idDocumentStatus == model.idDocumentStatus)];

                        if (model.idLayout != 0 && model.idLayout != null)
                            ddlLayout.SelectedItem = ddlLayout.Items[layouts.FindIndex(item => item.idLayout == model.idLayout)];

                        UsersBUS ub = new UsersBUS();

                        if (model.userCreated != null && model.userCreated != 0)
                            txtCus.Text = ub.getUserExact(Convert.ToInt32(model.userCreated)).nameUser;
                        if (model.userModified != null && model.userModified != 0)
                            txtMus.Text = ub.getUserExact(Convert.ToInt32(model.userModified)).nameUser;

                        uniqueNameForThisDocument = Path.GetFileNameWithoutExtension(model.fileDocument);

                        btndocType.Enabled = false;
                        btndocFind.Enabled = false;
                    }
                }
                else
                {
                    model = new DocumentsModel();

                    if (swhat == "client")
                    {
                        ClientBUS cb = new ClientBUS();
                        txtclient.Text = cb.GetClient(idClient).nameClient;
                        model.idClient = idClient;
                        btnClient.Enabled = false;
                        btnperson.Text = "...";

                        uniqueNameForThisDocument = CreateDocName(idClient);
                    }
                    else if (swhat == "person")
                    {
                        PersonBUS pbu = new PersonBUS();
                        txtperson.Text = pbu.GetPerson(idConstPers).fullname;
                        model.idContPers = idConstPers;
                        btnperson.Enabled = false;
                        btnClient.Text = "...";

                        uniqueNameForThisDocument = CreateDocName(idConstPers);
                    }
                    else if (swhat == "arr")
                    {
                        if (idClient > 0)
                        {
                            ClientBUS cb = new ClientBUS();
                            txtclient.Text = cb.GetClient(idClient).nameClient;
                            model.idClient = idClient;
                        }

                        if (idConstPers > 0)
                        {
                            PersonBUS pbu = new PersonBUS();
                            txtperson.Text = pbu.GetPerson(idConstPers).fullname;
                            model.idContPers = idConstPers;
                        }

                        ArrangementBUS abu = new ArrangementBUS();
                        txtArrangement.Text = abu.GetArrangementById(idArr).nameArrangement;
                        model.idArrangement = idArr;
                        btnArrangement.Enabled = false;

                        uniqueNameForThisDocument = CreateDocName(idArr);

                    }

                    dtCreated.Text = DateTime.Now.ToString();
                    txtCus.Text = Login._user.nameUser.ToString();


                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btndocType_Click(object sender, EventArgs e)
        
        {
            string extension = "";
            DocumentTypeBUS DocumentTypeBUS = new DocumentTypeBUS();
            List<IModel> gm1 = new List<IModel>();

            gm1 = DocumentTypeBUS.GetALLDocumentTypesCombo();
            var dlgSave = new GridLookupForm(gm1, "Type");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                DocumentTypeModel genm1 = new DocumentTypeModel();
                genm1 = (DocumentTypeModel)dlgSave.selectedRow;
                //set textbox
                lkpdocType.Text = genm1.nameDocumentType;
                txtext.Text = genm1.typeDocument;
                extension = genm1.extendDocumentType;
            }
        }

        private void btnperson_Click(object sender, EventArgs e)
        {
            // PersonModel m = (PersonModel)varijablaIModela;
            PersonBUS pbs = new PersonBUS();
            List<IModel> gm = new List<IModel>();

            gm = pbs.GetPersonsNoFilter();


            var dlgSave = new GridLookupForm(gm, "Person");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                PersonModel genm = new PersonModel();
                genm = (PersonModel)dlgSave.selectedRow;
                // set textbox
                
                txtperson.Text = genm.fullname;
                //update model
                model.idContPers = genm.idContPers;
                idConstPers = genm.idContPers;
            }
        }

        private void btnproject_Click(object sender, EventArgs e)
        {
          
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
                txtemployee.Text =  genm3.firstNameEmployee +" "+  genm3.lastNameEmployee;
                model.idEmployee = genm3.idEmployee;
                model.nameEmployee = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
            }
        }

        private void btnresponsible_Click(object sender, EventArgs e)
        {
            EmployeeBUS EmployeeBUS = new EmployeeBUS();
            List<IModel> gm4 = new List<IModel>();

            gm4 = EmployeeBUS.GetAllEmpl(Login._user.lngUser);


            var dlgSave = new GridLookupForm(gm4, "Employee");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                EmployeeModel genm4 = new EmployeeModel();
                genm4 = (EmployeeModel)dlgSave.selectedRow;
                txtresponsible.Text = genm4.firstNameEmployee + " " + genm4.lastNameEmployee;
                model.idResponsableEmployee = genm4.idEmployee;
                model.nameEmployeeResponsible = genm4.firstNameEmployee + " " + genm4.lastNameEmployee;
            }
        }

        private void setTranslation()
        {
          
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                
              //  lblDocId.Text = resxSet.GetString("Document Id");
                if (resxSet.GetString(lblDocId.Text) != null)
                    lblDocId.Text = resxSet.GetString(lblDocId.Text);
              //  lblDocType.Text = resxSet.GetString("iddct");
                if (resxSet.GetString(lblDocType.Text) != null)
                    lblDocType.Text = resxSet.GetString(lblDocType.Text);

               // lblDescription.Text = resxSet.GetString("Description");

                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);

              //  lblDocName.Text = resxSet.GetString("filedoc");

                if (resxSet.GetString(lblDocName.Text) != null)
                    lblDocName.Text = resxSet.GetString(lblDocName.Text);

               // lblDocNote.Text = resxSet.GetString("notedoc");

                if (resxSet.GetString(lblDocNote.Text) != null)
                    lblDocNote.Text = resxSet.GetString(lblDocNote.Text);

               // lblResponsible.Text = resxSet.GetString("Responsibile person");
                if (resxSet.GetString(lblResponsible.Text) != null)
                    lblResponsible.Text = resxSet.GetString(lblResponsible.Text);

              //  lblStatus.Text = resxSet.GetString("Status");
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);

               // lblLayout.Text = resxSet.GetString("Layout");
                if (resxSet.GetString(lblLayout.Text) != null)
                    lblLayout.Text = resxSet.GetString(lblLayout.Text);

              //  lblPerson.Text = resxSet.GetString("Person");
                if (resxSet.GetString(lblPerson.Text) != null)
                    lblPerson.Text = resxSet.GetString(lblPerson.Text);

               // lblClient.Text = resxSet.GetString("Client");
                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);

               // lblEmployee.Text = resxSet.GetString("Employee");
                if (resxSet.GetString(lblEmployee.Text) != null)
                    lblEmployee.Text = resxSet.GetString(lblEmployee.Text);

               // lblProject.Text = resxSet.GetString("Project");
                if (resxSet.GetString(lblProject.Text) != null)
                    lblProject.Text = resxSet.GetString(lblProject.Text);
                
              //  lblCus.Text = resxSet.GetString("Creator user");
                if (resxSet.GetString(lblCus.Text) != null)
                    lblCus.Text = resxSet.GetString(lblCus.Text);

               // lblMus.Text = resxSet.GetString("Modified user");
                if (resxSet.GetString(lblMus.Text) != null)
                    lblMus.Text = resxSet.GetString(lblMus.Text);

              //  lblDtCreate.Text = resxSet.GetString("Creation date");
                if (resxSet.GetString(lblDtCreate.Text) != null)
                    lblDtCreate.Text = resxSet.GetString(lblDtCreate.Text);

             //   lblDtModify.Text = resxSet.GetString("Modification date");
                if (resxSet.GetString(lblDtModify.Text) != null)
                    lblDtModify.Text = resxSet.GetString(lblDtModify.Text);

               // rbInOut.Text = resxSet.GetString("Ours");
                if (resxSet.GetString(rbInOut.Text) != null)
                    rbInOut.Text = resxSet.GetString(rbInOut.Text);

               // rbIncome.Text = resxSet.GetString("Incoming");
                if (resxSet.GetString(rbIncome.Text) != null)
                    rbIncome.Text = resxSet.GetString(rbIncome.Text);

               // rbNew.Text = resxSet.GetString("New document");
                if (resxSet.GetString(rbNew.Text) != null)
                    rbNew.Text = resxSet.GetString(rbNew.Text);

              //  rbExisting.Text = resxSet.GetString("Add existing document");
                if (resxSet.GetString(rbExisting.Text) != null)
                    rbExisting.Text = resxSet.GetString(rbExisting.Text);

               // btnSave.Text = resxSet.GetString("Save");
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

               // btnDeleteDoc.Text = resxSet.GetString("Delete");
                if (resxSet.GetString(btnDeleteDoc.Text) != null)
                    btnDeleteDoc.Text = resxSet.GetString(btnDeleteDoc.Text);

               // radRibbonDocuments.Text = resxSet.GetString("Document");
                if (resxSet.GetString(radRibbonDocuments.Text) != null)
                    radRibbonDocuments.Text = resxSet.GetString(radRibbonDocuments.Text);


                if (resxSet.GetString(lblArrangement.Text) != null)
                    lblArrangement.Text = resxSet.GetString(lblArrangement.Text);
            }
        }    
        private void btndocFind_Click(object sender, EventArgs e)
        {
            if (this.txtext.Text == "")
            {
                RadMessageBox.Show("Choose a document type, please!");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            

            string ext = dtm.Find(item => item.typeDocument.TrimEnd() == txtext.Text.TrimEnd()).extendDocumentType;
            openFileDialog.Filter = "( *." + ext + ")|*." + ext + "|All Files (*.*)|*.*";

            string sDest = "";                  
            if(ext.Trim() == "MSG" || ext.Trim() == "EML")
            {
                sDest = MainForm.myEmailFolder;
            }
            //else if (ext.Trim() == "WCPLET")
            //{
            //    sDest = MainForm.TemplatesFolder;
            //}
            else
            {
                sDest = MainForm.DocumentsFolder;
            }
            
            if(iDocument!=-1)
                openFileDialog.InitialDirectory = sDest;
            else
                openFileDialog.InitialDirectory = @"C:\";

            openFileDialog.Title = "Please select a file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sFile = openFileDialog.FileName;                

                string sFileName = Path.GetFileName(sFile);
               // txtdocName.Text = CreateDocName(idConstPers) + "." + ext;
                txtdocName.Text = uniqueNameForThisDocument + "." + ext;

                
                string fullname = sDest + "\\" + txtdocName.Text;
                bool bOpen = true;
                try
                {                   
                   System.IO.File.Copy(openFileDialog.FileName, fullname, true);
                }
                catch (Exception ex)
                {
                    bOpen = false;
                    RadMessageBox.Show("Error copying document.\nMessage: " + ex.Message, "Copy error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }

                if (bOpen)
                    OpenDocument(sDest, txtdocName.Text);
            }     
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtext.Text == "")
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Choose a document type, please!");                    
                    return;
                }                
                else  if (iDocument == -1)
                {
                    model.userCreated = Login._user.idUser;
                    model.dtCreated = DateTime.Now;
                }

                if(rbNew.CheckState==CheckState.Checked)
                    model.inOutDocument=1;
                else if (rbIncome.CheckState == CheckState.Checked)
                    model.inOutDocument = 2;
                model.idDocumentStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                model.idLayout = Convert.ToInt32(ddlLayout.SelectedValue);
                if ((txtext.Text).ToString() != "")
                    model.typeDocument = txtext.Text;
                if ((txtdescription.Text).ToString() != "")
                    model.descriptionDocument = txtdescription.Text;
                model.fileDocument = txtdocName.Text;
                pathdoc = txtdocName.Text;
                model.noteDocument = txtDocNote.Text;
                model.idContPers = idConstPers;
                model.userModified = Login._user.idUser;
                model.dtModified = DateTime.Now;
                model.userModified = Login._user.idUser;
                model.idArrangement = idArr;
                model.nameArrangement = nameArr;

                DocumentsBUS bus = new DocumentsBUS();
                if (iDocument != -1)
                {
                    model.idDocument = iDocument;
                    bus.Update(model, this.Name, Login._user.idUser);

                    
                }
                else
                {
                    bus.Save(model, this.Name, Login._user.idUser);

                                                  
                }
                modelChanged = true;
                this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnDeleteDoc_Click(object sender, EventArgs e)
        {
            if(iDocument!=-1)
            {               
                DialogResult dr = RadMessageBox.Show("Are you sure that you want to delete document ? \n " + txtdocName.Text, "", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    DocumentsBUS db = new DocumentsBUS();

                    bool bCllient = db.CheckDocumentIdClient(iDocument);
                    bool bProject = db.CheckDocumentIdProject(iDocument);
                    bool bEmployee = db.CheckDocumentIdEmployee(iDocument);
                    bool bArrangement = db.CheckDocumentidArrangement(iDocument);

                    if (bCllient == true || bProject == true || bEmployee == true || bArrangement == true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Document cannot be deleted.");
                    }
                    else
                    {
                        db.Delete(iDocument, this.Name, Login._user.idUser);

                        string filename = txtdocName.Text;
                        string ext = Path.GetExtension(txtdocName.Text).Replace(".", "");
                        string sDest = "";
                        if (ext.Trim() == "MSG" || ext.Trim() == "EML")
                        {
                            sDest = MainForm.myEmailFolder;
                        }
                        //else if (ext.Trim() == "WCPLET")
                        //{
                        //    sDest = MainForm.TemplatesFolder;
                        //}
                        else
                        {
                            sDest = MainForm.DocumentsFolder;
                        }

                        if(File.Exists(sDest + "\\" + filename))
                        {
                            File.Delete(sDest + "\\" + filename);
                        }

                        this.Close();
                        modelChanged = true;                        
                    }
                }                
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Nothing to delete.");
            }
        }

        private void btnOpenDocument_Click(object sender, EventArgs e)
        {
            if (txtdocName.Text != "")
            {
                string ext = dtm.Find(item => item.typeDocument.TrimEnd() == txtext.Text.TrimEnd()).extendDocumentType;
                string sDest = "";
                if (ext.Trim() == "MSG" || ext.Trim() == "EML")
                {
                    sDest = MainForm.myEmailFolder;
                }
                //else if (ext.Trim() == "WCPLET")
                //{
                //    sDest = MainForm.TemplatesFolder;
                //}
                else
                {
                    sDest = MainForm.DocumentsFolder;
                }

                string fullname = sDest + "\\" + txtdocName.Text;
                pathdoc = fullname;
                if (System.IO.File.Exists(fullname))
                    OpenDocument(sDest, txtdocName.Text);
                else
                    RadMessageBox.Show("Error opening document", "Open error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void OpenDocument(string sDest, string sFileName)
        {
            try
            {
                string sExtention = Path.GetExtension(sFileName).Replace(".", "");
                string sFullName = sDest + "\\" + sFileName;
                System.Diagnostics.Process.Start(sFullName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string CreateDocName(Int32 iID)
        {
            
            return iID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
        }

        private void rbIncome_CheckStateChanged(object sender, EventArgs e)
        {
            RadRadioButton rb = (RadRadioButton)sender;
            if (rb.CheckState == CheckState.Checked)
                rbNew.Visible = false;
            else
                rbNew.Visible = true;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientBUS ClientBUS = new ClientBUS();
            List<IModel> km = new List<IModel>();

            km = ClientBUS.GetAllClients(Login._user.lngUser);


            var dlgClient = new GridLookupForm(km, "Client");

            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                ClientModel okm = new ClientModel();
                okm = (ClientModel)dlgClient.selectedRow;
                txtclient.Text = okm.nameClient;
                model.idClient = okm.idClient;
                model.nameClient = okm.nameClient;
            }
        }

        private void btnarrangement_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ArrangementBUS bus = new ArrangementBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetAllArrangements();

            var dlgSave = new GridLookupForm(gm, "Arrangement");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel m = new ArrangementModel();
                m = (ArrangementModel)dlgSave.selectedRow;
                //set textbox
                txtArrangement.Text = m.nameArrangement.ToString();
                txtIdArrangement.Text = m.idArrangement.ToString();
                idArr = m.idArrangement;
                nameArr = m.nameArrangement;

               //update model
            }
            Cursor.Current = Cursors.Default;
        }

     

    }

}
