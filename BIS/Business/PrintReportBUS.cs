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
    public class PrintReportBUS
    {
        private PrintReportDAO pr;
        public PrintReportBUS() {
            pr = new PrintReportDAO();
        }

        public List<PrintReportModel> GetReportsForPrintingTraveler(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = pr.GetReportsForPrintingTraveler(idArrangement);
            List<PrintReportModel> reports = new List<PrintReportModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PrintReportModel model = new PrintReportModel();
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContactPerson = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["nameTravelPapers"].ToString() != "")
                            model.reportName =dr["nameTravelPapers"].ToString();

                        reports.Add(model);


                    }
                    return reports;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<PrintReportModel> GetReportsForPrintingVolunteers(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = pr.GetReportsForPrintingVolunteers(idArrangement);
            List<PrintReportModel> reports = new List<PrintReportModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PrintReportModel model = new PrintReportModel();
                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContactPerson = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["nameTravelPapers"].ToString() != "")
                            model.reportName = dr["nameTravelPapers"].ToString();

                        reports.Add(model);


                    }
                    return reports;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
