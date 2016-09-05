using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace BIS.Model
{
     public class ClientTelModel
    {
        [DisplayName("ID tel")]
        public int idTel { get; set; }

        [DisplayName("Client")]
        public int idClient { get; set; }

        [DisplayName("Telephone")]
        public string numberTel { get; set; }

        [DisplayName("Default")]
        public bool idDefaultTel { get; set; }

        [DisplayName("Description")]   
        public string descriptionTel { get; set; }

         [DisplayName("ID tel type")]
        public int? idTelType { get; set; }

         [DisplayName("Tel type")]
        public string nameTelType { get; set; }

         public ClientTelModel()
         {
             this.idTel = 0;
             this.idClient = 0;
             this.numberTel = String.Empty;
             this.idDefaultTel = false;
             this.descriptionTel = String.Empty;
             this.idTelType = 0;
             this.nameTelType = String.Empty;
         }

         public ClientTelModel(ClientTelModel sourceModel)
         {
             this.idTel = sourceModel.idTel;
             this.idClient = sourceModel.idClient;
             this.numberTel = sourceModel.numberTel;
             this.idDefaultTel = sourceModel.idDefaultTel;
             this.descriptionTel = sourceModel.descriptionTel;
             this.idTelType = sourceModel.idTelType;
             this.nameTelType = sourceModel.nameTelType;
         }

         public ClientTelModel ReturnCopy()
         {
             ClientTelModel newItem = new ClientTelModel();

             newItem.idTel = this.idTel;
             newItem.idClient = this.idClient;
             newItem.numberTel = this.numberTel;
             newItem.idDefaultTel = this.idDefaultTel;
             newItem.descriptionTel = this.descriptionTel;
             newItem.idTelType = this.idTelType;
             newItem.nameTelType = this.nameTelType; // dodati

             return newItem;
         }         
    }
     // insert, modify changes
     public class ClientTelModelComparer : IEqualityComparer<ClientTelModel>
     {
         public bool Equals(ClientTelModel x, ClientTelModel y)
         {
             return (x.idTel == y.idTel || x.idClient == y.idClient|| x.numberTel == y.numberTel || x.idDefaultTel == y.idDefaultTel || x.descriptionTel == y.descriptionTel || x.idTelType == y.idTelType);
         }

         public int GetHashCode(ClientTelModel obj)
         {
             return obj.idTel.GetHashCode() ^ obj.idClient.GetHashCode() ^ obj.numberTel.GetHashCode() ^ obj.idDefaultTel.GetHashCode() ^ obj.descriptionTel.GetHashCode() ^ obj.idTelType.GetHashCode();
         }
     }
}
