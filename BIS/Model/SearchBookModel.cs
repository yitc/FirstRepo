using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;


namespace BIS.Model
{
    public class SearchBookModel : IModel
    {
        //[DisplayName(" ")]
        //public bool select { get; set; }

        [DisplayName("Status Arrangement")]
        public string statusArrangement { get; set; } 

        [DisplayName("Name country")]
        public string nameCountry { get; set; } 

        [DisplayName("Theme trip")]
        public string themeTrip { get; set; } 

        [DisplayName("Date from")]
        public DateTime dtFromArrangement { get; set; } 

        [DisplayName("Date to")]
        public DateTime dtToArrangement { get; set; } 

        [DisplayName("ArticleId1")]
        public int ArticleId1 { get; set; }  

        [DisplayName("ArticleId2")]
        public int ArticleId2 { get; set; } 

        [DisplayName("ArticleId3")]
        public int ArticleId3 { get; set; } 

        [DisplayName("Wheelchair")]
        public int wheelchair { get; set; } 

        [DisplayName("Rollator")]
        public int Rollator { get; set; } 

        [DisplayName("ArmSometimes")]
        public int armSometimes { get; set; } 

        [DisplayName("Anchorage")]
        public int anchorage { get; set; } 

        [DisplayName("Name arrangement")]
        public string nameArrangement { get; set; }

        [DisplayName("idArrangement")]
        public int idArrangement { get; set; }


        public SearchBookModel()
        {
            this.statusArrangement = String.Empty;
            this.nameCountry = String.Empty;
            this.themeTrip = String.Empty;
            this.dtFromArrangement = DateTime.Now;
            this.dtToArrangement = DateTime.Now;
            this.ArticleId1 = 0;
            this.ArticleId2 = 0;
            this.ArticleId3 = 0;
            this.wheelchair = 0;
            this.Rollator = 0;
            this.armSometimes = 0;
            this.anchorage = 0;
            this.idArrangement = 0;
          
        }
    }
}
