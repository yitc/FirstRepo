using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementBookStatusModel : IModel
    {
        [DisplayName("ID status")]
        public int idStatus { get; set; }

        [DisplayName("Status")]
        public string nameStatus { get; set; }


        public ArrangementBookStatusModel()
        {
            this.idStatus = 0;
            this.nameStatus = String.Empty;
        }
    }
}
