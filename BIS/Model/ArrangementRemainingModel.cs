using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementRemainingModel
    {
        public int idArrangement { get; set; }

        public DateTime? awayDt { get; set; }

        public DateTime? awayDt2 { get; set; }

        public string awayAirport { get; set; }

        public string awayAirport2 { get; set; }

        public string awayFlightNr { get; set; }

        public string awayFlightNr2 { get; set; }

        public DateTime? arrivalDt { get; set; }

        public DateTime? arrivalDt2 { get; set; }

        public string arrivalAirport { get; set; }

        public string arrivalAirport2 { get; set; }

        public DateTime? backDt { get; set; }

        public DateTime? backDt2 { get; set; }

        public string backAirport { get; set; }

        public string backAirport2 { get; set; }

        public string backFlightNr { get; set; }

        public string backFlightNr2 { get; set; }

        public DateTime? arrivalDt3 { get; set; }

        public DateTime? arrivalDt4 { get; set; }

        public string arrivalAirport3 { get; set; }

        public string arrivalAirport4 { get; set; }

        public string collectTime { get; set; }

        public string airportSociety { get; set; }

        public string special { get; set; }

        public bool twoFlight { get; set; }

        public string program { get; set; }

        public string letter { get; set; }

        public string rulesAppointment { get; set; }

        public string letterImage { get; set; }

        public ArrangementRemainingModel()
        {
            this.idArrangement = 0;
            this.awayDt = DateTime.Now;
            this.awayDt2 = DateTime.Now;
            this.awayAirport = String.Empty;
            this.awayAirport2 = String.Empty;
            this.awayFlightNr = String.Empty;
            this.awayFlightNr2 = String.Empty;
            this.arrivalDt = DateTime.Now;
            this.arrivalDt2 = DateTime.Now;
            this.arrivalAirport = String.Empty;
            this.arrivalAirport2 = String.Empty;
            this.backDt = DateTime.Now;
            this.backDt2 = DateTime.Now;
            this.backAirport = String.Empty;
            this.backAirport2 = String.Empty;
            this.backFlightNr = String.Empty;
            this.backFlightNr2 = String.Empty;
            this.arrivalDt3 = DateTime.Now;
            this.arrivalDt4 = DateTime.Now;
            this.arrivalAirport3 = String.Empty;
            this.arrivalAirport4 = String.Empty;
            this.collectTime = String.Empty;
            this.airportSociety = String.Empty;
            this.special = String.Empty;
            this.twoFlight = false;
            this.letterImage = string.Empty;
           
        }

    }
    
}


