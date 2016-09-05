using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PersonReasonOutModel : IModel
    {
        [DisplayName("Id reason Out")]
        public int idReasonOut { get; set; }

        [DisplayName("Reason Out")]
        public string nameReasonOut { get; set; }


    }
}