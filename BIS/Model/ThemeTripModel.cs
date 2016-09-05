using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ThemeTripModel : IModel
    {
        [DisplayName("ID theme trip")]
        public int idThemeTrip { get; set; }

        [DisplayName("Theme trip")]
        public string nameThemeTrip { get; set; }


        public ThemeTripModel()
        {
            this.idThemeTrip = 0;
            this.nameThemeTrip = String.Empty;
        }
    }
}
