using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using System.Data.SqlTypes;
using BIS.Model;

namespace BIS.DAO
{
    public class ClientDAO
    {
        private dbConnection conn;

        public ClientDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetClient(int idClient)
        {
            string query = string.Format(@"
                SELECT idClient, accountCodeClient, nameClient, contactPersonName, webClient, idTypeClient, userCreated,um.nameuser as nameUserCreated, dtCreated, userModified, um.nameuser as nameUserModified,dtModified, isActiveClient
                FROM Client c
                LEFT OUTER JOIN Users um ON um.idUser = c.userModified
                LEFT OUTER JOIN Users uc ON uc.idUser = c.userCreated
                WHERE idClient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetContactPersonName(int idClient)
        {
            string query = string.Format(@"
                SELECT contactPersonName 
                FROM Client                
                WHERE idClient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAllClients(string idLang)
        {
            string query = string.Format(
                @"SELECT idClient, accountCodeClient, nameClient, contactPersonName, webClient, c.idTypeClient, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE ct.nameTypeClient END AS nameTypeClient,um.nameuser as nameUserCreated, userCreated, dtCreated, um.nameuser as nameUserModified, userModified,dtModified, isActiveClient
                    FROM Client c
                    LEFT OUTER JOIN Users um ON um.idUser = c.userModified
                    LEFT OUTER JOIN Users uc ON uc.idUser = c.userCreated
                    LEFT OUTER JOIN ClientTypes ct ON c.idTypeClient = ct.idTypeClient
                    LEFT JOIN STRING" + idLang + " s ON ct.nameTypeClient=s.stringKey");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetClientDebitor(int side)
        {
            string query = string.Format(
                @"
                SELECT * FROM Client a
                LEFT OUTER JOIN AccDebCre b on a.idClient = b.idClient
                WHERE b.isdebitor = '" + side.ToString() + "' and b.idClient != 0 ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetClientCreditor(int side)
        {
            string query = string.Format(
                @"
                SELECT * FROM Client a
                LEFT OUTER JOIN AccDebCre b on a.idClient = b.idClient
                WHERE b.iscreditor = '" + side.ToString() + "' and b.idClient != 0 ");

            return conn.executeSelectQuery(query, null);
        }

        public int Save(ClientModel client, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(
                @"INSERT INTO Client(accountCodeClient, nameClient, contactPersonName, webClient, idTypeClient, userCreated, dtCreated, userModified, dtModified, isActiveClient) 
                  VALUES(@accountCodeClient, @nameClient,@contactPersonName, @webClient, @idTypeClient, @userCreated, @dtCreated, @userModified, @dtModified, @isActiveClient)
                ;SELECT SCOPE_IDENTITY();");

            SqlParameter[] sqlParameter = new SqlParameter[10];

            sqlParameter[0] = new SqlParameter("@accountCodeClient", SqlDbType.NVarChar);
            sqlParameter[0].Value = client.accountCodeClient;

            sqlParameter[1] = new SqlParameter("@nameClient", SqlDbType.NVarChar);
            sqlParameter[1].Value = client.nameClient;

            sqlParameter[2] = new SqlParameter("@contactPersonName", SqlDbType.NVarChar);
            sqlParameter[2].Value = client.contactPersonName;

            sqlParameter[3] = new SqlParameter("@webClient", SqlDbType.NVarChar);
            sqlParameter[3].Value = client.webClient;

            sqlParameter[4] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameter[4].Value = client.idTypeClient;

            sqlParameter[5] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[5].Value = client.userCreated;

            sqlParameter[6] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = (client.dtCreated == null || client.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : client.dtCreated;

            sqlParameter[7] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[7].Value = client.userModified;

            sqlParameter[8] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[8].Value = (client.dtModified == null || client.dtModified == DateTime.MinValue) ? SqlDateTime.Null : client.dtModified;

            sqlParameter[9] = new SqlParameter("@isActiveClient", SqlDbType.Bit);
            sqlParameter[9].Value = client.isActiveClient;

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
            sqlParameter[4].Value = conn.GetLastTableID("Client")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Client";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
         //   return conn.executeInsertQuery(query, sqlParameters);
        }

        public bool Update(ClientModel client, int idClient, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(
                @"UPDATE Client SET accountCodeClient = @accountCodeClient, nameClient = @nameClient, contactPersonName = @contactPersonName, webClient = @webClient, 
                    idTypeClient = @idTypeClient, userCreated = @userCreated, dtCreated = @dtCreated, userModified = @userModified, dtModified = @dtModified, 
                    isActiveClient = @isActiveClient WHERE idClient = @idClient ");

            SqlParameter[] sqlParameter = new SqlParameter[11];

            sqlParameter[0] = new SqlParameter("@accountCodeClient", SqlDbType.NVarChar);
            sqlParameter[0].Value = client.accountCodeClient;

            sqlParameter[1] = new SqlParameter("@nameClient", SqlDbType.NVarChar);
            sqlParameter[1].Value = client.nameClient;

            sqlParameter[2] = new SqlParameter("@contactPersonName", SqlDbType.NVarChar);
            sqlParameter[2].Value = client.contactPersonName;
            
            sqlParameter[3] = new SqlParameter("@webClient", SqlDbType.NVarChar);
            sqlParameter[3].Value = client.webClient;

            sqlParameter[4] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameter[4].Value = client.idTypeClient;

            sqlParameter[5] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[5].Value = client.userCreated;

            sqlParameter[6] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = (client.dtCreated == null || client.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : client.dtCreated;

            sqlParameter[7] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[7].Value = client.userModified;

            sqlParameter[8] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[8].Value = (client.dtModified == null || client.dtModified == DateTime.MinValue) ? SqlDateTime.Null : client.dtModified;

            sqlParameter[9] = new SqlParameter("@isActiveClient", SqlDbType.Bit);
            sqlParameter[9].Value = client.isActiveClient;

            sqlParameter[10] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[10].Value = client.idClient;

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
            sqlParameter[4].Value = client.idClient;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Client";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool UpdateContactPersonName(int idClient, string contactPersonName, string nameForm,int idUser )
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(
               @"UPDATE Client SET contactPersonName = @contactPersonName WHERE idClient = @idClient ");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@contactPersonName", SqlDbType.NVarChar);
            sqlParameter[0].Value = contactPersonName;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = idClient;

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
            sqlParameter[4].Value = idClient.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Client";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update contact personName";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable GetClientByContract(int idContract)
        {
            string query = string.Format(@"
                SELECT c.idClient, accountCodeClient, nameClient, contactPersonName, webClient, idTypeClient, userCreated,um.nameuser as nameUserCreated, dtCreated, userModified, um.nameuser as nameUserModified,dtModified, isActiveClient
                FROM Client c
                LEFT OUTER JOIN Users um ON um.idUser = c.userModified
                LEFT OUTER JOIN Users uc ON uc.idUser = c.userCreated
                INNER JOIN PriceList p ON p.idClient = c.idClient
                WHERE p.idPriceList = @idContract");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContract", SqlDbType.Int);
            sqlParameters[0].Value = idContract;
            return conn.executeSelectQuery(query, sqlParameters);
        }
        ///   DELETE CLIENT
        public DataTable checkIsInInvoice(int idClient)
        {
            string query = string.Format(@"SELECT *
                      FROM Invoice
                      WHERE idClient ='" + idClient + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInMeetings(int idClient)
        {
            string query = string.Format(@"SELECT *
                      FROM Meetings
                      WHERE clientId ='" + idClient + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInMultimedia(int idClient)
        {
            string query = string.Format(@"SELECT *
                      FROM Multimedia
                      WHERE idClient ='" + idClient + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInArrangementPrice(int idClient)
        {
            string query = string.Format(@"SELECT *
                      FROM ArrangementPrice
                      WHERE idClient ='" + idClient + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInArrangementBookArticles(int idClient)
        {
            string query = string.Format(@"SELECT  pla.idPriceListArticle
                                         FROM PriceList p
                                         LEFT OUTER JOIN PriceListArticles pla ON pla.idPriceList = p.idPricelist
                                         INNER JOIN Arrangement a ON a.idArrangement = p.idArrangement 
                                         LEFT OUTER JOIN ArrangementBookArticles aba ON aba.id = pla.idPriceListArticle AND  aba.idArticle = pla.idArticle 
                                         WHERE p.idClient = '" + idClient + "' AND aba.isContract= '1'");

            return conn.executeSelectQuery(query, null);
        }



        public Boolean DeleteClient(int idClient, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Client WHERE idClient = @idClient ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[0].Value = idClient;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM AccDebCre WHERE idClient = @idClient ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ClientPerson WHERE idCLient = @idClient ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);
            //query = string.Format(@"DELETE FROM ContactPerson WHERE idClient = @idClient ");
            
            query = string.Format(@"UPDATE ContactPerson SET idClient = '0'  WHERE idClient = @idClient");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM BISAppointment WHERE client = @idClient ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

                      
            query = string.Format(@"DELETE FROM ClientNotes WHERE idClient = @idClient ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM PriceList WHERE idClient = @idClient ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"UPDATE PriceListArticles SET idClient = '0' WHERE idClient = @idClient");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM PriceListArticles WHERE idPriceList in
           (select distinct idPriceList from PriceList
            where idClient = @idClient ) ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ClientAddress WHERE idClient = @idClient ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ClientEmail WHERE idClient = @idClient ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ClientTel WHERE idClient = @idClient ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonFilter WHERE idContPers = @idClient ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonLabel WHERE idContPers = @idClient");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"DELETE FROM PriceList WHERE idClient = @idClient");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM PriceListArticles WHERE idClient = @idClient");

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
            sqlParameter[4].Value = idClient.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Client";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete client";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

    }
}
