using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Business;
using BIS.Model;
using System.Resources;
using Telerik.WinControls;

namespace GUI
{
    public partial class frmPriceListArticles : frmTemplate
    {
        //only this model uses for form changes        
        public PriceListArticlesModel pricelistModel;
        public PriceListArticlesModel pricelistModelClear;

        int idPriceListArticles = -1;
        ArrangementModel arrangem;
        Boolean isDescriptionArticle = false;

        //flag for update or insert 
        Boolean isNew = true;


        public frmPriceListArticles(int idPriceList, ArrangementModel am, Boolean isDescription)
        {
            InitializeComponent();
            pricelistModel = new PriceListArticlesModel();
            arrangem = am;
            pricelistModel.idPriceList = idPriceList;
            pricelistModel.DtFrom = am.dtFromArrangement;
            pricelistModel.DtTo = am.dtToArrangement;

            isDescriptionArticle = isDescription;
            isNew = true;
            btnSave.Click += save;
        }

        public frmPriceListArticles(PriceListArticlesModel model, ArrangementModel am, Boolean isDescription)
        {
            InitializeComponent();
            pricelistModel = (PriceListArticlesModel)model;
            pricelistModelClear = new PriceListArticlesModel(model);
            idPriceListArticles = pricelistModel.idPriceListArticle;
            arrangem = am;
            isNew = false;
            isDescriptionArticle = isDescription;
            btnSave.Click += save;
        }

        private void frmPriceList_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonContact.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonTask.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
            radRibbonReports.Visibility = ElementVisibility.Collapsed;

            numericNrArticle.MaskedEditBoxElement.EnableMouseWheel = false;
            numericPricePerArticle.MaskedEditBoxElement.EnableMouseWheel = false;
            numericPricePerQuantity.MaskedEditBoxElement.EnableMouseWheel = false;
            numCommission.MaskedEditBoxElement.EnableMouseWheel = false;

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
            

            if (arrangem.dtFromArrangement != null)
                dateFrom.Value = arrangem.dtFromArrangement;
            if (arrangem.dtToArrangement != null)
                dateTo.Value = arrangem.dtToArrangement;

            SetDataBindings();

            if(idPriceListArticles!=-1)
                ReadModel();

            if(isDescriptionArticle==true)
            {
                numericPricePerArticle.Enabled = false;
                numericPricePerQuantity.Enabled = false;
                numCommission.Enabled = false;
            }
            setTranslation();
            pricelistModel.isDirty = false;
        }

        private void SetDataBindings()
        {
            txtArticleID.DataBindings.Add("Text", pricelistModel, "IdArticle");
            txtArticleName.Text = pricelistModel.nameArtical;
            numericNrArticle.DataBindings.Add("Value", pricelistModel, "NrArticle");
            numericQuantity.Value = pricelistModel.quantity;
            numericPricePerArticle.DataBindings.Add("Value", pricelistModel, "PricePerArticle");
            numericPricePerQuantity.DataBindings.Add("Value", pricelistModel, "PricePerQuantity");
            chkExtra.DataBindings.Add("Checked", pricelistModel, "IsExtra");
            dateFrom.DataBindings.Add("Value", pricelistModel, "DtFrom");
            dateTo.DataBindings.Add("Value", pricelistModel, "DtTo");

            numCommission.DataBindings.Add("Value", pricelistModel, "Commission");
            chkAccomodation.DataBindings.Add("Checked", pricelistModel, "IsAccomodation");
            chkAway.DataBindings.Add("Checked", pricelistModel, "IsAway");
            chkBack.DataBindings.Add("Checked", pricelistModel, "IsBack");
            chkNotAccompaniment.DataBindings.Add("Checked", pricelistModel, "IsNotInAccompaniment");
            chkNotForTravelers.DataBindings.Add("Checked", pricelistModel, "IsNotForTraveler");
        }

