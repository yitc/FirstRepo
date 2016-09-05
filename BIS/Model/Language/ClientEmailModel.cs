using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class ClientEmailModel
    {
        public int idEmail { get; set; }

        [DisplayName("Client")]
        public int idClient { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("M")]
        public bool isCommunication { get; set; }

        [DisplayName("R")]
        public bool isProspect { get; set; }

        [DisplayName("F")]
        public bool isInvoicing { get; set; }

        public int? idEmailType { get; set; }


    }
}
