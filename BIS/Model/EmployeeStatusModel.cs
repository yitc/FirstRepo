using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class EmployeeStatusModel
    {
        [DisplayName("ID Status")]
        public int idStatusEmployee { get; set; }

        [DisplayName("Description")]
        public string descriptionEmployee { get; set; }

    }
}