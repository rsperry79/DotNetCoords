using System;
using System.Text.RegularExpressions;
using DotNetCoords.Datum;

// ReSharper disable InconsistentNaming

namespace DotNetCoords
{
    /// <summary>
    ///     Class to represent a Military Grid Reference System (MGRS) reference.
    ///     <p>
    ///         <h3> Military Grid Reference System (MGRS) </h3>
    ///     </p>
    ///     <p>
    ///         The Military Grid Reference System (MGRS) is an extension of the Universal Transverse
    ///         Mercator (UTM) reference system. An MGRS reference is made from 5 parts:
    ///     </p>
    ///     <h4> UTM Longitude Zone </h4>
    ///     <p>
    ///         This is a number indicating which UTM longitude zone the reference falls into. Zones are
    ///         numbered from 1 (starting at 180°W) through 60. Each zone is 6° wide.
    ///     </p>
    ///     <h4> UTM Latitude Zone </h4>
    ///     <p>
    ///         Latitude is split into regions that are 8° high, starting at 80°S. Latitude zones are
    ///         lettered using C through X, but omitting I and O as they can easily be confused with the
    ///         numbers 1 and 0.
    ///     </p>
    ///     <h4> 100,000m Square identification </h4>
    ///     <p>
    ///         Each UTM zone is treated as a square 100,000m to a side. The 50,000m easting is centered on
    ///         the center-point of the UTM zone. 100,000m squares are identified using two characters - one
    ///         to identify the row and one to identify the column.
    ///     </p>
    ///     <p>
    ///         Row identifiers use the characters A through V (omitting I and O again). The sequence is
    ///         repeated every 2,000,000m from the equator. If the UTM longitude zone is odd, then the
    ///         lettering is advanced by five characters to start at F.
    ///     </p>
    ///     <p> Column identifiers use the characters A through Z (again omitting I and O). </p>
    ///     <h4>
    ///         Easting and northing
    ///     </h4>
    ///     <p>
    ///         Each 100,000m grid square is further divided into smaller squares representing 1m, 10m,
    ///         100m, 1,000m and 10,000m precision. The easting and northing are given using the numeric row
    ///         and column reference of the square, starting at the bottom-left corner of the square.
    ///     </p>
    ///     <h4> MGRS Reference Example </h4>
    ///     <p>
    ///         18SUU8362601432 is an example of an MGRS reference. '18' is the UTM longitude zone, 'S' is
    ///         the UTM latitude zone, 'UU' is the 100,000m square identification, 83626 is the easting
    ///         reference to 1m precision and 01432 is the northing reference to 1m precision.
    ///     </p>
    ///     <h3> MGRSRef </h3>
    ///     <p>
    ///         Methods are provided to query an <see cref="MgrsRef" /> object for its parameters. As MGRS
    ///         references are related to UTM references, a <see cref="MgrsRef.ToUTMRef" /> method is
    ///         provided to convert an <see cref="MgrsRef" /> object into a <see cref="UtmRef" /> object.
    ///         The reverse conversion can be made using the <see cref="MgrsRef(UtmRef)" /> constructor.
    ///     </p>
    ///     <p>
    ///         <see cref="MgrsRef" /> objects can be converted to <see cref="LatLng" /> objects using the
    ///         <see cref="MgrsRef.ToLatLng" /> method. The reverse conversion is made using the
    ///         <see cref="LatLng.ToMgrsRef" /> method.
    ///     </p>
    ///     <p>
    ///         Some MGRS references use the Bessel 1841 ellipsoid rather than the Geodetic Reference System
    ///         1980 (GRS 1980), International or World Geodetic System 1984 (WGS84) ellipsoids. Use the
    ///         constructors with the optional boolean parameter to be able to specify whether your MGRS
    ///         reference uses the Bessel 1841 ellipsoid. Note that no automatic determination of the
    ///         correct ellipsoid to use is made.
    ///     </p>
    ///     <p>
    ///         <b> Important note </b>: There is currently no support for MGRS references in polar regions
    ///         north of 84°N and south of 80°S. There is also no account made for UTM zones with slightly
    ///         different sizes to normal around Svalbard and Norway.
    ///     </p>
    /// </summary>
    public class MgrsRef : CoordinateSystem
    {
        private const int LETTER_A = 0; /* ARRAY INDEX FOR LETTER A               */
        private const int LETTER_B = 1; /* ARRAY INDEX FOR LETTER B               */
        private const int LETTER_C = 2; /* ARRAY INDEX FOR LETTER C               */
        private const int LETTER_D = 3; /* ARRAY INDEX FOR LETTER D               */
        private const int LETTER_E = 4; /* ARRAY INDEX FOR LETTER E               */
        private const int LETTER_F = 5; /* ARRAY INDEX FOR LETTER E               */
        private const int LETTER_G = 6; /* ARRAY INDEX FOR LETTER H               */
        private const int LETTER_H = 7; /* ARRAY INDEX FOR LETTER H               */
        private const int LETTER_I = 8; /* ARRAY INDEX FOR LETTER I               */
        private const int LETTER_J = 9; /* ARRAY INDEX FOR LETTER J               */
        private const int LETTER_K = 10; /* ARRAY INDEX FOR LETTER J               */
        private const int LETTER_L = 11; /* ARRAY INDEX FOR LETTER L               */
        private const int LETTER_M = 12; /* ARRAY INDEX FOR LETTER M               */
        private const int LETTER_N = 13; /* ARRAY INDEX FOR LETTER N               */
        private const int LETTER_O = 14; /* ARRAY INDEX FOR LETTER O               */
        private const int LETTER_P = 15; /* ARRAY INDEX FOR LETTER P               */
        private const int LETTER_Q = 16; /* ARRAY INDEX FOR LETTER Q               */
        private const int LETTER_R = 17; /* ARRAY INDEX FOR LETTER R               */
        private const int LETTER_S = 18; /* ARRAY INDEX FOR LETTER S               */
        private const int LETTER_T = 19; /* ARRAY INDEX FOR LETTER S               */
        private const int LETTER_U = 20; /* ARRAY INDEX FOR LETTER U               */
        private const int LETTER_V = 21; /* ARRAY INDEX FOR LETTER V               */
        private const int LETTER_W = 22; /* ARRAY INDEX FOR LETTER W               */
        private const int LETTER_X = 23; /* ARRAY INDEX FOR LETTER X               */
        private const int LETTER_Y = 24; /* ARRAY INDEX FOR LETTER Y               */
        private const int LETTER_Z = 25; /* ARRAY INDEX FOR LETTER Z               */
        private const string CLARKE_1866 = "CC";
        private const string CLARKE_1880 = "CD";
        private const string BESSEL_1841 = "BR";
        private const string BESSEL_1841_NAMIBIA = "BN";

