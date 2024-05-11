using System;



namespace SWEN2.Places.Model
{
    /// <summary>This class provides basic configuration items.</summary>
    public static class Root
    {
        /// <summary>Configuration.</summary>
        public static readonly Configuration Config = Configuration.Load();

        /// <summary>Data context.</summary>
        public static readonly DataContext Context = new();
    }
}
