using Capstone.Web.DAO;
using Capstone.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private const string TEST_PARK_CODE = "CVNP";
        private TransactionScope _tran = null;
        private IParkDAO _db = null;

        [TestInitialize]
        public void Initialize()
        {
            _db = new ParkDAO("Data Source=.\\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True");
            _tran = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        [TestMethod]
        public void TestGetParkByCode()
        {
            Park park = _db.GetPark(TEST_PARK_CODE);

            Assert.AreEqual("Cuyahoga Valley National Park", park.ParkName, "The names do not match.");
            Assert.AreEqual("Ohio", park.State, "The states do not match.");
            Assert.AreEqual(32832, park.Acreage, "The acreage does not match.");
            Assert.AreEqual(696, park.Elevation, "The elevation does not match.");
            Assert.AreEqual(125, park.MilesOfTrail, "The trail length does not match.");
            Assert.AreEqual(0, park.NumberOfCampsites, "The campsites do not match.");
            Assert.AreEqual("Woodland", park.Climate, "The climates do not match.");
            Assert.AreEqual(2000, park.YearFounded, "The years do not match.");
            Assert.AreEqual(2189849, park.AnnualVisitorCount, "The visitor counts do not match.");
            Assert.AreEqual("John Muir", park.InspirationalQuoteSource, "The quote sources do not match.");
            Assert.AreEqual(0, park.EntryFee, "The entry fee does not match.");
            Assert.AreEqual(390, park.NumberOfAnimalSpecies, "The number of species do not match.");
            Assert.IsNotNull(park.Description, "Missing Description");
            Assert.IsNotNull(park.InspirationalQuote, "Missing Quote");
        }
        
        [TestMethod]
        public void TestGetParks()
        {
            List<Park> parks = _db.GetParks();

            Assert.AreEqual(10, parks.Count, "Number of parks do not match.");
            Assert.AreEqual(TEST_PARK_CODE, parks[0].ParkCode, "Park info does not match.");
        }

        [TestMethod]
        public void TestParksWithVotes()
        {
            List<Park> parks = _db.ParksWithVotes();
            Assert.IsNotNull(parks[0], "List did not populate.");
        }

        [TestMethod]
        public void TestGetWeather()
        {
            Park park = _db.GetPark(TEST_PARK_CODE);
            List<Weather> Carl = _db.GetWeather(park);
            Assert.AreEqual(5, Carl.Count, "Number of forecasts do not match.");
            Assert.AreEqual(38, Carl[0].Low, "Low does not match.");
            Assert.AreEqual(56, Carl[1].High, "High does not match.");
            Assert.AreEqual("partly cloudy", Carl[2].Forecast, "Forecast does not match.");
        }

        [TestMethod]
        public void TestAddPark()
        {
            int test = _db.AddPark();
            Assert.AreNotEqual(0, test, "Park Not Added");
        }

        [TestMethod]
        public void TestVote()
        {

            VoteViewModel vm = new VoteViewModel();

            vm.Email = "Carl@weathers.com";
            vm.Code = TEST_PARK_CODE;
            vm.State = "CA";
            vm.Activity = "Extremely Active";
            

            bool test = _db.Vote(vm);
            Assert.AreEqual(true, test, "Vote not counted.");
        }
    }
}
