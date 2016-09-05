using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class OptionsModel : IModel
    {
        [DisplayName("Id option")]
        public int idOption { get; set; }

        [DisplayName("Name option")]
        public string nameOption { get; set; }
    }
}
