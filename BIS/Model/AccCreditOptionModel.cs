using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccCreditOptionModel : IModel
    {
        [DisplayName("Id Option")]
        public int idOption { get; set; }

        [DisplayName("Description")]
        public string descriptionOption { get; set; }
    }
}