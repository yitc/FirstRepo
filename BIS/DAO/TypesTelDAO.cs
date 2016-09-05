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
    public class TypesTelDAO
    {
        private dbConnection conn;
        public TypesTelDAO()
        {
            conn = new dbConnection();

        }
        public DataTable GetAllTypeTel(string idLang)
        {

            string query = string.Format(@"SELECT idTelType,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameTelType END AS nameTelType FROM TypesTel tt LEFT OUTER JOIN STRING" + idLang + " s On s.stringKey = tt.nameTelType  ");
            
            return conn.executeSelectQuery(query, null);

        }

    }
}