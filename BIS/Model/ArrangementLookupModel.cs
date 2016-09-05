using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementLookupModel : IModel
    {

        [DisplayName("Arrangement id")]
        public int idArrangement { get; set; }

        [DisplayName("Status")]
        public string statusArrangement { get; set; }

        [DisplayName("Arrangement code")]
        public string codeArrangement { get; set; }

        [DisplayName("Arrangement name")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("Number of nights")]
        public int nrOfNights { get; set; }

        [DisplayName("City")]
        public string cityArrangement { get; set; }

        [DisplayName("Country")]
        public int countryArrangement { get; set; }
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

        [DisplayName("ID HotelService")]
        public int idHotelService { get; set; }

        [DisplayName("HotelService")]
        public string nameHotelService { get; set; }

        [DisplayName("WEB")]
        public bool isWeb { get; set; }

        [DisplayName("Male Voluntary")]
        public int nrMaleVoluntary { get; set; }

        [DisplayName("ID age category")]
        public int idAgeCategory { get; set; }

        [DisplayName("Age category")]
        public string descAgeCategory { get; set; }

        [DisplayName("Max wheelchairs")]
        public int nrMaximumWheelchairs { get; set; }

        [DisplayName("Whoose wheelchairs")]
        public int whoseElectricWheelchairs { get; set; }

        [DisplayName("Nr. supporting arms")]
        public int buSupportingArms { get; set; }

        [DisplayName("Rollators")]
        public int buRollators{get; set;}
              
        [DisplayName("Days first payment")]
        public int daysFirstPayment { get; set; }

        [DisplayName("Days last payment")]
        public int daysLastPayment { get; set; }

        [DisplayName("Percent first payment")]
        public decimal percentFirstPayment { get; set; }

        [DisplayName("Reservation costs")]
        public decimal reservationCosts { get; set; }

        [DisplayName("Anchorage")]
        public int nrAnchorage { get; set; }

        public ArrangementLookupModel()
        {
            this.idArrangement = 0;
            this.statusArrangement = String.Empty;
            this.codeArrangement = String.Empty;
            this.nameArrangement = String.Empty;
            this.dtFromArrangement = DateTime.Now;
            this.dtToArrangement = DateTime.Now;
            this.nrOfNights = 0;
            this.cityArrangement = String.Empty;
            this.countryArrangement = 0;
            this.countryNameArrangement = String.Empty;
            this.typeArrangement = -1;
            this.typeNameArrangement = String.Empty;
            this.nrTraveler = 0;
            this.minNrTraveler = 0;
            this.nrVoluntaryHelper = 0;
            this.idHotelService = 0;
            this.nameHotelService = String.Empty;
            this.isWeb = false;
            this.nrMaleVoluntary = 0;
            this.idAgeCategory = 0;
            this.descAgeCategory = String.Empty;
            this.nrMaximumWheelchairs = 0;
            this.whoseElectricWheelchairs = 0;
            this.buSupportingArms = 0;
            this.buRollators = 0;
            this.daysFirstPayment = 0;
            this.daysLastPayment = 0;
            this.percentFirstPayment = 0;
            this.reservationCosts = 0;
            this.nrAnchorage = 0;
        }

        //public ArrangementLookupModel(ArrangementLookupModel model)
        //{
        //    this.idArrangement = model.idArrangement;
        //    this.statusArrangement = model.statusArrangement;
        //    this.codeArrangement = model.codeArrangement;
        //    this.nameArrangement = model.nameArrangement;
        //    this.dtFromArrangement = model.dtFromArrangement;
        //    this.dtToArrangement = model.dtToArrangement;
        //    this.nrOfNights = model.nrOfNights;
        //    this.cityArrangement = model.cityArrangement;
        //    this.countryArrangement = model.countryArrangement;
        //    this.countryNameArrangement = model.countryNameArrangement;
        //    this.typeArrangement = model.typeArrangement;
        //    this.typeNameArrangement = model.typeNameArrangement;
        //    this.nrTraveler = model.nrTraveler;
        //    this.minNrTraveler = model.minNrTraveler;
        //    this.nrVoluntaryHelper = model.nrVoluntaryHelper;
        //    this.idHotelService = model.idHotelService;
        //    this.nameHotelService = model.nameHotelService;
        //    this.isWeb = model.isWeb;
        //    this.nrMaleVoluntary = model.nrMaleVoluntary;
        //    this.idAgeCategory = model.idAgeCategory;
        //    this.descAgeCategory = model.descAgeCategory;
        //    this.nrMaximumWheelchairs = model.nrMaximumWheelchairs;
        //    this.whoseElectricWheelchairs = model.whoseElectricWheelchairs;
        //    this.buSupportingArms = model.buSupportingArms;
        //    this.price = model.price;
        //    this.daysFirstPayment = model.daysFirstPayment;
        //    this.daysLastPayment = model.daysLastPayment;
        //    this.percentFirstPayment = model.percentFirstPayment;
        //    this.reservationCosts = model.reservationCosts;
        //    this.nrAnchorage = model.nrAnchorage;
        //}

      

    }
}
