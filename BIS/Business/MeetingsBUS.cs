using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;

namespace BIS.Business
{
    public class MeetingsBUS
    {
        private MeetingDAO meetingsDAO;

        public MeetingsBUS()
        {
            meetingsDAO = new MeetingDAO();
        }

        // SAKI ubacio za TAB Meetings na CP
        // Do ovde

        public List<MeetingsModel> GetMeetings(Int32 iUserID, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = meetingsDAO.GetMeetings(iUserID,idLang);
            List<MeetingsModel> clients = new List<MeetingsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MeetingsModel model = new MeetingsModel();
                        model.idMeeting = Int32.Parse(dr["idMeeting"].ToString());
                        model.categoryMeetingId = Int32.Parse(dr["categoryMeetingId"].ToString());
                        model.descriptionMeeting = dr["descriptionMeeting"].ToString();

                        if (dr["openDateMeeting"].ToString() != "")
                            model.openDateMeeting = DateTime.Parse(dr["openDateMeeting"].ToString());

                        if (dr["startTimeMeeting"].ToString() != "")
                            model.startTimeMeeting = GetTimeSpan(dr["startTimeMeeting"].ToString());

                        model.isAllDayMeeting = Boolean.Parse(dr["isAllDayMeeting"].ToString());

                        if (dr["clientId"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["clientId"].ToString());

                        if (dr["durationMeeting"].ToString() != "")
                            model.durationMeeting = TimeSpan.Parse(dr["durationMeeting"].ToString());

                        if (dr["contactPersonId"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["contactPersonId"].ToString());

                        if (dr["projectId"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["projectId"].ToString());

                        if (dr["priorityMeeting"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["priorityMeeting"].ToString());

                        if (dr["statusMeeting"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["statusMeeting"].ToString());

                        if (dr["emploeeOwner"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["emploeeOwner"].ToString());

                        if (dr["employeeResponsible"].ToString() != "")
                            model.categoryMeetingId = Int32.Parse(dr["employeeResponsible"].ToString());

                        model.noteMeeting = dr["noteMeeting"].ToString();

                        model.isRemind = Boolean.Parse(dr["isRemind"].ToString());

                        if (dr["timeRemind"].ToString() != "")
                            model.timeRemind = DateTime.Parse(dr["timeRemind"].ToString());

                        if (dr["dteRemind"].ToString() != "")
                            model.dteRemind = DateTime.Parse(dr["dteRemind"].ToString());

                        if (dr["endDateMeeting"].ToString() != "")
                            model.endDateMeeting = DateTime.Parse(dr["endDateMeeting"].ToString());

                        if (dr["descriptionMeeting"].ToString() != "")
                            model.descriptionMeeting = dr["descriptionMeeting"].ToString();

                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();

                        if (dr["namePriority"].ToString() != "")
                            model.namePriority = dr["namePriority"].ToString();

                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();


                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();
                        clients.Add(model);

                    }
                    return clients;
                }
                else
                    return clients;
            }
            else
                return clients;
        }

        public MeetingDescModel GetMeetingDesc(Int32 iMeetingID, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = meetingsDAO.GetMeeting(iMeetingID,idLang);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    MeetingDescModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MeetingDescModel();
                        model.idMeeting = Int32.Parse(dr["idMeeting"].ToString());
                        model.noteMeeting = dr["noteMeeting"].ToString();
                        model.CategoryMeeting = dr["categoryMeetingId"].ToString();
                        model.PriorityMeeting = dr["priorityMeeting"].ToString();
                        model.StatusMeeting = dr["statusMeeting"].ToString();
                        model.Client = dr["clientId"].ToString();
                        model.ContactPerson = dr["contactPersonId"].ToString();
                        model.Project = dr["projectId"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetMeetingStatuses()
        {
            DataTable dataTable = new DataTable();
            dataTable = meetingsDAO.GetMeetingStatuses();//idMeeting);
            List<IModel> meetings = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    MeetingsStatus model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MeetingsStatus();

                        if (dr["idMeetingStatus"].ToString() != "")
                            model.idMeetingStatus = Int32.Parse(dr["idMeetingStatus"].ToString());

                        model.desriptionMeetingStatus = dr["desriptionMeetingStatus"].ToString();

                        meetings.Add(model);
                    }
                    return meetings;
                }
                else
                    return meetings;
            }
            else
                return meetings;
        }

        public List<IModel> GetMeetingPriorities(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = meetingsDAO.GetMeetingPriorities(idLang);
            List<IModel> meetings = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    MeetingsPriorityModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MeetingsPriorityModel();

                        if (dr["idPriority"].ToString() != "")
                            model.idPriority = Int32.Parse(dr["idPriority"].ToString());

                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        meetings.Add(model);
                    }
                    return meetings;
                }
                else
                    return meetings;
            }
            else
                return meetings;
        }

        public List<IModel> GetMeetingCategories(string idLang)//int idMeeting)
        {
            DataTable dataTable = new DataTable();
            dataTable = meetingsDAO.GetMeetingCategories(idLang);//(idMeeting);
            List<IModel> meetings = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    MeetingsCategoryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MeetingsCategoryModel();

                        if (dr["idMeetingCategory"].ToString() != "")
                            model.idMeetingCategory = Int32.Parse(dr["idMeetingCategory"].ToString());

                        model.categoryDescription = dr["categoryDescription"].ToString();

                        meetings.Add(model);
                    }
                    return meetings;
                }
                else
                    return meetings;
            }
            else
                return meetings;
        }

        public TimeSpan GetTimeSpan(string sTime)
        {
            string[] nTime = sTime.Split(':');
            return new TimeSpan(Int32.Parse(nTime[0]), Int32.Parse(nTime[1]), Int32.Parse(nTime[2]));
        }

    }

}
