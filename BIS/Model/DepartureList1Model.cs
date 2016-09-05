using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class DepartureList1Model : IModel
    {
        [DisplayName("Label")]
        public string Label { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("TotalBooked")]
        public string TotalBooked { get; set; }

        [DisplayName("Code arrangement")]
        public string codeArrangement { get; set; }


        [DisplayName("Maximum")]
        public int Maximum { get; set; }

        [DisplayName("Occupancy(%)")]
        public int Occupancy { get; set; }

        [DisplayName("Date from1")]
        public DateTime dateFrom1 { get; set; }

        [DisplayName("Date to1")]
        public DateTime dateTo1 { get; set; }
    }

    
}





