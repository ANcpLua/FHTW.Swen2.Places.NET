using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace FHTW.Swen2.Places.Vm
{
    /// <summary>This class provides the main view model.</summary>
    internal class MainViewModel: INotifyPropertyChanged
    {
        /// <summary>Occurs when a property has changed.</summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>Result box visibility.</summary>
        private Visibility _ResultBoxVisibility = Visibility.Hidden;

        /// <summary>Search expression.</summary>
        private string _SearchExpression = string.Empty;


        /// <summary>Creates a new instance of this class.</summary>
        public MainViewModel() 
        {
            ResultPage = new(this);
            SearchCommand = new(this);
        }


        /// <summary>Gets the result page view model.</summary>
        public ResultPageViewModel ResultPage
        { 
            get; 
            private init; 
        }


        /// <summary>Gets the result page view model.</summary>
        public SearchCommand SearchCommand
        { 
            get; 
            private init; 
        }


        /// <summary>Gets or set the result box visibility.</summary>
        public Visibility ResultBoxVisibility
        {
            get { return _ResultBoxVisibility; }
            set
            {
                if(_ResultBoxVisibility != value)
                {
                    _ResultBoxVisibility = value;
                    PropertyChanged?.Invoke(this, new(nameof(ResultBoxVisibility)));
                }
            }
        }


        /// <summary>Gets or set the search expression.</summary>
        public string SearchExpression
        {
            get { return _SearchExpression; }
            set
            {
                if(_SearchExpression != value)
                {
                    _SearchExpression = value;
                    PropertyChanged?.Invoke(this, new(nameof(SearchExpression)));
                }
            }
        }


        /// <summary>Shows the result box (view).</summary>
        public void ShowSearchResults()
        {
            ResultBoxVisibility = Visibility.Visible;
        }
    }
}
