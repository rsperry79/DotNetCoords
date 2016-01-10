using System;
using DotNetCoords.Datum;
using DotNetCoords.Ellipsoid;

namespace DotNetCoords
{
    /// <summary>
    ///     <p> Class to represent an Ordnance Survey of Great Britain (OSGB) grid reference. </p>
    ///     <p>
    ///         <b> British National Grid </b><br />
    ///         <ul>
    ///             <li> Projection: Transverse Mercator </li>
    ///             <li>
    ///                 Reference ellipsoid: Airy 1830
    ///             </li>
    ///             <li> Units: meters </li><li> Origin: 49°N, 2°W </li>
    ///             <li>
    ///                 False co-ordinates of origin: 400000m east, -100000m north
    ///             </li>
    ///         </ul>
    ///     </p>
    ///     <p>
    ///         A full reference includes a two-character code identifying a particular 100,000m grid
    ///         square. The table below shows how the two-character 100,000m grid squares are identified.
    ///         The bottom left corner is at the false origin of the grid. Squares without values fall
    ///         outside the boundaries of the British National Grid.
    ///     </p>
    ///     <table border="1">
    ///         <tr>
    ///             <th> km </th><th> 0 </th><th> 100 </th><th> 200 </th>
    ///             <th>
    ///                 300
    ///             </th>
    ///             <th> 400 </th><th> 500 </th><th> 600 </th><th> 700 </th>
    ///         </tr>
    ///         <tr>
    ///             <th> 1200 </th>
    ///             <td>
    ///                 HL
    ///             </td>
    ///             <td> HM </td><td> HN </td><td> HO </td><td> HP </td><td> JL </td><td> JM </td>
    ///             <td>
    ///             </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 1100 </th><td> HQ </td><td> HR </td><td> HS </td><td> HT </td>
    ///             <td>
    ///                 HU
    ///             </td>
    ///             <td> JQ </td><td> JR </td><td> </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 1000 </th><td> HV </td>
    ///             <td>
    ///                 HW
    ///             </td>
    ///             <td> HX </td><td> HY </td><td> HZ </td><td> JV </td><td> JW </td><td> </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 900 </th><td> NA </td><td> NB </td><td> NC </td><td> ND </td><td> NE </td>
    ///             <td>
    ///                 OA
    ///             </td>
    ///             <td> OB </td><td> </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 800 </th><td> NF </td><td> NG </td>
    ///             <td>
    ///                 NH
    ///             </td>
    ///             <td> NJ </td><td> NK </td><td> OF </td><td> OG </td><td> OH </td>
    ///         </tr>
    ///         <tr>
    ///             <th>
    ///                 700
    ///             </th>
    ///             <td> NL </td><td> NM </td><td> NN </td><td> NO </td><td> NP </td><td> OL </td>
    ///             <td>
    ///                 OM
    ///             </td>
    ///             <td> ON </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 600 </th><td> NQ </td><td> NR </td><td> NS </td>
    ///             <td>
    ///                 NT
    ///             </td>
    ///             <td> NU </td><td> OQ </td><td> OR </td><td> OS </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 500 </th>
    ///             <td>
    ///             </td>
    ///             <td> NW </td><td> NX </td><td> NY </td><td> NZ </td><td> OV </td><td> OW </td>
    ///             <td>
    ///                 OX
    ///             </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 400 </th><td> </td><td> SB </td><td> SC </td><td> SD </td>
    ///             <td>
    ///                 SE
    ///             </td>
    ///             <td> TA </td><td> TB </td><td> TC </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 300 </th><td> </td>
    ///             <td>
    ///                 SG
    ///             </td>
    ///             <td> SH </td><td> SJ </td><td> SK </td><td> TF </td><td> TG </td><td> TH </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 200 </th><td> </td><td> SM </td><td> SN </td><td> SO </td><td> SP </td>
    ///             <td>
    ///                 TL
    ///             </td>
    ///             <td> TM </td><td> TN </td>
    ///         </tr>
    ///         <tr>
    ///             <th> 100 </th><td> SQ </td><td> SR </td>
    ///             <td>
    ///                 SS
    ///             </td>
    ///             <td> ST </td><td> SU </td><td> TQ </td><td> TR </td><td> TS </td>
    ///         </tr>
    ///         <tr>
    ///             <th>
    ///                 0
    ///             </th>
    ///             <td> SV </td><td> SW </td><td> SX </td><td> SY </td><td> SZ </td><td> TV </td>
    ///             <td>
    ///                 TW
    ///             </td>
    ///             <td> </td>
    ///         </tr>
    ///     </table>
    ///     <p>
    ///         Within each 100,000m square, the grid is further subdivided into 1000m squares. These 1km
    ///         squares are shown on Ordnance Survey 1:25000 and 1:50000 mapping as the main grid. To
    ///         reference a 1km square, give the easting and then the northing, e.g. TR2266. In this
    ///         example, TR represents the 100,000m square, 22 represents the easting (in km) and 66
    ///         represents the northing (in
    ///         km) . This is commonly called a four-figure grid reference.
    ///     </p>
    ///     <p>
    ///         It is possible to extend the four-figure grid reference for more accuracy. For example, a
    ///         six-figure grid reference would be accurate to 100m and an eight-figure grid reference would
    ///         be accurate to 10m.
    ///     </p>
    ///     <p>
    ///         When providing local references, the 2 characters representing the 100,000m square are often omitted.
    ///     </p>
    /// </summary>
    public class OsRef : CoordinateSystem
    {
        /**
         * The easting in meters relative to the origin of the British National Grid.
         */
        private double _easting;
        /**
         * The northing in meters relative to the origin of the British National Grid.
         */
        private double _northing;

