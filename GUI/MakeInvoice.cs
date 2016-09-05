using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls;
using System.ComponentModel;


namespace GUI
{
    class MakeInvoice
    {
        InvoiceModel invmodel;
        List<InvoiceItemsModel> itemsmodel;
        ArrangementBookModel arrbookmodel;
        public AccSettingsBUS asb;
        public AccSettingsModel asm;
        private bool isOk = false;
        public List<ArrangementInvoicePriceModel> itemsArr;
        public int nrinvoice;
        private int iID = -1;
        private decimal sum = 0;
        private int idLabel;
        private decimal percentPay;
        private int firstDaysPayment;
        private int restDaysPayment;
        private decimal moneyGroup = 0;
        private decimal calamitait = 0;
        private decimal reservationCosts = 0;
        public string client_name;
       


        public bool DoIt1(ArrangementBookModel bookmodel, List<ArrangementInvoicePriceModel> iitems, int label,string nameForm,int idUser)
        {

            int number_quantity;
            int number_quantity_insurance;
            int number_quantity_insuranceMedical;
            int number_quantity_cancelinsurance;
            idLabel = label;
            arrbookmodel = new ArrangementBookModel();
            arrbookmodel = bookmodel;

            ArrangementInvoicePriceBUS aip = new ArrangementInvoicePriceBUS();
            ArrangementInvoicePriceModel aim = new ArrangementInvoicePriceModel();
            ArrangementBUS ab = new ArrangementBUS();
            ArrangementModel am = new ArrangementModel();
            am = ab.GetArrangementById(arrbookmodel.idArrangement);
            if (am != null)
            {
                if (am.daysFirstPayment != 0)
                    firstDaysPayment = Convert.ToInt32(am.daysFirstPayment);
                else
                    return false;
                if (am.daysLastPayment != 0)
                    restDaysPayment = Convert.ToInt32(am.daysLastPayment);
                else
                    return false;
                if (am.percentFirstPayment != 0)
                    percentPay = Convert.ToDecimal(am.percentFirstPayment);
                else
                    return false;
                if (am.reservationCosts != 0)
                    reservationCosts = Convert.ToDecimal(am.reservationCosts);
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("No data in arrangement for pay invoice !");
                return false;
            }
            ArrangementCalculationBUS arcb = new ArrangementCalculationBUS();
            ArrangementCalculationModel arcm = new ArrangementCalculationModel();
            arcm = arcb.GetArrangementCalculation(arrbookmodel.idArrangement);
            if (arcm != null)
            {
                moneyGroup = Convert.ToDecimal(arcm.moneyGroup);
                calamitait = Convert.ToDecimal(arcm.calamiteitenFonds);

            }

            //=============
            ArrangementBookBUS nbb = new ArrangementBookBUS();
            List<ArrangementBookModel> nbm = new List<ArrangementBookModel>();

            nbm = nbb.GetPassingersForInvoicing(arrbookmodel.idArrangement, arrbookmodel.idContPers);
            if (nbm != null)
                if (nbm.Count > 0)
                    number_quantity = nbm.Count + 1; // ako placa za vise onda je broj za koji placa + za njega samog
                else
                    number_quantity = 1;
            else
                number_quantity = 1;

            //=============

            // read parameters
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            // asm = asb.GetAccSettingsByIDWithLabel(DateTime.Now.Year.ToString(), idLabel);
            asm = asb.GetSettingsByID(DateTime.Now.Year.ToString());
            if (asm != null)
            {
                if (asm.defPayCondition != null && asm.defPayCondition != 0)
                {
                    AccPaymentBUS pb = new AccPaymentBUS();
                    AccPaymentModel pm = new AccPaymentModel();
                    //       pm = pb.GetPaymentByID(asm.defPayCondition); 
                }
            }

            InvoiceItemsBUS iib = new InvoiceItemsBUS();

            AccAcountUpdate acm = new AccAcountUpdate();
            nrinvoice = acm.InvoiceNr();
            if (nrinvoice == -1)
            {
                RadMessageBox.Show("Problem with invoice number !!");
                // return;
            }
            invmodel = new InvoiceModel();
            invmodel.invoiceNr = nrinvoice.ToString();
            invmodel.invoiceRbr = "001";
            invmodel.idInvoiceStatus = 0;
            invmodel.dtInvoice = DateTime.Now;
            DateTime daysFirst = Convert.ToDateTime(invmodel.dtInvoice).AddDays(Convert.ToInt32(am.daysFirstPayment));
            DateTime daysLastPay = am.dtFromArrangement.AddDays(-Convert.ToInt32(am.daysLastPayment));
            invmodel.dtValuta = daysFirst;        //.AddDays(nodays);  // adding payments days
            
            invmodel.idContPerson = arrbookmodel.idContPers;
            if(arrbookmodel.idDebitor > 0)
            {
                if(arrbookmodel.typeDebitor == "P")
                {
                    invmodel.idContPerson = arrbookmodel.idDebitor;
                    invmodel.idClient = 0;
                }
                else if(arrbookmodel.typeDebitor == "C")
                {
                    invmodel.idClient = arrbookmodel.idDebitor;
                    invmodel.idContPerson = 0;
                }
                else
                {
                    invmodel.idContPerson = arrbookmodel.idContPers;
                }
            }
            
            
            invmodel.idVoucher = arrbookmodel.idArrangementBook;
            invmodel.netoAmount = arrbookmodel.price;
            invmodel.dtFirstPay = daysFirst; //Convert.ToDateTime("1999-01-01");
            invmodel.dtLastPay = daysLastPay; //Convert.ToDateTime("1999-01-01");

            //////
            if (invmodel.dtFirstPay > invmodel.dtLastPay)
                invmodel.dtFirstPay = invmodel.dtLastPay;


            string txt = "";
            txt = getNote(arrbookmodel.idArrangementBook);

            if(txt!="")
            invmodel.noteInvoice = txt;

            string codeProject = "";
            if (am!= null)
                if (am.codeProject != "")
                    codeProject =  " - " + am.codeProject;
            AccDebCreModel acdb = new AccDebCreModel();
            if (am != null)
                if (am.idClientInvoice != 0)
                {
                    acdb = new AccDebCreBUS().GetClientDebCre(am.idClientInvoice);
                }
                else
                    acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(invmodel.idContPerson));
            else
                acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(invmodel.idContPerson));

