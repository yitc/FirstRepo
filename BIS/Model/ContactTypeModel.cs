using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ContactTypeModel
    {
        [DisplayName("ID contact type")]
        public int idContactType { get; set; }

        [DisplayName("Contact type")]
        public string descContactType { get; set; }

    }
}