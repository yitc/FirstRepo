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
    public class CertificatesBUS
    {
        private CertificatesDAO certificatesDAO;

        public CertificatesBUS()
        {
            certificatesDAO = new CertificatesDAO();
        }

       
        public List<IModel> GetAllCertificates()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = certificatesDAO.GetAllCertificates();
                List<IModel> certificates = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            CertificatesModel model = new CertificatesModel();

                            if (dr["idCertificate"].ToString() != "")
                            {
                                model.idCertificate = Int32.Parse(dr["idCertificate"].ToString());
                            }

                            model.codeCertificate = dr["codeCertificate"].ToString();
                            model.nameCertificate = dr["nameCertificate"].ToString();

                            certificates.Add(model);
                        }
                        return certificates;
                    }
                    else
                        return certificates;
                }
                else
                    return certificates;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CertificatesModel> GetAllCertificate()
        {
            DataTable dataTable = new DataTable();
            dataTable = certificatesDAO.GetAllCertificates();
            List<CertificatesModel> compList = new List<CertificatesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        CertificatesModel model = new CertificatesModel();

                        if (dr["idCertificate"].ToString() != "")
                        {
                            model.idCertificate = Int32.Parse(dr["idCertificate"].ToString());
                        }
                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.nameCertificate = dr["nameCertificate"].ToString();

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

        public CertificatesModel GetCertificatesByID(string idCertificate)
        {
            DataTable dataTable = new DataTable();
            dataTable = certificatesDAO.GetCertificatesByID(idCertificate);
            CertificatesModel certificate = new CertificatesModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    CertificatesModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new CertificatesModel();

                        if (dr["idCertificate"].ToString() != "")
                        {
                            model.idCertificate = Int32.Parse(dr["idCertificate"].ToString());
                        }

                        model.codeCertificate = dr["codeCertificate"].ToString();
                        model.nameCertificate = dr["nameCertificate"].ToString();
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