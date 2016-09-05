using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementRoomsModel : IModel
    {
        [DisplayName("Id arrangement")]
        public int idArrangement { get; set; }

        [DisplayName("IdRoom")]
        public string idRoom { get; set; }

        [DisplayName("ID Artical")]
        public string idArticle { get; set; }

        [DisplayName("Is contract")]
        public bool isContract { get; set; }

        [DisplayName("Id")]
        public int id { get; set; }

        public ArrangementRoomsModel()
        {
            this.idArrangement = 0;
            this.idRoom = String.Empty;
            this.idArticle = String.Empty;
            this.isContract = false;
            this.id = 0;
        }
    }
    

}