        private static readonly Regex RxMgrsOrUsng = new
            Regex(
            @"^(?<lngZone>\d{1,2})(\/|\:| |)(?<latZone>[^aboiyzABOIYZ\d\[-\` -@])(\/|\:| |)(?<CollumLetter>[A-Z a-z])(\/|\:| |)(?<RowLetter>[A-Z a-z])(\/|\:| |)(?<eastNorth>\d{2,}|\d+ (\/|\:| |)\d+)$");

        /**
         * Northing characters
         */

        private static readonly char[] latBand =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
            'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V'
        };

        /**
         * The initial precision of this MGRS reference. Must be one of
         * MGRSRef.PRECISION_1M, MGRSRef.PRECISION_10M, MGRSRef.PRECISION_100M,
         * MGRSRef.PRECISION_1000M or MGRSRef.PRECISION_10000M.
         */

        private readonly bool isBessel;

        private readonly Latitude_Band[] Latitude_Band_Table =
        {
            new Latitude_Band(LETTER_C, 1100000.0, -72.0, -80.5),
            new Latitude_Band(LETTER_D, 2000000.0, -64.0, -72.0),
            new Latitude_Band(LETTER_E, 2800000.0, -56.0, -64.0),
            new Latitude_Band(LETTER_F, 3700000.0, -48.0, -56.0),
            new Latitude_Band(LETTER_G, 4600000.0, -40.0, -48.0),
            new Latitude_Band(LETTER_H, 5500000.0, -32.0, -40.0),
            new Latitude_Band(LETTER_J, 6400000.0, -24.0, -32.0),
            new Latitude_Band(LETTER_K, 7300000.0, -16.0, -24.0),
            new Latitude_Band(LETTER_L, 8200000.0, -8.0, -16.0),
            new Latitude_Band(LETTER_M, 9100000.0, 0.0, -8.0),
            new Latitude_Band(LETTER_N, 0.0, 8.0, 0.0),
            new Latitude_Band(LETTER_P, 800000.0, 16.0, 8.0),
            new Latitude_Band(LETTER_Q, 1700000.0, 24.0, 16.0),
            new Latitude_Band(LETTER_R, 2600000.0, 32.0, 24.0),
            new Latitude_Band(LETTER_S, 3500000.0, 40.0, 32.0),
            new Latitude_Band(LETTER_T, 4400000.0, 48.0, 40.0),
            new Latitude_Band(LETTER_U, 5300000.0, 56.0, 48.0),
            new Latitude_Band(LETTER_V, 6200000.0, 64.0, 56.0),
            new Latitude_Band(LETTER_W, 7000000.0, 72.0, 64.0),
            new Latitude_Band(LETTER_X, 7900000.0, 84.5, 72.0)
        };

