using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LanguagesModel
    {
        [DisplayName("ID language")]
        public int idLang { get; set; }

        [DisplayName("Language")]
        public string nameLang { get; set; }
    }
}
