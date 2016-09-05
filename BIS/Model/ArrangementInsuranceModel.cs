using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementInsuranceModel : IModel
    {
        [DisplayName("ID insurance")]
        public int idInsurance { get; set; }

        [DisplayName("Label insurance")]
        public string labelInsurance { get; set; }

        [DisplayName("Code insurance")]
        public string codeInsurance { get; set; }

        [DisplayName("Ammount")]
        public decimal? amountInsurance { get; set; }

        [DisplayName("Valid from")]
        public DateTime? dtValidFrom { get; set; }

        [DisplayName("Valid to")]
        public DateTime? dtValidTo { get; set; }

        public ArrangementInsuranceModel()
        {
            this.idInsurance = 0;
            this.labelInsurance = String.Empty;
            this.codeInsurance = String.Empty;
            this.amountInsurance = 0;
            this.dtValidFrom = DateTime.Now;
            this.dtValidTo = DateTime.Now;
        }

    }

     public class ArrangementInsuranceLabelModel
     {
         public string Name { get; set; }

         public ArrangementInsuranceLabelModel()
         {
             this.Name = String.Empty;
         }
     }

     public class ArrangementTravelInsuranceModel : IModel
     {
         [DisplayName("ID insurance")]
         public int idArrangementTravelInsurance { get; set; }

         [DisplayName("Description")]
         public string description { get; set; }

         [DisplayName("Ledger account")]
         public string ledgerAccount { get; set; }

         [DisplayName("Code insurance")]
         public string codeInsurance { get; set; }

         [DisplayName("Medical devices")]
         public Boolean isMedicalDevices { get; set; }

         [DisplayName("Sport Activity")]
         public Boolean isSportActivity { get; set; }

         public ArrangementTravelInsuranceModel()
         {
             this.idArrangementTravelInsurance = 0;
             this.description = String.Empty;
             this.ledgerAccount = String.Empty;
             this.codeInsurance = String.Empty;
             this.isMedicalDevices = false;
             this.isSportActivity = false;
         }

     }
}
