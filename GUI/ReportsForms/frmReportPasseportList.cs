using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.DAO;
using System.IO;
using BIS.Model;
using System.Xml;
namespace GUI
{
    public partial class frmReportPasseportList : Telerik.WinControls.UI.RadForm
    {
          string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportPasseportList(DataTable dt8)
        {

            dataTable = dt8;
            InitializeComponent();

        }

        private void frmReportPasseportList_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet(); // deklarisanje dataSeta


            if (dataTable.Rows.Count == 0)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");

                this.Close();
            }

            else
            {
                DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
                dataTableCopy = dataTable.Copy();
                dataSet.Tables.Add(dataTableCopy);

                //Za brisanje vremena iz datuma
                int g = 0;
                string d = "";
                string p = "";
                string h = "";
                string i = "";
                string v = "";

                dataTableCopy.Columns.Add("dtF", typeof(String));
                dataTableCopy.Columns.Add("dtT", typeof(String));
                dataTableCopy.Columns.Add("dtB", typeof(String));
                dataTableCopy.Columns.Add("dtI", typeof(String));
                dataTableCopy.Columns.Add("dtV", typeof(String));

                foreach (DataRow dr in dataTableCopy.Rows)
                {
                    d = dr["dtFromArrangement"].ToString();
                    p = dr["dtToArrangement"].ToString();
                    h = dr["birthdate"].ToString();
                    i = dr["dtPassportIssued"].ToString();
                    v = dr["dtPassportValid"].ToString();
                    if (h == "" || h == null)
                    {
                        h = DateTime.MinValue.ToString();
                    }                   
                    
                        if (i == "" || i == null)
                        {
                            i = DateTime.MinValue.ToString();
                        }                      
                        
                       
                            if (v == "" || v == null)
                            {
                                v = DateTime.MinValue.ToString();
                            }
                        
                    
                   

                    DateTime dateA = Convert.ToDateTime(d);
                    DateTime dateT = Convert.ToDateTime(p);
                    DateTime dateB = Convert.ToDateTime(h);
                    DateTime dateI = Convert.ToDateTime(i);
                    DateTime dateV = Convert.ToDateTime(v);

                    p = dateT.ToString("dd/MM/yyyy");
                    d = dateA.ToString("dd/MM/yyyy");
                    h = dateB.ToString("dd/MM/yyyy");
                    i = dateI.ToString("dd/MM/yyyy");
                    v = dateV.ToString("dd/MM/yyyy");

                    //dt.Rows[g]["dateFrom"] = d;
                    dataTableCopy.Rows[g]["dtF"] = d;
                    dataTableCopy.Rows[g]["dtT"] = p;
                    dataTableCopy.Rows[g]["dtB"] = h;
                    dataTableCopy.Rows[g]["dtI"] = i;
                    dataTableCopy.Rows[g]["dtV"] = v;

                    g++;

                    //Za dodeljivanje polja iz modela i ArrangemnentBookDAO radi prikaza u report i za skrivanje polja
                    dr["nameArrangement1"] = dr["nameArrangement"];
                    dr["dtFromArrangement1"] = dr["dtFromArrangement"];
                    dr["dtToArrangement1"] = dr["dtToArrangement"];
                    dr["codeArrangement1"] = dr["codeArrangement"];
                    //

                }

                //Remove sekcija kolona
                dataTableCopy.Columns.Remove("dtFromArrangement1");
                dataTableCopy.Columns.Remove("dtToArrangement1");
                dataTableCopy.Columns.Remove("birthdate");
                dataTableCopy.Columns.Remove("dtPassportIssued");
                dataTableCopy.Columns.Remove("dtPassportValid");
                //

                dataTableCopy.Columns["dtF"].ColumnName = "dtFromArrangement1";
                dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement1";
                dataTableCopy.Columns["dtB"].ColumnName = "birthdate";
                dataTableCopy.Columns["dtI"].ColumnName = "dtPassportIssued";
                dataTableCopy.Columns["dtV"].ColumnName = "dtPassportValid";
                ////end


                ReportDataSource rdSource = new ReportDataSource("PasseportListDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer8.LocalReport.DataSources.Add(rdSource);



                // prikaz podataka
                this.reportViewer8.LocalReport.DataSources.Clear();
                this.reportViewer8.LocalReport.DataSources.Add(rdSource);
                this.reportViewer8.LocalReport.Refresh();
                this.reportViewer8.Refresh();

                ReportClassPasseportList tr = new ReportClassPasseportList();
                reportName = "Reports//ReportPasseportListPreview.rdlc";

                tr.transl(reportName, dataTable);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ReportPasseportListPreview.rdlc");
                reportViewer8.LocalReport.ReportPath = reportPath;
                reportViewer8.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini !
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer8.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer8.LocalReport, nameRepTra);

                this.reportViewer8.RefreshReport();
            }


        }

       
    }
}
