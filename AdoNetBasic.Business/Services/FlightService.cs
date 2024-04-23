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
    {
        try
        {
            Flight? flight = await _flightRepository.GetWithLongestDistanceAsync();
			return flight != null ? await GetFlightDtoAsync(flight) : throw new NoDataException(nameof(Flight));
        }
        catch (Exception ex)
        {
            LogError($"{nameof(GetFlightWithLongestDistanceAsync)} failure: {ex.Message}");
            return null;
        }
    }

    public async Task<IReadOnlyList<FlightDto>> GetAllFlightsDepartingFromCountryAsync(string countryCode)
    {
		try
		{
			IReadOnlyList<Flight> flights = await _flightRepository.GetDepartingFromCountryAsync(countryCode);

            if (!flights.Any())
                throw new NoDataException(nameof(Flight));

            List<FlightDto> results = new();

			foreach (Flight flight in flights)
				results.Add(await GetFlightDtoAsync(flight));

			return results;
        }
		catch (Exception ex)
		{
			LogError($"{nameof(GetAllFlightsDepartingFromCountryAsync)} failure: {ex.Message}");
			return Array.Empty<FlightDto>();
		}
    }

    #region Locals

    private async Task<FlightDto> GetFlightDtoAsync(Flight flight)
	{
		AirportDto departureAirport = await GetAirportDtoAsync(flight.FlightArriveFrom);
		AirportDto arrivalAirport = await GetAirportDtoAsync(flight.FlightDepartTo);
		return FlightMapper.Map(flight, departureAirport, arrivalAirport);
    }

	private async Task<AirportDto> GetAirportDtoAsync(string airportCode)
	{
		Airport? airport = await _airportRepository.GetByAirportCodeAsync(airportCode)
			?? throw new ItemNotFoundException(nameof(Airport), airportCode);

		string countryName = await GetCountryNameAsync(airport.CountryCode);
		return AirportMapper.Map(airport, countryName);
    }

	private async Task<string> GetCountryNameAsync(string countryCode)
	{
		Country? country = await _countryRepository.GetByCountryCodeAsync(countryCode);
		return country?.CountryName ?? throw new ItemNotFoundException(nameof(Country), countryCode);
    }

    #endregion
}
