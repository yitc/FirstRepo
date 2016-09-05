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
    public partial class frmArticle : frmTemplate
    {
        ArticalModel articalModel;
        ArticalModel articalModelFirst;
        Boolean isTextboxesOK = false;

        public frmArticle()
        {
            InitializeComponent();
            articalModel = new ArticalModel();
            articalModelFirst = new ArticalModel();
            btnSave.Click += btnSaveInsert_Click;
        }

        public frmArticle(IModel model)
        {
            InitializeComponent();
            articalModel = (ArticalModel)model;
            articalModelFirst = new ArticalModel((ArticalModel)model);
            txtCodeArtical.Enabled = false;
            fillData();
            btnSave.Click += btnSaveUpdate_Click;
        }
        private void fillData()
        {
            if (articalModel.codeArtical != null)
                txtCodeArtical.Text = articalModel.codeArtical;

            if (articalModel.nameArtical != null)
                txtNameArtical.Text = articalModel.nameArtical;


            if (articalModel.codeArtikalGroup != null)
                txtCodeArticalGroup.Text = articalModel.codeArtikalGroup;

            if (articalModel.nameArtikalGroup != null)
                txtNameArticalGroup.Text = articalModel.nameArtikalGroup;

            if (articalModel.purchasePrice != null)
                numericPurchasePrce.Value = (decimal) articalModel.purchasePrice;

            if (articalModel.sellingPrice != null)
                numericSellingPrice.Value = (decimal)articalModel.sellingPrice;

            if (articalModel.quantity != null)
                numQuantity.Value = (int)articalModel.quantity;

            if (articalModel.isGroup != null)
            {
                if (articalModel.isGroup == true)
                    chkGroup.CheckState = CheckState.Checked;
                else
                    chkGroup.CheckState = CheckState.Unchecked;
            }
            if (articalModel.isOptional != null)
            {
                if (articalModel.isOptional == true)
                    chkOptional.CheckState = CheckState.Checked;
                else
                    chkOptional.CheckState = CheckState.Unchecked;
            } 
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblCodeArtical.Text) != null)
                    lblCodeArtical.Text = resxSet.GetString(lblCodeArtical.Text);

                if (resxSet.GetString(lblNameArtical.Text) != null)
                    lblNameArtical.Text = resxSet.GetString(lblNameArtical.Text);

                if (resxSet.GetString(lblCodeArticalGroup.Text) != null)
                    lblCodeArticalGroup.Text = resxSet.GetString(lblCodeArticalGroup.Text);

                if (resxSet.GetString(lblNameArticalGroup.Text) != null)
                    lblNameArticalGroup.Text = resxSet.GetString(lblNameArticalGroup.Text);

                if (resxSet.GetString(lblPurchasePrice.Text) != null)
                    lblPurchasePrice.Text = resxSet.GetString(lblPurchasePrice.Text);

                if (resxSet.GetString(lblSellingPrice.Text) != null)
                    lblSellingPrice.Text = resxSet.GetString(lblSellingPrice.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

                if (resxSet.GetString(lblQuantity.Text) != null)
                    lblQuantity.Text = resxSet.GetString(lblQuantity.Text);

                if (resxSet.GetString(chkGroup.Text) != null)
                    chkGroup.Text = resxSet.GetString(chkGroup.Text);

                if (resxSet.GetString(chkOptional.Text) != null)
                    chkOptional.Text = resxSet.GetString(chkOptional.Text);
            }
            
        }
        private void frmArtical_Load(object sender, EventArgs e)
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
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;

            numericPurchasePrce.MaskedEditBoxElement.EnableMouseWheel = false;
            numericSellingPrice.MaskedEditBoxElement.EnableMouseWheel = false;
            numQuantity.MaskedEditBoxElement.EnableMouseWheel = false;

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Name.Replace("frm", "")) != null)
                    formName = formName + " " + resxSet.GetString(this.Name.Replace("frm", ""));
                else
                    formName = formName + " " + this.Name.Replace("frm", "");
            }

            this.Text = formName;
             setTranslation();
        }


        private void btnCodeArticalGroup_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new ArticalGroupsBUS().GetAllArticalGroups();
            var dlgSave = new GridLookupForm(gm1, "ArticalGroups");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalGroupsModel genm1 = new ArticalGroupsModel();
                genm1 = (ArticalGroupsModel)dlgSave.selectedRow;
                //set textbox
                txtCodeArticalGroup.Text = genm1.codeArticalGroup;
                txtNameArticalGroup.Text = genm1.nameArticalGroup;
            }
        }

        private bool checkTextboxes()
        {
            if (txtCodeArtical.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to fill code for article!");
                return false;
            }
            else if (txtCodeArtical.Text.Length > 20)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Code article has maximum 20 character!");
                return false;
            }
            else if (txtNameArtical.Text.Length > 50)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Name article has maximum 50 character!");
                return false;

            }
            else if (txtCodeArticalGroup.Text=="")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to fill article group!");
                return false;

            }
            else
            {
                isTextboxesOK = true;
                return true;
            }
        }

        private void saveModel()
        {
            
            articalModel.codeArtical = txtCodeArtical.Text;
            articalModel.nameArtical = txtNameArtical.Text;
            articalModel.codeArtikalGroup = txtCodeArticalGroup.Text;
            articalModel.nameArtikalGroup = txtNameArticalGroup.Text;
            articalModel.purchasePrice = Convert.ToDecimal(numericPurchasePrce.Value);
            articalModel.sellingPrice = Convert.ToDecimal(numericSellingPrice.Value);
            articalModel.quantity = Convert.ToInt32(numQuantity.Value);
            if (chkGroup.CheckState==CheckState.Checked)
                articalModel.isGroup = true;
            else
                articalModel.isGroup = false;

            if (chkOptional.CheckState == CheckState.Checked)
                articalModel.isOptional = true;
            else
                articalModel.isOptional = false;
        }
        private void btnSaveInsert_Click(object sender, EventArgs e)
        {
            checkTextboxes();
            if (isTextboxesOK == true)
            {
                //ako je null onda artical ne postoji
                if (new ArticalBUS().GetArticalByID(txtCodeArtical.Text) == null)
                {
                    saveModel();
                    if (new ArticalBUS().Save(articalModel,this.Name ,Login._user.idUser) == true)
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
                    tr.translateAllMessageBox("Artical with that code already exist!");
                }

            }
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            checkTextboxes();
            if (isTextboxesOK == true)
            {
                saveModel();
                if (new ArticalBUS().Update(articalModel,this.Name, Login._user.idUser) == true)
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

        private void numQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void numQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Up)
            {
                e.SuppressKeyPress = true;
            }

            if (e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void numericPurchasePrce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                e.SuppressKeyPress = true;
            }

            if (e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void numericSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                e.SuppressKeyPress = true;
            }

            if (e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void UpdateOriginalValuesAfterSave()
        {
            articalModelFirst = new ArticalModel(articalModel);
            
        }
        private void frmArticle_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveModel();

            bool changes = articalModel.CompareWith(articalModelFirst);

            if (changes == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Save before close ?", "Save");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {

                    bool res = checkTextboxes();
                    if(res == false)
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
                    articalModel.CopyValues(articalModelFirst);
                }   
            }            
        }
    }
}
