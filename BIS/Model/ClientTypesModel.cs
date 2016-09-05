using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ClientTypesModel
    {
        [DisplayName("ID client type")]
        public int idTypeClient { get; set; }

        [DisplayName("Client type")]
        public string nameTypeClient { get; set; }
    }
}
