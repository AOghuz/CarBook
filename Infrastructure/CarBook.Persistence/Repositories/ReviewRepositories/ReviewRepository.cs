using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Application.Interfaces.ReviewInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;

namespace CarBook.Persistence.Repositories.ReviewRepositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly CarBookContext _context;
        public ReviewRepository(CarBookContext context) => _context = context;

        public async Task<List<Review>> GetReviewsByCarIdAsync(int carId, CancellationToken ct = default)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Where(x => x.CarID == carId)
                .OrderByDescending(x => x.ReviewDate)
                .ToListAsync(ct);
        }

    }
}
