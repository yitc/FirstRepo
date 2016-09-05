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
    public class TripCalculationBUS
    {
        private TripCalculationDAO tripCalculationDAO;

        public TripCalculationBUS()
        {
            tripCalculationDAO = new TripCalculationDAO();
        }

        public bool Save(TripCalculationModel calculation)
        {
            bool retval = false;
            try
            {
                retval = tripCalculationDAO.Save(calculation);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool Update(TripCalculationModel calculation)
        {
            bool retval = false;
            try
            {
                retval = tripCalculationDAO.Update(calculation);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Delete(int id)
        {
            bool retval = false;
            try
            {
                retval = tripCalculationDAO.Detele(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel>GetAllCalculations()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = tripCalculationDAO.GetAllTripCalculation();
                List<IModel> calculation = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TripCalculationModel model = new TripCalculationModel();

                            if (dr["idTripCalculation"].ToString() != "")
                            {
                                model.idTripCalculation = Int32.Parse(dr["idTripCalculation"].ToString());
                            }
                            model.nameTripCalculation = dr["nameTripCalculation"].ToString();
                            model.targetGroup = dr["targetGroup"].ToString();

                            if (dr["dtFrom"].ToString() != "")
                            {
                                model.dtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());
                            }

                            if (dr["dtTo"].ToString() != "")
                            {
                                model.dtTo = Convert.ToDateTime(dr["dtTo"].ToString());
                            }

                            if (dr["op1"].ToString() != "")
                            {
                                model.op1 = Convert.ToDecimal(dr["op1"].ToString());
                            }

                            if (dr["op2"].ToString() != "")
                            {
                                model.op2 = Convert.ToDecimal(dr["op2"].ToString());
                            }

                            if (dr["op3"].ToString() != "")
                            {
                                model.op3 = Convert.ToDecimal(dr["op3"].ToString());
                            }

                            calculation.Add(model);
                        }
                        return calculation;
                    }
                    else
                        return calculation;
                }
                else
                    return calculation;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TripCalculationModel>GetAllTripCalculation()
        {
            DataTable dataTable = new DataTable();
            dataTable = tripCalculationDAO.GetAllTripCalculation();
            List<TripCalculationModel> compList = new List<TripCalculationModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TripCalculationModel model = new TripCalculationModel();

                        if (dr["idTripCalculation"].ToString() != "")
                        {
                            model.idTripCalculation = Int32.Parse(dr["idTripCalculation"].ToString());
                        }
                        model.nameTripCalculation = dr["nameTripCalculation"].ToString();
                        model.targetGroup = dr["targetGroup"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                        {
                            model.dtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());
                        }

                        if (dr["dtTo"].ToString() != "")
                        {
                            model.dtTo = Convert.ToDateTime(dr["dtTo"].ToString());
                        }

                        if (dr["op1"].ToString() != "")
                        {
                            model.op1 = Convert.ToDecimal(dr["op1"].ToString());
                        }

                        if (dr["op2"].ToString() != "")
                        {
                            model.op2 = Convert.ToDecimal(dr["op2"].ToString());
                        }

                        if (dr["op3"].ToString() != "")
                        {
                            model.op3 = Convert.ToDecimal(dr["op3"].ToString());
                        }

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public TripCalculationModel GetAllTripCalculationByID(string idTripCalculation)
        {
            DataTable dataTable = new DataTable();
            dataTable = tripCalculationDAO.GetTripCalculationByID(idTripCalculation);
            TripCalculationModel calculation = new TripCalculationModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    TripCalculationModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new TripCalculationModel();

                        if (dr["idTripCalculation"].ToString() != "")
                        {
                            model.idTripCalculation = Int32.Parse(dr["idTripCalculation"].ToString());
                        }
                        model.nameTripCalculation = dr["nameTripCalculation"].ToString();
                        model.targetGroup = dr["targetGroup"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                        {
                            model.dtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());
                        }

                        if (dr["dtTo"].ToString() != "")
                        {
                            model.dtTo = Convert.ToDateTime(dr["dtTo"].ToString());
                        }

                        if (dr["op1"].ToString() != "")
                        {
                            model.op1 = Convert.ToDecimal(dr["op1"].ToString());
                        }

                        if (dr["op2"].ToString() != "")
                        {
                            model.op2 = Convert.ToDecimal(dr["op2"].ToString());
                        }

                        if (dr["op3"].ToString() != "")
                        {
                            model.op3 = Convert.ToDecimal(dr["op3"].ToString());
                        }
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