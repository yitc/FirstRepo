using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class ClientEmailModel
    {
        [DisplayName("ID Email")]
        public int idEmail { get; set; }

        [DisplayName("Client")]
        public int idClient { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("M")]
        public bool isCommunication { get; set; }

        [DisplayName("F")]
        public bool isInvoicing { get; set; }

        [DisplayName("ID email type")]
        public int? idEmailType { get; set; }

        [DisplayName("N")]
        public bool isNewsletters { get; set; }

        public ClientEmailModel()
        {
            this.idEmail = 0;
            this.idClient = 0;
            this.email = String.Empty;
            this.isCommunication = false;            
            this.isInvoicing = false;
            this.idEmailType = -1;
            this.isNewsletters = false;
        }

        public ClientEmailModel(ClientEmailModel sourceModel)
        {
            this.idEmail = sourceModel.idEmail;
            this.idClient = sourceModel.idClient;
            this.email = sourceModel.email;
            this.isCommunication = sourceModel.isCommunication;
            this.isInvoicing = sourceModel.isInvoicing;
            this.idEmailType = sourceModel.idEmailType;
            this.isNewsletters = sourceModel.isNewsletters;
        }

        public ClientEmailModel ReturnCopy()
        {
            ClientEmailModel newItem = new ClientEmailModel();

            newItem.idEmail = this.idEmail;
            newItem.idClient = this.idClient;
            newItem.email = this.email;
            newItem.isCommunication = this.isCommunication;
            newItem.isInvoicing = this.isInvoicing;
            newItem.idEmailType = this.idEmailType;
            newItem.isNewsletters = this.isNewsletters;

            return newItem;
        }        
    }
    public class ClientEmailModelComparer : IEqualityComparer<ClientEmailModel>
    {
        public bool Equals(ClientEmailModel x, ClientEmailModel y)
        {

            return (x.idEmail == y.idEmail || x.idClient == y.idClient || x.email == y.email || x.isCommunication == y.isCommunication || x.isInvoicing == y.isInvoicing || x.idEmailType == y.idEmailType || x.isNewsletters == y.isNewsletters);
        }

        public int GetHashCode(ClientEmailModel obj)
        {
            return obj.idEmail.GetHashCode() ^ obj.idClient.GetHashCode() ^ obj.email.GetHashCode() ^ obj.isCommunication.GetHashCode() ^ obj.isInvoicing.GetHashCode() ^ obj.idEmailType.GetHashCode() ^ obj.isNewsletters.GetHashCode();
        }
    }

}
