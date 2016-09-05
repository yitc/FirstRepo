using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccDailyTypeModel : IModel
    {
        [DisplayName("Daily type")]
        public int idDailyType { get; set; }

        [DisplayName("Daily type name")]
        public string descDailyType { get; set; }
    }
}