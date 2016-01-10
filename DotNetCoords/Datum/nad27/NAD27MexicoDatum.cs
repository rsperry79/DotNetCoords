using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
    /// <summary>
    ///     Class representing the NAD27 (Mexico) datum.
    /// </summary>
    public sealed class NAD27MexicoDatum : Datum<NAD27MexicoDatum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NAD27MexicoDatum" /> class.
        /// </summary>
        public NAD27MexicoDatum()
        {
            name = "North American Datum 1927 (NAD27) - Mexico";
            ellipsoid = Clarke1866Ellipsoid.Instance;
            dx = -12.0;
            dy = 130.0;
            dz = 190.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}