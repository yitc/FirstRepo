using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class FiltersLabelsModel : IModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Menu")]
        public string nameMenu { get; set; }

        [DisplayName("Unique")]
        public string uniques { get; set; }

        [DisplayName("ID label unique")]
        public int IDLabelUnique { get; set; }

       

    }
}
