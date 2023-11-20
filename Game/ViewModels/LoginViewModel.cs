using BLL.Models;
using BLL.Services.Interfaces;
using Game.Commands;
using Game.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class LoginViewModel : BaseMainViewModel
    {
        private IAccountService _accountService;
        private AccountModel _currentAccount;
        public AccountModel CurrentAccount
        {
            get { return _currentAccount; }
            set 
            {
                _currentAccount = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentAccount)));
            }
        }

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(IAccountService accountService, AccountModel currentAccount)
        {
            _accountService = accountService;
            
            LoginCommand = new RelayCommand(Login);
            CurrentAccount = currentAccount;
        }

        private void Login(object obj)
        {
            var passBox = (IPasswordContainer)obj;
            var password = passBox.Password;

            var loginCred = new LoginCredentialsModel(Username, password);
            var acc = _accountService.Login(loginCred);

            if (acc != null)
            {
                CurrentAccount.Email = acc.Email;
                CurrentAccount.Username = acc.Username;
                ViewModelChanged(new PropertyChangedEventArgs("main"));
            }
        }
    }
}
