using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArticalGroupsModel : IModel
    {

        [DisplayName("Article group code")]
        public string codeArticalGroup { get; set; }

        [DisplayName("Article group name")]
        public string nameArticalGroup { get; set; }

        [DisplayName("Purchase article")]
        public string inkopArtical { get; set; }

        [DisplayName("Purchase article description")]
        public string descInkopArtical { get; set; }

        [DisplayName("Selling article")]
        public string verkopArtical { get; set; }

        [DisplayName("Selling article description")]
        public string descVerkopArtical { get; set; }

        [DisplayName("Active")]
        public Boolean isActive { get; set; }

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        [DisplayName("Article group classification")]
        public string classArticalGroup { get; set; }

        public ArticalGroupsModel()
        {
            this.codeArticalGroup = String.Empty;
            this.nameArticalGroup = String.Empty;
            this.inkopArtical = String.Empty;
            this.descInkopArtical = String.Empty;
            this.verkopArtical = String.Empty;
            this.descVerkopArtical = String.Empty;
            this.isActive = false;
            this.idUserCreated = 0;
            this.nameUserCreated = String.Empty;
            this.dtUserCreated = DateTime.Now;
            this.idUserModified = 0;
            this.nameUserModified = String.Empty;
            this.dtUserModified = DateTime.Now;
            this.classArticalGroup = String.Empty;
        }

        public ArticalGroupsModel(ArticalGroupsModel model)
        {
            this.codeArticalGroup = model.codeArticalGroup;
            this.nameArticalGroup = model.nameArticalGroup;
            this.inkopArtical = model.inkopArtical;
            this.descInkopArtical = model.descInkopArtical;
            this.verkopArtical = model.verkopArtical;
            this.descVerkopArtical = model.descVerkopArtical;
            this.isActive = model.isActive;
            this.idUserCreated = model.idUserCreated;
            this.nameUserCreated = model.nameUserCreated;
            this.dtUserCreated = model.dtUserCreated;
            this.idUserModified = model.idUserModified;
            this.nameUserModified = model.nameUserModified;
            this.dtUserModified = model.dtUserModified;
            this.classArticalGroup = model.classArticalGroup;
        }

        public bool CompareWith(ArticalGroupsModel compareModel)
        {
            // compare all fields. if there is a change return true
            bool returnResult = false;

            if (this.codeArticalGroup != compareModel.codeArticalGroup)
                returnResult = true;

            if (this.nameArticalGroup != compareModel.nameArticalGroup)
                returnResult = true;

            if (this.inkopArtical != compareModel.inkopArtical)
                returnResult = true;

            if (this.descInkopArtical != compareModel.descInkopArtical)
                returnResult = true;

            if (this.verkopArtical != compareModel.verkopArtical)
                returnResult = true;

            if (this.descVerkopArtical != compareModel.descVerkopArtical)
                returnResult = true;

            if (this.isActive != compareModel.isActive)
                returnResult = true;

            if (this.classArticalGroup != compareModel.classArticalGroup)
                returnResult = true;


            return returnResult;

        }

        public void CopyValues(ArticalGroupsModel source)
        {
            this.codeArticalGroup = source.codeArticalGroup;
            this.nameArticalGroup = source.nameArticalGroup;
            this.inkopArtical = source.inkopArtical;
            this.descInkopArtical = source.descInkopArtical;
            this.verkopArtical = source.verkopArtical;
            this.descVerkopArtical = source.descVerkopArtical;
            this.isActive = source.isActive;
            this.idUserCreated = source.idUserCreated;
            this.nameUserCreated = source.nameUserCreated;
            this.dtUserCreated = source.dtUserCreated;
            this.idUserModified = source.idUserModified;
            this.nameUserModified = source.nameUserModified;
            this.dtUserModified = source.dtUserModified;
            this.classArticalGroup = source.classArticalGroup;
        }
    }

    
}