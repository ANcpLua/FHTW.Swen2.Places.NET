using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class represents a story for a place.</summary>
    [Table("STORIES")][PrimaryKey("ID")]
    public sealed class Story
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="place">Place.</param>
        public Story(Place place)
        {
            Place = place;
        }


        /// <summary>Creates a new instance of this class.</summary>
        private Story() 
        {
            Place = new();
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets the place for the story.</summary>
        [ForeignKey("PLACE_ID")]
        public Place Place
        {
            get; private set;
        }


        /// <summary>Gets the story ID.</summary>
        [Key][Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID
        {
            get; private set;
        }


        /// <summary>Gets the story text.</summary>
        [Column("TEXT")]
        public string Text
        {
            get; set;
        } = "";


        /// <summary>Gets the list of picture paths for this story.</summary>
        [Column("PICTURES")]
        public List<string> Pictures
        {
            get; private set;
        } = new();
    }
}
