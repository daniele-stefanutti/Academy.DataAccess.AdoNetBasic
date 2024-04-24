using AdoNetBasic.Models;

namespace AdoNetBasic.Repositories;

public interface IPlaneDetailRepository
{
    Task<PlaneDetail?> GetByPlaneIdAsync(int planeId);
}
