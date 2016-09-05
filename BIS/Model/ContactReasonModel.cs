using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ContactReasonModel
    {
        [DisplayName("ID Contact reason")]
        public int idContactReason { get; set; }

        [DisplayName("Contact reason")]
        public string descContactReason { get; set; }



    }
}