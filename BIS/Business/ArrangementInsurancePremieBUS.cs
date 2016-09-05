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
    public class ArrangementInsurancePremieBUS
    {
        private ArrangementInsurancePremieDAO adao;

        public ArrangementInsurancePremieBUS()
        {
            adao = new ArrangementInsurancePremieDAO();
        }

        public List<IModel> GetAllArrangementInsurancePremie()
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetAllArrangementInsurancePremie();
            List<IModel> lista = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArrangementInsurancePremieModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInsurancePremieModel();

                        if (dr["idPremie"].ToString() != "")
                            model.idPremie = Int32.Parse(dr["idPremie"].ToString());

                        model.premie = dr["premie"].ToString();
                        model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["amountPremie"].ToString() != "")
                            model.amountPremie = Decimal.Parse(dr["amountPremie"].ToString());

                        if (dr["dtValidFrom"].ToString() != "")
                            model.dtValidFrom = DateTime.Parse(dr["dtValidFrom"].ToString());

                        if (dr["dtValidTo"].ToString() != "")
                            model.dtValidTo = DateTime.Parse(dr["dtValidTo"].ToString());


                        lista.Add(model);
                    }
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public ArrangementInsurancePremieModel GetArrangementInsurancePremie(string premie, string code, DateTime dateFrom)
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetArrangementInsurancePremie(premie, code, dateFrom);
            ArrangementInsurancePremieModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInsurancePremieModel();

                        if (dr["idPremie"].ToString() != "")
                            model.idPremie = Int32.Parse(dr["idPremie"].ToString());

                        model.premie = dr["premie"].ToString();
                        model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["amountPremie"].ToString() != "")
                            model.amountPremie = Decimal.Parse(dr["amountPremie"].ToString());

                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public bool Save(ArrangementInsurancePremieModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = adao.Save(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ArrangementInsurancePremieModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = adao.Update(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

       
        public int checkIsInArrangemnet( string premie)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = adao.checkIsInArrangemnet(premie);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public bool DeleteArrangementInsuranceSript(int premie, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = adao.DeleteArrangementInsuranceSript(premie,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
