using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using BIS.DAO;

namespace BIS.Model
{
    public class PersonModel : IModel
    {
        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("Initials")]
        public string initialsContPers { get; set; }

        [DisplayName("First name")]
        public string firstname { get; set; }

        [DisplayName("Middle name")]
        public string midname { get; set; }

        [DisplayName("Last name")]
        public string lastname { get; set; }

        [DisplayName("Maiden name")]
        public string maidenname { get; set; }

        [DisplayName("ID Title")]
        public int idTitle { get; set; }

        [DisplayName("Title")]
        public string nameTitle { get; set; }

        [DisplayName("ID Gender")]

        public int idGender { get; set; }

        [DisplayName("Gender")]
        public string nameGender { get; set; }

        [DisplayName("Birth date")]
        public DateTime? birthdate { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtCreated { get; set; }

        [DisplayName("Active date")]
        public DateTime dtOfActive { get; set; }

        [DisplayName("Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtModified { get; set; }

        [DisplayName("Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Responsible person")]
        public int idUserResponsible { get; set; }

        [DisplayName("Married")]
        public bool isMaried { get; set; }

        [DisplayName("Active")]
        public bool isActive { get; set; }

        [DisplayName("Died")]
        public bool isDied { get; set; }

        [DisplayName("Date of dead")]
        public DateTime dtOfDeath { get; set; }

        [DisplayName("Send brochures")]
        public bool isNeedProspect { get; set; }

        [DisplayName("Send mail")]
        public bool isNeedMail { get; set; }

        [DisplayName("Image")]
        public string imageContPers { get; set; }

        [DisplayName("Name")]
        public string fullname
        {
            get { return firstname + " " + midname + " " + lastname; }
        }


        //[DisplayName("Name")]
        //public string fullname
        //{
        //    get { return firstname + " "  + lastname; }
        //}


        [DisplayName("Name Title")]
        public string fullname_with_title
        {
            get { return nameTitle + " " + firstname + " " + lastname; }
        }

        [DisplayName("Salutation")]
        public string identBSN { get; set; }

        [DisplayName("Pay invoice")]
        public bool isPayInvoice { get; set; }

        [DisplayName("No permission pictures")]
        public bool isSharePicture { get; set; }

        [DisplayName("Vouchers by post")]
        public bool isPaperByMail { get; set; }

        [DisplayName("Contact person")]
        public bool isContactPerson { get; set; }

        [DisplayName("Client id")]
        public int idClient { get; set; }
        [DisplayName("Client name")]
        public string nameClient { get; set; }
        [DisplayName("Live at")]
        public int livesIn { get; set; }
        [DisplayName("Description")]
        public string nameLiving { get; set; }
        [DisplayName("Function as contact person")]
        public int idCpFunction { get; set; }
        [DisplayName("Function name")]
        public string nameFunction { get; set; }
        [DisplayName("Request brochures")]
        public bool isRequestBrochure { get; set; }
        [DisplayName("Id Reason In")]
        public int idReasonIn { get; set; }
        [DisplayName("Reason In")]
        public string nameReasonIn { get; set; }
        [DisplayName("Id Reason Out")]
        public int idReasonOut { get; set; }
        [DisplayName("Reason Out")]
        public string nameReasonOut { get; set; }
        [DisplayName("Profession")]
        public string volProfession { get; set; }
        [DisplayName("Travel with us")]
        public int oldTripCount { get; set; }

        [DisplayName("Travel insurance")]
        public string travelInsurance { get; set; }

        [DisplayName("Polis number")]
        public string polisNumber { get; set; }

        [DisplayName("Alarm number")]
        public string alarmNumber { get; set; }

        [DisplayName("Postal code")]
        public string postalCode { get; set; }

        [DisplayName("city")]
        public string city { get; set; }

        [DisplayName("Booking to")]
        public int idContPersBookingTo { get; set; }

        public PersonModel()
        {            
            this.idContPers = 0;
            this.initialsContPers = String.Empty;
            this.firstname = String.Empty;
            this.midname = String.Empty;
            this.lastname = String.Empty;
            this.maidenname = String.Empty;
            this.idTitle = 0;
            this.nameTitle = String.Empty;
            this.idGender = 0;
            this.nameGender = String.Empty;
            //this.birthdate = DateTime.Now;
            this.birthdate = null;
            this.dtCreated = DateTime.Now;
            this.idUserCreated = 0;
            this.dtModified = DateTime.Now;
            this.idUserModified = 0;
            this.idUserResponsible = 0;
            this.isMaried = false;
            this.isActive = false;
            this.isDied = false;
            this.dtOfDeath = DateTime.Now;
            this.isNeedProspect = false;
            this.isNeedMail = false;
            this.imageContPers = String.Empty;                        
            this.identBSN = String.Empty;
            this.isPayInvoice = false;
            this.isSharePicture = false;
            this.isPaperByMail = false;
            this.isContactPerson = false;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.livesIn = 0;        
            this.nameLiving = String.Empty;
            this.idCpFunction = 0;
            this.nameFunction = String.Empty;
            this.isRequestBrochure = false;
            this.idReasonIn = 0;
            this.nameReasonIn = String.Empty;
            this.idReasonOut = 0;
            this.nameReasonOut = String.Empty;
            this.volProfession = String.Empty;
            this.oldTripCount = 0;
            this.travelInsurance = String.Empty;
            this.polisNumber = String.Empty;
            this.alarmNumber = String.Empty;
            this.postalCode = String.Empty;
            this.city = String.Empty;
            this.idContPersBookingTo = 0;
            this.dtOfActive = DateTime.Now;
        }

        public PersonModel(PersonModel model)
        {
            this.idContPers = model.idContPers;
            this.initialsContPers = model.initialsContPers;
            this.firstname = model.firstname;
            this.midname = model.midname;
            this.lastname = model.lastname;
            this.maidenname = model.maidenname;
            this.idTitle = model.idTitle;
            this.nameTitle = model.nameTitle;
            this.idGender = model.idGender;
            this.nameGender = model.nameGender;
            this.birthdate = model.birthdate;
            this.dtCreated = model.dtCreated;
            this.idUserCreated = model.idUserCreated;
            this.dtModified = model.dtModified;
            this.idUserModified = model.idUserModified;
            this.idUserResponsible = model.idUserResponsible;
            this.isMaried = model.isMaried;
            this.isActive = model.isActive;
            this.isDied = model.isDied;
            this.dtOfDeath = model.dtOfDeath;
            this.isNeedProspect = model.isNeedProspect;
            this.isNeedMail = model.isNeedMail;
            this.imageContPers = model.imageContPers;
            this.identBSN = model.identBSN;
            this.isPayInvoice = model.isPayInvoice;
            this.isSharePicture = model.isSharePicture;
            this.isPaperByMail = model.isPaperByMail;
            this.isContactPerson = model.isContactPerson;
            this.idClient = model.idClient;
            this.nameClient = model.nameClient;
            this.livesIn = model.livesIn;
            this.nameLiving = model.nameLiving;
            this.idCpFunction = model.idCpFunction;
            this.nameFunction = model.nameFunction;
            this.isRequestBrochure = model.isRequestBrochure;
            this.idReasonIn = model.idReasonIn;
            this.nameReasonIn = model.nameReasonIn;
            this.idReasonOut = model.idReasonOut;
            this.nameReasonOut = model.nameReasonOut;
            this.volProfession = model.volProfession;
            this.oldTripCount = model.oldTripCount;
            this.travelInsurance = model.travelInsurance;
            this.polisNumber = model.polisNumber;
            this.alarmNumber = model.alarmNumber;
            this.postalCode = model.postalCode;
            this.city = model.city;
            this.idContPersBookingTo = model.idContPersBookingTo;
            this.dtOfActive = model.dtOfActive;
        }

        public bool CompareWith(PersonModel compareModel)
        {
            // compare all fields. if there is a change return true
            bool returnResult = false;

            if (this.idContPers != compareModel.idContPers)
                returnResult = true;

            if (this.initialsContPers != compareModel.initialsContPers)
                returnResult = true;

            if(this.firstname != compareModel.firstname)
                returnResult = true;

            if(this.midname != compareModel.midname)
                returnResult = true;

            if(this.lastname != compareModel.lastname)
                returnResult = true;

            if(this.maidenname != compareModel.maidenname)
                returnResult = true;

            if(this.idTitle != compareModel.idTitle)
                returnResult = true;

            if(this.idGender != compareModel.idGender)
            returnResult = true;
            
            if(this.birthdate != compareModel.birthdate)
                returnResult = true;

            if (this.isMaried != compareModel.isMaried)
                return true;
            
            if(this.isActive != compareModel.isActive)
                returnResult = true;

            if (this.isDied != compareModel.isDied)
                return true;

            if(this.dtOfDeath != compareModel.dtOfDeath)
                returnResult = true;

            if (this.isNeedProspect != compareModel.isNeedProspect)
                return true;

            if(this.isNeedMail != compareModel.isNeedMail)
                returnResult = true;
            
            if(this.identBSN != compareModel.identBSN)
                returnResult = true;

            if(this.isPayInvoice != compareModel.isPayInvoice)
                returnResult = true;

            if(this.isSharePicture != compareModel.isSharePicture)
                returnResult = true;

            if(this.isPaperByMail != compareModel.isPaperByMail)
                returnResult = true;
            
            if(this.isContactPerson != compareModel.isContactPerson)
                returnResult = true;

            if(this.idClient != compareModel.idClient)
                returnResult = true;
            
            if(this.livesIn != compareModel.livesIn)
                returnResult = true;
                                    
            if(this.isRequestBrochure != compareModel.isRequestBrochure)
                returnResult = true;

            if(this.idReasonIn != compareModel.idReasonIn)
                returnResult = true;
            
            if(this.idReasonOut != compareModel.idReasonOut)
                returnResult = true;
            
            if(this.volProfession != compareModel.volProfession)
                returnResult = true;

            if(this.oldTripCount != compareModel.oldTripCount)
                returnResult = true;

            if(this.travelInsurance != compareModel.travelInsurance)
                returnResult = true;

            if(this.polisNumber != compareModel.polisNumber)
                returnResult = true;

            if(this.alarmNumber != compareModel.alarmNumber)
                returnResult = true;

            if (this.idContPersBookingTo != compareModel.idContPersBookingTo)
                returnResult = true;

            if (this.dtOfActive != compareModel.dtOfActive)
                returnResult = true;

            return returnResult;

        }

        public void CopyValues(PersonModel source)
        {
            this.idContPers = source.idContPers;
            this.initialsContPers = source.initialsContPers;
            this.firstname = source.firstname;
            this.midname = source.midname;
            this.lastname = source.lastname;
            this.maidenname = source.maidenname;
            this.idTitle = source.idTitle;
            this.nameTitle = source.nameTitle;
            this.idGender = source.idGender;
            this.nameGender = source.nameGender;
            this.birthdate = source.birthdate;
            this.dtCreated = source.dtCreated;
            this.idUserCreated = source.idUserCreated;
            this.dtModified = source.dtModified;
            this.idUserModified = source.idUserModified;
            this.idUserResponsible = source.idUserResponsible;
            this.isMaried = source.isMaried;
            this.isActive = source.isActive;
            this.isDied = source.isDied;
            this.dtOfDeath = source.dtOfDeath;
            this.isNeedProspect = source.isNeedProspect;
            this.isNeedMail = source.isNeedMail;
            this.imageContPers = source.imageContPers;
            this.identBSN = source.identBSN;
            this.isPayInvoice = source.isPayInvoice;
            this.isSharePicture = source.isSharePicture;
            this.isPaperByMail = source.isPaperByMail;
            this.isContactPerson = source.isContactPerson;
            this.idClient = source.idClient;
            this.nameClient = source.nameClient;
            this.livesIn = source.livesIn;
            this.nameLiving = source.nameLiving;
            this.idCpFunction = source.idCpFunction;
            this.nameFunction = source.nameFunction;
            this.isRequestBrochure = source.isRequestBrochure;
            this.idReasonIn = source.idReasonIn;
            this.nameReasonIn = source.nameReasonIn;
            this.idReasonOut = source.idReasonOut;
            this.nameReasonOut = source.nameReasonOut;
            this.volProfession = source.volProfession;
            this.oldTripCount = source.oldTripCount;
            this.travelInsurance = source.travelInsurance;
            this.polisNumber = source.polisNumber;
            this.alarmNumber = source.alarmNumber;
            this.idContPersBookingTo = source.idContPersBookingTo;
            this.dtOfActive = source.dtOfActive;
        }
    }
    public class PersonShortModel : IModel
    {
        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("First name")]
        public string firstname { get; set; }

        [DisplayName("Middle name")]
        public string midname { get; set; }

        [DisplayName("Last name")]
        public string lastname { get; set; }

        public PersonShortModel()
        {
            this.idContPers = 0 ;
            this.firstname = String.Empty;
            this.midname = String.Empty;
            this.lastname = String.Empty;
        }
    }
}
