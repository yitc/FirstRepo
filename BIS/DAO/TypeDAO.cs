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
    public class TypeDAO
    {
        private dbConnection conn;

        public TypeDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetAllType(string language)
        {
            string query = string.Format(
                @"(SELECT idTypeClient AS ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTypeClient END AS Name, CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Client type' END AS Type
                  FROM ClientTypes ct
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = ct.nameTypeClient
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Client type'
                  UNION
                  SELECT idContactType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descContactType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Contact type' END AS Type
                  FROM ContactType cct
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = cct.descContactType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Contact type'
                  UNION
                  SELECT idAnsType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameAnsType END AS Name,  CASE WHEN s2.stringValue IS NOT NULL THEN s2.stringValue ELSE 'Medical answer type' END AS Type
                  FROM MedAnsType mdt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = mdt.nameAnsType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Medical answer type'
                  UNION
                  SELECT idToDoType as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descriptionToDoType END AS Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'To do type' END AS Type
                  FROM ToDoType todo 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = todo.descriptionToDoType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'To do type'
                  UNION
                  SELECT idAddressType as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameAddressType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Address type' END AS Type
                  FROM TypesAddress at
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = at.nameAddressType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Address type' 
                  UNION
                  SELECT idEmailType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE  nameEmailType END as Name, CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Email type' END AS Type
                  FROM TypesEmail et
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = et.nameEmailType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Email type' 
                  UNION
                  SELECT idTypeNote as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTypeNote END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Note type' END AS Type
                  FROM TypesNote nt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = nt.nameTypeNote
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Note type' 
                  UNION
                  SELECT idTelType as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTelType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Telephone type' END AS Type
                  FROM TypesTel tt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = tt.nameTelType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Telephone type'
                  UNION
                  SELECT idAnsType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameAnsType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Volontary answer type' END AS Type
                  FROM VolAnsType vat  
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = vat.nameAnsType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Volontary answer type'
                  UNION
                  SELECT idStatusEmployee as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descriptionEmployee END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Employee status' END AS Type
                  FROM EmployeeStatus estatus  
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = estatus.descriptionEmployee
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Employee status'
                  UNION
                  SELECT idMeetingStatus as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE desriptionMeetingStatus END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Meeting status' END AS Type
                  FROM MeetingsStatus mstatus  
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = mstatus.desriptionMeetingStatus
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Meeting status'
                  UNION
                  SELECT idStatusToDo as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descriptionStatus END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'To do status' END AS Type
                  FROM ToDoStatus todostatus  
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = todostatus.descriptionStatus
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='To do status'
                  UNION
                  SELECT idPriority as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descriptionPriority END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'Meeting priority' END AS Type
                  FROM MeetingsPriority mp  
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = mp.descriptionPriority
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Meeting priority'
                  UNION
                  SELECT idPriorityToDo as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descriptionPriority END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'To do priority' END AS Type
                  FROM ToDoPriority todopriority 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = todopriority.descriptionPriority
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='To do priority'
                  UNION
                  SELECT idMeetingCategory as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE categoryDescription END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'Meeting category' END AS Type
                  FROM MeetingsCategory mcategory 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = mcategory.categoryDescription
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Meeting category'
                  UNION
                  SELECT idTitle as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTitle END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'Title' END AS Type
                  FROM Title tt 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = tt.nameTitle
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Title'
                  UNION
                  SELECT idContactReason as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descContactReason END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'Contact reason' END AS Type
                  FROM ContactReason cr 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = cr.descContactReason
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Contact reason'
                  UNION
                  SELECT idFunction as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameFunction END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE  'Employee function' END AS Type
                  FROM  EmployeeFunction ef 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = ef.nameFunction
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Employee function'
                  UNION
                  SELECT idDailyType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descDailyType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Acc daily type' END AS Type
                  FROM AccDailyType adt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = adt.descDailyType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Acc daily type') order by Type
                  ");
           

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.VarChar);
            sqlParameters[0].Value = language;

            return conn.executeSelectQuery(query, sqlParameters);
        }         

        #region Update
        public Boolean UpdateAccDailyType(int idDailyType, string descDailyType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccDailyType
                SET descDailyType = @descDailyType
	        	WHERE idDailyType = @idDailyType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idDailyType", SqlDbType.Int);
            sqlParameter[0].Value = idDailyType;

            sqlParameter[1] = new SqlParameter("@descDailyType", SqlDbType.VarChar);
            sqlParameter[1].Value = descDailyType;

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
            sqlParameter[4].Value = idDailyType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update acc daily type";
            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateTypesAddress(int idAddressType, string nameAddressType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE TypesAddress
                SET nameAddressType = @nameAddressType
	        	WHERE idAddressType = @idAddressType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idAddressType", SqlDbType.Int);
            sqlParameter[0].Value = idAddressType;

            sqlParameter[1] = new SqlParameter("@nameAddressType", SqlDbType.VarChar);
            sqlParameter[1].Value = nameAddressType;

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
            sqlParameter[4].Value = idAddressType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAddressType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesAddress";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update types address";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateClientType(int idTypeClient, string nameTypeClient, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ClientTypes
                SET nameTypeClient = @nameTypeClient
	        	WHERE idTypeClient = @idTypeClient");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameter[0].Value = idTypeClient;

            sqlParameter[1] = new SqlParameter("@nameTypeClient", SqlDbType.VarChar);
            sqlParameter[1].Value = nameTypeClient;

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
            sqlParameter[4].Value = idTypeClient;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTypeClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientTypes";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update client type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateContactType(int idContactType, string descContactType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ContactType
                SET descContactType = @descContactType
	        	WHERE idContactType = @idContactType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameter[0].Value = idContactType;

            sqlParameter[1] = new SqlParameter("@descContactType", SqlDbType.VarChar);
            sqlParameter[1].Value = descContactType;

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
            sqlParameter[4].Value = idContactType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContactType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update contact type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateMedAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE MedAnsType
                SET nameAnsType = @nameAnsType
	        	WHERE idAnsType = @idAnsType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[0].Value = idAnsType;

            sqlParameter[1] = new SqlParameter("@nameAnsType", SqlDbType.VarChar);
            sqlParameter[1].Value = nameAnsType;

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
            sqlParameter[4].Value = idAnsType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAnsType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedAnsType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update med ans type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateToDoType(int idToDoType, string descriptionToDoType, string nameForm, int idUser)
        {
             List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ToDoType
                SET descriptionToDoType = @descriptionToDoType
	        	WHERE idToDoType = @idToDoType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameter[0].Value = idToDoType;

            sqlParameter[1] = new SqlParameter("@descriptionToDoType", SqlDbType.VarChar);
            sqlParameter[1].Value = descriptionToDoType;

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
            sqlParameter[4].Value = idToDoType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idToDoType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update to do type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateVolAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE VolAnsType
                SET nameAnsType = @nameAnsType
	        	WHERE idAnsType = @idAnsType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[0].Value = idAnsType;

            sqlParameter[1] = new SqlParameter("@nameAnsType", SqlDbType.VarChar);
            sqlParameter[1].Value = nameAnsType;

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
            sqlParameter[4].Value = idAnsType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAnsType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAnsType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update vol ans type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateTypesTel(int idTelType, string nameTelType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE TypesTel
                SET nameTelType = @nameTelType
	        	WHERE idTelType = @idTelType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameter[0].Value = idTelType;

            sqlParameter[1] = new SqlParameter("@nameTelType", SqlDbType.VarChar);
            sqlParameter[1].Value = nameTelType;

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
            sqlParameter[4].Value = idTelType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTelType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update types tel";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateTypesNote(int idTypeNote, string nameTypeNote, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE TypesNote
                SET nameTypeNote = @nameTypeNote
	        	WHERE idTypeNote = @idTypeNote");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameter[0].Value = idTypeNote;

            sqlParameter[1] = new SqlParameter("@nameTypeNote", SqlDbType.VarChar);
            sqlParameter[1].Value = nameTypeNote;

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
            sqlParameter[4].Value = idTypeNote;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTypeNote";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesNote";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update types note";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateTypesEmail(int idEmailType, string nameEmailType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE TypesEmail
                SET nameEmailType = @nameEmailType
	        	WHERE idEmailType = @idEmailType");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idEmailType", SqlDbType.Int);
            sqlParameter[0].Value = idEmailType;

            sqlParameter[1] = new SqlParameter("@nameEmailType", SqlDbType.VarChar);
            sqlParameter[1].Value = nameEmailType;

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
            sqlParameter[4].Value = idEmailType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idEmailType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update types email";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertTypesAddress(int idAddressType, string nameAddressType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   TypesAddress(idAddressType,nameAddressType)
                                    VALUES(@idAddressType,@nameAddressType)"); 

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameAddressType", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameAddressType;

            sqlParameter[1] = new SqlParameter("@idAddressType", SqlDbType.Int);
            sqlParameter[1].Value = idAddressType;

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
            sqlParameter[4].Value = nameAddressType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "nameAddressType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesAddress";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert types address";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateMeetingCategory(int idMeetingCategory, string categoryDescription, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE MeetingsCategory
                SET categoryDescription = @categoryDescription
	        	WHERE idMeetingCategory = @idMeetingCategory");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idMeetingCategory", SqlDbType.Int);
            sqlParameter[0].Value = idMeetingCategory;

            sqlParameter[1] = new SqlParameter("@categoryDescription", SqlDbType.VarChar);
            sqlParameter[1].Value = categoryDescription;

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
            sqlParameter[4].Value = idMeetingCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMeetingCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update meetings category";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateMeetingPriority(int idPriority, string descriptionPriority, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE MeetingsPriority
                SET descriptionPriority = @descriptionPriority
	        	WHERE idPriority = @idPriority");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idPriority", SqlDbType.Int);
            sqlParameter[0].Value = idPriority;

            sqlParameter[1] = new SqlParameter("@descriptionPriority", SqlDbType.VarChar);
            sqlParameter[1].Value = descriptionPriority;

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
            sqlParameter[4].Value = idPriority;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriority";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsPriority";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update meetings priority";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateToDoPriority(int idPriorityToDo, string descriptionPriority, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ToDoPriority
                SET descriptionPriority = @descriptionPriority
	        	WHERE idPriorityToDo = @idPriorityToDo");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idPriorityToDo", SqlDbType.Int);
            sqlParameter[0].Value = idPriorityToDo;

            sqlParameter[1] = new SqlParameter("@descriptionPriority", SqlDbType.VarChar);
            sqlParameter[1].Value = descriptionPriority;

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
            sqlParameter[4].Value = idPriorityToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriorityToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoPriority";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update to do priority";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateTitle(int idTitle, string nameTitle, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Title
                SET nameTitle = @nameTitle
	        	WHERE idTitle = @idTitle");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idTitle", SqlDbType.Int);
            sqlParameter[0].Value = idTitle;

            sqlParameter[1] = new SqlParameter("@nameTitle", SqlDbType.VarChar);
            sqlParameter[1].Value = nameTitle;

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
            sqlParameter[4].Value = idTitle;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTitle";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Title";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update title";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateEmployeeFunction(int idFunction, string nameFunction, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE EmployeeFunction
                SET nameFunction = @nameFunction
	        	WHERE idFunction = @idFunction");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idFunction", SqlDbType.Int);
            sqlParameter[0].Value = idFunction;

            sqlParameter[1] = new SqlParameter("@nameFunction", SqlDbType.VarChar);
            sqlParameter[1].Value = nameFunction;

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
            sqlParameter[4].Value = idFunction;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFunction";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeFunction";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update employee function";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateEmployeeStatus(int idStatusEmployee, string descriptionEmployee, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE EmployeeStatus
                SET descriptionEmployee = @descriptionEmployee
	        	WHERE idStatusEmployee = @idStatusEmployee");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idStatusEmployee", SqlDbType.Int);
            sqlParameter[0].Value = idStatusEmployee;

            sqlParameter[1] = new SqlParameter("@descriptionEmployee", SqlDbType.VarChar);
            sqlParameter[1].Value = descriptionEmployee;

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
            sqlParameter[4].Value = idStatusEmployee;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idStatusEmployee";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update employee status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateMeetingStatus(int idMeetingStatus, string desriptionMeetingStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE MeetingsStatus
                SET desriptionMeetingStatus = @desriptionMeetingStatus
	        	WHERE idMeetingStatus = @idMeetingStatus");

            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idMeetingStatus", SqlDbType.Int);
            sqlParameter[0].Value = idMeetingStatus;

            sqlParameter[1] = new SqlParameter("@desriptionMeetingStatus", SqlDbType.VarChar);
            sqlParameter[1].Value = desriptionMeetingStatus;

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
            sqlParameter[4].Value = idMeetingStatus;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMeetingStatus";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update meetings status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateToDoStatus(int idStatusToDo, string descriptionStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ToDoStatus
                SET descriptionStatus = @descriptionStatus
	        	WHERE idStatusToDo = @idStatusToDo");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idStatusToDo", SqlDbType.Int);
            sqlParameter[0].Value = idStatusToDo;

            sqlParameter[1] = new SqlParameter("@descriptionStatus", SqlDbType.VarChar);
            sqlParameter[1].Value = descriptionStatus;

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
            sqlParameter[4].Value = idStatusToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idStatusToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update to do status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean UpdateContactReason(int idContactReason, string descContactReason, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ContactReason
                SET descContactReason = @descContactReason
	        	WHERE idContactReason = @idContactReason");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idContactReason", SqlDbType.Int);
            sqlParameter[0].Value = idContactReason;

            sqlParameter[1] = new SqlParameter("@descContactReason", SqlDbType.VarChar);
            sqlParameter[1].Value = descContactReason;

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
            sqlParameter[4].Value = idContactReason;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContactReason";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactReason";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update contact reason";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
