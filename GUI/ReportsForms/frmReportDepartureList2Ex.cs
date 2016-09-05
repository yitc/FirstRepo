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
using System.Resources;

namespace GUI
{
    public partial class frmReportDepartureList2Ex : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportDepartureList2Ex(DataTable dt28)
        {

            dataTable = dt28;
            InitializeComponent();

        }

        private void frmReportDepartureList2_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet(); // deklarisanje dataSeta
            if (dataTable != null)
            {

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

                    //// //Za brisanje vremena iz datuma
                    int g = 0;
                    string d = "";

                    string s = "";
                    dataTableCopy.Columns.Add("dtF", typeof(String));

                    foreach (DataRow dr in dataTableCopy.Rows)
                    {
                        d = dr["dateFrom"].ToString();


                        if (dr["dateFrom"].ToString() != "")
                        {

                            DateTime date = Convert.ToDateTime(d);



                            d = date.ToString("dd/MM/yyyy");

                            dataTableCopy.Rows[g]["dtF"] = d;

                        }
                        g++;

                        //Za dodeljivanje polja iz modela i DAO radi prikaza u report i za skrivanje polja
                        dr["dateFrom1"] = dr["DateFrom"];
                        dr["dateTo1"] = dr["DateTo"];
                        //

                    }

                    dataTableCopy.Columns.Remove("dateFrom");

                    dataTableCopy.Columns["dtF"].ColumnName = "dateFrom";

                    //// ////end


                    ReportDataSource rdSource = new ReportDataSource("DepartureList2DataSet", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer28.LocalReport.DataSources.Add(rdSource);



                    // prikaz podataka
                    this.reportViewer28.LocalReport.DataSources.Clear();
                    this.reportViewer28.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer28.LocalReport.Refresh();
                    this.reportViewer28.Refresh();

                    ReportClassDepartureList2Ex tr = new ReportClassDepartureList2Ex();
                    reportName = "Reports//ReportDepartureList2Preview.rdlc";

                    tr.transl(reportName, dataTable);


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//ReportDepartureList2Preview.rdlc");
                    reportViewer28.LocalReport.ReportPath = reportPath;
                    reportViewer28.LocalReport.ReportEmbeddedResource = reportPath;


                    // za prevod dijalogBoxa
                    //string newName = "";
                    //string zika = "ReportDepartureList";
                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString(zika) != null)

                    //        newName = zika;
                    //    //newName = "Date from"+ newName.ToString() +  "Date to"+ newName.ToString();
                    //}


                    //reportViewer28.LocalReport.DisplayName = newName;

                    //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    //saveFileDialog1.FileName = newName;

                    ////Prevod dijaloga


                    ////generisanje PDF u pozadini !
                    string nameRepTra = "";
                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer28.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer28.LocalReport, nameRepTra);

                    this.reportViewer28.RefreshReport();
                }


            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");

                this.Close();
            }



        }



    }
}

