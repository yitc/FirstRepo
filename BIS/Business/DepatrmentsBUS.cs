using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;

namespace BIS.Business
{
    public class DepartmentsBUS
    {
        private DepartmentsDAO departmentsDAO;

        public DepartmentsBUS()
        {
            departmentsDAO = new DepartmentsDAO();
        }

        public bool Save(DepartmentsModel departments, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = departmentsDAO.Save(departments, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool Update(DepartmentsModel departments, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = departmentsDAO.Update(departments, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
       

        public List<IModel> GetAllDepartments()
        {
            List<IModel> compList = new List<IModel>();

            DataTable dataTable = new DataTable();
            dataTable = departmentsDAO.GetAllDepartments();


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    DepartmentsModel model = new DepartmentsModel();

                    model.idDepartment = Int32.Parse(dr["idDepartment"].ToString());
                    model.nameDepartment = dr["nameDepartment"].ToString();
                    model.telephoneDepartment = dr["telephoneDepartment"].ToString();
                    model.emailDepartment = dr["emailDepartment"].ToString();

                    compList.Add(model);
                }

                //   return model;
                return compList;
            }
            else
            {
                return null;
            }
        }

        public List<DepartmentsModel> GetAllDepartments1()
        {
            List<DepartmentsModel> compList = new List<DepartmentsModel>();

            DataTable dataTable = new DataTable();
            dataTable = departmentsDAO.GetAllDepartments();


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    DepartmentsModel model = new DepartmentsModel();

                    model.idDepartment = Int32.Parse(dr["idDepartment"].ToString());
                    model.nameDepartment = dr["nameDepartment"].ToString();
                    model.telephoneDepartment = dr["telephoneDepartment"].ToString();
                    model.emailDepartment = dr["emailDepartment"].ToString();

                    compList.Add(model);
                }

                //   return model;
                return compList;
            }
            else
            {
                return null;
            }
        }

       
        public DepartmentsModel GetDepartmentByID(string idDepartment)
        {
            DataTable dataTable = new DataTable();
            dataTable = departmentsDAO.GetDepartmentsByID(idDepartment);
            DepartmentsModel department = new DepartmentsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    
                    DepartmentsModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new DepartmentsModel();

                        model.idDepartment = Int32.Parse(dr["idDepartment"].ToString());
                        model.nameDepartment = dr["nameDepartment"].ToString();
                        model.telephoneDepartment = dr["telephoneDepartment"].ToString();
                        model.emailDepartment = dr["emailDepartment"].ToString();
                        
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
