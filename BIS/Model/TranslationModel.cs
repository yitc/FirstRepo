using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class TranslationModel :IModel
    {
        public int idLang { get; set; }

        //public int idForm { get; set; }

        [DisplayName("Sentance for translation")]
        public string stringKey { get; set; }

        [DisplayName("Translated sentance")]
        public string stringValue { get; set; }

        [DisplayName("Date of translation")]
        public DateTime dtString { get; set; }

    }
}