using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.Model;
using BIS.DAO;
using System.Resources;
using System.IO;
using Telerik.WinControls.UI;


namespace GUI
{
    public partial class frmBeginYear : Telerik.WinControls.UI.RadForm
    {
        private List<AccLineModel> alm;
        private List<AccLineBeginModel> albm ;
        private List<AccLineModel> multimodel ;
        private List<AccDailyModel> adm;
        private AccDailyMemModel admmod;
        private int xDaily;
        private string layoutOldBegin;
        private string layoutNewBegin;
        private int disableDel = 0;
        private int aaa;


        public frmBeginYear()
        {
            InitializeComponent();
            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Begin balans";
            this.Icon = Login.iconForm;
        }

        private void frmBeginYear_Load(object sender, EventArgs e)
        {
            if (Login._user.username == "bu")
                radButton1.Visible = true;
            layoutOldBegin = MainForm.gridFiltersFolder + "\\layoutOldBegin.xml";
            layoutNewBegin = MainForm.gridFiltersFolder + "\\layoutNewBegin.xml";

            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblYear.Text) != null)
                    lblYear.Text = resxSet.GetString(lblYear.Text);

                if (resxSet.GetString(btnSettings.Text) != null)
                    btnSettings.Text = resxSet.GetString(btnSettings.Text);

                if (resxSet.GetString(btnDaily.Text) != null)
                    btnDaily.Text = resxSet.GetString(btnDaily.Text);

                if (resxSet.GetString(btnBegin.Text) != null)
                    btnBegin.Text = resxSet.GetString(btnBegin.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(lblDesription.Text) != null)
                    lblDesription.Text = resxSet.GetString(lblDesription.Text);

                if (resxSet.GetString(btnOpen.Text) != null)
                    btnOpen.Text = resxSet.GetString(btnOpen.Text);

                if (resxSet.GetString(lblold.Text) != null)
                    lblold.Text = resxSet.GetString(lblold.Text);

