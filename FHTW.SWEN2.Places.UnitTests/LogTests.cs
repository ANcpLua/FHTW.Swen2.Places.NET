using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Core;
using log4net.Config;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FHTW.SWEN2.Places.UnitTests
{
    [TestFixture]
    public class LogTests
    {
        private static ILog _Log = LogManager.GetLogger(typeof(LogTests));


        [SetUp]
        public void Setup()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            LogManager.GetRepository().Threshold = Level.Debug;
        }


        [Test]
        public void TestLog()
        {
            _Log.Info("Started.");
        }
    }
}
