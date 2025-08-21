using CarBook.Application.Features.CQRS.Queries.CarQueries;
using CarBook.Application.Features.CQRS.Results.CarResults;
using CarBook.Application.Interfaces.CarInterfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CarBook.Application.Features.CQRS.Handlers.CarHandlers
{
	public class GetCarByIdQueryHandler
	{
		private readonly ICarRepository _carRepository;
		public GetCarByIdQueryHandler(ICarRepository carRepository)
		{
			_carRepository = carRepository;
		}

		public async Task<GetCarByIdQueryResult?> Handle(GetCarByIdQuery query, CancellationToken ct = default)
		{
			var values = await _carRepository.GetByIdWithBrandAsync(query.Id, ct);
			if (values is null) return null;

			return new GetCarByIdQueryResult
			{
				CarID = values.CarID,
				BrandID = values.BrandID,
				BrandName = values.Brand?.Name,   // <— BrandName set
				Model = values.Model,
				CoverImageUrl = values.CoverImageUrl,
				Km = values.Km,
				Transmission = values.Transmission,
				Seat = values.Seat,
				Luggage = values.Luggage,
				Fuel = values.Fuel,
				BigImageUrl = values.BigImageUrl
			};
		}
	}
}
