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
    public class ContactsDAO
    {
        private dbConnection conn;
        public ContactsDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetContactsALL()
        {
            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact, uc.nameUser as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject 
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason  
                        LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject");

            
            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetContactsByPerson(int idContPers)
        {
            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact, uc.firstnameEmployee +' '+uc.midnameEmployee+' '+uc.lastnameEmployee  as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject , c.idArrangement, arr.nameArrangement
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason
                        LEFT OUTER JOIN Employees uc ON uc.idEmployee = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= c.idArrangement
                        WHERE c.idContPers ='" + idContPers.ToString() + "' ");

            //   LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetContactsByArrangament(int idArrangament)
        {
            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact, uc.firstnameEmployee +' '+uc.midnameEmployee+' '+uc.lastnameEmployee  as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject , c.idArrangement, arr.nameArrangement
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason
                        LEFT OUTER JOIN Employees uc ON uc.idEmployee = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= c.idArrangement
                        WHERE c.idArrangement ='" + idArrangament.ToString() + "' ");

            //   LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangament;
            return conn.executeSelectQuery(query, sqlParameters);

        }
        public DataTable GetContactsByClient(int idClient)
        {
            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact,  uc.firstnameEmployee +' '+uc.midnameEmployee+' '+uc.lastnameEmployee as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject , c.idArrangement, arr.nameArrangement
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason
                        LEFT OUTER JOIN Employees uc ON uc.idEmployee = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= c.idArrangement
                        WHERE c.idClient ='" + idClient.ToString() + "' ");

            //   LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;
            return conn.executeSelectQuery(query, sqlParameters);

        }
        public DataTable GetContactsByCreator(int idCreator)
        {
            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact, uc.nameUser as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject 
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason
                        LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        WHERE idCreator=@idCreator ");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idCreator", SqlDbType.Int);
            sqlParameters[0].Value = idCreator;
            return conn.executeSelectQuery(query, sqlParameters);

        }


        public DataTable GetContactById(int idContact)
        {
            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact, uc.nameUser as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject, c.idArrangement,arr.nameArrangement
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason
                        LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        LEFT OUTER JOIN Arrangement arr ON arr.idArrangement=c.idArrangement
                        WHERE c.idContact ='" + idContact.ToString() + "' ");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameters[0].Value = idContact;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetContactsByCreatorTypes(int idCreator, int reason, int type)
        {
            string par1 = "%";
            if (reason != 0)
                par1 = reason.ToString();

            string par2 = "%";
            if (type != 0)
                par2 = type.ToString();

            string query = string.Format(@"SELECT c.idContact,c.idContactReason,descContactReason,c.reasonContact,c.idContactType,descContactType,c.dateContact,c.openTimeContact,
                    c.closeTimeCOntact,c.durationContact,c.idCreator,c.idClient,c.idContPers, c.idProject,c.noteContact, uc.nameUser as nameUserCreated, cp.firstname + '' +cp.midname + '' +cp.lastname as nameContPers,
                    cl.nameClient,p.nameProject 
                        FROM Contacts c
                        LEFT OUTER JOIN ContactType d ON c.idContactType = d.idContactType
                        LEFT OUTER JOIN ContactReason e ON c.idContactReason = e.idContactReason
                        LEFT OUTER JOIN Users uc ON uc.idUser = c.idCreator
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPers
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        WHERE idCreator=@idCreator and c.idContactReason like '" + par1 + "' and c.idContactType like '" + par2 + "'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idCreator", SqlDbType.Int);
            sqlParameters[0].Value = idCreator;
            return conn.executeSelectQuery(query, sqlParameters);

        }


        public bool Update(ContactsModel model, int idContact, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            
            string query = string.Format(@"UPDATE Contacts SET idContactReason=@idContactReason,reasonContact=@reasonContact,idContactType=@idContactType,dateContact=@dateContact,
                                    openTimeContact=@openTimeContact, closeTimeCOntact=@closeTimeCOntact,durationContact=@durationContact,idCreator=@idCreator,
                                    idClient=@idClient,idContPers=@idContPers,idProject=@idProject,noteContact=@noteContact, idArrangement=@idArrangement
                                    WHERE idContact =@idContact");


            SqlParameter[] sqlParameter = new SqlParameter[14];

            sqlParameter[0] = new SqlParameter("@idContactReason", SqlDbType.Int);
            sqlParameter[0].Value = model.idContactReason;

            sqlParameter[1] = new SqlParameter("@reasonContact", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.reasonContact;

            sqlParameter[2] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameter[2].Value = model.idContactType;

            sqlParameter[3] = new SqlParameter("@dateContact", SqlDbType.DateTime);
            sqlParameter[3].Value = (model.dateContact == null || model.dateContact == DateTime.MinValue) ? SqlDateTime.Null : model.dateContact;

            sqlParameter[4] = new SqlParameter("@openTimeContact", SqlDbType.Time);
            sqlParameter[4].Value = model.openTimeContact;
            sqlParameter[5] = new SqlParameter("@closeTimeCOntact", SqlDbType.Time);
            sqlParameter[5].Value = model.closeTimeCOntact;
            sqlParameter[6] = new SqlParameter("@durationContact", SqlDbType.Time);
            sqlParameter[6].Value = model.durationContact;




            //sqlParameter[4].Value = (model.openTimeContact == null) ? (object)SqlDateTime.Null : model.openTimeContact;

            //sqlParameter[5].Value = (model.closeTimeCOntact == null) ? (object)SqlDateTime.Null : model.closeTimeCOntact;

            //sqlParameter[6].Value = (model.durationContact == null) ? (object)SqlDateTime.Null : model.durationContact;

            sqlParameter[7] = new SqlParameter("@idCreator", SqlDbType.Int);
            sqlParameter[7].Value = model.idCreator;

            sqlParameter[8] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[8].Value = model.idClient;

            sqlParameter[9] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[9].Value = model.idContPers;

            sqlParameter[10] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[10].Value = model.idProject;

            sqlParameter[11] = new SqlParameter("@noteContact", SqlDbType.NVarChar);
            sqlParameter[11].Value = (model.noteContact == null) ? SqlString.Null : model.noteContact;

            sqlParameter[12] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameter[12].Value = model.idContact;

            sqlParameter[13] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[13].Value = (model.idArrangement == -1) ? SqlInt32.Null : model.idArrangement;

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
            sqlParameter[4].Value = conn.GetLastTableID("Contacts")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContact";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Contacts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }
        public bool Save(ContactsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Contacts  (idContactReason,reasonContact,idContactType,dateContact,
                                    openTimeContact, closeTimeCOntact,durationContact,idCreator,idClient,idContPers,idProject,noteContact, idArrangement)
                             VALUES (@idContactReason,@reasonContact,@idContactType,@dateContact,@openTimeContact,@closeTimeCOntact,@durationContact,@idCreator,
                                    @idClient,@idContPers,@idProject,@noteContact, @idArrangement)");


            SqlParameter[] sqlParameter = new SqlParameter[14];

            sqlParameter[0] = new SqlParameter("@idContactReason", SqlDbType.Int);
            sqlParameter[0].Value = model.idContactReason;

            sqlParameter[1] = new SqlParameter("@reasonContact", SqlDbType.NVarChar);
            //sqlParameters[1].Value = model.reasonContact;
            sqlParameter[1].Value = (model.reasonContact == null) ? SqlString.Null : model.reasonContact;

            sqlParameter[2] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameter[2].Value = model.idContactType;

            sqlParameter[3] = new SqlParameter("@dateContact", SqlDbType.DateTime);
            sqlParameter[3].Value = (model.dateContact == null || model.dateContact == DateTime.MinValue) ? SqlDateTime.Null : model.dateContact;
            sqlParameter[4] = new SqlParameter("@openTimeContact", SqlDbType.Time);
            sqlParameter[4].Value = model.openTimeContact;
            sqlParameter[5] = new SqlParameter("@closeTimeCOntact", SqlDbType.Time);
            sqlParameter[5].Value = model.closeTimeCOntact;
            sqlParameter[6] = new SqlParameter("@durationContact", SqlDbType.Time);
            sqlParameter[6].Value = model.durationContact;

            //sqlParameters[4].Value = (model.openTimeContact == null) ? (object)SqlDateTime.Null : model.openTimeContact;

            //sqlParameters[5].Value = (model.closeTimeCOntact == null) ? (object)SqlDateTime.Null : model.closeTimeCOntact;

            //sqlParameters[6].Value = (model.durationContact == null) ? (object)SqlDateTime.Null : model.durationContact;

            sqlParameter[7] = new SqlParameter("@idCreator", SqlDbType.Int);
            sqlParameter[7].Value = model.idCreator;

            sqlParameter[8] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[8].Value = model.idClient;

            sqlParameter[9] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[9].Value = model.idContPers;

            sqlParameter[10] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[10].Value = model.idProject;

            sqlParameter[11] = new SqlParameter("@noteContact", SqlDbType.NVarChar);
            sqlParameter[11].Value = (model.noteContact == null) ? SqlString.Null : model.noteContact;

            sqlParameter[12] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameter[12].Value = model.idContact;

            sqlParameter[13] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[13].Value = (model.idArrangement == -1) ? SqlInt32.Null : model.idArrangement;

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
            sqlParameter[4].Value = conn.GetLastTableID("Contacts") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContact";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Contacts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }
        public Int32 SaveID(ContactsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Contacts  (idContactReason,reasonContact,idContactType,dateContact,
                                    openTimeContact, closeTimeCOntact,durationContact,idCreator,idClient,idContPers,idProject,noteContact)
                             VALUES (@idContactReason,@reasonContact,@idContactType,@dateContact,@openTimeContact,@closeTimeCOntact,@durationContact,@idCreator,
                                    @idClient,@idContPers,@idProject,@noteContact)");


            SqlParameter[] sqlParameter = new SqlParameter[13];

            sqlParameter[0] = new SqlParameter("@idContactReason", SqlDbType.Int);
            sqlParameter[0].Value = model.idContactReason;

            sqlParameter[1] = new SqlParameter("@reasonContact", SqlDbType.NVarChar);
            //sqlParameter[1].Value = model.reasonContact;
            sqlParameter[1].Value = (model.reasonContact == null) ? SqlString.Null : model.reasonContact;

            sqlParameter[2] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameter[2].Value = model.idContactType;

            sqlParameter[3] = new SqlParameter("@dateContact", SqlDbType.DateTime);
            sqlParameter[3].Value = (model.dateContact == null || model.dateContact == DateTime.MinValue) ? SqlDateTime.Null : model.dateContact;
            sqlParameter[4] = new SqlParameter("@openTimeContact", SqlDbType.Time);
            sqlParameter[4].Value = model.openTimeContact;
            sqlParameter[5] = new SqlParameter("@closeTimeCOntact", SqlDbType.Time);
            sqlParameter[5].Value = model.closeTimeCOntact;
            sqlParameter[6] = new SqlParameter("@durationContact", SqlDbType.Time);
            sqlParameter[6].Value = model.durationContact;

            //sqlParameters[4].Value = (model.openTimeContact == null) ? (object)SqlDateTime.Null : model.openTimeContact;

            //sqlParameters[5].Value = (model.closeTimeCOntact == null) ? (object)SqlDateTime.Null : model.closeTimeCOntact;

            //sqlParameters[6].Value = (model.durationContact == null) ? (object)SqlDateTime.Null : model.durationContact;

            sqlParameter[7] = new SqlParameter("@idCreator", SqlDbType.Int);
            sqlParameter[7].Value = model.idCreator;

            sqlParameter[8] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[8].Value = model.idClient;

            sqlParameter[9] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[9].Value = model.idContPers;

            sqlParameter[10] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[10].Value = model.idProject;

            sqlParameter[11] = new SqlParameter("@noteContact", SqlDbType.NVarChar);
            sqlParameter[11].Value = (model.noteContact == null) ? SqlString.Null : model.noteContact;

            sqlParameter[12] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameter[12].Value = model.idContact;

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
            sqlParameter[4].Value = conn.GetLastTableID("Contacts") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContact";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Contacts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save id";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            conn.executQueryTransaction(_query, sqlParameters);
            string query1 = string.Format(@" SELECT TOP 1 idContact FROM Contacts where idContPers = @idContPers ORDER BY idContact DESC");
            
            SqlParameter[] sqlParameters1 = new SqlParameter[2];

            sqlParameters1[0] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameters1[0].Value = model.idContact;
            sqlParameters1[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters1[1].Value = model.idContPers;

            DataTable dt = conn.executeSelectQuery(query1, sqlParameters1);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return Int32.Parse(dt.Rows[0][0].ToString());

            return -1;
     
            //return conn.executeUpdateQuery(query, sqlParameters);


        }
        public Int32    GetID(ContactsModel model)
        {
            string query1 = string.Format(@" SELECT TOP 1 idContact FROM Contacts where idContPers = @idContPers ORDER BY idContact DESC");

            SqlParameter[] sqlParameters1 = new SqlParameter[2];

            sqlParameters1[0] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameters1[0].Value = model.idContact;
            sqlParameters1[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters1[1].Value = model.idContPers;

            DataTable dt = conn.executeSelectQuery(query1, sqlParameters1);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return Int32.Parse(dt.Rows[0][0].ToString());

            return -1;


        }


        public bool Delete(int id, string nameForm,int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Contacts WHERE idContact = @idContact");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idContact", SqlDbType.Int);
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
            sqlParameter[4].Value = conn.GetLastTableID("Contacts") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContact";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Contacts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}


        