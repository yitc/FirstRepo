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
    public class PersonPassportBUS
    {
        private PersonPassportDAO personPassportDAO;

        public PersonPassportBUS()
        {
            personPassportDAO = new PersonPassportDAO();
        }

        public bool Save(PersonPassportModel personPass, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personPassportDAO.Save(personPass,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public bool Update(PersonPassportModel personPass, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personPassportDAO.Update(personPass,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public PersonPassportModel GetPassport(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personPassportDAO.GetPersonPassport(idPerson);
            PersonPassportModel personsPass = new PersonPassportModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PersonPassportModel model = new PersonPassportModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.namePassport = dr["namePassport"].ToString();
                        model.numberPassport = dr["numberPassport"].ToString();
                        model.birthPlacePassport = dr["birthPlacePassport"].ToString();
                        model.issuePlacePassport = dr["issuePlacePassport"].ToString();
                        
                        if (dr["dtPassportIssued"].ToString() != "")
                            model.dtPassportIssued = DateTime.Parse(dr["dtPassportIssued"].ToString());
                                             
                        if (dr["dtPassportValid"].ToString() != "")
                            model.dtPassportValid = DateTime.Parse(dr["dtPassportValid"].ToString());

                        if (dr["idCountry"].ToString() != "")
                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                        model.lastNamePassport = dr["lastNamePassport"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public string GetPersonPassportFullName(int idPerson)
        {
            string retval = String.Empty;
            try
            {

                object obj = personPassportDAO.GetPersonPassportFullName(idPerson);

                if (obj != null)
                    retval = Convert.ToString(obj);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
       
    }
}
