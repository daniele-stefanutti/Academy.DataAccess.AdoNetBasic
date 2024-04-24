using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IFlightInstanceRepository
{
    Task<FlightInstance?> GetByInstanceIdAsync(int instanceId);

    /// <remarks>
    /// Please, complete the implementation of this interface
    /// </remarks>
}
