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
    public class TrainingBUS
    {
        private TrainingDAO trainingDAO;

        public TrainingBUS()
        {
            trainingDAO = new TrainingDAO();
        }

       
        public List<IModel>GetAllTrainings()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = trainingDAO.GetAllTraining();
                List<IModel> training = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TrainingModel model = new TrainingModel();

                            if (dr["idTraining"].ToString() != "")
                            {
                                model.idTraining = Int32.Parse(dr["idTraining"].ToString());
                            }
                            model.codeTraining = dr["codeTraining"].ToString();
                            model.nameTraining = dr["nameTraining"].ToString();

                            training.Add(model);
                        }
                        return training;
                    }
                    else
                        return training;
                }
                else
                    return training;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TrainingModel> GetAllTraining()
        {
            DataTable dataTable = new DataTable();
            dataTable = trainingDAO.GetAllTraining();
            List<TrainingModel> compList = new List<TrainingModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TrainingModel model = new TrainingModel();

                        if (dr["idTraining"].ToString() != "")
                        {
                            model.idTraining = Int32.Parse(dr["idTraining"].ToString());
                        }
                        model.codeTraining = dr["codeTraining"].ToString();
                        model.nameTraining = dr["nameTraining"].ToString();

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
        
    }
}