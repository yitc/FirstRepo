using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;

namespace BIS.DAO
{
    public class CertificatesDAO
    {
        private dbConnection conn;
        public CertificatesDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllCertificates()
        {
            string query = string.Format(@"SELECT idCertificate, codeCertificate, nameCertificate FROM Certificates");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetCertificatesByID(string idCertificate)
        {
            string query = string.Format(@"SELECT idCertificate, codeCertificate, nameCertificate FROM Certificates WHERE codeCertificate = @idCertificate");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCertificate", SqlDbType.Int);
            sqlParameters[0].Value = idCertificate;

            return conn.executeSelectQuery(query, sqlParameters);
        }

       
    }
}