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
    public class ArrangementStatusDAO
    {
        private dbConnection conn;
        public ArrangementStatusDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllArrangementStatus()
        {
            string query = string.Format(@"SELECT idArrangementStatus, nameArrangementStatus FROM ArrangementStatus");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementStatusByID(int idArrangementStatus)
        {
            string query = string.Format(@"SELECT idArrangementStatus, nameArrangementStatus 
                                           FROM ArrangementStatus 
                                            WHERE idArrangementStatus = @idArrangementStatus");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangementStatus", SqlDbType.Int);
            sqlParameters[0].Value = idArrangementStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }

       
    }
}