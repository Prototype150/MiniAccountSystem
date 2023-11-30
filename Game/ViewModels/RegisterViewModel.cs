using BLL.Models;
using BLL.Services.Interfaces;
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
            var passBox = ((IPasswordContainer password, IPasswordContainer passwordRepeate))obj;
            var password = passBox.password.Password;
            var passwordRepeate = passBox.passwordRepeate.Password;

            if (await _accountService.Register(new RegisterCredentialsModel(Username, password, Email, passwordRepeate)))
            {
                var acc = await _accountService.Login(new LoginCredentialsModel(Username, password));

                _currentAccount.Username = acc.Username;
                _currentAccount.Email = acc.Email;
                ViewModelChanged(new PropertyChangedEventArgs("main"));
            }
        }
    }
}
