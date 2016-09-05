using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class HotelServicesModel : IModel
    {
    
        [DisplayName("Id Hotel Service")]
        public int idHotelService { get; set; }

        [DisplayName("Name Hotel Service")]
        public string nameHotelService { get; set; }
    }
}
