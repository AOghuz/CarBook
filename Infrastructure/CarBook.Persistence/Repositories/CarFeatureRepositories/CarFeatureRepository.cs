using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Application.Interfaces.CarFeatureInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;

namespace CarBook.Persistence.Repositories.CarFeatureRepositories
{
    public class CarFeatureRepository : ICarFeatureRepository
    {
        private readonly CarBookContext _context;
        public CarFeatureRepository(CarBookContext context) => _context = context;

        public async Task ChangeCarFeatureAvailableToFalseAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.CarFeatures
                .FirstOrDefaultAsync(x => x.CarFeatureID == id, ct);

            if (entity == null) return; // istersen throw da edebilirsin
            entity.Available = false;
            await _context.SaveChangesAsync(ct);
        }

        public async Task ChangeCarFeatureAvailableToTrueAsync(int id, CancellationToken ct = default)
        {
            var entity = await _context.CarFeatures
                .FirstOrDefaultAsync(x => x.CarFeatureID == id, ct);

            if (entity == null) return;
            entity.Available = true;
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateCarFeatureByCarAsync(CarFeature carFeature, CancellationToken ct = default)
        {
            await _context.CarFeatures.AddAsync(carFeature, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<List<CarFeature>> GetCarFeaturesByCarIDAsync(int carID, CancellationToken ct = default)
        {
            return await _context.CarFeatures
                .Include(y => y.Feature)
                .Where(x => x.CarID == carID)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
