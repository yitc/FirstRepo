using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System.ComponentModel;

namespace BIS.Business
{
    public class ToDoBUS
    {
        private ToDoDAO todoDAO;

        public ToDoBUS()
        {
            todoDAO = new ToDoDAO();
        }

        public int Save(ToDoModel todo, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = todoDAO.Save(todo, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(ToDoModel todo, int iID, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = todoDAO.Update(todo, iID, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = todoDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<ToDoModel> GetToDoALL()
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoALL();
            List<ToDoModel> personToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());

                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();
                        personToDo.Add(model);
                    }
                    return personToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<ToDoModel> GetToDoEmployeeByType(int idEmployee, int priority, int status, int type)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoEmployeeByTypes(idEmployee, priority, status, type);
            List<ToDoModel> personToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());

                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();


                        personToDo.Add(model);
                    }
                    return personToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }

        // SAKI ubacio za TAB Meetings na CP
        public List<ToDoModel> GetToDoPerson(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoPerson(idContPers);
            List<ToDoModel> personToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                  //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                           model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                           model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());


                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                       // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());

                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();
                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena


                        personToDo.Add(model);
                    }
                    return personToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ToDoModel> GetToDoArrangement(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoArrangement(idArrangement);
            List<ToDoModel> personToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());


                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());

                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();
                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena


                        personToDo.Add(model);
                    }
                    return personToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ToDoModel> GetToDoContact(int idContact)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoContact(idContact);
            List<ToDoModel> personToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());


                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());

                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();
                        personToDo.Add(model);
                    }
                    return personToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public List<ToDoModel> GetToDoClient(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoClient(idClient);
            List<ToDoModel> cliToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();

                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena

                        cliToDo.Add(model);
                    }
                    return cliToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ToDoModel> GetToDoEmployee(int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetToDoEmployee(idEmployee);
            List<ToDoModel> personToDo = new List<ToDoModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ToDoModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ToDoModel();
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

                        // model.isRemider = Boolean.Parse(dr["isRemider"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();

                        personToDo.Add(model);
                    }
                    return personToDo;
                }
                else
                    return null;
            }
            else
                return null;
        }
     
        public ToDoModel GetTaskById(int idTask)
        {
            DataTable dataTable = new DataTable();
            dataTable = todoDAO.GetTaskById(idTask);
           ToDoModel personToDo = new ToDoModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ToDoModel model = new ToDoModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        //   model = null;
                        if (dr["idToDo"].ToString() != "")
                            model.idToDo = Int32.Parse(dr["idToDo"].ToString());

                        if (dr["idToDoType"].ToString() != "")
                            model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());

                         model.descriptionToDoType = dr["descriptionToDoType"].ToString();

                        if (dr["dtOpenDate"].ToString() != "")
                            model.dtOpenDate = DateTime.Parse(dr["dtOpenDate"].ToString());
                        if (dr["dtCloseDate"].ToString() != "")
                            model.dtCloseDate = DateTime.Parse(dr["dtCloseDate"].ToString());
                        if (dr["dtEndDate"].ToString() != "")
                            model.dtEndDate = DateTime.Parse(dr["dtEndDate"].ToString());
                        if (dr["planedTime"].ToString() != "")
                            model.planedTime = Decimal.Parse(dr["planedTime"].ToString());
                        if (dr["actualTime"].ToString() != "")
                            model.actualTime = Decimal.Parse(dr["actualTime"].ToString());
                        if (dr["idStatusToDo"].ToString() != "")
                            model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                           model.descriptionStatus = dr["descriptionStatus"].ToString();

                        if (dr["idPriorityToDo"].ToString() != "")
                            model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                            model.descriptionPriority = dr["descriptionPriority"].ToString();

                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());
                        model.descriptionToDo = dr["descriptionToDo"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());

                        if (dr["idOwner"].ToString() != "")
                            model.idOwner = Int32.Parse(dr["idOwner"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

                      //  model.isRemider = Boolean.Parse(dr["isRemider"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nameOwner"].ToString() != "")
                            model.nameOwner = dr["nameOwner"].ToString();

                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        if (dr["reasonContact"].ToString() != "")
                            model.reasonContact = dr["reasonContact"].ToString();
                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena

                       // personToDo.Add(model);
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}

        // Do ovde