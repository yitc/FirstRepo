using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class ArrangementCalculationFirstArticlesModel
    {

        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Id arrangament")]
        public int idArrangement { get; set; }

        [DisplayName("Id article")]
        public string idArticle { get; set; }

        [DisplayName("Is contract")]
        public bool isContract { get; set; }

        public ArrangementCalculationFirstArticlesModel()
        {
            this.id = 0;
            this.idArrangement = 0;
            this.idArticle = String.Empty;
            this.isContract = false;
        }

    }

    public class ArrangementCalculationFirstNotArticlesModel
    {
        [DisplayName("Id arrangament")]
        public int idArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("Number of travelers")]
        public int nrTraveler { get; set; }

        [DisplayName("Minimum number of travelers")]
        public int minNrTraveler { get; set; }

        [DisplayName("Number of voluntary helpers")]
        public int nrVoluntaryHelper { get; set; }

        [DisplayName("Number of nights")]
        public int nrOfNights { get; set; }

        [DisplayName("ID country")]
        public int idCountry { get; set; }

        [DisplayName("Max wheelchairs")]
        public int nrMaximumWheelchairs { get; set; }

        [DisplayName("Whoose wheelchairs")]
        public int whoseElectricWheelchairs { get; set; }

        [DisplayName("Nr. supporting arms")]
        public int buSupportingArms { get; set; }

        [DisplayName("Anchorage")]
        public int nrAnchorage { get; set; }

        [DisplayName("User finished")]
        public int idUserFinished { get; set; }

        [DisplayName("Date finished")]
        public DateTime dtUserFinished  { get; set; }

        public ArrangementCalculationFirstNotArticlesModel()
        {
            this.idArrangement = 0;
            this.dtFromArrangement = DateTime.Now;
            this.dtToArrangement = DateTime.Now;
            this.nrTraveler = 0;
            this.minNrTraveler = 0;
            this.nrVoluntaryHelper = 0;
            this.nrOfNights = 0;
            this.idCountry = 0;
            this.nrMaximumWheelchairs = 0;
            this.whoseElectricWheelchairs = 0;
            this.buSupportingArms = 0;
            this.nrAnchorage = 0;
            this.idUserFinished = 0;
            this.dtUserFinished = DateTime.Now;

        }



    }
}
