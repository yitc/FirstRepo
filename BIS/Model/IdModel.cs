using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class IdModel : IModel
    {
        [DisplayName("ID")]
        public int idTab { get; set; }

        [DisplayName("Year")]
        public string yearId { get; set; }

        [DisplayName("IDVerkoop")]
        public int idDaily { get; set; }

        [DisplayName("IDVerkoop")]
        public int idNumber { get; set; }
    }

}
