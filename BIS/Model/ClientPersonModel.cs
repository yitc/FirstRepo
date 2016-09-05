using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.Model 
{
    public class ClientPersonModel : IModel
    {
        [DisplayName("Id Client Person")]
        public int idCliPer { get; set; }

        [DisplayName("Id Client")]
        public int? idCLient { get; set; }

        [DisplayName("Id Contact Person")]
        public int? idContPerson { get; set; }

        [DisplayName("Id Function")]
        public int? idFunction { get; set; }

        [DisplayName("Client name")]
        public string nameClient { get; set; }

        [DisplayName("Function name")]
        public string nameFunction { get; set; }

        [DisplayName("First name contact person")]
        public string firstname { get; set; }

        [DisplayName("Midname contact person")]
        public string midname { get; set; }

        [DisplayName("Last name contact person")]
        public string lastname { get; set; }

        

    }

}
