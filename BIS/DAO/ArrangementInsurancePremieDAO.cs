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
    public class ArrangementInsurancePremieDAO
    {
        private dbConnection conn;

        public ArrangementInsurancePremieDAO()
        {
            conn = new dbConnection();
            
        }

        public DataTable GetAllArrangementInsurancePremie()
        {
            string query = string.Format(@"SELECT idPremie, premie, codeInsurance, amountPremie,dtValidFrom, dtValidTo FROM ArrangementInsurancePremie");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementInsurancePremie(string premie, string code,DateTime dateFrom)
        {
            string query = string.Format(@"SELECT idPremie, premie, codeInsurance, amountPremie FROM ArrangementInsurancePremie
                WHERE premie = @premie  AND codeInsurance = @code AND  CONVERT(date,'" + dateFrom.ToString("MM/dd/yyyy") + @"')  BETWEEN  dtValidFrom and dtValidTo");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@premie", SqlDbType.NVarChar);
            sqlParameters[0].Value = premie;

            sqlParameters[1] = new SqlParameter("@code", SqlDbType.NVarChar);
            sqlParameters[1].Value = code;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(ArrangementInsurancePremieModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementInsurancePremie ( premie, codeInsurance, amountPremie,dtValidFrom,dtValidTo) 
                      VALUES(@premie, @codeInsurance, @amountPremie,@dtValidFrom, @dtValidTo)");


            SqlParameter[] sqlParameter = new SqlParameter[5];            

            sqlParameter[0] = new SqlParameter("@premie", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.premie;

            sqlParameter[1] = new SqlParameter("@codeInsurance", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.codeInsurance;

            sqlParameter[2] = new SqlParameter("@amountPremie", SqlDbType.Decimal);
            sqlParameter[2].Value = model.amountPremie;


            sqlParameter[3] = new SqlParameter("@dtValidFrom", SqlDbType.DateTime);
            sqlParameter[3].Value = model.dtValidFrom;

            sqlParameter[4] = new SqlParameter("@dtValidTo", SqlDbType.DateTime);
            sqlParameter[4].Value = model.dtValidTo;

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
            sqlParameter[4].Value = conn.GetLastTableID("ArrangementInsurancePremie")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPremie";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInsurancePremie";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(ArrangementInsurancePremieModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementInsurancePremie SET 
                premie = @premie, codeInsurance = @codeInsurance, amountPremie = @amountPremie, dtValidFrom= @dtValidFrom,
                dtValidTo= @dtValidTo
                WHERE idPremie = @idPremie");


            SqlParameter[] sqlParameter = new SqlParameter[6];
            sqlParameter[0] = new SqlParameter("@idPremie", SqlDbType.Int);
            sqlParameter[0].Value = model.idPremie;

            sqlParameter[1] = new SqlParameter("@premie", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.premie;

            sqlParameter[2] = new SqlParameter("@codeInsurance", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.codeInsurance;

            sqlParameter[3] = new SqlParameter("@amountPremie", SqlDbType.Decimal);
            sqlParameter[3].Value = model.amountPremie;

            sqlParameter[4] = new SqlParameter("@dtValidFrom", SqlDbType.DateTime);
            sqlParameter[4].Value = model.dtValidFrom;

            sqlParameter[5] = new SqlParameter("@dtValidTo", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtValidTo;

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
            sqlParameter[4].Value = model.idPremie;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPremie";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInsurancePremie";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

      
        public DataTable checkIsInArrangemnet(string premie)
        {
            string query = string.Format(@"SELECT distinct a.idArrangement
                        FROM  Arrangement a 
                        LEFT OUTER JOIN Country c ON a.countryArrangement= c.idCountry
                        WHERE c.premie='" + premie + "'");

            return conn.executeSelectQuery(query, null);
        }
        public Boolean DeleteArrangementInsuranceSript(int idPremie, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementInsurancePremie WHERE idPremie = @idPremie ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idPremie", SqlDbType.Int);
            sqlParameter[0].Value = idPremie;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

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
            sqlParameter[4].Value = idPremie.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPremie";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInsurancePremie";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete arrangement insurance sript";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
