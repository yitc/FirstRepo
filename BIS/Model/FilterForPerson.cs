using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class FilterForPerson
    {
        [DisplayName("ID filter")]
        public int idFilter { get; set; }

        [DisplayName("ID person")]
        public int idContPers { get; set; }
    }
}
