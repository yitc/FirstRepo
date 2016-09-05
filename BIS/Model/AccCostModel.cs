using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccCostModel : IModel
    {
        [DisplayName("Id")]
        public int idCost { get; set; }

        [DisplayName("Cost code")]
        public string codeCost { get; set; }

        [DisplayName("Description")]
        public string descCost { get; set; }
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

        public AccCostModel()
        {
            this.codeCost = String.Empty;
            //this.userCreated = 0;
            //this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.userModified = 0;
            this.dtModified = Convert.ToDateTime("1900-01-01");
        }
    }
}
