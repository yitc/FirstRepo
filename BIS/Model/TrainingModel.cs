using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class TrainingModel :IModel
    {
        [DisplayName ("Id Training")]
        public int idTraining { get; set; }

        [DisplayName ("Code Training")]
        public string codeTraining { get; set; }

        [DisplayName("Name Training")]
        public string nameTraining { get; set; }

    }

}