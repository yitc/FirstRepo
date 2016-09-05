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
    public partial class GridLookupFormExtraArticles : Telerik.WinControls.UI.RadForm
    {
        string layoutLookup;
        public static List<ArrangementArticalModel> selMenus1;
        List<ArrangementArticalModel> modelArticles;
        ArrangementBookBUS abm;
        int idArr = 0;
        int idArrBook = 0;

        public GridLookupFormExtraArticles(List<ArrangementArticalModel> model, List<ArrangementArticalModel> selectedModel,int idArrangement, string nameForm)
        {
            InitializeComponent();
            gridLookup.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            gridLookup.DataSource = model;
            gridLookup.AllowAutoSizeColumns = true;

            //set list of selected submenus for role
            selMenus1 = new List<ArrangementArticalModel>();
            selMenus1 = selectedModel;

            modelArticles = new List<ArrangementArticalModel>();
            modelArticles = model;

            idArr = idArrangement;


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

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_extraarticles")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\extraarticles"));

            }


            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\extraarticles\\" + nameForm.Replace("frm", "") + ".xml");


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
                    if (selMenus1.Find(s => s.idArticle == gridLookup.Rows[i].Cells["idArticle"].Value.ToString() && s.isContract == Convert.ToBoolean(gridLookup.Rows[i].Cells["isContract"].Value.ToString()) && s.id == Convert.ToInt32(gridLookup.Rows[i].Cells["id"].Value.ToString())) != null)
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


        private void btnSaveArticles_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gridLookup.Rows.Count; i++)
            {
                //first delete all menus that are on lookup control, and all submenus that are added but without their superior (if there is such)
                if (i == 0)
                {
                    selMenus1 = new List<ArrangementArticalModel>();
                }
                //add those which are checked
                if (gridLookup.Rows[i].Cells["isChecked"].Value != null)
                {
                    if (Convert.ToBoolean(gridLookup.Rows[i].Cells["isChecked"].Value.ToString()) == true)
                    {

                        ArrangementArticalModel pm = new ArrangementArticalModel();
                        string codeArticles = gridLookup.Rows[i].Cells["idArticle"].Value.ToString();
                        pm = modelArticles.Find(item => item.idArticle == codeArticles && item.isContract == Convert.ToBoolean(gridLookup.Rows[i].Cells["isContract"].Value.ToString()) && item.id == Convert.ToInt32(gridLookup.Rows[i].Cells["id"].Value.ToString()));
                        pm.number = 1;
                        selMenus1.Add(pm);

                    }
                }
            }
            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You save articles successfully!");

            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

        private void gridLookup_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (this.gridLookup.ActiveEditor is RadCheckBoxEditor)
            {
                int id = (int) gridLookup.CurrentRow.Cells["id"].Value;
                Boolean isContract = (Boolean) gridLookup.CurrentRow.Cells["isContract"].Value;
                string codeArticles = gridLookup.CurrentRow.Cells["idArticle"].Value.ToString();
                bool chechstate = Convert.ToBoolean(gridLookup.ActiveEditor.Value);

                abm = new ArrangementBookBUS();

                if(chechstate==false && abm.GetArticlesNumber(idArr,isContract,id,codeArticles)<=0)
                {

                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You can't add this article because there is no more of this article available from this arrangement!");
                    e.Cancel = true;
                }


            }
        }


    
    }
}
