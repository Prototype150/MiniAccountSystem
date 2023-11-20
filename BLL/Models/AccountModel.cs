using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AccountModel : LoginCredentialsModel
    {
        public AccountModel(string username = "", SecureString password = null, string email = ""):base(username, password)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}