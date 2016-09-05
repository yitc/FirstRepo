using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class ClientNotesModel
    {
        [DisplayName("ID note")]
        public int idNote { get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("ID employee")]
        public int? idEmployee { get; set; }

        [DisplayName("Note creation date")]
        public DateTime dtNoteDate { get; set; }

        [DisplayName("Note")]
        public string noteText { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtCreated { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtModified { get; set; }

        [DisplayName("Modified")]
        public int? idUserModified { get; set; }

        [DisplayName("ID Creator")]
        public int idUserCreated { get; set; }

        [DisplayName("Creator")]
        public string nameUserCreated { get; set; }

        [DisplayName("ID type note")]
        public int? idTypeNote { get; set; }

        [DisplayName("Type")]
        public string nameType { get; set; }



    }
}