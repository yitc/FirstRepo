using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArticalExtraOptionalModel
    {

        public int arranementBookID { get; set; }

        [DisplayName("ID article")]
        public string idArticle { get; set; }

        [DisplayName("Name artical")]
        public string nameArtical { get; set; }

        [DisplayName("Nr article")]
        public int nrArticle { get; set; }

        [DisplayName("Selling price")]
        public decimal sellingPrice { get; set; }

        [DisplayName("Extra")]
        public Boolean isExtra { get; set; }

        [DisplayName("Optional")]
        public Boolean isOptional { get; set; }


        public ArticalExtraOptionalModel()
        {
            this.arranementBookID = -1;
            this.idArticle = string.Empty;
            this.nameArtical = string.Empty;
            this.nrArticle = 0;
            this.sellingPrice = 0;
            this.isExtra = false;
            this.isOptional = false;
        }
    }
}
