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
    public class TitleDAO
    {
        private dbConnection conn;
        public TitleDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllTitle()
        {
            string query = string.Format(@"SELECT idTitle,nameTitle
              FROM Title");
               // WHERE idContPers = '" + idPerson.ToString() + "' 

            return conn.executeSelectQuery(query, null);

        }

    }
}