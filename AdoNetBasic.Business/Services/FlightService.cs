using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Business.Exceptions;
using AdoNetBasic.Business.Mappers;
using AdoNetBasic.Models;
using AdoNetBasic.Repositories;

namespace AdoNetBasic.Business.Services;

/// <remarks>
/// This example illustrates a simple asynchronous service with basic errors handling
/// </remarks>
internal sealed class FlightService : BaseService, IFlightService
{
	private readonly IFlightRepository _flightRepository;
	private readonly IAirportRepository _airportRepository;
	private readonly ICountryRepository _countryRepository;

    /// <remarks>
    /// In modern services, the needed instances are provided by Dependency Injection (DI) system
    /// </remarks>
    public FlightService(IFlightRepository flightRepository, IAirportRepository airportRepository, ICountryRepository countryRepository)
    {
		_flightRepository = flightRepository;
        _airportRepository = airportRepository;
        _countryRepository = countryRepository;
    }

    public async Task<FlightDto?> GetFlightWithLongestDistanceAsync()
        => throw new NotImplementedException();

    public async Task<IReadOnlyList<FlightDto>> GetAllFlightsDepartingFromCountryAsync(string countryCode)
        => throw new NotImplementedException();
}
