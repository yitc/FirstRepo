using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using BIS.Model;
using BIS.Business;
using BIS.Core;
using System.IO;
using System.Resources;

namespace GUI
{
    public partial class frmBookmark2 : RadForm
    {
        private Microsoft.Office.Interop.Word.Application wordApp;
        string templateFolder;

        // tabele za dropdown listu
        List<BookmarkDefModel> bookmarkTables = new List<BookmarkDefModel>();
        BindingList<BookmarkDefModel> documentBookmarks = new BindingList<BookmarkDefModel>();
        
        List<BookmarkTableModel> reportBookmarks = new List<BookmarkTableModel>();
        BindingList<BookmarkTableModel> addedReportTableBookmarks = new BindingList<BookmarkTableModel>();
        
        LayoutsModel layoutEdit;
        Microsoft.Office.Interop.Word.Document updateDoc;
        Microsoft.Office.Interop.Word.Document insertDoc = null ;


        bool pageLoaded = false;

        public frmBookmark2()
        {
            InitializeComponent();

            templateFolder = MainForm.TemplatesFolder;
            this.radDropDownListTables.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;

            layoutEdit = null;

            this.Icon = Login.iconForm;
            Translation();
        }

        public frmBookmark2(LayoutsModel lmodel)
        {
            InitializeComponent();

            templateFolder = MainForm.TemplatesFolder;
            this.radDropDownListTables.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;

            layoutEdit = lmodel;

            this.Icon = Login.iconForm;
            Translation();
        }


