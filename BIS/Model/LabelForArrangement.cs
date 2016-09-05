using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LabelForArrangement
    {
        [DisplayName("ID label")]
        public int idLabel { get; set; }

        [DisplayName("ID arrangement")]
        public int idArrangement { get; set; }
    }
}
