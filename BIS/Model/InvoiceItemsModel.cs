using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class InvoiceItemsModel :IModel
    {
        [DisplayName(" Id Invoice Item")]
        public int idInvItem { get; set; }

        [DisplayName ("Id Invoice")]
        public int idInvoice { get; set; }

        [DisplayName("Id Artical")]
        public string idArtical { get; set; }

        [DisplayName("Artical name")]
        public string nameArtical { get; set; }
               
        [DisplayName("Quantity")]
        public int? quantity { get; set; }

        [DisplayName("Price")]
        public decimal? price { get; set; }

        [DisplayName("Total")]
        public decimal? itemSum { get; set; }

        [DisplayName("ID Creator user")]
        public int userCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime? dtCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int userModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime? dtModified { get; set; }

        [DisplayName("Second")]
        public bool isSecondGrid { get; set; }
        [DisplayName("Cancelation insurance")]
        public bool isCancelationIns { get; set; }

        [DisplayName("Medical devices")]
        public bool isMedical { get; set; }

        public InvoiceItemsModel()
        {
            this.idInvItem = 0;
            this.idInvoice = 0;
            this.idArtical = String.Empty;
            this.nameArtical = String.Empty;
            this.quantity = 0;
            this.price = 0;
            this.userCreated = 0;
            this.dtCreated = DateTime.Now;
            this.userModified = 0;
            this.dtModified = DateTime.Now;
            this.isSecondGrid = false;
            this.isCancelationIns = false;
            this.itemSum = 0;
            this.isMedical = false;
        }
    }
}