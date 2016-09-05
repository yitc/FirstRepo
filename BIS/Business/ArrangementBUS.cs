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
    public class ArrangementBUS
    {
        private ArrangementDAO arrangeDAO;
        private LabelDAO labelDAO;

        public ArrangementBUS()
        {
            arrangeDAO = new ArrangementDAO();
            labelDAO = new LabelDAO();
        }

        public int Save(ArrangementModel arrangemodel, string nameForm, int idUser)
        {
            int retval = -1;
            try
            {

                retval = arrangeDAO.Save(arrangemodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ArrangementModel arrangemodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeDAO.Update(arrangemodel, nameForm, idUser);

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

                retval = arrangeDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetAllArrangements()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangements();
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["codeProject"].ToString() != "")
                            model.codeProject = dr["codeProject"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["nrOfNights"].ToString() != "")
                            model.nrOfNights = Int32.Parse(dr["nrOfNights"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryNameArrangement"].ToString() != "")
                            model.countryNameArrangement = dr["countryNameArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());
                       
                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        if (dr["isWeb"].ToString() != "")
                             model.isWeb = Boolean.Parse(dr["isWeb"].ToString());

                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());

                      
                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());


                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["booked"].ToString() != "")
                            model.booked = Int32.Parse(dr["booked"].ToString());

                        if (dr["optionalBooked"].ToString() != "")
                            model.optionalBooked = Int32.Parse(dr["optionalBooked"].ToString());

                        if (dr["freePlaces"].ToString() != "")
                            model.freePlaces = Int32.Parse(dr["freePlaces"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();  

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

        public List<IModel> GetAllArrangementsAccount(string bookyear)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementsAccount(bookyear);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["nrOfNights"].ToString() != "")
                            model.nrOfNights = Int32.Parse(dr["nrOfNights"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryNameArrangement"].ToString() != "")
                            model.countryNameArrangement = dr["countryNameArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        if (dr["isWeb"].ToString() != "")
                            model.isWeb = Boolean.Parse(dr["isWeb"].ToString());

                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());


                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());


                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["booked"].ToString() != "")
                            model.booked = Int32.Parse(dr["booked"].ToString());

                        if (dr["optionalBooked"].ToString() != "")
                            model.optionalBooked = Int32.Parse(dr["optionalBooked"].ToString());

                        if (dr["freePlaces"].ToString() != "")
                            model.freePlaces = Int32.Parse(dr["freePlaces"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["codeProject"].ToString() != "")
                            model.codeProject = dr["codeProject"].ToString();

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
        //==
        public ArrangementModel GetArrangementCodeProject(string code)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementCodeProject(code);
            ArrangementModel arrange = new ArrangementModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();


                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["codeProject"].ToString() != "")
                            model.codeProject = dr["codeProject"].ToString();

                        //arrange.Add(model);
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }
        //==
        public List<ArrangementHotelServiceModel> GetArrangementHotelService()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementHotelService();
            List<ArrangementHotelServiceModel> arrange = new List<ArrangementHotelServiceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementHotelServiceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementHotelServiceModel();


                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Convert.ToInt32(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        

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

        public List<IModel> GetAllArrangementsNotInActiveContracts(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementsNotInActiveContracts(idClient);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryNameArrangement"].ToString() != "")
                            model.countryNameArrangement = dr["countryNameArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        if (dr["isWeb"].ToString() != "")
                            model.isWeb = Boolean.Parse(dr["isWeb"].ToString());

                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());

                    
                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());


                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();  

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

        public Boolean checkIfArrangement(int idClient, int idArrangement, int idPriceList)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.checkIfArrangement(idClient, idArrangement, idPriceList);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    return true;

                }
                else
                    return false;
            }
            else
                return false;
        }

        public List<IModel> GetAllArrangementsMainGrid(int idFilter, List<int> labels, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementsMainGrid(idFilter, labels, idLang);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["nrOfNights"].ToString() != "")
                            model.nrOfNights = Int32.Parse(dr["nrOfNights"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryNameArrangement"].ToString() != "")
                            model.countryNameArrangement = dr["countryNameArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        if (dr["isWeb"].ToString() != "")
                            model.isWeb = Boolean.Parse(dr["isWeb"].ToString());

                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());

                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["descAgeCategory"].ToString() != "")
                            model.descAgeCategory = dr["descAgeCategory"].ToString();

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());

                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["booked"].ToString() != "")
                            model.booked = Int32.Parse(dr["booked"].ToString());

                        if (dr["optionalBooked"].ToString() != "")
                            model.optionalBooked = Int32.Parse(dr["optionalBooked"].ToString());

                        if (dr["freePlaces"].ToString() != "")
                            model.freePlaces = Int32.Parse(dr["freePlaces"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["price"].ToString() != "")
                            model.price = Decimal.Parse(dr["price"].ToString());

                        if (dr["daysFirstPayment"].ToString() != "")
                            model.daysFirstPayment = Int32.Parse(dr["daysFirstPayment"].ToString());

                        if (dr["daysLastPayment"].ToString() != "")
                            model.daysLastPayment = Int32.Parse(dr["daysLastPayment"].ToString());

                        if (dr["percentFirstPayment"].ToString() != "")
                            model.percentFirstPayment = Decimal.Parse(dr["percentFirstPayment"].ToString());

                        if (dr["reservationCosts"].ToString() != "")
                            model.reservationCosts = Decimal.Parse(dr["reservationCosts"].ToString());

                        if (dr["nrAnchorage"].ToString() != "")
                            model.nrAnchorage = Int32.Parse(dr["nrAnchorage"].ToString());

                        if (dr["invoiceDescription"].ToString() != "")
                            model.invoiceDescription = dr["invoiceDescription"].ToString();

                        if (dr["idClientInvoice"].ToString() != "")
                            model.idClientInvoice = Int32.Parse(dr["idClientInvoice"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

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

        public ArrangementModel GetArrangementByCode(string codeArr)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementByCode(codeArr);
            ArrangementModel arrange = new ArrangementModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();


                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());


                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["isWeb"].ToString() != "")
                             model.isWeb = Boolean.Parse(dr["isWeb"].ToString());
                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());


                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());

                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());
                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["daysFirstPayment"].ToString() != "")
                            model.daysFirstPayment = Int32.Parse(dr["daysFirstPayment"].ToString());

                        if (dr["daysLastPayment"].ToString() != "")
                            model.daysLastPayment = Int32.Parse(dr["daysLastPayment"].ToString());

                        if (dr["percentFirstPayment"].ToString() != "")
                            model.percentFirstPayment = Decimal.Parse(dr["percentFirstPayment"].ToString());

                        if (dr["reservationCosts"].ToString() != "")
                            model.reservationCosts = Decimal.Parse(dr["reservationCosts"].ToString());

                        if (dr["codeProject"].ToString() != "")
                            model.codeProject = dr["codeProject"].ToString();

                        if (dr["idClientInvoice"].ToString() != "")
                            model.idClientInvoice = Int32.Parse(dr["idClientInvoice"].ToString());

                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public ArrangementModel GetArrangementById(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementById(idArrangement);
            ArrangementModel arrange = new ArrangementModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["isWeb"].ToString() != "")
                            model.isWeb = Boolean.Parse(dr["isWeb"].ToString());


                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());

                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());

                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["daysFirstPayment"].ToString() != "")
                            model.daysFirstPayment = Int32.Parse(dr["daysFirstPayment"].ToString());

                        if (dr["daysLastPayment"].ToString() != "")
                            model.daysLastPayment = Int32.Parse(dr["daysLastPayment"].ToString());

                        if (dr["percentFirstPayment"].ToString() != "")
                            model.percentFirstPayment = Decimal.Parse(dr["percentFirstPayment"].ToString());

                        if (dr["reservationCosts"].ToString() != "")
                            model.reservationCosts = Decimal.Parse(dr["reservationCosts"].ToString());
                        //Aleksa i Mitar {country name, arrangament type field werent filled with data}
                        if (dr["countryName"].ToString() != "")
                            model.countryNameArrangement = dr["countryName"].ToString();
                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["codeProject"].ToString() != "")
                            model.codeProject = dr["codeProject"].ToString();

                        if (dr["idClientInvoice"].ToString() != "")
                            model.idClientInvoice = Int32.Parse(dr["idClientInvoice"].ToString());
                        //Aleksa i Mitar
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public ArrangementModel GetArrangementByArrangementBook(int idArrangementBook)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementByArrangementBook(idArrangementBook);
            ArrangementModel arrange = new ArrangementModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["isWeb"].ToString() != "")
                            model.isWeb = Boolean.Parse(dr["isWeb"].ToString());


                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());

                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());

                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["daysFirstPayment"].ToString() != "")
                            model.daysFirstPayment = Int32.Parse(dr["daysFirstPayment"].ToString());

                        if (dr["daysLastPayment"].ToString() != "")
                            model.daysLastPayment = Int32.Parse(dr["daysLastPayment"].ToString());

                        if (dr["percentFirstPayment"].ToString() != "")
                            model.percentFirstPayment = Decimal.Parse(dr["percentFirstPayment"].ToString());

                        if (dr["reservationCosts"].ToString() != "")
                            model.reservationCosts = Decimal.Parse(dr["reservationCosts"].ToString());
                        //Aleksa i Mitar {country name, arrangament type field werent filled with data}
                        if (dr["countryName"].ToString() != "")
                            model.countryNameArrangement = dr["countryName"].ToString();
                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["codeProject"].ToString() != "")
                            model.codeProject = dr["codeProject"].ToString();

                        //Aleksa i Mitar
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<LabelForArrangement> GetLabelsArrangement(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetLabelsArrangement(idArrangement);
            List<LabelForArrangement> arrLabels = new List<LabelForArrangement>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelForArrangement model = new LabelForArrangement();

                        if (dr["idLabel"].ToString() != "")
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());


                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        arrLabels.Add(model);
                    }
                    return arrLabels;
                }
                else
                    return arrLabels;
            }
            else
                return arrLabels;

        }

        public bool SaveLabel(LabelForArrangement label, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = labelDAO.insertArrangementLabels(label,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteLabel(int idArrangement, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = labelDAO.deleteArrangementLabels(idArrangement,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<ArrangementModel> IsIn(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.IsIn(idArrangement);
            List<ArrangementModel> arrange = new List<ArrangementModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());


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
        //NOVO 
        public List<IModel> GeArrangementsLookup()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GeArrangementsLookup();
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementLookupModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementLookupModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["codeArrangement"].ToString() != "")
                            model.codeArrangement = dr["codeArrangement"].ToString();

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        if (dr["nrOfNights"].ToString() != "")
                            model.nrOfNights = Int32.Parse(dr["nrOfNights"].ToString());

                        if (dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryNameArrangement"].ToString() != "")
                            model.countryNameArrangement = dr["countryNameArrangement"].ToString();

                        if (dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Int32.Parse(dr["countryArrangement"].ToString());

                        if (dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Int32.Parse(dr["typeArrangement"].ToString());

                        if (dr["typeNameArrangement"].ToString() != "")
                            model.typeNameArrangement = dr["typeNameArrangement"].ToString();

                        if (dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Int32.Parse(dr["nrTraveler"].ToString());

                        if (dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Int32.Parse(dr["minNrTraveler"].ToString());

                        if (dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Int32.Parse(dr["nrVoluntaryHelper"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        if (dr["isWeb"].ToString() != "")
                            model.isWeb = Boolean.Parse(dr["isWeb"].ToString());

                        if (dr["buRollators"].ToString() != "")
                            model.buRollators = Int32.Parse(dr["buRollators"].ToString());

                        if (dr["nrMaleVoluntary"].ToString() != "")
                            model.nrMaleVoluntary = Int32.Parse(dr["nrMaleVoluntary"].ToString());

                        if (dr["idAgeCategory"].ToString() != "")
                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());

                        if (dr["descAgeCategory"].ToString() != "")
                            model.descAgeCategory = dr["descAgeCategory"].ToString();

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());

                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["daysFirstPayment"].ToString() != "")
                            model.daysFirstPayment = Int32.Parse(dr["daysFirstPayment"].ToString());

                        if (dr["daysLastPayment"].ToString() != "")
                            model.daysLastPayment = Int32.Parse(dr["daysLastPayment"].ToString());

                        if (dr["percentFirstPayment"].ToString() != "")
                            model.percentFirstPayment = Decimal.Parse(dr["percentFirstPayment"].ToString());

                        if (dr["reservationCosts"].ToString() != "")
                            model.reservationCosts = Decimal.Parse(dr["reservationCosts"].ToString());

                        if (dr["nrAnchorage"].ToString() != "")
                            model.nrAnchorage = Int32.Parse(dr["nrAnchorage"].ToString());

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

    }
}