using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TargetGroupModel : IModel
    {
        [DisplayName("ID target gorup")]
        public int idTargetGroup { get; set; }

        [DisplayName("Shortcut target group")]
        public string shortcutTargeGroup { get; set; }

        [DisplayName("Boarding point")]
        public string nameTargetGroup { get; set; }


        public TargetGroupModel()
        {
            this.idTargetGroup = 0;
            this.shortcutTargeGroup = String.Empty;
            this.nameTargetGroup = String.Empty;
        }
    }
}
