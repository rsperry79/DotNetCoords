using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
    /// <summary>
    ///     Class representing the European 1950 (ED50) datum.
    /// </summary>
    public class ED50Datum : Datum<ED50Datum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ED50Datum" /> class.
        /// </summary>
        public ED50Datum()
        {
            name = "European Datum 1950";
            ellipsoid = InternationalEllipsoid.Instance;
            dx = -87;
            dy = -98;
            dz = -121;
        }
    }
}