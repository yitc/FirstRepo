using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccLedgerClassModel : IModel
    {
         [DisplayName("Id")]
        public int idClass { get; set; }
         [DisplayName("Code")]
        public string codeClass { get; set; }
         [DisplayName("Description")]
        public string descClass { get; set; }
         [DisplayName("Level")]
        public int? levelClass { get; set; }
         [DisplayName("Order")]
        public int? orderClass { get; set; }

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

        public  AccLedgerClassModel()
        {
            this.dtCreated = Convert.ToDateTime("1900-01-01");
            this.dtModified = Convert.ToDateTime("1900-01-01");
        }
    }
}