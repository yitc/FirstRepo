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
    public class ArticalBUS
    {
        private ArticalDAO articalDAO;

        public ArticalBUS()
        {
            articalDAO = new ArticalDAO();
        }

        public List<IModel> GetAllArticals()
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllArticals();
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalModel model = new ArticalModel();

                        model.codeArtical = dr["codeArtical"].ToString();
                        model.nameArtical = dr["nameArtical"].ToString();
                        model.codeArtikalGroup = dr["codeArtikalGroup"].ToString();
                        model.nameArtikalGroup = dr["nameArticalGroup"].ToString();

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Convert.ToInt32(dr["quantity"].ToString());


                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModifies"].ToString() != "")
                            model.idUserModifies = Int32.Parse(dr["idUserModifies"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                        if (dr["isOptional"].ToString() != "")
                            model.isOptional = Convert.ToBoolean(dr["isOptional"].ToString());


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




        public ArticalModel GetArticalByID(string codeArtical)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetArticalByID(codeArtical);
            ArticalModel model = new ArticalModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {                        
                        model.codeArtical = dr["codeArtical"].ToString();
                        model.nameArtical = dr["nameArtical"].ToString();
                        model.codeArtikalGroup = dr["codeArtikalGroup"].ToString();

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Convert.ToInt32(dr["quantity"].ToString());

                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModifies"].ToString() != "")
                            model.idUserModifies = Int32.Parse(dr["idUserModifies"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());
                        
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public bool Save(ArticalModel model,string nameForm,int idUser)
        {
            bool retval = false;
            try
            {

                retval = articalDAO.Save(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public bool Update(ArticalModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = articalDAO.Update(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Delete(string id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = articalDAO.Delete(id, nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


        public List<ArrangementRoomsArticle> GetAllBookedRoomsForArrangement(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllBookedRoomsForArrangement(idArrangement);
            List<ArrangementRoomsArticle> modellista = new List<ArrangementRoomsArticle>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    ArrangementRoomsArticle model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementRoomsArticle();
                        if (dr["idRoom"].ToString() != "")
                            model.idRoom = dr["idRoom"].ToString();
                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();
                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();
                        if (dr["isContract"].ToString() != "")
                            model.isContract = Boolean.Parse(dr["isContract"].ToString());
                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        modellista.Add(model);
                    }
                    return modellista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ArrangementArticalModel> GetAllArticalsForArrangemetAccomodation(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllArticalsForArrangemetAccomodation(idArrangement);
            List<ArrangementArticalModel> arrange = new List<ArrangementArticalModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementArticalModel model = new ArrangementArticalModel();

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
        public List<ArrangementArticalModel_Rooms> GetAllArticalsForArrangemetAccomodation1(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllArticalsForArrangemetAccomodationSAKI(idArrangement);     // orig GetAllArticalsForArrangemetAccomodation1
            List<ArrangementArticalModel_Rooms> arrange = new List<ArrangementArticalModel_Rooms>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementArticalModel_Rooms model = new ArrangementArticalModel_Rooms();

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

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());


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

        public List<ArrangementArticalModel_RoomsUpdate> GetAllArticalsForUpdateArrangementAccomodation(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllArticalsForUpdateArrangementAccomodation(idArrangement);     // orig GetAllArticalsForArrangemetAccomodation1
            List<ArrangementArticalModel_RoomsUpdate> arrange = new List<ArrangementArticalModel_RoomsUpdate>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementArticalModel_RoomsUpdate model = new ArrangementArticalModel_RoomsUpdate();

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

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["nrLast"].ToString() != "")
                            model.nrLast = dr["nrLast"].ToString();


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


        public Boolean checkIfArticleIsInExtraCalculation(int idArrangement, string idArticle)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.checkIfArticleIsInExtraCalculation(idArrangement, idArticle);
            Boolean result = false;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }
            return result;
        }
        // neta 4.12
        //public List<ArrangementRoomsArticle> GetAllArticalsRoomsForArrangement(int idArrangement)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = articalDAO.GetAllArticalsRoomsForArrangement(idArrangement);
        //    List<ArrangementRoomsArticle> modellista = new List<ArrangementRoomsArticle>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            //    DocumentsModel model = new DocumentsModel();
        //            ArrangementRoomsArticle model = null;
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                model = new ArrangementRoomsArticle();
        //                if (dr["idRoom"].ToString() != "")
        //                    model.idRoom = dr["idRoom"].ToString();
        //                if (dr["idArticle"].ToString() != "")
        //                    model.idArticle = dr["idArticle"].ToString();
        //                if (dr["nameArticle"].ToString() != "")
        //                    model.nameArticle = dr["nameArticle"].ToString();                        
        //                if (dr["isContract"].ToString() != "")
        //                    model.isContract = Boolean.Parse(dr["isContract"].ToString());
        //                if (dr["id"].ToString() != "")
        //                    model.id = Int32.Parse(dr["id"].ToString());
        //                if (dr["idContPers"].ToString() != "")
        //                    model.idContPers = Int32.Parse(dr["idContPers"].ToString());
        //                if (dr["name"].ToString() != "")
        //                    model.name = dr["name"].ToString();
        //                if (dr["nameGender"].ToString() != "")
        //                    model.nameGender = dr["nameGender"].ToString();

        //                modellista.Add(model);
        //            }
        //            return modellista;
        //        }
        //        else
        //            return null;
        //    }
        //    else
        //        return null;
        //}
        // neta 4.12
        //public List<ArrangementRoomsArticle> GetAllArticalsRoomsForArrangement(int idArrangement)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = articalDAO.GetAllArticalsRoomsForArrangement(idArrangement);
        //    List<ArrangementRoomsArticle> modellista = new List<ArrangementRoomsArticle>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            //    DocumentsModel model = new DocumentsModel();
        //            ArrangementRoomsArticle model = null;
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                model = new ArrangementRoomsArticle();
        //                if (dr["idRoom"].ToString() != "")
        //                    model.idRoom = dr["idRoom"].ToString();
        //                if (dr["idArticle"].ToString() != "")
        //                    model.idArticle = dr["idArticle"].ToString();
        //                if (dr["nameArticle"].ToString() != "")
        //                    model.nameArticle = dr["nameArticle"].ToString();
        //                if (dr["isContract"].ToString() != "")
        //                    model.isContract = Boolean.Parse(dr["isContract"].ToString());
        //                if (dr["id"].ToString() != "")
        //                    model.id = Int32.Parse(dr["id"].ToString());
        //                if (dr["idContPers"].ToString() != "")
        //                    model.idContPers = Int32.Parse(dr["idContPers"].ToString());
        //                if (dr["name"].ToString() != "")
        //                    model.name = dr["name"].ToString();
        //                if (dr["nameGender"].ToString() != "")
        //                    model.nameGender = dr["nameGender"].ToString();
        //                if (dr["type"].ToString() != "")
        //                    model.type = dr["type"].ToString();

        //                modellista.Add(model);
        //            }
        //            return modellista;
        //        }
        //        else
        //            return null;
        //    }
        //    else
        //        return null;
        //}
        public List<ArrangementRoomsArticle> GetAllArticalsRoomsForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllArticalsRoomsForArrangement(idArrangement, idLang);
            List<ArrangementRoomsArticle> modellista = new List<ArrangementRoomsArticle>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    ArrangementRoomsArticle model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementRoomsArticle();
                        if (dr["idRoom"].ToString() != "")
                            model.idRoom = dr["idRoom"].ToString();
                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();
                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();
                        if (dr["isContract"].ToString() != "")
                            model.isContract = Boolean.Parse(dr["isContract"].ToString());
                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["name"].ToString() != "")
                            model.name = dr["name"].ToString();
                        if (dr["nameGender"].ToString() != "")
                            model.nameGender = dr["nameGender"].ToString();
                        if (dr["type"].ToString() != "")
                            model.type = dr["type"].ToString();

                        modellista.Add(model);
                    }
                    return modellista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ArrangementArticalForBookPersonModel> GetAllArticalsForBookingLookup(int idContPers, int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetAllArticalsForBookingLookup(idContPers, idArrangement);
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

                        if (dr["idRoom"].ToString() != "")
                            model.idRoom = dr["idRoom"].ToString();

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

        public List<ArticalExtraOptionalModel> GetExtraOptionalData(int idArrangement)
        {

            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetExtraOptionalData(idArrangement);
            List<ArticalExtraOptionalModel> arrange = new List<ArticalExtraOptionalModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalExtraOptionalModel model = new ArticalExtraOptionalModel();

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtical = (dr["nameArtical"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Convert.ToDecimal(dr["sellingPrice"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isOptional"].ToString() != "")
                            model.isOptional = Convert.ToBoolean(dr["isOptional"].ToString());




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

        public List<ArticalExtraOptionalModel> GetArrangementBookOptionalForPerson(int idArrangementBook)
        {
            //ao.idArrangementBook,ao.idArticle,ao.sellingPrice,ao.isExtra,ao.isOptional
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetArrangementBookOptionalForPerson(idArrangementBook);
            List<ArticalExtraOptionalModel> arrange = new List<ArticalExtraOptionalModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalExtraOptionalModel model = new ArticalExtraOptionalModel();

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["idArrBook"].ToString() != "")
                            model.arranementBookID = Int32.Parse(dr["idArrBook"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Convert.ToDecimal(dr["sellingPrice"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isOptional"].ToString() != "")
                            model.isOptional = Convert.ToBoolean(dr["isOptional"].ToString());




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

        public List<ArticalExtraOptionalModel> GetArrangementBookOptionalForTravelers(List<int> idArrangementBook)
        {
            //ao.idArrangementBook,ao.idArticle,ao.sellingPrice,ao.isExtra,ao.isOptional
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetArrangementBookOptionalForTravelers(idArrangementBook);
            List<ArticalExtraOptionalModel> arrange = new List<ArticalExtraOptionalModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalExtraOptionalModel model = new ArticalExtraOptionalModel();

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                       
                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Convert.ToDecimal(dr["sellingPrice"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isOptional"].ToString() != "")
                            model.isOptional = Convert.ToBoolean(dr["isOptional"].ToString());




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

        public int numberOptionalArticle(List<int> idArrangementBook,string idArticle, decimal sellingPrice)
        {
            int result = 1;
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.numberOptionalArticle(idArrangementBook, idArticle, sellingPrice);
           

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["quantity"].ToString() != "")
                            result = Convert.ToInt32(dr["quantity"].ToString());

                    }

                }
            }
            return result;
        }

        public int numberExtraArticle(List<int> idArrangementBookList, int idArrangement,int MinNumTravelers,string idArticle, decimal sellingPrice)
        {
            int result = 1;
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.numberExtraArticle(idArrangementBookList,idArrangement,MinNumTravelers, idArticle, sellingPrice);
            

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["quantity"].ToString() != "")
                            result = Convert.ToInt32(dr["quantity"].ToString());

                    }

                }
            }
            return result;
        }

        public int GetArrangementBookID(int idContPers, int idArrangement)
        {
            int ID = -1;
            //ao.idArrangementBook,ao.idArticle,ao.sellingPrice,ao.isExtra,ao.isOptional
            DataTable dataTable = new DataTable();
            dataTable = articalDAO.GetArrangementBookID(idContPers, idArrangement);


            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalExtraOptionalModel model = new ArticalExtraOptionalModel();

                        if (dr["arranementBookID"].ToString() != "")
                            ID = Int32.Parse(dr["arranementBookID"].ToString());


                    }

                }

            }
            return ID;
        }

        public bool DeleteSaveScript(int iID, List<ArticalExtraOptionalModel> model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = articalDAO.DeleteSaveScript(iID, model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
