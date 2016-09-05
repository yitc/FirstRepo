using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.FormatProviders;
using Telerik.WinForms.Documents.Model;

namespace GUI
{
    public partial class frmMultimedia :frmTemplate
    {        
        MultimediaModel multimedia;
        MultimediaServerCredentialsModel serverLoginModel;
        MultimediaServerModel serverModel;

        BindingList<PhotosModel> photoList;


        private string selectedPicturePath = "";

        public frmMultimedia()
        {
            serverLoginModel = null;
            serverModel = null;

            InitializeComponent();
            multimedia = new MultimediaModel();
            btnSave.Click += SaveEvent;            
            
            photoList = new BindingList<PhotosModel>();
            gridPhotos.DataSource = photoList;

            // ako je insert tabPhotos ne treba
            pageMultimedia.Pages.Remove(tabPhotos);
            btnAddPicture.Enabled = false;
            btnUploadPictures.Enabled = false;
        }

        public frmMultimedia(IModel model)
        {         
            
            InitializeComponent();
            multimedia = (MultimediaModel)model;

           

            btnSave.Click += UpdateEvent;
            photoList = new BindingList<PhotosModel>();

            MultimediaBUS mbus = new MultimediaBUS();
            List<IModel> pmod = mbus.GetAllPhotosByMultimedia(multimedia.idMultimedia);
            btnChooseServer.Enabled = false;

            MultimediaBUS serverBus = new MultimediaBUS();
            serverModel = serverBus.GetMultimediaServersByID(multimedia.idServer);
            serverLoginModel = serverBus.GetMultimediaCredentials(multimedia.idServer);

            if (pmod != null)
            {               
                if (serverModel != null && serverLoginModel != null)
                {                    
                    Cursor.Current = Cursors.WaitCursor;
                    foreach (PhotosModel m in pmod)
                    {
                        Image img = DownloadPictureFromFTP(serverModel.path, serverModel.folder, m.namePhotos,
                           serverLoginModel.username, serverLoginModel.password);

                        if (img != null)
                            m.imagePhoto = img;
                 
                        photoList.Add(m);
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            
            gridPhotos.DataSource = photoList;

            btnAddPicture.Enabled = true;
            btnUploadPictures.Enabled = true;

            ReadModel();
                       
        }

        private void frmMultimedia_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Name.Replace("frm", "")) != null)
                    formName = formName + " " + resxSet.GetString(this.Name.Replace("frm", ""));
                else
                    formName = formName + " " + this.Name.Replace("frm", "");
            }

            this.Text = formName;

            

            setTranslation();

            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;

            //Ribbon bar
            this.richTextEditorRibbonBar3.MinimizeButton = false;
            this.richTextEditorRibbonBar3.MaximizeButton = false;
            this.richTextEditorRibbonBar3.CloseButton = false;
            this.richTextEditorRibbonBar3.RibbonBarElement.Children[1].Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            this.richTextEditorRibbonBar3.RibbonBarElement.Children[0].Visibility = Telerik.WinControls.ElementVisibility.Collapsed;


            SetGrid();

            RadDocument radDoc = ImportHtml(multimedia.description);
            rtbDescription.Document = radDoc;
            
                        
        }

