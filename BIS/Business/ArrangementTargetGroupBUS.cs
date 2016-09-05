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
    public class ArrangementTargetGroupBUS
    {
        private ArrangementTargetGroupDAO arrangeDAO;

        public ArrangementTargetGroupBUS()
        {
            arrangeDAO = new ArrangementTargetGroupDAO();
        }


        
        public List<TargetGroupModel> GetArrangementTargetGroup(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementTargetGroup(idArrangement);
            List<TargetGroupModel> list = new List<TargetGroupModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TargetGroupModel  model = new TargetGroupModel();

                        if (dr["idTargetGroup"].ToString() != "")
                        model.idTargetGroup = Int32.Parse(dr["idTargetGroup"].ToString());

                        if (dr["nameTargetGroup"].ToString() != "")
                            model.nameTargetGroup = dr["nameTargetGroup"].ToString();

                        model.shortcutTargeGroup = dr["shortcutTargeGroup"].ToString();

                        list.Add(model);
                                                                       
                    }
                    return list;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public string GetTargetGroupName(int idTargetGroup)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetTargetGroupName(idTargetGroup);
            string name = "";

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TargetGroupModel model = new TargetGroupModel();

                        if (dr["shortcutTargeGroup"].ToString() != "")
                            name = dr["shortcutTargeGroup"].ToString();

                        break;

                    }

                }
            }

            return name;
        }

        public List<TargetGroupModel> GetAllTargetGroup()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllTargetGroup();
            List<TargetGroupModel> list = new List<TargetGroupModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TargetGroupModel model = new TargetGroupModel();

                        if (dr["idTargetGroup"].ToString() != "")
                            model.idTargetGroup = Int32.Parse(dr["idTargetGroup"].ToString());

                        if (dr["nameTargetGroup"].ToString() != "")
                            model.nameTargetGroup = dr["nameTargetGroup"].ToString();

                        model.shortcutTargeGroup = dr["shortcutTargeGroup"].ToString();

                        list.Add(model);

                    }
                    return list;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public Boolean Save(ArrangementTargetGroupModel model, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = arrangeDAO.Save(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeDAO.Delete(id,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
