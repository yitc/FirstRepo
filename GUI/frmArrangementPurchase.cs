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
    public partial class frmArrangementPurchase : frmTemplate
    {
        ArrangementPriceModel ap;
        decimal PurchasePricePerArticle = 0;
        int iID = -1;
        Boolean isTextboxesOK = false;

        public frmArrangementPurchase(int idArrangement)
        {
            InitializeComponent();
            ap = new ArrangementPriceModel();
            ap.idArrangement = idArrangement;
            ArrangementModel am = new ArrangementBUS().GetArrangementById(idArrangement);
            dateFrom.Value = am.dtFromArrangement;
            dateTo.Value = am.dtToArrangement;
            btnSave.Click += btnSaveInsert_Click;
        }
         
        public frmArrangementPurchase(IModel model)
        {
            InitializeComponent();
            ap = (ArrangementPriceModel)model;
            iID = ap.idArrangementPrice;
            btnSave.Click += btnSaveUpdate_Click;
            fillData();
        }

        private void btnSaveInsert_Click(object sender, EventArgs e)
        {
            checkTextboxes();
            if (isTextboxesOK == true)
            {
                if (new ArrangementPriceBUS().checkIfArrangementPriceExist(ap.idArrangementPrice, ap.idArrangement, ap.idArticle, ap.idClient, true) <= 0)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You already have arrangement prices for that article and that client!");

                }
                else
                {
                    ap.idUserCreated = Login._user.idUser;
                    ap.dtUserCreated = DateTime.Now;
                    ap.nrArticle = Convert.ToInt32(numNrArticle.Value);
                    ap.pricePerArticle = Convert.ToDecimal(numericPricePerArticle.Value);
                    ap.pricePerQuantity = Convert.ToDecimal(numericPricePerQuantity.Value);
                    ap.priceTotal = Convert.ToDecimal(numericTotal.Value);
                    ap.dtFrom = dateFrom.Value;
                    ap.dtTo = dateTo.Value;
                    if (chkExtra.CheckState == CheckState.Checked)
                        ap.isExtra = true;
                    else
                        ap.isExtra = false;

                    ap.isAway = chkAway.Checked;
                    ap.isBack = chkBack.Checked;
                    ap.isAccomodation = chkAccomodation.Checked;
                    ap.isNotInAccompaniment = chkNotAccompaniment.Checked;
                    ap.isNotForTraveler = chkNotForTravelers.Checked;
                    ap.commission = Convert.ToDecimal(numCommission.Value);
                    if (new ArrangementPriceBUS().Save(ap, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have successfully insert data!");
                        this.Close();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong with inserting!");
                    }
                }
            }
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            checkTextboxes();
            if (isTextboxesOK == true)
            {
                 if (new ArrangementPriceBUS().checkIfArrangementPriceExist(ap.idArrangementPrice,ap.idArrangement,ap.idArticle,ap.idClient,false) <= 0)
                 {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You already have arrangement prices for that article and that client!");

                 }
                 else
                 {
                        ap.idUserModified = Login._user.idUser;
                        ap.dtUserModified = DateTime.Now;
                        ap.nrArticle = Convert.ToInt32(numNrArticle.Value);
                        ap.pricePerArticle = Convert.ToDecimal(numericPricePerArticle.Value);
                        ap.pricePerQuantity = Convert.ToDecimal(numericPricePerQuantity.Value);
                        ap.priceTotal = Convert.ToDecimal(numericTotal.Value);
                        ap.dtFrom = dateFrom.Value;
                        ap.dtTo = dateTo.Value;
                        if (chkExtra.CheckState == CheckState.Checked)
                            ap.isExtra = true;
                        else
                            ap.isExtra = false;

                        ap.isAway = chkAway.Checked;
                        ap.isBack = chkBack.Checked;
                        ap.isAccomodation = chkAccomodation.Checked;
                        ap.isNotInAccompaniment = chkNotAccompaniment.Checked;
                        ap.isNotForTraveler = chkNotForTravelers.Checked;
                        ap.commission = Convert.ToDecimal(numCommission.Value);
                        if (new ArrangementPriceBUS().Update(ap, this.Name, Login._user.idUser) == true)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have successfully update data!");
                            this.Close();
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with updating!");
                        }
                 }
            }
        }

        private void checkTextboxes()
        {
            int nrArticle = Convert.ToInt32(numNrArticle.Value);
            decimal pricep = Convert.ToDecimal(numericTotal.Value);

            if (txtArticleID.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select article!");
                isTextboxesOK = false;
            }
            else if (nrArticle <= 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to fill number of articles!");
                isTextboxesOK = false;

            }
            else if (txtArticleID.Text == "1 pk chauf")
            {
                isTextboxesOK = true;
            }
            else if (chkExtra.CheckState == CheckState.Checked)
            {
                isTextboxesOK = true;
            }
            else if (Convert.ToDecimal(numericPricePerArticle.Value) == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Article purchase price must be filled");
                isTextboxesOK = false;
            }
            else if (Convert.ToDecimal(numericPricePerQuantity.Value) == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Nr must be filled");
                isTextboxesOK = false;
            }
            else
            {
                isTextboxesOK = true;
            }
        }

        private void fillData()
        {
            txtArticleID.Text = ap.idArticle;
            txtArticleName.Text = new ArticalBUS().GetArticalByID(ap.idArticle).nameArtical;
            numNrArticle.Value = ap.nrArticle;
            numericQuantity.Value = ap.quantity;
            txtClient.Text = ap.nameClient;
            numericTotal.Value = ap.priceTotal;
            numericPricePerQuantity.Value = ap.pricePerQuantity;
            numericPricePerArticle.Value = ap.pricePerArticle;
            dateFrom.Value = ap.dtFrom;
            dateTo.Value = ap.dtTo;
            if (ap.isExtra!=null)
            {
                if (ap.isExtra==true)
                    chkExtra.CheckState = CheckState.Checked;
                else
                    chkExtra.CheckState = CheckState.Unchecked;
                
            }
            numCommission.Value = ap.commission;
            if (ap.isAccomodation != null)
                if (ap.isAccomodation == true)
                    chkAccomodation.CheckState = CheckState.Checked;
                else
                    chkAccomodation.CheckState = CheckState.Unchecked;

            if (ap.isAway != null)
                if (ap.isAway == true)
                    chkAway.CheckState = CheckState.Checked;
                else
                    chkAway.CheckState = CheckState.Unchecked;

            if (ap.isBack != null)
                if (ap.isBack == true)
                    chkBack.CheckState = CheckState.Checked;
                else
                    chkBack.CheckState = CheckState.Unchecked;

            if (ap.isNotInAccompaniment != null)
                if (ap.isNotInAccompaniment == true)
                    chkNotAccompaniment.CheckState = CheckState.Checked;
                else
                    chkNotAccompaniment.CheckState = CheckState.Unchecked;

            if (ap.isNotForTraveler != null)
                if (ap.isNotForTraveler == true)
                    chkNotForTravelers.CheckState = CheckState.Checked;
                else
                    chkNotForTravelers.CheckState = CheckState.Unchecked;

            if (ap.idClient != 0 && ap.nameClient != null)
                txtClient.Text = ap.nameClient;
            if (ap.idClient != 0 && ap.nameClient != null)
            {
                loadAddress(ap.idClient);
            }
            roomForBusDriver(ap.idArticle);
            recalculatePrice();
        }

        private void loadAddress(int idClient)
        {
            List<ClientAddressModel> cam = new List<ClientAddressModel>();
            cam = new ClientAddressBUS().GetClientAddressesByType(1, idClient);
            if (cam != null)
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

        private void frmArrangementPurchase_Load(object sender, EventArgs e)
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
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Visible;
            btnPurchase.Visibility = ElementVisibility.Visible;

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

        private void btnArticalID_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new ArrangementPriceBUS().GetAllArticals(ap.idArrangement,ap.idClient);
            var dlgSave = new GridLookupForm(gm1, "Articals");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalModel genm1 = new ArticalModel();
                genm1 = (ArticalModel)dlgSave.selectedRow;
                //set textbox
                ap.idArticle = genm1.codeArtical;
                txtArticleID.Text = genm1.codeArtical;
                txtArticleName.Text = genm1.nameArtical;
                numericQuantity.Value = genm1.quantity;
                setGroupArticle(genm1.isGroup);
                roomForBusDriver(genm1.codeArtical);
            }
        }

        private void roomForBusDriver(string articalCode)
        {
                numCommission.Value = 0;
                numericPricePerArticle.Value = 0;
                numericPricePerQuantity.Value = 0;
        }

        private void setGroupArticle(Boolean isGroup)
        {
            if(isGroup==true)
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

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);
                if (resxSet.GetString(lblArticleID.Text) != null)
                    lblArticleID.Text = resxSet.GetString(lblArticleID.Text);
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
            List<IModel> gm1 = new List<IModel>();
            if(ap.idContract!=0)
                gm1 = new ClientBUS().GetClientByContract(ap.idContract);
            else
                gm1 = new ClientBUS().GetAllClients(Login._user.lngUser);
            var dlgSave = new GridLookupForm(gm1, "Client");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ClientModel genm1 = new ClientModel();
                genm1 = (ClientModel)dlgSave.selectedRow;
                //set textbox
                ap.idClient = genm1.idClient;
                txtClient.Text = genm1.nameClient;
                btnOpenClient.Visible = true;
                loadAddress(ap.idClient);
            }
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            if(ap.idClient > 0)
            {
                ClientBUS cbus = new ClientBUS();
                ClientModel model =  cbus.GetClient(ap.idClient);
                frmClient frm = new frmClient(model, model.idClient);
                frm.showContractTab = true;
                frm.ShowDialog();
            }
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

        private void recalculatePrice()
        {
            Boolean isGroup = false;
            if (txtArticleID.Text != "")
            {
                ArticalModel am = new ArticalModel();
                am = new ArticalBUS().GetArticalByID(txtArticleID.Text);
                if (am != null)
                    isGroup = am.isGroup;
            }
            if(isGroup==true)
                numTotalReadOnly.Value = Convert.ToDecimal(numericPricePerArticle.Value) * Convert.ToDecimal(numericPricePerQuantity.Value) * Convert.ToDecimal(numNrArticle.Value);
            else
                numTotalReadOnly.Value = Convert.ToDecimal(numericPricePerArticle.Value) * Convert.ToDecimal(numericPricePerQuantity.Value);
            numericTotal.Value = Convert.ToDecimal(numTotalReadOnly.Value) - (Convert.ToDecimal(numTotalReadOnly.Value) * Convert.ToDecimal(numCommission.Value) / 100);
            numCommissionPrice.Value = (Convert.ToDecimal(numTotalReadOnly.Value) * Convert.ToDecimal(numCommission.Value) / 100);
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

        private void numPricePerArticle_KeyDown(object sender, KeyEventArgs e)
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

        private void numNrArticle_ValueChanged(object sender, EventArgs e)
        {
            recalculatePrice();
        }
    }
}
