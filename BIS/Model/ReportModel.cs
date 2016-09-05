using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ReportModel
    {

        [DisplayName("Checked")]
        public Boolean isChecked { get; set; }

        [DisplayName("ID column")]
        public string idColumn { get; set; }

        [DisplayName("Column")]
        public string nameColumn { get; set; }

        [DisplayName("Width")]
        public decimal widthColumn { get; set; }

        public ReportModel()
        {
            this.isChecked = true;
            this.idColumn = string.Empty;
            this.nameColumn = string.Empty;
            this.widthColumn = 0;
        }

    }

    public class ReportLayoutModel
    {

        
        [DisplayName("ID layout")]
        public int idLayout { get; set; }

        [DisplayName("Layout")]
        public string nameLayout { get; set; }


        public ReportLayoutModel()
        {
            this.idLayout = 0;
            this.nameLayout = string.Empty;
        }

    }
}
