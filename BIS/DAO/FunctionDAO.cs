using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.DAO
{
    public class FunctionDAO
    {

        private dbConnection conn;
        public FunctionDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetFunctions(string idLang)
        {
            string query = string.Format(@"SELECT idFunction,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameFunction END AS nameFunction
                FROM EmployeeFunction 
                LEFT OUTER JOIN STRING"+idLang+" s ON s.stringKey = nameFunction");

            return conn.executeSelectQuery(query, null);

        }
    }
}