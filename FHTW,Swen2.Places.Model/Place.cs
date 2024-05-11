using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SWEN2.Places.Model
{
    /// <summary>This class represents a place.</summary>
    [Table("PLACES")][PrimaryKey("ID")]
    public sealed class Place
    {
        /// <summary>Lazy loader.</summary>
        private ILazyLoader? _Lazy;

        /// <summary>Coordinates.</summary>
        private Coordinates? _Coordinates;

        /// <summary>Address.</summary>
        private Address? _Address;

        /// <summary>Stories.</summary>
        private List<Story>? _Stories = new();


        /// <summary>Creates a new instance of this class.</summary>
        public Place()
        {}


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="lazy">Lazy loader.</param>
        public Place(ILazyLoader lazy)
        {
            _Lazy = lazy;
        }


        [Column("LOCATION")]
        internal string? _Location
        {
            get
            {
                if(_Coordinates != null)
                {
                    return "cood://" + JsonSerializer.Serialize<Coordinates>(_Coordinates);
                }
                
                if(_Address != null)
                {
                    return "addr://" + JsonSerializer.Serialize<Address>(_Address);
                }

                return null;
            }
            set 
            { 
                if((value != null) && value.StartsWith("cood://"))
                {
                    _Coordinates = JsonSerializer.Deserialize<Coordinates>(value[7..]);
                }
                else if((value != null) && value.StartsWith("addr://"))
                {
                    _Address = JsonSerializer.Deserialize<Address>(value[7..]);
                }
                else { _Coordinates = null;  _Address = null; }
            }
        }


        /// <summary>Gets the place ID.</summary>
        [Key][Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID
        {
            get;
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

        /// <summary>Gets the stories for this place.</summary>
        public List<Story> Stories
        {
            get
            {
                if(_Lazy == null) { return _Stories!; }
                return _Lazy.Load(this, ref _Stories) ?? new();
            }
            private set { _Stories = value; }
        }

        /// <summary>Gets or sets the place location.</summary>
        [NotMapped]
        public ILocation? Location
        {
            get
            {
                return (ILocation?) _Coordinates ?? _Address;
            }
            set
            {
                if(value == null)
                {
                    _Coordinates = null; _Address = null;
                }
                else if(value is Coordinates)
                {
                    if(_Coordinates == ((Coordinates?) value)) return;
                    _Coordinates = (Coordinates?) value;
                    _Address = null;
                }
                else
                {
                    if(_Address == ((Address?) value)) return;
                    _Address = (Address?) value;
                    _Coordinates = null;
                }
            }
        }
    }
}
