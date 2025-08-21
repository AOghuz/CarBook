using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarBook.Application.Interfaces.CarPricingInterfaces;
using CarBook.Persistence.Context;
using CarBook.Domain.Entities;
using CarBook.Application.ViewModels;

namespace CarBook.Persistence.Repositories.CarPricingRepositories
{
    public class CarPricingRepository : ICarPricingRepository
    {
        private readonly CarBookContext _context;
        public CarPricingRepository(CarBookContext context)
        {
            _context = context;
        }
        public List<CarPricing> GetCarPricingWithCars()
        {
            var values = _context.CarPricings.Include(x => x.Car).ThenInclude(y => y.Brand).Include(x => x.Pricing).Where(z => z.PricingID == 2).ToList();
            return values;
        }
        public List<CarPricing> GetCarPricingWithTimePeriod()
        {
            throw new NotImplementedException();
        }
        public Task<List<CarPricingViewModel>> GetCarPricingWithTimePeriod1()
        {
            var values = new List<CarPricingViewModel>();

            using (var connection = _context.Database.GetDbConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"SELECT * FROM (
                      SELECT Cars.Model, Brands.Name, Cars.CoverImageUrl, CarPricings.PricingID, CarPricings.Amount
                      FROM CarPricings
                      INNER JOIN Cars   ON Cars.CarID   = CarPricings.CarId
                      INNER JOIN Brands ON Brands.BrandID = Cars.BrandID
                  ) AS SourceTable
                  PIVOT (SUM(Amount) FOR PricingID IN ([2],[3],[4])) AS PivotTable;";

                    command.CommandType = System.Data.CommandType.Text;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var vm = new CarPricingViewModel
                            {
                                Brand = reader["Name"]?.ToString(),
                                Model = reader["Model"]?.ToString(),
                                CoverImageUrl = reader["CoverImageUrl"]?.ToString(),
                                Amounts = new List<decimal>
                        {
                            reader["2"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["2"]),
                            reader["3"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["3"]),
                            reader["4"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["4"])
                        }
                            };
                            values.Add(vm);
                        }
                    }
                }
            }

            return Task.FromResult(values); // <-- kritik kısım
        }


    }
}



















/*
 var values = from x in _context.CarPricings
						 group x by x.PricingID into g
						 select new
						 {
							 CarId = g.Key,
							 DailyPrice = g.Where(y => y.CarPricingID == 2).Sum(z => z.Amount),
							 WeeklyPrice = g.Where(y => y.CarPricingID == 3).Sum(z => z.Amount),
							 MonthlyPrice = g.Where(y => y.CarPricingID == 4).Sum(z => z.Amount)
						 };
			return 0;
 */
/*
 public List<CarPricing> GetCarPricingWithTimePeriod()
		{
			//List<CarPricing> values = new List<CarPricing>();
			//using (var command = _context.Database.GetDbConnection().CreateCommand())
			//{
			//	command.CommandText = "Select * From (Select Model,PricingID,Amount From CarPricings Inner Join Cars On Cars.CarID=CarPricings.CarId Inner Join Brands On Brands.BrandID=Cars.BrandID) As SourceTable Pivot (Sum(Amount) For PricingID In ([2],[3],[4])) as PivotTable;";
			//	command.CommandType = System.Data.CommandType.Text;
			//	_context.Database.OpenConnection();
			//	using(var reader=command.ExecuteReader())
			//	{
			//		while (reader.Read())
			//		{
			//			CarPricing carPricing = new CarPricing();
			//			Enumerable.Range(1, 3).ToList().ForEach(x =>
			//			{
			//				if (DBNull.Value.Equals(reader[x]))
			//				{
			//					carPricing.
			//				}
			//				else
			//				{
			//					carPricing.Amount
			//				}
			//			});
			//			values.Add(carPricing);
			//		}
			//	}
			//	_context.Database.CloseConnection();
			//	return values;	
			}
		}
 */