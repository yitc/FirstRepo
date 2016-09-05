using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PeriodReportModel
    {

        public string class1 { get; set; }

        public string class2 { get; set; }

        public string class3 { get; set; }

        public string class4 { get; set; }

        public string class5 { get; set; }

        public string numberLedgerAccount { get; set; }

        public string description { get; set; }

        public decimal period0 { get; set; }

        public decimal period1 { get; set; }

        public decimal period2 { get; set; }

        public decimal period3 { get; set; }

        public decimal period4 { get; set; }

        public decimal period5 { get; set; }

        public decimal period6 { get; set; }

        public decimal period7 { get; set; }

        public decimal period8 { get; set; }

        public decimal period9 { get; set; }

        public decimal period10 { get; set; }

        public decimal period11 { get; set; }

        public decimal period12 { get; set; }

        public Boolean period1Visible { get; set; }

        public Boolean period2Visible { get; set; }

        public Boolean period3Visible { get; set; }

        public Boolean period4Visible { get; set; }

        public Boolean period5Visible { get; set; }

        public Boolean period6Visible { get; set; }

        public Boolean period7Visible { get; set; }

        public Boolean period8Visible { get; set; }

        public Boolean period9Visible { get; set; }

        public Boolean period10Visible { get; set; }

        public Boolean period11Visible { get; set; }

        public Boolean period12Visible { get; set; }

        public string type { get; set; }
        public string typeClassification { get; set; }
        public string year {get; set;}



       

        public PeriodReportModel()
        {
            this.class1 = String.Empty;
            this.class2 = String.Empty;
            this.class3 = String.Empty;
            this.class4 = String.Empty;
            this.class5 = String.Empty;
            this.numberLedgerAccount = String.Empty;
            this.description = String.Empty;
            this.period0 = 0;
            this.period1 = 0;
            this.period2 = 0;
            this.period3 = 0;
            this.period4 = 0;
            this.period5 =0;
            this.period6 = 0;
            this.period7 = 0;
            this.period8 = 0;
            this.period9 = 0;
            this.period10 = 0;
            this.period11 = 0;
            this.period12 = 0;
            this.period1Visible = true;
            this.period2Visible = true;
            this.period3Visible = true;
            this.period4Visible = true;
            this.period5Visible = true;
            this.period6Visible = true;
            this.period7Visible = true;
            this.period8Visible = true;
            this.period9Visible = true;
            this.period10Visible = true;
            this.period11Visible = true;
            this.period12Visible = true;
            this.type = String.Empty;
            this.year = DateTime.Now.Year.ToString();
            this.typeClassification = String.Empty;

        }


    }
}
