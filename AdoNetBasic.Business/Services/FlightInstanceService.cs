using AdoNetBasic.Business.Dtos;

namespace AdoNetBasic.Business.Services;

/// <remarks>
/// For the implementation of the methods in this class:
///     + implement needed Model classes
///     + implement needed Repository classes
///     + implement needed Mappers classes
///     + implement service class methods
/// </remarks>
internal sealed class FlightInstanceService : BaseService, IFlightInstanceService
{
    /// <summary>
    /// Retrieve flight instances having leaving date and time within the given range
    /// </summary>
    /// <param name="startDateTimeLeave">Beginning date and time of the range</param>
    /// <param name="endDateTimeLeave">Ending date and time of the range</param>
    /// <remarks>
    /// Please, implement this method by using asynchronous ADO.NET methods!
    /// </remarks>
    public Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesWithinDateTimeLeaveRange(DateTime startDateTimeLeave, DateTime endDateTimeLeave)
        => throw new NotImplementedException();

    /// <summary>
    /// Retrieve flight instances served by a plane produced by the given manufacturer
    /// </summary>
    /// <param name="planeManufacturerName">Name of the plane's manufacturer</param>
    /// <remarks>
    /// Please, implement this method by using asynchronous ADO.NET methods!
    /// </remarks>
    public Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesServedByPlaneManufacturer(string planeManufacturerName)
        => throw new NotImplementedException();

    /// <summary>
    /// Update the co-pilot for the flight instance having the given flight number and departure date and time
    /// </summary>
    /// <param name="flightNo">Given flight number</param>
    /// <param name="dateTimeLeave">Given departure date and time</param>
    /// <param name="coPilotFirstName">First name of the updated co-pilot</param>
    /// <param name="coPilotLastName">Last name of the updated co-pilot</param>
    /// <returns>Number of affected rows</returns>
    public Task<int> UpdateFlightInstanceCoPilot(string flightNo, DateTime dateTimeLeave, string coPilotFirstName, string coPilotLastName)
        => throw new NotImplementedException();

    /// <summary>
    /// Update the arrival date and time for the flight instances departing from the given airport by setting the given delay
    /// </summary>
    /// <param name="airportCode">Code of the departure airport</param>
    /// <param name="delay">Delay on arrival date and time</param>
    /// <returns>Number of affected rows</returns>
    public Task<int> SetDelayForFlightInstancesArrivingFromAirport(string airportCode, TimeSpan delay)
        => throw new NotImplementedException();
}
