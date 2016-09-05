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
using Telerik.WinControls.UI;
using System.IO;

namespace GUI
{
    public partial class frmPriceList : frmTemplate
    {
        PriceListModel pricelistModel;
        int clientID = -1;
        PriceListArticlesBUS pb = new PriceListArticlesBUS();
        int iId=-1;
        BindingList<PriceListArticlesModel> pam;
        
        ArrangementModel am;

        // Layout file names for all grids
        private string layoutPriceListArticles;
        private string layoutPriceListArticlesWithClient2;
        public Boolean isCopy = false;

        public frmPriceList(int clientid)
        {
            InitializeComponent();
            pricelistModel = new PriceListModel();
            this.clientID = clientid;
            btnSave.Click += SaveEvent;
        }
        public frmPriceList(PriceListModel model, int clientid)
        {

            InitializeComponent();
            pricelistModel = (PriceListModel)model;
            this.clientID = clientid;
            iId = model.idPriceList;
            btnArrangementID.Enabled = false;
            btnSave.Click += UpdateEvent;
            

        }


        private void SaveEvent(object sender, EventArgs e)
        {
                if (iId == -1 || isCopy == true)
                {
                    save();
                    if (isCopy == true)
                        isCopy = false;
                }
                else
                {
                    update();
                }
        }

        private Boolean save()
        {
            Boolean isSuccessfull = false;
            if (saveModel() == true)
            {
                if (iId == -1)
                {
                    pricelistModel.idUserCreated = Login._user.idUser;
                    pricelistModel.dtUserCreated = DateTime.Now;
                    pricelistModel.idHotelService = (int)dropdownHotelService.SelectedValue;
                    int id = new PriceListBUS().Save(pricelistModel, this.Name, Login._user.idUser);
                    if (id != 0)
                    {
                        iId = id;
                        pricelistModel.idPriceList = id;
                        for (int i = 0; i < pam.Count;i++ )
                        {
                            PriceListArticlesModel pm = new PriceListArticlesModel();
                            pam[i].idPriceList = id;
                            pam[i].idUserCreated = Login._user.idUser;
                            pam[i].dtUserCreated = DateTime.Now;
                            pm = (PriceListArticlesModel)pam[i];
                            int num = new PriceListArticlesBUS().Save(pm, this.Name, Login._user.idUser);
                            if (num <= 0)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("Something went wrong with inserting", pm.nameArtical);
                               
                            }
                            else 
                                pam[i].idPriceListArticle = num;
                        }
                        
                        rgvArticles.DataSource = null;
                        rgvArticles.DataSource = pam;
                        
                        translateRadMessageBox trr = new translateRadMessageBox();
                        trr.translateAllMessageBox("You have successfully insert data!");
                        isSuccessfull = true;
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong with inserting!");
                    }
                }
                else
                    isSuccessfull =  true;
            }
            return isSuccessfull;
        }

        private void UpdateEvent(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            if (saveModel() == true && isCopy==false)
            {
                pricelistModel.idUserModified = Login._user.idUser;
                pricelistModel.dtUserModified = DateTime.Now;
                pricelistModel.idHotelService = (int)dropdownHotelService.SelectedValue;

                if (new PriceListBUS().Update(pricelistModel, this.Name, Login._user.idUser) == true)
                {
                    for(int i = 0;i<pam.Count;i++)
                    {
                        PriceListArticlesModel pm = new PriceListArticlesModel();
                        pm = (PriceListArticlesModel)pam[i];
                        if (pm.idPriceListArticle != 0)
                        {
                            if (new PriceListArticlesBUS().GetArticleByPriceListArticle(pm.idPriceListArticle, pm.idPriceList) == true)
                            {
                                if (new PriceListArticlesBUS().Update(pm, this.Name, Login._user.idUser) == false)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translatePartAndNonTranslatedPart("Something went wrong with updating", pm.nameArtical);
                                }
                            }
                            else
                            {
                                int num = new PriceListArticlesBUS().Save(pm, this.Name, Login._user.idUser);
                                if (num <= 0)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translatePartAndNonTranslatedPart("Something went wrong with inserting", pm.nameArtical);

                                }
                                else
                                    pam[i].idPriceListArticle = num;
                            }
                        }
                        else
                        {
                            int num = new PriceListArticlesBUS().Save(pm, this.Name, Login._user.idUser);
                            if (num<=0)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("Something went wrong with inserting",pm.nameArtical);
                               
                            }
                            else
                                pam[i].idPriceListArticle = num;
                        }
                    }
                    

