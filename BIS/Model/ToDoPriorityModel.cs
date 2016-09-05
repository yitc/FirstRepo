using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ToDoPriorityModel
    {
        [DisplayName("ID priority")]
        public int idPriorityToDo { get; set; }

        [DisplayName("Description")]
        public string descriptionPriority { get; set; }


    }
}