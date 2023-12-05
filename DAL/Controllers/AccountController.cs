using BLL.Models;
using DAL.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Controllers
{
    public class ServerRequestException : Exception
    {
        public ServerRequestException(string message) : base(message) { }
    }

    public class AccountController : IAccountController
    {
        private HttpClient _httpClient;

        private class UnsafeAccount
        {
            public string Username { get; private set; }
            public string Password { get; private set; }

            public UnsafeAccount(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }

        private class UnsafeRegisterAccount : UnsafeAccount
        {
            public string Email { get; private set; }
            public UnsafeRegisterAccount(string username, string password, string email) : base(username, password) 
            {
                Email = email;
            }
        }

        public async Task<AccountModel> Login(LoginCredentialsModel loginCredentials)
        {
            using (_httpClient = new HttpClient())
            {
                var response = await _httpClient.GetAsync("https://localhost:7296/ac/login/" + JsonSerializer.Serialize(new UnsafeAccount(loginCredentials.Username, Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(loginCredentials.Password)))));

                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new ServerRequestException(errorMessage);
                }

                else if (response.StatusCode != HttpStatusCode.OK)
                    throw new ServerRequestException("Something went wrong");

                string serializedAccount = await response.Content.ReadAsStringAsync();
                AccountModel account = JsonSerializer.Deserialize<AccountModel>(serializedAccount);

                return account;
            }
        }

        public async Task<bool> Register(RegisterCredentialsModel registerCredentials)
        {
            using (_httpClient = new HttpClient())
            {
                UnsafeRegisterAccount accountDetailes = new UnsafeRegisterAccount(registerCredentials.Username, Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(registerCredentials.Password)), registerCredentials.Email);
                var serializedAccount = JsonSerializer.Serialize(accountDetailes);

                var response = await _httpClient.PostAsync("https://localhost:7296/ac/register", JsonContent.Create(serializedAccount));

                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorMesssage = await response.Content.ReadAsStringAsync();
                    throw new ServerRequestException(errorMesssage);
                }

                else if (response.StatusCode != HttpStatusCode.OK)
                    throw new ServerRequestException("Something went wrong");

                string result = await response.Content.ReadAsStringAsync();
                return result == "ok";
            }
        }
    }
}
