using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VolSimilarityModel
    {
        public string idSimilarity { get; set; }

        public int? idContPers { get; set; }

        public string optionSimilarity { get; set; }

        public DateTime dtEffectiveDate { get; set; }

        public DateTime dtExpirationDate { get; set; }

        public DateTime dtSent { get; set; }

        public VolSimilarityModel()
        {
            this.idSimilarity = String.Empty;
            this.idContPers = 0;
            this.optionSimilarity = String.Empty;
            this.dtEffectiveDate = DateTime.MinValue;
            this.dtExpirationDate = DateTime.MinValue;
            this.dtSent = DateTime.MinValue;
            
        }
    }

    public class VolSimilarityArchiveModel
    {
        public int id { get; set; }

        public string idSimilarity { get; set; }

        public int? idContPers { get; set; }

        public string optionSimilarity { get; set; }

        public DateTime dtEffectiveDate { get; set; }

        public DateTime dtExpirationDate { get; set; }

        public DateTime dtSent { get; set; }

        public VolSimilarityArchiveModel()
        {
            this.id = 0;
            this.idSimilarity = String.Empty;
            this.idContPers = 0;
            this.optionSimilarity = String.Empty;
            this.dtEffectiveDate = DateTime.MinValue;
            this.dtExpirationDate = DateTime.MinValue;
            this.dtSent = DateTime.MinValue;

        }
    }
}
