using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.Model
{
    public class ParticipantListReportModel : IModel
    {



        [DisplayName("Arrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("Title")]
        public string nameTitle { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Boarding point")]
        public string nameBoardingPoint { get; set; }


        [DisplayName("City")]
        public string cityArrangement { get; set; }

        [DisplayName("Arrangement name1")]
        public string nameArrangement1 { get; set; }

        [DisplayName("Date from1")]
        public DateTime dtFromArrangement1 { get; set; }

        [DisplayName("Date to1")]
        public DateTime dtToArrangement1 { get; set; }

        [DisplayName("Code arrangement1")]
        public DateTime codeArrangement1 { get; set; }
       



      

    }

}
