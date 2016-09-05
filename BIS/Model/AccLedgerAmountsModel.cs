using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccLedgerAmountsModel : IModel
    {
        public int idAccount {get; set;}

        public string numberLedgerAccount { get; set; }

        public string bookingYear { get; set; }

        public decimal? beginDebit { get; set; }

        public decimal? beginCredit { get; set; }

        public decimal? debitAmount { get; set; }

        public decimal? creditAmount { get; set; }

        public int? transactionsNo { get; set; }

        public int? userCreated { get; set; }

        public DateTime? dtCreated { get; set; }

        public int? userModified { get; set; }

        public DateTime? dtModified { get; set; }

        public AccLedgerAmountsModel()
        {
            this.beginDebit = 0;
            this.beginCredit = 0;
            this.debitAmount = 0;
            this.creditAmount = 0;
            this.transactionsNo = 0;
            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.dtModified = Convert.ToDateTime("1900-01-01");
        }
    }
}
