using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class RoleSecurityModel
    {
 
        [DisplayName("ID menu")]
        public int idMenu { get; set; }

        [DisplayName("Name menu")]
        public string nameMenu { get; set; }

        [DisplayName("ID menu superior")]
        public int? idMenuSuperior { get; set; }

        [DisplayName("By")]
        public string By { get; set; }

        [DisplayName("ID security")]
        public int? idSecurity { get; set; }

        [DisplayName("Name security")]
        public string nameSecurity { get; set; }


    }
}
