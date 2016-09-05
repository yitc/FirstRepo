using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class PersonEmailModel
    {
        [DisplayName("ID email")]
        public int idEmail { get; set; }

        [DisplayName("Person")]
        public int idContPers { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("M")]
        public bool isCommunication { get; set; }

        [DisplayName("R")]
        public bool isProspect { get; set; }

        [DisplayName("F")]
        public bool isInvoicing { get; set; }

        [DisplayName("ID type")]
        public int? idEmailType { get; set; }

        [DisplayName("N")]
        public bool isNewsletters { get; set; }

        [DisplayName("LVF")]
        public bool lastQuestionForm { get; set; }

        public PersonEmailModel()
        {
            this.idEmail = -1;
            this.idContPers = -1;
            this.email = String.Empty;
            this.isCommunication = false;
            this.isProspect = false;
            this.isInvoicing = false;
            this.idEmailType = -1;
            this.isNewsletters = false;
            this.lastQuestionForm = false;

        }
        public PersonEmailModel(PersonEmailModel sourceModel)
        {
            this.idEmail =sourceModel.idEmail;
            this.idContPers = sourceModel.idContPers;
            this.email = sourceModel.email;
            this.isCommunication = sourceModel.isCommunication;
            this.isProspect = sourceModel.isProspect;
            this.isInvoicing = sourceModel.isInvoicing;
            this.idEmailType = sourceModel.idEmailType;
            this.isNewsletters = sourceModel.isNewsletters;
            this.lastQuestionForm = sourceModel.lastQuestionForm;

        }


        public PersonEmailModel ReturnCopy()
        {
            PersonEmailModel newItem = new PersonEmailModel();

            newItem.idEmail = this.idEmail;
            newItem.idContPers = this.idContPers;
            newItem.email = this.email;
            newItem.isCommunication = this.isCommunication;
            newItem.isProspect = this.isProspect;
            newItem.isInvoicing = this.isInvoicing;
            newItem.idEmailType = this.idEmailType;
            newItem.isNewsletters = this.isNewsletters;
            newItem.lastQuestionForm = this.lastQuestionForm;

            return newItem;
        }

    }
    public class PersonEmailModelComparer : IEqualityComparer<PersonEmailModel>
    {
        public bool Equals(PersonEmailModel x, PersonEmailModel y)
        {

            return (x.idEmail == y.idEmail || x.idContPers == y.idContPers || x.email == y.email || x.isCommunication == y.isCommunication || x.isProspect == y.isProspect || x.isInvoicing == y.isInvoicing || x.idEmailType == y.idEmailType || x.isNewsletters == y.isNewsletters || x.lastQuestionForm == y.lastQuestionForm);
        }

        public int GetHashCode(PersonEmailModel obj)
        {
            return obj.idEmail.GetHashCode() ^ obj.idContPers.GetHashCode() ^ obj.email.GetHashCode() ^ obj.isCommunication.GetHashCode() ^ obj.isProspect.GetHashCode() ^ obj.isInvoicing.GetHashCode() ^ obj.idEmailType.GetHashCode() ^ obj.isNewsletters.GetHashCode() ^ obj.lastQuestionForm.GetHashCode();
        }
    }

     public class PersonEmailiSInvoiceModel
     {
         [DisplayName("Is Invoice")]
         public bool isInvoicing { get; set; }

         [DisplayName("email")]
         public string email { get; set; }

         public PersonEmailiSInvoiceModel()
         {
             this.isInvoicing = false;
             this.email = String.Empty;
         }
     }

}