        /// <summary>
        ///     Create a new MGRS reference object from the given UTM reference. It is assumed that the
        ///     UTMRef object is valid.
        /// </summary>
        /// <param name="utm"> A UTM reference. </param>
        public MgrsRef(UtmRef utm)
            : this(utm, false)
        {
        }

        /// <summary>
        ///     Create a new MGRS reference object from the given UTM reference. It is assumed that this
        ///     MGRS reference represents a point using the GRS 1980, International or WGS84 ellipsoids.
        ///     It is assumed that the UTMRef object is valid.
        /// </summary>
        /// <param name="utm"> A UTM reference. </param>
        /// <param name="isBessel">
        ///     true if the parameters represent an MGRS reference using the Bessel 1841 ellipsoid;
        ///     false is the parameters represent an MGRS reference using the GRS 1980, International or
        ///     WGS84 ellipsoids.
        /// </param>
        public MgrsRef(UtmRef utm, bool isBessel)
            : base(utm.Datum)
        {
            var lngZone = utm.LngZone;
            var set = (lngZone - 1)%6 + 1;
            var eID =
                (int) Math.Floor(utm.Easting/100000.0) + 8*((set - 1)%3);
            var nID = (int) Math.Floor(utm.Northing%2000000/100000.0);

            if (eID > 8)
                eID++; // Offset for no I character
            if (eID > 14)
                eID++; // Offset for no O character

            var eIDc = (char) (eID + 64);

            // Northing ID offset for sets 2, 4 and 6
            if (set%2 == 0)
            {
                nID += 5;
            }

            if (isBessel)
            {
                nID += 10;
            }

            if (nID > 19)
            {
                nID -= 20;
            }

            var nIDc = latBand[nID];

            LngZone = lngZone;
            LatZone = utm.LatZone;
            CollumLetter = eIDc;
            RowLetter = nIDc;
            Easting = (int) Math.Round(utm.Easting)%100000;
            Northing = (int) Math.Round(utm.Northing)%100000;
            Precision = Precision.Precision1M;
            this.isBessel = isBessel;
        }

        /// <summary>
        ///     Create a new MGRS reference object from the given paramaters. It is assumed that this
        ///     MGRS reference represents a point using the GRS 1980, International or WGS84 ellipsoids.
        ///     An IllegalArgumentException is thrown if any of the parameters are invalid.
        /// </summary>
        /// <param name="utmZoneNumber"> The UTM zone number representing the longitude. </param>
        /// <param name="utmZoneChar"> The UTM zone character representing the latitude. </param>
        /// <param name="eastingID"> Character representing the 100,000km easting square. </param>
        /// <param name="northingID"> Character representing the 100,000km northing square. </param>
        /// <param name="easting"> Easting in meters. </param>
        /// <param name="northing"> Northing in meters. </param>
        /// <param name="precision"> The precision of the given easting and northing. </param>
        /// <exception cref="ArgumentException"> If any of the given parameters are invalid. </exception>
        public MgrsRef(int utmZoneNumber, char utmZoneChar, char eastingID,
            char northingID, int easting, int northing, Precision precision)
            : this(utmZoneNumber, utmZoneChar, eastingID, northingID, easting, northing,
                precision, false)
        {
        }

