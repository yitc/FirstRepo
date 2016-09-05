using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public  class AvailabilitySkillsModel:IModel
    {

      [DisplayName("Title")]
      public string nameTitle { get; set; }

      [DisplayName("First name")]
      public string firstname { get; set; }

      [DisplayName("Last name")]
      public string lastname { get; set; }

      [DisplayName("Date from")]
      public DateTime dateFrom { get; set; }

      [DisplayName("Date to")]
      public DateTime dateTo { get; set; }

      [DisplayName("Age")]
      public int Age { get; set; }

      [DisplayName("Id")]
      public int ID { get; set; }

      [DisplayName("Quest")]
      public string quest { get; set; }

      [DisplayName("Date from1")]
      public DateTime dateFrom1 { get; set; }

      [DisplayName("Date to1")]
      public DateTime dateTo1 { get; set; }
        
        public AvailabilitySkillsModel()
        {
            this.nameTitle = String.Empty;
            this.firstname = String.Empty;
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now;
            this.Age = 0;
            this.ID = 0;
            this.quest = String.Empty;
           
        }
    }
}


