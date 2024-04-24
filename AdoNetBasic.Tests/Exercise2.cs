using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Business.Services;
using AdoNetBasic.Models;
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
    private readonly FlightInstanceRepository _flightInstanceRepository;
    private readonly FlightInstanceService _flightInstanceService;

    public Exercise2()
    {
        _flightService = new FlightService(
            new FlightRepository(),
            new AirportRepository(),
            new CountryRepository()
        );
        _flightInstanceRepository = new FlightInstanceRepository();
        _flightInstanceService = new
        (
            _flightInstanceRepository,
            new PlaneDetailRepository(),
            new PlaneModelRepository(),
            new PilotRepository(),
            new FlightAttendantRepository(),
            new FlightRepository()
        );
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

    #region 2. FlightInstanceService - GetAllFlightInstancesWithinDateTimeLeaveRange

    [Fact]
    public async Task Ex21_Implement_GetAllFlightInstancesWithinDateTimeLeaveRange_method_of_FlightInstanceService()
    {
        // Arrange
        DateTime StartDateTimeLeave = new(2017, 12, 10);
        DateTime EndDateTimeLeave = new(2017, 12, 13);

        IReadOnlyList<FlightInstanceDto> expected = new List<FlightInstanceDto>()
        {
            new
            (
                FlightNo: "JKL980",
                DepartTo: "MEL",
                ArriveFrom: "TIA",
                DateTimeLeave: new DateTime(2017, 12, 11, 10, 30, 0),
                DateTimeArrive: new DateTime(2017, 12, 14, 10, 30, 0),
                Plane: new
                (
                    ManufacturerName: "Boeing",
                    ModelNumber: "777",
                    RegistrationNo: "BO-1990"
                ),
                Pilot: new
                (
                    FirstName: "Tom",
                    LastName: "Hardy",
                    Age: 46
                ),
                AllAttendants: new AttendantDto[]
                {
                    new(
                        FirstName: "John",
                        LastName: "Rai",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Pramesh",
                        LastName: "Shrestha",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Ram",
                        LastName: "Sharma",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Amol",
                        LastName: "Pokharel",
                        IsMentor: true
                    )
                }
            ),
            new
            (
                FlightNo: "STH650",
                DepartTo: "PER",
                ArriveFrom: "MEL",
                DateTimeLeave: new DateTime(2017, 12, 11, 10, 30, 0),
                DateTimeArrive: new DateTime(2017, 12, 14, 10, 30, 0),
                Plane: new
                (
                    ManufacturerName: "Airbus",
                    ModelNumber: "A390",
                    RegistrationNo: "AU-1989"
                ),
                Pilot: new
                (
                    FirstName: "Huge",
                    LastName: "Glass",
                    Age: 43
                ),
                AllAttendants: new AttendantDto[]
                {
                    new(
                        FirstName: "Greg",
                        LastName: "Nepal",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Hari",
                        LastName: "Cobin",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Pratik",
                        LastName: "Shrestha",
                        IsMentor: false
                    )
                }
            )
        };

        // Act
        IReadOnlyList<FlightInstanceDto> actual = await _flightInstanceService.GetAllFlightInstancesWithinDateTimeLeaveRangeAsync(StartDateTimeLeave, EndDateTimeLeave);

        // Assert
        Assert.Equal(2, actual.Count);

        FlightInstanceDto firstFlightInstance = Assert.Single(actual.Where(f => f.FlightNo == expected[0].FlightNo));
        Assert.Equal(expected[0], firstFlightInstance);

        FlightInstanceDto secondFlightInstance = Assert.Single(actual.Where(f => f.FlightNo == expected[1].FlightNo));
        Assert.Equal(expected[1], secondFlightInstance);
    }

    [Fact]
    public async Task Ex22_Implement_GetAllFlightInstancesWithinDateTimeLeaveRange_method_of_FlightInstanceService()
    {
        // Arrange
        DateTime StartDateTimeLeave = new(2016, 06, 15);
        DateTime EndDateTimeLeave = new(2016, 07, 15);

        // Act
        IReadOnlyList<FlightInstanceDto> actual = await _flightInstanceService.GetAllFlightInstancesWithinDateTimeLeaveRangeAsync(StartDateTimeLeave, EndDateTimeLeave);

        // Assert
        Assert.Empty(actual);
        Assert.Equal("GetAllFlightInstancesWithinDateTimeLeaveRange failure: No FlightInstance has been found in database", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region 3. FlightInstanceService - GetAllFlightInstancesServedByPlaneManufacturer

    [Fact]
    public async Task Ex31_Implement_GetAllFlightInstancesServedByPlaneManufacturer_method_of_FlightInstanceService()
    {

        // Arrange
        const string PlaneManufacturerName = "Boeing";

        IReadOnlyList<FlightInstanceDto> expected = new List<FlightInstanceDto>()
        {
            new
            (
                FlightNo: "JKL980",
                DepartTo: "MEL",
                ArriveFrom: "TIA",
                DateTimeLeave: new DateTime(2017, 12, 11, 10, 30, 0),
                DateTimeArrive: new DateTime(2017, 12, 14, 10, 30, 0),
                Plane: new
                (
                    ManufacturerName: "Boeing",
                    ModelNumber: "777",
                    RegistrationNo: "BO-1990"
                ),
                Pilot: new
                (
                    FirstName: "Tom",
                    LastName: "Hardy",
                    Age: 46
                ),
                AllAttendants: new AttendantDto[]
                {
                    new(
                        FirstName: "John",
                        LastName: "Rai",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Pramesh",
                        LastName: "Shrestha",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Ram",
                        LastName: "Sharma",
                        IsMentor: true
                    ),
                    new(
                        FirstName: "Amol",
                        LastName: "Pokharel",
                        IsMentor: true
                    )
                }
            )
        };

        // Act
        IReadOnlyList<FlightInstanceDto> actual = await _flightInstanceService.GetAllFlightInstancesServedByPlaneManufacturerAsync(PlaneManufacturerName);

        // Assert
        FlightInstanceDto flightInstance = Assert.Single(actual);
        Assert.Equal(expected[0], flightInstance);
    }

    [Fact]
    public async Task Ex32_Implement_GetAllFlightInstancesServedByPlaneManufacturer_method_of_FlightInstanceService()
    {
        // Arrange
        const string PlaneManufacturerName = "Messerschmitt";

        // Act
        IReadOnlyList<FlightInstanceDto> actual = await _flightInstanceService.GetAllFlightInstancesServedByPlaneManufacturerAsync(PlaneManufacturerName);

        // Assert
        Assert.Empty(actual);
        Assert.Equal("GetAllFlightInstancesServedByPlaneManufacturer failure: No FlightInstance has been found in database", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region 4. FlightInstanceService - UpdateFlightInstanceCoPilot

    [Fact]
    public async Task Ex41_Implement_UpdateFlightInstanceCoPilot_method_of_FlightInstanceService()
    {

        // Arrange
        const string FlightNo = "QR340";
        DateTime DateTimeLeave = new(2015, 12, 11, 10, 30, 0);
        const string CoPilotFirstName = "Maila";
        const string CoPilotLastName = "Battard";

        // Act
        int actual = await _flightInstanceService.UpdateFlightInstanceCoPilotAsync(FlightNo, DateTimeLeave, CoPilotFirstName, CoPilotLastName);

        // Assert
        Assert.Equal(1, actual);

        FlightInstance? flightInstance = await _flightInstanceRepository.GetByInstanceIdAsync(5);
        Assert.NotNull(flightInstance);
        Assert.Equal(2, flightInstance.CoPilotAboardId);
    }

    [Fact]
    public async Task Ex42_Implement_UpdateFlightInstanceCoPilot_method_of_FlightInstanceService()
    {
        // Arrange
        const string FlightNo = "FK001";
        DateTime DateTimeLeave = new(1918, 04, 21, 10, 30, 0);
        const string CoPilotFirstName = "Lothar";
        const string CoPilotLastName = "von Richthofen";

        // Act
        int actual = await _flightInstanceService.UpdateFlightInstanceCoPilotAsync(FlightNo, DateTimeLeave, CoPilotFirstName, CoPilotLastName);

        // Assert
        Assert.Equal(0, actual);
        Assert.Equal("UpdateFlightInstanceCoPilot failure: No FlightInstance has been found in database", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region 5. FlightInstanceService - SetDelayForFlightInstancesArrivingFromAirport

    [Fact]
    public async Task Ex51_Implement_SetDelayForFlightInstancesArrivingFromAirport_method_of_FlightInstanceService()
    {

        // Arrange
        const string AirportCode = "MEL";
        TimeSpan Delay = new(1, 30, 0);

        // Act
        int actual = await _flightInstanceService.SetDelayForFlightInstancesArrivingFromAirportAsync(AirportCode, Delay);

        // Assert
        Assert.Equal(1, actual);

        FlightInstance? firstFlightInstance = await _flightInstanceRepository.GetByInstanceIdAsync(11);
        Assert.NotNull(firstFlightInstance);
        Assert.Equal(new DateTime(2015, 12, 14, 12, 0, 0), firstFlightInstance.DateTimeArrive);

        FlightInstance? secondFlightInstance = await _flightInstanceRepository.GetByInstanceIdAsync(16);
        Assert.NotNull(secondFlightInstance);
        Assert.Equal(new DateTime(2017, 12, 14, 12, 0, 0), secondFlightInstance.DateTimeArrive);
    }

    [Fact]
    public async Task Ex52_Implement_SetDelayForFlightInstancesArrivingFromAirport_method_of_FlightInstanceService()
    {
        // Arrange
        const string AirportCode = "IJK";
        TimeSpan Delay = new(0, 0, 0);

        // Act
        int actual = await _flightInstanceService.SetDelayForFlightInstancesArrivingFromAirportAsync(AirportCode, Delay);

        // Assert
        Assert.Equal(0, actual);
        Assert.Equal("SetDelayForFlightInstancesArrivingFromAirport failure: Delay cannot be zero", _flightInstanceService.ErrorMessage);
    }

    #endregion
}