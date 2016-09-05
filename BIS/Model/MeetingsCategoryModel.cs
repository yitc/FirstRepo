using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MeetingsCategoryModel : IModel
    {
        [DisplayName("Id")]
        public int idMeetingCategory { get; set; }
        
        [DisplayName("Category")]
        public string categoryDescription { get; set; }
    }
}
