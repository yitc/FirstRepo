using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class InvoiceStatusModel
    {      
        
            [DisplayName("Id Invoice Status")]
            public int idInvoiceStatus { get; set; }

            [DisplayName("Desc Invoice Status")]
            public string descInvoiceStatus { get; set; }        

    }
}