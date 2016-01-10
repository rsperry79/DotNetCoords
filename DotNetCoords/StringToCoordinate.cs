using System;
using System.Text.RegularExpressions;

namespace DotNetCoords
{
    /// <summary>
    ///     Allows detection of formats
    /// </summary>
    public static class StringToCoordinate
    {
        //reg ex to determine type of coordinate and verify as good.
        private static readonly Regex RxLatLngDecimal =
            new Regex(
                @"^(?<lat>[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?))(\/|\:| ||, )(?<lng>[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?))$");

        //private static readonly Regex RxLatLngDmDirection =
        //    new Regex(@"([SN])\s(\d+)\s(\d+(?:\.\d+)?)\s([EW])\s(\d+)\s(\d+(?:\.\d*)?)");

        private static readonly Regex RxUtm =
            new Regex(
                @"^(?<lngZone>\d{1,2})(\/|\:| |)(?<latZone>[^aboiyzABOIYZ\d\[-\` -@])(\/|\:| |)(?<eastNorth>\d{2,}|\d+ (\/|\:| |)\d+)$");

        private static readonly Regex RxMgrsOrUsng =
            new Regex(
                @"^(?<lngZone>\d{1,2})(\/|\:| |)(?<latZone>[^aboiyzABOIYZ\d\[-\` -@])(\/|\:| |)(?<CollumLetter>[A-Z a-z])(\/|\:| |)(?<RowLetter>[A-Z a-z])(\/|\:| |)(?<eastNorth>\d{2,}|\d+ (\/|\:| |)\d+)$");

        private static readonly Regex RxLatLngDms =
            new Regex(
                @"^(?<latDeg>[+-]?[1-8]?\d?|90)(\/|\:| )(?<latMin>[0-5]?[0-9]|60)(\/|\:| )(?<latSec>[0-5]?[0-9]?\.?\d+?|60)(\/|\:| )?(?<latDir>[NSEW]?)(\/|\:| |, )(?<lngDeg>[+-]?(\d{1,2})|[+-]?([1][0-7][0-9])|[+-]?(180))(\/|\:| )(?<lngMin>[0-5]?[0-9]|60)(\/|\:| )(?<lngSec>[0-5]?[0-9]?\.?\d+?|60)(\/|\:| )?(?<lngDir>[NSEW]?)$");

        /// <summary>
        ///     returns back an object
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static object ToStringType(string toConvert)
        {
            if (RxUtm.IsMatch(toConvert))
            {
                return new UtmRef(toConvert);
            }


            if (RxMgrsOrUsng.IsMatch(toConvert))
            {
                return new MgrsRef(toConvert);
            }


            if (RxLatLngDecimal.IsMatch(toConvert))
            {
                return new LatLng(toConvert);
            }


            if (RxLatLngDms.IsMatch(toConvert))
            {
                return new LatLng(toConvert);
            }

            return new InvalidFomat();
        }

        /// <summary>
        /// </summary>
        /// <param name="toConvert"></param>
        /// <returns></returns>
        public static LatLng ToLatLng(string toConvert)
        {
            var coord = ToStringType(toConvert);
            if (coord.GetType() == typeof (LatLng)) return coord as LatLng;
            var coordToTrans = coord as CoordinateSystem;
            return coordToTrans.ToLatLng();
        }
    }

    /// <summary>
    ///     return back an invalid coordinate
    /// </summary>
    public class InvalidFomat
    {
    }
}