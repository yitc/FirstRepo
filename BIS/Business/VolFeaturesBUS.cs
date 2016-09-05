using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;

namespace BIS.Business
{
    public class VolFeaturesBUS
    {
        private VolFeaturesDAO volFeaturesDAO;

        public VolFeaturesBUS()
        {
            volFeaturesDAO = new VolFeaturesDAO();
        }

        public bool Save(VolFeaturesModel volFeatures, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = volFeaturesDAO.Save(volFeatures,nameForm,idUser);
            }
            catch(Exception ex)
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
                retval = volFeaturesDAO.Delete(id,nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel>GetAllVolFeatures()
        {
            DataTable dataTable = new DataTable();
            dataTable = volFeaturesDAO.GetAllVolFeatures();

            List<IModel> volFeaturesModel = new List<IModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolFeaturesModel model = new VolFeaturesModel();

                        model.idFeatures = Int32.Parse(dr["idFeatures"].ToString());

                        if (dr["idContPers"].ToString() != "")
                        {
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.codeTraining = dr["@codeTraining"].ToString();

                        if (dr["expireDate"].ToString() != "")
                        {
                            model.expireDate = Convert.ToDateTime(dr["expireDate"].ToString());
                        }

                        if (dr["archiveDate"].ToString() != "")
                        {
                            model.archiveDate = Convert.ToDateTime(dr["archiveDate"].ToString());
                        }

                        if (dr["scheduleDate"].ToString() != "")
                        {
                            model.scheduleDate = Convert.ToDateTime(dr["scheduleDate"].ToString());
                        }

                        volFeaturesModel.Add(model);
                    }
                    return volFeaturesModel;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<VolFeaturesModel>GetAllFeaturesFromPersonGrid(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = volFeaturesDAO.GetAllFeaturesFromPersonGrid(idContPers);
            List<VolFeaturesModel> volFeaturesList = new List<VolFeaturesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VolFeaturesModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolFeaturesModel();

                        model.idFeatures = Int32.Parse(dr["idFeatures"].ToString());

                        if (dr["idContPers"].ToString() != "")
                        {
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.codeTraining = dr["codeTraining"].ToString();

                        if (dr["expireDate"].ToString() != "")
                        {
                            model.expireDate = Convert.ToDateTime(dr["expireDate"].ToString());
                        }

                        if (dr["archiveDate"].ToString() != "")
                        {
                            model.archiveDate = Convert.ToDateTime(dr["archiveDate"].ToString());
                        }

                        if (dr["scheduleDate"].ToString() != "")
                        {
                            model.scheduleDate = Convert.ToDateTime(dr["scheduleDate"].ToString());
                        }

                        model.nameCertificate = dr["nameCertificate"].ToString();
                        model.nameTraining = dr["nameTraining"].ToString();

                        volFeaturesList.Add(model);
                    }
                    return volFeaturesList;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<VolFeaturesModel> GetAllFeature()
        {
            DataTable dataTable = new DataTable();
            dataTable = volFeaturesDAO.GetAllVolFeatures();
            List<VolFeaturesModel> compList = new List<VolFeaturesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolFeaturesModel model = new VolFeaturesModel();

                        model.idFeatures = Int32.Parse(dr["idFeatures"].ToString());

                        if (dr["idContPers"].ToString() != "")
                        {
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.codeTraining = dr["codeTraining"].ToString();

                        if (dr["expireDate"].ToString() != "")
                        {
                            model.expireDate = Convert.ToDateTime(dr["expireDate"].ToString());
                        }

                        if (dr["archiveDate"].ToString() != "")
                        {
                            model.archiveDate = Convert.ToDateTime(dr["archiveDate"].ToString());
                        }

                        if (dr["scheduleDate"].ToString() != "")
                        {
                            model.scheduleDate = Convert.ToDateTime(dr["scheduleDate"].ToString());
                        }

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

        public VolFeaturesModel GetVolFeaturesByID(string idFeatures)
        {
            DataTable dataTable = new DataTable();
            dataTable = volFeaturesDAO.GetVolFeaturesByID(idFeatures);
            VolFeaturesModel volFeatures = new VolFeaturesModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VolFeaturesModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolFeaturesModel();

                        model.idFeatures = Int32.Parse(dr["idFeatures"].ToString());

                        if (dr["idContPers"].ToString() != "")
                        {
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.codeTraining = dr["codeTraining"].ToString();

                        if (dr["expireDate"].ToString() != "")
                        {
                            model.expireDate = Convert.ToDateTime(dr["expireDate"].ToString());
                        }

                        if (dr["archiveDate"].ToString() != "")
                        {
                            model.archiveDate = Convert.ToDateTime(dr["archiveDate"].ToString());
                        }

                        if (dr["scheduleDate"].ToString() != "")
                        {
                            model.scheduleDate = Convert.ToDateTime(dr["scheduleDate"].ToString());
                        }
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<VolFeaturesModel> GetGetVolFeaturesByCodeCertificate(string certificate)
        {
            DataTable dataTable = new DataTable();
            dataTable = volFeaturesDAO.GetVolFeaturesByCodeCertificate(certificate);
            List<VolFeaturesModel> compList = new List<VolFeaturesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolFeaturesModel model = new VolFeaturesModel();

                        model.idFeatures = Int32.Parse(dr["idFeatures"].ToString());

                        if (dr["idContPers"].ToString() != "")
                        {
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.codeTraining = dr["codeTraining"].ToString();

                        if (dr["expireDate"].ToString() != "")
                        {
                            model.expireDate = Convert.ToDateTime(dr["expireDate"].ToString());
                        }

                        if (dr["archiveDate"].ToString() != "")
                        {
                            model.archiveDate = Convert.ToDateTime(dr["archiveDate"].ToString());
                        }

                        if (dr["scheduleDate"].ToString() != "")
                        {
                            model.scheduleDate = Convert.ToDateTime(dr["scheduleDate"].ToString());
                        }

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

        public List<VolFeaturesModel>GetVolFeaturesByTraining(string training)
        {
            DataTable dataTable = new DataTable();
            dataTable = volFeaturesDAO.GetVolFeaturesByTraining(training);
            List<VolFeaturesModel> compList = new List<VolFeaturesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolFeaturesModel model = new VolFeaturesModel();

                        model.idFeatures = Int32.Parse(dr["idFeatures"].ToString());

                        if (dr["idContPers"].ToString() != "")
                        {
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.codeTraining = dr["codeTraining"].ToString();

                        if (dr["expireDate"].ToString() != "")
                        {
                            model.expireDate = Convert.ToDateTime(dr["expireDate"].ToString());
                        }

                        if (dr["archiveDate"].ToString() != "")
                        {
                            model.archiveDate = Convert.ToDateTime(dr["archiveDate"].ToString());
                        }

                        if (dr["scheduleDate"].ToString() != "")
                        {
                            model.scheduleDate = Convert.ToDateTime(dr["scheduleDate"].ToString());
                        }

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