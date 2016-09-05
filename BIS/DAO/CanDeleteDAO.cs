using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.Core;
using System.Data;
using System.Data.SqlClient;

namespace BIS.DAO
{
    public class CanDeleteDAO
    {
         private dbConnection conn;

         public CanDeleteDAO()
        {
            conn = new dbConnection();
        }


         public DataTable canDelete(string nameTable, string nameParametar, string valueParametar)
         {
             string query = string.Format(@"SELECT  " + nameParametar + " FROM " + nameTable + "  WHERE " + nameParametar + " = @" + nameParametar + "");

             SqlParameter[] sqlParameters = new SqlParameter[1];
             sqlParameters[0] = new SqlParameter("@" + nameParametar + "", SqlDbType.NVarChar);
             sqlParameters[0].Value = valueParametar;
             return conn.executeSelectQuery(query, sqlParameters);
         }
    }
}
