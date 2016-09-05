using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ToDoTypeModel
    {
        [DisplayName("ID type")]
        public int idToDoType { get; set; }

        [DisplayName("Type")]
        public string descriptionToDoType { get; set; }

    }
}