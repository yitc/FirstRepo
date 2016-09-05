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
using System.Linq;
using Telerik.Windows;
using BIS.Core;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using Telerik.WinControls.Data;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;





namespace GUI
{
    public partial class frmInvoice : Telerik.WinControls.UI.RadForm
    {

        public InvoiceModel model;
        public string layoutInvoice;
        private string codeD;
        public List<InvoiceItemsModel> iim;
        //customer account
        private string customer = "";
        private string nameFileToSend;
        //has account or not
        //private bool isNop = false;
        // ====

        // idTraveler je idCOntPerson sa ArrangementBookPErson zato sto u Invoiceu idContPerson moze da bude debitor
        int idTraveler = 0;

        ArrangementBUS arangeBUS;
        ArrangementBookBUS arangeBookBUS;
        private List<InvoiceItemsModel> iim1;
        private InvoiceBUS inbus;
        public ArrangementModel am;
        List<InvoiceItemsModel> secondItems;
        PersonModel Person;
        BindingList<ArrangementTravelersInvoiceModel> travelmod;
       // List<ArrangementBookModel> travelmod;
        ArrangementBookModel abm;


        private int idLabel = 0;
        private bool isMessage = true;
        public string attachPDFname = "";
        private int qol = 0;
        private bool isInvoiceMaked = false;
        private bool isLoaded = false;

        public frmInvoice(InvoiceModel imodel)
        {
            model = new InvoiceModel();
            model = imodel;
            InitializeComponent();

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            formName = formName + " " + this.Text;
            this.Text = formName;
        }
      
        private void frmInvoice_Load(object sender, EventArgs e)
        {
            txtDaily.Visible = false;
           
            // for buttons
            if (model.idInvoiceStatus == 0)
            {
                btnBooking.Visible = false;
                btnPrint.Visible = false;
                btnSendEmail.Visible = false;
                btnDaily.Visible = false;
            }

            if (model.idInvoiceStatus >= 2)
            {
                btnInvoice.Enabled = false;
                btnMake.Enabled = false;
                gridItems.AllowDeleteRow = false;
                btnAddArtical.Enabled = false;
                btnDaily.Visible = false;
            }
            else
            {
                if (model.idInvoiceStatus < 2)
                {
                    btnBooking.Enabled = false;
                    txtDaily.Visible = false;
                    btnDaily.Visible = false;
                    btnBooking.Visible = false;
                   
                }
            }
            if (model.idInvoiceStatus >= 4)
            {
                btnBooking.Enabled = false;
                btnBooking.Visible = false;
                btnDaily.Visible = false;
            }


            //foreach (Control c in this.Controls)
            //{
            //    //if(c.GetType()== )
            //    //{
            //    //CultureInfo ci = CultureInfo.CurrentCulture;
            //    //txtReservationCost.Culture = ci;
            //    //}
            //}

            arangeBookBUS = new ArrangementBookBUS();
            abm = new ArrangementBookModel();
            arangeBUS = new ArrangementBUS();

            int voucher = 0;
            if (model.idVoucher != null && model.idVoucher != 0)
            {
                voucher = Convert.ToInt32(model.idVoucher);
                txtVoucher.Text = model.idVoucher.ToString() + "/" + model.idContPerson.ToString();
            }
            abm = arangeBookBUS.GetArrangementBook(voucher);
            if (abm == null)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Can't read Arrangement");
                return;
            }

           
            am = new ArrangementModel();
            am = arangeBUS.GetArrangementById(abm.idArrangement);

           

            ArrangementCalculationBUS arcb = new ArrangementCalculationBUS();
            ArrangementCalculationModel arcm = new ArrangementCalculationModel();

            decimal calamitait = 0;
            decimal moneyGroup = 0;


            arcm = arcb.GetArrangementCalculation(am.idArrangement);
            if (arcm != null)
            {
                moneyGroup = Convert.ToDecimal(arcm.moneyGroup);
                calamitait = Convert.ToDecimal(arcm.calamiteitenFonds);
            }



            ImageDB image = new ImageDB();
            Image img = image.setImage(Login._companyModelList[0].logoCompany);
            int aa = Convert.ToInt32(model.idContPerson);

            #region Person(address, account)
            

            if (am != null)
                if (am.idClientInvoice != 0)
                {
                   
                    getClientAddressAndNumberAccount(am.idClientInvoice);
                }
                else if (model.idContPerson == 0 && model.idClient > 0)
                {
                    getClientAddressAndNumberAccount((int)model.idClient);
                }
                else
                    getPersonAddressAndNumberAccount(aa);
            else
                getPersonAddressAndNumberAccount(aa);
            

            #endregion Person


            if (model.invoiceNr != null)
                txtInvoice.Text = model.invoiceNr.ToString();
            if (model.invoiceRbr != null)
                txtInvoiceRbr.Text = model.invoiceRbr.ToString();
            if (model.dtCreated != null)
                // dpInvoice.Text = model.dtInvoice.ToString();
                dpInvoice.Value = Convert.ToDateTime(model.dtInvoice);
            if (model.idVoucher != null)
                txtVoucher.Text = model.idVoucher.ToString() + "/" + model.idContPerson.ToString();
            if (model.netoAmount != null)
                txtBrutoAmt.Text = model.netoAmount.ToString();

           
            if (model.dtFirstPay != null)
               
                    dpFirstpay.Text = model.dtFirstPay.ToString();
            if (model.dtLastPay != null)
                    txtLastPayment.Text = model.dtLastPay.ToString();
            if (model.percentFrstPay != null && model.percentFrstPay != 0)
                txtFrstPayPercent.Text = model.percentFrstPay.ToString();
            if (model.noteInvoice != null && model.noteInvoice != "")
                txtText.Text = model.noteInvoice.ToString();
            if (model.firstreferencePay != null)
                txtfirstreference.Text = model.firstreferencePay.ToString();
            if (model.secondreferencePay != null)
                txtSecondreference.Text = model.secondreferencePay.ToString();
            if (model.roomComment != null)
                if (model.roomComment != "")
                    txtRoomComment.Text = model.roomComment;

            ddlStatus.DataSource = new InvoiceStatusBUS().GetAllInvoiceStatus();
            ddlStatus.DisplayMember = "descInvoiceStatus";
            ddlStatus.ValueMember = "idInvoiceStatus";

            if (txtInvoiceRbr.Text.Trim() == "999")
            {
                lbltext2.Text = "Hierbij bevestigen wij voor u onderstaande te hebben geannuleerd:";
            }
            else
            {
                lbltext2.Text = "Hierbij bevestigen wij voor u onderstaande te hebben gereserveerd:";
            }

            // pitaj zasto
            btnMake.Enabled = false;


            //first payment
            if (model.invoiceRbr == "000")          // ako je first payment cant do edit
            {
                btnAddArtical.Enabled = false;
                gridItems.AllowEditRow = false;
                txtText.Text = "";
                btnInvoice.Enabled = false;
            }

            //second part of invoice
            if (model.invoiceRbr == "001")
            {
                btnMake.Enabled = true;
                InvoiceModel ikm000 = new InvoiceModel();
                InvoiceBUS ikb000 = new InvoiceBUS();
                ikm000 = ikb000.GetInvoiceByInvoiceAndExtension(model.invoiceNr, "000");
                if (ikm000 != null)
                {
                    if (ikm000.idInvoice != null && ikm000.idInvoice != 0)
                    {
                        btnMake.Enabled = false;
                    }
                }

            }

            nameFileToSend = "Factuur_" + txtInvoice.Text.Trim() + "-" + txtInvoiceRbr.Text.Trim() + ".pdf";

            InvoiceItemsBUS iib = new InvoiceItemsBUS();
            List<InvoiceItemsModel> iim = new List<InvoiceItemsModel>();
            iim = iib.GetInvoiceItemsByInvoice(model.invoiceNr, Login._user.lngUser);
            if (iim == null)
                iim = new List<InvoiceItemsModel>();
            gridItems.DataSource = null;
            gridItems.DataSource = iim;

            chkBooking();

            #region Arrangement (Boarding point, label)

           
           

            // boarding point
            if (abm.idBoarding != null && abm.idBoarding != 0)
            {
                ArrangementBoardingPointBUS arbb = new ArrangementBoardingPointBUS();
                ArrangementBoardingPointModel arbm = new ArrangementBoardingPointModel();
                txtBoarding.Text = arbb.GetBoardingPointName(abm.idBoarding);
            }
            // boarding point

           
            if (am != null)
            {
                int days = Convert.ToInt32((am.dtToArrangement - am.dtFromArrangement).TotalDays + 1);
                txtNoDays.Text = days.ToString();
                txtDateFrom.Text = am.dtFromArrangement.ToShortDateString();
                txtDateTo.Text = am.dtToArrangement.ToShortDateString();
                txtNameArr.Text = am.nameArrangement;
                txtHotelService.Text = am.nameHotelService;
                //codeArr = am.codeArrangement;
                //IDArrangement = am.idArrangement;
                if (model.idInvoiceStatus != null && model.idInvoiceStatus != 0)
                    ddlStatus.SelectedValue = model.idInvoiceStatus;

                txtHotelService.Text = new HotelServicesBUS().GetHotelServicesById(am.idHotelService).nameHotelService;
                DateTime daysPay = am.dtFromArrangement.AddDays(-Convert.ToInt32(am.daysLastPayment));
                if (model.dtLastPay == null || model.dtLastPay == Convert.ToDateTime("1999-01-01"))
                {
                    txtLastPayment.Text = daysPay.ToShortDateString();
                }
                else
                {
                    txtLastPayment.Text = model.dtLastPay.ToString();
                }
                int dtDifferenceDtFromDtInvoice = 0;
                    if (am.dtFromArrangement != null)
                        dtDifferenceDtFromDtInvoice = Convert.ToInt32((am.dtFromArrangement - DateTime.Now).TotalDays);
                DateTime daysFirst = Convert.ToDateTime(model.dtCreated).AddDays(Convert.ToInt32(am.daysFirstPayment));
                if (model.dtFirstPay == null || model.dtFirstPay == Convert.ToDateTime("1999-01-01"))
                {
                    
                    //if (dtDifferenceDtFromDtInvoice < 42)
                    if (dtDifferenceDtFromDtInvoice < am.daysLastPayment)
                        dpFirstpay.Text = model.dtInvoice.ToString();
                    else
                        dpFirstpay.Text = daysFirst.ToShortDateString();
                }
                else
                {
                    //if (dtDifferenceDtFromDtInvoice < 42)
                    if (dtDifferenceDtFromDtInvoice < am.daysLastPayment)
                        dpFirstpay.Text = model.dtInvoice.ToString();
                    else
                        dpFirstpay.Text = model.dtFirstPay.ToString();
                }
                if (model.percentFrstPay != Convert.ToDecimal(txtFrstPayPercent.Text))
                    txtFrstPayPercent.Value = Convert.ToDecimal(model.percentFrstPay);

            }

            ArrangementBUS arbs = new ArrangementBUS();
            List<LabelForArrangement> lbm = new List<LabelForArrangement>();
            if (am.idArrangement != 0 && am.idArrangement != null)
            {
                lbm = arbs.GetLabelsArrangement(am.idArrangement);
                if (lbm != null)
                {
                    if (lbm.Count > 0)
                        idLabel = lbm[0].idLabel;
                }
            }
            #endregion Arrangement


            #region Travelers
            ArrangementBookPersonsBUS bus = new ArrangementBookPersonsBUS();
            // ArrangementBookBUS bus1 = new ArrangementBookBUS();
            //travelmod = new BindingList<ArrangementTravelersModel>();
            travelmod = new BindingList<ArrangementTravelersInvoiceModel>();
            travelmod = bus.GetAllTravelersInvoicing( abm.idArrangementBook, true);
            //travelmod = bus1.GetPassingersForInvoicing(abm.idArrangementBook,Convert.ToInt32(model.idContPerson);

            if (travelmod == null)
                //  travelmod = new BindingList<ArrangementTravelersModel>();
                travelmod = new BindingList<ArrangementTravelersInvoiceModel>();

            if (travelmod.Count > 0)
                idTraveler = travelmod[0].idContPers;

            //========== ovo je visak jer se vec u gornjem upitu ucitava i platilac fakture =======================
            //ArrangementTravelersModel aitm = new ArrangementTravelersModel();
            //aitm.firstnameTraveler = perm.firstname;
            //aitm.lastnameTraveler = perm.lastname;
            //aitm.birthdate = perm.birthdate;
            //aitm.idTravelWithPerson = perm.idContPers;
            //     travelmod.Add(aitm);

