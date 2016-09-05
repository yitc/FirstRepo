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
    public class ArrNrDAO
    {
        private dbConnection conn;
        public ArrNrDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllArrNr()
        {
            string query = string.Format(@"SELECT idTbl, nrArrFak FROM ArrNr ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrNrByID(string idTbls)
        {
            string query = string.Format(@"SELECT idTbl, nrArrFak FROM ArrNr 
                                           WHERE nrArrFak=@idTbl");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idTbl", SqlDbType.Int);
            sqlParameters[0].Value = idTbls;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoice()
        {
            string query = string.Format(@" UPDATE ArrNr SET nrArrFak = nrArrFak + 1");

            conn.executeUpdateQuery(query, null);
            // conn.executeSelectQuery(query, null);
            string query1 = string.Format(@" SELECT * from ArrNr ");
            return conn.executeSelectQuery(query1, null);
        }
        public DataTable GetSepa()
        {
            string query = string.Format(@" UPDATE ArrNr SET nrSEPA = nrSEPA + 1");

            conn.executeUpdateQuery(query, null);
            // conn.executeSelectQuery(query, null);
            string query1 = string.Format(@" SELECT * from ArrNr ");
            return conn.executeSelectQuery(query1, null);
        }
        public DataTable GetSepaNoIncrement()
        {
            string query1 = string.Format(@" SELECT * from ArrNr ");
            return conn.executeSelectQuery(query1, null);
        }
       
      
    }
}