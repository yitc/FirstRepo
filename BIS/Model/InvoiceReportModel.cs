using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace BIS.Model
{
    public class InvoiceReportModel : IModel
    {
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

        [DisplayName("Id Traveler")]
        public int? idTraveler { get; set; }

        [DisplayName("Person name")]
        public string namePerson { get; set; }

        [DisplayName("Street")]
        public string street { get; set; }

        [DisplayName("House")]
        public string houseNr { get; set; }

        [DisplayName("Extension")]
        public string extend { get; set; }

        [DisplayName("Zip")]
        public string zip { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Country")]
        public string country { get; set; }

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
        [DisplayName("Total")]
        public decimal? total { get; set; }
        [DisplayName("First payment")]
        public decimal? firstPay { get; set; }
        [DisplayName("Last payment")]
        public decimal? lastPay { get; set; }
      //===
        [DisplayName("Arrangement")]
        public string arrName { get; set; }
        [DisplayName("Days")]
        public string noDays { get; set; }
        [DisplayName("Boarding")]
        public string boarding { get; set; }
        [DisplayName("Date from")]
        public string dateFrom { get; set; }
        [DisplayName("Date to")]
        public string dateTo { get; set; }
        [DisplayName("Service")]
        public string service { get; set; }
        [DisplayName("Employee")]
        public string nameEmployee { get; set; }
        [DisplayName("First amount")]
        public string firstAmount { get; set; }
        [DisplayName("Rest amount")]
        public string restAmount { get; set; }
        [DisplayName("first ref")]
        public string firstReference { get; set; }
        [DisplayName("Rest ref")]
        public string restReference { get; set; }
        [DisplayName("First reference Pay")]
        public string firstreferencePay { get; set; }
        [DisplayName("Last reference Pay")]
        public string secondreferencePay { get; set; }

        [DisplayName("Extra information")]
        public string extraInformation { get; set; }

        [DisplayName("Room comment")]
        public string roomComment { get; set; }
        //====


        public InvoiceReportModel()
        {
            this.idInvoice = 0;
            this.invoiceNr = String.Empty;
            this.idVoucher = 0;
            this.idInvoiceStatus = 0;
            this.descInvoiceStatus = String.Empty;
            this.descriptionInvoice = String.Empty;
            this.idClient = 0;
            this.idContPerson = 0;
            this.idTraveler = 0;
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
            this.dtFirstPay = null;
            this.dtLastPay = null;
            this.arrName = String.Empty;
            this.noDays = String.Empty;
            this.boarding = String.Empty;
            this.dateFrom = null;
            this.dateTo = null;
            this.service = String.Empty;
            this.nameEmployee = String.Empty;
            this.firstAmount = String.Empty;
            this.restAmount = String.Empty;
            this.firstReference = String.Empty;
            this.restReference = String.Empty;
            this.firstreferencePay = String.Empty;
            this.secondreferencePay = String.Empty;
            this.extraInformation = String.Empty;
            this.roomComment = String.Empty;


        }
     }    
}