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
    public class PersonBUS
    {
        private PersonDAO personDAO;
        private FiltersDAO filterDAO;
        private LabelDAO labelDAO;

        public PersonBUS()
        {
            personDAO = new PersonDAO();
            filterDAO = new FiltersDAO();
            labelDAO = new LabelDAO();
        }


        public bool Save(PersonModel person, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personDAO.Save(person, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveFilter(FilterForPerson filters, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = filterDAO.insertFilters(filters, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveLabel(LabelForPerson label, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = labelDAO.insertLabels(label,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(PersonModel person, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personDAO.Update(person, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteFilter(int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = filterDAO.deleteFilters(idContPers, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteLabel(int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = labelDAO.deleteLabels(idContPers,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public object GetPersonImage(int idContPers)
        {
            object retval = null;
            try
            {
                retval = personDAO.GetPersonImage(idContPers);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetPersonsNoFilter()
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetPersonsNoFilter();
            List<IModel> persons = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

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

        public PersonModel GetPerson(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetPerson(idPerson);
            PersonModel persons = new PersonModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PersonModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();
                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());
                        if (dr["nameTitle"].ToString() != "")
                            model.nameTitle = dr["nameTitle"].ToString();
                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
                        model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());
                        if (dr["oldTripCount"].ToString() != "")
                            model.oldTripCount = Int32.Parse(dr["oldTripCount"].ToString());

                        model.travelInsurance = dr["travelInsurance"].ToString();
                        model.polisNumber = dr["polisNumber"].ToString();
                        model.alarmNumber = dr["alarmNumber"].ToString();

                        if (dr["idContPersBookingTo"].ToString() != "")
                        model.idContPersBookingTo = Int32.Parse(dr["idContPersBookingTo"].ToString());

                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetPersonsButThis(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetPersonsButThis(idContPers);
            List<IModel> persons = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PersonShortModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonShortModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        persons.Add(model);
                    }
                }
            }
            return persons;
        }

        public int GeLastPersonID()
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetLastPersonID();
            return Int32.Parse(dataTable.Rows[0]["idContPers"].ToString());
        }

        public List<IModel> GetAllPersons(int idFilter, List<int> labels, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetAllPersons(idFilter, labels, idLang);
            List<IModel> persons = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
                        model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());
                        if (dr["oldTripCount"].ToString() != "")
                            model.oldTripCount = Int32.Parse(dr["oldTripCount"].ToString());

                        model.travelInsurance = dr["travelInsurance"].ToString();
                        model.polisNumber = dr["polisNumber"].ToString();
                        model.alarmNumber = dr["alarmNumber"].ToString();
                        model.postalCode = dr["postalCode"].ToString();
                        model.city = dr["city"].ToString();

                        if (dr["idContPersBookingTo"].ToString() != "")
                            model.idContPersBookingTo = Int32.Parse(dr["idContPersBookingTo"].ToString());
                        if (dr["dtOfActive"].ToString() != "")
                            model.dtOfActive = DateTime.Parse(dr["dtOfActive"].ToString());
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

        public List<FilterForPerson> GetFiltersPerson(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetFiltersPerson(idContPers);
            List<FilterForPerson> personsFilters = new List<FilterForPerson>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        FilterForPerson model = new FilterForPerson();

                        if (dr["idFilter"].ToString() != "")
                            model.idFilter = Int32.Parse(dr["idFilter"].ToString());


                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        personsFilters.Add(model);
                    }
                    return personsFilters;
                }
                else
                    return personsFilters;
            }
            else
                return personsFilters;

        }

        public List<LabelForPerson> GetLabelsPerson(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetLabelsPerson(idContPers);
            List<LabelForPerson> personsLabels = new List<LabelForPerson>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LabelForPerson model = new LabelForPerson();

                        if (dr["idLabel"].ToString() != "")
                            model.idLabel = Int32.Parse(dr["idLabel"].ToString());


                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        personsLabels.Add(model);
                    }
                    return personsLabels;
                }
                else
                    return personsLabels;
            }
            else
                return personsLabels;

        }

        //public List<PersonBookModel> GetAllPersonsForArrangement(int idArrangement, string idLang)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = personDAO.GetAllPersonsForArrangement(idArrangement, idLang);
        //    List<PersonBookModel> persons = new List<PersonBookModel>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                PersonBookModel model = new PersonBookModel();


        //                model.idContPers = Int32.Parse(dr["idContPers"].ToString());
        //                model.initialsContPers = dr["initialsContPers"].ToString();
        //                model.firstname = dr["firstname"].ToString();
        //                model.midname = dr["midname"].ToString();
        //                model.lastname = dr["lastname"].ToString();
        //                model.maidenname = dr["maidenname"].ToString();

        //                if (dr["idTitle"].ToString() != "")
        //                    model.idTitle = Int32.Parse(dr["idTitle"].ToString());

        //                model.nameTitle = dr["nameTitle"].ToString();

        //                if (dr["idGender"].ToString() != "")
        //                    model.idGender = Int32.Parse(dr["idGender"].ToString());

        //                model.nameGender = dr["nameGender"].ToString();

        //                if (dr["birthdate"].ToString() != "")
        //                    model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

        //                if (dr["dtCreated"].ToString() != "")
        //                    model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

        //                if (dr["idUserCreated"].ToString() != "")
        //                    model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

        //                if (dr["dtModified"].ToString() != "")
        //                    model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

        //                if (dr["idUserModified"].ToString() != "")
        //                    model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

        //                if (dr["idUserResponsible"].ToString() != "")
        //                    model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

        //                model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
        //                model.isActive = Boolean.Parse(dr["isActive"].ToString());
        //                model.isDied = Boolean.Parse(dr["isDied"].ToString());

        //                if (dr["dtOfDeath"].ToString() != "")
        //                    model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

        //                model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
        //                model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

        //                if (dr["identBSN"].ToString() != "")
        //                    model.identBSN = dr["identBSN"].ToString();
        //                model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
        //                model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
        //                model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
        //                model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
        //                if (dr["idClient"].ToString() != "")
        //                    model.idClient = Int32.Parse(dr["idClient"].ToString());
        //                if (dr["livesIn"].ToString() != "")
        //                    model.livesIn = Int32.Parse(dr["livesIn"].ToString());
        //                if (dr["idCpFunction"].ToString() != "")
        //                    model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
        //                model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());

        //                if (dr["nameStatus"].ToString() != "")
        //                    model.nameStatus = dr["nameStatus"].ToString();

        //                if (dr["type"].ToString() != "")
        //                    model.type = dr["type"].ToString();

        //                if (dr["dtBooked"].ToString() != "")
        //                    model.dtBooked = Convert.ToDateTime(dr["dtBooked"].ToString());
        //                if (dr["isInsurance"].ToString() != "")
        //                    model.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());

        //                persons.Add(model);
        //            }
        //            return persons;
        //        }
        //        else
        //            return persons;
        //    }
        //    else
        //        return persons;
        //}

        public List<PersonBookModel> GetAllPersonsForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetAllPersonsForArrangement(idArrangement, idLang);
            List<PersonBookModel> persons = new List<PersonBookModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonBookModel model = new PersonBookModel();


                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["nameUser"].ToString() != "")
                            model.nameUserCreated = dr["nameUser"].ToString();

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
                        model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());

                        if (dr["idStatus"].ToString() != "")
                            model.idStatus = Int32.Parse(dr["idStatus"].ToString());

                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();

                        if (dr["type"].ToString() != "")
                            model.type = dr["type"].ToString();

                        if (dr["dtBooked"].ToString() != "")
                            model.dtBooked = Convert.ToDateTime(dr["dtBooked"].ToString());
                        if (dr["isInsurance"].ToString() != "")
                            model.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());

                        if (dr["idArrangementBook"].ToString() != "")
                            model.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());


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

        public PersonBookModel GetExactPersonsForArrangement(int idArrangementBook, string idLang, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetExactPersonsForArrangement(idArrangementBook, idLang, idContPers);
            PersonBookModel model = new PersonBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {



                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["nameUser"].ToString() != "")
                            model.nameUserCreated = dr["nameUser"].ToString();

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
                        model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());

                        if (dr["idStatus"].ToString() != "")
                            model.idStatus = Int32.Parse(dr["idStatus"].ToString());

                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();

                        if (dr["type"].ToString() != "")
                            model.type = dr["type"].ToString();

                        if (dr["dtBooked"].ToString() != "")
                            model.dtBooked = Convert.ToDateTime(dr["dtBooked"].ToString());
                        if (dr["idArrangementBook"].ToString() != "")
                            model.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());
                        if (dr["isInsurance"].ToString() != "")
                            model.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());


                        break;
                        //persons.Add(model);
                    }

                }
            }
            return model;
        }

        public List<PersonModel> GetArrangementPersonsLookup(int idArrangement, int idContPers, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetArrangementPersonsLookup(idArrangement, idContPers, idLang);
            List<PersonModel> persons = new List<PersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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

        public List<PersonModel> GetArrangementVHPersonsLookup(int idArrangement, int idContPers, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetArrangementVHPersonsLookup(idArrangement, idContPers, idLang);
            List<PersonModel> persons = new List<PersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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

        public List<PersonModel> GetArrangementPersons(int idArrangementBook, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetArrangementPersons(idArrangementBook, idLang);
            List<PersonModel> persons = new List<PersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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

        public List<PersonModel> GetArrangementVHPersons(int idArrangementBook, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetArrangementVHPersons(idArrangementBook, idLang);
            List<PersonModel> persons = new List<PersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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

        public List<IModel> GetVHPersons(int idArrangement, int idContPers, string idLang, List<int> CheckedFun, List<int> CheckedQuest, List<int> CheckedTrip, List<int> CheckedTripQuest, List<int> CheckedVol, List<int> CheckedVolQuest)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetVHPersons(idArrangement, idContPers, idLang, CheckedFun, CheckedQuest, CheckedTrip, CheckedTripQuest, CheckedVol, CheckedVolQuest);
            List<IModel> persons = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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

        public int GetNrPersonsVHForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetPersonsVHForArrangement(idArrangement, idLang);
            int i = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    i = dataTable.Rows.Count;

                }
            }
            return i;
        }
        //=== male VH =================
        public int GetNrMalePersonsVHForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetMalePersonsVHForArrangement(idArrangement, idLang);
            int i = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    i = dataTable.Rows.Count;

                }
            }
            return i;
        }
        //==================

        public List<IModel> GetAllArrangement(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetAllArrangement(idContPers);
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

        public List<ArrangementAllModel> GetArrangementsForPerson(int idPerson)
        {
            DataTable dataTable = new DataTable();

            dataTable = personDAO.GetArrangementsForPerson(idPerson);
            List<ArrangementAllModel> menu = new List<ArrangementAllModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementAllModel model = new ArrangementAllModel();
                        if (dr["idArrangement"] != null)
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"]);

                        model.codeArrangement = dr["codeArrangement"].ToString();
                        model.nameArrangement = (dr["nameArrangement"].ToString());

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

                        model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["countryArrangement"] != null && dr["countryArrangement"].ToString() != "")
                            model.countryArrangement = Convert.ToInt32(dr["countryArrangement"]);

                        if (dr["cityArrangement"] != null && dr["cityArrangement"].ToString() != "")
                            model.cityArrangement = dr["cityArrangement"].ToString();

                        if (dr["typeArrangement"] != null && dr["typeArrangement"].ToString() != "")
                            model.typeArrangement = Convert.ToInt32(dr["typeArrangement"]);

                        if (dr["nrTraveler"] != null && dr["nrTraveler"].ToString() != "")
                            model.nrTraveler = Convert.ToInt32(dr["nrTraveler"]);

                        if (dr["minNrTraveler"] != null && dr["minNrTraveler"].ToString() != "")
                            model.minNrTraveler = Convert.ToInt32(dr["minNrTraveler"]);

                        if (dr["nrVoluntaryHelper"] != null && dr["nrVoluntaryHelper"].ToString() != "")
                            model.nrVoluntaryHelper = Convert.ToInt32(dr["nrVoluntaryHelper"]);

                        model.typeNameArrangement = dr["typeNameArrangement"].ToString();
                        model.countryNameArrangement = dr["countryNameArrangement"].ToString();

                        if (dr["idArrangementBook"].ToString() != "")
                            model.idArrangementBook = Convert.ToInt32(dr["idArrangementBook"]);

                        if (dr["idStatus"].ToString() != "")
                            model.idStatus = Convert.ToInt32(dr["idStatus"]);

                        if (dr["idTravelPapers"].ToString() != "")
                            model.idTravelPapers = Convert.ToInt32(dr["idTravelPapers"]);

                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();

                        if (dr["nameTravelPapers"].ToString() != "")
                            model.nameTravelPapers = dr["nameTravelPapers"].ToString();

                        if (dr["price"].ToString() != "")
                            model.price = Convert.ToDecimal(dr["price"]);

                        if (dr["nrMaximumWheelchairs"].ToString() != "")
                            model.nrMaximumWheelchairs = Int32.Parse(dr["nrMaximumWheelchairs"].ToString());

                        if (dr["whoseElectricWheelchairs"].ToString() != "")
                            model.whoseElectricWheelchairs = Convert.ToInt32(dr["whoseElectricWheelchairs"].ToString());

                        if (dr["buSupportingArms"].ToString() != "")
                            model.buSupportingArms = Int32.Parse(dr["buSupportingArms"].ToString());

                        if (dr["nrAnchorage"].ToString() != "")
                            model.nrAnchorage = Int32.Parse(dr["nrAnchorage"].ToString());


                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;

        }

        public List<LastIdModel> IsPersonVolHelp()
        {
            DataTable dataTable = new DataTable();

            dataTable = personDAO.IsPersonVolHelp();
            List<LastIdModel> menu = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();
                        if (dr["ID"] != null && dr["ID"].ToString() != "")
                            model.ID = Convert.ToInt32(dr["ID"]);



                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;

        }

        public List<LastIdModel> NrVolontary(int idArrangement)
        {
            DataTable dataTable = new DataTable();

            dataTable = personDAO.NrVolontary(idArrangement);
            List<LastIdModel> menu = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();
                        if (dr["ID"] != null && dr["ID"].ToString() != "")
                            model.ID = Convert.ToInt32(dr["ID"]);
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;

        }

        public List<LastIdModel> NrTraveler(int idArrangement)
        {
            DataTable dataTable = new DataTable();

            dataTable = personDAO.NrTraveler(idArrangement);
            List<LastIdModel> menu = new List<LastIdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        LastIdModel model = new LastIdModel();
                        if (dr["ID"] != null && dr["ID"].ToString() != "")
                            model.ID = Convert.ToInt32(dr["ID"]);
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;

        }

        public int IsAlreadyTraveling(int idArrangement, int idContpers)
        {
            DataTable dataTable = new DataTable();
            int br = 0;
            dataTable = personDAO.IsAlreadyTraveling(idArrangement, idContpers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    br = 1;

                }

            }

            return br;

        }

        public int CountNrTraveling( int idContpers)
        {
            DataTable dataTable = new DataTable();
            int br = 0;
            dataTable = personDAO.CountNrTraveling(idContpers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)

                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["br"] != null && dr["br"].ToString() != "")
                            br = Convert.ToInt32(dr["br"]);
                       // return br;
                    }
                }

            }

            return br;

        }

        public bool SaveArrangement(int idArrangement, int idContPers, int idStatus, int idTravelPapers, decimal price, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personDAO.SaveArrangement(idArrangement, idContPers, idStatus, idTravelPapers, price, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        //===========  delete person ===========================================================

        public int checkIsInDebitor(int idContPers)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = personDAO.checkIsInDebitor(idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInArrangementBook(int idContPers)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = personDAO.checkIsInArrangementBook(idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInBISAppointment(int idContPers)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = personDAO.checkIsInBISAppointment(idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInClientPerson(int idContPers)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = personDAO.checkIsInClientPerson(idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInDocuments(int idContPers)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = personDAO.checkIsInDocuments(idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInInvoice(int idContPers)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = personDAO.checkIsInInvoice(idContPers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public bool DeletePerson(int idContPers, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personDAO.DeletePerson(idContPers, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetTravelers(int idArrangement, int idContPers, string idLang, List<string> CheckedCity, List<string> CheckedQuest, List<int> CheckedMedical, List<int> CheckedMedicalQuest)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetTravelers(idArrangement, idContPers, idLang, CheckedCity, CheckedQuest, CheckedMedical, CheckedMedicalQuest);
            List<IModel> persons = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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

        public PersonBookModel GetPersonBookedForArrangement(string idLang, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetPersonBookedForArrangement(idLang, idContPers);
            PersonBookModel model = new PersonBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
                        model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());

                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();

                        if (dr["type"].ToString() != "")
                            model.type = dr["type"].ToString();

                        if (dr["dtBooked"].ToString() != "")
                            model.dtBooked = Convert.ToDateTime(dr["dtBooked"].ToString());

                        break;
                    }
                }
            }
            return model;
        }

        public List<IModel> GetTravelersReport(int idArrangement, int idContPers, string idLang, int gender, int status, int travelPapers, List<int> CheckedMedical, List<int> CheckedMedicalQuest)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetTravelersReport(idArrangement, idContPers, idLang, gender, status, travelPapers, CheckedMedical, CheckedMedicalQuest);
            List<IModel> persons = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonModel model = new PersonModel();

                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());

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
        
        //======================== jelena
        public int GetNrFemalePersonsVHForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetFemalePersonsVHForArrangement(idArrangement, idLang);
            int i = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    i = dataTable.Rows.Count;

                }
            }
            return i;
        }
        //==================
        public int GetReservesTravelerForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetReservesTravelerForArrangement(idArrangement, idLang);
            int i = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    i = dataTable.Rows.Count;

                }
            }
            return i;
        }

        public int GetReservesVHForArrangement(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetReservesVHForArrangement(idArrangement, idLang);
            int i = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    i = dataTable.Rows.Count;

                }
            }
            return i;
        }

        public PersonBookModel GetExactPersonBookForArrangementByIdArrangementBook(int idArrangementBook, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetExactPersonBookForArrangementByIdArrangementBook(idArrangementBook, idLang);
            PersonBookModel model = new PersonBookModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {



                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.initialsContPers = dr["initialsContPers"].ToString();
                        model.firstname = dr["firstname"].ToString();
                        model.midname = dr["midname"].ToString();
                        model.lastname = dr["lastname"].ToString();
                        model.maidenname = dr["maidenname"].ToString();

                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());

                        model.nameTitle = dr["nameTitle"].ToString();

                        if (dr["idGender"].ToString() != "")
                            model.idGender = Int32.Parse(dr["idGender"].ToString());

                        model.nameGender = dr["nameGender"].ToString();

                        if (dr["birthdate"].ToString() != "")
                            model.birthdate = DateTime.Parse(dr["birthdate"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["nameUser"].ToString() != "")
                            model.nameUserCreated = dr["nameUser"].ToString();

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["idUserResponsible"].ToString() != "")
                            model.idUserResponsible = Int32.Parse(dr["idUserResponsible"].ToString());

                        model.isMaried = Boolean.Parse(dr["isMaried"].ToString());
                        model.isActive = Boolean.Parse(dr["isActive"].ToString());
                        model.isDied = Boolean.Parse(dr["isDied"].ToString());

                        if (dr["dtOfDeath"].ToString() != "")
                            model.dtOfDeath = DateTime.Parse(dr["dtOfDeath"].ToString());

                        model.isNeedProspect = Boolean.Parse(dr["isNeedProspect"].ToString());
                        model.isNeedMail = Boolean.Parse(dr["isNeedMail"].ToString());

                        if (dr["identBSN"].ToString() != "")
                            model.identBSN = dr["identBSN"].ToString();
                        model.isPayInvoice = Boolean.Parse(dr["isPayInvoice"].ToString());
                        model.isSharePicture = Boolean.Parse(dr["isSharePicture"].ToString());
                        model.isPaperByMail = Boolean.Parse(dr["isPaperByMail"].ToString());
                        model.isContactPerson = Boolean.Parse(dr["isContactPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["livesIn"].ToString() != "")
                            model.livesIn = Int32.Parse(dr["livesIn"].ToString());
                        if (dr["idCpFunction"].ToString() != "")
                            model.idCpFunction = Int32.Parse(dr["idCpFunction"].ToString());
                        model.isRequestBrochure = Boolean.Parse(dr["isRequestBrochure"].ToString());

                        if (dr["idStatus"].ToString() != "")
                            model.idStatus = Int32.Parse(dr["idStatus"].ToString());

                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();

                        if (dr["type"].ToString() != "")
                            model.type = dr["type"].ToString();

                        if (dr["dtBooked"].ToString() != "")
                            model.dtBooked = Convert.ToDateTime(dr["dtBooked"].ToString());
                        if (dr["isInsurance"].ToString() != "")
                            model.isInsurance = Boolean.Parse(dr["isInsurance"].ToString());

                        if (dr["idArrangementBook"].ToString() != "")
                            model.idArrangementBook = Int32.Parse(dr["idArrangementBook"].ToString());

                        break;
                        //persons.Add(model);
                    }

                }
            }
            return model;
        }

        public int GetIfExistValidatePerson(string initials, string midname, string lastname, DateTime birthdate)
        {
            DataTable dataTable = new DataTable();
            dataTable = personDAO.GetIfExistValidatePerson(initials, midname, lastname, birthdate);
            int i = 0;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {


                    i = Convert.ToInt32(dataTable.Rows[0]["CountPerson"].ToString());

                }
            }
            return i;
        }

    }
}
