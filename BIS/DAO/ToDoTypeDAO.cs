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
    public class ToDoTypeDAO
    {
        private dbConnection conn;
        public ToDoTypeDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllToDoTypes(string idLang)
        {
            string query = string.Format(@"SELECT idToDoType,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE descriptionToDoType END AS descriptionToDoType FROM ToDoType 
                                            LEFT OUTER JOIN STRING"+idLang+" s ON s.stringKey =  descriptionToDoType");


            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            //sqlParameters[0].Value = idEmployee;
            return conn.executeSelectQuery(query, null);

        }
    }
}
