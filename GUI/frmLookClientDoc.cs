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
using System.Resources;
using System.IO;
using Telerik.WinControls.UI;
using System.Linq;

namespace GUI
{
    public partial class frmLookClientDoc : Telerik.WinControls.UI.RadForm
    {
        private int idClient;
        public int idDoc {get ; set;}
        private List<DocumentsModel> model;
        private DocumentsModel enterd;
        private string layoutDoc;
        private int  docId;
        private string layoutContract;


        public frmLookClientDoc(int xClient, int idDoc)
        {
          
            idClient = xClient;
            if (idDoc != null)
                docId = idDoc;
            else
                docId = 0;
            InitializeComponent();
        }

        private void frmLookClientDoc_Load(object sender, EventArgs e)
        {

            this.Icon = Login.iconForm;
            string name = "Client documents";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;


            layoutDoc = MainForm.gridFiltersFolder + "\\layoutDoc.xml";

            ClientBUS cbus = new ClientBUS();
            ClientModel cmod = new ClientModel();
            cmod = cbus.GetClient(idClient);
            if (cmod == null)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Wrong Client Id !!") != null)
                        RadMessageBox.Show(resxSet.GetString("Wrong Client Id !!"));
                    else
                        RadMessageBox.Show("Wrong Client Id !!");
                }
                return;
            }
            txtClient.Text = cmod.nameClient;
            DocumentsBUS dbus = new DocumentsBUS();
            model = new List<DocumentsModel>();
            model = dbus.GetClientDoc(idClient, Login._user.lngUser);
            gridDocuments.DataSource = null;
         //   gridDocuments.DataSource = model;
            if (model != null && model.Count > 0)
            {
              foreach (DocumentsModel dm in model)
              {
                  if (dm.idDocument == docId)
                      dm.selected = true;
              }
                
                //List<DocumentsModel> listAll = new List<DocumentsModel>();
                //listAll = (List<DocumentsModel>)gridDocuments.DataSource;
                //if (docId != null && docId != 0)
                //{
                // model.Where(i => i.idDocument == docId).Select(j => j.selected = true);
                //// if (val == true)
                ////     model[j].selected = true;
                //}
            }
            gridDocuments.DataSource = model;


        }

        private void btnGet_Click(object sender, EventArgs e)   
        {
            if (gridDocuments.CurrentRow != null)
            {
                GridViewRowInfo info = this.gridDocuments.CurrentRow;
                DocumentsModel selectedRow = (DocumentsModel)info.DataBoundItem;
                enterd = new DocumentsModel();
                enterd = selectedRow;
                if (selectedRow != null )
                {
                    if (selectedRow.selected == true)
                    {
                        idDoc = enterd.idDocument;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        idDoc = 0;
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Select a document, please!") != null)
                            RadMessageBox.Show(resxSet.GetString("Select a document, please!"));
                        else
                            RadMessageBox.Show("Select a document, please!");
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                string what = "client";
                frmDocuments frdn = new frmDocuments(idClient, what);
                frdn.ShowDialog();
                DocumentsBUS dbus = new DocumentsBUS();
                model = new List<DocumentsModel>();
                model = dbus.GetClientDoc(idClient, Login._user.lngUser);

                gridDocuments.DataSource = null;
                gridDocuments.DataSource = model;
            }
            catch (Exception ex)
            {

            }
        }

        private void gridDocuments_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridDocuments != null)
            {
                if (gridDocuments.Columns.Count > 0)
                {
                    for (int i = 0; i < gridDocuments.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridDocuments.Columns[i].HeaderText != null && resxSet.GetString(gridDocuments.Columns[i].HeaderText) != null)
                                gridDocuments.Columns[i].HeaderText = resxSet.GetString(gridDocuments.Columns[i].HeaderText);
                        }
                    }

                    gridDocuments.Columns["idLayout"].IsVisible = false;
                    gridDocuments.Columns["selected"].ReadOnly = false;

                    foreach (GridViewColumn col in gridDocuments.Columns)
                    {
                        if (col.Name != "selected")
                        {
                            col.ReadOnly = true;
                        }
                    }

                    gridDocuments.Columns["dtCreated"].FormatString = "{0: dd/MM/yyyy}";
                    gridDocuments.Columns["dtModified"].FormatString = "{0: dd/MM/yyyy}";
                }
            }

            if (File.Exists(layoutDoc))
            {
                gridDocuments.LoadLayout(layoutDoc);
            }

        }
        private void gridDocuments_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
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

            //==delete           
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout1) != null && resxSet.GetString(saveLayout1) != "")
                    saveLayout = resxSet.GetString(saveLayout1);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutClientDoc;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutDoc))
            {
                File.Delete(layoutDoc);
            }
            gridDocuments.SaveLayout(layoutDoc);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }

        private void gridDocuments_CurrentRowChanged(object sender, Telerik.WinControls.UI.CurrentRowChangedEventArgs e)
        {
          
        }

        private void gridDocuments_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (gridDocuments.CurrentRow != null)
            {
                try
                {
                    GridViewRowInfo info = this.gridDocuments.CurrentRow;
                    DocumentsModel selectedRow = (DocumentsModel)info.DataBoundItem;
                    enterd = new DocumentsModel();
                    enterd = selectedRow;
                    string what = "client";
                    frmDocuments ffd = new frmDocuments(enterd.idDocument, enterd.idClient, what);
                    ffd.ShowDialog();
                    idDoc = enterd.idDocument;
                    DocumentsBUS dbus = new DocumentsBUS();
                    model = dbus.GetClientDoc(idClient, Login._user.lngUser);
                    gridDocuments.DataSource = null;
                    gridDocuments.DataSource = model;
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void gridDocuments_CellClick(object sender, GridViewCellEventArgs e)
        {
            int a = gridDocuments.CurrentRow.Index;
            if (a > 0)
            {
                if (gridDocuments.SelectedRows != null)
                {
                    if (model != null && model.Count > 0)
                    {
                        if (e.Column.Name == "selected")
                        {
                            if (model[a].selected == true)
                            {
                                foreach (DocumentsModel col in model)
                                {
                                    col.selected = false;
                                }
                                model[a].selected = true;
                            }
                            else
                            {
                                model[a].selected = false;
                            }
                        
                        }
                    }
                }
            }
        }
        bool isokvalc = false;
        private void gridDocuments_ValueChanged(object sender, EventArgs e)
        {
            string aname = gridDocuments.CurrentCell.ColumnInfo.Name;

            if (this.gridDocuments.ActiveEditor is RadCheckBoxEditor && aname == "selected")
            {
                if (isokvalc == false)
                {
                    isokvalc = true;
                    DocumentsModel mod = (DocumentsModel)gridDocuments.CurrentRow.DataBoundItem;
                  //  decimal valueM = (decimal)mod.debitOpenLine - (decimal)mod.creditOpenLine;
                    //int id = (int)gridLookup.CurrentRow.Cells["selected"].Value;
                    bool chechstate = Convert.ToBoolean(gridDocuments.ActiveEditor.Value);
                    if (chechstate == true)
                    {
                        foreach (DocumentsModel dkm in model)
                        {
                            if (dkm.idDocument != mod.idDocument)
                                dkm.selected = false;
                        }
                    }
                    isokvalc = false;
                }
            }

            gridDocuments.DataSource = null;
            gridDocuments.DataSource = model;
        }
        private void SaveLayoutClientDoc(object sender, EventArgs e)
        {
            if (File.Exists(layoutDoc))
            {
                File.Delete(layoutDoc);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");


        }
     
    }
}
