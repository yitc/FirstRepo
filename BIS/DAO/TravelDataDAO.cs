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
    public class TravelDataDAO
    {
        private dbConnection conn;

        public TravelDataDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllTypeTravelData(string language)
        {
            string query = string.Format(
                  @"SELECT  '' AS code, idArrType AS ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameArrType END AS Name, CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Arrangement type' END AS Type
                  FROM ArrType ct
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = ct.nameArrType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Arrangement type'
                  UNION
                  SELECT codeCertificate AS code, idCertificate as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameCertificate END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Certificates type' END AS Type
                  FROM Certificates cct
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = cct.nameCertificate
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Certificates type'
                  UNION
                  SELECT codeTraining AS code, idTraining as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTraining END AS Name,  CASE WHEN s2.stringValue IS NOT NULL THEN s2.stringValue ELSE 'Training type' END AS Type
                  FROM Training mdt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = mdt.nameTraining
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Training type'                  
                  UNION
                  SELECT idArrangementStatus AS astatus, idArrangementStatus as code,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameArrangementStatus END AS Name,  CASE WHEN s2.stringValue IS NOT NULL THEN s2.stringValue ELSE 'Arrangement status' END AS Type
                  FROM ArrangementStatus arsta
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = arsta.nameArrangementStatus
                  LEFT OUTER JOIN STRING" + language + @" s2 ON s2.stringkey = 'Arrangement status'");           


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.VarChar);
            sqlParameters[0].Value = language;

            return conn.executeSelectQuery(query, sqlParameters);
        }      
       

        #region Update
        public Boolean UpdateArrType(int idArrType, string nameArrType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrType
                SET nameArrType = @nameArrType
	        	WHERE idArrType = @idArrType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameter[0].Value = idArrType;

            sqlParameter[1] = new SqlParameter("@nameArrType", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameArrType;

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
            sqlParameter[4].Value = idArrType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Upade arrangement type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateCertificates(int idCertificate, string nameCertificate, string codeCertificate, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Certificates
                SET nameCertificate = @nameCertificate , codeCertificate = @codeCertificate
	        	WHERE idCertificate = @idCertificate");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idCertificate", SqlDbType.Int);
            sqlParameter[0].Value = idCertificate;

            sqlParameter[1] = new SqlParameter("@nameCertificate", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameCertificate;

            sqlParameter[2] = new SqlParameter("@codeCertificate", SqlDbType.NVarChar);
            sqlParameter[2].Value = codeCertificate;

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
            sqlParameter[4].Value = idCertificate;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCertificate";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Certificates";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update certificates";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateTraining(int idTraining, string nameTraining, string codeTraining, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Training
                SET nameTraining = @nameTraining, codeTraining = @codeTraining
	        	WHERE idTraining = @idTraining");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idTraining", SqlDbType.Int);
            sqlParameter[0].Value = idTraining;

            sqlParameter[1] = new SqlParameter("@nameTraining", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameTraining;

            sqlParameter[2] = new SqlParameter("@codeTraining", SqlDbType.NVarChar);
            sqlParameter[2].Value = codeTraining;


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
            sqlParameter[4].Value = idTraining;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTraining";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Training";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update training";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateArrangementStatus(int idArrangementStatus, string nameArrangementStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementStatus
                   SET nameArrangementStatus = @nameArrangementStatus
                   WHERE idArrangementStatus = @idArrangementStatus");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idArrangementStatus", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementStatus;

            sqlParameter[1] = new SqlParameter("@nameArrangementStatus", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameArrangementStatus;

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
            sqlParameter[4].Value = idArrangementStatus;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementStatus";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update Arrangement status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        #endregion

        #region Insert
        public bool InsertArrangement(int idArrType, string nameArrType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   ArrType(idArrType,nameArrType)
                                    VALUES(@idArrType,@nameArrType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameArrType", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameArrType;

            sqlParameter[1] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameter[1].Value = idArrType;

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
            sqlParameter[4].Value = idArrType.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert Arrangement type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool InsertCertificate(int idCertificate, string nameCertificate, string codeCertificate, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   Certificates(idCertificate,nameCertificate, codeCertificate )
                                    VALUES(@idCertificate,@nameCertificate, @codeCertificate)");

            SqlParameter[] sqlParameter = new SqlParameter[3];


            sqlParameter[0] = new SqlParameter("@nameCertificate", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameCertificate;
          

            sqlParameter[1] = new SqlParameter("@idCertificate", SqlDbType.Int);
            sqlParameter[1].Value = idCertificate;

            sqlParameter[2] = new SqlParameter("@codeCertificate", SqlDbType.NVarChar);
            sqlParameter[2].Value = codeCertificate;

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
            sqlParameter[4].Value = idCertificate.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCertificate";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Certificates";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert certificate";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertTraining(int idTraining, string nameTraining, string codeTraining, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   Training(idTraining,nameTraining, codeTraining)
                                    VALUES(@idTraining,@nameTraining , @codeTraining)");

            SqlParameter[] sqlParameter = new SqlParameter[3];


            sqlParameter[0] = new SqlParameter("@nameTraining", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameTraining;

            sqlParameter[1] = new SqlParameter("@idTraining", SqlDbType.Int);
            sqlParameter[1].Value = idTraining;

            sqlParameter[2] = new SqlParameter("@codeTraining", SqlDbType.NVarChar);
            sqlParameter[2].Value = codeTraining;

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
            sqlParameter[4].Value = idTraining.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTraining";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Training";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert training";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertArrangementStatus(int idArrangementStatus, string nameArrangementStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"SET IDENTITY_INSERT ArrangementStatus ON 
                                           INSERT INTO 
                                           ArrangementStatus
                                 (idArrangementStatus, nameArrangementStatus)
                                 VALUES (@idArrangementStatus, @nameArrangementStatus)

                                SET IDENTITY_INSERT ArrangementStatus  OFF");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idArrangementStatus", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementStatus;

            sqlParameter[1] = new SqlParameter("@nameArrangementStatus", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameArrangementStatus;

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
            sqlParameter[4].Value = conn.GetLastTableID("ArrangementStatus") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementStatus";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert arrangement status";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        #endregion

        #region Delete
        public bool DeleteArrType(int idArrType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrType WHERE idArrType = @idArrType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameter[0].Value = idArrType;

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
            sqlParameter[4].Value = idArrType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete arrangement type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteCertificates(int idCertificates, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Certificates WHERE idCertificate = @idCertificates");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idCertificates", SqlDbType.Int);
            sqlParameter[0].Value = idCertificates;

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
            sqlParameter[4].Value = idCertificates;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCertificates";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Certificates";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete certificates";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeteleTraining(int idTraining, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Training WHERE idTraining = @idTraining");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTraining", SqlDbType.Int);
            sqlParameter[0].Value = idTraining;

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
            sqlParameter[4].Value = idTraining;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTraining";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Training";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete training";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteArrangementStatus(int idArrangementStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementStatus WHERE idArrangementStatus = @idArrangementStatus");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangementStatus", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementStatus;

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
            sqlParameter[4].Value = idArrangementStatus;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementStatus";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Arrangement status";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete arrangement status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        #endregion

        #  region LastID
        public DataTable idArrangementType()
        {
            string query = string.Format(@"SELECT TOP 1 idArrType FROM  ArrType ORDER BY idArrType DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable idCertificateType()
        {
            string query = string.Format(@"SELECT TOP 1 idCertificate FROM  Certificates ORDER BY idCertificate DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable idTrainingType()
        {
            string query = string.Format(@"SELECT TOP 1 idTraining FROM  Training ORDER BY idTraining DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable idArrangementStatus()
        {
            string query = string.Format(@"SELECT TOP 1 idArrangementStatus FROM ArrangementStatus ORDER BY idArrangementStatus DESC");

            return conn.executeSelectQuery(query, null);
        }
        #endregion

        #region IsIn
        public DataTable isInArrType()
        {
            string query = string.Format(@"SELECT distinct CASE WHEN idArrType IS NOT NULL THEN idArrType  ELSE '-1' END AS ID 
                           FROM  ArrType");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInTrainingInVolFeatures(string codeTraining )
        {
            string query = string.Format(@"SELECT idFeatures AS ID,'' as name, 'vf' as type, codeTraining as code FROM VolFeatures WHERE codeTraining = @codeTraining ");
            
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@codeTraining", SqlDbType.NVarChar);
            sqlParameters[0].Value = codeTraining;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable isInCertificatesInVolFeatures(string codeCertificate)
        {
            string query = string.Format(@"SELECT  idFeatures AS ID,'' as name, 'vf' as type, codeCertificate as code FROM VolFeatures WHERE codeCertificate = @codeCertificate ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@codeCertificate", SqlDbType.NVarChar);
            sqlParameters[0].Value = codeCertificate;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        #endregion
    }
       

    


}