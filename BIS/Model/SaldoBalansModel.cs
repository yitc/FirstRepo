using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class SaldoBalansModel 
    {

        public string class1 { get; set; }

        public string class2 { get; set; }

        public string class3{ get; set; }

        public string class4 { get; set; }

        public string class5 { get; set; }
     
        public string numberLedgerAccount { get; set; }
      
        public string description { get; set; }
  
        public decimal beginDebit { get; set; }

        public decimal beginCredit { get; set; }

        public decimal sumDebit { get; set; }

        public decimal sumCredit { get; set; }

        public DateTime ParamDateFrom { get; set; }

        public DateTime ParamDateTo { get; set; }

        public string paramLineYear { get; set; }

        public string type { get; set; }
  
        public SaldoBalansModel()
        {
            this.class1 = String.Empty;
            this.class2 = String.Empty;
            this.class3 = String.Empty;
            this.class4 = String.Empty;
            this.class5 = String.Empty;
            this.numberLedgerAccount = String.Empty;
            this.description = String.Empty;
            this.beginCredit = 0;
            this.beginDebit = 0;
            this.sumCredit = 0;
            this.sumDebit = 0;
            this.ParamDateFrom = DateTime.Now;
            this.ParamDateTo = DateTime.Now;
            this.paramLineYear = DateTime.Now.Year.ToString();
            this.type = String.Empty;

        }

       
    }
}
