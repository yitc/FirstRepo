using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class TitleModel
    {
        [DisplayName("ID title")]
        public int idTitle { get; set; }

        [DisplayName("Title")]
        public string nameTitle { get; set; }
    }
}