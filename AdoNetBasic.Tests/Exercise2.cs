using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Business.Services;
using AdoNetBasic.Repositories;

namespace AdoNetBasic.Tests;

/// <summary>
/// ### DO NOT CHANGE THE CONTENT OF THIS TEST CLASS ###
/// In order to complete the exercises, implement the required methods in the specified Repository class.
/// Next, run the tests to check if implemented methods work as expected.
/// </summary>
public sealed class Exercise2
{
    private readonly FlightService _flightService;

    public Exercise2()
    {
        _flightService = new FlightService(new FlightRepository(), new AirportRepository(), new CountryRepository());
    }

    #region 1. FlightService (EXAMPLE)

    [Fact]
    public async Task Ex11_GetFlightWithLongestDistanceAsync_method_should_provide_the_flight_covering_the_longest_distance()
    {
        // Arrange
        FlightDto expected = new
        (
            FlightNo: "JKL980",
            Distance: 9800,
            DepartureAirport: new
            (
                Code: "TIA",
                Longitude: 27.6981d,
                Latitude: 85.3592d,
                CountryName: "Nepal"
            ),
            ArrivalAirport: new
            (
                Code: "MEL",
                Longitude: 37.669d,
                Latitude: 144.841d,
                CountryName: "Australia"
            )
        );

        // Act
        FlightDto? actual = await _flightService.GetFlightWithLongestDistanceAsync();

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Ex12_GetAllFlightsDepartingFromCountryAsync_method_should_provide_all_flights_departing_from_given_country()
    {
        // Arrange
        const string CountryCode = "AUS";

        IReadOnlyList<FlightDto> expected = new List<FlightDto>()
        {
            new
            (
                FlightNo: "ABC123",
                Distance: 5500,
                DepartureAirport: new
                (
                    Code: "SYD",
                    Longitude: 33.9399d,
                    Latitude: 151.1753d,
                    CountryName: "Australia"
                ),
                ArrivalAirport: new
                (
                    Code: "TIA",
                    Longitude: 27.6981d,
                    Latitude: 85.3592d,
                    CountryName: "Nepal"
                )
            ),
            new
            (
                FlightNo: "STH650",
                Distance: 5680,
                DepartureAirport: new
                (
                    Code: "MEL",
                    Longitude: 37.669d,
                    Latitude: 144.841d,
                    CountryName: "Australia"
                ),
                ArrivalAirport: new
                (
                    Code: "PER",
                    Longitude: 31.9385d,
                    Latitude: 115.9672d,
                    CountryName: "Australia"
                )
            )
        };

        // Act
        IReadOnlyList<FlightDto> actual = await _flightService.GetAllFlightsDepartingFromCountryAsync(CountryCode);

        // Assert
        Assert.Equal(2, actual.Count);

        FlightDto firstFlight = Assert.Single(actual.Where(f => f.FlightNo == expected[0].FlightNo));
        Assert.Equal(expected[0], firstFlight);

        FlightDto secondFlight = Assert.Single(actual.Where(f => f.FlightNo == expected[1].FlightNo));
        Assert.Equal(expected[1], secondFlight);
    }

    [Fact]
    public async Task Ex12_GetAllFlightsDepartingFromCountryAsync_method_should_log_error_and_return_empty_list_if_no_flights_are_found()
    {
        // Arrange
        const string CountryCode = "Neverland";

        // Act
        IReadOnlyList<FlightDto> actual = await _flightService.GetAllFlightsDepartingFromCountryAsync(CountryCode);

        // Assert
        Assert.Empty(actual);
        Assert.Equal("GetAllFlightsDepartingFromCountryAsync failure: No Flight has been found in database", _flightService.ErrorMessage);
    }

    #endregion
}