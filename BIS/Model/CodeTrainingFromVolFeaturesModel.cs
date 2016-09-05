using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class CodeTrainingFromVolFeaturesModel : IModel
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Type")]
        public string type { get; set; }

        [DisplayName("Code")]
        public string code { get; set; }

        [DisplayName("name")]
        public string nameArrangementStatus { get; set; }

        
    }
}