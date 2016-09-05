using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.Model;
using Telerik.WinForms.RichTextEditor;
using Telerik.WinForms.RichTextEditor.RichTextBoxUI;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using Telerik.WinForms.Controls;
using Telerik.WinForms.Documents;
using Telerik.WinForms.Documents.FormatProviders;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.FormatProviders.Xaml;
using Telerik.WinForms.Documents.Layout;
using Telerik.WinForms.Documents.RichTextBoxCommands;
using Telerik.WinForms.Documents.Selection;


namespace Email
{
    public partial class NewEmailForm : Telerik.WinControls.UI.RadForm
    {
        private string sConnStr = "";
        private string sContnt = "", sSender = "", sSmtpClient = "", sNetworkCredential = "";
        private Int32 iPort = -1, iIDcpr = -1, iIDusr = -1, iIDcli = 0;

        public NewEmailForm()
        {
            InitializeComponent();

            Initialize();

            //mailRichTextEditor.Document = rteContnt;
            //    sConnStr = @"Data Source=SECOMSERVER\SQLEXPRESS;Initial Catalog=NeuCon;Persist Security Info=True;User ID=sa;Password=Yict#sys01#;MultipleActiveResultSets=True;Application Name=EntityFramework";
            sConnStr = @"Data Source=109.122.102.222;Initial Catalog=NeuCon;Persist Security Info=True;User ID=sa;Password=Yict#sys01#;MultipleActiveResultSets=True;Application Name=EntityFramework";
            iIDcpr = 11111; iIDusr = 28;
            toTextBoxControl.Text = toTextBoxControl.Text;
            ccTextBoxControl.Text = "";
            subjectTextBoxControl.Text = subjectTextBoxControl.Text;

            sSender = "sasa.petrovic@seriecom.nl"; sSmtpClient = "smtp.xs4all.nl"; sNetworkCredential = "se057;bizzv&01";
            iPort = 25;

        }
        //(25, "se057;bizzv&01", "smtp.xs4all.nl", "sasa.petrovic@seriecom.nl", ...)
        public NewEmailForm(RadDocument rteContnt, string to, string cc, string subject)
        {
            InitializeComponent();

            Initialize();

            mailRichTextEditor.Document = rteContnt;
            toTextBoxControl.Text = to;
            ccTextBoxControl.Text = cc;
            subjectTextBoxControl.Text = subject;
        }

        public NewEmailForm(string uContnt, string uConnStr, Int32 uIDusr,Int32 uIDcli, Int32 uIDcpr, Int32 uPort, string uNetworkCredential, string uSmtpClient, string uSender, string to, string cc, string subject)
        {
            InitializeComponent();

            Initialize();

            sContnt = uContnt;

            sConnStr = uConnStr;
            iIDusr = uIDusr;
            iIDcpr = uIDcpr;
            iIDcli = uIDcli;
            sSender = uSender;
            sSmtpClient = uSmtpClient;
            iPort = uPort;
            sNetworkCredential = uNetworkCredential;

            toTextBoxControl.Text = to;
            ccTextBoxControl.Text = cc;
            subjectTextBoxControl.Text = subject;
        }

