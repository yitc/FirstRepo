using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using BIS.Business;
using BIS.Model;

namespace GUI
{
    public partial class frmClientPayment : Telerik.WinControls.UI.RadForm
    {
        private string client;
        private string project;
        private string conto;
        private string layoutClipay;
        private string layoutConpay;
        private string layoutContract;
        private int idArr=0;
        private int idCli = 0;
        private int idArrange;


        public frmClientPayment(string uclient, string uproject, int idArr)
        {
            client = uclient;
            project = uproject;
            idArrange = idArr;
            if (client == null)
                client = "";
            if (project == null)
                project = "";
            if (idArr == null)
                idArrange = 0;

            InitializeComponent();
        }

        private void frmClientPayment_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;
            string name = "Client payment";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;


            layoutClipay = MainForm.gridFiltersFolder + "\\layoutClipay.xml";
            layoutConpay = MainForm.gridFiltersFolder + "\\layoutConpay.xml";
            layoutContract = MainForm.gridFiltersFolder + "\\layoutContract.xml";

            //Restriction for mouse wheel 
            txtCredit.MaskedEditBoxElement.EnableMouseWheel = false;
            txtTotal.MaskedEditBoxElement.EnableMouseWheel = false;
        

            Translation();

            AccSettingsBUS acsb = new AccSettingsBUS();
            AccSettingsModel acsm = new AccSettingsModel();
            acsm = acsb.GetSettingsByID(Login._bookyear);
            if (acsm != null)
            {
                conto = acsm.defCreditorAccount;
            }
            else
            {
                conto = "1600";
            }

            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> alm = new List<AccLineModel>();
            alm = alb.GetLinesByAccountAndCustomerAndProject(conto, client, project);

            gridPay.DataSource = null;
            gridPay.DataSource = alm;
            decimal suma = 0;

            if (alm != null && alm.Count > 0)
            {
                for (int q=0; q < alm.Count ; q++)
                {
                    suma = suma + (Convert.ToDecimal(alm[q].debitLine) - Convert.ToDecimal(alm[q].creditLine));
                }
            }


            ArrangementBUS aa = new ArrangementBUS();
            ArrangementModel am = new ArrangementModel();
            //am = aa.GetArrangementByCode(project);
            //if (am != null)
            //   idArr = am.idArrangement;
            //else
            //    idArr = 0;
            AccDebCreBUS dbb = new AccDebCreBUS();
            AccDebCreModel dbm = new AccDebCreModel();
            dbm=dbb.GetCustomerByAccCode(client);
            if (dbm != null)
                idCli = dbm.idClient;
            else
                idCli = 0;

            txtPurchase.Value = 0;
            ArrangementPriceBUS apb = new ArrangementPriceBUS();
            List<PriceListArticlesControlModel> apm = new List<PriceListArticlesControlModel>();

            apm = apb.GetAllArticalsModelNEW(idArrange, idCli);
            decimal prices = 0;
            if (apm != null && apm.Count > 0)
            { 
                    for (int w = 0; w < apm.Count;w++ )
                    {
                        prices = prices + Convert.ToDecimal(apm[w].priceTotal);
                            //Convert.ToDecimal(apm[w].nrArticle) * Convert.ToDecimal(apm[w].priceperarticle)*Convert.ToDecimal(apm[w].priceperquantity);
                    }

                    txtPurchase.Value = prices;
            }

            gridContract.DataSource = null;
            gridContract.DataSource = apm;

            decimal total = 0;
            total = prices - Math.Abs(suma) ;

