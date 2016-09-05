using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MedicalVoluntaryQuestModel:IModel
    {
        [DisplayName("Question group")]
        public string nameQuestGroup { get; set; }

        public int idQuestGroup { get; set; }

        public int idQuest { get; set; }

        [DisplayName("Question")]
        public string txtQuest { get; set; }

        public int? idAns { get; set; }

        [DisplayName("Answer")]
        public string txtAns { get; set; }

        public int idAnsType { get; set; }

        [DisplayName("Answer type")]
        public string nameAnsType { get; set; }

        //public int? questSort { get; set; }
        public decimal? questSort { get; set; }

        public int? ansSort { get; set; }

    }
    public class MedicalVoluntarySkillsModel
    {


        public int idQuest { get; set; }

        [DisplayName("Question")]
        public string txtQuest { get; set; }


        public int idQuestSkills { get; set; }


      
        public int? idQuestAns { get; set; }
        

       


    }
}
