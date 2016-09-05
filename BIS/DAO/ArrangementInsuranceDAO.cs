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
    public class ArrangementInsuranceDAO
    {
        private dbConnection conn;

        public ArrangementInsuranceDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllArrangementInsurance()
        {
            string query = string.Format(@"SELECT idInsurance, labelInsurance, codeInsurance, amountInsurance, dtValidFrom, dtValidTo FROM ArrangementInsurance");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementTravelInsurance(string codeInsurance, Boolean isSport)
        {
            string query = string.Format(@"SELECT idArrangementTravelInsurance,description,ledgerAccount,codeInsurance,isMedicalDevices,isSportActivity
                                           FROM ArrangementTravelInsurance
                                           WHERE codeInsurance = '" + codeInsurance + "' AND isSportActivity = '" + isSport + "' AND isMedicalDevices = 'false'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementTravelInsuranceWithMedical(string codeInsurance, Boolean isSport, Boolean isMedical)
        {
            string query = string.Format(@"SELECT idArrangementTravelInsurance,description,ledgerAccount,codeInsurance,isMedicalDevices,isSportActivity
                                           FROM ArrangementTravelInsurance
                                           WHERE codeInsurance = '" + codeInsurance + "' AND isSportActivity = '" + isSport + "' AND isMedicalDevices = '" + isMedical + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetUniqueLabelNames()
        {
            string query = string.Format(@"SELECT distinct nameLabel FROM Labels");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementInsurance(int idInsurance)
        {
            string query = string.Format(@"SELECT idInsurance, labelInsurance, codeInsurance, amountInsurance, dtValidFrom, dtValidTo FROM ArrangementInsurance
                WHERE idInsurance = @idInsurance");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idInsurance", SqlDbType.Int);
            sqlParameters[0].Value = idInsurance;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetArrangementInsuranceWithCountry(string label, string codeIns, DateTime dateFrom)
        {
            string query = string.Format(@"SELECT idInsurance, labelInsurance, codeInsurance, amountInsurance, dtValidFrom, dtValidTo FROM ArrangementInsurance
                WHERE labelInsurance = @label  and codeInsurance = @codeIns AND  CONVERT(date,'" + dateFrom.ToString("MM/dd/yyyy") + @"')  BETWEEN  dtValidFrom and dtValidTo");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@label", SqlDbType.NVarChar);
            sqlParameters[0].Value = label;

            sqlParameters[1] = new SqlParameter("@codeIns", SqlDbType.NVarChar);
            sqlParameters[1].Value = codeIns;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(ArrangementInsuranceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementInsurance (labelInsurance, codeInsurance, amountInsurance, dtValidFrom, dtValidTo) 
                      VALUES( @labelInsurance, @codeInsurance, @amountInsurance, @dtValidFrom, @dtValidTo)");

            SqlParameter[] sqlParameter = new SqlParameter[5];

            sqlParameter[0] = new SqlParameter("@labelInsurance", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.labelInsurance;

            sqlParameter[1] = new SqlParameter("@codeInsurance", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.codeInsurance;

            sqlParameter[2] = new SqlParameter("@amountInsurance", SqlDbType.Decimal);
            sqlParameter[2].Value = model.amountInsurance;

            sqlParameter[3] = new SqlParameter("@dtValidFrom", SqlDbType.DateTime);
            sqlParameter[3].Value = model.dtValidFrom;

            sqlParameter[4] = new SqlParameter("@dtValidTo", SqlDbType.DateTime);
            sqlParameter[4].Value = model.dtValidTo;

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
            sqlParameter[4].Value = conn.GetLastTableID("ArrangementInsurance")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInsurance";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInsurance";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(ArrangementInsuranceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementInsurance SET 
                labelInsurance = @labelInsurance, codeInsurance = @codeInsurance, amountInsurance  = @amountInsurance ,
                     dtValidFrom = @dtValidFrom, dtValidTo = @dtValidTo WHERE idInsurance = @idInsurance");


            SqlParameter[] sqlParameter = new SqlParameter[6];
            sqlParameter[0] = new SqlParameter("@idInsurance", SqlDbType.Int);
            sqlParameter[0].Value = model.idInsurance;

            sqlParameter[1] = new SqlParameter("@labelInsurance", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.labelInsurance;

            sqlParameter[2] = new SqlParameter("@codeInsurance", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.codeInsurance;

            sqlParameter[3] = new SqlParameter("@amountInsurance", SqlDbType.Decimal);
            sqlParameter[3].Value = model.amountInsurance;

            sqlParameter[4] = new SqlParameter("@dtValidFrom", SqlDbType.DateTime);
            sqlParameter[4].Value = model.dtValidFrom;

            sqlParameter[5] = new SqlParameter("@dtValidTo", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtValidTo;

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
            sqlParameter[4].Value = model.idInsurance;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInsurance";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInsurance";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        // DELETE NOVO

        public DataTable checkIsInArrangemnet(string nameLabel, string provision)
        {
            string query = string.Format(@"SELECT distinct a.idArrangement
                        FROM  Arrangement a 
                        LEFT OUTER JOIN Country c ON a.countryArrangement= c.idCountry
                        LEFT OUTER JOIN ArrangementLabel al on al.idArrangement=a.idArrangement
                        LEFT OUTER JOIN Labels l on al.idLabel=l.id
                        WHERE l.nameLabel = '" + nameLabel + "' and c.provision='" + provision + "'");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean DeleteArrangementInsuranceSript(int idInsurance, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementInsurance WHERE idInsurance = @idInsurance ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idInsurance", SqlDbType.Int);
            sqlParameter[0].Value = idInsurance;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

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
            sqlParameter[4].Value = idInsurance;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInsurance";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInsurance";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete arrangement insurance sript";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        
    }
}
