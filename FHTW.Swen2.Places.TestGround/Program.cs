using FHTW.Swen2.Places.Model;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Util;
using Microsoft.Data.Sqlite;

namespace FHTW.Swen2.Places.TestGround
{
    internal class Program
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(Program));


        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello, World!\n");

            //LogLog.InternalDebugging = true;

            
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            LogManager.GetRepository().Threshold = Level.Debug;
            */

            //App.Db.Database.EnsureCreated();
            /*
            Place p = new()
            {
                Name = "Test", Description = "A test place",
                Location = new Coordinates() { Latitude = 16.123, Longitude = 45.456 }
            };

            Story x = new Story(p)
            {
                Text = "Blaaa!"
            };
            x.Pictures.Add("pic1");
            x.Pictures.Add("pic2");

            p.Stories.Add(x);

            App.Db.Places.Add(p);
            App.Db.SaveChanges();
            */

            foreach(Place i in Root.Db.Places)
            {
                Console.WriteLine(i.Name);

                foreach(Story k in i.Stories)
                {
                    Console.WriteLine("    " + k.Text);
                }
            }
        }
    }
}