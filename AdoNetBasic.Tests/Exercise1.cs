using AdoNetBasic.Models;
using AdoNetBasic.Repositories;
using AdoNetBasic.Tests.Locals;

namespace AdoNetBasic.Tests;

/// <summary>
/// ### DO NOT CHANGE THE CONTENT OF THIS TEST CLASS ###
/// In order to complete the exercises, implement the required methods in the specified Repository class.
/// Next, run the tests to check if implemented methods work as expected.
/// </summary>
[TestCaseOrderer("AdoNetBasic.Tests.Locals.TestsOrderer", "AdoNetBasic.Tests")]
public sealed class Exercise1
{
    private readonly AirportRepository _airportRepository;

    public Exercise1()
    {
        _airportRepository = new AirportRepository();
    }

    #region 1. READ

    [Fact]
    [RunSortedByName]
    public void Ex11_Implement_GetByAirportCode_method_of_AirportRepository()
    {
        // Arrange
        const string AirportCode = "PER";

        // Act
        Airport? airport = _airportRepository.GetByAirportCode(AirportCode);

        // Assert
        Assert.NotNull(airport);
        Assert.Equal("Perth Airport", airport.AirportName);
        Assert.Equal(894788888m, airport.ContactNo);
        Assert.Equal(31.9385d, airport.Longitude);
        Assert.Equal(115.9672d, airport.Latitude);
        Assert.Equal("AUS", airport.CountryCode);
    }

    [Fact]
    [RunSortedByName]
    public void Ex12_Implement_GetByCountryCode_method_of_AirportRepository()
    {
        // Arrange
        const string CountryCode = "NPL";

        // Act
        IReadOnlyList<Airport> airports = _airportRepository.GetByCountryCode(CountryCode);

        // Assert
        Assert.Equal(2, airports.Count);

        Airport? pokharaAirport = airports.SingleOrDefault(a => a.AirportCode == "PKR");
        Assert.NotNull(pokharaAirport);
        Assert.Equal("Pokhara Airport", pokharaAirport.AirportName);
        Assert.Equal(97761465979m, pokharaAirport.ContactNo);
        Assert.Equal(28.2d, pokharaAirport.Longitude);
        Assert.Equal(83.9817d, pokharaAirport.Latitude);
        Assert.Equal("NPL", pokharaAirport.CountryCode);

        Airport? tribhuwanAirport = airports.SingleOrDefault(a => a.AirportCode == "TIA");
        Assert.NotNull(tribhuwanAirport);
        Assert.Equal("Tribhuwan International Airport", tribhuwanAirport.AirportName);
        Assert.Equal(97714113033m, tribhuwanAirport.ContactNo);
        Assert.Equal(27.6981d, tribhuwanAirport.Longitude);
        Assert.Equal(85.3592d, tribhuwanAirport.Latitude);
        Assert.Equal("NPL", tribhuwanAirport.CountryCode);
    }

    [Fact]
    [RunSortedByName]
    public void Ex13_Implement_GetBySquareArea_method_of_AirportRepository()
    {
        // Arrange
        (double Longitude, double Latitude) NorthWest = (35d, 140d);
        (double Longitude, double Latitude) SouthEast = (40d, 180d);

        // Act
        IReadOnlyList<Airport> airports = _airportRepository.GetBySquareArea(
            northWestLongitude: NorthWest.Longitude,
            northWestLatitude: NorthWest.Latitude,
            southEastLongitude: SouthEast.Longitude,
            southEastLatitude: SouthEast.Latitude
        );

        // Assert
        Assert.Equal(2, airports.Count);

        Airport? aucklandAirport = airports.SingleOrDefault(a => a.AirportCode == "AKL");
        Assert.NotNull(aucklandAirport);
        Assert.Equal("Auckland Airport", aucklandAirport.AirportName);
        Assert.Equal(6492750789m, aucklandAirport.ContactNo);
        Assert.Equal(37.0082d, aucklandAirport.Longitude);
        Assert.Equal(174.785d, aucklandAirport.Latitude);
        Assert.Equal("NZL", aucklandAirport.CountryCode);

        Airport? melbourneAirport = airports.SingleOrDefault(a => a.AirportCode == "MEL");
        Assert.NotNull(melbourneAirport);
        Assert.Equal("Melbourne Airport", melbourneAirport.AirportName);
        Assert.Equal(392971600m, melbourneAirport.ContactNo);
        Assert.Equal(37.669d, melbourneAirport.Longitude);
        Assert.Equal(144.841d, melbourneAirport.Latitude);
        Assert.Equal("AUS", melbourneAirport.CountryCode);
    }

    #endregion

    #region 2. CREATE

