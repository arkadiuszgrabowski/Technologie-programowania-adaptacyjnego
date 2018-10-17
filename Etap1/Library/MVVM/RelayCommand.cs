using System;
using System.Windows.Input;

namespace Library.MVVM
{
    public class RelayCommand : ICommand
    {

        #region constructors
        public RelayCommand(Action execute) : this(execute, null)
        { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            this.m_Execute = execute;
            this.m_CanExecute = canExecute;
        }
        #endregion

        #region ICommand
        public bool CanExecute(object parameter)
        {
            if (this.m_CanExecute == null)
                return true;
            if (parameter == null)
                return this.m_CanExecute();
            return this.m_CanExecute();
        }

        public virtual void Execute(object parameter)
        {
            this.m_Execute();
        }

        public event EventHandler CanExecuteChanged;
        #endregion

        #region API
        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region private
        private readonly Action m_Execute;
        private readonly Func<bool> m_CanExecute;
        #endregion

    }
}
