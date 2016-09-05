using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class BISAppointment : INotifyPropertyChanged
    {
    
        private Guid id = Guid.Empty;                
        private DateTime start = DateTime.Now;
        private DateTime end = DateTime.Now;
        private string subject = string.Empty;
        private string description = string.Empty;
        private string location = string.Empty;
        private int category = 0;
        private string categoryName = string.Empty;
        private int priority = 0;
        private string priorityName = string.Empty;
        private int status = 0;
        private string statusName = string.Empty;
        private int client = 0;
        private string clientName = string.Empty;
        private int contact = 0;
        private string contactName = string.Empty;
        private int project = 0;
        private string projectName = string.Empty;
        private int owner = 0;
        private string ownerName = string.Empty;
        private int responsible = 0;
        private string responsibleName = string.Empty;
        private bool isAllDay = false;
        private bool isReminder = false;
        private string background;
        private string showtime;
        private TimeSpan? reminder = null;
        private TimeSpan? reminderSnoozed = null;
        private bool reminderDismissed = false;
        //jelena
        private int idArrangement=0;
        private string nameArrangement = string.Empty;
        // end jelena
      //  private bool ReminderDismissed = false;

        private List<BISAppointment> exceptions;

        public bool newApp = true;
        public BISAppointment()
        {
            
        }

        // jelena - zadnja dva parametra - int idArrangement, string nameArrangement
        public BISAppointment(DateTime start, DateTime end, string subject, string description, string location, int category, string categoryName, int priority,
            string priorityName, int status, string statusName, int client, string clientName, int contact, string contactName, int project, string projectName,
            int owner, string ownerName, int responsible, string responsibleName, bool isAllDay, bool isReminder, string background, string showtime,
            TimeSpan reminder, int idArrangement, string nameArrangement)
        {
            this.start = start;
            this.end = end;
            this.subject = subject;
            this.description = description;
            this.location = location;
            this.category = category;
            this.categoryName = categoryName;
            this.priority = priority;
            this.priorityName = priorityName;
            this.status = status;
            this.statusName = statusName;
            this.client = client;
            this.clientName = clientName;
            this.contact = contact;
            this.contactName = contactName;
            this.project = project;
            this.projectName = projectName;
            this.owner = owner;
            this.ownerName = ownerName;
            this.responsible = responsible;
            this.responsibleName = responsibleName;
            this.isAllDay = isAllDay;
            this.isReminder = isReminder;
            this.background = background;
            this.showtime = showtime;
            this.reminder = reminder;
          //  this.reminderDismissed = reminderDismissed;
            // jelena
            this.idArrangement = idArrangement;
            this.nameArrangement = nameArrangement;
            // end jelena
  
        }
  
        public List<BISAppointment> Exceptions
        {
            get
            {
                return this.exceptions;
            }
            set
            {
                if (this.exceptions != value)
                {
                    this.exceptions = value;
                    this.OnPropertyChanged("Exceptions");
                }
            }
        }        
        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }

        public TimeSpan? Reminder
        {
            get
            {
                return this.reminder;
            }
            set
            {
                if (this.reminder != value)
                {
                    this.reminder = value;
                    this.OnPropertyChanged("Reminder");
                }
            }
        }

        public TimeSpan? ReminderSnoozed
        {
            get
            {
                return this.reminderSnoozed;
            }
            set
            {
                if (this.reminderSnoozed != value)
                {
                    this.reminderSnoozed = value;
                    this.OnPropertyChanged("ReminderSnoozed");
                }
            }
        }

        public bool ReminderDismissed
        {
            get
            {
                return this.reminderDismissed;
            }
            set
            {
                if (this.reminderDismissed != value)
                {
                    this.reminderDismissed = value;
                    this.OnPropertyChanged("ReminderDismissed");
                }
            }
        }
  
        public DateTime Start
        {
            get
            {
                return this.start;
            }
            set
            {
                if (this.start != value)
                {
                    this.start = value;
                    this.OnPropertyChanged("Start");
                }
            }
        }

        public DateTime End
        {
            get
            {
                return this.end;
            }
            set
            {
                if (this.end != value)
                {
                    this.end = value;
                    this.OnPropertyChanged("End");
                }
            }
        }

        public string Subject
        {
            get
            {
                return this.subject;
            }
            set
            {
                if (this.subject != value)
                {
                    this.subject = value;
                    this.OnPropertyChanged("Subject:");
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnPropertyChanged("Description");
                }
            }
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (this.location != value)
                {
                    this.location = value;
                    this.OnPropertyChanged("Location:");
                }
            }
        }

        public int Category
        {
            get
            {
                return this.category;
            }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.OnPropertyChanged("Category");
                }
            }
        }

        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                if (this.categoryName != value)
                {
                    this.categoryName = value;
                    this.OnPropertyChanged("CategoryName");
                }
            }
        }
        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (this.priority != value)
                {
                    this.priority = value;
                    this.OnPropertyChanged("Priority");
                }
            }
        }

        public string PriorityName
        {
            get
            {
                return this.priorityName;
            }
            set
            {
                if (this.priorityName != value)
                {
                    this.priorityName = value;
                    this.OnPropertyChanged("PriorityName");
                }
            }
        }
        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    this.OnPropertyChanged("Status");
                }
            }
        }

        public string StatusName
        {
            get
            {
                return this.statusName;
            }
            set
            {
                if (this.statusName != value)
                {
                    this.statusName = value;
                    this.OnPropertyChanged("StatusName");
                }
            }
        }
        public int Client
        {
            get
            {
                return this.client;
            }
            set
            {
                if (this.client != value)
                {
                    this.client = value;
                    this.OnPropertyChanged("Client");
                }
            }
        }

        public string ClientName
        {
            get
            {
                return this.clientName;
            }
            set
            {
                if (this.clientName != value)
                {
                    this.clientName = value;
                    this.OnPropertyChanged("ClientName");
                }
            }
        }

        public int Contact
        {
            get
            {
                return this.contact;
            }
            set
            {
                if (this.contact != value)
                {
                    this.contact = value;
                    this.OnPropertyChanged("Contact");
                }
            }
        }
        public string ContactName
        {
            get
            {
                return this.contactName;
            }
            set
            {
                if (this.contactName != value)
                {
                    this.contactName = value;
                    this.OnPropertyChanged("ContactName");
                }
            }
        }
        public int Project
        {
            get
            {
                return this.project;
            }
            set
            {
                if (this.project != value)
                {
                    this.project = value;
                    this.OnPropertyChanged("Project");
                }
            }
        }
        public string ProjectName
        {
            get
            {
                return this.projectName;
            }
            set
            {
                if (this.projectName != value)
                {
                    this.projectName = value;
                    this.OnPropertyChanged("ProjectName");
                }
            }
        }

        public int Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                if (this.owner != value)
                {
                    this.owner = value;
                    this.OnPropertyChanged("Owner");
                }
            }
        }

        public string OwnerName
        {
            get
            {
                return this.ownerName;
            }
            set
            {
                if (this.ownerName != value)
                {
                    this.ownerName = value;
                    this.OnPropertyChanged("OwnerName");
                }
            }
        }
        public int Responsible
        {
            get
            {
                return this.responsible;
            }
            set
            {
                if (this.responsible != value)
                {
                    this.responsible = value;
                    this.OnPropertyChanged("Responsible");
                }
            }
        }
        public string ResponsibleName
        {
            get
            {
                return this.responsibleName;
            }
            set
            {
                if (this.responsibleName != value)
                {
                    this.responsibleName = value;
                    this.OnPropertyChanged("ResponsibleName");
                }
            }
        }

        public bool IsAllDay
        {
            get
            {
                return this.isAllDay;
            }
            set
            {
                if (this.isAllDay != value)
                {
                    this.isAllDay = value;
                    this.OnPropertyChanged("IsAllDay");
                }
            }
        }

        public bool IsReminder
        {
            get
            {
                return this.isReminder;
            }
            set
            {
                if (this.isReminder != value)
                {
                    this.isReminder = value;
                    this.OnPropertyChanged("IsReminder");
                }
            }
        }



        public string Background
        {
            get
            {
                return this.background;
            }
            set
            {
                if (this.background != value)
                {
                    this.background = value;
                    this.OnPropertyChanged("Background");
                }
            }
        }

        public string ShowTime
        {
            get
            {
                return this.showtime;
            }
            set
            {
                if (this.showtime != value)
                {
                    this.showtime = value;
                    this.OnPropertyChanged("ShowTime");
                }
            }
        }
        // jelena
        public int IdArrangement
        {
            get
            {
                return this.idArrangement;
            }
            set
            {
                if (this.idArrangement != value)
                {
                    this.idArrangement = value;
                    this.OnPropertyChanged("IdArrangement");
                }
            }
        }

        public string NameArrangement
        {
            get
            {
                return this.nameArrangement;
            }
            set
            {
                if (this.nameArrangement != value)
                {
                    this.nameArrangement = value;
                    this.OnPropertyChanged("NameArrangement");
                }
            }
        }

        //end jelena
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }    
}
