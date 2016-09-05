using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccTaxModel : IModel
    {

        [DisplayName("Id")]
        public int idTax { get; set; }

        [DisplayName("BTW code")]
        public string codeTax { get; set; }

        [DisplayName("Description")]
        public string descTax { get; set; }

         [DisplayName("Type")]
        public decimal? typeTax { get; set; }

        [DisplayName("Name")]
         public string nameTax { get; set; }

        [DisplayName("Account")]
        public string numberLedAccount { get; set; }

        [DisplayName("Account name")]
        public string nameAccount { get; set; }
    }
}