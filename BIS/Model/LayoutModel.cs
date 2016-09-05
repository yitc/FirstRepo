using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LayoutModel : IModel
    {
        public int idLayout { get; set; }

        public string nameLayout { get; set; }

        public string typeDocument { get; set; }

        public string languageLayout { get; set; }

        public string fileLayout { get; set; }

        public string bookmark { get; set; }

        public int? userCreated { get; set; }

        public DateTime? dtCreted { get; set; }

        public int? userModified { get; set; }

        public DateTime? dtModified { get; set; }
    }
}