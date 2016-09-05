using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class ArrangementStatusModel :IModel
    {
        [DisplayName ("Id Arrangement status")]
        public int idArrangementStatus { get; set; }

        [DisplayName ("Name Arrangement status")]
        public string nameArrangementStatus { get; set; }

    }

}