using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.DAO;
using BIS.Core;
using System.Configuration;
using System.Resources;
using System.Globalization;
using System.Reflection;
using Telerik.WinControls;
using BIS.Business;
using BIS.Model;


namespace GUI
{
     public partial class BookYear : UserControl
    {
        public string bookyear;
       
        public event EventHandler<BookingYearChangedEvent> BookingYearComboChanged = delegate { };
        public void RaiseBookingYearChanged(string bookingYear)
        {
            BookingYearComboChanged(this, new BookingYearChangedEvent { bookingYear = bookingYear });
        }

        public BookYear()
        {
            bookyear = Login._bookyear;
                      
            InitializeComponent();
         
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                lblBook.Text = resxSet.GetString("Booking year");
            }
            ddlBook.SelectedItem = ddlBook.Items[ddlBook.FindString(bookyear)]; 
        }

        public void ddlBook_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
           //// aa = new MainForm();
           // bookyear = Convert.ToString(ddlBook.SelectedItem.Text);
           // Login._bookyear = bookyear;

           // //Type type = typeof(MainForm);
           // //MethodInfo method = type.GetMethod("OnAccSettingsClick");
           // //method.Invoke(this, null);

           // AccSettingsBUS acb = new AccSettingsBUS();
           // List<AccSettingsModel> acm = new List<AccSettingsModel>();

           // acm = acb.GetAllAccSettings(Login._bookyear);
           // MainForm.gridViewAccSettings.SetDataPersonBinding1(acm); //modelData

          //  aa.OnAccSettingsClick();
            if (ddlBook.SelectedItem != null)
            {
                RaiseBookingYearChanged(ddlBook.SelectedItem.Text);
                Login._bookyear = ddlBook.SelectedItem.Text;
            }
        }
    }
     public class BookingYearChangedEvent : EventArgs
     {
         public string bookingYear { get; set; }
     }
}