        /// <summary>
        ///     Create a new Ordnance Survey grid reference using a given easting and northing. The
        ///     easting and northing must be in meters and must be relative to the origin of the British
        ///     National Grid.
        /// </summary>
        /// <param name="easting">
        ///     The easting in meters. Must be greater than or equal to 0.0 and less than 800000.0.
        /// </param>
        /// <param name="northing">
        ///     The northing in meters. Must be greater than or equal to 0.0 and less than 1400000.0.
        /// </param>
        /// <exception cref="ArgumentException"> If the northing or easting are out of range </exception>
        public OsRef(double easting, double northing)
            : base(OSGB36Datum.Instance)
        {
            Easting = easting;
            Northing = northing;
        }

        /// <summary>
        ///     Take a string formatted as a 6, 8 or 10 figure OS grid reference (e.g. "TG514131") and
        ///     create a new OSRef object that represents that grid reference. The first character must
        ///     be H, N, S, O or T. The second character can be any uppercase character from A through Z
        ///     excluding I.
        /// </summary>
        /// <param name="gridRef">
        ///     a string representing a 6, 8 or 10 figure Ordnance Survey grid reference in the form XY123456.
        /// </param>
        /// <exception cref="ArgumentException"> If the northing or easting are out of range </exception>
        public OsRef(string gridRef)
            : base(OSGB36Datum.Instance)
        {
            int multiplier;
            int length;
            switch (gridRef.Length)
            {
                case 8:
                    multiplier = 100;
                    length = 3;
                    break;

                case 10:
                    multiplier = 10;
                    length = 4;
                    break;

                case 12:
                    multiplier = 1;
                    length = 5;
                    break;

                default:
                    throw new ArgumentException(
                        "Unexpected length of grid reference (should be 8, 10 or 12 characters long)", "gridRef");
            }

            var char1 = gridRef[0];
            var char2 = gridRef[1];
            // Thanks to Nick Holloway for pointing out the radix bug here
            var east = int.Parse(gridRef.Substring(2, length))*multiplier;
            var north = int.Parse(gridRef.Substring(2 + length, length))*multiplier;

            if (char1 == 'H')
            {
                north += 1000000;
            }
            else if (char1 == 'N')
            {
                north += 500000;
            }
            else if (char1 == 'O')
            {
                north += 500000;
                east += 500000;
            }
            else if (char1 == 'T')
            {
                east += 500000;
            }
            int char2ord = char2;
            if (char2ord > 73)
                char2ord--; // Adjust for no I
            double nx = (char2ord - 65)%5*100000;
            var ny = (4 - Math.Floor((double) (char2ord - 65)/5))*100000;

            Easting = east + nx;
            Northing = north + ny;
        }

