using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CarBook.Application.Interfaces.StatisticsInterfaces;
using CarBook.Persistence.Context;

namespace CarBook.Persistence.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly CarBookContext _context;

        public StatisticsRepository(CarBookContext context)
        {
            _context = context;
        }

        public async Task<string?> GetBlogTitleByMaxBlogCommentAsync()
        {
            // Select Top(1) BlogId, Count(*) as Sayi From Comments Group By BlogID Order By Sayi Desc
            var values = await _context.Comments
                .GroupBy(x => x.BlogID)
                .Select(g => new { BlogID = g.Key, Count = g.Count() })
                .OrderByDescending(z => z.Count)
                .FirstOrDefaultAsync();

            if (values == null) return null;

            var blogName = await _context.Blogs
                .Where(x => x.BlogID == values.BlogID)
                .Select(y => y.Title)
                .FirstOrDefaultAsync();

            return blogName;
        }

        public async Task<string?> GetBrandNameByMaxCarAsync()
        {
            // Select Top(1) BrandId, Count(*) as ToplamArac From Cars Group By BrandId Order By ToplamArac Desc
            var values = await _context.Cars
                .GroupBy(x => x.BrandID)
                .Select(g => new { BrandID = g.Key, Count = g.Count() })
                .OrderByDescending(z => z.Count)
                .FirstOrDefaultAsync();

            if (values == null) return null;

            var brandName = await _context.Brands
                .Where(x => x.BrandID == values.BrandID)
                .Select(y => y.Name)
                .FirstOrDefaultAsync();

            return brandName;
        }

        public async Task<int> GetAuthorCountAsync()
            => await _context.Authors.CountAsync();

        public async Task<decimal> GetAvgRentPriceForDailyAsync()
        {
            var id = await _context.Pricings
                .Where(y => y.Name == "Günlük")
                .Select(z => z.PricingID)
                .FirstOrDefaultAsync();

            return await _context.CarPricings
                .Where(w => w.PricingID == id)
                .AverageAsync(x => x.Amount);
        }

        public async Task<decimal> GetAvgRentPriceForMonthlyAsync()
        {
            var id = await _context.Pricings
                .Where(y => y.Name == "Aylık")
                .Select(z => z.PricingID)
                .FirstOrDefaultAsync();

            return await _context.CarPricings
                .Where(w => w.PricingID == id)
                .AverageAsync(x => x.Amount);
        }

        public async Task<decimal> GetAvgRentPriceForWeeklyAsync()
        {
            var id = await _context.Pricings
                .Where(y => y.Name == "Haftalık")
                .Select(z => z.PricingID)
                .FirstOrDefaultAsync();

            return await _context.CarPricings
                .Where(w => w.PricingID == id)
                .AverageAsync(x => x.Amount);
        }

        public async Task<int> GetBlogCountAsync()
            => await _context.Blogs.CountAsync();

        public async Task<int> GetBrandCountAsync()
            => await _context.Brands.CountAsync();

        public async Task<string?> GetCarBrandAndModelByRentPriceDailyMaxAsync()
        {
            var pricingID = await _context.Pricings
                .Where(x => x.Name == "Günlük")
                .Select(y => y.PricingID)
                .FirstOrDefaultAsync();

            var amount = await _context.CarPricings
                .Where(y => y.PricingID == pricingID)
                .MaxAsync(x => x.Amount);

            var carId = await _context.CarPricings
                .Where(x => x.Amount == amount && x.PricingID == pricingID)
                .Select(y => y.CarID)
                .FirstOrDefaultAsync();

            var brandModel = await _context.Cars
                .Where(x => x.CarID == carId)
                .Include(y => y.Brand)
                .Select(z => z.Brand.Name + " " + z.Model)
                .FirstOrDefaultAsync();

            return brandModel;
        }

        public async Task<string?> GetCarBrandAndModelByRentPriceDailyMinAsync()
        {
            var pricingID = await _context.Pricings
                .Where(x => x.Name == "Günlük")
                .Select(y => y.PricingID)
                .FirstOrDefaultAsync();

            var amount = await _context.CarPricings
                .Where(y => y.PricingID == pricingID)
                .MinAsync(x => x.Amount);

            var carId = await _context.CarPricings
                .Where(x => x.Amount == amount && x.PricingID == pricingID)
                .Select(y => y.CarID)
                .FirstOrDefaultAsync();

            var brandModel = await _context.Cars
                .Where(x => x.CarID == carId)
                .Include(y => y.Brand)
                .Select(z => z.Brand.Name + " " + z.Model)
                .FirstOrDefaultAsync();

            return brandModel;
        }

        public async Task<int> GetCarCountAsync()
            => await _context.Cars.CountAsync();

        public async Task<int> GetCarCountByFuelElectricAsync()
            => await _context.Cars.Where(x => x.Fuel == "Elektrik").CountAsync();

        public async Task<int> GetCarCountByFuelGasolineOrDieselAsync()
            => await _context.Cars.Where(x => x.Fuel == "Benzin" || x.Fuel == "Dizel").CountAsync();

        public async Task<int> GetCarCountByKmSmallerThen1000Async()
            => await _context.Cars.Where(x => x.Km <= 1000).CountAsync();

        public async Task<int> GetCarCountByTranmissionIsAutoAsync()
            => await _context.Cars.Where(x => x.Transmission == "Otomatik").CountAsync();

        public async Task<int> GetLocationCountAsync()
            => await _context.Locations.CountAsync();
    }
}
