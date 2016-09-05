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
    public class PersonAddressDAO
    {
        private dbConnection conn;
        public PersonAddressDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetPersonAddresses(int idPerson)
        {
            string query = string.Format(@"SELECT idContPers,idAddressType,street,housenr,extension,postalCode,city,country,
                region1,region2,isInternational
                FROM ContactPersonAddress
                WHERE idContPers = '" + idPerson.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetPersonAddressesByType(int idAddressType, int idContPers)
        {
            string query = string.Format(@"SELECT idContPers,idAddressType,street,housenr,extension,postalCode,city,country,
                region1,region2,isInternational
                FROM ContactPersonAddress
                WHERE idAddressType = '" + idAddressType + "' AND idContPers= '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);

        }
        public bool Save(PersonAddressModel address, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            
                string query = string.Format(@"INSERT INTO ContactPersonAddress
           (idContPers,idAddressType ,street,housenr,extension,postalCode,city,country,isInternational) 
            VALUES (@idContPers,@idAddressType ,@street,@housenr,@extension,@postalCode,@city,@country,@isInternational) ");


                SqlParameter[] sqlParameter = new SqlParameter[9];

                sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter[0].Value = address.idContPers;

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
                sqlParameter[4].Value = address.idContPers.ToString();

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idContPers";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ContactPersonAddress";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save contact person address";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
        }


        public bool Update(PersonAddressModel address, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idContPers,idAddressType,street,housenr,extension,postalCode,city,country,
                region1,region2,isInternational
                FROM ContactPersonAddress
                WHERE idContPers = '" + address.idContPers.ToString() + "' AND idAddressType = '" + address.idAddressType + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                string query = string.Format(@"UPDATE ContactPersonAddress SET street = @street,housenr = @housenr,extension = @extension,postalCode = @postalCode,city = @city,country = @country,isInternational = @isInternational WHERE idContPers = @idContPers AND idAddressType = @idAddressType ");

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

                sqlParameter[7] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter[7].Value = address.idContPers;

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
                sqlParameter[4].Value = address.idContPers + "_" + address.idAddressType;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idContPers_idAddressType";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ContactPersonAddress";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update contact person address";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
                return Save(address,nameForm,idUser);
        }

        public bool Delete(int idPerson, int idAddressType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ContactPersonAddress WHERE idContPers = @idContPers AND idAddressType = @idAddressType ");

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            param[0].Value = idPerson;

            param[1] = new SqlParameter("@idAddressType", SqlDbType.Int);
            param[1].Value = idAddressType;

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
            param[4].Value = idPerson + "_" + idAddressType;

            param[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            param[5].Value = "idContPers_idAddressType";

            param[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            param[6].Value = "ContactPersonAddress";

            param[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            param[7].Value = "Update contact person address";

            _query.Add(query);
            sqlParameters.Add(param);

            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public DataTable GetPersonCities()
        {
            string query = string.Format(@"select distinct city from ContactPersonAddress
                where idAddressType = '1' AND LTRIM(RTRIM(city)) <> ''");

            return conn.executeSelectQuery(query, null);

        }
    }
}
