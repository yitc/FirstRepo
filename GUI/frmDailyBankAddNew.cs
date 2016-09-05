using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.Model;
using System.Resources;

namespace GUI
{
    public partial class frmDailyBankAddNew : frmTempAccount
    {
        public AccDailyBankModel dailyBank;
        public AccDailyMemModel dailyMemo;
        public AccDailyKasModel dailyKas;
        private string daily = "";
       

        public frmDailyBankAddNew(string xdaily, int idDaily)
        {
            daily = xdaily;
           
            InitializeComponent();

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + Login._bookyear;
            formName = formName + " " + this.Text;
            this.Text = formName;


            if(idDaily == 1)
            {
                this.dailyBank = new AccDailyBankModel();
                this.dailyMemo = null;
                this.dailyKas = null;
            }

            if (idDaily == 4)
            {
                this.dailyBank = null;
                this.dailyKas = null;
                this.dailyMemo = new AccDailyMemModel(); 
            }

            if (idDaily == 5)
            {
                this.dailyBank = null;
                this.dailyMemo = null;
                this.dailyKas = new AccDailyKasModel();
            }

            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Add New Daily";
        }
        
        private void frmDailyBankAddNew_Load(object sender, EventArgs e)
        {

          

            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnBooking.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteItem.Visibility = ElementVisibility.Collapsed;
            btnNewItem.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteItem.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonExit.Visibility = ElementVisibility.Collapsed;
            btnBooking.Visibility = ElementVisibility.Collapsed;
            btnDelete.Visibility = ElementVisibility.Collapsed;
            btnExit.Visibility = ElementVisibility.Visible;
            btnNew.Text = "Save";


            radDateTimePickerDailyBank.Value = DateTime.Now;

            if(dailyBank != null)
            {
                lblBEginSaldo.Visible = true;
                lblEndSaldo.Visible = true;
                lblDescription.Text = "Statement"; 
                numericBeginSaldo.Visible = true;
                numericEndSaldo.Visible = true;
                rbBank.Visible = false;
                rbKas.Visible = false;
                lblBeginPeriod.Visible = false;
                chkBegin.Visible = false;

                txtCodeDaily.Text = daily;
                if (txtCodeDaily.Text != "")
                    txtCodeDaily.ReadOnly = true;
                txtRefNo.ReadOnly = true;
                AccDailyBankBUS abb = new AccDailyBankBUS(Login._bookyear);
                AccDailyBankModel abm = new AccDailyBankModel();
                abm = abb.GetLastBank(daily);
                    if(abm != null)
                    {
                        numericBeginSaldo.Text = abm.endSaldo.ToString();
                        txtRefNo.Text = (abm.refNo + 1).ToString();
                        txtBookingYear.Text = abm.bookingYear;
                    }
                    else
                    {
                        txtRefNo.Text = "1";
                        txtBookingYear.Text = Login._bookyear;
                    }

                

            }

            if(dailyMemo != null)
            {

                lblBEginSaldo.Visible = false;
                lblEndSaldo.Visible = false;
                lblDescription.Text = "Memoriaal nr."; 
                numericBeginSaldo.Visible = false;
                numericEndSaldo.Visible = false;
                rbBank.Visible = false;
                rbKas.Visible = false;
                btnAddPdf.Visible = false;
                txtCodeDaily.Text = daily;
                if (txtCodeDaily.Text != "")
                    txtCodeDaily.ReadOnly = true;
                AccDailyMemBUS abb = new AccDailyMemBUS(Login._bookyear);
                AccDailyMemModel abm = new AccDailyMemModel();
                abm = abb.GetLastMemByStatement(daily);
                if (abm != null)
                {
                    //numericBeginSaldo.Text = abm.endSaldo.ToString();
                    txtRefNo.Text = (abm.refNo + 1).ToString();
                    txtBookingYear.Text = abm.bookingYear;
                    if (abm.beginPeriod == true)
                        chkBegin.CheckState = CheckState.Checked;
                    else
                        chkBegin.CheckState = CheckState.Unchecked;
                }
                else
                {
                    txtRefNo.Text = "1";
                    txtBookingYear.Text = Login._bookyear;
                }
                
            }

            if (dailyKas != null)
            {
                lblBEginSaldo.Visible = true;
                lblEndSaldo.Visible = true;
                lblDescription.Text = "Kas nr.";
                numericBeginSaldo.Visible = true;
                numericEndSaldo.Visible = true;
                rbBank.Visible = false;
                rbKas.Visible = false;
                btnAddPdf.Visible = false;
                lblBeginPeriod.Visible = false;
                chkBegin.Visible = false;
                txtCodeDaily.Text = daily;
                if (txtCodeDaily.Text != "")
                    txtCodeDaily.ReadOnly = true;
                txtRefNo.ReadOnly = true;
                AccDailyKasBUS abb = new AccDailyKasBUS();
                AccDailyKasModel abm = new AccDailyKasModel();
                abm = abb.GetLastKas();
                if (abm != null)
                {
                    numericBeginSaldo.Text = abm.endSaldo.ToString();
                    txtRefNo.Text = (abm.refnoKas + 1).ToString();
                    txtBookingYear.Text = abm.bookingYear;
                }
                else
                {
                    txtRefNo.Text = "1";
                    txtBookingYear.Text = Login._bookyear;
                }

            }

            //clean code // depends on form type kass or bank
            AccDailyModel acdm = new AccDailyModel();
            acdm = new AccDailyBUS(txtBookingYear.Text).GetDailysByCode(daily);

            string descDaily = "";
            if (acdm != null)
            {
                descDaily = acdm.descDaily;
            }
            this.Text = this.Text + " " + descDaily;

            //translation
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDate.Text) != null)
                    lblDate.Text = resxSet.GetString(lblDate.Text);