        public void ReadModel()
        {
            txtArticleID.Text = multimedia.idArticle;
            txtArticleName.Text = multimedia.nameArtical;
            txtClient.Text = multimedia.nameClient;
            txtPeriod.Text = multimedia.namePeriod;
            rtbDescription.Text = multimedia.description;

            if(serverModel != null)
            {
                txtPath.Text = serverModel.path;
                txtFolder.Text = serverModel.folder;
            }

        }
        private void SetGrid()
        {
            if (gridPhotos.Columns.Count > 0)
            {
                gridPhotos.Columns["idPhotos"].IsVisible = false;
                gridPhotos.Columns["idMultimedia"].IsVisible = false;
                //gridPhotos.Columns["isActive"].IsVisible = false;
                gridPhotos.Columns["idUserCreator"].IsVisible = false;
                gridPhotos.Columns["dtUserCreator"].IsVisible = false;
                gridPhotos.Columns["idUserModified"].IsVisible = false;
                gridPhotos.Columns["dtUserModified"].IsVisible = false;
                gridPhotos.Columns["descMultimedia"].IsVisible = false;

                gridPhotos.Columns["namePhotos"].Width = 240;
                gridPhotos.Columns["imagePhoto"].Width = 240;
                gridPhotos.Columns["imagePhoto"].ImageLayout = ImageLayout.Zoom;
                gridPhotos.TableElement.RowHeight = 70;

                gridPhotos.Columns["namePhotos"].ReadOnly = true;
                gridPhotos.Columns["imagePhoto"].ReadOnly = true;
            }          
        }
        //private void SetGrid()
        //{
        //    gridPhotos.Columns["idPhotos"].IsVisible = false;
        //    gridPhotos.Columns["idMultimedia"].IsVisible = false;
        //    //gridPhotos.Columns["isActive"].IsVisible = false;
        //    gridPhotos.Columns["idUserCreator"].IsVisible = false;
        //    gridPhotos.Columns["dtUserCreator"].IsVisible = false;
        //    gridPhotos.Columns["idUserModified"].IsVisible = false;
        //    gridPhotos.Columns["dtUserModified"].IsVisible = false;
        //    gridPhotos.Columns["descMultimedia"].IsVisible = false;

        //    gridPhotos.Columns["namePhotos"].Width = 240;
        //    gridPhotos.Columns["imagePhoto"].Width = 240;
        //    gridPhotos.Columns["imagePhoto"].ImageLayout = ImageLayout.Zoom;
        //    gridPhotos.TableElement.RowHeight = 70;

        //    gridPhotos.Columns["namePhotos"].ReadOnly = true;
        //    gridPhotos.Columns["imagePhoto"].ReadOnly = true;
                        
