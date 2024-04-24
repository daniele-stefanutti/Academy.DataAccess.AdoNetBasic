using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IFlightAttendantRepository
{
    Task<IReadOnlyList<FlightAttendant>> GetAllByFlightInstanceIdAsync(int flightInstanceId);
    Task<bool> IsMentorAsync(int attendantId);
}
