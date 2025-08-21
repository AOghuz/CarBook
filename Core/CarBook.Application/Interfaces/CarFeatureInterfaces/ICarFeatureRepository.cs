using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Domain.Entities;

namespace CarBook.Application.Interfaces.CarFeatureInterfaces
{
    public interface ICarFeatureRepository
    {
        Task<List<CarFeature>> GetCarFeaturesByCarIDAsync(int carID, CancellationToken ct = default);
        Task ChangeCarFeatureAvailableToFalseAsync(int id, CancellationToken ct = default);
        Task ChangeCarFeatureAvailableToTrueAsync(int id, CancellationToken ct = default);
        Task CreateCarFeatureByCarAsync(CarFeature carFeature, CancellationToken ct = default);
    }
}
