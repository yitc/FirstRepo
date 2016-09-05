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
    public class EmployeePassportBUS
    {
        private EmployeePassportDAO employeePassportDAO;

        public EmployeePassportBUS()
        {
            employeePassportDAO = new EmployeePassportDAO();
        }

        public bool Save(EmployeePassportModel employeePasseport, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeePassportDAO.Save(employeePasseport, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(EmployeePassportModel employeePasseport, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = employeePassportDAO.Update(employeePasseport, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public EmployeePassportModel GetEmpoyeePassport(int idEmployee)
        {
            DataTable dataTable = new DataTable();
            dataTable = employeePassportDAO.GetAllEmployeePassport(idEmployee);
            EmployeePassportModel emlpoyeePass = new EmployeePassportModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    EmployeePassportModel model = new EmployeePassportModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model.idemppass = Int32.Parse(dr["idemppass"].ToString());
                        model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        model.passname = dr["passname"].ToString();
                        model.passnumber = dr["passnumber"].ToString();
                        model.passbrplace = dr["passbrplace"].ToString();
                        model.passisplace = dr["passisplace"].ToString();

                        if (dr["passisued"].ToString() != "")
                            model.passisued = DateTime.Parse(dr["passisued"].ToString());

                        if (dr["passvalid"].ToString() != "")
                            model.passvalid = DateTime.Parse(dr["passvalid"].ToString());

                        if (dr["passnational"].ToString() != "")
                            model.passnational = Int32.Parse(dr["passnational"].ToString());

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
