namespace AdoNetBasic.Business.Dtos;

public sealed record FlightDto
(
    string FlightNo,
    AirportDto DepartureAirport,
    AirportDto ArrivalAirport,
    int Distance
);
