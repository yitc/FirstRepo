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
    public partial class frmCost : frmTemplate
    {

        private int iID;
        public AccCostModel model;
        public bool modelChanged = false;
        public string Namef;
        private int createUser;
        private DateTime dateCreate;

        public frmCost()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Cost");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();
            iID = -1;
        }

        public frmCost(int eID)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Cost");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID = eID;
            InitializeComponent();
        }

        public frmCost(AccCostModel emodel)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Cost");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            model = emodel;
            iID = model.idCost;
            InitializeComponent();
        }

        private void frmCost_Load(object sender, EventArgs e)
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
                txtIdCost.Text = model.idCost.ToString();
                txtCodeCost.Text = model.codeCost.ToString();
                txtDescCost.Text = model.descCost.ToString();
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

                if (resxSet.GetString(lblCreated.Text) != null)
                    lblCreated.Text = resxSet.GetString(lblCreated.Text);

                if (resxSet.GetString(lblCodeCost.Text) != null)
                    lblCodeCost.Text = resxSet.GetString(lblCodeCost.Text);

                if (resxSet.GetString(lblIdCost.Text) != null)
                    lblIdCost.Text = resxSet.GetString(lblIdCost.Text);

                if (resxSet.GetString(lblDescCost.Text) != null)
                    lblDescCost.Text = resxSet.GetString(lblDescCost.Text);

                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

                if (resxSet.GetString(lblCreated.Text) != null)
                    lblCreated.Text = resxSet.GetString(lblCreated.Text);

                if (resxSet.GetString(lblModified.Text) != null)
                    lblModified.Text = resxSet.GetString(lblModified.Text);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (iID == -1)
               
                model = new AccCostModel();
                
                model.codeCost = txtCodeCost.Text;
                model.descCost = txtDescCost.Text;
                


                AccCostBUS bus = new AccCostBUS();
                if (iID != -1)
                {
                    model.idCost = iID;
                    model.userModified = Login._user.idUser;
                    model.userCreated = createUser;
                    model.dtCreated = dateCreate;
                    bus.Update(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                }
                else
                {
                    model.userCreated = Login._user.idUser;
                  
                    bus.Save(model,this.Name ,Login._user.idUser);
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
