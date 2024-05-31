using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FHTW.Swen2.Places.Model;



namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class implements a view model for the place details.</summary>
    public class PlaceDetailViewModel: INotifyPropertyChanged
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private static members                                                                                   //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Empty map image.</summary>
        private static readonly BitmapImage _EMPTY = _CreateEmpty();

        /// <summary>Gets the leaflet template.</summary>
        private static readonly string _LTEMPLATE = _GetLeafletTemplate();



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Place currently displayed.</summary>
        internal Place? _Place;

        /// <summary>Parent view model.</summary>
        internal readonly MainViewModel _Parent;

        private string _Name = "";
        private string _Description = "";
        private string _Latitude = "";
        private string _Longitude = "";
        private string _Street = "";
        private string _Code = "";
        private string _Town = "";
        private string _Country = "";

        /// <summary>Flag that controls if the address is being shown.</summary>
        private bool _AddressShowing;

        /// <summary>Edit lock flag.</summary>
        private bool _Locked = true;

        /// <summary>Editing border width.</summary>
        private int _EditingBorders = 0;

        /// <summary>Switch link label visibility.</summary>
        private Visibility _SwitchLinkVisibility = Visibility.Hidden;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal PlaceDetailViewModel(MainViewModel parent)
        {
            _Parent = parent;
            SwitchLocation = new(this);
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private static methods                                                                                   //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates an empty image.</summary>
        /// <returns>Image.</returns>
        private static BitmapImage _CreateEmpty()
        {
            BitmapImage rval = new();
            rval.BeginInit();
            rval.CacheOption = BitmapCacheOption.OnLoad;
            rval.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            rval.UriSource = new(Root.Config.ImagePath.Trim('\\') + @"\_empty.jpg");
            rval.EndInit();

            return rval;
        }


        /// <summary>Loads the leaflet source.</summary>
        /// <returns>Leaflet source.</returns>
        private static string _GetLeafletTemplate()
        {
            return File.ReadAllText("leaflet.html");
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets or sets the place name.</summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name != value ) 
                {
                    _Name = value;
                    PropertyChanged?.Invoke(this, new(nameof(Name)));
                    PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
                }
            }
        }


        /// <summary>Gets or sets the place description.</summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if(_Description != value)
                {
                    _Description = value;
                    PropertyChanged?.Invoke(this, new(nameof(Description)));
                }
            }
        }


        /// <summary>Gets or sets the latitude.</summary>
        public string Latitude
        {
            get { return _Latitude; }
            set
            {
                if(_Latitude != value)
                {
                    _Latitude = value;
                    PropertyChanged?.Invoke(this, new(nameof(Latitude)));
                    PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
                }
            }
        }


        /// <summary>Gets or sets the longitude.</summary>
        public string Longitude
        {
            get { return _Longitude; }
            set
            {
                if(_Longitude != value)
                {
                    _Longitude = value;
                    PropertyChanged?.Invoke(this, new(nameof(Longitude)));
                    PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
                }
            }
        }


        /// <summary>Gets or sets the street.</summary>
        public string Street
        {
            get { return _Street; }
            set
            {
                if(_Street != value)
                {
                    _Street = value;
                    PropertyChanged?.Invoke(this, new(nameof(Street)));
                    PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
                }
            }
        }


        /// <summary>Gets or sets the postal code.</summary>
        public string Code
        {
            get { return _Code; }
            set
            {
                if(_Code != value)
                {
                    _Code = value;
                    PropertyChanged?.Invoke(this, new(nameof(Code)));
                }
            }
        }


        /// <summary>Gets or sets the town.</summary>
        public string Town
        {
            get { return _Town; }
            set
            {
                if(_Town != value)
                {
                    _Town = value;
                    PropertyChanged?.Invoke(this, new(nameof(Town)));
                    PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
                }
            }
        }


        /// <summary>Gets or sets the country.</summary>
        public string Country
        {
            get { return _Country; }
            set
            {
                if(_Country != value)
                {
                    _Country = value;
                    PropertyChanged?.Invoke(this, new(nameof(Country)));
                    PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
                }
            }
        }


        /// <summary>Gets or sets if the view is locked for editing.</summary>
        public bool Locked
        {
            get { return _Locked; }
            set
            {
                if(_Locked != value)
                {
                    _Locked = value;
                    PropertyChanged?.Invoke(this, new(nameof(Locked)));
                }
            }
        }


        /// <summary>Gets or sets the editing border width.</summary>
        public int EditingBorders
        {
            get { return _EditingBorders; }
            set
            {
                if(_EditingBorders != value)
                {
                    _EditingBorders = value;
                    PropertyChanged?.Invoke(this, new(nameof(EditingBorders)));
                }
            }
        }


        /// <summary>Gets or sets if the switch link is visible.</summary>
        public Visibility SwitchLinkVisibility
        {
            get { return _SwitchLinkVisibility; }
            set
            {
                if(_SwitchLinkVisibility != value)
                {
                    _SwitchLinkVisibility = value;
                    PropertyChanged?.Invoke(this, new(nameof(SwitchLinkVisibility)));
                }
            }
        }


        /// <summary>Gets or sets if the address is shown.</summary>
        public bool AddressShowing
        {
            get { return _AddressShowing; }
            set
            {
                _AddressShowing = value;

                PropertyChanged?.Invoke(this, new(nameof(AddressShowing)));
                PropertyChanged?.Invoke(this, new(nameof(AddressVisibility)));
                PropertyChanged?.Invoke(this, new(nameof(CoordinatesVisibility)));
            }
        }


        /// <summary>Gets the map source.</summary>
        public Uri MapSrc
        {
            get 
            {
                Coordinates c;
                if(AddressShowing)
                {
                    MapData.ResolveAddress(Street, Code, Town, Country, out c);
                }
                else
                {
                    MapData.ResolveCoordinates(Latitude, Longitude, out c);
                }
                
                
                string src = _LTEMPLATE.Replace("$lat", c.Latitude.ToString().Replace(',', '.')).Replace("$lng", c.Longitude.ToString().Replace(',', '.'));
                var base64Html = Convert.ToBase64String(Encoding.UTF8.GetBytes(src));
                return new Uri($"data:text/html;base64,{base64Html}");
            }
        }


        /// <summary>Gets the address grid visiblility.</summary>
        public Visibility AddressVisibility
        {
            get { return (AddressShowing ? Visibility.Visible : Visibility.Hidden); }
        }


        /// <summary>Gets the coordinates grid visiblility.</summary>
        public Visibility CoordinatesVisibility
        {
            get { return (AddressShowing ? Visibility.Hidden: Visibility.Visible); }
        }


        /// <summary>Gets the switch location command.</summary>
        public SwitchLocationCommand SwitchLocation
        {
            get; private set;
        }


        /// <summary>Gets the stories.</summary>
        public ObservableCollection<StoryListData> Stories
        {
            get; private set;
        } = new();



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public methods                                                                                           //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Shows a place.</summary>
        /// <param name="place">Place.</param>
        public void Show(Place place)
        {
            _Place = place;

            Stories.Clear();
            foreach(Story i in _Place.Stories)
            {
                Stories.Add(new(this, i));
            }

            ResetData();

            PropertyChanged?.Invoke(this, new(nameof(AddressVisibility)));
            PropertyChanged?.Invoke(this, new(nameof(CoordinatesVisibility)));

            _Parent.ShowPlace();
        }


        /// <summary>Resets the data shown.</summary>
        public void ResetData()
        {
            if(_Place == null) return;

            Locked = true;
            EditingBorders = 0;
            SwitchLinkVisibility = Visibility.Hidden;

            Name = _Place.Name;
            Description = _Place.Description;

            AddressShowing = true;

            Street = Code = Town = Country = Latitude = Longitude = "";

            if(_Place.Location is Address)
            {
                Street = ((Address) _Place.Location).Street;
                Code = ((Address) _Place.Location).Code;
                Town = ((Address) _Place.Location).Town;
                Country = ((Address) _Place.Location).Country;
            }
            else if(_Place.Location is Coordinates) 
            { 
                Latitude = ((Coordinates) _Place.Location).Latitude.ToString();
                Longitude = ((Coordinates) _Place.Location).Longitude.ToString();

                AddressShowing = false;
            }

            _Parent.Button1 = new(true, "Edit", new EditPlaceCommand(this));
            _Parent.Button2 = new(true, "New", new NewPlaceCommand(this));
            _Parent.Button3 = ButtonViewModel.EMPTY;

            PropertyChanged?.Invoke(this, new(nameof(MapSrc)));
        }


        /// <summary>Returns if the place can be saved.</summary>
        /// <returns>Returns TRUE if the place can be saved, otherwise returns FALSE.</returns>
        public bool CanSave()
        {
            if(string.IsNullOrWhiteSpace(Name)) return false;

            if(AddressShowing)
            {
                if(!MapData.ResolveAddress(Street, Code, Town, Country, out _)) { return false; }
            }
            else if(!MapData.ResolveCoordinates(Latitude, Longitude, out _))
            {
                return false;
            }

            return true;
        }


        /// <summary>Saves the place.</summary>
        public void Save()
        {
            if(_Place == null) return;

            if(!Root.Db.Places.Contains(_Place))
            {
                Root.Db.Places.Add(_Place);
            }

            _Place.Name = Name;
            _Place.Description = Description;

            if(AddressShowing)
            {
                _Place.Location = new Address(Street, Town, Code, Country);
            }
            else { _Place.Location = new Coordinates(double.Parse(Latitude), double.Parse(Longitude)); }

            PropertyChanged?.Invoke(this, new(nameof(MapSrc)));

            Root.Db.SaveChanges();

            ResetData();
        }


        /// <summary>Starts editing mode.</summary>
        public void StartEdit()
        {
            Locked = false;
            EditingBorders = 1;
            SwitchLinkVisibility = Visibility.Visible;

            _Parent.Button1 = new(true, "Save", new SavePlaceCommand(this));
            _Parent.Button2 = new(true, "Cancel", new CancelEditPlaceCommand(this));
        }


        /// <summary>Cancels editing mode.</summary>
        public void CancelEdit()
        {
            ResetData();

            if(!Root.Db.Places.Contains(_Place))
            {
                _Place = null;
                _Parent.Button1 = new(true, "New", new NewPlaceCommand(this));
                _Parent.Button2 = _Parent.Button3 = ButtonViewModel.EMPTY;
                _Parent.ShowNothing();
            }
        }


        /// <summary>Creates a new place.</summary>
        public void CreateNew()
        {
            _Place = new();
            ResetData();
            StartEdit();
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [interface] INotifyPropertyChanged                                                                       //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Occurs when a property has changed.</summary>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
