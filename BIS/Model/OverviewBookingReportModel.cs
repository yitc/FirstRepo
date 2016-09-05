using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public  class OverviewBookingReportModel :IModel
    {
       [DisplayName("Arrangement code")]
        public string nameArrangement { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; }

        [DisplayName("Booked person")]
        public string bookedPerson { get; set; }

        [DisplayName("Invoice nr")]
        public int  invoiceRbr { get; set; }

        [DisplayName("Price invoice")]
        public int priceInvoice { get; set; }

        [DisplayName("Employee")]
        public string Employee { get; set; }
    }
  public class OverviewBookingFormModel : IModel {
      [DisplayName("Number")]
      public int idContPers { get; set; }

      [DisplayName("Name")]
      public string personFullName { get; set; }

      [DisplayName("Arrangement code")]
      public string nameArrangement { get; set; }

      [DisplayName("Status")]
      public string status { get; set; }

      [DisplayName("nameTravelPapers")]
      public string nameTravelPapers { get; set; }

      [DisplayName("Date from")]
      public DateTime dtFromArrangement { get; set; }

      //[DisplayName("Amount(all invoices on thist trip)")]
      //public string amount { get; set; }

      [DisplayName("User created")]
      public string userCreated { get; set; }

      [DisplayName("Date created")]
      public DateTime dateCreated { get; set; }

      public OverviewBookingFormModel() 
      {
          this.idContPers = 0;
          this.status = string.Empty;
          this.personFullName = string.Empty;
          this.nameArrangement = string.Empty;
          this.dtFromArrangement = DateTime.Now;
          //this.amount = string.Empty;
          this.userCreated = string.Empty;
          this.dateCreated = DateTime.Now;
          this.nameTravelPapers = string.Empty;
      }

  }
}









        

       

        //public OverviewBookingReportModel()
        //{
           
           
        //    this.nameArrangement = String.Empty;
        //    this.dtFromArrangement = DateTime.Now;
        //    this.dtToArrangement = DateTime.Now;
        //    this.nameTitle = String.Empty;
           
        //    this.name = String.Empty;
         
        //    this.address = String.Empty;
        //    this.birthdate = DateTime.Now;
        //    this.gender = String.Empty;
           
        //}

    