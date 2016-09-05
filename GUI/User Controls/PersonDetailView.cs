using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using Telerik.WinControls.UI;
using BIS.Model;
using System.Resources;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace GUI
{
    public partial class PersonDetailView : UserControl
    {
        // Person Detail View panel
        private int personID;
        private int emailPerson;
        PersonModel personmodel = null;

        public PersonDetailView()
        {
            InitializeComponent();
           // RadMessageBox.SetThemeName("Windows8");
        }

        public void RowSelected()
        {
            //MessageBox.Show("change");
        }
    
        public void SetPersonDetails(BIS.Model.PersonModel person)
        {
            personmodel = person;
            personID = person.idContPers;
            radListControl1.Items.Clear();
            //radListControl1 = new RadListControl();
            if (personID > 0)
            {
                //Mitar i Aleksa
                labelFullname.Text = person.nameTitle + " " + person.firstname + " " + person.midname+ " " + person.lastname;
                //Mitar i Aleksa

                // Set image

                PersonBUS bus = new PersonBUS();
                object objImage = bus.GetPersonImage(personID);
                string strImage = "";
                if (objImage != null && objImage != DBNull.Value)
                    strImage = (string)objImage;

                if (strImage != "")
                {
                    BIS.Core.ImageDB setImage = new BIS.Core.ImageDB();
                    imageProfile.Image = setImage.setImage(strImage); ;
                }
                else
                {
                    imageProfile.Image = null;
                }
               

                PersonTelBUS ptel = new PersonTelBUS();
                RadListDataItem ritem = new RadListDataItem();

                if (person.birthdate != null)
                {
                    DateTime tmpDt = (DateTime) person.birthdate;
                    if (tmpDt.Year != 1)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Age") != null)
                                ritem.Text = resxSet.GetString("Age") + ": ";
                            else
                                ritem.Text = "Age: ";
                        }
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                        radListControl1.Items.Add(ritem);

                        int years = 0;

                        DateTime currentDate = DateTime.Now;
                        int currentYear = currentDate.Year;
                        int currentMounth = currentDate.Month;
                        int currentDay = currentDate.Day;

                        int personYear = 0;
                        int personMounth = 0;
                        int personDay = 0;

                        personYear = tmpDt.Year;
                        personMounth = tmpDt.Month;
                        personDay = tmpDt.Day;

                        if (personMounth * 100 + personDay > currentMounth * 100 + currentDay)
                            years = currentYear - personYear - 1;
                        else
                            years = currentYear - personYear;
                        

                        ritem = new RadListDataItem();
                        ritem.Text = years.ToString();
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                        radListControl1.Items.Add(ritem);

                    }
                }
                PersonBUS pbus = new PersonBUS();
                List<LabelForPerson> labelForPerson = new List<LabelForPerson>();
                labelForPerson =  pbus.GetLabelsPerson(person.idContPers);

                if(labelForPerson != null)
                {
                    VolontaryFunctionBUS vbus = new VolontaryFunctionBUS();
                    List<MedicalVoluntaryQuestModel> vmodellist = new List<MedicalVoluntaryQuestModel>();
                    vmodellist = vbus.GetVoluntaryFunctionForPerson(labelForPerson, person.idContPers);

                    if (vmodellist != null && vmodellist.Count > 0)
                    {
                        ritem = new RadListDataItem();
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Function") != null)
                                ritem.Text = resxSet.GetString("Function") + ": ";
                            else
                                ritem.Text = "Function: ";
                        }
                        radListControl1.Items.Add(ritem);

                        foreach (MedicalVoluntaryQuestModel m in vmodellist)
                        {
                            ritem = new RadListDataItem();
                            ritem.Text = m.txtQuest;
                            ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                            radListControl1.Items.Add(ritem);
                        }
                    }
                }


                
                PersonEmailBUS pemail = new PersonEmailBUS();
                //ritem = new RadListDataItem();
                //List<PersonEmailModel> emailMode = pemail.GetPersonEmails(person.idContPers);
                //if(emailMode!=null)
                //{
                //    if (emailMode.Count > 0)
                //    {
                //        foreach (PersonEmailModel m in emailMode)
                //        {
                //            if (m.email.TrimEnd() != "" && m.isCommunication == true)
                //            {
                //                ritem = new RadListDataItem();
                //                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //                {
                //                    if (resxSet.GetString("Email") != null)
                //                        ritem.Text = resxSet.GetString("Email") + ": ";
                //                    else
                //                        ritem.Text = "Email: ";
                //                }
                //                ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                //                radListControl1.Items.Add(ritem);

                //                ritem = new RadListDataItem();
                //                if (m.isCommunication == true)
                //                {
                //                    ritem.Text = m.email.Trim();
                //                    ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                //                    radListControl1.Items.Add(ritem);
                //                }
                //            }

                //        }
                //    }
                //}

                TypesAddressBUS taddress = new TypesAddressBUS();
                List<TypesAddresslModel> typeAdressmodel = taddress.GetAllTypeAdress(Login._user.lngUser);
                PersonAddressBUS paddress = new PersonAddressBUS();
                ritem = new RadListDataItem();
                List<PersonAddressModel> addressMode = paddress.GetPersonAddresses(person.idContPers);
                if (addressMode != null)
                {
                    if (addressMode.Count > 0)
                    {
                        int i = 0;
                        foreach (PersonAddressModel m in addressMode)
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
                //Mitar i Aleksa
                List<PersonEmailModel> emailModeDesc = pemail.GetPersonEmails(person.idContPers);
                if (emailModeDesc != null)
                {
                    if (emailModeDesc.Count > 0)
                    {
                        ritem = new RadListDataItem();
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Email/s") != null)
                                ritem.Text = resxSet.GetString("Email/s") + ": ";
                            else
                                ritem.Text = "Email/s: ";
                        }
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                        radListControl1.Items.Add(ritem);
                        foreach (PersonEmailModel m in emailModeDesc)
                        {
                            // if (m.isDefaultTel == true)
                            //{
                            ritem = new RadListDataItem();
                            ritem.Text = m.email.Trim();
                            ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                            radListControl1.Items.Add(ritem);
                            //}
                        }
                    }
                }
                List<PersonTelModel> telMode = ptel.GetPersonTels(person.idContPers);
                if (telMode != null)
                {
                    if (telMode.Count > 0)
                    {
                        ritem = new RadListDataItem();
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Phone/s") != null)
                                ritem.Text = resxSet.GetString("Phone/s") + ": ";
                            else
                                ritem.Text = "Phone/s: ";
                        }
                        ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                        radListControl1.Items.Add(ritem);
                        foreach (PersonTelModel m in telMode)
                        {
                            // if (m.isDefaultTel == true)
                            //{
                            ritem = new RadListDataItem();
                            if (m.descriptionTel != null)
                            {
                                ritem.Text = m.descriptionTel.Trim() + ":";
                                ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                                radListControl1.Items.Add(ritem);
                                ritem = new RadListDataItem();
                            }
                            ritem.Text = m.numberTel.Trim();
                            ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                            radListControl1.Items.Add(ritem);
                            //}
                        }
                    }
                }
                //Mitar i Aleksa
            }
        }
        private void labelFullname_Click(object sender, EventArgs e)
        {

        }
        private void pictureEmail_Click(object sender, EventArgs e)
         {
             translateRadMessageBox msgboxquest = new translateRadMessageBox();
             DialogResult diagr = msgboxquest.translateAllMessageBoxDialogYesNo("Send email to contact person ?", "Email");
             if (diagr == DialogResult.Yes)
             {
                 if (Login.isOutlookInstalled == true)
                 {
                     BIS.Core.dbConnection dbcon = new BIS.Core.dbConnection();
                     BIS.DAO.PersonEmailDAO personemaildao = new BIS.DAO.PersonEmailDAO();
                     DataTable dt = personemaildao.GetPersonEmails(personID);

                     string emailTo = "";
                     if (dt != null)
                     {
                         if (dt.Rows.Count > 0)
                         {
                             DataRow dr = dt.Rows[0];
                             emailTo = dr["email"].ToString();
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
                             string personame = "";
                             if (personmodel != null)
                             {
                                 personame = personmodel.fullname_with_title;
                             }
                             oMailItem.Subject = "Hi " + personame;
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
                  
                   model.idContPers = personmodel.idContPers;
                   model.idClient = 0;
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

           private void pictureLetter_Click(object sender, EventArgs e)
           {
               translateRadMessageBox msgbox = new translateRadMessageBox();
               DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Create document from template ?", "Documents");
               if (dr == DialogResult.Yes)
               {

                   Cursor.Current = Cursors.WaitCursor;
                   List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
                   BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
                   lookupModel = bBUS.GetAllLayoutsbyTemplateTable("ContactPerson");

                   using (var lookfrm = new GridLookupForm(lookupModel, "Templates"))
                   {

                       if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                       {
                           Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                           BookmarkFunctions.ReadTemplateFile(wordApp, "ContactPerson", "idContPers", personID, null, (BIS.Model.LayoutsModel)lookfrm.selectedRow, this.Name, Login._user.idUser);

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
