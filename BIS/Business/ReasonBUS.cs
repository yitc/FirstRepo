using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
  public class ReasonBUS
    {
        private ReasonDAO tpDAO;
        public ReasonBUS()
        {
            tpDAO = new ReasonDAO();
        }
        public List<IModel> GetAll (string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.GetAllType( language);
            List<IModel> filters = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ReasonModel model = new ReasonModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());

                        model.name = dr["name"].ToString();
                        model.type = dr["type"].ToString();

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public bool UpdateContactPersonReasonOut(int idReasonOut, string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateContactPersonReasonOut(idReasonOut, nameReasonOut,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateContactPersonReasonIn(int idReasonOut, string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateContactPersonReasonIn(idReasonOut, nameReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateVoluntaryReasonIn(int idReasonIn, string nameReasonIn, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateVoluntaryReasonIn(idReasonIn, nameReasonIn, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateVoluntaryReasonOut(int idReasonOut, string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.UpdateVoluntaryReasonOut(idReasonOut, nameReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public bool InsertContactPersonReasonOut(string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertContactPersonReasonOut(nameReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool InsertContactPersonReasonIn(string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertContactPersonReasonIn(nameReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool InsertVoluntaryReasonIn(string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertVoluntaryReasonIn(nameReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool InsertVoluntaryReasonOut(string nameReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.InsertVoluntaryReasonOut(nameReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteContactPersonReasonOut(int idReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteContactPersonReasonOut(idReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool DeleteContactPersonReasonIn(int idReasonIn, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteContactPersonReasonIn(idReasonIn, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVoluntaryReasonIn(int idReasonIn, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteVoluntaryReasonIn(idReasonIn, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVoluntaryReasonOut(int idReasonOut, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tpDAO.DeleteVoluntaryReasonOut(idReasonOut, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<ReasonModel> AllCPReasonOut()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.AllCPReasonOut();
            List<ReasonModel> filters = new List<ReasonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ReasonModel model = new ReasonModel();

                        if (dr["ID"].ToString() != "" && dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());

                      
                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<ReasonModel> AllCPReasonIn()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.AllCPReasonIn();
            List<ReasonModel> filters = new List<ReasonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ReasonModel model = new ReasonModel();

                        if (dr["ID"].ToString() != "" && dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());


                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<ReasonModel> AllVHReasonOut()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.AllVHReasonOut();
            List<ReasonModel> filters = new List<ReasonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ReasonModel model = new ReasonModel();

                        if (dr["ID"].ToString() != "" && dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());


                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<ReasonModel> AllVHReasonIn()
        {
            DataTable dataTable = new DataTable();
            dataTable = tpDAO.AllVHReasonIn();
            List<ReasonModel> filters = new List<ReasonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ReasonModel model = new ReasonModel();

                        if (dr["ID"].ToString() != "" && dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());


                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }
  }

}
