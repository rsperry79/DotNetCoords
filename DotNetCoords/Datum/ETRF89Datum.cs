using DotNetCoords.Ellipsoid;

namespace DotNetCoords.Datum
{
    /// <summary>
    ///     Class representing the European Terrestrial Reference Frame (ETRF89) datum.
    /// </summary>
    public sealed class ETRF89Datum : Datum<ETRF89Datum>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ETRF89Datum" /> class.
        /// </summary>
        public ETRF89Datum()
        {
            name = "European Terrestrial Reference Frame (ETRF89)";
            ellipsoid = WGS84Ellipsoid.Instance;
            dx = 0.0;
            dy = 0.0;
            dz = 0.0;
            ds = 0.0;
            rx = 0.0;
            ry = 0.0;
            rz = 0.0;
        }
    }
}