using System;
using System.Windows.Input;

namespace PixelSort.App.Commands
{
    /// <summary>
    /// Relay command implementation.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Action to be executed.
        /// </summary>
        private readonly Action _action;

        /// <summary>
        /// Execution predicate will be called from CanExecute method.
        /// </summary>
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action action, Predicate<object> predicate)
        {
            _action = action;
            _canExecute = predicate;
        }

        /// <summary>
        /// Will be called when the CanExecute condition has changed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True if the predicate conditions are fulfilled</returns>
        public bool CanExecute(object parameter)
        {
            if (null == this._canExecute)
            {
                throw new InvalidOperationException(nameof(_canExecute));
            }

            return _canExecute(parameter);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action();
        }

        /// <summary>
        /// CanExecuteChanged event handler.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
