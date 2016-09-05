using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
   public class TypesSecurityModel
    {
       [DisplayName("ID security")]
        public int idSecurity { get; set; }

       [DisplayName("Security")]
        public string nameSecurity { get; set; }
    }
}
