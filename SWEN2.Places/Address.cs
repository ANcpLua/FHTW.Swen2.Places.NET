using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN2.Places
{
    /// <summary>This class represents an address.</summary>
    public record class Address(string Street,
                                string Code,
                                string Town,
                                string Country): ILocation
    {
        /// <summary>Creates a new instance of this clas.</summary>
        public Address(): this(string.Empty, string.Empty, string.Empty, string.Empty)
        {}
    }
}
