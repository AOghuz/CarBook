using CarBook.Application.Interfaces.CarInterfaces;
using CarBook.Domain.Entities;
using CarBook.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Persistence.Repositories.CarRepositories
{
	// CarBook.Persistence.Repositories.CarRepositories
	public class CarRepository : ICarRepository
	{
		private readonly CarBookContext _context;
		public CarRepository(CarBookContext context) => _context = context;

		public async Task<int> GetCarCountAsync() => await _context.Cars.CountAsync();

		public List<Car> GetCarListWithBrands()
			=> _context.Cars.Include(x => x.Brand).ToList();

		public List<Car> GetLast5CarsWithBrands()
			=> _context.Cars.Include(x => x.Brand)
							.OrderByDescending(x => x.CarID)
							.Take(5).ToList();

		// yeni:
		public async Task<Car?> GetByIdWithBrandAsync(int id, CancellationToken ct = default)
			=> await _context.Cars
							 .Include(x => x.Brand)
							 .AsNoTracking()
							 .FirstOrDefaultAsync(x => x.CarID == id, ct);
	}

}