        private void frmBookmark2_Load(object sender, EventArgs e)
        {
            radPageViewBookmarks.SelectedPage = pageClientPerson;
            
            radListViewDocBookmarks.DataSource = documentBookmarks;
            radListViewDocBookmarks.ValueMember = "nameBookmark";
            radListViewDocBookmarks.DisplayMember = "displayNameBookmark";

            radListViewReportsAdded.DataSource = addedReportTableBookmarks;
            radListViewReportsAdded.ValueMember = "idTable";
            radListViewReportsAdded.DisplayMember = "displayNameTable";

            BookmarkDefBUS bookBUS = new BookmarkDefBUS();
            bookmarkTables = bookBUS.GetBookmarksByDistinctTableName();

            if (bookmarkTables != null)
            {
                radDropDownListTables.DataSource = bookmarkTables;
                radDropDownListTables.ValueMember = "tableName";
                radDropDownListTables.DisplayMember = "tableDisplayName";


            }
            

            radListViewDocBookmarks.AllowDragDrop = true;

            if(layoutEdit == null)
            {
                this.Text = "New Template";
                radButtonCreateTemplate.Text = "Create Template";
                updateDoc = null;

                radListViewDocBookmarks.ShowCheckBoxes = false;
                radLabelRemoveBookmarks.Visible = false;
                radCheckBoxSaveAs.Visible = false;
                
            }
            else
            {                

                // EDIT TEMPLATE

                radListViewDocBookmarks.ShowCheckBoxes = true;
                radLabelRemoveBookmarks.Visible = true;
                radCheckBoxSaveAs.Visible = true;
               
                this.Text = "Edit Template";
                radButtonCreateTemplate.Text = "Edit Template";

                radDropDownListTables.SelectedValue = layoutEdit.templateTable;
                radDropDownListTables.Enabled = false;
                radButtonLoadBookmarks.Enabled = false;

                radListViewDBBookmarks.Items.Clear();
                radListViewDocBookmarks.Items.Clear();
                documentBookmarks.Clear();
                radButtonCreateTemplate.Enabled = false;
                
                List<BookmarkDefModel> dbBookmarks = new List<BookmarkDefModel>();
                dbBookmarks = bookBUS.GetBookmarksByTableName(radDropDownListTables.SelectedValue.ToString().Trim());

                if (dbBookmarks != null)
                {
                    radListViewDBBookmarks.DataSource = dbBookmarks;
                    radListViewDBBookmarks.ValueMember = "nameBookmark";
                    radListViewDBBookmarks.DisplayMember = "displayNameBookmark";


                }

                try
                {
                    if (File.Exists(templateFolder + "\\" + layoutEdit.fileLayout) == true)
                    {
                        wordApp = new Microsoft.Office.Interop.Word.Application();
                        updateDoc = wordApp.Documents.Open(templateFolder + "\\" + layoutEdit.fileLayout);

                        string bookmarksNames = "";
                        List<BookmarkDefModel> bookmarksInDocument = new List<BookmarkDefModel>();

                        foreach (Microsoft.Office.Interop.Word.Bookmark b in updateDoc.Bookmarks)
                        {
                            //Microsoft.Office.Interop.Word.Range deleteRange = doc.Range();
                            //deleteRange.Start = b.Range.Start;
                            //deleteRange.End = b.Range.End;
                            //deleteRange.Delete();

                            string bmName = b.Name;
                            string originalBookmarkName = b.Name;

                            if (bmName.Contains("__") == true)
                            {
                                int index = bmName.IndexOf("__");
                                bmName = bmName.Remove(index);
                            }
                            
                            if(bmName.Contains("Table") == true)
                            {
                                BookmarkTableModel tmpmodel = bookBUS.GetTableByName(bmName.Replace("Table",""));
                                if(tmpmodel != null)
                                {
                                    tmpmodel.nameTable = originalBookmarkName.Replace("Table","");
                                    addedReportTableBookmarks.Add(tmpmodel);
                                }
                            }
                            else if (bmName == "datetimebookmark")
                            {
                                chkDateTime.Checked = true;
                            }
                            else
                            {
                                BookmarkDefModel tmpmodel = bookBUS.GetBookmarksByNameBookmark(bmName);
                                if (tmpmodel != null)
                                {
                                    tmpmodel.nameBookmark = originalBookmarkName;
                                    documentBookmarks.Add(tmpmodel);
                                }
                            }

                            bookmarksNames += bmName;
                            bookmarksNames += "\n";
                        }

                        //change color to blue for existing bookmarks in document
                        foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                        {
                            item.ForeColor = Color.Blue;
                        }

                        //MessageBox.Show(bookmarksNames);
                    }
                    else
                    {
                        MessageBox.Show("Template " + layoutEdit.fileLayout + " does not exist");
                        this.Close();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
            }

            
            
            if (documentBookmarks.Count > 0)
                radButtonCreateTemplate.Enabled = true;
            else
                radButtonCreateTemplate.Enabled = false;

            reportBookmarks = bookBUS.GetTableBookmarks();
            //ako je edit mode loaduj bookmarke
            if (layoutEdit != null && reportBookmarks != null && radDropDownListTables.SelectedValue.ToString().Trim() == "ContactPerson")
            {
                radListViewReports.DataSource = reportBookmarks;
                radListViewReports.ValueMember = "idTable";
                radListViewReports.DisplayMember = "displayNameTable";
            }

            pageLoaded = true;
        }

        private void radButtonLoadBookmarks_Click(object sender, EventArgs e)
        {
            if (documentBookmarks.Count > 0 || addedReportTableBookmarks.Count > 0)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("This action will remove selected bookmarks, do you want to proceed ? ", "Warrning");

                if (dr == DialogResult.No || dr == DialogResult.Cancel)
                    return;
            }

            radListViewDBBookmarks.Items.Clear();
            radListViewDocBookmarks.Items.Clear();
            addedReportTableBookmarks.Clear();
            chkDateTime.Checked = false;
            documentBookmarks.Clear();
            radButtonCreateTemplate.Enabled = false;

            BookmarkDefBUS bookBUS = new BookmarkDefBUS();
            List<BookmarkDefModel> dbBookmarks = new List<BookmarkDefModel>();
            dbBookmarks = bookBUS.GetBookmarksByTableName(radDropDownListTables.SelectedValue.ToString().Trim());

            List<BookmarkDefModel> dbBookmarksAccDebCre = new List<BookmarkDefModel>();
            dbBookmarksAccDebCre = bookBUS.GetBookmarksByTableName("AccDebCre");
            if(dbBookmarksAccDebCre != null)
            {
                foreach(BookmarkDefModel m in dbBookmarksAccDebCre)
                {
                    dbBookmarks.Add(m);
                }
            }
            

            if (dbBookmarks != null)
            {
                radPageViewBookmarks.SelectedPage = pageClientPerson;
                radListViewDBBookmarks.DataSource = dbBookmarks;
                radListViewDBBookmarks.ValueMember = "nameBookmark";
                radListViewDBBookmarks.DisplayMember = "displayNameBookmark";

                
            }


            if(radDropDownListTables.SelectedValue.ToString().Trim() == "ContactPerson")
            {
                if (reportBookmarks != null)
                {
                    radListViewReports.DataSource = null;
                    radListViewReports.DataSource = reportBookmarks;
                    radListViewReports.ValueMember = "idTable";
                    radListViewReports.DisplayMember = "displayNameTable";
                }
            }
            else
            {
                radListViewReports.DataSource = null;
            }

                
            //if(dbBookmarks != null)
            //{
            //    foreach(BookmarkDefModel m in dbBookmarks)
            //    {
            //        //radListControlDatabseBookmarks.Items.Add()
            //    }
            //}
        }

        private void radListControlDatabseBookmarks_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void radListViewDBBookmarks_ItemMouseDoubleClick(object sender, ListViewItemEventArgs e)
        {
            
                BookmarkDefModel bModel = (BookmarkDefModel)e.Item.DataBoundItem;
                documentBookmarks.Add(bModel);

                if (documentBookmarks.Count > 0)
                    radButtonCreateTemplate.Enabled = true;
                else
                    radButtonCreateTemplate.Enabled = false;
            
        }
        private void radListViewDocBookmarks_ItemMouseDoubleClick(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ForeColor != Color.Blue && e.Item.ForeColor != Color.Red)
            {
                BookmarkDefModel bModel = (BookmarkDefModel)e.Item.DataBoundItem;
                documentBookmarks.Remove(bModel);

                if (documentBookmarks.Count > 0)
                    radButtonCreateTemplate.Enabled = true;
                else
                    radButtonCreateTemplate.Enabled = false;
            }
        }
        private void radButtonAdd_Click(object sender, EventArgs e)
        {
            if (radPageViewBookmarks.SelectedPage == pageClientPerson)
            {
                if (radListViewDBBookmarks.SelectedIndex != -1)
                {
                    BookmarkDefModel bModel = (BookmarkDefModel)radListViewDBBookmarks.SelectedItem.DataBoundItem;
                    documentBookmarks.Add(bModel);

                    if (documentBookmarks.Count > 0)
                        radButtonCreateTemplate.Enabled = true;
                    else
                        radButtonCreateTemplate.Enabled = false;
                }
            }

            if (radPageViewBookmarks.SelectedPage == pageTables)
            {
                if (radListViewReports.SelectedIndex != -1)
                {
                    BookmarkTableModel bModel = (BookmarkTableModel)radListViewReports.SelectedItem.DataBoundItem;

                    if (addedReportTableBookmarks.Contains(bModel) == false)
                        addedReportTableBookmarks.Add(bModel);                    
                }
            }
        }