        private void Initialize()
        {
            //enable themeing
            (this.FormBehavior as RadRibbonFormBehavior).AllowTheming = false;

            //preselect tab
            (richTextEditorRibbonBar1.CommandTabs[0] as RibbonTab).IsSelected = true;

            //adjust backstageview
            richTextEditorRibbonBar1.BackstageControl.Items.Clear();
            richTextEditorRibbonBar1.BackstageControl.BackstageElement.ItemsPanelElement.BackButtonElement.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem() { Text = "Info" });
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem() { Text = "Save As" });
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem() { Text = "Save Attachments", Enabled = false });
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem() { Text = "Print" });
            richTextEditorRibbonBar1.BackstageControl.Items.Add(new BackstageTabItem() { Text = "Close" });
        }

        string sMailBody = "";

        Int32 iPic = 1;
        List<System.IO.Stream> lstPS = new List<System.IO.Stream>();
        LinkedResource pics = null;
        AlternateView htmlView = null;//new AlternateView("");

        private void radButton6_Click(object sender, EventArgs e)
        {
            //this.backstageView = new RadRibbonBarBackstageView();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(sSmtpClient);

                mail.From = new MailAddress(sSender);
                // mail.From.DisplayName.ToString = "Sakica 64" ;
                mail.To.Add(toTextBoxControl.Text);
                if (ccTextBoxControl.Text.Trim() != "")
                    mail.CC.Add(ccTextBoxControl.Text);
                mail.Subject = subjectTextBoxControl.Text;

                //mail.Body = mailRichTextEditor.Text;

                mailRichTextEditor.Commands.SelectAllCommand.Execute();
                DocumentFragment fragment = new DocumentFragment(mailRichTextEditor.Document.Selection);
                RadDocument fragmentDocument = fragment.ToDocument();

                //HtmlFormatProvider htmlFormatProvider = new HtmlFormatProvider();
                HtmlFormatProvider htmlFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
                HtmlExportSettings settings = new HtmlExportSettings();
                //htmlFormatProvider.ExportSettings.DocumentExportLevel = DocumentExportLevel.Fragment;
                settings.DocumentExportLevel = DocumentExportLevel.Document;
                settings.ExportStyleMetadata = true;
                settings.ExportLocalOrStyleValueSource = true;
                settings.StylesExportMode = StylesExportMode.Classes;// Inline;
                settings.StyleRepositoryExportMode = StyleRepositoryExportMode.ExportStylesAsCssClasses;
                //settings.StyleRepositoryExportMode = StyleRepositoryExportMode.DontExportStyles;
                settings.ImageExportMode = Telerik.WinForms.Documents.FormatProviders.Html.ImageExportMode.AutomaticInline;
                //settings.ImageExportMode = Telerik.WinForms.Documents.FormatProviders.Html.ImageExportMode.UriSource;
                //settings.ImageExportMode = Telerik.WinForms.Documents.FormatProviders.Html.ImageExportMode.ImageExportingEvent;
                //settings.ImageExporting += (s, e1) =>
                //{
                //    e1.Src = "cid:pict_" + iPic.ToString();// CreatePicName(e1.Image.Extension);
                //    ////lstPS.Add(e1.Image.ImageSource.StreamSource);

                //    //pics = new LinkedResource(e1.Image.ImageSource.StreamSource);
                //    //pics.ContentId = "pict_" + (iPic++).ToString();
                //    //if (htmlView == null)
                //    //    htmlView = AlternateView.CreateAlternateViewFromString(sMailBody, null, "text/html");

                //    //htmlView.LinkedResources.Add(pics);
                //};

                htmlFormatProvider.ExportSettings = settings;

                mail.IsBodyHtml = true;

                sMailBody = htmlFormatProvider.Export(fragmentDocument);//.Replace('\"', '"');

                //mail.AlternateViews.Add(htmlView);
                mail.Body = sMailBody; //ExportCustomAnnotationsToHtml(mailRichTextEditor.Document);

                SmtpServer.Port = iPort;
                SmtpServer.Credentials = new System.Net.NetworkCredential(sNetworkCredential.Split(';')[0], sNetworkCredential.Split(';')[1]);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                RadMessageBox.Show("Mail successfully sent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            RadButton clickedButton = sender as RadButton;
            if (clickedButton != null)
            {
                string sDest = AppDomain.CurrentDomain.BaseDirectory + "Documents\\";
                string sFile = CreateDocName() + ".html";
                System.IO.File.WriteAllText(sDest + sFile, sMailBody);

                SaveDocumentDB(sFile);
            }
        }

        private RichTextBox rtbData = null;

        private void NewEmailForm_Load(object sender, EventArgs e)
        {
            mailRichTextEditor.Select();
            rtbData = new RichTextBox();
        }

        private string CreatePicName(string sType)
        {
            return "img_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString() + "." + sType;
        }

        private string CreateDocName()
        {
            if (iIDcpr != 0)  // ako je contact osoba
            {
                
                return iIDcpr.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
            }
            else      // ako je client
            {
                return iIDcli.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
            }
        }

        private void OnImageExporting(object sender, Telerik.WinForms.Documents.FormatProviders.Html.ImageExportingEventArgs e)
        {
            string sPath = e.Image.UriSource.AbsoluteUri;
        }

        private void SaveDocumentDB(string sDocName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = sConnStr;

            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlCommand command = conn.CreateCommand();
            string dtsave = DateTime.Now.ToString();

            //  dtc,'" + "'dtsave'"    filedoc, dtc,  cus, idlyt     , '" + sDocName + "', '@dtsave ',  " + iIDusr.ToString() + ", 46
            //     command.CommandText = "insert into documents (idcpr, inoutdoc, iddct, filedoc, dtc, cus, idlyt) values (" + iIDcpr.ToString() + ", 1, 'HTML', '" + sDocName + "', '" + DateTime.Now.ToString() + "', " + iIDusr.ToString() + ", 46)";
            //command.CommandText = "insert into documents (idcpr, inoutdoc, iddct, filedoc, dtc, cus, idlyt) values (" + iIDcpr.ToString() + ", 1, 'HTML', '" + sDocName + "',getdate(), " + iIDusr.ToString() + ", 46)";

            command.CommandText = "INSERT INTO documents (idContPers, inOutDocument, typeDocument, fileDocument, dtCreated, userCreated, idLayout, idClient, userModified) VALUES (" + iIDcpr.ToString() + ", 1, 'EML', '" + sDocName + "', GETDATE(), " + iIDusr.ToString() + ", 0, " + iIDcli.ToString() + ", " + iIDusr.ToString() + ")";

            try
            {
                command.ExecuteNonQuery();
                RadMessageBox.Show("Successfully saved document");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error saving document to Database.\nMessage:" + e.Message, "Error saving", MessageBoxButtons.OK);
            }

            if (conn.State == ConnectionState.Open)
                conn.Close();

            this.Close();


        }

        private void EmbedImages()
        {
            //create the mail message
            MailMessage mail = new MailMessage();

            //set the addresses
            mail.From = new MailAddress("me@mycompany.com");
            mail.To.Add("you@yourcompany.com");

            //set the content
            mail.Subject = "This is an email";

            //first we create the Plain Text part
            AlternateView plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", null, "text/plain");

            //then we create the Html part
            //to embed images, we need to use the prefix 'cid' in the img src value
            //the cid value will map to the Content-Id of a Linked resource.
            //thus <img src='cid:companylogo'> will map to a LinkedResource with a ContentId of 'companylogo'
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString("Here is an embedded image.<img src=cid:companylogo>", null, "text/html");

            //create the LinkedResource (embedded image)
            LinkedResource logo = new LinkedResource("c:\\temp\\logo.gif");
            logo.ContentId = "companylogo";
            //add the LinkedResource to the appropriate view
            htmlView.LinkedResources.Add(logo);

            //add the views
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);


            //send the message
            SmtpClient smtp = new SmtpClient("127.0.0.1"); //specify the mail server address
            smtp.Send(mail);
        }
    }
}
