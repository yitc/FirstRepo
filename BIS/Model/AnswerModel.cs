using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AnswerModel
    {
        public int idAns { get; set; }

        public int idAnsType { get; set; }

        public int idQuest { get; set; }

        public int idQueryType { get; set; }

        [DisplayName("Label name")]
        public string nameLabel { get; set; }

        [DisplayName("Answer")]
        public string txtAns { get; set; }

        [DisplayName("Answer sort")]
        public int? ansSort { get; set; }

       
        public int? idQuestSkills { get; set; }

      
    }
}