            txtText.Text = "";

            string rowOne = "";
            string rowTwo = "";
            string rowThree = "Uw contractnummer bij de Europeesche Verzekering is 406/415/453.3429840";
            txtText.Text = Environment.NewLine + @"Op deze overeenkomst zijn de ANVR-Reisvoorwaarden en de garantie van het Calamiteitenfonds van toepassing"
             + @". Op deze overeenkomst is tevens de SGR-garantieregeling van toepassing. U kunt de voorwaarden vinden op www.sgr.nl/garantieregeling"
             + @". Op verzoek stuurt SGR deze voorwaarden kosteloos toe"
            + @". Op onze facturen is de bijzondere regeling reisbureaus van toepassing.";


            // trazi isurance i cancel insurance za ljude u malom gridu
            ArrangementBookBUS apb = new ArrangementBookBUS();
            ArrangementBookModel apm = new ArrangementBookModel();
            for (int w = 0; w < travelmod.Count; w++)
            {
                apm = new ArrangementBookModel();
                apm = apb.GetTravelerIsInsurance(abm.idArrangement, travelmod[w].idContPers);  //travelmod[w].idTravelWithPerson
                if (apm != null)
                {
                    if (apm.isInsurance == true)
                    {
                        if (rowOne == "")
                            rowOne = "De reisverzekering is afgesloten t.b.v. ";
                        if (rowOne != "De reisverzekering is afgesloten t.b.v. ")
                            rowOne = rowOne + " en " + apm.firstname + " " + apm.midname + " " + apm.lastname;
                        else
                            rowOne = rowOne + apm.firstname + " " + apm.midname + " " + apm.lastname;
                    }
                    if (apm.isCancelInsurance == true)
                    {
                        if (rowTwo == "")
                            rowTwo = "De annuleringsverzekering is afgesloten t.b.v. ";
                        if (rowTwo != "De annuleringsverzekering is afgesloten t.b.v. ")
                            rowTwo = rowTwo + " en " + apm.firstname + " " + apm.midname + " " + apm.lastname;
                        else
                            rowTwo = rowTwo + apm.firstname + " " + apm.midname + " " + apm.lastname;
                    }
                }
            }

            if (rowOne != "" && rowTwo != "")
                txtText.Text = rowOne + ". " + rowTwo + ". " + rowThree + Environment.NewLine + txtText.Text;
            else if (rowOne != "" && rowTwo == "")
                txtText.Text = rowOne + ". " + rowThree + Environment.NewLine + txtText.Text;
            else if (rowTwo != "" && rowOne == "")
                txtText.Text = rowTwo + Environment.NewLine + txtText.Text;

            //================

            if (model.noteInvoice == "")
            {
                if (txtText.Text != "")
                    model.noteInvoice = txtText.Text;
            }
            else
                txtText.Text = model.noteInvoice;


            rgvTravelWith.DataSource = travelmod;

            rgvTravelWith.Columns["idArrangementBook"].IsVisible = false;
            rgvTravelWith.Columns["idArrangement"].IsVisible = false;
            rgvTravelWith.Columns["idContPers"].IsVisible = false;
            rgvTravelWith.Columns["idPayInvoice"].IsVisible = false;
            rgvTravelWith.Columns["fullname"].IsVisible = false;
            rgvTravelWith.Columns["passportname"].Width = 350;
            rgvTravelWith.Columns["firstnameTraveler"].IsVisible = false;
            rgvTravelWith.Columns["lastnameTraveler"].IsVisible = false;
            rgvTravelWith.Columns["birthdate"].IsVisible = true;
            rgvTravelWith.Columns["birthdate"].Width = 180;
            //==============================

            #endregion Travelers





            gridItems.DataSource = null;

            iib = new InvoiceItemsBUS();
            iim1 = new List<InvoiceItemsModel>();
            iim1 = iib.GetInvoiceItemsByInvoice(model.idInvoice.ToString(), Login._user.lngUser);       //  iib.GetInvoiceItemsByID(model.idInvoice);

            List<InvoiceItemsModel> iimm = new  List<InvoiceItemsModel>();
            if (iim1 != null)
               iimm = iim1.FindAll(iimelement => iimelement.idArtical == "Insurance");
            //=========================================== ovo parce koda je ubaceno da procita nazive Raispaketa i insuranca iz ArangeInvoicePrice
            string descReis = "";
            string descInsur = "";
            ArrangementInvoicePriceBUS aip = new ArrangementInvoicePriceBUS();
            List<ArrangementInvoicePriceModel> aim = new List<ArrangementInvoicePriceModel>();
            aim = aip.GetInvoicePrice(am.idArrangement);
            if (aim != null)
            {
                if (aim.Count > 0)
                    for (int q = 0; q < aim.Count; q++)
                    {
                        if (aim[q].idArticle == "Reis Pakket")
                            descReis = aim[q].descriptionArticle;
                        else
                            if (aim[q].idArticle == "Insurance")
                                descInsur = aim[q].descriptionArticle;
                    }
            }
            //=====================================================================
            if (iim1 != null)
            {
                if (iim1.Count > 0)
                    for (int w = 0; w < iim1.Count; w++)
                    {
                        if (iim1[w].idArtical == "Reis Pakket")
                        {
                            if (descReis != "")
                            {
                                iim1[w].nameArtical = descReis;
                            }
                        }
                        if (iim1[w].idArtical == "Insurance")
                            if (descInsur != "")
                                iim1[w].nameArtical = descInsur;

                    }
            }
            //============================================
            if (iimm != null)
            {
                    ArrangementTravelInsuranceModel atim = new ArrangementTravelInsuranceModel();
                    CountryModel cm = new CountryModel();
                    cm = new CountryBUS().GetCountryByID(am.countryArrangement);
                    ArrangementBookModel abmm = new ArrangementBookModel();
                    abmm = new ArrangementBookBUS().GetArrangementBook(Convert.ToInt32(model.idVoucher));
                    for (int j = 0; j < iimm.Count; j++)
                    {
                        if (arcm != null && cm != null && abmm != null)
                        {
                            atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, arcm.isSport, iimm[j].isMedical);
                            if (atim != null)
                                iimm[j].nameArtical = atim.description;
                        }
                    }
            }

            // qol = 0;
            if (model.idInvoiceStatus == 1 && btnMake.Enabled == true)
            {
                qol = travelmod.Count;
                for (int r = 0; r < iim1.Count; r++)
                {
                   // iim1[r].quantity = qol;
                }
            }
            gridItems.DataSource = iim1;
            cancelInsurance();
            qol = travelmod.Count; // ovde ubacuje pponovo da bi izbegao 0 vrednost kad ucitava ponovo fakturu

            //for status control enable/disable
            disableAllIfStatusIsntInProccessing(Convert.ToInt32(model.idInvoiceStatus));
            isLoaded = true;

