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
    public class PersonPassportDAO
    {
        private dbConnection conn;
        public PersonPassportDAO()
        {
            conn = new dbConnection();
  
        }

        public DataTable GetPersonPassport(int idPerson)
        {
             string query = string.Format( @"SELECT idContPers,namePassport,numberPassport,birthPlacePassport,issuePlacePassport,dtPassportIssued,dtPassportValid,idCountry,
             lastNamePassport FROM ContactPersonPassport
             WHERE idContPers = '" + idPerson.ToString() +"' " );
             return conn.executeSelectQuery(query, null);
        
        }

        public object GetPersonPassportFullName(int idPerson)
        {
            string query = string.Format(@"SELECT 
                CASE WHEN (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END + ' ' + CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END)='' THEN CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' ' + CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' + CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END ELSE (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END + ' ' + CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END) END as fullname
                FROM ContactPerson cp 
                left outer join ContactPersonPassport p On cp.idContPers = p.idContPers
                WHERE cp.idContPers = '" + idPerson.ToString() + "' ");
            
            return conn.executeScalarQuery(query, null);
        }

        public bool Save(PersonPassportModel pass, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ContactPersonPassport 
                (idContPers, namePassport, numberPassport, birthPlacePassport, issuePlacePassport, dtPassportIssued, 
                    dtPassportValid, idCountry, lastNamePassport) 
                VALUES(@idContPers, @namePassport, @numberPassport, @birthPlacePassport, @issuePlacePassport, @dtPassportIssued, 
                    @dtPassportValid, @idCountry, @lastNamePassport)");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = pass.idContPers;

            sqlParameter[1] = new SqlParameter("@namePassport", SqlDbType.NVarChar);
            sqlParameter[1].Value = pass.namePassport;

            sqlParameter[2] = new SqlParameter("@numberPassport", SqlDbType.NVarChar);
            sqlParameter[2].Value = pass.numberPassport;

            sqlParameter[3] = new SqlParameter("@birthPlacePassport", SqlDbType.NVarChar);
            sqlParameter[3].Value = pass.birthPlacePassport;

            sqlParameter[4] = new SqlParameter("@issuePlacePassport", SqlDbType.NVarChar);
            sqlParameter[4].Value = pass.issuePlacePassport;

            if (pass.dtPassportIssued == null)
            {
                sqlParameter[5] = new SqlParameter("@dtPassportIssued", SqlDbType.Date);
                sqlParameter[5].Value = DBNull.Value; 
            }
            else
            {
                sqlParameter[5] = new SqlParameter("@dtPassportIssued", SqlDbType.Date);
                sqlParameter[5].Value = pass.dtPassportIssued;
            }

            if (pass.dtPassportValid == null)
            {
                sqlParameter[6] = new SqlParameter("@dtPassportValid", SqlDbType.Date);
                sqlParameter[6].Value = DBNull.Value;
            }
            else
            {
                sqlParameter[6] = new SqlParameter("@dtPassportValid", SqlDbType.Date);
                sqlParameter[6].Value = pass.dtPassportValid;
            }

            sqlParameter[7] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[7].Value = pass.idCountry;

            sqlParameter[8] = new SqlParameter("@lastNamePassport", SqlDbType.NVarChar);
            sqlParameter[8].Value = pass.lastNamePassport;

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
            sqlParameter[4].Value = pass.idContPers.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContPers";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonPassport";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save contact person passport";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Update(PersonPassportModel pass, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            
            string querySelect = string.Format(@"SELECT idContPers,namePassport,numberPassport,birthPlacePassport,issuePlacePassport,dtPassportIssued,dtPassportValid,idCountry,
             lastNamePassport FROM ContactPersonPassport
             WHERE idContPers = '" + pass.idContPers.ToString() + "' ");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {
            
            string query = string.Format(@"UPDATE ContactPersonPassport SET namePassport = @namePassport, numberPassport = @numberPassport, 
                birthPlacePassport = @birthPlacePassport, issuePlacePassport = @issuePlacePassport, dtPassportIssued = @dtPassportIssued, 
                    dtPassportValid = @dtPassportValid, idCountry = @idCountry, lastNamePassport = @lastNamePassport
                WHERE idContPers = @idContPers");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@namePassport", SqlDbType.NVarChar);
            sqlParameter[0].Value = pass.namePassport;

            sqlParameter[1] = new SqlParameter("@numberPassport", SqlDbType.NVarChar);
            sqlParameter[1].Value = pass.numberPassport;

            sqlParameter[2] = new SqlParameter("@birthPlacePassport", SqlDbType.NVarChar);
            sqlParameter[2].Value = pass.birthPlacePassport;

            sqlParameter[3] = new SqlParameter("@issuePlacePassport", SqlDbType.NVarChar);
            sqlParameter[3].Value = pass.issuePlacePassport;

            if (pass.dtPassportIssued == null)
            {
                sqlParameter[4] = new SqlParameter("@dtPassportIssued", SqlDbType.Date);
                sqlParameter[4].Value = DBNull.Value;
            }
            else
            {
                sqlParameter[4] = new SqlParameter("@dtPassportIssued", SqlDbType.Date);
                sqlParameter[4].Value = pass.dtPassportIssued;
            }

            if (pass.dtPassportValid == null)
            {
                sqlParameter[5] = new SqlParameter("@dtPassportValid", SqlDbType.Date);
                sqlParameter[5].Value = DBNull.Value;
            }
            else
            {
                sqlParameter[5] = new SqlParameter("@dtPassportValid", SqlDbType.Date);
                sqlParameter[5].Value = pass.dtPassportValid;
            }

            sqlParameter[6] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[6].Value = pass.idCountry;

            sqlParameter[7] = new SqlParameter("@lastNamePassport", SqlDbType.NVarChar);
            sqlParameter[7].Value = pass.lastNamePassport;
       
            sqlParameter[8] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[8].Value = pass.idContPers;

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
            sqlParameter[4].Value = pass.idContPers;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContPers";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonPassport";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update contact person passport";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
               return Save(pass,nameForm,idUser);
            }
        }

     
    }
}

