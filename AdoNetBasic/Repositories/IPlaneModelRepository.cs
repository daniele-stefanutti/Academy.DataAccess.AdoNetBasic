using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IPlaneModelRepository
{
    Task<PlaneModel?> GetByModelNumberAsync(string modelNumber);
}
