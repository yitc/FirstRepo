using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrNrModel
    {
        
        
            [DisplayName ("ID")]
            public int idTbl { get; set; }

            [DisplayName("Invoice")]
            public int  nrArrFak { get; set; }

            [DisplayName("Sepa")]
            public int nrSEPA { get; set; }
    }
}