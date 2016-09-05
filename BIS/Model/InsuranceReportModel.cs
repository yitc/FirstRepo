using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public class InsuranceReportModel: IModel
    {
        [DisplayName("Invoice Number")]
        public string invoiceNr { get; set; }

        [DisplayName("Invoice date")]
        public DateTime? dtInvoice { get; set; }

        [DisplayName("Date from")]
        public DateTime? dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime? dtTo { get; set; }

        [DisplayName("Treveller")]
        public string namePerson { get; set; }

        [DisplayName("Price item")]
        public decimal price { get; set; }

       [DisplayName("Arrangement name")]
        public string nameArrangement { get; set; }

       [DisplayName("Param date from")]
       public string ParamDateFrom { get; set; }

       [DisplayName("Param date to")]
       public string ParamDateTo { get; set; }

       [DisplayName("User name")]
       public string userName { get; set; }

      [DisplayName("Code arrangement")]
       public string codeArrangement { get; set; }

       public InsuranceReportModel()
        {
            this.invoiceNr = String.Empty;
            this.dtInvoice = DateTime.Now;
            this.dtFrom = DateTime.Now;
            this.nameArrangement = String.Empty;
            this.dtTo = DateTime.Now;
            this.namePerson = String.Empty;
            this.price = 0;
            this.ParamDateFrom = String.Empty;
            this.ParamDateTo = String.Empty;
            this.userName = String.Empty;
            this.codeArrangement = String.Empty;
        }
      
     
    }
}
