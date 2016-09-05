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
using System.Xml;

namespace BIS.DAO
{
    public class AccSepaDAO
    {
        private dbConnection conn;
        public AccSepaDAO()
        {
            conn = new dbConnection();

        }
        public int GetLastID()
        {
            return conn.GetLastTableID("AccSepa");
        }
        public DataTable GetSepaById(int idSepa)
        {
            string query = string.Format(@" SELECT   idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove
                                        FROM AccSepa  WHERE idSepa='" + idSepa.ToString() + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllSepa()
        {
            string query = string.Format(@" SELECT   idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove
                                         FROM AccSepa  ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllSepaProgress()
        {
            string query = string.Format(@" SELECT   idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove
                                         FROM AccSepa WHERE status = 1 ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllSepaFinal()
        {
            string query = string.Format(@" SELECT   idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove
                                         FROM AccSepa WHERE status = 2  ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllSepaXml()
        {
            string query = string.Format(@" SELECT   idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove
                                         FROM AccSepa WHERE status = 3 or status = 4  ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllSepaHistory()
        {
            string query = string.Format(@" SELECT   idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove
                                         FROM AccSepa WHERE status = 5  ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLastSepa()
        {
            string query = string.Format(@" SELECT  TOP 1 idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove 
                                        FROM AccSepa ORDER BY idSepa DESC  ");

            return conn.executeSelectQuery(query, null);
        }
      

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccSepa WHERE idSepa = @idSepa");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[0].Value = id;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            string query1 = string.Format(@"Update AccOpenLines set idSepa=0 WHERE idSepa = @idSepa");

            sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[0].Value = id;

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
            sqlParameter[3].Value = "DU";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSepa";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSepa";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public int SaveAndReturnID(AccSepaModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccSepa ( nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove )
                                 Values ( @nameSepa, @dtSepa,@amountSepa,@status,@sepaFinal,@dtCreationDate, @approveUser,@dtApprove ); SELECT SCOPE_IDENTITY()");


            SqlParameter[] sqlParameter = new SqlParameter[8];

            sqlParameter[0] = new SqlParameter("@nameSepa", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.nameSepa;

            sqlParameter[1] = new SqlParameter("@dtSepa", SqlDbType.DateTime);
            sqlParameter[1].Value = linemodel.dtSepa;

            sqlParameter[2] = new SqlParameter("@amountSepa", SqlDbType.Decimal);
            sqlParameter[2].Value = linemodel.amountSepa;

            sqlParameter[3] = new SqlParameter("@status", SqlDbType.Int);
            sqlParameter[3].Value = linemodel.status;

            sqlParameter[4] = new SqlParameter("@sepaFinal", SqlDbType.NVarChar);
            sqlParameter[4].Value = linemodel.sepaFInal;

            sqlParameter[5] = new SqlParameter("@dtCreationDate", SqlDbType.DateTime);
            sqlParameter[5].Value = linemodel.dtCreationDate;

            sqlParameter[6] = new SqlParameter("@approveUser", SqlDbType.Int);
            sqlParameter[6].Value = linemodel.approveUser;

            sqlParameter[7] = new SqlParameter("@dtApprove", SqlDbType.DateTime);
            sqlParameter[7].Value = linemodel.dtApprove;

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
            sqlParameter[3].Value = "DU";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = (conn.GetLastTableID("AccSepa")+1).ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSepa";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSepa";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save and return id";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public bool Save(AccSepaModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccSepa (idSepa, nameSepa, dtSepa,amountSepa,status,sepaFinal,dtCreationDate, approveUser,dtApprove )
                                 Values (@idSepa, @nameSepa, @dtSepa,@amountSepa,@status,@sepaFinal,@dtCreationDate, @approveUser,@dtApprove )");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@nameSepa", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.nameSepa;

            sqlParameter[1] = new SqlParameter("@dtSepa", SqlDbType.DateTime);
            sqlParameter[1].Value = linemodel.dtSepa;

            sqlParameter[2] = new SqlParameter("@amountSepa", SqlDbType.Decimal);
            sqlParameter[2].Value = linemodel.amountSepa;

            sqlParameter[3] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[3].Value = linemodel.idSepa;

            sqlParameter[4] = new SqlParameter("@status", SqlDbType.Int);
            sqlParameter[4].Value = linemodel.status;

            sqlParameter[5] = new SqlParameter("@sepaFinal", SqlDbType.NVarChar);
            sqlParameter[5].Value = linemodel.sepaFInal;

            sqlParameter[6] = new SqlParameter("@dtCreationDate", SqlDbType.DateTime);
            sqlParameter[6].Value = linemodel.dtCreationDate;

            sqlParameter[7] = new SqlParameter("@approveUser", SqlDbType.Int);
            sqlParameter[7].Value = linemodel.approveUser;

            sqlParameter[8] = new SqlParameter("@dtApprove", SqlDbType.DateTime);
            sqlParameter[8].Value = linemodel.dtApprove;

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
            sqlParameter[4].Value = linemodel.idSepa;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSepa";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSepa";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }



        public bool Update(AccSepaModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"UPDATE AccSepa SET  nameSepa=@nameSepa,dtSepa=@dtSepa,amountSepa=@amountSepa,status=@status,sepaFinal=@sepaFinal,
                                            dtCreationDate=@dtCreationDate, approveUser=@approveUser,dtApprove=@dtApprove
                                           WHERE idSepa=@idSepa ");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@nameSepa", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.nameSepa;

            sqlParameter[1] = new SqlParameter("@dtSepa", SqlDbType.DateTime);
            sqlParameter[1].Value = linemodel.dtSepa;

            sqlParameter[2] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[2].Value = linemodel.idSepa;

            sqlParameter[3] = new SqlParameter("@amountSepa", SqlDbType.Decimal);
            sqlParameter[3].Value = linemodel.amountSepa;

            sqlParameter[4] = new SqlParameter("@status", SqlDbType.Int);
            sqlParameter[4].Value = linemodel.status;

            sqlParameter[5] = new SqlParameter("@sepaFinal", SqlDbType.NVarChar);
            sqlParameter[5].Value = linemodel.sepaFInal;

            sqlParameter[6] = new SqlParameter("@dtCreationDate", SqlDbType.DateTime);
            sqlParameter[6].Value = linemodel.dtCreationDate;

            sqlParameter[7] = new SqlParameter("@approveUser", SqlDbType.Int);
            sqlParameter[7].Value = linemodel.approveUser;

            sqlParameter[8] = new SqlParameter("@dtApprove", SqlDbType.DateTime);
            sqlParameter[8].Value = linemodel.dtApprove;

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
            sqlParameter[4].Value = linemodel.idSepa;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSepa";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSepa";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}