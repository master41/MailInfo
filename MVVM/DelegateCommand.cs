using System;
using System.Windows.Input;

namespace MVVM
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;

        public DelegateCommand(Action _action)
        {
            action = _action;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
