﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IAccountService
    {
        AccountModel Login(LoginCredentialsModel loginCredentials);
        bool Register(RegisterCredentialsModel accountModel);
    }
}
