using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

internal class AirportRepository : IAirportRepository
{
    #region READ

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    public Airport? GetByAirportCode(string airportCode) => throw new NotImplementedException();

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    public IReadOnlyList<Airport> GetByCountryCode(string countryCode) => throw new NotImplementedException();

    /// <remarks>
    /// Please, implement this method!
    /// 
    /// North West
    ///     \_________
    ///     |         |
    ///     |         |
    ///     |         |
    ///     |_________|
    ///                \
    ///            South East
    /// 
    /// </remarks>
    public IReadOnlyList<Airport> GetBySquareArea(double northWestLongitude, double northWestLatitude, double southEastLongitude, double southEastLatitude)
        => throw new NotImplementedException();

    #endregion

    #region CREATE

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int Add(Airport airport) => throw new NotImplementedException();

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int AddRange(IReadOnlyList<Airport> airports) => throw new NotImplementedException();

    #endregion

    #region UPDATE

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int Update(Airport airport) => throw new NotImplementedException();

    #endregion

    #region DELETE

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int DeleteByAirportCode(string airportCode) => throw new NotImplementedException();

    /// <remarks>
    /// Please, implement this method!
    /// </remarks>
    /// <returns>Number of affected rows</returns>
    public int DeleteByCountryCode(string countryCode) => throw new NotImplementedException();

    #endregion
}
