using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArticalModel : IModel
    {
        [DisplayName("Article code")]
        public string codeArtical { get; set; }

        [DisplayName("Article name")]
        public string nameArtical { get; set; }

        [DisplayName("Quantity")]
        public int? quantity { get; set; }

        [DisplayName("Article group code")]
        public string codeArtikalGroup { get; set; }

        [DisplayName("Article group name")]
        public string nameArtikalGroup { get; set; }

        [DisplayName("Purchase price")]
        public decimal? purchasePrice { get; set; }

        [DisplayName("Selling price")]
        public decimal? sellingPrice { get; set; }

        [DisplayName("Group")]
        public Boolean isGroup { get; set; }


        [DisplayName("ID Creator user")]
        public int? idUserCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int? idUserModifies { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        [DisplayName("Optional")]
        public Boolean isOptional { get; set; }

       

        public ArticalModel()
        {
            this.codeArtical = String.Empty;
            this.nameArtical = String.Empty;
            this.codeArtikalGroup = String.Empty;
            this.nameArtikalGroup = String.Empty;
            this.purchasePrice = 0;
            this.sellingPrice = 0;
            this.idUserCreated = 0;
            this.dtUserCreated = DateTime.Now;
            this.nameUserCreated = String.Empty;
            this.isGroup = false;
            this.idUserModifies = 0;
            this.nameUserModified = String.Empty;
            this.dtUserModified = DateTime.Now;
            this.quantity = 0;
            this.isOptional = false;
        }

        public ArticalModel(ArticalModel model)
        {
            this.codeArtical = model.codeArtical;
            this.nameArtical = model.nameArtical;
            this.codeArtikalGroup = model.codeArtikalGroup;
            this.nameArtikalGroup = model.nameArtikalGroup;
            this.purchasePrice = model.purchasePrice;
            this.sellingPrice = model.sellingPrice;
            this.idUserCreated = model.idUserCreated;
            this.dtUserCreated = model.dtUserCreated;
            this.nameUserCreated = model.nameUserCreated;
            this.isGroup = model.isGroup;
            this.idUserModifies = model.idUserModifies;
            this.nameUserModified = model.nameUserModified;
            this.dtUserModified = model.dtUserModified;
            this.quantity = model.quantity;
            this.isOptional = model.isOptional;
        }

        public bool CompareWith(ArticalModel compareModel)
        {
            // compare all fields. if there is a change return true
            bool returnResult = false;

            if (this.codeArtical != compareModel.codeArtical)
                returnResult = true;

            if (this.nameArtical != compareModel.nameArtical)
                returnResult = true;
            
            if (this.codeArtikalGroup != compareModel.codeArtikalGroup)
                returnResult = true;

            if (this.nameArtikalGroup != compareModel.nameArtikalGroup)
                returnResult = true;

            if (this.purchasePrice != compareModel.purchasePrice)
                returnResult = true;

            if (this.sellingPrice != compareModel.sellingPrice)
                returnResult = true;

            if (this.quantity != compareModel.quantity)
                returnResult = true;

            if (this.isGroup != compareModel.isGroup)
                returnResult = true;

            if (this.isOptional != compareModel.isOptional)
                returnResult = true;

            return returnResult;
        }

        public void CopyValues(ArticalModel source)
        {
            this.codeArtical = source.codeArtical;
            this.nameArtical = source.nameArtical;
            this.codeArtikalGroup = source.codeArtikalGroup;
            this.nameArtikalGroup = source.nameArtikalGroup;
            this.purchasePrice = source.purchasePrice;
            this.sellingPrice = source.sellingPrice;
            this.idUserCreated = source.idUserCreated;
            this.dtUserCreated = source.dtUserCreated;
            this.nameUserCreated = source.nameUserCreated;
            this.isGroup = source.isGroup;
            this.idUserModifies = source.idUserModifies;
            this.nameUserModified = source.nameUserModified;
            this.dtUserModified = source.dtUserModified;
            this.quantity = source.quantity;
            this.isOptional = source.isOptional;
        }
    }
    public class ArticalModelRooms : IModel
    {
        [DisplayName("Article name")]
        public string nameArtikal { get; set; }

        [DisplayName("Code article")]
        public string codeArticle { get; set; }

        public ArticalModelRooms()
        {

            this.nameArtikal = string.Empty;
            this.codeArticle = string.Empty;

        }
    }
}
