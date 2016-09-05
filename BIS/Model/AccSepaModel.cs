using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIS.Model
{
    public class AccSepaModel : IModel
    {
        [DisplayName("Id Sepa")]
        public int idSepa { get; set; }

        [DisplayName("Name")]
        public string nameSepa { get; set; }

        [DisplayName("Date")]
        public DateTime dtSepa { get; set; }

        [DisplayName("Amount")]
        public decimal amountSepa { get; set; }

        [DisplayName("Status")]
        public int status { get; set; }

        [DisplayName("Sepa")]
        public string sepaFInal { get; set; }

        [DisplayName("Created")]
        public DateTime dtCreationDate { get; set; }

        [DisplayName("Approved by")]
        public int approveUser { get; set; }

        [DisplayName("Date approved")]
        public DateTime dtApprove { get; set; }

        public AccSepaModel()
        {

            this.nameSepa = String.Empty;
            this.dtSepa = Convert.ToDateTime("1900-01-01");
            this.amountSepa = 0;
            this.status = 0;
            this.sepaFInal = String.Empty;
            this.dtSepa = DateTime.Now;
            this.approveUser = 0;
            this.dtApprove = Convert.ToDateTime("1900-01-01");
            this.dtCreationDate = Convert.ToDateTime("1900-01-01");
            
        }
    }
}
