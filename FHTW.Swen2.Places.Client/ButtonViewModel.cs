using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;



namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class implements a view model for adaptive buttons.</summary>
    public class ButtonViewModel: INotifyPropertyChanged
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public constants                                                                                         //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>An empty button view model.</summary>
        public static readonly ButtonViewModel EMPTY = new ButtonViewModel(false);



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="visible">Button visible flag.</param>
        /// <param name="text">Button text.</param>
        /// <param name="command">Button command.</param>
        public ButtonViewModel(bool visible, string text = "", ICommand? command = null)
        {
            Visibility = (visible ? Visibility.Visible : Visibility.Hidden);
            Text = text;
            Command = (command ?? new _EmptyCommand());

            PropertyChanged?.Invoke(this, new(nameof(Visibility)));
            PropertyChanged?.Invoke(this, new(nameof(Text)));
            PropertyChanged?.Invoke(this, new(nameof(Command)));
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Gets the button visibility.</summary>
        public Visibility Visibility
        {
            get; private set;
        }


        /// <summary>Gets the button text.</summary>
        public string Text
        {
            get; private set;
        }


        /// <summary>Gets the button command.</summary>
        public ICommand Command
        {
            get; private set;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [interface] INotifyPropertyChanged                                                                       //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Occurs when a property value has changed.</summary>
        public event PropertyChangedEventHandler? PropertyChanged;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [class] _EmptyCommand                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private class _EmptyCommand: ICommand
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            // [interface] ICommand                                                                                 //
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            /// <summary>Occurs when the CanExecute() result has changed.</summary>
            public event EventHandler? CanExecuteChanged;


            /// <summary>Returns if the command can be executed.</summary>
            /// <param name="parameter">Parameter.</param>
            /// <returns>Returns TRUE if the command can be executed, otherwise returns FALSE.</returns>
            public bool CanExecute(object? parameter)
            {
                return false;
            }


            /// <summary>Executes the command.</summary>
            /// <param name="parameter">Parameter.</param>
            public void Execute(object? parameter)
            {}



            
        }
    }
}
