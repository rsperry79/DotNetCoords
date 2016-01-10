using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum.NAD27
{
    /// <summary>
    ///     Class representing the NAD27 (San Salvador) datum.
    /// </summary>
    public sealed class NAD27SanSalvadorDatum : Datum<NAD27SanSalvadorDatum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NAD27SanSalvadorDatum" /> class.
        /// </summary>
        public NAD27SanSalvadorDatum()
        {
            name = "North American Datum 1927 (NAD27) - San Salvador";
            ellipsoid = Clarke1866Ellipsoid.Instance;
            dx = 1.0;
            dy = 140.0;
            dz = 165.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}