        private void radButtonRemove_Click(object sender, EventArgs e)
        {

            if (radListViewDocBookmarks.SelectedIndex != -1)
            {
                if (radListViewDocBookmarks.SelectedItem.ForeColor != Color.Blue && radListViewDocBookmarks.SelectedItem.ForeColor != Color.Red)
                {
                    BookmarkDefModel bModel = (BookmarkDefModel)radListViewDocBookmarks.SelectedItem.DataBoundItem;
                    documentBookmarks.Remove(bModel);

                    if (documentBookmarks.Count > 0)
                        radButtonCreateTemplate.Enabled = true;
                    else
                        radButtonCreateTemplate.Enabled = false;
                }
            }
        }

        private void radListViewDocBookmarks_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void radListViewDocBookmarks_DragOver(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.Move;
        }

        private void radListViewDocBookmarks_DragDrop(object sender, DragEventArgs e)
        {
           
            //Point point = radListViewDocBookmarks.PointToClient(new Point(e.X, e.Y));

            //BaseListViewVisualItem targetItem = this.radListViewDocBookmarks.ElementTree.GetElementAtPoint(point) as BaseListViewVisualItem;
            ////string draggedText = e.Data.GetData(typeof(string)).ToString();

            //if (targetItem != null)
            //{
            //    int index = this.radListViewDocBookmarks.Items.IndexOf(targetItem.Data);
            //    if (index < 0) index = this.radListViewDocBookmarks.Items.Count - 1;
            //    object data = e.Data.GetData(typeof(BookmarkDefModel));
            //    this.radListViewDocBookmarks.Items.Remove(data);
            //    this.radListViewDocBookmarks.Items.Insert(index, data);
            //}
            
        }

