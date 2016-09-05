using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;

namespace BIS.Business
{
    public class AccOpenLinesBUS
    {
        private AccOpenLinesDAO accOpenLinesDAO;

        public AccOpenLinesBUS()
        {
            accOpenLinesDAO = new AccOpenLinesDAO();
        }

        public bool Save(AccOpenLinesModel accOpenLines, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accOpenLinesDAO.Save(accOpenLines, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(AccOpenLinesModel accOpenLines, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accOpenLinesDAO.Update(accOpenLines, nameForm, idUser);
            }
            catch(Exception ex)
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
                retval = accOpenLinesDAO.Delete(id,nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool DeleteByInvoice(string invoice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accOpenLinesDAO.DeleteByInvoice(invoice, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public List<IModel> GetAllOpenLines()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAllAccOpenLines();
                List<IModel> openLines = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();

                           
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                            model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                            model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                            model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                            model.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                            model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                            model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                            model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                            model.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());

                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //===
        public List<AccOpenLinesModel> GetOpenLinesByDates(DateTime valuta, DateTime dateplus)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetOpenLinesByDates(valuta, dateplus);
                List<AccOpenLinesModel> openLines = new List<AccOpenLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();


                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            model.name = dr["name"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                model.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                model.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                model.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //===

        public List<AccOpenLinesModel> GetAllOpenLinesM()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAllAccOpenLines();
                List<AccOpenLinesModel> openLines = new List<AccOpenLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();


                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                model.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                model.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                model.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());


                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<AccOpenLinesModel> GetAccOpenLinesByID(string idClient)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLinesByID(idClient);
                List<AccOpenLinesModel> openLines = new List<AccOpenLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();


                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                model.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                model.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                model.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<AccOpenLinesReportModel> GetAccOpenLineReport(DateTime dtCombo, int deb_cre_all)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport(dtCombo,deb_cre_all);
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();

                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());

                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());

                            model.descOpenLine = dr["descOpenLine"].ToString();

                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());

                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());

                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());

                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());

                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());

                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());

                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());

                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            if (dr["isDebitor"].ToString() != "")
                                model.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());

                            if (dr["isCreditor"].ToString() != "")
                                model.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                            if (dr["Debitor/Creditor"].ToString() != "")
                                model.DebitorCreditor = dr["Debitor/Creditor"].ToString();
                            
                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AccOpenLinesReportModel> GetAccOpenLineReportDateAndAccNumber(DateTime dtCombo, string accnumber)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReportDateAndAccNumber(dtCombo, accnumber);
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = true;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            if (dr["isDebitor"].ToString() != "")
                                model.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());

                            if (dr["isCreditor"].ToString() != "")
                                model.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAccOpenLineReportDateAndAccNumber_dt(DateTime dtCombo, string accnumber)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReportDateAndAccNumber(dtCombo, accnumber);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<AccOpenLinesReportModel> GetAccOpenLineReport_1stWarringn()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_1stWarringn();
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            model.email = dr["email"].ToString();

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAccOpenLineReport_1stWarringn_dt()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_1stWarringn();

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //
        public List<AccOpenLinesReportModel> GetAccOpenLineReport_1stWarringn_ByLabel(int label)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_1stWarringn_ByLabel(label);
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            model.email = dr["email"].ToString();

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAccOpenLineReport_1stWarringn_ByLabel_dt(int label)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_1stWarringn_ByLabel(label);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //


        public List<AccOpenLinesReportModel> GetAccOpenLineReport_2ndWarringn()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_2ndWarringn();
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            model.email = dr["email"].ToString();

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAccOpenLineReport_2ndWarringn_dt()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_2ndWarringn();

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        //
        public List<AccOpenLinesReportModel> GetAccOpenLineReport_2ndWarringn_ByLabel(int label)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_2ndWarringn_ByLabel(label);
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            model.email = dr["email"].ToString();

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAccOpenLineReport_2ndWarringn_ByLabel_dt(int label)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_2ndWarringn_ByLabel(label);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //


        public List<AccOpenLinesReportModel> GetAccOpenLineReport_3rdWarringn()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_3rdWarringn();
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                            
                            model.email = dr["email"].ToString();

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAccOpenLineReport_3rdWarringn_dt()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_3rdWarringn();

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //
        public List<AccOpenLinesReportModel> GetAccOpenLineReport_3rdWarringn_ByLabel(int label)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_3rdWarringn_ByLabel(label);
                List<AccOpenLinesReportModel> openLines = new List<AccOpenLinesReportModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesReportModel model = new AccOpenLinesReportModel();

                            model.chk = false;
                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();

                            model.name = dr["name"].ToString();

                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());

                            if (dr["days"].ToString() != "")
                                model.days = Int32.Parse(dr["days"].ToString());

                            if (dr["dif"].ToString() != "")
                                model.dif = Decimal.Parse(dr["dif"].ToString());
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idContPerson"].ToString() != "")
                                model.idContPers = Int32.Parse(dr["idContPerson"].ToString());

                            if (dr["idClient"].ToString() != "")
                                model.idClient = Int32.Parse(dr["idClient"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            if (dr["isInvoicing"].ToString() != "")
                                model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                            model.email = dr["email"].ToString();

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAccOpenLineReport_3rdWarringn_ByLabel_dt(int label)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport_3rdWarringn_ByLabel(label);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //


        public bool SaveAccOpenLinesReportSent_1stWarning(DataTable dt, int nrWarning, string nameForm, int idUser)
        {
            try
            {
                bool b = false;
                b = accOpenLinesDAO.SaveAccOpenLinesReportSent_1stWarning(dt,nrWarning,nameForm,idUser);

                return b;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public DataTable GetAccOpenLineReport_dt(DateTime dtCombo, int deb_cre_all)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLineReport(dtCombo, deb_cre_all);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public AccOpenLinesModel GetAccOpenLinesByInvoice(string invoice, int term)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLinesByInvoice(invoice, term);
                AccOpenLinesModel openLines = new AccOpenLinesModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                         //   AccOpenLinesModel model = new AccOpenLinesModel();


                            openLines.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                openLines.idDebCre = dr["idDebCre"].ToString();
                            openLines.typeOpenLine = dr["typeOpenLine"].ToString();
                            openLines.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                openLines.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                openLines.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            openLines.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                openLines.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                openLines.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            openLines.codeCost = dr["codeCost"].ToString();
                            openLines.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                openLines.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                openLines.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                openLines.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                openLines.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                openLines.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                openLines.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                openLines.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                openLines.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                openLines.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                openLines.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                openLines.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                openLines.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                openLines.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                openLines.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                openLines.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                openLines.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                openLines.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());


                            //openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AccOpenLinesModel GetAccOpenLinesByInvoiceNoTerm(string invoice)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLinesByInvoiceNoTerm(invoice);
                AccOpenLinesModel openLines = new AccOpenLinesModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            //   AccOpenLinesModel model = new AccOpenLinesModel();


                            openLines.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                openLines.idDebCre = dr["idDebCre"].ToString();
                            openLines.typeOpenLine = dr["typeOpenLine"].ToString();
                            openLines.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                openLines.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                openLines.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            openLines.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                openLines.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                openLines.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            openLines.codeCost = dr["codeCost"].ToString();
                            openLines.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                openLines.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                openLines.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                openLines.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                openLines.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                openLines.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                openLines.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                openLines.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                openLines.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                openLines.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                openLines.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                openLines.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                openLines.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                openLines.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                openLines.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                openLines.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                openLines.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                openLines.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            //openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AccOpenLinesModel GetAccOpenLinesByInvoiceClient(string invoice, string client)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLinesByInvoiceClient(invoice, client);
                AccOpenLinesModel openLines = new AccOpenLinesModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            //   AccOpenLinesModel model = new AccOpenLinesModel();


                            openLines.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                openLines.idDebCre = dr["idDebCre"].ToString();
                            openLines.typeOpenLine = dr["typeOpenLine"].ToString();
                            openLines.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                openLines.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                openLines.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            openLines.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                openLines.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                openLines.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            openLines.codeCost = dr["codeCost"].ToString();
                            openLines.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                openLines.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                openLines.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                openLines.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                openLines.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                openLines.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                openLines.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                openLines.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                openLines.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                openLines.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                openLines.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                openLines.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                openLines.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                openLines.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                openLines.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                openLines.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                openLines.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                openLines.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            //openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AccOpenLinesModel> GetOpenLinesWithOpenLinesModel()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAllAccOpenLines();
                List<AccOpenLinesModel> openLines = new List<AccOpenLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();

                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine=dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            model.debitOpenLine=Decimal.Parse(dr["debitOpenLine"].ToString());
                            model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            model.idProject = dr["idProject"].ToString();
                            model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            model.periodOnenLines=Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                model.iban = dr["iban"].ToString();
                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<AccOpenLinesModel> GetAccOpenLinesByIDwList(string idClient)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLinesByID(idClient);
                List<AccOpenLinesModel> openLines = new List<AccOpenLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();

                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            model.idProject = dr["idProject"].ToString();
                            model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            model.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                model.iban = dr["iban"].ToString();
                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AccOpenLinesModel> GetAccOpenLinesSepa(int sepa)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accOpenLinesDAO.GetAccOpenLinesSepa(sepa);
                List<AccOpenLinesModel> openLines = new List<AccOpenLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccOpenLinesModel model = new AccOpenLinesModel();


                            model.idOpenLine = Int32.Parse(dr["idOpenLine"].ToString());
                            if (dr["idDebCre"].ToString() != "")
                                model.idDebCre = dr["idDebCre"].ToString();
                            model.typeOpenLine = dr["typeOpenLine"].ToString();
                            model.invoiceOpenLine = dr["invoiceOpenLine"].ToString();
                            if (dr["dtOpenLine"].ToString() != "")
                                model.dtOpenLine = DateTime.Parse(dr["dtOpenLine"].ToString());
                            if (dr["dtPayOpenLine"].ToString() != "")
                                model.dtPayOpenLine = DateTime.Parse(dr["dtPayOpenLine"].ToString());
                            model.descOpenLine = dr["descOpenLine"].ToString();
                            if (dr["debitOpenLine"].ToString() != "")
                                model.debitOpenLine = Decimal.Parse(dr["debitOpenLine"].ToString());
                            if (dr["creditOpenLine"].ToString() != "")
                                model.creditOpenLine = Decimal.Parse(dr["creditOpenLine"].ToString());
                            model.codeCost = dr["codeCost"].ToString();
                            model.codeArr = dr["codeArr"].ToString();
                            if (dr["idProject"].ToString() != "")
                                model.idProject = dr["idProject"].ToString();
                            if (dr["idPayCondition"].ToString() != "")
                                model.idPayCondition = Int32.Parse(dr["idPayCondition"].ToString());
                            if (dr["creditDays"].ToString() != "")
                                model.creditDays = Int32.Parse(dr["creditDays"].ToString());
                            if (dr["discauntDays"].ToString() != "")
                                model.discauntDays = Int32.Parse(dr["discauntDays"].ToString());
                            if (dr["periodOnenLines"].ToString() != "")
                                model.periodOnenLines = Int32.Parse(dr["periodOnenLines"].ToString());
                            if (dr["account"].ToString() != "")
                                model.account = dr["account"].ToString();
                            if (dr["iselected"].ToString() != "")
                                model.iselected = Boolean.Parse(dr["iselected"].ToString());
                            if (dr["referencePay"].ToString() != "")
                                model.referencePay = dr["referencePay"].ToString();
                            if (dr["idOption"].ToString() != "")
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            if (dr["iban"].ToString() != "")
                                model.iban = dr["iban"].ToString();
                            if (dr["term"].ToString() != "")
                                model.term = Int32.Parse(dr["term"].ToString());
                            if (dr["idSepa"].ToString() != "")
                                model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                            if (dr["isFirstWarrningSent"].ToString() != "")
                                model.isFirstWarrningSent = Boolean.Parse(dr["isFirstWarrningSent"].ToString());
                            if (dr["dtFirstWarrning"].ToString() != "")
                                model.dtFirstWarrning = DateTime.Parse(dr["dtFirstWarrning"].ToString());
                            if (dr["isSecondWarrningSent"].ToString() != "")
                                model.isSecondWarrningSent = Boolean.Parse(dr["isSecondWarrningSent"].ToString());
                            if (dr["dtSecondWarrning"].ToString() != "")
                                model.dtSecondWarrning = DateTime.Parse(dr["dtSecondWarrning"].ToString());
                            if (dr["dtCreationLine"].ToString() != "")
                                model.dtCreationLine = DateTime.Parse(dr["dtCreationLine"].ToString());
                          
                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}