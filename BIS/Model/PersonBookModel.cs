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
    public class PersonBookModel : IModel
    {
        [DisplayName("ID status")]
        public int idStatus { get; set; }

        [DisplayName("Status")]
        public string nameStatus { get; set; }

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
        public DateTime birthdate { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtCreated { get; set; }

        [DisplayName("ID creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }


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

        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Date booked")]
        public DateTime dtBooked { get; set; }

        [DisplayName("Insurance")]
        public bool isInsurance { get; set; }

        [DisplayName("ID voucher")]
        public int idArrangementBook { get; set; }

    }
}
