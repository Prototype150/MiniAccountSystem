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
        public async Task<AccountModel> Login(LoginCredentialsModel loginCredentials)
        {
            HttpClient server = new HttpClient();

            string p = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(loginCredentials.Password));
            var account = new { Username = loginCredentials.Username, Password = p };

            var response = await server.GetAsync("https://localhost:7296/ac/login/" + JsonSerializer.Serialize(account));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            string accountSerialized = await response.Content.ReadAsStringAsync();
            AccountModel result = JsonSerializer.Deserialize<AccountModel>(accountSerialized);

            return result;
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

            var response = await server.PostAsync("https://localhost:7296/ac/register", JsonContent.Create(serializedAcc));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return false;

            string accountSerialized = await response.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(accountSerialized);

            return result;
        }
    }
}
