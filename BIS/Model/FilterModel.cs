using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class FilterModel
    {
        [DisplayName("ID filter")]
        public int idFilter { get; set; }

        [DisplayName("Filter")]
        public string nameFilter { get; set; }

        [DisplayName("Sort")]
        public int sortFilter { get; set; }

    }
}
