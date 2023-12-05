using BLL.Models;
using BLL.Services.Interfaces;
using DAL.Controllers;
using DAL.Controllers.Interfaces;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.Json;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountController _accountController;

        public AccountService(IAccountController accountController)
        {
            _accountController = accountController;
        }

        public async Task<AccountModel> Login(LoginCredentialsModel loginCredentials)
        {
            AccountModel account = await _accountController.Login(loginCredentials);

            if (string.IsNullOrWhiteSpace(account.Username) || account.Username != loginCredentials.Username)
                throw new ServerRequestException("Major problem detected. Please, contact support");

            return account;
        }

        public async Task<bool> Register(RegisterCredentialsModel accountModel)
        {
            if (Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(accountModel.Password)) != Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(accountModel.PasswordRepeate)))
                throw new ServerRequestException("Password does not match repeated password!");



            bool result = await _accountController.Register(accountModel);

            if (!result)
                throw new ServerRequestException("Something went wrong");

            return result;
        }
    }
}
