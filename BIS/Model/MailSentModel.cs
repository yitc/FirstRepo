using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class MailSentModel : IModel
    {

        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Entry Id")]
        public string entryId { get; set; }

        [DisplayName("User Id")]
        public int idUser { get; set; }

        [DisplayName("Mail Subject")]
        public string Subject { get; set; }

        [DisplayName("To")]
        public int? idPersonTo { get; set; }

        [DisplayName("To")]
        public int? idClientTo { get; set; }

        [DisplayName("Local Copy")]
        public string locationOnDisk { get; set; }

        [DisplayName("Date sent")]
        public DateTime? dtSent { get; set; }

        public MailSentModel()
        {
            this.id = 0;
            this.entryId = String.Empty;
            this.idUser = 0;
            this.Subject = String.Empty;
            this.idPersonTo = 0;
            this.idClientTo = 0;
            this.locationOnDisk = String.Empty;
            this.dtSent = DateTime.Now;
        }
    }    
}
