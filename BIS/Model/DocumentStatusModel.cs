using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class DocumentStatusModel : IModel
    {

        [DisplayName("ID Status")]
        public int idDocumentStatus { get; set; }

        [DisplayName("Status value")]
        public int? valueStatus { get; set; }

        [DisplayName("Status")]
        public string descriptionStatus { get; set; }

    }
}