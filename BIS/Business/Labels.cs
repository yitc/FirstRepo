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
    public partial class LabelsBUS
    {

        private LabelDAO labelDAO;

        public LabelsBUS()
        {
            labelDAO = new LabelDAO();
        }


        public List<LabelModel> GetLabels(string language, int idMenu)
        {
            DataTable dataTable = new DataTable();
            dataTable = labelDAO.GetLabels(language, idMenu);

            if (dataTable.Rows.Count > 0)
            {
                List<LabelModel> labels = new List<LabelModel>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    LabelModel model = new LabelModel();

                    model.idLabel = Int32.Parse(dr["idLabel"].ToString());
                    model.nameLabel = dr["nameLabel"].ToString();

                    labels.Add(model);
                }

                return labels;

            }
            else
            {
                return null;
            }
        }

        public List<LabelModel> GetDistinctLabels()
        {
            DataTable dataTable = new DataTable();
            dataTable = labelDAO.GetDistinctLabels();

            if (dataTable.Rows.Count > 0)
            {
                List<LabelModel> labels = new List<LabelModel>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    LabelModel model = new LabelModel();

                    model.idLabel = Int32.Parse(dr["id"].ToString());
                    model.nameLabel = dr["nameLabel"].ToString();

                    labels.Add(model);
                }

                return labels;

            }
            else
            {
                return null;
            }
        }

        public LabelModel GetLabelById(int label)
        {
            DataTable dataTable = new DataTable();
            dataTable = labelDAO.GetLabelById(label);

            if (dataTable.Rows.Count > 0)
            {
                LabelModel labels = new LabelModel();

                foreach (DataRow dr in dataTable.Rows)
                {
                    //LabelModel labels = new LabelModel();

                    labels.idLabel = Int32.Parse(dr["idLabel"].ToString());
                    labels.nameLabel = dr["nameLabel"].ToString();

                  //  labels.Add(model);
                }

                return labels;

            }
            else
            {
                return null;
            }
        }
    }
}

