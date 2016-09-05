using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
    public class TypeBUS
    {
        private TypeDAO tpDAO;
        public TypeBUS()
        {
            tpDAO = new TypeDAO();
        }

        public List<IModel> GetAllType(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.GetAllType( language);
            List<IModel> filters = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TypeModel model = new TypeModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());

                        model.name = dr["name"].ToString();
                        model.type = dr["type"].ToString();

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public bool UpdateAccDailyType(int idDailyType, string descDailyType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateAccDailyType(idDailyType, descDailyType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateTypesAddress(int idAddressType, string nameAddressType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateTypesAddress(idAddressType, nameAddressType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateClientType(int idTypeClient, string nameTypeClient, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateClientType(idTypeClient, nameTypeClient, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateContactType(int idContactType, string descContactType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateContactType(idContactType, descContactType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMedAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateMedAnsType(idAnsType, nameAnsType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateToDoType(int idTypeToDo, string descriptionType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateToDoType(idTypeToDo, descriptionType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateVolAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateVolAnsType(idAnsType, nameAnsType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateTypesTel(int idTelType, string nameTelType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateTypesTel(idTelType, nameTelType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateTypesNote(int idTypeNote, string nameTypeNote, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateTypesNote(idTypeNote, nameTypeNote, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateTypesEmail(int idEmailType, string nameEmailType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateTypesEmail(idEmailType, nameEmailType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMeetingCategory(int idMeetingCategory, string categoryDescription, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateMeetingCategory(idMeetingCategory, categoryDescription, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMeetingPriority(int idPriority, string descriptionPriority, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateMeetingPriority(idPriority, descriptionPriority, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateToDoPriority(int idPriorityToDo, string descriptionPriority, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateToDoPriority(idPriorityToDo, descriptionPriority, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateTitle(int idTitle, string nameTitle, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateTitle(idTitle, nameTitle, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateEmployeeFunction(int idFunction, string nameFunction, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateEmployeeFunction(idFunction, nameFunction, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateEmployeeStatus(int idStatusEmployee, string descriptionEmployee, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateEmployeeStatus(idStatusEmployee, descriptionEmployee, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMeetingStatus(int idMeetingStatus, string desriptionMeetingStatus, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateMeetingStatus(idMeetingStatus, desriptionMeetingStatus, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateToDoStatus(int idStatusToDo, string descriptionStatus, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateToDoStatus(idStatusToDo, descriptionStatus, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateContactReason(int idContactReason, string descContactReason, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateContactReason(idContactReason, descContactReason, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertAccDailyType(int idDailyType, string descDailyType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertAccDailyType(idDailyType, descDailyType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertTypesAddress(int idAddressType, string nameAddressType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertTypesAddress(idAddressType, nameAddressType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertClientTypes(int idTypeClient, string nameTypeClient, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertClientTypes(idTypeClient, nameTypeClient, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertContactType(int idContactType, string descContactType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertContactType(idContactType, descContactType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertTypesEmail(int idEmailType, string nameEmailType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertTypesEmail(idEmailType, nameEmailType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertMedAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertMedAnsType(idAnsType, nameAnsType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertTypesNote(int idTypeNote, string nameTypeNote, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertTypesNote(idTypeNote, nameTypeNote, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertTypesTel(int idTelType, string nameTelType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertTypesTel(idTelType, nameTelType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertToDoType(int idToDoType, string descriptionToDoType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertToDoType(idToDoType, descriptionToDoType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertVolAnsType(int idAnsType, string nameAnsType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertVolAnsType(idAnsType, nameAnsType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertMeetingCategory(int idMeetingCategory, string categoryDescription, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertMeetingCategory(idMeetingCategory, categoryDescription, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertMeetingsPriority(int idPriority, string descriptionPriority, string nameForm, int idUser)
           {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertMeetingsPriority(idPriority, descriptionPriority, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertToDoPriority(int idPriorityToDo, string descriptionPriority, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertToDoPriority(idPriorityToDo, descriptionPriority, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertTitle(int idTitle, string nameTitle, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertTitle(idTitle, nameTitle, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertEmployeeFunction(int idFunction, string nameFunction, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertEmployeeFunction(idFunction, nameFunction, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertEmployeeStatus(int idStatusEmployee, string descriptionEmployee, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertEmployeeStatus(idStatusEmployee, descriptionEmployee, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertMeetingsStatus(int idMeetingStatus, string desriptionMeetingStatus, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertMeetingsStatus(idMeetingStatus, desriptionMeetingStatus, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertToDoStatus(int idStatusToDo, string descriptionStatus, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertToDoStatus(idStatusToDo, descriptionStatus, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertContactReason(int idContactReason, string descContactReason, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertContactReason(idContactReason, descContactReason, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteAccDailyType(int idDailyType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteAccDailyType(idDailyType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteTypesAddress(int idAddressType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteTypesAddress(idAddressType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteClientTypes(int idTypeClient, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteClientTypes(idTypeClient, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteContactType(int idContactType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteContactType(idContactType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVolAnsType(int idAnsType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteVolAnsType(idAnsType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteToDoType(int idTypeToDo, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteToDoType(idTypeToDo, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMedAnsType(int idAnsType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteMedAnsType(idAnsType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteTypesTel(int idTelType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteTypesTel(idTelType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteTypesNote(int idTypeNote, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteTypesNote(idTypeNote, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteTypesEmail(int idEmailType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteTypesEmail(idEmailType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMeetingsCategory(int idMeetingCategory, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteMeetingsCategory(idMeetingCategory, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMeetingsPriority(int idPriority, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteMeetingsPriority(idPriority, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteToDoPriority(int idPriorityToDo, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteToDoPriority(idPriorityToDo, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteTitle(int idTitle, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteTitle(idTitle, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteEmployeeFunction(int idFunction, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteEmployeeFunction(idFunction, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteEmployeeStatus(int idStatusEmployee, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteEmployeeStatus(idStatusEmployee, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMeetingsStatus(int idMeetingStatus, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteMeetingsStatus(idMeetingStatus, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteToDoStatus(int idStatusToDo, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteToDoStatus(idStatusToDo, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteContactReason(int idContactReason, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteContactReason(idContactReason, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        # region IsIn
        public List<LastIdModel> isInAccDailyType()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInAccDailyType();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInAddress()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInAddress();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                        model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInClient()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInClient();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"] != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInContact()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInContact();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInContactReason()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInContactReason();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInEmail()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInEmail();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInEmployeeFunction()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInEmloyeeFunction();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInEmployeeStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInEmployeestatus();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInMedAnsType()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInMedAnsType();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInMeetingsCategory()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInMeetingCategory();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInMeetingsPriority()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInMeetingPriority();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInMeetingsStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInMeetingStatus();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInNote()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInNote();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInTel()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInTel();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInToDoPriority()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInToDoPriority();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInToDoType()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInToDoType();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInToDoStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInToDoStatus();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInVolAnsType()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInVolAnsType();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        public List<LastIdModel> isInTitle()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.isInTitle();
            List<LastIdModel> f = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        f.Add(model);
                    }
                    return f;
                }
                else
                    return f;
            }
            else
                return f;
        }

        #endregion
    }

}
