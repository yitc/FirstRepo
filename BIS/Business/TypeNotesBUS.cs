using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
    using System.Data;
    using System.Data.SqlClient;
    using BIS.DAO;
    using BIS.Model;

    public class TypeNotesBUS
    {

        private TypeNotesDAO typeNoteDAO;

        public TypeNotesBUS()
        {
            typeNoteDAO = new TypeNotesDAO();
        }

        public List<TypeNotesModel> GetAllTypeNotes(string idLang)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = typeNoteDAO.GetAllTypeNotes(idLang);

                List<TypeNotesModel> typenote = new List<TypeNotesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TypeNotesModel model = new TypeNotesModel();

                            model.idTypeNote = Int32.Parse(dr["idTypeNote"].ToString());
                            model.nameTypeNote = dr["nameTypeNote"].ToString();


                            typenote.Add(model);
                        }
                        return typenote;
                    }
                    else
                        return typenote;
                }
                else
                    return typenote;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
