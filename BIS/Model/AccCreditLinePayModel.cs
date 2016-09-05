using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccCreditLinePayModel : IModel
    {
        [DisplayName("ID")]
        public int idCreditLinePay { get; set; }

        [DisplayName("Creditor")]
        public string accNumber { get; set; }
        [DisplayName("Invoice")]
        public string invoiceNr { get; set; }
        [DisplayName("Term")]
        public int? term { get; set; }
        [DisplayName("Percent")]
        public decimal? percentpay { get; set; }
        [DisplayName("Amount")]
        public decimal? amount { get; set; }
        [DisplayName("Date")]
        public DateTime dtDate { get; set; }



        public AccCreditLinePayModel()
        {
            this.accNumber = String.Empty;
            this.invoiceNr = String.Empty;
            this.term = 0;
            this.percentpay = 0;
            this.amount = 0;
            this.dtDate = Convert.ToDateTime("1900-01-01");


        }

    }
}