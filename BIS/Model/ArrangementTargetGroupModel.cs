using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementTargetGroupModel : IModel
    {
        [DisplayName("ID target group")]
        public int idTargetGroup { get; set; }

        [DisplayName("ID Arrangement")]
        public int idArrangement { get; set; }


        public ArrangementTargetGroupModel()
        {
            this.idTargetGroup = 0;
            this.idArrangement = 0;
        }
    }
}
