using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.Model
{

    public class BankHederModel : IModel
    {
        public int idBankHeder { get; set; }

        public DateTime? entryDate { get; set; }

        public string statementNo { get; set; }

        public string accountNumber { get; set; }

        public string debcrePrevius { get; set; }

        public DateTime? dateStatPrevius { get; set; }

        public decimal? amountPrevius { get; set; }

        public string debcreEnd { get; set; }

        public DateTime? dateEnd { get; set; }

        public decimal? amountEnd { get; set; }


        public BankHederModel()
        {

            this.entryDate = DateTime.Parse("1900-01-01");
            this.statementNo = String.Empty;
            this.accountNumber = String.Empty;
            this.debcrePrevius = String.Empty;
            this.dateStatPrevius = DateTime.Parse("1900-01-01"); 
            this.amountPrevius = 0;
            this.debcreEnd = String.Empty;
            this.dateEnd = DateTime.Parse("1900-01-01"); 
            this.amountEnd = 0;
           
        }

    }
    public class BankLinesModel
    {
         [DisplayName("Line")]
        public int idBankLine { get; set; }
         [DisplayName("Heder")]
        public int idBankHeder { get; set; }
         [DisplayName("Valuta")]
        public DateTime? valueDate { get; set; }
         [DisplayName("D/C")]
        public string debcreLine { get; set; }
         [DisplayName("Customer")]
        public string idCustomer { get; set; }
         [DisplayName("Amount")]
        public decimal? amountLine { get; set; }
         [DisplayName("Transaction")]
        public string transactType { get; set; }
         [DisplayName("Iban")]
        public string accountNo { get; set; }
         [DisplayName("Name")]
        public string payerLine { get; set; }
         [DisplayName("Reference")]
        public string refNo { get; set; }
         [DisplayName("Description 1")]
        public string desc1Line { get; set; }
         [DisplayName("Description 2")]
        public string desc2Line { get; set; }
         [DisplayName("Description 3")]
        public string desc3Line { get; set; }
         [DisplayName("Description 4")]
        public string desc4Line { get; set; }
         [DisplayName("Description 5")]
        public string desc5Line { get; set; }
         [DisplayName("Description 6")]
        public string desc6Line { get; set; }
         [DisplayName("Description 7")]
        public string desc7Line { get; set; }
         [DisplayName("Description 8")]
        public string desc8Line { get; set; }
         [DisplayName("Description 9")]
        public string desc9Line { get; set; }
       
        public BankLinesModel()
        {

            this.valueDate = DateTime.Parse("1900-01-01");
            this.debcreLine = String.Empty;
            this.idCustomer = String.Empty;
            this.amountLine = 0;
            this.transactType = String.Empty;
            this.accountNo = String.Empty;
            this.payerLine = String.Empty;
            this.refNo = String.Empty;
            this.desc1Line = String.Empty;
            this.desc2Line = String.Empty;
            this.desc3Line = String.Empty;
            this.desc4Line = String.Empty;
            this.desc5Line = String.Empty;
            this.desc6Line = String.Empty;
            this.desc7Line = String.Empty;
            this.desc8Line = String.Empty;
            this.desc9Line = String.Empty;
           
        }

    }

}
