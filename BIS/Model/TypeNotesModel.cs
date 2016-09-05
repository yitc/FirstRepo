using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TypeNotesModel
    {
        [DisplayName("ID type")]
        public int idTypeNote { get; set;}

        [DisplayName("Type")]
        public string nameTypeNote { get; set; }

    }
}
