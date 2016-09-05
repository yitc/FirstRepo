using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class MessageBoxCalendar : RadForm
    {
        public DateTime selectedDate;
        private string title;
        private string question;
        public MessageBoxCalendar()
        {
            InitializeComponent();
            
        }
        public MessageBoxCalendar(string title, string question)
        {
            InitializeComponent(); 
            
            this.Text = title.ToString();
            lblMessage.Text = question.ToString();


        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            selectedDate = radDateTimePicker1.Value;
            this.DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void MessageBoxCalendar_Load(object sender, EventArgs e)
        {
            
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {  
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);
                
                if (resxSet.GetString(btnYes.Text) != null)
                    btnYes.Text = resxSet.GetString(btnYes.Text);
                if (resxSet.GetString(btnNo.Text) != null)
                    btnNo.Text = resxSet.GetString(btnNo.Text);
                if (resxSet.GetString(lblMessage.Text) != null)
                    lblMessage.Text = resxSet.GetString(lblMessage.Text);             
            }
            
            this.radDateTimePicker1.Value = DateTime.Now;
            selectedDate = radDateTimePicker1.Value;
        }
    }
}
