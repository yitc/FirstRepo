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
    public class ContactTypeDAO
    {
        private dbConnection conn;
        public ContactTypeDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllContactType(string idLang)
        {
            string query = string.Format(@"SELECT idContactType,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE descContactType END AS descContactType  FROM ContactType ct
            LEFT OUTER JOIN STRING" + idLang + " s On s.stringKey = ct.descContactType ");


            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            //sqlParameters[0].Value = idEmployee;
            return conn.executeSelectQuery(query, null);

        }
    }
}