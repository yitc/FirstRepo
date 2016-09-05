using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class GenderModel 
    {
        [DisplayName("ID Gender")]
        public int idGender { get; set; }

        [DisplayName("Gender")]
        public string nameGender { get; set; }
    }
}
