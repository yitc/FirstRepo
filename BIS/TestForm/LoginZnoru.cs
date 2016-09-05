using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS;
using BIS.Model;
using BIS.Business;

namespace TestForm
{
    public partial class LoginZnoru : Form
    {
        private UsersBUS _usersBUS;
        private MenuBUS _menuBUS;
        public UsersModel _user;
        public List<MenuModel> _menuModelList;

        public LoginZnoru()
        {
            _usersBUS = new UsersBUS();
            _menuBUS = new MenuBUS();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _user = new UsersModel();
            _menuModelList = new List<MenuModel>();

            _user = _usersBUS.Login(textBoxUsername.Text, textBoxPassword.Text);

            if (_user == null)
            {
                MessageBox.Show("User not found");
            }
            else
            {
                MessageBox.Show(_user.nameUser);
                _menuModelList = _menuBUS.GetUserSecurityDetails(_user.idUser);

                if(_menuModelList != null)
                {
                    MessageBox.Show(_menuModelList.Count.ToString());
                }
                else
                {
                    MessageBox.Show("No security roles");
                }
            }
        }
    }
}