        /// <summary>
        ///     Create a new MGRS reference object from the given parameters. An ArgumentException is
        ///     thrown if any of the parameters are invalid.
        /// </summary>
        /// <param name="utmZoneNumber"> The UTM zone number representing the longitude. </param>
        /// <param name="utmZoneChar"> The UTM zone character representing the latitude. </param>
        /// <param name="eastingId"> Character representing the 100,000km easting square. </param>
        /// <param name="northingId"> Character representing the 100,000km northing square. </param>
        /// <param name="easting"> Easting in meters. </param>
        /// <param name="northing"> Northing in meters. </param>
        /// <param name="precision"> The precision of the given easting and northing. </param>
        /// <param name="isBessel">
        ///     true if the parameters represent an MGRS reference using the Bessel 1841 ellipsoid;
        ///     false is the parameters represent an MGRS reference using the GRS 1980, International or
        ///     WGS84 ellipsoids.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     If any of the given parameters are invalid. Note that the parameters are only checked
        ///     for the range of values that they can take on. Being able to create an MGRSRef object
        ///     does not necessarily imply that the reference is valid.
        /// </exception>
        public MgrsRef(int utmZoneNumber, char utmZoneChar, char eastingId,
            char northingId, int easting, int northing, Precision precision,
            bool isBessel)
            : base(WGS84Datum.Instance)
        {
            if (utmZoneNumber < 1 || utmZoneNumber > 60)
            {
                throw new ArgumentException("Invalid utmZoneNumber ("
                                            + utmZoneNumber + ")");
            }
            if (utmZoneChar < 'A' || utmZoneChar > 'Z')
            {
                throw new ArgumentException("Invalid utmZoneChar (" + utmZoneChar
                                            + ")");
            }
            if (eastingId < 'A' || eastingId > 'Z' || eastingId == 'I'
                || eastingId == 'O')
            {
                throw new ArgumentException("Invalid eastingId (" + eastingId
                                            + ")");
            }
            if (northingId < 'A' || northingId > 'Z' || northingId == 'I'
                || northingId == 'O')
            {
                throw new ArgumentException("Invalid northingID (" + northingId
                                            + ")");
            }
            if (easting < 0 || easting > 99999)
            {
                throw new ArgumentException("Invalid easting (" + easting + ")");
            }
            if (northing < 0 || northing > 99999)
            {
                throw new ArgumentException("Invalid northing (" + northing + ")");
            }

            LngZone = utmZoneNumber;
            LatZone = utmZoneChar;
            CollumLetter = eastingId;
            RowLetter = northingId;
            Easting = easting;
            Northing = northing;
            Precision = precision;
            this.isBessel = isBessel;
        }

        /// <summary>
        ///     Create a new MGRS reference object from the given String. Must be correctly formatted
        ///     otherwise an IllegalArgumentException will be thrown. It is assumed that this MGRS
        ///     reference represents a point using the GRS 1980, International or WGS84 ellipsoids.
        /// </summary>
        /// <param name="gridRef"> A string to create an MGRS reference from. </param>
        /// <exception cref="ArgumentException"> if the given String is not correctly formatted. </exception>
        public MgrsRef(string gridRef)
            : this(gridRef, false)
        {
        }

