using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TSP.Solver;

namespace TSP.Tests
{
    [TestFixture]
    public class TargetTests
    {
        [Test]
        public void NorthEastCoordinatesToStringTest()
        {
            var coords = new Coordinates(14.123, 13.345);

            Assert.AreEqual("14.123°N 13.345°E", coords.ToString());
            Assert.AreEqual("14.123,13.345", coords.ToDecimalDegreesString());
        }

        [Test]
        public void SouthWestCoordinatesToStringTest()
        {
            var coords = new Coordinates(-33.987, -25.543);

            Assert.AreEqual("33.987°S 25.543°W", coords.ToString());
            Assert.AreEqual("-33.987,-25.543", coords.ToDecimalDegreesString());
        }
    }
}
