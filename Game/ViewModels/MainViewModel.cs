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
        public MainViewModel(AccountModel currentAccount):base(currentAccount)
        {
            
        }
    }
}