                if (resxSet.GetString(lblnew.Text) != null)
                    lblnew.Text = resxSet.GetString(lblnew.Text);

            }


        }
        # region Buttons
        private void btnSettings_Click(object sender, EventArgs e)
        {
            //==== proverava da li postoje Daily book-ovi za tu godinu i setuje disable za delete button u frmSettings
              //=== cita daily-je za  godinu
            if (ddlYear.SelectedItem != null)
            {

                AccDailyBUS db = new AccDailyBUS(ddlYear.SelectedItem.Text);
                List<AccDailyModel> check = new List<AccDailyModel>();
                check = db.GetAllDailysList();
                if (check != null)
                {
                    disableDel = 1;
                }
                //=============================================
                AccSettingsBUS asb = new AccSettingsBUS();
                AccSettingsModel asm = new AccSettingsModel();
                AccSettingsModel previus = new AccSettingsModel();
                if (ddlYear.SelectedItem != null)
                {
                    if (ddlYear.SelectedItem.Text != null && ddlYear.SelectedItem.Text != "")
                    {
                        asm = asb.GetSettingsByID(ddlYear.SelectedItem.Text);
                        if (asm != null)
                        {
                            if (asm.yearSettings == ddlYear.SelectedItem.Text)
                            {
                                DialogResult dr = RadMessageBox.Show("That settings allredy exist! Do you want to change ?", "Question", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    frmAccSettings frs = new frmAccSettings(asm, disableDel);
                                    frs.ShowDialog();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            DialogResult dr = RadMessageBox.Show("Do you want to copy of previus year ?", "Question", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                int xyear = Convert.ToInt32(ddlYear.SelectedItem.Text) - 1;

                                previus = new AccSettingsBUS().GetSettingsByID(xyear.ToString());
                                if (previus == null)
                                {
                                    translateRadMessageBox msg = new translateRadMessageBox();
                                    msg.translateAllMessageBox("No previus year !");
                                    frmAccSettings frs = new frmAccSettings(Convert.ToInt32(ddlYear.SelectedItem.Text));
                                    frs.ShowDialog();
                                    return;
                                }
                                else
                                {
                                    int dtfrom = Convert.ToInt32(ddlYear.SelectedItem.Text);
                                    int dtTo = Convert.ToDateTime(previus.endBookYear).Year;
                                    DateTime dtfrom2 = DateTime.MinValue;
                                    DateTime dtTo2 = DateTime.MinValue;
                                    if (previus.beginBookYear != null)
                                        dtfrom2 = new DateTime(dtfrom, Convert.ToDateTime(previus.beginBookYear).Month, Convert.ToDateTime(previus.beginBookYear).Day);
                                    if (previus.endBookYear != null)
                                        dtTo2 = new DateTime(dtTo + 1, Convert.ToDateTime(previus.endBookYear).Month, Convert.ToDateTime(previus.endBookYear).Day);

                                    AccSettingsModel newyear = new AccSettingsModel();
                                    newyear = previus;
                                    newyear.userCreated = Login._user.idUser;
                                    newyear.yearSettings = ddlYear.SelectedItem.Text;
                                    newyear.beginBookYear = Convert.ToDateTime(dtfrom2);
                                    newyear.endBookYear = Convert.ToDateTime(dtTo2);
                                    try
                                    {
                                        //asb.Save(newyear);
                                        //newyear = new AccSettingsModel();
                                        //newyear = asb.GetSettingsByID(ddlYear.SelectedItem.Text);

                                        frmAccSettings frs = new frmAccSettings(newyear, disableDel);
                                        frs.ShowDialog();
                                        AccSettingsModel asmchk = new AccSettingsModel();
                                        asmchk = asb.GetSettingsByID(ddlYear.SelectedItem.Text);
                                        if (asmchk != null)
                                            btnSettings.Enabled = false;
                                        else
                                            btnSettings.Enabled = true;
                                        return;
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    //frmAccSettings frs1 = new frmAccSettings(newyear);
                                    //frs1.ShowDialog();
                                    //return;
                                }
                            }
                            else
                            {
                                frmAccSettings frs = new frmAccSettings(Convert.ToInt32(ddlYear.SelectedItem.Text));
                                frs.ShowDialog();
                                AccSettingsModel asmchk = new AccSettingsModel();
                                asmchk = asb.GetSettingsByID(ddlYear.SelectedItem.Text);
                                if (asmchk != null)
                                    btnSettings.Enabled = false;
                                else
                                    btnSettings.Enabled = true;
                                return;
                            }
                        }

                    }
                    else
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Choose year, please !");
                    }
                }
                else
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Choose year, please !");
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Choose year, please !");
            }

        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            AccSettingsBUS asb = new AccSettingsBUS();
            AccSettingsModel asm = new AccSettingsModel();
            List<AccDailyModel> previus = new List<AccDailyModel>();

            if (ddlYear.SelectedItem != null)
            {
                if (ddlYear.SelectedItem.Text != null && ddlYear.SelectedItem.Text != "")
                {
                    asm = asb.GetSettingsByID(ddlYear.SelectedItem.Text);
                    if (asm == null)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("No setting for new year! Enter a settings first, please!");
                        return;
                    }
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Choose year, please !");
                return;
            }
            int prevYear = Convert.ToInt32(ddlYear.SelectedItem.Text) - 1;
            //=== cita daily-je za predhodnu godinu
            AccDailyBUS db = new AccDailyBUS(prevYear.ToString());
            previus =  db.GetAllDailysList();
            if (previus == null || previus.Count <= 0)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No Daily entered for previus year! Enter manually, please!");
                return;
            }
            if (previus.Count > 0)
            {
                AccDailyBUS dbs = new AccDailyBUS(ddlYear.SelectedItem.Text);
                string frm = "frmNewYear_daily";
                bool isOk = false;
                foreach (AccDailyModel dm in previus)
                {
                    dm.bookingYear = ddlYear.SelectedItem.Text;
                    dm.userCreated = Login._user.idUser;

                    try
                    {
                        isOk=dbs.Save(dm,frm , Login._user.idUser);  
                        if (isOk == false)
                        {
                            translateRadMessageBox msg1 = new translateRadMessageBox();
                            msg1.translateAllMessageBox("Error writing Daily!");
                            return;
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Finish");
                btnDaily.Enabled = false;
            }
            //=== cita brojace i postavlja ih na 0;
            AccLineBUS alb = new AccLineBUS(prevYear.ToString());
            List<IdModel> counter_prev = new List<IdModel>();
            
            if (prevYear == null && prevYear == 0)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No counters for previous year !!!");
                return;
            }
            counter_prev = alb.GetAllCounters(prevYear.ToString());
            if (counter_prev != null && counter_prev.Count > 0)
            {
                string frm = "frmNewYear_counter";
                bool isOk = false;
                foreach (IdModel im in counter_prev)
                {
                    try
                    {
                       isOk=alb.MakeCounter(ddlYear.SelectedItem.Text, im.idDaily, 0);
                        if (isOk == false)
                        {
                            translateRadMessageBox msg1 = new translateRadMessageBox();
                            msg1.translateAllMessageBox("Error writing Counter!");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Finish");
                btnDaily.Enabled = false;


            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No counters for previous year !!!");
                return;
            }


        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            beginStatement();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            AccDailyModel adm = new AccDailyModel();
            AccDailyMemModel amemom = new AccDailyMemModel();
            AccAcountUpdate aU = new AccAcountUpdate();
            AccLineBUS linebus = new AccLineBUS(ddlYear.SelectedItem.Text);
            if (multimodel != null && multimodel.Count > 0)
            {
                if (alm != null && alm.Count > 0)
                {

                    DialogResult dr = RadMessageBox.Show("Delete previous daily book ?", "Question", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        // procedure za brisanje starog pocetnog stanja
                        try
                        {
                            foreach (AccLineModel aold in alm)
                            {
                                aU.SubstractAmount(aold, this.Name, Login._user.idUser);    // rasknjizavanje iznosa iz AccLedgerAmounts;
                                linebus.Delete(aold.idAccLine, this.Name, Login._user.idUser); // brisanje iz lines
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    }

                }
                //== ubacivanje novog u frmdailyMemorial
             //   AccDailyMemModel dailyMemo = new AccDailyMemModel();
             //   AccDailyMemModel db = new AccDailyMemModel();
             //   AccDailyMemBUS bus = new AccDailyMemBUS(Login._bookyear);

             ////  db = bus.GetLastMemByStatement(selectedDaily.idDaily.ToString());

             //   string RefNo = "0";
             //   string strBookYear = ddlYear.SelectedItem.Text;//Login._bookyear;
             //   bool beginPeriod = true;
             //   if (db != null)
             //   {
             //       RefNo = (db.refNo + 1).ToString();
             //       strBookYear = db.bookingYear;
             //    ////   if (db.beginPeriod == true)
             //    //       beginPeriod = true;
             //   }

             //   dailyMemo.codeDaily = xDaily.ToString(); //selectedDaily.codeDaily;
             //   dailyMemo.refNo = Int32.Parse(RefNo);
             //   dailyMemo.dtMem = DateTime.Now;
             //   dailyMemo.bookingYear = strBookYear;
             //   dailyMemo.beginPeriod = beginPeriod;
             //   dailyMemo.userCreated = Login._user.idUser;
             //   dailyMemo.idDailyMem = bus.SaveAndReturnID(dailyMemo, this.Name, Login._user.idUser);
             //   aaa = dailyMemo.idDailyMem;
             //   dailyMemo.bookingYear = ddlYear.SelectedItem.Text;



                AccDailyModel daily = new AccDailyModel();
                daily.idDaily = xDaily;
                frmDailyMemorial fdm = new frmDailyMemorial(daily, admmod, multimodel);
                fdm.ShowDialog();

                multimodel = new List<AccLineModel>();
                alm = new List<AccLineModel>();
                gridNew.DataSource = null;
                gridNew.DataSource = multimodel;
                gridOld.DataSource = null;
                gridOld.DataSource = alm;

                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Finish");

            }
        }

        #endregion Buttons

        private void ddlYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            string strYear="";
            if (ddlYear.SelectedItem != null)
                strYear = ddlYear.SelectedItem.Text;

            AccSettingsBUS asb = new AccSettingsBUS();
            AccSettingsModel asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(strYear);
            if (asm != null)
                btnSettings.Enabled = false;
            else
                btnSettings.Enabled = true;

            AccDailyBUS adb = new AccDailyBUS(strYear);
            List<AccDailyModel> adm = new List<AccDailyModel>();
            adm = adb.GetAllDailysList();
            if (adm != null && adm.Count > 0)
                btnDaily.Enabled = false;
            else
                btnDaily.Enabled = true;
        }

      private void beginStatement()
        {
            AccDailyBUS adb = new AccDailyBUS(ddlYear.SelectedItem.Text);
            List<AccDailyModel> adm = new List<AccDailyModel>();
            adm = adb.GetBeginDaily();
          if (adm == null)
          {
              translateRadMessageBox msg = new translateRadMessageBox();
              msg.translateAllMessageBox("No Daily Memoriaal for Begin balance !");
              return;
          }
          else
          {
              if (adm.Count > 1)
              {
                  translateRadMessageBox msg = new translateRadMessageBox();
                  msg.translateAllMessageBox("There are more then one Daily Memoriaal for Begin balance !");
                  return;
              }
              else
              {
                  xDaily = Convert.ToInt32(adm[0].codeDaily);
              }
          }

          // cita AccDailyMem
          AccDailyMemBUS admb = new AccDailyMemBUS(ddlYear.SelectedItem.Text);
          admmod = new AccDailyMemModel();
          admmod = admb.GetMemosById(adm[0].idDaily);
          if (admmod == null)
          {
              admmod = new AccDailyMemModel();
              admmod.bookingYear = ddlYear.SelectedItem.Text;
              admmod.codeDaily = xDaily.ToString();
              admmod.refNo = 0;
              admmod.beginPeriod = true;
              admmod.userCreated = Login._user.idUser;
              string begdatum = ddlYear.SelectedItem.Text + "-01-01";
              admmod.dtMem = Convert.ToDateTime(begdatum);
              int xMemo;
              try
              {
                 xMemo=admb.SaveAndReturnID(admmod,"frmBeginYear",Login._user.idUser);
                 if (xMemo != null && xMemo > 0)
                 {
                     admmod.idDailyMem = xMemo;
                     aaa = xMemo;
                 }
              }
              catch(Exception em)
              {

              }
          }
          else
          {
              aaa = admmod.idDailyMem;
          }
          //=== provera da li ima vec unetih slogova 
          AccLineBUS alb = new AccLineBUS(ddlYear.SelectedItem.Text);
          alm = new List<AccLineModel>();
          xDaily = adm[0].idDaily;
          alm = alb.GetAllLinesByDaily(adm[0].idDaily, 0);
          if (alm != null && alm.Count > 0)
          {
              gridOld.DataSource = null;
              gridOld.DataSource = alm;
          }
          newBegin();
       
        }
        private void newBegin()
      {
          int prevYear = Convert.ToInt32(ddlYear.SelectedItem.Text) - 1;
          AccLineBUS alb = new AccLineBUS(prevYear.ToString());
          AccSettingsBUS sb = new AccSettingsBUS();
          AccSettingsModel smo = new AccSettingsModel();
          smo = sb.GetSettingsByID(prevYear.ToString());
            if (smo != null)
            {
                if (smo.defCreditorAccount != null && smo.defDebitorAccount != null)
                {
                    albm = new List<AccLineBeginModel>();
                    albm = alb.GetBeginAmounts(prevYear.ToString(), smo.defCreditorAccount, smo.defDebitorAccount);
                    if (albm == null || albm.Count <= 0)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("No data for previous year!");
                        return;
                    }
                    fillItems();
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Can't read settings for previous year!");
                return;
            }
        
        
      }
        private void fillItems()
        {
            multimodel = new List<AccLineModel>();
            AccLineModel model = new AccLineModel();
            if (albm.Count > 0)
            {
                foreach(AccLineBeginModel abm in albm)
                {
                    if (abm.diff != 0)
                    {
                        model = new AccLineModel();
                        string begdatum = ddlYear.SelectedItem.Text + "-01-01";
                        model.dtLine = Convert.ToDateTime(begdatum);
                        model.bookingYear = ddlYear.SelectedItem.Text;
                        model.userCreated = Login._user.idUser;
                        model.descLine = txtDescription.Text;
                        model.periodLine = 0;
                        model.idCurrency = aaa;
                        model.statusLine = false;
                        model.numberLedAccount = abm.numberLedAccount;
                        model.idClientLine = abm.idClientLine;
                        //if (model.idClientLine != null)
                        //   model.idClientLine = abm.idClientLine;
                        model.idAccDaily = xDaily;
                        if (abm.diff > 0)
                        {
                            model.debitLine = abm.diff;
                            model.creditLine = 0;
                        }
                        else
                        {
                            model.creditLine = abm.diff*-1;
                            model.debitLine = 0;
                        }
                        //if (model.idCostLine != null)
                        //    model.idCostLine = abm.idCostLine;
                        //if (model.idProjectLine != null)
                        //    model.idProjectLine = abm.idProjectLine;

                        multimodel.Add(model);
                    }
                }
                findAllDifference();
                gridNew.DataSource = null;
                gridNew.DataSource = multimodel;
               
            }
        }
        private void findAllDifference()
        {
            AccSettingsBUS asb = new AccSettingsBUS();
            AccSettingsModel asm = new AccSettingsModel();
            AccSettingsModel previus = new AccSettingsModel();
            string diferent_account = "";
            if (ddlYear.SelectedItem != null)
            {
                if (ddlYear.SelectedItem.Text != null && ddlYear.SelectedItem.Text != "")
                {
                    asm = asb.GetSettingsByID(ddlYear.SelectedItem.Text);
                    if (asm != null)
                    {
                        if (asm.yearSettings == ddlYear.SelectedItem.Text)
                            diferent_account = asm.defTransferingAcc;
                    }
                }
            }
            int prevYear = Convert.ToInt32(ddlYear.SelectedItem.Text) - 1;
            AccLineBUS lbs = new AccLineBUS(prevYear.ToString());
            List<AccLineBeginModel> four = new List<AccLineBeginModel>();
            List<AccLineBeginModel> eight = new List<AccLineBeginModel>();
            four = lbs.GetBeginSUM4Amounts(prevYear.ToString());
            eight = lbs.GetBeginSUM8Amounts(prevYear.ToString());
            decimal sum4 = 0;
            decimal sum8 = 0;
            decimal versil = 0;
            //==== sabira 4xxxxx konta
            if (four != null)
                if (four.Count >0)
                {
                    foreach (AccLineBeginModel fm in four)
                    {
                        sum4 = sum4 + fm.diff;
                    }
                }
            //===== sabira 8xxxxx konta
            //if (eight != null)
            //    if (eight.Count > 0)
            //    {
            //        foreach (AccLineBeginModel fe in eight)
            //        {
            //            sum8 = sum8 + fe.diff;
            //        }
            //    }

            decimal totaldiff = 0;
             decimal totaldiff_D = 0;
             decimal totaldiff_C = 0;
            if (multimodel != null)
            {
                if (multimodel.Count > 0)
                {
                  //  versil = sum4 - Math.Abs(sum8);

                    AccLineModel new1 = new AccLineModel();
                    if (sum4 > 0)
                        new1.debitLine = Math.Abs(sum4);
                    else
                        new1.creditLine = Math.Abs(sum4);
                    if (diferent_account.Trim() != "")
                        new1.numberLedAccount = diferent_account;
                    else
                        new1.numberLedAccount = "9990";
                    new1.periodLine = 0;
                    new1.idAccDaily = xDaily;
                    new1.idCurrency = aaa;
                    string begdatum = ddlYear.SelectedItem.Text + "-01-01";
                    new1.dtLine = Convert.ToDateTime(begdatum);
                    new1.descLine = txtDescription.Text;

                    multimodel.Add(new1);                // ubacuje razliku izmedju 4 i 8 konta jedna strana

                    //AccLineModel new2 = new AccLineModel();
                    //if (sum4 > 0)
                    //    new2.creditLine = Math.Abs(sum4);
                    //else
                    //    new2.debitLine = Math.Abs(sum4);
                    //if (diferent_account.Trim() != "")
                    //    new2.numberLedAccount = diferent_account;
                    //else
                    //    new2.numberLedAccount = "9990";
                    //new2.periodLine = 0;
                    //new2.idAccDaily = xDaily;
                    //new2.idCurrency = aaa;
                    //new2.descLine = txtDescription.Text;
                   
                    //new2.dtLine = Convert.ToDateTime(begdatum);


                    //multimodel.Add(new2);                 // ubacuje razliku izmedju 4 i 8 konta druga strana strana

                    foreach (AccLineModel lm in multimodel)
                    {
                        totaldiff_D = totaldiff_D + lm.debitLine;
                        totaldiff_C = totaldiff_C + lm.creditLine;
                    }
                    totaldiff = totaldiff_D - Math.Abs(totaldiff_C);
                    if (totaldiff != 0)
                    {
                        AccLineModel new3 = new AccLineModel();
                        if (totaldiff > 0)
                            new3.creditLine = Math.Abs(totaldiff);
                        else
                            new3.debitLine = Math.Abs(totaldiff);
                        if (asm.defDifferenceAcc.Trim() != "")
                            new3.numberLedAccount = asm.defDifferenceAcc;
                        else
                            new3.numberLedAccount = "999999";
                        //new3.numberLedAccount = "999999";
                        new3.descLine = "Difference balans";
                        new3.periodLine = 0;
                        new3.idAccDaily = xDaily;
                        new3.idCurrency = aaa;
                        new3.dtLine = Convert.ToDateTime(begdatum);
                        new3.descLine = txtDescription.Text;

                        multimodel.Add(new3);                 // ubacuje razliku ako je ima
                    }

                }
            }
        }

      # region Grid
        private void gridNew_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridNew != null)
            {
                if (gridNew.Columns.Count > 0)
                {
                    for (int i = 0; i < gridNew.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridNew.Columns[i].HeaderText != null && resxSet.GetString(gridNew.Columns[i].HeaderText) != null)
                                gridNew.Columns[i].HeaderText = resxSet.GetString(gridNew.Columns[i].HeaderText);
                        }
                    }
                }
            }
            if (gridNew.Columns != null)
            {
                if (gridNew.RowCount > 0)
                {
                    this.gridNew.Columns["dtLine"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridNew.Columns["dtBooking"].FormatString = "{0: dd-MM-yyyy}";
                }
            }
            if (File.Exists(layoutNewBegin))
            {
                gridNew.LoadLayout(layoutNewBegin);
            }
            //if (File.Exists(layoutBankCreditPay))
            //{
            //    gridBank.LoadLayout(layoutBankCreditPay);
            //}
            //if (gridNew.Columns != null && gridNew.Columns.Count > 0)
            //    gridNew.Columns["date"].FormatString = "{0: dd/MM/yyyy}";
            // if (gridNew.Columns != null && gridNew.Columns.Count > 0)
            //    gridNew.Columns["amount"].FormatString = "{0:N2}";
        }

      private void gridOld_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
      {
          if (gridOld != null)
          {
              if (gridOld.Columns.Count > 0)
              {
                  for (int i = 0; i < gridOld.Columns.Count; i++)
                  {
                      using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                      {
                          if (gridOld.Columns[i].HeaderText != null && resxSet.GetString(gridOld.Columns[i].HeaderText) != null)
                              gridOld.Columns[i].HeaderText = resxSet.GetString(gridOld.Columns[i].HeaderText);
                      }
                  }
              }
          }
          if (gridOld.Columns != null)
          {
              if (gridOld.RowCount > 0)
              {
                  this.gridOld.Columns["dtLine"].FormatString = "{0: dd-MM-yyyy}";
                  this.gridOld.Columns["dtBooking"].FormatString = "{0: dd-MM-yyyy}";
              }
          }
          if (File.Exists(layoutOldBegin))
          {
              gridOld.LoadLayout(layoutOldBegin);
          }
      }
      private void gridOld_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
      {
          string saveLayout = "Save Layout";
          using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
          {

              if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                  saveLayout = resxSet.GetString(saveLayout);
          }
          RadMenuItem customMenuItem = new RadMenuItem();
          customMenuItem.Text = saveLayout;
          customMenuItem.Click += SaveLayoutOld;
          RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
          e.ContextMenu.Items.Add(separator);
          e.ContextMenu.Items.Add(customMenuItem);
      }
      private void SaveLayoutOld(object sender, EventArgs e)
      {
          if (File.Exists(layoutOldBegin))
          {
              File.Delete(layoutOldBegin);
          }
          gridOld.SaveLayout(layoutOldBegin);

          translateRadMessageBox msg = new translateRadMessageBox();
          msg.translateAllMessageBox("You have successfully save layout!");

      }

      private void gridNew_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
      {
          string saveLayout = "Save Layout";
          using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
          {

              if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                  saveLayout = resxSet.GetString(saveLayout);
          }
          RadMenuItem customMenuItem = new RadMenuItem();
          customMenuItem.Text = saveLayout;
          customMenuItem.Click += SaveLayoutNew;
          RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
          e.ContextMenu.Items.Add(separator);
          e.ContextMenu.Items.Add(customMenuItem);
      }
      private void SaveLayoutNew(object sender, EventArgs e)
      {
          if (File.Exists(layoutNewBegin))
          {
              File.Delete(layoutNewBegin);
          }
          gridOld.SaveLayout(layoutNewBegin);

          translateRadMessageBox msg = new translateRadMessageBox();
          msg.translateAllMessageBox("You have successfully save layout!");

      }

      private void gridNew_ViewCellFormatting(object sender, CellFormattingEventArgs e)
      {
          if (e.CellElement is GridSummaryCellElement)
          {
              if (!String.IsNullOrEmpty(e.CellElement.Text))
              {
                  e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                  e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                  e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
              }
              else
              {
                  e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                  e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
              }
          }
      }

      private void gridOld_ViewCellFormatting(object sender, CellFormattingEventArgs e)
      {
          if (e.CellElement is GridSummaryCellElement)
          {
              if (!String.IsNullOrEmpty(e.CellElement.Text))
              {
                  e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                  e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                  e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
              }
              else
              {
                  e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                  e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
              }
          }
      }


      #endregion Grid

      private void radButton1_Click(object sender, EventArgs e)  // usuzna funkcija sa "bu" obrise sve iznose za konto i bookingyear i ponovo proknjizi
      {
          string prevYear = Login._bookyear;
          AccAcountUpdate au = new AccAcountUpdate();
          AccLineBUS lineBus = new AccLineBUS(prevYear);
          List<AccLineModel> modelL = new List<AccLineModel>();

          modelL = lineBus.GetAllLinesYear(prevYear);
          if (modelL!=null)
          {
              if (modelL.Count > 0)
              {
                  AccLedgerAmountsBUS amb = new AccLedgerAmountsBUS();
                  List<AccLedgerAmountsModel> mod = new List<AccLedgerAmountsModel>();
                  mod = amb.GetAllAmounts(prevYear);
                  if (mod != null)
                      foreach(AccLedgerAmountsModel am in mod)
                      {
                          amb.Delete(am.numberLedgerAccount, prevYear, this.Name, Login._user.idUser);
                      }


                  foreach (AccLineModel alm in modelL)
                  {
                      au.AddAmount(alm, this.Name, Login._user.idUser);
                  }
              }
          }


      }

    

   

  

    }
}
