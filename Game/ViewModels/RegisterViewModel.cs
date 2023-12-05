using BLL.Models;
using BLL.Services.Interfaces;
using DAL.Controllers;
using Game.Commands;
using Game.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class RegisterViewModel : BaseMainViewModel
    {
        private IAccountService _accountService;

        private string _username;
        public string Username 
        {
            get { return _username; }
            set 
            {
                _username = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Username)));
            }
        }

        private string _email;
        public string Email
        {
            get
            { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        public ICommand RegisterCommand { get; set; }
        public RegisterViewModel(IAccountService accountService, AccountModel currentAccount) : base(currentAccount)
        {
            _accountService = accountService;

            RegisterCommand = new RelayCommand(Register);
        }

        private async void Register(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    ErrorMessage = "Username is empty";
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Email))
                {
                    ErrorMessage = "Email is empty";
                    return;
                }
                var passBox = ((IPasswordContainer password, IPasswordContainer passwordRepeate))obj;
                var password = passBox.password.Password;
                var passwordRepeate = passBox.passwordRepeate.Password;

                if(password == null || password.Length == 0)
                {
                    ErrorMessage = "Password is empty";
                    return;
                }

                else if(passwordRepeate == null || passwordRepeate.Length == 0)
                {
                    ErrorMessage = "Password repeate is empty";
                    return;
                }

                if (await _accountService.Register(new RegisterCredentialsModel(Username, password, Email, passwordRepeate)))
                {
                    var acc = await _accountService.Login(new LoginCredentialsModel(Username, password));

                    _currentAccount.Username = acc.Username;
                    _currentAccount.Email = acc.Email;
                    ViewModelChanged(new PropertyChangedEventArgs("main"));
                }
            }
            catch(ServerRequestException e)
            {
                if (e.Message == "username" || e.Message == "email")
                    ErrorMessage = e.Message + " already exists.";
                else
                    ErrorMessage = e.Message;
            }
        }
    }
}
