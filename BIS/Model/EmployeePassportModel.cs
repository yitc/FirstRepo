using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class EmployeePassportModel
    {
        [DisplayName("ID passport")]
        public int idemppass { get; set; }

        [DisplayName("ID Employee")]
        public int idEmployee { get; set; }

        [DisplayName("Passport")]
        public string passname { get; set; }

        [DisplayName("Number")]
        public string passnumber { get; set; }

        [DisplayName("Birth place")]
        public string passbrplace { get; set; }

        [DisplayName("Issue place")]
        public string passisplace { get; set; }

        [DisplayName("Issue date")]
        public DateTime? passisued { get; set; }

        [DisplayName("Valid to")]
        public DateTime? passvalid { get; set; }

        [DisplayName("Nacionality")]
        public int? passnational { get; set; }

        public EmployeePassportModel()
        {
            this.passbrplace = String.Empty;
            this.passisplace = String.Empty;
            this.passisued = DateTime.Now;
            this.passvalid = DateTime.Now;
            this.passnational = 0;
        }
    }
   
}