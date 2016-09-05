using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Business;

namespace GUI
{
    public partial class ChangePassword : Form
    {
        private string _idUser;
        private string _oldPassword;
        private UsersBUS _usersBUS;

        public ChangePassword(string iduser, string oldPassword)
        {
            _idUser = iduser;
            _oldPassword = oldPassword;
            _usersBUS = new UsersBUS();
            InitializeComponent();
        }

        private void radButtonSave_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPass.Text.TrimEnd();
            if (!(newPassword != "" && newPassword.Any(c => char.IsUpper(c)) && newPassword.Any(c => char.IsLower(c)) && newPassword.Any(c => char.IsNumber(c)) && (newPassword.Any(c => char.IsSymbol(c)) || newPassword.Any(c => char.IsPunctuation(c)))))
            {
                translateRadMessageBox trr = new translateRadMessageBox();
                trr.translateAllMessageBox("Not a valid password! Password needs to be atleast 8 characters long, contain one upercase letter, one lowrcase letter, one number and one symbol");
                return;
            }
            if (_usersBUS.ComparePassword(_oldPassword, txtNewPass.Text.TrimEnd()) == false)
            {
                if (_usersBUS.ChangePassword(_idUser, txtNewPass.Text.TrimEnd(), this.Name, Login._user.idUser) == false)
                {
                    MessageBox.Show("Something went wrong. Please contact your administrator!");
                }
                else
                {
                    MessageBox.Show("You have successfully change your password!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("You have to change your password!");
            }
        }
        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