        public void ReadModel()
        {            
            //dateFrom.Value = pricelistModel.dtFrom;  // prebaceno ispod prikaza dana da bi dao dobar broj dana aranzmana 2-11 Saki
            //dateTo.Value = pricelistModel.dtTo;
            
            numericTotal.Value = pricelistModel.priceTotal; 
            if (pricelistModel.IdClient != 0 && pricelistModel.nameClient != null)
            {
                txtClient.Text = pricelistModel.nameClient;
              loadAddress(pricelistModel.IdClient);
            }

            roomForBusDriver(pricelistModel.IdArticle);
            recalculatePrice();

        }

        private void loadAddress(int idClient)
        {
            List<ClientAddressModel> cam = new List<ClientAddressModel>();
            cam = new ClientAddressBUS().GetClientAddressesByType(1, idClient);
            if(cam!=null)
            for (int i = 0; i < cam.Count; i++)
            {
                string street = "";
                string streetnr = "";
                string zipcode = "";
                string city = "";
                string country = "";
                string address = "";
                if (cam[i].street != null)
                    street = cam[i].street;
                if (cam[i].housenr != null)
                    streetnr = cam[i].housenr;
                if (cam[i].postalCode != null)
                    zipcode = cam[i].postalCode;
                if (cam[i].city != null)
                    city = cam[i].city;
                if (cam[i].country != null)
                    country = cam[i].country;

                if (street != "")
                    address = address + street + " ";
                if (streetnr != "")
                    address = address + streetnr + "\n";
                if (zipcode != "")
                    address = address + zipcode + " ";
                if (city != "")
                    address = address + city + "\n";
                if (country != "")
                    address = address + country + " ";

                lblAddress.Text = address;
            }
        }

