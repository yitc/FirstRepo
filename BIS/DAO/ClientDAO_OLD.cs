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
                SELECT idClient, accountCodeClient, nameClient, addressClient, zipCodeClient, cityClient, countryClient, visitAddressClient, 
                    visitZipCodeClient, visitCityClient, emailClient, webClient, idTypeClient, userCreated, dtCreated, userModified,dtModified, isActiveClient
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
                @"SELECT idClient, accountCodeClient, nameClient, addressClient, zipCodeClient, cityClient, countryClient, visitAddressClient, 
                    visitZipCodeClient, visitCityClient, emailClient, webClient, c.idTypeClient, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE ct.nameTypeClient END AS nameTypeClient,userCreated, dtCreated, userModified,dtModified, isActiveClient
                    FROM Client c
                    LEFT OUTER JOIN ClientTypes ct ON c.idTypeClient = ct.idTypeClient
                    LEFT JOIN STRING" + idLang + " s ON ct.nameTypeClient=s.stringKey ");

            return conn.executeSelectQuery(query, null);
        }


        public bool Save(ClientModel client)
        {
            string query = string.Format(
                @"INSERT INTO Client(accountCodeClient, nameClient, addressClient, zipCodeClient, cityClient, countryClient, visitAddressClient, 
                    visitZipCodeClient, visitCityClient, emailClient, webClient, idTypeClient, userCreated, dtCreated, userModified, dtModified, isActiveClient) 
                  VALUES(@accountCodeClient, @nameClient, @addressClient, @zipCodeClient, @cityClient, @countryClient, @visitAddressClient, 
                    @visitZipCodeClient, @visitCityClient, @emailClient, @webClient, @idTypeClient, @userCreated, @dtCreated, @userModified, @dtModified, @isActiveClient)
                ");

            SqlParameter[] sqlParameters = new SqlParameter[17];

            sqlParameters[0] = new SqlParameter("@accountCodeClient", SqlDbType.NVarChar);
            sqlParameters[0].Value = client.accountCodeClient;

            sqlParameters[1] = new SqlParameter("@nameClient", SqlDbType.NVarChar);
            sqlParameters[1].Value = client.nameClient;

            sqlParameters[2] = new SqlParameter("@addressClient", SqlDbType.NVarChar);
            sqlParameters[2].Value = client.addressClient;

            sqlParameters[3] = new SqlParameter("@zipCodeClient", SqlDbType.NVarChar);
            sqlParameters[3].Value = client.zipCodeClient;

            sqlParameters[4] = new SqlParameter("@cityClient", SqlDbType.NVarChar);
            sqlParameters[4].Value = client.cityClient;

            sqlParameters[5] = new SqlParameter("@countryClient", SqlDbType.Int);
            sqlParameters[5].Value = client.countryClient;

            sqlParameters[6] = new SqlParameter("@visitAddressClient", SqlDbType.NVarChar);
            sqlParameters[6].Value = client.visitAddressClient;

            sqlParameters[7] = new SqlParameter("@visitZipCodeClient", SqlDbType.NVarChar);
            sqlParameters[7].Value = client.visitZipCodeClient;

            sqlParameters[8] = new SqlParameter("@visitCityClient", SqlDbType.NVarChar);
            sqlParameters[8].Value = client.visitCityClient;

            sqlParameters[9] = new SqlParameter("@emailClient", SqlDbType.NVarChar);
            sqlParameters[9].Value = client.emailClient;

            sqlParameters[10] = new SqlParameter("@webClient", SqlDbType.NVarChar);
            sqlParameters[10].Value = client.webClient;

            sqlParameters[11] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameters[11].Value = client.idTypeClient;

            sqlParameters[12] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameters[12].Value = client.userCreated;

            sqlParameters[13] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameters[13].Value = (client.dtCreated == null || client.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : client.dtCreated;

            sqlParameters[14] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameters[14].Value = client.userModified;

            sqlParameters[15] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameters[13].Value = (client.dtModified == null || client.dtModified == DateTime.MinValue) ? SqlDateTime.Null : client.dtModified;

            sqlParameters[16] = new SqlParameter("@isActiveClient", SqlDbType.Bit);
            sqlParameters[16].Value = client.isActiveClient;

            return conn.executeInsertQuery(query, sqlParameters);
        }

        public bool Update(ClientModel client, int idClient)
        {
            string query = string.Format(
                @"UPDATE Client SET accountCodeClient = @accountCodeClient, nameClient = @nameClient, addressClient = @addressClient, zipCodeClient = @zipCodeClient,
                cityClient = @cityClient, countryClient = @countryClient, visitAddressClient = @visitAddressClient, 
                    visitZipCodeClient = @visitZipCodeClient, visitCityClient = @visitCityClient, emailClient = @emailClient, webClient = @webClient, 
                    idTypeClient = @idTypeClient, userCreated = @userCreated, dtCreated = @dtCreated, userModified = @userModified, dtModified = @dtModified, 
                    isActiveClient = @isActiveClient WHERE idClient = @idClient ");

            SqlParameter[] sqlParameters = new SqlParameter[18];

            sqlParameters[0] = new SqlParameter("@accountCodeClient", SqlDbType.NVarChar);
            sqlParameters[0].Value = client.accountCodeClient;

            sqlParameters[1] = new SqlParameter("@nameClient", SqlDbType.NVarChar);
            sqlParameters[1].Value = client.nameClient;

            sqlParameters[2] = new SqlParameter("@addressClient", SqlDbType.NVarChar);
            sqlParameters[2].Value = client.addressClient;

            sqlParameters[3] = new SqlParameter("@zipCodeClient", SqlDbType.NVarChar);
            sqlParameters[3].Value = client.zipCodeClient;

            sqlParameters[4] = new SqlParameter("@cityClient", SqlDbType.NVarChar);
            sqlParameters[4].Value = client.cityClient;

            sqlParameters[5] = new SqlParameter("@countryClient", SqlDbType.Int);
            sqlParameters[5].Value = client.countryClient;

            sqlParameters[6] = new SqlParameter("@visitAddressClient", SqlDbType.NVarChar);
            sqlParameters[6].Value = client.visitAddressClient;

            sqlParameters[7] = new SqlParameter("@visitZipCodeClient", SqlDbType.NVarChar);
            sqlParameters[7].Value = client.visitZipCodeClient;

            sqlParameters[8] = new SqlParameter("@visitCityClient", SqlDbType.NVarChar);
            sqlParameters[8].Value = client.visitCityClient;

            sqlParameters[9] = new SqlParameter("@emailClient", SqlDbType.NVarChar);
            sqlParameters[9].Value = client.emailClient;

            sqlParameters[10] = new SqlParameter("@webClient", SqlDbType.NVarChar);
            sqlParameters[10].Value = client.webClient;

            sqlParameters[11] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameters[11].Value = client.idTypeClient;

            sqlParameters[12] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameters[12].Value = client.userCreated;

            sqlParameters[13] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameters[13].Value = (client.dtCreated == null || client.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : client.dtCreated;

            sqlParameters[14] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameters[14].Value = client.userModified;

            sqlParameters[15] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameters[13].Value = (client.dtModified == null || client.dtModified == DateTime.MinValue) ? SqlDateTime.Null : client.dtModified;

            sqlParameters[16] = new SqlParameter("@isActiveClient", SqlDbType.Bit);
            sqlParameters[16].Value = client.isActiveClient;

            sqlParameters[17] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[17].Value = client.idClient;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public bool Delete(int id)
        {
            string query = string.Format(@"DELETE FROM  Client WHERE idClient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

    }
}
