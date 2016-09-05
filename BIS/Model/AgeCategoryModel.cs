using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model 
{
    public class AgeCategoryModel : IModel
    {
        [DisplayName("Id Age Category")]
        public int idAgeCategory { get; set; }

        [DisplayName("Description Age Category")]
        public string descAgeCategory { get; set; }

        [DisplayName("Min Age Category")]
        public int? minAgeCategory { get; set; }

        [DisplayName("Max Age Category")]
        public int? maxAgeCategory { get; set; }



        public AgeCategoryModel()
        {
            this.descAgeCategory = String.Empty;
            this.idAgeCategory = 0;
            this.maxAgeCategory = 0;
            this.minAgeCategory = 0;
        }
    }

}