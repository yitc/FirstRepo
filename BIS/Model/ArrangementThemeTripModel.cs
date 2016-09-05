using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementThemeTripModel : IModel
    {
        [DisplayName("ID theme trip")]
        public int idThemeTrip { get; set; }

        [DisplayName("ID Arrangement")]
        public int idArrangement { get; set; }


        public ArrangementThemeTripModel()
        {
            this.idThemeTrip = 0;
            this.idArrangement = 0;
        }
    }
}
