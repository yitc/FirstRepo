using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class DocumentsModel 
    {
        public bool selected { get; set; }
        [DisplayName("Document Id")]
        public int idDocument { get; set; }

        [DisplayName("Document type")]
        public string typeDocument { get; set; }

        [DisplayName("Description")]
        public string descriptionDocument { get; set; }

        [DisplayName("Document file")]
        public string fileDocument { get; set; }

        [DisplayName("ID Our/Incoming")]
        public decimal inOutDocument { get; set; }

        [DisplayName("Our/Incoming")]
        public string nameInOutDocuments { get; set; }

        [DisplayName("ID project")]
        public int idProject { get; set; }

        [DisplayName("Project")]
        public string nameProject { get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("ID person")]
        public int? idContPers { get; set; }

        [DisplayName("Person")]
        public string namePerson { get; set; }

        [DisplayName("ID Employee")]
        public int idEmployee { get; set; }

        [DisplayName("Employee")]
        public string nameEmployee { get; set; }

        [DisplayName("ID Responsible")]
        public int idResponsableEmployee { get; set; }

        [DisplayName("Responsible")]
        public string nameEmployeeResponsible { get; set; }

        [DisplayName("ID Status")]
        public int idDocumentStatus { get; set; }

        [DisplayName("Status")]
        public string nameStatus { get; set; }

        [DisplayName("Document note")]
        public string noteDocument { get; set; }

        [DisplayName("Layout")]
        public int idLayout { get; set; }

        [DisplayName("ID Creator user")]
        public int? userCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int userModified { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtModified { get; set; }

        // jelena
        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("Name arrangement")]
        public string nameArrangement { get; set; }
        // end jelena

        public DocumentsModel()
        {
            this.selected = false;
        }
    }
}
