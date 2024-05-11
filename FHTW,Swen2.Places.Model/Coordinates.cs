using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN2.Places.Model
{
    /// <summary>This class represents coordinates.</summary>
    public record class Coordinates(double Latitude, double Longitude): ILocation
    {
        public Coordinates(): this(0, 0)
        {}
    }
}
