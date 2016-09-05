using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class CertificatesModel :IModel
    {
        [DisplayName ("Id Certificate")]
        public int idCertificate { get; set; }

        [DisplayName ("Code Certificate")]
        public string codeCertificate { get; set; }

        [DisplayName ("Name Certificate")]
        public string nameCertificate { get; set; }

    }
}