        private void radListViewDocBookmarks_MouseDown(object sender, MouseEventArgs e)
        {
            //if (this.radListViewDocBookmarks.SelectedItem == null) return;
            //this.radListViewDocBookmarks.DoDragDrop(this.radListViewDocBookmarks.SelectedItem, DragDropEffects.Move);
        }

        private void radListViewDocBookmarks_BindingContextChanged(object sender, EventArgs e)
        {
        }

        private void radListViewDocBookmarks_ItemCreating(object sender, ListViewItemCreatingEventArgs e)
        {
          
        }

        private void radListViewDocBookmarks_ItemRemoved(object sender, ListViewItemEventArgs e)
        {

          
        }

        private void radButtonCreateTemplate_Click(object sender, EventArgs e)
        {

             if (layoutEdit == null)
             {
                 SaveFileDialog saveFileDialog = new SaveFileDialog();
                 saveFileDialog.InitialDirectory = templateFolder;
                 saveFileDialog.Filter = "Word Files (.docx)|*.docx";
                 saveFileDialog.FilterIndex = 1;
                 saveFileDialog.RestoreDirectory = true;
                 //saveFileDialog.Multiselect = false;

                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
                 {
                     if (saveFileDialog.FileName != "")
                     {                         
                         if (System.IO.Path.GetDirectoryName(saveFileDialog.FileName).ToLower() == templateFolder.ToLower())
                         {
                             SaveTemplate(saveFileDialog.FileName, insertDoc);
                         }
                         else
                         {
                             MessageBox.Show("Templates cannon be saved in selected folder.");
                         }
                     }
                     else
                     {
                         MessageBox.Show("You must enter filename.");
                     }
                 }
             }
             else
             {
                 if (radCheckBoxSaveAs.Checked == true)
                 {
                     SaveFileDialog saveFileDialog = new SaveFileDialog();
                     saveFileDialog.InitialDirectory = templateFolder;
                     saveFileDialog.Filter = "Word Files (.docx)|*.docx";
                     saveFileDialog.FilterIndex = 1;
                     saveFileDialog.RestoreDirectory = true;

                     if (saveFileDialog.ShowDialog() == DialogResult.OK)
                     {
                         if (saveFileDialog.FileName != "")
                         {
                             if (System.IO.Path.GetDirectoryName(saveFileDialog.FileName).ToLower() == templateFolder.ToLower())
                             {
                                 UpdateTemplateSaveAs(ref updateDoc, saveFileDialog.FileName);
                             }
                             else
                             {
                                 MessageBox.Show("Templates cannon be saved in selected folder.");
                             }

                         }
                         else
                         {
                             MessageBox.Show("You must enter filename.");
                         }
                     }
                 }
                 else
                 {
                     DialogResult dr = MessageBox.Show("Update template: " + layoutEdit.nameLayout + " ?", "Edit Layout", MessageBoxButtons.YesNo);
                     if (dr == DialogResult.Yes)
                         UpdateTemplate(ref updateDoc);
                 }
             }
            
        }

