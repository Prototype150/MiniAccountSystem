using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ViewModels
{
    public class BaseMainViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler OnViewModelChange;
        protected AccountModel _currentAccount { get; private set; }
        public AccountModel CurrentAccount
        {
            get { return _currentAccount; }
        }

        public int ErrorMessageHeight
        {
            get
            {
                return string.IsNullOrWhiteSpace(ErrorMessage) ? 0 : 30;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessageHeight)));
            }
        }

        public BaseMainViewModel(AccountModel accountModel)
        {
            _currentAccount = accountModel;
        }

        protected void ViewModelChanged(PropertyChangedEventArgs e)
        {
            OnViewModelChange?.Invoke(this, e);
        }
    }
}
