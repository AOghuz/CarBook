using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Application.Interfaces.CarDescriptionInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;

namespace CarBook.Persistence.Repositories.CarDescriptionRepositories
{
    public class CarDescriptionRepository : ICarDescriptionRepository
    {
        private readonly CarBookContext _context;
        public CarDescriptionRepository(CarBookContext context) => _context = context;

        // ICarDescriptionRepository:
        // Task<CarDescription?> GetCarDescriptionAsync(int carId, CancellationToken ct = default);
        public async Task<CarDescription?> GetCarDescriptionAsync(int carId, CancellationToken ct = default)
        {
            return await _context.CarDescriptions
                .AsNoTracking()                          // okuma -> tracker yok, daha hızlı
                .FirstOrDefaultAsync(x => x.CarID == carId, ct);
        }
    }
}
