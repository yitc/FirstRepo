using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class PersonPassportModel
    {
        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("Passport")]
        public string namePassport { get; set; }

        [DisplayName("Number")]
        public string numberPassport { get; set; }

        [DisplayName("Birth place")]
        public string birthPlacePassport { get; set; }

        [DisplayName("Issue place")]
        public string issuePlacePassport { get; set; }

        [DisplayName("Issue date")]
        public DateTime? dtPassportIssued { get; set; }

        [DisplayName("Valid to")]
        public DateTime? dtPassportValid { get; set; }

        [DisplayName("ID country")]
        public int? idCountry { get; set; }

        [DisplayName("Last name")]
        public string lastNamePassport { get; set; }

    }
}