using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccDailyBankModel : IModel
    {
          [DisplayName("ID")]
        public int idDailyBank { get; set; }

          [DisplayName("Dbk.Code")]
        public string codeDaily { get; set; }

          [DisplayName("Statement")]
        public int refNo { get; set; }

          [DisplayName("Date")]
        public DateTime? dtStatement { get; set; }

          [DisplayName("Begin saldo")]
        public decimal? begSaldo { get; set; }

          [DisplayName("End saldo")]
        public decimal? endSaldo { get; set; }

          [DisplayName("Unlisted")]
          public decimal? difference { get; set; }

          [DisplayName("Booked")]
          public decimal? booked { get; set; }
          //[DisplayName("Bank/Kas")]
          //public int bankKas { get; set; }
          [DisplayName("File")]
          public string pdfFile { get; set; }

          [DisplayName("Booking year")]
          public string bookingYear { get; set; }

          //==========================================
          [DisplayName("User created")]
          public int userCreated { get; set; }
          [DisplayName("Date created")]
          public DateTime dtCreated { get; set; }
          [DisplayName("User modified")]
          public int userModified { get; set; }
          [DisplayName("Date modified")]
          public DateTime dtModified { get; set; }
          //==========================================

        public AccDailyBankModel()
          {
              this.codeDaily = String.Empty;
              this.refNo = 0;
              this.dtStatement = DateTime.Now;
              this.begSaldo = 0;
              this.endSaldo = 0;
              this.difference = 0;
              this.booked = 0;
              this.pdfFile = String.Empty;
              this.bookingYear = String.Empty;
              this.dtCreated = Convert.ToDateTime("1900-01-01");
              this.dtModified = Convert.ToDateTime("1900-01-01");
          }

    }
}