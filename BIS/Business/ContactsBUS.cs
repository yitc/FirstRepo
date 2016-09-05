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
    public class ContactsBUS
    {
        private ContactsDAO contactsDAO;

        public ContactsBUS()
        {
            contactsDAO = new ContactsDAO();
        }

        public Int32 SaveID(ContactsModel contacts, string nameForm, int idUser)
        {
            Int32 retval = -1;
            try
            {

                retval = contactsDAO.SaveID(contacts, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public Int32 GetID(ContactsModel contacts)
        {
            Int32 retval = -1;
            try
            {

                retval = contactsDAO.GetID(contacts);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Save(ContactsModel contacts, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = contactsDAO.Save(contacts, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(ContactsModel contacts, int iID, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = contactsDAO.Update(contacts, iID, nameForm, idUser);

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

                retval = contactsDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        // SAKI ubacio za TAB Meetings na CP
        public List<ContactsModel> GetContactsALL()
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactsALL();
            List<ContactsModel> personContacts = new List<ContactsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ContactsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ContactsModel();
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();




                        personContacts.Add(model);
                    }
                    return personContacts;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<ContactsModel> GetContactsByPerson(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactsByPerson(idContPers);
            List<ContactsModel> personContacts = new List<ContactsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ContactsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ContactsModel();
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena
                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();

                     
                       

                        personContacts.Add(model);
                    }
                    return personContacts;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ContactsModel> GetContactsByArrangament(int idArrangament)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactsByArrangament(idArrangament);
            List<ContactsModel> personContacts = new List<ContactsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ContactsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ContactsModel();
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena
                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();




                        personContacts.Add(model);
                    }
                    return personContacts;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<ContactsModel> GetContactsByCreator(int idCreator)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactsByCreator(idCreator);
            List<ContactsModel> personContacts = new List<ContactsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ContactsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ContactsModel();
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();




                        personContacts.Add(model);
                    }
                    return personContacts;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public ContactsModel GetContactById(int idContact)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactById(idContact);
            ContactsModel personToDo = new ContactsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactsModel model = new ContactsModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                       // model = new ContactsModel();
                        //   model = null;
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();
                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        //end jelena
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
      
        public List<ContactsModel> GetContactsByClient(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactsByClient(idClient);
            List<ContactsModel>clientContacts = new List<ContactsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ContactsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ContactsModel();
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();
                        // jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        //end jelena



                        clientContacts.Add(model);
                    }
                    return clientContacts;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ContactsModel> GetContactsByCreatorTypes(int idCreator, int reason, int type)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactsDAO.GetContactsByCreatorTypes(idCreator, reason, type);
            List<ContactsModel> personContacts = new List<ContactsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  ToDoModel model = new ToDoModel();
                    ContactsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new ContactsModel();
                        if (dr["idContact"].ToString() != "")
                            model.idContact = Int32.Parse(dr["idContact"].ToString());

                        if (dr["idContactReason"].ToString() != "")
                            model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());

                        model.reasonContact = dr["reasonContact"].ToString();

                        if (dr["idContactType"].ToString() != "")
                            model.idContactType = Int32.Parse(dr["idContactType"].ToString());

                        if (dr["dateContact"].ToString() != "")
                            model.dateContact = DateTime.Parse(dr["dateContact"].ToString());
                        if (dr["openTimeContact"].ToString() != "")
                            model.openTimeContact = TimeSpan.Parse(dr["openTimeContact"].ToString());
                        if (dr["closeTimeCOntact"].ToString() != "")
                            model.closeTimeCOntact = TimeSpan.Parse(dr["closeTimeCOntact"].ToString());
                        if (dr["durationContact"].ToString() != "")
                            model.durationContact = TimeSpan.Parse(dr["durationContact"].ToString());
                        if (dr["idCreator"].ToString() != "")
                            model.idCreator = Int32.Parse(dr["idCreator"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        model.noteContact = dr["noteContact"].ToString();
                        model.descContactReason = dr["descContactReason"].ToString();
                        model.descContactType = dr["descContactType"].ToString();




                        personContacts.Add(model);
                    }
                    return personContacts;
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