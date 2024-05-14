using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SWEN2.Places.Model;



namespace FHTW.Swen2.Places.Vm
{
    /// <summary>This class represents search results for the view model.</summary>
    internal class SearchResultData
    {
        /// <summary>Parent view model.</summary>
        internal ResultPageViewModel _Parent;


        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        /// <param name="place">Place.</param>
        internal SearchResultData(ResultPageViewModel parent, Place place)
        {
            _Parent = parent;
            Place = place;
        }


        /// <summary>Gets the place represented by this search result.</summary>
        public Place Place
        {
            get; private init;
        }


        /// <summary>Gets the place name.</summary>
        public string Name
        {
            get { return Place.Name; }
        }


        /// <summary>Gets the place description.</summary>
        public string Description
        {
            get { return Place.Description; }
        }
    }
}