    [Fact]
    [RunSortedByName]
    public void Ex21_Implement_Add_method_of_AirportRepository()
    {
        // Arrange
        Airport atlantaAirport = new()
        {
            AirportCode = "ATL",
            AirportName = "Hartsfield–Jackson Atlanta International Airport",
            ContactNo = 284387563m,
            Longitude = 33.6367d,
            Latitude = -84.4281d,
            CountryCode = "USA"
        };

        // Act
        int rowsAffected = _airportRepository.Add(atlantaAirport);

        // Assert
        Assert.Equal(1, rowsAffected);

        Airport? actual = _airportRepository.GetByAirportCode(atlantaAirport.AirportCode);
        Assert.NotNull(actual);
        Assert.Equal(atlantaAirport.AirportName, actual.AirportName);
        Assert.Equal(atlantaAirport.ContactNo, actual.ContactNo);
        Assert.Equal(atlantaAirport.Longitude, actual.Longitude);
        Assert.Equal(atlantaAirport.Latitude, actual.Latitude);
        Assert.Equal(atlantaAirport.CountryCode, actual.CountryCode);
    }

    [Fact]
    [RunSortedByName]
    public void Ex22_Implement_AddRange_method_of_AirportRepository()
    {
        // Arrange
        Airport[] airports = new Airport[]
        {
            new()
            {
                AirportCode = "MUC",
                AirportName = "Flughafen München Franz Josef Strauß",
                ContactNo = 758362746m,
                Longitude = 48.3538d,
                Latitude = 11.7861d,
                CountryCode = "GER"
            },
            new()
            {
                AirportCode = "FRA",
                AirportName = "Flughafen Frankfurt am Main",
                ContactNo = 583846754m,
                Longitude = 50.0330d,
                Latitude = 8.5705d,
                CountryCode = "GER"
            }
        };

        // Act
        int rowsAffected = _airportRepository.AddRange(airports);

        // Assert
        Assert.Equal(airports.Length, rowsAffected);

        Airport? munchenAirport = _airportRepository.GetByAirportCode(airports[0].AirportCode);
        Assert.NotNull(munchenAirport);
        Assert.Equal(airports[0].AirportName, munchenAirport.AirportName);
        Assert.Equal(airports[0].ContactNo, munchenAirport.ContactNo);
        Assert.Equal(airports[0].Longitude, munchenAirport.Longitude);
        Assert.Equal(airports[0].Latitude, munchenAirport.Latitude);
        Assert.Equal(airports[0].CountryCode, munchenAirport.CountryCode);

        Airport? frankfurtAirport = _airportRepository.GetByAirportCode(airports[1].AirportCode);
        Assert.NotNull(frankfurtAirport);
        Assert.Equal(airports[1].AirportName, frankfurtAirport.AirportName);
        Assert.Equal(airports[1].ContactNo, frankfurtAirport.ContactNo);
        Assert.Equal(airports[1].Longitude, frankfurtAirport.Longitude);
        Assert.Equal(airports[1].Latitude, frankfurtAirport.Latitude);
        Assert.Equal(airports[1].CountryCode, frankfurtAirport.CountryCode);
    }

    #endregion

    #region 3. UPDATE

    [Fact]
    [RunSortedByName]
    public void Ex31_Implement_Update_method_of_AirportRepository()
    {
        // Arrange
        const string AirportCode = "MUC";
        Airport munchenAirport = _airportRepository.GetByAirportCode(AirportCode)!;
        munchenAirport.AirportName = "New Munchen Airport";

        // Act
        int rowsAffected = _airportRepository.Update(munchenAirport);

        // Assert
        Assert.Equal(1, rowsAffected);

        Airport? actual = _airportRepository.GetByAirportCode(AirportCode);
        Assert.NotNull(actual);
        Assert.Equal(munchenAirport.AirportName, actual.AirportName);
    }

    #endregion

    #region 4. DELETE

    [Fact]
    [RunSortedByName]
    public void Ex41_Implement_DeleteByAirportCode_method_of_AirportRepository()
    {
        // Arrange
        const string AirportCode = "ATL";

        // Act
        int rowsAffected = _airportRepository.DeleteByAirportCode(AirportCode);

        // Assert
        Assert.Equal(1, rowsAffected);

        Airport? actual = _airportRepository.GetByAirportCode(AirportCode);
        Assert.Null(actual);
    }

    [Fact]
    [RunSortedByName]
    public void Ex42_Implement_DeleteByCountryCode_method_of_AirportRepository()
    {
        // Arrange
        const string CountryCode = "GER";

        // Act
        int rowsAffected = _airportRepository.DeleteByCountryCode(CountryCode);

        // Assert
        Assert.Equal(2, rowsAffected);

        IReadOnlyList<Airport> airports = _airportRepository.GetByCountryCode(CountryCode);
        Assert.Empty(airports);
    }

    #endregion
}