        private void btnArticalID_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new ArticalBUS().GetAllArticals();
            var dlgSave = new GridLookupForm(gm1, "Articals");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalModel genm1 = new ArticalModel();
                genm1 = (ArticalModel)dlgSave.selectedRow;
                //set textbox
                txtArticleID.Text = genm1.codeArtical;
                pricelistModel.IdArticle = txtArticleID.Text;
                txtArticleName.Text = genm1.nameArtical;
                numericQuantity.Value = genm1.quantity;
                setGroupArticle(genm1.isGroup);
                roomForBusDriver(genm1.codeArtical);
            }
        }

        private void roomForBusDriver(string articalCode)
        {
            if (articalCode == "1 pk chauf")
            {
               // isDescriptionArticle = true;
                numCommission.Enabled = false;
                numericPricePerArticle.Enabled = false;
                numericPricePerQuantity.Enabled = false;
                numCommission.Value = 0;
                numericPricePerArticle.Value = 0;
                numericPricePerQuantity.Value = 0;
                pricelistModel.PricePerArticle = 0;
                pricelistModel.PricePerQuantity = 0;
                pricelistModel.Commission = 0;
            }
            else
            {
               // isDescriptionArticle = false;
                if (isDescriptionArticle == false)
                {
                    numCommission.Enabled = true;
                    numericPricePerArticle.Enabled = true;
                    numericPricePerQuantity.Enabled = true;
                }
            }
        }

        private void setGroupArticle(Boolean isGroup)
        {
            if (isGroup == true)
                lblPricePerArticle.Text = "Purchase price per group";
            else
                lblPricePerArticle.Text = "Purchase price per person";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblPricePerArticle.Text) != null)
                    lblPricePerArticle.Text = resxSet.GetString(lblPricePerArticle.Text);
            }
            recalculatePrice();
        }

        public bool CheckInputs()
        {
            int number = Convert.ToInt32(numericNrArticle.Value);
            if (txtArticleID.Text == null || txtArticleID.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You must add article");
                return false;
            }
            else if (number <= 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Number of Articles must be > 0");
                return false;
            }
            else if (txtArticleID.Text == "1 pk chauf")
            {
                return true;
            }
            else if (chkExtra.CheckState == CheckState.Checked)
            {
                return true;
            }
            else if (isDescriptionArticle == true)
            {
                return true;
            }
            else if (Convert.ToDecimal(numericPricePerArticle.Value) == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Article purchase price must be filled");
                return false;
            }
            else if (Convert.ToDecimal(numericPricePerQuantity.Value) == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Nr must be filled");
                return false;
            }
            else
            {
                return true;
            }

        }

        private void save(object sender, EventArgs e)
        {
            saveIt();

        }

        private void saveIt()
        {
            bool b = CheckInputs();

            try
            {
                if (b == true)
                {
                    btnSave.Focus();
                    if (isNew == true)
                    {
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);
                        pricelistModel.DtFrom = dateFrom.Value;
                        pricelistModel.DtTo = dateTo.Value;
                        pricelistModel.PricePerArticle = Convert.ToDecimal(numericPricePerArticle.Value);
                        pricelistModel.PricePerQuantity = Convert.ToDecimal(numericPricePerQuantity.Value);
                        pricelistModel.IdArticle = txtArticleID.Text;
                        pricelistModel.NrArticle = Convert.ToInt32(numericNrArticle.Value);
                        pricelistModel.IsExtra = chkExtra.Checked;
                        pricelistModel.IsAway = chkAway.Checked;
                        pricelistModel.IsBack = chkBack.Checked;
                        pricelistModel.IsAccomodation = chkAccomodation.Checked;
                        pricelistModel.IsNotInAccompaniment = chkNotAccompaniment.Checked;
                        pricelistModel.IsNotForTraveler = chkNotForTravelers.Checked;
                        pricelistModel.Commission = Convert.ToDecimal(numCommission.Value);
                    }

                    pricelistModel.nameArtical = txtArticleName.Text;

                    pricelistModel.quantity = Convert.ToInt32(numericQuantity.Value);


                    pricelistModel.priceTotal = Convert.ToDecimal(numericTotal.Value);
                    pricelistModel.idUserModified = pricelistModel.IdClient;
                    pricelistModel.dtUserModified = DateTime.Now;
                    pricelistModel.IdClient = pricelistModel.IdClient;

                    if (isNew == true)
                    {
                        pricelistModel.idUserCreated = Login._user.idUser;
                        pricelistModel.dtUserCreated = DateTime.Now;
                    }
                    this.DialogResult = DialogResult.OK;
                    pricelistModel.isDirty = false;
                    //this.Close();
                }
                else
                {
                    pricelistModel.isDirty = false;
                    //this.DialogResult = DialogResult.Cancel;
                }
            }catch(Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }

        }


        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);
                if (resxSet.GetString(lvlArticleID.Text) != null)
                    lvlArticleID.Text = resxSet.GetString(lvlArticleID.Text);
                if (resxSet.GetString(lblArticleName.Text) != null)
                    lblArticleName.Text = resxSet.GetString(lblArticleName.Text);
                if (resxSet.GetString(lblNrArticle.Text) != null)
                    lblNrArticle.Text = resxSet.GetString(lblNrArticle.Text);
                if (resxSet.GetString(lblQuantity.Text) != null)
                    lblQuantity.Text = resxSet.GetString(lblQuantity.Text);
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                if (resxSet.GetString(lblPricePerArticle.Text) != null)
                    lblPricePerArticle.Text = resxSet.GetString(lblPricePerArticle.Text);
                if (resxSet.GetString(lblPricePerQuantity.Text) != null)
                    lblPricePerQuantity.Text = resxSet.GetString(lblPricePerQuantity.Text);
                if (resxSet.GetString(lblTotal.Text) != null)
                    lblTotal.Text = resxSet.GetString(lblTotal.Text);
                if (resxSet.GetString(lblCommission.Text) != null)
                    lblCommission.Text = resxSet.GetString(lblCommission.Text);
                if (resxSet.GetString(lblTotal2.Text) != null)
                    lblTotal2.Text = resxSet.GetString(lblTotal2.Text);
                if (resxSet.GetString(lblCommissionPrice.Text) != null)
                    lblCommissionPrice.Text = resxSet.GetString(lblCommissionPrice.Text);
                if (resxSet.GetString(lblAway.Text) != null)
                    lblAway.Text = resxSet.GetString(lblAway.Text);
                if (resxSet.GetString(lblBack.Text) != null)
                    lblBack.Text = resxSet.GetString(lblBack.Text);
                if (resxSet.GetString(lblAccomodation.Text) != null)
                    lblAccomodation.Text = resxSet.GetString(lblAccomodation.Text);
                if (resxSet.GetString(lblNotAccompaniment.Text) != null)
                    lblNotAccompaniment.Text = resxSet.GetString(lblNotAccompaniment.Text);
                if (resxSet.GetString(lblNotForTravelers.Text) != null)
                    lblNotForTravelers.Text = resxSet.GetString(lblNotForTravelers.Text);
                if (resxSet.GetString(lblExtra.Text) != null)
                    lblExtra.Text = resxSet.GetString(lblExtra.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                Boolean isGroup = false;
                if (txtArticleID.Text != "")
                {
                    ArticalModel am = new ArticalModel();
                    am = new ArticalBUS().GetArticalByID(txtArticleID.Text);
                    if (am != null)
                        isGroup = am.isGroup;
                }
                if (isGroup == true)
                    lblPricePerArticle.Text = "Purchase price per group";
                else
                    lblPricePerArticle.Text = "Purchase price per person";

                if (resxSet.GetString(lblPricePerArticle.Text) != null)
                    lblPricePerArticle.Text = resxSet.GetString(lblPricePerArticle.Text);
            }
           
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
                txtClient.Text = okm.nameClient;
                pricelistModel.IdClient = okm.idClient;
                pricelistModel.nameClient = okm.nameClient;
                loadAddress(pricelistModel.IdClient);
            }
        }

        private void recalculatePrice()
        {
            Boolean isGroup = false;
            if (txtArticleID.Text != "")
            {
                ArticalModel am = new ArticalModel();
                am = new ArticalBUS().GetArticalByID(txtArticleID.Text);
                if (am != null)
                    isGroup = am.isGroup;
                //txtArticleID.Text = am.codeArtical;
                 //  pricelistModel.IdArticle = txtArticleID.Text;
            }
            if (isGroup == true)
                numTotalReadOnly.Value = Convert.ToDecimal(numericPricePerArticle.Value) * Convert.ToDecimal(numericPricePerQuantity.Value) * Convert.ToDecimal(numericNrArticle.Value);
            else
                numTotalReadOnly.Value = Convert.ToDecimal(numericPricePerArticle.Value) * Convert.ToDecimal(numericPricePerQuantity.Value);
            numericTotal.Value = Convert.ToDecimal(numTotalReadOnly.Value) - (Convert.ToDecimal(numTotalReadOnly.Value) * Convert.ToDecimal(numCommission.Value) / 100);
            numCommissionPrice.Value = (Convert.ToDecimal(numTotalReadOnly.Value) * Convert.ToDecimal(numCommission.Value) / 100);
        
        }

        private void numericPricePerArticle_ValueChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void numericPricePerQuantity_ValueChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void numCommission_ValueChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void numNrArticle_KeyDown(object sender, KeyEventArgs e)
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

        private void numericQuantity_KeyDown(object sender, KeyEventArgs e)
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

        private void numericPricePerArticle_KeyDown(object sender, KeyEventArgs e)
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

        private void numericPricePerQuantity_KeyDown(object sender, KeyEventArgs e)
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

        private void numCommission_KeyDown(object sender, KeyEventArgs e)
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

        private void numericNrArticle_ValueChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }

        private void frmPriceListArticles_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.SelectNextControl(this.ActiveControl, true, true, true, true);
            if (pricelistModel.isDirty == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                System.Windows.Forms.DialogResult res = tr.translateAllMessageBoxDialogYesNo("Are you want to save the changes?", "Save");
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    saveIt();
                    //this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    pricelistModel.isDirty = false;
                    //pricelistModel = pricelistModelClear;                    

                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

            }

        }

        private void frmPriceListArticles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                saveIt();
            }
        }
    }
}
