using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PriceListModel : IModel
    {

        [DisplayName("ID pricelist")]
        public int idPriceList { get; set; }

        [DisplayName("ID arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Arrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("Status arrangement")]
        public string statusArrangement { get; set; }

        [DisplayName("ID Client")]
        public int idClient { get; set; }

        [DisplayName("Date from")]
        public DateTime dtFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dtTo { get; set; }

        [DisplayName("Release date")]
        public DateTime dtPriceList { get; set; }

        [DisplayName("Active")]
        public Boolean isActive { get; set; }

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Name hotel service")]
        public string nameHotelService { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        [DisplayName("Release")]
        public Boolean isReleaseDate { get; set; }

        [DisplayName("ID hotel service")]
        public int idHotelService { get; set; }

      public PriceListModel()
        {
            this.idPriceList = 0;
            this.isActive = false;
            this.dtPriceList = DateTime.Now;
            this.idArrangement = 0;
            this.nameArrangement = String.Empty;
            this.idClient = 0;
            this.dtFrom = DateTime.Now;
            this.dtTo = DateTime.Now;
            this.idUserCreated = 0;
            this.nameUserCreated = String.Empty;
            this.dtUserCreated = DateTime.Now;
            this.idUserModified = 0;
            this.nameUserModified = String.Empty;
            this.dtUserModified = DateTime.Now;
            this.isReleaseDate = false;
            this.idHotelService = 0;
            this.nameHotelService = string.Empty;
            this.statusArrangement = string.Empty;
        }

    }
}