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
    public class AccIbanDAO
    {
        private dbConnection conn;

        public AccIbanDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetIBANForPerson(int idContPers)
        {
            string query = string.Format(@"SELECT Id, accNumber,idClient,idContPers,ibanNumber FROM AccIban WHERE idContPers = @idContPers");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetIBANForClient(int idClient)
        {
            string query = string.Format(@"SELECT Id, accNumber,idClient,idContPers,ibanNumber FROM AccIban WHERE idClient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetIBANForClientString(string accNumber)
        {
            string query = string.Format(@"SELECT Id, accNumber,idClient,idContPers,ibanNumber FROM AccIban WHERE accNumber = @accNumber");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameters[0].Value = accNumber;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable CheckIbanForClient(string iban, int idClient)
        {

            string query = string.Format(@"SELECT ibanNumber FROM AccIban WHERE idClient = @idClient AND ibanNumber=@ibanNumber");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@ibanNumber", SqlDbType.NVarChar);
            sqlParameters[0].Value = iban;

            sqlParameters[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[1].Value = idClient;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable CheckIbanForPerson(string iban, int idContPers)
        {

            string query = string.Format(@"SELECT ibanNumber FROM AccIban WHERE idContPers = @idContPers AND ibanNumber=@ibanNumber");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@ibanNumber", SqlDbType.NVarChar);
            sqlParameters[0].Value = iban;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public int Save(AccIbanModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccIban (accNumber,idClient,idContPers,ibanNumber) 
                      VALUES(@accNumber,@idClient,@idContPers,@ibanNumber); SELECT SCOPE_IDENTITY()");


            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.accNumber;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = (model.idClient == 0) ? SqlInt32.Null : model.idClient; 

            sqlParameter[2] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[2].Value = (model.idContPers == 0) ? SqlInt32.Null : model.idContPers; 

            sqlParameter[3] = new SqlParameter("@ibanNumber", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.ibanNumber;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = conn.GetLastTableID("AccIban");

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccIban";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccIban";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

 
        public Boolean Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" DELETE FROM AccIban  WHERE id = @id ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[0].Value = id;


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = id.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccIban";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccIban";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
