using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;

namespace BIS.DAO
{
    public class MeetingDAO
    {
        private dbConnection conn;

        public MeetingDAO()
        {
            conn = new dbConnection();
        }

        // SAKI ubacio za Grid na CP 27.06.2015
        //

        public DataTable GetMeeting(Int32 iMeetingID, string idLang)
        {
            string query = string.Format(@"
             select idMeeting, categoryMeetingId,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryDescription,descriptionMeeting, openDateMeeting, startTimeMeeting, durationMeeting, isAllDayMeeting,  clientId,c.nameClient, contactPersonId, cp.firstname+' '+cp.midname +' '+cp.lastname AS namePerson, projectId, p.nameProject, priorityMeeting,CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS namePriority, statusMeeting, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS nameStatus, emploeeOwner,e2.firstnameEmployee+' '+e2.midnameEmployee +' '+e2.lastnameEmployee AS nameOwner, employeeResponsible,e.firstnameEmployee+' '+e.midnameEmployee +' '+e.lastnameEmployee AS nameEmployee, noteMeeting, isRemind, timeRemind, dteRemind, endDateMeeting
            from Meetings m
            LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = m.categoryMeetingId
            LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = mc.categoryDescription
            LEFT OUTER JOIN ContactPerson cp on cp.idContPers = m.contactPersonId
            LEFT OUTER JOIN Client c on c.idClient = m.clientId
            LEFT OUTER JOIN Project p on p.idProject = m.projectId
            LEFT OUTER JOIN Employees e on e.idEmployee = m.employeeResponsible
            LEFT OUTER JOIN Employees e2 on e2.idEmployee = m.emploeeOwner
            LEFT OUTER JOIN MeetingsPriority mp on  m.priorityMeeting = mp.idPriority
             LEFT OUTER JOIN STRING" + idLang + @" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on m.statusMeeting = ms.idMeetingStatus
            LEFT OUTER JOIN STRING" + idLang + @" ss on ss.stringKey = ms.desriptionMeetingStatus 
            where idMeeting = @iMeetingID");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@iMeetingID", SqlDbType.Int);
            sqlParameters[0].Value = iMeetingID;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetMeetings(Int32 iUsrID, string idLang)
        {
            string query = string.Format(@"
             select idMeeting, categoryMeetingId,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE mc.categoryDescription END AS categoryDescription,descriptionMeeting, openDateMeeting, startTimeMeeting, durationMeeting, isAllDayMeeting,  clientId,c.nameClient, contactPersonId, cp.firstname+' '+cp.midname +' '+cp.lastname AS namePerson, projectId, p.nameProject, priorityMeeting,CASE WHEN sp.stringValue IS NOT NULL THEN sp.stringValue ELSE mp.descriptionPriority END AS namePriority, statusMeeting, CASE WHEN ss.stringValue IS NOT NULL THEN ss.stringValue ELSE ms.desriptionMeetingStatus END AS nameStatus, emploeeOwner,e2.firstnameEmployee+' '+e2.midnameEmployee +' '+e2.lastnameEmployee AS nameOwner, employeeResponsible,e.firstnameEmployee+' '+e.midnameEmployee +' '+e.lastnameEmployee AS nameEmployee, noteMeeting, isRemind, timeRemind, dteRemind, endDateMeeting
            from Meetings m
            LEFT OUTER JOIN MeetingsCategory mc on mc.idMeetingCategory = m.categoryMeetingId
            LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = mc.categoryDescription
            LEFT OUTER JOIN ContactPerson cp on cp.idContPers = m.contactPersonId
            LEFT OUTER JOIN Client c on c.idClient = m.clientId
            LEFT OUTER JOIN Project p on p.idProject = m.projectId
            LEFT OUTER JOIN Employees e on e.idEmployee = m.employeeResponsible
            LEFT OUTER JOIN Employees e2 on e2.idEmployee = m.emploeeOwner
            LEFT OUTER JOIN MeetingsPriority mp on  m.priorityMeeting = mp.idPriority
             LEFT OUTER JOIN STRING" + idLang + @" sp on sp.stringKey = mp.descriptionPriority
            LEFT OUTER JOIN MeetingsStatus ms on m.statusMeeting = ms.idMeetingStatus
            LEFT OUTER JOIN STRING" + idLang + @" ss on ss.stringKey = ms.desriptionMeetingStatus
            where emploeeOwner = @iUsrID");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@iUsrID", SqlDbType.Int);
            sqlParameters[0].Value = iUsrID;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetMeetingCategories(string idLang)
        {
            string query = string.Format(@"
             select c.idMeetingCategory, CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE c.categoryDescription END AS categoryDescription
            from MeetingsCategory c
            LEFT OUTER JOIN STRING" + idLang + @" s on s.stringKey = c.categoryDescription
            ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMeetingPriorities(string idLang)
        {
            string query = string.Format(@"
             select p.idPriority,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE p.descriptionPriority END AS descriptionPriority from MeetingsPriority p
            LEFT OUTER JOIN STRING"+idLang+@" s on s.stringKey = p.descriptionPriority
            ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMeetingStatuses()
        {
            string query = string.Format(@"
            select s.idMeetingStatus, s.desriptionMeetingStatus from MeetingsStatus s
            ");

            return conn.executeSelectQuery(query, null);
        }

    }
}
