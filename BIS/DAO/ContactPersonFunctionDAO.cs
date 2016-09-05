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
    public class ContactPersonFunctionDAO
    {
        private dbConnection conn;
        public ContactPersonFunctionDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllContactPersonFunction(string idLang)
        {
            //string query = string.Format(@"SELECT idFunction , nameFunction FROM ContactPersonFunction");

            string query = string.Format(@"SELECT idFunction,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameFunction END AS nameFunction
                FROM ContactPersonFunction 
                LEFT OUTER JOIN STRING" + idLang + " s ON s.stringKey = nameFunction");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetContactPersonFunctionById(int idFunction)
        {
            string query = string.Format(@"SELECT idFunction, nameFunction FROM ContactPersonFunction
                                        WHERE nameFunction=@idFunction");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idFunction", SqlDbType.Int);
            sqlParameters[0].Value = idFunction;

            return conn.executeSelectQuery(query, null);
        }

      
        
    }
}