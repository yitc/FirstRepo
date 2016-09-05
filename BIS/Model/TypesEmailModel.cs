using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TypesEmailModel : IModel
    {
        [DisplayName("ID type")]
        public int idEmailType { get; set; }

        [DisplayName("Type")]
        public string nameEmailType { get; set; }

    }
}