        public void SaveTemplate(string filename, Microsoft.Office.Interop.Word.Document doc)
        {
            wordApp = new Microsoft.Office.Interop.Word.Application();
            doc = wordApp.Documents.Add();

            // add image header into document
            Microsoft.Office.Interop.Word.HeaderFooter header = doc.Sections[1].Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
            ImageDB image = new ImageDB();
            Image img = image.setImage(Login._companyModelList[0].logoCompany);
            img.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\companylogo.png");
            string logoFile = AppDomain.CurrentDomain.BaseDirectory + "Images\\companylogo.png";
            header.Range.InlineShapes.AddPicture(logoFile);
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Images\\companylogo.png");
            img.Dispose();

            string filenameWithoutPath = System.IO.Path.GetFileName(filename);
            string fileWitouthExtension = System.IO.Path.GetFileNameWithoutExtension(filenameWithoutPath);

            string bookmarksToSave = "";

            int i = 1;
            foreach(BookmarkDefModel b in documentBookmarks)
            {
                bookmarksToSave += b.Id.ToString();
                
                if (i < documentBookmarks.Count)
                    bookmarksToSave += ",";

                i++;                
            }

            LayoutsModel layoutsModel = new LayoutsModel(fileWitouthExtension,
               "WCPLET",
               Login._user.lngUser,
               filenameWithoutPath,
               bookmarksToSave,
               radDropDownListTables.SelectedValue.ToString(),
               Login._user.idUser, DateTime.Now,
               Login._user.idUser, DateTime.Now);

            LayoutsBUS lbus = new LayoutsBUS();
            bool retvalLayout = lbus.SaveLayout(layoutsModel, this.Name, Login._user.idUser);
            
            if(retvalLayout == true)
            {
                int j = 0;
                foreach(BookmarkDefModel b in documentBookmarks)
                {
                    if (doc.Bookmarks.Exists(b.nameBookmark))
                    {
                        string newbookmarkname = b.nameBookmark + "__" + j.ToString();
                        AddBookmark(doc, newbookmarkname, b.tableDisplayName + " - " + b.displayNameBookmark);
                    }
                    else
                    {
                        AddBookmark(doc, b.nameBookmark, b.tableDisplayName + " - " + b.displayNameBookmark);
                    }

                    j++;
                }

                foreach(BookmarkTableModel btm in addedReportTableBookmarks)
                {
                    AddBookmark(doc, btm.nameTable + "Table", btm.nameTable + "Table");
                }


                if(chkDateTime.Checked == true)
                {
                    AddBookmark(doc, "datetimebookmark", "DateTime");
                }

                doc.SaveAs2(filename);

                doc.Application.Visible = true;
                doc.Activate();

                this.Close();
            }            
        }

        public void UpdateTemplate(ref Microsoft.Office.Interop.Word.Document doc)
        {           
            string bookmarksToSave = "";

            try
            {
                //delete selected bookmars first
                List<BookmarkDefModel> bookmarksToDelete = new List<BookmarkDefModel>();
                foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        BookmarkDefModel bm = (BookmarkDefModel)item.DataBoundItem;
                        bookmarksToDelete.Add(bm);
                        //radListViewDocBookmarks.Items.Remove(bm);                   
                    }
                }

                //select new bookmarks to add to document
                List<BookmarkDefModel> bookmarksToUpdate = new List<BookmarkDefModel>();
                foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                {
                    if (item.ForeColor != Color.Red && item.ForeColor != Color.Blue)
                    {
                        BookmarkDefModel bm = (BookmarkDefModel)item.DataBoundItem;
                        bookmarksToUpdate.Add(bm);
                    }

                }

