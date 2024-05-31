using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;

namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class implements map functionality.</summary>
    public static class MapData
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private constants                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>MapQuest API key.</summary>
        private const string _KEY = "5b3ce3597851110001cf62482f5e55d721404adb954236144e1ae077";



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static methods                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Resolves an address.</summary>
        /// <param name="street">Street.</param>
        /// <param name="code">Postal code.</param>
        /// <param name="town">Town.</param>
        /// <param name="country">Country.</param>
        /// <param name="coordinates">Coordinates.</param>
        /// <returns>Returns TRUE if the address has been successfully resolved,
        ///          otherwise returns FALSE.</returns>
        public static bool ResolveAddress(string street, string code, string town, string country, out Coordinates coordinates)
        {
            return ResolveAddress(new Address(street, code, town, country), out coordinates);
        }


        /// <summary>Tries to resolve an address.</summary>
        /// <param name="address">Address.</param>
        /// <param name="coordinates">Result coordinates.</param>
        /// <returns>Returns TRUE if the address has been successfully resolved,
        ///          otherwise returns FALSE.</returns>
        public static bool ResolveAddress(Address address, out Coordinates coordinates)
        {
            double? lat = null, lng = null;
            bool rval = true;

            HttpClient cl = new();

            JsonDocument data = JsonDocument.Parse(cl.GetAsync($"https://api.openrouteservice.org/geocode/search?api_key={_KEY}&" +
                                $"text={HttpUtility.UrlEncode((address.Street + ", " + address.Code + ", " + address.Town + ", " + address.Country).Trim(' ', ','))}").Result.Content.ReadAsStringAsync().Result ?? "");

            if(data != null)
            {
                try
                {
                    lat = data.RootElement.GetProperty("features")[0].GetProperty("geometry").GetProperty("coordinates")[1].GetDouble();
                    lng = data.RootElement.GetProperty("features")[0].GetProperty("geometry").GetProperty("coordinates")[0].GetDouble();
                }
                catch(Exception) { rval = false; }
            }

            coordinates = new(lat ?? 0, lng ?? 0);
            return rval;
        }


        public static bool ResolveCoordinates(string latitude, string longitude, out Coordinates coordinates)
        {
            double lat = 0, lng = 0;
            bool rval = double.TryParse(latitude, out lat) &&
                        double.TryParse(longitude, out lng);

            if((lat > 90) || (lat < -90)) { lat = lng = 0; rval = false; }
            if((lng > 180) || (lat < -180)) { lat = lng = 0; rval = false; }

            coordinates = new(lat, lng);
            return rval;
        }
    }
}