        /// <summary>
        ///     Convert this latitude and longitude into an OSGB (Ordnance Survey of Great
        ///     Britain) grid reference.
        /// </summary>
        /// <param name="ll"> The latitude and longitude. </param>
        /// <exception cref="ArgumentException"> If the northing or easting are out of range </exception>
        public OsRef(LatLng ll)
            : base(OSGB36Datum.Instance)
        {
            ll.ToDatum(OSGB36Datum.Instance);

            var airy1830 = Airy1830Ellipsoid.Instance;
            var OSGB_F0 = 0.9996012717;
            var N0 = -100000.0;
            var E0 = 400000.0;
            var phi0 = Util.ToRadians(49.0);
            var lambda0 = Util.ToRadians(-2.0);
            var a = airy1830.SemiMajorAxis;
            var b = airy1830.SemiMinorAxis;
            var eSquared = airy1830.EccentricitySquared;
            var phi = Util.ToRadians(ll.Latitude);
            var lambda = Util.ToRadians(ll.Longitude);
            var E = 0.0;
            var N = 0.0;
            var n = (a - b)/(a + b);
            var v = a*OSGB_F0
                    *Math.Pow(1.0 - eSquared*Util.sinSquared(phi), -0.5);
            var rho = a*OSGB_F0*(1.0 - eSquared)
                      *Math.Pow(1.0 - eSquared*Util.sinSquared(phi), -1.5);
            var etaSquared = v/rho - 1.0;
            var M = b*OSGB_F0
                    *((1 + n + 5.0/4.0*n*n + 5.0/4.0*n*n*n)*(phi - phi0)
                      - (3*n + 3*n*n + 21.0/8.0*n*n*n)
                      *Math.Sin(phi - phi0)*Math.Cos(phi + phi0)
                      + (15.0/8.0*n*n + 15.0/8.0*n*n*n)
                      *Math.Sin(2.0*(phi - phi0))*Math.Cos(2.0*(phi + phi0)) - 35.0/24.0
                      *n*n*n
                      *Math.Sin(3.0*(phi - phi0))*
                      Math.Cos(3.0*(phi + phi0)));
            var I = M + N0;
            var II = v/2.0*Math.Sin(phi)*Math.Cos(phi);
            var III = v/24.0*Math.Sin(phi)*Math.Pow(Math.Cos(phi), 3.0)
                      *(5.0 - Util.tanSquared(phi) + 9.0*etaSquared);
            var IIIA = v/720.0*Math.Sin(phi)*Math.Pow(Math.Cos(phi), 5.0)
                       *(61.0 - 58.0*Util.tanSquared(phi) + Math.Pow(Math.Tan(phi), 4.0));
            var IV = v*Math.Cos(phi);
            var V = v/6.0*Math.Pow(Math.Cos(phi), 3.0)
                    *(v/rho - Util.tanSquared(phi));
            var VI = v/120.0
                     *Math.Pow(Math.Cos(phi), 5.0)
                     *(5.0 - 18.0*Util.tanSquared(phi) + Math.Pow(Math.Tan(phi), 4.0)
                       + 14*etaSquared - 58*Util.tanSquared(phi)*etaSquared);

            N = I + II*Math.Pow(lambda - lambda0, 2.0)
                + III*Math.Pow(lambda - lambda0, 4.0)
                + IIIA*Math.Pow(lambda - lambda0, 6.0);
            E = E0 + IV*(lambda - lambda0) + V*Math.Pow(lambda - lambda0, 3.0)
                + VI*Math.Pow(lambda - lambda0, 5.0);

            Easting = E;
            Northing = N;
        }

