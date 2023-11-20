using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _command;
        private Func<object, bool> _canExecute;
        public RelayCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _command?.Invoke(parameter);
        }
    }
}
