using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccSettingsModel : IModel
    {
        [DisplayName("Id Settings")]
        public int idSettings { get; set; }

        [DisplayName("Year Settings")]
        public string yearSettings { get; set; }

        [DisplayName("Label")]
        public int labelSettings { get; set; }

        [DisplayName("No Periods")]
        public int? noPeriods { get; set; }

        [DisplayName("Begin Book Year")]
        public DateTime beginBookYear { get; set; }

        [DisplayName("End Book Year")]
        public DateTime endBookYear { get; set; }

        [DisplayName("Is Vat")]
        public bool isVat { get; set; }

        [DisplayName("Def Debitor Account")]
        public string defDebitorAccount { get; set; }

        [DisplayName("Def Creditor Account")]
        public string defCreditorAccount { get; set; }

        [DisplayName("Def Vat Debitor")]
        public string defVatDebitor { get; set; }

        [DisplayName("Def Vat Creditor")]
        public string defVatCreditor { get; set; }

        [DisplayName("Current Diference Account")]
        public string currDeferenceAccount { get; set; }

        [DisplayName("Payment Diference Account")]
        public string paymentDiferenceAccount { get; set; }

        [DisplayName("Bank Cost Account")]
        public string bankCostAccount { get; set; }

        [DisplayName("Def Pay Condition")]
        public int? defPayCondition { get; set; }

        [DisplayName("No Day Frst Warning")]
        public int? noDayFrstWarning { get; set; }

        [DisplayName("No Day Second Warning")]
        public int? noDaySecondWorning { get; set; }

        [DisplayName("BTW invoicing")]
        public int? defBTWinvoicing { get; set; }

        [DisplayName("Pricelist account")]
        public string defLedgerPrice { get; set; }
        [DisplayName("Insurance account")]
        public string defLedgerIncurance { get; set; }
        [DisplayName("Cancelation insurance account")]
        public string defLedgerCancel { get; set; }

        [DisplayName("Calamitait account")]
        public string defLedgerCalamitu { get; set; }
        [DisplayName("Money group account")]
        public string defLedgerMoneyGr { get; set; }
        [DisplayName("Invoice daily")]
        public int? idDailyFak { get; set; }
        [DisplayName("Transfering Account")]
        public string defTransferingAcc { get; set; }

        [DisplayName("Reservation Account")]
        public string defReservationAcc { get; set; }
        [DisplayName("Cancelation Account")]
        public string defLedgerCancelation { get; set; }

        [DisplayName("Iban")]
        public string myIban { get; set; }
        [DisplayName("Bic")]
        public string myBic { get; set; }
        [DisplayName("First payment Account")]
        public string defFirstPayment { get; set; }
        [DisplayName("Reservation cost Account")]
        public string defLedReservationCost { get; set; }

        [DisplayName("Sepa path")]
        public string sepaPath { get; set; }

        [DisplayName("Debitor reservation account")]
        public string debitorReservationAccount { get; set; }

        [DisplayName("Sepa account")]
        public string defSepaAcc { get; set; }

        [DisplayName("Difference begin account")]
        public string defDifferenceAcc { get; set; }
        //==========================================
        [DisplayName("User created")]
        public int userCreated { get; set; }
        [DisplayName("Date created")]
        public DateTime dtCreated { get; set; }
        [DisplayName("User modified")]
        public int userModified { get; set; }
        [DisplayName("Date modified")]
        public DateTime dtModified { get; set; }
        //==========================================
        public AccSettingsModel()
        {
            this.labelSettings = 0;
            this.beginBookYear = DateTime.Now;
            this.endBookYear = DateTime.Now;
            this.defPayCondition = 0;
            this.noDayFrstWarning = 0;
            this.noDaySecondWorning = 0;
            this.defBTWinvoicing = 0;
            this.idDailyFak = 0;
            this.myIban = String.Empty;
            this.myBic = String.Empty;
            this.sepaPath = String.Empty;
            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.dtModified = Convert.ToDateTime("1900-01-01");
            
        }

    }
}