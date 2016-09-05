namespace GUI
{
    using BIS.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class ReportDepartureList2 : Telerik.Reporting.Report
    {
        public ReportDepartureList2(DataTable dt, Color[] c, Image logo, DateTime dtFrom, DateTime dtTo, BindingList<ReportModel> sel, string typeForm, string labelForm, string btwForm, string nameReport,string themeForm)
        {
            //
            // Required for telerik Reporting designer support
            //
            try
            {
            InitializeComponent();

            
                //header part

                this.logoPicture.Value = ((object)(logo));


                string nameTranslate = nameReport.ToUpper();
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(nameTranslate) != null)
                        nameTranslate = resxSet.GetString(nameTranslate);
                }

                textBox21.Value = nameTranslate;
                textBox21.Style.BorderStyle.Default = BorderType.None;
                textBox21.Style.BorderColor.Default = Color.Black;
                textBox21.Style.BackgroundColor = c[6];
                textBox21.Style.Color = c[7];
                textBox21.Style.Font.Bold = false;
                textBox21.Style.Font.Name = "Arial";
                textBox21.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);

                string DateFrom = "Date from";
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {

                    //preselection part
                    if (resxSet.GetString(DateFrom) != null)
                        DateFrom = resxSet.GetString(DateFrom);
                }
                textBox7.Value = DateFrom;
                textBox7.Style.BorderStyle.Default = BorderType.None;
                textBox7.Style.BorderColor.Default = Color.Black;
                textBox7.Style.BackgroundColor = c[4];
                textBox7.Style.Color = c[5];
                textBox7.Style.Font.Bold = true;
                textBox7.Style.Font.Name = "Arial";
                textBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);

                string DateTo = "Date to";
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {

                    //preselection part
                    if (resxSet.GetString(DateTo) != null)
                        DateTo = resxSet.GetString(DateTo);
                }
                textBox8.Value = DateTo;
                textBox8.Style.BorderStyle.Default = BorderType.None;
                textBox8.Style.BorderColor.Default = Color.Black;
                textBox8.Style.BackgroundColor = c[4];
                textBox8.Style.Color = c[5];
                textBox8.Style.Font.Bold = true;
                textBox8.Style.Font.Name = "Arial";
                textBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);

                if (typeForm != "")
                {
                    string type = "Type";
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {

                        //preselection part
                        if (resxSet.GetString(type) != null)
                            type = resxSet.GetString(type);
                    }
                    textBox18.Value = type;
                    textBox18.Style.BorderStyle.Default = BorderType.None;
                    textBox18.Style.BorderColor.Default = Color.Black;
                    textBox18.Style.BackgroundColor = c[4];
                    textBox18.Style.Color = c[5];
                    textBox18.Style.Font.Bold = true;
                    textBox18.Style.Font.Name = "Arial";
                    textBox18.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                {
                    textBox18.Visible = false;
                }

                if (labelForm != "")
                {
                    string label = "Label";
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {

                        //preselection part
                        if (resxSet.GetString(label) != null)
                            label = resxSet.GetString(label);
                    }
                    textBox17.Value = label;
                    textBox17.Style.BorderStyle.Default = BorderType.None;
                    textBox17.Style.BorderColor.Default = Color.Black;
                    textBox17.Style.BackgroundColor = c[4];
                    textBox17.Style.Color = c[5];
                    textBox17.Style.Font.Bold = true;
                    textBox17.Style.Font.Name = "Arial";
                    textBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                {
                    textBox17.Visible = false;
                }

                if (btwForm != "")
                {
                    string btw = "BTW";
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {

                        //preselection part
                        if (resxSet.GetString(btw) != null)
                            btw = resxSet.GetString(btw);
                    }
                    textBox19.Value = btw;
                    textBox19.Style.BorderStyle.Default = BorderType.None;
                    textBox19.Style.BorderColor.Default = Color.Black;
                    textBox19.Style.BackgroundColor = c[4];
                    textBox19.Style.Color = c[5];
                    textBox19.Style.Font.Bold = true;
                    textBox19.Style.Font.Name = "Arial";
                    textBox19.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                    textBox19.Visible = false;


                //Theme

                if (themeForm != "")
                {
                    string theme = "Theme";
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {

                        //preselection part
                        if (resxSet.GetString(theme) != null)
                            theme = resxSet.GetString(theme);
                    }
                    textBox23.Value = theme;
                    textBox23.Style.BorderStyle.Default = BorderType.None;
                    textBox23.Style.BorderColor.Default = Color.Black;
                    textBox23.Style.BackgroundColor = c[4];
                    textBox23.Style.Color = c[5];
                    textBox23.Style.Font.Bold = true;
                    textBox23.Style.Font.Name = "Arial";
                    textBox23.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                    textBox23.Visible = false;


                textBox9.Value = dtFrom.ToString("dd-MM-yyyy");
                textBox9.Style.BorderStyle.Default = BorderType.None;
                textBox9.Style.BorderColor.Default = Color.Black;
                textBox9.Style.BackgroundColor = c[6];
                textBox9.Style.Color = c[7];
                textBox9.Style.Font.Bold = false;
                textBox9.Style.Font.Name = "Arial";
                textBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);

                textBox10.Value = dtTo.ToString("dd-MM-yyyy");
                textBox10.Style.BorderStyle.Default = BorderType.None;
                textBox10.Style.BorderColor.Default = Color.Black;
                textBox10.Style.BackgroundColor = c[6];
                textBox10.Style.Color = c[7];
                textBox10.Style.Font.Bold = false;
                textBox10.Style.Font.Name = "Arial";
                textBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);

                if (typeForm != "")
                {
                    textBox16.Value = typeForm;
                    textBox16.Style.BorderStyle.Default = BorderType.None;
                    textBox16.Style.BorderColor.Default = Color.Black;
                    textBox16.Style.BackgroundColor = c[6];
                    textBox16.Style.Color = c[7];
                    textBox16.Style.Font.Bold = false;
                    textBox16.Style.Font.Name = "Arial";
                    textBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                {
                    textBox16.Visible = false;
                }

                if (labelForm != "")
                {
                    textBox15.Value = labelForm;
                    textBox15.Style.BorderStyle.Default = BorderType.None;
                    textBox15.Style.BorderColor.Default = Color.Black;
                    textBox15.Style.BackgroundColor = c[6];
                    textBox15.Style.Color = c[7];
                    textBox15.Style.Font.Bold = false;
                    textBox15.Style.Font.Name = "Arial";
                    textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                {
                    textBox15.Visible = false;
                }

                if (btwForm != "")
                {
                    textBox20.Value = btwForm;
                    textBox20.Style.BorderStyle.Default = BorderType.None;
                    textBox20.Style.BorderColor.Default = Color.Black;
                    textBox20.Style.BackgroundColor = c[6];
                    textBox20.Style.Color = c[7];
                    textBox20.Style.Font.Bold = false;
                    textBox20.Style.Font.Name = "Arial";
                    textBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                    textBox20.Visible = false;

                //Theme

                if (themeForm != "")
                {
                    textBox22.Value = themeForm;
                    textBox22.Style.BorderStyle.Default = BorderType.None;
                    textBox22.Style.BorderColor.Default = Color.Black;
                    textBox22.Style.BackgroundColor = c[6];
                    textBox22.Style.Color = c[7];
                    textBox22.Style.Font.Bold = false;
                    textBox22.Style.Font.Name = "Arial";
                    textBox22.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
                }
                else
                    textBox22.Visible = false;

                table1.ColumnGroups.Clear();
                table1.DataSource = dt;
                table1.Body.Columns.Clear();
                table1.Body.Rows.Clear();

                table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm)));
                table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm)));

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Telerik.Reporting.HtmlTextBox txtGroupHeaderCell = new Telerik.Reporting.HtmlTextBox();
                    Telerik.Reporting.HtmlTextBox txtGroupTotalCell = new Telerik.Reporting.HtmlTextBox();
                    Telerik.Reporting.HtmlTextBox txtTableRowCell = new Telerik.Reporting.HtmlTextBox();
                    var tableGroupColumn = new TableGroup();
                    table1.ColumnGroups.Add(tableGroupColumn);


                    string ColumnName = dt.Columns[i].Caption;
                    System.Drawing.Font f = new System.Drawing.Font("Arial", 8);
                    string value = "=Fields." + dt.Columns[i].ColumnName;
                    string valueTotal = "Total";
                    string valueSubtotal = "Subtotal";
                    string valueColorTotal = "Total";

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {

                        if (resxSet.GetString(dt.Columns[i].Caption) != null)
                            ColumnName = resxSet.GetString(dt.Columns[i].Caption);

                        if (resxSet.GetString(valueTotal) != null)
                            valueTotal = resxSet.GetString(valueTotal);

                        if (resxSet.GetString(valueSubtotal) != null)
                            valueSubtotal = resxSet.GetString(valueSubtotal);

                        if (resxSet.GetString(valueColorTotal) != null)
                            valueColorTotal = resxSet.GetString(valueColorTotal);

                    }


                    if (ColumnName.Contains(" "))
                    {
                        if (dt.Columns[i].DataType == typeof(DateTime))
                        {
                            value = "=Format('{0:dd/MM/yyyy}',Fields." + dt.Columns[i].ColumnName + ")";
                        }
                    }
                    else
                    {
                        if (dt.Columns[i].DataType == typeof(DateTime))
                        {
                            value = "=Format('{0:dd/MM/yyyy}',Fields." + dt.Columns[i].ColumnName + ")";
                        }
                    }

                    decimal wColumn = 0;
                    ReportModel rp = new ReportModel();
                    rp = sel.FirstOrDefault(s => s.idColumn == dt.Columns[i].ColumnName);
                    if (rp != null)
                        wColumn = rp.widthColumn;


                    txtGroupHeaderCell = new HtmlTextBox
                    {
                        Size = new SizeU(Unit.Inch((double)wColumn), Unit.Inch(0.3)),
                        Value = ColumnName,
                        Style =
                        {
                            BorderStyle = { Default = BorderType.None },
                            BorderColor = { Default = Color.Black },
                            BackgroundColor = c[0],
                            Color = c[1],
                            Font = { Bold = true, Name = "Arial", Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point) }
                        },
                    };


                    tableGroupColumn.ReportItem = txtGroupHeaderCell;

                    txtTableRowCell = new HtmlTextBox()
                    {
                        Size = new SizeU(Unit.Inch((double) wColumn), Unit.Inch(0.3)),
                        Value = value,
                        CanGrow = false,
                        Style =
                        {
                            BorderStyle = { Default = BorderType.None },
                            BorderColor = { Default = Color.Black },
                            BackgroundColor = c[2],
                            Color = c[3],
                            Font = { Bold = false, Name = "Arial", Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point) }
                        }
                    };

                    if (dt.Columns[i].DataType == typeof(int) || dt.Columns[i].DataType == typeof(decimal))
                    {
                        txtTableRowCell.Style.TextAlign = HorizontalAlign.Center;
                        txtGroupHeaderCell.Style.TextAlign = HorizontalAlign.Center;

                    }

                    //added cell on row for values
                    table1.Body.SetCellContent(0, columnIndex: i, item: txtTableRowCell);


                    if (dt.Columns[i].ColumnName == "Occupancy")
                    {
                        if (dt.Columns.Contains("nr") && dt.Columns.Contains("nrTraveler"))
                            valueTotal = "=Format('{0:N2}',IIF(SUM(Fields.nr)=0 OR  SUM(Fields.nrTraveler)=0,0,cDbl(SUM(Fields.nr))/cDbl(SUM(Fields.nrTraveler))*100))";
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You don't have both columns selected columns for maximum number and booked.");
                            valueTotal = "0";
                        }
                    }

                    else if (i != 0 && (dt.Columns[i].DataType == typeof(int) || dt.Columns[i].DataType == typeof(decimal)))
                        valueTotal = "=SUM(Fields." + dt.Columns[i].ColumnName + ")";
                    else if (i != 0)
                        valueTotal = "";

                     
                   
                    
                      
                        //txtTableRowCell.Style.BackgroundColor = "=IIF(Fields!arrangement.Value =\"" + valueColorTotal + "\" OR Fields!arrangement.Value =\"" + valueSubtotal + "\" ,\"LightGrey\",\"Transparent\")";
                        //txtTableRowCell.Style.BorderStyle.Default = BorderType.Solid;

                    //if (checkColumnExist(dt,"Label"))
                   // {
                        txtTableRowCell.Bindings.Add(new Telerik.Reporting.Binding("Style.BackgroundColor", "=IIF(Fields.Label =\"" + valueColorTotal + "\" OR Fields.Label =\"" + valueSubtotal + "\" , \"LightGray\",\"Transparent\" )"));
                       //txtTableRowCell.Bindings.Add(new Telerik.Reporting.Binding("Style.Font.Bold", "=IIF(Fields.Label =\"" + valueColorTotal + "\" OR Fields.Label =\"" + valueSubtotal + "\")"));
                  
                    //}
                   // else if (checkColumnExist(dt,"arrangement"))
                   // {
                      //  txtTableRowCell.Bindings.Add(new Telerik.Reporting.Binding("Style.BackgroundColor", "=IIF(Fields.arrangement =\"" + valueColorTotal + "\" OR Fields.arrangement =\"" + valueSubtotal + "\" ,\"LightGray\",\"Transparent\"  )"));

                   // }


                        //Expresion for exclusive                   
                        //txtTableRowCell.Bindings.Add(new Telerik.Reporting.Binding("Style.BackgroundColor", "=IIF(Fields.arrangement =\"" + valueColorTotal + "\" OR Fields.arrangement =\"" + valueSubtotal + "\" ,\"LightGray\",\"Transparent\")"));

                    //txtTableRowCell.Bindings.Add(new Telerik.Reporting.Binding("Style.BackgroundColor", "=IIF(Fields.label =\"" + valueColorTotal + "\" OR Fields.label =\"" + valueSubtotal + "\" ,\"LightGray\",\"Transparent\")"));

                    


                    //txtGroupTotalCell = new HtmlTextBox
                    //{

                    //    Size = new SizeU(Unit.Inch((double)wColumn), Unit.Inch(0.3)),
                    //    Value = valueTotal,
                    //    Style =
                    //    {
                    //        BorderStyle = { Default = BorderType.None },
                    //        BorderColor = { Default = Color.Black },
                    //        BackgroundColor = c[0],
                    //        Color = c[1],
                    //        TextAlign = HorizontalAlign.Center,
                    //        Font = { Bold = true, Name = "Arial", Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point) }
                    //    },
                    //};
                    

                    //so that Total cell first will be aligned left
                    if (i == 0)
                    {
                        txtGroupTotalCell.Style.TextAlign = HorizontalAlign.Left;
                    }


                    //added cell on row for total
                    table1.Body.SetCellContent(1, columnIndex: i, item: txtGroupTotalCell);
                }
            }
            catch(Exception e)
            {

            }
        }

            private Boolean checkColumnExist(DataTable dt, string columnName)
            {
                Boolean exist = false;
                if(dt!=null)
                    if(dt.Columns.Count>0)
                    {
                        for(int i =0; i < dt.Columns.Count; i++)
                            if(dt.Columns[i].ColumnName==columnName)
                            {
                                exist = true;
                                break;
                            }

                    }
                return exist;
            }


    }
}