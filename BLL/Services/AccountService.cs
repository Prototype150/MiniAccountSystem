using BLL.Models;
using BLL.Services.Interfaces;
using System.Runtime.InteropServices;
using System.Security;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        public AccountModel Login(LoginCredentialsModel loginCredentials)
        {
            IntPtr valuePtr = IntPtr.Zero;

            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(loginCredentials.Password);
            string stringPassword = Marshal.PtrToStringUni(valuePtr) ?? "";

            if (string.IsNullOrWhiteSpace(loginCredentials.Username) || string.IsNullOrWhiteSpace(stringPassword))
                return null;


            //get AccountModel from database
            var p = new SecureString();
            p.AppendChar('t');
            p.AppendChar('e');
            p.AppendChar('s');
            p.AppendChar('t');

            return new AccountModel("test", p, "test");
        }

        public bool Register(RegisterCredentialsModel accountModel)
        {
            IntPtr valuePtr = IntPtr.Zero;

            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(accountModel.Password);
            string stringPassword = Marshal.PtrToStringUni(valuePtr) ?? "";

            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(accountModel.PasswordRepeate);
            string stringPasswordRepeate = Marshal.PtrToStringUni(valuePtr) ?? "";

            if (stringPassword != stringPasswordRepeate)
                return false;

            if (string.IsNullOrWhiteSpace(accountModel.Username) || string.IsNullOrWhiteSpace(accountModel.Email))
                return false;

            //Contacting Database

            return true;
        }
    }
}