        /// <summary>
        ///     Gets or sets the easting in meters relative to the origin of the British National Grid..
        /// </summary>
        /// <value> The easting. </value>
        /// <exception cref="ArgumentException"> If the easting is out of range </exception>
        public double Easting
        {
            get { return _easting; }
            set
            {
                if (value < 0.0 || value >= 800000.0)
                {
                    throw new ArgumentException("Easting (" + value
                                                + ") is invalid. Must be greater than or equal to 0.0 and "
                                                + "less than 800000.0.");
                }

                _easting = value;
            }
        }

        /// <summary>
        ///     Gets or sets the northing in meters relative to the origin of the British National Grid.
        /// </summary>
        /// <value> The northing. </value>
        /// <exception cref="ArgumentException"> If the northing is out of range </exception>
        public double Northing
        {
            get { return _northing; }
            set
            {
                if (value < 0.0 || value >= 1400000.0)
                {
                    throw new ArgumentException("Northing (" + value
                                                + ") is invalid. Must be greater than or equal to 0.0 and less "
                                                + "than 1400000.0.");
                }

                _northing = value;
            }
        }

        /// <summary>
        ///     Return a string representation of this OSGB grid reference showing the easting and northing.
        /// </summary>
        /// <returns> a string represenation of this OSGB grid reference. </returns>
        public override string ToString()
        {
            return "(" + _easting + ", " + _northing + ")";
        }

        /// <summary>
        ///     Return a string representation of this OSGB grid reference using the six-figure notation
        ///     in the form XY 12345 67890.
        /// </summary>
        /// <returns> A string representing this OSGB grid reference in ten-figure notation </returns>
        public string ToTenFigureString()
        {
            var e = (int) Math.Floor(_easting - 100000*HundredKmEast());
            var n = (int) Math.Floor(_northing - 100000*HundredKmNorth());

            var es = e.ToString("D5");
            var ns = n.ToString("D5");

            return GetGridReferenceLetters() + " " + es + " " + ns;
        }

        /// <summary>
        ///     Return a string representation of this OSGB grid reference using the six-figure notation
        ///     in the form XY123456.
        /// </summary>
        /// <returns> A string representing this OSGB grid reference in six-figure notation </returns>
        public string ToSixFigureString()
        {
            var e = (int) Math.Floor((_easting - 100000*HundredKmEast())/100);
            var n = (int) Math.Floor((_northing - 100000*HundredKmNorth())/100);

            var es = e.ToString("D3");
            var ns = n.ToString("D3");

            return GetGridReferenceLetters() + es + ns;
        }

        private int HundredKmEast()
        {
            return (int) Math.Floor(_easting/100000);
        }

        private int HundredKmNorth()
        {
            return (int) Math.Floor(_northing/100000);
        }

        private string GetGridReferenceLetters()
        {
            string firstLetter;
            if (HundredKmNorth() < 5)
            {
                if (HundredKmEast() < 5)
                {
                    firstLetter = "S";
                }
                else
                {
                    firstLetter = "T";
                }
            }
            else if (HundredKmNorth() < 10)
            {
                if (HundredKmEast() < 5)
                {
                    firstLetter = "N";
                }
                else
                {
                    firstLetter = "O";
                }
            }
            else
            {
                firstLetter = "H";
            }

            var index = 65 + (4 - HundredKmNorth()%5)*5 + HundredKmEast()%5;

            if (index >= 73)
                index++;
            var secondLetter = ((char) index).ToString();

            return firstLetter + secondLetter;
        }

