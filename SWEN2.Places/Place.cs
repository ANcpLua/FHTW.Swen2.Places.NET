using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SWEN2.Places
{
    /// <summary>This class represents a place.</summary>
    public sealed class Place
    {
        /// <summary>Lazy loader.</summary>
        private ILazyLoader? _Lazy;

        /// <summary>Coordinates.</summary>
        private Coordinates? _Coordinates;

        /// <summary>Address.</summary>
        private Address? _Address;

        /// <summary>Stories.</summary>
        private List<Story> _Stories = new();


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
        private string? _Location
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
            }
        }
    }
}