            setLastPayDay();

        }

        private void getPersonAddressAndNumberAccount(int aa)
        {
            PersonBUS perb = new PersonBUS();
            PersonModel perm = new PersonModel();
            perm = perb.GetPerson(aa);

            //PersonPassportBUS ppbus = new PersonPassportBUS();
            //txtName.Text = ppbus.GetPersonPassportFullName(aa);
                                              
            if (perm != null)
            {
                string name = "";

                if (perm.nameTitle != "")
                    name = name + perm.nameTitle + ". ";
                if (perm.initialsContPers != "")
                    name = name + perm.initialsContPers + " ";
                if (perm.midname != "")
                    name = name + perm.midname + " ";
                if (perm.lastname != "")
                    name = name + perm.lastname;

                txtName.Text = name;
            }
           
            PersonAddressModel peradr = new PersonAddressModel();
            PersonAddressBUS peraBUS = new PersonAddressBUS();
            peradr = peraBUS.GetPersonAddressesByTypeOne(2, aa);

            if (peradr == null)
            {
                peradr = peraBUS.GetPersonAddressesByTypeOne(1, aa);
            }

            if (peradr != null)
            {
                txtAddress.Text = peradr.street;
                txtHouseNr.Text = peradr.housenr;
                txtExtension.Text = peradr.extension;
                if (peradr.postalCode.Length==6)
                    txtZip.Text = peradr.postalCode.Substring(0, 4) + " " + peradr.postalCode.Substring(4, 2); 
                else
                 txtZip.Text = peradr.postalCode;
                txtCity.Text = peradr.city;
                txtCountry.Text = peradr.country;
                txtCountry.Text = peradr.country;

                //if (peradr.extension != null && peradr.extension.Trim() != "")
                //{
                //    txtAddress.Text += " ";
                //    txtAddress.Text += peradr.housenr;
                //    txtAddress.Text += "-";
                //    txtAddress.Text += peradr.extension.Trim();
                //}

            }

         

            AccDebCreBUS debb = new AccDebCreBUS();
            AccDebCreModel debm = new AccDebCreModel();
            debm = debb.GetPersonDebCre(aa);
            if (debm != null)
            {
                if (debm.accNumber != null && debm.accNumber != "")
                {
                    customer = debm.accNumber;
                    //isNop = true;
                }
                else
                {
                    // btnBooking.Enabled = false;
                    RadMessageBox.Show("Customer does not have account number");
                    // isNop = false;
                }
            }
            else
            {
                // btnBooking.Enabled = false;
                RadMessageBox.Show("Customer does not have account number");
                // isNop = false;
            }
        }

        private void getClientAddressAndNumberAccount(int aa)
        {
            ClientBUS perb = new ClientBUS();
            ClientModel perm = new ClientModel();
            perm = perb.GetClient(aa);

            if (perm != null)
            {
                string name = "";
                if (perm.nameClient != "")
                    name = perm.nameClient;
                txtName.Text = name;
            }
            List<ClientAddressModel> cladrList = new List<ClientAddressModel>();
            ClientAddressBUS claBUS = new ClientAddressBUS();
            cladrList = claBUS.GetClientAddressesByType(2, aa);

            if (cladrList != null)
            {
                if (cladrList.Count > 0)
                {

                    if (cladrList[0].street == "" && cladrList[0].housenr == "" && cladrList[0].extension == "" && cladrList[0].postalCode == "" && cladrList[0].city == "")
                    {
                        cladrList = claBUS.GetClientAddressesByType(1, aa);

                        if (cladrList != null)
                        {
                            if (cladrList.Count > 0)
                            {
                                txtAddress.Text = cladrList[0].street;
                                txtHouseNr.Text = cladrList[0].housenr;
                                txtExtension.Text = cladrList[0].extension;
                                if (cladrList[0].postalCode.Length == 6)
                                    txtZip.Text = cladrList[0].postalCode.Substring(0, 4) + " " + cladrList[0].postalCode.Substring(4, 2);
                                else
                                    txtZip.Text = cladrList[0].postalCode;
                                txtCity.Text = cladrList[0].city;
                                txtCountry.Text = cladrList[0].country;

                                //if (cladrList[0].extension != null && cladrList[0].extension.Trim() != "")
                                //{
                                //    txtAddress.Text += " ";
                                //    txtAddress.Text += cladrList[0].housenr;
                                //    txtAddress.Text += "-";
                                //    txtAddress.Text += cladrList[0].extension.Trim();
                                //}
                            }
                        }
                    }
                }
               
            }

            AccDebCreBUS debb = new AccDebCreBUS();
            AccDebCreModel debm = new AccDebCreModel();
            debm = debb.GetClientDebCre(aa);
            if (debm != null)
            {
                if (debm.accNumber != null && debm.accNumber != "")
                {
                    customer = debm.accNumber;
                    //isNop = true;
                }
                else
                {
                    // btnBooking.Enabled = false;
                    RadMessageBox.Show("Customer does not have account number");
                    // isNop = false;
                }
            }
            else
            {
                // btnBooking.Enabled = false;
                RadMessageBox.Show("Customer does not have account number");
                // isNop = false;
            }
        }

        private void setLastPayDay()
        {
            if (txtInvoiceRbr.Text.Trim() == "999")
            {
                lblAfter.Visible = false;
                txtLastPayment.Visible = false;
            }
            else
            {
                int dtDifferenceDtFromDtInvoice = 0;
                //ArrangementModel am=new ArrangementModel();
                if (am != null)
                    if (am.dtFromArrangement != null)
                        dtDifferenceDtFromDtInvoice = Convert.ToInt32((am.dtFromArrangement - Convert.ToDateTime(dpInvoice.Value)).TotalDays);

                //if (dtDifferenceDtFromDtInvoice < 42)
                if (dtDifferenceDtFromDtInvoice < am.daysLastPayment)
                {
                    lblAfter.Visible = false;
                    txtLastPayment.Visible = false;

                }
                else
                {
                    lblAfter.Visible = true;
                    txtLastPayment.Visible = true;
                }
            }            
        }

        private void disableAllIfStatusIsntInProccessing(int idStatus)
        {
            if (idStatus <= 1)
            {
                btnDaily.Enabled = true;
                btnCountry.Enabled = true;
                txtRoomComment.Enabled = true;                
                if (model.invoiceRbr == "999")
                {
                    btnAddArtical.Enabled = false;
                    btnExtraArt.Enabled = false;
                }
                else
                {
                    btnExtraArt.Enabled = true;
                    btnAddArtical.Enabled = true;
                }
                gridItems.AllowAddNewRow = true;
                gridItems.AllowDeleteRow = true;
                gridItems.AllowEditRow = true;
                dpInvoice.Enabled = true;
            }
            else
            {
                txtRoomComment.Enabled = false;
                btnDaily.Enabled = false;
                btnCountry.Enabled = false;
                btnExtraArt.Enabled = false;
                btnAddArtical.Enabled = false;
                gridItems.AllowAddNewRow = false;
                gridItems.AllowDeleteRow = false;
                gridItems.AllowEditRow = false;
                dpInvoice.Enabled = false;                
            }
            if (idStatus >=2)
                btnDaily.Enabled = true;
        }

        //for button Booking
        private void chkBooking()
        {
            if (customer != "")
            {
                btnBooking.Enabled = false;
            }
            else
            {
                AccLineBUS aclb = new AccLineBUS(Login._bookyear);
                List<AccLineModel> aclm1 = new List<AccLineModel>();
                aclm1 = aclb.CheckLines(model.invoiceNr, customer);
               // btnBooking.Enabled = true;
                btnBooking.Enabled = false;
                if (aclm1 != null)
                {
                    if (aclm1.Count > 0)
                        btnBooking.Enabled = false;
                }
            }
        }


        private void gridItems_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridItems.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridItems.Columns[i].HeaderText != null && resxSet.GetString(gridItems.Columns[i].HeaderText) != null)
                        gridItems.Columns[i].HeaderText = resxSet.GetString(gridItems.Columns[i].HeaderText);
                }
            }
            if (gridItems.ColumnCount > 0)
            {

                gridItems.Columns["idArtical"].IsVisible = false;
                gridItems.Columns["idInvItem"].IsVisible = false;
                gridItems.Columns["idInvoice"].IsVisible = false;
                gridItems.Columns["userCreated"].IsVisible = false;
                gridItems.Columns["dtCreated"].IsVisible = false;
                gridItems.Columns["userModified"].IsVisible = false;
                gridItems.Columns["dtModified"].IsVisible = false;
                gridItems.Columns["isSecondGrid"].IsVisible = false;
                gridItems.Columns["isCancelationIns"].IsVisible = false;
                gridItems.Columns["isMedical"].IsVisible = false;
                gridItems.Columns["idArtical"].ReadOnly = true;
                gridItems.Columns["nameArtical"].ReadOnly = true;
                gridItems.Columns["itemSum"].ReadOnly = true;


            }
            if (gridItems != null)
                if (gridItems.RowCount > 0)
                {
                    for (int i = 0; i < gridItems.RowCount; i++)
                    {
                        if (gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "Cancel insurance" || gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "Insurance"
                            || gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "Reis Pakket" || gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "Money group"
                             || gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "Calamitait Fond" || gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "Reservation cost"
                             || gridItems.Rows[i].Cells["idArtical"].Value.ToString() == "First payment")
                        {
                            if (gridItems.ColumnCount > 0)
                                for (int j = 0; j < gridItems.ColumnCount; j++)
                                {
                                    gridItems.Rows[i].Cells[j].ReadOnly = true;
                                }
                        }
                        
                    }
                }
            if (File.Exists(layoutInvoice))
            {
                gridItems.LoadLayout(layoutInvoice);
            }
        }

        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutInvoice))
            {
                File.Delete(layoutInvoice);
            }
            gridItems.SaveLayout(layoutInvoice);
            translateRadMessageBox tr1 = new translateRadMessageBox();
            tr1.translateAllMessageBox("You have successfully save layout!");
        }

        private void gridItems_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click -= SaveLayout;
            customMenuItem.Click += SaveLayout;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }




        #region Buttons

        private void btnDaily_Click(object sender, EventArgs e)
        {
            AccDailyBUS acd = new AccDailyBUS(Login._bookyear);
            List<IModel> acm = new List<IModel>();


            acm = acd.GetBookingDailys();
            var dlgSave = new GridLookupForm(acm, "Daily");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccDailyModel genmX = new AccDailyModel();
                genmX = (AccDailyModel)dlgSave.selectedRow;

                if (genmX != null)
                {
                    //set textbox
                    if (genmX.codeDaily != null)
                    {
                        txtDaily.Text = genmX.codeDaily + "   " + genmX.descDaily;
                        codeD = genmX.codeDaily;
                        if (model.idInvoiceStatus >= 2)
                          //  btnBooking.Enabled = true;  izbaceno knjizenje ide preko invoice selection-a
                            btnBooking.Enabled = false;
                    }
                }
                else
                {
                    RadMessageBox.Show("Can't booking without Verkoop book !!!");
                    btnBooking.Enabled = false;
                }
            }

        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            translateRadMessageBox tr = new translateRadMessageBox();
            if (tr.translateAllMessageBoxDialog("Booking invoice ?", " ") == System.Windows.Forms.DialogResult.Yes)
            {
                AccDebCreBUS debcre = new AccDebCreBUS();
                AccDebCreModel dcmod = new AccDebCreModel();
                dcmod = debcre.GetPersonDebCre(Convert.ToInt32(model.idContPerson));
                if (dcmod != null)
                {
                    if (dcmod.idContPerson != null && dcmod.idContPerson != 0)
                    {
                        if (codeD != null && codeD != "")
                        {
                            //secondItems = new List<InvoiceItemsModel>();
                            //secondItems = gridItems.DataSource as List<InvoiceItemsModel>;
                             if (secondItems!=null)
                            if (secondItems.Count >= 0)
                            {
                                // int i = 0;
                                for (int j = 0; j < secondItems.Count; j++)
                                {
                                    //i++;
                                    decimal prod = 0;
                                    prod = Convert.ToDecimal(iim1[j].price) * Convert.ToDecimal(iim1[j].quantity);
                                    if (prod != 0) // i za cancel i za normano knjizenje
                                        iim1.Add(secondItems[j]);
                                }
                            }
                            bool llbookOk = false;
                            AccAcountUpdate book = new AccAcountUpdate();
                            llbookOk = book.InvoiceBooking(model, iim1, codeD, idLabel, am.codeProject, this.Name, Login._user.idUser);
                            if (llbookOk == false)
                            {
                                RadMessageBox.Show("NOT booked !!!");
                                return;
                            }

                            RadMessageBox.Show("Finish");
                            model.idInvoiceStatus = 4;
                            ddlStatus.SelectedValue = model.idInvoiceStatus;
                            inbus = new InvoiceBUS();
                            inbus.UpdateStatus(4, model.idInvoice, this.Name, Login._user.idUser);

                            btnBooking.Enabled = false;
                            disableAllIfStatusIsntInProccessing(Convert.ToInt32(model.idInvoiceStatus));
                        }
                        else
                        {
                            RadMessageBox.Show("Can't booking without Verkoop book !!!");
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Person does not have account id !!!");

                        return;
                    }
                }
            }
        }




        private void btnCountry_Click(object sender, EventArgs e)
        {

            CountryBUS accBUS = new CountryBUS();
            List<IModel> am1 = new List<IModel>();

            am1 = accBUS.GetCountries();


            var dlgClient = new GridLookupForm(am1, "Country");

            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {

                CountryModel okm = new CountryModel();
                okm = (CountryModel)dlgClient.selectedRow;
                txtCountry.Text = okm.nameCountry;

            }


        }

        private void btnAddArtical_Click(object sender, EventArgs e)
        {

            ArticalBUS aBUS = new ArticalBUS();
            List<IModel> am2 = new List<IModel>();

            am2 = aBUS.GetAllArticals();


            var dlgClient = new GridLookupForm(am2, "Artical");

            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {

                ArticalModel okm = new ArticalModel();
                okm = (ArticalModel)dlgClient.selectedRow;
                InvoiceItemsModel imone = new InvoiceItemsModel();
                imone.idArtical = okm.codeArtical;
                imone.nameArtical = okm.nameArtical;
                imone.quantity = qol;
                imone.price = okm.sellingPrice;
                imone.idInvoice = model.idInvoice;                  //Convert.ToInt32(txtInvoice.Text);
                imone.itemSum = qol * okm.sellingPrice;
                if (iim1 == null)
                    iim1 = new List<InvoiceItemsModel>();
                iim1.Add(imone);
                gridItems.DataSource = null;
                gridItems.DataSource = iim1;
                cancelInsurance();
            }
        }

        private void invoice_click()
        {
            InvoiceBUS ibus = new InvoiceBUS();
            InvoiceItemsBUS itemsbus = new InvoiceItemsBUS();
            if (model.idInvoiceStatus != 0)
                model.idInvoiceStatus = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
            model.dtFirstPay = Convert.ToDateTime(dpFirstpay.Text);
            model.dtLastPay = Convert.ToDateTime(txtLastPayment.Text);
            model.percentFrstPay = Convert.ToDecimal(txtFrstPayPercent.Text);
            model.brutoAmount = Convert.ToDecimal(txtBrutoAmt.Text);
            string codeProject = "";
            if (am != null)
                if (am.codeProject != "")
                    codeProject = " - " + am.codeProject;
            AccDebCreModel acdb = new AccDebCreModel();
            if (am.idClientInvoice != null)
                if (am.idClientInvoice != 0)
                {
                    acdb = new AccDebCreBUS().GetClientDebCre(am.idClientInvoice);
                }
                else
                    acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(model.idContPerson));
            else
                acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(model.idContPerson));

            string debitorAccount = "";
            if (acdb != null)
                if (acdb.debAccount != "")
                    debitorAccount = " - " + acdb.accNumber;


            if (model.invoiceRbr == "000")
            {
                if (model.brutoAmount >= 0)
                    model.descriptionInvoice = "Aanbetaling" + codeProject + debitorAccount;
                else
                    model.descriptionInvoice = "Credit aanbetaling" + codeProject + debitorAccount;
            }
            else if (model.invoiceRbr == "001")
            {
                if (model.brutoAmount >= 0)
                    model.descriptionInvoice = "Restantbetaling" + codeProject + debitorAccount;
                else
                    model.descriptionInvoice = "Credit restantbetaling" + codeProject + debitorAccount;
            }
            else if (Convert.ToInt32(model.invoiceRbr) >= 002 && Convert.ToInt32(model.invoiceRbr) < 999)
            {
                if (model.brutoAmount >= 0)
                    model.descriptionInvoice = "Extra" + codeProject + debitorAccount;
                else
                    model.descriptionInvoice = "Credit extra" + codeProject + debitorAccount;
            }
            else if (model.invoiceRbr == "999")
                model.descriptionInvoice = "Annulering" + debitorAccount;
            else
                model.descriptionInvoice = am.nameArrangement;

            model.userModified = Login._user.idUser;
            model.dtModified = DateTime.Now;
            model.noteInvoice = txtText.Text;
            model.firstreferencePay = txtfirstreference.Text;
            model.secondreferencePay = txtSecondreference.Text;
            model.roomComment = txtRoomComment.Text;
            // if (idLabel != null)
            //    model.idClient = idLabel;
            if (model.invoiceRbr == "000")
                model.typeinvoice = 10;
            else if (model.invoiceRbr == "001")
                model.typeinvoice = 11;
            else
                model.typeinvoice = 21;

            if (model.invoiceRbr == "001")
            {
                decimal suma = 0;
                decimal sumCancelInsuranceReservationCosts = 0;
                decimal sumInsGroupCalam = 0;
                int dtDifferenceDtFromDtInvoice = 0;

                //ArrangementModel am=new ArrangementModel();
                if (am != null)
                    if (am.dtFromArrangement != null)
                        dtDifferenceDtFromDtInvoice = Convert.ToInt32((am.dtFromArrangement - DateTime.Now).TotalDays);

                for (int t = 0; t < iim1.Count; t++)
                {
                    if (iim1[t].idArtical != "First payment")
                        suma = suma + Convert.ToDecimal(iim1[t].quantity) * Convert.ToDecimal(iim1[t].price);

                    //shimmy
                    //if (dtDifferenceDtFromDtInvoice >= 42)
                    if (dtDifferenceDtFromDtInvoice >= am.daysLastPayment)
                    {
                        if (iim1[t].idArtical == "Insurance" || iim1[t].idArtical == "Calamitait Fond" || iim1[t].idArtical == "Money group")
                        {
                            sumInsGroupCalam += Convert.ToDecimal(iim1[t].price) * Convert.ToDecimal(iim1[t].quantity);

                        }
                    }

                    if (iim1[t].idArtical == "Cancel insurance" || iim1[t].idArtical == "Reservation cost")
                    {
                        sumCancelInsuranceReservationCosts += Convert.ToDecimal(iim1[t].price) * Convert.ToDecimal(iim1[t].quantity);
                    }
                    //
                }
                decimal fpmodified = 0;  // azurira prvo placanje ako je nesto promenjeno
                //fpmodified = (Convert.ToDecimal(suma) * am.percentFirstPayment / 100) / 1;  // po jedinici kolicine
                fpmodified = ((Convert.ToDecimal(suma) - sumCancelInsuranceReservationCosts - sumInsGroupCalam) * am.percentFirstPayment / 100 + sumCancelInsuranceReservationCosts) / 1;  // po jedinici kolicine
                for (int t = 0; t < iim1.Count; t++)
                {
                    if (iim1[t].idArtical == "First payment")
                    {
                        iim1[t].price = -fpmodified;
                        iim1[t].quantity = 1;
                    }
                }
                decimal totalmodified = 0;
                for (int w = 0; w < iim1.Count; w++)
                {
                    totalmodified = totalmodified + Convert.ToDecimal(iim1[w].price) * Convert.ToDecimal(iim1[w].quantity);

                }
                model.brutoAmount = totalmodified;
                InvoiceModel ium = new InvoiceModel();
                ium = ibus.GetInvoiceByInvoiceAndExtension(model.invoiceNr, "000");
                if (ium != null)
                {
                    if (ium.idInvoice != 0 && ium.idInvoice != -1)
                    {
                        ium.brutoAmount = Math.Round(fpmodified * 1, 2);
                        if (idLabel != null)
                            ium.idClient = idLabel;
                        if (ium.invoiceRbr == "000")
                            ium.typeinvoice = 10;
                        else if (ium.invoiceRbr == "001")
                            ium.typeinvoice = 11;
                        else
                            ium.typeinvoice = 12;
                        ibus.Update(ium, this.Name, Login._user.idUser);

                        List<InvoiceItemsModel> iium = new List<InvoiceItemsModel>();
                        InvoiceItemsBUS iibus = new InvoiceItemsBUS();
                        iium = iibus.GetInvoiceItemsByID(ium.idInvoice, Login._user.lngUser);
                        if (iium != null)
                        {
                            for (int y = 0; y < iium.Count; y++)
                            {
                                if (iium[y].idArtical == "First payment")
                                {
                                    iium[y].price = fpmodified;
                                    iium[y].quantity = 1;
                                }
                            }
                            iibus.Delete(ium.idInvoice, this.Name, Login._user.idUser);
                            bool isOK = false;
                            if (iium != null)
                            {
                                for (int j = 0; j < iium.Count; j++)
                                {
                                    isOK = itemsbus.Save(iium[j], this.Name, Login._user.idUser);
                                    if (isOK == false)
                                    {
                                        translateRadMessageBox tr1 = new translateRadMessageBox();
                                        tr1.translateAllMessageBox("Error inserted First payment. Please check!");
                                    }


                                }
                            }

                        }


                    }

                }

                bool isupdate = ibus.Update(model, this.Name, Login._user.idUser);
                if (isupdate == false)
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("Error updating invoice");
                }


                itemsbus.Delete(model.idInvoice, this.Name, Login._user.idUser);  // brise stavke i upisuje nove

                bool isOK1 = false;
                if (iim1 != null)
                {
                    for (int j = 0; j < iim1.Count; j++)
                    {
                        isOK1 = itemsbus.Save(iim1[j], this.Name, Login._user.idUser);
                        if (isOK1 == false)
                        {
                            translateRadMessageBox tr1 = new translateRadMessageBox();
                            tr1.translateAllMessageBox("Error inserted items. Please check!");

                        }
                    }
                }

                if (isOK1 == true)
                {
                    if (isMessage == true)
                        if (model.idInvoiceStatus > 0)
                        {
                            translateRadMessageBox tr1 = new translateRadMessageBox();
                            tr1.translateAllMessageBox("Saved");
                        }
                }
                InvoiceItemsBUS iib = new InvoiceItemsBUS();
                iim1 = new List<InvoiceItemsModel>();
                iim1 = iib.GetInvoiceItemsByID(model.idInvoice, Login._user.lngUser);
                if (iim1 == null)
                    iim1 = new List<InvoiceItemsModel>();
                isLoaded = false;
                gridItems.DataSource = null;
                gridItems.DataSource = iim1;
                isLoaded = true;
                // save invoice heder

            }
            else
            {
                bool isupdate = ibus.Update(model, this.Name, Login._user.idUser);
                if (isupdate == false)
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("Error updating invoice");
                }


                itemsbus.Delete(model.idInvoice, this.Name, Login._user.idUser);  // brise stavke i upisuje nove

                bool isOK1 = false;
                if (iim1 != null)
                {
                    for (int j = 0; j < iim1.Count; j++)
                    {
                        isOK1 = itemsbus.Save(iim1[j], this.Name, Login._user.idUser);
                        if (isOK1 == false)
                        {
                            translateRadMessageBox tr1 = new translateRadMessageBox();
                            tr1.translateAllMessageBox("Error inserted items. Please check!");
                        }
                    }
                }
            }
            if (model.idInvoiceStatus == 0)
                btnMake.PerformClick();
        }
        private void btnInvoice_Click(object sender, EventArgs e)
        {

            invoice_click();

            if (model.idInvoiceStatus == 1)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("You want to change the status to ready to print ?", " ") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (new InvoiceDAO().UpdateStatus(6, model.idInvoice, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox tr1 = new translateRadMessageBox();
                        tr1.translateAllMessageBox("Something went wrong with updating status! Please contact the administrator");
                    }
                    else
                    {
                        model.idInvoiceStatus = 6;
                    }
                }
            }

            this.Close();

           // disableAllIfStatusIsntInProccessing(Convert.ToInt32(model.idInvoiceStatus));
        }
        List<InvoiceSelectionDocumentModel> documentsToBeSavedToDb = new List<InvoiceSelectionDocumentModel>();
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue.ToString() != "1" && ddlStatus.SelectedValue.ToString() != "6")
            {
                translateRadMessageBox tr1 = new translateRadMessageBox();
                tr1.translateAllMessageBox("Only Ready to print and In Progress invoices can be printed.");
                return;
            }

            //if (model.idContPerson == 0)
            //{
            //    translateRadMessageBox msgbox = new translateRadMessageBox();
            //    msgbox.translateAllMessageBox("You need to add person first.");
            //    return;
            //}

            documentsToBeSavedToDb.Clear();
            if (model.invoiceRbr != "000" && model.invoiceRbr != "001")
            {
                if (model.invoiceRbr == "999")
                {
                    //printFirstandLastPayment(true);


                    //PrintReport pr = printFirstandLastPaymentWithoutWindow(true);

                    //string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;
                    //InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                    //invmod.idContPers = model.idContPerson;
                    //invmod.idClient = 0;
                    //invmod.descriptionDocument = "Printed Invoice PDF";
                    //invmod.fileDocument = Path.GetFileName(savetopath);
                    //invmod.typeDocument = "SCANS";
                    //invmod.idDocumentStatus = 2;
                    //invmod.idEmployee = 0;
                    //invmod.idResponsableEmployee = 0;
                    //invmod.inOutDocument = 2;
                    //invmod.noteDocument = "";
                    //invmod.idArrangement = am.idArrangement;
                    //invmod.nameFileToSend = nameFileToSend;
                    //invmod.report = pr;

                    //invmod.dtCreated = DateTime.Now;
                    //invmod.dtModified = DateTime.Now;
                    //invmod.userCreated = Login._user.idUser;
                    //invmod.userModified = Login._user.idUser;

                    //documentsToBeSavedToDb.Add(invmod);
                }
                //
                List<InvoiceReportModel> irep = new List<InvoiceReportModel>();
                InvoiceReportModel aa = new InvoiceReportModel();
                List<InvoiceItemsReportModel> iirep = new List<InvoiceItemsReportModel>();
                InvoiceBUS rptb = new InvoiceBUS();
                InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
                DataTable invoiceAll = new DataTable();
                DataTable invoice = new DataTable();
                DataTable itemsInv = new DataTable();

                //if (model.invoiceRbr != "999")
                    invoiceAll = rptb.GetReportInvoiceByIntID(model.idInvoice);
                //else
                 //   invoiceAll = rptb.GetReportInvoiceFor999ByIntID(model.idInvoice);
                //
                if ((invoiceAll != null) && (invoiceAll.Rows.Count > 0))
                {
                    //DataRow dr = invoice.Rows[0];
                    foreach (DataRow dr in invoiceAll.Rows)
                    {
                        invoice = new DataTable();
                        invoice = invoiceAll.Copy();
                        invoice.Rows.Clear();
                        invoice.Rows.Add(dr.ItemArray);
                        if (dr["idContPerson"].ToString() == "")                            
                            dr["idContPerson"] = model.idContPerson;                        

                        if (dr["street"].ToString() == "")
                            dr["street"] = txtAddress.Text;
                        if (dr["houseNr"].ToString() == "")
                            dr["houseNr"] = txtHouseNr.Text;
                        if (dr["extend"].ToString() == "")
                            dr["extend"] = txtExtension.Text;
                        if (dr["zip"].ToString() == "")
                            dr["zip"] = txtZip.Text;
                        if (dr["City"].ToString() == "")
                            dr["City"] = txtCity.Text;
                        if (dr["country"].ToString() == "")
                            dr["country"] = txtCountry.Text;
                        //    if (dr["namePerson"].ToString() == "")
                        dr["namePerson"] = txtName.Text;

                        //==
                        dr["arrName"] = txtNameArr.Text;
                        dr["noDays"] = txtNoDays.Text;
                        dr["boarding"] = txtBoarding.Text;
                        dr["dateFrom"] = txtDateFrom.Text;
                        dr["dateTo"] = txtDateTo.Text;
                        dr["service"] = txtHotelService.Text;
                        if (model.invoiceRbr != "999")
                        {
                            dr["noteInvoice"] = txtText.Text;
                            dr["firstAmount"] = model.brutoAmount;
                            dr["firstReference"] = txtInvoice.Text.Trim() + "-" + txtInvoiceRbr.Text.Trim();
                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(model.idInvoice, Login._user.lngUser);
                        }
                        else
                        {
                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(Convert.ToInt32(dr["idInvoice"].ToString()), Login._user.lngUser);
                            nameFileToSend = "Factuur-" + dr["invoiceNr"].ToString() + "-" + dr["invoiceRbr"].ToString() + ".pdf";
                        }
                        //

                        if (itemsInv == null)
                            itemsInv = new DataTable();


                        frmInvoiceReport aaa = new frmInvoiceReport(invoice, itemsInv, nameFileToSend, idLabel, true);
                        aaa.ShowDialog();


                        PrintReport pr = new PrintReport(invoice, itemsInv, nameFileToSend, idLabel, false, false, false);
                        //pr.Run();
                        string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;
                        InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                        invmod.idContPers =idTraveler;
                        invmod.idClient = 0;
                        invmod.descriptionDocument = "Printed Invoice PDF";
                        invmod.fileDocument = Path.GetFileName(savetopath);
                        invmod.typeDocument = "SCANS";
                        invmod.idDocumentStatus = 2;
                        invmod.idEmployee = 0;
                        invmod.idResponsableEmployee = 0;
                        invmod.inOutDocument = 2;
                        invmod.noteDocument = "";
                        invmod.idArrangement = am.idArrangement;
                        invmod.nameFileToSend = nameFileToSend;
                        invmod.report = pr;

                        invmod.dtCreated = DateTime.Now;
                        invmod.dtModified = DateTime.Now;
                        invmod.userCreated = Login._user.idUser;
                        invmod.userModified = Login._user.idUser;
                        
                        documentsToBeSavedToDb.Add(invmod);

                        if (model.invoiceRbr != "999")
                            break;
                    }
                }


                if (invoice == null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Nothing for printing!");
                }
                else if (invoice.Rows.Count <= 0)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Nothing for printing!");
                }

            }
            else
            {
                if (model.brutoAmount < 0)
                {
                    printFirstandLastPayment(true);
                    PrintReport pr = printFirstandLastPaymentWithoutWindow(true);
                    //pr.Run();
                    string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;
                    InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                    invmod.idContPers = idTraveler;
                    invmod.idClient = 0;
                    invmod.descriptionDocument = "Printed Invoice PDF";
                    invmod.fileDocument = Path.GetFileName(savetopath);
                    invmod.typeDocument = "SCANS";
                    invmod.idDocumentStatus = 2;
                    invmod.idEmployee = 0;
                    invmod.idResponsableEmployee = 0;
                    invmod.inOutDocument = 2;
                    invmod.noteDocument = "";
                    invmod.idArrangement = am.idArrangement;
                    invmod.nameFileToSend = nameFileToSend;
                    invmod.report = pr;

                    invmod.dtCreated = DateTime.Now;
                    invmod.dtModified = DateTime.Now;
                    invmod.userCreated = Login._user.idUser;
                    invmod.userModified = Login._user.idUser;

                    documentsToBeSavedToDb.Add(invmod);
                }
                else
                {
                    printFirstandLastPayment(false);
                    PrintReport pr = printFirstandLastPaymentWithoutWindow(false);

                    string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;
                    InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                    invmod.idContPers = idTraveler;
                    invmod.idClient = 0;
                    invmod.descriptionDocument = "Printed Invoice PDF";
                    invmod.fileDocument = Path.GetFileName(savetopath);
                    invmod.typeDocument = "SCANS";
                    invmod.idDocumentStatus = 2;
                    invmod.idEmployee = 0;
                    invmod.idResponsableEmployee = 0;
                    invmod.inOutDocument = 2;
                    invmod.noteDocument = "";
                    invmod.idArrangement = am.idArrangement;
                    invmod.nameFileToSend = nameFileToSend;
                    invmod.report = pr;

                    invmod.dtCreated = DateTime.Now;
                    invmod.dtModified = DateTime.Now;
                    invmod.userCreated = Login._user.idUser;
                    invmod.userModified = Login._user.idUser;

                    documentsToBeSavedToDb.Add(invmod);
                }
                //}
            }

            isMessage = false;  // ubaceno da ne prikazuje Saved poruku...
            //btnInvoice_Click(sender, e);
            invoice_click();
            isMessage = true;
            inbus = new InvoiceBUS();
            translateRadMessageBox trr = new translateRadMessageBox();
            if (trr.translateAllMessageBoxDialog("Change status ?", " ") == System.Windows.Forms.DialogResult.Yes)
            {
                if (model.idInvoiceStatus <= 1 || model.idInvoiceStatus==6)
                {
                    model.idInvoiceStatus++;
                    ddlStatus.SelectedValue = model.idInvoiceStatus;

                    inbus.UpdateStatus(2, model.idInvoice, this.Name, Login._user.idUser);

                    if (model.invoiceRbr == "001")
                    {
                        InvoiceModel ium = new InvoiceModel();
                        ium = inbus.GetInvoiceByInvoiceAndExtension(model.invoiceNr, "000");
                        if (ium != null)
                        {
                            inbus.UpdateStatus(2, ium.idInvoice, this.Name, Login._user.idUser);
                            model.idInvoiceStatus = 2;
                            ddlStatus.SelectedValue = Convert.ToInt32(model.idInvoiceStatus);
                            disableAllIfStatusIsntInProccessing(Convert.ToInt32(model.idInvoiceStatus));
                        }
                    }
                    else
                    {
                        if (model.invoiceRbr == "000")
                        {
                            InvoiceModel ium = new InvoiceModel();
                            ium = inbus.GetInvoiceByInvoiceAndExtension(model.invoiceNr, "001");
                            if (ium != null)
                            {
                                inbus.UpdateStatus(2, ium.idInvoice, this.Name, Login._user.idUser);
                                model.idInvoiceStatus = 2;
                                ddlStatus.SelectedValue = Convert.ToInt32(model.idInvoiceStatus);
                                disableAllIfStatusIsntInProccessing(Convert.ToInt32(model.idInvoiceStatus));
                            }
                        }
                    }

                    DocumentsBUS sbus = new DocumentsBUS();
                    DocumentsModel docmodel;
                    foreach (InvoiceSelectionDocumentModel m in documentsToBeSavedToDb)
                    {
                        if (m.report != null && m.nameFileToSend != String.Empty)
                        {
                            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                            string savetopath = MainForm.DocumentsFolder + "\\" + m.nameFileToSend;

                            if (File.Exists(savetopath) == true)
                                File.Delete(savetopath);

                            rg.GenerateOutputPDF(m.report.report, savetopath);

                            docmodel = new DocumentsModel();
                            docmodel.idContPers = m.idContPers;
                            docmodel.idClient = m.idClient;
                            docmodel.descriptionDocument = m.descriptionDocument;
                            docmodel.fileDocument = m.fileDocument;
                            docmodel.typeDocument = m.typeDocument;
                            docmodel.idDocumentStatus = m.idDocumentStatus;
                            docmodel.idEmployee = m.idEmployee;
                            docmodel.idResponsableEmployee = m.idResponsableEmployee;
                            docmodel.inOutDocument = m.inOutDocument;
                            docmodel.noteDocument = m.noteDocument;
                            docmodel.idArrangement = m.idArrangement;

                            docmodel.dtCreated = DateTime.Now;
                            docmodel.dtModified = DateTime.Now;
                            docmodel.userCreated = Login._user.idUser;
                            docmodel.userModified = Login._user.idUser;

                            sbus.Save(docmodel, this.Name, Login._user.idUser);
                        }
                    }
                }
                if (model.idInvoiceStatus > 1)
                {
                    btnInvoice.Enabled = false;
                }
                txtDaily.Visible = false;  //disablevano ... booking se radi sa selection invoice
                btnDaily.Visible = false;
            }

            
        }

        private void printFirstandLastPayment(Boolean is999)
        {
            InvoiceBUS ib001 = new InvoiceBUS();
            DataTable invoice = new DataTable();
            DataTable itemsInv = new DataTable();
            if (is999 == false || (model.invoiceRbr == "000" && model.brutoAmount < 0))
                invoice = ib001.GetReportInvoiceByIntID(model.idInvoice);
            else
                invoice = ib001.GetReportInvoiceByIntID999(model.idInvoice);


            DataTable items000 = new DataTable();
            DataTable items001 = new DataTable();
            InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
            if (invoice != null)
            {
                if (invoice.Rows.Count > 0)
                {
                    DataRow dr = invoice.Rows[0];
                    if (dr["idContPerson"].ToString() == "")
                        dr["idContPerson"] = model.idContPerson;

                    if (dr["street"].ToString() == "")
                        dr["street"] = txtAddress.Text;
                    if (dr["houseNr"].ToString() == "")
                        dr["houseNr"] = txtHouseNr.Text;
                    if (dr["extend"].ToString() == "")
                        dr["extend"] = txtExtension.Text;
                    if (dr["zip"].ToString() == "")
                        dr["zip"] = txtZip.Text;
                    if (dr["City"].ToString() == "")
                        dr["City"] = txtCity.Text;
                    if (dr["country"].ToString() == "")
                        dr["country"] = txtCountry.Text;
                    //    if (dr["namePerson"].ToString() == "")
                    dr["namePerson"] = txtName.Text;

                    //==
                    dr["arrName"] = txtNameArr.Text;
                    dr["noDays"] = txtNoDays.Text;
                    dr["boarding"] = txtBoarding.Text;
                    dr["dateFrom"] = txtDateFrom.Text;
                    dr["dateTo"] = txtDateTo.Text;
                    dr["service"] = txtHotelService.Text;


                    itemsInv = rptiib.GetReportInvoiceItemsByID(model.idInvoice, Login._user.lngUser);

                    InvoiceModel im000 = new InvoiceModel();
                    im000 = ib001.GetInvoiceByInvoiceAndExtension999(model.invoiceNr, "000",is999);
                    if (im000 != null)
                        if (im000.idInvoice != null && im000.idInvoice != 0)
                        {
                            items000 = rptiib.GetReportInvoiceItemsByID(im000.idInvoice, Login._user.lngUser);

                            dr["firstAmount"] = im000.brutoAmount;
                            dr["firstReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                            dr["restAmount"] = im000.brutoAmount;
                            dr["restReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                        }

                    InvoiceModel im001 = new InvoiceModel();
                    im001 = ib001.GetInvoiceByInvoiceAndExtension999(model.invoiceNr, "001",is999);
                    if (im001.idInvoice != null && im001.idInvoice != 0)
                    {
                        items001 = rptiib.GetReportInvoiceItemsByID(im001.idInvoice, Login._user.lngUser);
                        if (im000 != null)
                        {
                            dr["firstAmount"] = im000.brutoAmount;
                            dr["firstReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                        }
                        else
                        {
                            dr["dtFirstPay"] = DBNull.Value;
                        }
                        dr["restAmount"] = im001.brutoAmount;
                        dr["restReference"] = im001.idContPerson + "/" + im001.invoiceNr + "-" + im001.invoiceRbr;
                        dr["noteInvoice"] = im001.noteInvoice;
                    }
                    decimal ukupno = Convert.ToDecimal(im001.brutoAmount);
                    if (im000 != null)
                    {
                        ukupno = Convert.ToDecimal(im000.brutoAmount) + Convert.ToDecimal(im001.brutoAmount);
                    }
                    dr["netoAmount"] = ukupno;
                }
                // cita grid za putnike
                ArrangementBookPersonsDAO bus = new ArrangementBookPersonsDAO();

                items000 = bus.GetAllTravelersInvoicing( abm.idArrangementBook, true); // ubaceno za osobe za koje se placa
                //items000 = bus.GetAllTravelersWith(Convert.ToInt32(model.idContPerson), abm.idArrangement);

                if (items000 == null)
                    items000 = new DataTable();

                if (items001 == null)
                    items001 = new DataTable();

                if (invoice == null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Nothing for printing!");
                }
                else if (invoice.Rows.Count <= 0)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Nothing for printing!");
                }
                else
                {
                    if (model.invoiceRbr == "000" && model.brutoAmount<0)
                        items001 = rptiib.GetReportInvoiceItemsByIDAll(model.idInvoice, Login._user.lngUser); 
                    frmInvoiceOneReport fmOne = new frmInvoiceOneReport(invoice, items001, items000, nameFileToSend, idLabel, true, true);
                    fmOne.ShowDialog();                    
                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Nothing for printing!");
            }
        }

        private DataTable ConvertListToDataTable(List<InvoiceItemsReportModel> iirep)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 8;


            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in iirep)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue.ToString() != "1" && ddlStatus.SelectedValue.ToString() != "6")
            {
                translateRadMessageBox tr1 = new translateRadMessageBox();
                tr1.translateAllMessageBox("Only Ready to print and In Progress invoices can be printed.");
                return;
            }

            if (Login.isOutlookInstalled == false)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
                return;
            }

            if (model.idContPerson == 0)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("You need to add person first.");
                return;
            }

            if (model.idContPerson != 0)
            {
                PersonEmailBUS pbus = new PersonEmailBUS();
                PersonEmailiSInvoiceModel isInvModel = new PersonEmailiSInvoiceModel();
                isInvModel = pbus.GetPersonEmailsISInoicing((int)model.idContPerson);

                if (isInvModel == null || isInvModel.email.Trim() == "")
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Person does not have invoicing email address.");
                    return;
                }

                if (BookmarkFunctions.IsEmailValid(isInvModel.email) == false)
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Invoicing Email address is not valid.");
                    return;
                }

                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    List<string> _tempFolders = new List<string>();
                    string tempFolder;

                    tempFolder = MainForm.GetTemporaryFolder();
                    _tempFolders.Add(tempFolder);

                    Outlook.Application outlookApp = new Outlook.Application();

                    Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                    outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);
                    oMailItem.DeleteAfterSubmit = false;

                    oMailItem.Subject = "";
                    oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                    oMailItem.Body = "";

                    Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                    Outlook.Recipient oRecip;

                    oRecip = (Outlook.Recipient)oRecips.Add(isInvModel.email);

                    oRecip.Resolve();

                    Outlook.Attachments oAttachs = (Outlook.Attachments)oMailItem.Attachments;
                    Outlook.Attachment oAtt = null;


                    if (model.invoiceRbr != "000" && model.invoiceRbr != "001")
                    {
                        if (model.invoiceRbr == "999")
                        {
                            PrintReport pr = printFirstandLastPaymentWithoutWindow(true);

                            //email
                            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                            string savetopath = tempFolder + "\\" + nameFileToSend;

                            if (File.Exists(savetopath) == true)
                                File.Delete(savetopath);

                            rg.GenerateOutputPDF(pr.report, savetopath);
                            oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);

                        }
                        //
                        List<InvoiceReportModel> irep = new List<InvoiceReportModel>();
                        InvoiceReportModel aa = new InvoiceReportModel();
                        List<InvoiceItemsReportModel> iirep = new List<InvoiceItemsReportModel>();
                        InvoiceBUS rptb = new InvoiceBUS();
                        InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
                        DataTable invoiceAll = new DataTable();
                        DataTable invoice = new DataTable();
                        DataTable itemsInv = new DataTable();

                        if (model.invoiceRbr != "999")
                            invoiceAll = rptb.GetReportInvoiceByIntID(model.idInvoice);
                        else
                            invoiceAll = rptb.GetReportInvoiceFor999ByIntID(model.idInvoice);
                        //
                        if ((invoiceAll != null) && (invoiceAll.Rows.Count > 0))
                        {
                            //DataRow dr = invoice.Rows[0];
                            foreach (DataRow dr in invoiceAll.Rows)
                            {
                                invoice = new DataTable();
                                invoice = invoiceAll.Copy();
                                invoice.Rows.Clear();
                                invoice.Rows.Add(dr.ItemArray);
                                if (dr["idContPerson"].ToString() == "")
                                    dr["idContPerson"] = model.idContPerson;

                                if (dr["street"].ToString() == "")
                                    dr["street"] = txtAddress.Text;
                                if (dr["houseNr"].ToString() == "")
                                    dr["houseNr"] = txtHouseNr.Text;
                                if (dr["extend"].ToString() == "")
                                    dr["extend"] = txtExtension.Text;
                                if (dr["zip"].ToString() == "")
                                    dr["zip"] = txtZip.Text;
                                if (dr["City"].ToString() == "")
                                    dr["City"] = txtCity.Text;
                                if (dr["country"].ToString() == "")
                                    dr["country"] = txtCountry.Text;
                                //    if (dr["namePerson"].ToString() == "")
                                dr["namePerson"] = txtName.Text;

                                //==
                                dr["arrName"] = txtNameArr.Text;
                                dr["noDays"] = txtNoDays.Text;
                                dr["boarding"] = txtBoarding.Text;
                                dr["dateFrom"] = txtDateFrom.Text;
                                dr["dateTo"] = txtDateTo.Text;
                                dr["service"] = txtHotelService.Text;
                                if (model.invoiceRbr != "999")
                                {
                                    dr["noteInvoice"] = txtText.Text;
                                    dr["firstAmount"] = model.brutoAmount;
                                    dr["firstReference"] = txtInvoice.Text.Trim() + "-" + txtInvoiceRbr.Text.Trim();
                                    itemsInv = rptiib.GetReportInvoiceItemsByIDAll(model.idInvoice, Login._user.lngUser);
                                }
                                else
                                {
                                    itemsInv = rptiib.GetReportInvoiceItemsByIDAll(Convert.ToInt32(dr["idInvoice"].ToString()), Login._user.lngUser);
                                    nameFileToSend = "Factuur-" + dr["invoiceNr"].ToString() + "-" + dr["invoiceRbr"].ToString() + ".pdf";
                                }
                                //

                                if (itemsInv == null)
                                    itemsInv = new DataTable();

                                //frmInvoiceReport aaa = new frmInvoiceReport(invoice, itemsInv, nameFileToSend, idLabel, true);
                                //aaa.ShowDialog();

                                PrintReport pr = new PrintReport(invoice, itemsInv, nameFileToSend, idLabel, false, false, false);
                                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                string savetopath = tempFolder + "\\" + nameFileToSend;

                                if (File.Exists(savetopath) == true)
                                    File.Delete(savetopath);

                                rg.GenerateOutputPDF(pr.report, savetopath);
                                oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);

                                if (model.invoiceRbr != "999")
                                    break;
                            }
                        }


                        if (invoice == null)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Nothing for printing!");
                        }
                        else if (invoice.Rows.Count <= 0)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Nothing for printing!");
                        }

                    }
                    else
                    {
                        if (model.brutoAmount < 0)
                        {
                            PrintReport pr = printFirstandLastPaymentWithoutWindow(true);

                            //email
                            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                            string savetopath = tempFolder + "\\" + nameFileToSend;

                            if (File.Exists(savetopath) == true)
                                File.Delete(savetopath);

                            rg.GenerateOutputPDF(pr.report, savetopath);
                            oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                        }
                        else
                        {
                            PrintReport pr = printFirstandLastPaymentWithoutWindow(false);

                            //email
                            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                            string savetopath = tempFolder + "\\" + nameFileToSend;

                            if (File.Exists(savetopath) == true)
                                File.Delete(savetopath);

                            rg.GenerateOutputPDF(pr.report, savetopath);
                            oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                        }

                        //}
                    }

                    translateRadMessageBox trr = new translateRadMessageBox();
                    if (trr.translateAllMessageBoxDialog("Change status ?", " ") == System.Windows.Forms.DialogResult.Yes)
                    {
                        bUpdateAfterPrinting = true;
                        
                    }
                    oMailItem.Display(true);

                    if(bUpdateAfterPrinting == true)
                    {
                        model.idInvoiceStatus = 2;
                    }


                    ddlStatus.SelectedValue = (int)model.idInvoiceStatus;
                    disableAllIfStatusIsntInProccessing((int)model.idInvoiceStatus);

                    if (model.idInvoiceStatus > 1)
                    {
                        btnInvoice.Enabled = false;
                    }

                    txtDaily.Visible = false;
                    btnDaily.Visible = false;

                    isMessage = false;  // ubaceno da ne prikazuje Saved poruku...
                    btnInvoice.PerformClick();
                    isMessage = true;
                    
                    

                    foreach (var t in _tempFolders)
                    {
                        if (Directory.Exists(t))
                            Directory.Delete(t, true);
                    }
                }
                catch (Exception objEx)
                {
                    RadMessageBox.Show(objEx.ToString());
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }


       bool bUpdateAfterPrinting = false;
       void outlookApp_ItemSend(object Item, ref bool Cancel)
       {
            try
            {
                if (Item is Microsoft.Office.Interop.Outlook.MailItem)
                {
                    if (model.idInvoiceStatus == 6 || model.idInvoiceStatus == 1)
                    {
                        Microsoft.Office.Interop.Outlook.MailItem item = (Microsoft.Office.Interop.Outlook.MailItem)Item;
                        item.Save();

                        if (bUpdateAfterPrinting == true)
                        {
                            //model.idInvoiceStatus++;
                            //ddlStatus.SelectedValue = model.idInvoiceStatus;
                            InvoiceBUS inbus = new InvoiceBUS();
                            inbus.UpdateStatus(2, model.idInvoice, this.Name, Login._user.idUser);

                            if (model.invoiceRbr == "001")
                            {
                                InvoiceModel ium = new InvoiceModel();
                                ium = inbus.GetInvoiceByInvoiceAndExtension(model.invoiceNr, "000");
                                if (ium != null)
                                {
                                    inbus.UpdateStatus(2, ium.idInvoice, this.Name, Login._user.idUser);
                                    model.idInvoiceStatus = 2;
                                    
                                }
                            }
                            else
                            {
                                if (model.invoiceRbr == "000")
                                {
                                    InvoiceModel ium = new InvoiceModel();
                                    ium = inbus.GetInvoiceByInvoiceAndExtension(model.invoiceNr, "001");
                                    if (ium != null)
                                    {
                                        inbus.UpdateStatus(2, ium.idInvoice, this.Name, Login._user.idUser);
                                        model.idInvoiceStatus = 2;
                                        //ddlStatus.SelectedValue = (int)model.idInvoiceStatus;
                                        //disableAllIfStatusIsntInProccessing((int)model.idInvoiceStatus);
                                    }
                                }
                            }

                            DocumentsBUS sbus = new DocumentsBUS();
                            PersonEmailBUS emailbus = new PersonEmailBUS();

                            string locationOnDisk = MainForm.DocumentsFolder + "\\" + item.EntryID + ".msg";

                            if (!File.Exists(locationOnDisk))
                                item.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                            //model.idInvoiceStatus
                            if (idTraveler != 0)
                            {
                                DocumentsModel docmodel = new DocumentsModel();
                                docmodel.idContPers = idTraveler;
                                docmodel.idClient = 0;
                                docmodel.descriptionDocument = "Emailed Invoice PDF";
                                docmodel.fileDocument = item.EntryID + ".msg";
                                docmodel.typeDocument = "EML";
                                docmodel.idDocumentStatus = 2;
                                docmodel.idEmployee = 0;
                                docmodel.idResponsableEmployee = 0;
                                docmodel.inOutDocument = 2;
                                docmodel.noteDocument = "Sent Email";
                                docmodel.idArrangement = am.idArrangement;
                                //model.id

                                docmodel.dtCreated = DateTime.Now;
                                docmodel.dtModified = DateTime.Now;
                                docmodel.userCreated = Login._user.idUser;
                                docmodel.userModified = Login._user.idUser;

                                sbus.Save(docmodel, this.Name, Login._user.idUser);
                            }
                        }
                                                                                                                                   
                        Cancel = false;
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cancel = true;
            }
        }

       private PrintReport printFirstandLastPaymentWithoutWindow(Boolean is999)
       {
           PrintReport return_report = null;

           InvoiceBUS ib001 = new InvoiceBUS();
           DataTable invoice = new DataTable();
           DataTable itemsInv = new DataTable();
           if (is999 == false || (model.invoiceRbr == "000" && model.brutoAmount < 0))
               invoice = ib001.GetReportInvoiceByIntID(model.idInvoice);
           else
               invoice = ib001.GetReportInvoiceByIntID999(model.idInvoice);


           DataTable items000 = new DataTable();
           DataTable items001 = new DataTable();
           InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
           if (invoice != null)
           {
               if (invoice.Rows.Count > 0)
               {
                   DataRow dr = invoice.Rows[0];
                   if (dr["idContPerson"].ToString() == "")
                       dr["idContPerson"] = model.idContPerson;

                   if (dr["street"].ToString() == "")
                       dr["street"] = txtAddress.Text;
                   if (dr["houseNr"].ToString() == "")
                       dr["houseNr"] = txtHouseNr.Text;
                   if (dr["extend"].ToString() == "")
                       dr["extend"] = txtExtension.Text;
                   if (dr["zip"].ToString() == "")
                       dr["zip"] = txtZip.Text;
                   if (dr["City"].ToString() == "")
                       dr["City"] = txtCity.Text;
                   if (dr["country"].ToString() == "")
                       dr["country"] = txtCountry.Text;
                   //    if (dr["namePerson"].ToString() == "")
                   dr["namePerson"] = txtName.Text;

                   //==
                   dr["arrName"] = txtNameArr.Text;
                   dr["noDays"] = txtNoDays.Text;
                   dr["boarding"] = txtBoarding.Text;
                   dr["dateFrom"] = txtDateFrom.Text;
                   dr["dateTo"] = txtDateTo.Text;
                   dr["service"] = txtHotelService.Text;


                   itemsInv = rptiib.GetReportInvoiceItemsByID(model.idInvoice, Login._user.lngUser);

                   InvoiceModel im000 = new InvoiceModel();
                   im000 = ib001.GetInvoiceByInvoiceAndExtension999(model.invoiceNr, "000", is999);
                   if (im000 != null)
                       if (im000.idInvoice != null && im000.idInvoice != 0)
                       {
                           items000 = rptiib.GetReportInvoiceItemsByID(im000.idInvoice, Login._user.lngUser);

                           dr["firstAmount"] = im000.brutoAmount;
                           dr["firstReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                           dr["restAmount"] = im000.brutoAmount;
                           dr["restReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                       }

                   InvoiceModel im001 = new InvoiceModel();
                   im001 = ib001.GetInvoiceByInvoiceAndExtension999(model.invoiceNr, "001", is999);
                   if (im001.idInvoice != null && im001.idInvoice != 0)
                   {
                       items001 = rptiib.GetReportInvoiceItemsByID(im001.idInvoice, Login._user.lngUser);
                       if (im000 != null)
                       {
                           dr["firstAmount"] = im000.brutoAmount;
                           dr["firstReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                       }
                       else
                       {
                           dr["dtFirstPay"] = DBNull.Value;
                       }
                       dr["restAmount"] = im001.brutoAmount;
                       dr["restReference"] = im001.idContPerson + "/" + im001.invoiceNr + "-" + im001.invoiceRbr;
                       dr["noteInvoice"] = im001.noteInvoice;
                   }
                   decimal ukupno = Convert.ToDecimal(im001.brutoAmount);
                   if (im000 != null)
                   {
                       ukupno = Convert.ToDecimal(im000.brutoAmount) + Convert.ToDecimal(im001.brutoAmount);
                   }
                   dr["netoAmount"] = ukupno;
               }
               // cita grid za putnike
               ArrangementBookPersonsDAO bus = new ArrangementBookPersonsDAO();
               items000 = bus.GetAllTravelersInvoicing(abm.idArrangementBook, true); // ubaceno za osobe za koje se placa
               //items000 = bus.GetAllTravelersWith(Convert.ToInt32(model.idContPerson), abm.idArrangement);

               if (items000 == null)
                   items000 = new DataTable();

               if (items001 == null)
                   items001 = new DataTable();

               if (invoice == null)
               {
                   translateRadMessageBox tr = new translateRadMessageBox();
                   tr.translateAllMessageBox("Nothing for printing!");
               }
               else if (invoice.Rows.Count <= 0)
               {
                   translateRadMessageBox tr = new translateRadMessageBox();
                   tr.translateAllMessageBox("Nothing for printing!");
               }
               else
               {
                   //frmInvoiceOneReport fmOne = new frmInvoiceOneReport(invoice, items001, items000, nameFileToSend, idLabel, true, true);
                  // fmOne.ShowDialog();
                   if (model.invoiceRbr == "000" && model.brutoAmount < 0)
                       items001 = rptiib.GetReportInvoiceItemsByIDAll(model.idInvoice, Login._user.lngUser); 
                    
                   return_report = new PrintReport(invoice, items001, items000, nameFileToSend, idLabel, false, false, false);
                   //return_report.Run();
               }
           }
           else
           {
               translateRadMessageBox tr = new translateRadMessageBox();
               tr.translateAllMessageBox("Nothing for printing!");
           }

           return return_report;
       }


        #endregion Buttons

        private void gridItems_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (isLoaded != false)
            {

                InvoiceItemsModel iim = new InvoiceItemsModel();
                if (gridItems.CurrentRow != null)
                    iim = (InvoiceItemsModel)gridItems.CurrentRow.DataBoundItem;
                if (iim != null)
                    iim.itemSum = iim.quantity * iim.price;
                if (txtInvoiceRbr.Text != "999")
                {
                    decimal sumCancIns = 0;
                    sumCancIns = sumCancelInsurance();
                    if (iim1 != null)
                        if (iim1.Count > 0)
                        {
                            InvoiceItemsModel iimodel = new InvoiceItemsModel();
                            iimodel = iim1.SingleOrDefault(s => s.idArtical == "Cancel insurance");
                            if (iimodel != null)
                            {
                                int index = 0;
                                index = iim1.IndexOf(iimodel);
                                iim1[index].price = Math.Round(sumCancIns, 2);
                                iim1[index].itemSum = iim1[index].price * iim1[index].quantity;
                                if (gridItems.Rows.Count > index)
                                    gridItems.Rows[index].InvalidateRow();
                            }

                        }
                }

                if (gridItems.CurrentRow != null)
                    gridItems.CurrentRow.InvalidateRow();
                //payments();


                countTotal();
            }

        }
        // Reis
        // Reisverzekering
        //Annuleringsverzekering
        //Groepsgeld
        //Bijdrage calamiteitenfonds
        //Reserveringskosten

        private void countTotal()
        {
            if (gridItems.Rows.Count > 0)
            {
                decimal totalLines = 0;
                for (int i = 0; i < gridItems.Rows.Count; i++)
                {
                    if (gridItems.Rows[i].Cells["quantity"].Value != null && gridItems.Rows[i].Cells["price"].Value != null)
                        totalLines = totalLines + Convert.ToDecimal(gridItems.Rows[i].Cells["quantity"].Value.ToString()) * Convert.ToDecimal(gridItems.Rows[i].Cells["price"].Value.ToString());
                }
                txtBrutoAmt.Text = totalLines.ToString();
            }
        }

        private decimal sumCancelInsurance()
        {

            decimal polisCost = 0;
            decimal travelIns = 0;
            decimal paketPrice = 0;
            decimal sumCancIns = 0;
            if (iim1 != null)
            {
                for (int w = 0; w < iim1.Count; w++)
                {
                    if (iim1[w].idArtical != "Insurance" && iim1[w].idArtical != "Cancel insurance" && iim1[w].idArtical != "First payment" && iim1[w].idArtical != "Money group" & iim1[w].idArtical != "Calamitait Fond" & iim1[w].idArtical != "Reservation cost")
                        paketPrice = paketPrice + Convert.ToDecimal(iim1[w].price) * Convert.ToDecimal(iim1[w].quantity);  // + extra articals 
                }
            }

            travelIns = Convert.ToDecimal(paketPrice) / 100 * Convert.ToDecimal("5,50");
            polisCost = travelIns / 100 * 21;
            sumCancIns = polisCost + travelIns;
            return sumCancIns;
        }

        private void cancelInsurance()
        {
            if (model.invoiceRbr != "999")
            {
                ArrangementBookBUS abp = new ArrangementBookBUS();
                ArrangementBookModel amp = new ArrangementBookModel();
                amp = abp.GetArrangementBook(Convert.ToInt32(model.idVoucher));
                if (amp == null)
                {
                    return;

                }
                else
                {
                    decimal calamitait = 0;
                    decimal moneyGroup = 0;
                    decimal sumCancIns = 0;
                    int quantity = 1;
                    
                    sumCancIns = sumCancelInsurance();
                    
                    // }
                    if (amp.isCancelInsurance == true)
                    {
                        if (iim1 != null)
                        {
                            if (iim1.Count > 0)
                            {
                                for (int i = 0; i < iim1.Count; i++)
                                {
                                    if (iim1[i].idArtical == "Cancel insurance")
                                    {
                                        quantity = new InvoiceBUS().GetNumberCancelInsurance(amp.idArrangementBook);
                                      
                                        iim1[i].quantity = quantity; //1;
                                        iim1[i].price = Math.Round(sumCancIns, 2)/quantity;
                                        iim1[i].itemSum = Math.Round(sumCancIns, 2);
                                    }
                                }
                            }
                            else
                            {

                                // secondItems = new List<InvoiceItemsModel>();
                                InvoiceItemsModel secondOne = new InvoiceItemsModel();
                                secondOne.idArtical = "Cancel insurance";
                                secondOne.idInvoice = model.idInvoice;
                                quantity = new InvoiceBUS().GetNumberCancelInsurance(amp.idArrangementBook);
                                secondOne.quantity = quantity;
                                secondOne.price = Math.Round(sumCancIns, 2) / quantity;
                                secondOne.itemSum = Math.Round(sumCancIns, 2);
                                secondOne.isCancelationIns = true;
                                secondOne.isSecondGrid = false;

                                iim1.Add(secondOne);

                                if (moneyGroup != 0)
                                {
                                    secondOne = new InvoiceItemsModel();
                                    secondOne.idArtical = "Money group";
                                    secondOne.idInvoice = model.idInvoice;
                                    secondOne.quantity = 1;
                                    secondOne.price = moneyGroup;
                                    secondOne.isSecondGrid = true;

                                    iim1.Add(secondOne);
                                }
                                if (calamitait != 0)
                                {
                                    secondOne = new InvoiceItemsModel();
                                    secondOne.idArtical = "Calamitait Fond";
                                    secondOne.idInvoice = model.idInvoice;
                                    secondOne.quantity = 1;
                                    secondOne.price = calamitait;
                                    secondOne.isSecondGrid = true;

                                    iim1.Add(secondOne);
                                }
                            }
                        }
                       
                    }
                    //countTotal();
                }
            }
            countTotal();

        }



        private void gridItems_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {

            InvoiceItemsModel iam = new InvoiceItemsModel();
            if (gridItems.CurrentRow!=null)
            if (gridItems.CurrentRow.DataBoundItem != null)
            {
                iam = (InvoiceItemsModel)gridItems.CurrentRow.DataBoundItem;
            }

            if (iam != null)
            {
                //if (iam.idArtical == "Cancel insurance" || iam.idArtical == "Insurance"
                if (iam.idArtical == "Insurance"
                                   || iam.idArtical == "Reis Pakket" || iam.idArtical == "Money group"
                                    || iam.idArtical == "First payment")
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("Can't delete this article!");
                    //  return;
                    e.Cancel = true;
                    return;
                }
            }
            else if (model.idInvoiceStatus >= 2)
            {
                gridItems.AllowDeleteRow = false;

                translateRadMessageBox tr1 = new translateRadMessageBox();
                tr1.translateAllMessageBox("Can't delete lines in this status");
                //  return;
                e.Cancel = true;
                return;

            }
            else
            {
                translateRadMessageBox tr1 = new translateRadMessageBox();
                if (tr1.translateAllMessageBoxDialogYesNo("Do you want to DELETE this line ?", "Delete") == DialogResult.Yes)
                {
                    gridItems.AllowDeleteRow = true;
                    if (gridItems.CurrentRow.DataBoundItem != null)
                    {

                        gridItems.CurrentRow.Delete();
                        //cancelInsurance();
                    }
                    else
                    {
                        translateRadMessageBox tr2 = new translateRadMessageBox();
                        tr2.translateAllMessageBox("No lines for delete");
                        return;
                    }

                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

        }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (model.idInvoiceStatus == 0)
            {
                inbus = new InvoiceBUS();
                inbus.Delete(model.idInvoice, this.Name, Login._user.idUser);
                InvoiceItemsBUS iitbus = new InvoiceItemsBUS();
                iitbus.Delete(model.idInvoice, this.Name, Login._user.idUser);
            }

            this.Close();
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            if (model.idInvoiceStatus == 0)
            {
                  int dtDifferenceDtFromDtInvoice = 0;
                    //ArrangementModel am=new ArrangementModel();
                    if (am != null)
                        if (am.dtFromArrangement != null)
                            dtDifferenceDtFromDtInvoice = Convert.ToInt32(( am.dtFromArrangement-DateTime.Now).TotalDays);
                
                InvoiceBUS busi = new InvoiceBUS();

                decimal facturSum = 0;
                decimal medjuzbir = 0;
                decimal firstAmount = 0;
                decimal restAmount = 0;
                decimal sumCancelInsuranceReservationCosts = 0;
                decimal sumInsGroupCalam = 0;
                for (int i = 0; i < iim1.Count; i++)
                {

                    facturSum = facturSum + Convert.ToDecimal(iim1[i].price) * Convert.ToDecimal(iim1[i].quantity);

                    //if (dtDifferenceDtFromDtInvoice >= 42)
                    if (dtDifferenceDtFromDtInvoice >= am.daysLastPayment)
                    {
                        if (iim1[i].idArtical == "Insurance" || iim1[i].idArtical == "Calamitait Fond" || iim1[i].idArtical == "Money group")
                        {
                            sumInsGroupCalam += Convert.ToDecimal(iim1[i].price) * Convert.ToDecimal(iim1[i].quantity);

                        }
                    }

                    if (iim1[i].idArtical == "Cancel insurance" || iim1[i].idArtical == "Reservation cost")
                    {
                        sumCancelInsuranceReservationCosts += Convert.ToDecimal(iim1[i].price) * Convert.ToDecimal(iim1[i].quantity);
                    }
                }

                medjuzbir = (facturSum - sumCancelInsuranceReservationCosts - sumInsGroupCalam) * am.percentFirstPayment / 100 + sumCancelInsuranceReservationCosts;

                firstAmount = medjuzbir / 1;
                restAmount = facturSum - medjuzbir;

                // ubacuje u postojecu fakturu - prvo placanje
                InvoiceItemsModel secondOne = new InvoiceItemsModel();

                bool isOK;
                //if (dtDifferenceDtFromDtInvoice >= 42)
                if (dtDifferenceDtFromDtInvoice >= am.daysLastPayment)
                {
                    secondOne.idArtical = "First payment";
                    secondOne.idInvoice = model.idInvoice;
                    secondOne.quantity = 1;
                    secondOne.price = -Math.Round(firstAmount, 2);
                    secondOne.isCancelationIns = false;
                    secondOne.isSecondGrid = false;


                    InvoiceItemsBUS addItem = new InvoiceItemsBUS();
                    isOK = addItem.Save(secondOne, this.Name,Login._user.idUser);
                    if (isOK == false)
                    {
                        translateRadMessageBox tr1 = new translateRadMessageBox();
                        tr1.translateAllMessageBox("Error inserted items. Please check!");
                    }
                }
                else
                {
                    isOK = true;
                    medjuzbir = 0;
                }
                // ovde azurira 001 fakturu za - prvo placanje
                model.brutoAmount = Math.Round((decimal)model.brutoAmount, 2) - Math.Round(medjuzbir, 2);
                model.idInvoiceStatus = 1;

                if (dtDifferenceDtFromDtInvoice < am.daysLastPayment)
                //if (dtDifferenceDtFromDtInvoice < 42)
                {
                    //==== ovde ide  datum fakture + broj dana iz arranzmana za prvo placanje
                    //DateTime daysFirst1 = dpInvoice.Value.AddDays(Convert.ToInt32(am.daysFirstPayment));    //Convert.ToDateTime(invmodel.dtInvoice)
                    //model.dtLastPay = daysFirst1;   //model.dtFirstPay;

                    model.dtLastPay = model.dtFirstPay;
                    model.dtValuta = model.dtFirstPay;
                }

                ////001
                if (model.dtFirstPay > model.dtLastPay)
                    model.dtFirstPay = model.dtLastPay;

                isOK = busi.Update(model, this.Name, Login._user.idUser);

                if (isOK == false)
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("Error updating invoice. Please check!");

                }

                isOK = false;
                // == puni slog za prvo placanje
                InvoiceModel invmodel;
                invmodel = new InvoiceModel();
                //if (dtDifferenceDtFromDtInvoice >= 42)
                if (dtDifferenceDtFromDtInvoice >= am.daysLastPayment)
                {
                    invmodel.invoiceNr = model.invoiceNr;
                    invmodel.invoiceRbr = "000";
                    invmodel.idInvoiceStatus = 1;
                    invmodel.dtInvoice = DateTime.Now;
                    DateTime daysFirst = Convert.ToDateTime(invmodel.dtInvoice).AddDays(Convert.ToInt32(am.daysFirstPayment));
                    DateTime daysLastPay = am.dtFromArrangement.AddDays(-Convert.ToInt32(am.daysLastPayment));
                    invmodel.dtValuta = daysFirst;        //.AddDays(nodays);  // adding payments days
                    invmodel.idContPerson = model.idContPerson;
                    invmodel.idVoucher = model.idVoucher;
                    invmodel.netoAmount = model.netoAmount;
                    invmodel.noteInvoice = model.noteInvoice;

                    ////000

                    invmodel.dtFirstPay = daysFirst; //Convert.ToDateTime("1999-01-01");
                    invmodel.dtLastPay = daysLastPay; //Convert.ToDateTime("1999-01-01");
                    string codeProject = "";
                    if (am != null)
                        if (am.codeProject != "")
                            codeProject = " - " + am.codeProject;
                    AccDebCreModel acdb = new AccDebCreModel();
                    if (am.idClientInvoice != null)
                        if (am.idClientInvoice != 0)
                        {
                            acdb = new AccDebCreBUS().GetClientDebCre(am.idClientInvoice);
                        }
                        else
                            acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(invmodel.idContPerson));
                    else
                        acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(invmodel.idContPerson));

                    string debitorAccount = "";
                    if (acdb != null)
                        if (acdb.debAccount != "")
                            debitorAccount = " - " + acdb.accNumber;


                    if (invmodel.invoiceRbr == "000")
                    {
                        if (invmodel.brutoAmount >= 0)
                            invmodel.descriptionInvoice = "Aanbetaling" + codeProject + debitorAccount;
                        else
                            invmodel.descriptionInvoice = "Credit aanbetaling" + codeProject + debitorAccount;
                    }
                    else if (invmodel.invoiceRbr == "001")
                    {
                        if (invmodel.brutoAmount >= 0)
                            invmodel.descriptionInvoice = "Restantbetaling" + codeProject + debitorAccount;
                        else
                            invmodel.descriptionInvoice = "Credit restantbetaling" + codeProject + debitorAccount;
                    }
                    else if (Convert.ToInt32(invmodel.invoiceRbr) >= 002 && Convert.ToInt32(invmodel.invoiceRbr) < 999)
                    {
                        if (invmodel.brutoAmount >= 0)
                            invmodel.descriptionInvoice = "Extra" + codeProject + debitorAccount;
                        else
                            invmodel.descriptionInvoice = "Credit extra" + codeProject + debitorAccount;
                    }
                    else if (invmodel.invoiceRbr == "999")
                        invmodel.descriptionInvoice = "Annulering" + debitorAccount;
                    else
                        invmodel.descriptionInvoice = am.nameArrangement;
                    invmodel.userCreated = Login._user.idUser;
                    invmodel.dtCreated = DateTime.Now;
                    invmodel.brutoAmount = Math.Round(firstAmount, 2);
                    invmodel.roomComment = model.roomComment;
                    isOK = busi.Save(invmodel, this.Name, Login._user.idUser);
                }
                else
                {
                    isOK = true;
                }
                if (isOK == false)
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("Error making frst payment invoice. Please check!");


                }
                int iID = -1;
                InvoiceModel invm = new InvoiceModel();
                invm = busi.GetInvoiceByID(model.invoiceNr);
                if (invm != null)
                    iID = invm.idInvoice;
                //if (dtDifferenceDtFromDtInvoice >= 42)
                if (dtDifferenceDtFromDtInvoice >= am.daysLastPayment)
                {
                    InvoiceItemsModel secondOneF = new InvoiceItemsModel();
                    secondOneF.idInvoice = iID;
                    secondOneF.idArtical = "First payment";
                    secondOneF.quantity = 1;
                    secondOneF.price = Math.Round(firstAmount, 2);
                    secondOneF.isCancelationIns = false;
                    secondOne.isSecondGrid = false;

                    InvoiceItemsBUS addItem2 = new InvoiceItemsBUS();
                    isOK = addItem2.Save(secondOneF, this.Name, Login._user.idUser);
                    if (isOK == false)
                    {
                        translateRadMessageBox tr1 = new translateRadMessageBox();
                        tr1.translateAllMessageBox("Error inserted items. Please check!");

                    }
                }
                
                isInvoiceMaked = true;
                //translateRadMessageBox tr = new translateRadMessageBox();
                //if (tr.translateAllMessageBoxDialog("You want to change the status to ready to print ?", " ") == System.Windows.Forms.DialogResult.Yes)
                //{
                //    if (new InvoiceDAO().UpdateStatus(6, model.idInvoice) == false)
                //    {
                //        translateRadMessageBox tr1 = new translateRadMessageBox();
                //        tr1.translateAllMessageBox("Something went wrong with updating status! Please contact the administrator");
                //    }
                //    else
                //    {
                //        model.idInvoiceStatus = 6;
                //        //if (dtDifferenceDtFromDtInvoice >= 42)
                //        if (dtDifferenceDtFromDtInvoice >= am.daysLastPayment)
                //        {
                //            if (new InvoiceDAO().UpdateStatus(6, iID) == false)
                //            {
                //                translateRadMessageBox tr1 = new translateRadMessageBox();
                //                tr1.translateAllMessageBox("Something went wrong with updating status! Please contact the administrator");
                //            }
                //        }

                //    }
                //}
                //this.Close();


            }
        }

        private void rgvTravelWith_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTravelWith.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTravelWith.Columns[i].HeaderText != null && resxSet.GetString(rgvTravelWith.Columns[i].HeaderText) != null)
                        rgvTravelWith.Columns[i].HeaderText = resxSet.GetString(rgvTravelWith.Columns[i].HeaderText);
                }
            }

            if (rgvTravelWith.Columns != null && rgvTravelWith.Columns.Count > 0)
                rgvTravelWith.Columns["birthdate"].FormatString = "{0: dd/MM/yyyy}";
        }


        private void frmInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (model.idInvoiceStatus == 0)
            {
                inbus = new InvoiceBUS();
                inbus.Delete(model.idInvoice, this.Name, Login._user.idUser);
                InvoiceItemsBUS iitbus = new InvoiceItemsBUS();
                iitbus.Delete(model.idInvoice, this.Name, Login._user.idUser);
            }

            //if (model.idInvoiceStatus == 1)
            //{
            //    translateRadMessageBox tr = new translateRadMessageBox();
            //     if (tr.translateAllMessageBoxDialog("You want to change the status to ready to print ?", " ") == System.Windows.Forms.DialogResult.Yes)
            //     {
            //         if (model.invoiceRbr == "000" || model.invoiceRbr == "001")
            //         {

            //             InvoiceBUS busi = new InvoiceBUS();
            //             List<InvoiceModel> invm = new List<InvoiceModel>();
            //             invm = busi.GetBasicInvoices(model.idInvoice.ToString());

            //             foreach (InvoiceModel i in invm)
            //             {
            //                 if (new InvoiceDAO().UpdateStatus(6, i.idInvoice) == false)
            //                 {
            //                     translateRadMessageBox tr1 = new translateRadMessageBox();
            //                     tr1.translateAllMessageBox("Something went wrong with updating status! Please contact the administrator");
            //                 }
            //             }
            //         }
            //         else if (new InvoiceDAO().UpdateStatus(6, model.idInvoice) == false)
            //         {
            //             translateRadMessageBox tr1 = new translateRadMessageBox();
            //             tr1.translateAllMessageBox("Something went wrong with updating status! Please contact the administrator");
            //         }
            //     }
            //}

            //this.Close();
        }

        private void btnExtraArt_Click(object sender, EventArgs e)
        {
            ArrangementPriceBUS ccentar1 = new ArrangementPriceBUS();
            List<IModel> gmX1 = new List<IModel>();
            InvoiceItemsModel apm = new InvoiceItemsModel();


            gmX1 = ccentar1.GetAllTotalWithExtraInvoice(am.idArrangement, am.minNrTraveler,  true);
            var dlgSave1 = new GridLookupForm(gmX1, "Extra articals");

            if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementPriceModel genmX1 = new ArrangementPriceModel();
                genmX1 = (ArrangementPriceModel)dlgSave1.selectedRow;
                apm = new InvoiceItemsModel();
                // puni grid na totalu
                apm.idArtical = genmX1.idArticle;
                apm.nameArtical = genmX1.nameArticle;
                apm.quantity = genmX1.nrArticle;
                apm.idInvoice = model.idInvoice;
                apm.price = genmX1.pricePerArticle;
                apm.itemSum = genmX1.nrArticle * genmX1.pricePerArticle;
                if (iim1 == null)
                    iim1 = new List<InvoiceItemsModel>();
                if (iim1 != null)
                {
                    for (int i = 0; i < iim1.Count; i++)
                    {

                        if (iim1[i].idArtical == apm.idArtical)
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You allready have that article!");
                            return;
                        }
                    }
                }
                iim1.Add(apm);
                gridItems.DataSource = null;
                gridItems.DataSource = iim1;
                gridItems.Show();

            }
        }

        private void ddlStatus_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (isLoaded == true)
            {
                disableAllIfStatusIsntInProccessing(Convert.ToInt32(ddlStatus.SelectedValue));
                if (ddlStatus.SelectedItem.Value != null)
                    if (ddlStatus.SelectedItem.Value != "")
                        if (Convert.ToInt32(ddlStatus.SelectedItem.Value) == 2)
                        {
                            // btnBooking.Visible = true; zbaceno dugme za knjizenje ... sve ide preko selection invoice
                            btnBooking.Visible = false;
                            if (codeD != null)
                                if (codeD != "")
                                   // btnBooking.Enabled = true;
                                    btnBooking.Enabled = false;
                        }
            }
        }


    }
}
