using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccLineModel : IModel
    {
         [DisplayName("ID")]
        public int idAccLine { get; set; }
         [DisplayName("ID Daily")]
        public int idAccDaily { get; set; }
       
         [DisplayName("Period")]
        public int periodLine { get; set; }
         [DisplayName("Date")]
        public DateTime dtLine { get; set; }
         [DisplayName("Dbk.Account")]
        public string numberLedAccount { get; set; }
         [DisplayName("Invoice nr")]
        public string invoiceNr { get; set; }
         [DisplayName("Description")]
        public string descLine { get; set; }
         [DisplayName("Dbk.DB/CR")]
        public string idClientLine { get; set; }
         [DisplayName("Dbk.DB/CR")]
        public string idPersonLine { get; set; }
          [DisplayName("Cost")]
        public string idCostLine { get; set; }
           [DisplayName("Project")]
        public string idProjectLine { get; set; }
         [DisplayName("debit")]
        public decimal debitLine { get; set; }
         [DisplayName("Credit")]
        public decimal creditLine { get; set; }
         [DisplayName("Id Btw")]
        public int? idBTW { get; set; }
         [DisplayName("Debit Btw")]
        public decimal debitBTW { get; set; }
         [DisplayName("Credit Btw")]
        public decimal creditBTW { get; set; }
         [DisplayName("Id Currency")]
        public int idCurrency { get; set; }
         [DisplayName("Debit Currency")]
        public decimal? debitCurr { get; set; }
         [DisplayName("Credit Currency")]
        public decimal? creditCurr { get; set; }
         [DisplayName("Dbk.Booking date")]
        public DateTime? dtBooking { get; set; }
         [DisplayName("Booking sort")]
        public int? booksort { get; set; }
         [DisplayName("Currency rate")]
        public decimal? currrate { get; set; }
         [DisplayName("Dbk.Counter")]
         public string  incopNr { get; set; }
         [DisplayName("Iban")]
         public string iban { get; set; }
          [DisplayName("Booking year")]
         public string bookingYear { get; set; }
          [DisplayName("Total")]
         public decimal versil { get; set; }
         public int term { get; set; }
         public int idSepa { get; set; }
         public string descDaily { get; set; }
         public string cond1 { get; set; }
         public string cond2 { get; set; }
         public string cond3 { get; set; }
         public string userN { get; set; }
         [DisplayName("Booked")]
         public bool statusLine { get; set; }

         [DisplayName("Master")]
         public string idMaster { get; set; }

         [DisplayName("Detail")]
         public string idDetail { get; set; }

         [DisplayName("Detail")]
         public string descLedgerAccount { get; set; }
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

        public AccLineModel()
         {
             this.idAccLine = 0;
             this.idAccDaily = 0;
             this.statusLine = false;
             this.periodLine = 0;
             this.dtLine = DateTime.Now;
             this.numberLedAccount = String.Empty;
             this.invoiceNr = String.Empty;
             this.descLine = String.Empty;
             this.idClientLine = String.Empty;
             this.idPersonLine = String.Empty;
             this.idCostLine = String.Empty;
             this.idProjectLine = String.Empty;
             this.debitLine = 0;
             this.creditLine = 0;
             this.idBTW = 0;
             this.debitBTW = 0;
             this.creditBTW = 0;
             this.idCurrency = 0;
             this.debitCurr = 0;
             this.creditCurr = 0;
             this.dtBooking = DateTime.Now;
             this.booksort = 0;
             this.currrate = 0;
             this.incopNr = String.Empty;
             this.iban = String.Empty;
             this.bookingYear = DateTime.Now.Year.ToString();
             this.versil = 0;  //razlika
             this.term = 0;
             this.idSepa = 0;
             this.idMaster = "";
             this.idDetail = "";
             this.descLedgerAccount = String.Empty;
             this.userCreated = 0;
             this.dtCreated = Convert.ToDateTime("1900-01-01");
             this.userModified = 0;
             this.dtModified = Convert.ToDateTime("1900-01-01");
         }

        public AccLineModel(AccLineModel sourceModel)
        {
            this.idAccLine = sourceModel.idAccLine;
            this.idAccDaily = sourceModel.idAccDaily;
            this.statusLine = sourceModel.statusLine;
            this.periodLine = sourceModel.periodLine;
            this.dtLine = sourceModel.dtLine;
            this.numberLedAccount = sourceModel.numberLedAccount;
            this.invoiceNr = sourceModel.invoiceNr;
            this.descLine = sourceModel.descLine;
            this.idClientLine = sourceModel.idClientLine;
            this.idPersonLine = sourceModel.idPersonLine;
            this.idCostLine = sourceModel.idCostLine;
            this.idProjectLine = sourceModel.idProjectLine;
            this.debitLine = sourceModel.debitLine;
            this.creditLine = sourceModel.creditLine;
            this.idBTW = sourceModel.idBTW;
            this.debitBTW = sourceModel.debitBTW;
            this.creditBTW = sourceModel.creditBTW;
            this.idCurrency = sourceModel.idCurrency;
            this.debitCurr = sourceModel.debitCurr;
            this.creditCurr = sourceModel.creditCurr;
            this.dtBooking = sourceModel.dtBooking;
            this.booksort = sourceModel.booksort;
            this.currrate = sourceModel.currrate;
            this.incopNr = sourceModel.incopNr;
            this.iban = sourceModel.iban;
            this.bookingYear = sourceModel.bookingYear;
            this.versil = sourceModel.versil;  //razlika
            this.term = sourceModel.term;
            this.idSepa = sourceModel.idSepa;
            this.idMaster = sourceModel.idMaster;
            this.idDetail = sourceModel.idDetail;
            this.descLedgerAccount = sourceModel.descLedgerAccount;
            this.userCreated = 0;
            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.userModified = 0;
            this.dtModified = Convert.ToDateTime("1900-01-01");
        }

        public AccLineModel ReturnCopy()
        {
            AccLineModel newItem = new AccLineModel();

            newItem.idAccLine = this.idAccLine;
            newItem.idAccDaily = this.idAccDaily;
            newItem.statusLine = this.statusLine;
            newItem.periodLine =  this.periodLine;
            newItem.dtLine = this.dtLine;
            newItem.numberLedAccount = this.numberLedAccount;
            newItem.invoiceNr = this.invoiceNr;
            newItem.descLine = this.descLine;
            newItem.idClientLine = this.idClientLine;
            newItem.idPersonLine = this.idPersonLine;
            newItem.idCostLine = this.idCostLine;
            newItem.idProjectLine = this.idProjectLine;
            newItem.debitLine = this.debitLine;
            newItem.creditLine = this.creditLine;
            newItem.idBTW = this.idBTW;
            newItem.debitBTW =  this.debitBTW;
            newItem.creditBTW = this.creditBTW;
            newItem.idCurrency = this.idCurrency;
            newItem.debitCurr = this.debitCurr;
            newItem.creditCurr = this.creditCurr;
            newItem.dtBooking = this.dtBooking;
            newItem.booksort = this.booksort;
            newItem.currrate = this.currrate;
            newItem.incopNr = this.incopNr;
            newItem.iban = this.iban;
            newItem.bookingYear = this.bookingYear;
            newItem.versil = this.versil;  //razlika
            newItem.term = this.term;
            newItem.idSepa =  this.idSepa;
            newItem.idMaster = this.idMaster;
            newItem.idDetail = this.idDetail;
            newItem.descLedgerAccount = this.descLedgerAccount;
            this.userCreated = 0;
            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.userModified = 0;
            this.dtModified = Convert.ToDateTime("1900-01-01");

            return newItem;
        }

    }
    public class AccLineBeginModel
    {
        [DisplayName("Dbk.Account")]
        public string numberLedAccount { get; set; }
        [DisplayName("Description")]
        public string descLine { get; set; }
        [DisplayName("Dbk.DB/CR")]
        public string idClientLine { get; set; }
        [DisplayName("Cost")]
        public string idCostLine { get; set; }
        [DisplayName("Project")]
        public string idProjectLine { get; set; }
        [DisplayName("debit")]
        public decimal debit { get; set; }
        [DisplayName("Credit")]
        public decimal credit { get; set; }
        [DisplayName("Total")]
        public decimal diff { get; set; }
       
    }
    public class AccLineReportModel : IModel
    {

        [DisplayName("ID")]
        public int idAccLine { get; set; }

        [DisplayName("Period")]
        public int periodLine { get; set; }

        [DisplayName("Date")]
        public DateTime dtLine { get; set; }

        [DisplayName("Invoice nr")]
        public string invoiceNr { get; set; }

        [DisplayName("Description")]
        public string descLine { get; set; }

        [DisplayName("Account")]
        public string numberLedAccount { get; set; }

        [DisplayName("Description ledger account")]
        public string descLedgerAccount { get; set; }

        [DisplayName("debit")]
        public decimal debitLine { get; set; }

        [DisplayName("Credit")]
        public decimal creditLine { get; set; }

        [DisplayName("Client")]
        public string idClientLine { get; set; }

        [DisplayName("Client name")]
        public string cName { get; set; }

        [DisplayName("Cost")]
        public string idCostLine { get; set; }

        [DisplayName("Project")]
        public string idProjectLine { get; set; }

        [DisplayName("Name user")]
        public string nameUser { get; set; }

        [DisplayName("From")]
        public int from { get; set; }

        [DisplayName("To")]
        public int to { get; set; }

        public AccLineReportModel()
        {
            this.idAccLine = 0;
            this.periodLine = 0;
            this.dtLine = DateTime.Now;
            this.invoiceNr = String.Empty;
            this.descLine = String.Empty;
            this.numberLedAccount = String.Empty;
            this.descLedgerAccount = String.Empty;
            this.debitLine = 0;
            this.creditLine = 0;
            this.idClientLine = String.Empty;
            this.cName = String.Empty;
            this.idCostLine = String.Empty;
            this.idProjectLine = String.Empty;
            this.nameUser = String.Empty;
            this.from = 0;
            this.to = 0;

        }
    }


    public class AccLineComparer : IEqualityComparer<AccLineModel>
    {
        public bool Equals(AccLineModel x, AccLineModel y)
        {
            return (x.dtLine == y.dtLine || x.debitLine == y.debitLine || x.creditLine == y.creditLine || x.numberLedAccount == y.numberLedAccount || x.descLine == y.descLine || x.idProjectLine == y.idProjectLine || x.idCostLine == y.idCostLine || x.idPersonLine == y.idPersonLine || x.idClientLine == y.idClientLine);
        }

        public int GetHashCode(AccLineModel obj)
        {
            return  obj.dtLine.GetHashCode() ^ obj.debitLine.GetHashCode() ^ obj.creditLine.GetHashCode() ^ obj.numberLedAccount.GetHashCode() ^ obj.descLine.GetHashCode() ^ obj.idProjectLine.GetHashCode() ^ obj.idCostLine.GetHashCode() ^ obj.idPersonLine.GetHashCode() ^ obj.idClientLine.GetHashCode();
        }
    }
}
