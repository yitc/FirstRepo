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
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;


namespace GUI
{
    public partial class frmAccClass : frmTemplate
    {
        private int iID;
        public AccLedgerClassModel model;
        public bool modelChanged = false;
        public string Namef;
        private int createUser;
        private DateTime dateCreate;

        public frmAccClass()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Class");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID = -1;
            InitializeComponent();
        }

        public frmAccClass(int eID)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Class");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID = eID;
            InitializeComponent();
        }

        public frmAccClass(AccLedgerClassModel emodel)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Class");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            model = emodel;
            iID = model.idClass;
            InitializeComponent();
        }


        private void frmAccClass_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;

            setTranslation();

            if (iID != -1)
            {
                txtIdClass.Text = model.idClass.ToString();
                txtCodeClass.Text = model.codeClass.ToString();
                txtDescClass.Text = model.descClass.ToString();
                ddlLevel.Text = model.levelClass.ToString();
                txtOrder.Text = model.orderClass.ToString();
                createUser = model.userCreated;
                dateCreate = model.dtCreated;
                AccAcountUpdate up = new AccAcountUpdate();
                txtUserCreated.Text = up.getUsername(model.userCreated);
                dpCreatdDate.Text = model.dtCreated.ToString();
                txtUserModified.Text = up.getUsername(model.userModified);
                dpModifiedDate.Text = model.dtModified.ToString();

            }
        }



        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

            //    lblIdClass.Text = resxSet.GetString("Id");
                if (resxSet.GetString(lblIdClass.Text) != null)
                    lblIdClass.Text = resxSet.GetString(lblIdClass.Text);
              //  lblCodeClass.Text = resxSet.GetString("Class");
                if (resxSet.GetString(lblCodeClass.Text) != null)
                    lblCodeClass.Text = resxSet.GetString(lblCodeClass.Text);
              //  lblDescClass.Text = resxSet.GetString("Description");
                if (resxSet.GetString(lblDescClass.Text) != null)
                    lblDescClass.Text = resxSet.GetString(lblDescClass.Text);
             //   lblLevelClass.Text = resxSet.GetString("Level");
                if (resxSet.GetString(lblLevelClass.Text) != null)
                    lblLevelClass.Text = resxSet.GetString(lblLevelClass.Text);
              //  lblOrderClass.Text = resxSet.GetString("Order");
                if (resxSet.GetString(lblOrderClass.Text) != null)
                    lblOrderClass.Text = resxSet.GetString(lblOrderClass.Text);
             //   btnSave.Text = resxSet.GetString("Save");
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
              //  lblCreated.Text = resxSet.GetString("Created");
                if (resxSet.GetString(lblCreated.Text) != null)
                    lblCreated.Text = resxSet.GetString(lblCreated.Text);
                lblModified.Text = resxSet.GetString("Modified");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (iID == -1)

                model = new AccLedgerClassModel();
                
                model.codeClass = txtCodeClass.Text;
                model.descClass = txtDescClass.Text;
                model.levelClass = Convert.ToInt32(ddlLevel.Text.ToString());
                model.orderClass = Convert.ToInt32(txtOrder.Text);
                if (dpCreatdDate.Text != "")
                   model.dtCreated = Convert.ToDateTime(dpCreatdDate.Text);
                if (createUser != 0)
                   model.userCreated = Convert.ToInt32(createUser);
          

                AccLedgerClassBUS bus = new AccLedgerClassBUS();
                if (iID != -1)
                {
                    model.idClass = iID;
                    model.userModified = Login._user.idUser;
                    bus.Update(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                }
                else
                {
                    model.userCreated = Login._user.idUser;
                    bus.Save(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                }
                modelChanged = true;
                this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

      
    }
}
