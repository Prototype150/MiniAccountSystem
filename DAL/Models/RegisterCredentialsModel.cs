using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RegisterCredentialsModel : AccountModel
    {
        public RegisterCredentialsModel(string username, SecureString password, string email, SecureString passwordRepeate = null) : base(username, password, email)
        {
            PasswordRepeate = passwordRepeate;
        }

        public SecureString PasswordRepeate { get; private set; }
    }
}
