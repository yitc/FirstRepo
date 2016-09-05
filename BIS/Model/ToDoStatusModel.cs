using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ToDoStatusModel
    {
        [DisplayName("ID status")]
        public int idStatusToDo { get; set; }

        [DisplayName("description")]
        public string descriptionStatus { get; set; }

    }
}