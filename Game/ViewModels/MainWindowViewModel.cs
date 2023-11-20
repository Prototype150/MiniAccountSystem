using BLL.Models;
using BLL.Services.Interfaces;
using Game.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IAccountService _accountService;
        private AccountModel CurrentAccount { get; set; }
        public MainWindowViewModel(IAccountService accountService)
        {
            _accountService = accountService;

            CurrentAccount = new AccountModel(); 
            
            SwitchViewsCommand = new RelayCommand(SwitchViews);
            SwitchViews("register");
        }

        private BaseMainViewModel _currentViewModel;
        public BaseMainViewModel CurrentViewModel 
        { 
            get { return _currentViewModel; }
            set 
            { 
                _currentViewModel = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentViewModel)));
            }
        }

        public ICommand SwitchViewsCommand { get; set; }

        private void SwitchViews(object obj)
        {
            switch((string)obj)
            {
                case "login":
                    CurrentViewModel = new LoginViewModel(_accountService, CurrentAccount);
                    _currentViewModel.OnViewModelChange += ChangeViewModel;
                    break;
                case "register":
                    CurrentViewModel = new RegisterViewModel(_accountService, CurrentAccount);
                    _currentViewModel.OnViewModelChange += ChangeViewModel;
                    break;
                case "main":
                    CurrentViewModel = new MainViewModel(CurrentAccount);
                    _currentViewModel.OnViewModelChange += ChangeViewModel;
                    break;
            }
        }

        private void ChangeViewModel(object? sender, PropertyChangedEventArgs e)
        {
            SwitchViews(e.PropertyName);
        }
    }
}
