using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LoginModel
    {
        private string _loginButton;
        public string loginButton
        {
            get { return _loginButton; }
            set { _loginButton = value; }
        }

        private string _usernameNullText;
        public string usernameNullText
        {
            get { return _usernameNullText; }
            set { _usernameNullText = value; }
        }

        private string _passwordNullText;

        public string passwordNullText
        {
            get { return _passwordNullText; }
            set { _passwordNullText = value; }
        }
    }
}
