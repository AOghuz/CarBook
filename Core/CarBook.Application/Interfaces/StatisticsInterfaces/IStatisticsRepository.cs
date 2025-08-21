using System.Threading.Tasks;

namespace CarBook.Application.Interfaces.StatisticsInterfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetCarCountAsync();
        Task<int> GetLocationCountAsync();
        Task<int> GetAuthorCountAsync();
        Task<int> GetBlogCountAsync();
        Task<int> GetBrandCountAsync();
        Task<decimal> GetAvgRentPriceForDailyAsync();
        Task<decimal> GetAvgRentPriceForWeeklyAsync();
        Task<decimal> GetAvgRentPriceForMonthlyAsync();
        Task<int> GetCarCountByTranmissionIsAutoAsync();
        Task<string?> GetBrandNameByMaxCarAsync();
        Task<string?> GetBlogTitleByMaxBlogCommentAsync();
        Task<int> GetCarCountByKmSmallerThen1000Async();
        Task<int> GetCarCountByFuelGasolineOrDieselAsync();
        Task<int> GetCarCountByFuelElectricAsync();
        Task<string?> GetCarBrandAndModelByRentPriceDailyMaxAsync();
        Task<string?> GetCarBrandAndModelByRentPriceDailyMinAsync();
    }
}
