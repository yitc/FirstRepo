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
    public class PersonReasonOutDAO
    {

        private dbConnection conn;
        public PersonReasonOutDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllReasonOut()
        {
            string query = string.Format(@"SELECT idReasonOut, nameReasonOut FROM ContactPersonReasonOut ");

            return conn.executeSelectQuery(query, null);

        }

    }
}