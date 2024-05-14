using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using SWEN2.Places.Model;

namespace FHTW.Swen2.Places.Vm
{
    /// <summary>This class implements the search command.</summary>
    internal class SearchCommand: ICommand
    {
        /// <summary>Parent view model.</summary>
        internal MainViewModel _Parent;


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal SearchCommand(MainViewModel parent)
        {
            _Parent = parent;
            _Parent.PropertyChanged += (sender, e) => 
            { 
                if(e.PropertyName == nameof(_Parent.SearchExpression)) 
                { 
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty); 
                }
            };
        }



        /// <summary>Occurs when the CanExecute property has changed.</summary>
        public event EventHandler? CanExecuteChanged;


        /// <summary>Determines if a command can be executed.</summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Returns TRUE if the command can be executed, otherwise returns FALSE.</returns>
        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_Parent.SearchExpression);
        }


        /// <summary>Executes the command.</summary>
        /// <param name="parameter">Parameter.</param>
        public void Execute(object? parameter)
        {
            _Parent.ResultPage.SearchResults.Clear();

            foreach(Place i in Root.Context.SearchPlaces(_Parent.SearchExpression))
            {
                _Parent.ResultPage.SearchResults.Add(new(_Parent.ResultPage, i));
            }
            _Parent.ShowSearchResults();
        }
    }
}
