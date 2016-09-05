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
    public class LayoutBUS
    {
        private LayoutDAO layoutDAO;

        public LayoutBUS()
        {
            layoutDAO = new LayoutDAO();
        }

        public List<LayoutModel> GetAllLayouts()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = layoutDAO.GetAllLayouts();
                List<LayoutModel> layout = new List<LayoutModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            LayoutModel model = new LayoutModel();

                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                            model.nameLayout = dr["nameLayout"].ToString();
                            model.typeDocument = dr["typeDocument"].ToString();
                            model.languageLayout = dr["languageLayout"].ToString();
                            model.bookmark = dr["bookmark"].ToString();
                            model.fileLayout = dr["fileLayout"].ToString();
                          
                            if (dr["dtCreted"].ToString() != "")
                                model.dtCreted = DateTime.Parse(dr["dtCreted"].ToString());
                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["userModified"].ToString() != "")
                                model.userModified = Int32.Parse(dr["userModified"].ToString());
                            if (dr["userCreated"].ToString() != "")
                                model.userCreated = Int32.Parse(dr["userCreated"].ToString());


                            layout.Add(model);
                        }
                        return layout;
                    }
                    else
                        return layout;
                }
                else
                    return layout;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
