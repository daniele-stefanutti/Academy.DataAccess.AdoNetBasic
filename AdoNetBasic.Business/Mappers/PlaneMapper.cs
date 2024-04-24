using AdoNetBasic.Business.Dtos;
using AdoNetBasic.Models;

namespace AdoNetBasic.Business.Mappers;

internal static class PlaneMapper
{
    public static PlaneDto Map(PlaneDetail planeDetail, PlaneModel planeModel)
        => new
        (
            ManufacturerName: planeModel.ManufacturerName,
            ModelNumber: planeDetail.ModelNumber,
            RegistrationNo: planeDetail.RegistrationNo
        );
}
