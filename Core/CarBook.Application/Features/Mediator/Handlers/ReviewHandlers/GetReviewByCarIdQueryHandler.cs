using CarBook.Application.Features.Mediator.Queries.ReviewQueries;
using CarBook.Application.Features.Mediator.Results.ReviewResults;
using CarBook.Application.Interfaces.ReviewInterfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CarBook.Application.Features.Mediator.Handlers.ReviewHandlers
{
    public class GetReviewByCarIdQueryHandler
        : IRequestHandler<GetReviewByCarIdQuery, List<GetReviewByCarIdQueryResult>>
    {
        private readonly IReviewRepository _repository;
        public GetReviewByCarIdQueryHandler(IReviewRepository repository) => _repository = repository;

        public async Task<List<GetReviewByCarIdQueryResult>> Handle(GetReviewByCarIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetReviewsByCarIdAsync(request.Id, cancellationToken);

            return values.Select(x => new GetReviewByCarIdQueryResult
            {
                ReviewID = x.ReviewID,
                CarID = x.CarID,
                Comment = x.Comment,
                CustomerName = x.CustomerName,
                CustomerImage = x.CustomerImage,
                RaytingValue = x.RaytingValue,   // senin modelde böyleyse aynı kalsın
                ReviewDate = x.ReviewDate
            }).ToList();
        }
    }
}
