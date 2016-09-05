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
    class ClientPersonDAO
    {
        private dbConnection conn;
        public ClientPersonDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllClientPerson()
        {
            string query = string.Format(@"SELECT idCliPer, idCLient, idContPerson, idFunction FROM ClientPerson");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetClientPersonById(int idCliPer)
        {
            string query = string.Format(@"SELECT idCliPer, idCLient, idContPerson, idFunction FROM ClientPerson WHERE idCliPer = @idCLient ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCliPer", SqlDbType.Int);
            sqlParameters[0].Value = idCliPer;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetContactPersonByClient(int idClient)
        {
            string query = string.Format(@"SELECT ClientPerson.idCliPer,idContPers,ContactPerson.firstname,ContactPerson.lastname,
                                           ClientPerson.idFunction,ContactPersonFunction.nameFunction
                                            FROM ClientPerson
                                            LEFT OUTER JOIN ContactPerson ON  ClientPerson.idCliPer=ContactPerson.idContPers
                                            LEFT OUTER JOIN ContactPersonFunction on ClientPerson.idFunction=ContactPersonFunction.idFunction
                                            WHERE ClientPerson.idCLient=@idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idClient",SqlDbType.Int);
            sqlParameters[0].Value = idClient;

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetClientById(int idClient )
        {
            string query = string.Format(@"SELECT Client.idClient,ClientPerson.idCliPer, Client.nameClient,Client.cityClient
                                          FROM Client
                                          LEFT OUTER JOIN ClientPerson ON ClientPerson.idCliPer = Client.idClient 
                                          WHERE ClientPerson.idClient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;

            return conn.executeSelectQuery(query, null);
        } 
      
        public DataTable GetAllClientsFromPerson(int idContPerson) // za formu Person
        {
            string query = string.Format(@"SELECT  cp.idCliPer,cp.idCLient,cp.idContPerson,cp.idFunction,c.nameClient,cpf.nameFunction
                                            FROM ClientPerson as cp
                                            LEFT JOIN Client as c ON c.idClient=cp.idCLient
                                            LEFT JOIN ContactPersonFunction as cpf ON cpf.idFunction=cp.idFunction
                                            WHERE cp.idContPerson = @idContPerson ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameters[0].Value = idContPerson;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllPersonsFromClient(int idClient) // za formu Client
        {
            string query = string.Format(@"SELECT  cp.idCliPer,cp.idCLient,cp.idContPerson,cp.idFunction,
            c.nameClient,cpf.nameFunction,conper.firstname,conper.lastname,conper.midname
                                            FROM ClientPerson as cp
                                            LEFT OUTER JOIN Client as c ON c.idClient=cp.idCLient
                                            LEFT OUTER JOIN ContactPersonFunction as cpf ON cpf.idFunction = cp.idFunction
                                            LEFT OUTER JOIN ContactPerson as conper ON conper.idContPers = cp.idContPerson
                                            WHERE cp.idCLient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;

            return conn.executeSelectQuery(query, sqlParameters);
        }



        public int SaveAndReturnID(ClientPersonModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ClientPerson (idClient, idContPerson, idFunction)
                                        VALUES (@idCLient, @idContPerson, @idFunction); SELECT SCOPE_IDENTITY() ");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idCLient", SqlDbType.Int);
            sqlParameter[0].Value = model.idCLient;

            sqlParameter[1] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameter[1].Value = model.idContPerson;

            sqlParameter[2] = new SqlParameter("@idFunction", SqlDbType.Int);
            sqlParameter[2].Value = model.idFunction;

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
            sqlParameter[4].Value = conn.GetLastTableID("ClientPerson") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCliPer";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientPerson";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save and return id";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
        }

        public Boolean Update(ClientPersonModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ClientPerson SET  idClient = @idClient, idContPerson = @idContPerson, 
                                            idFunction = @idFunction
                                            WHERE idCliPer = @idCliPer ");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idCliPer", SqlDbType.Int);
            sqlParameter[0].Value = model.idCliPer;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = model.idCLient;

            sqlParameter[2] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameter[2].Value = model.idContPerson;

            sqlParameter[3] = new SqlParameter("@idFunction", SqlDbType.Int);
            sqlParameter[3].Value = model.idFunction;

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
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.idCliPer.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCliPer";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientPerson";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idCliPer, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ClientPerson WHERE idCliPer = @idCliPer");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idCliPer", SqlDbType.Int);
            sqlParameter[0].Value = idCliPer;

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
            sqlParameter[4].Value = idCliPer;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCliPer";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientPerson";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}
