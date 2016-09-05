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
    public class EmployeeBUS
    {
        private EmployeeDAO EmployeeDAO;

        public EmployeeBUS()
        {
            EmployeeDAO = new EmployeeDAO();
        }
        public bool Save(EmployeeModel employee, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = EmployeeDAO.Save(employee, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(EmployeeModel employee, string nameForm , int idUser)
        {
            bool retval = false;
            try
            {

                retval = EmployeeDAO.Update(employee, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteEmployee(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = EmployeeDAO.DeleteEmployee(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetAllEmployees(int idFilter, List<int> labels,string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = EmployeeDAO.GetAllEmploees(idFilter, labels, idLang);
            List<IModel> employees = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        EmployeeModel model = new EmployeeModel();

                        model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        model.firstNameEmployee = dr["firstNameEmployee"].ToString();
                        model.initialsEmployee = dr["initialsEmployee"].ToString();
                        if (dr["titleEmployee"].ToString() != "")
                            model.titleEmployee = Int32.Parse(dr["titleEmployee"].ToString());
                        if (dr["nameTitle"].ToString() != "")
                            model.nameTitle = dr["nameTitle"].ToString();
                        model.midNameEmployee = dr["midNameEmployee"].ToString();
                        model.lastNameEmployee = dr["lastNameEmployee"].ToString();
                        model.maidenEmployee = dr["maidenEmployee"].ToString();
                        if (dr["genderEmployee"].ToString() != "")
                            model.genderEmployee = Int32.Parse(dr["genderEmployee"].ToString());
                        if (dr["nameGender"].ToString() != "")
                            model.nameGender = dr["nameGender"].ToString();
                        //adresa
                        model.addressEmployee = dr["addressEmployee"].ToString();
                        model.houseNumberEmployee = dr["houseNumberEmployee"].ToString();
                        model.extensionEmployee = dr["extensionEmployee"].ToString();
                        model.zipCodeEmployee = dr["zipCodeEmployee"].ToString();
                        model.cityEmployee = dr["cityEmployee"].ToString();

                        if (dr["idCountry"].ToString() != "")
                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                        if (dr["internationalCode"].ToString() != "")
                            model.internationalCode = dr["internationalCode"].ToString();
                        if (dr["dtBirthDateEmployee"].ToString() != "")
                            model.dtBirthDateEmployee = DateTime.Parse(dr["dtBirthDateEmployee"].ToString());
                        model.isMariedEmployee = Boolean.Parse(dr["isMariedEmployee"].ToString());

                        model.isentBsnEmploee = dr["isentBsnEmploee"].ToString();
                        model.bicEmployee = dr["bicEmployee"].ToString();
                        model.ibanEmployee = dr["ibanEmployee"].ToString();

                        model.emergencyPersonEmployee = dr["emergencyPersonEmployee"].ToString();
                        model.emergencyTelEmployee = dr["emergencyTelEmployee"].ToString();


                        if (dr["dtHireDateEmployee"].ToString() != "")
                            model.dtHireDateEmployee = DateTime.Parse(dr["dtHireDateEmployee"].ToString());
                        model.contractNumberEmployee = dr["contractNumberEmployee"].ToString();
                        if (dr["Department"].ToString() != "")
                            model.Department = Int32.Parse(dr["Department"].ToString());
                        if (dr["nameDepartment"].ToString() != "")
                            model.nameDepartment = dr["nameDepartment"].ToString();
                        
                        if (dr["Function"].ToString() != "")
                            model.Function = Int32.Parse(dr["Function"].ToString());
                        if (dr["nameFunction"].ToString() != "")
                            model.nameFunction = dr["nameFunction"].ToString();
                        if (dr["WishFunction"].ToString() != "")
                            model.WishFunction = Int32.Parse(dr["WishFunction"].ToString());
                        if (dr["nameWishFunction"].ToString() != "")
                            model.nameWishFunction = dr["nameWishFunction"].ToString();
                        if (dr["statusEmployee"].ToString() != "")
                            model.statusEmployee = Int32.Parse(dr["statusEmployee"].ToString());
                        if (dr["descriptionEmployee"].ToString() != "")
                            model.descriptionEmployee = dr["descriptionEmployee"].ToString();

                        model.imageEmployee = dr["imageEmployee"].ToString();
                        model.isAplicationUser = Boolean.Parse(dr["isAplicationUser"].ToString());



                        employees.Add(model);
                    }
                    return employees;
                }
                else
                    return employees;
            }
            else
                return employees;
        }
        public List<IModel> GetAllEmpl(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = EmployeeDAO.GetAllEmpl(idLang);
            List<IModel> employees = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        EmployeeModel model = new EmployeeModel();

                        model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        model.firstNameEmployee = dr["firstNameEmployee"].ToString();
                        model.initialsEmployee = dr["initialsEmployee"].ToString();
                        if (dr["titleEmployee"].ToString() != "")
                            model.titleEmployee = Int32.Parse(dr["titleEmployee"].ToString());
                        model.midNameEmployee = dr["midNameEmployee"].ToString();
                        model.lastNameEmployee = dr["lastNameEmployee"].ToString();
                        model.maidenEmployee = dr["maidenEmployee"].ToString();
                        if (dr["genderEmployee"].ToString() != "")
                            model.genderEmployee = Int32.Parse(dr["genderEmployee"].ToString());
                        //adresa
                        model.addressEmployee = dr["addressEmployee"].ToString();
                        model.houseNumberEmployee = dr["houseNumberEmployee"].ToString();
                        model.extensionEmployee = dr["extensionEmployee"].ToString();
                        model.zipCodeEmployee = dr["zipCodeEmployee"].ToString();
                        model.cityEmployee = dr["cityEmployee"].ToString();

                        if (dr["idCountry"].ToString() != "")
                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                        if (dr["dtBirthDateEmployee"].ToString() != "")
                            model.dtBirthDateEmployee = DateTime.Parse(dr["dtBirthDateEmployee"].ToString());
                        model.isMariedEmployee = Boolean.Parse(dr["isMariedEmployee"].ToString());

                        model.isentBsnEmploee = dr["isentBsnEmploee"].ToString();
                        model.bicEmployee = dr["bicEmployee"].ToString();
                        model.ibanEmployee = dr["ibanEmployee"].ToString();

                        model.emergencyPersonEmployee = dr["emergencyPersonEmployee"].ToString();
                        model.emergencyTelEmployee = dr["emergencyTelEmployee"].ToString();


                        if (dr["dtHireDateEmployee"].ToString() != "")
                            model.dtHireDateEmployee = DateTime.Parse(dr["dtHireDateEmployee"].ToString());
                        model.contractNumberEmployee = dr["contractNumberEmployee"].ToString();
                        if (dr["Department"].ToString() != "")
                            model.Department = Int32.Parse(dr["Department"].ToString());
                        if (dr["Function"].ToString() != "")
                            model.Function = Int32.Parse(dr["Function"].ToString());
                        if (dr["WishFunction"].ToString() != "")
                            model.WishFunction = Int32.Parse(dr["WishFunction"].ToString());
                        if (dr["statusEmployee"].ToString() != "")
                            model.statusEmployee = Int32.Parse(dr["statusEmployee"].ToString());

                        model.imageEmployee = dr["imageEmployee"].ToString();
                        model.isAplicationUser = Boolean.Parse(dr["isAplicationUser"].ToString());



                        employees.Add(model);
                    }
                    return employees;
                }
                else
                    return employees;
            }
            else
                return employees;
        }

        public List<EmployeeModel> GetAllEmpl1(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = EmployeeDAO.GetAllEmpl(idLang);
            List<EmployeeModel> employees = new List<EmployeeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        EmployeeModel model = new EmployeeModel();

                        model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        model.firstNameEmployee = dr["firstNameEmployee"].ToString();
                        model.initialsEmployee = dr["initialsEmployee"].ToString();
                        if (dr["titleEmployee"].ToString() != "")
                            model.titleEmployee = Int32.Parse(dr["titleEmployee"].ToString());
                        model.midNameEmployee = dr["midNameEmployee"].ToString();
                        model.lastNameEmployee = dr["lastNameEmployee"].ToString();
                        model.maidenEmployee = dr["maidenEmployee"].ToString();
                        if (dr["genderEmployee"].ToString() != "")
                            model.genderEmployee = Int32.Parse(dr["genderEmployee"].ToString());
                        //adresa
                        model.addressEmployee = dr["addressEmployee"].ToString();
                        model.houseNumberEmployee = dr["houseNumberEmployee"].ToString();
                        model.extensionEmployee = dr["extensionEmployee"].ToString();
                        model.zipCodeEmployee = dr["zipCodeEmployee"].ToString();
                        model.cityEmployee = dr["cityEmployee"].ToString();

                        if (dr["idCountry"].ToString() != "")
                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                        if (dr["dtBirthDateEmployee"].ToString() != "")
                            model.dtBirthDateEmployee = DateTime.Parse(dr["dtBirthDateEmployee"].ToString());
                        model.isMariedEmployee = Boolean.Parse(dr["isMariedEmployee"].ToString());

                        model.isentBsnEmploee = dr["isentBsnEmploee"].ToString();
                        model.bicEmployee = dr["bicEmployee"].ToString();
                        model.ibanEmployee = dr["ibanEmployee"].ToString();

                        model.emergencyPersonEmployee = dr["emergencyPersonEmployee"].ToString();
                        model.emergencyTelEmployee = dr["emergencyTelEmployee"].ToString();


                        if (dr["dtHireDateEmployee"].ToString() != "")
                            model.dtHireDateEmployee = DateTime.Parse(dr["dtHireDateEmployee"].ToString());
                        model.contractNumberEmployee = dr["contractNumberEmployee"].ToString();
                        if (dr["Department"].ToString() != "")
                            model.Department = Int32.Parse(dr["Department"].ToString());
                        if (dr["Function"].ToString() != "")
                            model.Function = Int32.Parse(dr["Function"].ToString());
                        if (dr["WishFunction"].ToString() != "")
                            model.WishFunction = Int32.Parse(dr["WishFunction"].ToString());
                        if (dr["statusEmployee"].ToString() != "")
                            model.statusEmployee = Int32.Parse(dr["statusEmployee"].ToString());

                        model.imageEmployee = dr["imageEmployee"].ToString();
                        model.isAplicationUser = Boolean.Parse(dr["isAplicationUser"].ToString());



                        employees.Add(model);
                    }
                    return employees;
                }
                else
                    return employees;
            }
            else
                return employees;
        }

        public EmployeeModel GetEmployee(Int32 iEmpID, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = EmployeeDAO.GetEmployee(iEmpID, idLang);
            EmployeeModel clients = new EmployeeModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    EmployeeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new EmployeeModel();
                        model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        model.firstNameEmployee = dr["firstNameEmployee"].ToString();
                        model.initialsEmployee = dr["initialsEmployee"].ToString();
                        if (dr["titleEmployee"].ToString() != "")
                            model.titleEmployee = Int32.Parse(dr["titleEmployee"].ToString());
                        model.midNameEmployee = dr["midNameEmployee"].ToString();
                        model.lastNameEmployee = dr["lastNameEmployee"].ToString();
                        model.maidenEmployee = dr["maidenEmployee"].ToString();
                        if (dr["genderEmployee"].ToString() != "")
                            model.genderEmployee = Int32.Parse(dr["genderEmployee"].ToString());
                        //adresa
                        model.addressEmployee = dr["addressEmployee"].ToString();
                        model.houseNumberEmployee = dr["houseNumberEmployee"].ToString();
                        model.extensionEmployee = dr["extensionEmployee"].ToString();
                        model.zipCodeEmployee = dr["zipCodeEmployee"].ToString();
                        model.cityEmployee = dr["cityEmployee"].ToString();

                        if (dr["idCountry"].ToString() != "")
                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                        if (dr["dtBirthDateEmployee"].ToString() != "")
                            model.dtBirthDateEmployee = DateTime.Parse(dr["dtBirthDateEmployee"].ToString());
                        model.isMariedEmployee = Boolean.Parse(dr["isMariedEmployee"].ToString());

                        model.isentBsnEmploee = dr["isentBsnEmploee"].ToString();
                        model.bicEmployee = dr["bicEmployee"].ToString();
                        model.ibanEmployee = dr["ibanEmployee"].ToString();

                        model.emergencyPersonEmployee = dr["emergencyPersonEmployee"].ToString();
                        model.emergencyTelEmployee = dr["emergencyTelEmployee"].ToString();


                        if (dr["dtHireDateEmployee"].ToString() != "")
                            model.dtHireDateEmployee = DateTime.Parse(dr["dtHireDateEmployee"].ToString());
                        model.contractNumberEmployee = dr["contractNumberEmployee"].ToString();
                        if (dr["Department"].ToString() != "")
                            model.Department = Int32.Parse(dr["Department"].ToString());
                        if (dr["Function"].ToString() != "")
                            model.Function = Int32.Parse(dr["Function"].ToString());
                        if (dr["WishFunction"].ToString() != "")
                            model.WishFunction = Int32.Parse(dr["WishFunction"].ToString());
                        if (dr["statusEmployee"].ToString() != "")
                            model.statusEmployee = Int32.Parse(dr["statusEmployee"].ToString());

                        model.imageEmployee = dr["imageEmployee"].ToString();
                        model.isAplicationUser = dr["isAplicationUser"].ToString() == "True" ? true : false;
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public int GeLastEmployeeID()
        {
            DataTable dataTable = new DataTable();
            dataTable = EmployeeDAO.GetLastEmployeeID();
            return Int32.Parse(dataTable.Rows[0]["idEmployee"].ToString());
        }

    }
}
