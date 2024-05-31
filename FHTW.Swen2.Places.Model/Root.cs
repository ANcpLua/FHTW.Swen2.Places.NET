using System;



namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class contains basic application data.</summary>
    public class Root
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static members                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Application configuration data.</summary>
        public static readonly Configuration Config = Configuration.Load();

        /// <summary>Gets the data context.</summary>
        public static readonly DataContext Db = new();



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static methods                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Initializes the application.</summary>
        public static void Init()
        {
            Db.RebuildFtsIndex();
        }
    }
}
