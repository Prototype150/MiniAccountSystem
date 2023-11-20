using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class LoginCredentialsModel
    {
        public LoginCredentialsModel(string username, SecureString password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public SecureString Password { get; set; }
    }
}
