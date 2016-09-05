using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VolAvailabilityPreselectionModel : IModel
    {
        //Za Lookupove model
        [DisplayName("Add/Not")]
        public bool select { get; set; }

        [DisplayName("idQuest")]
        public int idQuest { get; set; }

        [DisplayName("idQuestGroup")]
        public int idQuestGroup { get; set; }

        [DisplayName("Question")]
        public string txtQuest { get; set; }

        [DisplayName("idQueryType")]
        public int idQueryType { get; set; }

        [DisplayName("IdAns")]
        public int idAns { get; set; }

        [DisplayName("Name label")]
        public string nameLabel { get; set; }


        public VolAvailabilityPreselectionModel()
        {
            this.idQuest = 0;
            this.idQuestGroup = 0;
            this.txtQuest = String.Empty;
            this.idQueryType = 0;
            this.idAns = 0;
            this.select = false;
            
        }      

    }

    public class VolAvailabilityReasonOutPreselectionModel : IModel
    {

        [DisplayName("Number")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("First name")]
        public string firstName { get; set; }

        //[DisplayName("Name")]
        //public string Name { get; set; }
        [DisplayName("Middle name")]
        public string midName { get; set; }


        [DisplayName("Last name")]
        public string lastName { get; set; }

        [DisplayName("Function")]
        public string function { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }

        [DisplayName("Reason Out")]
        public string reasonOut { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Number tel")]
        public string numberTel { get; set; }





        public VolAvailabilityReasonOutPreselectionModel()
        {
            this.idContPers = 0;
            this.title = String.Empty;
            this.firstName = String.Empty;
            //this.Name = String.Empty;
            this.reasonOut = string.Empty;

            this.email = String.Empty;
            this.numberTel = String.Empty;
            this.Age = 0;

            this.lastName = String.Empty;
            this.midName = String.Empty;
            this.function = String.Empty;
        }
    }

    public class VolAvailabilityContPersPreselectionModel:IModel
    {

        [DisplayName("Number")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("First name")]
        public string firstname { get; set; }

        //[DisplayName("Name")]
        //public string Name { get; set; }

        [DisplayName("Name gender")]
        public string nameGender { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Number tel")]
        public string numberTel { get; set; }

        [DisplayName("Age")]
        public string Age { get; set; }

        [DisplayName("Last name")]
        public string lastname { get; set; }

        [DisplayName("Middle name")]
        public string midname { get; set; }

        [DisplayName("Availability")]
        public int Availability { get; set; }

        [DisplayName("Nr Booked")]
        public int nrBooked { get; set; }

        [DisplayName("Function")]
        public string function { get; set; }

        public VolAvailabilityContPersPreselectionModel()
        {
            this.idContPers = 0;
            this.title = String.Empty;
            this.firstname = String.Empty;
            //this.Name = String.Empty;
            this.nameGender = String.Empty;

            this.email = String.Empty;
            this.numberTel = String.Empty;
            this.Age = String.Empty;
            this.Availability = 0;
            this.nrBooked = 0;
            this.lastname = String.Empty;
            this.midname = String.Empty;
            this.function = String.Empty;
        }      
       
    }

    

    

    public class VolAvailabilityAgeListPreselectionModel : IModel
    {

        [DisplayName("Number")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string nameTitle { get; set; }

        [DisplayName("First name")]
        public string firstName { get; set; }

        //[DisplayName("Name")]
        //public string Name { get; set; }

        [DisplayName("Middle name")]
        public string midName { get; set; }


        [DisplayName("Last name")]
        public string lastName { get; set; }

        [DisplayName("Function")]
        public string function { get; set; }

        [DisplayName("Age (at the selected date)")]
        public int Age { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Number tel")]
        public string numberTel { get; set; }





        public VolAvailabilityAgeListPreselectionModel()
        {
            this.idContPers = 0;
            this.nameTitle = String.Empty;
            this.firstName = String.Empty;
            //this.Name = String.Empty;

            this.email = String.Empty;
            this.numberTel = String.Empty;
            this.Age = 0;

            this.lastName = String.Empty;
            this.midName = String.Empty;
            this.function = String.Empty;
        }
    }

    public class VolAvailabilityAllBookingsPreselectionModel : IModel
    {

        [DisplayName("Number")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string nameTitle { get; set; }

        [DisplayName("First name")]
        public string firstName { get; set; }

        [DisplayName("Middle name")]
        public string midName { get; set; }

        [DisplayName("Last name")]
        public string lastName { get; set; }

        [DisplayName("Function")]
        public string function { get; set; }

        [DisplayName("Name arrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Departure date")]
        public DateTime departureDate { get; set; }

        [DisplayName("Return date")]
        public DateTime returnDate { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Number tel")]
        public string numberTel { get; set; }





        public VolAvailabilityAllBookingsPreselectionModel()
        {
            this.idContPers = 0;
            this.nameTitle = String.Empty;
            this.firstName = String.Empty;
            this.email = String.Empty;
            this.numberTel = String.Empty;
            this.Age = 0;
            this.nameArrangement = String.Empty;
            this.idArrangement = 0;
            this.lastName = String.Empty;
            this.midName = String.Empty;
            this.function = String.Empty;
            this.departureDate = DateTime.Now;
            this.returnDate = DateTime.Now;
        }
    }
}