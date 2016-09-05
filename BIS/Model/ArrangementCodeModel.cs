using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
 public   class ArrangementCodeModel : IModel
    {

        [DisplayName("Arrangement id")]
        public int idArrangement { get; set; }

        [DisplayName("Arrangement code")]
        public string codeArrangement { get; set; }

        [DisplayName("Arrangement name")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }
    }
}
