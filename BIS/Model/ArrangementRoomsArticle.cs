using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementRoomsArticle : IModel
    {

        [DisplayName("ID rooms")]
        public string idRoom { get; set; }

        [DisplayName("Code article")]
        public string idArticle { get; set; }

        [DisplayName("Article name")]
        public string nameArticle { get; set; }

        [DisplayName("Contract")]
        public Boolean isContract { get; set; }

        [DisplayName("ID")]
        public int id{ get; set; }

        [DisplayName("ID person")]
        public int idContPers { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Gender")]
        public string nameGender { get; set; }

        [DisplayName("Type")]
        public string type { get; set; }

        public ArrangementRoomsArticle()
        {
            this.idRoom = String.Empty;
            this.idArticle = String.Empty;
            this.nameArticle = String.Empty;            
            this.isContract = false;
            this.id = 0;
            this.idContPers = 0;
            this.name = String.Empty;
            this.nameGender = String.Empty;
            this.type = String.Empty;
        }
    }
}
