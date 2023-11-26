using BLL.Models;
using BLL.Services.Interfaces;
using Game.Commands;
using Game.Models.Interfaces;
using System.ComponentModel;
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
