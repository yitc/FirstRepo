using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LanguageModel
    {
        private int _idLang;
        public int idLang
        {
            get { return _idLang; }
            set { _idLang = value; }
        }

        private int _idForm;
        public int idForm
        {
            get { return _idForm; }
            set { _idForm = value; }
        }

        private string _stringKey;
        public string stringKey
        {
            get { return _stringKey; }
            set { _stringKey = value; }
        }

        private string _stringValue;
        public string stringValue
        {
            get { return _stringValue; }
            set { _stringValue = value; }
        }
        private string _stringComment;

        public string stringComment
        {
            get { return _stringComment; }
            set { _stringComment = value; }
        }
    }
}