        /// <summary>
        ///     Create a new MGRS reference object from the given String. Must be correctly formatted
        ///     otherwise an ArgumentException will be thrown. Matching regex: (\d{1,2})([A-Z])([A-Z])([A-Z])(\d{2,10})
        /// </summary>
        /// <param name="toConvert"> a string to create an MGRS reference from. </param>
        /// <param name="isBessel">
        ///     True if the parameters represent an MGRS reference using the Bessel 1841 ellipsoid;
        ///     false is the parameters represent an MGRS reference using the GRS 1980, International or
        ///     WGS84 ellipsoids.
        /// </param>
        /// <exception cref="ArgumentException"> if the given String is not correctly formatted. </exception>
        public MgrsRef(string toConvert, bool isBessel)
            : base(WGS84Datum.Instance)
        {
            //if (string.IsNullOrEmpty(gridRef) || gridRef.Length < 6)
            //    throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", "gridRef");
            //var begin = 0;
            //var length = 1;
            //var gridRefArray = gridRef.ToCharArray();
            //if (char.IsDigit(gridRefArray[1]))
            //    length = 2;
            //LngZone = int.Parse(gridRef.Substring(begin, length));
            //begin += length;
            //if (!(char.IsUpper(gridRefArray[begin]) &&
            //      char.IsUpper(gridRefArray[begin + 1]) &&
            //      char.IsUpper(gridRefArray[begin + 2])))
            //    throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", "gridRef");
            //LatZone = gridRefArray[begin];
            //CollumLetter = gridRefArray[begin + 1];
            //RowLetter = gridRefArray[begin + 2];
            //begin += 3;
            //for (length = 0; begin + length < gridRefArray.Length; length++)
            //{
            //    if (!char.IsDigit(gridRefArray[begin + length]))
            //        throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", "gridRef");
            //}
            //if (length < 2 || length % 2 != 0)
            //    throw new ArgumentException("Invalid MGRS reference (" + gridRef + ")", "gridRef");
            //Precision = (Precision)Math.Pow(10, 5 - (length / 2));
            //Easting =
            //    int.Parse(gridRef.Substring(begin, length / 2)) * (int)Precision;
            //Northing = int.Parse(gridRef.Substring(begin + length / 2)) * (int)Precision;

            if (string.IsNullOrEmpty(toConvert) || toConvert.Length < 6)
                throw new ArgumentException("Invalid MGRS reference (" + toConvert + ")", "toConvert");
            if (RxMgrsOrUsng.IsMatch(toConvert))
            {
                char latZone, collum, row;
                int lngZone;
                int easting, northing;

                var mrgsMatch = RxMgrsOrUsng.Match(toConvert);
                var eastNorth = mrgsMatch.Groups["eastNorth"].Value;
                var srcLength = eastNorth.Length;

                var half = srcLength/2;

                var strEasting = eastNorth.Substring(0, half);
                var strNorthing = eastNorth.Substring(half, half);


                if (int.TryParse(mrgsMatch.Groups["lngZone"].Value, out lngZone) &&
                    char.TryParse(mrgsMatch.Groups["latZone"].Value, out latZone) &&
                    char.TryParse(mrgsMatch.Groups["CollumLetter"].Value, out collum) &&
                    char.TryParse(mrgsMatch.Groups["RowLetter"].Value, out row) &&
                    int.TryParse(strEasting, out easting) &&
                    int.TryParse(strNorthing, out northing))
                {
                    LngZone = lngZone;
                    LatZone = char.ToUpper(latZone);
                    CollumLetter = char.ToUpper(collum);
                    RowLetter = char.ToUpper(row);
                    Easting = easting;
                    Northing = northing;
                    Precision = (Precision) Math.Pow(10, 5 - srcLength/2);
                }

                else
                {
                    throw new ArgumentException("Invalid MGRS Format");
                }
            }
        }

        /// <summary>
        ///     Gets the easting.
        /// </summary>
        /// <value> The easting. </value>
        public int Easting { get; private set; }

        /// <summary>
        ///     Gets the easting ID.
        /// </summary>
        /// <value> The easting ID. </value>
        public char CollumLetter { get; private set; }

        /// <summary>
        ///     Gets the northing.
        /// </summary>
        /// <value> The northing. </value>
        public int Northing { get; private set; }

        /// <summary>
        ///     Gets the northing ID.
        /// </summary>
        /// <value> The northing ID. </value>
        public char RowLetter { get; private set; }

        /// <summary>
        ///     Gets the precision.
        /// </summary>
        /// <value> The precision. </value>
        public Precision Precision { get; private set; }

