using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
    /// <summary>
    ///     Class representing the NAD27 (Aleutian East) datum.
    /// </summary>
    public sealed class NAD27AleutianEastDatum : Datum<NAD27AleutianEastDatum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NAD27AleutianEastDatum" /> class.
        /// </summary>
        public NAD27AleutianEastDatum()
        {
            name = "North American Datum 1927 (NAD27) - Aleutian East";
            ellipsoid = Clarke1866Ellipsoid.Instance;
            dx = -2.0;
            dy = 152.0;
            dz = 149.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}