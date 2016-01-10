using System;
using DotNetCoords;
using DotNetCoords.Datum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoordsTest
{
    [TestClass]
    public class LatLongTests
    {
        [TestMethod]
        public void LatLongDec()
        {
            const double Lat = 47.99999993;
            const double Lng = -122.000001509;
            var ll = new LatLng(Lat, Lng);
            Assert.AreEqual(Lat, ll.Latitude);
            Assert.AreEqual(Lng, ll.Longitude);
        }

        [TestMethod]
        public void LatLongHeight()
        {
            const double Lat = 47.99999993;
            const double Lng = -122.000001509;
            const double height = 10;
            var ll = new LatLng(Lat, Lng, height);
            Assert.AreEqual(Lat, ll.Latitude);
            Assert.AreEqual(Lng, ll.Longitude);
            Assert.AreEqual(height, ll.Height);
        }

        [TestMethod]
        public void LatLongCopy()
        {
            const double Lat = 47.99999993;
            const double Lng = -122.000001509;

            var ll = new LatLng(Lat, Lng);
            var ll2 = new LatLng(ll);

            Assert.AreEqual(Lat, ll2.Latitude);
            Assert.AreEqual(Lng, ll2.Longitude);
        }

        [TestMethod]
        public void LatLongDms()
        {
            const double Lat = 47.99999993;
            const double Lng = -122.000001509;
            var ll = new LatLng(47, 59, 59.99975, NorthSouth.North, 122, 00, 00.00543, EastWest.West);
            Assert.AreEqual(Lat, ll.Latitude, 0.00000001);
            Assert.AreEqual(Lng, ll.Longitude, 0.00000001);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void LatLonInvalidLat()
        {
            const double Lat = 99;
            const double Lng = -122.000001509;
            var ll = new LatLng(Lat, Lng);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void LatLonInvalidLon()
        {
            const double Lat = 55;
            const double Lng = -190;
            var ll = new LatLng(Lat, Lng);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void LatLonHeDatInvalidLat()
        {
            const double Lat = 99;
            const double Lng = -122.000001509;
            const double Height = 10;
            var dat = WGS84Datum.Instance;
            var ll = new LatLng(Lat, Lng, Height, dat);
        }

        [TestMethod]
        public void LatLongHeightDatum()
        {
            const double Lat = 55;
            const double Lng = -122.000001509;
            const double Height = 10;
            var dat = WGS84Datum.Instance;
            var ll = new LatLng(Lat, Lng, Height, dat);

            Assert.AreEqual(Lat, ll.Latitude);
            Assert.AreEqual(Lng, ll.Longitude);
            Assert.AreEqual(Height, ll.Height);
            Assert.AreEqual(dat, WGS84Datum.Instance);
        }

        [TestMethod]
        public void LatLonToString()
        {
            const double Lat = 47.99999993;
            const double Lng = -122.000001509;
            var ll = new LatLng(Lat, Lng);
            var expected = string.Format("{0}, {1}", Lat, Lng);
            Assert.AreEqual(expected, ll.ToString());
        }

        [TestMethod]
        public void LatLonToDms()
        {
            const double Lat = 47.99999993;
            const double Lng = -122.000001509;
            var ll = new LatLng(Lat, Lng);
            var expected = "47 59 59.9997480000047 N 122 0 0.00543240000183687 W";
            Assert.AreEqual(expected, ll.ToDmsString());
        }

        [TestMethod]
        public void LatLontoUtmRef()
        {
            const double Lat = 47.99999993;
            const double Lon = -122.000001509;
            var ll = new LatLng(Lat, Lon);
            var utmActual = ll.ToUtmRef();
            var utmExpected = new UtmRef(10, 'T', 574595, 5316784, WGS84Datum.Instance);

            Assert.AreEqual(utmExpected, utmActual);
        }

        [TestMethod]
        public void LatLngDistanceKm()
        {
            const double Lat1 = 1;
            const double Lng1 = 1;

            const double Lat2 = 2;
            const double Lng2 = 2;

            var ll1 = new LatLng(Lat1, Lng1);
            var ll2 = new LatLng(Lat2, Lng2);

            var expected = 157.2;
            Assert.AreEqual(expected, ll1.Distance(ll2), 0.1);
        }

        [TestMethod]
        public void LatLngDistanceMi()
        {
            const double Lat1 = 1;
            const double Lng1 = 1;

            const double Lat2 = 2;
            const double Lng2 = 2;

            var ll1 = new LatLng(Lat1, Lng1);
            var ll2 = new LatLng(Lat2, Lng2);

            var expected = 97.6796;
            Assert.AreEqual(expected, ll1.DistanceMiles(ll2), 0.1);
        }

        [TestMethod]
        [ExpectedException(typeof (NotDefinedOnUtmGridException))]
        public void LatLontoUtmRefInvalidLat()
        {
            const double Lat = 85;
            const double Lon = -122.000001509;
            var ll = new LatLng(Lat, Lon);
            var utmActual = ll.ToUtmRef();
        }

        //  [TestMethod]
        public void LatLonToWgs84()
        {
            const double lat = 47.950967;
            const double lon = -122.197045;
            var actual = new LatLng(lat, lon, 10, OSGB36Datum.Instance);
            actual.ToWgs84();
            var expected = new LatLng(47.99999993, -122.000001509);

            Assert.AreEqual(expected, actual);
        }
    }
}