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
    public class PersonEmailBUS
    {
        private PersonEmailDAO personEmailDAO;

        public PersonEmailBUS()
        {
            personEmailDAO = new PersonEmailDAO();
        }

        public bool Save(PersonEmailModel personEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personEmailDAO.Save(personEmail,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personEmailDAO.Delete(idEmail,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(PersonEmailModel personEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personEmailDAO.Update(personEmail,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<PersonEmailModel> GetPersonEmails(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personEmailDAO.GetPersonEmails(idPerson);
            List<PersonEmailModel> personsEmails = new List<PersonEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonEmailModel model = new PersonEmailModel();

                        model.idEmail = Int32.Parse(dr["idEmail"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.email = dr["email"].ToString();
                        model.isCommunication = Boolean.Parse(dr["isCommunication"].ToString());
                        model.isProspect = Boolean.Parse(dr["isProspect"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        model.isNewsletters = Boolean.Parse(dr["isNewsletters"].ToString());
                         if (dr["idEmailType"].ToString() != "")
                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());

                         if (dr["lastQuestionForm"].ToString() != "")
                             model.lastQuestionForm = Boolean.Parse(dr["lastQuestionForm"].ToString());


                           personsEmails.Add(model);
                    }
                    return personsEmails;
                }
                else
                    return personsEmails;
            }
            else
                return personsEmails;
        }

        public List<PersonEmailModel> GetAllPersonEmails()
        {
            DataTable dataTable = new DataTable();
            dataTable = personEmailDAO.GetAllPersonEmails();
            List<PersonEmailModel> personsEmails = new List<PersonEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonEmailModel model = new PersonEmailModel();

                        model.idEmail = Int32.Parse(dr["idEmail"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.email = dr["email"].ToString();
                        model.isCommunication = Boolean.Parse(dr["isCommunication"].ToString());
                        model.isProspect = Boolean.Parse(dr["isProspect"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        model.isNewsletters = Boolean.Parse(dr["isNewsletters"].ToString());
                        if (dr["idEmailType"].ToString() != "")
                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());

                        if (dr["lastQuestionForm"].ToString() != "")
                            model.lastQuestionForm = Boolean.Parse(dr["lastQuestionForm"].ToString());


                        personsEmails.Add(model);
                    }
                    return personsEmails;
                }
                else
                    return personsEmails;
            }
            else
                return personsEmails;
        }

        public List<PersonEmailModel> GetPersonEmailsIsCommunication(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personEmailDAO.GetPersonEmailsISCommunication(idPerson);
            List<PersonEmailModel> personsEmails = new List<PersonEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonEmailModel model = new PersonEmailModel();

                        model.idEmail = Int32.Parse(dr["idEmail"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.email = dr["email"].ToString();
                        model.isCommunication = Boolean.Parse(dr["isCommunication"].ToString());
                        model.isProspect = Boolean.Parse(dr["isProspect"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        model.isNewsletters = Boolean.Parse(dr["isNewsletters"].ToString());
                        if (dr["idEmailType"].ToString() != "")
                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());

                        if (dr["lastQuestionForm"].ToString() != "")
                            model.lastQuestionForm = Boolean.Parse(dr["lastQuestionForm"].ToString());

                        personsEmails.Add(model);
                    }
                    return personsEmails;
                }
                else
                    return personsEmails;
            }
            else
                return personsEmails;
        }


        public bool CheckPErsonForInvoicingEmail(int idContPers)
        {
            return (bool) personEmailDAO.CheckPErsonForInvoicingEmail(idContPers);
        }
        public List<PersonEmailModel> GetPersonEmailsByType(int idEmialType, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personEmailDAO.GetPersonEmailsByType(idEmialType, idContPers);
            List<PersonEmailModel> personsEmails = new List<PersonEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonEmailModel model = new PersonEmailModel();

                        model.idEmail = Int32.Parse(dr["idEmail"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.email = dr["email"].ToString();
                        model.isCommunication = Boolean.Parse(dr["isCommunication"].ToString());
                        model.isProspect = Boolean.Parse(dr["isProspect"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        model.isNewsletters = Boolean.Parse(dr["isNewsletters"].ToString());
                        if (dr["idEmailType"].ToString() != "")
                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());

                        if (dr["lastQuestionForm"].ToString() != "")
                            model.lastQuestionForm = Boolean.Parse(dr["lastQuestionForm"].ToString());

                        personsEmails.Add(model);
                    }
                    return personsEmails;
                }
                else
                    return personsEmails;
            }
            else
                return personsEmails;
        }
        public DataTable GetPersonEmailsIsCommunicationTable(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personEmailDAO.GetPersonEmailsISCommunication(idPerson);
            return dataTable;
        }


        public PersonEmailiSInvoiceModel GetPersonEmailsISInoicing(int idPerson)
        {
            PersonEmailiSInvoiceModel model = new PersonEmailiSInvoiceModel();

            DataTable dataTable = new DataTable();
            dataTable = personEmailDAO.GetPersonEmailsISInoicing(idPerson);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["isInvoicing"].ToString() != "")
                            model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        
                        model.email = dr["email"].ToString();
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

