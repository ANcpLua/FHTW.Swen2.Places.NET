using FHTW.Swen2.Places.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class encapsulates search result data.</summary>
    public class SearchResultData
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Parent result page view model.</summary>
        internal ResultPageViewModel _Parent;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        /// <param name="place">Place that is represented by this instance.</param>
        internal SearchResultData(ResultPageViewModel parent, Place place)
        {
            _Parent = parent;
            Place = place;
            SelectCommand = new(parent);
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Gets the represented place.</summary>
        public Place Place
        {
            get; private set;
        }


        /// <summary>Gets the result name.</summary>
        public string Name
        {
            get { return Place.Name; }
        }


        /// <summary>Gets the result description.</summary>
        public string Description
        {
            get { return Place.Description; }
        }


        /// <summary>Gets the select command.</summary>
        public ResultSelectCommand SelectCommand
        {
            get; private set;
        }
    }
}
