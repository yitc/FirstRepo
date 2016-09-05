using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementBoardingPointModel : IModel
    {
        [DisplayName("ID boarding point")]
        public int idBoardingPoint { get; set; }

        [DisplayName("ID Arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Departure")]
        public DateTime dtDeparture { get; set; }

        [DisplayName("Arrival")]
        public DateTime dtArrival { get; set; }

        [DisplayName("Sort boarding point")]
        public int sortBoardingPoint { get; set; }

        public ArrangementBoardingPointModel()
        {
            this.idBoardingPoint = 0;
            this.idArrangement = 0;

            DateTime tmp = DateTime.Now;
            this.dtDeparture = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);
            this.dtArrival = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);

            this.sortBoardingPoint = 0;
        }
    }
}
