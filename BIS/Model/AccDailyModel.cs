using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccDailyModel : IModel
    {

        [DisplayName("Dbk.Code")]
        public int idDaily { get; set; }

        [DisplayName("Dbk.Code1")]
        public string codeDaily { get; set; }

        [DisplayName("Description")]
        public string descDaily { get; set; }

        [DisplayName("Type")]
        public int? idDailyType { get; set; }
        [DisplayName("Type name")]
        public string descDailyType { get; set; }

        [DisplayName("Account")]
        public string numberLedgerAccount { get; set; }
        [DisplayName("Account name")]
        public string descLedgerAccount { get; set; }

        [DisplayName("Bank")]
        public int idBank { get; set; }
        [DisplayName("Bank name")]
        public string nameBank { get; set; }

        [DisplayName("Iban")]
        public string ibanBank { get; set; }

        [DisplayName("Booking year")]
        public string bookingYear { get; set; }

        [DisplayName("Automatic booking")]
        public bool automaticBook { get; set; }

        [DisplayName("Begin preiod")]
        public bool beginPeriod { get; set; }
        //[DisplayName("Class")]
        //public int idDailyVerIn { get; set; }
        //[DisplayName("Class name")]
        //public string nameDailyVerIn { get; set; }
           

        [DisplayName("Unbooked")]
        public int unBooked { get; set; }

        [DisplayName("Use counter")]
        public bool isUseCounter { get; set; }
        [DisplayName("Counter")]
        public int inkop { get; set; }

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

        public AccDailyModel()
        {
            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.dtModified = Convert.ToDateTime("1900-01-01");
        }
    }
}