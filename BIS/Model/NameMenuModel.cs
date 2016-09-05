using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public  class NameMenuModel
    {
        [DisplayName("idMenu")]
        public int idMenu { get; set; }

        [DisplayName("Menu")]
        public string nameMenu { get; set; }
    }
}
