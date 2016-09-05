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
    public class LabelBUS
    {
        private LabelDAO labelDAO;

        public LabelBUS()
        {
            labelDAO = new LabelDAO();
        }

        public bool Save(LabelModel labels, int IdLabel)
        {
            bool retval = false;
            try
            {

                retval = labelDAO.Save(labels, IdLabel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(LabelModel labels, int iID)
        {
            bool retval = false;
            try
            {

                retval = labelDAO.Save(labels, iID);

            }
            catch (Exception ex)
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

                retval = labelDAO.Delete(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<LabelModel> GetAllLabels(string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = labelDAO.GetAllLabels(lang);
            List<LabelModel> labels = new List<LabelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    LabelModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new ToDoModel();
                        model = new LabelModel();
                        if (dr["idLabel"].ToString() != "")
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());

                        model.nameLabel = dr["nameLabel"].ToString();

                        labels.Add(model);
                    }
                    return labels;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }

}