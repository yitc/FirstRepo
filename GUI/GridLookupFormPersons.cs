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
    public partial class GridLookupFormPersons : Telerik.WinControls.UI.RadForm
    {
        string layoutLookup;
        private int idArrangement;
        private int idArrangementBook;
        public List<PersonModel> selMenus1;
        List<PersonModel> modelPerson;

        public GridLookupFormPersons(List<PersonModel> model, List<PersonModel> selectedModel,  string nameForm, int arrId, int arrBook)
        {
            InitializeComponent();
            gridLookup.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridLookup.DataSource = model;
            gridLookup.AllowAutoSizeColumns = true;

            //set list of selected submenus for role
            selMenus1 = new List<PersonModel>();
            selMenus1 = selectedModel;

            modelPerson = new List<PersonModel>();
            modelPerson = model;
            idArrangement = arrId;
            idArrangementBook = arrBook;

            //set name form and icon
            this.Name = nameForm;
            this.Icon = Login.iconForm;

            TranslationBUS tb = new TranslationBUS();
            List<TranslationModel> tm = new List<TranslationModel>();
            tm = tb.CheckIfTranslationExists(Login._user.lngUser, nameForm);
            if (tm != null)
            {
                if (tm.Count > 0)
                {
                    nameForm = tm[0].stringKey;
                }
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_person")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_person"));

            }


            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_person\\" + nameForm.Replace("frm", "") + ".xml");


            //do translate form form text 
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    this.Text = resxSet.GetString(nameForm);
                if (resxSet.GetString(radMenuItemSaveLookupLayout.Text) != null)
                    radMenuItemSaveLookupLayout.Text = resxSet.GetString(radMenuItemSaveLookupLayout.Text);
            }

           


            //add check box, move to firstplace and set column max size
            GridViewCheckBoxColumn chk = new GridViewCheckBoxColumn();
            chk.Name = "isChecked";
            chk.HeaderText = "Add/Not";
            gridLookup.Columns.Add(chk);

            gridLookup.Columns.Move(gridLookup.Columns.Count - 1, 0);
            gridLookup.Columns["isChecked"].MaxWidth = (int)(this.CreateGraphics().MeasureString(gridLookup.Columns["isChecked"].HeaderText, this.Font).Width + 10);
            checkedRows();

            if (File.Exists(layoutLookup))
            {
                gridLookup.LoadLayout(layoutLookup);
            }

       

        }
        private void checkedRows()
        {
            for (int i = 0; i < gridLookup.RowCount; i++)
            {
                if (selMenus1 != null)
                {
                    if (selMenus1.Find(s => s.idContPers == Convert.ToInt32(gridLookup.Rows[i].Cells["idContPers"].Value.ToString())) != null)
                    {

                        gridLookup.Rows[i].Cells["isChecked"].Value = true;
                        

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
                foreach (var column in gridLookup.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    if (column.Name != "isChecked")
                        column.ReadOnly = true;
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

        
        //load layouts
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


        private void btnSavePersons_Click(object sender, EventArgs e)
        {
            gridLookup.EndEdit();
            for (int i = 0; i < gridLookup.Rows.Count; i++)
            {
                //first delete all menus that are on lookup control, and all submenus that are added but without their superior (if there is such)
                if (i == 0)
                {
                    selMenus1 = new List<PersonModel>();
                }
                //add those which are checked
                if (gridLookup.Rows[i].Cells["isChecked"].Value != null)
                {
                    if (Convert.ToBoolean(gridLookup.Rows[i].Cells["isChecked"].Value.ToString()) == true)
                    {

                        PersonModel pm = new PersonModel();
                        int idContPerson = Convert.ToInt32(gridLookup.Rows[i].Cells["idContPers"].Value.ToString());
                        pm = modelPerson.Find(item => item.idContPers == idContPerson);

                        selMenus1.Add(pm);

                    }
                    else
                    {       //=== dodato da anulira idPayInvoice za decekirane
                        ArrangementBookPersonsBUS nab = new ArrangementBookPersonsBUS();
                        int idContPerson1 = Convert.ToInt32(gridLookup.Rows[i].Cells["idContPers"].Value.ToString());
                        nab.UpdatePayInvoicePerson(idArrangement, idContPerson1, 0, this.Name, Login._user.idUser);
                        nab.DeletePersonFromGrid(idArrangementBook, idContPerson1, this.Name, Login._user.idUser);
                    }
                }
            }
            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You save persons successfully!");

            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

    
    }
}
