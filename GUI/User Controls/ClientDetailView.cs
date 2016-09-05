using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Model;
using BIS.Business;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using System.Resources;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace GUI
{
    public partial class ClientDetailView : UserControl
    {
        private int clientID;
        private string clientEmail = "";
        ClientModel clientmodel = null;
        public ClientDetailView()
        {
            InitializeComponent();
            RadMessageBox.SetThemeName("Windows8");
        }

        public void SetClientDetails(ClientModel client)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblContactPerson.Text) != null)
                    lblContactPerson.Text = resxSet.GetString(lblContactPerson.Text);                
            }

            radListControl1.Items.Clear();

            clientmodel = client;
            clientID = client.idClient;
           
            //RadListDataItem ritem = new RadListDataItem();
            //ritem.Text = client.nameClient;
            //ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
            //radListControl1.Items.Add(ritem);

            ClientBUS bus = new ClientBUS();

            lblCompanyName.Text = client.nameClient;
            lblContactPersonName.Text = client.contactPersonName;
            
            RadListDataItem  ritem = new RadListDataItem();
            ritem.Text = client.webClient;
            ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Italic);
            radListControl1.Items.Add(ritem);

            

            ClientTelBUS ctel = new ClientTelBUS();
            ritem = new RadListDataItem();

            List<ClientTelModel> telMode = ctel.GetAllClientTels(client.idClient);
            if (telMode != null)
            {
                if (telMode.Count > 0)
                {
                    foreach (ClientTelModel m in telMode)
                    {
                        ritem = new RadListDataItem();
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Phone") != null)
                                ritem.Text = resxSet.GetString("Phone") + ": ";
                            else
                                ritem.Text = "Phone: ";
                        }

                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                        radListControl1.Items.Add(ritem);

                        ritem = new RadListDataItem();
                        ritem.Text = m.numberTel.Trim();
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                        radListControl1.Items.Add(ritem);

                    }
                }
            }
            
            telMode = ctel.GetAllClientFaxes(client.idClient);
            if (telMode != null)
            {
                if (telMode.Count > 0)
                {

                    foreach (ClientTelModel m in telMode)
                    {

                        ritem = new RadListDataItem();
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Fax") != null)
                                ritem.Text = resxSet.GetString("Fax") + ": ";
                            else
                                ritem.Text = "Fax: ";
                        }
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                        radListControl1.Items.Add(ritem);

                        ritem = new RadListDataItem();
                        ritem.Text = m.numberTel.Trim();
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                        radListControl1.Items.Add(ritem);
                    }
                }
            }

            ClientEmailBUS cemail = new ClientEmailBUS();
            ritem = new RadListDataItem();
            List<ClientEmailModel> emailMode = cemail.GetClientEmails(client.idClient);
            if (emailMode != null)
            {
                if (emailMode.Count > 0)
                {
                    foreach (ClientEmailModel m in emailMode)
                    {
                        if (m.email.TrimEnd() != "" && m.isCommunication == true)
                        {
                            ritem = new RadListDataItem();
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Email") != null)
                                    ritem.Text = resxSet.GetString("Email") + ": ";
                                else
                                    ritem.Text = "Email: ";
                            }
                            ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                            radListControl1.Items.Add(ritem);

                            ritem = new RadListDataItem();
                            if (m.isCommunication == true)
                            {
                                ritem.Text = m.email.Trim();
                                ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                                radListControl1.Items.Add(ritem);
                            }
                        }

                    }
                }
            }
            TypesAddressBUS taddress = new TypesAddressBUS();
            List<TypesAddresslModel> typeAdressmodel = taddress.GetAllTypeAdress(Login._user.lngUser);


            ClientAddressBUS caddress = new ClientAddressBUS();
            ritem = new RadListDataItem();
            List<ClientAddressModel> addressMode = caddress.GetClientAddresses(client.idClient);
            if (addressMode!=null)
            {
                if (addressMode.Count > 0)
                {
                    int i = 0;
                    foreach (ClientAddressModel m in addressMode)
                    {
                        if (m.street.TrimEnd() + " " + m.housenr.TrimEnd() != " " || m.postalCode.TrimEnd() + " " + m.city.TrimEnd() != " ")
                        {
                            ritem = new RadListDataItem();
                            if (typeAdressmodel[i].nameAddressType != null)
                                ritem.Text = typeAdressmodel[i].nameAddressType + ":";
                            ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                            radListControl1.Items.Add(ritem);


                            if (m.street.TrimEnd() + " " + m.housenr.TrimEnd() != " ")
                            {
                                ritem = new RadListDataItem();
                                ritem.Text = m.street.TrimEnd() + " " + m.housenr.TrimEnd();
                                ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                                radListControl1.Items.Add(ritem);
                            }


                            if (m.postalCode.TrimEnd() + " " + m.city.TrimEnd() != " ")
                            {
                                ritem = new RadListDataItem();
                                ritem.Text = m.postalCode.TrimEnd() + " " + m.city.TrimEnd();
                                ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                                radListControl1.Items.Add(ritem);
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void pictureEmail_Click(object sender, EventArgs e)
        {
            translateRadMessageBox msgboxquest = new translateRadMessageBox();
             DialogResult diagr = msgboxquest.translateAllMessageBoxDialogYesNo("Send email to client ?", "Email");
             if (diagr == DialogResult.Yes)
             {
                 if (Login.isOutlookInstalled == true)
                 {
                     BIS.Core.dbConnection dbcon = new BIS.Core.dbConnection();
                     BIS.DAO.ClientEmailDAO clientmaildao = new BIS.DAO.ClientEmailDAO();
                     System.Data.DataTable dt = clientmaildao.GetClientEmails(clientmodel.idClient);

                     string emailTo = "";
                     if (dt != null)
                     {
                         if (dt.Rows.Count > 0)
                         {
                             DataRow dr = dt.Rows[0];
                             emailTo = dr["email"].ToString();
                         }
                         else
                         {
                             RadMessageBox.Show("No Email address");
                             return;
                         }
                     }
                     try
                     {
                         List<string> lstAllRecipients = new List<string>();

                         if (emailTo.Trim() != "")
                             lstAllRecipients.Add(emailTo);

                         if (lstAllRecipients.Count > 0)
                         {
                             Outlook.Application outlookApp = new Outlook.Application();
                             outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);

                             Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                             Outlook.Inspector oInspector = oMailItem.GetInspector;
                             oMailItem.DeleteAfterSubmit = false;

                             // Recipient
                             Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                             foreach (String recipient in lstAllRecipients)
                             {
                                 Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(recipient);
                                 oRecip.Resolve();
                             }
                             //Add CC
                             // Outlook.Recipient oCCRecip = oRecips.Add("THIYAGARAJAN.DURAIRAJAN@testmail.com");
                             //oCCRecip.Type = (int)Outlook.OlMailRecipientType.olCC;
                             //oCCRecip.Resolve();

                             //Add Subject                        
                             oMailItem.Subject = "Hi " + clientmodel.nameClient;
                             //oMailItem.SaveSentMessageFolder = Login.sentFolder;

                             //Display the mailbox
                             oMailItem.Display(true);
                         }
                         else
                         {
                             translateRadMessageBox msgbox = new translateRadMessageBox();
                             msgbox.translateAllMessageBox("Invalid mail address.");
                         }

                     }
                     catch (Exception objEx)
                     {
                         RadMessageBox.Show(objEx.ToString());
                     }
                 }
                 else
                 {
                     translateRadMessageBox msgbox = new translateRadMessageBox();
                     msgbox.translateAllMessageBox("Cannot find Outlook.");
                 }
             }
        }
        void outlookApp_ItemSend(object Item, ref bool Cancel)
        {
            if (Item is Outlook.MailItem)
            {
                Outlook.MailItem item = (Outlook.MailItem)Item;
                item.Save();

                DocumentsModel model = new DocumentsModel();         
                model.idContPers = 0;
                model.idClient = clientmodel.idClient;
                model.descriptionDocument = "Email";
                model.fileDocument = item.EntryID + ".msg";
                model.typeDocument = "EML";
                model.idDocumentStatus = 2;
                model.idEmployee = 0;
                model.idResponsableEmployee = 0;
                model.inOutDocument = 0;
                model.noteDocument = "Sent Email";
                model.idArrangement = 0;
                //model.id

                model.dtCreated = DateTime.Now;
                model.dtModified = DateTime.Now;
                model.userCreated = Login._user.idUser;
                model.userModified = Login._user.idUser;

                item.SaveAs(MainForm.myEmailFolder + "\\" + item.EntryID + ".msg", Outlook.OlSaveAsType.olMSG);
                DocumentsBUS bus = new DocumentsBUS();
                bus.Save(model, this.Name, Login._user.idUser);

                //Cancel = true;
                //item.Close(Outlook.OlInspectorClose.olDiscard);
            }
        }

        private void pictureEmail_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureLetter_Click(object sender, EventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Create document from template ?", "Documents");
            if (dr == DialogResult.Yes)
            {

                Cursor.Current = Cursors.WaitCursor;
                List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
                BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
                lookupModel = bBUS.GetAllLayoutsbyTemplateTable("Client");

                using (var lookfrm = new GridLookupForm(lookupModel, "Templates"))
                {

                    if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                    {
                        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                        BookmarkFunctions.ReadTemplateFile(wordApp, "Client", "idClient", clientID, null, (BIS.Model.LayoutsModel)lookfrm.selectedRow, this.Name, Login._user.idUser);

                    }
                }
                Cursor.Current = Cursors.Default;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }        
    }
}
