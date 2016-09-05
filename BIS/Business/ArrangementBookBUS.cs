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
    public class ArrangementBookBUS
    {
        private ArrangementBookDAO arrBookDAO;

        public ArrangementBookBUS()
        {
            arrBookDAO = new ArrangementBookDAO();
        }

        public int checkIfArrangementBookIsInInvoice(int idArrangementBook)
        {
            int invoiceNr = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkIfArrangementBookIsInInvoice(idArrangementBook);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["invoiceNr"].ToString() != "")
                            invoiceNr = Convert.ToInt32(dr["invoiceNr"].ToString());
                    }
                }
            }

            return invoiceNr;
        }

        //====== check number medical (rollstul ...)
        public int GetBookPersMedic(List<int> idAns, int idArrangement)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetBookPersMedic(idAns, idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["num"].ToString() != "")
                            num = Convert.ToInt32(dr["num"].ToString());
                    }
                }
            }

            return num;
        }
        public bool UpdateBoardingPoint(int idArrangementBook, int IdBoardingPoint, string nameForm, int idUser) 
        {
            return arrBookDAO.UpdateBoardingPoint(idArrangementBook, IdBoardingPoint,nameForm,idUser);
        }
        public int GetBookPersMedicMoreAns(List<int> idAns, int idArrangement)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetBookPersMedicMoreAns(idAns, idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["num"].ToString() != "")
                            num = Convert.ToInt32(dr["num"].ToString());
                    }
                }
            }

            return num;
        }

        public int GetBookPersMedicPers(List<int> idAns, int idPerson)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetBookPersMedicPers(idAns, idPerson);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idcpr"].ToString() != "")
                            num = Convert.ToInt32(dr["idcpr"].ToString());
                    }
                }
            }

            return num;
        }

        public int GetNrTravelerByGender(int idGender, int idArrangement)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetNrTravelerByGender(idGender,idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["num"].ToString() != "")
                            num = Convert.ToInt32(dr["num"].ToString());
                    }
                }
            }

            return num;
        }

        public int GetNrBookedTraveler(int idArrangement)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetNrBookedTraveler(idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["num"].ToString() != "")
                            num = Convert.ToInt32(dr["num"].ToString());
                    }
                }
            }

            return num;
        }

        public Boolean checkCancelInInvoiseFinal(int idArrangement, int idContPers)
        {
            Boolean isInvoice = false;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkCancelInInvoiseFinal(idArrangement, idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    if(Convert.ToInt32(dataTable.Rows[0]["num"].ToString())!=0)
                         isInvoice = true;
                }
            }

            return isInvoice;
        }

        public Boolean checkFinal(int idArrangementBook)
        {
            Boolean canDelete = true;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkFinal(idArrangementBook);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    canDelete = false;
                }
            }

            return canDelete;
        }

        public string checkIsInTravelers(int idArrangementBook)
        {
            string Traveler = "";
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkIsInTravelers(idArrangementBook);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        if (dr["name"].ToString() != "")
                            Traveler = Traveler + ", " + dr["name"].ToString();
                    }
                }
            }

            return Traveler;
        }

        public string checkIsInTravelersNotPaidFor(int idArrangementBook)
        {
            string Traveler = "";
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkIsInTravelersNotPaidFor(idArrangementBook);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        if (dr["name"].ToString() != "")
                            Traveler = Traveler + ", " + dr["name"].ToString();
                    }
                }
            }

            return Traveler;
        }

        public DataTable checkIfPersonsIsExtraAndStatus(int idArrangementBook, int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkIfPersonsIsExtraAndStatus(idArrangementBook,idArrangement);
            return dataTable;
        }

        public Boolean checkIfPersonsHasExtraAndStatus(int idArrangementBook)
        {
            Boolean res = false;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkIfPersonsHasExtraAndStatus(idArrangementBook);
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        //public Boolean checkIfArrangementHasAnythingExtraAndNotFinal(int idArrangement, int idContPers)
        //{
        //    Boolean canDelete = false;
        //    DataTable dataTable = new DataTable();
        //    dataTable = arrBookDAO.checkIfArrangementHasAnythingExtraAndNotFinal(idArrangement, idContPers);

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            canDelete = true;
        //        }
        //    }

        //    return canDelete;
        //}

        public int checkIfInvoiceIsInAccLine(int invoiceNr)
        {
            int idAccLine = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkIfInvoiceIsInAccLine(invoiceNr);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idAccLine"].ToString() != "")
                            idAccLine = Convert.ToInt32(dr["idAccLine"].ToString());
                    }
                }
            }

            return idAccLine;
        }

        public List<ArrangementArticalForBookPersonModel> GetArrangementArticals(int idArrangement, int idArrangementBook)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetArrangementArticles(idArrangement, idArrangementBook);
            List<ArrangementArticalForBookPersonModel> arrange = new List<ArrangementArticalForBookPersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementArticalForBookPersonModel model = new ArrangementArticalForBookPersonModel();

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["number"].ToString() != "")
                            model.number = Int32.Parse(dr["number"].ToString());

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        if (dr["isContract"].ToString() != "")
                            model.isContract = Convert.ToBoolean(dr["isContract"].ToString());

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public int GetArticlesNumber(int idArrangement, Boolean isContract, int id, string idArticle)
        {
            int number = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetArticlesNumber(idArrangement, isContract, id,idArticle);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["number"].ToString()!="")
                            number = Convert.ToInt32(dr["number"].ToString());
                    }
                }
            }

            return number;
        }

        public int GetIdArrangementBookByIdBookArrangement(int idArrangementBook)
        {
            int i = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetIdArrangementBookByIdBookArrangement(idArrangementBook);
           

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idArrangementBook"].ToString() != "")
                            i = Int32.Parse(dr["idArrangementBook"].ToString());
                        break;
                       
                    }
                    return i;
                }
                else
                    return i;
            }
            else
                return i;
        }

        public int GetIdArrangementBook(int idArrangement, int idContPers)
        {
            int i = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetIdArrangementBook(idArrangement, idContPers);


            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idArrangementBook"].ToString() != "")
                            i = Int32.Parse(dr["idArrangementBook"].ToString());
                        break;

                    }
                    return i;
                }
                else
                    return i;
            }
            else
                return i;
        }

        public int GetArrangementBookBoardingPoint(int idArrangementBook, int idArrangement, int idContPers)
        {
            int i = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetArrangementBookBoardingPoint( idArrangementBook,  idArrangement,  idContPers);


            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idBoarding"].ToString() != "")
                            i = Int32.Parse(dr["idBoarding"].ToString());
                        break;

                    }
                    return i;
                }
                else
                    return i;
            }
            else
                return i;
        }

        public ArrangementBookModel GetArrangementBook(int idArrangementBook)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetArrangementBook(idArrangementBook);
            ArrangementBookModel arrBookModel = new ArrangementBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        arrBookModel.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idDebitor"].ToString() != "")
                            arrBookModel.idDebitor = Int32.Parse(dr["idDebitor"].ToString());
                        else
                            arrBookModel.idDebitor = 0;
                        
                        arrBookModel.typeDebitor = dr["typeDebitor"].ToString();

                        arrBookModel.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());
                        arrBookModel.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        arrBookModel.idStatus = Int32.Parse(dr["idStatus"].ToString());
                        arrBookModel.idTravelPapers = Int32.Parse(dr["idTravelPapers"].ToString());
                        arrBookModel.price = Decimal.Parse(dr["price"].ToString());
                        arrBookModel.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());
                        arrBookModel.isCancelInsurance = Boolean.Parse(dr["isCancelInsurance"].ToString());
                        if (dr["idBoarding"].ToString() != "")
                            arrBookModel.idBoarding = Int32.Parse(dr["idBoarding"].ToString());
                        if (dr["idUserCreated"].ToString() != "")
                            arrBookModel.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            arrBookModel.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            arrBookModel.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            arrBookModel.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                        if (dr["isMedicalDevices"].ToString() != "")
                            arrBookModel.isMedicalDevices = Boolean.Parse(dr["isMedicalDevices"].ToString());
                        if (dr["idPayInvoice"].ToString() != "")
                            arrBookModel.idPayInvoice = Int32.Parse(dr["idPayInvoice"].ToString());

                        break;
                    }
                    return arrBookModel;
                }
                else
                    return arrBookModel;
            }
            else
                return arrBookModel;
        }
        public ArrangementBookModel GetArrangementBookForTraveler(int idArrangement, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetArrangementBookForTraveler(idArrangement, idContPers);
            ArrangementBookModel arrBookModel = new ArrangementBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        arrBookModel.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        if (dr["idDebitor"].ToString() != "")
                            arrBookModel.idDebitor = Int32.Parse(dr["idDebitor"].ToString());
                        else
                            arrBookModel.idDebitor = 0;

                        arrBookModel.typeDebitor = dr["typeDebitor"].ToString();

                        arrBookModel.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());
                        arrBookModel.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        arrBookModel.idStatus = Int32.Parse(dr["idStatus"].ToString());
                        arrBookModel.idTravelPapers = Int32.Parse(dr["idTravelPapers"].ToString());
                        arrBookModel.price = Decimal.Parse(dr["price"].ToString());
                        arrBookModel.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());
                        arrBookModel.isCancelInsurance = Boolean.Parse(dr["isCancelInsurance"].ToString());
                        if (dr["idBoarding"].ToString() != "")
                            arrBookModel.idBoarding = Int32.Parse(dr["idBoarding"].ToString());
                        if (dr["idUserCreated"].ToString() != "")
                            arrBookModel.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            arrBookModel.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            arrBookModel.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            arrBookModel.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                        if (dr["isMedicalDevices"].ToString() != "")
                            arrBookModel.isMedicalDevices = Boolean.Parse(dr["isMedicalDevices"].ToString());
                        if (dr["idPayInvoice"].ToString() != "")
                            arrBookModel.idPayInvoice = Int32.Parse(dr["idPayInvoice"].ToString());

                        break;
                    }
                    return arrBookModel;
                }
                else
                    return arrBookModel;
            }
            else
                return arrBookModel;
        }

        public bool Delete(int idArrangementBook, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.Delete(idArrangementBook, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool CancelArrangament(int idArrangementBook, DateTime dt)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.CancelArrangement(idArrangementBook, dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteBookingIfNotFinal(int idArrangementBook, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.DeleteBookingIfNotFinal(idArrangementBook, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteExtraArticles(int idArrangementBook, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.DeleteExtraArticles(idArrangementBook, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int Save(ArrangementBookModel arrBookModel, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = arrBookDAO.Save(arrBookModel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SavePersons(int idArrangement, int idContPers, int idUserCreated, DateTime dtUserCreated, int idUserModified, DateTime dtUserModified, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.SavePersons(idArrangement, idContPers, idUserCreated, dtUserCreated, idUserModified, dtUserModified, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveArticles(int idArrangementBook, string idArticle, Boolean isContract, int id, string idRoom, int idUserCreated, DateTime dtUserCreated, int idUserModified, DateTime dtUserModified, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.SaveArticles(idArrangementBook, idArticle, isContract, id, idRoom, idUserCreated, dtUserCreated, idUserModified, dtUserModified, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveVolLookup(int idArrangement, int idContPers, int id, string type, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.SaveVolLookup(idArrangement, idContPers, id, type, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVolLookup(int idArrangement, int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.DeleteVolLookup(idArrangement, idContPers, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ArrangementBookModel arrBookModel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.Update(arrBookModel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateDebitor(ArrangementBookModel arrBookModel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.UpdateDebitor(arrBookModel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        
        public List<ArrangementSelectedFuncSkillsModel> GetSelectedSkillsOrFunctionsForArrangement(string type, int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetSelectedSkillsOrFunctionsForArrangement(type, idArrangement);

            List<ArrangementSelectedFuncSkillsModel> lista = new List<ArrangementSelectedFuncSkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementSelectedFuncSkillsModel arrBookModel = new ArrangementSelectedFuncSkillsModel();

                        if (dr["idContPers"].ToString() != "")
                            arrBookModel.idContPers = Convert.ToInt32(dr["idContPers"].ToString());
                        if (dr["id"].ToString() != "")
                            arrBookModel.id = Convert.ToInt32(dr["id"].ToString());


                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<ArrangementFuncSkillsModel> GetFunctionsForArrangement(int idArrangementBook)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetFunctionsForArrangement(idArrangementBook);

            List<ArrangementFuncSkillsModel> lista = new List<ArrangementFuncSkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementFuncSkillsModel arrBookModel = new ArrangementFuncSkillsModel();

                        if (dr["ID"].ToString() != "")
                            arrBookModel.ID = Int32.Parse(dr["ID"].ToString());

                        arrBookModel.Quest = dr["quest"].ToString();

                        if (dr["number"].ToString() != "")
                        {
                            int n;
                            bool isNumeric = int.TryParse(dr["number"].ToString(), out n);

                            if (isNumeric == true)
                                arrBookModel.Required = Int32.Parse(dr["number"].ToString());

                        }
                        if (dr["numberBooked"].ToString() != "")
                            arrBookModel.Booked = Int32.Parse(dr["numberBooked"].ToString());


                        arrBookModel.Available = arrBookModel.Required - arrBookModel.Booked;

                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<ArrangementFuncSkillsModel> GetSkillsForArrangement(int idArrangementBook)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetSkillsForArrangement(idArrangementBook);

            List<ArrangementFuncSkillsModel> lista = new List<ArrangementFuncSkillsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementFuncSkillsModel arrBookModel = new ArrangementFuncSkillsModel();

                        if (dr["ID"].ToString() != "")
                            arrBookModel.ID = Int32.Parse(dr["ID"].ToString());

                        arrBookModel.Quest = dr["quest"].ToString();

                        if (dr["number"].ToString() != "")
                        {
                            int n;
                            bool isNumeric = int.TryParse(dr["number"].ToString(), out n);

                            if (isNumeric == true)
                                arrBookModel.Required = Int32.Parse(dr["number"].ToString());

                        }
                        if (dr["numberBooked"].ToString() != "")
                            arrBookModel.Booked = Int32.Parse(dr["numberBooked"].ToString());


                        arrBookModel.Available = arrBookModel.Required - arrBookModel.Booked;

                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public int GetAverageAge(int idArrangement)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetAverageAge(idArrangement);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["num"].ToString() != "")
                            num = Convert.ToInt32(dr["num"].ToString());
                    }
                }
            }

            return num;
        }

        public bool UpdateVolLookup(int idArrangement, int idContPers, int idStatus,string nameForm,int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.UpdateVolLookup(idArrangement, idContPers, idStatus,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        // jelena 

        public List<OptionsModel> checkStatus(int idArrangement, int idStatus)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.checkStatus(idArrangement, idStatus);
            List<OptionsModel> persons = new List<OptionsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        OptionsModel model = new OptionsModel();

                        // name option:
                        model.idOption = Convert.ToInt32(dr["idOption"].ToString());
                        //    model.nrTraveler = Convert.ToInt32(dr["nrTraveler"]);




                        persons.Add(model);
                    }
                    return persons;
                }
                else
                    return persons;
            }
            else
                return persons;
        }

        public List<OptionsModel> NrStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.NrStatus();
            List<OptionsModel> persons = new List<OptionsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        OptionsModel model = new OptionsModel();

                        // name option:
                        model.nameOption = dr["nameOption"].ToString();
                        if (dr["idOption"].ToString() != "")
                            model.idOption = Convert.ToInt32(dr["idOption"].ToString());
                        //    model.nrTraveler = Convert.ToInt32(dr["nrTraveler"]);




                        persons.Add(model);
                    }
                    return persons;
                }
                else
                    return persons;
            }
            else
                return persons;
        }
        //
        // jelena

        public List<ArrangementModel> AllArrangementName()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AllArrangementName();

            List<ArrangementModel> lista = new List<ArrangementModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementModel arrBookModel = new ArrangementModel();

                        if (dr["idArrangement"].ToString() != "")
                            arrBookModel.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        arrBookModel.codeArrangement = (dr["codeArrangement"].ToString());


                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<ArrangementModel> NrBookedTrips(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.NrBookedTrips(idContPers);

            List<ArrangementModel> lista = new List<ArrangementModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementModel arrBookModel = new ArrangementModel();

                        if (dr["nrTraveler"].ToString() != "")
                            arrBookModel.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<MedicalVoluntaryModel> AnsAirportcodes()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AnsAirportcodes();
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> CheckAnsAirportcodes(int idContPers, string txtAnswer)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.CheckAnsAirportcodes(idContPers, txtAnswer);
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> AnsRentedDevice()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AnsRentedDevice();
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> CheckRentedDevice(int idContPers, string txtAnswer)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.CheckRentedDevice(idContPers, txtAnswer);
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> AnsDiets()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AnsDiets();
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> CheckAnsDiets(int idContPers, string txtAnswer)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.CheckAnsDiets(idContPers, txtAnswer);
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> AnsDevice()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AnsDevice();
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> CheckAnsDevice(int idContPers, string txtAnswer)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.CheckAnsDevice(idContPers, txtAnswer);
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> AnsEpilepsie()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AnsEpilepsie();
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> CheckAnsEpilepsie(int idContPers, string txtAnswer)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.CheckAnsEpilepsie(idContPers, txtAnswer);
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> AnsMedication()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AnsMedication();
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        //if (dr["ID"].ToString() != "")
                        //    model.ID = Int32.Parse(dr["ID"].ToString());

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<MedicalVoluntaryModel> CheckAnsMedication(int idContPers, string txtAnswer)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.CheckAnsMedication(idContPers, txtAnswer);
            List<MedicalVoluntaryModel> vollist = new List<MedicalVoluntaryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    MedicalVoluntaryModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new MedicalVoluntaryModel();

                        if (dr["txtAns"].ToString() != "")
                            model.txtAns = (dr["txtAns"].ToString());

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

        public List<PersonTelModel> AllTelephoneList(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AllTelephoneList(iDContPers);
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

        public List<ClientTelModel> ClientTel(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.ClientTel(iDContPers);
            List<ClientTelModel> vollist = new List<ClientTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    ClientTelModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientTelModel();
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

        public List<ClientEmailModel> ClientEmail(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.ClientEmail(iDContPers);
            List<ClientEmailModel> vollist = new List<ClientEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    ClientEmailModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientEmailModel();
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

        // jelena novo

        public List<PersonTelModel> GetCpTel(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetCpTel(iDContPers);
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

        public List<PersonEmailModel> GetCpEmail(int iDContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetCpEmail(iDContPers);
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

        public List<VolontaryFunctionModel> GetVoluntaryArrangmentDetails(int idArrangement)
        {
            List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetVoluntaryArrangement(idArrangement);

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

        public List<VolontaryFunctionModel> GetVoluntaryFunction(int idcpr, string txtAns)
        {
            List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetVoluntaryFunction(idcpr, txtAns);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    VolontaryFunctionModel MedVolMod = new VolontaryFunctionModel();


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

        public List<VolontaryFunctionModel> GetVoluntaryFunctionAll(int Arrangement)
        {
            List<VolontaryFunctionModel> MedVolList = new List<VolontaryFunctionModel>();

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetVoluntaryFunctionAll(Arrangement);

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
                    //if (dr["idArrangement"].ToString() != "")
                    //{
                    //    MedVolMod.idcpr = Convert.ToInt32(dr["idArrangement"].ToString());
                    //}
                    //else
                    //    MedVolMod.idcpr = null;
                    //MedVolMod.txt = dr["txt"].ToString();
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

        public List<VolontaryTripModel> GetVoluntaryTrip(int idArrangement)
        {
            List<VolontaryTripModel> MedVolList = new List<VolontaryTripModel>();

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetVoluntaryTrip(idArrangement);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    VolontaryTripModel MedVolMod = new VolontaryTripModel();

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
        //18.1
        public List<ArrangementBookStatusModel> AllStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AllSatatus();

            List<ArrangementBookStatusModel> lista = new List<ArrangementBookStatusModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementBookStatusModel arrBookModel = new ArrangementBookStatusModel();

                        if (dr["idStatus"].ToString() != "")
                            arrBookModel.idStatus = Int32.Parse(dr["idStatus"].ToString());

                        arrBookModel.nameStatus = (dr["nameStatus"].ToString());


                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<ArrangementBookTravelPapersModel> AllTPapersStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AllTPapersStatus();

            List<ArrangementBookTravelPapersModel> lista = new List<ArrangementBookTravelPapersModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementBookTravelPapersModel arrBookModel = new ArrangementBookTravelPapersModel();

                        if (dr["idTravelPapers"].ToString() != "")
                            arrBookModel.idTravelPapers = Int32.Parse(dr["idTravelPapers"].ToString());

                        arrBookModel.nameTravelPapers = (dr["nameTravelPapers"].ToString());


                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<ArrTypeModel> AllArrType()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AllArrType();

            List<ArrTypeModel> lista = new List<ArrTypeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrTypeModel arrBookModel = new ArrTypeModel();

                        if (dr["idArrType"].ToString() != "")
                            arrBookModel.idArrType = Int32.Parse(dr["idArrType"].ToString());

                        arrBookModel.nameArrType = (dr["nameArrType"].ToString());


                        lista.Add(arrBookModel);

                    }
                    return lista;
                }
                else
                    return lista;
            }
            else
                return lista;
        }

        public List<PersonTelModel> TelSrtatus(int iDContPers, int idTelType)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.TelSrtatus(iDContPers, idTelType);
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
                        if (dr["idTelType"].ToString() != "")
                            model.idTelType = Int32.Parse(dr["idTelType"].ToString());
                        model.descriptionTel = (dr["descriptionTel"].ToString());
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

        public List<IModel> GetAllArangementCode()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetAllArangementCode();
            List<IModel> arr = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    ArrangementCodeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementCodeModel();

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = (dr["codeArrangement"].ToString());

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = (dr["nameArrangement"].ToString());

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public ArrangementBookModel GetTravelerIsInsurance(int idArrangement, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetTravelerIsInsurance(idArrangement, idContPers);
            ArrangementBookModel arr = new ArrangementBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                   // ArrangementCodeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        arr = new ArrangementBookModel();

                        if (dr["idArrangement"].ToString() != "")
                            arr.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            arr.idContPers = Convert.ToInt32(dr["idContPers"].ToString());

                        if (dr["firstname"].ToString() != "")
                            arr.firstname = (dr["firstname"].ToString());

                        if (dr["midname"].ToString() != "")
                            arr.midname = (dr["midname"].ToString());

                        if (dr["lastname"].ToString() != "")
                            arr.lastname = (dr["lastname"].ToString());
                        if (dr["isInsurance"].ToString() != "")
                            arr.isInsurance = Convert.ToBoolean(dr["isInsurance"].ToString());
                        if (dr["isCancelInsurance"].ToString() != "")
                            arr.isCancelInsurance = Convert.ToBoolean(dr["isCancelInsurance"].ToString());

                      //  arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<EmployeeModel> AllEmployee(DateTime dateFrom, DateTime dateTo)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.AllEmployee(dateFrom, dateTo);
            List<EmployeeModel> arr = new List<EmployeeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    EmployeeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new EmployeeModel();

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Convert.ToInt32(dr["idEmployee"].ToString());
                        model.firstNameEmployee = (dr["firstNameEmployee"].ToString());


                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //NOVO     
        public bool SaveRemaining(ArrangementRemainingModel arrModel)
        {
            bool retval = false;

            try
            {

                retval = arrBookDAO.SaveRemaining(arrModel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;

        }

        public bool UpdateRemaining(ArrangementRemainingModel arrModel)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.UpdateRemaining(arrModel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int isInArrangementRemaining(int idArr)
        {
            int idArrangement = -1;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.isInArrangementRemaining(idArr);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idArrangement"].ToString() != "")
                            idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());
                    }
                }
            }

            return idArrangement;
        }

        public List<ArrangementRemainingModel> getArrangementRemaining(int idArr)
        {

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.getArrangementRemaining(idArr);

            List<ArrangementRemainingModel> arr = new List<ArrangementRemainingModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementRemainingModel model = new ArrangementRemainingModel();

                        //if (dr["idArrangement"].ToString() != "")
                        //    model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        if (dr["awayDt"].ToString() != "")
                            model.awayDt = DateTime.Parse(dr["awayDt"].ToString());
                        if (dr["awayDt2"].ToString() != "")
                            model.awayDt2 = DateTime.Parse(dr["awayDt2"].ToString());
                        if (dr["awayAirport"].ToString() != "")
                            model.awayAirport = (dr["awayAirport"].ToString());
                        if (dr["awayAirport2"].ToString() != "")
                            model.awayAirport2 = (dr["awayAirport2"].ToString());
                        if (dr["awayFlightNr"].ToString() != "")
                            model.awayFlightNr = (dr["awayFlightNr"].ToString());
                        if (dr["awayFlightNr2"].ToString() != "")
                            model.awayFlightNr2 = (dr["awayFlightNr2"].ToString());
                        if (dr["arrivalDt"].ToString() != "")
                            model.arrivalDt = DateTime.Parse(dr["arrivalDt"].ToString());
                        if (dr["arrivalDt2"].ToString() != "")
                            model.arrivalDt2 = DateTime.Parse(dr["arrivalDt2"].ToString());
                        if (dr["arrivalAirport"].ToString() != "")
                            model.arrivalAirport = (dr["arrivalAirport"].ToString());
                        if (dr["arrivalAirport2"].ToString() != "")
                            model.arrivalAirport2 = (dr["arrivalAirport2"].ToString());
                        if (dr["backDt"].ToString() != "")
                            model.backDt = DateTime.Parse(dr["backDt"].ToString());
                        if (dr["backDt2"].ToString() != "")
                            model.backDt2 = DateTime.Parse(dr["backDt2"].ToString());
                        if (dr["backAirport"].ToString() != "")
                            model.backAirport = (dr["backAirport"].ToString());
                        if (dr["backAirport2"].ToString() != "")
                            model.backAirport2 = (dr["backAirport2"].ToString());
                        if (dr["backFlightNr"].ToString() != "")
                            model.backFlightNr = (dr["backFlightNr"].ToString());
                        if (dr["backFlightNr2"].ToString() != "")
                            model.backFlightNr2 = (dr["backFlightNr2"].ToString());
                        if (dr["arrivalDt3"].ToString() != "")
                            model.arrivalDt3 = DateTime.Parse(dr["arrivalDt3"].ToString());
                        if (dr["arrivalDt4"].ToString() != "")
                            model.arrivalDt4 = DateTime.Parse(dr["arrivalDt4"].ToString());
                        if (dr["arrivalAirport3"].ToString() != "")
                            model.arrivalAirport3 = (dr["arrivalAirport3"].ToString());
                        if (dr["arrivalAirport4"].ToString() != "")
                            model.arrivalAirport4 = (dr["arrivalAirport4"].ToString());
                        if (dr["collectTime"].ToString() != "")
                            model.collectTime = (dr["collectTime"].ToString());
                        if (dr["airportSociety"].ToString() != "")
                            model.airportSociety = (dr["airportSociety"].ToString());
                        if (dr["special"].ToString() != "")
                            model.special = (dr["special"].ToString());
                        if (dr["program"].ToString() != "")
                            model.program = (dr["program"].ToString());
                        if (dr["letter"].ToString() != "")
                            model.letter = (dr["letter"].ToString());
                        if (dr["rulesAppointment"].ToString() != "")
                            model.rulesAppointment = (dr["rulesAppointment"].ToString());



                        model.twoFlight = Convert.ToBoolean(dr["twoFlight"].ToString());

                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ArrangementRemainingModel> isCheckedTwoFlight(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.isCheckedTwoFlight(idArrangement);
            List<ArrangementRemainingModel> arr = new List<ArrangementRemainingModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    // ArrangementCodeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementRemainingModel model = new ArrangementRemainingModel();

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        model.twoFlight = Convert.ToBoolean(dr["twoFlight"].ToString());
                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public int isAutitravelChecked(int idArr)
        {
            int idArrangement = -1;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.isAutitravelChecked(idArr);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["idArrangement"].ToString() != "")
                            idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());
                    }
                }
            }

            return idArrangement;
        }
        public bool chkMinDatePriceList(int idArrangementBook)
        {
            bool chkValue = false;
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.getMinDatePriceList(idArrangementBook);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["minDate"].ToString() != "")
                            if (DateTime.Compare(DateTime.Now, DateTime.Parse(dr["minDate"].ToString())) > 0)
                            {
                                chkValue = true;

                            };
                    }
                }
            }

            return chkValue;
        }
       
        public List<ArrangementBookModel> GetPassingersForInvoicing(int idArrangementBook, int idPayInvoice)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetPassingersForInvoicing(idArrangementBook, idPayInvoice);
            List<ArrangementBookModel> arrList = new List<ArrangementBookModel>();
            ArrangementBookModel arrBookModel = new ArrangementBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        arrBookModel = new ArrangementBookModel();

                        arrBookModel.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        arrBookModel.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());
                        arrBookModel.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        arrBookModel.idStatus = Int32.Parse(dr["idStatus"].ToString());
                        arrBookModel.idTravelPapers = Int32.Parse(dr["idTravelPapers"].ToString());
                        arrBookModel.price = Decimal.Parse(dr["price"].ToString());
                        arrBookModel.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());
                        arrBookModel.isCancelInsurance = Boolean.Parse(dr["isCancelInsurance"].ToString());
                        if (dr["idBoarding"].ToString() != "")
                            arrBookModel.idBoarding = Int32.Parse(dr["idBoarding"].ToString());
                        if (dr["idUserCreated"].ToString() != "")
                            arrBookModel.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            arrBookModel.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            arrBookModel.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            arrBookModel.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                        if (dr["isMedicalDevices"].ToString() != "")
                            arrBookModel.isMedicalDevices = Boolean.Parse(dr["isMedicalDevices"].ToString());
                        if (dr["idPayInvoice"].ToString() != "")
                            arrBookModel.idPayInvoice = Int32.Parse(dr["idPayInvoice"].ToString());

                        arrList.Add(arrBookModel);

                       // break;
                    }
                    return arrList;
                }
                else
                    return arrList;
            }
            else
                return arrList;
        }

        public List<ArrangementModel> getArrangementDate(int idArr)
        {

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.getArrangementDate(idArr);

            List<ArrangementModel> arr = new List<ArrangementModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementModel model = new ArrangementModel();


                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());
                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());


                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }

        ///NOVO
        public List<MedicalVoluntaryQuestModel> GetSkillForPerson(int idContPerson)
        {

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetSkillForPerson(idContPerson);

            List<MedicalVoluntaryQuestModel> arr = new List<MedicalVoluntaryQuestModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MedicalVoluntaryQuestModel model = new MedicalVoluntaryQuestModel();


                        if (dr["idQuestSkills"].ToString() != "")
                            model.idQuest = Int32.Parse(dr["idQuestSkills"].ToString());

                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public bool MedLookupSriptSave(int idContPers, int idArrangement, List<MedicalVoluntaryQuestModel> idQuest)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.MedLookupSriptSave(idContPers, idArrangement, idQuest);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<MedicalVoluntaryQuestModel> GetNrForSkillsArrangement(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetNrForSkillsArrangement(idArrangement);

            List<MedicalVoluntaryQuestModel> arr = new List<MedicalVoluntaryQuestModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MedicalVoluntaryQuestModel model = new MedicalVoluntaryQuestModel();


                        if (dr["idQuest"].ToString() != "")
                            model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        if (dr["idAns"].ToString() != "")
                            model.idAns = Int32.Parse(dr["idAns"].ToString());
                        //broj skilova:
                        if (dr["nr"].ToString() != "")
                            model.nameQuestGroup = (dr["nr"].ToString());


                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<MedicalVoluntaryQuestModel> GetAllSkillsVolArr(int idArrangement)
        {

            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetAllSkillsVolArr(idArrangement);

            List<MedicalVoluntaryQuestModel> arr = new List<MedicalVoluntaryQuestModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MedicalVoluntaryQuestModel model = new MedicalVoluntaryQuestModel();


                        if (dr["idQuest"].ToString() != "")
                            model.idQuest = Int32.Parse(dr["idQuest"].ToString());
                        if (dr["idArr"].ToString() != "")
                            model.idQuestGroup = Int32.Parse(dr["idArr"].ToString());

                        if (dr["txt"].ToString() != "")
                            model.nameQuestGroup = (dr["txt"].ToString());


                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public bool SaveVolArr(MedicalVoluntaryQuestModel listSkill, int Arr)
        {


            bool retval1 = false;
            try
            {

                retval1 = arrBookDAO.SaveVolArr(listSkill, Arr);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval1;
        }
        public bool UpdateVolArr(MedicalVoluntaryQuestModel listSkill, string txt, int idQuest, int Arr)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.UpdateVolArr(listSkill, txt, idQuest, Arr);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<MedicalVoluntaryQuestModel> GetSkillsForArrAndPerson(int idArrangement, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookDAO.GetSkillsForArrAndPerson(idArrangement, idContPers);

            List<MedicalVoluntaryQuestModel> arr = new List<MedicalVoluntaryQuestModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MedicalVoluntaryQuestModel model = new MedicalVoluntaryQuestModel();


                        if (dr["idQuestSkill"].ToString() != "")
                            model.idQuest = Int32.Parse(dr["idQuestSkill"].ToString());

                        arr.Add(model);
                    }
                    return arr;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public bool DeletePrsonFromMedLookup(int idArrangement, int idContPers)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.DeletePrsonFromMedLookup(idArrangement, idContPers);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteVolArr(int idArr, int idQuest)
        {
            bool retval = false;
            try
            {

                retval = arrBookDAO.DeleteVolArr(idArr, idQuest);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}

  