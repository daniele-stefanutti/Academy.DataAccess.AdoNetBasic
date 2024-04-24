using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IFlightInstanceRepository
{
    Task<FlightInstance?> GetByInstanceIdAsync(int instanceId);
    Task<FlightInstance?> GetByFlightNoAndDateTimeLeaveAsync(string flightNo, DateTime dateTimeLeave);
    Task<IReadOnlyList<FlightInstance>> GetWithinDateTimeLeaveRangeAsync(DateTime startDateTimeLeave, DateTime endDateTimeLeave);
    Task<IReadOnlyList<FlightInstance>> GetByPlaneManufacturerNameAsync(string planeManufacturerName);
    Task<IReadOnlyList<FlightInstance>> GetByFlightArriveFromAirportCodeAsync(string airportCode);
    Task<int> UpdateAsync(FlightInstance flightInstance);
    Task<int> UpdateRangeAsync(IReadOnlyList<FlightInstance> flightInstances);
}
