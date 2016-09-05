using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public  class ArrangementAllModel : IModel
    {

        [DisplayName("Arrangement id")]
        public int idArrangement { get; set; }

        [DisplayName("Arrangement code")]
        public string codeArrangement { get; set; }

        [DisplayName("Arrangement name")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("City")]
        public string cityArrangement { get; set; }

        [DisplayName("Country")]
        public int? countryArrangement { get; set; }

        [DisplayName("Country name")]
        public string countryNameArrangement { get; set; }

        [DisplayName("Type")]
        public int? typeArrangement { get; set; }

        [DisplayName("Type name")]
        public string typeNameArrangement { get; set; }

        [DisplayName("Number of travelers")]
        public int nrTraveler { get; set; }

        [DisplayName("Minimum number of travelers")]
        public int minNrTraveler { get; set; }

        [DisplayName("Number of voluntary helpers")]
        public int nrVoluntaryHelper { get; set; }

        [DisplayName("ID arrangement book")]
        public int idArrangementBook { get; set; }

        [DisplayName("ID status")]
        public int idStatus { get; set; }

        [DisplayName("Status name")]
        public string nameStatus { get; set; }

        [DisplayName("ID travel papers")]
        public int idTravelPapers { get; set; }

        [DisplayName("Name travel papers")]
        public string nameTravelPapers { get; set; }

        [DisplayName("Price")]
        public decimal price { get; set; }

        [DisplayName("Max wheelchairs")]
        public int nrMaximumWheelchairs { get; set; }

        [DisplayName("Whoose wheelchairs")]
        public int whoseElectricWheelchairs { get; set; }

        [DisplayName("Nr. supporting arms")]
        public int buSupportingArms { get; set; }

        [DisplayName("Anchorage")]
        public int nrAnchorage { get; set; }

        public ArrangementAllModel()
        {
            this.idArrangement = 0;
            this.codeArrangement = String.Empty;
            this.nameArrangement = String.Empty;
            this.dtFromArrangement = DateTime.Now;
            this.dtToArrangement = DateTime.Now;
            this.cityArrangement = String.Empty;
            this.countryArrangement = 0;
            this.countryNameArrangement = String.Empty;
            this.typeArrangement = -1;
            this.typeNameArrangement = String.Empty;
            this.nrTraveler = 0;
            this.minNrTraveler = 0;
            this.nrVoluntaryHelper = 0;
            this.idStatus = 0;
            this.idTravelPapers = 0;
            this.price = 0;
            this.nrMaximumWheelchairs = 0;
            this.whoseElectricWheelchairs = 0;
            this.buSupportingArms = 0;
            this.nrAnchorage = 0;
        }

    }
    
}
