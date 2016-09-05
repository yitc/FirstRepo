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
    public class DocumentsDAO
    {
        private dbConnection conn;
        public DocumentsDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetPersonDoc(int idPerson, string idLang)
        {
            string query = string.Format(@"SELECT idDocument,typeDocument,descriptionDocument,fileDocument,inOutDocument,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE (CASE WHEN inOutDocument=1 THEN 'Ours' ELSE CASE WHEN inOutDocument=2 THEN 'Incoming' ELSE NULL END END) END as nameInOutDocuments,d.idProject,d.idClient,c.nameClient,d.idContPers, cp.firstname+' '+cp.lastname as namePerson ,d.idEmployee,e.firstNameEmployee + ' ' +e.lastNameEmployee  as nameEmployee,idResponsableEmployee,er.firstNameEmployee + ' ' +er.lastNameEmployee  as nameEmployeeResponsible
                      ,d.idDocumentStatus,CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE ds.descriptionStatus END as nameStatus,noteDocument,idLayout,
                      d.userCreated,d.dtCreated,d.userModified,d.dtModified, uc.nameUser as nameUserCreated, um.nameUser as nameUserModified,p.nameProject,d.idarrangement, arr.nameArrangement
                        FROM Documents d
                        LEFT OUTER JOIN ContactPerson cp on cp.idContPers = d.idContPers
                        LEFT OUTER JOIN Client c on c.idClient = d.idClient
                        LEFT OUTER JOIN Employees e on e.idEmployee = d.idEmployee
                        LEFT OUTER JOIN Employees er on er.idEmployee = d.idResponsableEmployee
                        LEFT JOIN STRING" + idLang + @" s ON (CASE WHEN inOutDocument=1 THEN 'Ours' ELSE CASE WHEN inOutDocument=2 THEN 'Incoming' ELSE NULL END END) = s.stringKey
                        LEFT JOIN DocumentStatus ds ON ds.idDocumentStatus = d.idDocumentStatus
                        LEFT JOIN String" + idLang + @" std ON std.stringKey = ds.descriptionStatus
                        LEFT OUTER JOIN Users uc ON uc.idUser = d.userCreated
                        LEFT OUTER JOIN Users um ON um.idUser = d.userModified
                        LEFT OUTER JOIN Project p ON p.idProject = d.idProject
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= d.idArrangement
                        WHERE d.idContPers ='" + idPerson.ToString() + "'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idPerson", SqlDbType.Int);
            sqlParameters[0].Value = idPerson;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetArrangementDoc(int idArrangement, string idLang)
        {
            string query = string.Format(@"SELECT idDocument,typeDocument,descriptionDocument,fileDocument,inOutDocument,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE (CASE WHEN inOutDocument=1 THEN 'Ours' ELSE CASE WHEN inOutDocument=2 THEN 'Incoming' ELSE NULL END END) END as nameInOutDocuments,d.idProject,d.idClient,c.nameClient,d.idContPers, cp.firstname+' '+cp.lastname as namePerson ,d.idEmployee,e.firstNameEmployee + ' ' +e.lastNameEmployee  as nameEmployee,idResponsableEmployee,er.firstNameEmployee + ' ' +er.lastNameEmployee  as nameEmployeeResponsible
                      ,d.idDocumentStatus,CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE ds.descriptionStatus END as nameStatus,noteDocument,idLayout,
                      d.userCreated,d.dtCreated,d.userModified,d.dtModified, uc.nameUser as nameUserCreated, um.nameUser as nameUserModified,p.nameProject,d.idarrangement, arr.nameArrangement
                        FROM Documents d
                        LEFT OUTER JOIN ContactPerson cp on cp.idContPers = d.idContPers
                        LEFT OUTER JOIN Client c on c.idClient = d.idClient
                        LEFT OUTER JOIN Employees e on e.idEmployee = d.idEmployee
                        LEFT OUTER JOIN Employees er on er.idEmployee = d.idResponsableEmployee
                        LEFT JOIN STRING" + idLang + @" s ON (CASE WHEN inOutDocument=1 THEN 'Ours' ELSE CASE WHEN inOutDocument=2 THEN 'Incoming' ELSE NULL END END) = s.stringKey
                        LEFT JOIN DocumentStatus ds ON ds.idDocumentStatus = d.idDocumentStatus
                        LEFT JOIN String" + idLang + @" std ON std.stringKey = ds.descriptionStatus
                        LEFT OUTER JOIN Users uc ON uc.idUser = d.userCreated
                        LEFT OUTER JOIN Users um ON um.idUser = d.userModified
                        LEFT OUTER JOIN Project p ON p.idProject = d.idProject
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= d.idArrangement
                        WHERE d.idarrangement ='" + idArrangement.ToString() + "'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idarrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetDocument(int idDocument)
        {
            string query = string.Format(@"SELECT idDocument,typeDocument,descriptionDocument,fileDocument,inOutDocument,d.idProject,c.nameClient,d.idClient,d.idContPers, cp.firstname+' '+cp.lastname as namePerson,e.firstNameEmployee + ' ' +e.lastNameEmployee  as nameEmployee,d.idEmployee,idResponsableEmployee,
                        er.firstNameEmployee + ' ' +er.lastNameEmployee  as nameEmployeeResponsible, uc.nameUser as nameUserCreated, um.nameUser as nameUserModified,p.nameProject
                      ,idDocumentStatus,noteDocument,idLayout,d.userCreated,d.dtCreated,d.userModified,d.dtModified, d.idArrangement, arr.nameArrangement
                        FROM Documents d
                        LEFT OUTER JOIN ContactPerson cp on cp.idContPers = d.idContPers
                        LEFT OUTER JOIN Client c on c.idClient = d.idClient
                        LEFT OUTER JOIN Employees e on e.idEmployee = d.idEmployee
                        LEFT OUTER JOIN Employees er on er.idEmployee = d.idResponsableEmployee
                        LEFT OUTER JOIN Users uc ON uc.idUser = d.userCreated
                        LEFT OUTER JOIN Users um ON um.idUser = d.userModified
                        LEFT OUTER JOIN Project p ON p.idProject = d.idProject
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= d.idArrangement
                        WHERE idDocument ='" + idDocument.ToString() + "' ");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameters[0].Value = idDocument;
            return conn.executeSelectQuery(query, sqlParameters);

        }
        
        public DataTable GetClientDoc(int idClient, string idLang)
        {
         
            string query = string.Format(@"SELECT idDocument,typeDocument,descriptionDocument,fileDocument,inOutDocument,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE (CASE WHEN inOutDocument=1 THEN 'Ours' ELSE CASE WHEN inOutDocument=2 THEN 'Incoming' ELSE NULL END END) END as nameInOutDocuments,d.idProject,d.idClient,
                        c.nameClient,d.idContPers, cp.firstname+' '+cp.lastname as namePerson ,d.idEmployee,e.firstNameEmployee + ' ' +e.lastNameEmployee  as nameEmployee,idResponsableEmployee,er.firstNameEmployee + ' ' +er.lastNameEmployee  as nameEmployeeResponsible, uc.nameUser as nameUserCreated, um.nameUser as nameUserModified,p.nameProject
                      ,d.idDocumentStatus,CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE ds.descriptionStatus END as nameStatus,noteDocument,idLayout,
                       d.userCreated,d.dtCreated,d.userModified,d.dtModified, d.idarrangement, arr.nameArrangement
                        FROM Documents d
                        LEFT OUTER JOIN ContactPerson cp on cp.idContPers = d.idContPers
                        LEFT OUTER JOIN Client c on c.idClient = d.idClient
                        LEFT OUTER JOIN Employees e on e.idEmployee = d.idEmployee
                        LEFT OUTER JOIN Employees er on er.idEmployee = d.idResponsableEmployee
                        LEFT JOIN STRING" + idLang + @" s ON (CASE WHEN inOutDocument=1 THEN 'Ours' ELSE CASE WHEN inOutDocument=2 THEN 'Incoming' ELSE NULL END END) = s.stringKey
                        LEFT JOIN DocumentStatus ds ON ds.idDocumentStatus = d.idDocumentStatus
                        LEFT JOIN String" + idLang + @" std ON std.stringKey = ds.descriptionStatus
                        LEFT OUTER JOIN Users uc ON uc.idUser = d.userCreated
                        LEFT OUTER JOIN Users um ON um.idUser = d.userModified
                        LEFT OUTER JOIN Project p ON p.idProject = d.idProject
                        LEFT OUTER JOIN Arrangement arr ON arr.idArrangement= d.idArrangement 
                        WHERE d.idClient ='" + idClient.ToString() + "'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Update(DocumentsModel PersDoc, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Documents SET  
                     typeDocument = @typeDocument, descriptionDocument = @descriptionDocument, fileDocument = @fileDocument, inOutDocument = @inOutDocument, 
                    idProject = @idProject, idClient = @idClient, idContPers = @idContPers, idEmployee = @idEmployee, 
                    idResponsableEmployee = @idResponsableEmployee, idDocumentStatus = @idDocumentStatus, noteDocument = @noteDocument, idLayout = @idLayout, 
                    userCreated = @userCreated, dtCreated = @dtCreated, userModified = @userModified, dtModified = @dtModified ,
                    idArrangement=@idArrangement WHERE  idDocument = @idDocument");


            SqlParameter[] sqlParameter = new SqlParameter[18];

            sqlParameter[0] = new SqlParameter("@typeDocument", SqlDbType.NVarChar);
            sqlParameter[0].Value = PersDoc.typeDocument;

            sqlParameter[1] = new SqlParameter("@descriptionDocument", SqlDbType.NVarChar);
            sqlParameter[1].Value = (PersDoc.descriptionDocument == null) ? SqlString.Null : PersDoc.descriptionDocument;

            sqlParameter[2] = new SqlParameter("@fileDocument", SqlDbType.NVarChar);
            sqlParameter[2].Value = PersDoc.fileDocument;

            sqlParameter[3] = new SqlParameter("@inOutDocument", SqlDbType.Decimal);
            sqlParameter[3].Value = PersDoc.inOutDocument;

            sqlParameter[4] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[4].Value = (PersDoc.idProject == 0) ? SqlInt32.Null : PersDoc.idProject;

            sqlParameter[5] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[5].Value = (PersDoc.idClient == 0) ? SqlInt32.Null : PersDoc.idClient;

            sqlParameter[6] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[6].Value = PersDoc.idContPers;

            sqlParameter[7] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[7].Value = (PersDoc.idEmployee == 0) ? SqlInt32.Null : PersDoc.idEmployee;

            sqlParameter[8] = new SqlParameter("@idResponsableEmployee", SqlDbType.Int);
            sqlParameter[8].Value = (PersDoc.idResponsableEmployee == 0) ? SqlInt32.Null : PersDoc.idResponsableEmployee;

            sqlParameter[9] = new SqlParameter("@idDocumentStatus", SqlDbType.Int);
            sqlParameter[9].Value = (PersDoc.idDocumentStatus == 0) ? SqlInt32.Null : PersDoc.idDocumentStatus;

            sqlParameter[10] = new SqlParameter("@noteDocument", SqlDbType.NVarChar);
            sqlParameter[10].Value = PersDoc.noteDocument;

            sqlParameter[11] = new SqlParameter("@idLayout", SqlDbType.Int);
            sqlParameter[11].Value = (PersDoc.idLayout == 0) ? SqlInt32.Null : PersDoc.idLayout;

            sqlParameter[12] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[12].Value = PersDoc.userCreated;


            sqlParameter[13] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[13].Value = (PersDoc.dtCreated == null || PersDoc.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : PersDoc.dtCreated;

            sqlParameter[14] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[14].Value = (PersDoc.userModified == 0) ? SqlInt32.Null : PersDoc.userModified;


            sqlParameter[15] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[15].Value = (PersDoc.dtModified == null || PersDoc.dtModified == DateTime.MinValue) ? SqlDateTime.Null : PersDoc.dtModified;

            sqlParameter[16] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameter[16].Value = PersDoc.idDocument;


            sqlParameter[17] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[17].Value = PersDoc.idArrangement;


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
            sqlParameter[4].Value = PersDoc.idDocument;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDocument";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Documents";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  Documents WHERE idDocument = @idDocument");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idDocument", SqlDbType.Int);
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
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDocument";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Documents";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete ";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool RemoveClientFromDocument(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Documents SET idClient = '0' WHERE idDocument = @idDocument");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idDocument", SqlDbType.Int);
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
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDocument";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Documents";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Remove client from document";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Save(DocumentsModel PersDoc, string nameForm,int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  Documents (typeDocument, descriptionDocument, fileDocument, inOutDocument, 
                                 idProject, idClient, idContPers , idEmployee , idResponsableEmployee , idDocumentStatus, noteDocument, idLayout , 
                                 userCreated, dtCreated , userModified , dtModified, idArrangement )
                        Values ( @typeDocument,  @descriptionDocument, @fileDocument,  @inOutDocument,  @idProject,  @idClient, @idContPers,  @idEmployee, 
                                @idResponsableEmployee, @idDocumentStatus,@noteDocument, @idLayout, 
                                 @userCreated,  @dtCreated,@userModified, @dtModified, @idArrangement)");

            //,  @idDocument , idDocument
            SqlParameter[] sqlParameter = new SqlParameter[17];

            sqlParameter[0] = new SqlParameter("@typeDocument", SqlDbType.NVarChar);
            sqlParameter[0].Value = PersDoc.typeDocument;

            sqlParameter[1] = new SqlParameter("@descriptionDocument", SqlDbType.NVarChar);
            sqlParameter[1].Value = (PersDoc.descriptionDocument == null) ? SqlString.Null : PersDoc.descriptionDocument;

            sqlParameter[2] = new SqlParameter("@fileDocument", SqlDbType.NVarChar);
            sqlParameter[2].Value = PersDoc.fileDocument;

            sqlParameter[3] = new SqlParameter("@inOutDocument", SqlDbType.NVarChar);
            sqlParameter[3].Value = PersDoc.inOutDocument;


            sqlParameter[4] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[4].Value = (PersDoc.idProject == 0) ? SqlInt32.Null : PersDoc.idProject;

            sqlParameter[5] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[5].Value = (PersDoc.idClient == 0) ? SqlInt32.Null : PersDoc.idClient;

            sqlParameter[6] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[6].Value = PersDoc.idContPers;

            sqlParameter[7] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[7].Value = (PersDoc.idEmployee == 0) ? SqlInt32.Null : PersDoc.idEmployee;

            sqlParameter[8] = new SqlParameter("@idResponsableEmployee", SqlDbType.Int);
            sqlParameter[8].Value = (PersDoc.idResponsableEmployee == 0) ? SqlInt32.Null : PersDoc.idResponsableEmployee;

            sqlParameter[9] = new SqlParameter("@idDocumentStatus", SqlDbType.Int);
            sqlParameter[9].Value = (PersDoc.idDocumentStatus == 0) ? SqlInt32.Null : PersDoc.idDocumentStatus;

            sqlParameter[10] = new SqlParameter("@noteDocument", SqlDbType.NVarChar);
            sqlParameter[10].Value = PersDoc.noteDocument;

            sqlParameter[11] = new SqlParameter("@idLayout", SqlDbType.Int);
            sqlParameter[11].Value = (PersDoc.idLayout == 0) ? SqlInt32.Null : PersDoc.idLayout;

            sqlParameter[12] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[12].Value = PersDoc.userCreated;


            sqlParameter[13] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[13].Value = (PersDoc.dtCreated == null || PersDoc.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : PersDoc.dtCreated;

            sqlParameter[14] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[14].Value = (PersDoc.userModified == 0) ? SqlInt32.Null : PersDoc.userModified;


            sqlParameter[15] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[15].Value = (PersDoc.dtModified == null || PersDoc.dtModified == DateTime.MinValue) ? SqlDateTime.Null : PersDoc.dtModified;

            sqlParameter[16] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[16].Value = (PersDoc.idArrangement == 0) ? SqlInt32.Null : PersDoc.idArrangement;


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
            sqlParameter[4].Value = conn.GetLastTableID("Documents")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDocument";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Documents";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public object CheckDocumentIdClient(int id)
        {
            string query = string.Format(@"SELECT idClient FROM Documents WHERE idDocument = @idDocument");
            
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeScalarQuery(query, sqlParameters);
        }

        public object CheckDocumentIdProject(int id)
        {
            string query = string.Format(@"SELECT idProject FROM Documents WHERE idDocument = @idDocument");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeScalarQuery(query, sqlParameters);
        }

        public object CheckDocumentIdEmployee(int id)
        {
            string query = string.Format(@"SELECT idEmployee FROM Documents WHERE idDocument = @idDocument");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeScalarQuery(query, sqlParameters);
        }

        public object CheckDocumentidArrangement(int id)
        {
            string query = string.Format(@"SELECT idArrangement FROM Documents WHERE idDocument = @idDocument");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeScalarQuery(query, sqlParameters);
        }
    }
}
