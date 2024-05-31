using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;



namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class represents a place.</summary>
    [Table("PLACES")][PrimaryKey("ID")]
    public class Place
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Lazy loader.</summary>
        private readonly ILazyLoader? _Lazy;

        /// <summary>Stories collection.</summary>
        private ICollection<Story>? _Stories = null;

        /// <summary>Location backing field.</summary>
        private ILocation? _BackingLocation = null;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        public Place()
        {}


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="lazy">Lazy loader.</param>
        private Place(ILazyLoader lazy)
        {
            _Lazy = lazy;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private properties                                                                                       //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Gets or sets the serialized loaction data.</summary>
        [Column("LOCATION")]
        internal string _Location
        {
            get
            {
                if(Location == null) { return ""; }
                if(Location is Coordinates)
                {
                    return "cood://" + JsonSerializer.Serialize((Coordinates) Location);
                }
                if(Location is Address)
                {
                    return "addr://" + JsonSerializer.Serialize((Address) Location);
                }

                return "";
            }
            set
            {
                if(value != null)
                {
                    if(value.StartsWith("cood://"))
                    {
                        Location = JsonSerializer.Deserialize<Coordinates>(value[7..]);
                        return;
                    }
                    if(value.StartsWith("addr://"))
                    {
                        Location = JsonSerializer.Deserialize<Address>(value[7..]);
                        return;
                    }
                }

                Location = null;
            }
        }


        /// <summary>Gets the image path.</summary>
        private string _ImagePath
        {
            get { return Root.Config.ImagePath.TrimEnd('\\') + @"\_m" + ID.ToString() + ".jpg"; }
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets the place ID.</summary>
        [Key][Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID
        {
            get; private set;
        }


        /// <summary>Gets or sets the place name.</summary>
        [Column("NAME")]
        public string Name
        {
            get; set;
        } = "";


        /// <summary>Gets or sets the place description.</summary>
        [Column("DESCRIPTION")]
        public string Description
        {
            get; set;
        } = "";


        /// <summary>Gets or sets the place location.</summary>
        [NotMapped]
        public ILocation? Location
        {
            get { return _BackingLocation; } 
            set 
            {
                _BackingLocation = value;
            }
        }


        /// <summary>Gets or sets the place story list.</summary>
        public ICollection<Story> Stories
        {
            get
            {
                if(_Lazy == null)
                {
                    if(_Stories == null) { _Stories = new List<Story>(); }
                    return _Stories;
                }

                return _Lazy.Load(this, ref _Stories) ?? (_Stories = new List<Story>());
            }
        }
    }
}