using System;



namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class represents location coordinates.</summary>
    /// <param name="Latitude">Latitude.</param>
    /// <param name="Longitude">Longitude.</param>
    public sealed record class Coordinates(double Latitude, double Longitude): ILocation
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        Coordinates(): this(0, 0)
        {}
    }
}
