using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LabelModel
    {
        [DisplayName("ID label")]
        public int idLabel { get; set; }

        [DisplayName("Label")]
        public string nameLabel { get; set; }
    }
}
