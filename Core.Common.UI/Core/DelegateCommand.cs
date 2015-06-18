using System;
using System.Windows.Input;

namespace Core.Common.UI.Core
{
    public class DelegateCommand<T> : ICommand
    {
        public DelegateCommand(Action<T> execute) : this(execute, null) { }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute) : this(execute, canExecute, "") { }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute, string label)
        {
            _execute = execute;
            _canExecute = canExecute;

            Label = label;
        }

        readonly Action<T> _execute;
        readonly Predicate<T> _canExecute;

        public string Label { get; set; }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }
    }
}
