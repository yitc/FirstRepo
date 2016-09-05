using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI
{
    public partial class frmOpenLinesSendEmailForm : Telerik.WinControls.UI.RadForm
    {
        List<string> recipients;
        public string subject = "";
        public string message = "";

        public frmOpenLinesSendEmailForm(List<string> param)
        {
            recipients = new List<string>(param);
            
            InitializeComponent();
        }

        private void frmOpenLinesSendEmailForm_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;

            txtSubject.Text = "Beste , ";
            foreach(string s in recipients)
            {
                listRecipients.Items.Add(s);
            }

            SetTranslation();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

            this.subject = txtSubject.Text.Trim();
            this.message = txtMessage.Text.Trim();

            this.DialogResult = DialogResult.OK;
        } 
        
        private void SetTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(label1.Text) != null)
                    label1.Text = resxSet.GetString(label1.Text);
                if (resxSet.GetString(label2.Text) != null)
                    label2.Text = resxSet.GetString(label2.Text);
                if (resxSet.GetString(label3.Text) != null)
                    label3.Text = resxSet.GetString(label3.Text);

                if (resxSet.GetString(txtSubject.Text) != null)
                    txtSubject.Text = resxSet.GetString(txtSubject.Text);
                if (resxSet.GetString(txtMessage.Text) != null)
                    txtMessage.Text = resxSet.GetString(txtMessage.Text);
                if (resxSet.GetString(radButton1.Text) != null)
                    radButton1.Text = resxSet.GetString(radButton1.Text);
                
            }
        }
         
    }
}
