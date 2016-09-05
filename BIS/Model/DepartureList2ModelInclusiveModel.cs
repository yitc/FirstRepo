using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class DepartureList2ModelInclusiveModel : IModel
    {
        [DisplayName("Date from")]
        public DateTime dateFrom { get; set; }

        [DisplayName("Day")]
        public DateTime dayarr { get; set; }

        [DisplayName("Mounth")]
        public DateTime mounth { get; set; }

        [DisplayName("Arrangement")]
        public DateTime arrangement { get; set; }

        [DisplayName("Booked travelers")]
        public DateTime nr { get; set; }

        [DisplayName("Max travelers")]
        public DateTime nrTraveler { get; set; }

        [DisplayName("Sum of max")]
        public int Sum { get; set; }

        [DisplayName("Mounth Nr")]
        public DateTime mounthNr { get; set; }

        [DisplayName("Date from1")]
        public DateTime dateFrom1 { get; set; }

        [DisplayName("Date to1")]
        public DateTime dateTo1 { get; set; }



    }
}
