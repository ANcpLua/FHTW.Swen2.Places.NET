using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FHTW.Swen2.Places.Vm
{
    internal class ResultPageViewModel: INotifyPropertyChanged
    {
        /// <summary>Occurs when a property has changed.</summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>Parent view model.</summary>
        internal readonly MainViewModel _Parent;


        /// <summary>Result index.</summary>
        private int _ResultIndex = -1;


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal ResultPageViewModel(MainViewModel parent)
        {
            _Parent = parent;
        }


        /// <summary>Gets or set the result index.</summary>
        public int ResultIndex
        {
            get { return _ResultIndex; }
            set
            {
                if(_ResultIndex != value)
                {
                    _ResultIndex = value;
                    PropertyChanged?.Invoke(this, new(nameof(ResultIndex)));
                }
            }
        }


        /// <summary>Gets the search results.</summary>
        public ObservableCollection<SearchResultData> SearchResults
        {
            get;
        } = new();
    }
}
