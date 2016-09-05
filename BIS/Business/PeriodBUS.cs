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
    public class PeriodBUS
    {
        private PeriodDAO periodDAO;
        public PeriodBUS()
        {
            periodDAO = new PeriodDAO();
        }

        public List<IModel> GetAllPeriod()
        {
            DataTable dataTable = new DataTable();
            dataTable = periodDAO.GetAllPeriod();

            List<IModel> periodModel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PeriodModel model = new PeriodModel();

                        model.idPeriod = Int32.Parse(dr["idPeriod"].ToString());

                        if (dr["monthFrom"].ToString() != "")
                        {
                            model.monthFrom = Int32.Parse(dr["monthFrom"].ToString());
                        }

                        if (dr["monthTo"].ToString() != "")
                        {
                            model.monthTo = Int32.Parse(dr["monthTo"].ToString());
                        }
                        model.descPeriod = dr["descPeriod"].ToString();

                        periodModel.Add(model);

                    }
                    return periodModel;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public bool Save(int idPeriod, int monthFrom, int monthTo, string descPeriod, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = periodDAO.Save(idPeriod, monthFrom, monthTo, descPeriod, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(int idPeriod, int monthFrom, int monthTo, string descPeriod, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = periodDAO.Update(idPeriod, monthFrom, monthTo, descPeriod, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<PeriodModel> LastId()
        {
            DataTable dataTable = new DataTable();
            dataTable = periodDAO.LastId();

            List<PeriodModel> periodModel = new List<PeriodModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PeriodModel model = new PeriodModel();

                        model.idPeriod = Int32.Parse(dr["idPeriod"].ToString());

                        periodModel.Add(model);

                    }
                    return periodModel;
                }
                else
                    return null;
            }
            else
                return null;
        }
        // NOVO DELETE
        public bool DeletePeriodSript(int idPeriod, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = periodDAO.DeletePriodScript(idPeriod, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
