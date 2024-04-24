using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Business.Services;
using AdoNetBasic.Models;
using AdoNetBasic.Repositories;

namespace AdoNetBasic.Tests;

/// <summary>
/// ### DO NOT CHANGE THE CONTENT OF THIS TEST CLASS ###
/// In order to complete the exercises, implement the required methods in the specified Service and Repository classes.
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
    public async Task Ex13_GetAllFlightsDepartingFromCountryAsync_method_should_log_error_and_return_empty_list_if_no_flights_are_found()
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

    #region 2. FlightInstanceService - GetAllFlightInstancesWithinDateTimeLeaveRangeAsync

    [Fact]
    public async Task Ex21_Implement_GetAllFlightInstancesWithinDateTimeLeaveRangeAsync_method_of_FlightInstanceService()
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
        AssertAreEqualFlightInstanceDtoLists(expected, actual);
    }

    [Fact]
    public async Task Ex22_Implement_GetAllFlightInstancesWithinDateTimeLeaveRangeAsync_method_of_FlightInstanceService()
    {
        // Arrange
        DateTime StartDateTimeLeave = new(2016, 06, 15);
        DateTime EndDateTimeLeave = new(2016, 07, 15);

        // Act
        IReadOnlyList<FlightInstanceDto> actual = await _flightInstanceService.GetAllFlightInstancesWithinDateTimeLeaveRangeAsync(StartDateTimeLeave, EndDateTimeLeave);

        // Assert
        Assert.Empty(actual);
        Assert.Equal("GetAllFlightInstancesWithinDateTimeLeaveRangeAsync failure: No FlightInstance has been found in database", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region 3. FlightInstanceService - GetAllFlightInstancesServedByPlaneManufacturerAsync

    [Fact]
    public async Task Ex31_Implement_GetAllFlightInstancesServedByPlaneManufacturerAsync_method_of_FlightInstanceService()
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
        AssertAreEqualFlightInstanceDtoLists(expected, actual);
    }

    [Fact]
    public async Task Ex32_Implement_GetAllFlightInstancesServedByPlaneManufacturerAsync_method_of_FlightInstanceService()
    {
        // Arrange
        const string PlaneManufacturerName = "Messerschmitt";

        // Act
        IReadOnlyList<FlightInstanceDto> actual = await _flightInstanceService.GetAllFlightInstancesServedByPlaneManufacturerAsync(PlaneManufacturerName);

        // Assert
        Assert.Empty(actual);
        Assert.Equal("GetAllFlightInstancesServedByPlaneManufacturerAsync failure: No FlightInstance has been found in database", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region 4. FlightInstanceService - UpdateFlightInstanceCoPilot

    [Fact]
    public async Task Ex41_Implement_UpdateFlightInstanceCoPilotAsync_method_of_FlightInstanceService()
    {

        // Arrange
        const string FlightNo = "QR340";
        DateTime DateTimeLeave = new(2015, 12, 11, 10, 30, 0);
        const string CoPilotFirstName = "Maila";
        const string CoPilotLastName = "Battard";
        const int FlightInstanceId = 5;

        // Act
        int actual = await _flightInstanceService.UpdateFlightInstanceCoPilotAsync(FlightNo, DateTimeLeave, CoPilotFirstName, CoPilotLastName);

        // Assert
        Assert.Equal(1, actual);

        FlightInstance? flightInstance = await _flightInstanceRepository.GetByInstanceIdAsync(FlightInstanceId);
        Assert.NotNull(flightInstance);
        Assert.Equal(2, flightInstance.CoPilotAboardId);
    }

    [Fact]
    public async Task Ex42_Implement_UpdateFlightInstanceCoPilotAsync_method_of_FlightInstanceService()
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
        Assert.Equal("UpdateFlightInstanceCoPilotAsync failure: No FlightInstance has been found in database", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region 5. FlightInstanceService - SetDelayForFlightInstancesArrivingFromAirport

    [Fact]
    public async Task Ex51_Implement_SetDelayForFlightInstancesArrivingFromAirportAsync_method_of_FlightInstanceService()
    {

        // Arrange
        const string AirportCode = "DXB";
        TimeSpan Delay = new(1, 30, 0);
        const int FirstFlightInstanceId = 1;
        const int SecondFlightInstanceId = 7;

        DateTime firstFlightExpectedDateTimeArrive = (await _flightInstanceRepository.GetByInstanceIdAsync(FirstFlightInstanceId))!.DateTimeArrive + Delay;
        DateTime secondFlightExpectedDateTimeArrive = (await _flightInstanceRepository.GetByInstanceIdAsync(SecondFlightInstanceId))!.DateTimeArrive + Delay;

        // Act
        int actual = await _flightInstanceService.SetDelayForFlightInstancesArrivingFromAirportAsync(AirportCode, Delay);

        // Assert
        Assert.Equal(2, actual);

        DateTime firstFlightActualDateTimeArrive = (await _flightInstanceRepository.GetByInstanceIdAsync(FirstFlightInstanceId))!.DateTimeArrive;
        Assert.Equal(firstFlightExpectedDateTimeArrive, firstFlightActualDateTimeArrive);

        DateTime secondFlightActualDateTimeArrive = (await _flightInstanceRepository.GetByInstanceIdAsync(SecondFlightInstanceId))!.DateTimeArrive;
        Assert.Equal(secondFlightExpectedDateTimeArrive, secondFlightActualDateTimeArrive);
    }

    [Fact]
    public async Task Ex52_Implement_SetDelayForFlightInstancesArrivingFromAirportAsync_method_of_FlightInstanceService()
    {
        // Arrange
        const string AirportCode = "IJK";
        TimeSpan Delay = new(0, 0, 0);

        // Act
        int actual = await _flightInstanceService.SetDelayForFlightInstancesArrivingFromAirportAsync(AirportCode, Delay);

        // Assert
        Assert.Equal(0, actual);
        Assert.Equal("SetDelayForFlightInstancesArrivingFromAirportAsync failure: Delay cannot be zero", _flightInstanceService.ErrorMessage);
    }

    #endregion

    #region Locals

    private static void AssertAreEqualFlightInstanceDtoLists(IReadOnlyList<FlightInstanceDto> expected, IReadOnlyList<FlightInstanceDto> actual)
    {
        Assert.Equal(expected.Count, actual.Count);

        foreach (FlightInstanceDto expectedFlightInstance in expected)
        {
            FlightInstanceDto? actualFlightInstance = actual.SingleOrDefault(fi => fi.FlightNo == expectedFlightInstance.FlightNo && fi.DateTimeLeave == expectedFlightInstance.DateTimeLeave);
            Assert.NotNull(actualFlightInstance);
            AssertAreEqualFlightInstanceDtos(expectedFlightInstance, actualFlightInstance);
        }
    }

    private static void AssertAreEqualFlightInstanceDtos(FlightInstanceDto expected, FlightInstanceDto actual)
    {
        Assert.Equal(expected.FlightNo, actual.FlightNo);
        Assert.Equal(expected.DepartTo, actual.DepartTo);
        Assert.Equal(expected.ArriveFrom, actual.ArriveFrom);
        Assert.Equal(expected.DateTimeLeave, actual.DateTimeLeave);
        Assert.Equal(expected.DateTimeArrive, actual.DateTimeArrive);
        Assert.Equal(expected.Plane, actual.Plane);
        Assert.Equal(expected.Pilot, actual.Pilot);

        Assert.Equal(expected.AllAttendants.Count, actual.AllAttendants.Count);

        foreach (AttendantDto expectedAttendant in expected.AllAttendants)
        {
            AttendantDto? actualAttendant = actual.AllAttendants.SingleOrDefault(a => a.FirstName == expectedAttendant.FirstName && a.LastName == expectedAttendant.LastName);
            Assert.NotNull(actualAttendant);
            Assert.Equal(expectedAttendant, actualAttendant);
        }
    }

    #endregion
}