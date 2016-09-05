using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccDebCreModel : IModel
    {
        [DisplayName("Id")]
        public int idAccDebCre { get; set; }

        [DisplayName("Person")]
        public int idContPerson { get; set; }

        [DisplayName("Person name")]
        public string namePerson { get; set; }

        [DisplayName("Client")]
        public int idClient { get; set; }

        [DisplayName("Client name")]
        public string nameClient { get; set; }

        [DisplayName("Debitor")]
        public bool isDebitor { get; set; }

        [DisplayName("Creditor")]
        public bool isCreditor { get; set; }

        [DisplayName("Debit account")]
        public string debAccount { get; set; }

        [DisplayName("Account name")]
        public string debNameAccount { get; set; }

        [DisplayName("Credit account")]
        public string creditAccount { get; set; }

        [DisplayName("Account name")]
        public string creNameAccount { get; set; }

        [DisplayName("Payment condition")]
        public int payCondition { get; set; }

        [DisplayName("Iban")]
        public string iban { get; set; }

        [DisplayName("Account number")]
        public string accNumber { get; set; }

    }
}