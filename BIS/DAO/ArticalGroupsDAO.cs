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
    public class ArticalGroupsDAO
    {
        private dbConnection conn;

        public ArticalGroupsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllArticalGroups()
        {
            string query = string.Format(@"SELECT codeArticalGroup,nameArticalGroup,inkopArtical, la.descLedgerAccount as descInkopArtical,  verkopArtical,la2.descLedgerAccount as descVerkopArtical, isActive, idUserCreated,u.nameUser as nameUserCreated,ag.dtUserCreated,idUserModified,um.nameUser as nameUserModified,ag.dtUserModified,
                                            classArticalGroup
                                           FROM ArticalGroups ag LEFT OUTER JOIN AccLedgerAccount la ON ag.inkopArtical = la.numberLedgerAccount
                                           LEFT OUTER JOIN AccLedgerAccount la2 ON ag.verkopArtical = la2.numberLedgerAccount 
                                           LEFT OUTER JOIN Users u ON u.idUser=idUserCreated
                                           LEFT OUTER JOIN Users um ON um.idUser=idUserModified");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetArticalGroup(string group)
        {
            string query = string.Format(@"SELECT codeArticalGroup,nameArticalGroup,inkopArtical, la.descLedgerAccount as descInkopArtical,  verkopArtical,la2.descLedgerAccount as descVerkopArtical, isActive, idUserCreated,u.nameUser as nameUserCreated,ag.dtUserCreated,idUserModified,um.nameUser as nameUserModified,ag.dtUserModified,
                                            classArticalGroup
                                           FROM ArticalGroups ag LEFT OUTER JOIN AccLedgerAccount la ON ag.inkopArtical = la.numberLedgerAccount
                                           LEFT OUTER JOIN AccLedgerAccount la2 ON ag.verkopArtical = la2.numberLedgerAccount 
                                           LEFT OUTER JOIN Users u ON u.idUser=idUserCreated
                                           LEFT OUTER JOIN Users um ON um.idUser=idUserModified
                                            WHERE codeArticalGroup = '" + group + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllArticalClass()
        {
            string query = string.Format(@"SELECT DISTINCT classArticalGroup FROM ArticalGroups  ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetCodeFromArtical(string codeArticle)
        {
            string query = string.Format(@"SELECT ar.codeArtikalGroup AS codeArticalGroup
                                           FROM Artical as ar                                           
                                           WHERE ar.codeArtikalGroup = @codeArticle");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@codeArticle", SqlDbType.NVarChar);
            sqlParameters[0].Value = codeArticle;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkIfExist(string codeArticalGroup)
        {
            string query = string.Format(@"SELECT codeArticalGroup,nameArticalGroup,inkopArtical,verkopArtical,classArticalGroup
                                           FROM ArticalGroups WHERE codeArticalGroup='" +codeArticalGroup+"'");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean Save(ArticalGroupsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArticalGroups(codeArticalGroup,nameArticalGroup,inkopArtical,verkopArtical,isActive,idUserCreated,dtUserCreated,classArticalGroup)
                      VALUES (@codeArticalGroup,@nameArticalGroup,@inkopArtical,@verkopArtical,@isActive,@idUserCreated,@dtUserCreated,@classArticalGroup)");


            SqlParameter[] sqlParameter = new SqlParameter[8];
            sqlParameter[0] = new SqlParameter("@codeArticalGroup", SqlDbType.NVarChar);
            sqlParameter[0].Value = (model.codeArticalGroup == null) ? SqlString.Null : model.codeArticalGroup; 

            sqlParameter[1] = new SqlParameter("@nameArticalGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.nameArticalGroup == null) ? SqlString.Null : model.nameArticalGroup;

            sqlParameter[2] = new SqlParameter("@inkopArtical", SqlDbType.NVarChar);
            sqlParameter[2].Value = (model.inkopArtical == null) ? SqlString.Null : model.inkopArtical;

            sqlParameter[3] = new SqlParameter("@verkopArtical", SqlDbType.NVarChar);
            sqlParameter[3].Value = (model.verkopArtical == null) ? SqlString.Null : model.verkopArtical;

            sqlParameter[4] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameter[4].Value = model.isActive;

            sqlParameter[5] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[5].Value = (model.idUserCreated == null) ? SqlInt32.Null : model.idUserCreated;

            sqlParameter[6] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = (model.dtUserCreated == null) ? SqlDateTime.MinValue : model.dtUserCreated;

            sqlParameter[7] = new SqlParameter("@classArticalGroup", SqlDbType.NVarChar);
            sqlParameter[7].Value = (model.classArticalGroup == null) ? SqlString.Null : model.classArticalGroup;

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
            sqlParameter[4].Value = model.codeArticalGroup.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeArticalGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArticalGroups";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(ArticalGroupsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArticalGroups SET nameArticalGroup = @nameArticalGroup,inkopArtical= @inkopArtical,verkopArtical = @verkopArtical ,isActive = @isActive, 
                        classArticalGroup=@classArticalGroup,idUserModified = @idUserModified, dtUserModified = @dtUserModified,codeArticalGroup = @codeArticalGroup
                     
                      WHERE codeArticalGroup = @codeArticalGroup");


            SqlParameter[] sqlParameter = new SqlParameter[8];
            sqlParameter[0] = new SqlParameter("@codeArticalGroup", SqlDbType.NVarChar);
            sqlParameter[0].Value = (model.codeArticalGroup == null) ? SqlString.Null : model.codeArticalGroup;

            sqlParameter[1] = new SqlParameter("@nameArticalGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.nameArticalGroup == null) ? SqlString.Null : model.nameArticalGroup;

            sqlParameter[2] = new SqlParameter("@inkopArtical", SqlDbType.NVarChar);
            sqlParameter[2].Value = (model.inkopArtical == null) ? SqlString.Null : model.inkopArtical;

            sqlParameter[3] = new SqlParameter("@verkopArtical", SqlDbType.NVarChar);
            sqlParameter[3].Value = (model.verkopArtical == null) ? SqlString.Null : model.verkopArtical;

            sqlParameter[4] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameter[4].Value = model.isActive;

            sqlParameter[5] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[5].Value = (model.idUserModified == null) ? SqlInt32.Null : model.idUserModified;

            sqlParameter[6] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[6].Value = (model.dtUserModified == null) ? SqlDateTime.MinValue : model.dtUserModified;

            sqlParameter[7] = new SqlParameter("@classArticalGroup", SqlDbType.NVarChar);
            sqlParameter[7].Value = (model.classArticalGroup == null) ? SqlString.Null : model.classArticalGroup;

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
            sqlParameter[4].Value = model.codeArticalGroup.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeArticalGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArticalGroups";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public Boolean Delete(string codeGroup, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format("DELETE FROM ArticalGroups WHERE codeArticalGroup = @codeGroup");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("codeGroup", SqlDbType.NVarChar);
            sqlParameter[0].Value = codeGroup;

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
            sqlParameter[4].Value = codeGroup.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArticalGroups";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

    }


}