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

namespace GUI
{

    class TranslateReport
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
                catch(Exception e)
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
            rdlcXML.Save("GUI.ReportPassangerSelection.rdlc");
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
            XmlElement xC = rdlcXML.CreateElement("TablixColumns", rdlcXML.DocumentElement.NamespaceURI);
            for(int i = 0; i<dt.Columns.Count;i++)
            //for (int i = 0; i < 2; i++)
            {
                XmlElement xTablixMember = rdlcXML.CreateElement("TablixMember", rdlcXML.DocumentElement.NamespaceURI);
                xTablixMembers.AppendChild(xTablixMember);
                //columns part with width
                XmlElement xColumn = rdlcXML.CreateElement("TablixColumn", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xColumnWidth = rdlcXML.CreateElement("Width", rdlcXML.DocumentElement.NamespaceURI);
                // this need to add that column defines txt from translated column header and turn it to inches
                xColumnWidth.InnerXml = "1.52083in";
                xColumn.AppendChild(xColumnWidth);
                xC.AppendChild(xColumn);
                //rows part header
                //XmlElement xCells = rdlcXML.CreateElement("TablixCells");
                XmlElement xCell = rdlcXML.CreateElement("TablixCell", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xCellContents = rdlcXML.CreateElement("CellContents", rdlcXML.DocumentElement.NamespaceURI);
                XmlElement xTextBox = rdlcXML.CreateElement("Textbox", rdlcXML.DocumentElement.NamespaceURI);
                xTextBox.SetAttribute("Name", "TextBox"+i);
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
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(new PersonModel());
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    // ovo treba ispraviti ide column name ne display name jer datatable nema display name (ovo je samo u modelu)

                    if (resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName) != null)

                        xValue.InnerXml = resxSet.GetString(properties.Find(dt.Columns[i].ColumnName, true).DisplayName);
                }

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
                XmlElement xRD = rdlcXML.CreateElement("rd", "DefaultName",  "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
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

            }
            XmlElement xetb = rdlcXML.CreateElement("TablixBody", rdlcXML.DocumentElement.NamespaceURI);
            // table body appends rows and columns
            xetb.AppendChild(xC);
            xetb.AppendChild(xR);
            //Table appends table body
            //XmlElement xe = rdlcXML.CreateElement("Tablix", rdlcXML.DocumentElement.NamespaceURI);
            //xe.SetAttribute("Name","Tablix22");
            //xe.AppendChild(xetb);
            //XmlNode xNList = rdlcXML.GetElementsByTagName("ReportItems").Item(0);
            //xNList.AppendChild(xe);
            try
            {
               
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

                        xNList.RemoveChild(xNList);
                    }
            }
            catch (Exception e)
            {

            }
            XmlNode xTablix = rdlcXML.GetElementsByTagName("Tablix").Item(0);
            XmlNode xTablixColumnHierarchy = rdlcXML.GetElementsByTagName("TablixColumnHierarchy").Item(0);
            xTablixColumnHierarchy.AppendChild(xTablixMembers);
            xTablix.AppendChild(xetb);

           

        }
    }
}
