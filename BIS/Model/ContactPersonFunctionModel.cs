using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ContactPersonFunctionModel
    {
        [DisplayName ("Id Function")]
        public int idFunction { get; set; }

        [DisplayName ("Name Function")]
        public string nameFunction { get; set; }

    }
}