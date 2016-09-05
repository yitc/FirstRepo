using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementBookTravelPapersModel : IModel
    {
        [DisplayName("ID travel papers")]
        public int idTravelPapers { get; set; }

        [DisplayName("Travel papers")]
        public string nameTravelPapers { get; set; }


        public ArrangementBookTravelPapersModel()
        {
            this.idTravelPapers = 0;
            this.nameTravelPapers = String.Empty;
        }
    }
}
