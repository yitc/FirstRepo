using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PersonReasonInModel : IModel
    {
        [DisplayName("Id reason In")]
        public int idReasonIn { get; set; }

        [DisplayName("Reason In")]
        public string nameReasonIn { get; set; }

      
    }
}