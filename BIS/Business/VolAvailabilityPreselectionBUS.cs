using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Business
{
    public class VolAvailabilityPreselectionBUS
    {
        private VolAvailabilityPreselectionDAO volAvaPreDAO;

        public VolAvailabilityPreselectionBUS()
        {
            volAvaPreDAO = new VolAvailabilityPreselectionDAO();
        }

        public List<VolAvailabilityPreselectionModel> GetAllSkills(List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllSkills(label);
            List<VolAvailabilityPreselectionModel> volAvaSkkillModel = new List<VolAvailabilityPreselectionModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VolAvailabilityPreselectionModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolAvailabilityPreselectionModel();

                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.txtQuest = dr["txtQuest"].ToString();
                        model.idQueryType = Int32.Parse(dr["idQueryType"].ToString());
                        model.idAns = Int32.Parse(dr["idAns"].ToString());

                        model.select = bool.Parse("false");

                        model.nameLabel = dr["nameLabel"].ToString();

                        volAvaSkkillModel.Add(model);
                    }
                    return volAvaSkkillModel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<VolAvailabilityPreselectionModel> GetAllTripPreferences(List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllTripPreferences(label);
            List<VolAvailabilityPreselectionModel> volAvaTripModel = new List<VolAvailabilityPreselectionModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VolAvailabilityPreselectionModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolAvailabilityPreselectionModel();

                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.txtQuest = dr["txtQuest"].ToString();
                        model.idQueryType = Int32.Parse(dr["idQueryType"].ToString());
                        model.idAns = Int32.Parse(dr["idAns"].ToString());

                        model.select = bool.Parse("false");

                        model.nameLabel = dr["nameLabel"].ToString();

                        volAvaTripModel.Add(model);
                    }
                    return volAvaTripModel;

                }
                else
                    return null;
            }
            else
                return null;

        }

        public List<VolAvailabilityPreselectionModel> GetAllFunction(List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllFunction(label);
            List<VolAvailabilityPreselectionModel> volAvaFunModel = new List<VolAvailabilityPreselectionModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VolAvailabilityPreselectionModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolAvailabilityPreselectionModel();

                        model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        model.idQuestGroup = Int32.Parse(dr["idQuestGroup"].ToString());
                        model.txtQuest = dr["txtQuest"].ToString();
                        model.idQueryType = Int32.Parse(dr["idQueryType"].ToString());
                        model.idAns = Int32.Parse(dr["idAns"].ToString());

                        model.select = bool.Parse("false");

                        model.nameLabel = dr["nameLabel"].ToString();

                        volAvaFunModel.Add(model);
                    }
                    return volAvaFunModel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<VolAvailabilityContPersPreselectionModel> GetContactPersonPreselection(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<VolAvailabilityPreselectionModel> listSkills, List<VolAvailabilityPreselectionModel> listPreferences, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetContactPersonPreselection(dtFrom, dtTo, listFunction, listSkills, listPreferences, label);
            List<VolAvailabilityContPersPreselectionModel> getContPersModel = new List<VolAvailabilityContPersPreselectionModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VolAvailabilityContPersPreselectionModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolAvailabilityContPersPreselectionModel();

                        if (dr["idContPers"].ToString() != null)
                        {
                            model.idContPers = Convert.ToInt32(dr["idContPers"].ToString());
                        }

                        if (dr["Availability"].ToString() != null)
                        {
                            model.Availability = Convert.ToInt32(dr["Availability"].ToString());
                        }
                        if (dr["nrBooked"].ToString() != null)
                        {
                            model.nrBooked = Convert.ToInt32(dr["nrBooked"].ToString());
                        }

                        if (dr["title"].ToString() != null)
                        {
                            model.title = dr["title"].ToString();
                        }

                        if (dr["firstname"].ToString() != null)
                        {
                            model.firstname = dr["firstname"].ToString();
                        }

                        if (dr["lastname"].ToString() != null)
                        {
                            model.lastname = dr["lastname"].ToString();
                        }

                        if (dr["midname"].ToString() != null)
                        {
                            model.midname = dr["midname"].ToString();
                        }

                        //if(dr["Name"].ToString()!=null)
                        //{
                        //    model.Name=dr["Name"].ToString();
                        //}

                        //if(dr["nameGender"].ToString()!=null)
                        //{
                        //    model.nameGender=dr["nameGender"].ToString();
                        //}


                        if (dr["Age"].ToString() != null)
                        {
                            model.Age = dr["Age"].ToString();
                        }
                        model.function = dr["Functions"].ToString();

                        getContPersModel.Add(model);
                    }
                    return getContPersModel;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public List<VolVogCokGokPassModel> GetGOK(DateTime dtFrom, DateTime dtTo, DateTime expirationDate, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetGOK(dtFrom, dtTo, expirationDate, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());

                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());

                        model.type = dr["type"].ToString();

                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();


                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        public List<VolVogCokGokPassModel> GetCok(DateTime dtFrom, DateTime dtTo, DateTime expirationDate, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetCok(dtFrom, dtTo, expirationDate, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());

                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());


                        if (dr["dateExpiried"].ToString() != "")
                            model.dateExpiried = DateTime.Parse(dr["dateExpiried"].ToString());

                        model.type = dr["type"].ToString();

                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();
                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        public List<VolVogCokGokPassModel> GetVOK(DateTime dtFrom, DateTime dtTo, DateTime expirationDate, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetVOK(dtFrom, dtTo, expirationDate, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());


                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());

                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());



                        model.type = dr["type"].ToString();

                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();

                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        public List<VolVogCokGokPassModel> GetPassport(DateTime dtFrom, DateTime dtTo, DateTime expirationDate, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetPassport(dtFrom, dtTo, expirationDate, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());


                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());

                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());

                        model.type = dr["type"].ToString();

                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();
                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public List<VolVogCokGokPassModel> GetAllVokCokGokPass(DateTime dtFrom, DateTime dtTo, DateTime expirationDate, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllVokCokGokPass(dtFrom, dtTo, expirationDate, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();


                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());
                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());

                        if (dr["dateExpiried"].ToString() != "")
                            model.dateExpiried = DateTime.Parse(dr["dateExpiried"].ToString());

                        model.type = dr["type"].ToString();

                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();
                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        public List<VolVogCokGokPassModel> GeExitListData(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> volFunctionListExit, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GeExitListData(dtFrom, dtTo, volFunctionListExit, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());
                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());



                        if (dr["Age"].ToString() != "")
                            model.age = Int32.Parse(dr["Age"].ToString());

                        if (dr["NrTravel"].ToString() != "")
                            model.NrTravel = Int32.Parse(dr["NrTravel"].ToString());

                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();

                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public List<VoluntaryContPersReasonInModel> GetContactPersonReasionIn(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunctionReasonIn, List<int> label, List<VoluntaryReasonInModel> reasonIn)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetContactPersonReasionIn(dtFrom, dtTo, listFunctionReasonIn, label, reasonIn);
            List<VoluntaryContPersReasonInModel> getContPersReasonInModel = new List<VoluntaryContPersReasonInModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VoluntaryContPersReasonInModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VoluntaryContPersReasonInModel();

                        if (dr["idContPers"].ToString() != null)
                        {
                            model.idContPers = Convert.ToInt32(dr["idContPers"].ToString());
                        }

                        if (dr["title"].ToString() != null)
                        {
                            model.title = dr["title"].ToString();
                        }

                        if (dr["firstname"].ToString() != null)
                        {
                            model.firstname = dr["firstname"].ToString();
                        }

                        if (dr["lastname"].ToString() != null)
                        {
                            model.lastname = dr["lastname"].ToString();
                        }

                        if (dr["midname"].ToString() != null)
                        {
                            model.midname = dr["midname"].ToString();
                        }


                        if (dr["Age"].ToString() != null)
                        {
                            model.Age = dr["Age"].ToString();
                        }


                        if (dr["nameReasonIn"].ToString() != null)
                        {
                            model.nameReasonIn = dr["nameReasonIn"].ToString();
                        }

                        if (dr["functions"].ToString() != null)
                        {
                            model.function = dr["Functions"].ToString();
                        }

                        getContPersReasonInModel.Add(model);
                    }
                    return getContPersReasonInModel;
                }
                else
                    return null;
            }
            else
                return null;
        }


        #region ReasonOut
        public List<VoluntaryReasonOutModel> GetReasonOut()
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetReasonOut();
            List<VoluntaryReasonOutModel> volAvaPreModel = new List<VoluntaryReasonOutModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VoluntaryReasonOutModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VoluntaryReasonOutModel();
                        if (dr["idReasonOut"].ToString() != "")
                            model.idReasonOut = Int32.Parse(dr["idReasonOut"].ToString());
                        if (dr["nameReasonOut"].ToString() != "")
                            model.nameReasonOut = dr["nameReasonOut"].ToString();
                        volAvaPreModel.Add(model);
                    }
                    return volAvaPreModel;

                }
                else
                    return null;
            }
            else
                return null;

        }
        public List<VolAvailabilityReasonOutPreselectionModel> GetAllForReasonOut(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<VoluntaryReasonOutModel> reasonOut, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllForReasonOut(dtFrom, dtTo, listFunction, reasonOut, label);
            List<VolAvailabilityReasonOutPreselectionModel> lista = new List<VolAvailabilityReasonOutPreselectionModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolAvailabilityReasonOutPreselectionModel model = new VolAvailabilityReasonOutPreselectionModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());

                        if (dr["firstname"].ToString() != "")
                            model.firstName = (dr["firstname"].ToString());

                        if (dr["lastname"].ToString() != "")
                            model.lastName = (dr["lastname"].ToString());

                        if (dr["midname"].ToString() != "")
                            model.midName = (dr["midname"].ToString());

                        if (dr["age"].ToString() != "")
                            model.Age = Int32.Parse(dr["age"].ToString());

                        if (dr["functions"].ToString() != "")
                            model.function = (dr["functions"].ToString());

                        if (dr["telephoner"].ToString() != "")
                            model.numberTel = (dr["telephoner"].ToString());

                        if (dr["nameReasonOut"].ToString() != "")
                            model.reasonOut = dr["nameReasonOut"].ToString();

                        if (dr["functions"].ToString() != "")
                            model.function = dr["functions"].ToString();

                        if (dr["email"].ToString() != "")
                            model.email = (dr["email"].ToString());
                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        #endregion

        public List<VolVogCokGokPassModel> GetNotBookedData(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> volFunctionListExit, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetNotBookedData(dtFrom, dtTo, volFunctionListExit, label);
            List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>(); ;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolVogCokGokPassModel model = new VolVogCokGokPassModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["title"].ToString() != "")
                            model.title = (dr["title"].ToString());
                        if (dr["firstName"].ToString() != "")
                            model.firstName = (dr["firstName"].ToString());

                        if (dr["lastName"].ToString() != "")
                            model.lastName = (dr["lastName"].ToString());

                        if (dr["midName"].ToString() != "")
                            model.midName = (dr["midName"].ToString());



                        if (dr["Age"].ToString() != "")
                            model.age = Int32.Parse(dr["Age"].ToString());


                        model.phone = dr["phone"].ToString();

                        model.email = dr["email"].ToString();

                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        
        #region Age list
        public List<VolAvailabilityAgeListPreselectionModel> GetAllForAgeList(DateTime dtReference, int ageFrom, int ageTo, List<VolAvailabilityPreselectionModel> listFunction, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllForAgeList(dtReference, ageFrom, ageTo, listFunction, label);
            List<VolAvailabilityAgeListPreselectionModel> lista = new List<VolAvailabilityAgeListPreselectionModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolAvailabilityAgeListPreselectionModel model = new VolAvailabilityAgeListPreselectionModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["nameTitle"].ToString() != "")
                            model.nameTitle = (dr["nameTitle"].ToString());
                        if (dr["firstname"].ToString() != "")
                            model.firstName = (dr["firstname"].ToString());

                        if (dr["lastname"].ToString() != "")
                            model.lastName = (dr["lastname"].ToString());

                        if (dr["midname"].ToString() != "")
                            model.midName = (dr["midname"].ToString());

                        if (dr["age"].ToString() != "")
                            model.Age = Int32.Parse(dr["age"].ToString());

                        if (dr["functions"].ToString() != "")
                            model.function = (dr["functions"].ToString());

                        if (dr["telephoner"].ToString() != "")
                            model.numberTel = (dr["telephoner"].ToString());

                        if (dr["email"].ToString() != "")
                            model.email = (dr["email"].ToString());
                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }
        #endregion



        public List<VoluntaryContPersReasonInModel> UniqueVolunteers(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunctionUv,List<int>label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.UniqueVolunteers(dtFrom, dtTo,listFunctionUv,label);
            List<VoluntaryContPersReasonInModel> uniqueVolModel = new List<VoluntaryContPersReasonInModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VoluntaryContPersReasonInModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VoluntaryContPersReasonInModel();

                        if (dr["idContPers"].ToString() != null)
                        {
                            model.idContPers = Convert.ToInt32(dr["idContPers"].ToString());
                        }

                        if (dr["title"].ToString() != null)
                        {
                            model.title = dr["title"].ToString();
                        }

                        if (dr["firstname"].ToString() != null)
                        {
                            model.firstname = dr["firstname"].ToString();
                        }

                        if (dr["lastname"].ToString() != null)
                        {
                            model.lastname = dr["lastname"].ToString();
                        }

                        if (dr["midname"].ToString() != null)
                        {
                            model.midname = dr["midname"].ToString();
                        }

                        if (dr["Age"].ToString() != null)
                        {
                            model.Age = dr["Age"].ToString();
                        }

                        if (dr["functions"].ToString() != null)
                        {
                            model.function = dr["Functions"].ToString();
                        }

                        uniqueVolModel.Add(model);
                    }
                    return uniqueVolModel;
                }
                else
                    return null;
            }
            else
                return null;
        }

        #region All bookings
        public List<VolAvailabilityAllBookingsPreselectionModel> GetAllForAllBooking(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<int> label)
        {
            DataTable dataTable = new DataTable();
            dataTable = volAvaPreDAO.GetAllForAllBookings(dtFrom, dtTo, listFunction, label);
            List<VolAvailabilityAllBookingsPreselectionModel> lista = new List<VolAvailabilityAllBookingsPreselectionModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        VolAvailabilityAllBookingsPreselectionModel model = new VolAvailabilityAllBookingsPreselectionModel();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["nameTitle"].ToString() != "")
                            model.nameTitle = (dr["nameTitle"].ToString());
                        if (dr["firstname"].ToString() != "")
                            model.firstName = (dr["firstname"].ToString());

                        if (dr["lastname"].ToString() != "")
                            model.lastName = (dr["lastname"].ToString());

                        if (dr["midname"].ToString() != "")
                            model.midName = (dr["midname"].ToString());

                        if (dr["age"].ToString() != "")
                            model.Age = Int32.Parse(dr["age"].ToString());

                        if (dr["txtQuest"].ToString() != "")
                            model.function = (dr["txtQuest"].ToString());

                        if (dr["telephoner"].ToString() != "")
                            model.numberTel = (dr["telephoner"].ToString());

                        if (dr["email"].ToString() != "")
                            model.email = (dr["email"].ToString());

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = (dr["nameArrangement"].ToString());

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.departureDate = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.returnDate = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        lista.Add(model);
                    }

                    return lista;
                }
                else
                    return null;
            }
            else
                return null;

        }

        #endregion
    }
}