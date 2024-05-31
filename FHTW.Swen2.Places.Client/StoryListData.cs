using System.IO;

using FHTW.Swen2.Places.Model;



namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class represents story data in a list.</summary>
    public class StoryListData
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Parent view model.</summary>
        internal PlaceDetailViewModel _Parent;

        /// <summary>Story.</summary>
        private Story _Story;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        /// <param name="story">Story.</param>
        internal StoryListData(PlaceDetailViewModel parent, Story story)
        {
            _Parent = parent;
            _Story = story;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Gets the story image source.</summary>
        public string ImageSource
        {
            get
            {
                foreach(string i in _Story.Pictures) 
                {
                    string rval = Root.Config.ImagePath.TrimEnd('\\') + '\\' + i;
                    if(File.Exists(rval)) { return rval; }
                }

                return Root.Config.ImagePath.TrimEnd('\\') + @"\_missing.jpg";
            }
        }


        /// <summary>Gets the story text.</summary>
        public string Text
        {
            get { return _Story.Text; }
        }
    }
}
