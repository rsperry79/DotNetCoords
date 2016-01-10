using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
    /// <summary>
    ///     Class representing the NAD27 (Cuba) datum.
    /// </summary>
    public sealed class NAD27CubaDatum : Datum<NAD27CubaDatum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NAD27CubaDatum" /> class.
        /// </summary>
        public NAD27CubaDatum()
        {
            name = "North American Datum 1927 (NAD27) - Cuba";
            ellipsoid = Clarke1866Ellipsoid.Instance;
            dx = -9.0;
            dy = 152.0;
            dz = 178.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}