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
    public partial class GridLookupFormRooms : Telerik.WinControls.UI.RadForm
    {
        string layoutLookup;        

        BindingList<ArrangementArticalModel_Rooms> modelArticles;
        ArrangementBookBUS abm;
        int idArr = 0;
        int idArrBook = 0;

        DateTime xdtTo;
        DateTime xdtFrom;

        List<ArrangementRoomsModel> ar;
        List<string> listIdArticles;
        public GridLookupFormRooms(List<ArrangementArticalModel_Rooms> model, ArrangementArticalModel_Rooms selectedModel, int idArrangement, string nameForm)
        {
            InitializeComponent();

            if(model != null)
                modelArticles = new BindingList<ArrangementArticalModel_Rooms>(model);
            else
                modelArticles = new BindingList<ArrangementArticalModel_Rooms>();

            gridLookup.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;            
            gridLookup.DataSource = modelArticles;
            
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

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\rooms")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\rooms"));

            }


            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\rooms\\" + nameForm.Replace("frm", "") + ".xml");


            //do translate form form text 
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    this.Text = resxSet.GetString(nameForm);
                if (resxSet.GetString(radMenuItemSaveLookupLayout.Text) != null)
                    radMenuItemSaveLookupLayout.Text = resxSet.GetString(radMenuItemSaveLookupLayout.Text);
                if (resxSet.GetString(btnSaveArticles.Text) != null)
                    btnSaveArticles.Text = resxSet.GetString(btnSaveArticles.Text);
            }



            //add check box, move to firstplace and set column max size
            //GridViewCheckBoxColumn chk = new GridViewCheckBoxColumn();
            //chk.Name = "isChecked";
            //chk.HeaderText = "Add/Not";
            //gridLookup.Columns.Add(chk);

            //gridLookup.Columns.Move(gridLookup.Columns.Count - 1, 0);
            //gridLookup.Columns["isChecked"].MaxWidth = (int)(this.CreateGraphics().MeasureString(gridLookup.Columns["isChecked"].HeaderText, this.Font).Width + 10);
            
            ArrangementModel am = new ArrangementModel();
            am = new ArrangementBUS().GetArrangementById(idArr);

            xdtFrom = am.dtFromArrangement;
            xdtTo = am.dtToArrangement;            

            if (File.Exists(layoutLookup))
            {
                gridLookup.LoadLayout(layoutLookup);
            }

            List<ArrangementRoomsModel> arrRoomL = new ArrangementRoomsBUS().GetAllRoomsForArrangement(idArr);
            listIdArticles = new List<string>();
            if(arrRoomL != null)
            {
                foreach(ArrangementRoomsModel m in arrRoomL)
                {
                    if(!listIdArticles.Contains(m.idArticle))
                    {
                        listIdArticles.Add(m.idArticle);
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
                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
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

            ArrangementRoomsBUS arb = new ArrangementRoomsBUS();
            ar = new List<ArrangementRoomsModel>();

            if (modelArticles != null)
            {
                bool saveReult = arb.DeleteInsertArrangamentRooms(modelArticles, idArr, this.Name, Login._user.idUser);
                if (saveReult == true)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You save articles successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                }
            }                                
        }
       
        private void gridLookup_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (this.gridLookup.ActiveEditor is RadCheckBoxEditor)
            {
                if (gridLookup.CurrentRow.Cells["idArticle"].Value != null)
                {
                    string id = gridLookup.CurrentRow.Cells["idArticle"].Value.ToString();
                    bool chechstate = Convert.ToBoolean(gridLookup.ActiveEditor.Value);

                    abm = new ArrangementBookBUS();

                    if (chechstate == false)
                    {
                        ArrangementRoomsBUS arb = new ArrangementRoomsBUS();
                        if (arb.checkIfRoomAlready(idArr, id) > 0)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You already added this article room number!");
                            e.Cancel = true;
                        }                        
                    }
                }
            }
        }

        private void gridLookup_ValueChanged(object sender, EventArgs e)
        {
            
            if (this.gridLookup.ActiveEditor is RadCheckBoxEditor)
            {
                gridLookup.EndEdit();
            }
        }

        private void gridLookup_ViewRowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo.Cells["idArticle"].Value != null)
            {
                string stringID = e.RowElement.RowInfo.Cells["idArticle"].Value.ToString();
                if (listIdArticles.Contains(stringID))
                {
                    if (e.RowElement.ForeColor != Color.Blue)
                    {
                        e.RowElement.ForeColor = Color.Blue;
                    }
                }
            }
        }

        private void gridLookup_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {           
        }

    }
}
