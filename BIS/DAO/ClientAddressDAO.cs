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
    public class ClientAddressDAO
    {
        private dbConnection conn;
        public ClientAddressDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetClientAddresses(int idClient)
        {
            string query = string.Format(@"SELECT idClient,idAddressType,street,housenr,extension,postalCode,city,country,
                region1,region2,isInternational
                FROM ClientAddress
                WHERE idClient = '" + idClient.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetClientAddressesByType(int idAddressType, int idCLient)
        {
            string query = string.Format(@"SELECT idClient,idAddressType,street,housenr,extension,postalCode,city,country,
                region1,region2,isInternational
                FROM ClientAddress
                WHERE idAddressType = '" + idAddressType + "' AND idClient= '" + idCLient + "'");

            return conn.executeSelectQuery(query, null);

        }

        public bool Save(ClientAddressModel address, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ClientAddress
           (idClient,idAddressType ,street,housenr,extension,postalCode,city,country,isInternational) 
            VALUES (@idClient,@idAddressType ,@street,@housenr,@extension,@postalCode,@city,@country,@isInternational) ");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[0].Value = address.idClient;

            sqlParameter[1] = new SqlParameter("@idAddressType", SqlDbType.Int);
            sqlParameter[1].Value = address.idAddressType;

            sqlParameter[2] = new SqlParameter("@street", SqlDbType.NVarChar);
            sqlParameter[2].Value = address.street;

            sqlParameter[3] = new SqlParameter("@housenr", SqlDbType.NVarChar);
            sqlParameter[3].Value = address.housenr;

            sqlParameter[4] = new SqlParameter("@extension", SqlDbType.NVarChar);
            sqlParameter[4].Value = address.extension;

            sqlParameter[5] = new SqlParameter("@postalCode", SqlDbType.NVarChar);
            sqlParameter[5].Value = address.postalCode;

            sqlParameter[6] = new SqlParameter("@city", SqlDbType.NVarChar);
            sqlParameter[6].Value = address.city;

            sqlParameter[7] = new SqlParameter("@country", SqlDbType.NVarChar);
            sqlParameter[7].Value = address.country;

            sqlParameter[8] = new SqlParameter("@isInternational", SqlDbType.Bit);
            sqlParameter[8].Value = address.isInternational;


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
            sqlParameter[4].Value = address.idClient.ToString() + "_" + address.idAddressType.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient + idAddressType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientAddress";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }


        public bool Update(ClientAddressModel address, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idClient,idAddressType,street,housenr,extension,postalCode,city,country,
                region1,region2,isInternational
                FROM ClientAddress
                WHERE idClient = '" + address.idClient.ToString() + "' AND idAddressType = '" + address.idAddressType + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                string query = string.Format(@"UPDATE ClientAddress SET street = @street,housenr = @housenr,extension = @extension,postalCode = @postalCode,city = @city,country = @country,isInternational = @isInternational
                                            WHERE idClient = @idClient AND idAddressType = @idAddressType ");

                SqlParameter[] sqlParameter = new SqlParameter[9];


                sqlParameter[0] = new SqlParameter("@street", SqlDbType.NVarChar);
                sqlParameter[0].Value = address.street;

                sqlParameter[1] = new SqlParameter("@housenr", SqlDbType.NVarChar);
                sqlParameter[1].Value = address.housenr;

                sqlParameter[2] = new SqlParameter("@extension", SqlDbType.NVarChar);
                sqlParameter[2].Value = address.extension;

                sqlParameter[3] = new SqlParameter("@postalCode", SqlDbType.NVarChar);
                sqlParameter[3].Value = address.postalCode;

                sqlParameter[4] = new SqlParameter("@city", SqlDbType.NVarChar);
                sqlParameter[4].Value = address.city;

                sqlParameter[5] = new SqlParameter("@country", SqlDbType.NVarChar);
                sqlParameter[5].Value = address.country;

                sqlParameter[6] = new SqlParameter("@isInternational", SqlDbType.Bit);
                sqlParameter[6].Value = address.isInternational;

                sqlParameter[7] = new SqlParameter("@idClient", SqlDbType.Int);
                sqlParameter[7].Value = address.idClient;

                sqlParameter[8] = new SqlParameter("@idAddressType", SqlDbType.Int);
                sqlParameter[8].Value = address.idAddressType;

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
                sqlParameter[4].Value = address.idClient.ToString() + "_" + address.idAddressType.ToString();

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idClient + idAddressType";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ClientAddress";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
                return Save(address, nameForm, idUser);
        }

        public bool Delete(int idClient, int idAddressType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ClientAddress WHERE idClient = @idClient AND idAddressType = @idAddressType ");

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@idClient", SqlDbType.Int);
            param[0].Value = idClient;

            param[1] = new SqlParameter("@idAddressType", SqlDbType.Int);
            param[1].Value = idAddressType;

            //return conn.executeUpdateQuery(query, sqlParameters);

            _query.Add(query);
            sqlParameters.Add(param);

            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            param = new SqlParameter[8];


            param[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            param[0].Value = nameForm;

            param[1] = new SqlParameter("@idUser", SqlDbType.Int);
            param[1].Value = idUser;

            param[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            param[2].Value = DateTime.Now;

            param[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            param[3].Value = "U";

            param[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            param[4].Value = idClient + "_" + idAddressType;

            param[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            param[5].Value = "idContPers_idAddressType";

            param[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            param[6].Value = "ClientAddress";

            param[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            param[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(param);

            return conn.executQueryTransaction(_query, sqlParameters);


        }

    }
}
