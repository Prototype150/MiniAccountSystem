using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ViewModels
{
    public class MainViewModel : BaseMainViewModel
    {
        public MainViewModel(AccountModel currentAccount)
        {
            CurrentAccount = currentAccount;
        }

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
    }
}
