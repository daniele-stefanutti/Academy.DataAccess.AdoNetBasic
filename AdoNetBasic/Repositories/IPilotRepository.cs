using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IPilotRepository
{
    Task<Pilot?> GetByPilotIdAsync(int pilotId);
    Task<Pilot?> GetByFirstNameAndLastNameAsync(string firstName, string lastName);
}
