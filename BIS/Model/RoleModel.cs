using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class RoleModel : IModel
    {
        [DisplayName("ID role")]
        public int idRole { get; set; }

        [DisplayName("Role")]
        public string nameRole { get; set; }
    }
}
