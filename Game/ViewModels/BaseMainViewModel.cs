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

        protected void ViewModelChanged(PropertyChangedEventArgs e)
        {
            OnViewModelChange?.Invoke(this, e);
        }
    }
}
