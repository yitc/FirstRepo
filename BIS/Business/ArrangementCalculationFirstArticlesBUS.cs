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
    public class ArrangementCalculationFirstArticlesBUS
    {
        private ArrangementCalculationFirstArticlesDAO dao;

        public ArrangementCalculationFirstArticlesBUS()
        {
            dao = new ArrangementCalculationFirstArticlesDAO();
        }

        public bool SaveLabelsFirst(List<LabelForArrangement> label, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dao.insertArrangementLabelsFirst(label,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public List<LabelForArrangement> GetLabelsFirst(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = dao.GetLabelsFirst(idArrangement);
            List<LabelForArrangement> lista = new List<LabelForArrangement>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    LabelForArrangement model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new LabelForArrangement();

                        if (dr["idLabel"].ToString() != "")
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());


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

        public ArrangementCalculationFirstNotArticlesModel GetNotArticles(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = dao.GetNotArticles( idArrangement);
            ArrangementCalculationFirstNotArticlesModel model = new ArrangementCalculationFirstNotArticlesModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["nrOfNights"].ToString() != "")
                            model.nrOfNights = Int32.Parse(dr["nrOfNights"].ToString());

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idCountry"].ToString() != "")
                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());

                        if (dr["idUserFinished"].ToString() != "")
                            model.idUserFinished = Int32.Parse(dr["idUserFinished"].ToString());

                        if (dr["dtUserFinished"].ToString() != "")
                            model.dtUserFinished = DateTime.Parse(dr["dtUserFinished"].ToString());

                        //if (dr["buSupportingArms"].ToString() != "")
                        //    model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        //if (dr["nrAnchorage"].ToString() != "")
                        //    model.nrAnchorage = Int32.Parse(dr["nrAnchorage"].ToString());

                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public bool SaveFirst(int idArrangement, List<IModel> dtPrice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dao.SaveFirst(idArrangement, dtPrice,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public Boolean SaveNotArticles(ArrangementCalculationFirstNotArticlesModel model, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = dao.SaveNotArticles(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



    }
}
