using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MultimediaServersModel : IModel
    {

        [DisplayName("ID")]
        public int idServer { get; set; }

        [DisplayName("Path")]
        public string path { get; set; }

        [DisplayName("Folder")]
        public string folder{ get; set; }

        [DisplayName("Username")]
        public string username { get; set; }

        [DisplayName("Password")]
        public string password { get; set; }

       
    }

   
    }

