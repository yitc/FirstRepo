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
    public class CountryDAO
    {
         private dbConnection conn;

         public CountryDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetCountries()
        {
            string query = string.Format(@"SELECT idCountry, internationalCode, nameCountry, nacionality,provision,premie FROM Country");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetCountryByID(int idCountry)
        {
            string query = string.Format(@"SELECT idCountry,internationalCode,nameCountry,nacionality,provision,premie
                                           FROM Country
                                           WHERE idCountry=@idCountry");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameters[0].Value = idCountry;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetCountryByCodeOrName(string codeNameCountry)
        {
            string query = string.Format(@"SELECT idCountry,internationalCode,nameCountry,nacionality,provision,premie
                                           FROM Country
                                           WHERE internationalCode=@codeNameCountry OR nameCountry=@codeNameCountry");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@codeNameCountry", SqlDbType.NVarChar);
            sqlParameters[0].Value = codeNameCountry;

            return conn.executeSelectQuery(query, sqlParameters);
        }

//        public Boolean Save(CountryModel model)
//        {
//            string query = string.Format(@"INSERT INTO Country (internationalCode,nameCountry,nacionality)
//                                           VALUES (@internationalCode,@nameCountry,@nacionality) ");

//            SqlParameter[] sqlParameters = new SqlParameter[3];

//            sqlParameters[0] = new SqlParameter("@internationalCode", SqlDbType.Char);
//            sqlParameters[0].Value = model.interNationalCode;

//            sqlParameters[1] = new SqlParameter("@nameCountry", SqlDbType.NVarChar);
//            sqlParameters[1].Value = model.nameCountry;

//            sqlParameters[2] = new SqlParameter("@nacionality", SqlDbType.NVarChar);
//            sqlParameters[2].Value = model.nacionality;

//            return conn.executeInsertQuery(query, sqlParameters);
//        }
        public DataTable IsIn(int idCountry)
        {
            string query = string.Format(@"SELECT distinct countryArrangement as idCountry
                                           FROM Arrangement
                                           WHERE countryArrangement=@idCountry
                                           UNION 
                                           SELECT distinct idCountry as idCountry
                                           FROM Company
                                           WHERE idCountry=@idCountry
                                           SELECT distinct idCountry as idCountry
                                           FROM ContactPersonPassport
                                           WHERE idCountry=@idCountry
                                            ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameters[0].Value = idCountry;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public Boolean Save(CountryModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Country (idCountry,internationalCode,nameCountry,nacionality,provision,premie)
                                           VALUES (@idCountry,@internationalCode,@nameCountry,@nacionality,@provision,@premie) ");

            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[0].Value = model.idCountry;

            sqlParameter[1] = new SqlParameter("@internationalCode", SqlDbType.Char);
            sqlParameter[1].Value = model.interNationalCode;

            sqlParameter[2] = new SqlParameter("@nameCountry", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.nameCountry;

            sqlParameter[3] = new SqlParameter("@nacionality", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.nacionality;

            sqlParameter[4] = new SqlParameter("@provision", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.provision;

            sqlParameter[5] = new SqlParameter("@premie", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.premie;

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
            sqlParameter[4].Value = model.idCountry;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCountry";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Country";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public Boolean Update(CountryModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Country SET internationalCode=@internationalCode,
                                           nameCountry=@nameCountry,nacionality=@nacionality,provision=@provision,premie=@premie
                                           WHERE idCountry=@idCountry ");

            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[0].Value = model.idCountry;

            sqlParameter[1] = new SqlParameter("@internationalCode", SqlDbType.Char);
            sqlParameter[1].Value = model.interNationalCode;

            sqlParameter[2] = new SqlParameter("@nameCountry", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.nameCountry;

            sqlParameter[3] = new SqlParameter("@nacionality", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.nacionality;

            sqlParameter[4] = new SqlParameter("@provision", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.provision;

            sqlParameter[5] = new SqlParameter("@premie", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.premie;

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
            sqlParameter[4].Value = model.idCountry;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCountry";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Country";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idCountry, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE Country 
                                           WHERE idCountry=@idCountry ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[0].Value = idCountry;

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
            sqlParameter[4].Value = idCountry;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCountry";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Country";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public DataTable LastidCountry()
        {
            string query = string.Format(@"SELECT TOP 1 idCountry FROM  Country ORDER BY idCountry DESC");

            return conn.executeSelectQuery(query, null);
        }
        // NOVO DELETE
        public DataTable checkIsInArrrangement(int countryArrangement)
        {
            string query = string.Format(@"
                SELECT * FROM Arrangement 
                WHERE countryArrangement = '" + countryArrangement + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInContactPersonPassport(int idCountry)
        {
            string query = string.Format(@"
                SELECT * FROM ContactPersonPassport 
                WHERE idCountry = '" + idCountry + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInCompany(int idCountry)
        {
            string query = string.Format(@"
                SELECT * FROM Company 
                WHERE idCountry = '" + idCountry + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInEmployees(int idCountry)
        {
            string query = string.Format(@"
                SELECT * FROM Employees 
                WHERE idCountry = '" + idCountry + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInProvinces(int idCountry)
        {
            string query = string.Format(@"
                SELECT * FROM Provinces 
                WHERE idCountry = '" + idCountry + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInArrangementCalculationFirstNotArticles(int idCountry)
        {
            string query = string.Format(@"
                SELECT * FROM ArrangementCalculationFirstNotArticles 
                WHERE idCountry = '" + idCountry + "'");

            return conn.executeSelectQuery(query, null);
        }
        public Boolean DeleteCountryScript(int idCountry)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Country WHERE idCountry = @idCountry ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[0].Value = idCountry;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
