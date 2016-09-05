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
using System.Linq;

namespace GUI
{
    public partial class GridLookupFormReasonInOut : Telerik.WinControls.UI.RadForm
    {
        public IModel selectedRow;
        string layoutLookup;
        public List<VoluntaryReasonInModel> volList = new List<VoluntaryReasonInModel>();

        //selModel je lista selektovanih, a na formi to je npr volTripList, model je svi podaci na formi to je gmX3g
        public GridLookupFormReasonInOut(List<VoluntaryReasonInModel> selModel, List<VoluntaryReasonInModel> model, string nameForm)
        {
            InitializeComponent();

            //Gleda sta je selektovano i to iz panela prikazuje u grid
            if(selModel!=null)
               
            for (int i = 0; i < selModel.Count;i++)
            {
                VoluntaryReasonInModel mm = new VoluntaryReasonInModel();
                mm = model.SingleOrDefault(s => s.idReasonIn == selModel[i].idReasonIn);
                if (mm != null)
                    mm.select = true;
            }


            gridLookupPreselection.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None; 
            gridLookupPreselection.DataSource = model;
            gridLookupPreselection.AllowAutoSizeColumns = true;

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
                gridLookupPreselection.LoadLayout(layoutLookup);
            }
        }


        // do on double click
        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

        }

        //sets column width
        private void gridLookup_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in gridLookupPreselection.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);

                    //Enable/Desable editing
                    if (column.Name != "select")
                    { 
                        column.ReadOnly = true;

                    }
                    if (column.GetType() == typeof(GridViewDateTimeColumn))
                    {
                        if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                        {
                            column.FormatString = "{0: dd-MM-yyyy}";
                        }
                    }
                    
                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                }
               
            }
            
        }

        private void radMenuItemSaveLookupLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            gridLookupPreselection.SaveLayout(layoutLookup);
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
                if (gridLookupPreselection != null)
                {
                    GridViewRowInfo info = this.gridLookupPreselection.CurrentRow;
                    if (info.Index >= 0)
                    {
                        //set selected row in model so you can use any data you'll need
                        selectedRow = (VoluntaryReasonInModel)info.DataBoundItem;
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

       
        private void gridLookup_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            
            if (this.gridLookupPreselection.ActiveEditor is RadCheckBoxEditor)
            {
                if (gridLookupPreselection.CurrentRow.Cells["idQuest"].Value != null)
                {
                    string id = gridLookupPreselection.CurrentRow.Cells["idQuest"].Value.ToString();
                    bool chechstate = Convert.ToBoolean(gridLookupPreselection.ActiveEditor.Value);
                    
                }
            }
         
        }

        private void gridLookupPreselection_ValueChanged(object sender, EventArgs e)
        {

            if (this.gridLookupPreselection.ActiveEditor is RadCheckBoxEditor)
            {
                gridLookupPreselection.EndEdit();
            }
           
        }

        private void radMenuItemSave_Click(object sender, EventArgs e)
        {
            if(gridLookupPreselection!=null)
                if(gridLookupPreselection.DataSource!=null)
                {
                    List<VoluntaryReasonInModel> nn = new List<VoluntaryReasonInModel>();
                    nn = (List<VoluntaryReasonInModel>)gridLookupPreselection.DataSource;

                    volList = nn.FindAll(s => s.select == true);                   
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                   
                }
        }

    }
}
