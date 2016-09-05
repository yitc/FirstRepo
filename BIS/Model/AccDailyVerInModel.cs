using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class AccDailyVerInModel : IModel
    {
        [DisplayName("Daily class")]
        public int idDailyVerIn { get; set; }

        [DisplayName("Daily class name")]
        public string nameDailyVerIn { get; set; }
    }
}