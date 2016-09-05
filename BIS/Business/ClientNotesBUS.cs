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
    public class ClientNotesBUS
    {
        private ClientNotesDAO clientNotesDAO;

        public ClientNotesBUS()
        {
            clientNotesDAO = new ClientNotesDAO();
        }

        public List<ClientNotesModel> GetClientNotes(int idClient)
        {
            DataTable dataTable = clientNotesDAO.GetClientNotes(idClient);
            List<ClientNotesModel> lst = new List<ClientNotesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientNotesModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientNotesModel();
                        model.idNote = Int32.Parse(dr["idNote"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["dtNoteDate"].ToString() != "")
                            model.dtNoteDate = DateTime.Parse(dr["dtNoteDate"].ToString());
                        model.noteText = dr["noteText"].ToString();
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());
                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["idTypeNote"].ToString() != "")
                            model.idTypeNote = Int32.Parse(dr["idTypeNote"].ToString());
                        if (dr["nameType"].ToString() != "")
                            model.nameType = dr["nameType"].ToString();

                        lst.Add(model);
                    }
                    return lst;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<ClientNotesModel> GetClientNotesBy2(int idClient)
        {
            DataTable dataTable = clientNotesDAO.GetClientNotesBy2(idClient);
            List<ClientNotesModel> lst = new List<ClientNotesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientNotesModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientNotesModel();
                        model.idNote = Int32.Parse(dr["idNote"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["dtNoteDate"].ToString() != "")
                            model.dtNoteDate = DateTime.Parse(dr["dtNoteDate"].ToString());
                        model.noteText = dr["noteText"].ToString();
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());
                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["idTypeNote"].ToString() != "")
                            model.idTypeNote = Int32.Parse(dr["idTypeNote"].ToString());
                        if (dr["nameType"].ToString() != "")
                            model.nameType = dr["nameType"].ToString();

                        lst.Add(model);
                    }
                    return lst;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public ClientNotesModel GetClientNote(int idNote)
        {
            DataTable dataTable = clientNotesDAO.GetClientNote(idNote);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientNotesModel model = new ClientNotesModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model.idNote = Int32.Parse(dr["idNote"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["dtNoteDate"].ToString() != "")
                            model.dtNoteDate = DateTime.Parse(dr["dtNoteDate"].ToString());
                        model.noteText = dr["noteText"].ToString();
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());
                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());
                        if (dr["idTypeNote"].ToString() != "")
                            model.idTypeNote = Int32.Parse(dr["idTypeNote"].ToString());
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public bool Save(ClientNotesModel clientNotes, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientNotesDAO.Save(clientNotes, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ClientNotesModel clientNotes, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientNotesDAO.Update(clientNotes, nameForm, idUser);

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

                retval = clientNotesDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