            string debitorAccount = "";
            if (acdb!= null)
                if (acdb.debAccount != "")
                    debitorAccount = " - " + acdb.accNumber;


            if (invmodel.invoiceRbr == "000")
            {
                if (invmodel.brutoAmount>=0)
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


            InvoiceBUS inb = new InvoiceBUS();
            isOk = inb.Save(invmodel, nameForm, idUser);


            InvoiceModel invm = new InvoiceModel();
            invm = inb.GetInvoiceByID(invmodel.invoiceNr);
            if (invm != null)
                iID = invm.idInvoice;
            ////==================== za fakturu 001
            itemsArr = new List<ArrangementInvoicePriceModel>();
            ArrangementInvoicePriceBUS aib = new ArrangementInvoicePriceBUS();
            itemsArr = aib.GetInvoicePrice(arrbookmodel.idArrangement);
            itemsmodel = new List<InvoiceItemsModel>();
            //itemsArr = iitems;
            if (itemsArr != null)
            {
                for (int jm = 0; jm < itemsArr.Count; jm++)
                {

                    InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                    itemsmodelone.idInvoice = iID;
                    itemsmodelone.idArtical = itemsArr[jm].idArticle;
                    itemsmodelone.quantity = number_quantity;  //itemsArr[jm].nrArticle;
                    itemsmodelone.price = itemsArr[jm].sellingPrice;
                    //  sum = sum + itemsArr[jm].number * itemsArr[jm].pricePerQuantity;
                    itemsmodel.Add(itemsmodelone);
                }
            }
            //=====================
            number_quantity_insurance = 0;
            number_quantity_insuranceMedical = 0;
            number_quantity_cancelinsurance = 0;

            if (nbm != null)
            {
                if (nbm.Count > 0)
                {
                    for (int r = 0; r < nbm.Count; r++)
                    {
                        if (nbm[r].isInsurance == true)
                        if(nbm[r].isMedicalDevices==true)
                            number_quantity_insuranceMedical++;
                        else
                            number_quantity_insurance++;
                        else
                            if (nbm[r].isCancelInsurance == true)
                                number_quantity_cancelinsurance++;

                    }
                }
            }
            if (arrbookmodel.isInsurance == true)
                if (arrbookmodel.isMedicalDevices == true)
                    number_quantity_insuranceMedical++;
                else
                number_quantity_insurance++;
            if (arrbookmodel.isCancelInsurance == true)
                number_quantity_cancelinsurance++;
            //=====================

            if (arrbookmodel.isInsurance == true || number_quantity_insurance != 0 || number_quantity_insuranceMedical != 0)  // ili ako neko od ostalih ima insurance
            {
                itemsArr = new List<ArrangementInvoicePriceModel>();
                itemsArr = aib.GetInvoicePriceItemsOption(arrbookmodel.idArrangement);
                int quol = number_quantity;
                if (itemsArr != null)
                {
                    for (int jm = 0; jm < itemsArr.Count; jm++)
                    {
                        InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                        itemsmodelone.idInvoice = iID;
                        itemsmodelone.idArtical = itemsArr[jm].idArticle;
                        itemsmodelone.quantity = number_quantity_insurance;      //itemsArr[jm].nrArticle;
                        itemsmodelone.price = itemsArr[jm].sellingPrice;
                        int numberTravelers = 0 ;
                        if(nbm!=null)
                            numberTravelers = nbm.Count;
                         ArrangementTravelInsuranceModel atim = new ArrangementTravelInsuranceModel();
                        ArrangementCalculationModel accm = new ArrangementCalculationModel();
                        accm = new ArrangementCalculationBUS().GetArrangementCalculation(arrbookmodel.idArrangement);
                        ArrangementModel ammm = new ArrangementModel();
                        ammm = ab.GetArrangementById(arrbookmodel.idArrangement);
                        CountryModel cm = new CountryModel();
                        cm = new CountryBUS().GetCountryByID(ammm.countryArrangement);
                        if (arrbookmodel.isMedicalDevices == true && numberTravelers == number_quantity_insuranceMedical-1)
                        {

                            int daysOfTrip = 0;
                            if (ammm != null)
                            {
                                daysOfTrip = Convert.ToInt32((ammm.dtToArrangement - ammm.dtFromArrangement).TotalDays) + 1;
                            }
                            itemsmodelone.quantity = number_quantity_insuranceMedical;
                            itemsmodelone.price = itemsmodelone.price + ((decimal)0.65) * daysOfTrip;
                            itemsmodelone.isMedical = true;
                            if (accm != null && cm != null)
                            {
                                atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, accm.isSport, arrbookmodel.isMedicalDevices);
                                if (atim != null)
                                    itemsmodelone.nameArtical = atim.description;
                            }
                            itemsmodel.Add(itemsmodelone);

                        } 
                        else if (arrbookmodel.isMedicalDevices == true && numberTravelers != number_quantity_insuranceMedical-1)
                        {
                           
                            InvoiceItemsModel itemsmodelone2 = new InvoiceItemsModel();
                            int daysOfTrip = 0;
                            if (am != null)
                            {
                                daysOfTrip = Convert.ToInt32((ammm.dtToArrangement - ammm.dtFromArrangement).TotalDays) + 1;
                            }
                            if (accm != null && cm != null)
                            {
                                atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, accm.isSport, arrbookmodel.isMedicalDevices);
                                if (atim != null)
                                    itemsmodelone.nameArtical = atim.description;
                            }
                            itemsmodelone.price = itemsmodelone.price + ((decimal)0.65) * daysOfTrip;
                            itemsmodelone.quantity = number_quantity_insuranceMedical;
                            itemsmodelone.isMedical = true;
                            itemsmodelone2.idInvoice = iID;
                            itemsmodelone2.idArtical = itemsArr[jm].idArticle;
                            itemsmodelone2.quantity = number_quantity_insurance;      //itemsArr[jm].nrArticle;
                            itemsmodelone2.price = itemsArr[jm].sellingPrice;
                            if (accm != null && cm != null)
                            {
                                atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, accm.isSport, false);
                                if (atim != null)
                                    itemsmodelone2.nameArtical = atim.description;
                            }
                            itemsmodel.Add(itemsmodelone);
                            itemsmodel.Add(itemsmodelone2);
                        }
                        else if (arrbookmodel.isMedicalDevices != true && number_quantity_insuranceMedical>0)
                        {
                            if (accm != null && cm != null)
                            {
                                atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, accm.isSport, arrbookmodel.isMedicalDevices);
                                if (atim != null)
                                    itemsmodelone.nameArtical = atim.description;
                            }
                            
                             InvoiceItemsModel itemsmodelone2 = new InvoiceItemsModel();
                             int daysOfTrip = 0;
                             if (ammm != null)
                             {
                                 daysOfTrip = Convert.ToInt32((ammm.dtToArrangement - ammm.dtFromArrangement).TotalDays) + 1;
                             }
                             itemsmodelone2.price = itemsmodelone.price + ((decimal)0.65) * daysOfTrip;
                             itemsmodelone2.idInvoice = iID;
                             itemsmodelone2.idArtical = itemsArr[jm].idArticle;
                             itemsmodelone2.quantity = number_quantity_insuranceMedical;      //itemsArr[jm].nrArticle;
                             itemsmodelone2.price = itemsArr[jm].sellingPrice;
                             if (accm != null && cm != null)
                             {
                                 atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, accm.isSport, true);
                                 if (atim != null)
                                     itemsmodelone2.nameArtical = atim.description;
                             }
                             itemsmodelone2.isMedical = true;
                            //  sum = sum + itemsArr[jm].number * itemsArr[jm].pricePerQuantity;
                            itemsmodel.Add(itemsmodelone2);
                            itemsmodel.Add(itemsmodelone);

                        }
                        else
                        {
                            if (accm != null && cm != null)
                            {
                                atim = new ArrangementInsuranceBUS().GetArrangementTravelInsuranceWithMedical(cm.premie, accm.isSport, true);
                                if (atim != null)
                                    itemsmodelone.nameArtical = atim.description;
                            }
                            itemsmodel.Add(itemsmodelone);
                        }
                       
                    }
                }

            }

            decimal sumCancIns = 0;
            if (arrbookmodel.isCancelInsurance == true || number_quantity_cancelinsurance != 0) // ako neko drugi ima cancel insurance
            {
                int quol = number_quantity;
                decimal paketPrice = 0;
                decimal travelIns = 0;
                decimal polisCost = 0;

                if (itemsmodel != null)
                {
                    for (int w = 0; w < itemsmodel.Count; w++)
                    {
                        if (itemsmodel[w].idArtical != "Insurance")
                            paketPrice = paketPrice + Convert.ToDecimal(itemsmodel[w].price) * Convert.ToDecimal(itemsmodel[w].quantity);  // + extra articals 
                    }
                }

                travelIns = Convert.ToDecimal(paketPrice) / 100 * Convert.ToDecimal("5,50");
                polisCost = travelIns / 100 * 21;
                sumCancIns = polisCost + travelIns;

                InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                itemsmodelone.idInvoice = iID;
                itemsmodelone.idArtical = "Cancel insurance";
                //if (nbm != null)
                //{
                //    if (nbm.Count > 0)
                //    {
                //        for (int r = 0; r < nbm.Count; r++)
                //        {
                //            if (nbm[r].isInsurance == false)
                //                quol--;
                //        }
                //    }
                //}
                int quantity = 1;
                quantity = new InvoiceBUS().GetNumberCancelInsurance(arrbookmodel.idArrangementBook);
                itemsmodelone.quantity = quantity; //1;
                itemsmodelone.price = sumCancIns / quantity;
                itemsmodelone.itemSum = sumCancIns;
                itemsmodelone.isSecondGrid = false;
                itemsmodelone.isCancelationIns = true;

                itemsmodel.Add(itemsmodelone);
            }
            if (moneyGroup != 0)
            {
                InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                itemsmodelone.idArtical = "Money group";
                itemsmodelone.idInvoice = iID;
                itemsmodelone.quantity = number_quantity;  //1;
                itemsmodelone.price = moneyGroup;
                itemsmodelone.isSecondGrid = false;

                itemsmodel.Add(itemsmodelone);
            }
            if (calamitait != 0)
            {
                InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                itemsmodelone.idArtical = "Calamitait Fond";
                itemsmodelone.idInvoice = iID;
                itemsmodelone.quantity = 1;  // 1;
                itemsmodelone.price = calamitait;
                itemsmodelone.isSecondGrid = false;

                itemsmodel.Add(itemsmodelone);
            }
            InvoiceItemsModel itemsmodelone3 = new InvoiceItemsModel();
            itemsmodelone3.idArtical = "Reservation cost";
            itemsmodelone3.idInvoice = iID;
            itemsmodelone3.quantity = 1;  //1;
            if (am.reservationCosts != null)
                itemsmodelone3.price = am.reservationCosts;
            else
                itemsmodelone3.price = 0;
            itemsmodelone3.isSecondGrid = false;

            itemsmodel.Add(itemsmodelone3);

            List<int> travelers = new List<int>();
            if (nbm != null)
                if (nbm.Count > 0)
                    for (int t = 0; t < nbm.Count; t++)
                        travelers.Add(nbm[t].idArrangementBook);
            travelers.Add(arrbookmodel.idArrangementBook);

            List<ArrangementPriceModel> apm = new List<ArrangementPriceModel>();
            apm = new ArrangementPriceBUS().GetExtraAccomodation(arrbookmodel.idArrangement, travelers, am.minNrTraveler);
            if (apm != null)
                foreach (ArrangementPriceModel apmodel in apm)
                {
                    int numberExtra = 1;
                    InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                    itemsmodelone.idArtical = apmodel.idArticle;
                    itemsmodelone.idInvoice = iID;
                    numberExtra = new ArticalBUS().numberExtraArticle(travelers, arrbookmodel.idArrangement,am.minNrTraveler,apmodel.idArticle, apmodel.pricePerArticle);
                    itemsmodelone.quantity = numberExtra;
                    itemsmodelone.price = apmodel.pricePerArticle;
                    itemsmodelone.isSecondGrid = false;
                    itemsmodel.Add(itemsmodelone);
                }

            List<ArticalExtraOptionalModel> aeom = new List<ArticalExtraOptionalModel>();
           

            aeom = new ArticalBUS().GetArrangementBookOptionalForTravelers(travelers);
            if (aeom != null)
                foreach (ArticalExtraOptionalModel apmodel in aeom)
                {
                    int numberOptional = 1;
                    InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                    itemsmodelone.idArtical = apmodel.idArticle;
                    itemsmodelone.idInvoice = iID;
                    numberOptional = new ArticalBUS().numberOptionalArticle(travelers, apmodel.idArticle, apmodel.sellingPrice);
                    itemsmodelone.quantity = numberOptional;
                    itemsmodelone.price = apmodel.sellingPrice;
                    itemsmodelone.isSecondGrid = false;
                    itemsmodel.Add(itemsmodelone);
                }


            decimal amt = 0;
            for (int jn = 0; jn < itemsmodel.Count; jn++)
            {
                amt = amt + Convert.ToDecimal(itemsmodel[jn].price) * Convert.ToDecimal(itemsmodel[jn].quantity);
            }
            InvoiceModel invm4 = new InvoiceModel();
            invm4 = inb.GetInvoiceByInvoiceAndExtension(invmodel.invoiceNr, "001");
            if (invm4 != null)
                iID = invm4.idInvoice;

            if (invm4 != null)
            {
                invm4.brutoAmount = sum;
                isOk = inb.Update(invmodel, nameForm, idUser);
            }
            //=============================
            for (int jp = 0; jp < itemsmodel.Count; jp++)
            {
                isOk = iib.Save(itemsmodel[jp], nameForm,idUser);
                if (isOk == false)
                {
                    RadMessageBox.Show("Error writing items !!");
                    //return;
                }

            }
            return isOk;
        }

        private string getNote(int idArrangementBook)
        {
            ArrangementBookPersonsBUS bus = new ArrangementBookPersonsBUS();
            BindingList<ArrangementTravelersInvoiceModel> travelmod = new BindingList<ArrangementTravelersInvoiceModel>();
            travelmod = bus.GetAllTravelersInvoicing(idArrangementBook, false);

            if (travelmod == null)
                travelmod = new BindingList<ArrangementTravelersInvoiceModel>();


            string txtText = "";

            string rowOne = "";
            string rowTwo = "";
            string rowThree = "Uw contractnummer bij de Europeesche Verzekering is 406/415/453.3429840";
            txtText = Environment.NewLine + @"Op deze overeenkomst zijn de ANVR-Reisvoorwaarden en de garantie van het Calamiteitenfonds van toepassing"
             + @". Op deze overeenkomst is tevens de SGR-garantieregeling van toepassing. U kunt de voorwaarden vinden op www.sgr.nl/garantieregeling"
             + @". Op verzoek stuurt SGR deze voorwaarden kosteloos toe"
            + @". Op onze facturen is de bijzondere regeling reisbureaus van toepassing.";


            // trazi isurance i cancel insurance za ljude u malom gridu
            ArrangementBookBUS apb = new ArrangementBookBUS();
            ArrangementBookModel apm = new ArrangementBookModel();


            for (int w = 0; w < travelmod.Count; w++)
            {
                apm = new ArrangementBookModel();
                apm = apb.GetTravelerIsInsurance(new ArrangementBookBUS().GetArrangementBook(idArrangementBook).idArrangement, travelmod[w].idContPers);  //travelmod[w].idTravelWithPerson
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
                txtText = rowOne + ". " + rowTwo + ". " + rowThree + Environment.NewLine + txtText;
            else if (rowOne != "" && rowTwo == "")
                txtText = rowOne + ". " + rowThree + Environment.NewLine + txtText;
            else if (rowTwo != "" && rowOne == "")
                txtText = rowTwo + Environment.NewLine + txtText;


            return txtText;

        }

        public int DoItBlank(ArrangementBookModel bookmodel, int rbr, string invNumber, int label,string nameForm,int idUser)
        {
            idLabel = label;
            ArrangementInvoicePriceBUS aip = new ArrangementInvoicePriceBUS();
            ArrangementInvoicePriceModel aim = new ArrangementInvoicePriceModel();
           

            InvoiceItemsBUS iib = new InvoiceItemsBUS();
            arrbookmodel = new ArrangementBookModel();
            arrbookmodel = bookmodel;
            AccAcountUpdate acm = new AccAcountUpdate();
            invmodel = new InvoiceModel();
            invmodel.invoiceNr = invNumber;
            int em = rbr + 1;
            string cc = em.ToString();
            invmodel.invoiceRbr = cc.PadLeft(3, '0');
            invmodel.idInvoiceStatus = 1;
            invmodel.dtInvoice = DateTime.Now;
            invmodel.dtValuta = DateTime.Now;
            invmodel.idContPerson = arrbookmodel.idContPers;
            if (arrbookmodel.idDebitor > 0)
            {
                if (arrbookmodel.typeDebitor == "P")
                {
                    invmodel.idContPerson = arrbookmodel.idDebitor;
                    invmodel.idClient = 0;
                }
                else if (arrbookmodel.typeDebitor == "C")
                {
                    invmodel.idClient = arrbookmodel.idDebitor;
                    invmodel.idContPerson = 0;
                }
                else
                {
                    invmodel.idContPerson = arrbookmodel.idContPers;
                }
            }
            
            invmodel.idVoucher = arrbookmodel.idArrangementBook;
            invmodel.dtFirstPay = Convert.ToDateTime("1999-01-01");
            invmodel.dtLastPay = Convert.ToDateTime("1999-01-01");

            ArrangementModel am = new ArrangementModel();
            am = new ArrangementBUS().GetArrangementById(arrbookmodel.idArrangement);

            string txt = "";
            txt = getNote(arrbookmodel.idArrangementBook);

            if (txt != "")
                invmodel.noteInvoice = txt;

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

            InvoiceBUS inb = new InvoiceBUS();
            isOk = inb.Save(invmodel, nameForm, idUser);

            itemsmodel = new List<InvoiceItemsModel>();
            int number_quantity_cancelinsurance = 0;
           

            InvoiceModel invm = new InvoiceModel();
            invm = inb.GetInvoiceByID(invmodel.invoiceNr);
            if (invm != null)
            {
                iID = invm.idInvoice;
                if (arrbookmodel.isCancelInsurance == true)
                    number_quantity_cancelinsurance++;

                decimal sumCancIns = 0;
                if (arrbookmodel.isCancelInsurance == true || number_quantity_cancelinsurance != 0) // ako neko drugi ima cancel insurance
                {
                    InvoiceItemsModel itemsmodelone = new InvoiceItemsModel();
                    itemsmodelone.idInvoice = iID;
                    itemsmodelone.idArtical = "Cancel insurance";
                    itemsmodelone.quantity = number_quantity_cancelinsurance; //1;
                    itemsmodelone.price = sumCancIns;
                    itemsmodelone.isSecondGrid = false;
                    itemsmodelone.isCancelationIns = true;

                    itemsmodel.Add(itemsmodelone);
                }

                for (int jp = 0; jp < itemsmodel.Count; jp++)
                {
                    isOk = iib.Save(itemsmodel[jp], nameForm,idUser);
                    if (isOk == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error writing items !!");
                    }

                }
            }
            else
                iID = 0;
            return iID;
        }

        public int DoCancel(int rbr,string nameForm,int idUser)
        {
            int invoice;
            invoice = rbr;
            int idCC = -1;
            bool iiOk = false;
            int iIDnew = 0;
            InvoiceBUS ib = new InvoiceBUS();
            InvoiceModel im = new InvoiceModel();
            InvoiceItemsBUS iib = new InvoiceItemsBUS();
            List<InvoiceItemsModel> iim = new List<InvoiceItemsModel>();

            im = ib.GetInvoiceByIntID(invoice);
            if (im == null)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("No Invoice to cancel !");
                return idCC;
            }
            else
            {
                iim = iib.GetInvoiceItemsByID(im.idInvoice, Login._user.lngUser);
                if (iim == null || iim.Count == 0)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("No items for invoice !");
                    return idCC;
                }
                decimal newSum = 0;
                for (int i = 0; i < iim.Count; i++)
                {
                    newSum = newSum + Convert.ToDecimal(iim[i].price) * Convert.ToDecimal(iim[i].quantity);
                }

                //shimmy pocetak
                //trazi poslednji invoice rbr
                List<InvoiceModel> invoicelistazanoviRBR = new List<InvoiceModel>();
                invoicelistazanoviRBR = ib.GetAllInvoicesByVoucher((int)im.idVoucher);
                int poslednjiBroj = 0;
                if(invoicelistazanoviRBR != null)
                {
                    foreach(var item in invoicelistazanoviRBR)
                    {
                        int invoicerbr = Int32.Parse(item.invoiceRbr);

                        if (invoicerbr != 999 && invoicerbr > poslednjiBroj)
                            poslednjiBroj = invoicerbr;
                    }
                }
                // shimmy kraj

                // upis nove fakture
                InvoiceModel cancmodel = new InvoiceModel();
                cancmodel.invoiceNr = im.invoiceNr;
                //cancmodel.invoiceRbr = im.invoiceRbr;            //"9"+ im.invoiceRbr.Substring(1);

                poslednjiBroj = poslednjiBroj + 1;
                string cc = poslednjiBroj.ToString();
                cancmodel.invoiceRbr = cc.PadLeft(3, '0');

                cancmodel.isBooked = false;
                cancmodel.idVoucher = im.idVoucher;
                cancmodel.idInvoiceStatus = 2;
                cancmodel.idContPerson = im.idContPerson;
                cancmodel.brutoAmount = -newSum;  // iznos u minusu;
                cancmodel.noteInvoice = im.noteInvoice;
                cancmodel.idClient = im.idClient;

                ArrangementModel am = new ArrangementModel();
                am = new ArrangementBUS().GetArrangementById(new ArrangementBookBUS().GetArrangementBook(Convert.ToInt32(im.idVoucher)).idArrangement);

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
                        acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(cancmodel.idContPerson));
                else
                    acdb = new AccDebCreBUS().GetPersonDebCre(Convert.ToInt32(cancmodel.idContPerson));

                string debitorAccount = "";
                if (acdb != null)
                    if (acdb.debAccount != "")
                        debitorAccount = " - " + acdb.accNumber;


                if (cancmodel.invoiceRbr == "000")
                {
                    if (cancmodel.brutoAmount >= 0)
                        cancmodel.descriptionInvoice = "Aanbetaling" + codeProject + debitorAccount;
                    else
                        cancmodel.descriptionInvoice = "Credit aanbetaling" + codeProject + debitorAccount;
                }
                else if (cancmodel.invoiceRbr == "001")
                {
                    if (cancmodel.brutoAmount >= 0)
                        cancmodel.descriptionInvoice = "Restantbetaling" + codeProject + debitorAccount;
                    else
                        cancmodel.descriptionInvoice = "Credit restantbetaling" + codeProject + debitorAccount;
                }
                else if (Convert.ToInt32(cancmodel.invoiceRbr) >= 002 && Convert.ToInt32(cancmodel.invoiceRbr) < 999)
                {
                    if (cancmodel.brutoAmount >= 0)
                        cancmodel.descriptionInvoice = "Extra" + codeProject + debitorAccount;
                    else
                        cancmodel.descriptionInvoice = "Credit extra" + codeProject + debitorAccount;
                }
                else if (cancmodel.invoiceRbr == "999")
                    cancmodel.descriptionInvoice = "Annulering" + debitorAccount;
                else
                    cancmodel.descriptionInvoice = am.nameArrangement;


                cancmodel.dtFirstPay = Convert.ToDateTime("1999-01-01");
                cancmodel.dtLastPay = Convert.ToDateTime("1999-01-01");


                iiOk = ib.Save(cancmodel, nameForm, idUser);
                if (iiOk == false)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Error writing Cancel invoice !");
                    return idCC;
                }
                InvoiceModel mi = new InvoiceModel();
                mi = ib.GetInvoiceByInvoiceAndExtension(cancmodel.invoiceNr, cancmodel.invoiceRbr);
                iIDnew = mi.idInvoice;

                List<InvoiceItemsModel> iimod = new List<InvoiceItemsModel>();
                InvoiceItemsModel iimodone = new InvoiceItemsModel(); 
                for (int j = 0; j < iim.Count; j++)
                {
                    iimodone = new InvoiceItemsModel();
                    iimodone.idInvoice = iIDnew;
                    iimodone.idArtical = iim[j].idArtical;
                    iimodone.isSecondGrid = iim[j].isSecondGrid;
                    iimodone.price = -iim[j].price;
                    iimodone.quantity = iim[j].quantity;
                    iimodone.isCancelationIns = iim[j].isCancelationIns;

                    iimod.Add(iimodone);
                }
                for (int jp = 0; jp < iimod.Count; jp++)
                {
                    isOk = iib.Save(iimod[jp], nameForm, idUser);
                    if (isOk == false)
                    {
                        RadMessageBox.Show("Error writing items !!");
                        iib.Delete(iIDnew, nameForm, idUser);
                        ib.Delete(iIDnew, nameForm, idUser);
                        idCC = -1;      //return;
                    }
                    else
                    {
                        idCC = 1;
                    }

                }

            }


            return idCC;
        }
        public int DoDelete(int rbr, string nameForm, int idUser)
        {
            int invoice;
            invoice = rbr;
            int idCC = -1;
            InvoiceBUS ib = new InvoiceBUS();
            InvoiceModel im = new InvoiceModel();
            InvoiceItemsBUS iib = new InvoiceItemsBUS();
            List<InvoiceItemsModel> iim = new List<InvoiceItemsModel>();

            im = ib.GetInvoiceByIntID(invoice);
            if (im == null)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("No Invoice to delete !");
                idCC = 1;
                return idCC;
            }
            else
            {
                if (im.idInvoiceStatus > 1 && im.idInvoiceStatus!=6)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Can't delete invoice with this status !");
                    idCC = 1;
                    return idCC;
                }
                else
                {
                    iim = iib.GetInvoiceItemsByID(im.idInvoice, Login._user.lngUser);
                    if (iim != null && iim.Count != 0)
                    {
                        iib.Delete(invoice,nameForm,idUser);   // brisanje stavki
                    }

                    ib.Delete(invoice, nameForm, idUser);  // brisanje zaglavlja
                    idCC = 1;
                }
            }
            return idCC;

        }
        public bool CancelationInvoice(int idVoucher, DateTime dtCancel,string nameForm,int idUser)
        {
            DateTime cancelDate = new DateTime();
            DateTime dtStart = new DateTime();
            int idArrangement = 0;
            cancelDate = dtCancel;
            decimal sumAmountPerson = 0;
            string invoice = "";
            int Vou = 0;
            Vou = idVoucher;
            InvoiceBUS aib = new InvoiceBUS();
            InvoiceModel imdl = new InvoiceModel();
            if (Vou > 0)
            {
                imdl = aib.GetInvoiceByVoucher(Vou);
                if (imdl == null)
                {
                    RadMessageBox.Show("No Invoice number !!!");
                    return false;
                }
                else
                {
                    invoice = imdl.invoiceNr;
                }
            }
            else
            {
                RadMessageBox.Show("No Voucher number !!!");
                return false;
            }

            if (invoice != "")
                sumAmountPerson = aib.GetSumInvoicePerson(invoice,true); //  dobija sumu faktura za coveka 

            arrbookmodel = new ArrangementBookModel();
           
            decimal cancelAmt = 0;
            ArrangementBookBUS abb = new ArrangementBookBUS();
            ArrangementBookModel amb = new ArrangementBookModel();
            amb = abb.GetArrangementBook(idVoucher);
            if(amb!=null)
            idArrangement = amb.idArrangement;
            ArrangementBUS ab = new ArrangementBUS();
            ArrangementModel am = new ArrangementModel();
            am = ab.GetArrangementById(idArrangement);
            if (am != null)
            {
                dtStart = am.dtFromArrangement;
                if (am.daysFirstPayment != 0)
                    firstDaysPayment = Convert.ToInt32(am.daysFirstPayment);
                else
                    firstDaysPayment = 14;
            }
            ArrangementCalculationBUS arcb = new ArrangementCalculationBUS();
            ArrangementCalculationModel arcm = new ArrangementCalculationModel();
            arcm = arcb.GetArrangementCalculation(arrbookmodel.idArrangement);
            if (arcm != null)
            {
                moneyGroup = Convert.ToDecimal(arcm.moneyGroup);
                calamitait = Convert.ToDecimal(arcm.calamiteitenFonds);

            }

            invmodel = new InvoiceModel();
            invmodel.invoiceNr = invoice;
            invmodel.invoiceRbr = "999";
            invmodel.idInvoiceStatus = 1;
            invmodel.dtInvoice = cancelDate; //DateTime.Now;
            DateTime daysFirst = Convert.ToDateTime(invmodel.dtInvoice); //.AddDays(Convert.ToInt32(am.daysFirstPayment));
            DateTime daysLastPay = daysFirst;
            invmodel.dtValuta = daysFirst;        //.AddDays(nodays);  // adding payments days
            invmodel.idContPerson = imdl.idContPerson;                //arrbookmodel.idContPers;
            invmodel.idClient = imdl.idClient;
            invmodel.idVoucher = Vou;
            cancelAmt = GiveCancelAmount(dtStart, cancelDate,sumAmountPerson,invoice, am);  /// ovde da pozove funkciju ...      //(dtStart, cancelDate, sumAmountPerson, invoice)
            invmodel.netoAmount = arrbookmodel.price;
            invmodel.brutoAmount = cancelAmt;
            invmodel.dtFirstPay = daysFirst; //Convert.ToDateTime("1999-01-01");
            invmodel.dtLastPay = daysLastPay; //Convert.ToDateTime("1999-01-01");
            invmodel.noteInvoice = imdl.noteInvoice; 
            
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


            InvoiceBUS inb = new InvoiceBUS();
            isOk = inb.Save(invmodel, nameForm, idUser);
            if (isOk == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Error saving invoice");
                return false;
            }
            InvoiceModel invm = new InvoiceModel();
            invm = inb.GetInvoiceByID(invmodel.invoiceNr);
            if (invm != null)
                iID = invm.idInvoice;
            else
                iID = 0;

            InvoiceItemsBUS iib1 = new InvoiceItemsBUS();
            List<InvoiceItemsModel> iim = new List<InvoiceItemsModel>();
            iim = iib1.GetInvoiceItemsByInvoiceNrWithPrice(Convert.ToInt32(imdl.invoiceNr), Login._user.lngUser);
            decimal amountCancel = 0;
            decimal amountReservationCosts = 0;

            iib1 = new InvoiceItemsBUS();
            InvoiceBUS ib1 = new InvoiceBUS();
            List<InvoiceItemsModel> iimodoneList = new List<InvoiceItemsModel>();
            InvoiceItemsModel iimodone = new InvoiceItemsModel();
            iimodone = iim.SingleOrDefault(s=>s.idArtical=="Cancel insurance");
            decimal cancelInsuranceAmount = 0;

            if(iimodone!=null)
            {
                if (invoice != "")
                    cancelInsuranceAmount = aib.GetSumCancelInsurance(invoice);

                amountCancel = cancelInsuranceAmount;
                //amountCancel = Convert.ToDecimal(iimodone.price);
                iimodone.price = amountCancel;
                iimodone.quantity = 1;
                iimodone.idInvoice = iID; 
                iimodoneList.Add(iimodone);
            }
            iimodone = iim.SingleOrDefault(s=>s.idArtical=="Reservation cost");
            if(iimodone!=null)
            {   
                amountReservationCosts =Convert.ToDecimal( iimodone.price);
                iimodone.idInvoice = iID;
                iimodoneList.Add(iimodone);
            }

            iimodone = new InvoiceItemsModel();
            
                iimodone = new InvoiceItemsModel();
                iimodone.idInvoice = iID;
                iimodone.idArtical = "Cancellation cost";
                iimodone.isSecondGrid = false;
                iimodone.price = cancelAmt - amountCancel - amountReservationCosts; // isti iznos kao na fakturi -iim[j].price;
                iimodone.quantity = 1;
                iimodone.isCancelationIns = false;

                if (iimodone != null)
                iimodoneList.Add(iimodone);

                 isOk = iib1.SaveItemsTransaction(iimodoneList,nameForm,idUser);
                    if (isOk == false)
                    {
                        RadMessageBox.Show("Error writing items !!");
                        iib1.Delete(iID, nameForm,idUser);
                        ib1.Delete(iID, nameForm, idUser);
                        return false;
                    }
                
                 return true;
            }

        private decimal GiveCancelAmount(DateTime dtStartArr,DateTime dtCancel, decimal arrPrice,string invoiceNr, ArrangementModel am)
            {
                string iDinvoice = invoiceNr;
                int noDays = 0;
                int percent = 0;
                DateTime dateCancel = dtCancel;
                DateTime dateStart  = dtStartArr;
                decimal arrangemetPrice = arrPrice;
                decimal cancelAmount=0;
                noDays = Convert.ToInt32((dtStartArr - dtCancel).TotalDays);
                if (noDays > 42)
                    percent = (int) am.percentFirstPayment;
                    //percent = 15;
                if (noDays <= 42 && noDays >= 29)
                    percent = 35;
                if (noDays <= 28 && noDays >= 22)
                    percent = 40;
                if (noDays <= 21 && noDays >= 15)
                    percent = 50;
                if (noDays <= 14 && noDays >= 6)
                    percent = 75;
                 if (noDays <= 5 && noDays >= 1)
                    percent = 90;
                if (noDays <= 0)
                    percent = 100;
                if (percent != 0)
                {
                    cancelAmount = Math.Round(arrangemetPrice * percent / 100, 2);
                    decimal rest = 0;
                    rest = new InvoiceBUS().GetSumInvoicePerson(invoiceNr, false);
                    cancelAmount = cancelAmount + rest;

                }
                else
                {
                    //InvoiceBUS ib = new InvoiceBUS();
                    //InvoiceModel im = new InvoiceModel();
                    //im = ib.GetInvoiceByInvoiceAndExtension(iDinvoice, "000");
                    //if (im != null)
                    //{
                    //    if (im.invoiceNr == iDinvoice && im.invoiceRbr == "000")
                    //    {
                    //        cancelAmount = Convert.ToDecimal(im.brutoAmount);

                    //    }
                    //    else
                    //    {
                    //        RadMessageBox.Show("No first payment invoice !!");
                    //        cancelAmount = 0;
                    //    }
                    //}
                    
                }


                return cancelAmount;

            }
        public  bool ValidateIban(string bankAccount)
            {
                try
                {
                    bankAccount = bankAccount.ToUpper(); //IN ORDER TO COPE WITH THE REGEX BELOW
                    if (String.IsNullOrEmpty(bankAccount))
                        return false;
                    else if (System.Text.RegularExpressions.Regex.IsMatch(bankAccount, "^[A-Z0-9]"))
                    {
                        bankAccount = bankAccount.Replace(" ", String.Empty);
                        string bank =
                        bankAccount.Substring(4, bankAccount.Length - 4) + bankAccount.Substring(0, 4);
                        int asciiShift = 55;
                        StringBuilder sb = new StringBuilder();
                        foreach (char c in bank)
                        {
                            int v;
                            if (Char.IsLetter(c)) v = c - asciiShift;
                            else v = int.Parse(c.ToString());
                            sb.Append(v);
                        }
                        string checkSumString = sb.ToString();
                        int checksum = int.Parse(checkSumString.Substring(0, 1));
                        for (int i = 1; i < checkSumString.Length; i++)
                        {
                            int v = int.Parse(checkSumString.Substring(i, 1));
                            checksum *= 10;
                            checksum += v;
                            checksum %= 97;
                        }
                        return checksum == 1;
                    }
                    else
                        return false;
                }

               catch(Exception ex)
                {
                    return false;
                }
            }
        public string GiveCustomerName(string aaNumber)
            {
                if (aaNumber == "")
                {
                    client_name = "";
                }
                else
                {
                    AccDebCreBUS dcb = new AccDebCreBUS();
                    ClientBUS cb = new ClientBUS();
                    AccDebCreModel dcm = new AccDebCreModel();
                    ClientModel cm = new ClientModel();
                    dcm = dcb.GetCustomerByAccCode(aaNumber);
                    if (dcm == null)
                        client_name = "";
                    else
                    {
                        if (dcm.idClient == 0)
                            client_name = "";
                        else
                        {
                            cm=cb.GetClient(dcm.idClient);
                            client_name = dcm.nameClient;
                        }
                    }
                }
                

                return client_name;
            }
            
        //}

    }
}

