using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class FunctionModel
    {
        [DisplayName("ID function")]
        public int idFunction { get; set; }

        [DisplayName("Function")]
        public string nameFunction { get; set; }

    }
}