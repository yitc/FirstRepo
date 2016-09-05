using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VolontaryTripModel : IModel
    {
        [DisplayName("Question group")]
        public string nameQuestGroup { get; set; }

        public int idQuest { get; set; }

        [DisplayName("Question")]
        public string txtQuest { get; set; }

        public int? idAns { get; set; }

        [DisplayName("Answer")]
        public string txtAns { get; set; }

        public int idAnsType { get; set; }

        public int?  idcpr { get; set; }

        public string txt { get; set; }

        public int? questSort { get; set; }

        public int? ansSort { get; set; }


    }
}