                    translateRadMessageBox trr = new translateRadMessageBox();
                    trr.translateAllMessageBox("You have successfully update data!");
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Something went wrong with updating!");
                }
            }
        }

        private bool saveModel()
        {
            if (CheckInputs() == true)
            {
                //pricelistModel.isActive = chkActive.Checked;
                pricelistModel.isReleaseDate = chkReleaseDate.Checked;
                
                if(dtPriceList.Visible == true)
                    pricelistModel.dtPriceList = dtPriceList.Value;

                pricelistModel.idClient = clientID;
                return true;
            }
            else
                return false;
        }

        public bool CheckInputs()
        {
            if (pricelistModel.idArrangement == null || pricelistModel.idArrangement <= 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You must add arrangement");
                return false;
            }
            else
            {
                return true;
            }

        }

        private void frmPriceList_Load(object sender, EventArgs e)
        {
            layoutPriceListArticles = MainForm.gridFiltersFolder + "\\layoutPriceListArticles.xml";

            layoutPriceListArticlesWithClient2 = MainForm.gridFiltersFolder + "\\layoutPriceListArticlesWithClient2.xml";

            radRibbonDocuments.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonContact.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonTask.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
            radRibbonReports.Visibility = ElementVisibility.Collapsed;
            dtPriceList.Value = DateTime.Now;
            pam = new BindingList<PriceListArticlesModel>();
            
            am = new ArrangementModel();

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


            HotelServicesBUS hotelBUS = new HotelServicesBUS();
            List<HotelServicesModel> hotelServiceList = new List<HotelServicesModel>();
            hotelServiceList = hotelBUS.GetAllHotelServicesDropDown();

            if(hotelServiceList != null)
            {
                dropdownHotelService.DataSource = hotelServiceList;
                dropdownHotelService.ValueMember = "idHotelService";
                dropdownHotelService.DisplayMember = "nameHotelService";
            }

            setTranslation();
            if (iId != -1)
            {
                ReadModel();
            }


            List<PriceListArticlesModel> tmplist = new List<PriceListArticlesModel>();
            tmplist = pb.GetAllArticlesByPriceList(pricelistModel.idPriceList);
            if (tmplist != null)
                pam = new BindingList<PriceListArticlesModel>(tmplist);
            rgvArticles.DataSource = pam;

            
            if (isCopy == true)
            {
                iId = -1;
                btnSave.Click += SaveEvent;
                btnArrangementID.Enabled = true;
            }
        }

        public void ReadModel()
        {
            if (isCopy != true)
            {
                 am =  new ArrangementBUS().GetArrangementById(pricelistModel.idArrangement);
                setArrangmentLabels(am);
            }
            //if (pricelistModel.isActive == true)
            //    chkActive.CheckState = CheckState.Checked;
            //else
            //    chkActive.CheckState = CheckState.Unchecked;
            dtPriceList.Value = pricelistModel.dtPriceList;
            dropdownHotelService.SelectedValue = pricelistModel.idHotelService;

            if (pricelistModel.isReleaseDate == true)
                chkReleaseDate.CheckState = CheckState.Checked;
            else
                chkReleaseDate.CheckState = CheckState.Unchecked;

            
        }


        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblArrId.Text) != null)
                    lblArrId.Text = resxSet.GetString(lblArrId.Text);
                if (resxSet.GetString(lblArrCode.Text) != null)
                    lblArrCode.Text = resxSet.GetString(lblArrCode.Text);
                if (resxSet.GetString(lblArrName.Text) != null)
                    lblArrName.Text = resxSet.GetString(lblArrName.Text);
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                if (resxSet.GetString(lblCity.Text) != null)
                    lblCity.Text = resxSet.GetString(lblCity.Text);
                if (resxSet.GetString(lblCountry.Text) != null)
                    lblCountry.Text = resxSet.GetString(lblCountry.Text);
                if (resxSet.GetString(lblArrType.Text) != null)
                    lblArrType.Text = resxSet.GetString(lblArrType.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(lblNrTravelers.Text) != null)
                    lblNrTravelers.Text = resxSet.GetString(lblNrTravelers.Text);
                if (resxSet.GetString(lblMinNrTravelers.Text) != null)
                    lblMinNrTravelers.Text = resxSet.GetString(lblMinNrTravelers.Text);
                if (resxSet.GetString(lblNrVoluntaryHelpers.Text) != null)
                    lblNrVoluntaryHelpers.Text = resxSet.GetString(lblNrVoluntaryHelpers.Text);
                if (resxSet.GetString(lblTotal.Text) != null)
                    lblTotal.Text = resxSet.GetString(lblTotal.Text);
                if (resxSet.GetString(chkActive.Text) != null)
                    chkActive.Text = resxSet.GetString(chkActive.Text);
                if (resxSet.GetString(lbldtPriceList.Text) != null)
                    lbldtPriceList.Text = resxSet.GetString(lbldtPriceList.Text);
                if (resxSet.GetString(radMenuItemSaveArticles.Text) != null)
                    radMenuItemSaveArticles.Text = resxSet.GetString(radMenuItemSaveArticles.Text);                
                if (resxSet.GetString(btnAddArticle.Text) != null)
                    btnAddArticle.Text = resxSet.GetString(btnAddArticle.Text);
                if (resxSet.GetString(lblHotelService.Text) != null)
                    lblHotelService.Text = resxSet.GetString(lblHotelService.Text);

                
                
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
                
            }
        }

        private void btnArrangementID_Click(object sender, EventArgs e)
        {
            if (pricelistModel != null)
            {
                List<IModel> gm1 = new List<IModel>();

                gm1 = new ArrangementBUS().GetAllArrangementsNotInActiveContracts(clientID);
                var dlgSave = new GridLookupForm(gm1, "Arrangements");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    ArrangementModel genm1 = new ArrangementModel();
                    genm1 = (ArrangementModel)dlgSave.selectedRow;
                    //set textbox
                    setArrangmentLabels(genm1);
                    pricelistModel.idArrangement = genm1.idArrangement;
                    am = genm1;
                    if(isCopy==true)
                    {
                        pricelistModel.idPriceList = 0;
                        pricelistModel.idArrangement = genm1.idArrangement;

                        for(int i = 0 ;i<pam.Count;i++)
                        {
                            pam[i].idPriceList = 0;
                            pam[i].idPriceListArticle = 0;
                            pam[i].idUserCreated = 0;
                            pam[i].nameUserCreated = String.Empty;
                            pam[i].dtUserCreated = DateTime.Now;
                            pam[i].idUserModified = 0;
                            pam[i].nameUserModified = String.Empty;
                            pam[i].dtUserModified = DateTime.Now;
                            //Aleksa i Mitar (Vreme na artiklima se ne poklapa kod copy komande)
                            pam[i].DtFrom = genm1.dtFromArrangement;
                            pam[i].DtTo = genm1.dtToArrangement;
                            //Aleksa I Mitar
                        }                        
                    }
                }
            }
        }

        private void setArrangmentLabels(ArrangementModel genm1)
        {
            lblIdArrangement.Text = genm1.idArrangement.ToString();
            lblCodeArrangement.Text = genm1.codeArrangement.ToString();
            lblNameArrangement.Text = genm1.nameArrangement;
            if (genm1.dtFromArrangement.ToString() != "")
                lblDtFrom.Text = genm1.dtFromArrangement.ToString();
            if (genm1.dtToArrangement.ToString() != "")
                lblDtTo.Text = genm1.dtToArrangement.ToString();
            if (genm1.cityArrangement.ToString() != "")
                lblPlace.Text = genm1.cityArrangement.ToString();
            if (genm1.countryNameArrangement.ToString() != "")
                lblCountryArrangement.Text = genm1.countryNameArrangement.ToString();
            if (genm1.typeNameArrangement.ToString() != "")
                lblTypeArrangement.Text = genm1.typeNameArrangement.ToString();
            if (genm1.typeNameArrangement.ToString() != "")
                lblTypeArrangement.Text = genm1.typeNameArrangement.ToString();
            if (genm1.nrTraveler.ToString() != "")
                lblNumberTravelers.Text = genm1.nrTraveler.ToString();
            else
                lblNumberTravelers.Text = "0";
            if (genm1.minNrTraveler.ToString() != "")
                lblMinNumberTravelers.Text = genm1.minNrTraveler.ToString();
            else
                lblMinNumberTravelers.Text = "0";
            if (genm1.nrVoluntaryHelper.ToString() != "")
                lblVoluntaryHelperNumbers.Text = genm1.nrVoluntaryHelper.ToString();
            else
                lblVoluntaryHelperNumbers.Text = "0";
            lblNrTotal.Text = (Convert.ToInt32(lblNumberTravelers.Text) + Convert.ToInt32(lblVoluntaryHelperNumbers.Text)).ToString();
         
           

        }

        private void btnAddArticles_Click(object sender, EventArgs e)
        {
            if (pricelistModel.idArrangement == null || pricelistModel.idArrangement == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you have to choose arrangement.");
            }
            else
            {
                frmPriceListArticles frm = new frmPriceListArticles(pricelistModel.idPriceList,am,false);
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    pam.Add(frm.pricelistModel);
            }
        }

       
        private void rgvArticles_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if(e.Row.DataBoundItem!=null)
            {               
                PriceListArticlesModel model = (PriceListArticlesModel)e.Row.DataBoundItem;
                PriceListArticlesModel model1 = new PriceListArticlesModel((PriceListArticlesModel)e.Row.DataBoundItem);
                Boolean isDescriptionArticle = false;
                if (model.PricePerArticle == 0 && model.PricePerQuantity == 0 )
                    isDescriptionArticle = true;
                else
                    isDescriptionArticle = false;
                frmPriceListArticles frm = new frmPriceListArticles(model1, am, isDescriptionArticle);
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    //model = model1;
                    pam[e.RowIndex] = model1;
                }

                rgvArticles.DataSource = null;
                rgvArticles.DataSource = pam;
                
            }
        }

        

        private void rgvArticles_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutPriceListArticles))
            {
                rgvArticles.LoadLayout(layoutPriceListArticles);
            }
            if (rgvArticles != null)
            {
                if (rgvArticles.Columns.Count > 0)
                {
                    for (int i = 0; i < rgvArticles.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (rgvArticles.Columns[i].HeaderText != null && resxSet.GetString(rgvArticles.Columns[i].HeaderText) != null)
                                rgvArticles.Columns[i].HeaderText = resxSet.GetString(rgvArticles.Columns[i].HeaderText);
                        }
                    }
                }
            }
        }

       

        private void chkActive_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            RadCheckBox rchk = (RadCheckBox)sender;
            if(rchk.CheckState==CheckState.Unchecked)
            {
                if(new ArrangementBUS().checkIfArrangement(clientID,pricelistModel.idArrangement,pricelistModel.idPriceList)==true)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You already have active contract for this arrangement.");
                    args.Cancel=true;
                }
            }
        }

        private void radMenuItemSaveArticles_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutPriceListArticles))
            {
                File.Delete(layoutPriceListArticles);
            }
            rgvArticles.SaveLayout(layoutPriceListArticles);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

       

        private void btnAddArticle_Click(object sender, EventArgs e)
        {
            frmArticle frm = new frmArticle();
            frm.ShowDialog();
        }

        private void btnDeleteArticles_Click(object sender, EventArgs e)
        {
            if (rgvArticles != null)
            {
                if (rgvArticles.SelectedRows != null)
                {
                    if (rgvArticles.SelectedRows.Count > 0)
                    {
                        PriceListBUS pb = new PriceListBUS();
                        int idPL = 0;
                        if(rgvArticles.SelectedRows[0].Cells["idPriceList"]!=null)
                        if(rgvArticles.SelectedRows[0].Cells["idPriceList"].Value!=null)
                        idPL = Convert.ToInt32(rgvArticles.SelectedRows[0].Cells["idPriceList"].Value.ToString());
                        string idA = null;
                        if(rgvArticles.SelectedRows[0].Cells["idArticle"]!=null)
                        if(rgvArticles.SelectedRows[0].Cells["idArticle"].Value!=null)
                            idA = rgvArticles.SelectedRows[0].Cells["idArticle"].Value.ToString();
                        if(idPL!=0 && idA!=null)
                        {
                            if (pb.checkForDeleteArticle(idPL,idA) <= 0)
                            {
                                if (pb.DeleteArticle(idPL, idA, this.Name, Login._user.idUser) == true)
                                {
                                    PriceListArticlesModel pp = new PriceListArticlesModel();
                                    pp = pam.Where(s => s.idPriceList == Convert.ToInt32(rgvArticles.SelectedRows[0].Cells["idPriceList"].Value.ToString()) && s.IdArticle == rgvArticles.SelectedRows[0].Cells["idArticle"].Value.ToString()).FirstOrDefault();
                                    pam.Remove(pp);
                                    rgvArticles.DataSource = null;
                                    rgvArticles.DataSource = pam;
                                    rgvArticles.Show();
                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Probably you already have booked this article from contract please check!");
                            }
                        }
                         else
                            {
                                PriceListArticlesModel pp = new PriceListArticlesModel();
                                pp = pam.Where(s => s.idPriceList == Convert.ToInt32(rgvArticles.SelectedRows[0].Cells["idPriceList"].Value.ToString()) && s.IdArticle == rgvArticles.SelectedRows[0].Cells["idArticle"].Value.ToString()).FirstOrDefault();
                                pam.Remove(pp);
                                rgvArticles.DataSource = null;
                                rgvArticles.DataSource = pam;
                                rgvArticles.Show();

                                //translateRadMessageBox tr = new translateRadMessageBox();
                                //tr.translateAllMessageBox("Something is wrong with data. Please contact the administrator!");
                            }

                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have to have at least one article so you can delete them!");
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to have at least one article so you can delete them!");
                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to have at least article so you can delete them!");
            }
        }


        private void chkReleaseDate_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkReleaseDate.Checked == true)
            {
                dtPriceList.Visible = true;
            }
            else
            {
                dtPriceList.Visible = false;
            }
        }

        private void btnAddArticlesExtra_Click(object sender, EventArgs e)
        {
            if (pricelistModel.idArrangement == null || pricelistModel.idArrangement == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you have to choose arrangement.");
            }
            else
            {
                frmPriceListArticles frm = new frmPriceListArticles(pricelistModel.idPriceList, am, true);
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    pam.Add(frm.pricelistModel);
            }
        }


    }
}
