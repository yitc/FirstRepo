using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementGridDaysModel : IModel
    {
        [DisplayName("Date")]
        public DateTime date { get; set; }

        [DisplayName("Day")]
        public string day { get; set; }

        [DisplayName("Book")]
        public bool book { get; set; }
             
    }
}