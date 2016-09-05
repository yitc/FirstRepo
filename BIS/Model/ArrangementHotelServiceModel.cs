using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementHotelServiceModel : IModel
    {
        [DisplayName("ID service")]
        public int idHotelService { get; set; }

        [DisplayName("Service")]
        public string nameHotelService { get; set; }


        public ArrangementHotelServiceModel()
        {
            this.idHotelService = 0;
            this.nameHotelService = String.Empty;
        }
    }
}
