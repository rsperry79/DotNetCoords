using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
    /// <summary>
    ///     Class representing the NAD27 (Eastern US) datum.
    /// </summary>
    public sealed class NAD27EasternUSDatum : Datum<NAD27EasternUSDatum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NAD27EasternUSDatum" /> class.
        /// </summary>
        public NAD27EasternUSDatum()
        {
            name = "North American Datum 1927 (NAD27) - Eastern US";
            ellipsoid = Clarke1866Ellipsoid.Instance;
            dx = -9.0;
            dy = 161.0;
            dz = 179.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}