using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.Model;
using System.IO;
using System.Resources;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmAccCreditorApprove : Telerik.WinControls.UI.RadForm
    {
        List<AccCreditPayModel> model;
        private string layoutApprove;
        private AccCreditPayModel entereb;
        private int iDemp = 0;
        private int iD = 0;
        private int idArrange;
        List<AccLineModel> lines;
        

        public frmAccCreditorApprove()
        {
            InitializeComponent();
        }

        private void frmAccCreditorApprove_Load(object sender, EventArgs e)
        {
            clearlabel();
            
            layoutApprove = MainForm.gridFiltersFolder + "\\layoutApprove.xml";

            iDemp = Login._user.idEmployee;
            EmployeeBUS eb = new EmployeeBUS();
            EmployeeModel em = new EmployeeModel();
            em = eb.GetEmployee(iDemp, Login._user.lngUser);
            if (em != null)
                labelEmployee.Text = em.firstNameEmployee + "  " + em.lastNameEmployee;

            AccCreditPayBUS acb = new AccCreditPayBUS();
            model = new List<AccCreditPayModel>();

            model = acb.GetInvoicesByTaskEmployee(iDemp);
            gridInvoice.DataSource = null;
            gridInvoice.DataSource = model;



        }
        #region gridev
        private void gridInvoice_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridInvoice.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridInvoice.Columns[i].HeaderText != null && resxSet.GetString(gridInvoice.Columns[i].HeaderText) != null)
                        gridInvoice.Columns[i].HeaderText = resxSet.GetString(gridInvoice.Columns[i].HeaderText);
                }
            }
            if (gridInvoice.ColumnCount > 0)
            {

                gridInvoice.Columns["idCreditPay"].IsVisible = false;
                gridInvoice.Columns["idClient"].IsVisible = false;
                gridInvoice.Columns["idContPers"].IsVisible = false;
                gridInvoice.Columns["isApproved"].IsVisible = false;
                gridInvoice.Columns["isBooked"].IsVisible = false;
                gridInvoice.Columns["isSent"].IsVisible = false;
                gridInvoice.Columns["dtSent"].IsVisible = false;
                gridInvoice.Columns["namefile"].IsVisible = false;
                gridInvoice.Columns["approvedUser"].IsVisible = false;
                gridInvoice.Columns["createUser"].IsVisible = false;
                gridInvoice.Columns["dtCreation"].IsVisible = false;
                gridInvoice.Columns["payIban"].IsVisible = false;

                gridInvoice.Columns["isSelected"].IsVisible = true;
                gridInvoice.Columns["isSelected"].ReadOnly = false;

                gridInvoice.Columns["dtItem"].ReadOnly = true;
                gridInvoice.Columns["dtValuta"].ReadOnly = true;
                gridInvoice.Columns["accNumber"].ReadOnly = true;
                gridInvoice.Columns["account"].ReadOnly = true;
                gridInvoice.Columns["invoiceNr"].ReadOnly = true;
                gridInvoice.Columns["inkopNr"].ReadOnly = true;
                gridInvoice.Columns["iban"].ReadOnly = true;
                gridInvoice.Columns["descItem"].ReadOnly = true;
                gridInvoice.Columns["amountC"].ReadOnly = true;
                gridInvoice.Columns["amountD"].ReadOnly = true;
                gridInvoice.Columns["idBtw"].ReadOnly = true;
                gridInvoice.Columns["currency"].ReadOnly = true;
                gridInvoice.Columns["amountInCurr"].ReadOnly = true;
                gridInvoice.Columns["cost"].ReadOnly = true;
                gridInvoice.Columns["project"].ReadOnly = true;
            }
            if (File.Exists(layoutApprove))
            {
                gridInvoice.LoadLayout(layoutApprove);
            }
            if (gridInvoice.Columns != null && gridInvoice.Columns.Count > 0)
                gridInvoice.Columns["dtItem"].FormatString = "{0: dd/MM/yyyy}";
            if (gridInvoice.Columns != null && gridInvoice.Columns.Count > 0)
                gridInvoice.Columns["dtValuta"].FormatString = "{0: dd/MM/yyyy}";

         
        }

        private void gridInvoice_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayout;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutApprove))
            {
                File.Delete(layoutApprove);
            }
            gridInvoice.SaveLayout(layoutApprove);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void gridInvoice_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (gridInvoice.CurrentRow != null)
            {
                GridViewRowInfo info = this.gridInvoice.CurrentRow;
                AccCreditPayModel selectedRow = (AccCreditPayModel)info.DataBoundItem;
                entereb = new AccCreditPayModel();
               

                if (selectedRow != null)
                {
                    clearlabel();

                    iD = selectedRow.idCreditPay;
                    entereb = selectedRow;
                    getnames();
                    //iD = 0;
                    //iD = selectedRow.idCreditPay;
                    //fillform2(entereb);

                    //lines = new AccCreditLineBUS().GetLine(iD);
                    //gridItems.DataSource = null;
                    //gridItems.DataSource = lines;
                    //gridItems.Show();
                }
            }
        }
        private void gridInvoice_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            string aname = gridInvoice.CurrentCell.ColumnInfo.Name;

            if (aname == "isSelected")
            {

                lines = new AccCreditLineBUS().GetLine(iD);
                if (lines == null)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Can't approve, not ready for booking !!") != null)
                            RadMessageBox.Show(resxSet.GetString("Can't approve, not ready for booking !!"));
                        else
                            RadMessageBox.Show("Can't approve, not ready for booking !!");
                    }
                    e.Cancel = true;
                }
            }

        }

        #endregion gridev


        # region buttons
        private void btnApprove_Click(object sender, EventArgs e)
        {
            AccCreditPayBUS ab = new AccCreditPayBUS();
            bool isOk = false;
            if (model != null)
            {
                for (int y = 0; y < model.Count; y++)
                {
                    if (model[y].isSelected == true)
                    {
                        isOk = ab.UpdateApproved(model[y], 1, Login._user.idUser, this.Name, Login._user.idUser);
                        if (isOk == false)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Error writing line!") != null)
                                    RadMessageBox.Show(resxSet.GetString("Error writing line!"));
                                else
                                    RadMessageBox.Show("Error writing line!");
                            }
                        }
                        else
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Updated") != null)
                                    RadMessageBox.Show(resxSet.GetString("Updated"));
                                else
                                    RadMessageBox.Show("Updated");
                            }
                        }
                    }
                }
            }
            AccCreditPayBUS acb = new AccCreditPayBUS();
            model = new List<AccCreditPayModel>();

            model = acb.GetInvoicesByTaskEmployee(iDemp);
            if (model == null)
                clearlabel();
            gridInvoice.DataSource = null;
            gridInvoice.DataSource = model;
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            int idta = -1;
            string what = "new";
            string forwho = "client";
            int cli = 0;
            if (entereb != null)
            {
                if (entereb.idClient != 0)
                    cli = Convert.ToInt32(entereb.idClient);
                if (entereb.idTask != 0)
                {
                    what = "old";
                    idta = entereb.idTask;
                }
                frmTasks fts = new frmTasks(idta, 0, what, forwho, cli, 0);
                fts.ShowDialog();
            }
          //  idTsk = fts.idTsk;
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            //if (gridInvoice.CurrentRow != null)
            //{
            //    GridViewRowInfo info = this.gridInvoice.CurrentRow;
            //    AccCreditPayModel selectedRow = (AccCreditPayModel)info.DataBoundItem;
            //    if (enterb == null)
            //        enterb = new AccCreditPayModel();
            //    enterb = selectedRow;
            if (entereb != null)
            {
                if (entereb.idDocument == null && entereb.idDocument == 0)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No document to view ...") != null)
                            RadMessageBox.Show(resxSet.GetString("No document to view ..."));
                        else
                            RadMessageBox.Show("No document to view ...");
                    }
                }
                else
                {
                    DocumentsBUS dbs = new DocumentsBUS();
                    DocumentsModel dbm = new DocumentsModel();
                    dbm = dbs.GetDocument(entereb.idDocument);
                    if (dbm == null)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("No document to view ...") != null)
                                RadMessageBox.Show(resxSet.GetString("No document to view ..."));
                            else
                                RadMessageBox.Show("No document to view ...");
                        }
                    }
                    else
                    {
                        string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
                        sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Documents\\";
                        string fullname = sDest + dbm.fileDocument;
                        if (System.IO.File.Exists(fullname))
                            OpenDocument(sDest, dbm.fileDocument);
                        else
                            RadMessageBox.Show("Error opening document", "Open error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            
        }
        private void OpenDocument(string sDest, string sFileName)
        {
            string sExtention = sFileName.Split('.')[sFileName.Split('.').Length - 1];
            string sFullName = sDest + sFileName;
            System.Diagnostics.Process.Start(sFullName);
        }

        private void btnViewContract_Click(object sender, EventArgs e)
        {
            if (entereb != null)
            {
                frmClientPayment fcp = new frmClientPayment(entereb.accNumber, entereb.project, idArrange);
                fcp.Show();
            }
            else
            {
                RadMessageBox.Show("Nothing to show ...", "Open error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion buttons


        private void getnames()
        {
            if (entereb.idTask != 0)
                btnTask.Enabled = true;
            else
                btnTask.Enabled = false;
            if (entereb.idDocument != 0)
                btnDocument.Enabled = true;
            else
                btnDocument.Enabled = false;


            labelInvoice.Text = entereb.invoiceNr;
            labelIban.Text = entereb.iban;
            labelDescription.Text = entereb.descItem;
            if (entereb.amountC != 0)
                labelAmount.Text = entereb.amountC.ToString();
            else
                labelAmount.Text = entereb.amountD.ToString();

            if (entereb.accNumber != "") 
            {
                AccDebCreBUS debpers = new AccDebCreBUS();
                AccDebCreModel pm1X = new AccDebCreModel();
                string fn = "";
                string mn = "";
                string ln = "";
                pm1X = debpers.GetCreditorName(entereb.accNumber);
                if (pm1X != null)
                {
                    if (pm1X.idClient != 0)
                    {
                        ClientBUS cb = new ClientBUS();
                        ClientModel cm = new ClientModel();
                        cm = cb.GetClient(pm1X.idClient);
                        if (cm != null)
                        {
                            labelClient.Text = entereb.accNumber + "  " + cm.nameClient;
                           // entereb.idClient = cm.idClient;
                            if (pm1X.payCondition != null && pm1X.payCondition != 0)
                            {
                                AccPaymentBUS pmb = new AccPaymentBUS();
                                AccPaymentModel pmm = new AccPaymentModel();
                                pmm = pmb.GetPaymentByID(pm1X.payCondition);
                                if (pmm != null)
                                {
                                  //  txtPaydays2.Text = pmm.numberDays.ToString();
                                    labelPayment.Text = pmm.description + "  " + pmm.numberDays.ToString(); 
                                }
                            }
                        }

                    }
                    else
                    {
                        if (pm1X.idContPerson != 0)
                        {
                            PersonBUS pb = new PersonBUS();
                            PersonModel pm = new PersonModel();
                            pm = pb.GetPerson(pm1X.idContPerson);
                            if (pm != null)
                            {
                                if (pm.firstname == null)
                                    fn = "";
                                else
                                    fn = pm.firstname;
                                if (pm.midname == null)
                                    mn = "";
                                else
                                    mn = pm.midname;
                                if (pm.lastname == null)
                                    ln = "";
                                else
                                    ln = pm.lastname;

                                labelClient.Text = fn + " " + mn + " " + ln;

                            }
                            else
                            {
                                labelClient.Text = "";
                            }

                        }
                    }
                }
                else
                {
                    labelClient.Text = "";
                }



                //}
            }
            //if (txtBtw2.Text != "")
            //{
            //    AccTaxBUS tb = new AccTaxBUS();
            //    AccTaxModel tm = new AccTaxModel();
            //    tm = tb.GetTaxByID(Convert.ToInt32(txtBtw2.Text));
            //    if (tm != null)
            //        labelBtw2.Text = tm.descTax;
            //    else
            //        labelBtw2.Text = "";
            //}
            //else
            //{
            //    labelBtw2.Text = "";
            //}

            //if (txtAccount2.Text != "")
            //{
            //    LedgerAccountBUS lb = new LedgerAccountBUS();
            //    LedgerAccountModel lm = new LedgerAccountModel();
            //    lm = lb.GetAccount(txtAccount2.Text);
            //    if (lm != null)
            //        labelAccount2.Text = lm.descLedgerAccount;
            //    else
            //        labelAccount2.Text = "";
            //}
            //else
            //{
            //    labelAccount2.Text = "";
            //}

            if (entereb.cost != "")
            {
                AccCostBUS ab = new AccCostBUS();
                AccCostModel am = new AccCostModel();
                am = ab.GetCostByID(entereb.cost);  
                if (am != null)
                    labelCost.Text = entereb.cost+" "+am.descCost;
                else
                    labelCost.Text = "";
            }
            else
            {
                labelCost.Text = "";
            }
            if (entereb.project != "")
            {
                ArrangementBUS arb = new ArrangementBUS();
                ArrangementModel arm = new ArrangementModel();
                arm = arb.GetArrangementByCode(entereb.project);
                if (arm != null)
                {
                    labelProject.Text = entereb.project + "  " + arm.nameArrangement;
                    idArrange = arm.idArrangement;
                }
                else
                    labelProject.Text = "";
            }
            else
            {
                labelProject.Text = "";
            }

        }
        private void controla()
        {
           
        }
       
        private void gridInvoice_ValueChanged(object sender, EventArgs e)
        {

        }
        private void clearlabel()
        {
            labelEmployee.Text = "";
            labelClient.Text = "";
            labelInvoice.Text = "";
            labelIban.Text = "";
            labelDescription.Text = "";
            labelAmount.Text = "";
            labelProject.Text = "";
            labelCost.Text = "";
            labelPayment.Text = "";
        }

     private void setTranslate()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);

                if (resxSet.GetString(lblInvoice.Text) != null)
                    lblInvoice.Text = resxSet.GetString(lblInvoice.Text);

                if (resxSet.GetString(lblIban.Text) != null)
                    lblIban.Text = resxSet.GetString(lblIban.Text);

                if (resxSet.GetString(lblAmount.Text) != null)
                    lblAmount.Text = resxSet.GetString(lblAmount.Text);

                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);

                if (resxSet.GetString(lblProject.Text) != null)
                    lblProject.Text = resxSet.GetString(lblProject.Text);

                if (resxSet.GetString(lblCost.Text) != null)
                    lblCost.Text = resxSet.GetString(lblCost.Text);

                if (resxSet.GetString(lblPayment.Text) != null)
                    lblPayment.Text = resxSet.GetString(lblPayment.Text);

                if (resxSet.GetString(btnApprove.Text) != null)
                    btnApprove.Text = resxSet.GetString(btnApprove.Text);

                if (resxSet.GetString(btnTask.Text) != null)
                    btnTask.Text = resxSet.GetString(btnTask.Text);

                if (resxSet.GetString(btnDocument.Text) != null)
                    btnDocument.Text = resxSet.GetString(btnDocument.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
            }
        }

 

  




    }
}
