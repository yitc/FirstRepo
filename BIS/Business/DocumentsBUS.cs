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
    public class DocumentsBUS
    {
        private DocumentsDAO docDAO;

        public DocumentsBUS()
        {
            docDAO = new DocumentsDAO();
        }

        public bool Save(DocumentsModel PersDoc, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = docDAO.Save(PersDoc,nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(DocumentsModel PersDoc, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = docDAO.Update(PersDoc, nameForm, idUser);

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

                retval = docDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool RemoveClientFromDocument(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = docDAO.RemoveClientFromDocument(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<DocumentsModel> GetPersonDoc(int idPerson, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = docDAO.GetPersonDoc(idPerson, idLang);
            List<DocumentsModel> personsDoc = new List<DocumentsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    DocumentsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new DocumentsModel();
                        model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        model.typeDocument = dr["typeDocument"].ToString();
                        model.descriptionDocument = dr["descriptionDocument"].ToString();
                        model.fileDocument = dr["fileDocument"].ToString();
                        if (dr["inOutDocument"].ToString() != "")
                            model.inOutDocument = Decimal.Parse(dr["inOutDocument"].ToString());
                        if (dr["nameInOutDocuments"].ToString() != "")
                            model.nameInOutDocuments = dr["nameInOutDocuments"].ToString();
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();
                        if (dr["idResponsableEmployee"].ToString() != "")
                            model.idResponsableEmployee = Int32.Parse(dr["idResponsableEmployee"].ToString());
                        if (dr["nameEmployeeResponsible"].ToString() != "")
                            model.nameEmployeeResponsible = dr["nameEmployeeResponsible"].ToString();
                        if (dr["idDocumentStatus"].ToString() != "")
                            model.idDocumentStatus = Int32.Parse(dr["idDocumentStatus"].ToString());
                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();
                        model.noteDocument = dr["noteDocument"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["idLayout"].ToString() != "")
                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        //end jelena

                        personsDoc.Add(model);
                    }
                    return personsDoc;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<DocumentsModel> GetArrangementDoc(int idArrangement, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = docDAO.GetArrangementDoc(idArrangement, idLang);
            List<DocumentsModel> personsDoc = new List<DocumentsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    DocumentsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new DocumentsModel();
                        model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        model.typeDocument = dr["typeDocument"].ToString();
                        model.descriptionDocument = dr["descriptionDocument"].ToString();
                        model.fileDocument = dr["fileDocument"].ToString();
                        if (dr["inOutDocument"].ToString() != "")
                            model.inOutDocument = Decimal.Parse(dr["inOutDocument"].ToString());
                        if (dr["nameInOutDocuments"].ToString() != "")
                            model.nameInOutDocuments = dr["nameInOutDocuments"].ToString();
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();
                        if (dr["idResponsableEmployee"].ToString() != "")
                            model.idResponsableEmployee = Int32.Parse(dr["idResponsableEmployee"].ToString());
                        if (dr["nameEmployeeResponsible"].ToString() != "")
                            model.nameEmployeeResponsible = dr["nameEmployeeResponsible"].ToString();
                        if (dr["idDocumentStatus"].ToString() != "")
                            model.idDocumentStatus = Int32.Parse(dr["idDocumentStatus"].ToString());
                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();
                        model.noteDocument = dr["noteDocument"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["idLayout"].ToString() != "")
                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();
                        //jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        //end jelena

                        personsDoc.Add(model);
                    }
                    return personsDoc;
                }
                else
                    return null;
            }
            else
                return null;
        }
      //  public List<DocumentsModel> GetDocument(int idDocument)
        public DocumentsModel GetDocument(int idDocument)
        {
            DataTable dataTable = new DataTable();
            dataTable = docDAO.GetDocument(idDocument);
            //  List<DocumentsModel> personsDoc = new List<DocumentsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    DocumentsModel model = new DocumentsModel();
                    //   DocumentsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //model = new DocumentsModel();
                        model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        model.typeDocument = dr["typeDocument"].ToString();
                        model.descriptionDocument = dr["descriptionDocument"].ToString();
                        model.fileDocument = dr["fileDocument"].ToString();
                        if (dr["inOutDocument"].ToString() != "")
                            model.inOutDocument = Decimal.Parse(dr["inOutDocument"].ToString());
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();
                        if (dr["idResponsableEmployee"].ToString() != "")
                            model.idResponsableEmployee = Int32.Parse(dr["idResponsableEmployee"].ToString());
                        if (dr["nameEmployeeResponsible"].ToString() != "")
                            model.nameEmployeeResponsible = dr["nameEmployeeResponsible"].ToString();
                        if (dr["idDocumentStatus"].ToString() != "")
                            model.idDocumentStatus = Int32.Parse(dr["idDocumentStatus"].ToString());

                        model.noteDocument = dr["noteDocument"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        if (dr["idLayout"].ToString() != "")
                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                        // jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena

                        //   personsDoc.Add(model);
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<DocumentsModel> GetClientDoc(int idClient, string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = docDAO.GetClientDoc(idClient, idLang);
            List<DocumentsModel> clientDoc = new List<DocumentsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    DocumentsModel model = new DocumentsModel();
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DocumentsModel();
                        model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        model.typeDocument = dr["typeDocument"].ToString();
                        model.descriptionDocument = dr["descriptionDocument"].ToString();
                        model.fileDocument = dr["fileDocument"].ToString();
                        if (dr["inOutDocument"].ToString() != "")
                            model.inOutDocument = Decimal.Parse(dr["inOutDocument"].ToString());
                        if (dr["nameInOutDocuments"].ToString() != "")
                            model.nameInOutDocuments = dr["nameInOutDocuments"].ToString();
                        if (dr["idProject"].ToString() != "")
                            model.idProject = Int32.Parse(dr["idProject"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();
                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();
                        if (dr["idResponsableEmployee"].ToString() != "")
                            model.idResponsableEmployee = Int32.Parse(dr["idResponsableEmployee"].ToString());
                        if (dr["nameEmployeeResponsible"].ToString() != "")
                            model.nameEmployeeResponsible = dr["nameEmployeeResponsible"].ToString();
                        if (dr["idDocumentStatus"].ToString() != "")
                            model.idDocumentStatus = Int32.Parse(dr["idDocumentStatus"].ToString());
                        if (dr["nameStatus"].ToString() != "")
                            model.nameStatus = dr["nameStatus"].ToString();
                        model.noteDocument = dr["noteDocument"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();
                        if (dr["nameProject"].ToString() != "")
                            model.nameProject = dr["nameProject"].ToString();

                        if (dr["idLayout"].ToString() != "")
                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                        // jelena
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        // end jelena

                        clientDoc.Add(model);

                    }
                    return clientDoc;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public bool CheckDocumentIdClient(int id)
        {
            // ako postoji idclient vraca true
            // ako ne postoji vraca false

            bool retval = false;

            try
            {

                object obj = docDAO.CheckDocumentIdClient(id);

                string str = obj.ToString();
                if(obj == null || str == "")
                {
                    retval = false;
                }
                else
                {
                    retval = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool CheckDocumentIdProject(int id)
        {
            // ako postoji idproject vraca true
            // ako ne postoji vraca false

            bool retval = false;

            try
            {

                object obj = docDAO.CheckDocumentIdProject(id);

                string str = obj.ToString();
                if (obj == null || str == "")
                {
                    retval = false;
                }
                else
                {
                    retval = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool CheckDocumentIdEmployee(int id)
        {
            // ako postoji idEmployee vraca true
            // ako ne postoji vraca false

            bool retval = false;

            try
            {

                object obj = docDAO.CheckDocumentIdEmployee(id);

                string str = obj.ToString();
                if (obj == null || str == "")
                {
                    retval = false;
                }
                else
                {
                    retval = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool CheckDocumentidArrangement(int id)
        {
            // ako postoji idArrangement vraca true
            // ako ne postoji vraca false

            bool retval = false;

            try
            {

                object obj = docDAO.CheckDocumentidArrangement(id);

                string str = obj.ToString();
                if (obj == null || str == "")
                {
                    retval = false;
                }
                else
                {
                    retval = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
