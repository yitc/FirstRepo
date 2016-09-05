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
    public partial class frmDepartments : frmTemplate

    {
        private int iID;
        public DepartmentsModel model;
        public bool modelChanged = false;
        public string Namef;

        public frmDepartments()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Departments");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef; 
            InitializeComponent();
            iID = -1;
        }

        public frmDepartments(int eID)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Departments");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID = eID;
            InitializeComponent();
        }

        public frmDepartments(DepartmentsModel emodel)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Departments");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            model = emodel;
            iID = model.idDepartment;
            InitializeComponent();
        }

        private void frmDepartments_Load(object sender, EventArgs e)
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
                txtIdDepartments.Text = model.idDepartment.ToString();
                txtDepartments.Text = model.nameDepartment.ToString();
                txtTelephone.Text = model.telephoneDepartment.ToString();
                txtEmail.Text = model.emailDepartment.ToString();
            }
        }
        private void setTranslation ()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                lblDepartmentsId.Text = resxSet.GetString("Id department");
                lblDepartments.Text = resxSet.GetString("Department");
                lblTelephone.Text = resxSet.GetString("Telephone");
                lblEmail.Text = resxSet.GetString("Email");
                btnSave.Text = resxSet.GetString("Save");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model = new DepartmentsModel();

                model.nameDepartment = txtDepartments.Text;
                model.telephoneDepartment = txtTelephone.Text;
                model.emailDepartment = txtEmail.Text;


                DepartmentsBUS bus = new DepartmentsBUS();
                if(iID != -1)
                {
                    model.idDepartment = iID;
                    bus.Update(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                }
                else
                {
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
