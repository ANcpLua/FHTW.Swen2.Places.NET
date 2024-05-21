using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace FHTW.Swen2.Places.Vm
{
    /// <summary>This class implements a command for selecting a search result.</summary>
    internal class SelectResultCommand: ICommand
    {
        /// <summary>Occurs when the CanExecute result has changed.</summary>
        public event EventHandler? CanExecuteChanged;


        /// <summary>Parent search result (view model).</summary>
        internal SearchResultData _Parent;


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal SelectResultCommand(SearchResultData parent)
        {
            _Parent = parent;
        }


        /// <summary>Returns if the command can be executed.</summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Returns if the command can be executed.</returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }


        /// <summary>Executes the command.</summary>
        /// <param name="parameter">Parameter.</param>
        public void Execute(object? parameter)
        {
            _Parent._Parent._Parent.PlaceDetails.Show(_Parent.Place);
        }
    }
}