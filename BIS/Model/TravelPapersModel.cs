using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
 public class TravelPapersModel : IModel
    {
        //[DisplayName("ID travel papers")]
        //public int idTravelPapers { get; set; }

        [DisplayName("Name travel paper")]
        public string nameTravelPapers { get; set; }

    }
}
