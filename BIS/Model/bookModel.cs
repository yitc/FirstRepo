using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class bookModel : IModel
    {
        [DisplayName("ID")]
        public int idAccLine { get; set; }
        [DisplayName("ID Daily")]
        public int idAccDaily { get; set; }
        [DisplayName("Booked")]
        public bool statusLine { get; set; }
        [DisplayName("Period")]
        public int periodLine { get; set; }
        [DisplayName("Date")]
        public DateTime dtLine { get; set; }
        [DisplayName("Account")]
        public string numberLedAccount { get; set; }
        [DisplayName("Invoice nr")]
        public string invoiceNr { get; set; }
        [DisplayName("Description")]
        public string descLine { get; set; }
        [DisplayName("Client")]
        public string idClientLine { get; set; }
        [DisplayName("Person")]
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
        [DisplayName("Booking date")]
        public DateTime? dtBooking { get; set; }
        [DisplayName("Booking sort")]
        public int? booksort { get; set; }
        [DisplayName("Currency rate")]
        public decimal? currrate { get; set; }
        [DisplayName("Incop nr")]
        public string incopNr { get; set; }
        [DisplayName("Iban")]
        public string iban { get; set; }
        public string bookingYear { get; set; }
        public decimal versil { get; set; }
        public int term { get; set; }
        public int idSepa { get; set; }
        public string descDaily { get; set; }
        public string cond1 { get; set; }
        public string cond2 { get; set; }
        public string cond3 { get; set; }
        public string userN { get; set; }

        public bookModel()
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
        }
    }
   
    
}
