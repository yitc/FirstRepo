using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class TravelerPapersReportModel : IModel
    {
        [DisplayName("ID Contact person")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("Initials")]
        public string initials { get; set; }

        [DisplayName("Middle name")]
        public string midname { get; set; }

        [DisplayName("First name")]
        public string firstname { get; set; }

        [DisplayName("Last name")]
        public string lastname { get; set; }


        [DisplayName("Street")]
        public string street { get; set; }

        [DisplayName("House number")]
        public string housenr { get; set; }

        [DisplayName("Postal code")]
        public string postalCode { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("Name arrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Full name")]
        public string lastMiddInitTitle { get; set; }
        

        [DisplayName("Name passengers")]
        public string namePasengers { get; set; }

        [DisplayName("Departure")]
        public DateTime departure { get; set; }

        [DisplayName("Arrivel")]
        public DateTime arrivel { get; set; }

        [DisplayName("Sort boarding point")]
        public int sort { get; set; }

        [DisplayName("Name boarding point")]
        public string nameBoardingPoint { get; set; }

        [DisplayName("Type")]
        public int type { get; set; }

        [DisplayName("Nr")]
        public string nr { get; set; }

        [DisplayName("Address")]
        public string address { get; set; }

        [DisplayName("Departure boardng point")]
        public DateTime departureBP { get; set; }

        [DisplayName("Arrivel boardng point")]
        public DateTime arrivelBP { get; set; }

        [DisplayName("Departure time boardng point")]
        public TimeSpan departureTBP { get; set; }

        [DisplayName("Arrivel time boardng point")]
        public TimeSpan arrivelTBP { get; set; }

        [DisplayName("NameBP")]
        public string nameBP { get; set; }

        [DisplayName("addressBP")]
        public string adressBP { get; set; }

        [DisplayName("Function")]
        public string Function { get; set; }

        [DisplayName("Medical")]
        public string medical { get; set; }




        public TravelerPapersReportModel()
        {
            this.idContPers = 0;
            this.initials = String.Empty;
            this.title = String.Empty;
            this.firstname = String.Empty;
            this.lastname = String.Empty;
            this.midname = String.Empty;
            this.street = String.Empty;
            this.housenr = String.Empty;
            this.postalCode = String.Empty;
            this.city = String.Empty;
            this.nameArrangement = String.Empty;
            this.dtFromArrangement = DateTime.Now;
            this.name = String.Empty;
            this.departure = DateTime.Now;
            this.arrivel = DateTime.Now;
            this.sort = 0;
            this.nameBoardingPoint = String.Empty;
            this.type = 0;
            this.address = String.Empty;
            this.namePasengers = String.Empty;
            this.nr = String.Empty;
            this.dtToArrangement = DateTime.Now;
            this.departureBP = DateTime.Now;
            this.arrivelBP = DateTime.Now;
            this.departureTBP = TimeSpan.MinValue;
            this.arrivelTBP = TimeSpan.MinValue;
            this.nameBP = String.Empty;
            this.adressBP = String.Empty;
            this.Function = String.Empty;
            this.medical = String.Empty;
            this.lastMiddInitTitle = String.Empty;
            //this.isAway = 0;
            //this.isBack = 0;
            //this.isAccomodation = 0;
            //this.idAddressType = 0;
        }
    }

    public class TravelerPapersNewModel : IModel
    {

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Name")]
        public string nameClient { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Postal code")]
        public string postalCode { get; set; }

        [DisplayName("City")]
        public string city2 { get; set; }

        [DisplayName("Address")]
        public string Address2 { get; set; }

        [DisplayName("Telephone")]
        public string TelephoneNumber { get; set; }

        [DisplayName("Telephone fax")]
        public string TelephoneFax { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        public TravelerPapersNewModel()
        {
            dtFrom = DateTime.Now;
            nameClient = String.Empty;
            dtTo = DateTime.Now;
            postalCode = String.Empty;
            city2 = String.Empty;
            Address2 = String.Empty;
            TelephoneNumber = String.Empty;
            TelephoneFax = String.Empty;
            Email = String.Empty;
            Country = String.Empty;

        }


    }


    public class TravelerPapersTekstModel : IModel
    {
        [DisplayName("Letter")]
        public string letter { get; set; }

        [DisplayName("Program")]
        public string program { get; set; }

        [DisplayName("Appoinment")]
        public string rulesAppointment { get; set; }
        

        public TravelerPapersTekstModel()
        {
            this.letter = String.Empty;
            this.program = String.Empty;
            this.rulesAppointment = String.Empty;
        }

    }
}