                if (resxSet.GetString(lblBeginPeriod.Text) != null)
                    lblBeginPeriod.Text = resxSet.GetString(lblBeginPeriod.Text);

                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);

                //if (resxSet.GetString(radLabelCredit.Text) != null)
                //    radLabelCredit.Text = resxSet.GetString(radLabelCredit.Text);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if(txtCodeDaily.Text.Trim() == "")
            {
                RadMessageBox.Show("Code daily not entered");
                return;
            }
            if (txtRefNo.Text.Trim() == "")
            {
                RadMessageBox.Show("Description not entered");
                return;
            }

            if (dailyBank != null)
            {
                DialogResult dr = RadMessageBox.Show("Save changes ?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        AccDailyBankBUS bus = new AccDailyBankBUS(Login._bookyear);
                        dailyBank.codeDaily = txtCodeDaily.Text;
                        dailyBank.refNo = Convert.ToInt32(txtRefNo.Text);
                        dailyBank.dtStatement = radDateTimePickerDailyBank.Value;
                        dailyBank.begSaldo = Convert.ToDecimal(numericBeginSaldo.Text);               //  numericBeginSaldo.Value;
                        dailyBank.endSaldo = Convert.ToDecimal(numericEndSaldo.Text);
                        dailyBank.bookingYear = Login._bookyear;//txtBookingYear.Text;

                        dailyBank.dtCreated = DateTime.Now;
                        dailyBank.userCreated = Login._user.idUser;
                        //numericEndSaldo.Value;
                        //if (rbBank.CheckState == CheckState.Checked)
                        //     dailyBank.bankKas = 1;
                        //if (rbKas.CheckState == CheckState.Checked)


                        dailyBank.idDailyBank = bus.SaveAndReturnID(dailyBank, this.Name, Login._user.idUser);

                        this.DialogResult = DialogResult.Yes;
                        this.Close();

                    }
                    catch(Exception ex)
                    {
                        RadMessageBox.Show(ex.Message);
                    }
                }
            }

            if (dailyMemo != null)
            {
                DialogResult dr = RadMessageBox.Show("Save changes ?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        AccDailyMemBUS bus = new AccDailyMemBUS(Login._bookyear);
                        dailyMemo.codeDaily = txtCodeDaily.Text;
                        dailyMemo.refNo = Convert.ToInt32(txtRefNo.Text);
                        dailyMemo.dtMem = radDateTimePickerDailyBank.Value;
                        dailyMemo.bookingYear = Login._bookyear; // txtBookingYear.Text;
                        if (chkBegin.CheckState == CheckState.Checked)
                            dailyMemo.beginPeriod = true;
                        else
                            dailyMemo.beginPeriod = false;
                        dailyMemo.idDailyMem = bus.SaveAndReturnID(dailyMemo, this.Name, Login._user.idUser);
                       
                        this.DialogResult = DialogResult.Yes;
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        RadMessageBox.Show(ex.Message);
                    }
                }
            }
            if (dailyKas != null)
            {
                DialogResult dr = RadMessageBox.Show("Save changes ?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        AccDailyKasBUS bus = new AccDailyKasBUS();
                        dailyKas.codeDaily = txtCodeDaily.Text;
                        dailyKas.refnoKas = Convert.ToInt32(txtRefNo.Text);
                        dailyKas.dtKas = radDateTimePickerDailyBank.Value;
                        dailyKas.begSaldo = Convert.ToDecimal(numericBeginSaldo.Text);               //  numericBeginSaldo.Value;
                        dailyKas.endSaldo = Convert.ToDecimal(numericEndSaldo.Text);                 //numericEndSaldo.Value;
                        dailyKas.bookingYear = Login._bookyear; // txtBookingYear.Text;

                        dailyKas.dtCreated = DateTime.Now;
                        dailyKas.userCreated = Login._user.idUser;
                        //if (rbBank.CheckState == CheckState.Checked)
                        //     dailyBank.bankKas = 1;
                        //if (rbKas.CheckState == CheckState.Checked)
                       // dailyKas.bankKas = 0;

                        dailyKas.idAccDailyKas = bus.SaveAndReturnID(dailyKas, this.Name, Login._user.idUser);

                        this.DialogResult = DialogResult.Yes;
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        RadMessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnAddPdf_Click(object sender, EventArgs e)
        {
            if (dailyBank != null)
            {
                if (dailyBank.pdfFile != null && dailyBank.pdfFile != "") // ima dokument
                {
                    string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
                    sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Documents\\";
                    string fullname = dailyBank.pdfFile;
                    if (System.IO.File.Exists(fullname))
                        OpenDocument( dailyBank.pdfFile);  // sDest,
                    else
                        RadMessageBox.Show("Error opening document", "Open error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
                else
                {
                    OpenFileDialog dialog = new OpenFileDialog();

                    string ext = "pdf";//dtm.Find(item => item.typeDocument.TrimEnd() == txtext.Text.TrimEnd()).extendDocumentType;
                    dialog.Filter = "( *." + ext + ")|*." + ext + "|All Files (*.*)|*.*";
                    string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
                    sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Documents\\";
                    dialog.InitialDirectory = sDest;
                    dialog.Title = "Please select a file";

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string sFile = dialog.FileName;
                        string sFileName = sFile.Split('\\')[sFile.Split('\\').Length - 1];
                        
                       // txtdocName.Text = CreateDocName(idConstPers) + "." + ext;


                        string fullname = sFile;
                        if (dailyBank != null)
                            dailyBank.pdfFile = fullname;
                        bool bOpen = true;
                        //try
                        //{
                        //    if (!System.IO.Directory.Exists(sDest))
                        //        System.IO.Directory.CreateDirectory(sDest);

                        //   // System.IO.File.Copy(dialog.FileName, fullname, true);
                        //}
                        //catch (Exception ex)
                        //{
                        //    bOpen = false;
                        //    RadMessageBox.Show("Error copying document.\nMessage: " + ex.Message, "Copy error", MessageBoxButtons.OK, RadMessageIcon.Error);
                        //}

                        if (bOpen)
                            OpenDocument(dailyBank.pdfFile); //sDest, 
                    }     
                }
            }
        }
        private void OpenDocument(string sFileName)  //string sDest,
        {
            string sExtention = sFileName.Split('.')[sFileName.Split('.').Length - 1];
            string sFullName = sFileName;  // sDest + 
            System.Diagnostics.Process.Start(sFullName);
        }
    }
}