# endregion Update

        # region Insert
        public bool InsertAccDailyType(int idDailyType, string descDailyType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   AccDailyType(idDailyType,descDailyType)
                                    VALUES(@idDailyType,@descDailyType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descDailyType", SqlDbType.NChar);
            sqlParameter[0].Value = descDailyType;

            sqlParameter[1] = new SqlParameter("@idDailyType", SqlDbType.Int);
            sqlParameter[1].Value = idDailyType;

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
            sqlParameter[4].Value = idDailyType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert acc daily type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertClientTypes(int idTypeClient, string nameTypeClient, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   ClientTypes(idTypeClient,nameTypeClient)
                                    VALUES(@idTypeClient,@nameTypeClient)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameTypeClient", SqlDbType.NChar);
            sqlParameter[0].Value = nameTypeClient;

            sqlParameter[1] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameter[1].Value = idTypeClient;

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
            sqlParameter[4].Value = idTypeClient;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTypeClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientTypes";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert client typese";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertContactType(int idContactType, string descContactType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   ContactType(idContactType,descContactType)
                                    VALUES(@idContactType,@descContactType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descContactType", SqlDbType.NChar);
            sqlParameter[0].Value = descContactType;

            sqlParameter[1] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameter[1].Value = idContactType;

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
            sqlParameter[4].Value = idContactType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContactType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert contact type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertTypesEmail(int idEmailType, string nameEmailType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   TypesEmail(idEmailType,nameEmailType)
                                    VALUES(@idEmailType,@nameEmailType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameEmailType", SqlDbType.NChar);
            sqlParameter[0].Value = nameEmailType;

            sqlParameter[1] = new SqlParameter("@idEmailType", SqlDbType.Int);
            sqlParameter[1].Value = idEmailType;

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
            sqlParameter[4].Value = idEmailType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idEmailType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert types email";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertMedAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   MedAnsType(idAnsType,nameAnsType)
                                    VALUES(@idAnsType,@nameAnsType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameAnsType", SqlDbType.NChar);
            sqlParameter[0].Value = nameAnsType;

            sqlParameter[1] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[1].Value = idAnsType;

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
            sqlParameter[4].Value = idAnsType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAnsType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MedAnsType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert med ans type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertTypesNote(int idTypeNote, string nameTypeNote, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   TypesNote(idTypeNote,nameTypeNote)
                                    VALUES(@idTypeNote,@nameTypeNote)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameTypeNote", SqlDbType.NChar);
            sqlParameter[0].Value = nameTypeNote;

            sqlParameter[1] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameter[1].Value = idTypeNote;

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
            sqlParameter[4].Value = idTypeNote;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTypeNote";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesNote";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert types note";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertTypesTel(int idTelType, string nameTelType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   TypesTel(idTelType,nameTelType)
                                    VALUES(@idTelType,@nameTelType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameTelType", SqlDbType.NChar);
            sqlParameter[0].Value = nameTelType;

            sqlParameter[1] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameter[1].Value = idTelType;

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
            sqlParameter[4].Value = idTelType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTelType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert types tel";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertToDoType(int idToDoType, string descriptionToDoType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   ToDoType(idToDoType,descriptionToDoType)
                                    VALUES(@idToDoType,@descriptionToDoType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descriptionToDoType", SqlDbType.NChar);
            sqlParameter[0].Value = descriptionToDoType;

            sqlParameter[1] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameter[1].Value = idToDoType;

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
            sqlParameter[4].Value = idToDoType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idToDoType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert to do type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertVolAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   VolAnsType(idAnsType,nameAnsType)
                                    VALUES(@idAnsType,@nameAnsType)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameAnsType", SqlDbType.NChar);
            sqlParameter[0].Value = nameAnsType;

            sqlParameter[1] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[1].Value = idAnsType;

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
            sqlParameter[4].Value = idAnsType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAnsType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAnsType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert vol ans type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertMeetingCategory(int idMeetingCategory, string categoryDescription, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   MeetingsCategory(idMeetingCategory,categoryDescription)
                                    VALUES(@idMeetingCategory,@categoryDescription)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@categoryDescription", SqlDbType.NChar);
            sqlParameter[0].Value = categoryDescription;

             sqlParameter[1] = new SqlParameter("@idMeetingCategory", SqlDbType.Int);
            sqlParameter[1].Value = idMeetingCategory;

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
            sqlParameter[4].Value = idMeetingCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMeetingCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert meetings category";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertMeetingsPriority(int idPriority, string descriptionPriority, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   MeetingsPriority(idPriority,descriptionPriority)
                                    VALUES(@idPriority,@descriptionPriority)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descriptionPriority", SqlDbType.NChar);
            sqlParameter[0].Value = descriptionPriority;

            sqlParameter[1] = new SqlParameter("@idPriority", SqlDbType.Int);
            sqlParameter[1].Value = idPriority;

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
            sqlParameter[4].Value = idPriority;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriority";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsPriority";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert meetings priority";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertToDoPriority(int idPriorityToDo, string descriptionPriority, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   ToDoPriority(idPriorityToDo,descriptionPriority)
                                    VALUES(@idPriorityToDo,@descriptionPriority)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descriptionPriority", SqlDbType.NChar);
            sqlParameter[0].Value = descriptionPriority;

            sqlParameter[1] = new SqlParameter("@idPriorityToDo", SqlDbType.Int);
            sqlParameter[1].Value = idPriorityToDo;

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
            sqlParameter[4].Value = idPriorityToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriorityToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoPriority";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert to do priority";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertTitle(int idTitle, string nameTitle, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   Title(idTitle,nameTitle)
                                    VALUES(@idTitle,@nameTitle)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameTitle", SqlDbType.NChar);
            sqlParameter[0].Value = nameTitle;

            sqlParameter[1] = new SqlParameter("@idTitle", SqlDbType.Int);
            sqlParameter[1].Value = idTitle;

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
            sqlParameter[4].Value = idTitle;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTitle";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Title";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert title";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertEmployeeFunction(int idFunction, string nameFunction, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   EmployeeFunction(idFunction,nameFunction)
                                    VALUES(@idFunction,@nameFunction)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@nameFunction", SqlDbType.NChar);
            sqlParameter[0].Value = nameFunction;

            sqlParameter[1] = new SqlParameter("@idFunction", SqlDbType.Int);
            sqlParameter[1].Value = idFunction;

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
            sqlParameter[4].Value = idFunction;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFunction";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeFunction";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert employee function";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertEmployeeStatus(int idStatusEmployee, string descriptionEmployee, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   EmployeeStatus(idStatusEmployee,descriptionEmployee)
                                    VALUES(@idStatusEmployee,@descriptionEmployee)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descriptionEmployee", SqlDbType.NChar);
            sqlParameter[0].Value = descriptionEmployee;

            sqlParameter[1] = new SqlParameter("@idStatusEmployee", SqlDbType.Int);
            sqlParameter[1].Value = idStatusEmployee;

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
            sqlParameter[4].Value = idStatusEmployee;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idStatusEmployee";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert employee status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertMeetingsStatus(int idMeetingStatus, string desriptionMeetingStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   MeetingsStatus(idMeetingStatus,desriptionMeetingStatus)
                                    VALUES(@idMeetingStatus,@desriptionMeetingStatus)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@desriptionMeetingStatus", SqlDbType.NChar);
            sqlParameter[0].Value = desriptionMeetingStatus;

            sqlParameter[1] = new SqlParameter("@idMeetingStatus", SqlDbType.Int);
            sqlParameter[1].Value = idMeetingStatus;

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
            sqlParameter[4].Value = idMeetingStatus;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMeetingStatus";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert meetings status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertToDoStatus(int idStatusToDo, string descriptionStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   ToDoStatus(idStatusToDo,descriptionStatus)
                                    VALUES(@idStatusToDo,@descriptionStatus)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descriptionStatus", SqlDbType.NChar);
            sqlParameter[0].Value = descriptionStatus;

            sqlParameter[1] = new SqlParameter("@idStatusToDo", SqlDbType.Int);
            sqlParameter[1].Value = idStatusToDo;

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
            sqlParameter[4].Value = idStatusToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idStatusToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert to do status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertContactReason(int idContactReason, string descContactReason, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"INSERT INTO 
                                   ContactReason(idContactReason,descContactReason)
                                    VALUES(@idContactReason,@descContactReason)");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@descContactReason", SqlDbType.NChar);
            sqlParameter[0].Value = descContactReason;

            sqlParameter[1] = new SqlParameter("@idContactReason", SqlDbType.Int);
            sqlParameter[1].Value = idContactReason;

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
            sqlParameter[4].Value = idContactReason;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContactReason";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactReason";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert contact reason";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
#endregion

        #region delete
        public bool DeleteAccDailyType(int idDailyType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccDailyType WHERE idDailyType = @idDailyType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idDailyType", SqlDbType.Int);
            sqlParameter[0].Value = idDailyType;

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
            sqlParameter[4].Value = idDailyType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete acc daily type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteTypesAddress(int idAddressType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  TypesAddress WHERE idAddressType = @idAddressType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idAddressType", SqlDbType.Int);
            sqlParameter[0].Value = idAddressType;

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
            sqlParameter[4].Value = idAddressType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAddressType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesAddress";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete types address";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteClientTypes(int idTypeClient, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ClientTypes WHERE idTypeClient = @idTypeClient");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameter[0].Value = idTypeClient;

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
            sqlParameter[4].Value = idTypeClient;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTypeClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientTypes";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete client types";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteContactType(int idContactType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ContactType WHERE idContactType = @idContactType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameter[0].Value = idContactType;

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
            sqlParameter[4].Value = idContactType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContactType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete contact type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteTypesEmail(int idEmailType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  TypesEmail WHERE idEmailType = @idEmailType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idEmailType", SqlDbType.Int);
            sqlParameter[0].Value = idEmailType;

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
            sqlParameter[4].Value = idEmailType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idEmailType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete types email";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteMedAnsType(int idAnsType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  MedAnsType WHERE idAnsType = @idAnsType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[0].Value = idAnsType;

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
            sqlParameter[4].Value = idAnsType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAnsType";

             sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
             sqlParameter[6].Value = "MedAnsType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete med ans type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteTypesNote(int idTypeNote, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  TypesNote WHERE idTypeNote = @idTypeNote");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameter[0].Value = idTypeNote;

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
            sqlParameter[4].Value = idTypeNote;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTypeNote";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesNote";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete types note";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteTypesTel(int idTelType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  TypesTel WHERE idTelType = @idTelType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameter[0].Value = idTelType;

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
            sqlParameter[4].Value = idTelType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTelType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "TypesTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete types tel";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteToDoType(int idToDoType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ToDoType WHERE idToDoType = @idToDoType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameter[0].Value = idToDoType;

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
            sqlParameter[4].Value = idToDoType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idToDoType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete to do type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteVolAnsType(int idAnsType, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  VolAnsType WHERE idAnsType = @idAnsType");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameter[0].Value = idAnsType;

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
            sqlParameter[4].Value = idAnsType;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAnsType";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAnsType";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete vol ans type";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteMeetingsCategory(int idMeetingCategory, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  MeetingsCategory WHERE idMeetingCategory = @idMeetingCategory");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idMeetingCategory", SqlDbType.Int);
            sqlParameter[0].Value = idMeetingCategory;

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
            sqlParameter[4].Value = idMeetingCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMeetingCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete meetings category";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteMeetingsPriority(int idPriority, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  MeetingsPriority WHERE idPriority = @idPriority");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idPriority", SqlDbType.Int);
            sqlParameter[0].Value = idPriority;

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
            sqlParameter[4].Value = idPriority;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriority";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsPriority";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete meetings priority";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteToDoPriority(int idPriorityToDo, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ToDoPriority WHERE idPriorityToDo = @idPriorityToDo");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idPriorityToDo", SqlDbType.Int);
            sqlParameter[0].Value = idPriorityToDo;

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
            sqlParameter[4].Value = idPriorityToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriorityToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoPriority";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete to do priority";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteTitle(int idTitle, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  Title WHERE idTitle = @idTitle");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTitle", SqlDbType.Int);
            sqlParameter[0].Value = idTitle;

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
            sqlParameter[4].Value = idTitle;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeCost";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Title";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete title";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteEmployeeFunction(int idFunction, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  EmployeeFunction WHERE idFunction = @idFunction");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idFunction", SqlDbType.Int);
            sqlParameter[0].Value = idFunction;

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
            sqlParameter[4].Value = idFunction;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFunction";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeFunction";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete employee function";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteEmployeeStatus(int idStatusEmployee, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  EmployeeStatus WHERE idStatusEmployee = @idStatusEmployee");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idStatusEmployee", SqlDbType.Int);
            sqlParameter[0].Value = idStatusEmployee;

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
            sqlParameter[4].Value = idStatusEmployee;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idStatusEmployee";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete employee status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteMeetingsStatus(int idMeetingStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  MeetingsStatus WHERE idMeetingStatus = @idMeetingStatus");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idMeetingStatus", SqlDbType.Int);
            sqlParameter[0].Value = idMeetingStatus;

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
            sqlParameter[4].Value = idMeetingStatus;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMeetingStatus";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MeetingsStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete meetings status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteToDoStatus(int idStatusToDo, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ToDoStatus WHERE idStatusToDo = @idStatusToDo");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idStatusToDo", SqlDbType.Int);
            sqlParameter[0].Value = idStatusToDo;

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
            sqlParameter[4].Value = idStatusToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idStatusToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDoStatus";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete to do status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteContactReason(int idContactReason, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ContactReason WHERE idContactReason = @idContactReason");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idContactReason", SqlDbType.Int);
            sqlParameter[0].Value = idContactReason;

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
            sqlParameter[4].Value = idContactReason;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContactReason";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactReason";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete contact reason";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        #endregion

        #  region LastID
        public DataTable idAccDailyType()
        {
            string query = string.Format(@"SELECT TOP 1 idDailyType FROM  AccDailyType ORDER BY idDailyType DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable idClientType()
        {
            string query = string.Format(@"SELECT TOP 1 idTypeClient FROM  ClientTypes ORDER BY idTypeClient DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable idAddressType()
        {
            string query = string.Format(@"SELECT TOP 1 idAddressType FROM  TypesAddress ORDER BY idAddressType DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idContactType()
        {
            string query = string.Format(@"SELECT TOP 1 idContactType FROM  ContactType ORDER BY idContactType DESC");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable idEmailType()
        {
            string query = string.Format(@"SELECT TOP 1 idEmailType FROM  TypesEmail ORDER BY idEmailType DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idMedAnsType()
        {
            string query = string.Format(@"SELECT TOP 1 idAnsType FROM  MedAnsType ORDER BY idAnsType DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idNoteType()
        {
            string query = string.Format(@"SELECT TOP 1 idTypeNote FROM  TypesNote ORDER BY idTypeNote DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idToDoType()
        {
            string query = string.Format(@"SELECT TOP 1 idToDoType FROM  ToDoType ORDER BY idToDoType DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idTelephoneType()
        {
            string query = string.Format(@"SELECT TOP 1 idTelType FROM  TypesTel ORDER BY idTelType DESC");

            return conn.executeSelectQuery(query, null);
        }


        public DataTable idTypesNote()
        {
            string query = string.Format(@"SELECT TOP 1 idTypeNote FROM  TypesNote ORDER BY idTypeNote DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idVolAnsType()
        {
            string query = string.Format(@"SELECT TOP 1 idAnsType FROM  VolAnsType ORDER BY idAnsType DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idMeetingCategory()
        {
            string query = string.Format(@"SELECT TOP 1 idMeetingCategory FROM  MeetingsCategory ORDER BY idMeetingCategory DESC");
   
            return conn.executeSelectQuery(query, null);
        }
        public DataTable idMeetingPriority()
        {
            string query = string.Format(@"SELECT TOP 1 idPriority FROM  MeetingsPriority ORDER BY idPriority DESC");

            return conn.executeSelectQuery(query, null);
        }   
        public DataTable idToDoPriority()
        {
            string query = string.Format(@"SELECT TOP 1 idPriorityToDo FROM  ToDoPriority ORDER BY idPriorityToDo DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idTitle()
        {
            string query = string.Format(@"SELECT TOP 1 idTitle FROM  Title ORDER BY idTitle DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idEmployeeFunction()
        {
            string query = string.Format(@"SELECT TOP 1 idFunction FROM  EmployeeFunction ORDER BY idFunction DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idEmployeeStatus()
        {
            string query = string.Format(@"SELECT TOP 1 idStatusEmployee FROM  EmployeeStatus ORDER BY idStatusEmployee DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idMeetingsStatus()
        {
            string query = string.Format(@"SELECT TOP 1 idMeetingStatus FROM  MeetingsStatus ORDER BY idMeetingStatus DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable idToDoStatus()
        {
            string query = string.Format(@"SELECT TOP 1 idStatusToDo FROM  ToDoStatus ORDER BY idStatusToDo DESC");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable idContactReason()
        {
            string query = string.Format(@"SELECT  idContactReason FROM  ContactReason ORDER BY idContactReason DESC");
          return conn.executeSelectQuery(query, null);
        }

        # endregion

        #region IsIn
        public DataTable isInAccDailyType()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idDailyType IS NOT NULL THEN idDailyType  ELSE '-1' END AS ID 
                           FROM  AccDaily");


            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInAddress()
        {
             string query = string.Format(@"
                           SELECT distinct CASE WHEN idAddressType IS NOT NULL THEN idAddressType  ELSE '-1' END AS ID 
                           FROM  ClientAddress
                           UNION
                           SELECT distinct CASE WHEN idAddressType IS NOT NULL THEN idAddressType  ELSE '-1' END AS ID 
                             from ContactPersonAddress
                               ");
            

            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInNote()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idTypeNote IS NOT NULL THEN idTypeNote  ELSE '-1' END AS ID                          
                           FROM  ClientNotes
                           UNION
                           SELECT distinct CASE WHEN idTypeNote IS NOT NULL THEN idTypeNote  ELSE '-1' END AS ID 
                           FROM ContactPersonNotes
                          ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInEmail()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idEmailType IS NOT NULL THEN idEmailType  ELSE '-1' END AS ID
                           FROM  ClientEmail
                           UNION
                            SELECT distinct CASE WHEN idEmailType IS NOT NULL THEN idEmailType  ELSE '-1' END AS ID
                           FROM ContactPersonEmail
                           UNION
                           SELECT distinct CASE WHEN emailType IS NOT NULL THEN emailType  ELSE '-1' END AS ID
                           FROM EmployeeEmail
                          ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInTel()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idTelType IS NOT NULL THEN idTelType  ELSE '-1' END AS ID 
                           FROM  ClientTel
                           UNION
                           SELECT distinct CASE WHEN idTelType IS NOT NULL THEN idTelType  ELSE '-1' END AS ID 
                           FROM ContactPersonTel
                           UNION
                           SELECT distinct CASE WHEN telephoneType IS NOT NULL THEN telephoneType  ELSE '-1' END AS ID 
                           FROM EmployeeTel
                          ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInClient()
        {
            string query = string.Format(@"
                           SELECT distinct  CASE WHEN idTypeClient IS NOT NULL THEN idTypeClient else '-1' END AS ID 
                           FROM  Client ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInContact()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idContactType IS NOT NULL THEN idContactType ELSE '-1' END AS ID 
                           FROM  Contacts ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInContactReason()
        {
            string query = string.Format(@"
                           SELECT  distinct CASE WHEN idContactReason IS NOT NULL THEN idContactReason ELSE '-1' END AS ID 
                           FROM  Contacts ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInEmloyeeFunction()
        {
            string query = string.Format(@"
                           SELECT  distinct CASE WHEN [Function] IS NOT NULL THEN [Function] ELSE '-1' END AS ID 
                           FROM  Employees ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInEmployeestatus()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN statusEmployee IS NOT NULL THEN statusEmployee ELSE '-1' END AS ID 
                           FROM  Employees ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInMeetingPriority()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN priorityMeeting IS NOT NULL THEN priorityMeeting ELSE '-1' END AS ID 
                           FROM  Meetings ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInMeetingStatus()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN statusMeeting IS NOT NULL THEN statusMeeting ELSE '-1' END AS ID 
                           FROM  Meetings ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInMeetingCategory()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN categoryMeetingId IS NOT NULL THEN categoryMeetingId ELSE '-1' END AS ID 
                           FROM  Meetings ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInToDoType()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idToDoType IS NOT NULL THEN idToDoType ELSE '-1' END AS ID
                           FROM  ToDo ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInToDoStatus()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idStatusToDo IS NOT NULL THEN idStatusToDo ELSE '-1' END AS ID
                           FROM  ToDo ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInToDoPriority()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idPriorityToDo IS NOT NULL THEN idPriorityToDo ELSE '-1' END AS ID
                           FROM  ToDo ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInMedAnsType()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idAnsType IS NOT NULL THEN idAnsType ELSE '-1' END AS ID
                           FROM  MedAns ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInVolAnsType()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idAnsType IS NOT NULL THEN idAnsType ELSE '-1' END AS ID
                           FROM  VolAns ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable isInTitle()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN titleEmployee IS NOT NULL THEN titleEmployee ELSE '-1' END AS ID
                           FROM  Employees
                           UNION
                           SELECT distinct CASE WHEN idTitle IS NOT NULL THEN idTitle ELSE '-1' END AS ID
                           FROM ContactPerson");
            return conn.executeSelectQuery(query, null);
        }
        #endregion
    }
}
