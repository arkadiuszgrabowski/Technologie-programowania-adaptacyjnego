using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Library.TreeView
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }
        public void Execute(object parameter)
        {
            _action();
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
