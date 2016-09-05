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
    public class ToDoStatusDAO
    {
        private dbConnection conn;
        public ToDoStatusDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllToDoStatus()
        {
            string query = string.Format(@"SELECT idStatusToDo,descriptionStatus FROM ToDoStatus ");


            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            //sqlParameters[0].Value = idEmployee;
            return conn.executeSelectQuery(query, null);

        }
    }
}
