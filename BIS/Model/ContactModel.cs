using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using BIS.DAO;

namespace BIS.Model
{
    public class ContactsModel
    {
        [DisplayName("Contact Id")]
        public int idContact { get; set; }

        [DisplayName("Reason Id")]
        public int idContactReason { get; set; }

        [DisplayName("Reason")]
        public string reasonContact { get; set; }

        [DisplayName("Contact type")]
        public int? idContactType { get; set; }

        [DisplayName("Date")]
        public DateTime dateContact { get; set; }

        [DisplayName("Open time")]
        public TimeSpan? openTimeContact { get; set; }

        [DisplayName("Close time")]
        public TimeSpan? closeTimeCOntact { get; set; }

        [DisplayName("Duration")]
        public TimeSpan? durationContact { get; set; }

        [DisplayName("ID Creator user")]
        public int idCreator { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("ID client")]
        public int? idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("ID person")]
        public int? idContPers { get; set; }

        [DisplayName("Person")]
        public string nameContPers { get; set; }

        [DisplayName("ID project")]
        public int? idProject { get; set; }

        [DisplayName("Project")]
        public string nameProject { get; set; }

        [DisplayName("Note")]
        public string noteContact { get; set; }

        // opisna polja

        [DisplayName("Reason description")]
        public string descContactReason { get; set; }

        [DisplayName("Type description")]
        public string descContactType { get; set; }

        //jelena
        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Name arrangement")]
        public string nameArrangement { get; set; }
        //end jelena

        public ContactsModel()
        {
            this.idContact = 0;
            this.idContactReason = 0;
            this.reasonContact = String.Empty;
            this.idContactType = 0;
            this.dateContact = DateTime.Now;
            this.openTimeContact = TimeSpan.Zero;
            this.closeTimeCOntact = TimeSpan.Zero;
            this.durationContact = TimeSpan.Zero;
            this.idCreator = 0;
            this.nameUserCreated = String.Empty;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idContPers = 0;
            this.nameContPers = String.Empty;
            this.idProject = 0;
            this.nameProject = String.Empty;
            this.noteContact = String.Empty;
            this.descContactReason = String.Empty;
            this.descContactType = String.Empty;
            this.idArrangement = 0;
            this.nameArrangement = String.Empty;
        }
    }
    
}