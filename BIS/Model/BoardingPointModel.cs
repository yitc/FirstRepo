using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class BoardingPointModel : IModel    
    {

        [DisplayName(" ")]
        public bool isChecked { get; set; }

        [DisplayName("ID boarding point")]
        public int idBoardingPoint { get; set; }

        [DisplayName("Boarding point")]
        public string nameBoardingPoint { get; set; }

        [DisplayName("Address")]
        public string addressBoardingPoint { get; set; }

        [DisplayName("Departure")]
        public DateTime dtDeparture { get; set; }


        [DisplayName("Arrival")]
        public DateTime dtArrival { get; set; }

        [DisplayName("Sort")]
        public int sortBoardingPoint { get; set; }


        public BoardingPointModel()
        {
            this.isChecked = false;
            this.idBoardingPoint = 0;
            this.nameBoardingPoint = String.Empty;
            this.addressBoardingPoint = String.Empty;
            DateTime tmp = DateTime.Parse("1/1/1900");
            this.dtDeparture = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);
            this.dtArrival = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);
            this.sortBoardingPoint = 0;
        }
    }
}
