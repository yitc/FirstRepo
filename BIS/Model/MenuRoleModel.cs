using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MenuRoleModel :IModel
    {
        [DisplayName("ID menu")]
        public int idMenu { get; set; }

        [DisplayName("Menu")]
        public string nameMenu { get; set; }

        [DisplayName("ID menu superior")]
        public int? idMenuSuperior { get; set; }

        [DisplayName("ID security")]
        public int? idSecurity { get; set; }

        [DisplayName("Security")]
        public string nameSecurity { get; set; }


    }
}
