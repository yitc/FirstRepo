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
    public class TrainingDAO
    {
        private dbConnection conn;
        public TrainingDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllTraining()
        {
            string query = string.Format(@"SELECT idTraining, codeTraining, nameTraining FROM Training ");

            return conn.executeSelectQuery(query, null);
        }

    }
}