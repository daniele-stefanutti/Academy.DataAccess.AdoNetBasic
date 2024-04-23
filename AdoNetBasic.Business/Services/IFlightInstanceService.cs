using AdoNetBasic.Business.Dtos;

namespace AdoNetBasic.Business.Services;

public interface IFlightInstanceService
{
    Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesWithinDateTimeLeaveRange(DateTime startDateTimeLeave, DateTime endDateTimeLeave);
    Task<IReadOnlyList<FlightInstanceDto>> GetAllFlightInstancesServedByPlaneManufacturer(string planeManufacturerName);
    Task<int> UpdateFlightInstanceCoPilot(string flightNo, DateTime dateTimeLeave, string coPilotFirstName, string coPilotLastName);
    Task<int> SetDelayForFlightInstancesArrivingFromAirport(string airportCode, TimeSpan delay);
}
