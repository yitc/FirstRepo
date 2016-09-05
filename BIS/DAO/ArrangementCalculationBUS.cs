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
    public class ArrangementCalculationBUS
    {
        private ArrangementCalculationDAO arrangeDAO;

        public ArrangementCalculationBUS()
        {
            arrangeDAO = new ArrangementCalculationDAO();
        }

        public ArrangementCalculationModel GetArrangementCalculation(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementCalculation(idArrangement);
            ArrangementCalculationModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementCalculationModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["minNumberTravelers"].ToString() != "")
                            model.minNumberTravelers = Int32.Parse(dr["minNumberTravelers"].ToString());

                        if (dr["provision"].ToString() != "")
                            model.provision = Decimal.Parse(dr["provision"].ToString());

                        if (dr["calamiteitenFonds"].ToString() != "")
                            model.calamiteitenFonds = Decimal.Parse(dr["calamiteitenFonds"].ToString());

                        if (dr["correction"].ToString() != "")
                            model.correction = Decimal.Parse(dr["correction"].ToString());

                        if (dr["travelInsurace"].ToString() != "")
                            model.travelInsurace = Decimal.Parse(dr["travelInsurace"].ToString());

                        if (dr["travelInsurance2"].ToString() != "")
                            model.travelInsurance2 = Decimal.Parse(dr["travelInsurance2"].ToString());

                        if (dr["polisCosts"].ToString() != "")
                            model.polisCosts = Decimal.Parse(dr["polisCosts"].ToString());

                        if (dr["price"].ToString() != "")
                            model.price = Decimal.Parse(dr["price"].ToString());

                        if (dr["moneyGroup"].ToString() != "")
                            model.moneyGroup = Decimal.Parse(dr["moneyGroup"].ToString());

                        if (dr["insuranceVolontary"].ToString() != "")
                            model.insuranceVolontary = Decimal.Parse(dr["insuranceVolontary"].ToString());

                        if (dr["singleRoomPrice"].ToString() != "")
                            model.singleRoomPrice = Decimal.Parse(dr["singleRoomPrice"].ToString());

                        if (dr["discount"].ToString() != "")
                            model.discount = Decimal.Parse(dr["discount"].ToString());

                        if (dr["txt"].ToString() != "")
                            model.txt = dr["txt"].ToString();

                        if (dr["txtAmount"].ToString() != "")
                            model.txtAmount = Decimal.Parse(dr["txtAmount"].ToString());

                        if (dr["numberLeader"].ToString() != "")
                            model.numberLeader = Int32.Parse(dr["numberLeader"].ToString());
                        if (dr["premie1"].ToString() != "")
                            model.premie1 = Decimal.Parse(dr["premie1"].ToString());
                        if (dr["premie2"].ToString() != "")
                            model.premie2 = Decimal.Parse(dr["premie2"].ToString());
                        if (dr["numberCO"].ToString() != "")
                            model.numberCO = Int32.Parse(dr["numberCO"].ToString());
                        if (dr["minNumberTravelers"].ToString() != "")
                            model.minNumberTravelers = Int32.Parse(dr["minNumberTravelers"].ToString());
                        if (dr["volontaryDays"].ToString() != "")
                            model.volontaryDays = Int32.Parse(dr["volontaryDays"].ToString());
                        if (dr["isSport"].ToString() != "")
                            model.isSport = Boolean.Parse(dr["isSport"].ToString());

                        if (dr["nrVoluntary"].ToString() != "")
                            model.nrVoluntary = Int32.Parse(dr["nrVoluntary"].ToString());

                       

                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public ArrangementCalculationSecondModel GetArrangementCalculationSecond(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementCalculationSecond(idArrangement);
            ArrangementCalculationSecondModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementCalculationSecondModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["minNumberTravelers"].ToString() != "")
                            model.minNumberTravelers = Int32.Parse(dr["minNumberTravelers"].ToString());

                        if (dr["provision"].ToString() != "")
                            model.provision = Decimal.Parse(dr["provision"].ToString());

                        if (dr["calamiteitenFonds"].ToString() != "")
                            model.calamiteitenFonds = Decimal.Parse(dr["calamiteitenFonds"].ToString());

                        if (dr["correction"].ToString() != "")
                            model.correction = Decimal.Parse(dr["correction"].ToString());

                        if (dr["travelInsurace"].ToString() != "")
                            model.travelInsurace = Decimal.Parse(dr["travelInsurace"].ToString());

                        if (dr["travelInsurance2"].ToString() != "")
                            model.travelInsurance2 = Decimal.Parse(dr["travelInsurance2"].ToString());

                        if (dr["polisCosts"].ToString() != "")
                            model.polisCosts = Decimal.Parse(dr["polisCosts"].ToString());

                        if (dr["price"].ToString() != "")
                            model.price = Decimal.Parse(dr["price"].ToString());

                        if (dr["moneyGroup"].ToString() != "")
                            model.moneyGroup = Decimal.Parse(dr["moneyGroup"].ToString());

                        if (dr["insuranceVolontary"].ToString() != "")
                            model.insuranceVolontary = Decimal.Parse(dr["insuranceVolontary"].ToString());

                        if (dr["singleRoomPrice"].ToString() != "")
                            model.singleRoomPrice = Decimal.Parse(dr["singleRoomPrice"].ToString());

                        if (dr["discount"].ToString() != "")
                            model.discount = Decimal.Parse(dr["discount"].ToString());

                        if (dr["txt"].ToString() != "")
                            model.txt = dr["txt"].ToString();

                        if (dr["txtAmount"].ToString() != "")
                            model.txtAmount = Decimal.Parse(dr["txtAmount"].ToString());

                        if (dr["numberLeader"].ToString() != "")
                            model.numberLeader = Int32.Parse(dr["numberLeader"].ToString());
                        if (dr["premie1"].ToString() != "")
                            model.premie1 = Decimal.Parse(dr["premie1"].ToString());
                        if (dr["premie2"].ToString() != "")
                            model.premie2 = Decimal.Parse(dr["premie2"].ToString());
                        if (dr["numberCO"].ToString() != "")
                            model.numberCO = Int32.Parse(dr["numberCO"].ToString());
                        if (dr["minNumberTravelers"].ToString() != "")
                            model.minNumberTravelers = Int32.Parse(dr["minNumberTravelers"].ToString());
                        if (dr["volontaryDays"].ToString() != "")
                            model.volontaryDays = Int32.Parse(dr["volontaryDays"].ToString());
                        if (dr["isSport"].ToString() != "")
                            model.isSport = Boolean.Parse(dr["isSport"].ToString());
                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());
                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());
                        if (dr["nrVoluntary"].ToString() != "")
                            model.nrVoluntary = Int32.Parse(dr["nrVoluntary"].ToString());


                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public Boolean isCalculationFinished(int idArrangement)
        {
            Boolean res = false;
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.isCalculationFinished(idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["isFinished"].ToString() != "")
                            res = Boolean.Parse(dr["isFinished"].ToString());
                    }

                }
            }
            return res;
        }

        public Boolean Save(ArrangementCalculationModel arrangemodel, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = arrangeDAO.Save(arrangemodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public Boolean SaveSecond(ArrangementCalculationSecondModel arrangemodel, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = arrangeDAO.SaveSecond(arrangemodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public Boolean SaveFinished(int idArrangement, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = arrangeDAO.SaveFinished(idArrangement, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
