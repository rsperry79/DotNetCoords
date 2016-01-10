using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
    /// <summary>
    ///     Class representing the World Geodetic System 1984 (WGS84) datum.
    /// </summary>
    public class WGS84Datum : Datum<WGS84Datum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WGS84Datum" /> class.
        /// </summary>
        public WGS84Datum()
        {
            name = "World Geodetic System 1984 (WGS84)";
            ellipsoid = WGS84Ellipsoid.Instance;
            dx = 0.0;
            dy = 0.0;
            dz = 0.0;
            ds = 1.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}