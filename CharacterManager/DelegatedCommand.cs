using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DarkSunProgramming.Commands
{
    public class DelegatedCommand : ICommand
    {
        private Action doDelegate;

        private Func<bool> verifier;

        /// <summary>
        /// Initializes a new instance of the DelegatedCommand class.
        /// </summary>
        /// <param name="addMethod">The method to perform.</param>
        /// <param name="canAddVerifier">The method that verifies, the command can be executed.</param>
        public DelegatedCommand(Action doMethod, Func<bool> verifier)
        {
            doDelegate = doMethod;
            this.verifier = verifier;
        }

        #region ICommand members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return (bool)verifier.Invoke();
        }

        public void Execute(object parameter)
        {
            doDelegate.Invoke();
        }

        #endregion
    }
}
