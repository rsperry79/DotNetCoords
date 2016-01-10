using DotNetCoords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCoordsTest
{
    [TestClass]
    public class StringToCoordinateTests
    {
        [TestMethod]
        public void UtmNoSpace()
        {
            var actual = StringToCoordinate.ToStringType("4Q6109372363778") as UtmRef;
            Assert.AreEqual(actual.GetType(), typeof (UtmRef));
            Assert.AreEqual(4, actual.LngZone);
            Assert.AreEqual('Q', actual.LatZone);
            Assert.AreEqual(610937, actual.Easting);
            Assert.AreEqual(2363778, actual.Northing);
        }

        [TestMethod]
        public void UtmNoSpaceEvenEasting()
        {
            var actual = StringToCoordinate.ToStringType("4Q06109372363778") as UtmRef;
            Assert.AreEqual(actual.GetType(), typeof (UtmRef));
            Assert.AreEqual(4, actual.LngZone);
            Assert.AreEqual('Q', actual.LatZone);
            Assert.AreEqual(610937, actual.Easting);
            Assert.AreEqual(2363778, actual.Northing);
        }

        [TestMethod]
        public void UtmWithSpace()
        {
            var actual = StringToCoordinate.ToStringType("4 Q 610937 2363778") as UtmRef;
            Assert.AreEqual(actual.GetType(), typeof (UtmRef));
            Assert.AreEqual(4, actual.LngZone);
            Assert.AreEqual('Q', actual.LatZone);
            Assert.AreEqual(610937, actual.Easting);
            Assert.AreEqual(2363778, actual.Northing);
        }

        [TestMethod]
        public void UtmWithSpaceExceptEn()
        {
            var actual = StringToCoordinate.ToStringType("4 Q 6109372363778") as UtmRef;
            Assert.AreEqual(actual.GetType(), typeof (UtmRef));
            Assert.AreEqual(4, actual.LngZone);
            Assert.AreEqual('Q', actual.LatZone);
            Assert.AreEqual(610937, actual.Easting);
            Assert.AreEqual(2363778, actual.Northing);
        }

        [TestMethod]
        public void UtmWithSlash()
        {
            var actual = StringToCoordinate.ToStringType("18/S/3541674320320") as UtmRef;
            Assert.AreEqual(actual.GetType(), typeof (UtmRef));
            Assert.AreEqual(18, actual.LngZone);
            Assert.AreEqual('S', actual.LatZone);
            Assert.AreEqual(354167, actual.Easting);
            Assert.AreEqual(4320320, actual.Northing);
        }

        [TestMethod]
        public void UtmLong()
        {
            var actual = StringToCoordinate.ToStringType("2e01928391087509127405123521353526798");
            Assert.AreEqual(actual.GetType(), typeof (UtmRef));
        }

        [TestMethod]
        public void MgrsAllCaps()
        {
            var actual = StringToCoordinate.ToStringType("4QFJ1093763778") as MgrsRef;
            Assert.AreEqual(actual.GetType(), typeof (MgrsRef));
            Assert.AreEqual(4, actual.LngZone);
            Assert.AreEqual('Q', actual.LatZone);

            Assert.AreEqual('F', actual.CollumLetter);

            Assert.AreEqual('J', actual.RowLetter);
            Assert.AreEqual(10937, actual.Easting);
            Assert.AreEqual(63778, actual.Northing);
        }

        [TestMethod]
        public void MgrsMixedCase()
        {
            var actual = StringToCoordinate.ToStringType("2QaK3093763778") as MgrsRef;
            Assert.AreEqual(actual.GetType(), typeof (MgrsRef));
            Assert.AreEqual(2, actual.LngZone);
            Assert.AreEqual('Q', actual.LatZone);
            Assert.AreEqual('A', actual.CollumLetter);
            Assert.AreEqual('K', actual.RowLetter);
            Assert.AreEqual(30937, actual.Easting);
            Assert.AreEqual(63778, actual.Northing);
        }

        [TestMethod]
        public void MgrsAllLower()
        {
            var actual = StringToCoordinate.ToStringType("1fgh23456789") as MgrsRef;
            Assert.AreEqual(actual.GetType(), typeof (MgrsRef));
            Assert.AreEqual(1, actual.LngZone);
            Assert.AreEqual('F', actual.LatZone);
            Assert.AreEqual('G', actual.CollumLetter);
            Assert.AreEqual('H', actual.RowLetter);
            Assert.AreEqual(2345, actual.Easting);
            Assert.AreEqual(6789, actual.Northing);
            Assert.AreEqual(Precision.Precision10M, actual.Precision);
        }

        [TestMethod]
        public void LatLngDecimalCommaSeperated()
        {
            var actual = StringToCoordinate.ToStringType("47.947575, -122.080611") as LatLng;
            Assert.AreEqual(actual.GetType(), typeof (LatLng));
            Assert.AreEqual(47.947575, actual.Latitude);
            Assert.AreEqual(-122.080611, actual.Longitude);
        }

        [TestMethod]
        public void LatLngDecimalSpaceSeperated()
        {
            var actual = StringToCoordinate.ToStringType("47.947575 -122.080611") as LatLng;
            Assert.AreEqual(actual.GetType(), typeof (LatLng));
            Assert.AreEqual(47.947575, actual.Latitude);
            Assert.AreEqual(-122.080611, actual.Longitude);
        }

        [TestMethod]
        public void DetectLatLngDmsSpaceSeperated()
        {
            var actual = StringToCoordinate.ToStringType("47 13 3 -123 48 10.42") as LatLng;
            Assert.AreEqual(typeof (LatLng), actual.GetType());
            Assert.AreEqual(47, actual.LatitudeDegrees);
            Assert.AreEqual(-122, actual.LongitudeDegrees);
        }

        [TestMethod]
        public void DetectLatLngDmsCommaSeperated()
        {
            var actual = StringToCoordinate.ToStringType("-47 57 59.7, -23 48 10.4") as LatLng;
            Assert.AreEqual(typeof (LatLng), actual.GetType());
            /*
            converted data comes from 
            http://www.rcn.montana.edu/resources/converter.aspx
            */
            Assert.AreEqual(-47.96658333333334, actual.Latitude, 0.00000001);
            Assert.AreEqual(-23.80288888888889, actual.Longitude, 0.00000001);
        }

        [TestMethod]
        public void DetectLatLngDmsDirectionCommaSeperated()
        {
            var actual = StringToCoordinate.ToStringType("47 57 3.64 N, 123 48 10.42 W") as LatLng;
            /*
             converted data comes from 
             http://www.rcn.montana.edu/resources/converter.aspx
            */
            Assert.AreEqual(47.951011111111114, actual.Latitude, 0.00000001);
            Assert.AreEqual(-123.80289444444445, actual.Longitude, 0.00000001);
            Assert.AreEqual(typeof (LatLng), actual.GetType());
        }
    }
}