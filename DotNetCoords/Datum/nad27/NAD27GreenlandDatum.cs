using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
    /// <summary>
    ///     Class representing the NAD27 (Greenland) datum.
    /// </summary>
    public sealed class NAD27GreenlandDatum : Datum<NAD27GreenlandDatum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NAD27GreenlandDatum" /> class.
        /// </summary>
        public NAD27GreenlandDatum()
        {
            name = "North American Datum 1927 (NAD27) - Greenland";
            ellipsoid = Clarke1866Ellipsoid.Instance;
            dx = 11.0;
            dy = 114.0;
            dz = 195.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}