       // }
        private void btnChooseServer_Click_1(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();
            MultimediaBUS bus = new MultimediaBUS();

            gm1 = bus.GetAllMultimediaServers();

            var dlgSave = new GridLookupForm(gm1, "Servers");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                MultimediaServerModel genm1 = new MultimediaServerModel();
                genm1 = (MultimediaServerModel)dlgSave.selectedRow;
                //set textbox
                txtPath.Text = genm1.path;
                txtFolder.Text = genm1.folder;
                serverModel = genm1;
                serverLoginModel = bus.GetMultimediaCredentials(genm1.idServer);

                btnAddPicture.Enabled = true;
                btnUploadPictures.Enabled = true;
            }
        }

        

        private void SaveEvent(object sender, EventArgs e)
        {

            
            MultimediaBUS mbus = new MultimediaBUS();
            

            multimedia.description = rtbDescription.Text;
            multimedia.idUserCreated = Login._user.idUser;
            multimedia.dtUserCreated = DateTime.Now;
           
            if(serverModel != null)
                multimedia.idServer = serverModel.idServer;

            if(multimedia.idArticle == String.Empty)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select article.");
                return;
            }

            if (multimedia.idClient == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select client.");
                return;
            }
            if (multimedia.idPeriod == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select period.");
                return;
            }

            if (serverModel == null)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select server.");
                return;
            }

            MultimediaModel ifexist = new MultimediaModel();
            ifexist = mbus.GetMultimediaByArticleClientPeriod(multimedia.idArticle, multimedia.idClient, multimedia.idPeriod, multimedia.idMultimedia);
            if(ifexist != null)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You cannot save because multimedia with that article, client and period already exist.");
                return;
            }

            exportMultimediaStyle(); //pozivanje metode za export stilova u bazu


            int retval = mbus.SaveAndReturnID(multimedia, this.Name, Login._user.idUser);

            

            if (retval != 0)
            {
                multimedia.idMultimedia = retval;

                

                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have successfully insert data!");
                
                btnSave.Click -= SaveEvent;
                btnSave.Click += UpdateEvent;
                if (pageMultimedia.Pages.Contains(tabPhotos) == false)
                {
                    pageMultimedia.Pages.Add(tabPhotos);
                    pageMultimedia.SelectedPage = tabPhotos;
                }
                btnChooseServer.Enabled = false;
                SetGrid();
                
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Something went wrong with inserting!");
            }
        }

        private void UpdateEvent(object sender, EventArgs e)
        {
            //if (serverModel == null)
            //{
            //    translateRadMessageBox tr = new translateRadMessageBox();
            //    tr.translateAllMessageBox("You have to select server!");
            //    return;
            //}
                      

            MultimediaBUS mbus = new MultimediaBUS();

            //RadDocument radDoc = ImportHtml(multimedia.description);
            //rtbDescription.Document = radDoc;

            exportMultimediaStyle();
            RadDocument radDoc = ImportHtml(multimedia.description);
            rtbDescription.Document = radDoc;
            //multimedia.description = rtbDescription.Text;            
            multimedia.idUserModified = Login._user.idUser;
            multimedia.dtUserModified = DateTime.Now;


            MultimediaModel ifexist = new MultimediaModel();
            ifexist = mbus.GetMultimediaByArticleClientPeriod(multimedia.idArticle, multimedia.idClient, multimedia.idPeriod, multimedia.idMultimedia);
            if (ifexist != null)
            {
                

                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You cannot save because multimedia with that article, client and period already exist.");
                return;
            }


            if (mbus.Update(multimedia, this.Name, Login._user.idUser) == true)
            {

                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have successfully insert data!");
                //this.Close();
            }


            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Something went wrong with inserting!");
            }
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(tabMultimedia.Text) != null)
                    tabMultimedia.Text = resxSet.GetString(tabMultimedia.Text);
                if (resxSet.GetString(tabPhotos.Text) != null)
                    tabPhotos.Text = resxSet.GetString(tabPhotos.Text);

                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);
                if (resxSet.GetString(lblArticleName.Text) != null)
                    lblArticleName.Text = resxSet.GetString(lblArticleName.Text);
                if (resxSet.GetString(lvlArticleID.Text) != null)
                    lvlArticleID.Text = resxSet.GetString(lvlArticleID.Text);

                if (resxSet.GetString(lblPeriod.Text) != null)
                    lblPeriod.Text = resxSet.GetString(lblPeriod.Text);
                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
            }
        }



        private void btnArticalID_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new ArticalBUS().GetAllArticals();
            var dlgSave = new GridLookupForm(gm1, "Articals");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalModel genm1 = new ArticalModel();
                genm1 = (ArticalModel)dlgSave.selectedRow;
                //set textbox
                txtArticleID.Text = genm1.codeArtical;
                multimedia.idArticle= genm1.codeArtical;
                txtArticleName.Text = genm1.nameArtical;
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new ClientBUS().GetAllClients(Login._user.lngUser);
            var dlgSave = new GridLookupForm(gm1, "Client");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ClientModel genm1 = new ClientModel();
                genm1 = (ClientModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = genm1.nameClient;
                multimedia.idClient = genm1.idClient;
                multimedia.nameClient = genm1.nameClient;
            }
        }



        private void btnPerid_Click(object sender, EventArgs e)
        {
            List<IModel> gm1 = new List<IModel>();

            gm1 = new MultimediaBUS().GetAllPeriods();
            var dlgSave = new GridLookupForm(gm1, "Period");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                PeriodModel genm1 = new PeriodModel();
                genm1 = (PeriodModel)dlgSave.selectedRow;
                //set textbox
                txtPeriod.Text = genm1.descPeriod;
                multimedia.idPeriod = genm1.idPeriod;
                multimedia.namePeriod = genm1.descPeriod;
            }
        }
        

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select Image to Upload";
            fileDialog.InitialDirectory = "C:";
            fileDialog.FileName = "";
            fileDialog.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|BITMAPS|*.bmp";

            DialogResult dr = fileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtSelectedPicture.Text = fileDialog.SafeFileName;
                picSelectedPicture.ImageLocation = fileDialog.FileName;
                selectedPicturePath = fileDialog.FileName;
            }
        }
        
        public FtpStatusCode ftp_UploadFile(string ftpaddress, string folder, string username, string password, string filetoUpload, string filenameToSave)
        {
            FtpStatusCode returnVal = FtpStatusCode.FileActionAborted;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpaddress + folder + "/" + filenameToSave);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(username, password);                
                byte[] fileContents = File.ReadAllBytes(filetoUpload);                
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                returnVal = response.StatusCode;
                //RadMessageBox.Show("Upload File Complete, status {0}", response.StatusDescription);

                response.Close();


            }
            catch(Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }

            return returnVal;
        }

        public FtpStatusCode ftp_DeleteFile(string ftpaddress, string folder, string username, string password, string filetoDelete)
        {
            FtpStatusCode returnVal = FtpStatusCode.FileActionAborted;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpaddress + folder + "/" + filetoDelete);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                request.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    returnVal = response.StatusCode;                    
                }
            }
            catch (WebException ex)
            {
                
            }
            catch (Exception ex)
            {

            }

            return returnVal;
        }

        public bool ftp_CheckIfFileExist(string ftpaddress, string folder, string filenameToSave, string username, string password)
        {
            bool retval = true;
            
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftpaddress + folder + "/" + filenameToSave);
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.GetFileSize;

                using(FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {

                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    retval = false;
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

            return retval;
        }

        private Image DownloadPictureFromFTP(string ftpaddress, string folder, string filenameToRead, string username, string password)
        {
            Image returnImage = null;

           

            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftpaddress + folder + "/" + filenameToRead);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UseBinary = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                
                    using (var stream = response.GetResponseStream())
                    {
                        returnImage = Image.FromStream(stream);
                        //using (var img = Image.FromStream(stream))
                        //{                            
                        //}
                    }
                }                            
            }
            catch (WebException ex)
            {
                FtpWebResponse exresponse = (FtpWebResponse)ex.Response;
                if (exresponse.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    returnImage = null;
                }
            }
            catch (Exception ex)
            {

            }


            return returnImage;
        }
        private void btnUploadPictures_Click(object sender, EventArgs e)
        {
            MultimediaBUS mbus = new MultimediaBUS();

            string uploadStatuses = "";

            Cursor.Current = Cursors.WaitCursor;

            if (selectedPicturePath != "")
            {
                bool exist = ftp_CheckIfFileExist(serverModel.path, serverModel.folder,txtSelectedPicture.Text,
                    serverLoginModel.username, serverLoginModel.password);

                //if(exist == true)
                //{
                //    translateRadMessageBox tr = new translateRadMessageBox();
                //    tr.translateAllMessageBox("File: " + txtSelectedPicture.Text + " already exist on server folder.");
                //    return;
                //}

                FtpStatusCode statusCode = ftp_UploadFile(serverModel.path, serverModel.folder, serverLoginModel.username,
                    serverLoginModel.password, selectedPicturePath, txtSelectedPicture.Text);

                if (statusCode == FtpStatusCode.ClosingData)
                {
                    RadMessageBox.Show("File succesfully uploaded.");

                    PhotosModel photo = new PhotosModel();

                    photo.namePhotos = txtSelectedPicture.Text;
                    photo.imagePhoto = picSelectedPicture.Image;
                    photo.idMultimedia = multimedia.idMultimedia;                    
                    photo.isActive = chkIsActive.Checked;
                    photo.idUserCreator = Login._user.idUser;
                    photo.dtUserCreator = DateTime.Now;
                    photo.idUserModified = Login._user.idUser;
                    photo.dtUserModified = DateTime.Now;

                    photo.idPhotos = mbus.SavePhotosAndReturnID(photo, this.Name, Login._user.idUser);

                    photoList.Add(photo);

                    selectedPicturePath = "";
                    txtSelectedPicture.Text = "";
                    picSelectedPicture.Image = null;

                }

                uploadStatuses += "\n";

            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to choose picture for upload.");
            }
            
            Cursor.Current = Cursors.Default;            
        }

        private void gridPhotos_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            
        }

        private void gridPhotos_ValueChanged(object sender, EventArgs e)
        {
            if (this.gridPhotos.ActiveEditor is RadCheckBoxEditor)
            {
                int id = (int)gridPhotos.CurrentRow.Cells["idPhotos"].Value;
                bool chechstate = Convert.ToBoolean(gridPhotos.ActiveEditor.Value);
                //if( == CheckedMode.)

                MultimediaBUS bus = new MultimediaBUS();
                bus.UpdatePhotosIsActive(chechstate, id, this.Name, Login._user.idUser);
                
            }
        }

        private void gridPhotos_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {

                     
        }

        private void gridPhotos_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            try
            {
                if (gridPhotos.CurrentRow.Cells["namePhotos"].Value != null && gridPhotos.CurrentRow.Cells["idPhotos"].Value != null)
                {
                    string photoName = gridPhotos.CurrentRow.Cells["namePhotos"].Value.ToString();
                    int id = (int)gridPhotos.CurrentRow.Cells["idPhotos"].Value;

                    translateRadMessageBox tr = new translateRadMessageBox();
                    DialogResult dr = tr.translateAllMessageBoxDialog("Delete pircture " + photoName + " from server ??", "Delete");
                    if (dr == DialogResult.Cancel || dr == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    else
                    {                                                                                                                                        
                        Cursor.Current = Cursors.WaitCursor;

                        FtpStatusCode statusCode = ftp_DeleteFile(serverModel.path, serverModel.folder, serverLoginModel.username,
                            serverLoginModel.password, photoName);

                        if (statusCode == FtpStatusCode.FileActionOK)
                        {
                            
                        }

                        MultimediaBUS bus = new MultimediaBUS();
                        bus.DeletePhoto(id,this.Name, Login._user.idUser);

                        Cursor.Current = Cursors.Default;                     
                    }
                }
            }
            catch(Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }

        private void gridPhotos_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            //if (e.Parent == this.gridPhotos.MasterTemplate)
            //{
            //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //    {
            //        if (resxSet.GetString("Total") != null)
            //            e.FormatString = String.Format(resxSet.GetString("Total") + " " + e.Value, e.Value);
            //        else
            //            e.FormatString = String.Format("Total " + e.Value, e.Value);
            //    }
            //}
        }
        
        private void exportMultimediaStyle()
        {
            rtbDescription.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbDescription.Document.Selection);
            // HtmlFormatProvider HtmlFormatProvider = new HtmlFormatProvider();
            RadDocument fragmentDocument = fragment.ToDocument();

            HtmlFormatProvider htmlFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settings = new HtmlExportSettings();

            settings.DocumentExportLevel = DocumentExportLevel.Document;
            settings.ExportStyleMetadata = true;
            settings.ExportLocalOrStyleValueSource = true;
            settings.StylesExportMode = StylesExportMode.Classes;
            settings.StyleRepositoryExportMode = StyleRepositoryExportMode.ExportStylesAsCssClasses;
            settings.DocumentExportLevel = DocumentExportLevel.Fragment;
            settings.StylesExportMode = StylesExportMode.Inline;
            htmlFormatProvider.ExportSettings = settings;

            string htmlString;
            htmlString = ExportToHTML(rtbDescription.Document);

            htmlString = htmlFormatProvider.Export(fragmentDocument);

            multimedia.description = htmlString;
        }

        public string ExportToHTML(RadDocument document)
        {
            HtmlFormatProvider HtmlFormatProvider = new HtmlFormatProvider();
            return HtmlFormatProvider.Export(document);

        }

        public RadDocument ImportHtmlStyle(string content)
        {

            HtmlFormatProvider provider = new HtmlFormatProvider();
            return provider.Import(content);
        }

        public RadDocument ImportHtml(string htmlString)
        {
            rtbDescription.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbDescription.Document.Selection);
            RadDocument fragmentDocument = fragment.ToDocument();

            RadDocument document = null;
            HtmlFormatProvider provider = new HtmlFormatProvider();
            HtmlImportSettings settings = new HtmlImportSettings();
            settings.UseDefaultStylesheetForFontProperties = true;

            provider.ImportSettings = settings;


            document = provider.Import(htmlString);


            return document;
            //return provider.Import(aaa);
        }

        private void ImportMultimediaStyle()
        {
            rtbDescription.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbDescription.Document.Selection);
            HtmlFormatProvider HtmlFormatProvider = new HtmlFormatProvider();
            RadDocument fragmentDocument = fragment.ToDocument();
            HtmlFormatProvider htmlFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlImportSettings settings = new HtmlImportSettings();

            htmlFormatProvider.ImportSettings = settings;

            //string import;
            RadDocument import = ImportHtmlStyle(multimedia.description);

            //  import = htmlFormatProvider.Import(fragmentDocument);
            rtbDescription.Document = import;
        }
       
    }
    
}