            txtCredit.Value = suma;
            txtPurchase.Value = prices;
            txtTotal.Value = total;

        }
        private void Translation()
        {
              using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblCredit.Text) != null)
                    lblCredit.Text = resxSet.GetString(lblCredit.Text);

                if (resxSet.GetString(lblPurchase.Text) != null)
                    lblPurchase.Text = resxSet.GetString(lblPurchase.Text);

                if (resxSet.GetString(lblTotal.Text) != null)
                    lblTotal.Text = resxSet.GetString(lblTotal.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

              
              }
        }

        private void gridPay_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridPay != null)
            {
                if (gridPay.Columns.Count > 0)
                {
                    for (int i = 0; i < gridPay.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridPay.Columns[i].HeaderText != null && resxSet.GetString(gridPay.Columns[i].HeaderText) != null)
                                gridPay.Columns[i].HeaderText = resxSet.GetString(gridPay.Columns[i].HeaderText);
                        }
                    }

                    gridPay.Columns["idAccLine"].IsVisible = false;
                    gridPay.Columns["idAccDaily"].IsVisible = false;
                    gridPay.Columns["statusLine"].IsVisible = false;
                    gridPay.Columns["periodLine"].IsVisible = false;
                    gridPay.Columns["idPersonLine"].IsVisible = false;
                    gridPay.Columns["idBTW"].IsVisible = false;
                    gridPay.Columns["debitBTW"].IsVisible = false;
                    gridPay.Columns["creditBTW"].IsVisible = false;
                    gridPay.Columns["idCurrency"].IsVisible = false;
                    gridPay.Columns["debitCurr"].IsVisible = false;
                    gridPay.Columns["creditCurr"].IsVisible = false;
                    gridPay.Columns["debitCurr"].IsVisible = false;
                    gridPay.Columns["booksort"].IsVisible = false;
                    gridPay.Columns["currrate"].IsVisible = false;
                    gridPay.Columns["iban"].IsVisible = false;
                    gridPay.Columns["incopNr"].IsVisible = false;


                    if (gridPay.Columns["dtLine"].IsVisible == true)
                        //if (gridPay.Columns != null && gridPay.Columns.Count > 0)
                        gridPay.Columns["dtLine"].FormatString = "{0: dd/MM/yyyy}";
                    if (gridPay.Columns["dtBooking"].IsVisible == true)
                        //if (gridPay.Columns != null && gridPay.Columns.Count > 0)
                        gridPay.Columns["dtBooking"].FormatString = "{0: dd/MM/yyyy}";

                }
            }

            if (File.Exists(layoutClipay))
            {
                gridPay.LoadLayout(layoutClipay);
            }

        }

        private void gridPay_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
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

            //==delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutClyPay;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);


        }
        private void SaveLayoutClyPay(object sender, EventArgs e)
        {
            if (File.Exists(layoutClipay))
            {
                File.Delete(layoutClipay);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");


        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutClipay))
            {
                File.Delete(layoutClipay);
            }
            gridPay.SaveLayout(layoutClipay);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridContract_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (gridContract != null)
            {
                if (gridContract.Columns.Count > 0)
                {
                    for (int i = 0; i < gridContract.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridContract.Columns[i].HeaderText != null && resxSet.GetString(gridContract.Columns[i].HeaderText) != null)
                                gridContract.Columns[i].HeaderText = resxSet.GetString(gridContract.Columns[i].HeaderText);
                        }
                    }

                    //gridPay.Columns["idCreditPay"].IsVisible = false;
                    //gridPay.Columns["idClient"].IsVisible = false;
                    //gridPay.Columns["idContPers"].IsVisible = false;
                    //gridPay.Columns["isApproved"].IsVisible = false;
                    //gridPay.Columns["isBooked"].IsVisible = false;
                    //gridPay.Columns["isSent"].IsVisible = false;
                    //gridPay.Columns["dtSent"].IsVisible = false;
                    //gridPay.Columns["namefile"].IsVisible = false;
                    //gridPay.Columns["approvedUser"].IsVisible = false;
                    //gridPay.Columns["createUser"].IsVisible = false;
                    //gridPay.Columns["dtCreation"].IsVisible = false;
                    //gridPay.Columns["payIban"].IsVisible = false;
                    //gridPay.Columns["isSelected"].IsVisible = false;

                }
            }

            if (File.Exists(layoutContract))
            {
                gridContract.LoadLayout(layoutContract);
            }

        }

        private void gridContract_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutD;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);


            //==delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutContract;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }
        private void SaveLayoutD(object sender, EventArgs e)
        {
            if (File.Exists(layoutContract))
            {
                File.Delete(layoutContract);
            }
            gridContract.SaveLayout(layoutContract);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void MasterTemplate_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void gridContract_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void SaveLayoutContract(object sender, EventArgs e)
        {
            if (File.Exists(layoutContract))
            {
                File.Delete(layoutContract);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");


        }

        private void txtCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void txtTotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

    }
}
