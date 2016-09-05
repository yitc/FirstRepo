using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public class ProvincesModel : IModel
    {
        [DisplayName("ID Provinces")]
      public int idProvinces { get; set; }

        [DisplayName("Code provinces")]
        public string codeProvinces { get; set; }

        [DisplayName("ID Country")]
        public int idCountry { get; set; }

        //[DisplayName("Country")]
        //public string nameCountry { get; set; }

        [DisplayName("Provinces")]
        public string nameProvinces { get; set; }

        
    }
}
