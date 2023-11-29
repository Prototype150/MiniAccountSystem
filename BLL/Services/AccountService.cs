using BLL.Models;
using BLL.Services.Interfaces;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.Json;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        public AccountModel Login(LoginCredentialsModel loginCredentials)
        {
            return new AccountModel(loginCredentials.Username, null, "test");
        }

        public async Task<bool> Register(RegisterCredentialsModel accountModel)
        {
            HttpClient server = new HttpClient();

            string p = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(accountModel.Password));
            string pr = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(accountModel.PasswordRepeate));

            if (p != pr)
                throw new Exception("Password does not match password repeat");

            var account = new { Username = accountModel.Username, Email = accountModel.Email, Password = p };
            string serializedAccount = JsonSerializer.Serialize(account);

            var r = await server.GetAsync("https://localhost:7296/ac/getaccount/" + serializedAccount);
            string accountSerialized = await r.Content.ReadAsStringAsync();
            AccountModel acc = JsonSerializer.Deserialize<AccountModel>(accountSerialized);

            return true;
        }
    }
}
