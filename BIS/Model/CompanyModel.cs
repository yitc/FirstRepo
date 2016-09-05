using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class CompanyModel
    {
        public int idCompany { get; set; }

        public string nameCompany { get; set; }

        public string yearCompany { get; set; }

        public string dbPathCompany { get; set; }

        public string addressCompany { get; set; }

        public string cityCompany { get; set; }

        public int? idCountry { get; set; }

        public string emailCompany { get; set; }

        public string logoCompany { get; set; }

        public string iconCompany { get; set; }

        public string loginScreenCompany { get; set; }

        public string flag { get; set; }
    }
}
