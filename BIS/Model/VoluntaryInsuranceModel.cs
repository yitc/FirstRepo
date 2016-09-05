using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VoluntaryInsuranceModel 
    {

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Function")]
        public string txtQuest { get; set; }

        [DisplayName("Insurance code")]
        public string code { get; set; }

        [DisplayName("Person name")]
        public string name { get; set; }

        [DisplayName("Days Trip")]
        public int daysTrip { get; set; }

        [DisplayName("Premie")]
        public decimal amountPremie { get; set; }

        [DisplayName("From")]
        public DateTime From { get; set; }

        [DisplayName("To")]
        public DateTime To { get; set; }

        [DisplayName("Username")]
        public string username { get; set; }

        [DisplayName("Arrangement")]
        public string codeArrangement { get; set; }

        
        public VoluntaryInsuranceModel()
        {
            this.dtFromArrangement = DateTime.Now;
            this.code = String.Empty;
            this.txtQuest = String.Empty;
            this.name = String.Empty;
            this.daysTrip = 0;
            this.amountPremie = 0;
            this.From = DateTime.Now;
            this.To = DateTime.Now;
            this.username = String.Empty;
            this.codeArrangement = String.Empty;
        }

      
    }

}
