using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccCreditPayModel : IModel
    {
        [DisplayName("ID")]
        public int idCreditPay { get; set; }

        [DisplayName("Date item")]
        public DateTime? dtItem { get; set; }
         [DisplayName("Valuta")]
        public DateTime? dtValuta { get; set; }
         [DisplayName("Creditor nr.")]
        public string accNumber { get; set; }
         [DisplayName("Client")]
        public int? idClient { get; set; }
        [DisplayName("Person")]
        public int? idContPers { get; set; }
        [DisplayName("Account")]
        public string account { get; set; }
        [DisplayName("Invoice")]
        public string invoiceNr { get; set; }
        [DisplayName("Inkop nr.")]
        public string inkopNr { get; set; }
         [DisplayName("Iban")]
        public string iban { get; set; }
         [DisplayName("Description")]
        public string descItem { get; set; }
         [DisplayName("Credit")]
        public decimal? amountC { get; set; }
         [DisplayName("Debit")]
         public decimal? amountD { get; set; }
         [DisplayName("BTW")]
        public int? idBtw { get; set; }
         [DisplayName("Currency")]
        public string currency { get; set; }
         [DisplayName("Amount currency")]
        public decimal? amountInCurr { get; set; }
         [DisplayName("Cost")]
        public string cost { get; set; }
         [DisplayName("Project")]
        public string project { get; set; }
         [DisplayName("Approved")]
         public bool isApproved { get; set; }
         [DisplayName("Booked")]
        public bool isBooked { get; set; }
          [DisplayName("Sent")]
        public bool isSent { get; set; }
          [DisplayName("Sent date")]
        public DateTime? dtSent { get; set; }
          [DisplayName("Name file")]
        public string namefile { get; set; }
         [DisplayName("Approved")]
        public int? approvedUser { get; set; }
         [DisplayName("Creator")]
        public int? createUser { get; set; }
         [DisplayName("Creation date")]
        public DateTime? dtCreation { get; set; }
         [DisplayName("Paying iban")]
        public string payIban { get; set; }
         [DisplayName("Select")]
         public bool isSelected { get; set; }
         [DisplayName("Document id")]
         public int idDocument { get; set; }
         [DisplayName("Option")]
         public int idOption { get; set; }
         [DisplayName("Pay days")]
         public int paydays { get; set; }
         [DisplayName("Task")]
         public int idTask { get; set; }
         [DisplayName("Approve book")]
         public bool isAprBook { get; set; }
         [DisplayName("Days")]
         public int ndays { get; set; }
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
         [DisplayName("Creditor name")]
         public string creditorName { get; set; }
        public AccCreditPayModel()
        {
            this.dtItem = DateTime.Now;
            this.dtValuta = DateTime.Now;
            this.accNumber = String.Empty;
            this.idClient = 0;
            this.idContPers = 0;
            this.account = String.Empty;
            this.invoiceNr = String.Empty;
            this.inkopNr = String.Empty;
            this.iban = String.Empty;
            this.descItem = String.Empty;
            this.amountC = 0;
            this.amountD = 0;
            this.idBtw = 0;
            this.currency = String.Empty;
            this.amountInCurr = 0;
            this.cost = String.Empty;
            this.project = String.Empty;
            this.isApproved = false;
            this.isBooked = false;
            this.isSent = false;
            this.dtSent = DateTime.Parse("1900-01-01");
            this.namefile = String.Empty;
            this.approvedUser = 0;
            this.createUser = 0;
            this.dtCreation = DateTime.Parse("1900-01-01");
            this.payIban = String.Empty;
            this.isSelected = false;
            this.idDocument = 0;
            this.idOption = 0;
            this.paydays = 0;
            this.idTask = 0;
            this.isAprBook = false;
            this.ndays = 0;
            this.userModified = 0;
            this.dtModified = DateTime.Parse("1900-01-01");
            this.creditorName = String.Empty;


        }

    }
}