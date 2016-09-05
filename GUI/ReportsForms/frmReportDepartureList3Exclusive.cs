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
    public partial class frmReportDepartureList3Exclusive : Telerik.WinControls.UI.RadForm
    {
        string reportName;
        string l;
        string t;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportDepartureList3Exclusive(DataTable dt71, string label, string type)
        {

            dataTable = dt71;
            InitializeComponent();
            l = label;
            t = type;
        }

        private void frmReportDepartureList3Exclusive_Load(object sender, EventArgs e)
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
                    //string p = "";
                    // string h = "";

                    string s = "";
                    dataTableCopy.Columns.Add("dtF", typeof(String));
                    //dataTableCopy.Columns.Add("dtT", typeof(String));
                    //dataTableCopy.Columns.Add("dtB", typeof(String));
                    foreach (DataRow dr in dataTableCopy.Rows)
                    {
                        d = dr["dateFrom"].ToString();
                        //p = dr["dtToArrangement"].ToString();
                        // h = dr["birthdate"].ToString();

                        // dataTableCopy.Select("[Sum]=' '".ToString());

                        //if (d == "")
                        //{
                        //    d = DateTime.MinValue.ToString();
                        //}

                        //string a = dr["dateFrom"].ToString();

                        //Pitalica za Suma kad je prazan string!
                        if (dr["dateFrom"].ToString() != "")
                        {

                            DateTime date = Convert.ToDateTime(d);
                            //DateTime dateT = Convert.ToDateTime(p);
                            //DateTime dateB = Convert.ToDateTime(h);

                            // p = dateT.ToString("dd/MM/yyyy");
                            d = date.ToString("dd/MM/yyyy");
                            // h = date.ToString("dd/MM/yyyy");
                            //dt.Rows[g]["dateFrom"] = d;
                            dataTableCopy.Rows[g]["dtF"] = d;
                            //dataTableCopy.Rows[g]["dtT"] = p;
                            //dataTableCopy.Rows[g]["dtB"] = h;
                        }


                        g++;
                        //Za dodeljivanje polja iz modela i DAO radi prikaza u report i za skrivanje polja
                        dr["dateFrom1"] = dr["DateFrom"];
                        dr["dateTo1"] = dr["DateTo"];
                        //

                    }

                    dataTableCopy.Columns.Remove("dateFrom");
                    //dataTableCopy.Columns.Remove("dtToArrangement");

                    //dataTableCopy.Columns.Remove("birthdate");

                    dataTableCopy.Columns["dtF"].ColumnName = "dateFrom";
                    //dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement";
                    //dataTableCopy.Columns["dtB"].ColumnName = "birthdate";
                    //// ////end



                    ReportDataSource rdSource = new ReportDataSource("DepartureList3ExclusiveDataSet", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer71.LocalReport.DataSources.Add(rdSource);



                    // prikaz podataka
                    this.reportViewer71.LocalReport.DataSources.Clear();
                    this.reportViewer71.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer71.LocalReport.Refresh();
                    this.reportViewer71.Refresh();

                    ReportClassDepartureList3Exclusive tr = new ReportClassDepartureList3Exclusive();
                    reportName = "Reports//ReportDepartureList3ExclusivePreview.rdlc";

                    tr.transl(reportName, dataTable, l, t);


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//ReportDepartureList3ExclusivePreview.rdlc");
                    reportViewer71.LocalReport.ReportPath = reportPath;
                    reportViewer71.LocalReport.ReportEmbeddedResource = reportPath;



                    ////generisanje PDF u pozadini !
                    string nameRepTra = "";
                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer71.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer71.LocalReport, nameRepTra);

                    this.reportViewer71.RefreshReport();
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
