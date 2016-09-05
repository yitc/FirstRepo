using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccIbanModel : IModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Acc number")]
        public string accNumber { get; set; }

        [DisplayName("Id client")]
        public int idClient { get; set; }

        [DisplayName("Id person")]
        public int idContPers { get; set; }

        [DisplayName("IBAN")]
        public string ibanNumber { get; set; }

        public AccIbanModel()
        {
            this.accNumber = String.Empty;
            this.idClient = 0;
            this.idContPers = 0;
            this.ibanNumber = String.Empty;
        }
    }
}
