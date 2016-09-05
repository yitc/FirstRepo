using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model 
{
    public class PathsModel 
    {
        [DisplayName("ID Path")]
        public int idPath { get; set; }

        [DisplayName("Type")]
        public string typePath { get; set; }

        [DisplayName("Name Path")]
        public string namePath { get; set; }

        [DisplayName("Path")]
        public string path { get; set; }


        public PathsModel()
        {
            this.idPath = 0;
            this.typePath = String.Empty;
            this.namePath = String.Empty;
            this.path = String.Empty;
        }
    }
}