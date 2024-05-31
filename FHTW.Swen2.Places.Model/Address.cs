using System;



namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class represents an address.</summary>
    /// <param name="Street">Street.</param>
    /// <param name="Town">Town.</param>
    /// <param name="Code">Postal code.</param>
    /// <param name="Country">Country.</param>
    public sealed record class Address(string Street, string Town, string Code, string Country): ILocation
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="street">Street.</param>
        /// <param name="town">Town.</param>
        /// <param name="code">Postal code.</param>
        /// <param name="country">Country.</param>
        public Address(): this("", "", "", "")
        {}
    }
}
