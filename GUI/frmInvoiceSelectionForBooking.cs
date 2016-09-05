using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls.UI;
using System.IO;
using System.Diagnostics;

namespace GUI
{
    public partial class frmInvoiceSelectionForBooking : Telerik.WinControls.UI.RadForm
    {
        private int idArr = 0;

        private BindingList<InvoiceModel> limBind;

        readonly List<string> _tempFolders = new List<string>();
        string tempFolder;

        private string layoutInvoiceSelectionForBooking = MainForm.gridFiltersFolder + "\\layoutInvoiceSelectionForBooking.xml";

        public frmInvoiceSelectionForBooking()
        {
            InitializeComponent();
        }

        private void frmInvoiceSelectionForBooking_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;

            tempFolder = GetTemporaryFolder();
            _tempFolders.Add(tempFolder);

            ddlStatus.DataSource = new InvoiceStatusBUS().GeInvoiceStatus(6);
            ddlStatus.DisplayMember = "descInvoiceStatus";
            ddlStatus.ValueMember = "idInvoiceStatus";

            ddlLabel.DataSource = new LabelsBUS().GetDistinctLabels();
            ddlLabel.DisplayMember = "nameLabel";
            ddlLabel.ValueMember = "idLabel";

            limBind = new BindingList<InvoiceModel>();

            rgvInvoice.DataSource = limBind;

            setTranslation();
        }

