using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Application.Features.Mediator.Queries.CarDescriptionQueries;
using CarBook.Application.Features.Mediator.Results.CarDescriptionResults;
using CarBook.Application.Interfaces.CarDescriptionInterfaces;

namespace CarBook.Application.Features.Mediator.Handlers.CarDescriptionHandlers
{
    // Query'nizi IRequest<GetCarDescriptionQueryResult?> yapın
    public class GetCarDescriptionByCarIdQueryHandler
        : IRequestHandler<GetCarDescriptionByCarIdQuery, GetCarDescriptionQueryResult?>
    {
        private readonly ICarDescriptionRepository _repository;
        public GetCarDescriptionByCarIdQueryHandler(ICarDescriptionRepository repository)
            => _repository = repository;

        public async Task<GetCarDescriptionQueryResult?> Handle(
            GetCarDescriptionByCarIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetCarDescriptionAsync(request.Id, cancellationToken);
            if (entity is null) return null;

            return new GetCarDescriptionQueryResult
            {
                CarDescriptionID = entity.CarDescriptionID,
                CarID = entity.CarID,
                Details = entity.Details
            };
        }
    }
}
