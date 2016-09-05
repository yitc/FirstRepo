using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
   public class PrognoseReportModel
    {
        public int idArrangement { get; set; }

        public string codeArrangement { get; set; }

        public DateTime dtFromArrangement { get; set; }

        public decimal total { get; set; }

        public decimal subtotal { get; set; }

        public decimal total2 { get; set; }

        public decimal subtotal2 { get; set; }

        public decimal totalSecond { get; set; }
    
        public decimal subTotalSecondPla { get; set; }

        public decimal subTotalSecondAp { get; set; }

        public string userName { get; set; }

        public DateTime ParamDateTo { get; set; }

        public DateTime ParamDateFrom { get; set; }

        public string ParamLabel { get; set; }

        public PrognoseReportModel()
        {
            this.idArrangement = -1;
            this.dtFromArrangement = DateTime.Now;
            this.codeArrangement = String.Empty;
            this.total = 0;
            this.subtotal = 0;
            this.total2 = 0;
            this.subtotal2 = 0;
            this.totalSecond = 0;
            this.subTotalSecondPla = 0;
            this.subTotalSecondAp = 0;
            this.userName = String.Empty;
            this.ParamDateFrom = DateTime.Now;
            this.ParamDateTo = DateTime.Now;
            this.ParamLabel = String.Empty;
        }
      
    }
}
