using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TypesMenuModel : IModel
    {
        [DisplayName("ID type")]
        public int idMenu { get; set; }

        [DisplayName("Type")]
        public string nameMenu { get; set; }

    }
}
