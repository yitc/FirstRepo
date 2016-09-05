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
    public class AppointmentsDAO
    {
        private dbConnection conn;

        public AppointmentsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetALLAppointments(string idLang)
        {
//            string query = string.Format(@"
//                SELECT ID, dtStart, dtEnd, subject, description, location, category,categoryName, priority, priorityName,
//                    status, statusName, client, clientName, contact, contactName, project, projectName, owner, ownerName, responsible, responsibleName,
//                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed 
//                FROM BISAppointment ");

             string query = string.Format(@"SELECT ID, dtStart, dtEnd, subject, description, location, category,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryName, priority, CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS priorityName,
                    status, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS statusName, client,cl.nameClient AS clientName, contact, cp.firstname + ' ' + cp.midname + ' ' +cp.lastname AS contactName, project, p.nameProject AS projectName, owner, e.firstnameEmployee + ' ' + e.midnameEmployee + ' ' +e.lastnameEmployee AS ownerName, responsible,e2.firstnameEmployee + ' ' + e2.midnameEmployee + ' ' +e2.lastnameEmployee AS responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed 
                FROM BISAppointment ba
						LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = ba.category
						LEFT OUTER JOIN STRING"+idLang+@" s on s.stringKey = mc.categoryDescription
						 LEFT OUTER JOIN MeetingsPriority mp on  ba.priority = mp.idPriority
             LEFT OUTER JOIN STRING"+idLang+@" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on ba.status = ms.idMeetingStatus
            LEFT OUTER JOIN STRING"+idLang+@" ss on ss.stringKey = ms.desriptionMeetingStatus
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ba.contact
                        LEFT OUTER JOIN Client cl ON cl.idClient = ba.client
                        LEFT OUTER JOIN Project p ON p.idProject = ba.project
                        LEFT OUTER JOIN Employees e ON e.idEmployee = ba.owner
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = ba.responsible");
         
            return conn.executeSelectQuery(query, null);
        }



        public DataTable GetAppointment(Guid id)
        {
            string query = string.Format(@"
                SELECT ID, dtStart, dtEnd, subject, description, location, category,categoryName, priority, priorityName,
                    status, statusName, client, clientName, contact, contactName, project, projectName, owner, ownerName, responsible, responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed 
                FROM BISAppointment ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            sqlParameters[0].Value = id;
            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAppointmentsByPerson(int contact, string idLang)
        {
            string query = string.Format(@"
                SELECT ID, dtStart, dtEnd, subject, description, location, category,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryName, priority, CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS priorityName,
                    status, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS statusName, client,cl.nameClient AS clientName, contact, cp.firstname + ' ' + cp.midname + ' ' +cp.lastname AS contactName, project, p.nameProject AS projectName, owner, e.firstnameEmployee + ' ' + e.midnameEmployee + ' ' +e.lastnameEmployee AS ownerName, responsible,e2.firstnameEmployee + ' ' + e2.midnameEmployee + ' ' +e2.lastnameEmployee AS responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed , ba.idArrangement, arr.nameArrangement
                FROM BISAppointment ba
						LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = ba.category
						LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = mc.categoryDescription
						 LEFT OUTER JOIN MeetingsPriority mp on  ba.priority = mp.idPriority
             LEFT OUTER JOIN STRING" + idLang + @" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on ba.status = ms.idMeetingStatus
            LEFT OUTER JOIN STRING" + idLang + @" ss on ss.stringKey = ms.desriptionMeetingStatus
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ba.contact
                        LEFT OUTER JOIN Client cl ON cl.idClient = ba.client
                        LEFT OUTER JOIN Project p ON p.idProject = ba.project
                        LEFT OUTER JOIN Employees e ON e.idEmployee = ba.owner
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = ba.responsible 
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= ba.idArrangement
                        WHERE contact = @contact");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@contact", SqlDbType.Int);
            sqlParameters[0].Value = contact;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAppointmentsByArrangement(int idArrangement, string idLang)
        {
            string query = string.Format(@"
                SELECT ID, dtStart, dtEnd, subject, description, location, category,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryName, priority, CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS priorityName,
                    status, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS statusName, client,cl.nameClient AS clientName, contact, cp.firstname + ' ' + cp.midname + ' ' +cp.lastname AS contactName, project, p.nameProject AS projectName, owner, e.firstnameEmployee + ' ' + e.midnameEmployee + ' ' +e.lastnameEmployee AS ownerName, responsible,e2.firstnameEmployee + ' ' + e2.midnameEmployee + ' ' +e2.lastnameEmployee AS responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed , ba.idArrangement, arr.nameArrangement
                FROM BISAppointment ba
						LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = ba.category
						LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = mc.categoryDescription
						 LEFT OUTER JOIN MeetingsPriority mp on  ba.priority = mp.idPriority
             LEFT OUTER JOIN STRING" + idLang + @" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on ba.status = ms.idMeetingStatus
            LEFT OUTER JOIN STRING" + idLang + @" ss on ss.stringKey = ms.desriptionMeetingStatus
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ba.contact
                        LEFT OUTER JOIN Client cl ON cl.idClient = ba.client
                        LEFT OUTER JOIN Project p ON p.idProject = ba.project
                        LEFT OUTER JOIN Employees e ON e.idEmployee = ba.owner
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = ba.responsible 
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= ba.idArrangement
                        WHERE ba.idArrangement = @idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAppointmentsByClient(int client, string idLang)
        {
            string query = string.Format(@"SELECT ID, dtStart, dtEnd, subject, description, location, category,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryName, priority, CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS priorityName,
                    status, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS statusName, client,cl.nameClient AS clientName, contact, cp.firstname + ' ' + cp.midname + ' ' +cp.lastname AS contactName, project, p.nameProject AS projectName, owner, e.firstnameEmployee + ' ' + e.midnameEmployee + ' ' +e.lastnameEmployee AS ownerName, responsible,e2.firstnameEmployee + ' ' + e2.midnameEmployee + ' ' +e2.lastnameEmployee AS responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed , ba.idArrangement, arr.nameArrangement
                FROM BISAppointment ba
						LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = ba.category
						LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = mc.categoryDescription
						 LEFT OUTER JOIN MeetingsPriority mp on  ba.priority = mp.idPriority
             LEFT OUTER JOIN STRING" + idLang + @" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on ba.status = ms.idMeetingStatus
            LEFT OUTER JOIN STRING" + idLang + @" ss on ss.stringKey = ms.desriptionMeetingStatus
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ba.contact
                        LEFT OUTER JOIN Client cl ON cl.idClient = ba.client
                        LEFT OUTER JOIN Project p ON p.idProject = ba.project
                        LEFT OUTER JOIN Employees e ON e.idEmployee = ba.owner
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = ba.responsible
                        LEFT OUTER JOIN Arrangement arr on arr.idArrangement= ba.idArrangement
WHERE client = @client");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@client", SqlDbType.Int);
            sqlParameters[0].Value = client;
            return conn.executeSelectQuery(query, sqlParameters);


        }

        public DataTable GetAppointmentsByOwner(int owner, string idLang)
        {
            // Employees by employee that created appointment
            //
            string query = string.Format(@"SELECT ID, dtStart, dtEnd, subject, description, location, category,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryName, priority, CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS priorityName,
                    status, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS statusName, client,cl.nameClient AS clientName, contact, cp.firstname + ' ' + cp.midname + ' ' +cp.lastname AS contactName, project, p.nameProject AS projectName, owner, e.firstnameEmployee + ' ' + e.midnameEmployee + ' ' +e.lastnameEmployee AS ownerName, responsible,e2.firstnameEmployee + ' ' + e2.midnameEmployee + ' ' +e2.lastnameEmployee AS responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, ReminderDismissed, ReminderSnoozed 
                FROM BISAppointment ba
						LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = ba.category
						LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = mc.categoryDescription
						 LEFT OUTER JOIN MeetingsPriority mp on  ba.priority = mp.idPriority
             LEFT OUTER JOIN STRING" + idLang + @" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on ba.status = ms.idMeetingStatus
            LEFT OUTER JOIN STRING" + idLang + @" ss on ss.stringKey = ms.desriptionMeetingStatus
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ba.contact
                        LEFT OUTER JOIN Client cl ON cl.idClient = ba.client
                        LEFT OUTER JOIN Project p ON p.idProject = ba.project
                        LEFT OUTER JOIN Employees e ON e.idEmployee = ba.owner
                        LEFT OUTER JOIN Employees e2 ON e2.idEmployee = ba.responsible WHERE owner = @owner");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@owner", SqlDbType.Int);
            sqlParameters[0].Value = owner;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public object ChechIfAppointmentExist(Guid id)
        {
            string query = string.Format(@"
                SELECT ID 
                FROM BISAppointment
                WHERE ID = @ID");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            sqlParameters[0].Value = id;
            return conn.executeScalarQuery(query, sqlParameters);
        }


        public bool Save(BISAppointment app, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                    BISAppointment(ID, dtStart, dtEnd, subject, description, location, category, categoryName, priority, priorityName, status, statusName,
                    client, clientName, contact, contactName, project, projectName, owner, ownerName, responsible, responsibleName,
                    isAllDay, isRemind, background, showtime, Reminder, idArrangement) 
                    VALUES (@ID, @dtStart, @dtEnd, @subject, @description, @location, @category, @categoryName, @priority, @priorityName, @status, @statusName,
                    @client, @clientName, @contact, @contactName, @project, @projectName, @owner, @ownerName, @responsible, @responsibleName, 
                    @isAllDay, @isRemind, @background, @showtime, @Reminder, @idArrangement)");


            SqlParameter[] sqlParameter = new SqlParameter[28];

            sqlParameter[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            sqlParameter[0].Value = app.Id;

            sqlParameter[1] = new SqlParameter("@dtStart", SqlDbType.DateTime);
            sqlParameter[1].Value = (app.Start == null || app.Start == DateTime.MinValue) ? SqlDateTime.Null : app.Start;

            sqlParameter[2] = new SqlParameter("@dtEnd", SqlDbType.DateTime);
            sqlParameter[2].Value = (app.End == null || app.End == DateTime.MinValue) ? SqlDateTime.Null : app.End;

            sqlParameter[3] = new SqlParameter("@subject", SqlDbType.NVarChar);
            sqlParameter[3].Value = app.Subject;

            sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[4].Value = app.Description;

            sqlParameter[5] = new SqlParameter("@location", SqlDbType.NVarChar);
            sqlParameter[5].Value = app.Location;

            sqlParameter[6] = new SqlParameter("@category", SqlDbType.Int);
            sqlParameter[6].Value = app.Category;

            sqlParameter[7] = new SqlParameter("@categoryName", SqlDbType.NVarChar);
            sqlParameter[7].Value = app.CategoryName;

            sqlParameter[8] = new SqlParameter("@priority", SqlDbType.Int);
            sqlParameter[8].Value = app.Priority;

            sqlParameter[9] = new SqlParameter("@priorityName", SqlDbType.NVarChar);
            sqlParameter[9].Value = app.PriorityName;

            sqlParameter[10] = new SqlParameter("@status", SqlDbType.Int);
            sqlParameter[10].Value = app.Status;

            sqlParameter[11] = new SqlParameter("@statusName", SqlDbType.NVarChar);
            sqlParameter[11].Value = app.StatusName;

            sqlParameter[12] = new SqlParameter("@client", SqlDbType.Int);
            sqlParameter[12].Value = app.Client;

            sqlParameter[13] = new SqlParameter("@clientName", SqlDbType.NVarChar);
            sqlParameter[13].Value = app.ClientName;

            sqlParameter[14] = new SqlParameter("@contact", SqlDbType.Int);
            sqlParameter[14].Value = app.Contact;

            sqlParameter[15] = new SqlParameter("@contactName", SqlDbType.NVarChar);
            sqlParameter[15].Value = app.ContactName;

            sqlParameter[16] = new SqlParameter("@project", SqlDbType.Int);
            sqlParameter[16].Value = app.Project;

            sqlParameter[17] = new SqlParameter("@projectName", SqlDbType.NVarChar);
            sqlParameter[17].Value = app.ProjectName;

            sqlParameter[18] = new SqlParameter("@owner", SqlDbType.Int);
            sqlParameter[18].Value = app.Owner;

            sqlParameter[19] = new SqlParameter("@ownerName", SqlDbType.NVarChar);
            sqlParameter[19].Value = app.OwnerName;

            sqlParameter[20] = new SqlParameter("@responsible", SqlDbType.Int);
            sqlParameter[20].Value = app.Responsible;

            sqlParameter[21] = new SqlParameter("@responsibleName", SqlDbType.NVarChar);
            sqlParameter[21].Value = app.ResponsibleName;

            sqlParameter[22] = new SqlParameter("@isAllDay", SqlDbType.Bit);
            sqlParameter[22].Value = app.IsAllDay;

            sqlParameter[23] = new SqlParameter("@isRemind", SqlDbType.Bit);
            sqlParameter[23].Value = app.IsReminder;

            sqlParameter[24] = new SqlParameter("@background", SqlDbType.NVarChar);
            sqlParameter[24].Value = app.Background;

            sqlParameter[25] = new SqlParameter("@showtime", SqlDbType.NVarChar);
            sqlParameter[25].Value = app.ShowTime;

            sqlParameter[26] = new SqlParameter("@Reminder", SqlDbType.Time);
            sqlParameter[26].Value = (app.Reminder == null) ? (object)SqlDateTime.Null : app.Reminder;


            sqlParameter[27] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[27].Value = app.IdArrangement;

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
            sqlParameter[4].Value = app.Id.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "ID";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BISAppointment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(BISAppointment app, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE BISAppointment SET
                    ID = @ID, dtStart = @dtStart, dtEnd = @dtEnd, subject = @subject, description = @description, location = @location, 
                        category = @category, categoryName = @categoryName,  priority = @priority, priorityName = @priorityName, status = @status, statusName = @statusName,
                        client = @client, clientName = @clientName, contact = @contact, contactName = @contactName, project = @project, projectName = @projectName,
                        owner = @owner, ownerName = @ownerName, responsible = @responsible, responsibleName = @responsibleName ,isAllDay = @isAllDay, 
                        isRemind = @isRemind, background = @background, showtime = @showtime, Reminder = @Reminder , idArrangement=@idArrangement
                    WHERE ID = @ID");

            SqlParameter[] sqlParameter = new SqlParameter[28];

            sqlParameter[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            sqlParameter[0].Value = app.Id;

            sqlParameter[1] = new SqlParameter("@dtStart", SqlDbType.DateTime);
            sqlParameter[1].Value = (app.Start == null || app.Start == DateTime.MinValue) ? SqlDateTime.Null : app.Start;

            sqlParameter[2] = new SqlParameter("@dtEnd", SqlDbType.DateTime);
            sqlParameter[2].Value = (app.End == null || app.End == DateTime.MinValue) ? SqlDateTime.Null : app.End;

            sqlParameter[3] = new SqlParameter("@subject", SqlDbType.NVarChar);
            sqlParameter[3].Value = app.Subject;

            sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[4].Value = app.Description;

            sqlParameter[5] = new SqlParameter("@location", SqlDbType.NVarChar);
            sqlParameter[5].Value = app.Location;

            sqlParameter[6] = new SqlParameter("@category", SqlDbType.Int);
            sqlParameter[6].Value = app.Category;

            sqlParameter[7] = new SqlParameter("@categoryName", SqlDbType.NVarChar);
            sqlParameter[7].Value = app.CategoryName;

            sqlParameter[8] = new SqlParameter("@priority", SqlDbType.Int);
            sqlParameter[8].Value = app.Priority;

            sqlParameter[9] = new SqlParameter("@priorityName", SqlDbType.NVarChar);
            sqlParameter[9].Value = app.PriorityName;

            sqlParameter[10] = new SqlParameter("@status", SqlDbType.Int);
            sqlParameter[10].Value = app.Status;

            sqlParameter[11] = new SqlParameter("@statusName", SqlDbType.NVarChar);
            sqlParameter[11].Value = app.StatusName;

            sqlParameter[12] = new SqlParameter("@client", SqlDbType.Int);
            sqlParameter[12].Value = app.Client;

            sqlParameter[13] = new SqlParameter("@clientName", SqlDbType.NVarChar);
            sqlParameter[13].Value = app.ClientName;

            sqlParameter[14] = new SqlParameter("@contact", SqlDbType.Int);
            sqlParameter[14].Value = app.Contact;

            sqlParameter[15] = new SqlParameter("@contactName", SqlDbType.NVarChar);
            sqlParameter[15].Value = app.ContactName;

            sqlParameter[16] = new SqlParameter("@project", SqlDbType.Int);
            sqlParameter[16].Value = app.Project;

            sqlParameter[17] = new SqlParameter("@projectName", SqlDbType.NVarChar);
            sqlParameter[17].Value = app.ProjectName;

            sqlParameter[18] = new SqlParameter("@owner", SqlDbType.Int);
            sqlParameter[18].Value = app.Owner;

            sqlParameter[19] = new SqlParameter("@ownerName", SqlDbType.NVarChar);
            sqlParameter[19].Value = app.OwnerName;

            sqlParameter[20] = new SqlParameter("@responsible", SqlDbType.Int);
            sqlParameter[20].Value = app.Responsible;

            sqlParameter[21] = new SqlParameter("@responsibleName", SqlDbType.NVarChar);
            sqlParameter[21].Value = app.ResponsibleName;

            sqlParameter[22] = new SqlParameter("@isAllDay", SqlDbType.Bit);
            sqlParameter[22].Value = app.IsAllDay;

            sqlParameter[23] = new SqlParameter("@isRemind", SqlDbType.Bit);
            sqlParameter[23].Value = app.IsReminder;

            sqlParameter[24] = new SqlParameter("@background", SqlDbType.NVarChar);
            sqlParameter[24].Value = app.Background;

            sqlParameter[25] = new SqlParameter("@showtime", SqlDbType.NVarChar);
            sqlParameter[25].Value = app.ShowTime;

            sqlParameter[26] = new SqlParameter("@Reminder", SqlDbType.Time);
            sqlParameter[26].Value = (app.Reminder == null) ? (object)SqlDateTime.Null : app.Reminder;


            sqlParameter[27] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[27].Value = app.IdArrangement;


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
            sqlParameter[4].Value = app.Id.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "ID";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BISAppointment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool UpdateTime(Guid g, DateTime start, DateTime end, bool isAllDay, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE BISAppointment SET
                    ID = @ID, dtStart = @dtStart, dtEnd = @dtEnd, isAllDay = @isAllDay 
                    WHERE ID = @ID");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            sqlParameter[0].Value = g;

            sqlParameter[1] = new SqlParameter("@dtStart", SqlDbType.DateTime);
            sqlParameter[1].Value = (start == null || start == DateTime.MinValue) ? SqlDateTime.Null : start;

            sqlParameter[2] = new SqlParameter("@dtEnd", SqlDbType.DateTime);
            sqlParameter[2].Value = (end == null || end == DateTime.MinValue) ? SqlDateTime.Null : end;

            sqlParameter[3] = new SqlParameter("@isAllDay", SqlDbType.Bit);
            sqlParameter[3].Value = isAllDay;


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
            sqlParameter[4].Value = g.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "ID";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BISAppointment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update time";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Delete(Guid id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  BISAppointment WHERE ID = @ID");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
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
            sqlParameter[4].Value = id.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "ID";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BISAppointment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public DataTable GetAppointmentsById(string id)
        {
            // Employees by employee that created appointment
            //
            string query = string.Format(@"SELECT ba.idArrangement, arr.nameArrangement 
                FROM BISAppointment ba						
               LEFT OUTER JOIN Arrangement arr on arr.idArrangement= ba.idArrangement
               WHERE ID = @id");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameters[0].Value = id;
            return conn.executeSelectQuery(query, sqlParameters);
        }
    }
}
