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
    public class EmployeeEmailBUS
    {
        private EmployeeEmailDAO employeeEmailDAO;

        public EmployeeEmailBUS()
        {
            employeeEmailDAO = new EmployeeEmailDAO();
        }

        public bool Save(EmployeeEmailModel employeeEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeeEmailDAO.Save(employeeEmail, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Delete(int idEmpEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeeEmailDAO.Delete(idEmpEmail, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(EmployeeEmailModel employeeEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeeEmailDAO.Update(employeeEmail, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<EmployeeEmailModel> GetEmployeeEmails(int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = employeeEmailDAO.GetEmployeeEmails(idEmployee);
            List<EmployeeEmailModel> employeesEmails = new List<EmployeeEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        EmployeeEmailModel model = new EmployeeEmailModel();

                        model.idEmpEmail = Int32.Parse(dr["idEmpEmail"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                        {
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        }

                        model.email = dr["email"].ToString();

                        if (dr["emailType"].ToString() != "")
                        {
                            model.emailType = Int32.Parse(dr["emailType"].ToString());
                        }

                        employeesEmails.Add(model);
                    }
                    return employeesEmails;
                }
                else
                    return employeesEmails;

            }
            else
                return employeesEmails;

        }

        public List<EmployeeEmailModel> GetEmployeeEmailByType(int emailType, int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = employeeEmailDAO.GetEmployeeEmailsByType(emailType, idEmployee);
            List<EmployeeEmailModel> employeesEmails = new List<EmployeeEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        EmployeeEmailModel model = new EmployeeEmailModel();

                        model.idEmpEmail = Int32.Parse(dr["idEmpEmail"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                        {
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        }

                        model.email = dr["email"].ToString();

                        if (dr["emailType"].ToString() != "")
                        {
                            model.emailType = Int32.Parse(dr["emailType"].ToString());
                        }

                        employeesEmails.Add(model);

                    }
                    return
                        employeesEmails;
                }
                else
                    return employeesEmails;
            }
            else
                return employeesEmails;
        }

        public DataTable GetEmployeeEmailByTypeTable(int emailType, int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = employeeEmailDAO.GetEmployeeEmailsByType(emailType, idEmployee);

            return dataTable;
        }
    }
}

