using SWEN2.Places.Model;


namespace FHTW.SWEN2.Places.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {}


        [Test][Order(0)]
        public void TestPlacesRetrieval()
        {
            int n = 0;
            foreach(Place i in Root.Context.Places)
            {
                n++;
            }

            Assert.That(n == 3);
        }


        [Test][Order(1)]
        public void TestORF()
        {
            Place? orf = Root.Context.Places.Where(m => m.Description.ToLower().StartsWith("orf")).FirstOrDefault();
            Assert.Multiple(() =>
            {
                Assert.That(orf, Is.Not.Null);
                Assert.That(((Coordinates) orf!.Location!).Latitude == 48.175977224330936);
                Assert.That(orf!.Name == "ORF Zentrum");
            });
        }
    }
}