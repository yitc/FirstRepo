using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Linq;
using System.Resources;

namespace GUI
{
    class AccAcountUpdate
     {
        AccLineModel aclinemodel;
        AccLedgerAmountsModel ledgermodel;
        private bool isOK = false;
        public Int32 per = -1;
        public string daily;
        private string basicKonto;
        public AccSettingsBUS asb;
        public AccSettingsModel asm;
        public AccLineModel linemodelone;
        public int idBTW;
        public string defRaisPriceConto;
        public string defInsuranceConto;
        public string defCancelInsConto;
        public string clientBook;
        public string descriptionLine;
        public string defCalamitait;
        public string defMoneyGroup;
        public string defFirstPayment;
        public string defReservationCost;
        public string debitorReservationAcc;
        public string defLedgerCancelation;
        public int idDaily;
        public int idLabel;
        private bool finish = false;
        private DateTime date130500;
        private string codeArr;
        private int ClientBooking=0;

       
        public Boolean AddAmount(AccLineModel model, string nameForm, int idUser)
        {
           // return true;
            aclinemodel = new AccLineModel();
            aclinemodel = model;

            if (aclinemodel.numberLedAccount.Trim() != "" && aclinemodel.numberLedAccount.Trim() != null)
            {
                AccLedgerAmountsBUS lab = new AccLedgerAmountsBUS();
                ledgermodel = new AccLedgerAmountsModel();

                ledgermodel = lab.GetAmountPerYear(aclinemodel.numberLedAccount.Trim(), aclinemodel.dtLine.Year.ToString());
                if (ledgermodel != null)
                {
                    if (ledgermodel.numberLedgerAccount.Trim() == aclinemodel.numberLedAccount.Trim() && ledgermodel.bookingYear == aclinemodel.dtLine.Year.ToString())
                    {
                        if (aclinemodel.periodLine == 0)   // ako je pocetno stanje
                        {
                            ledgermodel.beginDebit = ledgermodel.beginDebit + aclinemodel.debitLine;
                            ledgermodel.beginCredit = ledgermodel.beginCredit + aclinemodel.creditLine;
                            ledgermodel.transactionsNo = ledgermodel.transactionsNo + 1;
                           
                        }
                        if (aclinemodel.periodLine != 0)  // redovno knjizenje
                        {
                            ledgermodel.debitAmount = ledgermodel.debitAmount + aclinemodel.debitLine;
                            ledgermodel.creditAmount = ledgermodel.creditAmount + aclinemodel.creditLine;
                            ledgermodel.transactionsNo = ledgermodel.transactionsNo + 1;
                            
                        }
                       
                        ledgermodel.userModified = Login._user.idUser;
                        isOK = lab.Update(ledgermodel, nameForm, idUser);
                        if (isOK == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    ledgermodel = new AccLedgerAmountsModel();
                    ledgermodel.numberLedgerAccount = aclinemodel.numberLedAccount;
                    ledgermodel.bookingYear = aclinemodel.dtLine.Year.ToString();

                    if (aclinemodel.periodLine == 0)   // ako je pocetno stanje
                    {
                        ledgermodel.beginDebit = ledgermodel.beginDebit + aclinemodel.debitLine;
                        ledgermodel.beginCredit = ledgermodel.beginCredit + aclinemodel.creditLine;
                        ledgermodel.transactionsNo = ledgermodel.transactionsNo + 1;

                    }
                    if (aclinemodel.periodLine != 0)  // redovno knjizenje
                    {
                        ledgermodel.debitAmount = ledgermodel.debitAmount + aclinemodel.debitLine;
                        ledgermodel.creditAmount = ledgermodel.creditAmount + aclinemodel.creditLine;
                        ledgermodel.transactionsNo = ledgermodel.transactionsNo + 1;

                    }
                    ledgermodel.userCreated = Login._user.idUser;
                    ledgermodel.userModified = 0;
                    ledgermodel.dtModified = Convert.ToDateTime("1900-01-01");
                    isOK = lab.Save(ledgermodel, nameForm, idUser);
                    if (isOK == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    //============================== ako ne postoji ====
                    //return false;
                }
            }
            else
            {
                return false;
            }
        }
             public Boolean SubstractAmount(AccLineModel model, string nameForm, int idUser)
        {
            //return true;

            aclinemodel = new AccLineModel();
            aclinemodel = model;

            if (aclinemodel.numberLedAccount.Trim() != "" && aclinemodel.numberLedAccount.Trim() != null)
            {
                AccLedgerAmountsBUS lab = new AccLedgerAmountsBUS();
                ledgermodel = new AccLedgerAmountsModel();

                ledgermodel = lab.GetAmountPerYear(aclinemodel.numberLedAccount.Trim(), aclinemodel.dtLine.Year.ToString());
                if (ledgermodel != null)
                {
                    if (ledgermodel.numberLedgerAccount.Trim() == aclinemodel.numberLedAccount.Trim() && ledgermodel.bookingYear == aclinemodel.dtLine.Year.ToString())
                    {
                        if (aclinemodel.periodLine == 0)   // ako je pocetno stanje
                        {
                            ledgermodel.beginDebit = ledgermodel.beginDebit - aclinemodel.debitLine;
                            if (ledgermodel.beginDebit < 0)
                                ledgermodel.beginDebit = 0;
                            ledgermodel.beginCredit = ledgermodel.beginCredit - aclinemodel.creditLine;
                            if (ledgermodel.beginCredit < 0)
                                ledgermodel.beginCredit = 0;
                            ledgermodel.transactionsNo = ledgermodel.transactionsNo - 1;
                            if (ledgermodel.transactionsNo < 0)
                                ledgermodel.transactionsNo = 0;

                        }
                        if (aclinemodel.periodLine != 0)  // redovno knjizenje
                        {
                            ledgermodel.debitAmount = ledgermodel.debitAmount - aclinemodel.debitLine;
                            if (ledgermodel.debitAmount < 0)
                                ledgermodel.debitAmount = 0;
                            ledgermodel.creditAmount = ledgermodel.creditAmount - aclinemodel.creditLine;
                            if (ledgermodel.creditAmount < 0)
                                ledgermodel.creditAmount = 0;
                            ledgermodel.transactionsNo = ledgermodel.transactionsNo - 1;
                            if (ledgermodel.transactionsNo < 0)
                                ledgermodel.transactionsNo = 0;

                        }

                        ledgermodel.userModified = Login._user.idUser;
                        isOK = lab.Update(ledgermodel, nameForm, idUser);
                        if (isOK == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public string AccountName(string conto)
        {
            LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();    
                if (conto != null && conto != "")
                {
                    lam = lab.GetAccount(conto,Login._bookyear);
                    if (lam != null)
                    {
                        if (lam.numberLedgerAccount.Trim() == conto.Trim())
                        {
                            return lam.descLedgerAccount;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }

                }
                else
                {
                    return "";
                }
         }
        public Int32 Period(DateTime datum)
        {
            Int32 per = -1;
            DateTime xDate;
            xDate = Convert.ToDateTime(datum);
            AccSettingsBUS acs = new AccSettingsBUS();
            AccSettingsModel sm = new AccSettingsModel();

            sm = acs.GetSettingsByID(Login._bookyear);
            if (sm != null)
            {
                if (sm.yearSettings == Login._bookyear) //DateTime.Now.Year.ToString())
                {
                  
                    int periods = Convert.ToInt32(sm.noPeriods);
                    switch(periods)
                    {
                        case 1:  // Cela godina
                            if (xDate >= Convert.ToDateTime("01-01-" + Login._bookyear) && xDate <= Convert.ToDateTime("31-12-" + Login._bookyear))
                            {
                                per = 1;
                                return per;
                            }
                            else
                            {
                                return -1;
                            }
                            break;
                        case 2:  // 6 meseci
                            if (xDate >= Convert.ToDateTime("01-01-" + Login._bookyear) && xDate <= Convert.ToDateTime("30-06-" + Login._bookyear))
                               {
                                   per = 1;
                                   return per;
                               }
                               else
                               {
                                   per = 2;
                                   return per;
                               }
                            break;
                        case 3:  // 4 meseca
                            if (xDate >= Convert.ToDateTime("01-01-" + Login._bookyear) && xDate <= Convert.ToDateTime("30-04-" + Login._bookyear))
                            {
                                per = 1;
                                return per;
                            }
                            else
                            {
                                if (xDate >= Convert.ToDateTime("01-05-" + Login._bookyear) && xDate <= Convert.ToDateTime("31-08-" + Login._bookyear))
                                {
                                    per = 2;
                                    return per;
                                }
                                else
                                {
                                    if (xDate >= Convert.ToDateTime("01-09-" + Login._bookyear) && xDate <= Convert.ToDateTime("31-12-" + Login._bookyear))
                                     {
                                       per = 3;
                                         return per;
                                     }
                                     else
                                     {
                                         return -1;
                                     }
                                }
                             }
                            break;

                        case 4:  // 3 meseca
                            if (xDate >= Convert.ToDateTime("01-01-" + Login._bookyear) && xDate <= Convert.ToDateTime("31-03-" + Login._bookyear))
                            {
                                 per = 1;
                                return per;
                            }
                            else
                            {
                                if (xDate >= Convert.ToDateTime("01-04-" + Login._bookyear) && xDate <= Convert.ToDateTime("30-06-" + Login._bookyear))
                                {
                                    per = 2;
                                    return per;
                                }
                                else
                                {
                                    if (xDate >= Convert.ToDateTime("01-07-" + Login._bookyear) && xDate <= Convert.ToDateTime("30-09-" + Login._bookyear))
                                     {
                                         per = 3;
                                         return per;
                                     }
                                     else
                                     {
                                         per = 4;
                                         return per;
                                     }
                                }
                            }
                            break;

                        case 12:  // mesecno

                        
                           per = Convert.ToInt32(xDate.Month.ToString());
                           return per;

                            break;
                    }
                    return per;
                }
                return per;
            }
            return per;
 
        }
        public bool CheckOpenLines(string invoice)
        {
            string iinvoice;
            iinvoice = invoice;

            AccOpenLinesBUS newbus = new AccOpenLinesBUS();
            AccOpenLinesModel newmod = new AccOpenLinesModel();

            newmod = newbus.GetAccOpenLinesByInvoiceNoTerm(iinvoice);
            if (newmod != null)
            {
                if (newmod.invoiceOpenLine.Trim() == iinvoice.Trim())
                {
                    if (newmod.debitOpenLine != 0 && newmod.creditOpenLine != 0 && newmod.debitOpenLine == newmod.creditOpenLine )
                    {
                        return false;
                    }
                    else
                    {
                        if((newmod.creditOpenLine != 0 && newmod.debitOpenLine == 0) || (newmod.debitOpenLine != 0 && newmod.creditOpenLine == 0 ))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            
        }

        public Int32 InvoiceNr()
        {
            ArrNrBUS lab = new ArrNrBUS();
            ArrNrModel lam = new ArrNrModel();
          
            lam = lab.GetInvoiceNr();
                if (lam != null)
                {
                    //var result = nid.idNumber.ToString().PadLeft(6, '0');
                    //// DateTime YearDate = DateTime.Now;
                    //// string year2 = YearDate.ToString("yy");
                    //var aa = nid.idDaily.ToString().PadRight(6, '0');
                    //string SubString = nid.yearId.Substring(yearId.Length - 2);
                    //txtIncop.Text = SubString + aa + result;
                     return lam.nrArrFak;
                }
                else
                {
                        return -1;
                }
            
         }
        public Int32 SepaNr()
        {
            ArrNrBUS lab = new ArrNrBUS();
            ArrNrModel lam = new ArrNrModel();

            lam = lab.GetSepaNr();
            if (lam != null)
            {
              return lam.nrSEPA;
            }
            else
            {
                return -1;
            }

        }

        public Boolean InvoiceBookingORIGINAL(InvoiceModel imodel, List<InvoiceItemsModel> iitemsmodel, string codeDaily, int label, string code, string nameForm, int idUser)
        {
            InvoiceModel model;
            List<InvoiceItemsModel> itemsmodel;
            AccLineModel linemodel;
            List<AccLineModel> lineitems;
            
            

            idLabel = label;
            codeArr = code;
            model = new InvoiceModel();
            itemsmodel = new List<InvoiceItemsModel>();
            daily = codeDaily;
            model = imodel;
            itemsmodel = iitemsmodel;
            descriptionLine = model.descriptionInvoice;
// reading settings
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
          //  asm = asb.GetAccSettingsByIDWithLabel(DateTime.Now.Year.ToString(), idLabel);
            asm = asb.GetSettingsByID(Login._bookyear);
            if (asm != null)
            {
                idBTW = Convert.ToInt32(asm.defBTWinvoicing);
                defRaisPriceConto = asm.defLedgerPrice;
                defInsuranceConto = asm.defLedgerIncurance;
                defCancelInsConto = asm.defLedgerCancel;
                defCalamitait = asm.defLedgerCalamitu;
                defMoneyGroup = asm.defLedgerMoneyGr;
                defFirstPayment = asm.defFirstPayment;
                defReservationCost = asm.defLedReservationCost;
                debitorReservationAcc = asm.debitorReservationAccount;
                idDaily = Convert.ToInt32(asm.idDailyFak);
            }
            else
            {
                RadMessageBox.Show("No predefined accounts !! Check settings, please !!!");
                return finish;
            }
// reading Daily 
            AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
            AccDailyModel adm = new AccDailyModel();
            adm = adb.GetDailysByCode(daily);
            if (adm != null)
            {
                if (adm.idDailyType != 3)
                {
                    RadMessageBox.Show("Only Verkoop boek allowed");
                    return finish;
                }
                basicKonto = adm.numberLedgerAccount;
            }
            linemodel = new AccLineModel();
// fill accline
            linemodel.dtLine = Convert.ToDateTime(model.dtCreated);
            linemodel.idAccDaily = Convert.ToInt32(daily);
            if (model.invoiceRbr == "000")
                linemodel.dtBooking = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
            else
                if (model.invoiceRbr == "001")
                    linemodel.dtBooking = Convert.ToDateTime(model.dtLastPay);
                else
                    linemodel.dtBooking = model.dtLastPay;            //dtValuta;

          //  linemodel.dtBooking = model.dtValuta;
            linemodel.invoiceNr = model.invoiceNr + "-"+ model.invoiceRbr;
            linemodel.idProjectLine = codeArr;
            if (basicKonto != "" && basicKonto != null)
            {
                linemodel.numberLedAccount = basicKonto;
            }

            //else
            //{
            //    asm.
            //}
            DateTime datum = new DateTime();
            datum = Convert.ToDateTime(model.dtCreated);       //model.dtValuta
            linemodel.periodLine = Period(datum);
            linemodel.booksort = 1;
            if (model.brutoAmount < 0)
            {
                linemodel.creditLine = Convert.ToDecimal(model.brutoAmount)*-1;
                linemodel.debitLine = 0;
            }
            else
            {
                linemodel.debitLine = Convert.ToDecimal(model.brutoAmount);
                linemodel.creditLine = 0;
            }
            //linemodel.descLine = descriptionLine.Trim() + " -  " + model.idVoucher.ToString();//Convert.ToChar(model.idVoucher);
            linemodel.descLine = model.descriptionInvoice;
            linemodel.idPersonLine = Convert.ToString(model.idContPerson);
            AccDebCreBUS adcb = new AccDebCreBUS();
            AccDebCreModel adcm = new AccDebCreModel();
        
            adcm = adcb.GetPersonDebCre(Convert.ToInt32(model.idContPerson));
            if (adcm != null)
            {
                if (adcm.accNumber != null && adcm.accNumber != "")
                {
                    linemodel.idClientLine = adcm.accNumber;
                    clientBook = adcm.accNumber;
 
                }
                else
                {
                    RadMessageBox.Show("Person does not have a Account number");
                    return finish;
                }
            }
            linemodel.incopNr = getIncopNr(adm.idDaily, nameForm, idUser);

            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            isOK = alb.Save(linemodel, nameForm,idUser);
            if (isOK == false)
            {
                RadMessageBox.Show("Error writing line !!");
                return finish;
            }
            // making open line
            AccOpenLinesBUS olb = new AccOpenLinesBUS();
            AccOpenLinesModel olmodel = new AccOpenLinesModel();
            //=== spaja fakturu i redni broj u jedno za pronalazenje otvorene stavke;
            string invoice = model.invoiceNr.Trim() + "-" + model.invoiceRbr.Trim();
            olmodel = olb.GetAccOpenLinesByInvoiceClient(invoice, clientBook);
            if (olmodel != null)   // ne postoji otvorena stavka
            {
                if (olmodel.invoiceOpenLine.Trim() != invoice && olmodel.idDebCre != clientBook)
                {
                    olmodel = new AccOpenLinesModel();
                    olmodel.invoiceOpenLine = linemodel.invoiceNr;
                    olmodel.descOpenLine = linemodel.descLine;   //descriptionLine.Trim();
                    olmodel.idDebCre = linemodel.idClientLine;
                    olmodel.account = linemodel.numberLedAccount;
                    olmodel.codeCost = linemodel.idCostLine;
                    olmodel.debitOpenLine = linemodel.debitLine;
                    olmodel.creditOpenLine = linemodel.creditLine;
                    olmodel.periodOnenLines = linemodel.periodLine;
                    olmodel.dtCreationLine = linemodel.dtLine;
                    //   olmodel.idProject = linemodel.idProjectLine;
                    if (model.invoiceRbr == "000")
                        olmodel.dtOpenLine = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
                    else
                        if (model.invoiceRbr == "001")
                            olmodel.dtOpenLine = Convert.ToDateTime(model.dtLastPay);
                        else
                            olmodel.dtOpenLine = linemodel.dtLine;
                    olmodel.codeArr = linemodel.idProjectLine;
                    if (linemodel.debitLine > 0)
                        olmodel.typeOpenLine = "D";// xSideBooking;
                    else
                        olmodel.typeOpenLine = "C";
                    olmodel.idProject = linemodel.idProjectLine;
                    olmodel.idPayCondition = 0;
                    if (adcm.payCondition != null && adcm.payCondition != 0)  // pay condition from person
                    {
                        olmodel.discauntDays = PaymentDays(adcm.payCondition);
                    }
                    else
                    {
                        if (asm.defPayCondition != null)
                        {
                            olmodel.discauntDays = PaymentDays(Convert.ToInt32(asm.defPayCondition)); // pay condition default settings
                        }
                        else
                        {
                            olmodel.discauntDays = 0;
                        }
                    }

                    olmodel.creditDays = 0;
                    olmodel.referencePay = linemodel.incopNr;
                    olmodel.bookingYear = Login._bookyear;


                    isOK = olb.Save(olmodel, nameForm,idUser);
                    if (isOK == false)
                    {
                        RadMessageBox.Show("Error writing open line !!");
                    }
                }
                else
                {
                    if (olmodel.invoiceOpenLine.Trim() == invoice && olmodel.idDebCre == clientBook)
                    {
                        if (linemodel.debitLine != 0)
                            olmodel.debitOpenLine = linemodel.debitLine;
                        if (linemodel.creditLine != 0)
                            olmodel.creditOpenLine = linemodel.creditLine;
                        //if (linemodel.debitLine > 0)
                        //    olmodel.creditOpenLine = linemodel.debitLine;
                        //else
                        //    olmodel.debitOpenLine = linemodel.creditLine;
                        isOK = olb.Update(olmodel, nameForm,idUser);
                        if (isOK == false)
                        {
                            RadMessageBox.Show("Error Updating open line !!!");
                            finish = false;
                            return finish;
                        }
                    }
                }
            }
            else
            {
                if (linemodel.debitLine > 0)
                    olmodel.creditOpenLine = linemodel.debitLine;
                else
                    olmodel.debitOpenLine = linemodel.creditLine;
                isOK = olb.Update(olmodel, nameForm,idUser);
                if (isOK == false)
                {
                    RadMessageBox.Show("Error Updating open line !!!");
                    finish = false;
                    return finish;
                }
            }
            //=============================================  zavrsava open lines ========================================
            int booksortw = 1;
            // booking items ======
            lineitems = new List<AccLineModel>();
            for (int jm = 0; jm < itemsmodel.Count; jm++)
            {
                AccLineModel linemodelone = new AccLineModel();
                booksortw++;
                linemodelone.idAccDaily = Convert.ToInt32(daily);
                linemodelone.invoiceNr = linemodel.invoiceNr;   //itemsArr[jm].idArticle;
                //== nadji konto 
                if (itemsmodel[jm].idArtical == "Reis Pakket")
                {
                    linemodelone.numberLedAccount = defRaisPriceConto;
                    descriptionLine = itemsmodel[jm].idArtical;
                }
                else
                {
                    if (itemsmodel[jm].idArtical == "Insurance")
                    {
                        linemodelone.numberLedAccount = defInsuranceConto;
                        descriptionLine = itemsmodel[jm].idArtical;
                    }
                    else
                    {
                        if (itemsmodel[jm].idArtical == "Cancel insurance")
                        {
                            linemodelone.numberLedAccount = defCancelInsConto;
                            descriptionLine = itemsmodel[jm].idArtical;
                        }
                        else
                        {
                            if (itemsmodel[jm].idArtical == "Calamitait Fond")
                            {
                                linemodelone.numberLedAccount = defCalamitait; //
                                descriptionLine = itemsmodel[jm].idArtical;
                            }
                            else
                            {
                                if (itemsmodel[jm].idArtical == "Money group")
                                {
                                    linemodelone.numberLedAccount = defMoneyGroup; //
                                    descriptionLine = itemsmodel[jm].idArtical;
                                }

                                else
                                {
                                    if (itemsmodel[jm].idArtical == "First payment")
                                    {
                                        linemodelone.numberLedAccount = defFirstPayment;        // defRaisPriceConto; //
                                        descriptionLine = itemsmodel[jm].idArtical;
                                    }
                                    else
                                    {
                                        if (itemsmodel[jm].idArtical == "Reservation cost")
                                        {
                                            linemodelone.numberLedAccount = defReservationCost;// "999999"; //
                                            descriptionLine = itemsmodel[jm].idArtical;
                                        }
                                        else
                                        {
                                            linemodelone.numberLedAccount = GetKonto(itemsmodel[jm].idArtical);
                                            descriptionLine = itemsmodel[jm].idArtical;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


               // linemodelone.numberLedAccount = GetKonto(itemsmodel[jm].idArtical);
                linemodelone.dtLine = linemodel.dtLine;
                
                if (model.invoiceRbr == "000")
                     linemodelone.dtBooking = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
                else
                    if (model.invoiceRbr == "001")
                         linemodelone.dtBooking = Convert.ToDateTime(model.dtLastPay);
                    else
                         linemodelone.dtBooking = model.dtLastPay;  
                linemodelone.descLine = descriptionLine.Trim();
                linemodelone.periodLine = linemodel.periodLine;   // itemsArr[jm].number;
                linemodelone.booksort = booksortw; // linemodel.booksort + 1;
                linemodelone.idProjectLine = codeArr;
                linemodelone.bookingYear = Login._bookyear;
                // ovde ubaciti za storno

                linemodelone.debitLine = 0;
                decimal sumi = 0;
                sumi = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                if (sumi < 0)
                {
                    linemodelone.debitLine = sumi * -1;
                    linemodelone.creditLine = 0;
                }
                else
                {
                    linemodelone.creditLine = sumi;
                    linemodelone.debitLine = 0;
                }

                    linemodelone.idClientLine = clientBook;
                    linemodelone.incopNr = linemodel.incopNr;

                lineitems.Add(linemodelone);
            }
            // Save lines
            for (int jw = 0; jw < lineitems.Count; jw++)
            {
                isOK = AddAmount(lineitems[jw], nameForm, idUser);
                isOK = alb.Save(lineitems[jw], nameForm,idUser);
                if (isOK == false)
                {
                    RadMessageBox.Show("Error writing lines !!");
                    return finish;
                }
            }


            finish = true;
            return finish;
            //linemodel.idAccDaily = Convert.ToInt32(daily);
            //linemodel.dtBooking = model.dtValuta;
            //linemodel.invoiceNr = model.invoiceNr;
            //if (basicKonto != "" && basicKonto != null)
            //{
            //    linemodel.numberLedAccount = basicKonto;
            //}
            ////else
            ////{
            ////    asm.
            ////}
            //DateTime datum = new DateTime();
            //datum = Convert.ToDateTime(model.dtValuta);
            //linemodel.periodLine = Period(datum);
            //linemodel.booksort = 1;
            //linemodel.debitLine = Convert.ToDecimal(model.brutoAmount);
            //linemodel.descLine = model.descriptionInvoice + " " + Convert.ToChar(model.idVoucher);
            //linemodel.idPersonLine = Convert.ToString(model.idContPerson);
        }

        private string getIncopNr(int xDaily, string nameForm, int idUser)
        {
            AccLineBUS gn = new AccLineBUS(Login._bookyear);
            IdModel nid = new IdModel();
            int idDaily = 0;
            string yearId = Login._bookyear;//DateTime.Now.Year.ToString();
            if (xDaily != -1)
                idDaily = xDaily;
            nid = gn.GetIncop(yearId, idDaily, nameForm, idUser);
            var result = nid.idNumber.ToString().PadLeft(6, '0');
          
            var aa = nid.idDaily.ToString().PadRight(6, '0');
            string SubString = nid.yearId.Substring(yearId.Length - 2);
            string number = SubString + aa + result;

            return number;

        }

        private int PaymentDays(int idPayment)
        {
            AccPaymentBUS gn = new AccPaymentBUS();
            AccPaymentModel nid = new AccPaymentModel();
            int days = 0;

            nid = gn.GetPaymentByID(idPayment);
            if (nid != null)
            {
                if (nid.numberDays != null)
                {
                        days=Convert.ToInt32(nid.numberDays);
                }
                else
                {
                        days = 0;
                }
         
            }
            else
            {
                days = 0;
            }

            return days;

        }
        private string GetKonto(string idArtical)
        {
            string konto = "";
            string group = "";
            ArticalBUS ab = new ArticalBUS();
            ArticalModel am = new ArticalModel();
            am = ab.GetArticalByID(idArtical);
            if (am != null)
            {
                if (idArtical == am.codeArtical)
                {
                    group = am.codeArtikalGroup;
                }
            }
            ArticalGroupsBUS agb = new ArticalGroupsBUS();
            ArticalGroupsModel agm = new ArticalGroupsModel();
            agm = agb.GetArticalGroup(group);
            if (agm != null)
            {
                konto = agm.verkopArtical;
            }
            else
            {
                konto = "";
            }

            return konto;
        }
        private bool clientinvoice( string accClient, string inv, string bookyear)
        {
            bool isfind = false;
            string dtable;
            string client;
            string invoice;

            client = accClient;
            invoice = inv;

            AccSettingsBUS asb = new AccSettingsBUS();
            isfind = asb.ClientInvoice(client, invoice, bookyear);

            return isfind;
        }
        public string clientName(string accNumber)
        {
            string namecli="";
            string client;
            client = accNumber;
            AccDebCreBUS adcb = new AccDebCreBUS();
            AccDebCreModel adcm = new AccDebCreModel();

            adcm = adcb.GetCustomerByAccCode(client);
            if (adcm != null)
            {
                if (adcm.namePerson != "" && adcm.namePerson != null)
                    namecli = adcm.namePerson;
                else
                    if (adcm.nameClient != "" && adcm.nameClient != null)
                        namecli = adcm.nameClient;

                    else
                        namecli = "";


            }
            return namecli;

        }

        public Boolean InvoiceBooking(InvoiceModel imodel, List<InvoiceItemsModel> iitemsmodel, string codeDaily, int label, string code, string nameForm, int idUser)
        {
            InvoiceModel model;
            List<InvoiceItemsModel> itemsmodel;
            AccLineModel linemodel;
            List<AccLineModel> lineitems;
            

            finish = false;


            idLabel = label;
            codeArr = code;
            model = new InvoiceModel();
            itemsmodel = new List<InvoiceItemsModel>();
            daily = codeDaily;
            model = imodel;
            itemsmodel = iitemsmodel;
            descriptionLine = model.descriptionInvoice;

            //=== kontrole ================================================================================================================= 
            if (model != null)
            {
                if (model.invoiceNr == "")
                {
                    RadMessageBox.Show("Wrong invoice number");
                    return finish;
                }
                if (model.invoiceRbr == "000")
                {
                    if (model.dtFirstPay == null || model.dtFirstPay == DateTime.MinValue || model.dtFirstPay.ToString() == "1900-01-01")
                    {
                        RadMessageBox.Show("Wrong First payment date");
                        return finish;
                    }
                }
                else
                {
                    if (model.invoiceRbr == "001")
                        if (model.dtLastPay == null || model.dtLastPay == DateTime.MinValue || model.dtLastPay.ToString() == "1900-01-01")
                        {
                            RadMessageBox.Show("Wrong Last payment date");
                            return finish;
                        }
                }
                decimal sum_items = 0;
                if (itemsmodel != null)
                {
                    for (int y = 0; y < itemsmodel.Count; y++)
                    {
                        sum_items = sum_items + Convert.ToInt32(itemsmodel[y].quantity) * Convert.ToDecimal(itemsmodel[y].price);
                    }

                    if (model.brutoAmount != sum_items)
                    {
                        RadMessageBox.Show("Difference in amount !");
                        return finish;
                    }
                }
                else
                {
                    RadMessageBox.Show("No items for booking !");
                    return finish;
                }
                if (codeArr == "")
                {
                    RadMessageBox.Show("No project !");
                    return finish;
                }
            }
            else
            {
                RadMessageBox.Show("No invoice for booking !");
                return finish;
            }
            //===============================================================================================================================


            ArrangementBUS ab = new ArrangementBUS();
            ArrangementModel awm = new ArrangementModel();

            awm = ab.GetArrangementByCode(codeArr);
            if (awm != null)
            {
                date130500 = awm.dtFromArrangement;
                if (awm.idClientInvoice != null && awm.idClientInvoice != 0)
                    ClientBooking = awm.idClientInvoice;        //=== ispituje da li se aranzman fakturise na Pravno lice
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("No date-From !!!") != null)
                        RadMessageBox.Show(resxSet.GetString("No date-From !!!"));
                    else
                        RadMessageBox.Show("No date-From !!!");
                }
                return finish;
            }
            // reading settings
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            //  asm = asb.GetAccSettingsByIDWithLabel(DateTime.Now.Year.ToString(), idLabel);
            asm = asb.GetSettingsByID(Login._bookyear);
            if (asm != null)
            {
                idBTW = Convert.ToInt32(asm.defBTWinvoicing);
                defRaisPriceConto = asm.defLedgerPrice;
                defInsuranceConto = asm.defLedgerIncurance;
                defCancelInsConto = asm.defLedgerCancel;
                defCalamitait = asm.defLedgerCalamitu;
                defMoneyGroup = asm.defLedgerMoneyGr;
                defFirstPayment = asm.defFirstPayment;
                defReservationCost = asm.defLedReservationCost;
                debitorReservationAcc = asm.debitorReservationAccount;
                defLedgerCancelation = asm.defLedgerCancelation;
                idDaily = Convert.ToInt32(asm.idDailyFak);
            }
            else
            {
                RadMessageBox.Show("No predefined accounts !! Check settings, please !!!");
                return finish;
            }
            // reading Daily 
            AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
            AccDailyModel adm = new AccDailyModel();
            adm = adb.GetDailysByCode(daily);
            if (adm != null)
            {
                if (adm.idDailyType != 3)
                {
                    RadMessageBox.Show("Only Verkoop boek allowed");
                    return finish;
                }
                basicKonto = adm.numberLedgerAccount;
            }
            linemodel = new AccLineModel();
            // fill accline
            linemodel.dtLine = Convert.ToDateTime(model.dtCreated);
            linemodel.idAccDaily = Convert.ToInt32(daily);
            if (model.invoiceRbr == "000")
                linemodel.dtBooking = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
            else
                if (model.invoiceRbr == "001")
                    linemodel.dtBooking = Convert.ToDateTime(model.dtLastPay);
                else
                    linemodel.dtBooking = model.dtLastPay;            //dtValuta;

            //  linemodel.dtBooking = model.dtValuta;
            linemodel.invoiceNr = model.invoiceNr + "-" + model.invoiceRbr;
            linemodel.idProjectLine = codeArr;
            if (basicKonto != "" && basicKonto != null)
            {
                linemodel.numberLedAccount = basicKonto;
            }

            //else
            //{
            //    asm.
            //}
            DateTime datum = new DateTime();
            datum = Convert.ToDateTime(model.dtCreated);       //model.dtValuta
            linemodel.periodLine = Period(datum);
            linemodel.bookingYear = model.dtCreated.Value.Year.ToString();  // ubacuje godinu iz datuma creiranja
            linemodel.booksort = 1;
            if (model.brutoAmount < 0)
            {
                linemodel.creditLine = Convert.ToDecimal(model.brutoAmount) * -1;
                linemodel.debitLine = 0;
            }
            else
            {
                linemodel.debitLine = Convert.ToDecimal(model.brutoAmount);
                linemodel.creditLine = 0;
            }
            //linemodel.descLine = descriptionLine.Trim() + " -  " + model.idVoucher.ToString();//Convert.ToChar(model.idVoucher);
            linemodel.descLine = model.descriptionInvoice;
            linemodel.idPersonLine = Convert.ToString(model.idContPerson);
            linemodel.statusLine = true;

            AccDebCreBUS adcb = new AccDebCreBUS();
            AccDebCreModel adcm = new AccDebCreModel();
            if (ClientBooking != 0)                            //=== ako je client ili person za fakturu
                adcm = adcb.GetClientDebCre(ClientBooking);
            else
                adcm = adcb.GetPersonDebCre(Convert.ToInt32(model.idContPerson));
            if (adcm != null)
            {
                if (adcm.accNumber != null && adcm.accNumber != "")
                {
                    linemodel.idClientLine = adcm.accNumber;
                    clientBook = adcm.accNumber;

                }
                else
                {
                    RadMessageBox.Show("Person does not have a Account number");
                    return finish;
                }
            }
            else
            {
                RadMessageBox.Show("Can't read Customer " + model.idContPerson);
                finish = false;
                return finish;
            }
            linemodel.incopNr = getIncopNr(adm.idDaily, nameForm,idUser);

            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            isOK = alb.Save(linemodel, nameForm,idUser);
            if (isOK == false)
            {
                RadMessageBox.Show("Error writing line !!");
                return finish;
            }
            // making open line
            AccOpenLinesBUS olb = new AccOpenLinesBUS();
            AccOpenLinesModel olmodel = new AccOpenLinesModel();
            //=== spaja fakturu i redni broj u jedno za pronalazenje otvorene stavke;
            string invoice = model.invoiceNr.Trim() + "-" + model.invoiceRbr.Trim();
            olmodel = olb.GetAccOpenLinesByInvoiceClient(invoice, clientBook);
            if (olmodel != null)   // ne postoji otvorena stavka
            {
                if (olmodel.invoiceOpenLine.Trim() != invoice && olmodel.idDebCre != clientBook)
                {
                    olmodel = new AccOpenLinesModel();
                    olmodel.invoiceOpenLine = linemodel.invoiceNr;
                    olmodel.descOpenLine = linemodel.descLine;   //descriptionLine.Trim();
                    olmodel.idDebCre = linemodel.idClientLine;
                    olmodel.account = linemodel.numberLedAccount;
                    olmodel.codeCost = linemodel.idCostLine;
                    olmodel.debitOpenLine = linemodel.debitLine;
                    olmodel.creditOpenLine = linemodel.creditLine;
                    olmodel.periodOnenLines = linemodel.periodLine;
                    olmodel.dtCreationLine = linemodel.dtLine;
                    //   olmodel.idProject = linemodel.idProjectLine;
                    if (model.invoiceRbr == "000")
                        olmodel.dtOpenLine = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
                    else
                        if (model.invoiceRbr == "001")
                            olmodel.dtOpenLine = Convert.ToDateTime(model.dtLastPay);
                        else
                            olmodel.dtOpenLine = Convert.ToDateTime(linemodel.dtBooking);
                    olmodel.codeArr = linemodel.idProjectLine;
                    if (linemodel.debitLine > 0)
                        olmodel.typeOpenLine = "D";// xSideBooking;
                    else
                        olmodel.typeOpenLine = "C";
                    olmodel.idProject = linemodel.idProjectLine;
                    olmodel.idPayCondition = 0;
                    if (adm != null && adcm.payCondition != null && adcm.payCondition != 0)  // pay condition from person
                    {
                        olmodel.discauntDays = PaymentDays(adcm.payCondition);
                    }
                    else
                    {
                        if (asm.defPayCondition != null)
                        {
                            olmodel.discauntDays = PaymentDays(Convert.ToInt32(asm.defPayCondition)); // pay condition default settings
                        }
                        else
                        {
                            olmodel.discauntDays = 0;
                        }
                    }

                    olmodel.creditDays = 0;
                    olmodel.referencePay = linemodel.incopNr;
                    olmodel.bookingYear = olmodel.dtOpenLine.Year.ToString();  //Login._bookyear; stavlja godinu knjizenja od datuma stavke


                    isOK = olb.Save(olmodel, nameForm,idUser);
                    if (isOK == false)
                    {
                        RadMessageBox.Show("Error writing open line !!");
                    }
                }
                else
                {
                    if (olmodel.invoiceOpenLine.Trim() == invoice && olmodel.idDebCre == clientBook)
                    {
                        if (linemodel.debitLine != 0)
                            olmodel.debitOpenLine = olmodel.debitOpenLine+linemodel.debitLine;
                        if (linemodel.creditLine != 0)
                            olmodel.creditOpenLine = olmodel.creditOpenLine + linemodel.creditLine;
                        //if (linemodel.debitLine > 0)
                        //    olmodel.creditOpenLine = linemodel.debitLine;
                        //else
                        //    olmodel.debitOpenLine = linemodel.creditLine;
                        isOK = olb.Update(olmodel, nameForm,idUser);
                        if (isOK == false)
                        {
                            RadMessageBox.Show("Error Updating open line !!!");
                            finish = false;
                            return finish;
                        }
                    }
                }
            }
            else
            {
                if (linemodel.debitLine > 0)
                    olmodel.creditOpenLine = linemodel.debitLine;
                else
                    olmodel.debitOpenLine = linemodel.creditLine;
                isOK = olb.Update(olmodel, nameForm,idUser);
                if (isOK == false)
                {
                    RadMessageBox.Show("Error Updating open line !!!");
                    finish = false;
                    return finish;
                }
            }
            //=============================================  zavrsava open lines ========================================
            int booksortw = 1;
            // booking items ======
            lineitems = new List<AccLineModel>();
            AccLineModel linemodelone = new AccLineModel();
            AccLineModel extend1 = new AccLineModel();
            AccLineModel extend2 = new AccLineModel();
            for (int jm = 0; jm < itemsmodel.Count; jm++)
            {
                linemodelone = new AccLineModel();
                booksortw++;
                linemodelone.idAccDaily = Convert.ToInt32(daily);
                linemodelone.invoiceNr = linemodel.invoiceNr;   //itemsArr[jm].idArticle;
                //== nadji konto 
                if (itemsmodel[jm].idArtical == "Reis Pakket")
                {
                    linemodelone.numberLedAccount = defRaisPriceConto;
                    descriptionLine = itemsmodel[jm].idArtical;
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(descriptionLine) != null)
                            descriptionLine = resxSet.GetString(descriptionLine);
                    }
                }
                else
                {
                    if (itemsmodel[jm].idArtical == "Insurance")
                    {
                        linemodelone.numberLedAccount = defInsuranceConto;
                        descriptionLine = itemsmodel[jm].idArtical;
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(descriptionLine) != null)
                                descriptionLine = resxSet.GetString(descriptionLine);
                        }
                    }
                    else
                    {
                        if (itemsmodel[jm].idArtical == "Cancel insurance")
                        {
                            linemodelone.numberLedAccount = defCancelInsConto;
                            descriptionLine = itemsmodel[jm].idArtical;
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString(descriptionLine) != null)
                                    descriptionLine = resxSet.GetString(descriptionLine);
                            }
                        }
                        else
                        {
                            if (itemsmodel[jm].idArtical == "Calamitait Fond")
                            {
                                linemodelone.numberLedAccount = defCalamitait; //
                                descriptionLine = itemsmodel[jm].idArtical;
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString(descriptionLine) != null)
                                        descriptionLine = resxSet.GetString(descriptionLine);
                                }
                            }
                            else
                            {
                                if (itemsmodel[jm].idArtical == "Money group")
                                {
                                    linemodelone.numberLedAccount = defMoneyGroup; //
                                    descriptionLine = itemsmodel[jm].idArtical;
                                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                    {
                                        if (resxSet.GetString(descriptionLine) != null)
                                            descriptionLine = resxSet.GetString(descriptionLine);
                                    }
                                }

                                else
                                {
                                    if (itemsmodel[jm].idArtical == "First payment")
                                    {
                                        linemodelone.numberLedAccount = defFirstPayment;        // defRaisPriceConto; //
                                        descriptionLine = itemsmodel[jm].idArtical;
                                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                        {
                                            if (resxSet.GetString(descriptionLine) != null)
                                                descriptionLine = resxSet.GetString(descriptionLine);
                                        }
                                    }
                                    else
                                    {
                                        if (itemsmodel[jm].idArtical == "Reservation cost")
                                        {
                                            linemodelone.numberLedAccount = defReservationCost;// "999999"; //
                                            descriptionLine = itemsmodel[jm].idArtical;
                                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                            {
                                                if (resxSet.GetString(descriptionLine) != null)
                                                    descriptionLine = resxSet.GetString(descriptionLine);
                                            }
                                        }
                                        else
                                        {
                                            if (itemsmodel[jm].idArtical == "Cancellation cost")
                                            {
                                                linemodelone.numberLedAccount = defLedgerCancelation;  // "999999"; //
                                                descriptionLine = itemsmodel[jm].idArtical;
                                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                                {
                                                    if (resxSet.GetString(descriptionLine) != null)
                                                        descriptionLine = resxSet.GetString(descriptionLine);
                                                }
                                            }
                                            else
                                            {

                                                linemodelone.numberLedAccount = GetKonto(itemsmodel[jm].idArtical);
                                                descriptionLine = itemsmodel[jm].idArtical;
                                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                                {
                                                    if (resxSet.GetString(descriptionLine) != null)
                                                        descriptionLine = resxSet.GetString(descriptionLine);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                // linemodelone.numberLedAccount = GetKonto(itemsmodel[jm].idArtical);
                linemodelone.dtLine = date130500;   // linemodel.dtLine;

                if (model.invoiceRbr == "000")
                    linemodelone.dtBooking = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
                else
                    if (model.invoiceRbr == "001")
                        linemodelone.dtBooking = Convert.ToDateTime(model.dtLastPay);
                    else
                        linemodelone.dtBooking = model.dtLastPay;
                linemodelone.descLine = descriptionLine.Trim();
                linemodelone.periodLine = Period(date130500);  //linemodel.periodLine;   // itemsArr[jm].number;
                linemodelone.booksort = booksortw; // linemodel.booksort + 1;
                if (itemsmodel[jm].idArtical == "Cancellation cost")
                {
                    linemodelone.idProjectLine = "";
                    linemodelone.dtLine = linemodel.dtLine;
                }
                else
                    linemodelone.idProjectLine = codeArr;
                linemodelone.bookingYear = date130500.Year.ToString();  // godina     // Login._bookyear;
                // ovde ubaciti za storno

                linemodelone.debitLine = 0;
                decimal sumi = 0;
                sumi = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                if (sumi < 0)
                {
                    linemodelone.debitLine = sumi * -1;
                    linemodelone.creditLine = 0;
                }
                else
                {
                    linemodelone.creditLine = sumi;
                    linemodelone.debitLine = 0;
                }

                linemodelone.idClientLine = clientBook;
                linemodelone.incopNr = linemodel.incopNr;

                linemodelone.statusLine = true;

                lineitems.Add(linemodelone);

                isOK = AddAmount(linemodelone, nameForm, idUser);
                isOK = alb.Save(linemodelone,nameForm,idUser);
                if (isOK == false)
                {
                    RadMessageBox.Show("Error writing lines !!");
                    return finish;
                }
                if (linemodelone.idProjectLine != "")
                {
                    //================  odavde za svaku stavku fakture pravi dve dodatne stavke na 130500  sa datumom pocetka aranzmana ================
                    extend1 = new AccLineModel();
                    extend1 = linemodelone;
                    booksortw++;
                    extend1.dtLine = linemodel.dtLine; //date130500; //
                    extend1.booksort = booksortw;
                    extend1.periodLine = Period(linemodel.dtLine);                           //Period(date130500);
                    extend1.bookingYear = linemodel.dtLine.Year.ToString();   // godina knjigovodstva
                    decimal sumi1 = 0;
                    sumi1 = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                    extend1.statusLine = true;

                    if (debitorReservationAcc != "")
                        extend1.numberLedAccount = debitorReservationAcc;
                    else
                        extend1.numberLedAccount = "130500";

                    if (sumi1 < 0)
                    {
                        linemodelone.debitLine = sumi1 * -1;
                        linemodelone.creditLine = 0;
                    }
                    else
                    {
                        linemodelone.creditLine = sumi1;
                        linemodelone.debitLine = 0;
                    }

                    //if (linemodelone.debitLine != 0)
                    //{
                    //    extend1.debitLine = 0;
                    //    extend1.creditLine = sumi1;
                    //}
                    //else
                    //{
                    //    extend1.creditLine = 0;
                    //    extend1.debitLine = sumi1;
                    //}
                    //--- ovde ide iznos
                    //   lineitems.Add(extend1);
                    extend1.statusLine = true;

                    isOK = AddAmount(extend1, nameForm, idUser);
                    isOK = alb.Save(extend1, nameForm,idUser);
                    if (isOK == false)
                    {
                        RadMessageBox.Show("Error writing lines !!");
                        return finish;
                    }

                    //==============================================================
                    extend2 = new AccLineModel();
                    extend2 = extend1;
                    booksortw++;
                    extend2.dtLine = date130500; //linemodel.dtLine;
                    extend2.booksort = booksortw;
                    extend2.periodLine = Period(date130500);
                    extend2.bookingYear = date130500.Year.ToString(); // godina knjigovostva
                    decimal sumi2 = 0;
                    sumi2 = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                    if (sumi2 < 0)
                        sumi2 = sumi2 * -1;
                    //--- ovde ide iznos --- ovde ga obrce zbog ravnoteze
                    if (extend1.debitLine != 0)
                    {
                        extend2.creditLine = sumi2;
                        extend2.debitLine = 0;
                    }
                    else
                    {
                        extend2.debitLine = sumi2;
                        extend2.creditLine = 0;
                    }
                    extend2.statusLine = true;
                    isOK = AddAmount(extend2, nameForm, idUser);
                    isOK = alb.Save(extend2, nameForm, idUser);
                    if (isOK == false)
                    {
                        RadMessageBox.Show("Error writing lines !!");
                        return finish;
                    }
                    //lineitems.Add(extend2);
                    //=======================================================
                }
            }
            // Save lines
            //for (int jw = 0; jw < lineitems.Count; jw++)
            //{
            //    isOK = AddAmount(lineitems[jw]);
            //    isOK = alb.Save(lineitems[jw]);
            //    if (isOK == false)
            //    {
            //        RadMessageBox.Show("Error writing lines !!");
            //        return finish;
            //    }
            //}


            finish = true;
            return finish;
            //linemodel.idAccDaily = Convert.ToInt32(daily);
            //linemodel.dtBooking = model.dtValuta;
            //linemodel.invoiceNr = model.invoiceNr;
            //if (basicKonto != "" && basicKonto != null)
            //{
            //    linemodel.numberLedAccount = basicKonto;
            //}
            ////else
            ////{
            ////    asm.
            ////}
            //DateTime datum = new DateTime();
            //datum = Convert.ToDateTime(model.dtValuta);
            //linemodel.periodLine = Period(datum);
            //linemodel.booksort = 1;
            //linemodel.debitLine = Convert.ToDecimal(model.brutoAmount);
            //linemodel.descLine = model.descriptionInvoice + " " + Convert.ToChar(model.idVoucher);
            //linemodel.idPersonLine = Convert.ToString(model.idContPerson);
        }


        public Boolean InvoiceBookingWithoutMessageBoxes(InvoiceModel imodel, List<InvoiceItemsModel> iitemsmodel, string codeDaily, int label, string code, string nameForm,int idUser)
        {
            InvoiceModel model;
            List<InvoiceItemsModel> itemsmodel;
            AccLineModel linemodel;
            List<AccLineModel> lineitems;


            finish = false;


            idLabel = label;
            codeArr = code;
            model = new InvoiceModel();
            itemsmodel = new List<InvoiceItemsModel>();
            daily = codeDaily;
            model = imodel;
            itemsmodel = iitemsmodel;
            descriptionLine = model.descriptionInvoice;

            //=== kontrole ================================================================================================================= 
            if (model != null)
            {
                if (model.invoiceNr == "")
                {
                    //RadMessageBox.Show("Wrong invoice number");
                    return finish;
                }
                if (model.invoiceRbr == "000")
                {
                    if (model.dtFirstPay == null || model.dtFirstPay == DateTime.MinValue || model.dtFirstPay.ToString() == "1900-01-01")
                    {
                        //RadMessageBox.Show("Wrong First payment date");
                        return finish;
                    }
                }
                else
                {
                    if (model.invoiceRbr == "001")
                        if (model.dtLastPay == null || model.dtLastPay == DateTime.MinValue || model.dtLastPay.ToString() == "1900-01-01")
                        {
                            //RadMessageBox.Show("Wrong Last payment date");
                            return finish;
                        }
                }
                decimal sum_items = 0;
                if (itemsmodel != null)
                {
                    for (int y = 0; y < itemsmodel.Count; y++)
                    {
                        sum_items = sum_items + Convert.ToInt32(itemsmodel[y].quantity) * Convert.ToDecimal(itemsmodel[y].price);
                    }

                    if (model.brutoAmount != sum_items)
                    {
                        //RadMessageBox.Show("Difference in amount !");
                        return finish;
                    }
                }
                else
                {
                    //RadMessageBox.Show("No items for booking !");
                    return finish;
                }
                if (codeArr == "")
                {
                    //RadMessageBox.Show("No project !");
                    return finish;
                }
            }
            else
            {
                //RadMessageBox.Show("No invoice for booking !");
                return finish;
            }
            //===============================================================================================================================


            ArrangementBUS ab = new ArrangementBUS();
            ArrangementModel awm = new ArrangementModel();

            awm = ab.GetArrangementByCode(codeArr);
            if (awm != null)
            {
                date130500 = awm.dtFromArrangement;
                if (awm.idClientInvoice != null && awm.idClientInvoice != 0)
                    ClientBooking = awm.idClientInvoice;        //=== ispituje da li se aranzman fakturise na Pravno lice
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("No date-From !!!") != null)
                        RadMessageBox.Show(resxSet.GetString("No date-From !!!"));
                    //else
                       // RadMessageBox.Show("No date-From !!!");
                }
                return finish;
            }
            // reading settings
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            //  asm = asb.GetAccSettingsByIDWithLabel(DateTime.Now.Year.ToString(), idLabel);
            asm = asb.GetSettingsByID(Login._bookyear);
            if (asm != null)
            {
                idBTW = Convert.ToInt32(asm.defBTWinvoicing);
                defRaisPriceConto = asm.defLedgerPrice;
                defInsuranceConto = asm.defLedgerIncurance;
                defCancelInsConto = asm.defLedgerCancel;
                defCalamitait = asm.defLedgerCalamitu;
                defMoneyGroup = asm.defLedgerMoneyGr;
                defFirstPayment = asm.defFirstPayment;
                defReservationCost = asm.defLedReservationCost;
                debitorReservationAcc = asm.debitorReservationAccount;
                defLedgerCancelation = asm.defLedgerCancelation;
                idDaily = Convert.ToInt32(asm.idDailyFak);
            }
            else
            {
                //RadMessageBox.Show("No predefined accounts !! Check settings, please !!!");
                return finish;
            }
            // reading Daily 
            AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
            AccDailyModel adm = new AccDailyModel();
            adm = adb.GetDailysByCode(daily);
            if (adm != null)
            {
                if (adm.idDailyType != 3)
                {
                    //RadMessageBox.Show("Only Verkoop boek allowed");
                    return finish;
                }
                basicKonto = adm.numberLedgerAccount;
            }
            linemodel = new AccLineModel();
            // fill accline
            linemodel.dtLine = Convert.ToDateTime(model.dtCreated);
            linemodel.idAccDaily = Convert.ToInt32(daily);
            if (model.invoiceRbr == "000")
                linemodel.dtBooking = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
            else
                if (model.invoiceRbr == "001")
                    linemodel.dtBooking = Convert.ToDateTime(model.dtLastPay);
                else
                    linemodel.dtBooking = model.dtLastPay;            //dtValuta;

            //  linemodel.dtBooking = model.dtValuta;
            linemodel.invoiceNr = model.invoiceNr + "-" + model.invoiceRbr;
            linemodel.idProjectLine = codeArr;
            if (basicKonto != "" && basicKonto != null)
            {
                linemodel.numberLedAccount = basicKonto;
            }

            //else
            //{
            //    asm.
            //}
            DateTime datum = new DateTime();
            datum = Convert.ToDateTime(model.dtCreated);       //model.dtValuta
            linemodel.periodLine = Period(datum);
            linemodel.bookingYear = model.dtCreated.Value.Year.ToString();  // ubacuje godinu iz datuma creiranja
            linemodel.booksort = 1;
            if (model.brutoAmount < 0)
            {
                linemodel.creditLine = Convert.ToDecimal(model.brutoAmount) * -1;
                linemodel.debitLine = 0;
            }
            else
            {
                linemodel.debitLine = Convert.ToDecimal(model.brutoAmount);
                linemodel.creditLine = 0;
            }
            //linemodel.descLine = descriptionLine.Trim() + " -  " + model.idVoucher.ToString();//Convert.ToChar(model.idVoucher);
            linemodel.descLine = model.descriptionInvoice;
            linemodel.idPersonLine = Convert.ToString(model.idContPerson);
            linemodel.statusLine = true;

            AccDebCreBUS adcb = new AccDebCreBUS();
            AccDebCreModel adcm = new AccDebCreModel();
            if (ClientBooking != 0)                            //=== ako je client ili person za fakturu
                adcm = adcb.GetClientDebCre(ClientBooking);
            else
                adcm = adcb.GetPersonDebCre(Convert.ToInt32(model.idContPerson));
            if (adcm != null)
            {
                if (adcm.accNumber != null && adcm.accNumber != "")
                {
                    linemodel.idClientLine = adcm.accNumber;
                    clientBook = adcm.accNumber;

                }
                else
                {
                    //RadMessageBox.Show("Person does not have a Account number");
                    return finish;
                }
            }
            else
            {
                //RadMessageBox.Show("Can't read Customer " + model.idContPerson);
                finish = false;
                return finish;
            }
            linemodel.incopNr = getIncopNr(adm.idDaily, nameForm, idUser);

            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            isOK = alb.Save(linemodel, nameForm,idUser);
            if (isOK == false)
            {
               // RadMessageBox.Show("Error writing line !!");
                return finish;
            }
            // making open line
            AccOpenLinesBUS olb = new AccOpenLinesBUS();
            AccOpenLinesModel olmodel = new AccOpenLinesModel();
            //=== spaja fakturu i redni broj u jedno za pronalazenje otvorene stavke;
            string invoice = model.invoiceNr.Trim() + "-" + model.invoiceRbr.Trim();
            olmodel = olb.GetAccOpenLinesByInvoiceClient(invoice, clientBook);
            if (olmodel != null)   // ne postoji otvorena stavka
            {
                if (olmodel.invoiceOpenLine.Trim() != invoice && olmodel.idDebCre != clientBook)
                {
                    olmodel = new AccOpenLinesModel();
                    olmodel.invoiceOpenLine = linemodel.invoiceNr;
                    olmodel.descOpenLine = linemodel.descLine;   //descriptionLine.Trim();
                    olmodel.idDebCre = linemodel.idClientLine;
                    olmodel.account = linemodel.numberLedAccount;
                    olmodel.codeCost = linemodel.idCostLine;
                    olmodel.debitOpenLine = linemodel.debitLine;
                    olmodel.creditOpenLine = linemodel.creditLine;
                    olmodel.periodOnenLines = linemodel.periodLine;
                    olmodel.dtCreationLine = linemodel.dtLine;
                    //   olmodel.idProject = linemodel.idProjectLine;
                    if (model.invoiceRbr == "000")
                        olmodel.dtOpenLine = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
                    else
                        if (model.invoiceRbr == "001")
                            olmodel.dtOpenLine = Convert.ToDateTime(model.dtLastPay);
                        else
                            olmodel.dtOpenLine = Convert.ToDateTime(linemodel.dtBooking);
                    olmodel.codeArr = linemodel.idProjectLine;
                    if (linemodel.debitLine > 0)
                        olmodel.typeOpenLine = "D";// xSideBooking;
                    else
                        olmodel.typeOpenLine = "C";
                    olmodel.idProject = linemodel.idProjectLine;
                    olmodel.idPayCondition = 0;
                    if (adm != null && adcm.payCondition != null && adcm.payCondition != 0)  // pay condition from person
                    {
                        olmodel.discauntDays = PaymentDays(adcm.payCondition);
                    }
                    else
                    {
                        if (asm.defPayCondition != null)
                        {
                            olmodel.discauntDays = PaymentDays(Convert.ToInt32(asm.defPayCondition)); // pay condition default settings
                        }
                        else
                        {
                            olmodel.discauntDays = 0;
                        }
                    }

                    olmodel.creditDays = 0;
                    olmodel.referencePay = linemodel.incopNr;
                    olmodel.bookingYear = olmodel.dtOpenLine.Year.ToString();  //Login._bookyear; stavlja godinu knjizenja od datuma stavke


                    isOK = olb.Save(olmodel, nameForm,idUser);
                    if (isOK == false)
                    {
                       // RadMessageBox.Show("Error writing open line !!");
                    }
                }
                else
                {
                    if (olmodel.invoiceOpenLine.Trim() == invoice && olmodel.idDebCre == clientBook)
                    {
                        if (linemodel.debitLine != 0)
                            olmodel.debitOpenLine = olmodel.debitOpenLine + linemodel.debitLine;
                        if (linemodel.creditLine != 0)
                            olmodel.creditOpenLine = olmodel.creditOpenLine + linemodel.creditLine;
                        //if (linemodel.debitLine > 0)
                        //    olmodel.creditOpenLine = linemodel.debitLine;
                        //else
                        //    olmodel.debitOpenLine = linemodel.creditLine;
                        isOK = olb.Update(olmodel,nameForm,idUser);
                        if (isOK == false)
                        {
                            //RadMessageBox.Show("Error Updating open line !!!");
                            finish = false;
                            return finish;
                        }
                    }
                }
            }
            else
            {
                if (linemodel.debitLine > 0)
                    olmodel.creditOpenLine = linemodel.debitLine;
                else
                    olmodel.debitOpenLine = linemodel.creditLine;
                isOK = olb.Update(olmodel, nameForm,idUser);
                if (isOK == false)
                {
                    //RadMessageBox.Show("Error Updating open line !!!");
                    finish = false;
                    return finish;
                }
            }
            //=============================================  zavrsava open lines ========================================
            int booksortw = 1;
            // booking items ======
            lineitems = new List<AccLineModel>();
            AccLineModel linemodelone = new AccLineModel();
            AccLineModel extend1 = new AccLineModel();
            AccLineModel extend2 = new AccLineModel();
            for (int jm = 0; jm < itemsmodel.Count; jm++)
            {
                linemodelone = new AccLineModel();
                booksortw++;
                linemodelone.idAccDaily = Convert.ToInt32(daily);
                linemodelone.invoiceNr = linemodel.invoiceNr;   //itemsArr[jm].idArticle;
                //== nadji konto 
                if (itemsmodel[jm].idArtical == "Reis Pakket")
                {
                    linemodelone.numberLedAccount = defRaisPriceConto;
                    descriptionLine = itemsmodel[jm].idArtical;
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(descriptionLine) != null)
                            descriptionLine = resxSet.GetString(descriptionLine);
                    }
                }
                else
                {
                    if (itemsmodel[jm].idArtical == "Insurance")
                    {
                        linemodelone.numberLedAccount = defInsuranceConto;
                        descriptionLine = itemsmodel[jm].idArtical;
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(descriptionLine) != null)
                                descriptionLine = resxSet.GetString(descriptionLine);
                        }
                    }
                    else
                    {
                        if (itemsmodel[jm].idArtical == "Cancel insurance")
                        {
                            linemodelone.numberLedAccount = defCancelInsConto;
                            descriptionLine = itemsmodel[jm].idArtical;
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString(descriptionLine) != null)
                                    descriptionLine = resxSet.GetString(descriptionLine);
                            }
                        }
                        else
                        {
                            if (itemsmodel[jm].idArtical == "Calamitait Fond")
                            {
                                linemodelone.numberLedAccount = defCalamitait; //
                                descriptionLine = itemsmodel[jm].idArtical;
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString(descriptionLine) != null)
                                        descriptionLine = resxSet.GetString(descriptionLine);
                                }
                            }
                            else
                            {
                                if (itemsmodel[jm].idArtical == "Money group")
                                {
                                    linemodelone.numberLedAccount = defMoneyGroup; //
                                    descriptionLine = itemsmodel[jm].idArtical;
                                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                    {
                                        if (resxSet.GetString(descriptionLine) != null)
                                            descriptionLine = resxSet.GetString(descriptionLine);
                                    }
                                }

                                else
                                {
                                    if (itemsmodel[jm].idArtical == "First payment")
                                    {
                                        linemodelone.numberLedAccount = defFirstPayment;        // defRaisPriceConto; //
                                        descriptionLine = itemsmodel[jm].idArtical;
                                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                        {
                                            if (resxSet.GetString(descriptionLine) != null)
                                                descriptionLine = resxSet.GetString(descriptionLine);
                                        }
                                    }
                                    else
                                    {
                                        if (itemsmodel[jm].idArtical == "Reservation cost")
                                        {
                                            linemodelone.numberLedAccount = defReservationCost;// "999999"; //
                                            descriptionLine = itemsmodel[jm].idArtical;
                                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                            {
                                                if (resxSet.GetString(descriptionLine) != null)
                                                    descriptionLine = resxSet.GetString(descriptionLine);
                                            }
                                        }
                                        else
                                        {
                                            if (itemsmodel[jm].idArtical == "Cancellation cost")
                                            {
                                                linemodelone.numberLedAccount = defLedgerCancelation;  // "999999"; //
                                                descriptionLine = itemsmodel[jm].idArtical;
                                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                                {
                                                    if (resxSet.GetString(descriptionLine) != null)
                                                        descriptionLine = resxSet.GetString(descriptionLine);
                                                }
                                            }
                                            else
                                            {

                                                linemodelone.numberLedAccount = GetKonto(itemsmodel[jm].idArtical);
                                                descriptionLine = itemsmodel[jm].idArtical;
                                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                                {
                                                    if (resxSet.GetString(descriptionLine) != null)
                                                        descriptionLine = resxSet.GetString(descriptionLine);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                // linemodelone.numberLedAccount = GetKonto(itemsmodel[jm].idArtical);
                linemodelone.dtLine = date130500;   // linemodel.dtLine;

                if (model.invoiceRbr == "000")
                    linemodelone.dtBooking = Convert.ToDateTime(model.dtFirstPay);//linemodel.dtLine;  
                else
                    if (model.invoiceRbr == "001")
                        linemodelone.dtBooking = Convert.ToDateTime(model.dtLastPay);
                    else
                        linemodelone.dtBooking = model.dtLastPay;
                linemodelone.descLine = descriptionLine.Trim();
                linemodelone.periodLine = Period(date130500);  //linemodel.periodLine;   // itemsArr[jm].number;
                linemodelone.booksort = booksortw; // linemodel.booksort + 1;
                if (itemsmodel[jm].idArtical == "Cancellation cost")
                {
                    linemodelone.idProjectLine = "";
                    linemodelone.dtLine = linemodel.dtLine;
                }
                else
                    linemodelone.idProjectLine = codeArr;
                linemodelone.bookingYear = date130500.Year.ToString();  // godina     // Login._bookyear;
                // ovde ubaciti za storno

                linemodelone.debitLine = 0;
                decimal sumi = 0;
                sumi = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                if (sumi < 0)
                {
                    linemodelone.debitLine = sumi * -1;
                    linemodelone.creditLine = 0;
                }
                else
                {
                    linemodelone.creditLine = sumi;
                    linemodelone.debitLine = 0;
                }

                linemodelone.idClientLine = clientBook;
                linemodelone.incopNr = linemodel.incopNr;

                linemodelone.statusLine = true;

                lineitems.Add(linemodelone);

                isOK = AddAmount(linemodelone,nameForm,idUser);
                isOK = alb.Save(linemodelone, nameForm,idUser);
                if (isOK == false)
                {
                    //RadMessageBox.Show("Error writing lines !!");
                    return finish;
                }
                if (linemodelone.idProjectLine != "")
                {
                    //================  odavde za svaku stavku fakture pravi dve dodatne stavke na 130500  sa datumom pocetka aranzmana ================
                    extend1 = new AccLineModel();
                    extend1 = linemodelone;
                    booksortw++;
                    extend1.dtLine = linemodel.dtLine; //date130500; //
                    extend1.booksort = booksortw;
                    extend1.periodLine = Period(linemodel.dtLine);                           //Period(date130500);
                    extend1.bookingYear = linemodel.dtLine.Year.ToString();   // godina knjigovodstva
                    decimal sumi1 = 0;
                    sumi1 = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                    extend1.statusLine = true;

                    if (debitorReservationAcc != "")
                        extend1.numberLedAccount = debitorReservationAcc;
                    else
                        extend1.numberLedAccount = "130500";

                    if (sumi1 < 0)
                    {
                        linemodelone.debitLine = sumi1 * -1;
                        linemodelone.creditLine = 0;
                    }
                    else
                    {
                        linemodelone.creditLine = sumi1;
                        linemodelone.debitLine = 0;
                    }

                    //if (linemodelone.debitLine != 0)
                    //{
                    //    extend1.debitLine = 0;
                    //    extend1.creditLine = sumi1;
                    //}
                    //else
                    //{
                    //    extend1.creditLine = 0;
                    //    extend1.debitLine = sumi1;
                    //}
                    //--- ovde ide iznos
                    //   lineitems.Add(extend1);
                    extend1.statusLine = true;

                    isOK = AddAmount(extend1,nameForm,idUser);
                    isOK = alb.Save(extend1, nameForm,idUser);
                    if (isOK == false)
                    {
                        //RadMessageBox.Show("Error writing lines !!");
                        return finish;
                    }

                    //==============================================================
                    extend2 = new AccLineModel();
                    extend2 = extend1;
                    booksortw++;
                    extend2.dtLine = date130500; //linemodel.dtLine;
                    extend2.booksort = booksortw;
                    extend2.periodLine = Period(date130500);
                    extend2.bookingYear = date130500.Year.ToString(); // godina knjigovostva
                    decimal sumi2 = 0;
                    sumi2 = Convert.ToDecimal(itemsmodel[jm].price) * Convert.ToDecimal(itemsmodel[jm].quantity);
                    if (sumi2 < 0)
                        sumi2 = sumi2 * -1;
                    //--- ovde ide iznos --- ovde ga obrce zbog ravnoteze
                    if (extend1.debitLine != 0)
                    {
                        extend2.creditLine = sumi2;
                        extend2.debitLine = 0;
                    }
                    else
                    {
                        extend2.debitLine = sumi2;
                        extend2.creditLine = 0;
                    }
                    extend2.statusLine = true;
                    isOK = AddAmount(extend2,nameForm,idUser);
                    isOK = alb.Save(extend2, nameForm,idUser);
                    if (isOK == false)
                    {
                        //RadMessageBox.Show("Error writing lines !!");
                        return finish;
                    }
                    //lineitems.Add(extend2);
                    //=======================================================
                }
            }
            // Save lines
            //for (int jw = 0; jw < lineitems.Count; jw++)
            //{
            //    isOK = AddAmount(lineitems[jw]);
            //    isOK = alb.Save(lineitems[jw]);
            //    if (isOK == false)
            //    {
            //        RadMessageBox.Show("Error writing lines !!");
            //        return finish;
            //    }
            //}


            finish = true;
            return finish;
            //linemodel.idAccDaily = Convert.ToInt32(daily);
            //linemodel.dtBooking = model.dtValuta;
            //linemodel.invoiceNr = model.invoiceNr;
            //if (basicKonto != "" && basicKonto != null)
            //{
            //    linemodel.numberLedAccount = basicKonto;
            //}
            ////else
            ////{
            ////    asm.
            ////}
            //DateTime datum = new DateTime();
            //datum = Convert.ToDateTime(model.dtValuta);
            //linemodel.periodLine = Period(datum);
            //linemodel.booksort = 1;
            //linemodel.debitLine = Convert.ToDecimal(model.brutoAmount);
            //linemodel.descLine = model.descriptionInvoice + " " + Convert.ToChar(model.idVoucher);
            //linemodel.idPersonLine = Convert.ToString(model.idContPerson);
        }
    
        
        public string getUsername(int idUser)
        {
            string name = "";
            UsersBUS ubs = new UsersBUS();
            UsersModel um = new UsersModel();
            if (idUser != null && idUser != 0)
            {
                um = ubs.getUserExact(idUser);
                if (um != null)
                {
                    name = um.nameUser;
                }
            }
            return name;
        }

    }
}