                // save bookmark ids to bookmarkdef table           
                foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.Off)
                    {
                        BookmarkDefModel bm = (BookmarkDefModel)item.DataBoundItem;

                        bookmarksToSave += bm.Id.ToString();
                        bookmarksToSave += ",";
                    }
                }

                char c = bookmarksToSave[bookmarksToSave.Length - 1];
                if (c == ',')
                    bookmarksToSave = bookmarksToSave.Remove(bookmarksToSave.Length - 1);

                //int i = 1;
                //foreach (BookmarkDefModel b in bookmarksToUpdate)
                //{
                //    bookmarksToSave += b.Id.ToString();

                //    if (i < bookmarksToUpdate.Count)
                //        bookmarksToSave += ",";

                //    i++;
                //}

                LayoutsModel newModel = new LayoutsModel();
                newModel.idLayout = layoutEdit.idLayout;
                newModel.nameLayout = layoutEdit.nameLayout;
                newModel.typeDocument = layoutEdit.typeDocument;
                newModel.languageLayout = layoutEdit.languageLayout;
                newModel.fileLayout = layoutEdit.fileLayout;
                newModel.bookmarks = bookmarksToSave;
                newModel.templateTable = layoutEdit.templateTable;
                newModel.userCreated = layoutEdit.userCreated;
                newModel.dtCreated = layoutEdit.dtCreated;
                newModel.userModified = Login._user.idUser;
                newModel.dtModified = DateTime.Now;


                LayoutsBUS lbus = new LayoutsBUS();
                bool retvalLayout = lbus.UpdateLayout(newModel, this.Name, Login._user.idUser);


                // reset bookmars name to original. as defined in BookmarkDef tabke (nameBookmark)
                foreach (BookmarkDefModel b in bookmarksToUpdate)
                {
                    string bmName = b.nameBookmark;
                    if (bmName.Contains("__") == true)
                    {
                        int index = bmName.IndexOf("__");
                        bmName = bmName.Remove(index);
                    }
                    b.nameBookmark = bmName;
                }

                // add bookmarks to document
                int j = 1000;
                foreach (BookmarkDefModel b in bookmarksToUpdate)
                {
                    if (doc.Bookmarks.Exists(b.nameBookmark))
                    {
                        string newbookmarkname = b.nameBookmark + "__" + j.ToString();
                        AddBookmark(doc, newbookmarkname, b.tableDisplayName + " - " + b.displayNameBookmark);
                    }
                    else
                    {
                        AddBookmark(doc, b.nameBookmark, b.tableDisplayName + " - " + b.displayNameBookmark);
                    }

                    j++;
                }

                // delete selected bookmarks
                foreach (BookmarkDefModel b in bookmarksToDelete)
                {
                    if (doc.Bookmarks.Exists(b.nameBookmark))
                    {
                        RemoveBookmark(doc, b.nameBookmark);
                    }
                }

                doc.Save();


                doc.Application.Visible = true;
                doc.Activate();


                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void UpdateTemplateSaveAs(ref Microsoft.Office.Interop.Word.Document doc, string filename)
        {

            try
            {
                string filenameWithoutPath = System.IO.Path.GetFileName(filename);
                string fileWitouthExtension = System.IO.Path.GetFileNameWithoutExtension(filenameWithoutPath);

                string bookmarksToSave = "";



                //delete selected bookmars first
                List<BookmarkDefModel> bookmarksToDelete = new List<BookmarkDefModel>();
                foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        BookmarkDefModel bm = (BookmarkDefModel)item.DataBoundItem;
                        bookmarksToDelete.Add(bm);
                        //radListViewDocBookmarks.Items.Remove(bm);                   
                    }
                }

                //select new bookmarks to add to document
                List<BookmarkDefModel> bookmarksToUpdate = new List<BookmarkDefModel>();
                foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                {
                    if (item.ForeColor != Color.Red && item.ForeColor != Color.Blue)
                    {
                        BookmarkDefModel bm = (BookmarkDefModel)item.DataBoundItem;
                        bookmarksToUpdate.Add(bm);
                    }

                }

                // save bookmark ids to bookmarkdef table           
                foreach (ListViewDataItem item in radListViewDocBookmarks.Items)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.Off)
                    {
                        BookmarkDefModel bm = (BookmarkDefModel)item.DataBoundItem;

                        bookmarksToSave += bm.Id.ToString();
                        bookmarksToSave += ",";
                    }
                }

                char c = bookmarksToSave[bookmarksToSave.Length - 1];
                if (c == ',')
                    bookmarksToSave = bookmarksToSave.Remove(bookmarksToSave.Length - 1);

                LayoutsModel newModel = new LayoutsModel();
                newModel.nameLayout = fileWitouthExtension;
                newModel.typeDocument = layoutEdit.typeDocument;
                newModel.languageLayout = layoutEdit.languageLayout;
                newModel.fileLayout = filenameWithoutPath;
                newModel.bookmarks = bookmarksToSave;
                newModel.templateTable = layoutEdit.templateTable;
                newModel.userCreated = Login._user.idUser;
                newModel.dtCreated = DateTime.Now;
                newModel.userModified = Login._user.idUser;
                newModel.dtModified = DateTime.Now;


                LayoutsBUS lbus = new LayoutsBUS();
                bool retvalLayout = lbus.SaveLayout(newModel, this.Name, Login._user.idUser);


                // reset bookmars name to original. as defined in BookmarkDef tabke (nameBookmark)
                foreach (BookmarkDefModel b in bookmarksToUpdate)
                {
                    string bmName = b.nameBookmark;
                    if (bmName.Contains("__") == true)
                    {
                        int index = bmName.IndexOf("__");
                        bmName = bmName.Remove(index);
                    }
                    b.nameBookmark = bmName;
                }

                // add bookmarks to document
                int j = 1000;
                foreach (BookmarkDefModel b in bookmarksToUpdate)
                {
                    if (doc.Bookmarks.Exists(b.nameBookmark))
                    {
                        string newbookmarkname = b.nameBookmark + "__" + j.ToString();
                        AddBookmark(doc, newbookmarkname, b.tableDisplayName + " - " + b.displayNameBookmark);
                    }
                    else
                    {
                        AddBookmark(doc, b.nameBookmark, b.tableDisplayName + " - " + b.displayNameBookmark);
                    }

                    j++;
                }

                // delete selected bookmarks
                foreach (BookmarkDefModel b in bookmarksToDelete)
                {
                    if (doc.Bookmarks.Exists(b.nameBookmark))
                    {
                        RemoveBookmark(doc, b.nameBookmark);
                    }
                }

                doc.SaveAs2(filename);

                doc.Application.Visible = true;
                doc.Activate();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddBookmark(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, string text)
        {
            Microsoft.Office.Interop.Word.Range rng = doc.Range();
            rng.SetRange(doc.Content.End, doc.Content.End + 4);
            rng.Text = " \n ";

            Microsoft.Office.Interop.Word.Range rng1 = doc.Range();
            rng1.SetRange(doc.Content.End, doc.Content.End + text.Length + 2);
            rng1.Text = "[" + text + "]";
            object r = rng1;

            doc.Bookmarks.Add(bookmarkName, ref r);
        }

        private void RemoveBookmark(Microsoft.Office.Interop.Word.Document doc, string bookmarkName)
        {
            if (doc.Bookmarks.Exists(bookmarkName))
            {
                var start = doc.Bookmarks[bookmarkName].Start;
                var end = doc.Bookmarks[bookmarkName].End;
                Microsoft.Office.Interop.Word.Range range = doc.Range(start, end);
                range.Delete();
            }
        }

        private void radListViewDocBookmarks_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ForeColor == Color.Red)
                e.Item.ForeColor = Color.Blue;
            else if (e.Item.ForeColor == Color.Blue)
                e.Item.ForeColor = Color.Red;
            else
            {

            }

        }

        private void radListViewDocBookmarks_ItemCheckedChanging(object sender, ListViewItemCancelEventArgs e)
        {
            if (e.Item.ForeColor != Color.Blue && e.Item.ForeColor != Color.Red)
                e.Cancel = true;
        }

        private void frmBookmark2_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (insertDoc != null)
                {
                    if (insertDoc.Application.Visible == false)
                    {
                        ((Microsoft.Office.Interop.Word._Document)insertDoc).Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                        insertDoc = null;
                        ((Microsoft.Office.Interop.Word.Application)wordApp).Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                    }
                }

                if (updateDoc != null)
                {
                    if (updateDoc.Application.Visible == false)
                    {
                        ((Microsoft.Office.Interop.Word._Document)updateDoc).Close(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                        updateDoc = null;
                        ((Microsoft.Office.Interop.Word.Application)wordApp).Quit(Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radListViewDocBookmarks_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            //if (e.VisualItem.ForeColor != Color.Red && e.VisualItem.ForeColor != Color.Blue && pageLoaded == true)
            //{
                //e.VisualItem.ToggleElement.Enabled = false;
               // e.VisualItem.ToggleElement.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            //}
        }

        private void radListViewDocBookmarks_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            //if(pageLoaded == true)
            //{
            //    e.VisualItem.ToggleElement.Visibility = Telerik.WinControls.ElementVisibility.Hidden;                
            //}
        }

        private void radListViewReports_ItemMouseDoubleClick(object sender, ListViewItemEventArgs e)
        {
            BookmarkTableModel bModel = (BookmarkTableModel)e.Item.DataBoundItem;

            if(addedReportTableBookmarks.Contains(bModel) == false)
                addedReportTableBookmarks.Add(bModel);
            
        }

        private void radListViewReportsAdded_ItemMouseDoubleClick(object sender, ListViewItemEventArgs e)
        {
            BookmarkTableModel bModel = (BookmarkTableModel)e.Item.DataBoundItem;
            addedReportTableBookmarks.Remove(bModel);
                
        }


        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);

                if (resxSet.GetString(pageClientPerson.Text) != null)
                    pageClientPerson.Text = resxSet.GetString(pageClientPerson.Text);

                if (resxSet.GetString(pageTables.Text) != null)
                    pageTables.Text = resxSet.GetString(pageTables.Text);

                if (resxSet.GetString(radButtonLoadBookmarks.Text) != null)
                    radButtonLoadBookmarks.Text = resxSet.GetString(radButtonLoadBookmarks.Text);

                if (resxSet.GetString(radButtonAdd.Text) != null)
                    radButtonAdd.Text = resxSet.GetString(radButtonAdd.Text);

                if (resxSet.GetString(radButtonRemove.Text) != null)
                    radButtonRemove.Text = resxSet.GetString(radButtonRemove.Text);

                if (resxSet.GetString(radLabel1.Text) != null)
                    radLabel1.Text = resxSet.GetString(radLabel1.Text);

                if (resxSet.GetString(chkDateTime.Text) != null)
                    chkDateTime.Text = resxSet.GetString(chkDateTime.Text);

                if (resxSet.GetString(radButtonCreateTemplate.Text) != null)
                    radButtonCreateTemplate.Text = resxSet.GetString(radButtonCreateTemplate.Text);

                if (resxSet.GetString(radCheckBoxSaveAs.Text) != null)
                    radCheckBoxSaveAs.Text = resxSet.GetString(radCheckBoxSaveAs.Text);

                if (resxSet.GetString(radLabelRemoveBookmarks.Text) != null)
                    radLabelRemoveBookmarks.Text = resxSet.GetString(radLabelRemoveBookmarks.Text);

            }
        }
        
    }
}
