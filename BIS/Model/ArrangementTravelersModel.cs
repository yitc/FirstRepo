using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    // dao and bus in ArrangementBookPerson
    public class ArrangementTravelersModel
    {
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Id person")]
        public int idContPers { get; set; }
        
        [DisplayName("Id travel with")]
        public int idTravelWithPerson { get; set; }
   
        [DisplayName("First name")]
        public string firstnameTraveler { get; set; }

        [DisplayName("Mid name")]
        public string midnameTraveler { get; set; }
        
        [DisplayName("Last name")]
        public string lastnameTraveler { get; set; }
      


        [DisplayName("Full Name")]
        public string fullname
        {
            get { return firstnameTraveler.Trim() + " " + midnameTraveler.Trim() + " " + lastnameTraveler.Trim(); }
        }
        [DisplayName("Birth date")]
        public DateTime birthdate { get; set; }

        public ArrangementTravelersModel()
        {
            this.id = 0;
            this.idArrangement = 0;
            this.idContPers = 0;
            this.idTravelWithPerson = 0;
            this.firstnameTraveler = String.Empty;
            this.lastnameTraveler = String.Empty;
            this.midnameTraveler = String.Empty;
            this.birthdate = Convert.ToDateTime("1900-01-01");
        }
    }

    public class ArrangementTravelersInvoiceModel
    {
        [DisplayName("Id")]
        public int idArrangementBook { get; set; }

        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Id person")]
        public int idContPers { get; set; }

        [DisplayName("Pay invoice")]
        public int idPayInvoice { get; set; }

        [DisplayName("First name")]
        public string firstnameTraveler { get; set; }

        [DisplayName("Last name")]
        public string lastnameTraveler { get; set; }

        [DisplayName("Full Name")]
        public string passportname { get; set; }

        [DisplayName("Full Name")]
        public string fullname
        {
            get { return firstnameTraveler.Trim() + " " + lastnameTraveler.Trim(); }            
        }

        [DisplayName("Birth date")]
        public DateTime birthdate { get; set; }

        public ArrangementTravelersInvoiceModel()
        {
            this.idArrangementBook = 0;
            this.idArrangement = 0;
            this.idContPers = 0;
            this.idPayInvoice = 0;
            this.firstnameTraveler = String.Empty;
            this.lastnameTraveler = String.Empty;
            this.passportname = String.Empty;
            this.birthdate = Convert.ToDateTime("1900-01-01");
        }
    }
}
