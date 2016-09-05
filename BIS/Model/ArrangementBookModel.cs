using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementBookModel : IModel
    {
        [DisplayName("ID arrangement booking")]
        public int idArrangementBook { get; set; }

        [DisplayName("ID arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("ID debitor")]
        public int idDebitor { get; set; }

        [DisplayName("Type Debitor")]
        public string typeDebitor { get; set; }

        [DisplayName("ID status")]
        public int idStatus { get; set; }

        [DisplayName("Status date")]
        public DateTime dtStatus { get; set; }

        [DisplayName("ID travel papers")]
        public int idTravelPapers { get; set; }

        [DisplayName("Date booked")]
        public DateTime dtBooked { get; set; }

        [DisplayName("Price")]
        public decimal price { get; set; }

        [DisplayName("Insurance")]
        public bool isInsurance { get; set; }

        [DisplayName("ID boarding")]
        public int idBoarding { get; set; }

        [DisplayName("Cancel isurance")]
        public bool isCancelInsurance { get; set; }


        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

    //==== potrebno za fakturu ===
        [DisplayName("First name")]
        public string firstname { get; set; }
        [DisplayName("Mid name")]
        public string midname { get; set; }
        [DisplayName("Last name")]
        public string lastname { get; set; }
    //========
        
        [DisplayName("Medical devices")]
        public bool isMedicalDevices { get; set; }
    //===== id ko placa
        [DisplayName("Pay Invoice")]
        public int idPayInvoice { get; set; }
   
        public ArrangementBookModel()
        {
            this.idArrangementBook = 0;
            this.idArrangement = 0;
            this.idContPers = 0;
            this.idDebitor = 0;
            this.typeDebitor = String.Empty;
            this.idStatus = 0;
            this.dtStatus = DateTime.Now;
            this.idTravelPapers = 0;
            this.dtBooked = DateTime.Now;
            this.price = 0;
            this.isInsurance = false;
            this.idBoarding = 0;
            this.idUserCreated = 0;
            this.idUserModified = 0;
            this.dtUserCreated = DateTime.Now;
            this.dtUserModified = DateTime.Now;
            this.isMedicalDevices = false;
            this.idPayInvoice = 0;
        }
    }

    public class ArrangementFuncSkillsModel
    {

        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Quest")]
        public string Quest { get; set; }

        public int Required { get; set; }

        public int Booked { get; set; }

        public int Available { get; set; }

        public ArrangementFuncSkillsModel()
        {
            this.ID = 0;
            this.Quest = String.Empty;
            this.Required = 1;
            this.Booked = 0;
            this.Available = 0;
        }
    }

    public class ArrangementSelectedFuncSkillsModel
    {

        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("ID quest")]
        public int id { get; set; }


        public ArrangementSelectedFuncSkillsModel()
        {
            this.idContPers = 0;
            this.id = 0;
        }
    }

}
