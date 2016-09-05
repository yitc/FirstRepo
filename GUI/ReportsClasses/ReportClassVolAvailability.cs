﻿using System;
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

    class ReportClassVolAvailability : System.Windows.Forms.UserControl
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
                    //XmlNamespaceManager ns = new XmlNamespaceManager(rdlcXML.NameTable);
                    // This appears to be a reserved default?
                    //ns.AddNamespace("def", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
                    //ns.AddNamespace("xmlns:rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");

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
            rdlcXML.Save("GUI.VolAvailabilityReport.rdlc");
        }

        private void addTable(XmlDocument rdlcXML)
        {

            XmlElement xTablixMembers = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
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

            XmlElement xC = rdlcXML.CreateElement("TablixColumns", rdlcXML.DocumentElement.NamespaceURI);
            
            //Definicija Noda za Fields
            XmlElement xFields = rdlcXML.CreateElement("Fields", rdlcXML.DocumentElement.NamespaceURI);

            //Sakrivanje kolona iz datatable
            dt.Columns.Remove("idLabel");
            dt.Columns.Remove("idContPers");
            dt.Columns.Remove("DateFrom");
            dt.Columns.Remove("DateTo");

         



            for (int i = 0; i < dt.Columns.Count; i++)
            //for (int i = 0; i < 2; i++)
            {
                
                //Fields
                XmlElement xDataField = rdlcXML.CreateElement("DataField", rdlcXML.DocumentElement.NamespaceURI);
                xDataField.InnerXml = dt.Columns[i].ColumnName;
                XmlElement xField = rdlcXML.CreateElement("Field", rdlcXML.DocumentElement.NamespaceURI);
                //xField.SetAttribute("Name", "Field" + i);

               

                //if (dt.Columns[i].Caption != null && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                //{
                //    xField.SetAttribute("Name", dt.Columns[i].Caption);
                //}
                //else
                //{
                    xField.SetAttribute("Name", dt.Columns[i].ColumnName);
                //}

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

                if (dt.Columns[i].ColumnName != "nameLabel")
                {
                    XmlElement xTablixMember = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixMembers.AppendChild(xTablixMember);
                    //columns part with width
                    XmlElement xColumn = rdlcXML.CreateElement("TablixColumn", rdlcXML.DocumentElement.NamespaceURI);
                    xC.AppendChild(xColumn);
                    //rows part header
                    //XmlElement xCells = rdlcXML.CreateElement("TablixCells");
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
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(new AvailabilitySkillsModel());

                    //za sirinu kolona
                    decimal wColumn = 0;
                    //

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        // ovo treba ispraviti ide column name ne display name jer datatable nema display name (ovo je samo u modelu)
                        if ((properties.Find(dt.Columns[i].ColumnName, true) != null))
                        {
                            if (resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName) != null)

                                xValue.InnerXml = resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName);
                        }

                            //Za prevod provera da li postoji caption ( kod dinamickih kolona)
                        else
                        {
                            if (dt.Columns[i].Caption != null)
                            {
                                if (resxSet.GetString(dt.Columns[i].Caption) != null)

                                    xValue.InnerXml = resxSet.GetString(dt.Columns[i].Caption);
                                else
                                {
                                    xValue.InnerXml = dt.Columns[i].Caption;
                                }
                            }
                            else
                            {
                                xValue.InnerXml = dt.Columns[i].ColumnName;

                            }

                        }

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
                        //Deo za sakrivanje kolona u XML-u
                        else
                        {
                            wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 35) * (decimal)0.0138889;
                        }
                        //
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
                    xFontFamily.InnerXml = "Verdana";
                    xTextRunStyle.AppendChild(xFontFamily);
                    XmlElement xFontSize = rdlcXML.CreateElement("FontSize", rdlcXML.DocumentElement.NamespaceURI);
                    xFontSize.InnerXml = "9pt";
                    xTextRunStyle.AppendChild(xFontSize);
                    XmlElement xFontWeight = rdlcXML.CreateElement("FontWeight", rdlcXML.DocumentElement.NamespaceURI);
                    xFontWeight.InnerXml = "Bold";
                    xTextRunStyle.AppendChild(xFontWeight);
                    xTextRun.AppendChild(xTextRunStyle);
                    xTextRuns.AppendChild(xTextRun);
                    XmlElement xStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextAlign = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
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
                    xPaddingLeft.InnerXml = "2pt";
                    xMainStyle.AppendChild(xPaddingLeft);
                    XmlElement xPaddingRight = rdlcXML.CreateElement("PaddingRight", rdlcXML.DocumentElement.NamespaceURI);
                    xPaddingRight.InnerXml = "2pt";
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
                    //XmlElement xCells2 = rdlcXML.CreateElement("TablixCells");
                    XmlElement xCell2 = rdlcXML.CreateElement("TablixCell", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xCellContents2 = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextBox2 = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);

                    //if (dt.Columns[i].Caption != null && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                    //{
                    //    xTextBox2.SetAttribute("Name", dt.Columns[i].Caption);
                    //}

                    //else
                    //{
                    xTextBox2.SetAttribute("Name", dt.Columns[i].ColumnName);
                    //}

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
                    //XmlElement dtF = rdlcXML.CreateElement("dtF", rdlcXML.DocumentElement.NamespaceURI);

                    //Prikaz vrednosti 
                    //if (dt.Columns[i].Caption != null && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                    //{
                    //    xValue2.InnerXml = "=Fields!" + dt.Columns[i].Caption + ".Value";
                    //    //dtF.InnerXml = "=Fields!" + dt.Columns[i].Caption + ".Value";
                    //}
                    //else
                    //{
                    xValue2.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";
                    //dtF.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";
                    //}
                    //xFields.InnerXml = dt.Columns[i].ColumnName;

                    xTextRun2.AppendChild(xValue2);
                    //xTextRun2.AppendChild(dtF);
                    XmlElement xTextRunStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xFontFamily2 = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                    xFontFamily2.InnerXml = "Verdana";
                    xTextRunStyle2.AppendChild(xFontFamily2);
                    xTextRun2.AppendChild(xTextRunStyle2);
                    xTextRuns2.AppendChild(xTextRun2);
                    XmlElement xStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextAlign2 = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
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
                    //XmlElement xBackgroundColor2 = rdlcXML.CreateElement("BackgroundColor");
                    //xBackgroundColor2.InnerXml = "LightGrey";
                    //xMainStyle2.AppendChild(xBackgroundColor2);
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

                    //if (rdlcXML.GetElementsByTagName("TablixCell") != null)
                    //    if (rdlcXML.GetElementsByTagName("TablixCell").Count > 0)
                    //    {
                    //        XmlNode xNList = rdlcXML.GetElementsByTagName("TablixCell").Item(0).ChildNodes[0].ChildNodes[0].ChildNodes[2];
                    //        //xNList.RemoveChild(xNList);

                    //        xTextBox.AppendChild(xNList);
                    //        XmlNode xNList2 = rdlcXML.GetElementsByTagName("TablixCell").Item(0);
                    //        //xNList2.RemoveChild(xNList2);
                    //        xTextBox2.AppendChild(xNList2);
                    //    }

                    xTextBox2.AppendChild(xParagraphs2);
                    xTextBox2.AppendChild(xRD2);
                    xTextBox2.AppendChild(xMainStyle2);
                    xCellContents2.AppendChild(xTextBox2);
                    xCell2.AppendChild(xCellContents2);
                    xRowCellsHeader2.AppendChild(xCell2);
                    xRow2.AppendChild(xRowCellsHeader2);

                    //rows part 
                    //XmlElement xCells2 = rdlcXML.CreateElement("TablixCells");
                    XmlElement xCell3 = rdlcXML.CreateElement("TablixCell", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xCellContents3 = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextBox3 = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);
                    xTextBox3.SetAttribute("Name", "Textbox" + (dt.Columns.Count + i).ToString());
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

                    else if (dt.Columns[i].ColumnName.StartsWith("Column_"))
                    {
                        string str = "\"x\"";
                        xValue3.InnerXml = "=sum(IIF(Fields!" + dt.Columns[i].ColumnName + ".Value=" + str + ",1,0))";
                    }
                    xTextRun3.AppendChild(xValue3);
                    XmlElement xTextRunStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xFontFamily3 = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                    xFontFamily3.InnerXml = "Verdana";
                    xTextRunStyle3.AppendChild(xFontFamily3);
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
                    xRD3.InnerXml = "Textbox" + (dt.Columns.Count + i).ToString();
                    XmlElement xMainStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xBorder3 = rdlcXML.CreateElement("Border", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xBorderStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    xBorderStyle3.InnerXml = "None";
                    xBorder3.AppendChild(xBorderStyle3);
                    xMainStyle3.AppendChild(xBorder3);
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

                    
                }
                else
                {
                    XmlElement xTablixMembersG = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMemberG = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMemberHeader = rdlcXML.CreateElement("TablixHeader", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMembersInto = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixKeepWithGroup = rdlcXML.CreateElement("KeepWithGroup", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixKeepWithGroup.InnerXml = "After";
                    XmlElement xTablixMemberInto = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixMembersInto.AppendChild(xTablixMemberInto);
                    XmlElement xTablixMemberSize = rdlcXML.CreateElement("Size", rdlcXML.DocumentElement.NamespaceURI);
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


                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(new DeviceReportModel());

                    //za sirinu kolona
                    decimal wColumn = 0;
                    //

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        // ovo treba ispraviti ide column name ne display name jer datatable nema display name (ovo je samo u modelu)
                        if ((properties.Find(dt.Columns[i].ColumnName, true) != null))
                        {
                            if (resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName) != null)

                                xValue.InnerXml = resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName);
                        }

                            //Za prevod provera da li postoji caption ( kod dinamickih kolona)
                        else
                        {
                            if (dt.Columns[i].Caption != null)
                            {
                                if (resxSet.GetString(dt.Columns[i].Caption) != null)

                                    xValue.InnerXml = resxSet.GetString(dt.Columns[i].Caption);
                                else
                                {
                                    xValue.InnerXml = dt.Columns[i].Caption;
                                }
                            }
                            else
                            {
                                xValue.InnerXml = dt.Columns[i].ColumnName;

                            }

                        }


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
                        else
                        {
                            wColumn = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 35) * (decimal)0.0138889;
                        }
                        //
                    }

                    XmlElement xColumnWidth = rdlcXML.CreateElement("Width", rdlcXML.DocumentElement.NamespaceURI);
                    //za sirinu kolona
                    // this need to add that column defines txt from translated column header and turn it to inches
                    if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator != ".")
                        xTablixMemberSize.InnerXml = wColumn.ToString().Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".") + "in";
                    else
                        xTablixMemberSize.InnerXml = wColumn.ToString() + "in";
                    //

                    xTextRun.AppendChild(xValue);
                    XmlElement xTextRunStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xFontFamily = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                    xFontFamily.InnerXml = "Verdana";
                    xTextRunStyle.AppendChild(xFontFamily);
                    XmlElement xFontSize = rdlcXML.CreateElement("FontSize", rdlcXML.DocumentElement.NamespaceURI);
                    xFontSize.InnerXml = "9pt";
                    xTextRunStyle.AppendChild(xFontSize);
                    XmlElement xFontWeight = rdlcXML.CreateElement("FontWeight", rdlcXML.DocumentElement.NamespaceURI);
                    xFontWeight.InnerXml = "Bold";
                    xTextRunStyle.AppendChild(xFontWeight);
                    xTextRun.AppendChild(xTextRunStyle);
                    xTextRuns.AppendChild(xTextRun);
                    XmlElement xStyle = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextAlign = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
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
                    xPaddingLeft.InnerXml = "2pt";
                    xMainStyle.AppendChild(xPaddingLeft);
                    XmlElement xPaddingRight = rdlcXML.CreateElement("PaddingRight", rdlcXML.DocumentElement.NamespaceURI);
                    xPaddingRight.InnerXml = "2pt";
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
                    xTablixMemberHeader.AppendChild(xCellContents);

                    xTablixMemberHeader.AppendChild(xTablixMemberSize);
                    xTablixMemberG.AppendChild(xTablixMembersInto);
                    xTablixMemberG.AppendChild(xTablixMemberHeader);
                    xTablixMemberG.AppendChild(xTablixKeepWithGroup);
                    XmlElement xTablixMemberG2 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMemberHeader2 = rdlcXML.CreateElement("TablixHeader", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixGroup2 = rdlcXML.CreateElement("Group", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixGroup2.SetAttribute("Name", dt.Columns[i].ColumnName);
                    XmlElement xTablixGroupExpressions2 = rdlcXML.CreateElement("GroupExpressions", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixGroupExpression2 = rdlcXML.CreateElement("GroupExpression", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixGroupExpression2.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";
                    xTablixGroupExpressions2.AppendChild(xTablixGroupExpression2);
                    xTablixGroup2.AppendChild(xTablixGroupExpressions2);
                    XmlElement xTablixSortExpressions2 = rdlcXML.CreateElement("SortExpressions", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixSortExpression2 = rdlcXML.CreateElement("SortExpression", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixSortExpressionValue = rdlcXML.CreateElement("Value", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixSortExpressionValue.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";
                    xTablixSortExpression2.AppendChild(xTablixSortExpressionValue);
                    xTablixSortExpressions2.AppendChild(xTablixSortExpression2);
                    XmlElement xTablixMemberSize2 = rdlcXML.CreateElement("Size", rdlcXML.DocumentElement.NamespaceURI);
                    if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator != ".")
                        xTablixMemberSize2.InnerXml = wColumn.ToString().Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".") + "in";
                    else
                        xTablixMemberSize2.InnerXml = wColumn.ToString() + "in";
                    //
                    xTablixMemberHeader2.AppendChild(xTablixMemberSize2);
                    XmlElement xCellContents2 = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextBox2 = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);

                    //if (dt.Columns[i].Caption != null && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                    //{
                    //    xTextBox2.SetAttribute("Name", dt.Columns[i].Caption);
                    //}

                    //else
                    //{
                    xTextBox2.SetAttribute("Name", dt.Columns[i].ColumnName);
                    //}

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
                    //XmlElement dtF = rdlcXML.CreateElement("dtF", rdlcXML.DocumentElement.NamespaceURI);

                    //Prikaz vrednosti 
                    //if (dt.Columns[i].Caption != null && !dt.Columns[i].ColumnName.StartsWith("Column_"))
                    //{
                    //    xValue2.InnerXml = "=Fields!" + dt.Columns[i].Caption + ".Value";
                    //    //dtF.InnerXml = "=Fields!" + dt.Columns[i].Caption + ".Value";
                    //}
                    //else
                    //{
                    xValue2.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";
                    //dtF.InnerXml = "=Fields!" + dt.Columns[i].ColumnName + ".Value";
                    //}
                    //xFields.InnerXml = dt.Columns[i].ColumnName;

                    xTextRun2.AppendChild(xValue2);
                    //xTextRun2.AppendChild(dtF);
                    XmlElement xTextRunStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xFontFamily2 = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                    xFontFamily2.InnerXml = "Verdana";
                    xTextRunStyle2.AppendChild(xFontFamily2);
                    xTextRun2.AppendChild(xTextRunStyle2);
                    xTextRuns2.AppendChild(xTextRun2);
                    XmlElement xStyle2 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextAlign2 = rdlcXML.CreateElement("TextAlign", rdlcXML.DocumentElement.NamespaceURI);
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

                    XmlElement xTablixMembersInto2 = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMemberInto2 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement Group2 = rdlcXML.CreateElement("Group", rdlcXML.DocumentElement.NamespaceURI);
                    Group2.SetAttribute("Name", "Details");
                    xTablixMemberInto2.AppendChild(Group2);
                    xTablixMembersInto2.AppendChild(xTablixMemberInto2);
                    xTablixMemberHeader2.AppendChild(xCellContents2);
                    xTablixMemberG2.AppendChild(xTablixSortExpressions2);
                    xTablixMemberG2.AppendChild(xTablixGroup2);
                    xTablixMemberG2.AppendChild(xTablixMembersInto2);
                    xTablixMemberG2.AppendChild(xTablixMemberHeader2);
                    xTablixMembersG.AppendChild(xTablixMemberG);
                    xTablixMembersG.AppendChild(xTablixMemberG2);



                    XmlElement xTablixMemberG3 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMemberHeader3 = rdlcXML.CreateElement("TablixHeader", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixMembersInto3 = rdlcXML.CreateElement("TablixMembers", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTablixKeepWithGroup3 = rdlcXML.CreateElement("KeepWithGroup", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixKeepWithGroup3.InnerXml = "After";
                    XmlElement xTablixMemberInto3 = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                    xTablixMembersInto3.AppendChild(xTablixMemberInto3);
                    XmlElement xTablixMemberSize3 = rdlcXML.CreateElement("Size", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xCellContents3 = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xTextBox3= rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);
                    xTextBox3.SetAttribute("Name", "TextBox" +(dt.Columns.Count+ dt.Columns.Count+i).ToString());
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
                      //za sirinu kolona
                    decimal wColumn3 = 0;
                    //

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Total", true) != null)

                            xValue3.InnerXml = resxSet.GetString("Total", true);
                        else
                            xValue3.InnerXml = "Total";

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
                            wColumn3 = (decimal)(this.CreateGraphics().MeasureString(LongestWordInArray, this.Font).Width + 35) * (decimal)0.0138889;
                        }
                        else
                        {
                            wColumn3 = (decimal)(this.CreateGraphics().MeasureString(xValue.InnerXml, this.Font).Width + 35) * (decimal)0.0138889;
                        }
                        //
                    }

                    XmlElement xColumnWidth3 = rdlcXML.CreateElement("Width", rdlcXML.DocumentElement.NamespaceURI);
                    //za sirinu kolona
                    // this need to add that column defines txt from translated column header and turn it to inches
                    if (CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator != ".")
                        xTablixMemberSize3.InnerXml = wColumn3.ToString().Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".") + "in";
                    else
                        xTablixMemberSize3.InnerXml = wColumn3.ToString() + "in";
                    //

                    xTextRun3.AppendChild(xValue3);
                    XmlElement xTextRunStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xFontFamily3 = rdlcXML.CreateElement("FontFamily", rdlcXML.DocumentElement.NamespaceURI);
                    xFontFamily3.InnerXml = "Verdana";
                    xTextRunStyle3.AppendChild(xFontFamily3);
                    XmlElement xFontSize3 = rdlcXML.CreateElement("FontSize", rdlcXML.DocumentElement.NamespaceURI);
                    xFontSize3.InnerXml = "9pt";
                    xTextRunStyle3.AppendChild(xFontSize3);
                    XmlElement xFontWeight3 = rdlcXML.CreateElement("FontWeight", rdlcXML.DocumentElement.NamespaceURI);
                    xFontWeight3.InnerXml = "Bold";
                    xTextRunStyle3.AppendChild(xFontWeight3);
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
                    xRD3.InnerXml = "TextBox" + (dt.Columns.Count + dt.Columns.Count + i).ToString();
                    XmlElement xMainStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xBorder3 = rdlcXML.CreateElement("Border", rdlcXML.DocumentElement.NamespaceURI);
                    XmlElement xColor3 = rdlcXML.CreateElement("Color", rdlcXML.DocumentElement.NamespaceURI);
                    xColor3.InnerXml = "LightGrey";
                    xBorder3.AppendChild(xColor3);
                    XmlElement xBorderStyle3 = rdlcXML.CreateElement("Style", rdlcXML.DocumentElement.NamespaceURI);
                    xBorderStyle3.InnerXml = "Solid";
                    xBorder3.AppendChild(xBorderStyle3);
                    xMainStyle3.AppendChild(xBorder3);
                    XmlElement xBackgroundColor3 = rdlcXML.CreateElement("BackgroundColor", rdlcXML.DocumentElement.NamespaceURI);
                    xBackgroundColor3.InnerXml = "LightGrey";
                    xMainStyle3.AppendChild(xBackgroundColor3);
                    XmlElement xPaddingLeft3 = rdlcXML.CreateElement("PaddingLeft", rdlcXML.DocumentElement.NamespaceURI);
                    xPaddingLeft3.InnerXml = "2pt";
                    xMainStyle3.AppendChild(xPaddingLeft3);
                    XmlElement xPaddingRight3 = rdlcXML.CreateElement("PaddingRight", rdlcXML.DocumentElement.NamespaceURI);
                    xPaddingRight3.InnerXml = "2pt";
                    xMainStyle3.AppendChild(xPaddingRight3);
                    XmlElement xPaddingTop3 = rdlcXML.CreateElement("PaddingTop", rdlcXML.DocumentElement.NamespaceURI);
                    xPaddingTop3.InnerXml = "2pt";
                    xMainStyle3.AppendChild(xPaddingTop3);
                    XmlElement xPaddingBottom3 = rdlcXML.CreateElement("PaddingBottom", rdlcXML.DocumentElement.NamespaceURI);
                    xPaddingBottom3.InnerXml = "2pt";
                    xMainStyle3.AppendChild(xPaddingBottom3);
                    xTextBox3.AppendChild(xParagraphs3);
                    xTextBox3.AppendChild(xRD3);
                    xTextBox3.AppendChild(xMainStyle3);
                    xCellContents3.AppendChild(xTextBox3);
                    xTablixMemberHeader3.AppendChild(xCellContents3);

                    xTablixMemberHeader3.AppendChild(xTablixMemberSize3);
                    xTablixMemberG3.AppendChild(xTablixMembersInto3);
                    xTablixMemberG3.AppendChild(xTablixMemberHeader3);
                    xTablixMemberG3.AppendChild(xTablixKeepWithGroup3);
                    xTablixMembersG.AppendChild(xTablixMemberG3);

                    // za grupisanje
                    if (rdlcXML.GetElementsByTagName("TablixRowHierarchy") != null)
                        if (rdlcXML.GetElementsByTagName("TablixRowHierarchy").Count > 0)
                        {

                            XmlNode xNList = rdlcXML.GetElementsByTagName("TablixRowHierarchy").Item(0);

                            xNList.RemoveChild(xNList.FirstChild);
                        }
                    XmlNode xTablixRowHierarchy = rdlcXML.GetElementsByTagName("TablixRowHierarchy").Item(0);
                    xTablixRowHierarchy.AppendChild(xTablixMembersG);


                }


            }
            XmlElement xetb = rdlcXML.CreateElement("TablixBody", rdlcXML.DocumentElement.NamespaceURI);
            // table body appends rows and columns
            xetb.AppendChild(xC);
            xetb.AppendChild(xR);
            // xR.AppendChild(xFields);
            //Table appends table body
            //XmlElement xe = rdlcXML.CreateElement("Tablix", rdlcXML.DocumentElement.NamespaceURI);
            //xe.SetAttribute("Name","Tablix22");
            //xe.AppendChild(xetb);
            //XmlNode xNList = rdlcXML.GetElementsByTagName("ReportItems").Item(0);
            //xNList.AppendChild(xe);
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



                //Brisanje Fields nodova
                if (rdlcXML.GetElementsByTagName("Fields") != null)
                    if (rdlcXML.GetElementsByTagName("Fields").Count > 0)
                    {
                        XmlNode xNList = rdlcXML.GetElementsByTagName("Fields").Item(0);
                        xNList.ParentNode.RemoveChild(xNList);
                    }

            }
            catch (Exception e)
            {

            }

            XmlNode xFds = rdlcXML.GetElementsByTagName("DataSet").Item(0);

            XmlNode xTablix = rdlcXML.GetElementsByTagName("Tablix").Item(0);

            XmlNode xTablixColumnHierarchy = rdlcXML.GetElementsByTagName("TablixColumnHierarchy").Item(0);

            xTablixColumnHierarchy.AppendChild(xTablixMembers);
            xTablix.AppendChild(xetb);
            xFds.AppendChild(xFields);

        }
    }
}
