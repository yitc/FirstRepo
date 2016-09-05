using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;

namespace BIS.DAO
{
    public class TypesEmailDAO
    {
        private dbConnection conn;
        public TypesEmailDAO()
        {
            conn = new dbConnection();

        }
        public DataTable GetAllTypesEmail(string idLang)
        {

            string query = string.Format(@"SELECT idEmailType,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameEmailType END as nameEmailType  FROM TypesEmail te
                LEFT OUTER JOIN STRING"+idLang+" s On s.stringKey = te.nameEmailType ");

            return conn.executeSelectQuery(query, null);

        }
    }
}