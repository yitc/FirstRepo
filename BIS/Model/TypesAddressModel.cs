using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TypesAddresslModel : IModel
    {
        [DisplayName("ID type")]
        public int idAddressType { get; set; }

        [DisplayName("Type")]
        public string nameAddressType { get; set; }

        [DisplayName("Show")]
        public bool showInControl { get; set; }

    }
}
