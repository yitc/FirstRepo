using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class ContactReasonDAO
    {
        private dbConnection conn;
        public ContactReasonDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllContactReason(string idLang)
        {
            string query = string.Format(@"SELECT idContactReason,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE descContactReason END as descContactReason FROM ContactReason cr
            LEFT OUTER JOIN STRING"+idLang+" s On s.stringKey = cr.descContactReason");


            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            //sqlParameters[0].Value = idEmployee;
            return conn.executeSelectQuery(query, null);

        }
    }
}