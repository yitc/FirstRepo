﻿using System;
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
    public partial class GridLookupForm : Telerik.WinControls.UI.RadForm
    {
        public IModel selectedRow;
        string layoutLookup;

        public GridLookupForm(List<IModel> model, string nameForm)
        {
            InitializeComponent();
            if (nameForm == "Travel papers")
            {
                this.Text = nameForm;
                this.Name = nameForm;
            }
            gridLookup.DataSource = model;
            gridLookup.AllowAutoSizeColumns = true;

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
                this.Close();
                
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
                    if (this.Name == "Travel papers")
                    {
                        column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 350);
                        column.Width = column.MaxWidth;
                        column.MinWidth = column.MaxWidth;

                    }
                     if( column.GetType()==typeof(GridViewDateTimeColumn))
                    {
                        if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                         {
                         column.FormatString = "{0: dd-MM-yyyy}";
                         }
                    }

                }

              
                    //if (gridLookup.RowCount > 0)
                    //{
                    //    if (this.gridLookup.Columns["dtFromArrangement"] != null)
                    //    {
                    //        this.gridLookup.Columns["dtFromArrangement"].FormatString = "{0: dd-MM-yyyy}";
                    //    }
                    //    if (this.gridLookup.Columns["dtToArrangement"] != null)
                    //    {
                    //        this.gridLookup.Columns["dtToArrangement"].FormatString = "{0: dd-MM-yyyy}";
                    //    }
                    //}
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
                if (gridLookup != null)
                {
                    GridViewRowInfo info = this.gridLookup.CurrentRow;
                    if (info != null)
                        if (info.Index >= 0)
                        {
                            //set selected row in model so you can use any data you'll need
                            selectedRow = (IModel)info.DataBoundItem;
                            this.DialogResult = DialogResult.Yes;
                            this.Close();

                        }
                }
            }
              if (e.KeyData == Keys.Escape)
              {
                  this.Close();
              }
        }

        private void GridLookupForm_Load(object sender, EventArgs e)
        {

        }

      
    
    }
}
