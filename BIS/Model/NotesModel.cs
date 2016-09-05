using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class NotesModel 
    {
        [DisplayName("ID note")]
        public int idNote { get; set; }

        [DisplayName("ID Person")]
        public int idContPers { get; set; }

        [DisplayName("Person")]
        public string nameContPers { get; set; }

        [DisplayName("ID Employee")]
        public int? idEmployee { get; set; }

        [DisplayName("Employee")]
        public string nameEmployee { get; set; }

        [DisplayName("Note date")]
        public DateTime dtNoteDate { get; set; }

        [DisplayName("Note")]
        public string noteText { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtCreated { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtModified { get; set; }

        [DisplayName("ID Creator user")]
        public int? idUserModified { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserCreated { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("ID type")]
        public int? idTypeNote { get; set; }

        [DisplayName("Type")]
        public string nameType { get; set; }



    }
    public class importMedicalModel
    {
        public int customerId { get; set; }
        public int preferenceId { get; set; }
        public bool selected { get; set; }
        public string preference { get; set; }
        public bool txt { get; set; }
        public string preferenceText { get; set; }
    }
}