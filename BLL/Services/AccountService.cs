using BLL.Models;
using BLL.Services.Interfaces;
using System.Net.Http.Json;
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
                return false;

            var account = new { Username = accountModel.Username, Email = accountModel.Email, Password = p };
            string serializedAcc = JsonSerializer.Serialize(account);

            var r = await server.PostAsync("https://localhost:7296/ac/register", JsonContent.Create(serializedAcc));

            string accountSerialized = await r.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(accountSerialized);

            return result;
        }
    }
}
