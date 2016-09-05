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
    public class AppointmentsBUS
    {
        private AppointmentsDAO appDAO;


        public AppointmentsBUS()
        {
            appDAO = new AppointmentsDAO();
        }
        public List<BISAppointment> GetALLAppointment_BISModel(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = appDAO.GetALLAppointments(idLang);
            List<BISAppointment> appointments = new List<BISAppointment>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BISAppointment model = new BISAppointment();

                        model.Id = Guid.Parse(dr["ID"].ToString());

                        if (dr["dtStart"].ToString() != "")
                            model.Start = DateTime.Parse(dr["dtStart"].ToString());

                        if (dr["dtEnd"].ToString() != "")
                            model.End = DateTime.Parse(dr["dtEnd"].ToString());

                        model.Subject = dr["subject"].ToString();
                        model.Description = dr["description"].ToString();
                        model.Location = dr["location"].ToString();

                        if (dr["category"].ToString() != "")
                            model.Category = Int32.Parse(dr["category"].ToString());

                        model.CategoryName = dr["categoryName"].ToString();

                        if (dr["priority"].ToString() != "")
                            model.Priority = Int32.Parse(dr["priority"].ToString());

                        model.PriorityName = dr["priorityName"].ToString();

                        if (dr["status"].ToString() != "")
                            model.Status = Int32.Parse(dr["status"].ToString());

                        model.StatusName = dr["statusName"].ToString();

                        if (dr["client"].ToString() != "")
                            model.Client = Int32.Parse(dr["client"].ToString());

                        model.ClientName = dr["clientName"].ToString();

                        if (dr["contact"].ToString() != "")
                            model.Contact = Int32.Parse(dr["contact"].ToString());

                        model.ContactName = dr["contactName"].ToString();

                        if (dr["project"].ToString() != "")
                            model.Project = Int32.Parse(dr["project"].ToString());

                        model.ProjectName = dr["projectName"].ToString();

                        if (dr["owner"].ToString() != "")
                            model.Owner = Int32.Parse(dr["owner"].ToString());

                        model.OwnerName = dr["ownerName"].ToString();

                        if (dr["responsible"].ToString() != "")
                            model.Responsible = Int32.Parse(dr["responsible"].ToString());

                        model.ResponsibleName = dr["responsibleName"].ToString();

                        if (dr["isAllDay"].ToString() != "")
                            model.IsAllDay = Boolean.Parse(dr["isAllDay"].ToString());

                        if (dr["isRemind"].ToString() != "")
                            model.IsReminder = Boolean.Parse(dr["isRemind"].ToString());

                        model.Background = dr["background"].ToString();
                        model.ShowTime = dr["showtime"].ToString();

                        if (dr["Reminder"].ToString() != "")
                            model.Reminder = TimeSpan.Parse(dr["Reminder"].ToString());

                        if (dr["ReminderSnoozed"].ToString() != "")
                            model.ReminderSnoozed = TimeSpan.Parse(dr["ReminderSnoozed"].ToString());

                        if (dr["ReminderDismissed"].ToString() != "")
                            model.ReminderDismissed = Boolean.Parse(dr["ReminderDismissed"].ToString());

                       appointments.Add(model);
                    }
                    return appointments;
                }
                else
                    return appointments;
            }
            else
                return appointments;
        }
        public List<BISAppointment> GetAppointmentsByPerson(int contact, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = appDAO.GetAppointmentsByPerson(contact, idLang);
            List<BISAppointment> appointments = new List<BISAppointment>();
            //appointments.Capacity = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BISAppointment model = new BISAppointment();
                        model.Id = Guid.Parse(dr["ID"].ToString());

                        if (dr["dtStart"].ToString() != "")
                            model.Start = DateTime.Parse(dr["dtStart"].ToString());

                        if (dr["dtEnd"].ToString() != "")
                            model.End = DateTime.Parse(dr["dtEnd"].ToString());

                        model.Subject = dr["subject"].ToString();
                        model.Description = dr["description"].ToString();
                        model.Location = dr["location"].ToString();

                        if (dr["category"].ToString() != "")
                            model.Category = Int32.Parse(dr["category"].ToString());

                        model.CategoryName = dr["categoryName"].ToString();

                        if (dr["priority"].ToString() != "")
                            model.Priority = Int32.Parse(dr["priority"].ToString());

                        model.PriorityName = dr["priorityName"].ToString();

                        if (dr["status"].ToString() != "")
                            model.Status = Int32.Parse(dr["status"].ToString());

                        model.StatusName = dr["statusName"].ToString();

                        if (dr["client"].ToString() != "")
                            model.Client = Int32.Parse(dr["client"].ToString());

                        model.ClientName = dr["clientName"].ToString();

                        if (dr["contact"].ToString() != "")
                            model.Contact = Int32.Parse(dr["contact"].ToString());

                        model.ContactName = dr["contactName"].ToString();

                        if (dr["project"].ToString() != "")
                            model.Project = Int32.Parse(dr["project"].ToString());

                        model.ProjectName = dr["projectName"].ToString();

                        if (dr["owner"].ToString() != "")
                            model.Owner = Int32.Parse(dr["owner"].ToString());

                        model.OwnerName = dr["ownerName"].ToString();

                        if (dr["responsible"].ToString() != "")
                            model.Responsible = Int32.Parse(dr["responsible"].ToString());

                        model.ResponsibleName = dr["responsibleName"].ToString();

                        if (dr["isAllDay"].ToString() != "")
                            model.IsAllDay = Boolean.Parse(dr["isAllDay"].ToString());

                        if (dr["isRemind"].ToString() != "")
                            model.IsReminder = Boolean.Parse(dr["isRemind"].ToString());

                        model.Background = dr["background"].ToString();
                        model.ShowTime = dr["showtime"].ToString();

                        if (dr["Reminder"].ToString() != "")
                            model.Reminder = TimeSpan.Parse(dr["Reminder"].ToString());

                        if (dr["ReminderSnoozed"].ToString() != "")
                            model.ReminderSnoozed = TimeSpan.Parse(dr["ReminderSnoozed"].ToString());

                        if (dr["ReminderDismissed"].ToString() != "")
                            model.ReminderDismissed = Boolean.Parse(dr["ReminderDismissed"].ToString());

                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.IdArrangement = Convert.ToInt32(dr["idArrangement"]);

                        if (dr["nameArrangement"].ToString() != "")
                            model.NameArrangement = (dr["nameArrangement"].ToString());
                        // end jelena

                        appointments.Add(model);
                    }
                    //appointments.Capacity = dataTable.Rows.Count;
                    return appointments;
                }
                else
                    return appointments;
            }
            else
                return appointments;
        }

        public List<BISAppointment> GetAppointmentsByArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = appDAO.GetAppointmentsByArrangement(idArrangement, idLang);
            List<BISAppointment> appointments = new List<BISAppointment>();
            //appointments.Capacity = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BISAppointment model = new BISAppointment();
                        model.Id = Guid.Parse(dr["ID"].ToString());

                        if (dr["dtStart"].ToString() != "")
                            model.Start = DateTime.Parse(dr["dtStart"].ToString());

                        if (dr["dtEnd"].ToString() != "")
                            model.End = DateTime.Parse(dr["dtEnd"].ToString());

                        model.Subject = dr["subject"].ToString();
                        model.Description = dr["description"].ToString();
                        model.Location = dr["location"].ToString();

                        if (dr["category"].ToString() != "")
                            model.Category = Int32.Parse(dr["category"].ToString());

                        model.CategoryName = dr["categoryName"].ToString();

                        if (dr["priority"].ToString() != "")
                            model.Priority = Int32.Parse(dr["priority"].ToString());

                        model.PriorityName = dr["priorityName"].ToString();

                        if (dr["status"].ToString() != "")
                            model.Status = Int32.Parse(dr["status"].ToString());

                        model.StatusName = dr["statusName"].ToString();

                        if (dr["client"].ToString() != "")
                            model.Client = Int32.Parse(dr["client"].ToString());

                        model.ClientName = dr["clientName"].ToString();

                        if (dr["contact"].ToString() != "")
                            model.Contact = Int32.Parse(dr["contact"].ToString());

                        model.ContactName = dr["contactName"].ToString();

                        if (dr["project"].ToString() != "")
                            model.Project = Int32.Parse(dr["project"].ToString());

                        model.ProjectName = dr["projectName"].ToString();

                        if (dr["owner"].ToString() != "")
                            model.Owner = Int32.Parse(dr["owner"].ToString());

                        model.OwnerName = dr["ownerName"].ToString();

                        if (dr["responsible"].ToString() != "")
                            model.Responsible = Int32.Parse(dr["responsible"].ToString());

                        model.ResponsibleName = dr["responsibleName"].ToString();

                        if (dr["isAllDay"].ToString() != "")
                            model.IsAllDay = Boolean.Parse(dr["isAllDay"].ToString());

                        if (dr["isRemind"].ToString() != "")
                            model.IsReminder = Boolean.Parse(dr["isRemind"].ToString());

                        model.Background = dr["background"].ToString();
                        model.ShowTime = dr["showtime"].ToString();

                        if (dr["Reminder"].ToString() != "")
                            model.Reminder = TimeSpan.Parse(dr["Reminder"].ToString());

                        if (dr["ReminderSnoozed"].ToString() != "")
                            model.ReminderSnoozed = TimeSpan.Parse(dr["ReminderSnoozed"].ToString());

                        if (dr["ReminderDismissed"].ToString() != "")
                            model.ReminderDismissed = Boolean.Parse(dr["ReminderDismissed"].ToString());

                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.IdArrangement = Convert.ToInt32(dr["idArrangement"]);

                        if (dr["nameArrangement"].ToString() != "")
                            model.NameArrangement = (dr["nameArrangement"].ToString());
                        // end jelena

                        appointments.Add(model);
                    }
                    //appointments.Capacity = dataTable.Rows.Count;
                    return appointments;
                }
                else
                    return appointments;
            }
            else
                return appointments;
        }

        public List<BISAppointment> GetAppointmentsByOwner(int owner, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = appDAO.GetAppointmentsByOwner(owner, idLang);
            List<BISAppointment> appointments = new List<BISAppointment>();
            //appointments.Capacity = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BISAppointment model = new BISAppointment();
                        model.Id = Guid.Parse(dr["ID"].ToString());

                        if (dr["dtStart"].ToString() != "")
                            model.Start = DateTime.Parse(dr["dtStart"].ToString());

                        if (dr["dtEnd"].ToString() != "")
                            model.End = DateTime.Parse(dr["dtEnd"].ToString());

                        model.Subject = dr["subject"].ToString();
                        model.Description = dr["description"].ToString();
                        model.Location = dr["location"].ToString();

                        if (dr["category"].ToString() != "")
                            model.Category = Int32.Parse(dr["category"].ToString());

                        model.CategoryName = dr["categoryName"].ToString();

                        if (dr["priority"].ToString() != "")
                            model.Priority = Int32.Parse(dr["priority"].ToString());

                        model.PriorityName = dr["priorityName"].ToString();

                        if (dr["status"].ToString() != "")
                            model.Status = Int32.Parse(dr["status"].ToString());

                        model.StatusName = dr["statusName"].ToString();

                        if (dr["client"].ToString() != "")
                            model.Client = Int32.Parse(dr["client"].ToString());

                        model.ClientName = dr["clientName"].ToString();

                        if (dr["contact"].ToString() != "")
                            model.Contact = Int32.Parse(dr["contact"].ToString());

                        model.ContactName = dr["contactName"].ToString();

                        if (dr["project"].ToString() != "")
                            model.Project = Int32.Parse(dr["project"].ToString());

                        model.ProjectName = dr["projectName"].ToString();

                        if (dr["owner"].ToString() != "")
                            model.Owner = Int32.Parse(dr["owner"].ToString());

                        model.OwnerName = dr["ownerName"].ToString();

                        if (dr["responsible"].ToString() != "")
                            model.Responsible = Int32.Parse(dr["responsible"].ToString());

                        model.ResponsibleName = dr["responsibleName"].ToString();

                        if (dr["isAllDay"].ToString() != "")
                            model.IsAllDay = Boolean.Parse(dr["isAllDay"].ToString());

                        if (dr["isRemind"].ToString() != "")
                            model.IsReminder = Boolean.Parse(dr["isRemind"].ToString());

                        model.Background = dr["background"].ToString();
                        model.ShowTime = dr["showtime"].ToString();

                        if (dr["Reminder"].ToString() != "")
                            model.Reminder = TimeSpan.Parse(dr["Reminder"].ToString());

                        if (dr["ReminderSnoozed"].ToString() != "")
                            model.ReminderSnoozed = TimeSpan.Parse(dr["ReminderSnoozed"].ToString());

                        if (dr["ReminderDismissed"].ToString() != "")
                            model.ReminderDismissed = Boolean.Parse(dr["ReminderDismissed"].ToString());

                        appointments.Add(model);
                    }
                    //appointments.Capacity = dataTable.Rows.Count;
                    return appointments;
                }
                else
                    return appointments;
            }
            else
                return appointments;
        }

        public List<BISAppointment> GetAppointmentsByClient(int client, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = appDAO.GetAppointmentsByClient(client, idLang);
            List<BISAppointment> appointments = new List<BISAppointment>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BISAppointment model = new BISAppointment();

                        model.Id = Guid.Parse(dr["ID"].ToString());

                        if (dr["dtStart"].ToString() != "")
                            model.Start = DateTime.Parse(dr["dtStart"].ToString());

                        if (dr["dtEnd"].ToString() != "")
                            model.End = DateTime.Parse(dr["dtEnd"].ToString());

                        model.Subject = dr["subject"].ToString();
                        model.Description = dr["description"].ToString();
                        model.Location = dr["location"].ToString();

                        if (dr["category"].ToString() != "")
                            model.Category = Int32.Parse(dr["category"].ToString());

                        model.CategoryName = dr["categoryName"].ToString();

                        if (dr["priority"].ToString() != "")
                            model.Priority = Int32.Parse(dr["priority"].ToString());

                        model.PriorityName = dr["priorityName"].ToString();

                        if (dr["status"].ToString() != "")
                            model.Status = Int32.Parse(dr["status"].ToString());

                        model.StatusName = dr["statusName"].ToString();

                        if (dr["client"].ToString() != "")
                            model.Client = Int32.Parse(dr["client"].ToString());

                        model.ClientName = dr["clientName"].ToString();

                        if (dr["contact"].ToString() != "")
                            model.Contact = Int32.Parse(dr["contact"].ToString());

                        model.ContactName = dr["contactName"].ToString();

                        if (dr["project"].ToString() != "")
                            model.Project = Int32.Parse(dr["project"].ToString());

                        model.ProjectName = dr["projectName"].ToString();

                        if (dr["owner"].ToString() != "")
                            model.Owner = Int32.Parse(dr["owner"].ToString());

                        model.OwnerName = dr["ownerName"].ToString();

                        if (dr["responsible"].ToString() != "")
                            model.Responsible = Int32.Parse(dr["responsible"].ToString());

                        model.ResponsibleName = dr["responsibleName"].ToString();

                        if (dr["isAllDay"].ToString() != "")
                            model.IsAllDay = Boolean.Parse(dr["isAllDay"].ToString());

                        if (dr["isRemind"].ToString() != "")
                            model.IsReminder = Boolean.Parse(dr["isRemind"].ToString());

                        model.Background = dr["background"].ToString();
                        model.ShowTime = dr["showtime"].ToString();

                        if (dr["Reminder"].ToString() != "")
                            model.Reminder = TimeSpan.Parse(dr["Reminder"].ToString());

                        if (dr["ReminderSnoozed"].ToString() != "")
                            model.ReminderSnoozed = TimeSpan.Parse(dr["ReminderSnoozed"].ToString());

                        if (dr["ReminderDismissed"].ToString() != "")
                            model.ReminderDismissed = Boolean.Parse(dr["ReminderDismissed"].ToString());

                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.IdArrangement = Convert.ToInt32(dr["idArrangement"]);

                        if (dr["nameArrangement"].ToString() != "")
                            model.NameArrangement = (dr["nameArrangement"].ToString());
                        // end jelena
                        appointments.Add(model);
                    }
                    return appointments;
                }
                else
                    return appointments;
            }
            else
                return appointments;
        }
        public bool Save(BISAppointment app, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = appDAO.Save(app, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(BISAppointment app, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = appDAO.Update(app, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool UpdateTime(Guid g, DateTime start, DateTime end, bool isAllDay,string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = appDAO.UpdateTime(g, start, end, isAllDay, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(Guid g, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = appDAO.Delete(g,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool ChechIfAppointmentExist(Guid guid)
        {
            bool retval = false;
            try
            {

                object obj = appDAO.ChechIfAppointmentExist(guid);
                if (obj != null)
                    retval = true;
                else
                    retval = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<BISAppointment> GetAppointmentsById(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = appDAO.GetAppointmentsById(idLang);
            List<BISAppointment> appointments = new List<BISAppointment>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BISAppointment model = new BISAppointment();


                        if (dr["idArrangement"].ToString() != "")
                            model.IdArrangement = Convert.ToInt32(dr["idArrangement"]);

                        if (dr["nameArrangement"].ToString() != "")
                            model.NameArrangement = (dr["nameArrangement"].ToString());

                        appointments.Add(model);
                    }
                    return appointments;
                }
                else
                    return appointments;
            }
            else
                return appointments;
        }
    
    }
}
