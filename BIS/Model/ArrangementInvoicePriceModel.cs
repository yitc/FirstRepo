using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementInvoicePriceModel : IModel
    {
         [DisplayName("ID Arrangement")]
        public int idArrangement { get; set; }

         [DisplayName("ID Article")]
        public string idArticle { get; set; }

         [DisplayName("Description")]
        public string descriptionArticle { get; set; }

         [DisplayName("Quantity")]
        public int? nrArticle { get; set; }

         [DisplayName("Purchase price per artikal")]
        public decimal? purchasePrice { get; set; }

         [DisplayName("Purchase Total price")]
        public decimal? purchasePriceTotal { get; set; }

         [DisplayName("Sell price")]
        public decimal? sellingPrice { get; set; }

         [DisplayName("Calculation item")]
        public bool calculation { get; set; }

         [DisplayName("Extra artical")]
        public bool isExtra { get; set; }

         [DisplayName("Optional")]
        public bool isOption { get; set; }

        public ArrangementInvoicePriceModel()
        {
            this.idArrangement = 0;
            this.idArticle = String.Empty;
            this.descriptionArticle = String.Empty;
            this.nrArticle = 0;
            this.purchasePrice = 0;
            this.purchasePriceTotal = 0;
            this.sellingPrice = 0;
            this.calculation = false;
            this.isExtra = false;
            this.isOption = false;
           
        }
    }
}