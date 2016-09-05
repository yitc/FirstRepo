using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementPriceModel : IModel
    {

        [DisplayName("Arrangement price id")]
        public int idArrangementPrice { get; set; }


        [DisplayName("Arrangement id")]
        public int idArrangement { get; set; }

        [DisplayName("Code article")]
        public string idArticle { get; set; }

        [DisplayName("Name article")]
        public string nameArticle { get; set; }

        [DisplayName("Nr article")]
        public int nrArticle { get; set; }

        [DisplayName("Quantity")]
        public int quantity { get; set; }

        [DisplayName("ID contract")]
        public int idContract { get; set; }

        [DisplayName("Contract")]
        public string nameContract { get; set; }

        [DisplayName("ID Client")]
        public int idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Group")]
        public Boolean isGroup { get; set; }

        [DisplayName("Article purchase price")]
        public decimal pricePerArticle { get; set; }

        [DisplayName("Nr")]
        public decimal pricePerQuantity { get; set; }

        [DisplayName("Commission")]
        public decimal commission { get; set; }

        [DisplayName("Purchase price")]
        public decimal purchasePrice { get; set; }

        [DisplayName("Subtotal")]
        public decimal priceTotal { get; set; }

        [DisplayName("Total")]
        public decimal total { get; set; }

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }


        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        [DisplayName("Extra")]
        public Boolean isExtra { get; set; }

        [DisplayName("Away")]
        public bool isAway { get; set; }

        [DisplayName("Back")]
        public bool isBack { get; set; }

        [DisplayName("Accomodation")]
        public bool isAccomodation { get; set; }

        [DisplayName("Not in acompaniment")]
        public bool isNotInAccompaniment { get; set; }

        [DisplayName("Not for travelers")]
        public bool isNotForTraveler { get; set; }

        
      
        public ArrangementPriceModel()
        {
            this.idArrangementPrice = 0; 
            this.idArrangement = 0;
            this.idArticle = String.Empty;
            this.quantity = 0;
            this.idContract = 0;
            this.nameContract = String.Empty;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.nrArticle = 0;
            this.dtFrom = DateTime.Now;
            this.pricePerArticle = 0;
            this.pricePerQuantity = 0;
            this.priceTotal = 0;
            this.total = 0;
            this.dtTo = DateTime.Now;
            this.dtUserCreated = DateTime.Now;
            this.idUserCreated = 0;
            this.idUserModified = 0;
            this.dtUserCreated = DateTime.Now;
            this.dtUserModified = DateTime.Now;
            this.isGroup = false;
            this.purchasePrice = 0;
            this.isExtra = false;
            this.isAway = false;
            this.isAccomodation = false;
            this.isBack = false;
            this.isNotInAccompaniment = false;
            this.isNotForTraveler = false;
            this.commission = 0;
        }

    }
}