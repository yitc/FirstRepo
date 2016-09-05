using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class DebCreLookupModel : IModel
    {
        [DisplayName("Account number")]
        public string accNumber { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Zip")]
        public string zip { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

     
   }


    public class DebCreLookupModelAdvanced : IModel
    {
        [DisplayName("ID person")]
        public int idContPerson { get; set; }
        
        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Account number")]
        public string accNumber { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Street")]
        public string street { get; set; }

        [DisplayName("House nr.")]
        public string housenr { get; set; }

        [DisplayName("Extension")]
        public string extension { get; set; }

        [DisplayName("Zip")]
        public string zip { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Country")]
        public string country { get; set; }

        public DebCreLookupModelAdvanced()
        {
            this.idContPerson = 0;
            this.idClient = 0;
            this.accNumber = String.Empty;
            this.name = String.Empty;
            this.street = String.Empty;
            this.housenr = String.Empty;
            this.extension = String.Empty;
            this.zip = String.Empty;
            this.city = String.Empty;
            this.country = String.Empty;

        }


    }


    public class DebCreCreditCardBalansModel : IModel
    {
        [DisplayName("Account number")]
        public string accNumber { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }
       
        [DisplayName("Zip")]
        public string zip { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Debit balans")]
        public decimal DebitBalans { get; set; }

        [DisplayName("Credit balans")]
        public decimal CreditBalans { get; set; }

        [DisplayName("Debit")]
        public decimal Debit { get; set; }

        [DisplayName("Credit")]
        public decimal Credit { get; set; }

        [DisplayName("Saldo")]
        public decimal Saldo { get; set; }

        [DisplayName("Account")]
        public string numberLedAccount { get; set; }

        //[DisplayName("Name user")]
        //public string nameUser { get; set; }

        //[DisplayName("From")]
        //public int from { get; set; }

        //[DisplayName("To")]
        //public int to { get; set; }

        //[DisplayName("Debitor")]
        //public bool deb { get; set; }

        public DebCreCreditCardBalansModel()
        {
            this.accNumber = String.Empty;
            this.name = String.Empty;
            this.zip = String.Empty;
            this.city = String.Empty;
            this.DebitBalans = 0;
            this.CreditBalans = 0;
            this.Debit = 0;
            this.Credit = 0;
            this.Saldo = 0;
            this.numberLedAccount = String.Empty;
            //this.nameUser = String.Empty;
            //this.from = 0;
            //this.to = 0;
            //this.deb = false;
        }

    }

    public class DebCreCreditCardDetailBalansModel : IModel
    {
        [DisplayName("Account number")]
        public string accNumber { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Invoice nr")]
        public string invoiceNr { get; set; }

        [DisplayName("IncopNr")]
        public string incopNr { get; set; }

        [DisplayName("Date line")]
        public DateTime dtLine { get; set; }

        [DisplayName("Period line")]
        public int periodLine { get; set; }

        [DisplayName("Description line")]
        public string descLine { get; set; }

        [DisplayName("Debit balans")]
        public decimal DebitBalans { get; set; }

        [DisplayName("Credit balans")]
        public decimal CreditBalans { get; set; }

        [DisplayName("Debit")]
        public decimal Debit { get; set; }

        [DisplayName("Credit")]
        public decimal Credit { get; set; }

        [DisplayName("Saldo")]
        public decimal Saldo { get; set; }

        [DisplayName("Account")]
        public string numberLedAccount { get; set; }

        public DebCreCreditCardDetailBalansModel()
        {
            this.accNumber = String.Empty;
            this.name = String.Empty;
            this.invoiceNr = String.Empty;
            this.incopNr = String.Empty;
            this.periodLine = 0;
            this.dtLine = DateTime.MinValue;
            this.descLine = String.Empty;
            this.DebitBalans = 0;
            this.CreditBalans = 0;
            this.Debit = 0;
            this.Credit = 0;
            this.Saldo = 0;
            this.numberLedAccount = String.Empty;
        }

    }

    public class DebCreCreditCardModel : IModel
    {
        [DisplayName("Account number")]
        public string accNumber { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Zip")]
        public string zip { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Debit")]
        public decimal Debit { get; set; }

        [DisplayName("Credit")]
        public decimal Credit { get; set; }

        [DisplayName("Saldo")]
        public decimal Saldo { get; set; }

        [DisplayName("Account")]
        public string numberLedAccount { get; set; }

        public DebCreCreditCardModel()
        {
            this.accNumber = String.Empty;
            this.name = String.Empty;
            this.zip = String.Empty;
            this.city = String.Empty;
            this.Debit = 0;
            this.Credit = 0;
            this.Saldo = 0;
            this.numberLedAccount = String.Empty;
        }

        

    }
    public class DebCreCreditCardDetailModel : IModel
    {
        [DisplayName("Account number")]
        public string accNumber { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Invoice nr")]
        public string invoiceNr { get; set; }

        [DisplayName("IncopNr")]
        public string incopNr { get; set; }

        [DisplayName("Date line")]
        public DateTime dtLine { get; set; }

        [DisplayName("Period line")]
        public int periodLine { get; set; }

        [DisplayName("Description line")]
        public string descLine { get; set; }


        [DisplayName("Debit")]
        public decimal Debit { get; set; }

        [DisplayName("Credit")]
        public decimal Credit { get; set; }

        [DisplayName("Saldo")]
        public decimal Saldo { get; set; }

        [DisplayName("Name user")]
        public string nameUser { get; set; }

        [DisplayName("From")]
        public int from { get; set; }

        [DisplayName("To")]
        public int to { get; set; }

        [DisplayName("Debitor")]
        public bool deb { get; set; }

        [DisplayName("Zip")]
        public string zip { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Debit balans")]
        public decimal DebitBalans { get; set; }

        [DisplayName("Credit balans")]
        public decimal CreditBalans { get; set; }

        [DisplayName("Account")]
        public string numberLedAccount { get; set; }


         [DisplayName("Code daily")]
        public int idAccDaily { get; set; }
        





        public DebCreCreditCardDetailModel()
        {
            this.accNumber = String.Empty;
            this.name = String.Empty;
            this.invoiceNr = String.Empty;
            this.incopNr = String.Empty;
            this.periodLine = 0;
            this.dtLine = DateTime.MinValue;
            this.descLine = String.Empty;
            this.Debit = 0;
            this.Credit = 0;
            this.Saldo = 0;
            this.nameUser = String.Empty;
            this.from = 0;
            this.to = 0;
            this.deb = false;
            this.zip = String.Empty;
            this.city = String.Empty;
            this.address = String.Empty;
            this.DebitBalans = 0;
            this.CreditBalans = 0;
            this.numberLedAccount = String.Empty;
            this.idAccDaily = 0;
        }

    }
}