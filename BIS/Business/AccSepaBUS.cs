using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System.Xml;


namespace BIS.Business
{
    public class AccSepaBUS
    {
        private AccSepaDAO sepaDAO;

        public AccSepaBUS()
        {
            sepaDAO = new AccSepaDAO();
        }

        public int GetLastID()
        {
            return sepaDAO.GetLastID();
        }
        public bool Save(AccSepaModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = sepaDAO.Save(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int SaveAndReturnID(AccSepaModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = sepaDAO.SaveAndReturnID(model, nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccSepaModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = sepaDAO.Update(model, nameForm, idUser);

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

                retval = sepaDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public AccSepaModel GetSepaById(int idSepa)
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetSepaById(idSepa);
            AccSepaModel model = new AccSepaModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                  //  AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSepaModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());
                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();
                      
                       // linesmodel.Add(model);
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccSepaModel> GetAllSepa()
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetAllSepa();
            List<AccSepaModel> linesmodel = new List<AccSepaModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSepaModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());
                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccSepaModel> GetAllSepaProgress()
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetAllSepaProgress();
            List<AccSepaModel> linesmodel = new List<AccSepaModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSepaModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());

                        if (dr["dtCreationDate"].ToString() != "")
                            model.dtCreationDate = DateTime.Parse(dr["dtCreationDate"].ToString());
                        if (dr["approveUser"].ToString() != "")
                            model.approveUser = Int32.Parse(dr["approveUser"].ToString());
                        if (dr["dtApprove"].ToString() != "")
                            model.dtApprove = DateTime.Parse(dr["dtApprove"].ToString());

                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccSepaModel> GetAllSepaFinal()
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetAllSepaFinal();
            List<AccSepaModel> linesmodel = new List<AccSepaModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSepaModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());

                        if (dr["dtCreationDate"].ToString() != "")
                            model.dtCreationDate = DateTime.Parse(dr["dtCreationDate"].ToString());
                        if (dr["approveUser"].ToString() != "")
                            model.approveUser = Int32.Parse(dr["approveUser"].ToString());
                        if (dr["dtApprove"].ToString() != "")
                            model.dtApprove = DateTime.Parse(dr["dtApprove"].ToString());

                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccSepaModel> GetAllSepaXml()
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetAllSepaXml();
            List<AccSepaModel> linesmodel = new List<AccSepaModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSepaModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());

                        if (dr["dtCreationDate"].ToString() != "")
                            model.dtCreationDate = DateTime.Parse(dr["dtCreationDate"].ToString());
                        if (dr["approveUser"].ToString() != "")
                            model.approveUser = Int32.Parse(dr["approveUser"].ToString());
                        if (dr["dtApprove"].ToString() != "")
                            model.dtApprove = DateTime.Parse(dr["dtApprove"].ToString());

                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccSepaModel> GetAllSepaHistory()
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetAllSepaHistory();
            List<AccSepaModel> linesmodel = new List<AccSepaModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSepaModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());

                        if (dr["dtCreationDate"].ToString() != "")
                            model.dtCreationDate = DateTime.Parse(dr["dtCreationDate"].ToString());
                        if (dr["approveUser"].ToString() != "")
                            model.approveUser = Int32.Parse(dr["approveUser"].ToString());
                        if (dr["dtApprove"].ToString() != "")
                            model.dtApprove = DateTime.Parse(dr["dtApprove"].ToString());

                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccSepaModel GetLastSepa()
        {
            DataTable dataTable = new DataTable();
            dataTable = sepaDAO.GetLastSepa();
            AccSepaModel model = new AccSepaModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    // AccDailyBankModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new AccDailyBankModel();

                        model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["nameSepa"].ToString() != "")
                            model.nameSepa = dr["nameSepa"].ToString();
                        if (dr["dtSepa"].ToString() != "")
                            model.dtSepa = DateTime.Parse(dr["dtSepa"].ToString());
                        if (dr["amountSepa"].ToString() != "")
                            model.amountSepa = Decimal.Parse(dr["amountSepa"].ToString());
                        if (dr["status"].ToString() != "")
                            model.status = Int32.Parse(dr["status"].ToString());

                        if (dr["dtCreationDate"].ToString() != "")
                            model.dtCreationDate = DateTime.Parse(dr["dtCreationDate"].ToString());
                        if (dr["approveUser"].ToString() != "")
                            model.approveUser = Int32.Parse(dr["approveUser"].ToString());
                        if (dr["dtApprove"].ToString() != "")
                            model.dtApprove = DateTime.Parse(dr["dtApprove"].ToString());

                        if (dr["sepaFInal"].ToString() != "")
                            model.sepaFInal = dr["sepaFInal"].ToString();

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