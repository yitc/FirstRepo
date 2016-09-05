using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ExtraArticleModel
    {

        [DisplayName("dtFromArrangement")]
        public DateTime dtFromArrangement { get; set; }

        [DisplayName("nameArrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("nameArtical")]
        public string nameArtical { get; set; }

        [DisplayName("fullName")]
        public string fullName { get; set; }

        [DisplayName("NameLogin")]
        public string nameLogin { get; set; }

        [DisplayName("invoicefullNumber")]
        public string invoicefullNumber { get; set; }

        [DisplayName("price")]
        public decimal price { get; set; }

        [DisplayName("DateFrom")]
        public DateTime dateFrom { get; set; }

        [DisplayName("DateTo")]
        public DateTime dateTo { get; set; }

        public ExtraArticleModel()
        {
            this.dtFromArrangement = DateTime.Now;
            this.nameArrangement = String.Empty;
            this.nameArtical = String.Empty;
            this.fullName = String.Empty;
            this.invoicefullNumber = String.Empty;
            this.price = 0;
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now;
            this.nameLogin = string.Empty;
        }


    }

}