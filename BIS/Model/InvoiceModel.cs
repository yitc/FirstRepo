using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace BIS.Model
{
    public class InvoiceModel : IModel
    {
        [DisplayName(" ")]
        public bool select { get; set; }

        [DisplayName("Id Invoice")]
        public int idInvoice { get; set; }

        [DisplayName("Invoice Number")]
        public string invoiceNr { get; set; }

        [DisplayName("Invoice extend")]
        public string invoiceRbr { get; set; }

        [DisplayName ("Id Voucher")]
        public int? idVoucher { get; set; }

        [DisplayName("Id Invoice Status")]
        public int? idInvoiceStatus { get; set; }

        [DisplayName("Status Description")]
        public string descInvoiceStatus { get; set; }

        [DisplayName("Description Invoice")]
        public string descriptionInvoice { get; set; }

        [DisplayName("Id Client")]
        public int? idClient { get; set; }

        [DisplayName("Id Contact Person")]
        public int? idContPerson { get; set; }

        [DisplayName("Date Invoice")]
        public DateTime? dtInvoice { get; set; }

        [DisplayName("Date Valuta")]
        public DateTime? dtValuta { get; set; }

        [DisplayName("Bruto Amount")]
        public decimal? brutoAmount { get; set; }

        [DisplayName("Neto Amount")]
        public decimal? netoAmount { get; set; }

        [DisplayName("Id Btw")]
        public int? idBtw { get; set; }

        [DisplayName("Is Booked")]
        public bool isBooked { get; set; }

        [DisplayName("Note Invoice")]
        public string noteInvoice { get; set; }

        [DisplayName("ID Creator user")]
        public int userCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime? dtCreated { get; set; }
        
        [DisplayName("ID Modified user")]
        public int userModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime? dtModified { get; set; }

        [DisplayName("First payment")]
        public DateTime? dtFirstPay { get; set; }

        [DisplayName("Last payment")]
        public DateTime? dtLastPay { get; set; }

        [DisplayName("Percent first payment")]
        public decimal? percentFrstPay { get; set; }

        [DisplayName("Reservation cost")]
        public decimal? reservationCost { get; set; }

        [DisplayName("Person name")]
        public string namePerson { get; set; }

        [DisplayName("First reference")]
        public string firstreferencePay { get; set; }

        [DisplayName("Last reference")]
        public string secondreferencePay { get; set; }

        [DisplayName("Type invoice")]
        public int typeinvoice { get; set; }
        
        [DisplayName("Invoicing")]
        public bool isInvoicing { get; set; }

        [DisplayName("Invoicing email")]
        public string email { get; set; }

        [DisplayName("Room comment")]
        public string roomComment { get; set; }

        public InvoiceModel()
        {
            this.idInvoice = 0;
            this.invoiceNr = String.Empty;
            this.idVoucher = 0;
            this.idInvoiceStatus = 0;
            this.descInvoiceStatus = String.Empty;
            this.descriptionInvoice = String.Empty;
            this.idClient = 0;
            this.idContPerson = 0;
            this.dtInvoice = DateTime.Now;
            this.dtValuta = DateTime.Now;
            this.brutoAmount = 0;
            this.netoAmount = 0;
            this.idBtw = 0;
            this.isBooked = false;
            this.noteInvoice = String.Empty;
            this.userCreated = 0;
            this.dtCreated = DateTime.Now;
            this.userModified = 0;
            this.dtModified = DateTime.Now;
            this.percentFrstPay = 0;
            this.reservationCost = 0;
            this.dtFirstPay = DateTime.MinValue;
            this.dtLastPay = DateTime.MinValue;
            this.namePerson = String.Empty;
            this.firstreferencePay = String.Empty;
            this.secondreferencePay = String.Empty;
            this.typeinvoice = 0;
            this.isInvoicing = false;
            this.email = String.Empty;
            this.roomComment = String.Empty;
        }
     }

    public class InvoicePaidModel : IModel
    {
        [DisplayName("Id Invoice")]
        public int idInvoice { get; set; }

        [DisplayName("Paid")]
        public decimal paid { get; set; }

        [DisplayName("Date until")]
        public DateTime dtUntil { get; set; }


        public InvoicePaidModel()
        {
            this.idInvoice = 0;
            this.dtUntil = DateTime.Now;
            this.paid = 0;

        }
    }    
}