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
    public class VolFeaturesDAO
    {
        private dbConnection conn;
        public VolFeaturesDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllVolFeatures()
        {
            string query = string.Format(@"SELECT idFeatures, idContPers, codeCertificate, codeTraining, 
                                           [expireDate], archiveDate, scheduleDate
                                           FROM VolFeatures");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetVolFeaturesByID(string idFeatures)
        {
            string query = string.Format(@"SELECT idFeatures, idContPers, codeCertificate, codeTraining, 
                                           [expireDate], archiveDate, scheduleDate
                                           FROM VolFeatures
                                           WHERE idContPers=@idFeatures");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idFeatures", SqlDbType.Int);
            sqlParameters[0].Value = idFeatures;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllFeaturesFromPersonGrid(int idContPers) // za formu Features uzima ID Persona
        {
            string query = string.Format(@"SELECT vf.idContPers, vf.idFeatures, vf.codeCertificate, vf.codeTraining,tr.nameTraining,cer.nameCertificate,
                                           vf.[expireDate],vf.archiveDate, vf.scheduleDate
                                           FROM VolFeatures AS vf                                           
                                           left outer JOIN Training as tr ON tr.codeTraining=vf.codeTraining
                                           left outer join Certificates cer ON cer.codeCertificate=vf.codeCertificate                                          
                                           WHERE vf.idContPers= @idContPers");

            //SELECT cp.idContPers, vf.idFeatures, vf.codeCertificate, vf.codeTraining,
            //                               vf.[expireDate],vf.archiveDate, vf.scheduleDate
            //                               FROM VolFeatures AS vf
            //                               LEFT OUTER JOIN ContactPerson as cp ON vf.idContPers=cp.idContPers
            //                               WHERE cp.idContPers= @idContPers

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetVolFeaturesByCodeCertificate(string certificate )
        {
            string query = string.Format(@"SELECT idFeatures, idContPers, codeCertificate, codeTraining, 
                                        [expireDate], archiveDate, scheduleDate
                                        FROM VolFeatures
                                        WHERE codeCertificate = @certificate");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@certificate", SqlDbType.NVarChar);
            sqlParameters[0].Value = certificate;

            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetVolFeaturesByTraining(string training)
        {
            string query = string.Format(@"SELECT idFeatures, idContPers, codeCertificate, codeTraining, 
                                          [expireDate], archiveDate, scheduleDate
                                          FROM VolFeatures
                                          WHERE codeTraining = @training");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@training", SqlDbType.NVarChar);
            sqlParameters[0].Value = training;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(VolFeaturesModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO VolFeatures (idContPers, codeCertificate, codeTraining, 
                                        [expireDate], archiveDate, scheduleDate)
                                        VALUES (@idContPers, @codeCertificate, @codeTraining, @expireDate, @archiveDate, @scheduleDate)");

            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = model.idContPers;

            sqlParameter[1] = new SqlParameter("@codeCertificate", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.codeCertificate;

            sqlParameter[2] = new SqlParameter("@codeTraining", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.codeTraining;

            sqlParameter[3] = new SqlParameter("@expireDate", SqlDbType.DateTime);
            sqlParameter[3].Value = model.expireDate;

            sqlParameter[4] = new SqlParameter("@archiveDate", SqlDbType.DateTime);
            sqlParameter[4].Value = model.archiveDate;

            sqlParameter[5] = new SqlParameter("@scheduleDate", SqlDbType.DateTime);
            sqlParameter[5].Value = model.scheduleDate;



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
            sqlParameter[4].Value = conn.GetLastTableID("VolFeatures") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFeatures";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolFeatures";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save Vol features ";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }


        public Boolean Delete(int idFeatures, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE VolFeatures WHERE idFeatures = @idFeatures");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idFeatures", SqlDbType.Int);
            sqlParameter[0].Value = idFeatures;

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
            sqlParameter[4].Value = idFeatures;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFeatures";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolFeatures";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete Vol features ";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}