using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class QuestGroupModel
    {
        public int idQuestGroup { get; set; }

        [DisplayName("Question group name")]
        public string nameQuestGroup { get; set; }

        public int idCompany { get; set; }
    }
}
