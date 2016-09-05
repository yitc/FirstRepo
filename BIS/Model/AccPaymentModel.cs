using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccPaymentModel : IModel
    {
        [DisplayName ("Id Payment")]
        public int idPayment { get; set; }

        [DisplayName ("Number days")]
        public int? numberDays { get; set; }

        [DisplayName ("Debitor")]
        public bool isDebitor { get; set; }

        [DisplayName("Creditor")]
        public bool isCreditor { get; set; }

        [DisplayName ("Description")]
        public string description { get; set; }

    }
}
