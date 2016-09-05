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
    public partial class GridLookupThemeTrip : Telerik.WinControls.UI.RadForm
    {
        public IModel selectedRow;
        string layoutLookup;
       public bool isEdit=false;
      

        public GridLookupThemeTrip(List<IModel> model, string nameForm)
        {
            InitializeComponent();
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

                if (info.DataBoundItem != null)
                {
                    ThemeTripModel newArrangement = new ThemeTripModel();
                    newArrangement = (ThemeTripModel)selectedRow;
              

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
          
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 128);
                    column.Width = column.MaxWidth;
                    column.MinWidth = column.MaxWidth;
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
