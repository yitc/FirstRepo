using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementArticalModel : IModel
    {
        [DisplayName("Article code")]
        public string idArticle { get; set; }

        [DisplayName("Article name")]
        public string nameArticle { get; set; }

        [DisplayName("Number")]
        public int number { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("isContract")]
        public Boolean isContract { get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("Room")]
        public string idRoom { get; set; }

        [DisplayName("Date of night")]
        public DateTime dtOfNight { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Price per article")]
        public decimal pricePerArticle { get; set; }

        [DisplayName("Price per quantity")]
        public decimal pricePerQuantity { get; set; }

        [DisplayName("Total")]
        public decimal priceTotal { get; set; }


        public ArrangementArticalModel()
        {
            this.idArticle = String.Empty;
            this.nameArticle = String.Empty;
            this.number = 0;
            this.id = 0;
            this.isContract = true;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idRoom = String.Empty;
            this.dtOfNight = DateTime.MinValue;
            this.dtTo = DateTime.Now;
            this.dtFrom = DateTime.Now;
            this.pricePerArticle = 0;
            this.pricePerQuantity = 0;
            this.priceTotal = 0;
        }
    }


    public class ArrangementArticalForBookPersonModel : IModel
    {
        [DisplayName("Article code")]
        public string idArticle { get; set; }

        [DisplayName("Article name")]
        public string nameArticle { get; set; }

        [DisplayName("Number")]
        public int number { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("isContract")]
        public Boolean isContract { get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("Room")]
        public string idRoom { get; set; }

        [DisplayName("Date of night")]
        public DateTime dtOfNight { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Price per article")]
        public decimal pricePerArticle { get; set; }

        [DisplayName("Price per quantity")]
        public decimal pricePerQuantity { get; set; }

        [DisplayName("Total")]
        public decimal priceTotal { get; set; }

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        public ArrangementArticalForBookPersonModel()
        {
            this.idArticle = String.Empty;
            this.nameArticle = String.Empty;
            this.number = 0;
            this.id = 0;
            this.isContract = true;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idRoom = String.Empty;
            this.dtOfNight = DateTime.MinValue;
            this.dtTo = DateTime.Now;
            this.dtFrom = DateTime.Now;
            this.pricePerArticle = 0;
            this.pricePerQuantity = 0;
            this.priceTotal = 0;
            this.idUserCreated = 0;
            this.idUserModified = 0;
            this.dtUserCreated = DateTime.Now;
            this.dtUserModified = DateTime.Now;
        }
    }
    public class ArrangementArticalModel_Rooms : IModel
    {
        [DisplayName("Add/Not")]
        public bool isChecked { get; set; }

        [DisplayName("Article code")]
        public string idArticle { get; set; }

        [DisplayName("Article name")]
        public string nameArticle { get; set; }

        [DisplayName("Number")]
        public int number { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("isContract")]
        public Boolean isContract { get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("Room")]
        public string idRoom { get; set; }

        [DisplayName("Date of night")]
        public DateTime dtOfNight { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Price per article")]
        public decimal pricePerArticle { get; set; }

        [DisplayName("Price per quantity")]
        public decimal pricePerQuantity { get; set; }

        [DisplayName("Total")]
        public decimal priceTotal { get; set; }

        [DisplayName("Quantity")]
        public int quantity { get; set; }

        [DisplayName("Nr. Article")]
        public int nrArticle { get; set; }

        public ArrangementArticalModel_Rooms()
        {
            this.isChecked = false;
            this.idArticle = String.Empty;
            this.nameArticle = String.Empty;
            this.number = 0;
            this.id = 0;
            this.isContract = true;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idRoom = String.Empty;
            this.dtOfNight = DateTime.MinValue;
            this.dtTo = DateTime.Now;
            this.dtFrom = DateTime.Now;
            this.pricePerArticle = 0;
            this.pricePerQuantity = 0;
            this.priceTotal = 0;
            this.quantity = 0;
            this.nrArticle = 0;
        }
    }

    public class ArrangementArticalModel_RoomsUpdate 
    {

        [DisplayName("Article code")]
        public string idArticle { get; set; }

        [DisplayName("Article name")]
        public string nameArticle { get; set; }

        [DisplayName("Number")]
        public int number { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("isContract")]
        public Boolean isContract { get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("Room")]
        public string idRoom { get; set; }

        [DisplayName("Date of night")]
        public DateTime dtOfNight { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Price per article")]
        public decimal pricePerArticle { get; set; }

        [DisplayName("Price per quantity")]
        public decimal pricePerQuantity { get; set; }

        [DisplayName("Total")]
        public decimal priceTotal { get; set; }

        [DisplayName("Quantity")]
        public int quantity { get; set; }

        [DisplayName("Nr. Article")]
        public int nrArticle { get; set; }

        [DisplayName("Last added")]
        public string nrLast { get; set; }

        public ArrangementArticalModel_RoomsUpdate()
        {
            this.idArticle = String.Empty;
            this.nameArticle = String.Empty;
            this.number = 0;
            this.id = 0;
            this.isContract = true;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idRoom = String.Empty;
            this.dtOfNight = DateTime.MinValue;
            this.dtTo = DateTime.Now;
            this.dtFrom = DateTime.Now;
            this.pricePerArticle = 0;
            this.pricePerQuantity = 0;
            this.priceTotal = 0;
            this.quantity = 0;
            this.nrArticle = 0;
            this.nrLast = String.Empty;
        }
    }

}
