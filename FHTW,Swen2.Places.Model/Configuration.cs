using System.Text.Json;

namespace SWEN2.Places.Model
{
    /// <summary>This class implements configuration data.</summary>
    public class Configuration
    {
        /// <summary>Gets or sets the database path.</summary>
        public string DatabasePath
        {
            get; set;
        } = @"C:\home\test\places.db";


        /// <summary>Gets or sets the image path.</summary>
        public string ImagePath
        {
            get; set;
        } = @"C:\home\test\img";


        /// <summary>Loads the configuration.</summary>
        /// <returns>Configuration.</returns>
        public static Configuration Load()
        {
            Configuration rval = new();

            if(File.Exists("places.config"))
            {
                rval = JsonSerializer.Deserialize<Configuration>(File.ReadAllText("places.config")) ?? rval;
            }

            return rval;
        }


        /// <summary>Saves the configuration.</summary>
        public void Save()
        {
            File.WriteAllText("places.config", JsonSerializer.Serialize(this));
        }
    }
}