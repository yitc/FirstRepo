using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VolAvailabilityModel : IModel
    {
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Id Volontary")]
        public int idContPers { get; set; }

        [DisplayName("Available from")]
        public DateTime? dateFrom { get; set; }

        [DisplayName("Available to")]
        public DateTime? dateTo { get; set; }

        [DisplayName("Available")]
        public int nrTimes { get; set; }

        public VolAvailabilityModel()
        {
            this.id = 0;
            this.idContPers = 0;
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now;
            this.nrTimes = 0;
        }
    }    

}
