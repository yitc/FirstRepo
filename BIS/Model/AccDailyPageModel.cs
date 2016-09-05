using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccDailyPageModel : IModel
    {
          [DisplayName("ID")]
        public int idAccPage { get; set; }

          [DisplayName("Dbk.Code")]
        public string codeDaily { get; set; }

          [DisplayName("Ref.number")]
        public int refNumber { get; set; }

          [DisplayName("Date")]
        public DateTime? pageDate { get; set; }

          [DisplayName("Period")]
        public int? periodPage { get; set; }

          [DisplayName("Begin saldo")]
        public decimal? beginSaldo { get; set; }

          [DisplayName("End saldo")]
        public decimal? endSaldo { get; set; }

          [DisplayName("Debit")]
        public decimal? debitPage { get; set; }

          [DisplayName("Credit")]
        public decimal? creditPage { get; set; }

          [DisplayName("Type")]
        public int? typeDaily { get; set; }

          [DisplayName("Description")]
        public string descPage { get; set; }
    }

}
