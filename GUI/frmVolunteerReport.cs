using BIS.Business;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace GUI.User_Controls
{
    public partial class frmVolunteerReport : frmTemplate
    {
        AvailabilitySkillsModel model = new AvailabilitySkillsModel();
        int nr = 0;
        int nrWithSkills = 0; // broj kolona kad se dodaju skilovi u tabelu
        int nrVolQuestType = 0;
        List<AvailabilitySkillsModel> vollist = new List<AvailabilitySkillsModel>();
        List<int> lstabrojeva = new List<int>();
        DataTable dt = new DataTable();
        
        public frmVolunteerReport()
        {
            InitializeComponent();

        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(btnAvailibillitySkills.Text) != null)
                    btnAvailibillitySkills.Text = resxSet.GetString(btnAvailibillitySkills.Text);
                if (resxSet.GetString(btnNotBooketAvailable.Text) != null)
                    btnNotBooketAvailable.Text = resxSet.GetString(btnNotBooketAvailable.Text);
                if (resxSet.GetString(btnAgeList.Text) != null)
                    btnAgeList.Text = resxSet.GetString(btnAgeList.Text);
                if (resxSet.GetString(btnVogCok.Text) != null)
                    btnVogCok.Text = resxSet.GetString(btnVogCok.Text);
                if (resxSet.GetString(btnCleaninglist.Text) != null)
                    btnCleaninglist.Text = resxSet.GetString(btnCleaninglist.Text);
            }
        }

        private DataTable VolAvailabilitySkills()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            dt = new DataTable();

            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu koji nisu bukirani:
            dt = dao.GetAvailabilityByVolontarySkills(model.dateFrom, model.dateTo);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameLabel"]!=null)
                    dt.Columns["nameLabel"].Caption = "Label";
                    if (dt.Columns["idLabel"] != null)
                    dt.Columns["idLabel"].Caption = "ID label";
                    if (dt.Columns["idContPers"] != null)
                    dt.Columns["idContPers"].Caption = "ID person";
                    if (dt.Columns["nameTitle"] != null)
                    dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                    dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["dateFrom"] != null)
                    dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["dateTo"] != null)
                    dt.Columns["dateTo"].Caption = "Date to";
                    if (dt.Columns["Age"] != null)
                    dt.Columns["Age"].Caption = "Age";
                    if (dt.Columns["nameGender"] != null)
                    dt.Columns["nameGender"].Caption = "Gender";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele

                }   

                List<AvailabilitySkillsModel> lista = new List<AvailabilitySkillsModel>();
                // sve skilove:
                lista = bus.GetAvailabilitySkills();

                List<AvailabilitySkillsModel> skills = new List<AvailabilitySkillsModel>();

             

                //  dodavanje skillova kao novih checkBox kolona u dataTable
                int n = 1;

                if (lista != null)
                
                {
                    foreach (AvailabilitySkillsModel item in lista)
                    {

                        DataColumn dc = new DataColumn(item.quest, typeof(string));
                        dc.DefaultValue = false;
                        dc.Caption = dc.ColumnName;
                        dc.ColumnName = "Column_" + n.ToString();
                        // dc.Caption = item.;
                        dt.Columns.Add(dc);
                        n++;

                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string quest = dt.Columns[j].Caption;

                                List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                result = bus.IsCheckedSkills(Convert.ToInt32(idP), quest);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }
                                   
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }   
                
            }
            //  rgvResult.DataSource = dt;
            return dt;

        }
        private DataTable VolAvailabilityNotBookedFunction()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            dt = new DataTable();


            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu:


            dt = dao.GetAvailabilityNotBooked(model.dateFrom, model.dateTo);

            if (dt != null)
            {
                if(dt.Columns.Count>0)
                {
                     if (dt.Columns["nameLabel"]!=null)
                         dt.Columns["nameLabel"].Caption = "Label";
                     if (dt.Columns["idLabel"]!=null)
                         dt.Columns["idLabel"].Caption = "ID label";
                     if (dt.Columns["idContPers"]!=null)
                         dt.Columns["idContPers"].Caption = "ID person";
                     if (dt.Columns["nameTitle"]!=null)
                         dt.Columns["nameTitle"].Caption = "Title";
                     if (dt.Columns["name"]!=null)
                         dt.Columns["name"].Caption = "Name";
                     if (dt.Columns["dateFrom"]!=null)
                         dt.Columns["dateFrom"].Caption = "Date from";
                     if (dt.Columns["dateTo"]!=null)
                         dt.Columns["dateTo"].Caption = "Date to";
                     if (dt.Columns["Age"]!=null)
                         dt.Columns["Age"].Caption = "Age";
                }

      
            
                nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                
                List<AvailabilitySkillsModel> lista = new List<AvailabilitySkillsModel>();
                // svi skilovi:
                lista = bus.GetAvailabilitySkills();

                if (lista != null)
                {
                    List<string> skills = new List<string>();
                    string s = "";
                    if (lista != null)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            s = lista[i].quest.ToString();
                            skills.Add(s);
                        }


                        ////  dodavanje skillova kao novih checkBox kolona u dataTable
                        //foreach (AvailabilitySkillsModel item in lista)
                        //{
                        //    DataColumn dc = new DataColumn(item.quest, typeof(string));
                        //    dc.DefaultValue = false;
                        //    dc.Caption = item.quest;
                        //    dt.Columns.Add(dc);

                        //}

                        int n = 1;
                        //  dodavanje skillova kao novih checkBox kolona u dataTable
                        foreach (AvailabilitySkillsModel item in lista)
                        {
                            DataColumn dc = new DataColumn(item.quest, typeof(string));
                            dc.DefaultValue = false;
                            dc.ColumnName = "Column_" + n.ToString();
                            dc.Caption = item.quest;
                            dt.Columns.Add(dc);
                            n++;
                        }

                        nrWithSkills = dt.Columns.Count;
                        // setovanje cekboxova na tru ili false za skilove
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = nr; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i]["idContPers"].ToString() != "")
                                {
                                    string idP = dt.Rows[i]["idContPers"].ToString();
                                    string quest = dt.Columns[j].ToString();

                                    List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                    result = bus.IsCheckedSkills(Convert.ToInt32(idP), quest);
                                    if (result != null)
                                    {
                                        if (result.Count > 0)
                                        {
                                            dt.Rows[i][j] = "x";
                                        }
                                    }
                                    else
                                    {
                                        dt.Rows[i][j] = "";
                                    }
                                }
                            }
                        }
                    }
                }
                List<AvailabilitySkillsModel> listaFunkcija = new List<AvailabilitySkillsModel>();

                // sve f-je:
                listaFunkcija = bus.GetAvailabilityFunction();

                List<string> function = new List<string>();
                string sf = "";
                if (listaFunkcija != null)
                {
                    for (int i = 0; i < listaFunkcija.Count; i++)
                    {
                        sf = listaFunkcija[i].quest.ToString();
                        function.Add(sf);
                    }
                    int nrColumnsF = 0;
                    ////  dodavanje f-ja kao novih checkBox kolona u dataTable
                    //foreach (AvailabilitySkillsModel items in listaFunkcija)
                    //{
                    //    DataColumn dc = new DataColumn(items.quest, typeof(string));
                    //    dc.DefaultValue = false;
                    //    dc.Caption = items.quest;
                    //    dt.Columns.Add(dc);
                    //}

                    int n = 1;
                    int brojac = 0;
                    //  dodavanje f-ja kao novih checkBox kolona u dataTable
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        if (dataColumn.ColumnName.Length > 7)
                        {
                            string a = dataColumn.ColumnName.Substring(0, 7);
                            if (a == "Column_")
                            {
                                brojac++;
                            }
                        }
                    }
                    n = brojac + 1;

                    foreach (AvailabilitySkillsModel items in listaFunkcija)
                    {
                        DataColumn dc = new DataColumn(items.quest, typeof(string));
                        dc.DefaultValue = false;
                        dc.Caption = items.quest;
                        dc.ColumnName = "Column_" + n.ToString();
                        dt.Columns.Add(dc);
                        n++;
                    }

                    // setovanje cekboxova na tru ili false za f-je
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nrWithSkills; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string quest = dt.Columns[j].Caption;

                                List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                result = bus.IsCheckedFunction(Convert.ToInt32(idP), quest);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }

                            }
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyy");
                }
            }
            // rgvResult.DataSource = dt;
            return dt;

        }
      
               
        private DataTable VolAvailabilityAge()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();

            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu koji nisu bukirani:
            dt = dao.GetAvailabilityByVolontaryAge(model.dateFrom, model.dateTo);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameLabel"] != null)
                        dt.Columns["nameLabel"].Caption = "Label";
                    if (dt.Columns["idLabel"] != null)
                        dt.Columns["idLabel"].Caption = "ID label";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["Age"] != null)
                        dt.Columns["Age"].Caption = "Age";
                    //     dt.Columns["email"].Caption = "Email";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                // dodavanje emaila
                List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaEmail = bus.GetAvailabilityByVolontaryEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                            resultE = bus.GetAvailabilityByVolontaryEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


                //broj kolona posle dodavanja emailova
                int nr1 = dt.Columns.Count;

               
                List<AvailabilitySkillsModel> lista = new List<AvailabilitySkillsModel>();
                // sve f-je:
                lista = bus.GetAvailabilityFunction();


                List<string> function = new List<string>();
                string s = "";
                if (lista != null)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        s = lista[i].quest.ToString();
                        function.Add(s);
                    }
                    //  dodavanje f-ja kao novih checkBox kolona u dataTable

                    //foreach (AvailabilitySkillsModel item in lista)
                    //{
                    //    DataColumn dc = new DataColumn(item.quest, typeof(string));
                    //    dc.DefaultValue = false;
                    //    dc.Caption = item.quest;
                    //    dt.Columns.Add(dc);
                    //}

                    int n = 1;

                    foreach (AvailabilitySkillsModel item in lista)
                    {
                        DataColumn dc = new DataColumn(item.quest, typeof(string));
                        dc.DefaultValue = false;
                        dc.Caption = item.quest;
                        dc.ColumnName = "Column_" + n.ToString();
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr1; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string quest = dt.Columns[j].Caption;

                                List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                result = bus.IsCheckedFunction(Convert.ToInt32(idP), quest);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    int nr4 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr4] = model.dateFrom.ToString("dd/MM/yyy");
                    dt.Rows[0][nr4 + 1] = model.dateTo.ToString("dd/MM/yyy");
                }
            }
            // rgvResult.DataSource = dt;
            return dt;

        }
        private DataTable VogCokVogPass( )
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            //  model.dateTo= Convert.To
            DataTable dt = new DataTable();

            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu koji nisu bukirani:
            dt = dao.ExpiredVokGoKPass(model.dateFrom, model.dateTo, Login._user.lngUser);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameTitle"]!=null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["Type"] != null)
                        dt.Columns["Type"].Caption = "Type";
                    if (dt.Columns["dtExpirationDate"] != null)
                        dt.Columns["dtExpirationDate"].Caption = "Expiration date";
                    //   dt.Columns["email"].Caption = "Email";

                    nr = dt.Columns.Count;
                }
                // dodavanje emaila
                List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaEmail = bus.GetAvailabilityByVolontaryEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }


                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            string quest = dt.Columns[j].Caption;

                            List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                            resultE = bus.GetAvailabilityByVolontaryEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                //       dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
                if (dt.Rows.Count > 0)
                {
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyy");
                }

            }
            return dt;
        }

        private DataTable VolAvailabilityClining()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();

            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu koji nisu bukirani:
            dt = dao.GetAvailabilityByVolontaryClining(model.dateFrom, model.dateTo);
            if(dt!=null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["idLabel"]!=null)
                        dt.Columns["idLabel"].Caption = "ID label";
                    if (dt.Columns["nameLabel"] != null)
                        dt.Columns["nameLabel"].Caption = "Label";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    //    dt.Columns["email"].Caption = "Email";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele

                }   

            // dodavanje emaila
            List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
            // broj koliko neka osoba ima brojeva defaultnih email-ova
            int brojE = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                listaEmail = bus.GetAvailabilityByVolontaryEmail(idP);
                // koliko emailova ima posmatrana osoba
                if (listaEmail != null)
                {
                    if (listaEmail.Count > brojE)
                        brojE = listaEmail.Count;

                    listaEmail = null;
                }
            }
            List<string> listaEm = new List<string>();
            if (brojE != 0)
            {
                for (int i = 0; i < brojE; i++)
                {
                    if (i == 0)
                    {
                        listaEm.Add("Email");
                    }
                    else
                    {

                        listaEm.Add("Email" + i.ToString());
                    }
                }


                foreach (string item in listaEm)
                {
                    DataColumn dc = new DataColumn(item, typeof(string));
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                }
            }
            int countE = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                countE = 0;
                for (int j = nr; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i]["idContPers"].ToString() != "")
                    {
                        string idP = dt.Rows[i]["idContPers"].ToString();
                        string quest = dt.Columns[j].Caption;

                        List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                        resultE = bus.GetAvailabilityByVolontaryEmail(Convert.ToInt32(idP));
                        if (resultE != null)
                        {
                            if (resultE.Count > 0)
                            {
                                if (countE < resultE.Count)
                                {
                                    dt.Rows[i][j] = resultE[j - nr].email;
                                    countE++;
                                }
                            }
                        }
                        else
                        {
                            //       dt.Rows[i][j] = "";

                        }
                    }

                }
            }


           int nr1 = dt.Columns.Count;

                List<AvailabilitySkillsModel> lista = new List<AvailabilitySkillsModel>();
                // sve f-je:
                lista = bus.GetAvailabilityFunction();

                if (lista != null)
                {
                    List<string> function = new List<string>();
                    string s = "";

                    for (int i = 0; i < lista.Count; i++)
                    {
                        s = lista[i].quest.ToString();
                        function.Add(s);
                    }
                    ////  dodavanje f-ja kao novih checkBox kolona u dataTable

                    //foreach (AvailabilitySkillsModel item in lista)
                    //{
                    //    DataColumn dc = new DataColumn((item.quest).ToString(), typeof(string));
                    //    dc.DefaultValue = false;
                    //    dc.Caption = item.quest;
                    //    dt.Columns.Add(dc);
                    //}

                    //  dodavanje f-ja kao novih checkBox kolona u dataTable

                    int n = 1;
                    foreach (AvailabilitySkillsModel item in lista)
                    {
                        DataColumn dc = new DataColumn((item.quest).ToString(), typeof(string));
                        dc.DefaultValue = false;
                        dc.ColumnName = "Column_" + n.ToString();
                        dc.Caption = item.quest;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr1; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string quest = dt.Columns[j].Caption;

                                List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                result = bus.IsCheckedFunction(Convert.ToInt32(idP), quest);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    int nr3 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr3] = model.dateFrom.ToString("dd/MM/yyy");
                    dt.Rows[0][nr3 + 1] = model.dateTo.ToString("dd/MM/yyy");
                } 
            }
        
            return dt;

        }

        private DataTable VolAvailabilityReasonIn()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();

            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu koji nisu bukirani:
            dt = dao.GetAvailabilityByVolontaryReasonIn(model.dateFrom, model.dateTo);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameLabel"] != null)
                        dt.Columns["nameLabel"].Caption = "Label";
                    if (dt.Columns["idLabel"] != null)
                        dt.Columns["idLabel"].Caption = "ID label";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["Age"] != null)
                        dt.Columns["Age"].Caption = "Age";
                    if (dt.Columns["nameReasonIn"] != null)
                        dt.Columns["nameReasonIn"].Caption = "Reason in";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                List<PersonTelModel> listaTel = new List<PersonTelModel>();

                // broj koliko neka osoba ima brojeva telefona
                int brojT = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaTel = bus.GetAvailabilityByVolontaryTel(idP);
                    // koliko brojeva telefona ima posmatrana osoba
                    if (listaTel != null)
                    {
                        if (listaTel.Count > brojT)
                            brojT = listaTel.Count;

                        listaTel = null;
                    }
                }
                List<string> listaBrojeva = new List<string>();
                if (brojT != 0)
                {
                    for (int i = 0; i < brojT; i++)
                    {
                        if (i == 0)
                        {
                            listaBrojeva.Add("Tel");
                        }
                        else
                        {

                            listaBrojeva.Add("Tel" + i.ToString());
                        }
                    }
                    foreach (string item in listaBrojeva)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = bus.GetAvailabilityByVolontaryTel(Convert.ToInt32(idP));
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
                // broj kolona posle dodavanja brojeva telefona
                int nr1 = dt.Columns.Count;

                // dodavanje emaila
                List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaEmail = bus.GetAvailabilityByVolontaryEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr1; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                            resultE = bus.GetAvailabilityByVolontaryEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr1].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


                //broj kolona posle dodavanja emailova
                int nr2 = dt.Columns.Count;
                List<AvailabilitySkillsModel> lista = new List<AvailabilitySkillsModel>();
                // sve f-je:
                lista = bus.GetAvailabilityFunction();


                List<string> function = new List<string>();
                string s = "";
                if (lista != null)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        s = lista[i].quest.ToString();
                        function.Add(s);
                    }
                    ////  dodavanje f-ja kao novih checkBox kolona u dataTable

                    //foreach (AvailabilitySkillsModel item in lista)
                    //{
                    //    DataColumn dc = new DataColumn(item.quest, typeof(string));
                    //    dc.DefaultValue = false;
                    //    dc.Caption = item.quest;
                    //    dt.Columns.Add(dc);
                    //}

                    //  dodavanje f-ja kao novih checkBox kolona u dataTable

                    int n = 1;
                    foreach (AvailabilitySkillsModel item in lista)
                    {
                        DataColumn dc = new DataColumn((item.quest).ToString(), typeof(string));
                        dc.DefaultValue = false;
                        dc.ColumnName = "Column_" + n.ToString();
                        dc.Caption = item.quest;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr2; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string quest = dt.Columns[j].Caption;

                                List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                result = bus.IsCheckedFunction(Convert.ToInt32(idP), quest);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    int nr3 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr3] = model.dateFrom.ToString("dd/MM/yyy");
                    dt.Rows[0][nr3 + 1] = model.dateTo.ToString("dd/MM/yyy");
                }
            }
            // rgvResult.DataSource = dt;
            return dt;

        }


        private DataTable VolAvailabilityReasonOut()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();

            VolAvailabilityDAO dao = new VolAvailabilityDAO();
            VolAvailabilityBUS bus = new VolAvailabilityBUS();

            // sve VolHelpere dostupne u tom terminu koji nisu bukirani:
            dt = dao.GetAvailabilityByVolontaryReasonOut(model.dateFrom, model.dateTo);
            if(dt!=null)
            {
                if (dt.Columns.Count > 0)
                {
                    if( dt.Columns["nameLabel"]!=null)
                    dt.Columns["nameLabel"].Caption = "Label";
                    if( dt.Columns["nameLabel"]!=null)
                    dt.Columns["idLabel"].Caption = "ID label";
                    if( dt.Columns["nameLabel"]!=null)
                    dt.Columns["idContPers"].Caption = "ID person";
                    if( dt.Columns["nameLabel"]!=null)
                    dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                    dt.Columns["name"].Caption = "Name";
                    if( dt.Columns["nameLabel"]!=null)
                    dt.Columns["Age"].Caption = "Age";
                    if( dt.Columns["nameLabel"]!=null)
                    dt.Columns["nameReasonOut"].Caption = "Reason out";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                List<PersonTelModel> listaTel = new List<PersonTelModel>();

                // broj koliko neka osoba ima brojeva telefona
                int brojT = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaTel = bus.GetAvailabilityByVolontaryTel(idP);
                    // koliko brojeva telefona ima posmatrana osoba
                    if (listaTel != null)
                    {
                        if (listaTel.Count > brojT)
                            brojT = listaTel.Count;

                        listaTel = null;
                    }
                }
                List<string> listaBrojeva = new List<string>();
                if (brojT != 0)
                {
                    for (int i = 0; i < brojT; i++)
                    {
                        if (i == 0)
                        {
                            listaBrojeva.Add("Tel");
                        }
                        else
                        {

                            listaBrojeva.Add("Tel" + i.ToString());
                        }
                    }
                    foreach (string item in listaBrojeva)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = bus.GetAvailabilityByVolontaryTel(Convert.ToInt32(idP));
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
                // broj kolona posle dodavanja brojeva telefona
                int nr1 = dt.Columns.Count;

                // dodavanje emaila
                List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaEmail = bus.GetAvailabilityByVolontaryEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr1; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                            resultE = bus.GetAvailabilityByVolontaryEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr1].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


                //broj kolona posle dodavanja emailova
                int nr2 = dt.Columns.Count;
                List<AvailabilitySkillsModel> lista = new List<AvailabilitySkillsModel>();
                // sve f-je:
                lista = bus.GetAvailabilityFunction();


                List<string> function = new List<string>();
                string s = "";
                if (lista != null)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        s = lista[i].quest.ToString();
                        function.Add(s);
                    }
                    ////  dodavanje f-ja kao novih checkBox kolona u dataTable

                    //foreach (AvailabilitySkillsModel item in lista)
                    //{
                    //    DataColumn dc = new DataColumn(item.quest, typeof(string));
                    //    dc.DefaultValue = false;
                    //    dc.Caption = item.quest;
                    //    dt.Columns.Add(dc);
                    //}

                    //  dodavanje f-ja kao novih checkBox kolona u dataTable

                    int n = 1;
                    foreach (AvailabilitySkillsModel item in lista)
                    {
                        DataColumn dc = new DataColumn((item.quest).ToString(), typeof(string));
                        dc.DefaultValue = false;
                        dc.ColumnName = "Column_" + n.ToString();
                        dc.Caption = item.quest;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr2; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string quest = dt.Columns[j].Caption;

                                List<AvailabilitySkillsModel> result = new List<AvailabilitySkillsModel>();
                                result = bus.IsCheckedFunction(Convert.ToInt32(idP), quest);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    int nr3 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr3] = model.dateFrom.ToString("dd/MM/yyy");
                    dt.Rows[0][nr3 + 1] = model.dateTo.ToString("dd/MM/yyy");
                }
            }
            // rgvResult.DataSource = dt;
            return dt;

        }


        private void frmVolunteerReport_Load(object sender, EventArgs e)
        {
            btnSave.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;

            VolAvailabilityBUS v = new VolAvailabilityBUS();
            vollist = v.GetNrVolQueryType();

            if (vollist != null)
                nrVolQuestType = vollist.Count;

            this.Icon = Login.iconForm;
            //string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            //formName = formName + " " + model.nameArrangement;
            string name = this.Name.Substring(3);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;


            setTranslation();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //DataTable result = new DataTable();
            DataTable dt1 = new DataTable();

            Cursor.Current = Cursors.WaitCursor;
            if (btnAvailibillitySkills.IsChecked == true)
            {

                VolAvailabilitySkills();
                // za test
                //rgvResult.DataSource = VolAvailabilitySkills();
                //rgvResult.DataSource = null;
                dt1 = VolAvailabilitySkills();


            }
            DataTable dt2 = new DataTable();
            if (btnNotBooketAvailable.IsChecked == true)
            {
                VolAvailabilityNotBookedFunction();


                //test
                //rgvResult.DataSource = null;
                //rgvResult.DataSource = VolAvailabilityNotBookedFunction();
                dt2 = VolAvailabilityNotBookedFunction();
            }

            DataTable dt3 = new DataTable();
            if (btnAgeList.IsChecked == true)
            {
                VolAvailabilityAge();

                //rgvResult.DataSource = null;
                dt3 = VolAvailabilityAge();
                //rgvResult.DataSource = VolAvailabilityAge();


            }

            DataTable dt5 = new DataTable();
            if (btnVogCok.IsChecked == true)
            {
                VogCokVogPass();

                //test
                //rgvResult.DataSource = null;
                dt5 = VogCokVogPass();
                //rgvResult.DataSource = VogCokVogPass();

            }

            DataTable dt6 = new DataTable();
            if (btnCleaninglist.IsChecked == true)
            {
                VolAvailabilityClining();


                //test
                //rgvResult.DataSource = null;
                dt6 = VolAvailabilityClining();
                //rgvResult.DataSource = VolAvailabilityClining();


            }

            DataTable dt7 = new DataTable();
            if (btnReasonIn.IsChecked == true)
            {
               VolAvailabilityReasonIn();


                ////test
                //rgvResult.DataSource = null;
               dt7 = VolAvailabilityReasonIn();
                //rgvResult.DataSource = VolAvailabilityReasonIn();
            }

            DataTable dt8 = new DataTable();
            if (btnReasonOut.IsChecked == true)
            {
                VolAvailabilityReasonOut();

                ////test
                //rgvResult.DataSource = null;
                dt8 = VolAvailabilityReasonOut();
                //rgvResult.DataSource = VolAvailabilityReasonOut();

            }



            Cursor.Current = Cursors.Default;

            //Konstruktori za reporte
            if (btnAvailibillitySkills.IsChecked == true)
            {
                frmVolAvailabilityReport frm = new frmVolAvailabilityReport(dt1);
                frm.Show();
            }
            else if (btnNotBooketAvailable.IsChecked == true)
            {
                ReportAvailabilityNotBookedFunction frm2 = new ReportAvailabilityNotBookedFunction(dt2);
                frm2.Show();
            }
            else if (btnAgeList.IsChecked == true)
            {
                ReportAvailabilityAge frm3 = new ReportAvailabilityAge(dt3);
                frm3.Show();
            }
            else if (btnVogCok.IsChecked == true)
            {
                ReportListWithExpired frm5 = new ReportListWithExpired(dt5);
                frm5.Show();
            }
            else if (btnCleaninglist.IsChecked == true)
            {
                ReportCleaningList frm6 = new ReportCleaningList(dt6);
                frm6.Show();
            }
            else if(btnReasonIn.IsChecked == true)
            {
                frmReportReasonIn frm7 = new frmReportReasonIn(dt7);
                frm7.Show();
            }
            else if(btnReasonOut.IsChecked == true)
            {
                frmReportReasonOut frm8 = new frmReportReasonOut(dt8);
                frm8.Show();
            }
        }

        List<string> listPersonEmails;
        List<string> listPersonIDS;
                
        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (Login.isOutlookInstalled == true)
            {
                DataTable dt = new DataTable();
                Cursor.Current = Cursors.WaitCursor;
                if (btnAvailibillitySkills.IsChecked == true)
                {
                    VolAvailabilitySkills();
                    // za test
                    //rgvResult.DataSource = VolAvailabilitySkills();
                    //rgvResult.DataSource = null;
                    dt = VolAvailabilitySkills();
                }                
                if (btnNotBooketAvailable.IsChecked == true)
                {
                    VolAvailabilityNotBookedFunction();
                    //test
                    //rgvResult.DataSource = null;
                    //rgvResult.DataSource = VolAvailabilityNotBookedFunction();
                    dt = VolAvailabilityNotBookedFunction();
                }                
                if (btnAgeList.IsChecked == true)
                {
                    VolAvailabilityAge();
                    //rgvResult.DataSource = null;
                    dt = VolAvailabilityAge();
                    //rgvResult.DataSource = VolAvailabilityAge();
                }                
                if (btnVogCok.IsChecked == true)
                {
                    VogCokVogPass();
                    //test
                    //rgvResult.DataSource = null;
                    dt = VogCokVogPass();
                    //rgvResult.DataSource = VogCokVogPass();
                }                
                if (btnCleaninglist.IsChecked == true)
                {
                    VolAvailabilityClining();
                    //test
                    //rgvResult.DataSource = null;
                    dt = VolAvailabilityClining();
                    //rgvResult.DataSource = VolAvailabilityClining();
                }
                
                if (btnReasonIn.IsChecked == true)
                {
                    VolAvailabilityReasonIn();
                    ////test
                    //rgvResult.DataSource = null;
                    dt = VolAvailabilityReasonIn();
                    //rgvResult.DataSource = VolAvailabilityReasonIn();
                }                
                if (btnReasonOut.IsChecked == true)
                {
                    VolAvailabilityReasonOut();
                    ////test
                    //rgvResult.DataSource = null;
                    dt = VolAvailabilityReasonOut();
                    //rgvResult.DataSource = VolAvailabilityReasonOut();
                }
                
                PersonEmailBUS pbus = new PersonEmailBUS();
                
                listPersonEmails = new List<string>();
                listPersonIDS = new List<string>();
                
                if(dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strIdPerson = dr["idContPers"].ToString();
                        if(strIdPerson != "")
                        {
                            
                            int id = Int32.Parse(strIdPerson);
                            DataTable dtMails = pbus.GetPersonEmailsIsCommunicationTable(id);
                            if(dtMails != null && dtMails.Rows.Count > 0)
                            {
                                DataRow drMail = dtMails.Rows[0];
                                if (!listPersonEmails.Exists(s => s == drMail["email"].ToString()))
                                {
                                    listPersonEmails.Add(drMail["email"].ToString());
                                    listPersonIDS.Add(strIdPerson);
                                }
                            }
                        }
                    }
                    //MessageBox.Show(listPersonEmails.ToArray().ToString());

                    try
                    {
                        if (listPersonEmails.Count > 0)
                        {
                            Outlook.Application outlookApp = new Outlook.Application();
                            outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);

                            Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                            Outlook.Inspector oInspector = oMailItem.GetInspector;
                            oMailItem.DeleteAfterSubmit = false;

                            Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                            foreach (String recipient in listPersonEmails)
                            {
                                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(recipient);
                                oRecip.Resolve();
                            }

                            oMailItem.Subject = "";
                            oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText;
                            oMailItem.Body = "Beste, \r\n";

                            Outlook.Folder outlookfolder = Login.GetOutlookBisFolder();
                            if (outlookfolder != null)
                                oMailItem.SaveSentMessageFolder = outlookfolder;
                            //  oMailItem.SaveSentMessageFolder = Login.sentFolder;

                            //Display the mailbox
                            oMailItem.Display(true);

                        }
                        else
                        {
                            translateRadMessageBox msgbox = new translateRadMessageBox();
                            msgbox.translateAllMessageBox("Cannot find recipients address.");
                        }
                    }
                    catch (Exception objEx)
                    {
                        RadMessageBox.Show(objEx.Message);
                    }
                }
                else
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Cannot find recipients address.");
                }
            }
            else
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
            }
        }

        void outlookApp_ItemSend(object Item, ref bool Cancel)
        {
            if (Item is Microsoft.Office.Interop.Outlook.MailItem)
            {
                Microsoft.Office.Interop.Outlook.MailItem item = (Microsoft.Office.Interop.Outlook.MailItem)Item;
                item.Save();

                DocumentsBUS sbus = new DocumentsBUS();
                PersonEmailBUS emailbus = new PersonEmailBUS();


                string locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";

                if (!File.Exists(locationOnDisk))
                    item.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                foreach (String person in listPersonIDS)
                {
                    if (Int32.Parse(person) != 0)
                    {
                        DocumentsModel model = new DocumentsModel();
                        model.idContPers = Int32.Parse(person);
                        model.idClient = 0;
                        model.descriptionDocument = "Email";
                        model.fileDocument = item.EntryID + ".msg";
                        model.typeDocument = "EML";
                        model.idDocumentStatus = 2;
                        model.idEmployee = 0;
                        model.idResponsableEmployee = 0;
                        model.inOutDocument = 0;
                        model.noteDocument = "Sent Email";
                        model.idArrangement = 0;
                        //model.id

                        model.dtCreated = DateTime.Now;
                        model.dtModified = DateTime.Now;
                        model.userCreated = Login._user.idUser;
                        model.userModified = Login._user.idUser;

                        sbus.Save(model, this.Name, Login._user.idUser);
                    }
                }

                Cancel = false;
            }
        }

    }
}
