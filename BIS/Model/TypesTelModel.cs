using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TypesTelModel : IModel
    {
        [DisplayName("ID type")]
        public int idTelType { get; set; }

        [DisplayName("Type")]
        public string nameTelType { get; set; }

    }
}