using DotNetCoords;
using DotNetCoords.Datum;
using DotNetCoords.Datum.NAD27;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoordsTest
{
    [TestClass]
    public class UtmRefTests
    {
        //used http://www.agc.army.mil/Missions/Corpscon.aspx
        //to help find valid test coords

        //this:     -121.999994919552
        //Corpscon: -121.999994920

        [TestMethod]
        public void UTMRefTest()
        {
            var autm = new UtmRef(10, 'T', 574595, 5316784);
            Assert.AreEqual(10, autm.LngZone);
            Assert.AreEqual('T', autm.LatZone);
            Assert.AreEqual(574595, autm.Easting);
            Assert.AreEqual(5316784, autm.Northing);
        }

        [TestMethod]
        public void UTMRefDatumTest()
        {
            var utmd = new UtmRef(10, 'T', 574595, 5316784, NAD27ContiguousUSDatum.Instance);
            Assert.AreEqual(10, utmd.LngZone);
            Assert.AreEqual('T', utmd.LatZone);
            Assert.AreEqual(574595, utmd.Easting);
            Assert.AreEqual(5316784, utmd.Northing);
            Assert.AreEqual(NAD27ContiguousUSDatum.Instance, utmd.Datum);
        }

        [TestMethod]
        public void ToLatLngTest()
        {
            var autm = new UtmRef(10, 'T', 574595, 5316784);

            var alat = 47.99999993;
            var alon = -122.000001509;
            var alatlon = autm.ToLatLng();

            Assert.AreEqual(alat, alatlon.Latitude, 0.00000001);

            Assert.AreEqual(alon, alatlon.Longitude, 0.00000001);
        }

        //negagtive check only, cords not checked
        [TestMethod]
        public void ToLatLngSouthern()
        {
            var autm = new UtmRef(10, 'D', 574595, 5316784);

            var alat = -42.2976024212208;
            var alon = -122.095060688641;

            var alatlon = autm.ToLatLng();

            Assert.AreEqual(alat, alatlon.Latitude, 0.00000001);

            Assert.AreEqual(alon, alatlon.Longitude, 0.00000001);
        }

        [TestMethod]
        public void ToLatLngTestWithNad27()
        {
            var utmd = new UtmRef(10, 'T', 574595, 5316784, NAD27ContiguousUSDatum.Instance);

            var lat = 48.00196908;
            var lon = -121.99999492;

            var latlon = utmd.ToLatLng();

            Assert.AreEqual(lat, latlon.Latitude, 0.00000001);
            Assert.AreEqual(lon, latlon.Longitude, 0.00000001);
        }

        [TestMethod]
        public void LatZone()
        {
            var autm = new UtmRef(10, 'T', 574595, 5316784);
            var expected = 'T';
            var actual = autm.LatZone;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var expected = "10T 574595 5316784";
            var autm = new UtmRef(10, 'T', 574595, 5316784);
            Assert.AreEqual(expected, autm.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof (NotDefinedOnUtmGridException))]
        public void UtmNotDefinedEasting()
        {
            var autm = new UtmRef(10, 'T', -1, 5316784);
        }

        [TestMethod]
        [ExpectedException(typeof (NotDefinedOnUtmGridException))]
        public void UtmNotDefinedNorthing()
        {
            var autm = new UtmRef(10, 'T', 10, -10);
        }

        [TestMethod]
        [ExpectedException(typeof (NotDefinedOnUtmGridException))]
        public void UtmNotDefinedLongZone()
        {
            var autm = new UtmRef(-1, 'T', 1, 5316784);
        }

        [TestMethod]
        [ExpectedException(typeof (NotDefinedOnUtmGridException))]
        public void UtmNotDefinedLatZone()
        {
            var autm = new UtmRef(10, 'A', 1, 5316784);
        }

        [TestMethod]
        public void GetUtmLatitudeZoneLetterTest()
        {
            var expected = 'T';
            var actual = UtmRef.GetUtmLatitudeZoneLetter(47.99999993);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetHashTest()
        {
            const double Lat = 47.99999993;
            const double Lon = -122.000001509;
            var ll = new LatLng(Lat, Lon);
            var utmActual = ll.ToUtmRef();
            var utmExpected = new UtmRef(10, 'T', 574595, 5316784, WGS84Datum.Instance);

            Assert.AreEqual(utmExpected.GetHashCode(), utmActual.GetHashCode());
        }

        [TestMethod]
        public void UtmAreSameObject()
        {
            var utmExpected = new UtmRef(10, 'T', 574595, 5316784, WGS84Datum.Instance);
            var utmActual = utmExpected;

            Assert.AreSame(utmExpected, utmActual);
        }

        [TestMethod]
        public void UtmAreEqualNullObject()
        {
            UtmRef utmActual = null;
            var utmExpected = new UtmRef(10, 'T', 574595, 5316784, WGS84Datum.Instance);

            Assert.AreNotEqual(utmExpected, utmActual);
        }
    }
}