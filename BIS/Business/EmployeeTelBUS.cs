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
    public class EmployeeTelBUS
    {
        private EmployeeTelDAO employeeTelDAO;

        public EmployeeTelBUS()
        {
            employeeTelDAO = new EmployeeTelDAO();
        }

        //public List<EmployeeTelModel> GetEmployeeTels(int idEmployee, string idLang)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = employeeTelDAO.GetEmployeeTels(idEmployee,idLang);
        //    List<EmployeeTelModel> employeeTel = new List<EmployeeTelModel>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                EmployeeTelModel model = new EmployeeTelModel();

        //              //  model.idtelemp = Int32.Parse(dr["idtelemp"].ToString());
        //              //  model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

        //                model.telephone = dr["telephone"].ToString();
        //                if (dr["telephoneType"].ToString() != "")
        //                    model.telephoneType = Int32.Parse(dr["telephoneType"].ToString());
        //                model.isDefault = Boolean.Parse(dr["isDefault"].ToString());
        //                model.description = dr["description"].ToString();

        //                employeeTel.Add(model);
        //            }
        //            return employeeTel;
        //        }
        //        else
        //            return employeeTel;
        //    }
        //    else
        //        return employeeTel;
        //}

        public bool Save(EmployeeTelModel employeeTel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeeTelDAO.Save(employeeTel, nameForm, idUser);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(EmployeeTelModel employeeTel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeeTelDAO.Update(employeeTel, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<EmployeeTelModel> GetEmployeeTels(int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = employeeTelDAO.GetEmployeeTels(idEmployee);
            List<EmployeeTelModel> employeeTel = new List<EmployeeTelModel>();
            {
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            EmployeeTelModel model = new EmployeeTelModel();

                            model.idtelemp = Int32.Parse(dr["idtelemp"].ToString());
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                            model.telephone = dr["telephone"].ToString();

                            if (dr["telephoneType"].ToString() != "")
                            {
                                model.telephoneType = Int32.Parse(dr["telephoneType"].ToString());
                            }

                            if (dr["isDefault"].ToString() != "")
                            {
                                model.isDefault = Boolean.Parse(dr["isDefault"].ToString());
                            }

                            model.description = dr["description"].ToString();

                            employeeTel.Add(model);
                        }
                        return employeeTel;
                    }
                    else
                        return employeeTel;
                }
                else
                    return employeeTel;
            }
        }

        public List<EmployeeTelModel> GetPersonTelsByType(int telefoneType , int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = employeeTelDAO.GetEmployeeTelsByType(telefoneType, idEmployee);
            List<EmployeeTelModel> employeeTel = new List<EmployeeTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        EmployeeTelModel model = new EmployeeTelModel();

                        model.idtelemp = Int32.Parse(dr["idtelemp"].ToString());
                        model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        model.telephone = dr["telephone"].ToString();

                        if (dr["telephoneType"].ToString() != "")
                        {
                            model.telephoneType = Int32.Parse(dr["telephoneType"].ToString());
                        }

                        if (dr["isDefault"].ToString() != "")
                        {
                            model.isDefault = Boolean.Parse(dr["isDefault"].ToString());
                        }

                        model.description = dr["description"].ToString();

                        employeeTel.Add(model);
                    }
                    return employeeTel;
                }
                else
                    return employeeTel;
            }
            else
                return employeeTel;
        }
    }
}
            
            
            
        

    

    


