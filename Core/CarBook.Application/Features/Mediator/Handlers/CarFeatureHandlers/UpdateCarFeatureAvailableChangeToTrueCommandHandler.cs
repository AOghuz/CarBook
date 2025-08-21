using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CarBook.Application.Features.Mediator.Commands.CarFeatureCommands;
using CarBook.Application.Interfaces.CarFeatureInterfaces;

namespace CarBook.Application.Features.Mediator.Handlers.CarFeatureHandlers
{
    public class UpdateCarFeatureAvailableChangeToTrueCommandHandler
        : IRequestHandler<UpdateCarFeatureAvailableChangeToTrueCommand, Unit>
    {
        private readonly ICarFeatureRepository _repository;

        public UpdateCarFeatureAvailableChangeToTrueCommandHandler(ICarFeatureRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(UpdateCarFeatureAvailableChangeToTrueCommand request, CancellationToken cancellationToken)
        {
            await _repository.ChangeCarFeatureAvailableToTrueAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
