using System;
using System.Windows.Input;



namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class implements a command to add places.</summary>
    public class NewPlaceCommand: ICommand
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Parent view model.</summary>
        internal PlaceDetailViewModel _Parent;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal NewPlaceCommand(PlaceDetailViewModel parent)
        {
            _Parent = parent;

            _Parent.PropertyChanged += (sender, e) =>
            { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
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
            return _Parent.Locked;
        }


        /// <summary>Executes the command.</summary>
        /// <param name="parameter">Paramter.</param>
        public void Execute(object? parameter)
        {
            _Parent._Parent.ShowPlace();
            _Parent.CreateNew();
        }
    }
}
