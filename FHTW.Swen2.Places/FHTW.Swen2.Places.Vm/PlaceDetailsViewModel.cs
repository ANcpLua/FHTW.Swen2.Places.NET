using System;
using System.ComponentModel;
using System.Windows;

using SWEN2.Places.Model;



namespace FHTW.Swen2.Places.Vm
{
    /// <summary>This class provides a view model for the place details.</summary>
    internal class PlaceDetailsViewModel: INotifyPropertyChanged
    {
        /// <summary>Occurs when a property is changed.</summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>Parent view model.</summary>
        internal readonly MainViewModel _Parent;

        /// <summary>Place.</summary>
        private Place? _Place;


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal PlaceDetailsViewModel(MainViewModel parent)
        {
            _Parent = parent;
        }


        /// <summary>Shows a place in the detail view.</summary>
        /// <param name="place">Place.</param>
        public void Show(Place place)
        {
            _Place = place;

            // TODO: implement real logic
            MessageBox.Show(_Place.Name);
        }
    }
}
