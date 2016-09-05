using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI
{
    public partial class frmArticleGroups : frmTemplate
    {
        ArticalGroupsModel ArticalGroups;
        ArticalGroupsModel ArticalGroupsFirst;
        Boolean isTextboxesOK = false;
        public List<ArticalGroupsModel> agm;

        public frmArticleGroups()
        {
            InitializeComponent();
            ArticalGroups = new ArticalGroupsModel();
            ArticalGroupsFirst = new ArticalGroupsModel();
            btnSave.Click += btnSaveInsert_Click;

            //ArticalGroupsBUS agb = new ArticalGroupsBUS();
            //agm = new List<ArticalGroupsModel>();
            //agm = agb.GetAllArticalClass();
            //ddlClass.DataSource = agm;
            //ddlClass.DisplayMember = "classArticalGroup";
            //ddlClass.ValueMember = "classArticalGroup";
        }

        public frmArticleGroups(IModel model)
        {
            InitializeComponent();
            ArticalGroups = (ArticalGroupsModel) model;
            ArticalGroupsFirst = new ArticalGroupsModel((ArticalGroupsModel)model);
            txtCodeArticalGroups.Enabled = false;
            btnSave.Click += btnSaveUpdate_Click;

            //ArticalGroupsBUS agb = new ArticalGroupsBUS();
            //// List<ArticalGroupsModel> agm = new List<ArticalGroupsModel>();
            //agm = new List<ArticalGroupsModel>();
            //agm = agb.GetAllArticalClass();
            //ddlClass.DataSource = agm;
            //ddlClass.DisplayMember = "classArticalGroup";
            //ddlClass.ValueMember = "classArticalGroup";


            fillData();
        }

        private void fillData()
        {
            if (ArticalGroups.codeArticalGroup != null)
                txtCodeArticalGroups.Text = ArticalGroups.codeArticalGroup;

            if (ArticalGroups.nameArticalGroup != null)
                txtNameArticalGroups.Text = ArticalGroups.nameArticalGroup;

            if (ArticalGroups.descInkopArtical != null)
                txtInkopArtical.Text = ArticalGroups.descInkopArtical;

            if (ArticalGroups.descVerkopArtical != null)
                txtVerkopArticalGroups.Text = ArticalGroups.descVerkopArtical;

            if (ArticalGroups.inkopArtical != null)
                txtInkopNumber.Text = ArticalGroups.inkopArtical;

            if (ArticalGroups.verkopArtical != null)
                txtVerkopNumber.Text = ArticalGroups.verkopArtical;
                                                
            if (ArticalGroups.isActive != null)
            {
                if (ArticalGroups.isActive ==true)
                    chkActive.CheckState = CheckState.Checked;
                else
                    chkActive.CheckState = CheckState.Unchecked;
            }
            if (ArticalGroups.classArticalGroup != null)
                ddlClass.SelectedValue = ArticalGroups.classArticalGroup;
              //  ddlClass.SelectedItem = ddlClass.Items[agm.FindIndex(item => item.classArticalGroup == ArticalGroups.classArticalGroup)];
             //  ddlClass.SelectedItem.Text = ArticalGroups.classArticalGroup;
          //  ddlClass.SelectedText = ArticalGroups.classArticalGroup;
        
          //  ddlClass.ValueMember = ArticalGroups.classArticalGroup;
           // ddlClass.Text = ArticalGroups.classArticalGroup;
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblCodeArticalGroups.Text) != null)
                    lblCodeArticalGroups.Text = resxSet.GetString(lblCodeArticalGroups.Text);
                if (resxSet.GetString(lblNameArticalGroups.Text) != null)
                    lblNameArticalGroups.Text = resxSet.GetString(lblNameArticalGroups.Text);
                if (resxSet.GetString(lblInkopAccount.Text) != null)
                    lblInkopAccount.Text = resxSet.GetString(lblInkopAccount.Text);
                if (resxSet.GetString(lblVerkopAccount.Text) != null)
                    lblVerkopAccount.Text = resxSet.GetString(lblVerkopAccount.Text);
                if (resxSet.GetString(chkActive.Text) != null)
                    chkActive.Text = resxSet.GetString(chkActive.Text);
                if (resxSet.GetString(lblClassification.Text) != null)
                    lblClassification.Text = resxSet.GetString(lblClassification.Text);
            }
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            checkTextboxes();
            if(isTextboxesOK==true)
            {
                saveModel();
                ArticalGroups.idUserModified = Login._user.idUser;
                ArticalGroups.dtUserModified = DateTime.Now;
                if (new ArticalGroupsBUS().Update(ArticalGroups, this.Name, Login._user.idUser) == true)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have successfully update data!");
                    UpdateOriginalValuesAfterSave();
                    this.Close();
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Something went wrong with updating!");
                }
            }
        }

        private void btnSaveInsert_Click(object sender, EventArgs e)
        {
            checkTextboxes();
            if (isTextboxesOK == true)
            {
                if(new ArticalGroupsBUS().checkIfExist(txtCodeArticalGroups.Text)<=0)
                {
                    saveModel();
                    ArticalGroups.idUserCreated = Login._user.idUser;
                    ArticalGroups.dtUserCreated = DateTime.Now;
                    if (new ArticalGroupsBUS().Save(ArticalGroups, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have successfully insert data!");
                        UpdateOriginalValuesAfterSave();
                        this.Close();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong with inserting!");
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Article group with code already exist!");
                }

            }
        }

        private void saveModel()
        {
            ArticalGroups.codeArticalGroup = txtCodeArticalGroups.Text;
            if (txtNameArticalGroups.Text!=null)
            ArticalGroups.nameArticalGroup = txtNameArticalGroups.Text;
            if (chkActive.Checked == true)
                ArticalGroups.isActive = true;
            else
                ArticalGroups.isActive = false;
            if (ddlClass.SelectedItem != null)
              ArticalGroups.classArticalGroup = ddlClass.SelectedItem.ToString();             // ddlClass.SelectedItem.ToString();
        }

        private void frmArticalGroups_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Name.Replace("frm", "")) != null)
                    formName = formName + " " + resxSet.GetString(this.Name.Replace("frm", ""));
                else
                    formName = formName + " " + this.Name.Replace("frm", "");
            }

            //ArticalGroupsBUS agb = new ArticalGroupsBUS();
            //// List<ArticalGroupsModel> agm = new List<ArticalGroupsModel>();
            //agm = new List<ArticalGroupsModel>();
            //agm = agb.GetAllArticalClass();
            //ddlClass.DataSource = agm;
            //ddlClass.DisplayMember = "classArticalGroup";
            //ddlClass.ValueMember = "classArticalGroup";


            this.Text = formName;
            setTranslation();


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
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
        }

        private bool checkTextboxes()
        {
            if (txtCodeArticalGroups.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to fill code for article groups!");
                return false;
            }
            else if (txtCodeArticalGroups.Text.Length > 20)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Code article groups has maximum 20 character!");
                return false;
            }
            else if (txtNameArticalGroups.Text.Length > 50)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Name article groups has maximum 50 character!");
                return false;
            }
            else  if (txtInkopNumber.Text == "")
            {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to fill purchase number for article groups!");
                    return false;
            }
            else if (txtVerkopNumber.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to fill sell number for article groups!");
                return false;
            }
            else
            {
                isTextboxesOK = true;
                return true;
            }
        }

        private void btnInkopArtical_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new LedgerAccountBUS(Login._bookyear).GetAllAccounts();
            var dlgSave = new GridLookupForm(gm1, "PurchaseAccount");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genm1 = new LedgerAccountModel();
                genm1 = (LedgerAccountModel)dlgSave.selectedRow;
                //set textbox
                txtInkopArtical.Text = genm1.descLedgerAccount;
                txtInkopNumber.Text = genm1.numberLedgerAccount.ToString();
                ArticalGroups.inkopArtical = genm1.numberLedgerAccount.ToString();
            }
        }

        private void btnVerkopArtical_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new LedgerAccountBUS(Login._bookyear).GetAllAccounts();
            var dlgSave = new GridLookupForm(gm1, "SellingAccount");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genm1 = new LedgerAccountModel();
                genm1 = (LedgerAccountModel)dlgSave.selectedRow;
                //set textbox
                txtVerkopArticalGroups.Text = genm1.descLedgerAccount;
                txtVerkopNumber.Text = genm1.numberLedgerAccount.ToString();
                ArticalGroups.verkopArtical = genm1.numberLedgerAccount.ToString();
                
            }
        }

        private void UpdateOriginalValuesAfterSave()
        {
            ArticalGroupsFirst = new ArticalGroupsModel(ArticalGroups);

        }

        private void frmArticleGroups_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveModel();
            bool changes = ArticalGroups.CompareWith(ArticalGroupsFirst);

            if (changes == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Save before close ?", "Save");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool res = checkTextboxes();
                    if (res == false)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        btnSave.PerformClick();
                    }
                }
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    // NO option
                    ArticalGroups.CopyValues(ArticalGroupsFirst);
                }
            } 
        }
    }
}
