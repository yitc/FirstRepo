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
    public class ArrangementRoomsBUS
    {
        private ArrangementRoomsDAO dao;

        public ArrangementRoomsBUS()
        {
            dao = new ArrangementRoomsDAO();
        }

        public Boolean checkIfRoomsAreBooked(int idArrangement, string idArticle, int id)
        {
            Boolean result = false;
            DataTable dataTable = new DataTable();
            dataTable = dao.checkIfRoomsAreBooked(idArrangement, idArticle,  id);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public List<ArrangementRoomsModel> GetAllRoomsForArrangement(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = dao.GetAllRoomsForArrangement(idArrangement);
            List<ArrangementRoomsModel> modellista = new List<ArrangementRoomsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    ArrangementRoomsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementRoomsModel();
                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        model.idRoom = dr["idRoom"].ToString();
                        model.idArticle = dr["idArticle"].ToString();

                        if(dr["isContract"].ToString() != "")
                            model.isContract = Boolean.Parse(dr["isContract"].ToString());

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());


                        modellista.Add(model);
                    }
                    return modellista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public int checkIfRoomAlready(int idArrangement, string idArticle)
        {
            int result = 0;
            DataTable dataTable = new DataTable();
            dataTable = dao.checkIfRoomAlready(idArrangement, idArticle);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    result = 1;
                }
            }

            return result;
        }

        public int getNumberofBookedRooms(int idArrangement, string idArticle)
        {
            int result = 0;
            DataTable dataTable = new DataTable();
            dataTable = dao.getNumberofBookedRooms(idArrangement,  idArticle);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    result = Convert.ToInt32(dataTable.Rows[0]["Room"].ToString());
                }
            }

            return result;
        }

        public Boolean checkifRoomCanBeDeleted(ArrangementArticalModel_RoomsUpdate lista, int idArrangement)
        {
            Boolean result = false;
            DataTable dataTable = new DataTable();
            dataTable = dao.checkifRoomCanBeDeleted(lista, idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool Delete(ArrangementRoomsModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dao.Delete(model,nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteInsertArrangamentRooms(BindingList<ArrangementArticalModel_Rooms> lista, int idArrangement, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dao.DeleteInsertArrangamentRooms(lista, idArrangement, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateArrangamentRooms(List<ArrangementArticalModel_RoomsUpdate> lista, int idArrangement, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dao.UpdateArrangamentRooms(lista, idArrangement, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
