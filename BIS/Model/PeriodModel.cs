using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PeriodModel : IModel
    {

        [DisplayName("ID period")]
        public int idPeriod { get; set; }

        [DisplayName("Description")]
        public string descPeriod { get; set; }

        [DisplayName("Month from")]
        public int monthFrom { get; set; }

        [DisplayName("Month to")]
        public int monthTo { get; set; }

        public PeriodModel()
        {
            this.idPeriod = 0;
            this.descPeriod = String.Empty;
            this.monthFrom = 0;
            this.monthTo = 0;

        }
    }
}
