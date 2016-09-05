using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VolFeaturesModel :IModel
    {
        [DisplayName("Id Features")]
        public int idFeatures { get; set; }

        [DisplayName("Id Contact Person")]
        public int idContPers { get; set; }

        [DisplayName("Code Sertificate")]
        public string codeCertificate { get; set; }

        [DisplayName("Code Training")]
        public string codeTraining { get; set; }

        [DisplayName("Expire Date")]
        public DateTime? expireDate { get; set; }

        [DisplayName("Archive Date")]
        public DateTime? archiveDate { get; set; }

        [DisplayName("Schedule Date")]
        public DateTime? scheduleDate { get; set; }

        //Training
        [DisplayName("Name Training")]
        public string nameTraining { get; set; }

        //Certificates
        [DisplayName("Name Certificate")]
        public string nameCertificate { get; set; }

    }
}