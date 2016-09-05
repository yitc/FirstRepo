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

        [DisplayName("ID status")]
        public int idStatus { get; set; }

        [DisplayName("ID travel papers")]
        public int idTravelPapers { get; set; }

        [DisplayName("Date booked")]
        public DateTime dtBooked { get; set; }

        [DisplayName("Price")]
        public decimal price { get; set; }


        public ArrangementBookModel()
        {
            this.idArrangementBook = 0;
            this.idArrangement = 0;
            this.idContPers = 0;
            this.idStatus = 0;
            this.idTravelPapers = 0;
            this.dtBooked = DateTime.Now;
            this.price = 0;
        }
    }

    public class ArrangementFuncSkillsModel 
    {

        [DisplayName("Quest")]
        public string Quest { get; set; }

        public int Required { get; set; }

        public int Booked { get; set; }

        public int Available { get; set; }

        public ArrangementFuncSkillsModel()
        {
            this.Quest = String.Empty;
            this.Required = 0;
            this.Booked = 0;
            this.Available = 0;
        }
    }
}
