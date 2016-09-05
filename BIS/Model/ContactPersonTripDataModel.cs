using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BIS.Model
{

    public class ContactPersonTripDataModel : IModel
    {        
        public int idContPersTravel { get; set; }

        public string descriptionTripSort { get; set; }

        public int idContactPerson { get; set; }

        public DateTime? dtFrom { get; set; }

        public DateTime? dtTo { get; set; }

        
        public int? idTargetGroup { get; set; }

        public bool op1 { get; set; }

        public bool op2 { get; set; }

        public bool op3 { get; set; }

        public string shortcutTargeGroup { get; set; }

        public string nameTargetGroup { get; set; }

        public string helpP { get; set; }

    }
}