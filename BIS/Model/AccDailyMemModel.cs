using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccDailyMemModel : IModel
    {
        [DisplayName("ID")]
        public int idDailyMem { get; set; }

        [DisplayName("Dbk.Code")]
        public string codeDaily { get; set; }

        [DisplayName("Statement")]
        public int refNo { get; set; }

        [DisplayName("Date")]
        public DateTime? dtMem { get; set; }

        [DisplayName("Booking year")]
        public string bookingYear { get; set; }

        [DisplayName("Begin period")]
        public bool beginPeriod { get; set; }

        [DisplayName("Debit")]
        public decimal debit { get; set; }

        [DisplayName("Credit")]
        public decimal credit { get; set; }

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
        
        public AccDailyMemModel()
        {
            this.codeDaily = String.Empty;
            this.refNo = 0;
            this.dtMem = DateTime.Now;
            this.bookingYear = String.Empty;
            this.beginPeriod = false;

            this.debit = 0;
            this.credit = 0;

            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.dtModified = Convert.ToDateTime("1900-01-01");
        }
    }
}
