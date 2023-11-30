using BLL.Models;
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
    public class MainViewModel : BaseMainViewModel
    {
        public ICommand LogOutCommand { get; set; }
        
        public MainViewModel(AccountModel currentAccount):base(currentAccount)
        {
            LogOutCommand = new RelayCommand(LogOut);
        }

        private void LogOut(object obj)
        {
            ViewModelChanged(new PropertyChangedEventArgs("login"));
        }
    }
}
