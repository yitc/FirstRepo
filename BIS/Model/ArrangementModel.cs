using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementModel : IModel
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

        [DisplayName("Booked")]
        public int booked { get; set; }

        [DisplayName("Optional")]
        public int optionalBooked { get; set; }

        [DisplayName("Free")]
        public int freePlaces { get; set; }

        [DisplayName("Price")]
        public decimal price { get; set; }

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

        [DisplayName("Invoice description")]
        public string invoiceDescription { get; set; }

        [DisplayName("Account code")]
        public string codeProject { get; set; }

        [DisplayName("Client invoice")]
        public int idClientInvoice { get; set; }

        [DisplayName("Name client")]
        public string nameClient { get; set; }

        public ArrangementModel()
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
            this.booked = 0;
            this.optionalBooked = 0;
            this.freePlaces = 0;
            this.price = 0;
            this.daysFirstPayment = 0;
            this.daysLastPayment = 0;
            this.percentFirstPayment = 0;
            this.reservationCosts = 0;
            this.nrAnchorage = 0;
            this.invoiceDescription = String.Empty;
            this.codeProject = String.Empty;
            this.idClientInvoice = 0;
            this.nameClient = String.Empty;
        }

        public ArrangementModel(ArrangementModel model)
        {
            this.idArrangement = model.idArrangement;
            this.statusArrangement = model.statusArrangement;
            this.codeArrangement = model.codeArrangement;
            this.nameArrangement = model.nameArrangement;
            this.dtFromArrangement = model.dtFromArrangement;
            this.dtToArrangement = model.dtToArrangement;
            this.nrOfNights = model.nrOfNights;
            this.cityArrangement = model.cityArrangement;
            this.countryArrangement = model.countryArrangement;
            this.countryNameArrangement = model.countryNameArrangement;
            this.typeArrangement = model.typeArrangement;
            this.typeNameArrangement = model.typeNameArrangement;
            this.nrTraveler = model.nrTraveler;
            this.minNrTraveler = model.minNrTraveler;
            this.nrVoluntaryHelper = model.nrVoluntaryHelper;
            this.idHotelService = model.idHotelService;
            this.nameHotelService = model.nameHotelService;
            this.isWeb = model.isWeb;
            this.nrMaleVoluntary = model.nrMaleVoluntary;
            this.idAgeCategory = model.idAgeCategory;
            this.descAgeCategory = model.descAgeCategory;
            this.nrMaximumWheelchairs = model.nrMaximumWheelchairs;
            this.whoseElectricWheelchairs = model.whoseElectricWheelchairs;
            this.buSupportingArms = model.buSupportingArms;
            this.booked = model.booked;
            this.optionalBooked = model.optionalBooked;
            this.freePlaces = model.freePlaces;
            this.price = model.price;
            this.daysFirstPayment = model.daysFirstPayment;
            this.daysLastPayment = model.daysLastPayment;
            this.percentFirstPayment = model.percentFirstPayment;
            this.reservationCosts = model.reservationCosts;
            this.nrAnchorage = model.nrAnchorage;
            this.invoiceDescription = model.invoiceDescription;
            this.codeProject = model.codeProject;
            this.idClientInvoice = model.idClientInvoice;
            this.nameClient = model.nameClient;
        }

        public void CopyValues(ArrangementModel source)
        {
            this.idArrangement = source.idArrangement;
            this.statusArrangement = source.statusArrangement;
            this.codeArrangement = source.codeArrangement;
            this.nameArrangement = source.nameArrangement;
            this.dtFromArrangement = source.dtFromArrangement;
            this.dtToArrangement = source.dtToArrangement;
            this.nrOfNights = source.nrOfNights;
            this.cityArrangement = source.cityArrangement;
            this.countryArrangement = source.countryArrangement;
            this.countryNameArrangement = source.countryNameArrangement;
            this.typeArrangement = source.typeArrangement;
            this.typeNameArrangement = source.typeNameArrangement;
            this.nrTraveler = source.nrTraveler;
            this.minNrTraveler = source.minNrTraveler;
            this.nrVoluntaryHelper = source.nrVoluntaryHelper;
            this.idHotelService = source.idHotelService;
            this.nameHotelService = source.nameHotelService;
            this.isWeb = source.isWeb;
            this.nrMaleVoluntary = source.nrMaleVoluntary;
            this.idAgeCategory = source.idAgeCategory;
            this.descAgeCategory = source.descAgeCategory;
            this.nrMaximumWheelchairs = source.nrMaximumWheelchairs;
            this.whoseElectricWheelchairs = source.whoseElectricWheelchairs;
            this.buSupportingArms = source.buSupportingArms;
            this.booked = source.booked;
            this.optionalBooked = source.optionalBooked;
            this.freePlaces = source.freePlaces;
            this.price = source.price;
            this.daysFirstPayment = source.daysFirstPayment;
            this.daysLastPayment = source.daysLastPayment;
            this.percentFirstPayment = source.percentFirstPayment;
            this.reservationCosts = source.reservationCosts;
            this.nrAnchorage = source.nrAnchorage;
            this.invoiceDescription = source.invoiceDescription;
            this.codeProject = source.codeProject;
            this.idClientInvoice = source.idClientInvoice;
            this.nameClient = source.nameClient;
        }

        public bool CompareWith(ArrangementModel compareModel)
        {
            // compare all fields. if there is a change return true
            bool returnResult = false;

            if (this.idArrangement != compareModel.idArrangement)
                returnResult = true;

            if(this.statusArrangement != compareModel.statusArrangement)
                returnResult = true;

            if(this.codeArrangement != compareModel.codeArrangement)
                returnResult = true;

            if(this.nameArrangement != compareModel.nameArrangement)
                returnResult = true;

            if(this.dtFromArrangement != compareModel.dtFromArrangement)
                returnResult = true;

            if(this.dtToArrangement != compareModel.dtToArrangement)
                returnResult = true;

            if(this.nrOfNights != compareModel.nrOfNights)
                returnResult = true;

            if(this.cityArrangement != compareModel.cityArrangement)
                returnResult = true;

            if(this.countryArrangement != compareModel.countryArrangement)
                returnResult = true;

            if(this.typeArrangement != compareModel.typeArrangement)
                returnResult = true;

            if(this.nrTraveler != compareModel.nrTraveler)
                returnResult = true;

            if(this.minNrTraveler != compareModel.minNrTraveler)
                returnResult = true;

            if(this.nrVoluntaryHelper != compareModel.nrVoluntaryHelper)
                returnResult = true;

            if(this.idHotelService != compareModel.idHotelService)
                returnResult = true;

            if(this.isWeb != compareModel.isWeb)
                returnResult = true;

            if(this.nrMaleVoluntary != compareModel.nrMaleVoluntary)
                returnResult = true;

            if(this.idAgeCategory != compareModel.idAgeCategory)
                returnResult = true;

            if(this.nrMaximumWheelchairs != compareModel.nrMaximumWheelchairs)
                returnResult = true;

            if(this.whoseElectricWheelchairs != compareModel.whoseElectricWheelchairs)
                returnResult = true;

            if(this.buSupportingArms != compareModel.buSupportingArms)
                returnResult = true;

            if(this.booked != compareModel.booked)
                returnResult = true;

            if(this.optionalBooked != compareModel.optionalBooked)
                returnResult = true;

            if(this.freePlaces != compareModel.freePlaces)
                returnResult = true;

            if(this.price != compareModel.price)
                returnResult = true;

            if(this.daysFirstPayment != compareModel.daysFirstPayment)
                returnResult = true;

            if(this.daysLastPayment != compareModel.daysLastPayment)
                returnResult = true;

            if(this.percentFirstPayment != compareModel.percentFirstPayment)
                returnResult = true;

            if(this.reservationCosts != compareModel.reservationCosts)
                returnResult = true;

            if(this.nrAnchorage != compareModel.nrAnchorage)
                returnResult = true;

            if (this.invoiceDescription != compareModel.invoiceDescription)
                returnResult = true;

            if (this.codeProject != compareModel.codeProject)
                returnResult = true;

            if (this.idClientInvoice != compareModel.idClientInvoice)
                returnResult = true;

            if (this.nameClient != compareModel.nameClient)
                returnResult = true;


            return returnResult;
        }

    }
}