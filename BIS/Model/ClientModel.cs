using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.Model
{
    public class ClientModel : IModel
    {
        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Account code")]
        public string accountCodeClient { get; set; }

        [DisplayName("Name")]
        public string nameClient { get; set; }

        [DisplayName("Contact person")]
        public string contactPersonName { get; set; }        

        //[DisplayName("Address")]
        //public string addressClient { get; set; }

        //[DisplayName("Zip code")]
        //public string zipCodeClient { get; set; }

        //[DisplayName("City")]
        //public string cityClient { get; set; }

        //[DisplayName("Country")]
        //public int countryClient { get; set; }

        //[DisplayName("Visiting address")]
        //public string visitAddressClient { get; set; }

        //[DisplayName("Visiting post code")]
        //public string visitZipCodeClient { get; set; }

        //[DisplayName("Visiting city")]
        //public string visitCityClient { get; set; }

        //[DisplayName("Email")]
        //public string emailClient { get; set; }

        [DisplayName("Web")]
        public string webClient { get; set; }

        [DisplayName("ID type client")]
        public int idTypeClient { get; set; }

        [DisplayName("Type")]
        public string nameTypeClient { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("ID Creator user")]
        public int userCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtCreated { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("ID Modified user")]
        public int userModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtModified { get; set; }

        [DisplayName("Active")]
        public Boolean isActiveClient { get; set; }

        public ClientModel()
        {
            this.idClient = 0;
            this.accountCodeClient = String.Empty;
            this.nameClient = String.Empty;
            this.contactPersonName = String.Empty;
            this.webClient = String.Empty;
            this.idTypeClient = 0;
            this.nameTypeClient = String.Empty;
            this.nameUserCreated = String.Empty;
            this.userCreated = 0;
            this.dtCreated = DateTime.Now;
            this.nameUserModified = String.Empty;
            this.userModified = 0;
            this.dtModified = DateTime.Now;
            this.isActiveClient = false;

        }

        public ClientModel(ClientModel model)
        {
            this.idClient = model.idClient;
            this.accountCodeClient = model.accountCodeClient;
            this.nameClient = model.nameClient;
            this.contactPersonName = model.contactPersonName;
            this.webClient = model.webClient;
            this.idTypeClient = model.idTypeClient;
            this.nameTypeClient = model.nameTypeClient;
            this.nameUserCreated = model.nameUserCreated;
            this.userCreated = model.userCreated;
            this.dtCreated = model.dtCreated;
            this.nameUserModified = model.nameUserModified;
            this.userModified = model.userModified;
            this.dtModified = model.dtModified;
            this.isActiveClient = model.isActiveClient;

        }

        public bool CompareWith(ClientModel compareModel)
        {
            // compare all fields. if there is a change return true
            bool returnResult = false;

            if (this.idClient != compareModel.idClient)
                returnResult = true;
            
            if(this.nameClient != compareModel.nameClient)
                returnResult = true;

            if(this.contactPersonName != compareModel.contactPersonName)
                returnResult = true;

            if(this.webClient != compareModel.webClient)
                returnResult = true;

            if(this.idTypeClient != compareModel.idTypeClient)
                returnResult = true;

            if(this.nameTypeClient != compareModel.nameTypeClient)
                returnResult = true;
            
            if(this.isActiveClient != compareModel.isActiveClient)
                returnResult = true;
            if (this.accountCodeClient != compareModel.accountCodeClient)
                returnResult = true;

            return returnResult;
        }

        public void CopyValues(ClientModel source)
        {
            this.idClient = source.idClient;
            this.accountCodeClient = source.accountCodeClient;
            this.nameClient = source.nameClient;
            this.contactPersonName = source.contactPersonName;
            this.webClient = source.webClient;
            this.idTypeClient = source.idTypeClient;
            this.nameTypeClient = source.nameTypeClient;
            this.nameUserCreated = source.nameUserCreated;
            this.userCreated = source.userCreated;
            this.dtCreated = source.dtCreated;
            this.nameUserModified = source.nameUserModified;
            this.userModified = source.userModified;
            this.dtModified = source.dtModified;
            this.isActiveClient = source.isActiveClient;

        }
    }
}
