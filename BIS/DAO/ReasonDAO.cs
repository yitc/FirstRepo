using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
   public class ReasonDAO
    {
         private dbConnection conn;
         public ReasonDAO()
        {
            conn = new dbConnection();

        }

       public DataTable GetAllType(string language)
       {
           string query = string.Format(
               @"(SELECT idReasonOut AS ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameReasonOut END AS Name, CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Contact person reason out' END AS Type
                  FROM ContactPersonReasonOut cpro
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = cpro.nameReasonOut
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Contact person reason out'
                  UNION
                  SELECT idReasonIn as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameReasonIn END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Contact person reason in' END AS Type
                  FROM ContactPersonReasonIn cpri
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = cpri.nameReasonIn
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Contact person reason in'
                  UNION
                  SELECT idReasonIn as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameReasonIn END AS Name,  CASE WHEN s2.stringValue IS NOT NULL THEN s2.stringValue ELSE 'Voluntary reason in' END AS Type
                  FROM VoluntaryReasonIn vri
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = vri.nameReasonIn
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Voluntary reason in'
                  UNION
                  SELECT idReasonOut as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameReasonOut END AS Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Voluntary reason out' END AS Type
                  FROM VoluntaryReasonOut vro 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = vro.nameReasonOut
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Voluntary reason out'
                 ) order by Type
                  ");


           SqlParameter[] sqlParameters = new SqlParameter[1];
           sqlParameters[0] = new SqlParameter("@language", SqlDbType.VarChar);
           sqlParameters[0].Value = language;

           return conn.executeSelectQuery(query, sqlParameters);
       }

       public Boolean UpdateContactPersonReasonOut(int idReasonOut, string nameReasonOut, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"UPDATE ContactPersonReasonOut
                SET nameReasonOut = @nameReasonOut
	        	WHERE idReasonOut = @idReasonOut");
           SqlParameter[] sqlParameter = new SqlParameter[2];
           sqlParameter[0] = new SqlParameter("@idReasonOut", SqlDbType.Int);
           sqlParameter[0].Value = idReasonOut;

           sqlParameter[1] = new SqlParameter("@nameReasonOut", SqlDbType.VarChar);
           sqlParameter[1].Value = nameReasonOut;

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
           sqlParameter[4].Value = idReasonOut;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonOut";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "ContactPersonReasonOut";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Update contact person reason out";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public Boolean UpdateContactPersonReasonIn(int idReasonIn, string nameReasonIn, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"UPDATE ContactPersonReasonIn
                SET nameReasonIn = @nameReasonIn
	        	WHERE idReasonIn = @idReasonIn");
           SqlParameter[] sqlParameter = new SqlParameter[2];
           sqlParameter[0] = new SqlParameter("@idReasonIn", SqlDbType.Int);
           sqlParameter[0].Value = idReasonIn;

           sqlParameter[1] = new SqlParameter("@nameReasonIn", SqlDbType.VarChar);
           sqlParameter[1].Value = nameReasonIn;

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
           sqlParameter[4].Value = idReasonIn;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonIn";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "ContactPersonReasonIn";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Delete contact person reason in";

           
           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public Boolean UpdateVoluntaryReasonIn(int idReasonIn, string nameReasonIn, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"UPDATE VoluntaryReasonIn
                SET nameReasonIn = @nameReasonIn
	        	WHERE idReasonIn = @idReasonIn");
           SqlParameter[] sqlParameter = new SqlParameter[2];
           sqlParameter[0] = new SqlParameter("@idReasonIn", SqlDbType.Int);
           sqlParameter[0].Value = idReasonIn;

           sqlParameter[1] = new SqlParameter("@nameReasonIn", SqlDbType.VarChar);
           sqlParameter[1].Value = nameReasonIn;

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
           sqlParameter[4].Value = idReasonIn;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonIn";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "VoluntaryReasonIn";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Update voluntary reason in";
           
           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public Boolean UpdateVoluntaryReasonOut(int idReasonOut, string nameReasonOut, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"UPDATE VoluntaryReasonOut
                SET nameReasonOut = @nameReasonOut
	        	WHERE idReasonOut = @idReasonOut");
           SqlParameter[] sqlParameter = new SqlParameter[2];
           sqlParameter[0] = new SqlParameter("@idReasonOut", SqlDbType.Int);
           sqlParameter[0].Value = idReasonOut;

           sqlParameter[1] = new SqlParameter("@nameReasonOut", SqlDbType.VarChar);
           sqlParameter[1].Value = nameReasonOut;

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
           sqlParameter[4].Value = idReasonOut;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonOut";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "VoluntaryReasonOut";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Update voluntary reason out";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public bool InsertContactPersonReasonOut(string nameReasonOut, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"INSERT INTO 
                                   ContactPersonReasonOut(nameReasonOut)
                                    VALUES(@nameReasonOut)");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@nameReasonOut", SqlDbType.NChar);
           sqlParameter[0].Value = nameReasonOut;

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
           sqlParameter[4].Value = conn.GetLastTableID("ContactPersonReasonOut") + 1;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonOut";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "ContactPersonReasonOut";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Insert contact person reason out";


           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);

       }
       /// <summary>
       /// /////
       /// </summary>
       /// <param name="nameReasonIn"></param>
       /// <param name="nameForm"></param>
       /// <param name="idUser"></param>
       /// <returns></returns>
       public bool InsertContactPersonReasonIn(string nameReasonIn, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"INSERT INTO 
                                   ContactPersonReasonIn(nameReasonIn)
                                    VALUES(@nameReasonIn)");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@nameReasonIn", SqlDbType.NChar);
           sqlParameter[0].Value = nameReasonIn;

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
           sqlParameter[4].Value = conn.GetLastTableID("ContactPersonReasonIn") + 1;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonIn";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonReasonIn";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Insert contact person reason in";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);

       }

       public bool InsertVoluntaryReasonIn(string nameReasonIn, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"INSERT INTO 
                                   VoluntaryReasonIn(nameReasonIn)
                                    VALUES(@nameReasonIn)");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@nameReasonIn", SqlDbType.NChar);
           sqlParameter[0].Value = nameReasonIn;

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
           sqlParameter[4].Value = conn.GetLastTableID("VoluntaryReasonIn") + 1;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonIn";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "VoluntaryReasonIn";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Insert voluntary reason in";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);

       }

       public bool InsertVoluntaryReasonOut(string nameReasonOut, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"INSERT INTO 
                                   VoluntaryReasonOut(nameReasonOut)
                                    VALUES(@nameReasonOut)");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@nameReasonOut", SqlDbType.NChar);
           sqlParameter[0].Value = nameReasonOut;

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
           sqlParameter[4].Value = conn.GetLastTableID("VoluntaryReasonOut") + 1;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonOut";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "VoluntaryReasonOut";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Insert voluntary reason out";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);

       }

       public bool DeleteContactPersonReasonOut(int idReasonOut, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"DELETE FROM  ContactPersonReasonOut WHERE idReasonOut = @idReasonOut");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@idReasonOut", SqlDbType.Int);
           sqlParameter[0].Value = idReasonOut;

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
           sqlParameter[4].Value = idReasonOut;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonOut";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonReasonOut";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Delete contact person reason out";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public bool DeleteContactPersonReasonIn(int idReasonIn, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"DELETE FROM  ContactPersonReasonIn WHERE idReasonIn = @idReasonIn");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@idReasonIn", SqlDbType.Int);
           sqlParameter[0].Value = idReasonIn;

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
           sqlParameter[4].Value = idReasonIn;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonIn";
           
           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "ContactPersonReasonIn";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Update contact person reason in";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public bool DeleteVoluntaryReasonIn(int idReasonIn, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"DELETE FROM  VoluntaryReasonIn WHERE idReasonIn = @idReasonIn");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@idReasonIn", SqlDbType.Int);
           sqlParameter[0].Value = idReasonIn;

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
           sqlParameter[4].Value = idReasonIn;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonIn";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "VoluntaryReasonIn";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Update voluntary reason in";


           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public bool DeleteVoluntaryReasonOut(int idReasonOut, string nameForm, int idUser)
       {
           List<string> _query = new List<string>();
           List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

           string query = string.Format(@"DELETE FROM  VoluntaryReasonOut WHERE idReasonOut = @idReasonOut");

           SqlParameter[] sqlParameter = new SqlParameter[1];

           sqlParameter[0] = new SqlParameter("@idReasonOut", SqlDbType.Int);
           sqlParameter[0].Value = idReasonOut;

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
           sqlParameter[4].Value = idReasonOut;

           sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
           sqlParameter[5].Value = "idReasonOut";

           sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
           sqlParameter[6].Value = "VoluntaryReasonOut";

           sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
           sqlParameter[7].Value = "Update voluntary reason out";

           _query.Add(query);
           sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);
       }

       public DataTable AllCPReasonOut()
       {
           string query = string.Format(
              @"SELECT distinct cp.idReasonOut as ID
                  FROM ContactPerson cp
                 LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers 
                    WHERE cpf.idFilter != '4'");

           return conn.executeSelectQuery(query, null);

       }

       public DataTable AllCPReasonIn()
       {
           string query = string.Format(
              @"SELECT distinct cp.idReasonIn as ID
                  FROM ContactPerson cp
                 LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers 
                    WHERE cpf.idFilter != '4'");

           return conn.executeSelectQuery(query, null);

       }

       public DataTable AllVHReasonOut()
       {
           string query = string.Format(
              @"SELECT distinct cp.idReasonOut as ID
                  FROM ContactPerson cp
                 LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers 
                    WHERE cpf.idFilter = '4'");

           return conn.executeSelectQuery(query, null);

       }

       public DataTable AllVHReasonIn()
       {
           string query = string.Format(
              @"SELECT distinct cp.idReasonIn as ID
                  FROM ContactPerson cp
                 LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers 
                    WHERE cpf.idFilter = '4'");

           return conn.executeSelectQuery(query, null);

       }
    }
}
