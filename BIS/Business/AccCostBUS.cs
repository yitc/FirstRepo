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
    public class AccCostBUS
    {
        private AccCostDAO costDAO;

        public AccCostBUS()
        {
            costDAO = new AccCostDAO();
        }

        public bool Save(AccCostModel cost,string nameForm,int idUser)
        {
            bool retval = false;
            try
            {

                retval = costDAO.Save(cost,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccCostModel cost, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = costDAO.Update(cost, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(string id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = costDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetAllCost()
        {
            DataTable dataTable = new DataTable();
            dataTable = costDAO.GetAllCost();
            List<IModel> cost = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccCostModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCostModel();
                        model.idCost = Int32.Parse(dr["idCost"].ToString());
                        model.codeCost = dr["codeCost"].ToString();
                        model.descCost = dr["descCost"].ToString();
                        if (dr["userCreated"].ToString() != "")
                           model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        cost.Add(model);
                    }
                    return cost;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public AccCostModel GetCostByID(string idCost)
        {
            DataTable dataTable = new DataTable();
            dataTable = costDAO.GetCostByID(idCost);
           AccCostModel cost = new AccCostModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccCostModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCostModel();
                        model.idCost = Int32.Parse(dr["idCost"].ToString());
                        model.codeCost = dr["codeCost"].ToString();
                        model.descCost = dr["descCost"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        //cost.Add(model);
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
