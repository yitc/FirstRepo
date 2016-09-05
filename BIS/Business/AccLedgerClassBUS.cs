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
    public class AccLedgerClassBUS
    {
        private AccLedgerClassDAO classDAO;

        public AccLedgerClassBUS()
        {
            classDAO = new AccLedgerClassDAO();
        }

        public bool Save(AccLedgerClassModel mclass, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = classDAO.Save(mclass, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccLedgerClassModel mclass, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = classDAO.Update(mclass, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetAllClass()
        {
            DataTable dataTable = new DataTable();
            dataTable = classDAO.GetAllClass();
            List<IModel> mclass = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccLedgerClassModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLedgerClassModel();
                        model.idClass = Int32.Parse(dr["idClass"].ToString());
                        model.codeClass = dr["codeClass"].ToString();
                        model.descClass = dr["descClass"].ToString();
                        model.levelClass = Int32.Parse(dr["levelClass"].ToString());
                        model.orderClass = Int32.Parse(dr["orderClass"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        mclass.Add(model);
                    }
                    return mclass;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetCostByLevel(int level)
        {
            DataTable dataTable = new DataTable();
            dataTable = classDAO.GetCostByLevel(level);
            List<IModel> mclass = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccLedgerClassModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLedgerClassModel();
                        model.idClass = Int32.Parse(dr["idClass"].ToString());
                        model.codeClass = dr["codeClass"].ToString();
                        model.descClass = dr["descClass"].ToString();
                        model.levelClass = Int32.Parse(dr["levelClass"].ToString());
                        model.orderClass = Int32.Parse(dr["orderClass"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        mclass.Add(model);
                    }
                    return mclass;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccLedgerClassModel GetClassById(int idClass)
        {
            DataTable dataTable = new DataTable();
            dataTable = classDAO.GetClassById(idClass);
            AccLedgerClassModel mclass = new AccLedgerClassModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccLedgerClassModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLedgerClassModel();
                        model.idClass = Int32.Parse(dr["idClass"].ToString());
                        model.codeClass = dr["codeClass"].ToString();
                        model.descClass = dr["descClass"].ToString();
                        model.levelClass = Int32.Parse(dr["levelClass"].ToString());
                        model.orderClass = Int32.Parse(dr["orderClass"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                       // mclass.Add(model);
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
