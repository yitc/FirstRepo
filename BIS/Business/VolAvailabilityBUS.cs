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
    public class VolAvailabilityBUS
    {
        private VolAvailabilityDAO volDAO;

        public VolAvailabilityBUS()
        {
            volDAO = new VolAvailabilityDAO();
        }

        public List<VolAvailabilityModel> GetAvailabilityByVolontary(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.GetAvailabilityByVolontary(idContPers);
            List<VolAvailabilityModel> vollist = new List<VolAvailabilityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    VolAvailabilityModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolAvailabilityModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["dateFrom"].ToString() != "")
                            model.dateFrom = DateTime.Parse(dr["dateFrom"].ToString());

                        if (dr["dateTo"].ToString() != "")
                            model.dateTo = DateTime.Parse(dr["dateTo"].ToString());

                        if (dr["nrTimes"].ToString() != "")
                            model.nrTimes = Int32.Parse(dr["nrTimes"].ToString());

                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public object GetSignedupNrTimesForPeriod(int idArrangement, int idContPers)
        {

            object obj = volDAO.GetSignedupNrTimesForPeriod(idArrangement, idContPers);
            return obj;

        }

        public object GetFinishedNrTimesForPeriod(int idArrangement, int idContPers)
        {

            object obj = volDAO.GetFinishedNrTimesForPeriod(idArrangement, idContPers);
            return obj;

        }
       
        public int SaveAndReturnID(VolAvailabilityModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {
                retval = volDAO.SaveAndReturnID(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

       

        public bool UpdateNrTimes(int nrTimes, int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = volDAO.UpdateNrTimes(nrTimes, id, nameForm, idUser);

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

                retval = volDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        //======== jeca
        public List<AvailabilitySkillsModel> IsCheckedSkills(int idContPers, string txtQuest)
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.IsCheckedSkills(idContPers, txtQuest);
            List<AvailabilitySkillsModel> vollist = new List<AvailabilitySkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AvailabilitySkillsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AvailabilitySkillsModel();

                        if (dr["ID"].ToString() != "")
                            model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["quest"].ToString() != "")
                            model.quest = (dr["quest"].ToString());

                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AvailabilitySkillsModel> IsCheckedFunction(int idContPers, string txtQuest)
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.IsCheckedFunction(idContPers, txtQuest);
            List<AvailabilitySkillsModel> vollist = new List<AvailabilitySkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AvailabilitySkillsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AvailabilitySkillsModel();

                        if (dr["ID"].ToString() != "")
                            model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["quest"].ToString() != "")
                            model.quest = (dr["quest"].ToString());

                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AvailabilitySkillsModel> GetAvailabilitySkills()
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.GetAvailabilitySkills();
            List<AvailabilitySkillsModel> vollist = new List<AvailabilitySkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AvailabilitySkillsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AvailabilitySkillsModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["quest"].ToString() != "")
                            model.quest = (dr["quest"].ToString());

                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AvailabilitySkillsModel> GetAvailabilityFunction()
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.GetAvailabilityFunction();
            List<AvailabilitySkillsModel> vollist = new List<AvailabilitySkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AvailabilitySkillsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AvailabilitySkillsModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["quest"].ToString() != "")
                            model.quest = (dr["quest"].ToString());

                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }
        //==========
        // novo jelena:

        public List<PersonTelModel> GetAvailabilityByVolontaryTel(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.GetAvailabilityByVolontaryTel(iDContPers);
            List<PersonTelModel> vollist = new List<PersonTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    PersonTelModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonTelModel();
                        if (dr["numberTel"].ToString() != "")
                            model.numberTel = (dr["numberTel"].ToString());
                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<PersonEmailModel> GetAvailabilityByVolontaryEmail(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.GetAvailabilityByVolontaryEmail(iDContPers);
            List<PersonEmailModel> vollist = new List<PersonEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    PersonEmailModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonEmailModel();
                        if (dr["email"].ToString() != "")
                            model.email = (dr["email"].ToString());
                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }
        //============

        public List<AvailabilitySkillsModel> GetNrVolQueryType()
        {
            DataTable dataTable = new DataTable();
            dataTable = volDAO.GetNrVolQueryType();
            List<AvailabilitySkillsModel> vollist = new List<AvailabilitySkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AvailabilitySkillsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AvailabilitySkillsModel();

                        if (dr["ID"].ToString() != "")
                            model.ID = Int32.Parse(dr["ID"].ToString());
                        model.quest = dr["quest"].ToString();


                        vollist.Add(model);
                    }
                    return vollist;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
