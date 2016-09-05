using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model 
{
    public class PersonAddressModel 
    {
        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("ID Address")]
        public int? idAddressType { get; set; }

        [DisplayName("Street")]
        public string street { get; set; }

         [DisplayName("House nr")]
        public string housenr { get; set; }

         [DisplayName("Extension")]
        public string extension { get; set; }

        [DisplayName("Zip code")]
        public string postalCode { get; set; }

         [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Country")]
        public string country { get; set; }

        [DisplayName("Region 1")]
        public string region1 { get; set; }

         [DisplayName("Region 2")]
        public string region2 { get; set; }

        [DisplayName("International")]
        public bool isInternational { get; set; }

    }
}