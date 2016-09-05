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
    public class VolSimilarityDAO
    {
        private dbConnection conn;

        public VolSimilarityDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllSimilarities(int idContPers)
        {
            string query = string.Format(@"SELECT idSimilarity, idContPers, optionSimilarity, dtEffectiveDate, dtExpirationDate,dtSent FROM VolSimilarity WHERE idContPers = '"+ idContPers +"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetSimilarityById(string idSimilarity, int idContPers)
        {
            string query = string.Format(@"SELECT idSimilarity, idContPers, optionSimilarity, dtEffectiveDate, dtExpirationDate,dtSent FROM VolSimilarity WHERE idSimilarity = '" + idSimilarity + "' AND idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean Save(List<VolSimilarityModel> model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            foreach(VolSimilarityModel m in model)
            {
                string deleteQuery = string.Format(@"DELETE FROM VolSimilarity WHERE idSimilarity = '" + m.idSimilarity + "' AND idContPers = '"+ m.idContPers +"'");

                _query.Add(deleteQuery);
                sqlParameters.Add(null);

                string insertQuery = string.Format(@"INSERT INTO VolSimilarity (idSimilarity, idContPers, optionSimilarity, dtEffectiveDate, dtExpirationDate,dtSent) 
                      VALUES(@idSimilarity, @idContPers, @optionSimilarity, @dtEffectiveDate, @dtExpirationDate,@dtSent)");

                SqlParameter[] sqlParameter = new SqlParameter[6];

                sqlParameter[0] = new SqlParameter("@idSimilarity", SqlDbType.NVarChar);
                sqlParameter[0].Value = m.idSimilarity;

                sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter[1].Value = m.idContPers;

                sqlParameter[2] = new SqlParameter("@optionSimilarity", SqlDbType.NVarChar);
                sqlParameter[2].Value = m.optionSimilarity;

                sqlParameter[3] = new SqlParameter("@dtEffectiveDate", SqlDbType.DateTime);
                sqlParameter[3].Value = (m.dtEffectiveDate == null || m.dtEffectiveDate == DateTime.MinValue) ? SqlDateTime.Null : m.dtEffectiveDate;

                sqlParameter[4] = new SqlParameter("@dtExpirationDate", SqlDbType.DateTime);
                sqlParameter[4].Value = (m.dtExpirationDate == null || m.dtExpirationDate == DateTime.MinValue) ? SqlDateTime.Null : m.dtExpirationDate;

                sqlParameter[5] = new SqlParameter("@dtSent", SqlDbType.DateTime);
                sqlParameter[5].Value = (m.dtSent == null || m.dtSent == DateTime.MinValue) ? SqlDateTime.Null : m.dtSent;

                _query.Add(insertQuery);
                sqlParameters.Add(sqlParameter);

                string query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


                sqlParameter = new SqlParameter[8];


                sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                sqlParameter[0].Value = nameForm;

                sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                sqlParameter[1].Value = idUser;

                sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                sqlParameter[2].Value = DateTime.Now;

                sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                sqlParameter[3].Value = "DI";

                sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                sqlParameter[4].Value = m.idSimilarity.ToString() + "_" + m.idContPers.ToString();

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idSimilarity_idContPers";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "VolSimilarity";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete and insert vol simularity";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }

            return conn.executQueryTransaction(_query,sqlParameters);            
        }
        
        public Boolean Delete(List<VolSimilarityModel> model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            foreach (VolSimilarityModel m in model)
            {
                string deleteQuery = string.Format(@"DELETE FROM VolSimilarity WHERE idSimilarity = '" + m.idSimilarity + "' AND idContPers = '" + m.idContPers + "'");

                _query.Add(deleteQuery);
                sqlParameters.Add(null);

                string query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


                SqlParameter[] sqlParameter = new SqlParameter[8];


                sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                sqlParameter[0].Value = nameForm;

                sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                sqlParameter[1].Value = idUser;

                sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                sqlParameter[2].Value = DateTime.Now;

                sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                sqlParameter[3].Value = "D";

                sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                sqlParameter[4].Value = m.idSimilarity.ToString() + "_" + m.idContPers.ToString();

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idSimilarity_idContPers";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "VolSimilarity";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete from volSimularity";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }

            return conn.executQueryTransaction(_query,sqlParameters);   
        }

        // SIMILAARITY ARCHIVE

        public DataTable GetAllSimilaritiesArchive(int idContPers)
        {
            string query = string.Format(@"SELECT id, idSimilarity, idContPers, optionSimilarity, dtEffectiveDate, dtExpirationDate, dtSent 
                FROM VolSimilarityArchive WHERE idContPers = '" + idContPers + "' ORDER BY dtExpirationDate DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetSimilarityArchiveByDate(string idSimilarity, int idContPers, DateTime expdate)
        {
            string query = string.Format(@"SELECT id, idSimilarity, idContPers, optionSimilarity, dtEffectiveDate, dtExpirationDate, dtSent
                FROM VolSimilarityArchive WHERE idSimilarity = @idSimilarity AND idContPers = @idContPers AND dtExpirationDate = @dtExpirationDate");

            SqlParameter[] sqlParameters = new SqlParameter[3];

            sqlParameters[0] = new SqlParameter("@idSimilarity", SqlDbType.NVarChar);
            sqlParameters[0].Value = idSimilarity;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            sqlParameters[2] = new SqlParameter("@dtExpirationDate", SqlDbType.DateTime);
            sqlParameters[2].Value = (expdate == null || expdate == DateTime.MinValue) ? SqlDateTime.Null : expdate;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean SaveToArchive(VolSimilarityModel m, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO VolSimilarityArchive (idSimilarity, idContPers, optionSimilarity, dtEffectiveDate, dtExpirationDate,dtSent) 
                    VALUES(@idSimilarity, @idContPers, @optionSimilarity, @dtEffectiveDate, @dtExpirationDate,@dtSent)");

            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@idSimilarity", SqlDbType.NVarChar);
            sqlParameter[0].Value = m.idSimilarity;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = m.idContPers;

            sqlParameter[2] = new SqlParameter("@optionSimilarity", SqlDbType.NVarChar);
            sqlParameter[2].Value = m.optionSimilarity;

            sqlParameter[3] = new SqlParameter("@dtEffectiveDate", SqlDbType.DateTime);
            sqlParameter[3].Value = (m.dtEffectiveDate == null || m.dtEffectiveDate == DateTime.MinValue) ? SqlDateTime.Null : m.dtEffectiveDate;

            sqlParameter[4] = new SqlParameter("@dtExpirationDate", SqlDbType.DateTime);
            sqlParameter[4].Value = (m.dtExpirationDate == null || m.dtExpirationDate == DateTime.MinValue) ? SqlDateTime.Null : m.dtExpirationDate;

            sqlParameter[5] = new SqlParameter("@dtSent", SqlDbType.DateTime);
            sqlParameter[5].Value = (m.dtSent == null || m.dtSent == DateTime.MinValue) ? SqlDateTime.Null : m.dtSent;

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
            sqlParameter[4].Value = conn.GetLastTableID("VolSimilarityArchive") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "id";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolSimilarityArchive";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save to VolSimilarityArchive";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
                     
        }
    }
}
