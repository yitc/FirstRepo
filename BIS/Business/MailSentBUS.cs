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
    public class MailSentBUS
    {
        private MailSentDAO msDAO;

        public MailSentBUS()
        {
            msDAO = new MailSentDAO();
        }

        public List<IModel> GetMyEmails(int id)
        {
            DataTable dataTable = new DataTable();
            dataTable = msDAO.GetMyEmails(id);
            List<IModel> mails = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MailSentModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MailSentModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        //if(dr["entryId"].ToString() != null)
                            model.entryId = dr["entryId"].ToString();

                        if (dr["idUser"].ToString() != "")
                            model.idUser = Int32.Parse(dr["idUser"].ToString());

                        model.Subject = dr["Subject"].ToString();

                        if (dr["idPersonTo"].ToString() != "")
                            model.idPersonTo = Int32.Parse(dr["idPersonTo"].ToString());

                        if (dr["idClientTo"].ToString() != "")
                            model.idClientTo = Int32.Parse(dr["idClientTo"].ToString());

                        model.locationOnDisk = dr["locationOnDisk"].ToString();

                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                                                
                        mails.Add(model);
                    }
                    return mails;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<MailSentModel> GetMyEmails_1(int id)
        {
            DataTable dataTable = new DataTable();
            dataTable = msDAO.GetMyEmails(id);
            List<MailSentModel> mails = new List<MailSentModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MailSentModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MailSentModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        //if (dr["entryId"].ToString() != null)
                            model.entryId = dr["entryId"].ToString();

                        if (dr["idUser"].ToString() != "")
                            model.idUser = Int32.Parse(dr["idUser"].ToString());

                        model.Subject = dr["Subject"].ToString();

                        if (dr["idPersonTo"].ToString() != "")
                            model.idPersonTo = Int32.Parse(dr["idPersonTo"].ToString());

                        if (dr["idClientTo"].ToString() != "")
                            model.idClientTo = Int32.Parse(dr["idClientTo"].ToString());

                        model.locationOnDisk = dr["locationOnDisk"].ToString();

                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());

                        mails.Add(model);
                    }
                    return mails;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public List<MailSentModel> GetEmailByID(Guid entryId)
        {
            DataTable dataTable = new DataTable();
            dataTable = msDAO.GetEmailByID(entryId);
            List<MailSentModel> mails = new List<MailSentModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MailSentModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MailSentModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        //if (dr["entryId"].ToString() != null)
                            model.entryId = dr["entryId"].ToString();

                        if (dr["idUser"].ToString() != "")
                            model.idUser = Int32.Parse(dr["idUser"].ToString());

                        model.Subject = dr["Subject"].ToString();

                        if (dr["idPersonTo"].ToString() != "")
                            model.idPersonTo = Int32.Parse(dr["idPersonTo"].ToString());

                        if (dr["idClientTo"].ToString() != "")
                            model.idClientTo = Int32.Parse(dr["idClientTo"].ToString());

                        model.locationOnDisk = dr["locationOnDisk"].ToString();

                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());

                        mails.Add(model);
                    }
                    return mails;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public bool Save(MailSentModel cost)
        {
            bool retval = false;
            try
            {

                retval = msDAO.Save(cost);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
