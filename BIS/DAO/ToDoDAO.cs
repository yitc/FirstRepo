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
    public class ToDoDAO
    {
        private dbConnection conn;
        public ToDoDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetToDoALL()
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                         LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                         LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                         LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact");            
            
            return conn.executeSelectQuery(query, null);


        }
        public DataTable GetToDoPerson(int idContPers)
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject, c.idArrangement, arr.nameArrangement
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= c.idArrangement
                        WHERE c.idContPers ='" + idContPers.ToString() + "' ");

            // ,c.isRemider
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetToDoArrangement(int idArrangement)
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject, c.idArrangement, arr.nameArrangement
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                        LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= c.idArrangement
                        WHERE c.idArrangement ='" + idArrangement.ToString() + "' ");

            // ,c.isRemider
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetToDoContact(int idContact)
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                         LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                         LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                         LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                        WHERE c.idContact ='" + idContact.ToString() + "' ");

            // ,c.isRemider
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameters[0].Value = idContact;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        // jeca 12.12
        public DataTable GetTaskById(int idTask)
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient,
                     p.nameProject, c.idArrangement, arr.nameArrangement
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                         LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                         LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                         LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement=c.idArrangement
                        WHERE c.idToDo ='" + idTask.ToString() + "' ");

            // c.isRemider
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idTask", SqlDbType.Int);
            sqlParameters[0].Value = idTask;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetToDoClient(int idClient)
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject,  c.idArrangement, arr.nameArrangement
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                         LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                         LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                         LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                         LEFT OUTER JOIN Arrangement arr on arr.idArrangement= c.idArrangement
                        WHERE c.idClient ='" + idClient.ToString() + "' ");
            //,c.isRemider

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;
            return conn.executeSelectQuery(query, sqlParameters);

        }
        public DataTable GetToDoEmployee(int idEmployee)
        {
            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                         LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                         LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                         LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                        WHERE c.idEmployee ='" + idEmployee.ToString() + "' ");
            //,c.isRemider

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameters[0].Value = idEmployee;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetToDoEmployeeByTypes(int idEmployee, int priority, int status, int type)
        {
            string par1 = "%";
            if (priority != 0)
                par1 = priority.ToString();

            string par2 = "%";
            if (status != 0)
                par2 = status.ToString();
            string par3 = "%";
            if (type != 0)
                par3 = type.ToString(); ;



            string query = string.Format(@"SELECT c.idToDo,c.idToDoType,descriptionToDoType,c.dtOpenDate,c.dtCloseDate,c.dtEndDate,c.planedTime,c.actualTime,c.idStatusToDo,descriptionStatus,
                    c.idPriorityToDo,descriptionPriority, c.idContact,cr.reasonContact,c.descriptionToDo,c.idClient,c.idContPers,c.idProject,c.idOwner,c.idEmployee
                    ,cpe.firstname+' '+cpe.midname+' '+cpe.lastname as nameContPers,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee
                    ,e2.firstnameEmployee +' '+e2.midnameEmployee+' '+e2.lastnameEmployee as nameOwner, cl.nameClient, p.nameProject
                        FROM ToDo c
                        LEFT OUTER JOIN ToDoType ct ON c.idToDoType = ct.idToDoType
                        LEFT OUTER JOIN ToDoStatus cs ON c.idStatusToDo = cs.idStatusToDo
                        LEFT OUTER JOIN ToDoPriority cp ON c.idPriorityToDo = cp.idPriorityToDo
                        LEFT OUTER JOIN ContactPerson cpe ON cpe.idContPers = c.idContPers
                        LEFT OUTER JOIN Employees e ON e.idEmployee = c.idEmployee
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = c.idOwner
                         LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                         LEFT OUTER JOIN Project p ON p.idProject = c.idProject
                         LEFT OUTER JOIN Contacts cr On cr.idContact = c.idContact
                        WHERE c.idEmployee ='" + idEmployee.ToString() + "' and c.idToDoType like '" + par3 + "' and c.idStatusToDo like '" + par2 + "' and c.idPriorityToDo like '" + par1 + "'");
            //,c.isRemider

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameters[0].Value = idEmployee;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public bool Update(ToDoModel model, int idToDo, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ToDo SET   dtOpenDate=@dtOpenDate,dtCloseDate=@dtCloseDate,dtEndDate=@dtEndDate,planedTime=@planedTime,actualTime=@actualTime,
                                    idToDoType=@idToDoType,idPriorityToDo=@idPriorityToDo,idStatusToDo=@idStatusToDo,
                                    idContact=@idContact,descriptionToDo=@descriptionToDo,idClient=@idClient,idContPers=@idContPers,
                                   idProject=@idProject,idOwner=@idOwner,idEmployee=@idEmployee, idArrangement=@idArrangement  WHERE idToDo =@idToDo");

            //            string query = string.Format(@"UPDATE ToDo SET  idToDoType=@idToDoType ,dtOpenDate=@dtOpenDate,CloseDate=@dtCloseDate,
            //                    dtEndDate=@dtEndDate,planedTime=@planedTime,actualTime=@actualTime,idStatusToDo=@idStatusToDo,
            //                    idPriorityToDo=@idPriorityToDo,idContact=@idContact,descriptionToDo=@descriptionToDo,idClient=@idClient,idContPers=@idContPers,
            //                    idProject=@idProject,idOwner=@idOwner,idEmployee=@idEmployee WHERE idToDo =@idToDo");

            //     WHERE idToDo ='" + idToDo.ToString() + "' ");

            //,isRemider=@isRemider
            SqlParameter[] sqlParameter = new SqlParameter[17];

            sqlParameter[0] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameter[0].Value = model.idToDoType;

            sqlParameter[1] = new SqlParameter("@dtOpenDate", SqlDbType.DateTime);
            sqlParameter[1].Value = (model.dtOpenDate == null || model.dtOpenDate == DateTime.MinValue) ? SqlDateTime.Null : model.dtOpenDate;

            sqlParameter[2] = new SqlParameter("@dtCloseDate", SqlDbType.DateTime);
            sqlParameter[2].Value = (model.dtCloseDate == null || model.dtCloseDate == DateTime.MinValue) ? SqlDateTime.Null : model.dtCloseDate;

            sqlParameter[3] = new SqlParameter("@dtEndDate", SqlDbType.DateTime);
            sqlParameter[3].Value = (model.dtEndDate == null || model.dtEndDate == DateTime.MinValue) ? SqlDateTime.Null : model.dtEndDate;

            sqlParameter[4] = new SqlParameter("@idStatusToDo", SqlDbType.Int);
            sqlParameter[4].Value = model.idStatusToDo;

            sqlParameter[5] = new SqlParameter("@idPriorityToDo", SqlDbType.Int);
            sqlParameter[5].Value = model.idPriorityToDo;


            sqlParameter[6] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameter[6].Value = model.idContact;

            sqlParameter[7] = new SqlParameter("@descriptionToDo", SqlDbType.NVarChar);
            //     sqlParameters[5].Value = model.descriptionToDo;
            sqlParameter[7].Value = (model.descriptionToDo == null) ? SqlString.Null : model.descriptionToDo;

            sqlParameter[8] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[8].Value = model.idClient;

            sqlParameter[9] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[9].Value = model.idContPers;

            sqlParameter[10] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[10].Value = model.idProject;

            sqlParameter[11] = new SqlParameter("@idOwner", SqlDbType.Int);
            sqlParameter[11].Value = model.idOwner;

            sqlParameter[12] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[12].Value = model.idEmployee;

            sqlParameter[13] = new SqlParameter("@planedTime", SqlDbType.Decimal);
            sqlParameter[13].Value = model.planedTime;

            sqlParameter[14] = new SqlParameter("@actualTime", SqlDbType.Decimal);
            sqlParameter[14].Value = model.actualTime;

            sqlParameter[15] = new SqlParameter("@idToDo", SqlDbType.Int);
            sqlParameter[15].Value = model.idToDo;

            sqlParameter[16] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[16].Value = (model.idArrangement == -1) ? SqlInt32.Null : model.idArrangement;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId, tableName, description)
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
            sqlParameter[4].Value = idToDo;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDo";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update to do";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

            //sqlParameters[15] = new SqlParameter("@isRemider", SqlDbType.Bit);
            //sqlParameters[15].Value = model.isRemider;

        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ToDo WHERE idToDo = @idToDo");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idToDo", SqlDbType.Int);
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
            sqlParameter[5].Value = "idToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDo";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete to do";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public int Save(ToDoModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ToDo  (idToDoType,dtOpenDate,dtCloseDate,dtEndDate,idStatusToDo,planedTime,actualTime,
                    idPriorityToDo,idContact,descriptionToDo,idClient,idContPers,idProject,idOwner,idEmployee, idArrangement) VALUES
                       (@idToDoType,@dtOpenDate,@dtCloseDate,@dtEndDate,@idStatusToDo,@planedTime,@actualTime,@idPriorityToDo,
                        @idContact,@descriptionToDo,@idClient,@idContPers,@idProject,@idOwner,@idEmployee,@idArrangement);SELECT SCOPE_IDENTITY();");
            //         WHERE idToDo ='" + idToDo.ToString() + "' ");

            //planedTime,actualTime,,isRemider @planedTime,@actualTime,,@isRemider

            SqlParameter[] sqlParameter = new SqlParameter[16];

            sqlParameter[0] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameter[0].Value = model.idToDoType;

            sqlParameter[1] = new SqlParameter("@dtOpenDate", SqlDbType.DateTime);
            sqlParameter[1].Value = (model.dtOpenDate == null || model.dtOpenDate == DateTime.MinValue) ? SqlDateTime.Null : model.dtOpenDate;

            sqlParameter[2] = new SqlParameter("@dtCloseDate", SqlDbType.DateTime);
            sqlParameter[2].Value = (model.dtCloseDate == null || model.dtCloseDate == DateTime.MinValue) ? SqlDateTime.Null : model.dtCloseDate;

            sqlParameter[3] = new SqlParameter("@dtEndDate", SqlDbType.DateTime);
            sqlParameter[3].Value = (model.dtEndDate == null || model.dtEndDate == DateTime.MinValue) ? SqlDateTime.Null : model.dtEndDate;

            sqlParameter[4] = new SqlParameter("@planedTime", SqlDbType.Decimal);
            sqlParameter[4].Value = model.planedTime;

            sqlParameter[5] = new SqlParameter("@actualTime", SqlDbType.Decimal);
            sqlParameter[5].Value = model.actualTime;

            sqlParameter[6] = new SqlParameter("@idStatusToDo", SqlDbType.Int);
            sqlParameter[6].Value = model.idStatusToDo;

            sqlParameter[7] = new SqlParameter("@idPriorityToDo", SqlDbType.Int);
            sqlParameter[7].Value = model.idPriorityToDo;


            sqlParameter[8] = new SqlParameter("@idContact", SqlDbType.Int);
            sqlParameter[8].Value = model.idContact;

            sqlParameter[9] = new SqlParameter("@descriptionToDo", SqlDbType.NVarChar);
            //       sqlParameters[7].Value = model.descriptionToDo;
            sqlParameter[9].Value = (model.descriptionToDo == null) ? SqlString.Null : model.descriptionToDo;

            sqlParameter[10] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[10].Value = model.idClient;

            sqlParameter[11] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[11].Value = model.idContPers;

            sqlParameter[12] = new SqlParameter("@idProject", SqlDbType.Int);
            sqlParameter[12].Value = model.idProject;

            sqlParameter[13] = new SqlParameter("@idOwner", SqlDbType.Int);
            sqlParameter[13].Value = model.idOwner;

            sqlParameter[14] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[14].Value = model.idEmployee;

            sqlParameter[15] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[15].Value = (model.idArrangement == -1) ? SqlInt32.Null : model.idArrangement;


            //sqlParameters[15] = new SqlParameter("@isRemider", SqlDbType.Bit);
            //sqlParameters[15].Value = model.isRemider;

            //sqlParameters[13] = new SqlParameter("@idToDo", SqlDbType.Int);
            //sqlParameters[13].Value = model.idToDo;

          //  return conn.executeInsertQuery(query, sqlParameters);

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId, tableName, description)
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
            sqlParameter[4].Value = conn.GetLastTableID("ToDo") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idToDo";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ToDo";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save to do";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

    }
}

