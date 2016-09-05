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
    public class VolontaryFunctionDAO
    {

        private dbConnection conn;

        public VolontaryFunctionDAO()
        {
            conn = new dbConnection();
        }

      

        public DataTable GetVoluntaryQuestGroup(int idCompany)
        {

            string selectQuery = " select VolFunctionQuestGroup.idQuestGroup,nameQuestGroup from VolFunctionQuestGroup WHERE VolFunctionQuestGroup.idCompany = '" + idCompany + "'";

            return conn.executeSelectQuery(selectQuery, null);

        }



        public DataTable GetVoluntaryQuest2(int idCompany, int idQuestGroup)
        {

            string selectQuery = "SELECT distinct VolFunctionQuest.idQuest,idQuestGroup,CONVERT(nvarchar(max),txtQuest) as txtQuest,questSort FROM VolFunctionQuest LEFT OUTER JOIN VolFunctionAns ON VolFunctionAns.idQuest = VolFunctionQuest.idQuest  WHERE idQuestGroup = '" + idQuestGroup + "' AND (idQuestGroup in (SELECT DISTINCT idQuestGroup FROM VolFunctionQuestGroup WHERE VolFunctionQuestGroup.idCompany = '" + idCompany + "') )  ORDER BY questSort";

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
            string selectQuery = @"select mqg.nameQuestGroup,mq.idQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
                                     ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from VolFunctionQuest mq 
                                     left outer join VolFunctionQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                     left outer join VolFunctionAns ma on ma.idQuest = mq.idQuest  
                                     left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType order by txtQuest, mq.questSort, ma.ansSort";
            if (queryCondition != "where")
            {

                selectQuery = @"select mqg.nameQuestGroup,,mq.idQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
                                 ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from VolFunctionQuest mq 
                                 left outer join VolFunctionQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                 left outer join VolFunctionAns ma on ma.idQuest = mq.idQuest  
                                 left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType " + queryCondition + " order by txtQuest, mq.questSort, ma.ansSort";


            }
            return conn.executeSelectQuery(selectQuery, null);



        }

        public DataTable GetVolAnswerTypes()
        {

            string selectQuery = "SELECT idAnsType, nameAnsType FROM VolAnsType";

            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable GetVoluntaryAnswer(int idQuest, List<int> Labels)
        {
            string condition = "";
            for (int i = 0; i < Labels.Count; i++)
            {
                if (i == 0)
                {
                    condition = " WHERE VolFunctionAns.idQueryType='" + Labels[i].ToString() + "'";
                }
                else
                {
                    condition = condition + " OR VolFunctionAns.idQueryType='" + Labels[i].ToString() + "'";
                }
            }
            string selectQuery = "SELECT DISTINCT idAns,idAnsType,idQuest,idQueryType,l.nameLabel,CONVERT(nvarchar(max),txtAns) as txtAns,ansSort FROM VolFunctionAns LEFT OUTER JOIN Labels l  ON l.idLabel = VolFunctionAns.idQueryType WHERE idQuest = '" + idQuest + "' ORDER BY ansSort";
            if (condition != "" && condition != null)
            {
                selectQuery = "SELECT DISTINCT idAns,idAnsType,idQuest,idQueryType,l.nameLabel,CONVERT(nvarchar(max),txtAns) as txtAns,ansSort FROM VolFunctionAns LEFT OUTER JOIN Labels l  ON l.idLabel = VolFunctionAns.idQueryType WHERE idQuest = '" + idQuest + "' AND (" + condition.Replace("WHERE", "") + ") ORDER BY ansSort";

            }
            return conn.executeSelectQuery(selectQuery, null);
        }


//        public DataTable GetVoluntaryQuest(List<int> idQueryType)
//        {
//            string queryCondition = "where";

//            for (int i = 0; i < idQueryType.Count; i++)
//            {
//                if (queryCondition != "where")
//                {
//                    queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].ToString() + "'";
//                }
//                else
//                {
//                    queryCondition += " ma.idQueryType = '" + idQueryType[i].ToString() + "'";
//                }
//            }
//            string selectQuery = @"select mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
//                                     ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from VolFunctionQuest mq 
//                                     left outer join VolFunctionQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
//                                     left outer join VolFunctionAns ma on ma.idQuest = mq.idQuest  
//                                     left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType order by txtQuest, mq.questSort, ma.ansSort";
//            if (queryCondition != "where")
//            {

//                selectQuery = @"select mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest, ma.idAns,
//                                 ma.txtAns,ma.idAnsType, mat.nameAnsType,ma.ansSort from VolFunctionQuest mq 
//                                 left outer join VolFunctionQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
//                                 left outer join VolFunctionAns ma on ma.idQuest = mq.idQuest  
//                                 left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType " + queryCondition + " order by txtQuest, mq.questSort, ma.ansSort";


//            }
//            return conn.executeSelectQuery(selectQuery, null);



//        }

        public DataTable GetVoluntaryFunctionForPerson(List<LabelForPerson> idQueryType, int idContPers)
        {
            string queryCondition = "where";

            for (int i = 0; i < idQueryType.Count; i++)
            {
                if (queryCondition != "where")
                {
                    queryCondition += " OR " + " ma.idQueryType =  '" + idQueryType[i].idLabel.ToString() + "'";
                }
                else
                {
                    queryCondition += " ma.idQueryType = '" + idQueryType[i].idLabel.ToString() + "'";
                }
            }
            string selectQuery = @"select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest from VolFunctionQuest mq 
                                     left outer join VolFunctionQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                     left outer join VolFunctionAns ma on ma.idQuest = mq.idQuest  
                                     left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType 
                                     inner join VolFunctionCpr vfc on vfc.idQuest  =  ma.idQuest 
                                     WHERE vfc.idcpr = '" + idContPers.ToString() + @"'
                                     order by txtQuest";
            if (queryCondition != "where")
            {
                queryCondition = " AND (" + queryCondition.Replace("where", "") + ")";
                selectQuery = @"select distinct mqg.nameQuestGroup,mq.idQuest, CONVERT(NVARCHAR(2000),mq.txtQuest) as txtQuest from VolFunctionQuest mq 
                                 left outer join VolFunctionQuestGroup mqg on mq.idQuestGroup = mqg.idQuestGroup 
                                 left outer join VolFunctionAns ma on ma.idQuest = mq.idQuest  
                                 left outer join VolAnsType mat on ma.idAnsType = mat.idAnsType 
                                 inner join VolFunctionCpr vfc on vfc.idQuest  =  ma.idQuest 
                                 WHERE vfc.idcpr = '" + idContPers.ToString() + "' " + queryCondition + " order by txtQuest";


            }
            return conn.executeSelectQuery(selectQuery, null);



        }

        public Boolean InsertVoluntaryQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string insertQuery = " INSERT INTO VolFunctionQuestGroup (nameQuestGroup,idCompany) VALUES (@nameQuestGroup,@idCompany)";

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
             sqlParameter[4].Value = conn.GetLastTableID("VolFunctionQuestGroup") + 1;

             sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
             sqlParameter[5].Value = "idQuestGroup";

             sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
             sqlParameter[6].Value = "VolFunctionQuestGroup";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Insert voluntary question group";

             _query.Add(insertQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);

         }

        public Boolean UpdateVoluntaryQuestionGroup(QuestGroupModel qg, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string updateQuery = "UPDATE VolFunctionQuestGroup SET nameQuestGroup = @nameQuestGroup WHERE idQuestGroup = @idQuestGroup";

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
             sqlParameter[6].Value = "VolFunctionQuestGroup";

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

             string deleteQuery = "DELETE FROM VolFunctionQuestGroup WHERE idQuestGroup = @idQuestGroup";

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
             sqlParameter[6].Value = "VolFunctionQuestGroup";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Delete voluntary question group";

             _query.Add(deleteQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);

         }


        public Boolean InsertVoluntaryQuestion(QuestModel qg, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string insertQuery = " INSERT INTO VolFunctionQuest (idQuestGroup,txtQuest,questSort) VALUES (@idQuestGroup,@txtQuest,@questSort)";

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
             sqlParameter[4].Value = conn.GetLastTableID("VolFunctionQuest") + 1;

             sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
             sqlParameter[5].Value = "idQuestGroup";

             sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
             sqlParameter[6].Value = "VolFunctionQuest";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Insert voluntary question ";

             _query.Add(insertQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);

         }

        public Boolean UpdateVoluntaryQuestion(QuestModel qg, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string updateQuery = "UPDATE VolFunctionQuest SET txtQuest = @txtQuest, questSort = @QuestSort WHERE idQuest = @idQuest";

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
             sqlParameter[6].Value = "VolFunctionQuest";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Update voluntary question ";

             _query.Add(updateQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);
         }

        public Boolean DeleteVoluntaryQuestion(QuestModel qg, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string deleteQuery = "DELETE FROM VolFunctionQuest WHERE idQuest = @idQuest";

             SqlParameter[] sqlParameter = new SqlParameter[1];

             sqlParameter[0] = new SqlParameter("@idQuest", SqlDbType.Int);
             sqlParameter[0].Value = qg.idQuest;

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
             sqlParameter[4].Value = qg.idQuest;

             sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
             sqlParameter[5].Value = "idQuest";

             sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
             sqlParameter[6].Value = "VolFunctionQuest";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Delete voluntary question ";

             _query.Add(deleteQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);

         }


         public DataTable getLastVolAnswerId()
         {
             string selectQuery = "select top 1 idAns from VolFunctionAns order by idAns desc";

             return conn.executeSelectQuery(selectQuery, null);
         }

         public Boolean InsertVoluntaryAnswer(AnswerModel a, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string insertQuery = " INSERT INTO VolFunctionAns (idAns,idAnsType,idQuest,idQueryType,txtAns,ansSort) VALUES (@idAns,@idAnsType,@idQuest,@idQueryType,@txtAns,@ansSort)";

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
             sqlParameter[4].Value = a.idAns.ToString();

             sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
             sqlParameter[5].Value = "idAns";

             sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
             sqlParameter[6].Value = "VolFunctionAns";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Insert voluntary answer ";

             _query.Add(insertQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);

         }

         public Boolean UpdateVoluntaryAnswer(AnswerModel a, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string updateQuery = "UPDATE VolFunctionAns set idAnsType = @idAnsType,txtAns = @txtAns,ansSort=@ansSort where idAns=@idAns";

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
             sqlParameter[6].Value = "VolFunctionAns";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Update voluntary answer ";

             _query.Add(updateQuery);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);

         }

         public DataTable GetSameQuestAnswer(string idQuest, string idAns, string idContPers)
         {

             string selectQuery = @"SELECT va.idQuest, idAns FROM VolFunctionAns va
                                LEFT OUTER JOIN VolFunctionQuest vq ON va.idQuest = vq.idQuest
                                WHERE  RTRIM(LTRIM(CONVERT(NVARCHAR(2000),vq.txtQuest))) + RTRIM(LTRIM(CONVERT(NVARCHAR(2000),va.txtAns))) = 
                                ( SELECT RTRIM(LTRIM(CONVERT(NVARCHAR(2000),vq.txtQuest))) + RTRIM(LTRIM(CONVERT(NVARCHAR(2000),va.txtAns))) FROM  VolFunctionAns va
                                LEFT OUTER JOIN VolFunctionQuest vq ON va.idQuest = vq.idQuest WHERE va.idQuest='" + idQuest + "' AND va.idAns= '" + idAns + @"')
                                AND va.idQueryType IN (SELECT idLabel FROM ContactPersonLabel WHERE idContPers = '"+idContPers+"')";

             return conn.executeSelectQuery(selectQuery, null);

         }

         public DataTable GetVoluntary(List<String> idQueryType, int idContPers, Boolean isDefaultSort, Boolean isAll)
        {
            string queryCondition = "where";
            string join = "inner join";
            string sort = "txtQuest, questSort, ansSort";

            if (isDefaultSort == false)
            {
                sort = "questSort, ansSort";
            }

            if (isAll == false)
            {
                join = "left outer join";
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

            string selectQuery = "select distinct nameQuestGroup,idQuest,txtQuest,idAns,txtAns, idAnsType,idCpr, txt, questSort, ansSort FROM (select distinct vqg.nameQuestGroup,(SELECT TOP 1 idQuest FROM VolFunctionQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as idQuest , CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, (SELECT TOP 1 idAns FROM VolFunctionAns v LEFT OUTER JOIN VolFunctionQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns)) as idAns, va.txtAns,va.idAnsType," +
                "vc.idCpr, CONVERT(NVARCHAR(2000),vc.txt) as txt,(SELECT TOP 1 questSort FROM VolFunctionQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as questSort, (SELECT TOP 1 ansSort FROM VolFunctionAns v LEFT OUTER JOIN VolFunctionQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns))  as ansSort from VolFunctionQuest vq " + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolFunctionAns va on " +
                "va.idQuest = vq.idQuest " + join + "  (select vcp.idcpr, vqu.txtQuest, van.txtAns,vcp.txt from VolFunctionCpr vcp " + join + @" VolFunctionQuest vqu ON vqu.idQuest = vcp.idQuest
               " + join + " VolFunctionAns van ON van.idAns = vcp.idAns where (idCpr = '" + idContPers + "' or idCpr is null)) vc on CONVERT(NVARCHAR(2000),vc.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest) and CONVERT(NVARCHAR(2000),va.txtAns) = CONVERT(NVARCHAR(2000),vc.txtAns)  and va.idAns is not null " +
                " ) cc order by "+sort;

            if (queryCondition != "where")
            {

                selectQuery = "select distinct nameQuestGroup,idQuest,txtQuest,idAns,txtAns, idAnsType,idCpr, txt, questSort, ansSort FROM (select distinct vqg.nameQuestGroup,(SELECT TOP 1 idQuest FROM VolFunctionQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, (SELECT TOP 1 idAns FROM VolFunctionAns v LEFT OUTER JOIN VolFunctionQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns) and  ("+ queryCondition.Replace("where","") + @" ) ) as idAns, va.txtAns,va.idAnsType," +
                "vc.idCpr, CONVERT(NVARCHAR(2000),vc.txt) as txt,(SELECT TOP 1 questSort FROM VolFunctionQuest v where CONVERT(NVARCHAR(2000),v.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest)) as questSort, (SELECT TOP 1 ansSort FROM VolFunctionAns v LEFT OUTER JOIN VolFunctionQuest vqq ON v.idQuest = vqq.idQuest where CONVERT(NVARCHAR(2000),vqq.txtQuest)+CONVERT(NVARCHAR(2000),v.txtAns) = CONVERT(NVARCHAR(2000),vq.txtQuest)+CONVERT(NVARCHAR(2000),va.txtAns))  as ansSort  from VolFunctionQuest vq " + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolFunctionAns va on " +
                "va.idQuest = vq.idQuest " + join + " (select vcp.idcpr, vqu.txtQuest, van.txtAns,vcp.txt from VolFunctionCpr vcp " + join + @" VolFunctionQuest vqu ON vqu.idQuest = vcp.idQuest
               " + join + " VolFunctionAns van ON van.idAns = vcp.idAns where (idCpr = '" + idContPers + "' or idCpr is null)) vc on CONVERT(NVARCHAR(2000),vc.txtQuest) = CONVERT(NVARCHAR(2000),vq.txtQuest) and CONVERT(NVARCHAR(2000),va.txtAns) = CONVERT(NVARCHAR(2000),vc.txtAns)  and va.idAns is not null " +
                " " + queryCondition + " ) cc order by " + sort;

            }

            return conn.executeSelectQuery(selectQuery, null);

        }


         public DataTable GetVoluntaryArrangement(List<String> idQueryType, int idArrangement, Boolean isDefaultSort, Boolean isAll)
         {
             string queryCondition = "where";
             string join = "inner join";
             string sort = "txtQuest, vq.questSort, va.ansSort";

             if (isDefaultSort == false)
             {
                 sort = "vq.questSort, va.ansSort";
             }

             if (isAll == false)
             {
                 join = "left outer join";
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
                 "vc.idArrangement, CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolFunctionQuest vq " + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolFunctionAns va on " +
                 "va.idQuest = vq.idQuest " + join + "  (select * from VolFunctionArr where (idArrangement = '" + idArrangement + "' or idArrangement is null)) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns  " +
                 " order by " + sort;

             if (queryCondition != "where")
             {

                 selectQuery = "select distinct vqg.nameQuestGroup,vq.idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, va.idAns, va.txtAns,va.idAnsType," +
                 "vc.idArrangement, CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolFunctionQuest vq " + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup " + join + " VolFunctionAns va on " +
                 "va.idQuest = vq.idQuest " + join + "  (select * from VolFunctionArr where (idArrangement = '" + idArrangement + "' or idArrangement is null)) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns  " +
                 " " + queryCondition + " order by " + sort;

             }

             return conn.executeSelectQuery(selectQuery, null);

         }

         public Boolean SaveVoluntary(VolontaryFunctionModel vol, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            //string dateTimeChanged = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            string insertQuery = "insert into VolFunctionCpr (idcpr,idQuest,idAns,txt) values( @idcpr,@idQuest,@idAns,@txt)";

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
            sqlParameter[4].Value = conn.GetLastTableID("VolFunctionCpr") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idVol";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolFunctionCpr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save voluntary";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

         public Boolean DeleteVoluntary(int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string deleteQuery = "delete from VolFunctionCpr where idcpr = @idcpr";

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
            sqlParameter[6].Value = "VolFunctionCpr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete voluntary";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

         public Boolean SaveVoluntaryArrangement(VolontaryFunctionModel vol, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            //string dateTimeChanged = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            string insertQuery = "insert into VolFunctionArr (idArrangement,idQuest,idAns,txt) values( @idArrangement,@idQuest,@idAns,@txt)";

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
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
            sqlParameter[4].Value = conn.GetLastTableID("VolFunctionArr") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idVol";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolFunctionArr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save voluntary arrangement";

            _query.Add(insertQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

         public Boolean DeleteVoluntaryArrangement(int idArrangement, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string deleteQuery = "delete from VolFunctionArr where idArrangement = @idArrangement";

            SqlParameter[] sqlParameter = new SqlParameter[1];


            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
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
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolFunctionArr";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete voluntary arrangement";

            _query.Add(deleteQuery);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        //==================================
        public DataTable checkIsOneAnsVF(AnswerModel ma)
        {
            string query = string.Format(@"SELECT distinct ma.idAns, ma.idQueryType
                                           FROM VolFunctionAns ma                                 
                                           WHERE ma.idQuest = '" + ma.idQuest + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameters[0].Value = ma.idQuest;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkIsOneQuestVF(int idGroup)
        {
            string query = string.Format(@"SELECT distinct ma.idQuest
                                           FROM VolFunctionQuest ma                                 
                                           WHERE  ma.idQuestGroup='" + idGroup + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idGroup", SqlDbType.Int);
            sqlParameters[0].Value = idGroup;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkAnsIsInVFCpr(int idAns, int idQueryType)
        {
            string query = string.Format(@"SELECT distinct mcpr.idAns,mcpr.idcpr, cpl.idLabel
                                           FROM VolFunctionAns ma
                                           INNER JOIN VolFunctionCpr mcpr on ma.idAns= mcpr.idAns
                                           LEFT OUTER JOIN  ContactPersonLabel cpl on cpl.idLabel = ma.idQueryType
                                           WHERE ma.idAns = '" + idAns + "' AND ma.idQueryType='" + idQueryType + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameters[0].Value = idAns;

            sqlParameters[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameters[1].Value = idQueryType;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkAnsIsInVFArr(int idAns, int idQueryType)
        {
            string query = string.Format(@"SELECT distinct mcpr.idAns
                                           FROM VolFunctionAns ma
                                           INNER JOIN VolFunctionArr mcpr on ma.idAns= mcpr.idAns
                                           WHERE ma.idAns = '" + idAns + "' AND ma.idQueryType='" + idQueryType + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameters[0].Value = idAns;

            sqlParameters[1] = new SqlParameter("@idQueryType", SqlDbType.Int);
            sqlParameters[1].Value = idQueryType;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public Boolean DeleteAnsSriptVF(AnswerModel ma, bool quest, bool group, int idQuestGroup, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            if (ma.idAns != -1)
            {
                string query = string.Format(@"DELETE FROM VolFunctionAns WHERE idAns = @idAns AND idQueryType=@idQueryType");

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
                sqlParameter[4].Value = ma.idQueryType;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idQueryType";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "VolFunctionAns";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete Vol function ans";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


               // return conn.executQueryTransaction(_query, sqlParameters);
            }
            if (quest == true)
            {
                if (ma.idQuest != -1)
                {
                    string query = string.Format(@"DELETE FROM VolFunctionQuest WHERE idQuest = @idQuest");

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
                    sqlParameter[6].Value = "VolFunctionQuest";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete Vol function quest";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);


                }

                if (group == true)
                {

                    string query = string.Format(@"DELETE FROM VolFunctionQuestGroup WHERE idQuestGroup = @idQuestGroup");

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
                    sqlParameter[6].Value = "VolFunctionQuestGroup";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Delete Vol function quest group";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);


                }
            }

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable checkIsOneQuestInGroupVF(QuestModel ma)
        {
            string query = string.Format(@"SELECT distinct ma.idQuest
                                           FROM VolFunctionQuest ma                                 
                                           WHERE  ma.idQuestGroup='" + ma.idQuestGroup + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idQuestGroup", SqlDbType.Int);
            sqlParameters[0].Value = ma.idQuestGroup;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public Boolean DeleteQuestSriptVF(QuestModel ma, bool group, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM VolFunctionQuest WHERE idQuest = @idQuest");

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
            sqlParameter[6].Value = "VolFunctionQuest";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete Vol function quest";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            if (group == true)
            {
                query = string.Format(@"DELETE FROM VolFunctionQuestGroup WHERE idQuestGroup = @idQuestGroup");

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
                sqlParameter[6].Value = "VolFunctionQuestGroup";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Delete Vol function quest group";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


            }

            return conn.executQueryTransaction(_query, sqlParameters);

        }
        //==================================


        public DataTable GetAllFromVolFunction()
        {
            string query = string.Format(@"SELECT DISTINCT  cast(vfq.txtQuest as varchar(max)) as txtQuest, idcpr
                           FROM VolFunctionQuest vfq INNER JOIN
                           (select distinct idQuest , idcpr FROM VolFunctionCpr) vcp ON vcp.idQuest=vfq.idQuest
                           LEFT OUTER JOIN VolFunctionAns va on va.idQuest= vfq.idQuest");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllFromVolFunctionAB()
        {
            string query = string.Format(@"SELECT DISTINCT  cast(vfq.txtQuest as varchar(max)) as txtQuest,vcp.idContPers as idcpr,vcp.idArrangement as idAns
                           FROM VolFunctionQuest vfq INNER JOIN
                           (select distinct id , idContPers,idArrangement FROM VolLookup WHERE type ='F' ) vcp ON vcp.id=vfq.idQuest
                           LEFT OUTER JOIN VolFunctionAns va on va.idQuest= vfq.idQuest");
            return conn.executeSelectQuery(query, null);
        }
    }
}
