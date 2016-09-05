using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementInsurancePremieModel : IModel
    {
        [DisplayName("ID premie")]
        public int idPremie { get; set; }

        [DisplayName("Premie")]
        public string premie { get; set; }

        [DisplayName("Code insurance")]
        public string codeInsurance { get; set; }

        [DisplayName("Ammount")]
        public decimal? amountPremie { get; set; }

        [DisplayName("Valid from")]
        public DateTime? dtValidFrom { get; set; }

        [DisplayName("Valid to")]
        public DateTime? dtValidTo { get; set; }


        public ArrangementInsurancePremieModel()
        {
            this.idPremie = 0;
            this.premie = String.Empty;
            this.codeInsurance = String.Empty;
            this.amountPremie = 0;
            this.dtValidFrom = DateTime.Now;
            this.dtValidTo = DateTime.Now;
        }
    }
}
