using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace GUI
{
    public static class BookmarkFunctions
    {
        public static void ReadTemplateFile(Microsoft.Office.Interop.Word.Application wordApp, string TableName, string IdFiledName, int IdFieldValue, DataTable datatable, BIS.Model.LayoutsModel layoutmodel, string nameForm, int idUser)
        {
            BIS.Model.LayoutsModel selmodel = null;
            wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = null;

            try
            {
                

                    selmodel = layoutmodel;
                    string allBOokmarks = selmodel.bookmarks;

                    string[] bookmarkArray = allBOokmarks.Split(',');
                    List<BIS.Model.BookmarkDefModel> bookmarkList = new List<BIS.Model.BookmarkDefModel>();

                    BIS.Business.BookmarkDefBUS bdefBUS = new BIS.Business.BookmarkDefBUS();

                    foreach (string bina in bookmarkArray)
                    {
                        BIS.Model.BookmarkDefModel model = bdefBUS.GetBookmarkByID(Int32.Parse(bina.Trim()));
                        if (model != null)
                        {
                            bookmarkList.Add(model);
                        }
                    }

                    //SELECT firstname FROM ContactPerson WHERE  idContPers = 3

                    //SELECT nameTitle FROM Title WHERE  idTitle = 1

                    //SELECT street FROM ContactPersonAddress WHERE  idContPers = 3 and idAddressType = 1

                    List<BIS.Model.BookmarkSpecModel> specModelList = new List<BIS.Model.BookmarkSpecModel>();
                    foreach (BIS.Model.BookmarkDefModel m in bookmarkList)
                    {
                        BIS.Model.BookmarkSpecModel s = new BIS.Model.BookmarkSpecModel();
                        s.field = m.nameBookmark;
                        s.table = m.tableName;

                        if (m.isRelationBmk == false)
                        {
                            object obj;
                            if (m.tableName == "AccDebCre")
                                obj = bdefBUS.CustomBookmarksQuery(m.tableName, m.fieldBookmark, "idContPerson", IdFieldValue, null, -1);
                            else
                                obj = bdefBUS.CustomBookmarksQuery(m.tableName, m.fieldBookmark, IdFiledName, IdFieldValue, null, -1);

                            if (obj != null)
                                s.value = obj.ToString().Trim();
                            else
                                s.value = "";
                        }
                        else
                        {
                            if (m.hasType == false)
                            {
                                
                                //object obj = bdefBUS.CustomBookmarksQuery(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, null, -1);
                                object obj = bdefBUS.CustomBookmarksQueryForTypes(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, null, -1, TableName, IdFiledName);

                                if (obj != null)
                                    s.value = obj.ToString().Trim();
                                else
                                    s.value = "";
                            }
                            else
                            {
                                object obj = bdefBUS.CustomBookmarksQuery(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, m.typeFieldName, m.typeFieldValue);

                                if (obj != null)
                                    s.value = obj.ToString().Trim();
                                else
                                    s.value = "";
                            }
                        }
                        specModelList.Add(s);
                    }


                    string templateFolder = MainForm.TemplatesFolder + "\\";
                    string documentFolder = MainForm.DocumentsFolder + "\\";

                    object fileName = templateFolder + selmodel.nameLayout + ".docx";
                    object confirmConversions = Type.Missing;
                    object readOnly = Type.Missing;
                    object addToRecentFiles = Type.Missing;
                    object passwordDoc = Type.Missing;
                    object passwordTemplate = Type.Missing;
                    object revert = Type.Missing;
                    object writepwdoc = Type.Missing;
                    object writepwTemplate = Type.Missing;
                    object format = Type.Missing;
                    object encoding = Type.Missing;
                    object visible = Type.Missing;
                    object openRepair = Type.Missing;
                    object docDirection = Type.Missing;
                    object notEncoding = Type.Missing;
                    object xmlTransform = Type.Missing;

                    //wordApp = new Microsoft.Office.Interop.Word.Application();
                    doc = wordApp.Documents.Open(ref fileName, ref confirmConversions, ref readOnly, ref addToRecentFiles, ref passwordDoc, ref passwordTemplate, ref revert, ref writepwdoc, ref writepwTemplate, ref format, ref encoding, ref visible, ref openRepair, ref docDirection, ref notEncoding, ref xmlTransform);



                    foreach (BIS.Model.BookmarkSpecModel oBooks in specModelList)
                        ReplaceBookmarkTextNew(doc, oBooks);

                    if (datatable != null)
                        ReplaceBookmarkTable(doc, datatable);

                    DeleteEmptyBookmarks(doc, specModelList);

                    string sFile = CreateDocName(IdFieldValue) + ".docx";
                    fileName = documentFolder + sFile;

                    doc.SaveAs2(fileName);

                    ////Otvaranje template-a za pregled
                    doc.Application.Visible = true;
                    doc.Activate();

                    //   BIS.Model.LayoutsModel tmpLayout = (BIS.Model.LayoutsModel)lookupModel[0];
                    BIS.Model.DocumentsModel docmodel = new BIS.Model.DocumentsModel();


                    if (TableName == "ContactPerson")
                    {
                        docmodel.idContPers = IdFieldValue;
                        docmodel.idClient = 0;
                    }
                    if (TableName == "Client")
                    {
                        docmodel.idClient = IdFieldValue;
                        docmodel.idContPers = 0;
                    }

                    docmodel.inOutDocument = 1;
                    docmodel.typeDocument = selmodel.typeDocument;
                    docmodel.fileDocument = sFile;
                    docmodel.dtCreated = DateTime.Now;
                    docmodel.dtModified = DateTime.Now;
                    docmodel.userCreated = Login._user.idUser;
                    docmodel.idLayout = selmodel.idLayout;
                    docmodel.descriptionDocument = "_";

                    docmodel.idEmployee = Login._user.idEmployee;
                    docmodel.idResponsableEmployee = 2; // ?
                    docmodel.idDocumentStatus = 1; // ?
                    docmodel.noteDocument = "_";

                    docmodel.userModified = Login._user.idUser; // ?
                    BIS.Business.DocumentsBUS docbus = new BIS.Business.DocumentsBUS();
                    docbus.Save(docmodel, nameForm,idUser);

                
            }
            catch (Exception ex)
            {
               

                MessageBox.Show(ex.Message);
            }
            finally
            {
                //if (doc != null)
                //{
                //    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                //    object typeMissing = Type.Missing;
                //    doc.Close(ref doNotSaveChanges, ref typeMissing, ref typeMissing);
                //}

                //if (wordApp != null)
                //{
                //    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                //    object typeMissing = Type.Missing;
                //    wordApp.Quit(ref doNotSaveChanges, ref typeMissing, ref typeMissing);
                //}
            }
        }



        public static void ReadTemplateFileAndPrint(Microsoft.Office.Interop.Word.Application wordApp, string TableName, string IdFiledName, int IdFieldValue, 
            DataTable datatable, BIS.Model.LayoutsModel layoutmodel, string tempfolder, string printername, string nameForm, int idUser)
        {
            Cursor.Current = Cursors.WaitCursor;

            wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = null;

            BIS.Model.LayoutsModel selmodel = null;

            try
            {

                selmodel = layoutmodel;
                //selmodel = (BIS.Model.LayoutsModel)lookfrm.selectedRow;

                string allBOokmarks = selmodel.bookmarks;

                string[] bookmarkArray = allBOokmarks.Split(',');
                List<BIS.Model.BookmarkDefModel> bookmarkList = new List<BIS.Model.BookmarkDefModel>();

                BIS.Business.BookmarkDefBUS bdefBUS = new BIS.Business.BookmarkDefBUS();

                foreach (string bina in bookmarkArray)
                {
                    BIS.Model.BookmarkDefModel model = bdefBUS.GetBookmarkByID(Int32.Parse(bina.Trim()));
                    if (model != null)
                    {
                        bookmarkList.Add(model);
                    }
                }

                //SELECT firstname FROM ContactPerson WHERE  idContPers = 3

                //SELECT nameTitle FROM Title WHERE  idTitle = 1

                //SELECT street FROM ContactPersonAddress WHERE  idContPers = 3 and idAddressType = 1

                List<BIS.Model.BookmarkSpecModel> specModelList = new List<BIS.Model.BookmarkSpecModel>();
                foreach (BIS.Model.BookmarkDefModel m in bookmarkList)
                {
                    BIS.Model.BookmarkSpecModel s = new BIS.Model.BookmarkSpecModel();
                    s.field = m.nameBookmark;
                    s.table = m.tableName;

                    if (m.isRelationBmk == false)
                    {
                        object obj;
                        if (m.tableName == "AccDebCre")
                            obj = bdefBUS.CustomBookmarksQuery(m.tableName, m.fieldBookmark, "idContPerson", IdFieldValue, null, -1);
                        else
                            obj = bdefBUS.CustomBookmarksQuery(m.tableName, m.fieldBookmark, IdFiledName, IdFieldValue, null, -1);

                        if (obj != null)
                            s.value = obj.ToString();
                        else
                            s.value = "";
                    }
                    else
                    {
                        if (m.hasType == false)
                        {
                            //object obj = bdefBUS.CustomBookmarksQuery(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, null, -1);
                            object obj = bdefBUS.CustomBookmarksQueryForTypes(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, null, -1, TableName, IdFiledName);

                            if (obj != null)
                                s.value = obj.ToString();
                            else
                                s.value = "";
                        }
                        else
                        {
                            object obj = bdefBUS.CustomBookmarksQuery(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, m.typeFieldName, m.typeFieldValue);

                            if (obj != null)
                                s.value = obj.ToString();
                            else
                                s.value = "";
                        }
                    }
                    specModelList.Add(s);
                }


                string templateFolder = MainForm.TemplatesFolder + "\\";
                string documentFolder = MainForm.DocumentsFolder + "\\";
               // MessageBox.Show(templateFolder + selmodel.nameLayout + ".docx");

                object fileName = templateFolder + selmodel.nameLayout + ".docx";
                object confirmConversions = Type.Missing;
                object readOnly = Type.Missing;
                object addToRecentFiles = Type.Missing;
                object passwordDoc = Type.Missing;
                object passwordTemplate = Type.Missing;
                object revert = Type.Missing;
                object writepwdoc = Type.Missing;
                object writepwTemplate = Type.Missing;
                object format = Type.Missing;
                object encoding = Type.Missing;
                object visible = Type.Missing;
                object openRepair = Type.Missing;
                object docDirection = Type.Missing;
                object notEncoding = Type.Missing;
                object xmlTransform = Type.Missing;
                                            
                doc = wordApp.Documents.Open(ref fileName, ref confirmConversions, ref readOnly, ref addToRecentFiles, ref passwordDoc, ref passwordTemplate, ref revert, ref writepwdoc, ref writepwTemplate, ref format, ref encoding, ref visible, ref openRepair, ref docDirection, ref notEncoding, ref xmlTransform);

                
                
                foreach (BIS.Model.BookmarkSpecModel oBooks in specModelList)
                    ReplaceBookmarkTextNew(doc, oBooks);

                if (datatable != null)
                    ReplaceBookmarkTable(doc, datatable);

                DeleteEmptyBookmarks(doc, specModelList);

                string sFile = CreateDocName(IdFieldValue) + ".docx";
                //fileName = documentFolder + sFile;
                fileName = tempfolder + "\\" + sFile;
                //fileName = templateFolder + "\\" + sFile;


                doc.SaveAs2(fileName);
                System.IO.File.Copy(tempfolder + "\\" + sFile, MainForm.DocumentsFolder + "\\" + sFile, true);
                ////Otvaranje template-a za pregled
                //doc.Application.Visible = true;
                //doc.Activate();

                object copies = "1";
                object pages = "";
                object range = Microsoft.Office.Interop.Word.WdPrintOutRange.wdPrintAllDocument;
                object items = Microsoft.Office.Interop.Word.WdPrintOutItem.wdPrintDocumentContent;
                object pageType = Microsoft.Office.Interop.Word.WdPrintOutPages.wdPrintAllPages;
                object oTrue = true;
                object oFalse = false;
                object missing = Type.Missing;

                wordApp.ActivePrinter = printername;
                doc.PrintOut(
                    ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
                    ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
                    ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);


                object missingValue = Type.Missing;
                doc.Close(ref missingValue, ref missingValue, ref missingValue);
                doc = null;

                while (wordApp.BackgroundPrintingStatus > 0)
                {
                    System.Threading.Thread.Sleep(250);
                }
                
                ////////   BIS.Model.LayoutsModel tmpLayout = (BIS.Model.LayoutsModel)lookupModel[0];
                BIS.Model.DocumentsModel docmodel = new BIS.Model.DocumentsModel();


                if (TableName == "ContactPerson")
                {
                    docmodel.idContPers = IdFieldValue;
                    docmodel.idClient = 0;
                }
                if (TableName == "Client")
                {
                    docmodel.idClient = IdFieldValue;
                    docmodel.idContPers = 0;
                }

                docmodel.inOutDocument = 1;
                docmodel.typeDocument = selmodel.typeDocument;
                docmodel.fileDocument = sFile;
                docmodel.dtCreated = DateTime.Now;
                docmodel.dtModified = DateTime.Now;
                docmodel.userCreated = Login._user.idUser;
                docmodel.idLayout = selmodel.idLayout;
                docmodel.descriptionDocument = "Printed Letter - Open Lines";

                docmodel.idEmployee = Login._user.idEmployee;
                docmodel.idResponsableEmployee = 2; // ?
                docmodel.idDocumentStatus = 1; // ?
                docmodel.noteDocument = "_";

                docmodel.userModified = Login._user.idUser; // ?
                BIS.Business.DocumentsBUS docbus = new BIS.Business.DocumentsBUS();
                docbus.Save(docmodel,nameForm,idUser);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                             
            }
            finally
            {
                if (doc != null)
                {
                    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    object typeMissing = Type.Missing;
                    doc.Close(ref doNotSaveChanges, ref typeMissing, ref typeMissing);
                }

                if (wordApp != null)
                {
                    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    object typeMissing = Type.Missing;
                    wordApp.Quit(ref doNotSaveChanges, ref typeMissing, ref typeMissing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }

            Cursor.Current = Cursors.WaitCursor;
        }


        public static void ReadTemplateFileAndSendEmail(Microsoft.Office.Interop.Word.Application wordApp, 
            string TableName, string IdFiledName, int IdFieldValue, DataTable datatable, BIS.Model.LayoutsModel layoutmodel,
            string email, string tempfolder, string subject, string body_message, string nameForm, int idUser)
        {
            Cursor.Current = Cursors.WaitCursor;
            wordApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = null;

            BIS.Model.LayoutsModel selmodel = null;

            try
            {

                selmodel = layoutmodel;
                //selmodel = (BIS.Model.LayoutsModel)lookfrm.selectedRow;

                string allBOokmarks = selmodel.bookmarks;

                string[] bookmarkArray = allBOokmarks.Split(',');
                List<BIS.Model.BookmarkDefModel> bookmarkList = new List<BIS.Model.BookmarkDefModel>();

                BIS.Business.BookmarkDefBUS bdefBUS = new BIS.Business.BookmarkDefBUS();

                foreach (string bina in bookmarkArray)
                {
                    BIS.Model.BookmarkDefModel model = bdefBUS.GetBookmarkByID(Int32.Parse(bina.Trim()));
                    if (model != null)
                    {
                        bookmarkList.Add(model);
                    }
                }

                //SELECT firstname FROM ContactPerson WHERE  idContPers = 3

                //SELECT nameTitle FROM Title WHERE  idTitle = 1

                //SELECT street FROM ContactPersonAddress WHERE  idContPers = 3 and idAddressType = 1

                List<BIS.Model.BookmarkSpecModel> specModelList = new List<BIS.Model.BookmarkSpecModel>();
                foreach (BIS.Model.BookmarkDefModel m in bookmarkList)
                {
                    BIS.Model.BookmarkSpecModel s = new BIS.Model.BookmarkSpecModel();
                    s.field = m.nameBookmark;
                    s.table = m.tableName;

                    if (m.isRelationBmk == false)
                    {
                        object obj;
                        if (m.tableName == "AccDebCre")
                            obj = bdefBUS.CustomBookmarksQuery(m.tableName, m.fieldBookmark, "idContPerson", IdFieldValue, null, -1);
                        else
                            obj = bdefBUS.CustomBookmarksQuery(m.tableName, m.fieldBookmark, IdFiledName, IdFieldValue, null, -1);

                        if (obj != null)
                            s.value = obj.ToString();
                        else
                            s.value = "";
                    }
                    else
                    {
                        if (m.hasType == false)
                        {
                            //object obj = bdefBUS.CustomBookmarksQuery(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, null, -1);
                            object obj = bdefBUS.CustomBookmarksQueryForTypes(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, null, -1, TableName, IdFiledName);

                            if (obj != null)
                                s.value = obj.ToString();
                            else
                                s.value = "";
                        }
                        else
                        {
                            object obj = bdefBUS.CustomBookmarksQuery(m.relationTableName, m.fieldBookmark, m.relationFieldName, IdFieldValue, m.typeFieldName, m.typeFieldValue);

                            if (obj != null)
                                s.value = obj.ToString();
                            else
                                s.value = "";
                        }
                    }
                    specModelList.Add(s);
                }


                string templateFolder = MainForm.TemplatesFolder + "\\";
                string documentFolder = MainForm.DocumentsFolder + "\\";

                object fileName = templateFolder + selmodel.nameLayout + ".docx";
                object confirmConversions = Type.Missing;
                object readOnly = Type.Missing;
                object addToRecentFiles = Type.Missing;
                object passwordDoc = Type.Missing;
                object passwordTemplate = Type.Missing;
                object revert = Type.Missing;
                object writepwdoc = Type.Missing;
                object writepwTemplate = Type.Missing;
                object format = Type.Missing;
                object encoding = Type.Missing;
                object visible = Type.Missing;
                object openRepair = Type.Missing;
                object docDirection = Type.Missing;
                object notEncoding = Type.Missing;
                object xmlTransform = Type.Missing;

                //wordApp = new Microsoft.Office.Interop.Word.Application();
                doc = wordApp.Documents.Open(ref fileName, ref confirmConversions, ref readOnly, ref addToRecentFiles, ref passwordDoc, ref passwordTemplate, ref revert, ref writepwdoc, ref writepwTemplate, ref format, ref encoding, ref visible, ref openRepair, ref docDirection, ref notEncoding, ref xmlTransform);

                foreach (BIS.Model.BookmarkSpecModel oBooks in specModelList)
                    ReplaceBookmarkTextNew(doc, oBooks);

                if (datatable != null)
                    ReplaceBookmarkTable(doc, datatable);

                DeleteEmptyBookmarks(doc, specModelList);

                string sFile = CreateDocName(IdFieldValue) + ".docx";
                fileName = tempfolder + "\\" + sFile;

                string replacefilename = fileName.ToString();
                replacefilename = replacefilename.Replace(".docx", ".pdf");

                doc.SaveAs2(fileName);
                
                doc.ExportAsFixedFormat(replacefilename, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF,
                        OptimizeFor: Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                        BitmapMissingFonts: true, DocStructureTags: false);

                object missingValue = Type.Missing;
                doc.Close(ref missingValue, ref missingValue, ref missingValue);
                doc = null;


                while (wordApp.BackgroundSavingStatus > 0)
                {
                    System.Threading.Thread.Sleep(250);
                }

                ////Otvaranje template-a za pregled
                //doc.Application.Visible = true;
                //doc.Activate();

                //object copies = "1";
                //object pages = "";
                //object range = Microsoft.Office.Interop.Word.WdPrintOutRange.wdPrintAllDocument;
                //object items = Microsoft.Office.Interop.Word.WdPrintOutItem.wdPrintDocumentContent;
                //object pageType = Microsoft.Office.Interop.Word.WdPrintOutPages.wdPrintAllPages;
                //object oTrue = true;
                //object oFalse = false;
                //object missing = Type.Missing;

                //doc.PrintOut(
                //    ref oTrue, ref oFalse, ref range, ref missing, ref missing, ref missing,
                //    ref items, ref copies, ref pages, ref pageType, ref oFalse, ref oTrue,
                //    ref missing, ref oFalse, ref missing, ref missing, ref missing, ref missing);

                


                //using(System.IO.FileStream fs = System.IO.File.Open(fileName))
                //{
                //    fs.
                //}


                Outlook.Application outlookApp = new Outlook.Application();                

                Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);                
                oMailItem.DeleteAfterSubmit = false;

                // Recipient
                Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(email);
                oRecip.Resolve();

                oMailItem.Subject = subject;
                oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                oMailItem.Body = body_message;

                Outlook.Attachments oAttachs = (Outlook.Attachments)oMailItem.Attachments;
                Outlook.Attachment oAtt = (Outlook.Attachment)oAttachs.Add(replacefilename, Outlook.OlAttachmentType.olByValue);

                oMailItem.Save();

                string locationOnDisk = MainForm.myEmailFolder + "\\" + oMailItem.EntryID + ".msg";
                string docname = oMailItem.EntryID + ".msg";

                if (!System.IO.File.Exists(locationOnDisk))
                    oMailItem.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                
                oMailItem.Send();



                ////////   BIS.Model.LayoutsModel tmpLayout = (BIS.Model.LayoutsModel)lookupModel[0];
                BIS.Model.DocumentsModel docmodel = new BIS.Model.DocumentsModel();

                
                
                if (TableName == "ContactPerson")
                {
                    docmodel.idContPers = IdFieldValue;
                    docmodel.idClient = 0;
                }
                if (TableName == "Client")
                {
                    docmodel.idClient = IdFieldValue;
                    docmodel.idContPers = 0;
                }

                docmodel.inOutDocument = 1;
                docmodel.typeDocument = "EML";
                docmodel.fileDocument = docname;
                docmodel.dtCreated = DateTime.Now;
                docmodel.dtModified = DateTime.Now;
                docmodel.userCreated = Login._user.idUser;
                docmodel.idLayout = selmodel.idLayout;
                docmodel.descriptionDocument = "Emailed Letter - Open Lines";

                docmodel.idEmployee = Login._user.idEmployee;
                docmodel.idResponsableEmployee = 2; // ?
                docmodel.idDocumentStatus = 1; // ?
                docmodel.noteDocument = "_";

                docmodel.userModified = Login._user.idUser; // ?
                BIS.Business.DocumentsBUS docbus = new BIS.Business.DocumentsBUS();
                docbus.Save(docmodel, nameForm,idUser);


            }
            catch (Exception ex)
            {
               

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (doc != null)
                {
                    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    object typeMissing = Type.Missing;
                    doc.Close(ref doNotSaveChanges, ref typeMissing, ref typeMissing);
                }

                if (wordApp != null)
                {
                    object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                    object typeMissing = Type.Missing;
                    wordApp.Quit(ref doNotSaveChanges, ref typeMissing, ref typeMissing);
                }
            }

            Cursor.Current = Cursors.WaitCursor;
        }


        public static void ReplaceBookmarkTextNew(Microsoft.Office.Interop.Word.Document doc, BIS.Model.BookmarkSpecModel oBooks)
        {
            foreach (Microsoft.Office.Interop.Word.Bookmark b in doc.Bookmarks)
            {
                if (b.Name.StartsWith(oBooks.field) == true)
                {
                    Object name = oBooks.field;
                    Microsoft.Office.Interop.Word.Range range = b.Range;
                    range.Text = oBooks.value;
                    object newRange = range;
                    // doc.Bookmarks.Add(oBooks.field, ref newRange);
                }
                else if (b.Name == "datetimebookmark")
                {
                    Object name = oBooks.field;
                    Microsoft.Office.Interop.Word.Range range = b.Range;
                    range.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    object newRange = range;
                }
                else
                {

                }

                //if (b.Name == "table")
                //{
                //    Object name = "table";
                //    Microsoft.Office.Interop.Word.Range range = b.Range;
                //    range.Text = "Ovde stoji tabela";
                //    object newRange = range;
                //}
            }
        }
        private static float InchesToPoints(float fInches)
        {
            return fInches * 72.0f;
        }
        public static void ReplaceBookmarkTable(Microsoft.Office.Interop.Word.Document doc, DataTable dt)
        {
            foreach (Microsoft.Office.Interop.Word.Bookmark b in doc.Bookmarks)
            {
                if (b.Name.ToLower().Contains("table") == true)
                {

                    BookmarkDefBUS bbus = new BookmarkDefBUS();
                    BookmarkTableModel tablemodel = new BookmarkTableModel();

                    string tablename = b.Name.Replace("Table", "");
                    tablemodel = bbus.GetTableByName(tablename);

                    if (tablemodel != null)
                    {
                        //Object name = "table";
                        Microsoft.Office.Interop.Word.Range range = b.Range;
                        range.InsertBefore("Open Lines");
                        range.Font.Name = "Verdana";
                        range.Font.Size = 16;
                        range.PageSetup.LeftMargin = InchesToPoints(tablemodel.leftMargin);
                        range.PageSetup.RightMargin = InchesToPoints(tablemodel.rightMargin);
                        range.InsertParagraphAfter();
                        range.InsertParagraphAfter();
                        range.SetRange(range.End, range.End);


                        List<BookmarkTableFieldsModel> fieldsmodel = new List<BookmarkTableFieldsModel>();
                        fieldsmodel = bbus.GetTableBookmarksFields(tablemodel.idTable);
                        if (fieldsmodel != null)
                        {
                            object missing = Type.Missing;
                            // + 2 because of last total row, +1 would be normally with header
                            range.Tables.Add(range, dt.Rows.Count + 2, fieldsmodel.Count, ref missing, ref missing);

                            Microsoft.Office.Interop.Word.Table tbl = range.Tables[1];
                            tbl.Range.Font.Size = tablemodel.fontSizeFields;
                            //tbl.LeftPadding = InchesToPoints(0);
                            //tbl.RightPadding = InchesToPoints(0);
                            tbl.Columns.DistributeWidth();

                            //object stylename = "Table Professional";

                            Object stylename = Microsoft.Office.Interop.Word.WdBuiltinStyle.wdStyleTableLightGrid;
                            //object stylename = "Grid Table 1 Light";
                            tbl.set_Style(ref stylename);

                            int numCol = 1;
                            int numRows = 1;
                            //////foreach (DataColumn col in dt.Columns)
                            //////{
                            //////    tbl.Cell(numRows, numCol).Range.Text = col.ColumnName;
                            //////    //tbl.Cell(1, 2).Range.Text = "Value";
                            //////    numCol++;
                            //////}

                            foreach (BookmarkTableFieldsModel m in fieldsmodel)
                            {
                                tbl.Cell(numRows, numCol).Range.Text = m.displayNameField;
                                //tbl.Columns[numCol].SetWidth(9f, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustNone);
                                tbl.Columns[numCol].PreferredWidth = m.widthField;
                                numCol++;
                            }

                            numCol = 1;
                            numRows++;
                            decimal[] total = new decimal[fieldsmodel.Count];

                            foreach (DataRow dr in dt.Rows)
                            {
                                foreach (BookmarkTableFieldsModel col in fieldsmodel)
                                {
                                    //foramt date time
                                    string celltext = "";
                                    if (dt.Columns[col.nameField].DataType == typeof(DateTime))
                                    {
                                        celltext = DateTime.Parse(dr[col.nameField].ToString()).ToString("dd-MM-yyyy");
                                    }
                                    else if(dt.Columns[col.nameField].DataType == typeof(decimal))
                                    {
                                        celltext = Math.Round(Decimal.Parse(dr[col.nameField].ToString()),2).ToString();
                                    }
                                    else
                                    {
                                        celltext = dr[col.nameField].ToString();
                                    }

                                    // remember total values
                                    if (col.isTotal == true && col.visible == true)
                                    {
                                        decimal cellvalue = Math.Round(Decimal.Parse(dr[col.nameField].ToString()), 2);
                                        total[numCol-1] += cellvalue;
                                    }
                                    else
                                    {
                                        total[numCol-1] += 0;
                                    }

                                    tbl.Cell(numRows, numCol).Range.Text = celltext;
                                    tbl.Cell(numRows, numCol).LeftPadding = InchesToPoints(0);
                                    tbl.Cell(numRows, numCol).RightPadding = InchesToPoints(0);

                                    if (dt.Columns[col.nameField].DataType == typeof(decimal) || dt.Columns[col.nameField].DataType == typeof(int))
                                        tbl.Cell(numRows, numCol).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

                                    numCol++;
                                }
                                numCol = 1;
                                numRows++;

                            }

                            // Count Total if theres any
                            //////numRows++;
                            //////for (int i = 0; i < total.Length; i++)
                            //////{
                            //////    if (total[i] != 0)
                            //////    {
                            //////        tbl.Cell(numRows, i + 1).Range.Text = total[i].ToString();
                            //////    }
                            //////}
                            numCol = 1;
                            //numRows++;
                            foreach(BookmarkTableFieldsModel col in fieldsmodel)
                            {
                                if(col.visible == true && col.isTotal)
                                {
                                    tbl.Cell(numRows, numCol).Range.Text = total[numCol - 1].ToString();
                                    tbl.Cell(numRows, numCol).LeftPadding = InchesToPoints(0);
                                    tbl.Cell(numRows, numCol).RightPadding = InchesToPoints(0);
                                    tbl.Cell(numRows, numCol).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                                    tbl.Cell(numRows, numCol).Range.Bold = 1;
                                    
                                }
                                numCol++;
                            }
                            //tbl.Cell(1, 1).Range.Text = "Document Property";
                            //tbl.Cell(1, 2).Range.Text = "Value";

                            //tbl.Cell(2, 1).Range.Text = "Subject";
                            //tbl.Cell(2, 2).Range.Text = "value";

                            //tbl.Cell(3, 1).Range.Text = "Author";
                            //tbl.Cell(3, 2).Range.Text = "Value";

                        }
                        object newRange = range;
                    }
                }
            }
        }
        public static void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, BIS.Model.BookmarkSpecModel oBooks)
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
        public static void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, string text)
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
        public static void DeleteEmptyBookmarks(Microsoft.Office.Interop.Word.Document doc, List<BIS.Model.BookmarkSpecModel> existingBookmarks)
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

        private static string CreateDocName(Int32 iID)
        {
            //return iID.ToString("00000000") + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString("00000");
            return iID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
        }
        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


    }
}
