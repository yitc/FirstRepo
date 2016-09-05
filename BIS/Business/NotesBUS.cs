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
    public class NotesBUS
    {
        private NotesDAO personNotesDAO;

        public NotesBUS()
        {
            personNotesDAO = new NotesDAO();
        }

        public List<NotesModel> GetPersonNotes(int idContPers)
        {
            DataTable dataTable = personNotesDAO.GetPersonNotes(idContPers);
            List<NotesModel> lst = new List<NotesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    NotesModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new NotesModel();
                        model.idNote = Int32.Parse(dr["idNote"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
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

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

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

        public List<NotesModel> GetPersonStatus2(int idContPers)
        {
            DataTable dataTable = personNotesDAO.GetPersonStatus2(idContPers);
            List<NotesModel> lst = new List<NotesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    NotesModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new NotesModel();
                        model.idNote = Int32.Parse(dr["idNote"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
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

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();

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


        public NotesModel GetPersonNote(int idNote)
        {
            DataTable dataTable = personNotesDAO.GetPersonNote(idNote);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    NotesModel model = new NotesModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model.idNote = Int32.Parse(dr["idNote"].ToString());
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
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
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["nameContPers"].ToString() != "")
                            model.nameContPers = dr["nameContPers"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public bool Save(NotesModel personNotes, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personNotesDAO.Save(personNotes, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(NotesModel personNotes, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personNotesDAO.Update(personNotes,nameForm,idUser);

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

                retval = personNotesDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