        /// <summary>
        ///     Convert this OSGB grid reference to a latitude/longitude pair using the OSGB36 datum.
        ///     Note that, the LatLng object may need to be converted to the WGS84 datum depending on
        ///     the application.
        /// </summary>
        /// <returns>
        ///     A LatLng object representing this OSGB grid reference using the OSGB36 datum
        /// </returns>
        public override LatLng ToLatLng()
        {
            var OSGB_F0 = 0.9996012717;
            var N0 = -100000.0;
            var E0 = 400000.0;
            var phi0 = Util.ToRadians(49.0);
            var lambda0 = Util.ToRadians(-2.0);
            var a = Datum.ReferenceEllipsoid.SemiMajorAxis;
            var b = Datum.ReferenceEllipsoid.SemiMinorAxis;
            var eSquared = Datum.ReferenceEllipsoid.EccentricitySquared;
            var phi = 0.0;
            var lambda = 0.0;
            var E = _easting;
            var N = _northing;
            var n = (a - b)/(a + b);
            var M = 0.0;
            var phiPrime = (N - N0)/(a*OSGB_F0) + phi0;
            do
            {
                M = b*OSGB_F0
                    *((1 + n + 5.0/4.0*n*n + 5.0/4.0*n*n*n)*(phiPrime - phi0)
                      - (3*n + 3*n*n + 21.0/8.0*n*n*n)
                      *Math.Sin(phiPrime - phi0)*Math.Cos(phiPrime + phi0)
                      + (15.0/8.0*n*n + 15.0/8.0*n*n*n)
                      *Math.Sin(2.0*(phiPrime - phi0))*Math
                          .Cos(2.0*(phiPrime + phi0)) - 35.0/24.0*n*n*n
                      *Math.Sin(3.0*(phiPrime - phi0))*Math
                          .Cos(3.0*(phiPrime + phi0)));
                phiPrime += (N - N0 - M)/(a*OSGB_F0);
            } while (N - N0 - M >= 0.001);
            var v = a*OSGB_F0
                    *Math.Pow(1.0 - eSquared*Util.sinSquared(phiPrime), -0.5);
            var rho = a*OSGB_F0*(1.0 - eSquared)
                      *Math.Pow(1.0 - eSquared*Util.sinSquared(phiPrime), -1.5);
            var etaSquared = v/rho - 1.0;
            var VII = Math.Tan(phiPrime)/(2*rho*v);
            var VIII = Math.Tan(phiPrime)/(24.0*rho*Math.Pow(v, 3.0))
                       *(5.0 + 3.0*Util.tanSquared(phiPrime) + etaSquared - 9.0*Util
                           .tanSquared(phiPrime)*etaSquared);
            var IX = Math.Tan(phiPrime)/(720.0*rho*Math.Pow(v, 5.0))
                     *(61.0 + 90.0*Util.tanSquared(phiPrime) + 45.0*Util
                         .tanSquared(phiPrime)*Util.tanSquared(phiPrime));
            var X = Util.sec(phiPrime)/v;
            var XI = Util.sec(phiPrime)/(6.0*v*v*v)
                     *(v/rho + 2*Util.tanSquared(phiPrime));
            var XII = Util.sec(phiPrime)/(120.0*Math.Pow(v, 5.0))
                      *(5.0 + 28.0*Util.tanSquared(phiPrime) + 24.0*Util
                          .tanSquared(phiPrime)*Util.tanSquared(phiPrime));
            var XIIA = Util.sec(phiPrime)/(5040.0*Math.Pow(v, 7.0))
                       *(61.0 + 662.0*Util.tanSquared(phiPrime)
                         + 1320.0*Util.tanSquared(phiPrime)*Util.tanSquared(phiPrime) + 720.0
                         *Util.tanSquared(phiPrime)*
                         Util.tanSquared(phiPrime)*
                         Util
                             .tanSquared(phiPrime));
            phi = phiPrime - VII*Math.Pow(E - E0, 2.0)
                  + VIII*Math.Pow(E - E0, 4.0) - IX*Math.Pow(E - E0, 6.0);
            lambda = lambda0 + X*(E - E0) - XI*Math.Pow(E - E0, 3.0)
                     + XII*Math.Pow(E - E0, 5.0) - XIIA*Math.Pow(E - E0, 7.0);

            return new LatLng(Util.ToDegrees(phi), Util.ToDegrees(lambda), 0, Datum);
        }
    }
}