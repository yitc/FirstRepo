using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.Model
{
    public class DeviceReportModel : IModel
    {



        [DisplayName("Arrangement name")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("Code arrangement")]
        public DateTime codeArrangement { get; set; }

        [DisplayName("Title")]
        public string nameTitle { get; set; }

        [DisplayName("Name")]
        public string  name { get; set; }
        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Birth date")]
        public DateTime birthdate { get; set; }
        [DisplayName("Gender")]
        public string gender { get; set; }


        [DisplayName("Arrangement name1")]
        public string nameArrangement1 { get; set; }

        [DisplayName("Date from1")]
        public DateTime dtFromArrangement1 { get; set; }

        [DisplayName("Date to1")]
        public DateTime dtToArrangement1 { get; set; }

        [DisplayName("Code arrangement1")]
        public DateTime codeArrangement1 { get; set; }


        public DeviceReportModel()
        {


            this.nameArrangement = String.Empty;
            this.dtFromArrangement = DateTime.Now;
            this.dtToArrangement = DateTime.Now;
            this.nameTitle = String.Empty;

            this.name = String.Empty;

            this.address = String.Empty;
            this.birthdate = DateTime.Now;
            this.gender = String.Empty;

        }

    }

}
