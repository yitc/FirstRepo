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
    public class MedicalVoluntaryBUS
    {
        private MedicalVoluntaryDAO MedVolDAO;

        public MedicalVoluntaryBUS()
        {
            MedVolDAO = new MedicalVoluntaryDAO();
        }

        public bool Save(MedicalVoluntaryModel vol, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.SaveVoluntary(vol, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveVoluntaryArrangement(MedicalVoluntaryArrangementModel vol, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.SaveVoluntaryArrangement(vol, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveMedicalQuestGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.InsertMedicalQuestionGroup(qg,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMedicalQuestGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.UpdateMedicalQuestionGroup(qg,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMedicalQuestGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.DeleteMedicalQuestionGroup(qg,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveVoluntaryQuestGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.InsertVoluntaryQuestionGroup(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateVoluntaryQuestGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.UpdateVoluntaryQuestionGroup(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVoluntaryQuestGroup(QuestGroupModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.DeleteVoluntaryQuestionGroup(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveMedicalQuest(QuestModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.InsertMedicalQuestion(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMedicalQuest(QuestModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.UpdateMedicalQuestion(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public bool SaveVoluntaryQuest(QuestModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.InsertVoluntaryQuestion(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateVoluntaryQuest(QuestModel qg, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.UpdateVoluntaryQuestion(qg, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveMedicalAnswer(AnswerModel am, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.InsertMedicalAnswer(am, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateMedicalAnswer(AnswerModel am, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.UpdateMedicalAnswer(am, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public bool SaveVoluntaryAnswer(AnswerModel am, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.InsertVoluntaryAnswer(am, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateVoluntaryAnswer(AnswerModel am, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.UpdateVoluntaryAnswer(am, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public bool Delete(int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = MedVolDAO.DeleteVoluntary(idContPers, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVoluntaryArrangement(int idArrangement, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = MedVolDAO.DeleteVoluntaryArrangement(idArrangement, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveMedical(MedicalVoluntaryModel med, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.SaveMedical(med, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMedical(int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = MedVolDAO.DeleteMedical(idContPers, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int GetLastVolAnswerId()
        {
            int LastId = 0;

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.getLastVolAnswerId();

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    LastId = Convert.ToInt32(dataTable.Rows[0]["idAns"].ToString());
                }
            }
            return LastId + 1;
        }

        public int GetLastAnswerId()
        {
            int LastId = 0;

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.getLastAnswerId();

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    LastId = Convert.ToInt32(dataTable.Rows[0]["idAns"].ToString());
                }
            }
            return LastId + 1;
        }

        public List<AnswerTypeModel> GetAnswerType()
        {
            List<AnswerTypeModel> MedVolList = new List<AnswerTypeModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetAnswerTypes();

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AnswerTypeModel MedVolMod = new AnswerTypeModel();

                    MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    MedVolMod.nameAnsType = dr["nameAnsType"].ToString();
                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }

        public List<AnswerTypeModel> GetVolAnswerType()
        {
            List<AnswerTypeModel> MedVolList = new List<AnswerTypeModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVolAnswerTypes();

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AnswerTypeModel MedVolMod = new AnswerTypeModel();

                    MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    MedVolMod.nameAnsType = dr["nameAnsType"].ToString();
                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }


        public List<MedicalVoluntaryModel> GetMedicalDetails(List<string> query, int idContPers, Boolean isDefaultSort, Boolean isAll)
        {
            List<MedicalVoluntaryModel> MedVolList = new List<MedicalVoluntaryModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetMedical(query, idContPers, isDefaultSort, isAll);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MedicalVoluntaryModel MedVolMod = new MedicalVoluntaryModel();

                    MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                    MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolMod.txtAns = dr["txtAns"].ToString();
                    if (dr["idAnsType"].ToString() != "")
                        MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    if (dr["idcpr"].ToString() != "")
                    {
                        MedVolMod.idcpr = Convert.ToInt32(dr["idcpr"].ToString());
                    }
                    else
                        MedVolMod.idcpr = null;
                    MedVolMod.txt = dr["txt"].ToString();
                    if (dr["questSort"].ToString() != "")
                    {
                        //MedVolMod.questSort = Convert.ToInt32(dr["questSort"].ToString());
                        MedVolMod.questSort = Convert.ToDecimal(dr["questSort"].ToString());
                    }
                    else
                        MedVolMod.questSort = null;
                    if (dr["ansSort"].ToString() != "")
                    {
                        MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
                    }
                    else
                        MedVolMod.ansSort = null;

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }

        public List<IModel> GetMedicalQuestDetails(List<int> query)
        {
            List<IModel> MedVolList = new List<IModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetMedicalQuest(query);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {

                    MedicalVoluntaryQuestModel MedVolMod = new MedicalVoluntaryQuestModel();

                    MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                    if (dr["idQuestGroup"].ToString() != "")
                    {
                        MedVolMod.idQuestGroup = Convert.ToInt32(dr["idQuestGroup"].ToString());
                    }

                    MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolMod.txtAns = dr["txtAns"].ToString();
                    if (dr["idAnsType"].ToString() != "")
                        MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    MedVolMod.nameAnsType = dr["nameAnsType"].ToString();
                    if (dr["ansSort"].ToString() != "")
                    {
                        MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
                    }
                    else
                        MedVolMod.ansSort = null;

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }
        public List<IModel> GetVoluntaryQuestDetails(List<int> query)
        {
            List<IModel> MedVolList = new List<IModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVoluntaryQuest(query);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {

                    MedicalVoluntaryQuestModel MedVolMod = new MedicalVoluntaryQuestModel();
                    if (dr["idQuestGroup"].ToString() != "")
                    {
                        MedVolMod.idQuestGroup = Convert.ToInt32(dr["idQuestGroup"].ToString());
                    }

                    MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                    MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolMod.txtAns = dr["txtAns"].ToString();
                    if (dr["idAnsType"].ToString() != "")
                        MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    MedVolMod.nameAnsType = dr["nameAnsType"].ToString();
                    if (dr["ansSort"].ToString() != "")
                    {
                        MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
                    }
                    else
                        MedVolMod.ansSort = null;

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }
        //public List<IModel> GetVoluntaryQuestDetails(List<int> query)
        //{
        //    List<IModel> MedVolList = new List<IModel>();

        //    DataTable dataTable = new DataTable();
        //    dataTable = MedVolDAO.GetVoluntaryQuest(query);

        //    if (dataTable != null)
        //    {
        //        foreach (DataRow dr in dataTable.Rows)
        //        {

        //            MedicalVoluntaryQuestModel MedVolMod = new MedicalVoluntaryQuestModel();

        //            MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
        //            MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
        //            MedVolMod.txtQuest = dr["txtQuest"].ToString();
        //            if (dr["idAns"].ToString() != "")
        //            {
        //                MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
        //            }
        //            else
        //                MedVolMod.idAns = null;
        //            MedVolMod.txtAns = dr["txtAns"].ToString();
        //            if (dr["idAnsType"].ToString() != "")
        //                MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
        //            MedVolMod.nameAnsType = dr["nameAnsType"].ToString();
        //            if (dr["ansSort"].ToString() != "")
        //            {
        //                MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
        //            }
        //            else
        //                MedVolMod.ansSort = null;

        //            MedVolList.Add(MedVolMod);
        //        }

        //        return MedVolList;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<QuestGroupModel> GetQuestionGroupMedical(int idCompany)
        {
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetMedicalQuestGroup(idCompany);
            List<QuestGroupModel> medQuestGroup = new List<QuestGroupModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        QuestGroupModel model = new QuestGroupModel();
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.nameQuestGroup = dr["nameQuestGroup"].ToString();

                        medQuestGroup.Add(model);
                    }
                    return medQuestGroup;
                }
                else
                    return medQuestGroup;
            }
            else
                return medQuestGroup;

        }

        public List<QuestGroupModel> GetQuestionGroupVoluntary(int idCompany)
        {
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVoluntaryQuestGroup(idCompany);
            List<QuestGroupModel> volQuestGroup = new List<QuestGroupModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        QuestGroupModel model = new QuestGroupModel();
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.nameQuestGroup = dr["nameQuestGroup"].ToString();

                        volQuestGroup.Add(model);
                    }
                    return volQuestGroup;
                }
                else
                    return volQuestGroup;
            }
            else
                return volQuestGroup;

        }

        public List<QuestModel> GetQuestionMedical(int idCompany, int idQuestGroup)
        {
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetMedicalQuest(idCompany, idQuestGroup);
            List<QuestModel> medQuest = new List<QuestModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        QuestModel model = new QuestModel();
                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.txtQuest = dr["txtQuest"].ToString();
                        if (dr["questSort"].ToString() != "")
                            //model.questSort = Int32.Parse(dr["questSort"].ToString());
                            model.questSort = Decimal.Parse(dr["questSort"].ToString());

                        medQuest.Add(model);
                    }
                    return medQuest;
                }
                else
                    return medQuest;
            }
            else
                return medQuest;

        }

        public List<QuestModel> GetQuestionVoluntary(int idCompany, int idQuestGroup)
        {
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVoluntaryQuest(idCompany, idQuestGroup);
            List<QuestModel> volQuest = new List<QuestModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        QuestModel model = new QuestModel();
                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.txtQuest = dr["txtQuest"].ToString();
                        if (dr["questSort"].ToString() != "")
                            model.questSort = Int32.Parse(dr["questSort"].ToString());


                        volQuest.Add(model);
                    }
                    return volQuest;
                }
                else
                    return volQuest;
            }
            else
                return volQuest;

        }

        public List<AnswerModel> GetAnswerMedical(int idQuest, List<int> Labels)
        {
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetMedicalAnswer(idQuest, Labels);
            List<AnswerModel> medAnswer = new List<AnswerModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AnswerModel model = new AnswerModel();
                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQueryType = Int32.Parse(dr["idQueryType"].ToString());
                        model.idAns = Int32.Parse(dr["idAns"].ToString());
                        model.idAnsType = Int32.Parse(dr["idAnsType"].ToString());
                        model.nameLabel = dr["nameLabel"].ToString();
                        model.txtAns = dr["txtAns"].ToString();
                        if (dr["ansSort"].ToString() != "")
                            model.ansSort = Int32.Parse(dr["ansSort"].ToString());
                        if (dr["idQuestSkills"].ToString() != "")
                            model.idQuestSkills = Int32.Parse(dr["idQuestSkills"].ToString());

                        medAnswer.Add(model);
                    }
                    return medAnswer;
                }
                else
                    return medAnswer;
            }
            else
                return medAnswer;

        }

        public List<AnswerModel> GetAnswerVoluntary(int idQuest, List<int> Labels)
        {
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVoluntaryAnswer(idQuest, Labels);
            List<AnswerModel> volAnswer = new List<AnswerModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AnswerModel model = new AnswerModel();
                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQueryType = Int32.Parse(dr["idQueryType"].ToString());
                        model.idAns = Int32.Parse(dr["idAns"].ToString());
                        model.idAnsType = Int32.Parse(dr["idAnsType"].ToString());
                        model.nameLabel = dr["nameLabel"].ToString();
                        model.txtAns = dr["txtAns"].ToString();
                        if (dr["ansSort"].ToString() != "")
                            model.ansSort = Int32.Parse(dr["ansSort"].ToString());

                        volAnswer.Add(model);
                    }
                    return volAnswer;
                }
                else
                    return volAnswer;
            }
            else
                return volAnswer;

        }



        public List<MedicalVoluntaryModel> GetVoluntaryDetails(List<string> query, int idContPers, Boolean isDefaultSort, Boolean isAll)
        {
            List<MedicalVoluntaryModel> MedVolList = new List<MedicalVoluntaryModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVoluntary(query, idContPers, isDefaultSort, isAll);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MedicalVoluntaryModel MedVolMod = new MedicalVoluntaryModel();

                    MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                    MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolMod.txtAns = dr["txtAns"].ToString();
                    MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    if (dr["idcpr"].ToString() != "")
                    {
                        MedVolMod.idcpr = Convert.ToInt32(dr["idcpr"].ToString());
                    }
                    else
                        MedVolMod.idcpr = null;
                    MedVolMod.txt = dr["txt"].ToString();
                    if (dr["questSort"].ToString() != "")
                    {
                        MedVolMod.questSort = Convert.ToInt32(dr["questSort"].ToString());
                        //MedVolMod.questSort = Convert.ToDecimal(dr["questSort"].ToString());
                    }
                    else
                        MedVolMod.questSort = null;
                    if (dr["ansSort"].ToString() != "")
                    {
                        MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
                    }
                    else
                        MedVolMod.ansSort = null;

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }


        public List<MedicalVoluntaryArrangementModel> GetVoluntaryForArrangement(List<String> idQueryType, int idArrangement, Boolean isDefaultSort, Boolean isAll)
        {
            List<MedicalVoluntaryArrangementModel> MedVolList = new List<MedicalVoluntaryArrangementModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetVoluntaryForArrangement(idQueryType, idArrangement, isDefaultSort, isAll);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MedicalVoluntaryArrangementModel MedVolMod = new MedicalVoluntaryArrangementModel();

                    MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                    if (dr["idQuest"].ToString() != "")
                        MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolMod.txtAns = dr["txtAns"].ToString();

                    if (dr["idAnsType"].ToString() != "")
                    {
                        MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    }
                    if (dr["idArr"].ToString() != "")
                    {
                        MedVolMod.idArr = Convert.ToInt32(dr["idArr"].ToString());
                    }
                    else
                        MedVolMod.idArr = null;
                    MedVolMod.txt = dr["txt"].ToString();
                    if (dr["questSort"].ToString() != "")
                    {
                        MedVolMod.questSort = Convert.ToInt32(dr["questSort"].ToString());
                    }
                    else
                        MedVolMod.questSort = null;
                    if (dr["ansSort"].ToString() != "")
                    {
                        MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
                    }
                    else
                        MedVolMod.ansSort = null;

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }

        //public List<MedicalVoluntaryModel> GetMedicalForBooking(List<string> query)
        //{
        //    List<MedicalVoluntaryModel> MedVolList = new List<MedicalVoluntaryModel>();

        //    DataTable dataTable = new DataTable();
        //    dataTable = MedVolDAO.GetMedicalForBooking(query);

        //    if (dataTable != null)
        //    {
        //        foreach (DataRow dr in dataTable.Rows)
        //        {
        //            MedicalVoluntaryModel MedVolMod = new MedicalVoluntaryModel();

        //            MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
        //            MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
        //            MedVolMod.txtQuest = dr["txtQuest"].ToString();
        //            if (dr["idAns"].ToString() != "")
        //            {
        //                MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
        //            }
        //            else
        //                MedVolMod.idAns = null;
        //            MedVolMod.txtAns = dr["txtAns"].ToString();
        //            if (dr["idAnsType"].ToString() != "")
        //                MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
        //            if (dr["idcpr"].ToString() != "")
        //            {
        //                MedVolMod.idcpr = Convert.ToInt32(dr["idcpr"].ToString());
        //            }
        //            else
        //                MedVolMod.idcpr = null;
        //            MedVolMod.txt = dr["txt"].ToString();
        //            if (dr["questSort"].ToString() != "")
        //            {
        //                //MedVolMod.questSort = Convert.ToInt32(dr["questSort"].ToString());
        //                MedVolMod.questSort = Convert.ToDecimal(dr["questSort"].ToString());
        //            }
        //            else
        //                MedVolMod.questSort = null;
        //            if (dr["ansSort"].ToString() != "")
        //            {
        //                MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
        //            }
        //            else
        //                MedVolMod.ansSort = null;

        //            MedVolList.Add(MedVolMod);
        //        }

        //        return MedVolList;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<MedicalVoluntaryModel> GetMedicalForBooking(List<string> query)
        {
            List<MedicalVoluntaryModel> MedVolList = new List<MedicalVoluntaryModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetMedicalForBooking(query);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MedicalVoluntaryModel MedVolMod = new MedicalVoluntaryModel();

                    MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                    MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolMod.txtAns = dr["txtAns"].ToString();
                    if (dr["idAnsType"].ToString() != "")
                        MedVolMod.idAnsType = Convert.ToInt32(dr["idAnsType"].ToString());
                    //MedVolMod.txt = dr["txt"].ToString();
                    if (dr["questSort"].ToString() != "")
                    {
                        //MedVolMod.questSort = Convert.ToInt32(dr["questSort"].ToString());
                        MedVolMod.questSort = Convert.ToDecimal(dr["questSort"].ToString());
                    }
                    else
                        MedVolMod.questSort = null;
                    if (dr["ansSort"].ToString() != "")
                    {
                        MedVolMod.ansSort = Convert.ToInt32(dr["ansSort"].ToString());
                    }
                    else
                        MedVolMod.ansSort = null;

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }

        public int checkAnsIsInMedCpr(int idAns, int idQueryType)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkAnsIsInMedCpr(idAns, idQueryType);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsOneAns(AnswerModel ma)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkIsOneAns(ma);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = dataTable.Rows.Count;
                }

            }
            return num;
        }
        public int checkIsOneQuest(int idGroup)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkIsOneQuest(idGroup);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = dataTable.Rows.Count;
                }

            }
            return num;
        }
        public bool DeleteAnsSript(AnswerModel ma, bool quest, bool group, int idQuestGroup, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.DeleteAnsSript(ma, quest, group, idQuestGroup, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public int checkIsOneQuestInGroup(QuestModel ma)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkIsOneQuestInGroup(ma);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = dataTable.Rows.Count;
                }

            }
            return num;
        }

        public bool DeleteQuestSript(QuestModel ma, bool group, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.DeleteQuestSript(ma, group, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<int> checkAnsForQuestDevice(int idQuest)
        {
            List<int> idAns = new List<int>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkAnsForQuestDevice(idQuest);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    if ((dr["idAns"].ToString()) != "")
                        idAns.Add(Convert.ToInt32(dr["idAns"].ToString()));

                }

                return idAns;
            }
            else
            {
                return null;
            }
        }

        public List<MedicalVoluntaryModel> GetSameQuestAnswer(string idQuest, string idAns)
        {
            List<MedicalVoluntaryModel> MedVolList = new List<MedicalVoluntaryModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetSameQuestAnswer(idQuest, idAns);
            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MedicalVoluntaryModel MedVolMod = new MedicalVoluntaryModel();

                    MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    if (dr["idAns"].ToString() != "")
                    {
                        MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());
                    }
                    else
                        MedVolMod.idAns = null;
                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }

        //================
        public int checkAnsIsInVolCpr(int idAns, int idQueryType)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkAnsIsInVolCpr(idAns, idQueryType);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkAnsIsInVolArr(int idAns, int idQueryType)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkAnsIsInVolArr(idAns, idQueryType);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsOneAnsVol(AnswerModel ma)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkIsOneAnsVol(ma);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = dataTable.Rows.Count;
                }

            }
            return num;
        }

        public int checkIsOneQuestVol(int idGroup)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkIsOneQuestVol(idGroup);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = dataTable.Rows.Count;
                }

            }
            return num;
        }

        public bool DeleteAnsSriptVol(AnswerModel ma, bool quest, bool group, int idQuestGroup, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.DeleteAnsSriptVol(ma, quest, group, idQuestGroup, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public int checkIsOneQuestInGroupVol(QuestModel ma)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.checkIsOneQuestInGroupVol(ma);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = dataTable.Rows.Count;
                }

            }
            return num;
        }

        public bool DeleteQuestSriptVol(QuestModel ma, bool group, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = MedVolDAO.DeleteQuestSriptVol(ma, group, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        //================

        public List<MedicalVoluntarySkillsModel> GetSkills(List<int> Labels)
        {
            List<MedicalVoluntarySkillsModel> MedVolList = new List<MedicalVoluntarySkillsModel>();

            DataTable dataTable = new DataTable();
            dataTable = MedVolDAO.GetSkills(Labels);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MedicalVoluntarySkillsModel MedVolMod = new MedicalVoluntarySkillsModel();

                    MedVolMod.txtQuest = (dr["txtQuest"].ToString());
                    if (dr["idQuest"].ToString() != "")
                        MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                    if (dr["idQuestSkills"].ToString() != "")
                        MedVolMod.idQuestSkills = Convert.ToInt32(dr["idQuestSkills"].ToString());
                    if (dr["idQuestAns"].ToString() != "")
                        MedVolMod.idQuestAns = Convert.ToInt32(dr["idQuestAns"].ToString());

                    MedVolList.Add(MedVolMod);
                }

                return MedVolList;
            }
            else
            {
                return null;
            }
        }
    }
}
