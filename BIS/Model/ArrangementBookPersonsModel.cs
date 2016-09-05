using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementBookPersonsModel : IModel
    {
        [DisplayName("ID arrangement booking")]
        public int idArrangementBook { get; set; }

        [DisplayName("Status")]
        public int idContPers { get; set; }

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        public ArrangementBookPersonsModel()
        {
            this.idArrangementBook = 0;
            this.idContPers = 0;
            this.idUserCreated = 0;
            this.idUserModified = 0;
            this.dtUserCreated = DateTime.Now;
            this.dtUserModified = DateTime.Now;
        }
    }
}
