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
    public class ArrangementBookPersonsBUS
    {
        private ArrangementBookPersonsDAO arrBookPersonsDAO;

        public ArrangementBookPersonsBUS()
        {
            arrBookPersonsDAO = new ArrangementBookPersonsDAO();
        }

        public bool SaveTravelWith(System.ComponentModel.BindingList<ArrangementTravelersModel> lista, int idArrangement, int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookPersonsDAO.SaveTravelWith(lista, idArrangement, idContPers, nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;

        }

        public bool UpdatePayInvoicePerson(int idArrangement, int idContPers, int idPayInvoice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookPersonsDAO.UpdatePayInvoicePerson(idArrangement, idContPers, idPayInvoice, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;

        }

        public bool DeletePersonFromGrid(int idArrangementBook, int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookPersonsDAO.DeletePersonFromGrid(idArrangementBook, idContPers, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<PersonModel> GetAllTravelersForArrangement(int idArrangement, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookPersonsDAO.GetAllTravelersForArrangement(idArrangement, idContPers);
            List<PersonModel> persons = new List<PersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();                        

                        persons.Add(model);
                    }
                    return persons;
                }
                else
                    return persons;
            }
            else
                return persons;
        }

        public BindingList<ArrangementTravelersModel> GetAllTravelersWith(int idContPers, int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookPersonsDAO.GetAllTravelersWith(idContPers, idArrangement);
            BindingList<ArrangementTravelersModel> persons = new BindingList<ArrangementTravelersModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementTravelersModel model = new ArrangementTravelersModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idTravelWithPerson"].ToString() != "")
                            model.idTravelWithPerson = Int32.Parse(dr["idTravelWithPerson"].ToString());

                        model.firstnameTraveler = dr["firstname"].ToString();
                        model.lastnameTraveler = dr["lastname"].ToString();
                        model.midnameTraveler = dr["midname"].ToString();
                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        persons.Add(model);
                    }
                    return persons;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public BindingList<ArrangementTravelersInvoiceModel> GetAllTravelersInvoicing(int idArrangementBook, Boolean isNamePassport)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookPersonsDAO.GetAllTravelersInvoicing(idArrangementBook, isNamePassport);
            BindingList<ArrangementTravelersInvoiceModel> persons = new BindingList<ArrangementTravelersInvoiceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementTravelersInvoiceModel model = new ArrangementTravelersInvoiceModel();

                        if (dr["idArrangementBook"].ToString() != "")
                            model.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idPayInvoice"].ToString() != "")
                            model.idPayInvoice = Int32.Parse(dr["idPayInvoice"].ToString());

                        model.firstnameTraveler = dr["firstname"].ToString();
                        model.lastnameTraveler = dr["lastname"].ToString();

                        if (dr["fullname"] != null)
                            model.passportname = dr["fullname"].ToString();  

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        persons.Add(model);
                    }
                    return persons;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public BindingList<ArrangementTravelersModel> GetTravelerForTravelerWith(int idContPers, int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookPersonsDAO.GetTravelerForTravelerWith(idContPers, idArrangement);
            BindingList<ArrangementTravelersModel> persons = new BindingList<ArrangementTravelersModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementTravelersModel model = new ArrangementTravelersModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idTravelWithPerson"].ToString() != "")
                            model.idTravelWithPerson = Int32.Parse(dr["idTravelWithPerson"].ToString());

                        model.firstnameTraveler = dr["firstname"].ToString();
                        model.lastnameTraveler = dr["lastname"].ToString();
                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        persons.Add(model);
                    }
                    return persons;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}

  