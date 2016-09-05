using System;

using NUnit.Framework;
using BIS.Business;
using BIS.Model;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.IO;
using System.Resources;

using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.Model;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.Core.Utils;

namespace GUI
{
    public partial class frmArrangementTravelPapers : frmTemplate
    {
        ArrangementModel arrange;
        ArrangementRemainingModel remainingModel;

        public frmArrangementTravelPapers(ArrangementModel model)
        {
            arrange = model;
            InitializeComponent();
        }

        private void frmArrangementTravelPapers_Load(object sender, EventArgs e)
        {
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            btnPurchase.Visibility = ElementVisibility.Collapsed;
            btnDelPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;
            btnAddTraveler.Visibility = ElementVisibility.Collapsed;
            btnAddVoluntary.Visibility = ElementVisibility.Collapsed;
            btnDeleteTraveler.Visibility = ElementVisibility.Collapsed;
            btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTravelpapers.Visibility = ElementVisibility.Collapsed;
            btnRibbonTravelpapers.Visibility = ElementVisibility.Collapsed;
            radRibbonReports.Visibility = ElementVisibility.Collapsed;

            btnSave.Click += btnSave_Click;

            SetTranslation();
            loadRemaining();


            //Ribbon bar program 
            this.richTextEditorRibbonBar2.MinimizeButton = false;
            this.richTextEditorRibbonBar2.MaximizeButton = false;
            this.richTextEditorRibbonBar2.CloseButton = false;
            if (richTextEditorRibbonBar2.RibbonBarElement.Children.Count > 0)
            {
                this.richTextEditorRibbonBar2.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[6]);
                this.richTextEditorRibbonBar2.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[5]);
                this.richTextEditorRibbonBar2.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[2]);
                this.richTextEditorRibbonBar2.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1]);
                this.richTextEditorRibbonBar2.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[0]);

            }

            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[0].Children[4]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[0].Children[3]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[0].Children[0]);

            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[7]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[6]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[5]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[4]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[3]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[1]);
            this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBar2.RibbonBarElement.Children[1].Children[1].Children[1].Children[0]);

            for (int i = 2; i < this.richTextEditorRibbonBar2.CommandTabs.Count; i++)
            {
                this.richTextEditorRibbonBar2.CommandTabs.Remove(this.richTextEditorRibbonBar2.CommandTabs[i]);
            }
            for (int i = 0; i < this.richTextEditorRibbonBar2.QuickAccessToolBar.Children.Count; i++)
            {
                this.richTextEditorRibbonBar2.QuickAccessToolBar.Children.RemoveAt(i);
            }
            this.richTextEditorRibbonBar2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextEditorRibbonBar2.QuickAccessToolBar.Visibility = ElementVisibility.Collapsed;
            this.richTextEditorRibbonBar2.CommandTabs[1].Visibility = ElementVisibility.Visible;


            //  this.richTextEditorRibbonBar2.RibbonBarElement. = "";
            //   Ribbon bar letter
            this.richTextEditorRibbonBarLetter.MinimizeButton = false;
            this.richTextEditorRibbonBarLetter.MaximizeButton = false;
            this.richTextEditorRibbonBarLetter.CloseButton = false;
            if (richTextEditorRibbonBarLetter.RibbonBarElement.Children.Count > 0)
            {
                this.richTextEditorRibbonBarLetter.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[6]);
                this.richTextEditorRibbonBarLetter.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[5]);
                this.richTextEditorRibbonBarLetter.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[2]);
                this.richTextEditorRibbonBarLetter.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1]);
                this.richTextEditorRibbonBarLetter.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[0]);

            }
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[0].Children[4]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[0].Children[3]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[0].Children[0]);

            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[7]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[6]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[5]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[4]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[3]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[1]);
            this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarLetter.RibbonBarElement.Children[1].Children[1].Children[1].Children[0]);

            for (int i = 2; i < this.richTextEditorRibbonBarLetter.CommandTabs.Count; i++)
            {
                this.richTextEditorRibbonBarLetter.CommandTabs.Remove(this.richTextEditorRibbonBarLetter.CommandTabs[i]);
            }
            for (int i = 0; i < this.richTextEditorRibbonBarLetter.QuickAccessToolBar.Children.Count; i++)
            {
                this.richTextEditorRibbonBarLetter.QuickAccessToolBar.Children.RemoveAt(i);
            }

            this.richTextEditorRibbonBarLetter.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextEditorRibbonBarLetter.QuickAccessToolBar.Visibility = ElementVisibility.Collapsed;
            this.richTextEditorRibbonBarLetter.CommandTabs[1].Visibility = ElementVisibility.Visible;

            //   Ribbon bar rules
            this.richTextEditorRibbonBarRules.MinimizeButton = false;
            this.richTextEditorRibbonBarRules.MaximizeButton = false;
            this.richTextEditorRibbonBarRules.CloseButton = false;
            if (richTextEditorRibbonBarRules.RibbonBarElement.Children.Count > 0)
            {
                this.richTextEditorRibbonBarRules.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[6]);
                this.richTextEditorRibbonBarRules.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[5]);
                this.richTextEditorRibbonBarRules.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[2]);
                this.richTextEditorRibbonBarRules.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1]);
                this.richTextEditorRibbonBarRules.RibbonBarElement.Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[0]);

            }
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[0].Children[4]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[0].Children[3]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[0].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[0].Children[0]);

            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[7]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[6]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[5]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[4]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[3]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[1]);
            this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children.Remove(this.richTextEditorRibbonBarRules.RibbonBarElement.Children[1].Children[1].Children[1].Children[0]);
            for (int i = 2; i < this.richTextEditorRibbonBarRules.CommandTabs.Count; i++)
            {
                this.richTextEditorRibbonBarRules.CommandTabs.Remove(this.richTextEditorRibbonBarRules.CommandTabs[i]);
            }
            for (int i = 0; i < this.richTextEditorRibbonBarRules.QuickAccessToolBar.Children.Count; i++)
            {
                this.richTextEditorRibbonBarRules.QuickAccessToolBar.Children.RemoveAt(i);
            }
            this.richTextEditorRibbonBarRules.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextEditorRibbonBarRules.QuickAccessToolBar.Visibility = ElementVisibility.Collapsed;
            this.richTextEditorRibbonBarRules.CommandTabs[1].Visibility = ElementVisibility.Visible;



            if (isAutitravel() == false)
            {
                tabRulesAppointment.Item.Visibility = ElementVisibility.Collapsed;
                tabProgram.Item.Visibility = ElementVisibility.Collapsed;
            }

            dtArr2.Format = DateTimePickerFormat.Custom;
            // ((DateTime)dtArr2.FormatString = "{0: HH:mm}";
            this.dtArr2.DateTimePickerElement.ShowTimePicker = true;
            dtArr.Format = DateTimePickerFormat.Custom;
            this.dtArr.DateTimePickerElement.ShowTimePicker = true;
            dtAway2.Format = DateTimePickerFormat.Custom;
            this.dtAway2.DateTimePickerElement.ShowTimePicker = true;
            // dtArr2.Format = "{0:dd.MM.yyyy HH:mm 'h'}";


        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            if (isInArrangementRemaining() == false)
                saveRemaining();
            else
                updateRemaining();
        }

        private void SetTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                //tab travelpapers- remaining
                if (resxSet.GetString(chkFlight.Text) != null)
                    chkFlight.Text = resxSet.GetString(chkFlight.Text);
                if (resxSet.GetString(lblAwayDt.Text) != null)
                    lblAwayDt.Text = resxSet.GetString(lblAwayDt.Text);
                if (resxSet.GetString(lblAwayAirport.Text) != null)
                    lblAwayAirport.Text = resxSet.GetString(lblAwayAirport.Text);
                if (resxSet.GetString(lblAwayFlightNr.Text) != null)
                    lblAwayFlightNr.Text = resxSet.GetString(lblAwayFlightNr.Text);
                if (resxSet.GetString(lblArrDt.Text) != null)
                    lblArrDt.Text = resxSet.GetString(lblArrDt.Text);
                if (resxSet.GetString(lblArrAirport.Text) != null)
                    lblArrAirport.Text = resxSet.GetString(lblArrAirport.Text);
                if (resxSet.GetString(lblBackDt.Text) != null)
                    lblBackDt.Text = resxSet.GetString(lblBackDt.Text);
                if (resxSet.GetString(lblBackAirport.Text) != null)
                    lblBackAirport.Text = resxSet.GetString(lblBackAirport.Text);
                if (resxSet.GetString(lblBackFlight.Text) != null)
                    lblBackFlight.Text = resxSet.GetString(lblBackFlight.Text);
                if (resxSet.GetString(lblArrDt3.Text) != null)
                    lblArrDt3.Text = resxSet.GetString(lblArrDt3.Text);
                if (resxSet.GetString(lblArrAirport3.Text) != null)
                    lblArrDt3.Text = resxSet.GetString(lblArrDt3.Text);
                if (resxSet.GetString(lblCollectTime.Text) != null)
                    lblCollectTime.Text = resxSet.GetString(lblCollectTime.Text);
                if (resxSet.GetString(lblAirportSociety.Text) != null)
                    lblAirportSociety.Text = resxSet.GetString(lblAirportSociety.Text);
                if (resxSet.GetString(lblSpecials.Text) != null)
                    lblSpecials.Text = resxSet.GetString(lblSpecials.Text);
            }
        }
        private void loadRemaining()
        {
            if (isInArrangementRemaining() == true)
            {


                if (isCheckedTwoFlight() == true)
                {
                    loadRemainingTrue();
                }
                else
                {
                    loadRemainingFalse();
                }


                if (remainingModel.program != null)
                {
                    RadDocument radDoc = ImportHtml(remainingModel.program);

                    rtbProgramTravel.Document = radDoc;

                }
                if (remainingModel.letter != null)
                {
                    RadDocument radDocLetter = ImportHtmlLetter(remainingModel.letter);

                    rtbLetterTravel.Document = radDocLetter;

                }
                if (remainingModel.rulesAppointment != null)
                {
                    RadDocument radDocRules = ImportHtmlRules(remainingModel.rulesAppointment);

                    rtbRules.Document = radDocRules;

                }
            }
            else
            {
                loadRemainingFalse();
                //  program
                RadDocument radDoc = new RadDocument();

                rtbProgramTravel.Document = radDoc;

                rtbProgramTravel.ChangeFontFamily(new Telerik.WinControls.RichTextEditor.UI.FontFamily("Verdana"));
                rtbProgramTravel.RichTextBoxElement.ChangeFontSize(Unit.PointToDip(9));
                rtbProgramTravel.Document.Style.ParagraphProperties.LineSpacingType = LineSpacingType.Auto;
                //rtbProgramTravel.Document.Style.ParagraphProperties.SpacingAfter = 0;
                //rtbProgramTravel.Document.LineSpacing = Unit.PointToDip(0.6);
                rtbProgramTravel.ChangeParagraphLineSpacing(0.7);
                rtbProgramTravel.ChangeParagraphSpacingAfter(0.0);



                // letter
                RadDocument radDocLetter = new RadDocument();

                rtbLetterTravel.Document = radDocLetter;

                rtbLetterTravel.ChangeFontFamily(new Telerik.WinControls.RichTextEditor.UI.FontFamily("Verdana"));
                rtbLetterTravel.RichTextBoxElement.ChangeFontSize(Unit.PointToDip(9));
                rtbLetterTravel.Document.Style.ParagraphProperties.LineSpacingType = LineSpacingType.Auto;
                //   rtbLetterTravel.Document.Style.ParagraphProperties.SpacingAfter = 0;
                //    rtbLetterTravel.Document.LineSpacing = Unit.PointToDip(0.6);
                rtbLetterTravel.ChangeParagraphLineSpacing(0.7);
                rtbLetterTravel.ChangeParagraphSpacingAfter(0.0);



                //rules
                RadDocument radDocRules = new RadDocument();

                rtbRules.Document = radDocRules;
                rtbRules.ChangeFontFamily(new Telerik.WinControls.RichTextEditor.UI.FontFamily("Verdana"));
                rtbRules.RichTextBoxElement.ChangeFontSize(Unit.PointToDip(9));
                rtbRules.Document.Style.ParagraphProperties.LineSpacingType = LineSpacingType.Auto;
                //rtbRules.Document.Style.ParagraphProperties.SpacingAfter = 0;
                //rtbRules.Document.LineSpacing = Unit.PointToDip(0.6);
                rtbRules.ChangeParagraphLineSpacing(0.7);
                rtbRules.ChangeParagraphSpacingAfter(0.0);
            }
        }

        private void saveRemaining()
        {
            remainingModel = new ArrangementRemainingModel();
            remainingModel.idArrangement = arrange.idArrangement;
            remainingModel.awayDt = dtAway.Value;
            remainingModel.awayDt2 = dtAway2.Value;
            remainingModel.awayAirport = txtAwAirport.Text;
            remainingModel.awayAirport2 = txtAwAirport2.Text;
            remainingModel.awayFlightNr = txtAwFlight.Text;
            remainingModel.awayFlightNr2 = txtAwFlight2.Text;
            remainingModel.arrivalDt = dtArr.Value;
            remainingModel.arrivalDt2 = dtArr2.Value;
            remainingModel.arrivalAirport = txtArrAirport.Text;
            remainingModel.arrivalAirport2 = txtArrAirport2.Text;
            remainingModel.backDt = dtBack.Value;
            remainingModel.backDt2 = dtBack2.Value;
            remainingModel.backAirport = txtBackAirport.Text;
            remainingModel.backAirport2 = txtBackAirport2.Text;
            remainingModel.backFlightNr = txtBackFlightNr.Text;
            remainingModel.backFlightNr2 = txtBackFlightNr2.Text;
            remainingModel.arrivalDt3 = dtArr3.Value;
            remainingModel.arrivalDt4 = dtArr4.Value;
            remainingModel.arrivalAirport3 = txtArrAirport3.Text;
            remainingModel.arrivalAirport4 = txtArrAirport4.Text;
            remainingModel.collectTime = txtCollectTime.Text;
            remainingModel.airportSociety = txtAirportSociety.Text;
            remainingModel.special = txtSpecials.Text;
            if (chkFlight.IsChecked == true)
                remainingModel.twoFlight = true;
            else
                remainingModel.twoFlight = false;

            exportProgramTravelStyle();
            ArrangementBookBUS arbus = new ArrangementBookBUS();
            if (txtSpecials.Text.Length <= 100)
            {
                if (arbus.SaveRemaining(remainingModel) != true)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have not succesufully save travelpapers data");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have succesufully save travelpapers data");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Max length for specials fild is 100 characters");
            }

        }
        private void updateRemaining()
        {
            remainingModel = new ArrangementRemainingModel();
            remainingModel.idArrangement = arrange.idArrangement;
            remainingModel.awayDt = dtAway.Value;
            remainingModel.awayDt2 = dtAway2.Value;
            remainingModel.awayAirport = txtAwAirport.Text;
            remainingModel.awayAirport2 = txtAwAirport2.Text;
            remainingModel.awayFlightNr = txtAwFlight.Text;
            remainingModel.awayFlightNr2 = txtAwFlight2.Text;
            remainingModel.arrivalDt = dtArr.Value;
            remainingModel.arrivalDt2 = dtArr2.Value;
            remainingModel.arrivalAirport = txtArrAirport.Text;
            remainingModel.arrivalAirport2 = txtArrAirport2.Text;
            remainingModel.backDt = dtBack.Value;
            remainingModel.backDt2 = dtBack2.Value;
            remainingModel.backAirport = txtBackAirport.Text;
            remainingModel.backAirport2 = txtBackAirport2.Text;
            remainingModel.backFlightNr = txtBackFlightNr.Text;
            remainingModel.backFlightNr2 = txtBackFlightNr2.Text;
            remainingModel.arrivalDt3 = dtArr3.Value;
            remainingModel.arrivalDt4 = dtArr4.Value;
            remainingModel.arrivalAirport3 = txtArrAirport3.Text;
            remainingModel.arrivalAirport4 = txtArrAirport4.Text;
            remainingModel.collectTime = txtCollectTime.Text;
            remainingModel.airportSociety = txtAirportSociety.Text;
            remainingModel.special = txtSpecials.Text;
            if (chkFlight.IsChecked == true)
                remainingModel.twoFlight = true;
            else
                remainingModel.twoFlight = false;

            exportProgramTravelStyle();
            ArrangementBookBUS arbus = new ArrangementBookBUS();
            if (txtSpecials.Text.Length <= 100)
            {
                if (arbus.UpdateRemaining(remainingModel) != true)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have not succesufully update travelpapers data");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have succesufully update travelpapers data");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Max length for specials fild is 100 characters");
            }

        }

        private bool isInArrangementRemaining()
        {
            bool result = false;
            ArrangementBookBUS arbus = new ArrangementBookBUS();

            int contains = arbus.isInArrangementRemaining(arrange.idArrangement);
            if (contains > -1)
                result = true;
            return result;

        }
        private bool isCheckedTwoFlight()
        {
            bool result = false;
            ArrangementBookBUS arbus = new ArrangementBookBUS();
            List<ArrangementRemainingModel> lista = new List<ArrangementRemainingModel>();
            lista = arbus.isCheckedTwoFlight(arrange.idArrangement);
            if (lista[0].twoFlight == true)
                result = true;
            return result;

        }
        private bool isAutitravel()
        {
            bool isChecked = false;
            ArrangementBookBUS arrBUS = new ArrangementBookBUS();

            if (arrBUS.isAutitravelChecked(arrange.idArrangement) > -1)
                isChecked = true;
            return isChecked;
        }
        private void extraFieldsRemainingTrue()
        {
            dtAway2.Visible = true;
            txtAwAirport2.Visible = true;
            txtAwFlight2.Visible = true;
            dtArr2.Visible = true;
            dtArr4.Visible = true;
            txtArrAirport2.Visible = true;
            txtArrAirport4.Visible = true;

            dtBack2.Visible = true;
            txtBackAirport2.Visible = true;
            txtBackFlightNr2.Visible = true;
        }

        private void loadRemainingTrue()
        {
            extraFieldsRemainingTrue();
            ArrangementBookBUS arbus = new ArrangementBookBUS();
            List<ArrangementRemainingModel> lista = new List<ArrangementRemainingModel>();


            lista = arbus.getArrangementRemaining(arrange.idArrangement);

            if (lista != null)
            {
                dtAway.Value = Convert.ToDateTime(lista[0].awayDt.ToString());
                dtAway2.Value = Convert.ToDateTime(lista[0].awayDt2.ToString());

                if (lista[0].awayAirport != null)
                    txtAwAirport.Text = lista[0].awayAirport.ToString();
                if (lista[0].awayAirport2 != null)
                    txtAwAirport2.Text = lista[0].awayAirport2.ToString();
                if (lista[0].awayFlightNr != null)
                    txtAwFlight.Text = lista[0].awayFlightNr.ToString();
                if (lista[0].awayFlightNr2 != null)
                    txtAwFlight2.Text = lista[0].awayFlightNr2.ToString();
                // dtArr2.Text = (lista[0].arrivalDt2.ToString());
                dtArr.Value = Convert.ToDateTime(lista[0].arrivalDt.ToString());
                dtArr2.Value = Convert.ToDateTime(lista[0].arrivalDt2.ToString());
                dtArr3.Value = Convert.ToDateTime(lista[0].arrivalDt3.ToString());
                dtArr4.Value = Convert.ToDateTime(lista[0].arrivalDt4.ToString());

                if (lista[0].arrivalAirport != null)
                    txtArrAirport.Text = lista[0].arrivalAirport.ToString();
                if (lista[0].arrivalAirport2 != null)
                    txtArrAirport2.Text = lista[0].arrivalAirport2.ToString();
                if (lista[0].arrivalAirport3 != null)
                    txtArrAirport3.Text = lista[0].arrivalAirport3.ToString();
                if (lista[0].arrivalAirport4 != null)
                    txtArrAirport4.Text = lista[0].arrivalAirport4.ToString();

                dtBack.Value = Convert.ToDateTime(lista[0].backDt.ToString());
                dtBack2.Value = Convert.ToDateTime(lista[0].backDt2.ToString());
                if (lista[0].backAirport != null)
                    txtBackAirport.Text = lista[0].backAirport.ToString();
                if (lista[0].backAirport2 != null)
                    txtBackAirport2.Text = lista[0].backAirport2.ToString();
                if (lista[0].backFlightNr != null)
                    txtBackFlightNr.Text = lista[0].backFlightNr.ToString();
                if (lista[0].backFlightNr2 != null)
                    txtBackFlightNr2.Text = lista[0].backFlightNr2.ToString();
                if (lista[0].collectTime != null)
                    txtCollectTime.Text = lista[0].collectTime.ToString();
                if (lista[0].airportSociety != null)
                    txtAirportSociety.Text = lista[0].airportSociety.ToString();
                if (lista[0].special != null)
                    txtSpecials.Text = lista[0].special.ToString();
                if (lista[0].twoFlight == true)
                {
                    chkFlight.Checked = true;
                    chkFlight.ReadOnly = false;
                }
                //else
                //{
                //    chkFlight.Checked = false;
                //}
                remainingModel = new ArrangementRemainingModel();
                remainingModel.program = lista[0].program;
                remainingModel.letter = lista[0].letter;
                remainingModel.rulesAppointment = lista[0].rulesAppointment;
            }

        }
        private void loadRemainingFalse()
        {
            dtAway2.Visible = false;
            txtAwAirport2.Visible = false;
            txtAwFlight2.Visible = false;
            dtArr2.Visible = false;
            dtArr4.Visible = false;
            txtArrAirport2.Visible = false;
            txtArrAirport4.Visible = false;

            dtBack2.Visible = false;
            txtBackAirport2.Visible = false;
            txtBackFlightNr2.Visible = false;

            ArrangementBookBUS arbus = new ArrangementBookBUS();
            List<ArrangementRemainingModel> lista = new List<ArrangementRemainingModel>();


            lista = arbus.getArrangementRemaining(arrange.idArrangement);
            if (lista != null)           
            {
                 dtAway.Value = Convert.ToDateTime(lista[0].awayDt.ToString());

                 if (lista[0].awayAirport != null)
                     txtAwAirport.Text = lista[0].awayAirport.ToString();

                 if (lista[0].awayFlightNr != null)
                     txtAwFlight.Text = lista[0].awayFlightNr.ToString();

                 dtArr.Value = Convert.ToDateTime(lista[0].arrivalDt.ToString());

                 dtArr3.Value = Convert.ToDateTime(lista[0].arrivalDt3.ToString());

                 if (lista[0].arrivalAirport != null)
                     txtArrAirport.Text = lista[0].arrivalAirport.ToString();

                 if (lista[0].arrivalAirport3 != null)
                     txtArrAirport3.Text = lista[0].arrivalAirport3.ToString();

                 dtBack.Value = Convert.ToDateTime(lista[0].backDt.ToString());

                 if (lista[0].backAirport != null)
                     txtBackAirport.Text = lista[0].backAirport.ToString();

                 if (lista[0].backFlightNr != null)
                     txtBackFlightNr.Text = lista[0].backFlightNr.ToString();

                 if (lista[0].collectTime != null)
                     txtCollectTime.Text = lista[0].collectTime.ToString();
                 if (lista[0].airportSociety != null)
                     txtAirportSociety.Text = lista[0].airportSociety.ToString();
                 if (lista[0].special != null)
                     txtSpecials.Text = lista[0].special.ToString();

                 remainingModel = new ArrangementRemainingModel();
                 remainingModel.program = lista[0].program;
                 remainingModel.letter = lista[0].letter;
                 remainingModel.rulesAppointment = lista[0].rulesAppointment;
                         
            }
                //Ako nema snimljeno u ArrRemaining za taj aranzman
            else 
            {
                List<ArrangementModel> listaDateArr = new List<ArrangementModel>();
                listaDateArr = arbus.getArrangementDate(arrange.idArrangement);

                if (listaDateArr != null)
                {
                    dtAway.Value = listaDateArr[0].dtFromArrangement;
                    dtAway2.Value = listaDateArr[0].dtFromArrangement;
                    dtArr.Value = listaDateArr[0].dtFromArrangement;
                    dtArr2.Value = listaDateArr[0].dtFromArrangement;
                    dtBack.Value = listaDateArr[0].dtToArrangement;
                    dtBack2.Value = listaDateArr[0].dtToArrangement;
                    dtArr3.Value = listaDateArr[0].dtToArrangement;
                    dtArr4.Value = listaDateArr[0].dtToArrangement;
                }

            } 

        }
      

        #region Export/Import ProgramTravel Style

        private void exportProgramTravelStyle1()
        {
            //program
            rtbProgramTravel.Commands.SelectAllCommand.Execute();
            rtbLetterTravel.Commands.SelectAllCommand.Execute();
            rtbRules.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbProgramTravel.Document.Selection);
            RadDocument fragmentDocument = fragment.ToDocument();
            // letter
            DocumentFragment fragmentLetter = new DocumentFragment(rtbLetterTravel.Document.Selection);
            RadDocument fragmentDocumentLetter = fragmentLetter.ToDocument();
            // rules
            DocumentFragment fragmentRulles = new DocumentFragment(rtbRules.Document.Selection);
            RadDocument fragmentDocumentRules = fragmentRulles.ToDocument();
            // program
            HtmlFormatProvider htmlFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settings = new HtmlExportSettings();
            //letter
            HtmlFormatProvider htmlFormatProviderLetter = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settingsLetter = new HtmlExportSettings();
            //rules
            HtmlFormatProvider htmlFormatProviderRules = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settingsRules = new HtmlExportSettings();
            // program
            settings.DocumentExportLevel = DocumentExportLevel.Document;

            settings.ExportStyleMetadata = true;
            settings.ExportLocalOrStyleValueSource = true;
            settings.StylesExportMode = StylesExportMode.Classes;
            settings.StyleRepositoryExportMode = StyleRepositoryExportMode.ExportStylesAsCssClasses;
            settings.DocumentExportLevel = DocumentExportLevel.Fragment;
            settings.StylesExportMode = StylesExportMode.Inline;
            htmlFormatProvider.ExportSettings = settings;
            // letter
            settingsLetter.DocumentExportLevel = DocumentExportLevel.Document;
            settingsLetter.ExportStyleMetadata = true;
            settingsLetter.ExportLocalOrStyleValueSource = true;
            settingsLetter.StylesExportMode = StylesExportMode.Classes;
            settingsLetter.StyleRepositoryExportMode = StyleRepositoryExportMode.ExportStylesAsCssClasses;
            settingsLetter.DocumentExportLevel = DocumentExportLevel.Fragment;
            settingsLetter.StylesExportMode = StylesExportMode.Inline;
            htmlFormatProviderLetter.ExportSettings = settingsLetter;
            //rules
            settingsRules.DocumentExportLevel = DocumentExportLevel.Document;
            settingsRules.ExportStyleMetadata = true;
            settingsRules.ExportLocalOrStyleValueSource = true;
            settingsRules.StylesExportMode = StylesExportMode.Classes;
            settingsRules.StyleRepositoryExportMode = StyleRepositoryExportMode.ExportStylesAsCssClasses;
            settingsRules.DocumentExportLevel = DocumentExportLevel.Fragment;
            settingsRules.StylesExportMode = StylesExportMode.Inline;
            htmlFormatProviderLetter.ExportSettings = settingsRules;
            //program
            string htmlString;
            htmlString = ExportToHTML(rtbProgramTravel.Document);
            htmlString = htmlFormatProvider.Export(fragmentDocument);
            remainingModel.program = htmlString;
            //letter
            string htmlStringLetter;
            htmlStringLetter = ExportToHTML(rtbLetterTravel.Document);
            htmlString = htmlFormatProvider.Export(fragmentDocumentLetter);
            remainingModel.letter = htmlStringLetter;
            //rules

            string htmlStringRules;
            htmlStringRules = ExportToHTML(rtbRules.Document);
            htmlStringRules = htmlFormatProviderRules.Export(fragmentDocumentRules);
            remainingModel.rulesAppointment = htmlStringRules;
        }


        private void exportProgramTravelStyle()
        {
            //program
            rtbProgramTravel.Commands.SelectAllCommand.Execute();
            rtbLetterTravel.Commands.SelectAllCommand.Execute();
            rtbRules.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbProgramTravel.Document.Selection);
            RadDocument fragmentDocument = fragment.ToDocument();
            // letter
            DocumentFragment fragmentLetter = new DocumentFragment(rtbLetterTravel.Document.Selection);
            RadDocument fragmentDocumentLetter = fragmentLetter.ToDocument();
            // rules
            DocumentFragment fragmentRulles = new DocumentFragment(rtbRules.Document.Selection);
            RadDocument fragmentDocumentRules = fragmentRulles.ToDocument();
            // program
            HtmlFormatProvider htmlFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settings = new HtmlExportSettings();
            //letter
            HtmlFormatProvider htmlFormatProviderLetter = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settingsLetter = new HtmlExportSettings();
            //rules
            HtmlFormatProvider htmlFormatProviderRules = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlExportSettings settingsRules = new HtmlExportSettings();
            // program
            settings.DocumentExportLevel = DocumentExportLevel.Fragment;
            settings.ExportStyleMetadata = false;
            settings.ExportLocalOrStyleValueSource = false;
            settings.StylesExportMode = StylesExportMode.Inline;
            settings.StyleRepositoryExportMode = StyleRepositoryExportMode.DontExportStyles;
            htmlFormatProvider.ExportSettings = settings;
            // letter
            settingsLetter.DocumentExportLevel = DocumentExportLevel.Fragment;
            settingsLetter.ExportStyleMetadata = false;
            settingsLetter.ExportLocalOrStyleValueSource = false;
            settingsLetter.StylesExportMode = StylesExportMode.Inline;
            settingsLetter.StyleRepositoryExportMode = StyleRepositoryExportMode.DontExportStyles;
            htmlFormatProviderLetter.ExportSettings = settingsLetter;

            //rules
            settingsRules.DocumentExportLevel = DocumentExportLevel.Fragment;
            settingsRules.ExportStyleMetadata = false;
            settingsRules.ExportLocalOrStyleValueSource = false;
            settingsRules.StylesExportMode = StylesExportMode.Inline;
            settingsRules.StyleRepositoryExportMode = StyleRepositoryExportMode.DontExportStyles;
            htmlFormatProviderRules.ExportSettings = settingsRules;
            //program
            string htmlString;
            htmlString = ExportToHTML(rtbProgramTravel.Document);
            htmlString = htmlFormatProvider.Export(fragmentDocument);
            remainingModel.program = htmlString;
            //letter  
            // Aleksa i Mitar html to image
            string htmlStringLetter;
            htmlStringLetter = ExportToHTML(rtbLetterTravel.Document);
            htmlStringLetter = htmlFormatProviderLetter.Export(fragmentDocumentLetter);
            remainingModel.letter = htmlStringLetter;
            //letterimage

            Image image = TheArtOfDev.HtmlRenderer.WinForms.HtmlRender.RenderToImage(htmlStringLetter);
            remainingModel.letterImage = ImageToBase64(image, System.Drawing.Imaging.ImageFormat.Png);
            // Aleksa i Mitar html to image
            
            
            //rules

            string htmlStringRules;
            htmlStringRules = ExportToHTML(rtbRules.Document);
            htmlStringRules = htmlFormatProviderRules.Export(fragmentDocumentRules);
            remainingModel.rulesAppointment = htmlStringRules;
        }
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        // Aleksa i Mitar html to image
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
            rtbProgramTravel.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbProgramTravel.Document.Selection);
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

        public RadDocument ImportHtmlLetter(string htmlString)
        {
            rtbLetterTravel.Commands.SelectAllCommand.Execute();
            DocumentFragment fragmentLetter = new DocumentFragment(rtbLetterTravel.Document.Selection);
            RadDocument fragmentDocumentLetter = fragmentLetter.ToDocument();

            RadDocument document = null;
            HtmlFormatProvider provider = new HtmlFormatProvider();
            HtmlImportSettings settings = new HtmlImportSettings();
            settings.UseDefaultStylesheetForFontProperties = true;

            provider.ImportSettings = settings;


            document = provider.Import(htmlString);


            return document;
            //return provider.Import(aaa);
        }

        public RadDocument ImportHtmlRules(string htmlString)
        {
            rtbRules.Commands.SelectAllCommand.Execute();
            DocumentFragment fragmentRules = new DocumentFragment(rtbRules.Document.Selection);
            RadDocument fragmentDocumentRules = fragmentRules.ToDocument();

            RadDocument document = null;
            HtmlFormatProvider provider = new HtmlFormatProvider();
            HtmlImportSettings settings = new HtmlImportSettings();
            settings.UseDefaultStylesheetForFontProperties = true;

            provider.ImportSettings = settings;


            document = provider.Import(htmlString);


            return document;
            //return provider.Import(aaa);
        }
        private void ImportProgramTravelStyle()
        {
            rtbProgramTravel.Commands.SelectAllCommand.Execute();
            DocumentFragment fragment = new DocumentFragment(rtbProgramTravel.Document.Selection);
            HtmlFormatProvider HtmlFormatProvider = new HtmlFormatProvider();
            RadDocument fragmentDocument = fragment.ToDocument();
            HtmlFormatProvider htmlFormatProvider = DocumentFormatProvidersManager.GetProviderByExtension("html") as HtmlFormatProvider;
            HtmlImportSettings settings = new HtmlImportSettings();

            htmlFormatProvider.ImportSettings = settings;

            //string import;
            RadDocument import = ImportHtmlStyle(remainingModel.program);

            //  import = htmlFormatProvider.Import(fragmentDocument);
            rtbProgramTravel.Document = import;
        }
        #endregion Export/Import ProgramTravel Style

        private void chkFlight_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkFlight.Checked == true)
            {
                loadRemainingTrue();
            }
            else if(chkFlight.Checked == false)
            {
                loadRemainingFalse();
            }
        }


    }
}
