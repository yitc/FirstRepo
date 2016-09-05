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
    public class PersonReasonInDAO
    {

        private dbConnection conn;
        public PersonReasonInDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllReasonIn()
        {
            string query = string.Format(@"SELECT idReasonIn, nameReasonIn FROM ContactPersonReasonIn ");

            return conn.executeSelectQuery(query, null);

        }

       
    }
}