        /// <summary>
        ///     Gets the UTM zone character representing the latitude.
        /// </summary>
        /// <value> The UTM zone character representing the latitude. </value>
        public char LatZone { get; private set; }

        /// <summary>
        ///     Gets the UTM zone number representing the longitude.
        /// </summary>
        /// <value> The UTM zone number representing the longitude. </value>
        public int LngZone { get; private set; }

        private void Get_Latitude_Band_Min_Northing(char letter, out double min_northing)
            /*
    * The function Get_Latitude_Band_Min_Northing receives a latitude band letter
    * and uses the Latitude_Band_Table to determine the minimum northing for that
    * latitude band letter.
    *
    *   letter        : Latitude band letter             (input)
    *   min_northing  : Minimum northing for that letter(output)
    */
        {
            /* Get_Latitude_Band_Min_Northing */

            var letterVal = letter - 65;
            if ((letter >= 'C') && (letter <= 'H'))
                min_northing = Latitude_Band_Table[letterVal - 2].min_northing;
            else if ((letter >= 'J') && (letter <= 'N'))
                min_northing = Latitude_Band_Table[letterVal - 3].min_northing;
            else if ((letter >= 'P') && (letter <= 'X'))
                min_northing = Latitude_Band_Table[letterVal - 4].min_northing;
            else
                throw new Exception("String error");
        } /* Get_Latitude_Band_Min_Northing */

        private void Get_Grid_Values(out long ltr2_low_value, out long ltr2_high_value, out double false_northing)
            /*
    * The function Get_Grid_Values sets the letter range used for
    * the 2nd letter in the MGRS coordinate string, based on the set
    * number of the utm zone. It also sets the false northing using a
    * value of A for the second letter of the grid square, based on
    * the grid pattern and set number of the utm zone.
    *
    *    zone            : Zone number             (input)
    *    ltr2_low_value  : 2nd letter low number   (output)
    *    ltr2_high_value : 2nd letter high number  (output)
    *    false_northing  : False northing          (output)
    */
        {
            /* BEGIN Get_Grid_Values */
            long set_number; /* Set number (1-6) based on UTM zone number */
            bool aa_pattern; /* Pattern based on ellipsoid code */

            set_number = LngZone%6;

            if (set_number == 0)
                set_number = 6;

            //if (!strcmp(MGRS_Ellipsoid_Code, CLARKE_1866) || !strcmp(MGRS_Ellipsoid_Code, CLARKE_1880) ||
            //  !strcmp(MGRS_Ellipsoid_Code, BESSEL_1841) || !strcmp(MGRS_Ellipsoid_Code, BESSEL_1841_NAMIBIA))
            //  aa_pattern = false;
            //else
            aa_pattern = true;

            ltr2_low_value = 0;
            ltr2_high_value = 0;
            if ((set_number == 1) || (set_number == 4))
            {
                ltr2_low_value = LETTER_A;
                ltr2_high_value = LETTER_H;
            }
            else if ((set_number == 2) || (set_number == 5))
            {
                ltr2_low_value = LETTER_J;
                ltr2_high_value = LETTER_R;
            }
            else if ((set_number == 3) || (set_number == 6))
            {
                ltr2_low_value = LETTER_S;
                ltr2_high_value = LETTER_Z;
            }

            /* False northing at A for second letter of grid square */
            false_northing = set_number%2 == 0 ? 1500000.0 : 0.0;
        } /* END OF Get_Grid_Values */

