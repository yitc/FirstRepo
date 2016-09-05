using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MeetingDescModel
    {
        [DisplayName("ID Meeting")]
        public int idMeeting { get; set; }

        [DisplayName("Note")]
        public string noteMeeting { get; set; }

        [DisplayName("Category")]
        public string CategoryMeeting { get; set; }

        [DisplayName("Priority")]
        public string PriorityMeeting { get; set; }

        [DisplayName("Status")]
        public string StatusMeeting { get; set; }

        [DisplayName("Client")]
        public string Client { get; set; }

        [DisplayName("Person")]
        public string ContactPerson { get; set; }

        [DisplayName("Project")]
        public string Project { get; set; }
    }
}
