using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class CountryModel : IModel
    {
        [DisplayName("ID Country")]
        public int idCountry { get; set; }

        [DisplayName("Country code")]
        public string interNationalCode { get; set; }

        [DisplayName("Country")]
        public string nameCountry{ get; set; }

        [DisplayName("Nacionality")]
        public string nacionality { get; set; }

        [DisplayName("Provision")]
        public string provision { get; set; }

        [DisplayName("Premie")]
        public string premie { get; set; }
    }
}
