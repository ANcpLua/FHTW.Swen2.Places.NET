using System;
using System.Windows.Controls;
using System.Windows.Input;

using FHTW.Swen2.Places.Model;



namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class provides a search command.</summary>
    public class SearchCommand: ICommand
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Parent view model.</summary>
        private MainViewModel _Parent;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
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



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [interface] ICommand                                                                                     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Occures when the CanExecute() result has changed.</summary>
        public event EventHandler? CanExecuteChanged;


        /// <summary>Returns if the command can be executed.</summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Returns TRUE when the command can be executed, otherwise returns FALSE.</returns>
        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_Parent.SearchExpression);
        }


        /// <summary>Executes the command.</summary>
        /// <param name="parameter">Paramter.</param>
        public void Execute(object? parameter)
        {
            _Parent.ResultPage.SearchResults.Clear();

            foreach(Place i in Root.Db.SearchPlaces(_Parent.SearchExpression)) 
            { 
                _Parent.ResultPage.SearchResults.Add(new(_Parent.ResultPage, i));
            }
            
            _Parent.ShowSearchResults();

            _Parent.Button1 = new(true, "New", new NewPlaceCommand(_Parent.PlaceDetails));
            _Parent.Button2 = _Parent.Button3 = ButtonViewModel.EMPTY;
        }
    }
}
