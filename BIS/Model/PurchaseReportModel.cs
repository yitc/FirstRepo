using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class PurchaseReportModel : IModel
    {
        //idArrangementBook,a.nameArrangement, at.nameArrType, a.nrTraveler,
        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Name arrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("Name type")]
        public string nameArrType { get; set; }

        [DisplayName("Nr of travelers")]
        public int nrTraveler { get; set; }

        [DisplayName("Date from")]
        public DateTime dateFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dateTo { get; set; }

        [DisplayName("Nr of trips")]
        public int NrType { get; set; }


        public PurchaseReportModel()
        {
            this.idArrangement = 0;
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now;
            this.nameArrType = String.Empty;
            this.nrTraveler = 0;
            this.nameArrangement = String.Empty;
            this.NrType = 0;



        }
    }

    public class PurchaseReportModel2 : IModel
    {

        //[DisplayName("Name arrangement")]
        //public string nameArrangement { get; set; }        

        [DisplayName("Code arrangement")]
        public string codeArrangement { get; set; }

        [DisplayName("Type")]
        public string nameArrType { get; set; }

        [DisplayName("Nr of travelers")]
        public int nrTraveler { get; set; }

        [DisplayName("Date from")]
        public DateTime dateFrom { get; set; }

        [DisplayName("Date to")]
        public DateTime dateTo { get; set; }

        [DisplayName("Date from1")]
        public DateTime dateFrom1 { get; set; }

        [DisplayName("Date to1")]
        public DateTime dateTo1 { get; set; }

        public PurchaseReportModel2()
        {
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now;
            this.nameArrType = String.Empty;
            this.nrTraveler = 0;
            //this.nameArrangement = String.Empty;
            this.codeArrangement = String.Empty;
            //this.nameArrangement1 = String.Empty;



        }
    }
}
