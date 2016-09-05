using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace BIS.Model
{
    public class PrintReportModel
    {
        public PrintReportModel() { 
            this.idArrangement = 0;
            this.idContactPerson = 0;
            this.reportName="";
        }
        [DisplayName("idArrangement")]
        public int idArrangement { get; set; }
        [DisplayName("IdContactPerson")]
        public int idContactPerson { get; set; }
        [DisplayName("ReportName")]
        public string reportName { get; set; }
    }
}
