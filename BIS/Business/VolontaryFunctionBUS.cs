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
    public class VolontaryFunctionBUS
    {
         private VolontaryFunctionDAO VolFunctionDAO;

         public VolontaryFunctionBUS()
        {
            VolFunctionDAO = new VolontaryFunctionDAO();
        }

         public bool Save(VolontaryFunctionModel vol, string nameForm, int idUser)
         {
             bool retval = false;
             try
             {

                 retval = VolFunctionDAO.SaveVoluntary(vol, nameForm, idUser);    //SaveVoluntary

             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return retval;
         }

         public bool SaveArrangement(VolontaryFunctionModel vol, string nameForm, int idUser)
         {
             bool retval = false;
             try
             {

                 retval = VolFunctionDAO.SaveVoluntaryArrangement(vol, nameForm, idUser);

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

                 retval = VolFunctionDAO.InsertVoluntaryQuestionGroup(qg, nameForm, idUser);

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

                 retval = VolFunctionDAO.UpdateVoluntaryQuestionGroup(qg, nameForm, idUser);

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

                 retval = VolFunctionDAO.DeleteVoluntaryQuestionGroup(qg, nameForm, idUser);

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

                 retval = VolFunctionDAO.InsertVoluntaryQuestion(qg, nameForm, idUser);

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

                 retval = VolFunctionDAO.UpdateVoluntaryQuestion(qg, nameForm, idUser);

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

                 retval = VolFunctionDAO.InsertVoluntaryAnswer(am, nameForm, idUser);

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

                 retval = VolFunctionDAO.UpdateVoluntaryAnswer(am, nameForm, idUser);

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
                 retval = VolFunctionDAO.DeleteVoluntary(idContPers, nameForm, idUser);

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
                 retval = VolFunctionDAO.DeleteVoluntaryArrangement(idArrangement, nameForm, idUser);

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
             dataTable = VolFunctionDAO.getLastVolAnswerId();

             if (dataTable != null)
             {
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     LastId = Convert.ToInt32(dataTable.Rows[0]["idAns"].ToString());
                 }
             }
             return LastId+1;
         }


      

         public List<AnswerTypeModel> GetVolAnswerType()
         {
             List<AnswerTypeModel> MedVolList = new List<AnswerTypeModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVolAnswerTypes();

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


         public List<IModel> GetVoluntaryQuestDetails(List<int> query)
         {
             List<IModel> MedVolList = new List<IModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntaryQuest(query);

             if (dataTable != null)
             {
                 foreach (DataRow dr in dataTable.Rows)
                 {

                     MedicalVoluntaryQuestModel MedVolMod = new MedicalVoluntaryQuestModel();

                     MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                     MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                     if (dr["idQuestGroup"].ToString() != "")
                     {
                         MedVolMod.idQuestGroup = Convert.ToInt32(dr["idQuestGroup"].ToString());
                     }

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
         //    dataTable = VolFunctionDAO.GetVoluntaryQuest(query);

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

    

         //public List<IModel> GetVoluntaryQuestDetails(List<int> query)
         //{
         //    List<IModel> MedVolList = new List<IModel>();

         //    DataTable dataTable = new DataTable();
         //    dataTable = VolFunctionDAO.GetVoluntaryQuest(query);

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



       
         public List<QuestGroupModel> GetQuestionGroupVoluntary(int idCompany)
         {
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntaryQuestGroup(idCompany);
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

     

         public List<QuestModel> GetQuestionVoluntary(int idCompany, int idQuestGroup)
         {
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntaryQuest2(idCompany, idQuestGroup);
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

     

         public List<AnswerModel> GetAnswerVoluntary(int idQuest, List<int> Labels)
         {
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntaryAnswer(idQuest, Labels);
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

         public List<VolontaryFunctionModel> GetSameQuestAnswer(string idQuest, string idAns, string idContPers)
         {
             List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetSameQuestAnswer(idQuest, idAns, idContPers);
             if (dataTable != null)
             {
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     VolontaryFunctionModel MedVolMod = new VolontaryFunctionModel();

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

         public List<VolontaryFunctionModel> GetVoluntaryDetails(List<string> query, int idContPers, Boolean isDefaultSort, Boolean isAll)
         {
             List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntary(query, idContPers, isDefaultSort, isAll);

             if (dataTable != null)
             {
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     VolontaryFunctionModel MedVolMod = new VolontaryFunctionModel();

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

   
         public List<VolontaryFunctionModel> GetVoluntaryArrangmentDetails(List<string> query, int idArrangement, Boolean isDefaultSort, Boolean isAll)
         {
             List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntaryArrangement(query, idArrangement, isDefaultSort, isAll);

             if (dataTable != null)
             {
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     VolontaryFunctionModel MedVolMod = new VolontaryFunctionModel();

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
                     if (dr["idArrangement"].ToString() != "")
                     {
                         MedVolMod.idcpr = Convert.ToInt32(dr["idArrangement"].ToString());
                     }
                     else
                         MedVolMod.idcpr = null;
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

         public List<MedicalVoluntaryQuestModel> GetVoluntaryFunctionForPerson(List<LabelForPerson> query, int idContPers)
         {
             List<MedicalVoluntaryQuestModel> MedVolList = new List<MedicalVoluntaryQuestModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetVoluntaryFunctionForPerson(query, idContPers);

             if (dataTable != null)
             {
                 foreach (DataRow dr in dataTable.Rows)
                 {

                     MedicalVoluntaryQuestModel MedVolMod = new MedicalVoluntaryQuestModel();

                     MedVolMod.nameQuestGroup = dr["nameQuestGroup"].ToString();
                     MedVolMod.idQuest = Convert.ToInt32(dr["idQuest"].ToString());
                     MedVolMod.txtQuest = dr["txtQuest"].ToString();
                    
                     MedVolList.Add(MedVolMod);
                 }

                 return MedVolList;
             }
             else
             {
                 return null;
             }
         }

        //=================
         public int checkIsOneAnsVF(AnswerModel ma)
         {
             int num = 0;
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.checkIsOneAnsVF(ma);

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     num = dataTable.Rows.Count;
                 }

             }
             return num;
         }
         public int checkIsOneQuestVF(int idGroup)
         {
             int num = 0;
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.checkIsOneQuestVF(idGroup);

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     num = dataTable.Rows.Count;
                 }

             }
             return num;
         }

         public int checkAnsIsInVFCpr(int idAns, int idQueryType)
         {
             int num = 0;
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.checkAnsIsInVFCpr(idAns, idQueryType);

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     num = 1;
                 }

             }
             return num;
         }
         public int checkAnsIsInVFArr(int idAns, int idQueryType)
         {
             int num = 0;
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.checkAnsIsInVFArr(idAns, idQueryType);

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     num = 1;
                 }

             }
             return num;
         }

         public bool DeleteAnsSriptVF(AnswerModel ma, bool quest, bool group, int idQuestGroup, string nameForm, int idUser)
         {
             bool retval = false;
             try
             {

                 retval = VolFunctionDAO.DeleteAnsSriptVF(ma, quest, group, idQuestGroup, nameForm, idUser);

             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return retval;
         }
         public int checkIsOneQuestInGroupVF(QuestModel ma)
         {
             int num = 0;
             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.checkIsOneQuestInGroupVF(ma);

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     num = dataTable.Rows.Count;
                 }

             }
             return num;
         }

         public bool DeleteQuestSriptVF(QuestModel ma, bool group, string nameForm, int idUser)
         {
             bool retval = false;
             try
             {

                 retval = VolFunctionDAO.DeleteQuestSriptVF(ma, group,nameForm,idUser);

             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return retval;
         }
        //=================


         public List<VolontaryFunctionModel> GetAllFromVolFunction()
         {
             List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetAllFromVolFunction();

             if (dataTable != null)
             {
                 //idcpr,idQuest,idAns,txt
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     VolontaryFunctionModel MedVolMod = new VolontaryFunctionModel();
                     if (dr["idcpr"].ToString() != "")
                         MedVolMod.idcpr = Convert.ToInt32(dr["idcpr"].ToString());

                     MedVolMod.txtQuest = dr["txtQuest"].ToString();




                     MedVolList.Add(MedVolMod);
                 }

                 return MedVolList;
             }
             else
             {
                 return null;
             }
         }
        public List<VolontaryFunctionModel> GetAllFromVolFunctionAB()
         {
             List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

             DataTable dataTable = new DataTable();
             dataTable = VolFunctionDAO.GetAllFromVolFunctionAB();

             if (dataTable != null)
             {
                 //idcpr,idQuest,idAns,txt
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     VolontaryFunctionModel MedVolMod = new VolontaryFunctionModel();
                     if (dr["idcpr"].ToString() != "")
                         MedVolMod.idcpr = Convert.ToInt32(dr["idcpr"].ToString());

                     if (dr["idcpr"].ToString() != "")
                         MedVolMod.idcpr = Convert.ToInt32(dr["idcpr"].ToString());

                     if (dr["idAns"].ToString() != "")
                         MedVolMod.idAns = Convert.ToInt32(dr["idAns"].ToString());



                     MedVolMod.txtQuest = dr["txtQuest"].ToString();




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
