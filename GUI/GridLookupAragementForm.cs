using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using Telerik.WinControls.UI;
using System.Resources;
using System.IO;
using BIS.Business;

namespace GUI
{
    public partial class GridLookupAragementForm : Telerik.WinControls.UI.RadForm
    {
        public IModel selectedRow;
        string layoutLookup;
        bool isVolonary;
        ArrangementBookModel modelArragement = new ArrangementBookModel();
        int alredyTraining;

        bool chkIfExpired;

        public GridLookupAragementForm(List<IModel> model, string nameForm, bool isVol,  ArrangementBookModel bookModel)
        {
            InitializeComponent();
            gridLookup.DataSource = model;
            gridLookup.AllowAutoSizeColumns = true;
            isVolonary = isVol;
            modelArragement = bookModel;
        
           
            //set name form and icon
            this.Name = nameForm;
            this.Icon = Login.iconForm;

            TranslationBUS tb = new TranslationBUS();
            List<TranslationModel> tm = new List<TranslationModel>();
            tm = tb.CheckIfTranslationExists(Login._user.lngUser, nameForm);
            if(tm!=null)
            {
                if(tm.Count>0)
                {
                    nameForm = tm[0].stringKey;
                }
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup"));

            }

           

            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup\\" + nameForm.Replace("frm", "")+".xml");


            //do translate form form text 
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm)!=null)
                     this.Text = resxSet.GetString(nameForm);
                if (resxSet.GetString(radMenuItemSaveLookupLayout.Text) != null)
                    radMenuItemSaveLookupLayout.Text = resxSet.GetString(radMenuItemSaveLookupLayout.Text);
            }

            if (File.Exists(layoutLookup))
            {
                gridLookup.LoadLayout(layoutLookup);
            }

        }


        // do on double click
        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridLookup.CurrentRow;
            if (e.RowIndex >= 0)
            {
                //set selected row in model so you can use any data you'll need
                selectedRow = (IModel)info.DataBoundItem;
                this.DialogResult = DialogResult.Yes;
              //  this.Close();
                

                if(info.DataBoundItem!=null)
                {
                    ArrangementModel newArrangement=new ArrangementModel();
                    newArrangement=(ArrangementModel)selectedRow;
                    modelArragement.idArrangement = newArrangement.idArrangement;
                    alredyTraining = new PersonBUS().IsAlreadyTraveling(newArrangement.idArrangement, modelArragement.idContPers);

                    ArrangementModel amodel = new ArrangementModel();
                    ArrangementBookBUS arrBUS = new ArrangementBookBUS();
                    amodel = (ArrangementModel)selectedRow;
                    chkIfExpired = arrBUS.chkMinDatePriceList(amodel.idArrangement);
  
                     if (newArrangement.statusArrangement.ToUpper() == "CXD")
                        {
                            translateRadMessageBox trr = new translateRadMessageBox();
                            trr.translateAllMessageBox("Not possible to book, because status of  trip is cxd!");
                            return;
                        }

                     else  if (chkIfExpired==true)
                     {
                         translateRadMessageBox trr = new translateRadMessageBox();
                         trr.translateAllMessageBox("Not possible to book, releasedate from contract is expired!");
                         return;
                     }

                    else if (isVolonary == true)
                    {
                        if (Convert.ToInt32(new PersonBUS().NrVolontary(newArrangement.idArrangement)[0].ID) > 0)
                        {
                            var newdgl = new frmArrangementBookingPerson_VH(modelArragement, false);
                            newdgl.txtperson.Text = new PersonBUS().GetPerson(modelArragement.idContPers).fullname;
                            newdgl.ShowDialog();
                            this.Close();
                        }
                        
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Arragement is full for volontary helper!");
                            this.DialogResult = DialogResult.No;
                            this.Close();
                        }
                    }
                    else
                    {
                        bool bContinue = true;
                        if (Convert.ToInt32(new PersonBUS().NrTraveler(newArrangement.idArrangement)[0].ID) > 0)
                        {
                            ArrangementBookBUS arrBookBUS = new ArrangementBookBUS();
                            int xrollator = newArrangement.nrMaximumWheelchairs;
                            int xrolstool = newArrangement.whoseElectricWheelchairs;  // obrnuo 1 2                                     
                            int xarmafa = newArrangement.buSupportingArms;
                            int xnrAnchorage = newArrangement.nrAnchorage;

                            //Rollator
                            int rfld2 = arrBookBUS.GetBookPersMedic(new List<int> { 446, 447, 448 }, newArrangement.idArrangement);  // ovde umesto rfld1 -> rfld2

                            //Rolstoel
                            int rfld1 = arrBookBUS.GetBookPersMedic(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, newArrangement.idArrangement);

                            //Arm sometimes
                            int rfld4 = arrBookBUS.GetBookPersMedic(new List<int> { 439, 440 }, newArrangement.idArrangement);

                            //Anchorage
                            int fldAnchorage = arrBookBUS.GetBookPersMedicMoreAns(new List<int> { 823 }, newArrangement.idArrangement);

                            int rnrAnchorage = (fldAnchorage);


                            ArrangementBookBUS arbus = new ArrangementBookBUS();
                            int idcpr = arbus.GetBookPersMedicPers(new List<int> { 446, 447, 448 }, modelArragement.idContPers);
                            if (idcpr == modelArragement.idContPers)
                            {

                                if (xrollator == 0 && rfld2 != 0)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("There is no possibillity for Rollator");
                                    //this.DialogResult = DialogResult.No;
                                    //this.Close();

                                    bContinue = false;
                                }
                                else
                                {
                                    if (xrollator < rfld2 + 1)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("Exceeded number of Rollator");
                                        //this.DialogResult = DialogResult.No;
                                        //this.Close();
                                        bContinue = false;
                                    }
                                }
                            }
                            //Rolstoel

                            int idcpr1 = arbus.GetBookPersMedicPers(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, modelArragement.idContPers);
                            int xrolstoolNew = 0;
                            if (idcpr1 == modelArragement.idContPers)
                            {
                                xrolstoolNew = rfld1 + 1;
                            }

                            if (xrolstool == 0 && xrolstoolNew != 0)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("There is no possibillity for wheelchair");
                                //this.DialogResult = DialogResult.No;
                                //this.Close();
                                bContinue = false;
                                
                            }
                            else if (xrolstool < xrolstoolNew)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Exceeded number of wheelchair");
                                //this.DialogResult = DialogResult.No;
                                this.Close();
                                bContinue = false;
                            }

                            //Arm sometimes

                            int idcpr3 = arbus.GetBookPersMedicPers(new List<int> { 439, 440 }, modelArragement.idContPers);
                            if (idcpr3 == modelArragement.idContPers)
                            {
                                if (xarmafa == 0 && rfld4 != 0)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("There is no possibillity for arm sometimes");
                                    //this.DialogResult = DialogResult.No;
                                    //this.Close();
                                    bContinue = false;
                                }
                                else
                                    if (xarmafa < rfld4 + 1)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("Exceeded number of  arm sometimes");
                                        //this.DialogResult = DialogResult.No;
                                        //this.Close();
                                        bContinue = false;
                                    }
                            }
                            
                            //Anchorage

                            int idcpra = arbus.GetBookPersMedicPers(new List<int> { 823 }, modelArragement.idContPers);

                            int xAnchorageNew = 0;
                            if (idcpra == modelArragement.idContPers)
                            {
                                xAnchorageNew = rnrAnchorage + 1;
                            }
                            if (xnrAnchorage == 0 && xAnchorageNew != 0)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("There is no possibillity for anchorage");
                                //this.DialogResult = DialogResult.No;
                                //this.Close();
                                bContinue = false;
                            }
                            else if (xnrAnchorage < xAnchorageNew)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Exceeded number of anchorage");
                                //this.DialogResult = DialogResult.No;
                                //this.Close();
                                bContinue = false;
                            }

                            if (bContinue == true)
                            {
                                var newdgl = new frmArrangementBookingPerson(modelArragement, newArrangement.nrMaximumWheelchairs, newArrangement.whoseElectricWheelchairs, newArrangement.buSupportingArms, newArrangement.nrAnchorage, false, false);
                                newdgl.txtperson.Text = new PersonBUS().GetPerson(modelArragement.idContPers).fullname;
                                newdgl.ShowDialog();
                            }

                            this.Close();

                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Arragement is full for traveler!");
                        }
                    
                    }                                            
                }            
            }
        }

        //sets column width
        private void gridLookup_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                    // ovo je namerno skinuto da mogu da se razvlace kolone pa da se sacuva layout


                    //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                    //column.Width = column.MaxWidth;
                    //column.MinWidth = column.MaxWidth;
                    //do translate form form text and grid columns
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    if (column.GetType() == typeof(GridViewDateTimeColumn))
                    {
                        if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                        {
                            column.FormatString = "{0: dd-MM-yyyy}";
                        }
                    }
                   
                }
            }
        }

        private void radMenuItemSaveLookupLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            gridLookup.SaveLayout(layoutLookup);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }
        private void gridLookup_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)  // namesteno da se i sa ENTER bira slog
            {
                GridViewRowInfo info = this.gridLookup.CurrentRow;
                if (info.Index >= 0)
                {
                    //set selected row in model so you can use any data you'll need
                    selectedRow = (IModel)info.DataBoundItem;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
    
    }
}
