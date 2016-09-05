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
    public class InvoiceStatusDAO
    {

        private dbConnection conn;
        public InvoiceStatusDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllInvoiceStatus()
        {
            string query = string.Format(@"SELECT idInvoiceStatus, descInvoiceStatus FROM InvoiceStatus ");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GeInvoiceStatus(int id)
        {
            string query = string.Format(@"SELECT idInvoiceStatus, descInvoiceStatus FROM InvoiceStatus WHERE idInvoiceStatus = @idInvoiceStatus");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoiceStatus", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetInvoiceStatusByID(string idInvoiceStatus)
        {
            string query = string.Format(@"SELECT idInvoiceStatus, descInvoiceStatus FROM InvoiceStatus
                                          WHERE descInvoiceStatus = @idInvoiceStatus ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoiceStatus", SqlDbType.Int);
            sqlParameters[0].Value = idInvoiceStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(InvoiceStatusModel model)
        {
            string query = string.Format(@"INSERT INTO InvoiceStatus (descInvoiceStatus)
                                            VALUES (@descInvoiceStatus)");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@descInvoiceStatus",SqlDbType.NVarChar);
            sqlParameters[0].Value = model.descInvoiceStatus;

            return conn.executeInsertQuery(query, sqlParameters);
        }

        public Boolean Update(InvoiceStatusModel model)
        {
            string query = string.Format(@"UPDATE InvoiceStatus SET descInvoiceStatus = @descInvoiceStatus
                                            WHERE idInvoiceStatus = @idInvoiceStatus");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idInvoiceStatus", SqlDbType.Int);
            sqlParameters[0].Value = model.idInvoiceStatus;

            sqlParameters[1] = new SqlParameter("descInvoiceStatus", SqlDbType.NVarChar);
            sqlParameters[1].Value = model.descInvoiceStatus;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean Delete(int idInvoiceStatus)
        {
            string query = string.Format(@"DELETE InvoiceStatus WHERE idInvoiceStatus=@idInvoiceStatus");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoiceStatus", SqlDbType.Int);
            sqlParameters[0].Value = idInvoiceStatus;

            return conn.executeDeleteQuery(query, sqlParameters);
        }
    }
}