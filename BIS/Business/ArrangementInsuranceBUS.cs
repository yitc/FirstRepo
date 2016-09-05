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
    public class ArrangementInsuranceBUS
    {
        private ArrangementInsuranceDAO adao;

        public ArrangementInsuranceBUS()
        {
            adao = new ArrangementInsuranceDAO();
        }

        public List<IModel> GetAllArrangementInsurance()
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetAllArrangementInsurance();
            List<IModel> lista = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {                 
                    ArrangementInsuranceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInsuranceModel();

                        if (dr["idInsurance"].ToString() !=  "")
                            model.idInsurance = Int32.Parse(dr["idInsurance"].ToString());

                        model.labelInsurance = dr["labelInsurance"].ToString();
                        model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["amountInsurance"].ToString() != "")
                            model.amountInsurance = Decimal.Parse(dr["amountInsurance"].ToString());

                        //Neta changes on date
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

        public ArrangementTravelInsuranceModel GetArrangementTravelInsurance(string codeInsurance, Boolean isSport)
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetArrangementTravelInsurance(codeInsurance, isSport);
            ArrangementTravelInsuranceModel model = new ArrangementTravelInsuranceModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementTravelInsuranceModel();

                        if (dr["idArrangementTravelInsurance"].ToString() != "")
                            model.idArrangementTravelInsurance = Int32.Parse(dr["idArrangementTravelInsurance"].ToString());

                        if (dr["description"].ToString() != "")
                            model.description = dr["description"].ToString();

                        if (dr["ledgerAccount"].ToString() != "")
                            model.ledgerAccount = dr["ledgerAccount"].ToString();

                        if (dr["codeInsurance"].ToString() != "")
                            model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["isMedicalDevices"].ToString() != "")
                            model.isMedicalDevices = Boolean.Parse(dr["isMedicalDevices"].ToString());

                        if (dr["isSportActivity"].ToString() != "")
                            model.isSportActivity = Boolean.Parse(dr["isSportActivity"].ToString());

                        break;
                    }
                }
            }
           return model;
        }

        public ArrangementTravelInsuranceModel GetArrangementTravelInsuranceWithMedical(string codeInsurance, Boolean isSport, Boolean isMedicalDevices)
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetArrangementTravelInsuranceWithMedical(codeInsurance, isSport, isMedicalDevices);
            ArrangementTravelInsuranceModel model = new ArrangementTravelInsuranceModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementTravelInsuranceModel();

                        if (dr["idArrangementTravelInsurance"].ToString() != "")
                            model.idArrangementTravelInsurance = Int32.Parse(dr["idArrangementTravelInsurance"].ToString());

                        if (dr["description"].ToString() != "")
                            model.description = dr["description"].ToString();

                        if (dr["ledgerAccount"].ToString() != "")
                            model.ledgerAccount = dr["ledgerAccount"].ToString();

                        if (dr["codeInsurance"].ToString() != "")
                            model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["isMedicalDevices"].ToString() != "")
                            model.isMedicalDevices = Boolean.Parse(dr["isMedicalDevices"].ToString());

                        if (dr["isSportActivity"].ToString() != "")
                            model.isSportActivity = Boolean.Parse(dr["isSportActivity"].ToString());

                        break;
                    }
                }
            }
            return model;
        }

        public ArrangementInsuranceModel GetArrangementInsurance(int idInsurance)
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetArrangementInsurance(idInsurance);
            ArrangementInsuranceModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInsuranceModel();

                        if (dr["idInsurance"].ToString() != "")
                            model.idInsurance = Int32.Parse(dr["idInsurance"].ToString());

                        model.labelInsurance = dr["labelInsurance"].ToString();
                        model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["amountInsurance"].ToString() != "")
                            model.amountInsurance = Decimal.Parse(dr["amountInsurance"].ToString());

                        //Neta changes on date
                        if (dr["dtValidFrom"].ToString() != "")
                            model.dtValidFrom = DateTime.Parse(dr["dtValidFrom"].ToString());

                        if (dr["dtValidTo"].ToString() != "")
                            model.dtValidTo = DateTime.Parse(dr["dtValidTo"].ToString());
                        
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public ArrangementInsuranceModel GetArrangementInsuranceWithCountry(string nameLab, string codeins, DateTime dateFrom)
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetArrangementInsuranceWithCountry(nameLab, codeins,  dateFrom);
            ArrangementInsuranceModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInsuranceModel();

                        if (dr["idInsurance"].ToString() != "")
                            model.idInsurance = Int32.Parse(dr["idInsurance"].ToString());

                        model.labelInsurance = dr["labelInsurance"].ToString();
                        model.codeInsurance = dr["codeInsurance"].ToString();

                        if (dr["amountInsurance"].ToString() != "")
                            model.amountInsurance = Decimal.Parse(dr["amountInsurance"].ToString());

                        //Neta changes on date
                        if (dr["dtValidFrom"].ToString() != "")
                            model.dtValidFrom = DateTime.Parse(dr["dtValidFrom"].ToString());

                        if (dr["dtValidTo"].ToString() != "")
                            model.dtValidTo = DateTime.Parse(dr["dtValidTo"].ToString());

                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ArrangementInsuranceLabelModel> GetUniqueLabelNames()
        {
            DataTable dataTable = new DataTable();
            dataTable = adao.GetUniqueLabelNames();
            List<ArrangementInsuranceLabelModel> lista = new List<ArrangementInsuranceLabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArrangementInsuranceLabelModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInsuranceLabelModel();

                        model.Name = dr["nameLabel"].ToString();                        

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

        public bool Save(ArrangementInsuranceModel model, string nameForm, int idUser)
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

        public bool Update(ArrangementInsuranceModel model, string nameForm, int idUser)
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
        // Brisanje novo
        public int checkIsInArrangemnet(string idLabel, string provision)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = adao.checkIsInArrangemnet( idLabel, provision);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public bool DeleteArrangementInsuranceSript(int idInsurance, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = adao.DeleteArrangementInsuranceSript(idInsurance, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
     

    }
}
