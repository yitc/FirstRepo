using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class DocumentTypeModel : IModel
    {
        [DisplayName("ID document type")]
        public int idDocumentType { get; set; }

        [DisplayName("Document type")]
        public string typeDocument { get; set; }

        [DisplayName("Name document type")]
        public string nameDocumentType { get; set; }

        [DisplayName("Extend document type")]
        public string extendDocumentType { get; set; }

        [DisplayName("Has layout")]
        public bool haveLayout { get; set; }

        [DisplayName("Table document type")]
        public string tableDocumentType { get; set; }

        [DisplayName("Creation date")]
        public DateTime? dtCreted { get; set; }

        [DisplayName("Modification date")]
        public DateTime? dtModified { get; set; }

        [DisplayName("Default bookmark")]
        public string defaultBookmark { get; set; }

       [DisplayName("ID Modified user")]
        public int? idModifiedUser { get; set; }

        [DisplayName("ID Creator user")]
        public int? idCreatedUser { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }
    }
}