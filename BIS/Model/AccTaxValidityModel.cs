using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccTaxValidityModel : IModel
    {
        [DisplayName("Id")]
        public int idTaxValidity { get; set; }

         [DisplayName("BTW code")]
        public string codeTax { get; set; }

         [DisplayName("Percent")]
        public decimal? percentTax { get; set; }

         [DisplayName("Start date")]
         public DateTime startDate { get; set; }

         [DisplayName("End date")]
         public DateTime endDate { get; set; }

    }
}