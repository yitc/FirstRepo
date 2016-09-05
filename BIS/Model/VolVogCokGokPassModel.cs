using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VolVogCokGokPassModel
    {
        [DisplayName("Number")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("First name")]
        public string firstName { get; set; }

        [DisplayName("Last name")]
        public string lastName { get; set; }

        [DisplayName("Middle name")]
        public string midName { get; set; }

        [DisplayName("Age")]
        public int age { get; set; }

        //[DisplayName("Gender")]
        //public string gender { get; set; }

        [DisplayName("Date expiried")]
        public DateTime dateExpiried { get; set; }

        [DisplayName("Documet expiried")]
        public string type { get; set; }

        [DisplayName("E-mail")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public string phone { get; set; }

        [DisplayName("Function")]
        public string function { get; set; }

        [DisplayName("Travel with us")]
        public int NrTravel { get; set; }

        public VolVogCokGokPassModel()
        {

            this.title = String.Empty;
            this.firstName = String.Empty;
            this.lastName = String.Empty;
            this.midName = String.Empty;
            this.age = 0;
            //this.gender = String.Empty;
            //this.dateExpiried = DateTime.Now.ToString("dd/mm/yyyy");
            this.dateExpiried = DateTime.Now;
            this.type = String.Empty;
            this.email = String.Empty;
            this.phone = String.Empty;
            this.idContPers = -1;
            this.function = string.Empty;
            this.NrTravel = 0;


        }


    }
}

