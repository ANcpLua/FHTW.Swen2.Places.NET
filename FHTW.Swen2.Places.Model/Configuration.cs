using log4net.Core;
using System.Text.Json;



namespace FHTW.Swen2.Places.Model
{
    /// <summary>This class contains application configuration data.</summary>
    public sealed class Configuration
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static methods                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Loads the configuration.</summary>
        /// <returns>Configuration.</returns>
        public static Configuration Load()
        {
            Configuration rval = new();

            if(File.Exists("places.config"))
            {
                rval = (JsonSerializer.Deserialize<Configuration>(File.ReadAllText("places.config")) ?? rval);
            }

            return rval;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets or sets the database file.</summary>
        public string DatabaseFile
        {
            get; set;
        } = @"C:\home\test\places.db";


        /// <summary>Gets the image path.</summary>
        public string ImagePath
        {
            get; set;
        } = @"C:\home\test\img";


        /// <summary>Gets or sets the log level for the application.</summary>
        public Level LogLevel
        {
            get; set;
        } = Level.Info;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public methods                                                                                           //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>Saves the configuration into a configuration file.</summary>
        public void Save()
        {
            File.WriteAllText("places.config", JsonSerializer.Serialize(this));
        }
    }
}
