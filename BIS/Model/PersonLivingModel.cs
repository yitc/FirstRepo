using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PersonLivingModel
    {
        [DisplayName("ID Living")]
        public int idLiving { get; set; }

        [DisplayName("Name")]
        public string nameLiving { get; set; }

    }
}