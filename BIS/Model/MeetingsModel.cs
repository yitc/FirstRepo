using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MeetingsModel
    {
        [DisplayName("ID Meeting")]
        public int idMeeting { get; set; }

        [DisplayName("ID Category")]
        public int categoryMeetingId { get; set; }

         [DisplayName("Category")]
        public string categoryDescription { get; set; }

        [DisplayName("Description")]
        public string descriptionMeeting { get; set; }

        [DisplayName("Open date")]
        public Nullable<System.DateTime> openDateMeeting { get; set; }

        [DisplayName("Start date")]
        public Nullable<System.TimeSpan> startTimeMeeting { get; set; }

        [DisplayName("Duration")]
        public Nullable<System.TimeSpan> durationMeeting { get; set; }

        [DisplayName("All day")]
        public bool isAllDayMeeting { get; set; }

        [DisplayName("ID client")]
        public Nullable<int> clientId { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("ID person")]
        public Nullable<int> contactPersonId { get; set; }

        [DisplayName("Person")]
        public string namePerson { get; set; }

        [DisplayName("ID project")]
        public Nullable<int> projectId { get; set; }

        [DisplayName("Project")]
        public string nameProject { get; set; }

        [DisplayName("ID Priority")]
        public Nullable<int> priorityMeeting { get; set; }

        [DisplayName("Priority")]
        public string namePriority { get; set; }

        [DisplayName("ID Status")]
        public Nullable<int> statusMeeting { get; set; }

        [DisplayName("Status")]
        public string nameStatus { get; set; }

        [DisplayName("ID Owner")]
        public Nullable<int> emploeeOwner { get; set; }

        [DisplayName("Owner")]
        public string nameOwner { get; set; }

        [DisplayName("ID Responsible")]
        public Nullable<int> employeeResponsible { get; set; }

        [DisplayName("Responsible")]
        public string nameEmployee { get; set; }

        [DisplayName("Note")]
        public string noteMeeting { get; set; }

        [DisplayName("Remind")]
        public bool isRemind { get; set; }

        [DisplayName("Time remind")]
        public Nullable<System.DateTime> timeRemind { get; set; }

        [DisplayName("Date remind")]
        public Nullable<System.DateTime> dteRemind { get; set; }

        [DisplayName("End date")]
        public Nullable<System.DateTime> endDateMeeting { get; set; }
    }
}
