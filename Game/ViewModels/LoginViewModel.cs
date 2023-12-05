using BLL.Models;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Controllers;
using Game.Commands;
using Game.Models.Interfaces;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class LoginViewModel : BaseMainViewModel
    {
        private IAccountService _accountService;

        

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

        public LoginViewModel(IAccountService accountService, AccountModel currentAccount) : base(currentAccount)
        {
            _accountService = accountService;
            
            LoginCommand = new RelayCommand(Login);
        }

        private async void Login(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    ErrorMessage = "Username field is empty!";
                    return;
                }

                var passBox = (IPasswordContainer)obj;
                var password = passBox.Password;
                if(password == null || password.Length == 0)
                {
                    ErrorMessage = "Password field is empty";
                    return;
                }

                var loginCred = new LoginCredentialsModel(Username, password);
                var acc = await _accountService.Login(loginCred);

                if (acc != null)
                {
                    CurrentAccount.Email = acc.Email;
                    CurrentAccount.Username = acc.Username;
                    ViewModelChanged(new PropertyChangedEventArgs("main"));
                }
            }
            catch(HttpRequestException)
            {
                ErrorMessage = "No server :(";
            }
            catch(ServerRequestException e)
            {
                if (e.Message == "login" || e.Message == "password")
                    ErrorMessage = "Wrong " + e.Message + "!";
                else
                    ErrorMessage = e.Message;
            }
        }
    }
}