        /// <summary>
        ///     Convert this MGRS reference to an equivelent UTM reference. This method based on
        ///     http://www.stellman-greene.com/mgrs_to_utm/
        /// </summary>
        /// <returns> The equivalent UTM reference. </returns>
        public UtmRef ToUTMRef()
        {
            var e = CollumLetter - 65;
            if (e >= 15)
                e--;
            if (e >= 9)
                e--;

            var ex = (Easting + (e%8 + 1)*100000)%1000000;

            long ltr2_low_value;
            long ltr2_high_value;
            double false_northing;
            Get_Grid_Values(out ltr2_low_value, out ltr2_high_value, out false_northing);

            /* Check that the second letter of the MGRS string is within
            * the range of valid second letter values
            * Also check that the third letter is valid */
            //if ((letters[1] < ltr2_low_value) || (letters[1] > ltr2_high_value) || (letters[2] > LETTER_V))
            //  error_code |= MGRS_STRING_ERROR;

            var offsetNorthing = RowLetter - 65;
            var grid_northing = offsetNorthing*100000.0 + false_northing;

            if (offsetNorthing > LETTER_O)
                grid_northing = grid_northing - 100000.0;

            if (offsetNorthing > LETTER_I)
                grid_northing = grid_northing - 100000.0;

            if (grid_northing >= 2000000.0)
                grid_northing = grid_northing - 2000000.0;

            double min_northing;
            Get_Latitude_Band_Min_Northing(LatZone, out min_northing);

            var scaled_min_northing = min_northing;
            while (scaled_min_northing >= 2000000.0)
            {
                scaled_min_northing = scaled_min_northing - 2000000.0;
            }

            grid_northing = grid_northing - scaled_min_northing;
            if (grid_northing < 0.0)
                grid_northing = grid_northing + 2000000.0;

            grid_northing = min_northing + grid_northing + Northing;

            return new UtmRef(LngZone, LatZone, ex, grid_northing);
        }

        /// <summary>
        ///     Convert this MGRS reference to a latitude and longitude.
        /// </summary>
        /// <returns> The converted latitude and longitude. </returns>
        public override LatLng ToLatLng()
        {
            return ToUTMRef().ToLatLng();
        }

        /// <summary>
        ///     Return a string representation of this MGRS Reference to whatever precision this
        ///     reference is set to.
        /// </summary>
        /// <returns>
        ///     a string representation of this MGRS reference to whatever precision this reference is
        ///     set to.
        /// </returns>
        public override string ToString()
        {
            return ToString(Precision);
        }

        /// <summary>
        ///     Return a String representation of this MGRS reference to 1m, 10m, 100m, 1000m or 10000m precision.
        /// </summary>
        /// <param name="precision"> The required precision. </param>
        /// <returns> A string representation of this MGRS reference to the required precision. </returns>
        public string ToString(Precision precision)
        {
            var eastingR = (int) Math.Floor(Easting/(int) precision);
            var northingR = (int) Math.Floor(Northing/(int) precision);

            var padding = 5;

            switch (precision)
            {
                case Precision.Precision10M:
                    padding = 4;
                    break;

                case Precision.Precision100M:
                    padding = 3;
                    break;

                case Precision.Precision1000M:
                    padding = 2;
                    break;

                case Precision.Precision10000M:
                    padding = 1;
                    break;
            }

            var eastingRs = eastingR.ToString();
            var ez = padding - eastingRs.Length;
            while (ez > 0)
            {
                eastingRs = "0" + eastingRs;
                ez--;
            }

            var northingRs = northingR.ToString();
            var nz = padding - northingRs.Length;
            while (nz > 0)
            {
                northingRs = "0" + northingRs;
                nz--;
            }

            var utmZonePadding = "";
            if (LngZone < 10)
            {
                utmZonePadding = "0";
            }

            return utmZonePadding + LngZone + LatZone
                   + CollumLetter + RowLetter
                   + eastingRs + northingRs;
        }

        /// <summary>
        ///     Determines whether this instance represents an MGRS reference using the Bessel 1841 ellipsoid.
        /// </summary>
        /// <returns>
        ///     <c> true </c> if the instance represents an MGRS reference using the Bessel 1841
        ///     ellipsoid; <c> false </c> if the instance represents an MGRS reference using the GRS
        ///     1980, International or WGS84 ellipsoids.
        /// </returns>
        public bool IsBessel()
        {
            return isBessel;
        }

        internal struct Latitude_Band
        {
            internal long letter; /* letter representing latitude band  */
            internal double min_northing; /* minimum northing for latitude band */
            internal double north; /* upper latitude for latitude band   */
            internal double south; /* lower latitude for latitude band   */

            public Latitude_Band(long letter, double min_northing, double north, double south)
            {
                this.letter = letter;
                this.min_northing = min_northing;
                this.north = north;
                this.south = south;
            }
        }
    }
}