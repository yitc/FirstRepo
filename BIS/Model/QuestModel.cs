using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class QuestModel
    {
        public int idQuest { get; set; }

        public int idQuestGroup { get; set; }


        [DisplayName("Question name")]
        public string txtQuest { get; set; }

        [DisplayName("Question sort")]
        //public int? questSort { get; set; }
        public decimal? questSort { get; set; }

    }
}
