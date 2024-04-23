namespace AdoNetBasic.Business.Dtos;

public sealed record AirportDto
(
    string Code,
    double Longitude,
    double Latitude,
    string CountryName
);
