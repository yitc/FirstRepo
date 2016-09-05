using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI
{
    public partial class frmTempAccount : Telerik.WinControls.UI.RadForm
    {
        public frmTempAccount()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            //this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
           //ribbonExampleMenu.Text = "";
            //translate();
        }
       
        //private void translate()
        //{
        //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
        //    {
        //        //ribbon bar
        //        if (resxSet.GetString("Save")!=null)
        //        btnSave.Text = resxSet.GetString("Save");
        //        if (resxSet.GetString("New") != null)
        //        {
        //            btnNewDoc.Text = resxSet.GetString("New");
        //            btnNewMemo.Text = resxSet.GetString("New");
        //            btnNewContact.Text = resxSet.GetString("New");
        //            btnNewTask.Text = resxSet.GetString("New");
        //        }
        //        if (resxSet.GetString("Delete") != null)
        //        {
        //            btnDeleteDoc.Text = resxSet.GetString("Delete");
        //            btnDeleteMemo.Text = resxSet.GetString("Delete");
        //            btnDelContact.Text = resxSet.GetString("Delete");
        //            btnDelTask.Text = resxSet.GetString("Delete");
        //        }
        //        if (resxSet.GetString("Email") != null)
        //        btnEmail.Text = resxSet.GetString("Email");
        //        if (resxSet.GetString("Report") != null)
        //        btnReport.Text = resxSet.GetString("Report");
        //        if (resxSet.GetString("Memo") != null)
        //        radRibbonMemo.Text = resxSet.GetString("Memo");
        //        if (resxSet.GetString("Document") != null)
        //        radRibbonDocuments.Text = resxSet.GetString("Document");
        //        if (resxSet.GetString("HOME") != null)
        //        ribbonTab1.Text = resxSet.GetString("HOME");
        //        if (resxSet.GetString("Task") != null)
        //        radRibbonTask.Text = resxSet.GetString("Task");
        //        if (resxSet.GetString("Contact") != null)
        //        radRibbonContact.Text = resxSet.GetString("Contact");
        //    }
        //}

        protected void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, BIS.Model.BookmarkSpecModel oBooks)
        {
            if (doc.Bookmarks.Exists(oBooks.field))
            {
                Object name = oBooks.field;

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = oBooks.value;
                object newRange = range;
                doc.Bookmarks.Add(oBooks.field, ref newRange);
            }
        }
        protected void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, string text)
        {
            if (doc.Bookmarks.Exists(bookmarkName))
            {
                Object name = bookmarkName;

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = text;
                object newRange = range;
                doc.Bookmarks.Add(bookmarkName, ref newRange);
            }
        }
        protected void DeleteEmptyBookmarks(Microsoft.Office.Interop.Word.Document doc, List<BIS.Model.BookmarkSpecModel> existingBookmarks)
        {
            foreach (Microsoft.Office.Interop.Word.Bookmark b in doc.Bookmarks)
            {
                //MessageBox.Show(b.Name + " - " + b.Range);
                bool exist = existingBookmarks.Exists(x => x.field == b.Name);

                if (exist == false)
                {
                    object bmkname = b.Name;
                    Microsoft.Office.Interop.Word.Range deleteRange = doc.Range();

                    deleteRange.Start = doc.Bookmarks.get_Item(ref bmkname).Range.Start;
                    deleteRange.End = doc.Bookmarks.get_Item(ref bmkname).Range.End + 1;
                    //MessageBox.Show(deleteRange.Text);
                    deleteRange.Delete();
                }
            }

            Microsoft.Office.Interop.Word.Paragraphs paragraphs = doc.Paragraphs;

            foreach (Microsoft.Office.Interop.Word.Paragraph paragraph in paragraphs)
            {

                if (paragraph.Range.Text.Trim() == string.Empty)
                {
                    //izbrisi prazne redove
                  //  paragraph.Range.Delete();
                }
                else
                {
                    // sko ima space na pocetku reda izbrisi ga
                    if (paragraph.Range.Characters[1].Text == " ")
                    {
                        paragraph.Range.Characters[1].Delete();
                    }
                }
            }

        }

        protected string CreateDocName(Int32 iID)
        {
            //return iID.ToString("00000000") + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString("00000");
            return iID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