        private static string GetTemporaryFolder()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);

                if (resxSet.GetString(radioSearchByArr.Text) != null)
                    radioSearchByArr.Text = resxSet.GetString(radioSearchByArr.Text);
                if (resxSet.GetString(radioSeatchByLabel.Text) != null)
                    radioSeatchByLabel.Text = resxSet.GetString(radioSeatchByLabel.Text);
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);                
                if (resxSet.GetString(btnExit.Text) != null)
                    btnExit.Text = resxSet.GetString(btnExit.Text);
                if (resxSet.GetString(radMenuButtonSaveLayout.Text) != null)
                    radMenuButtonSaveLayout.Text = resxSet.GetString(radMenuButtonSaveLayout.Text);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                radioByEmail.IsChecked = false;
                radioByPost.IsChecked = false;
                btnSendByEmail.Visible = false;
                btnSendByPost.Visible = false;

                if (radioSearchByArr.IsChecked == true)
                {
                    if(txtArrangement.Text.Trim() == "")
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        msgbox.translateAllMessageBox("Select arrangement first");
                        return;
                    }
                    InvoiceBUS ib = new InvoiceBUS();
                    List<InvoiceModel> lim = new List<InvoiceModel>();
                    lim = ib.GetInvoicesForPrintForBooking(idArr, (int)ddlStatus.SelectedValue);

                    if (lim != null)
                    {
                        limBind.Clear();
                        foreach (InvoiceModel m in lim)
                        {                         
                            limBind.Add(m);
                        }

                    }
                    else
                        limBind.Clear();
                }
                else if (radioSeatchByLabel.IsChecked == true)
                {
                    InvoiceBUS ib = new InvoiceBUS();
                    List<InvoiceModel> lim = new List<InvoiceModel>();
                    lim = ib.GetInvoicesForPrintForBookingByLabel((int)ddlLabel.SelectedValue, (int)ddlStatus.SelectedValue);
                    if (lim != null)
                    {
                        limBind.Clear();
                        foreach (InvoiceModel m in lim)
                        {                            
                            limBind.Add(m);
                        }
                    }
                    else
                        limBind.Clear();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

            Cursor.Current = Cursors.Default;

        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            ArrangementBUS ccentar1 = new ArrangementBUS();
            List<IModel> gmX1 = new List<IModel>();

            gmX1 = ccentar1.GetAllArrangements();
            var dlgSave1 = new GridLookupForm(gmX1, "Arrangement");

            if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel genmX1 = new ArrangementModel();
                genmX1 = (ArrangementModel)dlgSave1.selectedRow;
                //set textbox
                if(genmX1!=null)
                txtArrangement.Text = genmX1.codeArrangement + " - " + genmX1.nameArrangement;
                idArr = genmX1.idArrangement;
                
                //invStatus = ddlStatus.SelectedIndex + 1;
                //getdata();


            }
        }

        private void StartProcessingDocuments(bool printoremail, string printername, string subject, string body_message)
        {
            List<InvoiceModel> invoiceForPrint = new List<InvoiceModel>();
            List<InvoiceModel> personsToPrint = new List<InvoiceModel>();

            //dodaj prvo cekirane koji su za print
            foreach (InvoiceModel m in limBind)
            {
                if (m.select == true)
                    invoiceForPrint.Add(m);
            }

            if (invoiceForPrint.Count <= 0)
            {
                MessageBox.Show("Please select what you want to print.");
                return;
            }

            // onda od cekiranih izdvoji samo distinct persone
            foreach (InvoiceModel m in invoiceForPrint)
            {
                var isInList = personsToPrint.Find(s => s.idContPerson == m.idContPerson || s.idClient == m.idClient);
                if ((m.idContPerson != 0 || m.idClient != 0) && isInList == null)
                {
                    personsToPrint.Add(m);
                }
            }

            List<InvoiceModel> sendToPrinter = new List<InvoiceModel>();
            List<InvoiceModel> forStatusUpdate = new List<InvoiceModel>();
            documentsToBeSavedToDb.Clear();        
            foreach (var n in personsToPrint)
            {                
                sendToPrinter.Clear();                
                foreach(var i in invoiceForPrint)
                {
                    if (n.idContPerson == i.idContPerson)
                    {
                        sendToPrinter.Add(i);
                        forStatusUpdate.Add(i);
                    }
                }

                //uzima se idContPerson iz ArrangementBook za travelera, posto u invoiceu idContPers moze biti debitor
                int idTraveler = 0;
                ArrangementBookBUS bus = new ArrangementBookBUS();
                ArrangementBookModel model = bus.GetArrangementBook((int)n.idVoucher);
                if (model != null)
                    idTraveler = model.idContPers;

                if (printoremail == true)
                {
                  
                    PrintInvoice(printername, sendToPrinter, (int)n.idContPerson, idTraveler);
                }
                else
                {
                    EmailInvoice(sendToPrinter, subject, body_message, n.email, (int)n.idContPerson, idTraveler);
                }
                //PrintOrEmail(printoremail, printername, sendToPrinter, subject, body_message, n.email, (int)n.idContPerson);
            }
            UpdateInvoiceStatuses(forStatusUpdate, 2);
        }

        int idPersonForEmail = 0;
        int idArrangementForEmail = 0;


        private PrintReport PrintFirstPayment(bool is999, bool printoremail, InvoiceModel mod, int idArrangement, int idLabel, out string nameFileToSend)
        {
            InvoiceBUS ib001_999 = new InvoiceBUS();
            DataTable invoice_999 = new DataTable();
            DataTable itemsInv_999 = new DataTable();
            
            if (is999 == false)
                invoice_999 = ib001_999.GetReportInvoiceByIntID(mod.idInvoice);
            else
                invoice_999 = ib001_999.GetReportInvoiceByIntID999(mod.idInvoice);


            DataTable items000_999 = new DataTable();
            DataTable items001_999 = new DataTable();
            InvoiceItemsBUS rptiib_999 = new InvoiceItemsBUS();

            if (invoice_999 != null)
            {
                if (invoice_999.Rows.Count > 0)
                {
                    DataRow dr = invoice_999.Rows[0];
                    itemsInv_999 = rptiib_999.GetReportInvoiceItemsByID(mod.idInvoice, Login._user.lngUser);

                    InvoiceModel im000 = new InvoiceModel();
                    im000 = ib001_999.GetInvoiceByInvoiceAndExtension999(mod.invoiceNr, "000", is999);
                    if (im000 != null)
                        if (im000.idInvoice != null && im000.idInvoice != 0)
                        {
                            items000_999 = rptiib_999.GetReportInvoiceItemsByID(im000.idInvoice, Login._user.lngUser);

                            dr["firstAmount"] = im000.brutoAmount;
                            dr["firstReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                            dr["restAmount"] = im000.brutoAmount;
                            dr["restReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                        }

                    InvoiceModel im001 = new InvoiceModel();
                    im001 = ib001_999.GetInvoiceByInvoiceAndExtension999(mod.invoiceNr, "001", is999);
                    if (im001.idInvoice != null && im001.idInvoice != 0)
                    {
                        items001_999 = rptiib_999.GetReportInvoiceItemsByID(im001.idInvoice, Login._user.lngUser);
                        if (im000 != null)
                        {
                            dr["firstAmount"] = im000.brutoAmount;
                            dr["firstReference"] = im000.idContPerson + "/" + im000.invoiceNr + "-" + im000.invoiceRbr;
                        }
                        else
                        {
                            dr["dtFirstPay"] = DBNull.Value;
                        }
                        dr["restAmount"] = im001.brutoAmount;
                        dr["restReference"] = im001.idContPerson + "/" + im001.invoiceNr + "-" + im001.invoiceRbr;
                        dr["noteInvoice"] = im001.noteInvoice;
                    }
                    decimal ukupno = Convert.ToDecimal(im001.brutoAmount);
                    if (im000 != null)
                    {
                        ukupno = Convert.ToDecimal(im000.brutoAmount) + Convert.ToDecimal(im001.brutoAmount);
                    }
                    dr["netoAmount"] = ukupno;
                }
                // cita grid za putnike
                BIS.DAO.ArrangementBookPersonsDAO bus = new BIS.DAO.ArrangementBookPersonsDAO();
               
                items000_999 = bus.GetAllTravelersInvoicing(Convert.ToInt32(mod.idVoucher), true); // ubaceno za osobe za koje se placa
                //items000 = bus.GetAllTravelersWith(Convert.ToInt32(model.idContPerson), abm.idArrangement);

                if (items000_999 == null)
                    items000_999 = new DataTable();

                if (items001_999 == null)
                    items001_999 = new DataTable();

                if (invoice_999 != null && invoice_999.Rows.Count > 0)
                {
                    nameFileToSend = "Factuur_" + mod.invoiceNr.ToString().Trim() + "-" + invoice_999.Rows[0] ["invoiceRbr"].ToString().TrimEnd()+ ".pdf";

                     if (Convert.ToDecimal(invoice_999.Rows[0] ["brutoAmount"].ToString())<0)
                         nameFileToSend = "Factuur_" + mod.invoiceNr.ToString().Trim() + "-" + invoice_999.Rows[0]["invoiceRbr"].ToString().TrimEnd() + "c" + ".pdf";
                    //frmInvoiceOneReport fmOne = new frmInvoiceOneReport(invoice_999, items001_999, items000_999, nameFileToSend, idLabel, true, true);
                    //fmOne.ShowDialog();

                    PrintReport pr = new PrintReport(invoice_999, items001_999, items000_999, nameFileToSend, idLabel, printoremail, false, false);
                    return pr;

                }
                else
                {
                    nameFileToSend = "";
                    return null;
                }
            }
            else
            {
                nameFileToSend = "";
                return null;
            }
        }



        List<InvoiceSelectionDocumentModel> documentsToBeSavedToDb = new List<InvoiceSelectionDocumentModel>();
        private void PrintInvoice(string printername, List<InvoiceModel> forPrinting, int idPerson, int idTraveler)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                string nameFileToSend = "";                        

                if (forPrinting != null && forPrinting.Count > 0)
                {
                    DocumentsBUS docbus = new DocumentsBUS();
                    DocumentsModel docmodel = new DocumentsModel();
                                        
                    foreach (InvoiceModel mod in forPrinting)
                    {
                        if (mod.select == true)
                        {
                            //
                            // get Label for showing exact version of report
                            //
                            int idLabel = 1;
                            int idArrangement = 0;
                            ArrangementModel am = new ArrangementModel();
                            am = new ArrangementBUS().GetArrangementByArrangementBook(Convert.ToInt32(mod.idVoucher));
                            List<LabelForArrangement> alm = new List<LabelForArrangement>();

                            if (am != null)
                            {
                                alm = new ArrangementBUS().GetLabelsArrangement(am.idArrangement);
                                idArrangementForEmail = am.idArrangement;
                            }
                            else
                                idArrangementForEmail = 0;

                            if (alm != null)
                                if (alm.Count > 0)
                                    idLabel = alm[0].idLabel;
                            // END GET LABEL

                            if (mod.invoiceRbr != "000" && mod.invoiceRbr != "001")
                            {
                                //if (mod.invoiceRbr == "999")
                                //{
                                //    PrintReport pr = PrintFirstPayment(true, true, mod, idArrangementForEmail, idLabel, out nameFileToSend);
                                    
                                //    //print
                                //    pr.RunTo(printername);
                                //    string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;
                                   
                                //    InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                                //    invmod.idContPers = idPerson;
                                //    invmod.idClient = 0;
                                //    invmod.descriptionDocument = "Printed Invoice PDF";
                                //    invmod.fileDocument = Path.GetFileName(savetopath);
                                //    invmod.typeDocument = "SCANS";
                                //    invmod.idDocumentStatus = 2;
                                //    invmod.idEmployee = 0;
                                //    invmod.idResponsableEmployee = 0;
                                //    invmod.inOutDocument = 2;
                                //    invmod.noteDocument = "";
                                //    invmod.idArrangement = idArrangementForEmail;
                                //    invmod.nameFileToSend = nameFileToSend;
                                //    invmod.report = pr;

                                //    invmod.dtCreated = DateTime.Now;
                                //    invmod.dtModified = DateTime.Now;
                                //    invmod.userCreated = Login._user.idUser;
                                //    invmod.userModified = Login._user.idUser;

                                //    documentsToBeSavedToDb.Add(invmod);
                                   
                                //}

                                List<InvoiceReportModel> irep = new List<InvoiceReportModel>();
                                InvoiceReportModel aa = new InvoiceReportModel();
                                List<InvoiceItemsReportModel> iirep = new List<InvoiceItemsReportModel>();
                                InvoiceBUS rptb = new InvoiceBUS();
                                InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
                                DataTable invoiceAll = new DataTable();
                                DataTable invoice = new DataTable();
                                DataTable itemsInv = new DataTable();

                               // if (mod.invoiceRbr != "999")
                                    invoiceAll = rptb.GetReportInvoiceByIntID(mod.idInvoice);
                               // else
                               //     invoiceAll = rptb.GetReportInvoiceFor999ByIntID(mod.idInvoice);


                                if ((invoiceAll != null) && (invoiceAll.Rows.Count > 0))
                                {
                                    //DataRow dr = invoice.Rows[0];
                                    foreach (DataRow dr in invoiceAll.Rows)
                                    {
                                        invoice = new DataTable();
                                        invoice = invoiceAll.Copy();
                                        invoice.Rows.Clear();
                                        invoice.Rows.Add(dr.ItemArray);
                                        if (dr["idContPerson"].ToString() == "")
                                            dr["idContPerson"] = mod.idContPerson;

                                        if (mod.invoiceRbr != "999")
                                        {
                                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(mod.idInvoice, Login._user.lngUser);
                                        }
                                        else
                                        {
                                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(Convert.ToInt32(dr["idInvoice"].ToString()), Login._user.lngUser);
                                            nameFileToSend = "Factuur-" + dr["invoiceNr"].ToString() + "-" + dr["invoiceRbr"].ToString() + ".pdf";
                                        }

                                        if (itemsInv == null)
                                            itemsInv = new DataTable();

                                        //frmInvoiceReport aaa = new frmInvoiceReport(invoice, itemsInv, nameFileToSend, idLabel, true);
                                        //aaa.ShowDialog();
                                        
                                        //print
                                        PrintReport pr = new PrintReport(invoice, itemsInv, nameFileToSend, idLabel, true, false, false);
                                        pr.RunTo(printername);
                                       
                                        string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;

                                        InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                                        invmod.idContPers = idTraveler;
                                        invmod.idClient = 0;
                                        invmod.descriptionDocument = "Printed Invoice PDF";
                                        invmod.fileDocument = Path.GetFileName(savetopath);
                                        invmod.typeDocument = "SCANS";
                                        invmod.idDocumentStatus = 2;
                                        invmod.idEmployee = 0;
                                        invmod.idResponsableEmployee = 0;
                                        invmod.inOutDocument = 2;
                                        invmod.noteDocument = "";
                                        invmod.idArrangement = idArrangementForEmail;
                                        invmod.nameFileToSend = nameFileToSend;
                                        invmod.report = pr;

                                        invmod.dtCreated = DateTime.Now;
                                        invmod.dtModified = DateTime.Now;
                                        invmod.userCreated = Login._user.idUser;
                                        invmod.userModified = Login._user.idUser;
                                                                                
                                        documentsToBeSavedToDb.Add(invmod);

                                                                               
                                        if (mod.invoiceRbr != "999")
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                PrintReport prRep;
                                if (mod.brutoAmount < 0)
                                {
                                    prRep = PrintFirstPayment(true, true, mod, idArrangementForEmail, idLabel, out nameFileToSend);
                                }
                                else
                                {
                                    prRep = PrintFirstPayment(false, true, mod, idArrangementForEmail, idLabel, out nameFileToSend);
                                }

                                prRep.RunTo(printername);
                                
                                string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;


                                InvoiceSelectionDocumentModel invmod = new InvoiceSelectionDocumentModel();
                                invmod.idContPers = idTraveler;
                                invmod.idClient = 0;
                                invmod.descriptionDocument = "Printed Invoice PDF";
                                invmod.fileDocument = Path.GetFileName(savetopath);
                                invmod.typeDocument = "SCANS";
                                invmod.idDocumentStatus = 2;
                                invmod.idEmployee = 0;
                                invmod.idResponsableEmployee = 0;
                                invmod.inOutDocument = 2;
                                invmod.noteDocument = "";
                                invmod.idArrangement = idArrangementForEmail;
                                invmod.nameFileToSend = nameFileToSend;
                                invmod.report = prRep;

                                invmod.dtCreated = DateTime.Now;
                                invmod.dtModified = DateTime.Now;
                                invmod.userCreated = Login._user.idUser;
                                invmod.userModified = Login._user.idUser;

                                //docbus.Save(docmodel);                                                                
                                documentsToBeSavedToDb.Add(invmod);
                            }
                        }
                    }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void EmailInvoice(List<InvoiceModel> forPrinting, string subject, string body_message, string personemail, int idPerson, int idTraveler)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                PrintReport firstPayment;
                string nameFileToSend = "";                

                if (forPrinting != null && forPrinting.Count > 0)
                {
                    DocumentsBUS docbus = new DocumentsBUS();
                    DocumentsModel docmodel = new DocumentsModel();

                    Outlook.Application outlookApp = new Outlook.Application();

                    Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                    outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);
                    oMailItem.DeleteAfterSubmit = false;

                    oMailItem.Subject = subject;
                    oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                    oMailItem.Body = body_message;

                    Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                    Outlook.Recipient oRecip;
                    
                    oRecip = (Outlook.Recipient)oRecips.Add(personemail);
                    
                    oRecip.Resolve();

                    Outlook.Attachments oAttachs = (Outlook.Attachments)oMailItem.Attachments;
                    Outlook.Attachment oAtt = null;

                    foreach (InvoiceModel mod in forPrinting)
                    {
                        if (mod.select == true)
                        {
                            //
                            // get Label for showing exact version of report
                            //
                            int idLabel = 1;
                            int idArrangement = 0;
                            ArrangementModel am = new ArrangementModel();
                            am = new ArrangementBUS().GetArrangementByArrangementBook(Convert.ToInt32(mod.idVoucher));
                            List<LabelForArrangement> alm = new List<LabelForArrangement>();

                            if (am != null)
                            {
                                alm = new ArrangementBUS().GetLabelsArrangement(am.idArrangement);
                                idArrangementForEmail = am.idArrangement;
                            }
                            else
                                idArrangementForEmail = 0;

                            if (alm != null)
                                if (alm.Count > 0)
                                    idLabel = alm[0].idLabel;
                            // END GET LABEL
                            nameFileToSend = "Factuur-" +  mod.invoiceNr + "-" + mod.invoiceRbr + ".pdf";
                            if (mod.invoiceRbr != "000" && mod.invoiceRbr != "001")
                            {
                                //if (mod.invoiceRbr == "999")
                                //{
                                //    PrintReport pr = PrintFirstPayment(true, false, mod, idArrangementForEmail, idLabel, out nameFileToSend);                                    
                                //    //email
                                //    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                //    string savetopath = tempFolder + "\\" + nameFileToSend;

                                //    if (File.Exists(savetopath) == true)
                                //        File.Delete(savetopath);

                                //    rg.GenerateOutputPDF(pr.report, savetopath);
                                //    oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);                                   
                                //}

                                List<InvoiceReportModel> irep = new List<InvoiceReportModel>();
                                InvoiceReportModel aa = new InvoiceReportModel();
                                List<InvoiceItemsReportModel> iirep = new List<InvoiceItemsReportModel>();
                                InvoiceBUS rptb = new InvoiceBUS();
                                InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
                                DataTable invoiceAll = new DataTable();
                                DataTable invoice = new DataTable();
                                DataTable itemsInv = new DataTable();

                                //if (mod.invoiceRbr != "999")
                                    invoiceAll = rptb.GetReportInvoiceByIntID(mod.idInvoice);
                                //else
                                //    invoiceAll = rptb.GetReportInvoiceFor999ByIntID(mod.idInvoice);


                                if ((invoiceAll != null) && (invoiceAll.Rows.Count > 0))
                                {
                                    //DataRow dr = invoice.Rows[0];
                                    foreach (DataRow dr in invoiceAll.Rows)
                                    {
                                        invoice = new DataTable();
                                        invoice = invoiceAll.Copy();
                                        invoice.Rows.Clear();
                                        invoice.Rows.Add(dr.ItemArray);
                                        if (dr["idContPerson"].ToString() == "")
                                            dr["idContPerson"] = mod.idContPerson;

                                        if (mod.invoiceRbr != "999")
                                        {
                                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(mod.idInvoice, Login._user.lngUser);
                                        }
                                        else
                                        {
                                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(Convert.ToInt32(dr["idInvoice"].ToString()), Login._user.lngUser);
                                            nameFileToSend = "Factuur-" + dr["invoiceNr"].ToString() + "-" + dr["invoiceRbr"].ToString() + ".pdf";
                                        }

                                        if (itemsInv == null)
                                            itemsInv = new DataTable();

                                        //frmInvoiceReport aaa = new frmInvoiceReport(invoice, itemsInv, nameFileToSend, idLabel, true);
                                        //aaa.ShowDialog();
                                                                                
                                        //email
                                        PrintReport pr = new PrintReport(invoice, itemsInv, nameFileToSend, idLabel, false, false, false);
                                        ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                        string savetopath = tempFolder + "\\" + nameFileToSend;

                                        if (File.Exists(savetopath) == true)
                                            File.Delete(savetopath);

                                        rg.GenerateOutputPDF(pr.report, savetopath);
                                        oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                                        
                                        if (mod.invoiceRbr != "999")
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                PrintReport prRep;
                                if (mod.brutoAmount < 0)
                                {
                                    prRep = PrintFirstPayment(true, false, mod, idArrangementForEmail, idLabel, out nameFileToSend);
                                }
                                else
                                {
                                    prRep = PrintFirstPayment(false, false, mod, idArrangementForEmail, idLabel, out nameFileToSend);
                                }
                                
                                //email
                                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                string savetopath = tempFolder + "\\" + nameFileToSend;

                                if (File.Exists(savetopath) == true)
                                    File.Delete(savetopath);

                                rg.GenerateOutputPDF(prRep.report, savetopath);
                                oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                                
                            }
                        }
                    }
                    
                    idPersonForEmail = idTraveler;
                    oMailItem.Send();

                    oRecip = null;
                    oRecips = null;
                    oAtt = null;
                    oAttachs = null;
                    oMailItem = null;
                    outlookApp = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void PrintOrEmail(bool printoremail, string printername, List<InvoiceModel> forPrinting, string subject, string body_message, string personemail, int idPerson)
        {
            // true- print 
            // false -email

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                PrintReport firstPayment;
                string nameFileToSend = "";
   
                if (forPrinting != null && forPrinting.Count > 0)
                {
                    DocumentsBUS docbus = new DocumentsBUS();
                    DocumentsModel docmodel = new DocumentsModel();

                    Outlook.Application outlookApp = new Outlook.Application();

                    Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                    outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);
                    oMailItem.DeleteAfterSubmit = false;

                    oMailItem.Subject = subject;
                    oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                    oMailItem.Body = body_message;

                    Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;                     
                    Outlook.Recipient oRecip;
                    
                    if(printoremail == false)
                        oRecip = (Outlook.Recipient)oRecips.Add(personemail);
                    else
                        oRecip = (Outlook.Recipient)oRecips.Add("noone@seriecom.net");
                        
                    oRecip.Resolve();

                    Outlook.Attachments oAttachs = (Outlook.Attachments)oMailItem.Attachments;
                    Outlook.Attachment oAtt = null;

                    foreach (InvoiceModel mod in forPrinting)
                    {
                        if (mod.select == true)
                        {
                            //
                            // get Label for showing exact version of report
                            //
                            int idLabel = 1;
                            int idArrangement = 0;
                            ArrangementModel am = new ArrangementModel();
                            am = new ArrangementBUS().GetArrangementByArrangementBook(Convert.ToInt32(mod.idVoucher));
                            List<LabelForArrangement> alm = new List<LabelForArrangement>();

                            if (am != null)
                            {
                                alm = new ArrangementBUS().GetLabelsArrangement(am.idArrangement);
                                idArrangementForEmail = am.idArrangement;
                            }
                            else
                                idArrangementForEmail = 0;

                            if (alm != null)
                                if (alm.Count > 0)
                                    idLabel = alm[0].idLabel;
                            // END GET LABEL

                            if (mod.invoiceRbr != "000" && mod.invoiceRbr != "001")
                            {                                
                                if (mod.invoiceRbr == "999")
                                {                                    
                                    PrintReport pr = PrintFirstPayment(true,printoremail, mod, idArrangementForEmail, idLabel, out nameFileToSend);
                                    if (printoremail == true)
                                    {
                                        //print
                                        pr.RunTo(printername);

                                        ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                        string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;

                                        if (File.Exists(savetopath) == true)
                                            File.Delete(savetopath);

                                        rg.GenerateOutputPDF(pr.report, savetopath);

                                        docmodel = new DocumentsModel();
                                        docmodel.idContPers = idPerson;
                                        docmodel.idClient = 0;
                                        docmodel.descriptionDocument = "Printed Invoice PDF";
                                        docmodel.fileDocument = Path.GetFileName(savetopath);
                                        docmodel.typeDocument = "SCANS";
                                        docmodel.idDocumentStatus = 2;
                                        docmodel.idEmployee = 0;
                                        docmodel.idResponsableEmployee = 0;
                                        docmodel.inOutDocument = 2;
                                        docmodel.noteDocument = "";
                                        docmodel.idArrangement = idArrangementForEmail;

                                        //model.id

                                        docmodel.dtCreated = DateTime.Now;
                                        docmodel.dtModified = DateTime.Now;
                                        docmodel.userCreated = Login._user.idUser;
                                        docmodel.userModified = Login._user.idUser;

                                        docbus.Save(docmodel, this.Name, Login._user.idUser);
                                    }
                                    else
                                    {
                                        //email
                                        ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                        string savetopath = tempFolder + "\\" + nameFileToSend;

                                        if (File.Exists(savetopath) == true)
                                            File.Delete(savetopath);

                                        rg.GenerateOutputPDF(pr.report, savetopath);
                                        oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                                    }
                                }

                                List<InvoiceReportModel> irep = new List<InvoiceReportModel>();
                                InvoiceReportModel aa = new InvoiceReportModel();
                                List<InvoiceItemsReportModel> iirep = new List<InvoiceItemsReportModel>();
                                InvoiceBUS rptb = new InvoiceBUS();
                                InvoiceItemsBUS rptiib = new InvoiceItemsBUS();
                                DataTable invoiceAll = new DataTable();
                                DataTable invoice = new DataTable();
                                DataTable itemsInv = new DataTable();

                                if (mod.invoiceRbr != "999")
                                    invoiceAll = rptb.GetReportInvoiceByIntID(mod.idInvoice);
                                else
                                    invoiceAll = rptb.GetReportInvoiceFor999ByIntID(mod.idInvoice);


                                if ((invoiceAll != null) && (invoiceAll.Rows.Count > 0))
                                {
                                    //DataRow dr = invoice.Rows[0];
                                    foreach (DataRow dr in invoiceAll.Rows)
                                    {
                                        invoice = new DataTable();
                                        invoice = invoiceAll.Copy();
                                        invoice.Rows.Clear();
                                        invoice.Rows.Add(dr.ItemArray);
                                        if (dr["idContPerson"].ToString() == "")
                                            dr["idContPerson"] = mod.idContPerson;

                                        if (mod.invoiceRbr != "999")
                                        {                                            
                                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(mod.idInvoice, Login._user.lngUser);
                                        }
                                        else
                                        {
                                            itemsInv = rptiib.GetReportInvoiceItemsByIDAll(Convert.ToInt32(dr["idInvoice"].ToString()), Login._user.lngUser);
                                            nameFileToSend = "Factuur-" + dr["invoiceNr"].ToString() + "-" + dr["invoiceRbr"].ToString() + ".pdf";
                                        }                                        

                                        if (itemsInv == null)
                                            itemsInv = new DataTable();

                                        //frmInvoiceReport aaa = new frmInvoiceReport(invoice, itemsInv, nameFileToSend, idLabel, true);
                                        //aaa.ShowDialog();

                                        if (printoremail == true)
                                        {
                                            //print
                                            PrintReport pr = new PrintReport(invoice, itemsInv, nameFileToSend, idLabel, true, false, false);
                                            pr.RunTo(printername);

                                            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                            string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;

                                            if (File.Exists(savetopath) == true)
                                                File.Delete(savetopath);

                                            rg.GenerateOutputPDF(pr.report, savetopath);

                                            docmodel = new DocumentsModel();
                                            docmodel.idContPers = idPerson;
                                            docmodel.idClient = 0;
                                            docmodel.descriptionDocument = "Printed Invoice PDF";
                                            docmodel.fileDocument = Path.GetFileName(savetopath);
                                            docmodel.typeDocument = "SCANS";
                                            docmodel.idDocumentStatus = 2;
                                            docmodel.idEmployee = 0;
                                            docmodel.idResponsableEmployee = 0;
                                            docmodel.inOutDocument = 2;
                                            docmodel.noteDocument = "";
                                            docmodel.idArrangement = idArrangementForEmail;

                                            //model.id

                                            docmodel.dtCreated = DateTime.Now;
                                            docmodel.dtModified = DateTime.Now;
                                            docmodel.userCreated = Login._user.idUser;
                                            docmodel.userModified = Login._user.idUser;

                                            docbus.Save(docmodel, this.Name, Login._user.idUser);

                                        }
                                        else
                                        {
                                            //email
                                            PrintReport pr = new PrintReport(invoice, itemsInv, nameFileToSend, idLabel, false, false, false);
                                            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                            string savetopath = tempFolder + "\\" + nameFileToSend;

                                            if (File.Exists(savetopath) == true)
                                                File.Delete(savetopath);

                                            rg.GenerateOutputPDF(pr.report, savetopath);
                                            oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                                        }
                                        if (mod.invoiceRbr != "999")
                                            break;
                                    }
                                }                                
                            }
                            else
                            {
                                PrintReport prRep;
                                if(mod.brutoAmount < 0)
                                {
                                    prRep = PrintFirstPayment(true,printoremail, mod, idArrangementForEmail, idLabel, out nameFileToSend);                                    
                                }
                                else
                                {
                                    prRep = PrintFirstPayment(false,printoremail, mod, idArrangementForEmail, idLabel, out nameFileToSend);                                    
                                }

                                if (printoremail == true)
                                {
                                    prRep.RunTo(printername);

                                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                    string savetopath = MainForm.DocumentsFolder + "\\" + nameFileToSend;

                                    if (File.Exists(savetopath) == true)
                                        File.Delete(savetopath);

                                    rg.GenerateOutputPDF(prRep.report, savetopath);

                                    docmodel = new DocumentsModel();
                                    docmodel.idContPers = idPerson;
                                    docmodel.idClient = 0;
                                    docmodel.descriptionDocument = "Printed Invoice PDF";
                                    docmodel.fileDocument = Path.GetFileName(savetopath);
                                    docmodel.typeDocument = "SCANS";
                                    docmodel.idDocumentStatus = 2;
                                    docmodel.idEmployee = 0;
                                    docmodel.idResponsableEmployee = 0;
                                    docmodel.inOutDocument = 2;
                                    docmodel.noteDocument = "";
                                    docmodel.idArrangement = idArrangementForEmail;

                                    //model.id

                                    docmodel.dtCreated = DateTime.Now;
                                    docmodel.dtModified = DateTime.Now;
                                    docmodel.userCreated = Login._user.idUser;
                                    docmodel.userModified = Login._user.idUser;

                                    docbus.Save(docmodel, this.Name, Login._user.idUser);
                                }
                                else
                                {
                                    //email
                                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                                    string savetopath = tempFolder + "\\" + nameFileToSend;

                                    if (File.Exists(savetopath) == true)
                                        File.Delete(savetopath);

                                    rg.GenerateOutputPDF(prRep.report, savetopath);
                                    oAtt = (Outlook.Attachment)oAttachs.Add(savetopath, Outlook.OlAttachmentType.olByValue);
                                }
                            }                                                        
                        }
                    }

                    // false - send by email
                    if(printoremail == false)
                    {
                        idPersonForEmail = idPerson;                        
                        oMailItem.Send();
                        
                    }                    

                    oRecip = null;
                    oRecips = null;
                    oAtt = null;
                    oAttachs = null;
                    oMailItem = null;
                    outlookApp = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateInvoiceStatuses(List<InvoiceModel> forUpdate, int newStatus)
        {            
            translateRadMessageBox tr = new translateRadMessageBox();
            if (tr.translateAllMessageBoxDialog("Change status ?", " ") == System.Windows.Forms.DialogResult.Yes)
            {
                InvoiceBUS inbus = new InvoiceBUS();
                foreach (var m in forUpdate)
                {
                    if (m.idInvoiceStatus == 6)
                    {
                        inbus.UpdateStatus(2, m.idInvoice, this.Name, Login._user.idUser);
                        if (m.invoiceRbr == "001")
                        {
                            InvoiceModel ium = new InvoiceModel();
                            ium = inbus.GetInvoiceByInvoiceAndExtension(m.invoiceNr, "000");
                            if (ium != null)
                            {
                                inbus.UpdateStatus(2, ium.idInvoice, this.Name, Login._user.idUser);
                            }
                        }
                        else
                        {
                            if (m.invoiceRbr == "000")
                            {
                                InvoiceModel ium = new InvoiceModel();
                                ium = inbus.GetInvoiceByInvoiceAndExtension(m.invoiceNr, m.invoiceRbr);
                                if (ium != null)
                                {
                                    inbus.UpdateStatus(2, ium.idInvoice, this.Name, Login._user.idUser);
                                }
                            }
                        }
                    }
                }

                DocumentsBUS sbus = new DocumentsBUS();
                DocumentsModel docmodel;
                foreach(InvoiceSelectionDocumentModel m in documentsToBeSavedToDb)
                {
                    if (m.report != null && m.nameFileToSend != String.Empty)
                    {
                        ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                        string savetopath = MainForm.DocumentsFolder + "\\" + m.nameFileToSend;

                        if (File.Exists(savetopath) == true)
                            File.Delete(savetopath);

                        rg.GenerateOutputPDF(m.report.report, savetopath);

                        docmodel = new DocumentsModel();
                        docmodel.idContPers = m.idContPers;
                        docmodel.idClient = m.idClient;
                        docmodel.descriptionDocument = m.descriptionDocument;
                        docmodel.fileDocument = m.fileDocument;
                        docmodel.typeDocument = m.typeDocument;
                        docmodel.idDocumentStatus = m.idDocumentStatus;
                        docmodel.idEmployee = m.idEmployee;
                        docmodel.idResponsableEmployee = m.idResponsableEmployee;
                        docmodel.inOutDocument = m.inOutDocument;
                        docmodel.noteDocument = m.noteDocument;
                        docmodel.idArrangement = m.idArrangement;

                        docmodel.dtCreated = DateTime.Now;
                        docmodel.dtModified = DateTime.Now;
                        docmodel.userCreated = Login._user.idUser;
                        docmodel.userModified = Login._user.idUser;

                        sbus.Save(docmodel, this.Name, Login._user.idUser);
                    }
                    else if(m.mailitemlocation != String.Empty)
                    {   
                        if(File.Exists(m.mailitemlocation) == true)
                        {
                            File.Copy(m.mailitemlocation, MainForm.DocumentsFolder + "\\" + m.nameFileToSend);

                            docmodel = new DocumentsModel();
                            docmodel.idContPers = m.idContPers;
                            docmodel.idClient = m.idClient;
                            docmodel.descriptionDocument = m.descriptionDocument;
                            docmodel.fileDocument = m.fileDocument;
                            docmodel.typeDocument = m.typeDocument;
                            docmodel.idDocumentStatus = m.idDocumentStatus;
                            docmodel.idEmployee = m.idEmployee;
                            docmodel.idResponsableEmployee = m.idResponsableEmployee;
                            docmodel.inOutDocument = m.inOutDocument;
                            docmodel.noteDocument = m.noteDocument;
                            docmodel.idArrangement = m.idArrangement;

                            docmodel.dtCreated = DateTime.Now;
                            docmodel.dtModified = DateTime.Now;
                            docmodel.userCreated = Login._user.idUser;
                            docmodel.userModified = Login._user.idUser;

                            sbus.Save(docmodel, this.Name, Login._user.idUser);
                        }
                        
                    }
                }
            }                
        }

        void outlookApp_ItemSend(object Item, ref bool Cancel)
        {
            try
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


                    if (idPersonForEmail != 0)
                    {
                        InvoiceSelectionDocumentModel model = new InvoiceSelectionDocumentModel();
                        model.idContPers = idPersonForEmail;
                        model.idClient = 0;
                        model.descriptionDocument = "Emailed Invoice PDF";
                        model.fileDocument = item.EntryID + ".msg";
                        model.typeDocument = "EML";
                        model.idDocumentStatus = 2;
                        model.idEmployee = 0;
                        model.idResponsableEmployee = 0;
                        model.inOutDocument = 2;
                        model.noteDocument = "Sent Email";
                        model.idArrangement = idArrangementForEmail;                        
                        model.nameFileToSend = item.EntryID + ".msg";
                        model.mailitemlocation = locationOnDisk;

                        //sbus.Save(model);
                        documentsToBeSavedToDb.Add(model);

                    }

                    Cancel = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cancel = true;
            }
        }
               
        private void btnPrint_Click(object sender, EventArgs e)
        {
                 
            
        }

        private void radioByEmail_CheckStateChanging(object sender, Telerik.WinControls.UI.CheckStateChangingEventArgs args)
        {
            if (limBind == null || limBind.Count <= 0)
            {
                translateRadMessageBox tmsg = new translateRadMessageBox();
                tmsg.translateAllMessageBox("Nothing to select");
                args.Cancel = true;
            }
        }

        private void radioByPost_CheckStateChanging(object sender, Telerik.WinControls.UI.CheckStateChangingEventArgs args)
        {
            if (limBind == null || limBind.Count <= 0)
            {
                translateRadMessageBox tmsg = new translateRadMessageBox();
                tmsg.translateAllMessageBox("Nothing to select");
                args.Cancel = true;
            }
        }

        private void radioByEmail_CheckStateChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (InvoiceModel m in limBind)
            {
                //PersonEmailBUS bus = new PersonEmailBUS();
                //PersonEmailiSInvoiceModel isInvModel = new PersonEmailiSInvoiceModel();

                //isInvModel = bus.GetPersonEmailsISInoicing((int)m.idContPerson);

                //if(isInvModel != null)
                //{
                   
                //}

                if (m.isInvoicing == true)
                    m.select = true;
                else
                    m.select = false;
            }

            rgvInvoice.DataSource = null;
            rgvInvoice.DataSource = limBind;

            btnSendByEmail.Visible = true;
            btnSendByPost.Visible = false;

            Cursor.Current = Cursors.Default;
        }

        private void radioByPost_CheckStateChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (InvoiceModel m in limBind)
            {                
                if (m.isInvoicing == false)
                    m.select = true;
                else
                    m.select = false;
            }

            rgvInvoice.DataSource = null;
            rgvInvoice.DataSource = limBind;

            btnSendByEmail.Visible = false;
            btnSendByPost.Visible = true;

            Cursor.Current = Cursors.Default;
        }

        private void rgvInvoice_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {
            if (radioByEmail.IsChecked == true)
            {
                if ((bool)e.RowElement.RowInfo.Cells["isInvoicing"].Value == true)
                {
                    if (BookmarkFunctions.IsEmailValid((string)e.RowElement.RowInfo.Cells["email"].Value) == false)
                    {
                        e.RowElement.ForeColor = Color.Red;
                        e.RowElement.RowInfo.Cells["select"].Value = false;
                    }
                    else
                    {
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    }
                }
                else
                {
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                }
            }
            else
            {
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
            }
        }

        private void btnSendByPost_Click(object sender, EventArgs e)
        {
            rgvInvoice.EndEdit();

            PrintDialog printDialog1 = new PrintDialog();
            //printDialog1.Document = printDocument1;    
            printDialog1.AllowSomePages = false;
            printDialog1.AllowSelection = false;
            printDialog1.AllowPrintToFile = false;
            printDialog1.AllowCurrentPage = false;
            //printDialog1.

            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
               StartProcessingDocuments(true, printDialog1.PrinterSettings.PrinterName, "","");
            }
        }

        private void rgvInvoice_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutInvoiceSelectionForBooking))
            {
                rgvInvoice.LoadLayout(layoutInvoiceSelectionForBooking);
            }

            if (this.rgvInvoice.Columns != null)
            {               
                //this.gridOpenLines.Columns["chk"].IsVisible = false;

                foreach (GridViewColumn col in rgvInvoice.Columns)
                {
                    if (col.Name != "select")
                    {
                        col.ReadOnly = true;                                               
                        //col.Width = 100;                       
                    }
                }

                if (rgvInvoice.Columns["select"] != null)
                    rgvInvoice.Columns["select"].IsVisible = true;

                //SortDescriptor descriptor = new SortDescriptor();
                //descriptor.PropertyName = "dtOpenLine";
                //descriptor.Direction = ListSortDirection.Descending;
                //this.gridOpenLinesLetters.MasterTemplate.SortDescriptors.Add(descriptor);

                for (int i = 0; i < rgvInvoice.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvInvoice.Columns[i].HeaderText != null && resxSet.GetString(rgvInvoice.Columns[i].HeaderText) != null)
                            rgvInvoice.Columns[i].HeaderText = resxSet.GetString(rgvInvoice.Columns[i].HeaderText);
                    }
                }
            }
        }

        private void btnSendByEmail_Click(object sender, EventArgs e)
        {
            rgvInvoice.EndEdit();

            if (Login.isOutlookInstalled == false)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
                return;

            }
            List<string> persons = new List<string>();
            foreach (var p in limBind)
            {
                int isInList = persons.IndexOf(p.namePerson);
                if (p.select == true && isInList == -1)
                    persons.Add(p.namePerson);
            }

            if (persons.Count > 0)
            {
                using (frmOpenLinesSendEmailForm frm = new frmOpenLinesSendEmailForm(persons))
                {
                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {                        
                        StartProcessingDocuments(false, "", frm.subject, frm.message);
                    }
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Nothing selected.");
            }
        }

        private void frmInvoiceSelectionForBooking_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var tempFolder in _tempFolders)
            {
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
        }

        private void rgvInvoice_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtInvoice" || e.Column.Name == "dtValuta"
                || e.Column.Name == "dtFirstPay" || e.Column.Name == "dtLastPay"
                 || e.Column.Name == "dtCreated" || e.Column.Name == "dtModified")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void radMenuButtonSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutInvoiceSelectionForBooking))
            {
                File.Delete(layoutInvoiceSelectionForBooking);
            }
            rgvInvoice.SaveLayout(layoutInvoiceSelectionForBooking);

            if (rgvInvoice.Columns["select"] != null)
                rgvInvoice.Columns["select"].IsVisible = true;

            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

    }

    public class InvoiceSelectionDocumentModel : DocumentsModel
    {
        public PrintReport report { get; set; }
        public string nameFileToSend { get; set; }
        public string mailitemlocation { get; set; }

        public InvoiceSelectionDocumentModel()
        {
            this.report = null;
            this.nameFileToSend = String.Empty;
            this.mailitemlocation = String.Empty;
        }
    }
}
