using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrTypeModel : IModel
    {

        [DisplayName("ID")]
        public int idArrType { get; set; }

        [DisplayName("Name")]
        public string nameArrType { get; set; }
    }
}