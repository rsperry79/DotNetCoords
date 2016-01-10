namespace DotNetCoords
{
    /// <summary>
    ///     Enumerated type used to indicate the required precision
    /// </summary>
    public enum Precision
    {
        /// <summary>
        ///     Used to indicate a required precision of 10000m (10km).
        /// </summary>
        Precision10000M = 10000,

        /// <summary>
        ///     Used to indicate a required precision of 1000m (1km).
        /// </summary>
        Precision1000M = 1000,

        /// <summary>
        ///     Used to indicate a required precision of 100m.
        /// </summary>
        Precision100M = 100,

        /// <summary>
        ///     Used to indicate a required precision of 10m.
        /// </summary>
        Precision10M = 10,

        /// <summary>
        ///     Used to indicate a required precision of 1m.
        /// </summary>
        Precision1M = 1
    }
}