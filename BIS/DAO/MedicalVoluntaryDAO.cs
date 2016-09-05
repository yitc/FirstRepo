using BIS.Core;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
    public class MedicalVoluntaryDAO
    {

        private dbConnection conn;

        public MedicalVoluntaryDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetMedical(List<String> idQueryType, int idContPers, Boolean isDefaultSort, Boolean isAll)
        {
            string queryCondition = "where";
            string join = "inner join";
            string sort = "mq.questSort,txtQuest,  ma.ansSort";

            if (isDefaultSort == false)
            {
                sort = " mq.questSort,mqg.nameQuestGroup, ma.ansSort";  // ovde ubacena grupa 
            }

            if (isAll == false)
            {
                join = "left outer join";
                //=== saki
                sort = "mq.questSort";
            }


            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].ToString() + "'";
                }
                else
                {
                    queryCondition += " ma.idQueryType = '" + idQueryType[i].ToString() + "'";
                }
            }

            string selectQuery = "select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns, ma.txtAns,ma.idAnsType," +
                "mc.idCpr, CONVERT(NVARCHAR(2000),mc.txt) as txt,mq.questSort, ma.ansSort from MedQuest mq " + join + " MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup " + join + " MedAns ma on " +
                "ma.idQuest = mq.idQuest " + join + "  (select * from MedCpr where (idCpr = '" + idContPers + "' or idCpr is null)) mc on mc.idQuest = mq.idQuest and ma.idAns = mc.idAns and ma.idAns is not null " +
                " order by " + sort;

            if (queryCondition != "where")
            {

                selectQuery = "select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns, ma.txtAns,ma.idAnsType," +
                "mc.idCpr, CONVERT(NVARCHAR(2000),mc.txt) as txt,mq.questSort, ma.ansSort from MedQuest mq " + join + " MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup " + join + " MedAns ma on " +
                "ma.idQuest = mq.idQuest " + join + "  (select * from MedCpr where (idCpr = '" + idContPers + "' or idCpr is null)) mc on mc.idQuest = mq.idQuest and ma.idAns = mc.idAns and ma.idAns is not null " +
                " " + queryCondition + " order by " + sort;

            }

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetMedicalQuestGroup(int idCompany)
        {

            string selectQuery = " select MedQuestGroup.idQuestGroup,nameQuestGroup from MedQuestGroup WHERE MedQuestGroup.idCompany = '" + idCompany + "'";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetVoluntaryQuestGroup(int idCompany)
        {

            string selectQuery = " select VolQuestGroup.idQuestGroup,nameQuestGroup from VolQuestGroup WHERE VolQuestGroup.idCompany = '" + idCompany + "'";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetMedicalQuest(int idCompany, int idQuestGroup)
        {

            string selectQuery = "SELECT distinct MedQuest.idQuest,idQuestGroup,CONVERT(nvarchar(max),txtQuest) as txtQuest,questSort FROM MedQuest LEFT OUTER JOIN MedAns ON MedAns.idQuest = MedQuest.idQuest  WHERE idQuestGroup = '" + idQuestGroup + "' AND (idQuestGroup in (SELECT DISTINCT idQuestGroup FROM MedQuestGroup WHERE MedQuestGroup.idCompany = '" + idCompany + "') )  ORDER BY questSort";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetVoluntaryQuest(int idCompany, int idQuestGroup)
        {

            string selectQuery = "SELECT distinct VolQuest.idQuest,idQuestGroup,CONVERT(nvarchar(max),txtQuest) as txtQuest,questSort FROM VolQuest LEFT OUTER JOIN VolAns ON VolAns.idQuest = VolQuest.idQuest  WHERE idQuestGroup = '" + idQuestGroup + "' AND (idQuestGroup in (SELECT DISTINCT idQuestGroup FROM VolQuestGroup WHERE VolQuestGroup.idCompany = '" + idCompany + "') )  ORDER BY questSort";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetAnswerTypes()
        {

            string selectQuery = "SELECT idAnsType, nameAnsType FROM MedAnsType";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetVolAnswerTypes()
        {

            string selectQuery = "SELECT idAnsType, nameAnsType FROM VolAnsType";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetMedicalAnswer(int idQuest, List<int> Labels)
        {
            string condition = "";
            for (int i = 0; i < Labels.Count; i++)
            {
                if (i == 0)
                {
                    condition = " WHERE MedAns.idQueryType='" + Labels[i].ToString() + "'";
                }
                else
                {
                    condition = condition + " OR MedAns.idQueryType='" + Labels[i].ToString() + "'";
                }
            }
            string selectQuery = "SELECT DISTINCT idAns,idAnsType,idQuest,idQueryType,l.nameLabel,CONVERT(nvarchar(max),txtAns) as txtAns,ansSort,idQuestSkills FROM MedAns LEFT OUTER JOIN Labels l  ON l.idLabel = MedAns.idQueryType WHERE idQuest = '" + idQuest + "' ORDER BY ansSort";
            if (condition != "" && condition != null)
            {
                selectQuery = "SELECT DISTINCT idAns,idAnsType,idQuest,idQueryType,l.nameLabel,CONVERT(nvarchar(max),txtAns) as txtAns,ansSort,idQuestSkills FROM MedAns LEFT OUTER JOIN Labels l  ON l.idLabel = MedAns.idQueryType WHERE idQuest = '" + idQuest + "' AND (" + condition.Replace("WHERE", "") + ") ORDER BY ansSort";

            }
            return conn.executeSelectQuery(selectQuery, null);
        }

        public DataTable GetVoluntaryAnswer(int idQuest, List<int> Labels)
        {
            string condition = "";
            for (int i = 0; i < Labels.Count; i++)
            {
                if (i == 0)
                {
                    condition = " WHERE VolAns.idQueryType='" + Labels[i].ToString() + "'";
                }
                else
                {
                    condition = condition + " OR VolAns.idQueryType='" + Labels[i].ToString() + "'";
                }
            }
            string selectQuery = "SELECT DISTINCT idAns,idAnsType,idQuest,idQueryType,l.nameLabel,CONVERT(nvarchar(max),txtAns) as txtAns,ansSort FROM VolAns LEFT OUTER JOIN Labels l  ON l.idLabel = VolAns.idQueryType WHERE idQuest = '" + idQuest + "' ORDER BY ansSort";
            if (condition != "" && condition != null)
            {
                selectQuery = "SELECT DISTINCT idAns,idAnsType,idQuest,idQueryType,l.nameLabel,CONVERT(nvarchar(max),txtAns) as txtAns,ansSort FROM VolAns LEFT OUTER JOIN Labels l  ON l.idLabel = VolAns.idQueryType WHERE idQuest = '" + idQuest + "' AND (" + condition.Replace("WHERE", "") + ") ORDER BY ansSort";

            }
            return conn.executeSelectQuery(selectQuery, null);
        }



        public DataTable GetMedicalQuest(List<int> idQueryType)
        {
            string queryCondition = "where";

            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].ToString() + "'";
                }
                else
                {
                    queryCondition += " ma.idQueryType = '" + idQueryType[i].ToString() + "'";
                }
            }
            string selectQuery = @"select mqg.nameQuestGroup, mqg.idQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
                                     ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from MedQuest mq 
                                     left outer join MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                     left outer join MedAns ma on ma.idQuest = mq.idQuest  
                                     left outer join MedAnsType mat on ma.idAnsType = mat.idAnsType order by txtQuest, mq.questSort, ma.ansSort";
            if (queryCondition != "where")
            {

                selectQuery = @"select mqg.nameQuestGroup,mqg.idQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
                                 ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from MedQuest mq 
                                 left outer join MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                 left outer join MedAns ma on ma.idQuest = mq.idQuest  
                                 left outer join MedAnsType mat on ma.idAnsType = mat.idAnsType " + queryCondition + " order by txtQuest, mq.questSort, ma.ansSort";


            }
            return conn.executeSelectQuery(selectQuery, null);



        }

        public DataTable GetVoluntaryQuest(List<int> idQueryType)
        {
            string queryCondition = "where";

            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].ToString() + "'";
                }
                else
                {
                    queryCondition += " ma.idQueryType = '" + idQueryType[i].ToString() + "'";
                }
            }
            string selectQuery = @"select mqg.nameQuestGroup, mqg.idQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
                                     ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from VolQuest mq 
                                     left outer join VolQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                     left outer join VolAns ma on ma.idQuest = mq.idQuest  
                                     left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType order by txtQuest, mq.questSort, ma.ansSort";
            if (queryCondition != "where")
            {

                selectQuery = @"select mqg.nameQuestGroup,mqg.idQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
                                 ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from VolQuest mq 
                                 left outer join VolQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                 left outer join VolAns ma on ma.idQuest = mq.idQuest  
                                 left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType " + queryCondition + " order by txtQuest, mq.questSort, ma.ansSort";


            }
            return conn.executeSelectQuery(selectQuery, null);



        }

        public Boolean InsertMedicalQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string insertQuery = " INSERT INTO MedQuestGroup (nameQuestGroup,idCompany) VALUES (@nameQuestGroup,@idCompany)";

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idCompany", SqlDbType.Int);
            sqlParameter[0].Value = qg.idCompany;

            sqlParameter[1] = new SqlParameter("@nameQuestGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.nameQuestGroup == null) ? "" : qg.nameQuestGroup;

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("MedQuestGroup") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuestGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedQuestGroup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert medical question group";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdateMedicalQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string updateQuery = "UPDATE MedQuestGroup SET nameQuestGroup = @nameQuestGroup WHERE idQuestGroup = @idQuestGroup";

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuestGroup;

            sqlParameter[1] = new SqlParameter("@nameQuestGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.nameQuestGroup == null) ? SqlString.Null : qg.nameQuestGroup;

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);

            updateQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = qg.idQuestGroup;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuestGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedQuestGroup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update medical question group";

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean DeleteMedicalQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string deleteQuery = "DELETE FROM MedQuestGroup WHERE idQuestGroup = @idQuestGroup";

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuestGroup;

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            deleteQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = qg.idQuestGroup;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuestGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedQuestGroup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete medical question group";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean InsertVoluntaryQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string insertQuery = " INSERT INTO VolQuestGroup (nameQuestGroup,idCompany) VALUES (@nameQuestGroup,@idCompany)";

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idCompany", SqlDbType.Int);
            sqlParameter[0].Value = qg.idCompany;

            sqlParameter[1] = new SqlParameter("@nameQuestGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.nameQuestGroup == null) ? "" : qg.nameQuestGroup;

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("VolQuestGroup") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuestGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Multimedia";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update multimedia";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdateVoluntaryQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string updateQuery = "UPDATE VolQuestGroup SET nameQuestGroup = @nameQuestGroup WHERE idQuestGroup = @idQuestGroup";

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuestGroup;

            sqlParameter[1] = new SqlParameter("@nameQuestGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.nameQuestGroup == null) ? SqlString.Null : qg.nameQuestGroup;

             _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);

            updateQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = qg.idQuestGroup;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuestGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolQuestGroup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update voluntary question group";

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean DeleteVoluntaryQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string deleteQuery = "DELETE FROM VolQuestGroup WHERE idQuestGroup = @idQuestGroup";

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuestGroup;

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            deleteQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = qg.idQuestGroup;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuestGroup";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolQuestGroup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete voluntary question group";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean InsertMedicalQuestion(QuestModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string insertQuery = " INSERT INTO MedQuest (idQuestGroup,txtQuest,questSort) VALUES (@idQuestGroup,@txtQuest,@questSort)";

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuestGroup;

            sqlParameter[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.txtQuest == null) ? "" : qg.txtQuest;

            //sqlParameters[2] = new SqlParameter("@questSort", SqlDbType.Int);
            sqlParameter[2] = new SqlParameter("@questSort", SqlDbType.Decimal);
            sqlParameter[2].Value = qg.questSort;

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("MedQuest") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuest";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert medical question";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdateMedicalQuestion(QuestModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string updateQuery = "UPDATE MedQuest SET txtQuest = @txtQuest, questSort = @QuestSort WHERE idQuest = @idQuest";

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuest;

            sqlParameter[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.txtQuest == null) ? SqlString.Null : qg.txtQuest;

            //sqlParameters[2] = new SqlParameter("@questSort", SqlDbType.Int);
            sqlParameter[2] = new SqlParameter("@questSort", SqlDbType.Decimal);
            sqlParameter[2].Value = qg.questSort;

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);

            updateQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = qg.idQuest;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuest";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update medical question";

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }


        public Boolean InsertVoluntaryQuestion(QuestModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string insertQuery = " INSERT INTO VolQuest (idQuestGroup,txtQuest,questSort) VALUES (@idQuestGroup,@txtQuest,@questSort)";

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuestGroup;

            sqlParameter[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.txtQuest == null) ? "" : qg.txtQuest;

            sqlParameter[2] = new SqlParameter("@questSort", SqlDbType.Int);
            sqlParameter[2].Value = qg.questSort;

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("VolQuest") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuest";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert voluntary question";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdateVoluntaryQuestion(QuestModel qg, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string updateQuery = "UPDATE VolQuest SET txtQuest = @txtQuest, questSort = @QuestSort WHERE idQuest = @idQuest";

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[0].Value = qg.idQuest;

            sqlParameter[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameter[1].Value = (qg.txtQuest == null) ? SqlString.Null : qg.txtQuest;

            sqlParameter[2] = new SqlParameter("@questSort", SqlDbType.Int);
            sqlParameter[2].Value = qg.questSort;

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);

            updateQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = qg.idQuest;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuest";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update voluntary question";

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

     
        public DataTable getLastAnswerId()
        {
            string selectQuery = "select top 1 idAns from MedAns order by idAns desc";

            return conn.executeSelectQuery(selectQuery, null);
        }

        public Boolean InsertMedicalAnswer(AnswerModel a, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string insertQuery = " INSERT INTO MedAns (idAns,idAnsType,idQuest,idQueryType,txtAns,ansSort,idQuestSkills) VALUES (@idAns,@idAnsType,@idQuest,@idQueryType,@txtAns,@ansSort,@idQuestSkills)";

            SqlParameter[] sqlParameter = new SqlParameter[7];

            sqlParameter[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[0].Value = a.idAns;

            sqlParameter[1] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[1].Value = a.idAnsType;

            sqlParameter[2] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[2].Value = a.idQuest;

            sqlParameter[3] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameter[3].Value = a.idQueryType;

            sqlParameter[4] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameter[4].Value = (a.txtAns == null) ? "" : a.txtAns;

            sqlParameter[5] = new SqlParameter("@ansSort", SqlDbType.Int);
            sqlParameter[5].Value = a.ansSort;


            sqlParameter[6] = new SqlParameter("@idQuestSkills", SqlDbType.Int);
            sqlParameter[6].Value = (a.idQuestSkills == -1 || a.idQuestSkills == null) ? SqlInt32.Null : Convert.ToInt32(a.idQuestSkills);


            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = a.idAns.ToString() + "_" + a.idQueryType.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAns_idQueryType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedAns";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert medical answer";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdateMedicalAnswer(AnswerModel a, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string updateQuery = "UPDATE MedAns set idAnsType = @idAnsType,txtAns = @txtAns,ansSort=@ansSort,idQuestSkills=@idQuestSkills  where idAns=@idAns";

            SqlParameter[] sqlParameter = new SqlParameter[5];


            sqlParameter[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[0].Value = a.idAnsType;

            sqlParameter[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameter[1].Value = (a.txtAns == null) ? "" : a.txtAns;

            sqlParameter[2] = new SqlParameter("@ansSort", SqlDbType.Int);
            sqlParameter[2].Value = a.ansSort;

            sqlParameter[3] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[3].Value = a.idAns;

            sqlParameter[4] = new SqlParameter("@idQuestSkills", SqlDbType.Int);
            sqlParameter[4].Value = (a.idQuestSkills == -1 || a.idQuestSkills == null) ? SqlInt32.Null : Convert.ToInt32(a.idQuestSkills);


            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);

            updateQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = a.idAns;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAns";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedAns";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update medical answer";

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }


        public DataTable getLastVolAnswerId()
        {
            string selectQuery = "select top 1 idAns from VolAns order by idAns desc";

            return conn.executeSelectQuery(selectQuery, null);
        }

        public Boolean InsertVoluntaryAnswer(AnswerModel a, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string insertQuery = " INSERT INTO VolAns (idAns,idAnsType,idQuest,idQueryType,txtAns,ansSort) VALUES (@idAns,@idAnsType,@idQuest,@idQueryType,@txtAns,@ansSort)";

            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[0].Value = a.idAns;

            sqlParameter[1] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[1].Value = a.idAnsType;

            sqlParameter[2] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[2].Value = a.idQuest;

            sqlParameter[3] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameter[3].Value = a.idQueryType;

            sqlParameter[4] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameter[4].Value = (a.txtAns == null) ? "" : a.txtAns;

            sqlParameter[5] = new SqlParameter("@ansSort", SqlDbType.Int);
            sqlParameter[5].Value = a.ansSort;

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = a.idAns.ToString() + "_" + a.idAnsType.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAns_idAnsType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAns";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert voluntary answer";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateVoluntaryAnswer(AnswerModel a, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string updateQuery = "UPDATE VolAns set idAnsType = @idAnsType,txtAns = @txtAns,ansSort=@ansSort where idAns=@idAns";

            SqlParameter[] sqlParameter = new SqlParameter[4];


            sqlParameter[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[0].Value = a.idAnsType;

            sqlParameter[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameter[1].Value = (a.txtAns == null) ? "" : a.txtAns;

            sqlParameter[2] = new SqlParameter("@ansSort", SqlDbType.Int);
            sqlParameter[2].Value = a.ansSort;

            sqlParameter[3] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[3].Value = a.idAns;


            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);

            updateQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = a.idAns;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAns";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAns";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update voluntary answer";

            _query.Add(updateQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

       
        public DataTable GetVoluntary(List<String> idQueryType, int idContPers, Boolean isDefaultSort, Boolean isAll)
        {
            string queryCondition = "where";
            string join = "inner join";
            string sort = "questSort,txtQuest,  ansSort";

            if (isDefaultSort == false)
            {
                sort = " questSort,nameQuestGroup, ansSort";  // ovde ubacena grupa 
            }

            if (isAll == false)
            {
                join = "left outer join";
                sort = "questSort";
            }


            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " va.idQueryType =  '" + idQueryType[i].ToString() + "'";
                }
                else
                {
                    queryCondition += " va.idQueryType = '" + idQueryType[i].ToString() + "'";
                }
            }

            string selectQuery = "select distinct nameQuestGroup,idQuest,txtQuest,idAns,txtAns, idAnsType,idCpr, txt, questSort, ansSort FROM (select distinct vqg.nameQuestGroup,(SELECT TOP 1 idQuest FROM VolQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest,(SELECT TOP 1 idAns FROM VolAns v LEFT OUTER JOIN VolQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns) ) as idAns, va.txtAns,va.idAnsType," +
                "vc.idCpr, CONVERT(NVARCHAR(2000),vc.txt) as txt,(SELECT TOP 1 questSort FROM VolQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as questSort, (SELECT TOP 1 ansSort FROM VolAns v LEFT OUTER JOIN VolQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns))  as ansSort  from VolQuest vq " + join + " VolQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolAns va on " +
                "va.idQuest = vq.idQuest " + join + "  (select vcp.idcpr, vqu.txtQuest, van.txtAns,vcp.txt from VolCpr vcp " + join + @" VolQuest vqu ON vqu.idQuest = vcp.idQuest
               " + join + " VolAns van ON van.idAns = vcp.idAns where (idCpr = '" + idContPers + "' or idCpr is null)) vc on CONVERT(NVARCHAR(2000),vc.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest) and CONVERT(NVARCHAR(2000),va.txtAns) = CONVERT(NVARCHAR(2000),vc.txtAns)  and va.idAns is not null " +
                " ) cc order by " + sort;

            if (queryCondition != "where")
            {

                selectQuery = "select distinct nameQuestGroup,idQuest,txtQuest,idAns,txtAns, idAnsType,idCpr, txt, questSort, ansSort FROM (select distinct vqg.nameQuestGroup,(SELECT TOP 1 idQuest FROM VolQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, (SELECT TOP 1 idAns FROM VolAns v LEFT OUTER JOIN VolQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns) and  (" + queryCondition.Replace("where", "") + @" ) ) as idAns, va.txtAns,va.idAnsType," +
                "vc.idCpr, CONVERT(NVARCHAR(2000),vc.txt) as txt,(SELECT TOP 1 questSort FROM VolQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as questSort, (SELECT TOP 1 ansSort FROM VolAns v LEFT OUTER JOIN VolQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns))  as ansSort  from VolQuest vq " + join + " VolQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolAns va on " +
                "va.idQuest = vq.idQuest " + join + "  (select vcp.idcpr, vqu.txtQuest, van.txtAns,vcp.txt from VolCpr vcp " + join + @" VolQuest vqu ON vqu.idQuest = vcp.idQuest
               " + join + " VolAns van ON van.idAns = vcp.idAns where (idCpr = '" + idContPers + "' or idCpr is null)) vc ON CONVERT(NVARCHAR(2000),vc.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest) and CONVERT(NVARCHAR(2000),va.txtAns) = CONVERT(NVARCHAR(2000),vc.txtAns)  and  va.idAns is not null " +
                " " + queryCondition + " ) cc order by " + sort;

            }

            return conn.executeSelectQuery(selectQuery, null);

        }
        public DataTable GetSameQuestAnswer(string idQuest, string idAns)
        {

            string selectQuery = @"SELECT va.idQuest, idAns FROM VolAns va
                                LEFT OUTER JOIN VolQuest vq ON va.idQuest = vq.idQuest
                                WHERE  RTRIM(LTRIM(CONVERT(NVARCHAR(2000),vq.txtQuest))) + RTRIM(LTRIM(CONVERT(NVARCHAR(2000),va.txtAns))) = 
                                ( SELECT RTRIM(LTRIM(CONVERT(NVARCHAR(2000),vq.txtQuest))) + RTRIM(LTRIM(CONVERT(NVARCHAR(2000),va.txtAns))) FROM  VolAns va
                                LEFT OUTER JOIN VolQuest vq ON va.idQuest = vq.idQuest WHERE va.idQuest='" + idQuest + "' AND va.idAns= '" + idAns + "')";

            return conn.executeSelectQuery(selectQuery, null);

        }


        public DataTable GetVoluntaryForArrangement(List<String> idQueryType, int idArrangement, Boolean isDefaultSort, Boolean isAll)
        {

            string queryCondition = "where";
            string join = "inner join";
            string sort = " vq.questSort,txtQuest,  ansSort";

            if (isDefaultSort == false)
            {
                sort = " vq.questSort,nameQuestGroup, ansSort";  // ovde ubacena grupa 
            }

            if (isAll == false)
            {
                join = "left outer join";
                sort = " vq.questSort";
            }

            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " va.idQueryType =  '" + idQueryType[i].ToString() + "'";
                }
                else
                {
                    queryCondition += " va.idQueryType = '" + idQueryType[i].ToString() + "'";
                }
            }

            string selectQuery = "select distinct vqg.nameQuestGroup,vq.idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, va.idAns, va.txtAns,va.idAnsType," +
                "vc.idArr, CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolQuest vq " + join + " VolQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolAns va on " +
                "va.idQuest = vq.idQuest " + join + "  (select * from VolArr where (idArr = '" + idArrangement + "' or idArr is null)) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns  " +
                " order by " + sort;

            if (queryCondition != "where")
            {

                selectQuery = "select distinct vqg.nameQuestGroup,vq.idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, va.idAns, va.txtAns,va.idAnsType," +
                "vc.idArr, CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolQuest vq " + join + " VolQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolAns va on " +
                "va.idQuest = vq.idQuest " + join + "  (select * from VolArr where (idArr = '" + idArrangement + "' or idArr is null)) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns  " +
                " " + queryCondition + " order by " + sort;

            }

            return conn.executeSelectQuery(selectQuery, null);

        }


        public Boolean SaveVoluntary(MedicalVoluntaryModel vol, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            //string dateTimeChanged = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            string insertQuery = "insert into VolCpr (idcpr,idQuest,idAns,txt) values( @idcpr,@idQuest,@idAns,@txt)";

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idcpr", SqlDbType.Int);
            sqlParameter[0].Value = vol.idcpr;

            sqlParameter[1] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[1].Value = vol.idQuest;

            sqlParameter[2] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[2].Value = vol.idAns;

            sqlParameter[3] = new SqlParameter("@txt", SqlDbType.NVarChar);
            sqlParameter[3].Value = (vol.txt == null) ? SqlString.Null : vol.txt;

            //sqlParameters[4] = new SqlParameter("@dtm", SqlDbType.DateTime);
            //sqlParameters[4].Value = (Convert.ToDateTime(dateTimeChanged) == null || Convert.ToDateTime(dateTimeChanged) == DateTime.MinValue) ? SqlDateTime.Null : Convert.ToDateTime(dateTimeChanged);



            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("VolCpr") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idVol";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolCpr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save voluntary";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean SaveVoluntaryArrangement(MedicalVoluntaryArrangementModel vol, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            //string dateTimeChanged = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            string insertQuery = "insert into VolArr (idArr,idQuest,idAns,txt) values( @idArr,@idQuest,@idAns,@txt)";

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idArr", SqlDbType.Int);
            sqlParameter[0].Value = vol.idArr;

            sqlParameter[1] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[1].Value = vol.idQuest;

            sqlParameter[2] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[2].Value = vol.idAns;

            sqlParameter[3] = new SqlParameter("@txt", SqlDbType.NVarChar);
            sqlParameter[3].Value = (vol.txt == null) ? SqlString.Null : vol.txt;

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("VolArr") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idVol";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolArr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save voluntary arrangement";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean DeleteVoluntary(int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string deleteQuery = "delete from VolCpr where idcpr = @idcpr";

            SqlParameter[] sqlParameter = new SqlParameter[1];


            sqlParameter[0] = new SqlParameter("@idcpr", SqlDbType.Int);
            sqlParameter[0].Value = idContPers;

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            deleteQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = idContPers;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idcpr";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolCpr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete voluntary";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean DeleteVoluntaryArrangement(int idArrangement, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string deleteQuery = "delete from VolArr where idArr = @idArr";

            SqlParameter[] sqlParameter = new SqlParameter[1];


            sqlParameter[0] = new SqlParameter("@idArr", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            deleteQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = idArrangement;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArr";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolArr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete voluntary arrangement";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean SaveMedical(MedicalVoluntaryModel med, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            //  string dateTimeChanged = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            string insertQuery = "insert into MedCpr (idcpr,idQuest,idAns,txt) values( @idcpr,@idQuest,@idAns,@txt)";
            //,dtm  ,@dtm
            SqlParameter[] sqlParameter = new SqlParameter[4];


            sqlParameter[0] = new SqlParameter("@idcpr", SqlDbType.Int);
            sqlParameter[0].Value = med.idcpr;

            sqlParameter[1] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[1].Value = med.idQuest;

            sqlParameter[2] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[2].Value = med.idAns;

            sqlParameter[3] = new SqlParameter("@txt", SqlDbType.NVarChar);
            sqlParameter[3].Value = (med.txt == null) ? SqlString.Null : med.txt;

            //sqlParameters[4] = new SqlParameter("@dtm", SqlDbType.DateTime);
            //sqlParameters[4].Value = (Convert.ToDateTime(dateTimeChanged) == null || Convert.ToDateTime(dateTimeChanged) == DateTime.MinValue) ? SqlDateTime.Null : Convert.ToDateTime(dateTimeChanged);


            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);

            insertQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = conn.GetLastTableID("MedCpr") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMed";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedCpr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save medical";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean DeleteMedical(int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string deleteQuery = "delete from MedCpr where idcpr = @idcpr";

            SqlParameter[] sqlParameter = new SqlParameter[1];


            sqlParameter[0] = new SqlParameter("@idcpr", SqlDbType.Int);
            sqlParameter[0].Value = idContPers;

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            deleteQuery = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
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
            sqlParameter[4].Value = idContPers;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idcpr";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedCpr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete med cpr";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        //public DataTable GetMedicalForBooking(List<String> idQueryType)
        //{
        //    string queryCondition = "where";

        //    for (int i = 0; i < idQueryType.Count; i++)
        //    {
        //        if (queryCondition != "where")
        //        {
        //            queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].ToString() + "'";
        //        }
        //        else
        //        {
        //            queryCondition += " ma.idQueryType = '" + idQueryType[i].ToString() + "'";
        //        }
        //    }

        //    string selectQuery = "select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) + '  (' +  ma.txtAns + ')'  as txtQuest, ma.idAns, ma.txtAns,ma.idAnsType," +
        //        "mc.idCpr, CONVERT(NVARCHAR(2000),mc.txt) as txt,mq.questSort, ma.ansSort from MedQuest mq inner join MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup inner join MedAns ma on " +
        //        "ma.idQuest = mq.idQuest INNER JOIN  (select * from MedCpr) mc on mc.idQuest = mq.idQuest and ma.idAns = mc.idAns  " +
        //        " order by mq.questSort,txtQuest,  ma.ansSort";

        //    if (queryCondition != "where")
        //    {

        //        selectQuery = "select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) + '  (' + ma.txtAns + ')'   as txtQuest, ma.idAns, ma.txtAns,ma.idAnsType," +
        //        "mc.idCpr, CONVERT(NVARCHAR(2000),mc.txt) as txt,mq.questSort, ma.ansSort from MedQuest mq inner join MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup inner join MedAns ma on " +
        //        "ma.idQuest = mq.idQuest INNER JOIN  (select * from MedCpr) mc on mc.idQuest = mq.idQuest and ma.idAns = mc.idAns  " +
        //        " " + queryCondition + " order by mq.questSort,txtQuest,  ma.ansSort";

        //    }

        //    return conn.executeSelectQuery(selectQuery, null);

        //}
        public DataTable GetMedicalForBooking(List<String> idQueryType)
        {
            string queryCondition = "where";

            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].ToString() + "'";
                }
                else
                {
                    queryCondition += " ma.idQueryType = '" + idQueryType[i].ToString() + "'";
                }
            }

            string selectQuery = "select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) + '  (' +  ma.txtAns + ')'  as txtQuest, ma.idAns, ma.txtAns,ma.idAnsType," +
                " mq.questSort, ma.ansSort from MedQuest mq inner join MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup inner join MedAns ma on " +
                "ma.idQuest = mq.idQuest INNER JOIN  (select * from MedCpr) mc on mc.idQuest = mq.idQuest and ma.idAns = mc.idAns  " +
                " order by mq.questSort,txtQuest,  ma.ansSort";

            if (queryCondition != "where")
            {

                selectQuery = "select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) + '  (' + ma.txtAns + ')'   as txtQuest, ma.idAns, ma.txtAns,ma.idAnsType," +
                " mq.questSort, ma.ansSort from MedQuest mq inner join MedQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup inner join MedAns ma on " +
                "ma.idQuest = mq.idQuest INNER JOIN  (select * from MedCpr) mc on mc.idQuest = mq.idQuest and ma.idAns = mc.idAns  " +
                " " + queryCondition + " order by mq.questSort,txtQuest,  ma.ansSort";

            }

            return conn.executeSelectQuery(selectQuery, null);

        }
        // NOVO



        public DataTable checkAnsIsInMedCpr(int idAns, int idQueryType)
        {
            string query = string.Format(@"SELECT distinct mcpr.idAns,mcpr.idcpr, cpl.idLabel
                                           FROM MedAns ma
                                           INNER JOIN MedCpr mcpr on ma.idAns= mcpr.idAns
                                           LEFT OUTER JOIN  ContactPersonLabel cpl on cpl.idLabel = ma.idQueryType
                                           WHERE ma.idAns = '" + idAns + "' AND ma.idQueryType='" + idQueryType + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameters[0].Value = idAns;

            sqlParameters[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameters[1].Value = idQueryType;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable checkIsOneAns(AnswerModel ma)
        {
            string query = string.Format(@"SELECT distinct ma.idAns, ma.idQueryType
                                           FROM MedAns ma                                 
                                           WHERE ma.idQuest = '" + ma.idQuest + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameters[0].Value = ma.idQuest;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkIsOneQuest(int idGroup)
        {
            string query = string.Format(@"SELECT distinct ma.idQuest
                                           FROM MedQuest ma                                 
                                           WHERE  ma.idQuestGroup='" + idGroup + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idGroup", SqlDbType.Int);
            sqlParameters[0].Value = idGroup;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean DeleteAnsSript(AnswerModel ma, bool quest, bool group, int idQuestGroup, string nameForm, int idUser)
        {
            
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            if (ma.idAns != -1)
            {
                string query = string.Format(@"DELETE FROM MedAns WHERE idAns = @idAns AND idQueryType=@idQueryType");

                SqlParameter[] sqlParameter = new SqlParameter[2];
                sqlParameter[0] = new SqlParameter("@idAns", SqlDbType.Int);
                sqlParameter[0].Value = ma.idAns;

                sqlParameter[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
                sqlParameter[1].Value = ma.idQueryType;


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
                sqlParameter[4].Value = ma.idAns;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idAns";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "Multimedia";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete med ans";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                
            }
            if (quest == true )
            {
                if (ma.idQuest != -1)
                {
                    string query = string.Format(@"DELETE FROM MedQuest WHERE idQuest = @idQuest");

                    SqlParameter[] sqlParameter = new SqlParameter[1];
                    sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
                    sqlParameter[0].Value = ma.idQuest;

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
                    sqlParameter[4].Value = ma.idQuest;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idQuest";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "MedQuest";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete med quest ";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);

                }

                if (group == true)
                {
                   
                    string query = string.Format(@"DELETE FROM MedQuestGroup WHERE idQuestGroup = @idQuestGroup");

                    SqlParameter[] sqlParameter = new SqlParameter[1];
                    sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
                    sqlParameter[0].Value = idQuestGroup;

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
                    sqlParameter[4].Value = idQuestGroup;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idQuestGroup";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "MedQuestGroup";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete med quest group";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);

                }
            }


            //      string query = string.Format(@"DELETE FROM MedAns WHERE idAns = @idAns");
            //           

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable checkIsOneQuestInGroup(QuestModel ma)
        {
            string query = string.Format(@"SELECT distinct ma.idQuest
                                           FROM MedQuest ma                                 
                                           WHERE  ma.idQuestGroup='" + ma.idQuestGroup + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameters[0].Value = ma.idQuestGroup;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean DeleteQuestSript(QuestModel ma, bool group, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MedQuest WHERE idQuest = @idQuest");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[0].Value = ma.idQuest;

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
            sqlParameter[4].Value = ma.idQuest;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuest";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete med quest";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            if (group == true)
            {
                query = string.Format(@"DELETE FROM MedQuestGroup WHERE idQuestGroup = @idQuestGroup");

                sqlParameter = new SqlParameter[1];
                sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
                sqlParameter[0].Value = ma.idQuestGroup;

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
                sqlParameter[4].Value = ma.idQuestGroup;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idQuestGroup";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "MedQuestGroup";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete med quest group";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }



            //      string query = string.Format(@"DELETE FROM MedAns WHERE idAns = @idAns");
            //           

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean DeletetSript(AnswerModel am, bool questOne, bool group, int idQuestGroup, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MedAns WHERE idAns = @idAns");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameter[0].Value = am.idAns;

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
            sqlParameter[4].Value = am.idAns;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAns";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedAns";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update med ans";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);
            if (questOne == true)
            {
                query = string.Format(@"DELETE FROM MedQuest WHERE idQuest = @idQuest");

                sqlParameter = new SqlParameter[1];
                sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
                sqlParameter[0].Value = am.idQuest;

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
                sqlParameter[4].Value = am.idQuest;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idQuest";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "MedQuest";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update med quest";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
                if (group == true)
                {
                    query = string.Format(@"DELETE FROM MedQuestGroup WHERE idQuestGroup = @idQuestGroup");

                    sqlParameter = new SqlParameter[1];
                    sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
                    sqlParameter[0].Value = idQuestGroup;

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
                    sqlParameter[4].Value = idQuestGroup;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idQuestGroup";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "MedQuestGroup";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete med quest group";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);
                }

            }

            //      string query = string.Format(@"DELETE FROM MedAns WHERE idAns = @idAns");
            //           

            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public DataTable checkAnsForQuestDevice(int idQuest)
        {
            string query = string.Format(@"SELECT distinct ma.idAns                                          
                                           FROM MedAns ma
                                           WHERE ma.idQuest = '" + @idQuest + "'");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[0].Value = idQuest;

            return conn.executeSelectQuery(query, sqlParameter);
        }
        //=============
        // Volontary novo:
        public DataTable checkAnsIsInVolCpr(int idAns, int idQueryType)
        {
            string query = string.Format(@"SELECT distinct mcpr.idAns,mcpr.idcpr, cpl.idLabel
                                           FROM VolAns ma
                                           INNER JOIN VolCpr mcpr on ma.idAns= mcpr.idAns
                                           LEFT OUTER JOIN  ContactPersonLabel cpl on cpl.idLabel = ma.idQueryType
                                           WHERE ma.idAns = '" + idAns + "' AND ma.idQueryType='" + idQueryType + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameters[0].Value = idAns;

            sqlParameters[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameters[1].Value = idQueryType;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkAnsIsInVolArr(int idAns, int idQueryType)
        {
            string query = string.Format(@"SELECT distinct mcpr.idAns
                                           FROM VolAns ma
                                           INNER JOIN VolArr mcpr on ma.idAns= mcpr.idAns
                                           WHERE ma.idAns = '" + idAns + "' AND ma.idQueryType='" + idQueryType + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameters[0].Value = idAns;

            sqlParameters[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameters[1].Value = idQueryType;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable checkIsOneAnsVol(AnswerModel ma)
        {
            string query = string.Format(@"SELECT distinct ma.idAns, ma.idQueryType
                                           FROM VolAns ma                                 
                                           WHERE ma.idQuest = '" + ma.idQuest + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameters[0].Value = ma.idQuest;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkIsOneQuestVol(int idGroup)
        {
            string query = string.Format(@"SELECT distinct ma.idQuest
                                           FROM VolQuest ma                                 
                                           WHERE  ma.idQuestGroup='" + idGroup + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idGroup", SqlDbType.Int);
            sqlParameters[0].Value = idGroup;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean DeleteAnsSriptVol(AnswerModel ma, bool quest, bool group, int idQuestGroup, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            if (ma.idAns != -1)
            {
                string query = string.Format(@"DELETE FROM VolAns WHERE idAns = @idAns AND idQueryType=@idQueryType");

                SqlParameter[] sqlParameter = new SqlParameter[2];
                sqlParameter[0] = new SqlParameter("@idAns", SqlDbType.Int);
                sqlParameter[0].Value = ma.idAns;

                sqlParameter[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
                sqlParameter[1].Value = ma.idQueryType;


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
                sqlParameter[4].Value = ma.idAns;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idAns";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "VolAns";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete vol ans";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }
            if (quest == true)
            {
                if (ma.idQuest != -1)
                {
                    string query = string.Format(@"DELETE FROM VolQuest WHERE idQuest = @idQuest");

                    SqlParameter[] sqlParameter = new SqlParameter[1];
                    sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
                    sqlParameter[0].Value = ma.idQuest;

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
                    sqlParameter[4].Value = ma.idQuest;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idQuest";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "VolQuest";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete vol quest";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);
                }

                if (group == true)
                {

                    string query = string.Format(@"DELETE FROM VolQuestGroup WHERE idQuestGroup = @idQuestGroup");

                    SqlParameter[] sqlParameter = new SqlParameter[1];
                    sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
                    sqlParameter[0].Value = idQuestGroup;

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
                    sqlParameter[4].Value = idQuestGroup;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idQuestGroup";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "VolQuestGroup";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete vol quest group";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);
                }
            }


            //      string query = string.Format(@"DELETE FROM MedAns WHERE idAns = @idAns");
            //           

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable checkIsOneQuestInGroupVol(QuestModel ma)
        {
            string query = string.Format(@"SELECT distinct ma.idQuest
                                           FROM VolQuest ma                                 
                                           WHERE  ma.idQuestGroup='" + ma.idQuestGroup + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameters[0].Value = ma.idQuestGroup;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean DeleteQuestSriptVol(QuestModel ma, bool group, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM VolQuest WHERE idQuest = @idQuest");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameter[0].Value = ma.idQuest;

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
            sqlParameter[4].Value = ma.idQuest;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idQuest";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete vol quest";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            if (group == true)
            {
                query = string.Format(@"DELETE FROM VolQuestGroup WHERE idQuestGroup = @idQuestGroup");

                sqlParameter = new SqlParameter[1];
                sqlParameter[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
                sqlParameter[0].Value = ma.idQuestGroup;

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
                sqlParameter[4].Value = ma.idQuestGroup;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idQuestGroup";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "VolQuestGroup";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete vol quest group";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public DataTable GetSkills(List<int> Labels)
        {
            string condition = "";
            for (int i = 0; i < Labels.Count; i++)
            {
                if (i == 0)
                {
                    condition = " WHERE va.idQueryType='" + Labels[i].ToString() + "'";
                }
                else
                {
                    condition = condition + " OR va.idQueryType='" + Labels[i].ToString() + "'";
                }
            }


            string selectQuery = @"select distinct v.txtQuest,v.idQuest as idQuestSkills,
                          CASE WHEN ma.idQuestSkills IS NOT NULL THEN ma.idQuestSkills else '-1' END  as idQuest,
                          CASE WHEN ma.idQuest IS NOT NULL THEN ma.idQuest else '-1' END as idQuestAns
                                FROM ( select distinct
                                CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest,
                                (SELECT TOP 1 idQuest FROM VolQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as idQuest
                                from VolQuest vq  ) v
                                LEFT OUTER JOIN VolAns va ON va.idQuest=v.idQuest
                                LEFT JOIN  MedAns ma on ma.idQuestSkills = v.idQuest
                                LEFT JOIN (SELECT DISTINCT CASE WHEN id IS NOT NULL THEN id ELSE idLabel END as id , nameLabel FROM labels) l ON l.id = va.idQueryType
                      " + condition;


            return conn.executeSelectQuery(selectQuery, null);

        }

        //===========
    }
}
