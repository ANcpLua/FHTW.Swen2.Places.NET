using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN2.Places
{
    /// <summary>This class represents a story.</summary>
    public class Story
    {
        /// <summary>Creates a new instance of this class.</summary>
        private Story()
        {}

        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="place">Place.</param>
        public Story(Place place)
        {
            Place = place;
        }


        /// <summary>Gets the story ID.</summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID
        {
            get;
        }

        /// <summary>Gets the place this story belongs to.</summary>
        [Column("PLACE_ID")]
        public Place? Place
        {
            get; private set;
        }

        /// <summary>Gets the place text.</summary>
        [Column("TEXT")]
        public string? Text
        {
            get; set;
        } = "";

        /// <summary>Gets the place text.</summary>
        [Column("PICTURES")]
        public List<string> Pictures
        {
            get; set;
        } = new();
    }
}
