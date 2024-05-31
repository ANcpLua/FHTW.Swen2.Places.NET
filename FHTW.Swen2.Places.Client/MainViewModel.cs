using System.ComponentModel;
using System.Windows;

using FHTW.Swen2.Places.Model;



namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class implements the main view model for this application.</summary>
    public class MainViewModel: INotifyPropertyChanged
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Result box visibility flag.</summary>
        private Visibility _ResultBoxVisibility = Visibility.Hidden;

        private Visibility _PlaceControlVisibility = Visibility.Hidden;

        /// <summary>Search expression.</summary>
        private string _SearchExpression = "";

        private ButtonViewModel _Button1 = ButtonViewModel.EMPTY;
        private ButtonViewModel _Button2 = ButtonViewModel.EMPTY;
        private ButtonViewModel _Button3 = ButtonViewModel.EMPTY;


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        public MainViewModel()
        {
            ResultPage = new(this);
            PlaceDetails = new(this);

            SearchCommand = new(this);
            GenerateReportCommand = new(this);

            Button1 = new(true, "New", new NewPlaceCommand(PlaceDetails));

            Root.Init();
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [interface] INotifyPropertyChanged                                                                       //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Occurs when a property has changed.</summary>
        public event PropertyChangedEventHandler? PropertyChanged;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets the result page view model.</summary>
        public ResultPageViewModel ResultPage
        {
            get; private set;
        }


        /// <summary>Gets the place details view model.</summary>
        public PlaceDetailViewModel PlaceDetails
        {
            get; private set;
        }


        /// <summary>Gets or sets the visibility for the result box.</summary>
        public Visibility ResultBoxVisibility
        {
            get { return _ResultBoxVisibility; }
            set
            {
                if(_ResultBoxVisibility != value)
                {
                    _ResultBoxVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultBoxVisibility)));
                }
            }
        }


        /// <summary>Gets or sets the visibility for the place control.</summary>
        public Visibility PlaceControlVisibility
        {
            get { return _PlaceControlVisibility; }
            set
            {
                if(_PlaceControlVisibility != value)
                {
                    _PlaceControlVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceControlVisibility)));
                }
            }
        }


        /// <summary>Gets or sets the search expression string.</summary>
        public string SearchExpression
        {
            get { return _SearchExpression; }
            set
            {
                if(_SearchExpression != value)
                {
                    _SearchExpression = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchExpression)));
                }
            }
        }


        /// <summary>Gets or sets the view model for button 1.</summary>
        public ButtonViewModel Button1
        {
            get { return _Button1; }
            set
            {
                if(_Button1 != value)
                {
                    _Button1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Button1)));
                }
            }
        }


        /// <summary>Gets or sets the view model for button 2.</summary>
        public ButtonViewModel Button2
        {
            get { return _Button2; }
            set
            {
                if(_Button2 != value)
                {
                    _Button2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Button2)));
                }
            }
        }


        /// <summary>Gets or sets the view model for button 3.</summary>
        public ButtonViewModel Button3
        {
            get { return _Button3; }
            set
            {
                if(_Button3 != value)
                {
                    _Button3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Button3)));
                }
            }
        }


        /// <summary>Gets the search command.</summary>
        public SearchCommand SearchCommand
        {
            get; private set;
        }


        /// <summary>Gets the report generation command.</summary>
        public GenerateReportCommand GenerateReportCommand
        {
            get; private set;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public methods                                                                                           //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Shows nothing on the main window.</summary>
        public void ShowNothing()
        {
            ResultBoxVisibility = PlaceControlVisibility = Visibility.Hidden;
        }


        /// <summary>Shows the search result page.</summary>
        public void ShowSearchResults()
        {
            ResultBoxVisibility = Visibility.Visible;
            PlaceControlVisibility = Visibility.Hidden;
        }


        /// <summary>Shows the place details page.</summary>
        public void ShowPlace()
        {
            PlaceControlVisibility = Visibility.Visible;
            ResultBoxVisibility = Visibility.Hidden;
        }
    }
}
