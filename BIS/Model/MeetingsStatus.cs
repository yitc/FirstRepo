using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MeetingsStatus : IModel
    {
        [DisplayName("Id")]
        public int idMeetingStatus { get; set; }

        [DisplayName("Status")]
        public string desriptionMeetingStatus { get; set; }
    }
}
