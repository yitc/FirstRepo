using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Resources;
using System.Xml;
using System.Data;
using BIS.Model;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Forms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using System.Globalization;

namespace GUI
{

    class ReportClassCencelledTrips : System.Windows.Forms.UserControl
    {
        string xReport;
        DataTable dt;
        FileStream streamingReport;
        public void transl(string reportName, DataTable dtReport)
        {


            xReport = reportName;
            dt = dtReport;

            XmlDocument rdlcXML = new XmlDocument();

            try
            {
                streamingReport = new FileStream(xReport, FileMode.Open);
                try
                {
                    rdlcXML.Load(streamingReport);
                    addTable(rdlcXML);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    streamingReport.Close();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {

            }


            rdlcXML.Save(xReport);
            rdlcXML.Save("GUI.CencelledTripsPreview.rdlc");
        }

        private void addTable(XmlDocument rdlcXML)
        {

            XmlElement xTablixMembers = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xTablixMembersRow = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xR = rdlcXML.CreateElement("TablixRows", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xRow = rdlcXML.CreateElement("TablixRow", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xRow2 = rdlcXML.CreateElement("TablixRow", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xRowHeight = rdlcXML.CreateElement("Height", rdlcXML.DocumentElement.NamespaceURI);
            xRowHeight.InnerXml = "0.25in";
            xRow.AppendChild(xRowHeight);
            XmlElement xRowHeight2 = rdlcXML.CreateElement("Height", rdlcXML.DocumentElement.NamespaceURI);
            xRowHeight2.InnerXml = "0.25in";
            xRow2.AppendChild(xRowHeight2);
            XmlElement xRowCellsHeader = rdlcXML.CreateElement("TablixCells", rdlcXML.DocumentElement.NamespaceURI);
            xRow.AppendChild(xRowCellsHeader);
            xR.AppendChild(xRow);
            XmlElement xRowCellsHeader2 = rdlcXML.CreateElement("TablixCells", rdlcXML.DocumentElement.NamespaceURI);
            xRow2.AppendChild(xRowCellsHeader2);
            xR.AppendChild(xRow2);

            //for sum
            XmlElement xRow3 = rdlcXML.CreateElement("TablixRow", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xRowHeight3 = rdlcXML.CreateElement("Height", rdlcXML.DocumentElement.NamespaceURI);
            xRowHeight3.InnerXml = "0.25in";
            xRow3.AppendChild(xRowHeight3);
            XmlElement xRowCellsHeader3 = rdlcXML.CreateElement("TablixCells", rdlcXML.DocumentElement.NamespaceURI);
            xRow3.AppendChild(xRowCellsHeader3);
            xR.AppendChild(xRow3);
            //

            XmlElement xC = rdlcXML.CreateElement("TablixColumns", rdlcXML.DocumentElement.NamespaceURI);

            //Definicija Noda za Fields
            XmlElement xFields = rdlcXML.CreateElement("Fields", rdlcXML.DocumentElement.NamespaceURI);

            //Sakrivanje kolona iz datatable
            dt.Columns.Remove("idArrangement");
            //dt.Columns.Remove("nrTraveler");
            dt.Columns.Remove("DateFrom");
            dt.Columns.Remove("DateTo");

            //sum 
            XmlElement xTablixMemberRow1 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xTablixMemberRowKeepWithGroup = rdlcXML.CreateElement("KeepWithGroup", rdlcXML.DocumentElement.NamespaceURI);
            xTablixMemberRowKeepWithGroup.InnerXml = "After";
            xTablixMemberRow1.AppendChild(xTablixMemberRowKeepWithGroup);
            //Za ponavljenje hedera
            XmlNode xRepeat = rdlcXML.CreateElement("RepeatOnNewPage", rdlcXML.DocumentElement.NamespaceURI);
            xRepeat.InnerXml = "true";
            xTablixMemberRow1.AppendChild(xRepeat);
            //
            xTablixMembersRow.AppendChild(xTablixMemberRow1);
            XmlElement xTablixMemberRow2 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xTablixMemberRowGroup = rdlcXML.CreateElement("Group", rdlcXML.DocumentElement.NamespaceURI);
            xTablixMemberRowGroup.SetAttribute("Name", "Details");
            xTablixMemberRow2.AppendChild(xTablixMemberRowGroup);
            xTablixMembersRow.AppendChild(xTablixMemberRow2);
            XmlElement xTablixMemberRow3 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
            XmlElement xTablixMemberRowKeepWithGroup2 = rdlcXML.CreateElement("KeepWithGroup", rdlcXML.DocumentElement.NamespaceURI);
            xTablixMemberRowKeepWithGroup2.InnerXml = "Before";
            xTablixMemberRow3.AppendChild(xTablixMemberRowKeepWithGroup2);
            xTablixMembersRow.AppendChild(xTablixMemberRow3);
            //

            for (int i = 0; i < dt.Columns.Count; i++)
            //for (int i = 0; i < 2; i++)
            {

                //Fields
                XmlElement xDataField = rdlcXML.CreateElement("DataField", rdlcXML.DocumentElement.NamespaceURI);
                xDataField.InnerXml = dt.Columns[i].ColumnName;
                XmlElement xField = rdlcXML.CreateElement("Field", rdlcXML.DocumentElement.NamespaceURI);
                xField.SetAttribute("Name", dt.Columns[i].ColumnName);

                xFields.AppendChild(xField);
                xField.AppendChild(xDataField);
                XmlElement xRdF = rdlcXML.CreateElement("rd", "TypeName", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");

                //Ispitivanje tipa kolona
                if (dt.Columns[i].DataType == typeof(Int32))
                {
                    xRdF.InnerXml = "System.Int32";
                }
                else if (dt.Columns[i].DataType == typeof(DateTime))
                {

                    xRdF.InnerXml = "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]";

                }
                else if (dt.Columns[i].DataType == typeof(String))
                {
                    xRdF.InnerXml = "System.String";
                }
                else if (dt.Columns[i].DataType == typeof(Boolean))
                {
                    xRdF.InnerXml = "System.Boolean";
                }

                xField.AppendChild(xRdF);


                XmlElement xTablixMember = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                xTablixMembers.AppendChild(xTablixMember);

                //columns part with width
                XmlElement xColumn = rdlcXML.CreateElement("TablixColumn", rdlcXML.DocumentElement.NamespaceURI);
                xC.AppendChild(xColumn);
                //rows part header
                XmlElement xCell = rdlcXML.CreateElement("TablixCell", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xCellContents = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextBox = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);
                xTextBox.SetAttribute("Name", "TextBox" + i);
                XmlElement xCanGrow = rdlcXML.CreateElement("CanGrow", rdlcXML.DocumentElement.NamespaceURI);
                xCanGrow.InnerXml = "true";
                xTextBox.AppendChild(xCanGrow);
                XmlElement xKeepTogether = rdlcXML.CreateElement("KeepTogether", rdlcXML.DocumentElement.NamespaceURI);
                xKeepTogether.InnerXml = "true";
                xTextBox.AppendChild(xKeepTogether);
                XmlElement xParagraphs = rdlcXML.CreateElement("Paragraphs", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xParagraph = rdlcXML.CreateElement("Paragraph", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextRuns = rdlcXML.CreateElement("TextRuns", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextRun = rdlcXML.CreateElement("TextRun", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xValue = rdlcXML.CreateElement("Value", rdlcXML.DocumentElement.NamespaceURI);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(new PurchaseReportModel2());

                //za sirinu kolona
                decimal wColumn = 0;
                //

                string name = dt.Columns[i].ColumnName; //provera da li postoji prevod
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    // ovo treba ispraviti ide column name ne display name jer datatable nema display name (ovo je samo u modelu)
                    if ((properties.Find(dt.Columns[i].ColumnName, true) != null))
                    {
                        name = (properties.Find(dt.Columns[i].ColumnName, true).DisplayName);
                        if (resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName) != null)
                            name = resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName); // provera da li postoji prevod

                    }

                        //Za prevod provera da li postoji caption ( kod dinamickih kolona)
                    else
                    {
                        if (dt.Columns[i].Caption != null)
                        {
                            if (resxSet.GetString(dt.Columns[i].Caption) != null)

                                name = resxSet.GetString(dt.Columns[i].Caption);
                            else
                            {
                                name = dt.Columns[i].Caption;
                            }
                        }
                        else
                        {
                            name = dt.Columns[i].ColumnName;

                        }

                    }
                    xValue.InnerXml = name;
                    //Provera da li postoji prevod

                    //za sirinu kolona
                    if (xValue.InnerXml.Contains(" "))
                    {
                        string[] strArray = xValue.InnerXml.Split(' ');
                        string LongestWordInArray = xValue.InnerXml.Split(' ')[0];
                        for (int m = 1; m < strArray.Length; m++)
                        {
                            if (LongestWordInArray.Length < strArray[m].Length)
                                LongestWordInArray = strArray[m];
                        }

                        wColumn = (decimal)(this.CreateGraphics().MeasureString(LongestWordInArray, this.Font).Width + 35) * (decimal)0.0138889;
                    }
                    //Deo za sakrivanje kolona u XML-u
                    if (dt.Columns[i].ColumnName == "dateFrom1")
                    {
                        wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 0) * (decimal)0.0;
                    }
                    else if (dt.Columns[i].ColumnName == "dateTo1")
                    {
                        wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 0) * (decimal)0.0;

                    }

                    //END Deo za sakrivanje kolona u XML-u

                        //Deo za podesavanje sifire kolona
                    else
                    {
                        if (dt.Columns[i].Caption != null)
                        {
                            if (dt.Columns[i].ColumnName.StartsWith("Column_") || dt.Columns[i].ColumnName.StartsWith("nrTraveler"))
                            {
                                wColumn = (decimal)(60) * (decimal)0.0138889;
                            }
                            else if (dt.Columns[i].ColumnName.StartsWith("codeArrangement"))
                            {
                                wColumn = (decimal)(95) * (decimal)0.0138889;
                            }
                            else if(dt.Columns[i].ColumnName.StartsWith("nameLabel"))
                            {
                                wColumn = (decimal)(70) * (decimal)0.0138889;
                            }
                            else if (dt.Columns[i].ColumnName.StartsWith("nameArrType"))
                            {
                                if (Login._user.lngUser == "EN")
                                {
                                    wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 80) * (decimal)0.0138889;
                                }
                                else
                                {
                                    wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 89) * (decimal)0.0138889;
                                }
                            }
                            else if (dt.Columns[i].ColumnName.StartsWith("date"))
                                wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width) * (decimal)0.0138889;
                            else

                                wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 15) * (decimal)0.0138889;
                        }
                        else
                            wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 15) * (decimal)0.0138889;
                    }
                    //End
                }

                XmlElement xColumnWidth = rdlcXML.CreateElement("Width", rdlcXML.DocumentElement.NamespaceURI);
                //za sirinu kolona
                // this need to add that column defines txt from translated column header and turn it to inches
                if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator != ".")
                    xColumnWidth.InnerXml = wColumn.ToString().Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".") + "in";
                else
                    xColumnWidth.InnerXml = wColumn.ToString() + "in";
                //
                xColumn.AppendChild(xColumnWidth);

                xTextRun.AppendChild(xValue);
                XmlElement xTextRunStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xFontFamily = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                xFontFamily.InnerXml = "Arial";
                xTextRunStyle.AppendChild(xFontFamily);
                XmlElement xFontSize = rdlcXML.CreateElement("FontSize", rdlcXML.DocumentElement.NamespaceURI);
                xFontSize.InnerXml = "8pt";
                xTextRunStyle.AppendChild(xFontSize);
                XmlElement xFontWeight = rdlcXML.CreateElement("FontWeight", rdlcXML.DocumentElement.NamespaceURI);
                xFontWeight.InnerXml = "Bold";
                xTextRunStyle.AppendChild(xFontWeight);
                xTextRun.AppendChild(xTextRunStyle);
                xTextRuns.AppendChild(xTextRun);
                XmlElement xStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextAlign = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
                //Za slagenje teksta
                if (dt.Columns[i].DataType == (typeof(string)) && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                {
                    xTextAlign.InnerXml = "Left";
                }
                //else if(dt.Columns[i].DataType==(typeof(int)) && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                //{
                //    xTextAlign.InnerXml = "Right";
                //}
                else
                    //
                    xTextAlign.InnerXml = "Center";
                xStyle.AppendChild(xTextAlign);
                xParagraph.AppendChild(xTextRuns);
                xParagraph.AppendChild(xStyle);
                xParagraphs.AppendChild(xParagraph);
                XmlElement xRD = rdlcXML.CreateElement("rd", "DefaultName", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
                xRD.InnerXml = "TextBox" + i;
                XmlElement xMainStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xBorder = rdlcXML.CreateElement("Border", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xColor = rdlcXML.CreateElement("Color", rdlcXML.DocumentElement.NamespaceURI);
                xColor.InnerXml = "LightGrey";
                xBorder.AppendChild(xColor);
                XmlElement xBorderStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                xBorderStyle.InnerXml = "Solid";
                xBorder.AppendChild(xBorderStyle);
                xMainStyle.AppendChild(xBorder);
                XmlElement xBackgroundColor = rdlcXML.CreateElement("BackgroundColor", rdlcXML.DocumentElement.NamespaceURI);
                xBackgroundColor.InnerXml = "LightGrey";
                xMainStyle.AppendChild(xBackgroundColor);
                XmlElement xPaddingLeft = rdlcXML.CreateElement("PaddingLeft", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingLeft.InnerXml = "0pt";
                xMainStyle.AppendChild(xPaddingLeft);
                XmlElement xPaddingRight = rdlcXML.CreateElement("PaddingRight", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingRight.InnerXml = "0pt";
                xMainStyle.AppendChild(xPaddingRight);
                XmlElement xPaddingTop = rdlcXML.CreateElement("PaddingTop", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingTop.InnerXml = "2pt";
                xMainStyle.AppendChild(xPaddingTop);
                XmlElement xPaddingBottom = rdlcXML.CreateElement("PaddingBottom", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingBottom.InnerXml = "2pt";
                xMainStyle.AppendChild(xPaddingBottom);
                xTextBox.AppendChild(xParagraphs);
                xTextBox.AppendChild(xRD);
                xTextBox.AppendChild(xMainStyle);
                xCellContents.AppendChild(xTextBox);
                xCell.AppendChild(xCellContents);
                xRowCellsHeader.AppendChild(xCell);
                xRow.AppendChild(xRowCellsHeader);



                //rows part 
                XmlElement xCell2 = rdlcXML.CreateElement("TablixCell", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xCellContents2 = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextBox2 = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);

                xTextBox2.SetAttribute("Name", dt.Columns[i].ColumnName);

                XmlElement xCanGrow2 = rdlcXML.CreateElement("CanGrow", rdlcXML.DocumentElement.NamespaceURI);
                xCanGrow2.InnerXml = "true";
                xTextBox2.AppendChild(xCanGrow2);
                XmlElement xKeepTogether2 = rdlcXML.CreateElement("KeepTogether", rdlcXML.DocumentElement.NamespaceURI);
                xKeepTogether2.InnerXml = "true";
                xTextBox2.AppendChild(xKeepTogether2);
                XmlElement xParagraphs2 = rdlcXML.CreateElement("Paragraphs", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xParagraph2 = rdlcXML.CreateElement("Paragraph", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextRuns2 = rdlcXML.CreateElement("TextRuns", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextRun2 = rdlcXML.CreateElement("TextRun", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xValue2 = rdlcXML.CreateElement("Value", rdlcXML.DocumentElement.NamespaceURI);

                xValue2.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";

                xTextRun2.AppendChild(xValue2);
                XmlElement xTextRunStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xFontFamily2 = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                xFontFamily2.InnerXml = "Arial";
                xTextRunStyle2.AppendChild(xFontFamily2);
                XmlElement xFontSize2 = rdlcXML.CreateElement("FontSize", rdlcXML.DocumentElement.NamespaceURI);
                xFontSize2.InnerXml = "8pt";
                xTextRunStyle2.AppendChild(xFontSize2);
                xTextRun2.AppendChild(xTextRunStyle2);
                xTextRuns2.AppendChild(xTextRun2);
                XmlElement xStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                //Za slaganje teksta 
                XmlElement xTextAlign2 = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
                if (dt.Columns[i].DataType == (typeof(string)) && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                {
                    xTextAlign2.InnerXml = "Left";
                }
                //else if(dt.Columns[i].DataType==(typeof(int)) && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                //{
                //    xTextAlign2.InnerXml = "Right";
                //}
                else
                    //
                    xTextAlign2.InnerXml = "Center";
                xStyle2.AppendChild(xTextAlign2);
                xParagraph2.AppendChild(xTextRuns2);
                xParagraph2.AppendChild(xStyle2);
                xParagraphs2.AppendChild(xParagraph2);
                XmlElement xRD2 = rdlcXML.CreateElement("rd", "DefaultName", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
                xRD2.InnerXml = dt.Columns[i].ColumnName;
                XmlElement xMainStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xBorder2 = rdlcXML.CreateElement("Border", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xBorderStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                xBorderStyle2.InnerXml = "None";
                xBorder2.AppendChild(xBorderStyle2);
                xMainStyle2.AppendChild(xBorder2);
                XmlElement xPaddingLeft2 = rdlcXML.CreateElement("PaddingLeft", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingLeft2.InnerXml = "2pt";
                xMainStyle2.AppendChild(xPaddingLeft2);
                XmlElement xPaddingRight2 = rdlcXML.CreateElement("PaddingRight", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingRight2.InnerXml = "2pt";
                xMainStyle2.AppendChild(xPaddingRight2);
                XmlElement xPaddingTop2 = rdlcXML.CreateElement("PaddingTop", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingTop2.InnerXml = "2pt";
                xMainStyle2.AppendChild(xPaddingTop2);
                XmlElement xPaddingBottom2 = rdlcXML.CreateElement("PaddingBottom", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingBottom2.InnerXml = "2pt";
                xMainStyle2.AppendChild(xPaddingBottom2);


                xTextBox2.AppendChild(xParagraphs2);
                xTextBox2.AppendChild(xRD2);
                xTextBox2.AppendChild(xMainStyle2);
                xCellContents2.AppendChild(xTextBox2);
                xCell2.AppendChild(xCellContents2);
                xRowCellsHeader2.AppendChild(xCell2);
                xRow2.AppendChild(xRowCellsHeader2);




                //sum 
                XmlElement xCell3 = rdlcXML.CreateElement("TablixCell", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xCellContents3 = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextBox3 = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);
                xTextBox3.SetAttribute("Name", "Textbox" + (dt.Columns.Count + i + 7).ToString());
                XmlElement xCanGrow3 = rdlcXML.CreateElement("CanGrow", rdlcXML.DocumentElement.NamespaceURI);
                xCanGrow3.InnerXml = "true";
                xTextBox3.AppendChild(xCanGrow3);
                XmlElement xKeepTogether3 = rdlcXML.CreateElement("KeepTogether", rdlcXML.DocumentElement.NamespaceURI);
                xKeepTogether3.InnerXml = "true";
                xTextBox3.AppendChild(xKeepTogether3);
                XmlElement xParagraphs3 = rdlcXML.CreateElement("Paragraphs", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xParagraph3 = rdlcXML.CreateElement("Paragraph", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextRuns3 = rdlcXML.CreateElement("TextRuns", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextRun3 = rdlcXML.CreateElement("TextRun", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xValue3 = rdlcXML.CreateElement("Value", rdlcXML.DocumentElement.NamespaceURI);
                if (i == 0)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Total") != null)
                            xValue3.InnerXml = resxSet.GetString("Total");
                        else
                            xValue3.InnerXml = "Total";
                    }
                }

                else if (dt.Columns[i].ColumnName.StartsWith("Column_") || dt.Columns[i].ColumnName.StartsWith("nrTraveler") || dt.Columns[i].ColumnName.StartsWith("Canceled"))
                {
                    xValue3.InnerXml = "=sum(Fields!" + dt.Columns[i].ColumnName + ".Value)";
                }
                xTextRun3.AppendChild(xValue3);
                XmlElement xTextRunStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xFontFamily3 = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                xFontFamily3.InnerXml = "Arial";
                xTextRunStyle3.AppendChild(xFontFamily3);
                XmlElement xFontSize3 = rdlcXML.CreateElement("FontSize", rdlcXML.DocumentElement.NamespaceURI);
                xFontSize3.InnerXml = "8pt";
                xTextRunStyle3.AppendChild(xFontSize3);
                xTextRun3.AppendChild(xTextRunStyle3);
                xTextRuns3.AppendChild(xTextRun3);
                XmlElement xStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextAlign3 = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
                xTextAlign3.InnerXml = "Center";
                xStyle3.AppendChild(xTextAlign3);
                xParagraph3.AppendChild(xTextRuns3);
                xParagraph3.AppendChild(xStyle3);
                xParagraphs3.AppendChild(xParagraph3);
                XmlElement xRD3 = rdlcXML.CreateElement("rd", "DefaultName", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
                xRD3.InnerXml = "Textbox" + (dt.Columns.Count + i + 7).ToString();
                XmlElement xMainStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xBorder3 = rdlcXML.CreateElement("Border", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xColor3 = rdlcXML.CreateElement("Color", rdlcXML.DocumentElement.NamespaceURI);
                xColor3.InnerXml = "LightGrey";
                xBorder3.AppendChild(xColor3);
                XmlElement xBorderStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                xBorderStyle3.InnerXml = "None";
                xBorder3.AppendChild(xBorderStyle3);
                xMainStyle3.AppendChild(xBorder3);
                XmlElement xBackgroundColor3 = rdlcXML.CreateElement("BackgroundColor", rdlcXML.DocumentElement.NamespaceURI);
                xBackgroundColor3.InnerXml = "LightGrey";
                xMainStyle3.AppendChild(xBackgroundColor3);
                XmlElement xPaddingLeft3 = rdlcXML.CreateElement("PaddingLeft", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingLeft3.InnerXml = "3pt";
                xMainStyle3.AppendChild(xPaddingLeft3);
                XmlElement xPaddingRight3 = rdlcXML.CreateElement("PaddingRight", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingRight3.InnerXml = "3pt";
                xMainStyle3.AppendChild(xPaddingRight3);
                XmlElement xPaddingTop3 = rdlcXML.CreateElement("PaddingTop", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingTop3.InnerXml = "3pt";
                xMainStyle3.AppendChild(xPaddingTop3);
                XmlElement xPaddingBottom3 = rdlcXML.CreateElement("PaddingBottom", rdlcXML.DocumentElement.NamespaceURI);
                xPaddingBottom3.InnerXml = "3pt";
                xMainStyle3.AppendChild(xPaddingBottom3);
                xTextBox3.AppendChild(xParagraphs3);
                xTextBox3.AppendChild(xRD3);
                xTextBox3.AppendChild(xMainStyle3);
                xCellContents3.AppendChild(xTextBox3);
                xCell3.AppendChild(xCellContents3);
                xRowCellsHeader3.AppendChild(xCell3);
                //


            }
            XmlElement xetb = rdlcXML.CreateElement("TablixBody", rdlcXML.DocumentElement.NamespaceURI);
            xetb.AppendChild(xC);
            xetb.AppendChild(xR);
            try
            {
                //Za brisanje nodova TablixBody
                if (rdlcXML.GetElementsByTagName("TablixBody") != null)
                    if (rdlcXML.GetElementsByTagName("TablixBody").Count > 0)
                    {

                        XmlNode xNList = rdlcXML.GetElementsByTagName("TablixBody").Item(0);

                        xNList.ParentNode.RemoveChild(xNList);
                    }
                if (rdlcXML.GetElementsByTagName("TablixColumnHierarchy") != null)
                    if (rdlcXML.GetElementsByTagName("TablixColumnHierarchy").Count > 0)
                    {

                        XmlNode xNList = rdlcXML.GetElementsByTagName("TablixColumnHierarchy").Item(0);

                        xNList.RemoveChild(xNList.FirstChild);
                    }
                //sum 
                if (rdlcXML.GetElementsByTagName("TablixRowHierarchy") != null)
                    if (rdlcXML.GetElementsByTagName("TablixRowHierarchy").Count > 0)
                    {

                        XmlNode xNList = rdlcXML.GetElementsByTagName("TablixRowHierarchy").Item(0);

                        xNList.RemoveChild(xNList.FirstChild);
                    }
                //

                //Brisanje Fields nodova
                if (rdlcXML.GetElementsByTagName("Fields") != null)
                    if (rdlcXML.GetElementsByTagName("Fields").Count > 0)
                    {
                        XmlNode xNList = rdlcXML.GetElementsByTagName("Fields").Item(0);
                        xNList.ParentNode.RemoveChild(xNList);
                    }


                //Za prevod fiksnih textBoxova date from i date to
                if (rdlcXML.GetElementsByTagName("Value") != null)
                    if (rdlcXML.GetElementsByTagName("Value").Count > 0)
                    {
                        XmlNodeList xNList = rdlcXML.GetElementsByTagName("Value");

                        foreach (XmlNode x in xNList)
                        {
                            if (x.InnerXml == "Date to" || x.InnerXml == "Date from")
                            {
                                //Za prevod
                                //
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString(x.InnerXml) != null)
                                        x.InnerXml = resxSet.GetString(x.InnerXml);
                                }
                            }
                        }
                    }
                //End
            }
            catch (Exception e)
            {

            }

            XmlNode xFds = rdlcXML.GetElementsByTagName("DataSet").Item(0);

            XmlNode xTablix = rdlcXML.GetElementsByTagName("Tablix").Item(0);
            //Za ponavljanje hedera
            XmlNode xRepeat2 = rdlcXML.GetElementsByTagName("RepeatColumnHeaders").Item(0);
            xRepeat2.InnerXml = "true";

            if (rdlcXML.GetElementsByTagName("Tablix") != null)
                if (rdlcXML.GetElementsByTagName("Tablix").Count > 0)
                {

                    XmlNode xNList = rdlcXML.GetElementsByTagName("Tablix").Item(0);
                    xNList.AppendChild(xRepeat2);
                }
            //

            XmlNode xTablixColumnHierarchy = rdlcXML.GetElementsByTagName("TablixColumnHierarchy").Item(0);

            xTablixColumnHierarchy.AppendChild(xTablixMembers);

            XmlNode xTablixRowHierarchy = rdlcXML.GetElementsByTagName("TablixRowHierarchy").Item(0);

            xTablixRowHierarchy.AppendChild(xTablixMembersRow);


            xTablix.AppendChild(xetb);
            xFds.AppendChild(xFields);

        }
    }
}
