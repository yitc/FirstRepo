using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class BookmarkModel : IModel
    {
        public int idbmk { get; set; }
        public string idstrtbl { get; set; }
        public string tbnamebmk { get; set; }
        public string tablebmk { get; set; }
        public string fieldbmk { get; set; }
        public string namebmk { get; set; }
        public string capbmk { get; set; }
        public string idstrbmk { get; set; }
        public bool isidbmk { get; set; }
        public bool isfidbmk { get; set; }
        public Nullable<decimal> reltypbmk { get; set; }
        public string ftablebmk { get; set; }
        public string idftbbmk { get; set; }
        public bool islytbmk { get; set; }
        public string aliasbmk { get; set; }
        public bool gblrepbmk { get; set; }
        public Nullable<System.DateTime> dtc { get; set; }
        public Nullable<System.DateTime> dtm { get; set; }
        public string mus { get; set; }
        public string cus { get; set; }
    }

    public class BookmarkSpecModel : IModel
    {
        public string table { get; set; }
        public string field { get; set; }
        public string value { get; set; }
    }

    public class SifEmailModel : IModel
    {
        public int ID { get; set; }
        public string dsc { get; set; }
    }

    public class SifTelModel : IModel
    {
        public int ID { get; set; }
        public string dsc { get; set; }
    }
}
