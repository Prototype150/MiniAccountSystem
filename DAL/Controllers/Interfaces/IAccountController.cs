using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Controllers.Interfaces
{
    public interface IAccountController
    {
        Task<AccountModel> Login(LoginCredentialsModel loginCredentials);
        Task<bool> Register(RegisterCredentialsModel registerCredentials);
    }
}
