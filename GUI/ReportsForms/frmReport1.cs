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

namespace GUI{

    public partial class frmReport1 : Telerik.WinControls.UI.RadForm
    {
        //promenljiva iz klase TranslateReport
       string reportName;        

        //za travelers tab parametri
        List<int> selectedAns_MedicalR = new List<int>();
        List<int> selectedQuests_MedicalR = new List<int>();
        int idStatusR = -1;
        int idGenderR = -1;
        int idTravelPapersR = -1;
        int idArrangementR = -1;
        int idContPR = -1;

        public int checkMethod = 0;   
       

        //Parametri za volontere tab
        List<int> selectedIDAnsR = new List<int>();
        List<int> selectedIDQuestsR = new List<int>();

        List<int> selectedIDAns_TripsR = new List<int>();
        List<int> selectedIDQuests_TripsR = new List<int>();

        List<int> selectedIDAns_SkillsR = new List<int>();
        List<int> selectedIDQuests_SkillsR = new List<int>();

        int idContPersR = -1;
        int idArrangementVR = -1; // promenljiva za volontere tab




        //konstruktor za travelers tab
        public frmReport1(int idArrangement, int idContP, int idGender, int idStatus, int idTravelPapers, List<int> selectedIDAns_Medical, List<int> selectedIDQuests_Medical)
        {
            checkMethod = 1;
            InitializeComponent();

            selectedAns_MedicalR = selectedIDAns_Medical;
            selectedQuests_MedicalR = selectedIDQuests_Medical;
            idStatusR = idStatus;
            idGenderR = idGender;
            idTravelPapersR = idTravelPapers;
            idArrangementR = idArrangement;
            idContPR = idContP;

        }

        //konstruktor za volontere tab        
        public frmReport1(int idArrangement, int idContPers, List<int> selectedIDAns, List<int> selectedIDQuests, List<int> selectedIDAns_Trips, List<int> selectedIDQuests_Trips, List<int> selectedIDAns_Skills, List<int> selectedIDQuests_Skills)
        {
            checkMethod = 0;
            InitializeComponent();

            selectedIDAnsR = selectedIDAns;
            selectedIDQuestsR = selectedIDQuests;

            selectedIDAns_TripsR = selectedIDAns_Trips;
            selectedIDQuests_TripsR = selectedIDQuests_Trips;

            selectedIDAns_SkillsR = selectedIDAns_Skills;
            selectedIDQuests_SkillsR = selectedIDQuests_Skills;

            idContPersR = idContPers;

            idArrangementVR = idArrangement;

        }

        private void frmReport1_Load(object sender, EventArgs e)
        {           

            if (checkMethod == 1)
            {
                // travelers tab
                DataSet dataSet = new DataSet(); // deklarisanje dataSeta
                DataTable dataTable = new DataTable();
                //int num = 3; // za prosledjivanja parametara >samo test<  

                
                dataTable = new PersonDAO().GetTravelersReport(idArrangementR, idContPR, Login._user.lngUser, idGenderR, idStatusR, idTravelPapersR, selectedAns_MedicalR, selectedQuests_MedicalR);

                if (dataTable.Rows.Count == 0)
                {

                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");

                    this.Close();
                    
                }

                else
                {
                    //cc = nn.Tables[0];

                    DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
                    dataTableCopy = dataTable.Copy();
                    dataSet.Tables.Add(dataTableCopy);                    

                    ReportDataSource rdSource = new ReportDataSource("DataSetPassengerSelection", (DataTable)dataTable);
                    
                    rdSource.Value = dataTableCopy;

                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);
                    //this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportPassangerSelection.rdlc";
                    //this.reportViewer1.LocalReport.ReportPath = "ReportPassangerSelection.rdlc";                 

                    //Za prevod toolbara
                    //CCustomMessageClass myMessageClass = new CCustomMessageClass();
                    //reportViewer1.Messages = myMessageClass;                

                    
                    // prikaz podataka
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer1.LocalReport.Refresh();
                    this.reportViewer1.Refresh();

                    //Za prevod hedera tabele nova klasa
                    TranslateReport tr = new TranslateReport();
                    reportName = "Reports//ReportPassangerSelection.rdlc";
                    tr.transl(reportName, dataTable);


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//ReportPassangerSelection.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;

                   //generisanje PDF u pozadini Traveler tab!
                    string nameRepTra = "";
                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer1.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer1.LocalReport, nameRepTra);

                    this.reportViewer1.RefreshReport();
                }         
                              
            }

            else
            {
                // Volonter tab
                DataSet dataSetV = new DataSet(); // deklarisanje dataSeta
                DataTable dataTableV = new DataTable();

                dataTableV = new PersonDAO().GetVHPersons(idArrangementVR, idContPersR, Login._user.lngUser, selectedIDAnsR, selectedIDQuestsR, selectedIDAns_TripsR, selectedIDQuests_TripsR, selectedIDAns_SkillsR, selectedIDQuests_SkillsR);

                if (dataTableV.Rows.Count == 0)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");

                    this.Close();
                }

                else //za volontere
                {
                    DataTable dataTableCopyV = new DataTable(); // kopiranje dataTable
                    dataTableCopyV = dataTableV.Copy();
                    dataSetV.Tables.Add(dataTableCopyV);

                    ReportDataSource rdSourceV = new ReportDataSource("DataSetPassengerSelection", (DataTable)dataTableV);

                    rdSourceV.Value = dataTableCopyV;

                    this.reportViewer1.LocalReport.DataSources.Add(rdSourceV);
                    //this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI\\Report2.rdlc";
                    // this.reportViewer1.LocalReport.ReportPath = "C:\\Users\\JJanko68\\Desktop\\WIS Report\\WIS\\GUI\\Report2.rdlc";

                    // prikaz podataka 
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rdSourceV);
                   

                    this.reportViewer1.LocalReport.Refresh();
                    this.reportViewer1.Refresh();

                    //Za prevod hedera
                    TranslateReport trV = new TranslateReport();
                    reportName = "Reports//ReportPassangerSelection.rdlc";
                    trV.transl(reportName, dataTableV);

                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//ReportPassangerSelection.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;

                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer1.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();

                    //generisanje PDF u pozadini Volonter tab!
                    string nameRepVol = "";
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer1.LocalReport,nameRepVol);                   
                   

                    this.reportViewer1.RefreshReport();
                }
            }       
            
            this.reportViewer1.RefreshReport();
           
        }     
                     
    }
}


