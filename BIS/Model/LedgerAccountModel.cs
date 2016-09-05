using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class LedgerAccountModel : IModel
    {
        [DisplayName("Account Id")]
        public int idLedgerAccount { get; set; }

        [DisplayName("Account")]
        public string numberLedgerAccount { get; set; }

        [DisplayName("Description")]
        public string descLedgerAccount { get; set; }

        [DisplayName("Open Debit")]
        public decimal openDebitAccount { get; set; }

        [DisplayName("Open Credit")]
        public decimal openCreditAccount { get; set; }

        [DisplayName("Type")]
        public decimal accountTypeAccount { get; set; }
        [DisplayName("Name type")]
        public string nameTypeAccount { get; set; }

        [DisplayName("Cost")]
        public string idCostCenter { get; set; }
        [DisplayName("Name Cost")]
        public string nameCostLedgerAccount { get; set; }

        [DisplayName("Cost mandatory")]
        public bool mandatoryCostAccount { get; set; }

        [DisplayName("Debitor mandatory")]
        public bool mandatoryDebitorAccount { get; set; }

        [DisplayName("Creditor mandatory")]
        public bool mandatoryCreditorAccount { get; set; }

        [DisplayName("Project mandatory")]
        public bool mandatoryProjectAccount { get; set; }

        [DisplayName("Budget account")]
        public bool isBudgetAccount { get; set; }

        [DisplayName("Class 1")]
        public int class1Account { get; set; }

        [DisplayName("Class 2")]
        public int class2Account { get; set; }

        [DisplayName("Class 3")]
        public int class3Account { get; set; }

        [DisplayName("Class 4")]
        public int class4Account { get; set; }

        [DisplayName("Class 5")]
        public int class5Account { get; set; }

         [DisplayName("Debit amount")]
        public decimal debitAccount { get; set; }

          [DisplayName("Credit amount")]
        public decimal creditAccount { get; set; }
          [DisplayName("Transaction count")]
        public int transactionNoAccount { get; set; }

          [DisplayName("Booking side D/C")]
          public string sideBooking { get; set; }
        // nazivi

         [DisplayName("Block memorial booking")]
          public bool isBlockMemorial { get; set; }
        
       
          [DisplayName("Name Class 1")]
          public string nameClass1LedgerAccount { get; set; }
          [DisplayName("Name Class 2")]
          public string nameClass2LedgerAccount { get; set; }
          [DisplayName("Name Class 3")]
          public string nameClass3LedgerAccount { get; set; }
          [DisplayName("Name Class 4")]
          public string nameClass4LedgerAccount { get; set; }
          [DisplayName("Name Class 5")]
          public string nameClass5LedgerAccount { get; set; }
        
          [DisplayName("Valuta debit")]
          public decimal valutaDebitLedgerAccount { get; set; }
          [DisplayName("Valuta credit")]
          public decimal valutaCreditLedgerAccount { get; set; }
          [DisplayName("BTW")]
          public bool isBTWLedgerAccount { get; set; }
          [DisplayName("Active")]
          public bool isActiveLedgerAccount { get; set; }
          [DisplayName("BTW id")]
          public int btwId { get; set; }

          public string bookingYear { get; set; }

          [DisplayName("Name user")]
          public string nameUser { get; set; }

          [DisplayName("From")]
          public int from { get; set; }

          [DisplayName("To")]
          public int to { get; set; }

       
    }

    public class LedgerAccDataModel:IModel
    {      

        [DisplayName("Account")]
        public string numberLedgerAccount { get; set; }

        [DisplayName("Description")]
        public string descLedgerAccount { get; set; }

        [DisplayName("Cost mandatory")]
        public bool mandatoryCostAccount { get; set; }

        [DisplayName("Debitor mandatory")]
        public bool mandatoryDebitorAccount { get; set; }

        [DisplayName("Creditor mandatory")]
        public bool mandatoryCreditorAccount { get; set; }

        [DisplayName("Project mandatory")]
        public bool mandatoryProjectAccount { get; set; }

        [DisplayName("Budget account")]
        public bool isBudgetAccount { get; set; }

        [DisplayName("Name Class 1")]
        public string accountclass1 { get; set; }

        [DisplayName("Name Class 2")]
        public string accountclass2 { get; set; }

        [DisplayName("Name Class 3")]
        public string accountclass3 { get; set; }

        [DisplayName("Name Class 4")]
        public string accountclass4 { get; set; }

        [DisplayName("Name Class 5")]
        public string accountclass5 { get; set; }

        [DisplayName("BTW")]
        public bool isBTWLedgerAccount { get; set; }

        [DisplayName("Active")]
        public bool isActiveLedgerAccount { get; set; }

        [DisplayName("Block memorial booking")]
        public bool isBlockMemorial { get; set; }
        

        [DisplayName("BTW id")]
        public int btwId { get; set; }

        [DisplayName("Booking side D/C")]
        public string sideBooking { get; set; }

        [DisplayName("Name user")]
        public string nameUser { get; set; }

        [DisplayName("From")]
        public int from { get; set; }

        [DisplayName("To")]
        public int to { get; set; }
       

    }
}