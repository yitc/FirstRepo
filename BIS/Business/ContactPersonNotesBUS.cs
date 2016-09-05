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
    public class ContactPersonNotesBUS
    {
        private ContactPersonNotesDAO personNotesDAO;

        public ContactPersonNotesBUS()
        {
            personNotesDAO = new ContactPersonNotesDAO();
        }

        public List<ContactPersonNotesModel> GetPersonNotes(int idContPers)
        {
            DataTable dataTable = personNotesDAO.GetPersonNotes(idContPers);
            List<ContactPersonNotesModel> lst = new List<ContactPersonNotesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactPersonNotesModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ContactPersonNotesModel();
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

        public ContactPersonNotesModel GetPersonNote(int idNote)
        {
            DataTable dataTable = personNotesDAO.GetPersonNote(idNote);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactPersonNotesModel model = new ContactPersonNotesModel();
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
