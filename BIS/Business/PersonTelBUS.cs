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
    public class PersonTelBUS
    {
        private PersonTelDAO personTelDAO;

        public PersonTelBUS()
        {
            personTelDAO = new PersonTelDAO();
        }

        public bool Save(PersonTelModel personTel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personTelDAO.Save(personTel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idTel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personTelDAO.Delete(idTel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(PersonTelModel personTel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personTelDAO.Update(personTel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public List<PersonTelModel> GetPersonTels(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personTelDAO.GetPersonTels(idPerson);
            List<PersonTelModel> personsTel = new List<PersonTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonTelModel model = new PersonTelModel();
                        model.idTel = Int32.Parse(dr["idTel"].ToString());
                       model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                       model.numberTel = dr["numberTel"].ToString();
                       model.isDefaultTel = Boolean.Parse(dr["isDefaultTel"].ToString());
                       model.descriptionTel = dr["descriptionTel"].ToString();
                       if (dr["idTelType"].ToString() != "")
                           model.idTelType = Int32.Parse(dr["idTelType"].ToString());

                       personsTel.Add(model);
                    }
                    return personsTel;
                }
                else
                    return personsTel;
            }
            else
                return personsTel;
        }

        public List<PersonTelModel> GetAllPersonTels()
        {
            DataTable dataTable = new DataTable();
            dataTable = personTelDAO.GetAllPersonTels();
            List<PersonTelModel> personsTel = new List<PersonTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonTelModel model = new PersonTelModel();
                        model.idTel = Int32.Parse(dr["idTel"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.numberTel = dr["numberTel"].ToString();
                        model.isDefaultTel = Boolean.Parse(dr["isDefaultTel"].ToString());
                        model.descriptionTel = dr["descriptionTel"].ToString();
                        if (dr["idTelType"].ToString() != "")
                            model.idTelType = Int32.Parse(dr["idTelType"].ToString());

                        personsTel.Add(model);
                    }
                    return personsTel;
                }
                else
                    return personsTel;
            }
            else
                return personsTel;
        }

        public List<PersonTelModel> GetPersonTelsByType(int idTypeTel, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personTelDAO.GetPersonTelsByType(idTypeTel, idContPers);
            List<PersonTelModel> personsTel = new List<PersonTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonTelModel model = new PersonTelModel();
                        model.idTel = Int32.Parse(dr["idTel"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.numberTel = dr["numberTel"].ToString();
                        model.isDefaultTel = Boolean.Parse(dr["isDefaultTel"].ToString());
                        model.descriptionTel = dr["descriptionTel"].ToString();
                        if (dr["idTelType"].ToString() != "")
                            model.idTelType = Int32.Parse(dr["idTelType"].ToString());

                        personsTel.Add(model);
                    }
                    return personsTel;
                }
                else
                    return personsTel;
            }
            else
                return personsTel;
        }
    }
}

